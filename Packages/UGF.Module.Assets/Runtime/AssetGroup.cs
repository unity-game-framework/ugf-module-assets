using System;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public class AssetGroup : Provider<string, IAssetInfo>, IAssetGroup
    {
        public string LoaderId { get; }

        public AssetGroup(string loaderId)
        {
            if (string.IsNullOrEmpty(loaderId)) throw new ArgumentException("Value cannot be null or empty.", nameof(loaderId));

            LoaderId = loaderId;
        }
    }
}
