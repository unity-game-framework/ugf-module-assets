using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TGroup, TInfo> : AssetLoaderBase
        where TGroup : class, IAssetGroup
        where TInfo : class, IAssetInfo
    {
        protected override object OnLoad(IAssetProvider provider, string id, Type type)
        {
            var group = (TGroup)provider.GetGroupByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnLoad(provider, group, info, id, type);
        }

        protected override Task<object> OnLoadAsync(IAssetProvider provider, string id, Type type)
        {
            var group = (TGroup)provider.GetGroupByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnLoadAsync(provider, group, info, id, type);
        }

        protected override void OnUnload(IAssetProvider provider, string id, object asset)
        {
            var group = (TGroup)provider.GetGroupByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            OnUnload(provider, group, info, id, asset);
        }

        protected override Task OnUnloadAsync(IAssetProvider provider, string id, object asset)
        {
            var group = (TGroup)provider.GetGroupByAsset(id);
            var info = group.GetInfo<TInfo>(id);

            return OnUnloadAsync(provider, group, info, id, asset);
        }

        protected abstract object OnLoad(IAssetProvider provider, TGroup group, TInfo info, string id, Type type);
        protected abstract Task<object> OnLoadAsync(IAssetProvider provider, TGroup group, TInfo info, string id, Type type);
        protected abstract void OnUnload(IAssetProvider provider, TGroup group, TInfo info, string id, object asset);
        protected abstract Task OnUnloadAsync(IAssetProvider provider, TGroup group, TInfo info, string id, object asset);
    }
}
