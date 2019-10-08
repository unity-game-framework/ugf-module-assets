using System;
using UGF.Application.Runtime;
using UGF.Coroutines.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModule
    {
        ICoroutine<T> LoadAsync<T>(string assetName);
        ICoroutine<object> LoadAsync(string assetName, Type assetType);
        void Release<T>(T asset);
        void Release(object asset);
    }
}
