using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModule : ApplicationModuleDescribed<AssetsModuleDescription>, IAssetsModule
    {
        public IAssetProvider Provider { get; }

        IAssetsModuleDescription IAssetsModule.Description { get { return Description; } }

        public event AssetLoadHandler Loading;
        public event AssetLoadHandler Loaded;
        public event AssetLoadHandler Unloading;
        public event AssetLoadHandler Unloaded;

        public AssetsModule(IApplication application, AssetsModuleDescription description, IAssetProvider provider) : base(application, description)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public T Load<T>(string id) where T : class
        {
        }

        public object Load(string id, Type type)
        {
        }

        public Task<T> LoadAsync<T>(string id) where T : class
        {
        }

        public Task<object> LoadAsync(string id, Type type)
        {
        }

        public void Unload<T>(T asset) where T : class
        {
        }

        public void Unload(object asset)
        {
        }

        public Task UnloadAsync<T>(T asset) where T : class
        {
        }

        public Task UnloadAsync(object asset)
        {
        }
    }
}
