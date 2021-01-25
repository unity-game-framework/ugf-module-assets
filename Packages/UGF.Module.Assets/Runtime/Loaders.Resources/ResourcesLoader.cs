using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
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

        protected override object OnLoad(IAssetGroup group, IAssetInfo info, string id, Type type, IContext context)
        {
            Object asset = UnityEngine.Resources.Load(info.Address, type);

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override async Task<object> OnLoadAsync(IAssetGroup group, IAssetInfo info, string id, Type type, IContext context)
        {
            ResourceRequest request = UnityEngine.Resources.LoadAsync(info.Address, type);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            Object asset = request.asset;

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override void OnUnload(IAssetGroup group, IAssetInfo info, string id, object asset, IContext context)
        {
            InternalUnload(asset);
        }

        protected override Task OnUnloadAsync(IAssetGroup group, IAssetInfo info, string id, object asset, IContext context)
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
