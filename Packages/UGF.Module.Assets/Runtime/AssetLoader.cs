using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TInfo> : AssetLoaderBase where TInfo : class, IAssetInfo
    {
        protected override object OnLoad(string id, Type type, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoad(info, id, type, context);
        }

        protected override Task<object> OnLoadAsync(string id, Type type, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoadAsync(info, id, type, context);
        }

        protected override void OnUnload(string id, object asset, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            OnUnload(info, id, asset, context);
        }

        protected override Task OnUnloadAsync(string id, object asset, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnUnloadAsync(info, id, asset, context);
        }

        protected abstract object OnLoad(TInfo info, string id, Type type, IContext context);
        protected abstract Task<object> OnLoadAsync(TInfo info, string id, Type type, IContext context);
        protected abstract void OnUnload(TInfo info, string id, object asset, IContext context);
        protected abstract Task OnUnloadAsync(TInfo info, string id, object asset, IContext context);
    }
}
