using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        object Load(string id, Type type, IAssetLoadParameters parameters, IContext context);
        Task<object> LoadAsync(string id, Type type, IAssetLoadParameters parameters, IContext context);
        void Unload(string id, object asset, IAssetUnloadParameters parameters, IContext context);
        Task UnloadAsync(string id, object asset, IAssetUnloadParameters parameters, IContext context);
    }
}
