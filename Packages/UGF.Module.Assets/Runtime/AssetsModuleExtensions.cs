using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public static class AssetsModuleExtensions
    {
        public static IAssetLoader GetLoaderByAsset(this IAssetsModule assetsModule, string id)
        {
            return TryGetLoaderByAsset(assetsModule, id, out IAssetLoader loader) ? loader : throw new ArgumentException($"Asset loader not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetLoaderByAsset(this IAssetsModule assetsModule, string id, out IAssetLoader loader)
        {
            loader = default;
            return assetsModule.Assets.TryGet(id, out IAssetInfo asset) && assetsModule.Loaders.TryGet(asset.LoaderId, out loader);
        }

        public static T Load<T>(this IAssetsModule assetsModule, string id) where T : class
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            return assetsModule.Load<T>(id, AssetLoadParameters.Default);
        }

        public static object Load(this IAssetsModule assetsModule, string id, Type type)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            return assetsModule.Load(id, type, AssetLoadParameters.Default);
        }

        public static Task<T> LoadAsync<T>(this IAssetsModule assetsModule, string id, Type type) where T : class
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            return assetsModule.LoadAsync<T>(id, AssetLoadParameters.Default);
        }

        public static Task<object> LoadAsync(this IAssetsModule assetsModule, string id, Type type)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            return assetsModule.LoadAsync(id, type, AssetLoadParameters.Default);
        }

        public static void Unload(this IAssetsModule assetsModule, string id, object asset)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            assetsModule.Unload(id, asset, AssetUnloadParameters.Default);
        }

        public static Task UnloadAsync(this IAssetsModule assetsModule, string id, object asset)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            return assetsModule.UnloadAsync(id, asset, AssetUnloadParameters.Default);
        }

        public static void UnloadUnused(this IAssetsModule assetsModule, bool resourcesUnloadUnused = true)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            var entries = new List<KeyValuePair<string, AssetTrack>>(assetsModule.Tracker.Entries);

            for (int i = 0; i < entries.Count; i++)
            {
                KeyValuePair<string, AssetTrack> entry = entries[i];

                if (entry.Value.Zero)
                {
                    assetsModule.Unload(entry.Key, entry.Value.Asset, AssetUnloadParameters.DefaultForce);
                }
            }

            if (resourcesUnloadUnused)
            {
                Resources.UnloadUnusedAssets();
            }
        }

        public static async Task UnloadUnusedAsync(this IAssetsModule assetsModule, bool resourcesUnloadUnused = true)
        {
            if (assetsModule == null) throw new ArgumentNullException(nameof(assetsModule));

            var entries = new List<KeyValuePair<string, AssetTrack>>(assetsModule.Tracker.Entries);

            for (int i = 0; i < entries.Count; i++)
            {
                KeyValuePair<string, AssetTrack> entry = entries[i];

                if (entry.Value.Zero)
                {
                    await assetsModule.UnloadAsync(entry.Key, entry.Value.Asset, AssetUnloadParameters.DefaultForce);
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
