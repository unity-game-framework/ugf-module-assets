using System;

namespace UGF.Module.Assets.Editor
{
    public abstract class AssetFolderAsset<TAsset> : AssetFolderAsset
    {
        protected override Type OnGetAssetType()
        {
            return typeof(TAsset);
        }
    }
}
