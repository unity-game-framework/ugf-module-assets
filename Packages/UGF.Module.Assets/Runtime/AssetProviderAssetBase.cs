using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetProviderAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, IAssetProvider
        {
            return (T)OnBuild();
        }

        public IAssetProvider Build()
        {
            return OnBuild();
        }

        protected abstract IAssetProvider OnBuild();
    }
}
