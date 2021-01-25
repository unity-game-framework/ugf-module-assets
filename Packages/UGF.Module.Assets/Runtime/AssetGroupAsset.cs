using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAsset : AssetGroupAssetBase
    {
        [AssetGuid(typeof(AssetLoaderAssetBase))]
        [SerializeField] private string m_loader;

        public string Loader { get { return m_loader; } set { m_loader = value; } }

        protected override AssetGroup OnBuild()
        {
            AssetGroup group = OnBuildGroup();

            OnPopulateGroup(group);

            return group;
        }

        protected virtual AssetGroup OnBuildGroup()
        {
            return new AssetGroup(m_loader);
        }

        protected abstract void OnPopulateGroup(AssetGroup group);
    }
}
