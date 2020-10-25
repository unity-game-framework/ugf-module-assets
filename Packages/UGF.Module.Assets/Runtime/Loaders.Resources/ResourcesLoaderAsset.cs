using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    [CreateAssetMenu(menuName = "UGF/Assets/Resources Loader", order = 2000)]
    public class ResourcesLoaderAsset : AssetLoaderAssetBase
    {
        [SerializeField] private bool m_provideAssetUnload;

        public bool ProvideAssetUnload { get { return m_provideAssetUnload; } set { m_provideAssetUnload = value; } }

        protected override IAssetLoader OnBuild()
        {
            return new ResourcesLoader(m_provideAssetUnload);
        }
    }
}
