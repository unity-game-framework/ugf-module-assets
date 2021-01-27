using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetModule : IApplicationModule
    {
        new IAssetModuleDescription Description { get; }
        IProvider<string, IAssetLoader> Loaders { get; }
        IProvider<string, IAssetInfo> Assets { get; }
        IAssetTracker Tracker { get; }
        IContext Context { get; }

        event AssetLoadHandler Loading;
        event AssetLoadedHandler Loaded;
        event AssetUnloadHandler Unloading;
        event AssetUnloadedHandler Unloaded;

        object Load(string id, Type type, IAssetLoadParameters parameters);
        Task<object> LoadAsync(string id, Type type, IAssetLoadParameters parameters);
        void Unload(string id, object asset, IAssetUnloadParameters parameters);
        Task UnloadAsync(string id, object asset, IAssetUnloadParameters parameters);
    }
}
