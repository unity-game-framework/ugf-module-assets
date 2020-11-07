using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    public class ResourcesLoader : AssetLoader<IAssetGroup, IAssetInfo>
    {
        public bool ProviderAssetUnload { get; }

        public ResourcesLoader(bool providerAssetUnload = true)
        {
            ProviderAssetUnload = providerAssetUnload;
        }

        protected override object OnLoad(IAssetProvider provider, IAssetGroup group, IAssetInfo info, string id, Type type)
        {
            Object asset = UnityEngine.Resources.Load(info.Address, type);

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override async Task<object> OnLoadAsync(IAssetProvider provider, IAssetGroup group, IAssetInfo info, string id, Type type)
        {
            ResourceRequest request = UnityEngine.Resources.LoadAsync(info.Address, type);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            Object asset = request.asset;

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override void OnUnload(IAssetProvider provider, IAssetGroup group, IAssetInfo info, string id, object asset)
        {
            InternalUnload(asset);
        }

        protected override Task OnUnloadAsync(IAssetProvider provider, IAssetGroup group, IAssetInfo info, string id, object asset)
        {
            InternalUnload(asset);

            return Task.CompletedTask;
        }

        private void InternalUnload(object asset)
        {
            if (ProviderAssetUnload)
            {
                if (!(asset is Object unityAsset)) throw new ArgumentException($"Asset must be a Unity Object to unload: '{asset}'.");

                UnityEngine.Resources.UnloadAsset(unityAsset);
            }
        }
    }
}
