using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModuleDescribed
    {
        new IAssetsModuleDescription Description { get; }
        IAssetProvider Provider { get; }

        event AssetLoadHandler Loading;
        event AssetLoadHandler Loaded;
        event AssetLoadHandler Unloading;
        event AssetLoadHandler Unloaded;

        T Load<T>(string id);
        object Load(string id, Type type);
        Task<T> LoadAsync<T>(string id);
        Task<object> LoadAsync(string id, Type type);
        void Unload<T>(T asset);
        void Unload(object asset);
        Task UnloadAsync<T>(T asset);
        Task UnloadAsync(object asset);
    }
}
