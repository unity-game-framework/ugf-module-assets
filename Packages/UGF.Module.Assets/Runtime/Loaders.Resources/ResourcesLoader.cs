using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    public class ResourcesLoader : AssetLoaderBase
    {
        public bool ProviderAssetUnload { get; }

        public ResourcesLoader(bool providerAssetUnload = false)
        {
            ProviderAssetUnload = providerAssetUnload;
        }

        protected override object OnLoad(IAssetProvider provider, string id, Type type)
        {
            if (!typeof(Object).IsAssignableFrom(type)) throw new ArgumentException("Asset type must be a Unity Object type.");

            IAssetGroup group = provider.GetGroupByAsset(id);
            IAssetInfo info = group.GetInfo(id);
            Object asset = UnityEngine.Resources.Load(info.Address, type);

            return asset;
        }

        protected override async Task<object> OnLoadAsync(IAssetProvider provider, string id, Type type)
        {
            if (!typeof(Object).IsAssignableFrom(type)) throw new ArgumentException("Asset type must be a Unity Object type.");

            IAssetGroup group = provider.GetGroupByAsset(id);
            IAssetInfo info = group.GetInfo(id);

            ResourceRequest request = UnityEngine.Resources.LoadAsync(info.Address, type);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            Object asset = request.asset;

            return asset;
        }

        protected override void OnUnload(IAssetProvider provider, string id, object asset)
        {
            InternalUnload(provider, id, asset);
        }

        protected override Task OnUnloadAsync(IAssetProvider provider, string id, object asset)
        {
            InternalUnload(provider, id, asset);

            return Task.CompletedTask;
        }

        private void InternalUnload(IAssetProvider provider, string id, object asset)
        {
            if (ProviderAssetUnload)
            {
                if (!(asset is Object unityAsset)) throw new ArgumentException($"Asset must be a Unity Object to unload: '{asset}'.");

                UnityEngine.Resources.UnloadAsset(unityAsset);
            }
        }
    }
}
