using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModule
    {
        new IAssetsModuleDescription Description { get; }
        IProvider<string, IAssetLoader> Loaders { get; }
        IProvider<string, IAssetGroup> Groups { get; }
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
