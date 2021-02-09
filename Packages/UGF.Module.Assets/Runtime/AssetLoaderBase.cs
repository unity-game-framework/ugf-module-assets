using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoaderBase : IAssetLoader
    {
        public object Load(string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoad(id, type, parameters, context);
        }

        public Task<object> LoadAsync(string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnLoadAsync(id, type, parameters, context);
        }

        public void Unload(string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            OnUnload(id, asset, parameters, context);
        }

        public Task UnloadAsync(string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (context == null) throw new ArgumentNullException(nameof(context));

            return OnUnloadAsync(id, asset, parameters, context);
        }

        protected abstract object OnLoad(string id, Type type, IAssetLoadParameters parameters, IContext context);
        protected abstract Task<object> OnLoadAsync(string id, Type type, IAssetLoadParameters parameters, IContext context);
        protected abstract void OnUnload(string id, object asset, IAssetUnloadParameters parameters, IContext context);
        protected abstract Task OnUnloadAsync(string id, object asset, IAssetUnloadParameters parameters, IContext context);
    }
}
