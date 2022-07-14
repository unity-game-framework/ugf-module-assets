using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TInfo, TLoadParameters, TUnloadParameters> : AssetLoaderBase
        where TInfo : class, IAssetInfo
        where TLoadParameters : class, IAssetLoadParameters
        where TUnloadParameters : class, IAssetUnloadParameters
    {
        protected AssetLoader(TLoadParameters defaultLoadParameters, TUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override object OnLoad(GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoad(info, id, type, (TLoadParameters)parameters, context);
        }

        protected override Task<object> OnLoadAsync(GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnLoadAsync(info, id, type, (TLoadParameters)parameters, context);
        }

        protected override void OnUnload(GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            OnUnload(info, id, asset, (TUnloadParameters)parameters, context);
        }

        protected override Task OnUnloadAsync(GlobalId id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            var provider = context.Get<IProvider<GlobalId, IAssetInfo>>();
            var info = provider.Get<TInfo>(id);

            return OnUnloadAsync(info, id, asset, (TUnloadParameters)parameters, context);
        }

        protected abstract object OnLoad(TInfo info, GlobalId id, Type type, TLoadParameters parameters, IContext context);
        protected abstract Task<object> OnLoadAsync(TInfo info, GlobalId id, Type type, TLoadParameters parameters, IContext context);
        protected abstract void OnUnload(TInfo info, GlobalId id, object asset, TUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(TInfo info, GlobalId id, object asset, TUnloadParameters parameters, IContext context);
    }
}
