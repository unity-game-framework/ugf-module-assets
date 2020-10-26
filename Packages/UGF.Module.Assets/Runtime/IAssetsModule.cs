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

        T Load<T>(string id) where T : class;
        object Load(string id, Type type);
        Task<T> LoadAsync<T>(string id) where T : class;
        Task<object> LoadAsync(string id, Type type);
        void Unload<T>(string id, T asset, bool force = false) where T : class;
        void Unload(string id, object asset, bool force = false);
        Task UnloadAsync<T>(string id, T asset, bool force = false) where T : class;
        Task UnloadAsync(string id, object asset, bool force = false);
    }
}
