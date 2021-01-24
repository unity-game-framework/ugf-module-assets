using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TGroup, TInfo> : AssetLoaderBase
        where TGroup : class, IAssetGroup
        where TInfo : class, IAssetInfo
    {
        protected override object OnLoad(string id, Type type, IContext context)
        {
            var provider = context.Get<IAssetGroupProvider>();
            var group = (TGroup)provider.GetByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnLoad(group, info, id, type, context);
        }

        protected override Task<object> OnLoadAsync(string id, Type type, IContext context)
        {
            var provider = context.Get<IAssetGroupProvider>();
            var group = (TGroup)provider.GetByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnLoadAsync(group, info, id, type, context);
        }

        protected override void OnUnload(string id, object asset, IContext context)
        {
            var provider = context.Get<IAssetGroupProvider>();
            var group = (TGroup)provider.GetByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            OnUnload(group, info, id, asset, context);
        }

        protected override Task OnUnloadAsync(string id, object asset, IContext context)
        {
            var provider = context.Get<IAssetGroupProvider>();
            var group = (TGroup)provider.GetByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnUnloadAsync(group, info, id, asset, context);
        }

        protected abstract object OnLoad(TGroup group, TInfo info, string id, Type type, IContext context);
        protected abstract Task<object> OnLoadAsync(TGroup group, TInfo info, string id, Type type, IContext context);
        protected abstract void OnUnload(TGroup group, TInfo info, string id, object asset, IContext context);
        protected abstract Task OnUnloadAsync(TGroup group, TInfo info, string id, object asset, IContext context);
    }
}
