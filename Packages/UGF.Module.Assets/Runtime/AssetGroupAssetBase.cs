using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAssetBase : ScriptableObject
    {
        public T Build<T>() where T : class, IAssetGroup
        {
            return (T)OnBuild();
        }

        public IAssetGroup Build()
        {
            return OnBuild();
        }

        protected abstract IAssetGroup OnBuild();
    }
}
