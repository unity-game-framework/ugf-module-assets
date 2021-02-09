using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TInfo, TLoadParameters, TUnloadParameters> : AssetLoaderBase
        where TInfo : class, IAssetInfo
        where TLoadParameters : class, IAssetLoadParameters
        where TUnloadParameters : class, IAssetUnloadParameters
    {
        protected override object OnLoad(string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoad(info, id, type, (TLoadParameters)parameters, context);
        }

        protected override Task<object> OnLoadAsync(string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoadAsync(info, id, type, (TLoadParameters)parameters, context);
        }

        protected override void OnUnload(string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            OnUnload(info, id, asset, (TUnloadParameters)parameters, context);
        }

        protected override Task OnUnloadAsync(string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<string, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnUnloadAsync(info, id, asset, (TUnloadParameters)parameters, context);
        }

        protected abstract object OnLoad(TInfo info, string id, Type type, TLoadParameters parameters, IContext context);
        protected abstract Task<object> OnLoadAsync(TInfo info, string id, Type type, TLoadParameters parameters, IContext context);
        protected abstract void OnUnload(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context);
    }
}
