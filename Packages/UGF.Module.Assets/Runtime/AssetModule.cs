using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime
{
    public class AssetModule : ApplicationModule<AssetModuleDescription>, IAssetModule, IApplicationModuleAsync
    {
        public IProvider<string, IAssetLoader> Loaders { get; }
        public IProvider<string, IAssetInfo> Assets { get; }
        public IAssetTracker Tracker { get; }
        public IContext Context { get; } = new Context();

        IAssetModuleDescription IAssetModule.Description { get { return Description; } }

        public event AssetLoadHandler Loading;
        public event AssetLoadedHandler Loaded;
        public event AssetUnloadHandler Unloading;
        public event AssetUnloadedHandler Unloaded;

        public AssetModule(AssetModuleDescription description, IApplication application) : this(description, application, new Provider<string, IAssetLoader>(), new Provider<string, IAssetInfo>(), new AssetTracker())
        {
        }

        public AssetModule(AssetModuleDescription description, IApplication application, IProvider<string, IAssetLoader> loaders, IProvider<string, IAssetInfo> assets, IAssetTracker tracker) : base(description, application)
        {
            Loaders = loaders ?? throw new ArgumentNullException(nameof(loaders));
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));

            Context.Add(Application);
            Context.Add(Loaders);
            Context.Add(Assets);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, IAssetLoader> pair in Description.Loaders)
            {
                Loaders.Add(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, IAssetInfo> pair in Description.Assets)
            {
                Assets.Add(pair.Key, pair.Value);
            }

            Log.Debug("Assets Module initialized", new
            {
                loadersCount = Loaders.Entries.Count,
                assetsCount = Assets.Entries.Count
            });

            for (int i = 0; i < Description.PreloadAssets.Count; i++)
            {
                string id = Description.PreloadAssets[i];

                this.Load<Object>(id, AssetLoadParameters.Empty);
            }

            Log.Debug("Assets Module preload", new
            {
                count = Description.PreloadAssets.Count
            });
        }

        public async Task InitializeAsync()
        {
            for (int i = 0; i < Description.PreloadAssetsAsync.Count; i++)
            {
                string id = Description.PreloadAssetsAsync[i];

                await this.LoadAsync<Object>(id, AssetLoadParameters.Empty);
            }

            Log.Debug("Assets Module preload async", new
            {
                count = Description.PreloadAssetsAsync.Count
            });
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadTrackedAssetsOnUninitialize)
            {
                Log.Debug("Assets Module unload tracked assets on uninitialize", new
                {
                    count = Tracker.Entries.Count
                });

                while (Tracker.Entries.Count > 0)
                {
                    KeyValuePair<string, AssetTrack> pair = Tracker.Entries.First();

                    Unload(pair.Key, pair.Value.Asset, AssetUnloadParameters.Empty);
                }
            }

            Tracker.Clear();

            foreach (KeyValuePair<string, IAssetLoader> pair in Description.Loaders)
            {
                Loaders.Remove(pair.Key);
            }

            foreach (KeyValuePair<string, IAssetInfo> pair in Description.Assets)
            {
                Assets.Remove(pair.Key);
            }
        }

        public object Load(string id, Type type, IAssetLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Loading?.Invoke(id, type, parameters);

            object asset = OnLoad(id, type, parameters);

            Loaded?.Invoke(id, asset, parameters);

            return asset;
        }

        public async Task<object> LoadAsync(string id, Type type, IAssetLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Loading?.Invoke(id, type, parameters);

            object asset = await OnLoadAsync(id, type, parameters);

            Loaded?.Invoke(id, asset, parameters);

            return asset;
        }

        public void Unload(string id, object asset, IAssetUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset, parameters);

            OnUnload(id, asset, parameters);

            Unloaded?.Invoke(id, type, parameters);
        }

        public async Task UnloadAsync(string id, object asset, IAssetUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset, parameters);

            await OnUnloadAsync(id, asset, parameters);

            Unloaded?.Invoke(id, type, parameters);
        }

        protected virtual object OnLoad(string id, Type type, IAssetLoadParameters parameters)
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

        protected virtual async Task<object> OnLoadAsync(string id, Type type, IAssetLoadParameters parameters)
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

        protected virtual void OnUnload(string id, object asset, IAssetUnloadParameters parameters)
        {
            if (Tracker.UnTrack(id, out _))
            {
                Tracker.Remove(id);
                UnloadAsset(id, asset, parameters);
            }
        }

        protected virtual Task OnUnloadAsync(string id, object asset, IAssetUnloadParameters parameters)
        {
            if (Tracker.UnTrack(id, out _))
            {
                Tracker.Remove(id);

                return UnloadAssetAsync(id, asset, parameters);
            }

            return Task.CompletedTask;
        }

        protected virtual object LoadAsset(string id, Type type, IAssetLoadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            object asset = loader.Load(id, type, Context);

            return asset;
        }

        protected virtual Task<object> LoadAssetAsync(string id, Type type, IAssetLoadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            Task<object> task = loader.LoadAsync(id, type, Context);

            return task;
        }

        protected virtual void UnloadAsset(string id, object asset, IAssetUnloadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            loader.Unload(id, asset, Context);
        }

        protected virtual Task UnloadAssetAsync(string id, object asset, IAssetUnloadParameters parameters)
        {
            IAssetLoader loader = this.GetLoaderByAsset(id);

            Task task = loader.UnloadAsync(id, asset, Context);

            return task;
        }
    }
}
