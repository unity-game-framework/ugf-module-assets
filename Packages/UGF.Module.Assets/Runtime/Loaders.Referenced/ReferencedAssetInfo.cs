using System;
using UGF.EditorTools.Runtime.Ids;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime.Loaders.Referenced
{
    public class ReferencedAssetInfo : AssetInfo
    {
        public Object Asset { get; }

        public ReferencedAssetInfo(GlobalId loaderId, string address, Object asset) : base(loaderId, address)
        {
            Asset = asset ? asset : throw new ArgumentNullException(nameof(asset));
        }
    }
}
