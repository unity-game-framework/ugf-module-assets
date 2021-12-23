using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Referenced
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Referenced Asset Loader", order = 2000)]
    public class ReferencedAssetLoaderAsset : AssetLoaderAsset
    {
        protected override IAssetLoader OnBuild()
        {
            return new ReferencedAssetLoader();
        }
    }
}
