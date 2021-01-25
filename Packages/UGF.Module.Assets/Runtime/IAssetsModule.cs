using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModule
    {
        new IAssetsModuleDescription Description { get; }
        IAssetLoaderProvider Loaders { get; }
        IAssetGroupProvider Groups { get; }
        IAssetTracker Tracker { get; }
        IContext Context { get; }

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
