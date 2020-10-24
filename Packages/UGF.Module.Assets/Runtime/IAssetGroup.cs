using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetGroup
    {
        string LoaderId { get; }
        IReadOnlyDictionary<string, IAssetInfo> Assets { get; }

        bool TryGetInfo(string id, out IAssetInfo info);
    }
}
