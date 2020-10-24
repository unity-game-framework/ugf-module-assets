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

        protected override async Task<object> OnLoad(IAssetProvider provider, string id, Type type)
        {
            IAssetGroup group = provider.GetGroupByAsset(id);
            IAssetInfo assetInfo = group.GetInfo(id);

            ResourceRequest request = UnityEngine.Resources.LoadAsync(assetInfo.Address, type);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            return request.asset;
        }

        protected override Task OnUnload(IAssetProvider provider, string id, object asset)
        {
            if (ProviderAssetUnload)
            {
                if (!(asset is Object unityAsset)) throw new ArgumentException($"Asset must be a Unity Object to unload: '{asset}'.");

                UnityEngine.Resources.UnloadAsset(unityAsset);
            }

            return Task.CompletedTask;
        }
    }
}
