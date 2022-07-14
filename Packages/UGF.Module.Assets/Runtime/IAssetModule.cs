using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetModule : IApplicationModule
    {
        new IAssetModuleDescription Description { get; }
        IProvider<GlobalId, IAssetLoader> Loaders { get; }
        IProvider<GlobalId, IAssetInfo> Assets { get; }
        IAssetTracker Tracker { get; }
        IContext Context { get; }

        event AssetLoadHandler Loading;
        event AssetLoadedHandler Loaded;
        event AssetUnloadHandler Unloading;
        event AssetUnloadedHandler Unloaded;

        object Load(GlobalId id, Type type, IAssetLoadParameters parameters);
        Task<object> LoadAsync(GlobalId id, Type type, IAssetLoadParameters parameters);
        void Unload(GlobalId id, object asset, IAssetUnloadParameters parameters);
        Task UnloadAsync(GlobalId id, object asset, IAssetUnloadParameters parameters);
    }
}
