using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoaderBase : IAssetLoader
    {
        public object Load(IAssetProvider provider, string id, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            return OnLoad(provider, id, type);
        }

        public Task<object> LoadAsync(IAssetProvider provider, string id, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            return OnLoadAsync(provider, id, type);
        }

        public void Unload(IAssetProvider provider, string id, object asset)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            OnUnload(provider, id, asset);
        }

        public Task UnloadAsync(IAssetProvider provider, string id, object asset)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            return OnUnloadAsync(provider, id, asset);
        }

        protected abstract object OnLoad(IAssetProvider provider, string id, Type type);
        protected abstract Task<object> OnLoadAsync(IAssetProvider provider, string id, Type type);
        protected abstract void OnUnload(IAssetProvider provider, string id, object asset);
        protected abstract Task OnUnloadAsync(IAssetProvider provider, string id, object asset);
    }
}
