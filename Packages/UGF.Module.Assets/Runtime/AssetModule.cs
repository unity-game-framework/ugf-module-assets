using System;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime
{
    public class AssetModule : ApplicationModuleAsync<AssetModuleDescription>, IAssetModule
    {
        public IProvider<GlobalId, IAssetLoader> Loaders { get; }
        public IProvider<GlobalId, IAssetInfo> Assets { get; }
        public IAssetTracker Tracker { get; }
        public IContext Context { get; } = new Context();

        IAssetModuleDescription IAssetModule.Description { get { return Description; } }

        public event AssetLoadHandler Loading;
        public event AssetLoadedHandler Loaded;
        public event AssetUnloadHandler Unloading;
        public event AssetUnloadedHandler Unloaded;

        private ILog m_logger;

        public AssetModule(AssetModuleDescription description, IApplication application) : this(description, application, new Provider<GlobalId, IAssetLoader>(), new Provider<GlobalId, IAssetInfo>(), new AssetTracker())
        {
        }

        public AssetModule(AssetModuleDescription description, IApplication application, IProvider<GlobalId, IAssetLoader> loaders, IProvider<GlobalId, IAssetInfo> assets, IAssetTracker tracker) : base(description, application)
        {
            Loaders = loaders ?? throw new ArgumentNullException(nameof(loaders));
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));

            Context.Add(Application);
            Context.Add(Loaders);
            Context.Add(Assets);

            m_logger = Log.CreateWithLabel<AssetModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((GlobalId key, IAssetLoader value) in Description.Loaders)
            {
                Loaders.Add(key, value);
            }

            foreach ((GlobalId key, IAssetInfo value) in Description.Assets)
            {
                Assets.Add(key, value);
            }

            m_logger.Debug("Assets Module initialized", new
            {
                loadersCount = Loaders.Entries.Count,
                assetsCount = Assets.Entries.Count
            });

            for (int i = 0; i < Description.PreloadAssets.Count; i++)
            {
                GlobalId id = Description.PreloadAssets[i];

                this.Load<Object>(id);
            }

            m_logger.Debug("Assets Module preload", new
            {
                count = Description.PreloadAssets.Count
            });
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            for (int i = 0; i < Description.PreloadAssetsAsync.Count; i++)
            {
                GlobalId id = Description.PreloadAssetsAsync[i];

                await this.LoadAsync<Object>(id);
            }

            m_logger.Debug("Assets Module preload async", new
            {
                count = Description.PreloadAssetsAsync.Count
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadTrackedAssetsOnUninitialize)
            {
                m_logger.Debug("Assets Module unload tracked assets on uninitialize", new
                {
                    count = Tracker.Entries.Count
                });

                while (Tracker.Entries.Count > 0)
                {
                    (GlobalId key, AssetTrack value) = Tracker.Entries.First();

                    this.Unload(key, value.Asset);
                }
            }

            Tracker.Clear();

            foreach ((GlobalId key, _) in Description.Loaders)
            {
                Loaders.Remove(key);
            }

            foreach ((GlobalId key, _) in Description.Assets)
            {
                Assets.Remove(key);
            }
        }

        public object Load(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Loading?.Invoke(id, type, parameters);

            object asset = OnLoad(id, type, parameters);

            Loaded?.Invoke(id, asset, parameters);

            return asset;
        }

        public async Task<object> LoadAsync(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Loading?.Invoke(id, type, parameters);

            object asset = await OnLoadAsync(id, type, parameters);

            Loaded?.Invoke(id, asset, parameters);

            return asset;
        }

        public void Unload(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset, parameters);

            OnUnload(id, asset, parameters);

            Unloaded?.Invoke(id, type, parameters);
        }

        public async Task UnloadAsync(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset, parameters);

            await OnUnloadAsync(id, asset, parameters);

            Unloaded?.Invoke(id, type, parameters);
        }

        protected virtual object OnLoad(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            if (Tracker.TryGet(id, out AssetTrack track))
            {
                track = Tracker.Increment(id);
            }
            else
            {
                object asset = LoadAsset(id, type, parameters);

                Tracker.Track(id, asset, out track);
            }

            return track.Asset;
        }

        protected virtual async Task<object> OnLoadAsync(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            if (Tracker.TryGet(id, out AssetTrack track))
            {
                track = Tracker.Increment(id);
            }
            else
            {
                object asset = await LoadAssetAsync(id, type, parameters);

                Tracker.Track(id, asset, out track);
            }

            return track.Asset;
        }

        protected virtual void OnUnload(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            if (Tracker.UnTrack(id, asset, out _))
            {
                Tracker.Remove(id);
                UnloadAsset(id, asset, parameters);
            }
        }

        protected virtual Task OnUnloadAsync(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            if (Tracker.UnTrack(id, asset, out _))
            {
                Tracker.Remove(id);

                return UnloadAssetAsync(id, asset, parameters);
            }

            return Task.CompletedTask;
        }

        protected virtual object LoadAsset(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            object asset = loader.Load(id, type, parameters, Context);

            return asset;
        }

        protected virtual Task<object> LoadAssetAsync(GlobalId id, Type type, IAssetLoadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            Task<object> task = loader.LoadAsync(id, type, parameters, Context);

            return task;
        }

        protected virtual void UnloadAsset(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            loader.Unload(id, asset, parameters, Context);
        }

        protected virtual Task UnloadAssetAsync(GlobalId id, object asset, IAssetUnloadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            Task task = loader.UnloadAsync(id, asset, parameters, Context);

            return task;
        }
    }
}
