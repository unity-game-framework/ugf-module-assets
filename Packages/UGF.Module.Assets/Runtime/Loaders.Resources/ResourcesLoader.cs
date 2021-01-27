using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    public class ResourcesLoader : AssetLoader<IAssetInfo>
    {
        public bool EnableUnload { get; }

        public ResourcesLoader(bool enableUnload = true)
        {
            EnableUnload = enableUnload;
        }

        protected override object OnLoad(IAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            Object asset = UnityEngine.Resources.Load(info.Address, type);

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override async Task<object> OnLoadAsync(IAssetInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            ResourceRequest request = UnityEngine.Resources.LoadAsync(info.Address, type);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            Object asset = request.asset;

            return asset ? asset : throw new NullReferenceException($"Resource load result is null by the specified arguments: id:'{id}', type:'{type}'.");
        }

        protected override void OnUnload(IAssetInfo info, string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            if (EnableUnload)
            {
                InternalUnload(asset);
            }
        }

        protected override Task OnUnloadAsync(IAssetInfo info, string id, object asset, IAssetUnloadParameters parameters, IContext context)
        {
            if (EnableUnload)
            {
                InternalUnload(asset);
            }

            return Task.CompletedTask;
        }

        private static void InternalUnload(object asset)
        {
            if (!(asset is Object unityAsset)) throw new ArgumentException($"Asset must be a Unity Object to unload: '{asset}'.");

            UnityEngine.Resources.UnloadAsset(unityAsset);
        }
    }
}
