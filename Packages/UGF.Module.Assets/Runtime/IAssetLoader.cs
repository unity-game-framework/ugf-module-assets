using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoader
    {
        IAssetLoadParameters DefaultLoadParameters { get; }
        IAssetUnloadParameters DefaultUnloadParameters { get; }

        object Load(GlobalId id, Type type, IContext context);
        object Load(GlobalId id, Type type, IAssetLoadParameters parameters, IContext context);
        Task<object> LoadAsync(GlobalId id, Type type, IContext context);
        Task<object> LoadAsync(GlobalId id, Type type, IAssetLoadParameters parameters, IContext context);
        void Unload(GlobalId id, object asset, IContext context);
        void Unload(GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context);
        Task UnloadAsync(GlobalId id, object asset, IContext context);
        Task UnloadAsync(GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context);
    }
}
