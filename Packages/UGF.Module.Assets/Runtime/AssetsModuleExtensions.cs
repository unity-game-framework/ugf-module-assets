using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public static class AssetsModuleExtensions
    {
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
    }
}
