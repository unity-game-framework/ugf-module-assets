using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Resources Loader", order = 2000)]
    public class ResourcesLoaderAsset : AssetLoaderAsset
    {
        [SerializeField] private bool m_enableUnload = true;

        public bool EnableUnload { get { return m_enableUnload; } set { m_enableUnload = value; } }

        protected override IAssetLoader OnBuild()
        {
            return new ResourcesLoader(m_enableUnload);
        }
    }
}
