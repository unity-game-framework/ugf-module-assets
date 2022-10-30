using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime.Loaders.AssetDatabases
{
    public partial class AssetDatabaseAssetLoader : AssetLoader<IAssetInfo>
    {
        protected override object OnLoad(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            return OnLoadAssetDatabase(info, id, type, parameters, context);
        }

        protected override Task<object> OnLoadAsync(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            return Task.FromResult(OnLoadAssetDatabase(info, id, type, parameters, context));
        }

        protected override void OnUnload(IAssetInfo info, GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
        }

        protected override Task OnUnloadAsync(IAssetInfo info, GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            return Task.CompletedTask;
        }

        private partial object OnLoadAssetDatabase(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context);
    }
}
