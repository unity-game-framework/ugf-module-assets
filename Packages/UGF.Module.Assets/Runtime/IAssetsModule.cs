using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModule : IApplicationModule
    {
        T Load<T>(string assetName);
        object Load(string assetName, Type assetType);
        Task<T> LoadAsync<T>(string assetName);
        Task<object> LoadAsync(string assetName, Type assetType);
        void Release<T>(T asset);
        void Release(object asset);
    }
}
