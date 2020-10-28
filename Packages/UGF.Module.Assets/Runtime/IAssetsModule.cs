using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModuleDescribed
    {
        new IAssetsModuleDescription Description { get; }
        IAssetProvider Provider { get; }
        IAssetTracker Tracker { get; }

        event AssetLoadHandler Loading;
        event AssetLoadedHandler Loaded;
        event AssetUnloadHandler Unloading;
        event AssetUnloadedHandler Unloaded;

        T Load<T>(string id, AssetLoadParameters parameters) where T : class;
        Task<T> LoadAsync<T>(string id, AssetLoadParameters parameters) where T : class;
        object Load(string id, Type type, AssetLoadParameters parameters);
        Task<object> LoadAsync(string id, Type type, AssetLoadParameters parameters);
        void Unload(string id, object asset, AssetUnloadParameters parameters);
        Task UnloadAsync(string id, object asset, AssetUnloadParameters parameters);
    }
}
