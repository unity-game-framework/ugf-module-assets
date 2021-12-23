using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime.Loaders.Referenced
{
    public class ReferencedAssetLoader : AssetLoader<ReferencedAssetInfo>
    {
        protected override object OnLoad(ReferencedAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            return info.Asset;
        }

        protected override Task<object> OnLoadAsync(ReferencedAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            return Task.FromResult<object>(info.Asset);
        }

        protected override void OnUnload(ReferencedAssetInfo info, string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
        }

        protected override Task OnUnloadAsync(ReferencedAssetInfo info, string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            return Task.CompletedTask;
        }
    }
}
