using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoaderAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, IAssetLoader
        {
            return (T)OnBuild();
        }

        public IAssetLoader Build()
        {
            return OnBuild();
        }

        protected abstract IAssetLoader OnBuild();
    }
}
