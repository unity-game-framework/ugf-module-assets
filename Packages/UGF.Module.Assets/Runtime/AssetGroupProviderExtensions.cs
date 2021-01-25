using System;
using System.Collections.Generic;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public static class AssetGroupProviderExtensions
    {
        public static IAssetGroup GetByAssetId(this IProvider<string, IAssetGroup> provider, string id)
        {
            return TryGetByAssetId(provider, id, out IAssetGroup group) ? group : throw new ArgumentException($"Asset group not found by the specified asset id: '{id}'.");
        }

        public static bool TryGetByAssetId(this IProvider<string, IAssetGroup> provider, string id, out IAssetGroup group)
        {
            foreach (KeyValuePair<string, IAssetGroup> pair in provider.Entries)
            {
                if (pair.Value.Entries.ContainsKey(pair.Key))
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
