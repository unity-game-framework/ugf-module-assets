using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoaderBase : IAssetLoader
    {
        public Task<T> Load<T>(IAssetProvider provider, string id)
        {
            return OnLoad<T>(provider, id);
        }

        public Task<object> Load(IAssetProvider provider, string id, Type type)
        {
            return OnLoad(provider, id, type);
        }

        public Task Unload<T>(IAssetProvider provider, string id, T asset)
        {
            return OnUnload(provider, id, asset);
        }

        public Task Unload(IAssetProvider provider, string id, object asset)
        {
            return OnUnload(provider, id, asset);
        }

        protected virtual async Task<T> OnLoad<T>(IAssetProvider provider, string id)
        {
            return (T)await OnLoad(provider, id, typeof(T));
        }

        protected virtual Task OnUnload<T>(IAssetProvider provider, string id, T asset)
        {
            return OnUnload(provider, id, (object)asset);
        }

        protected abstract Task<object> OnLoad(IAssetProvider provider, string id, Type type);
        protected abstract Task OnUnload(IAssetProvider provider, string id, object asset);
    }
}
