using System;
using System.Threading.Tasks;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoaderBase : IAssetLoader
    {
        public T Load<T>(IAssetProvider provider, string id) where T : class
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return OnLoad<T>(provider, id);
        }

        public object Load(IAssetProvider provider, string id, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            return OnLoad(provider, id, type);
        }

        public Task<T> LoadAsync<T>(IAssetProvider provider, string id) where T : class
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return OnLoadAsync<T>(provider, id);
        }

        public Task<object> LoadAsync(IAssetProvider provider, string id, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (type == null) throw new ArgumentNullException(nameof(type));

            return OnLoadAsync(provider, id, type);
        }

        public void Unload<T>(IAssetProvider provider, string id, T asset) where T : class
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            OnUnload(provider, id, asset);
        }

        public void Unload(IAssetProvider provider, string id, object asset)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            OnUnload(provider, id, asset);
        }

        public Task UnloadAsync<T>(IAssetProvider provider, string id, T asset) where T : class
        {
            return OnUnloadAsync(provider, id, asset);
        }

        public Task UnloadAsync(IAssetProvider provider, string id, object asset)
        {
            return OnUnloadAsync(provider, id, asset);
        }

        protected virtual T OnLoad<T>(IAssetProvider provider, string id)
        {
            return (T)OnLoad(provider, id, typeof(T));
        }

        protected virtual async Task<T> OnLoadAsync<T>(IAssetProvider provider, string id)
        {
            return (T)await OnLoadAsync(provider, id, typeof(T));
        }

        protected virtual void OnUnload<T>(IAssetProvider provider, string id, T asset)
        {
            OnUnload(provider, id, (object)asset);
        }

        protected virtual Task OnUnloadAsync<T>(IAssetProvider provider, string id, T asset)
        {
            return OnUnloadAsync(provider, id, (object)asset);
        }

        protected abstract object OnLoad(IAssetProvider provider, string id, Type type);
        protected abstract Task<object> OnLoadAsync(IAssetProvider provider, string id, Type type);
        protected abstract void OnUnload(IAssetProvider provider, string id, object asset);
        protected abstract Task OnUnloadAsync(IAssetProvider provider, string id, object asset);
    }
}
