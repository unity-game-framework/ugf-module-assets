using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetUnloadHandler(GlobalId id, object asset, IAssetUnloadParameters parameters);
}
