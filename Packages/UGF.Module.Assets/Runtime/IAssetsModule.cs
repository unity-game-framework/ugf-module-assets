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

        T Load<T>(string id) where T : class;
        object Load(string id, Type type);
        Task<T> LoadAsync<T>(string id) where T : class;
        Task<object> LoadAsync(string id, Type type);
        void Unload<T>(T asset) where T : class;
        void Unload(object asset);
        Task UnloadAsync<T>(T asset) where T : class;
        Task UnloadAsync(object asset);
    }
}
