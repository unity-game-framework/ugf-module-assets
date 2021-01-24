using System;
using System.Collections.Generic;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public class AssetGroupProvider : Provider<string, IAssetGroup>, IAssetGroupProvider
    {
        public IAssetGroup GetByAsset(string id)
        {
            return TryGetByAsset(id, out IAssetGroup group) ? group : throw new ArgumentException($"Asset group not found by the specified asset id: '{id}'.");
        }

        public bool TryGetByAsset(string id, out IAssetGroup group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            foreach (KeyValuePair<string, IAssetGroup> pair in this)
            {
                if (pair.Value.Assets.ContainsKey(id))
                {
                    group = pair.Value;
                    return true;
                }
            }

            group = default;
            return false;
        }
    }
}
