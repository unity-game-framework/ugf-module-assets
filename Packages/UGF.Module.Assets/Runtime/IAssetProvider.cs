using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetProvider
    {
        IReadOnlyDictionary<string, IAssetLoader> Loaders { get; }
        IReadOnlyDictionary<string, IAssetGroup> Groups { get; }

        bool TryGetLoader(string id, out IAssetLoader loader);
        bool TryGetGroup(string id, out IAssetGroup group);
        bool TryGetInfo(string id, out IAssetInfo info);
    }
}
