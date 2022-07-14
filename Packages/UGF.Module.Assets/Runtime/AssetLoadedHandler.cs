using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetLoadedHandler(GlobalId id, object asset, IAssetLoadParameters parameters);
}
