using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.AssetDatabases
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/AssetDatabase Asset Loader", order = 2000)]
    public class AssetDatabaseAssetLoaderAsset : AssetLoaderAsset
    {
        protected override IAssetLoader OnBuild()
        {
            return new AssetDatabaseAssetLoader();
        }
    }
}
