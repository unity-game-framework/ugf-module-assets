using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsResourcesModule : ApplicationModuleBase, IAssetsModule
    {
        public T Load<T>(string assetName)
        {
            return (T)Load(assetName, typeof(T));
        }

        public object Load(string assetName, Type assetType)
        {
            return Resources.Load(assetName, assetType);
        }

        public async Task<T> LoadAsync<T>(string assetName)
        {
            return (T)await LoadAsync(assetName, typeof(T));
        }

        public async Task<object> LoadAsync(string assetName, Type assetType)
        {
            if (string.IsNullOrEmpty(assetName)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetName));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));

            ResourceRequest operation = Resources.LoadAsync(assetName, assetType);

            while (operation.isDone)
            {
                await Task.Yield();
            }

            return operation.asset;
        }

        public void Release<T>(T asset)
        {
        }

        public void Release(object asset)
        {
        }
    }
}
