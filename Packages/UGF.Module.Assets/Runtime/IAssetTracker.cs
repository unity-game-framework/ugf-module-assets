using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetTracker : IProvider<GlobalId, AssetTrack>
    {
        AssetTrack Add(GlobalId id, object asset);
        void Update(GlobalId id, AssetTrack track);
        bool Track(GlobalId id, object asset, out AssetTrack track);
        bool UnTrack(GlobalId id, object asset, out AssetTrack track);
        AssetTrack Increment(GlobalId id);
        AssetTrack Decrement(GlobalId id);
        AssetTrack GetByAsset(object asset);
        bool TryGetByAsset(object asset, out AssetTrack track);
    }
}
