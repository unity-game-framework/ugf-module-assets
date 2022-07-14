using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public static class AssetModuleExtensions
    {
        public static IAssetLoadParameters GetDefaultLoadParametersByAsset(this IAssetModule assetModule, GlobalId id)
        {
            return TryGetDefaultLoadParametersByAsset(assetModule, id, out IAssetLoadParameters parameters) ? parameters : throw new ArgumentException($"Asset load parameters not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetDefaultLoadParametersByAsset(this IAssetModule assetModule, GlobalId id, out IAssetLoadParameters parameters)
        {
            if (TryGetLoaderByAsset(assetModule, id, out IAssetLoader loader))
            {
                parameters = loader.DefaultLoadParameters;
                return true;
            }

            parameters = null;
            return false;
        }

        public static IAssetUnloadParameters GetDefaultUnloadParametersByAsset(this IAssetModule assetModule, GlobalId id)
        {
            return TryGetDefaultUnloadParametersByAsset(assetModule, id, out IAssetUnloadParameters parameters) ? parameters : throw new ArgumentException($"Asset unload parameters not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetDefaultUnloadParametersByAsset(this IAssetModule assetModule, GlobalId id, out IAssetUnloadParameters parameters)
        {
            if (TryGetLoaderByAsset(assetModule, id, out IAssetLoader loader))
            {
                parameters = loader.DefaultUnloadParameters;
                return true;
            }

            parameters = null;
            return false;
        }

        public static IAssetLoader GetLoaderByAsset(this IAssetModule assetModule, GlobalId id)
        {
            return TryGetLoaderByAsset(assetModule, id, out IAssetLoader loader) ? loader : throw new ArgumentException($"Asset loader not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetLoaderByAsset(this IAssetModule assetModule, GlobalId id, out IAssetLoader loader)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            loader = default;
            return assetModule.Assets.TryGet(id, out IAssetInfo asset) && assetModule.Loaders.TryGet(asset.LoaderId, out loader);
        }

        public static T Load<T>(this IAssetModule assetModule, GlobalId id, IAssetLoadParameters parameters = null) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            parameters ??= GetDefaultLoadParametersByAsset(assetModule, id);

            return (T)assetModule.Load(id, typeof(T), parameters);
        }

        public static async Task<T> LoadAsync<T>(this IAssetModule assetModule, GlobalId id, IAssetLoadParameters parameters = null) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            parameters ??= GetDefaultLoadParametersByAsset(assetModule, id);

            return (T)await assetModule.LoadAsync(id, typeof(T), parameters);
        }

        public static object Load(this IAssetModule assetModule, GlobalId id, Type type)
        {
            IAssetLoadParameters parameters = GetDefaultLoadParametersByAsset(assetModule, id);

            return assetModule.Load(id, type, parameters);
        }

        public static Task<object> LoadAsync(this IAssetModule assetModule, GlobalId id, Type type)
        {
            IAssetLoadParameters parameters = GetDefaultLoadParametersByAsset(assetModule, id);

            return assetModule.LoadAsync(id, type, parameters);
        }

        public static void Unload(this IAssetModule assetModule, GlobalId id, object asset)
        {
            IAssetUnloadParameters parameters = GetDefaultUnloadParametersByAsset(assetModule, id);

            assetModule.Unload(id, asset, parameters);
        }

        public static Task UnloadAsync(this IAssetModule assetModule, GlobalId id, object asset)
        {
            IAssetUnloadParameters parameters = GetDefaultUnloadParametersByAsset(assetModule, id);

            return assetModule.UnloadAsync(id, asset, parameters);
        }

        public static void UnloadForce(this IAssetModule assetModule, GlobalId id, IAssetUnloadParameters parameters = null)
        {
            parameters ??= GetDefaultUnloadParametersByAsset(assetModule, id);

            AssetTrack track = assetModule.Tracker.Get(id);
            IAssetLoader loader = GetLoaderByAsset(assetModule, id);

            assetModule.Tracker.Remove(id);

            loader.Unload(id, track.Asset, parameters, assetModule.Context);
        }

        public static Task UnloadForceAsync(this IAssetModule assetModule, GlobalId id, IAssetUnloadParameters parameters = null)
        {
            parameters ??= GetDefaultUnloadParametersByAsset(assetModule, id);

            AssetTrack track = assetModule.Tracker.Get(id);
            IAssetLoader loader = GetLoaderByAsset(assetModule, id);

            assetModule.Tracker.Remove(id);

            return loader.UnloadAsync(id, track.Asset, parameters, assetModule.Context);
        }
    }
}
