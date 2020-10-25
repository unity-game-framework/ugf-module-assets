using System;

namespace UGF.Module.Assets.Runtime
{
    public class AssetInfo : IAssetInfo
    {
        public string Address { get; }

        public AssetInfo(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Value cannot be null or empty.", nameof(address));

            Address = address;
        }
    }
}
