using System;

namespace UGF.Module.Assets.Runtime
{
    public class AssetInfo : IAssetInfo
    {
        public string LoaderId { get; }
        public string Address { get; }

        public AssetInfo(string loaderId, string address)
        {
            if (string.IsNullOrEmpty(loaderId)) throw new ArgumentException("Value cannot be null or empty.", nameof(loaderId));
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Value cannot be null or empty.", nameof(address));

            LoaderId = loaderId;
            Address = address;
        }
    }
}
