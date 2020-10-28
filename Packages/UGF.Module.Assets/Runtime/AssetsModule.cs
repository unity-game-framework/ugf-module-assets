using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModule : ApplicationModuleDescribed<AssetsModuleDescription>, IAssetsModule, IApplicationModuleAsync
    {
        public IAssetProvider Provider { get; }
        public IAssetTracker Tracker { get; }

        IAssetsModuleDescription IAssetsModule.Description { get { return Description; } }

        public event AssetLoadHandler Loading;
        public event AssetLoadedHandler Loaded;
        public event AssetUnloadHandler Unloading;
        public event AssetUnloadedHandler Unloaded;

        public AssetsModule(IApplication application, AssetsModuleDescription description, IAssetProvider provider = null, IAssetTracker tracker = null) : base(application, description)
        {
            Provider = provider ?? new AssetProvider();
            Tracker = tracker ?? new AssetTracker();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, IAssetLoader> pair in Description.Loaders)
            {
                Provider.AddLoader(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, IAssetGroup> pair in Description.Groups)
            {
                Provider.AddGroup(pair.Key, pair.Value);
            }

            Log.Debug("Assets Module initialized", new
            {
                loadersCount = Provider.Loaders.Count,
                groupsCount = Provider.Groups.Count
            });

            for (int i = 0; i < Description.PreloadAssets.Count; i++)
            {
                string id = Description.PreloadAssets[i];

                Load<Object>(id, AssetLoadParameters.Default);
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

                await LoadAsync<Object>(id, AssetLoadParameters.Default);
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
                    count = Tracker.Tracks.Count
                });

                while (Tracker.Tracks.Count > 0)
                {
                    KeyValuePair<string, AssetTrack> pair = Tracker.Tracks.First();

                    Unload(pair.Key, pair.Value.Asset, AssetUnloadParameters.DefaultForce);
                }
            }

            Tracker.Clear();

            foreach (KeyValuePair<string, IAssetLoader> pair in Description.Loaders)
            {
                Provider.RemoveLoader(pair.Key);
            }

            foreach (KeyValuePair<string, IAssetGroup> pair in Description.Groups)
            {
                Provider.RemoveGroup(pair.Key);
            }
        }

        public T Load<T>(string id, AssetLoadParameters parameters) where T : class
        {
            return (T)Load(id, typeof(T), parameters);
        }

        public async Task<T> LoadAsync<T>(string id, AssetLoadParameters parameters) where T : class
        {
            return (T)await LoadAsync(id, typeof(T), parameters);
        }

        public object Load(string id, Type type, AssetLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Loading?.Invoke(id, type);

            object asset = OnLoad(id, type, parameters);

            Loaded?.Invoke(id, asset);

            return asset;
        }

        public async Task<object> LoadAsync(string id, Type type, AssetLoadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Loading?.Invoke(id, type);

            object asset = await OnLoadAsync(id, type, parameters);

            Loaded?.Invoke(id, asset);

            return asset;
        }

        public void Unload(string id, object asset, AssetUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset);

            OnUnload(id, asset, parameters);

            Unloaded?.Invoke(id, type);
        }

        public async Task UnloadAsync(string id, object asset, AssetUnloadParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset);

            await OnUnloadAsync(id, asset, parameters);

            Unloaded?.Invoke(id, type);
        }

        protected virtual object OnLoad(string id, Type type, AssetLoadParameters parameters)
        {
            switch (parameters.Mode)
            {
                case AssetLoadMode.Track:
                {
                    if (Tracker.TryGet(id, out AssetTrack track))
                    {
                        track = Tracker.Increment(id);
                    }
                    else
                    {
                        object asset = LoadAsset(id, type);

                        Tracker.Track(id, asset, out track);
                    }

                    return track.Asset;
                }
                case AssetLoadMode.Direct:
                {
                    return LoadAsset(id, type);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameters.Mode), $"Invalid asset load mode specified: '{parameters.Mode}'.");
            }
        }

        protected virtual async Task<object> OnLoadAsync(string id, Type type, AssetLoadParameters parameters)
        {
            switch (parameters.Mode)
            {
                case AssetLoadMode.Track:
                {
                    if (Tracker.TryGet(id, out AssetTrack track))
                    {
                        track = Tracker.Increment(id);
                    }
                    else
                    {
                        object asset = await LoadAssetAsync(id, type);

                        Tracker.Track(id, asset, out track);
                    }

                    return track.Asset;
                }
                case AssetLoadMode.Direct:
                {
                    return await LoadAssetAsync(id, type);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameters.Mode), $"Invalid asset load mode specified: '{parameters.Mode}'.");
            }
        }

        protected virtual void OnUnload(string id, object asset, AssetUnloadParameters parameters)
        {
            switch (parameters.Mode)
            {
                case AssetLoadMode.Track:
                {
                    if (parameters.Force || Tracker.UnTrack(id, out _))
                    {
                        Tracker.Remove(id);
                        UnloadAsset(id, asset);
                    }

                    break;
                }
                case AssetLoadMode.Direct:
                {
                    UnloadAsset(id, asset);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameters.Mode), $"Invalid asset load mode specified: '{parameters.Mode}'.");
            }
        }

        protected virtual Task OnUnloadAsync(string id, object asset, AssetUnloadParameters parameters)
        {
            switch (parameters.Mode)
            {
                case AssetLoadMode.Track:
                {
                    if (parameters.Force || Tracker.UnTrack(id, out _))
                    {
                        Tracker.Remove(id);

                        return UnloadAssetAsync(id, asset);
                    }

                    return Task.CompletedTask;
                }
                case AssetLoadMode.Direct:
                {
                    return UnloadAssetAsync(id, asset);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameters.Mode), $"Invalid asset load mode specified: '{parameters.Mode}'.");
            }
        }

        protected virtual object LoadAsset(string id, Type type)
        {
            IAssetLoader loader = GetLoaderByAsset(id);

            object asset = loader.Load(Provider, id, type);

            return asset;
        }

        protected virtual async Task<object> LoadAssetAsync(string id, Type type)
        {
            IAssetLoader loader = GetLoaderByAsset(id);

            object asset = await loader.LoadAsync(Provider, id, type);

            return asset;
        }

        protected virtual void UnloadAsset(string id, object asset)
        {
            IAssetLoader loader = GetLoaderByAsset(id);

            loader.Unload(Provider, id, asset);
        }

        protected virtual Task UnloadAssetAsync(string id, object asset)
        {
            IAssetLoader loader = GetLoaderByAsset(id);

            Task task = loader.UnloadAsync(Provider, id, asset);

            return task;
        }

        protected IAssetLoader GetLoaderByAsset(string id)
        {
            return TryGetLoaderByAsset(id, out IAssetLoader loader) ? loader : throw new ArgumentException($"Asset loader not found by the specified asset id: '{id}'.");
        }

        protected bool TryGetLoaderByAsset(string id, out IAssetLoader loader)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            loader = default;
            return Provider.TryGetGroupByAsset(id, out IAssetGroup group) && Provider.TryGetLoader(group.LoaderId, out loader);
        }
    }
}
