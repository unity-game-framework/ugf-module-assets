using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public static class AssetModuleExtensions
    {
        public static IAssetLoader GetLoaderByAsset(this IAssetModule assetModule, string id)
        {
            return TryGetLoaderByAsset(assetModule, id, out IAssetLoader loader) ? loader : throw new ArgumentException($"Asset loader not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetLoaderByAsset(this IAssetModule assetModule, string id, out IAssetLoader loader)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            loader = default;
            return assetModule.Assets.TryGet(id, out IAssetInfo asset) && assetModule.Loaders.TryGet(asset.LoaderId, out loader);
        }

        public static T Load<T>(this IAssetModule assetModule, string id, IAssetLoadParameters parameters) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return (T)assetModule.Load(id, typeof(T), parameters);
        }

        public static async Task<T> LoadAsync<T>(this IAssetModule assetModule, string id, IAssetLoadParameters parameters) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return (T)await assetModule.LoadAsync(id, typeof(T), parameters);
        }

        public static void UnloadForce(this IAssetModule assetModule, string id, IAssetUnloadParameters parameters)
        {
            AssetTrack track = assetModule.Tracker.Get(id);
            IAssetLoader loader = GetLoaderByAsset(assetModule, id);

            assetModule.Tracker.Remove(id);

            loader.Unload(id, track.Asset, parameters, assetModule.Context);
        }

        public static Task UnloadForceAsync(this IAssetModule assetModule, string id, IAssetUnloadParameters parameters)
        {
            AssetTrack track = assetModule.Tracker.Get(id);
            IAssetLoader loader = GetLoaderByAsset(assetModule, id);

            assetModule.Tracker.Remove(id);

            return loader.UnloadAsync(id, track.Asset, parameters, assetModule.Context);
        }
    }
}
