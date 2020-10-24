using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        Task<T> Load<T>(IAssetProvider provider, string id);
        Task<object> Load(IAssetProvider provider, string id, Type type);
        Task Unload<T>(IAssetProvider provider, string id, T asset);
        Task Unload(IAssetProvider provider, string id, object asset);
    }
}
