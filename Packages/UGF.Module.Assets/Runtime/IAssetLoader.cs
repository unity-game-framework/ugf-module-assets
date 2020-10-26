using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        object Load(IAssetProvider provider, string id, Type type);
        Task<object> LoadAsync(IAssetProvider provider, string id, Type type);
        void Unload(IAssetProvider provider, string id, object asset);
        Task UnloadAsync(IAssetProvider provider, string id, object asset);
    }
}
