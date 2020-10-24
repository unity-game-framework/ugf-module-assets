using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetGroup
    {
        string LoaderId { get; }
        IReadOnlyDictionary<string, IAssetInfo> Assets { get; }

        T GetInfo<T>(string id) where T : class, IAssetInfo;
        IAssetInfo GetInfo(string id);
        bool TryGetInfo<T>(string id, out T info) where T : class, IAssetInfo;
        bool TryGetInfo(string id, out IAssetInfo info);
    }
}
