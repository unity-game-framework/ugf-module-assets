using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        T Load<T>(IAssetProvider provider, string id) where T : class;
        object Load(IAssetProvider provider, string id, Type type);
        Task<T> LoadAsync<T>(IAssetProvider provider, string id) where T : class;
        Task<object> LoadAsync(IAssetProvider provider, string id, Type type);
        void Unload<T>(IAssetProvider provider, string id, T asset) where T : class;
        void Unload(IAssetProvider provider, string id, object asset);
        Task UnloadAsync<T>(IAssetProvider provider, string id, T asset) where T : class;
        Task UnloadAsync(IAssetProvider provider, string id, object asset);
    }
}
