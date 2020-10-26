using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModule : ApplicationModuleDescribed<AssetsModuleDescription>, IAssetsModule
    {
        public IAssetProvider Provider { get; }
        public IAssetTracker Tracker { get; }

        IAssetsModuleDescription IAssetsModule.Description { get { return Description; } }

        public event AssetLoadHandler Loading;
        public event AssetLoadedHandler Loaded;
        public event AssetUnloadHandler Unloading;
        public event AssetUnloadedHandler Unloaded;

        public AssetsModule(IApplication application, AssetsModuleDescription description, IAssetProvider provider, IAssetTracker tracker = null) : base(application, description)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
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
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (Description.UnloadTrackedAssetsOnUninitialize)
            {
                while (Tracker.Tracks.Count > 0)
                {
                    KeyValuePair<string, AssetTrack> pair = Tracker.Tracks.First();

                    Unload(pair.Key, pair.Value.Asset, true);
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

        public T Load<T>(string id) where T : class
        {
            return (T)Load(id, typeof(T));
        }

        public object Load(string id, Type type)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Loading?.Invoke(id, type);

            object asset = OnLoad(id, type);

            Loaded?.Invoke(id, asset);

            return asset;
        }

        public async Task<T> LoadAsync<T>(string id) where T : class
        {
            return (T)await LoadAsync(id, typeof(T));
        }

        public async Task<object> LoadAsync(string id, Type type)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Loading?.Invoke(id, type);

            object asset = await OnLoadAsync(id, type);

            Loaded?.Invoke(id, asset);

            return asset;
        }

        public void Unload<T>(string id, T asset, bool force = false) where T : class
        {
            Unload(id, (object)asset);
        }

        public void Unload(string id, object asset, bool force = false)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset);

            OnUnload(id, asset, force);

            Unloaded?.Invoke(id, type);
        }

        public Task UnloadAsync<T>(string id, T asset, bool force = false) where T : class
        {
            return UnloadAsync(id, (object)asset, force);
        }

        public async Task UnloadAsync(string id, object asset, bool force = false)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            Type type = asset.GetType();

            Unloading?.Invoke(id, asset);

            await OnUnloadAsync(id, asset, force);

            Unloaded?.Invoke(id, type);
        }

        protected virtual object OnLoad(string id, Type type)
        {
            if (!Tracker.TryGet(id, out AssetTrack track))
            {
                IAssetLoader loader = GetLoaderByAsset(id);

                object asset = loader.Load(Provider, id, type);

                track = Tracker.Add(id, asset);
            }

            track++;

            Tracker.Update(id, track);

            return track.Asset;
        }

        protected virtual async Task<object> OnLoadAsync(string id, Type type)
        {
            if (!Tracker.TryGet(id, out AssetTrack track))
            {
                IAssetLoader loader = GetLoaderByAsset(id);

                object asset = await loader.LoadAsync(Provider, id, type);

                track = Tracker.Add(id, asset);
            }

            track++;

            Tracker.Update(id, track);

            return track.Asset;
        }

        protected virtual void OnUnload(string id, object asset, bool force)
        {
            AssetTrack track = Tracker.Get(id);

            track--;

            Tracker.Update(id, track);

            if (track.Zero || force)
            {
                Tracker.Remove(id);

                IAssetLoader loader = GetLoaderByAsset(id);

                loader.Unload(Provider, id, asset);
            }
        }

        protected virtual Task OnUnloadAsync(string id, object asset, bool force)
        {
            AssetTrack track = Tracker.Get(id);

            track--;

            Tracker.Update(id, track);

            if (track.Zero || force)
            {
                Tracker.Remove(id);

                IAssetLoader loader = GetLoaderByAsset(id);

                return loader.UnloadAsync(Provider, id, asset);
            }

            return Task.CompletedTask;
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
