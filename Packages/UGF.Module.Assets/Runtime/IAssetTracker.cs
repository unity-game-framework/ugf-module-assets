using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetTracker
    {
        IReadOnlyDictionary<string, AssetTrack> Tracks { get; }

        bool Contains(string id);
        AssetTrack Add(string id, object asset);
        bool Remove(string id);
        void Clear();
        void Update(string id, AssetTrack track);
        AssetTrack Get(string id);
        bool TryGet(string id, out AssetTrack track);
    }
}
