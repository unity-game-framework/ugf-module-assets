using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

        public static T Load<T>(this IAssetModule assetModule, string id) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return assetModule.Load<T>(id, AssetLoadParameters.Default);
        }

        public static object Load(this IAssetModule assetModule, string id, Type type)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return assetModule.Load(id, type, AssetLoadParameters.Default);
        }

        public static Task<T> LoadAsync<T>(this IAssetModule assetModule, string id) where T : class
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return assetModule.LoadAsync<T>(id, AssetLoadParameters.Default);
        }

        public static Task<object> LoadAsync(this IAssetModule assetModule, string id, Type type)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return assetModule.LoadAsync(id, type, AssetLoadParameters.Default);
        }

        public static void Unload(this IAssetModule assetModule, string id, object asset)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            assetModule.Unload(id, asset, AssetUnloadParameters.Default);
        }

        public static Task UnloadAsync(this IAssetModule assetModule, string id, object asset)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            return assetModule.UnloadAsync(id, asset, AssetUnloadParameters.Default);
        }

        public static void UnloadUnused(this IAssetModule assetModule, bool resourcesUnloadUnused = true)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            var entries = new List<KeyValuePair<string, AssetTrack>>(assetModule.Tracker.Entries);

            for (int i = 0; i < entries.Count; i++)
            {
                KeyValuePair<string, AssetTrack> entry = entries[i];

                if (entry.Value.Zero)
                {
                    assetModule.Unload(entry.Key, entry.Value.Asset, AssetUnloadParameters.DefaultForce);
                }
            }

            if (resourcesUnloadUnused)
            {
                Resources.UnloadUnusedAssets();
            }
        }

        public static async Task UnloadUnusedAsync(this IAssetModule assetModule, bool resourcesUnloadUnused = true)
        {
            if (assetModule == null) throw new ArgumentNullException(nameof(assetModule));

            var entries = new List<KeyValuePair<string, AssetTrack>>(assetModule.Tracker.Entries);

            for (int i = 0; i < entries.Count; i++)
            {
                KeyValuePair<string, AssetTrack> entry = entries[i];

                if (entry.Value.Zero)
                {
                    await assetModule.UnloadAsync(entry.Key, entry.Value.Asset, AssetUnloadParameters.DefaultForce);
                }
            }

            if (resourcesUnloadUnused)
            {
                AsyncOperation operation = Resources.UnloadUnusedAssets();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }
            }
        }
    }
}
