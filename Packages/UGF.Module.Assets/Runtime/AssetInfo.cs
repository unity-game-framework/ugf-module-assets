using System;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public class AssetInfo : IAssetInfo
    {
        public GlobalId LoaderId { get; }
        public string Address { get; }

        public AssetInfo(GlobalId loaderId, string address)
        {
            if (!loaderId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(loaderId));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Value cannot be null or empty.", nameof(address));

            LoaderId = loaderId;
            Address = address;
        }
    }
}
