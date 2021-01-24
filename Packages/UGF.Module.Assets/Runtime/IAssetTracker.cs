using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetTracker : IProvider<string, AssetTrack>
    {
        bool Contains(string id);
        AssetTrack Add(string id, object asset);
        void Update(string id, AssetTrack track);
        bool Track(string id, object asset, out AssetTrack track);
        bool UnTrack(string id, out AssetTrack track);
        AssetTrack Increment(string id);
        AssetTrack Decrement(string id);
        AssetTrack GetByAsset(object asset);
        bool TryGetByAsset(object asset, out AssetTrack track);
    }
}
