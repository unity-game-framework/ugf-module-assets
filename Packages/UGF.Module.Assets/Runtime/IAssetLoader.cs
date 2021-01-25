using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        object Load(string id, Type type, IContext context);
        Task<object> LoadAsync(string id, Type type, IContext context);
        void Unload(string id, object asset, IContext context);
        Task UnloadAsync(string id, object asset, IContext context);
    }
}
