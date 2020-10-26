using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Assets/Asset Provider", order = 2000)]
    public class AssetProviderAsset : AssetProviderAssetBase
    {
        protected override IAssetProvider OnBuild()
        {
            return new AssetProvider();
        }
    }
}
