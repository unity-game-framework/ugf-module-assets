namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TInfo> : AssetLoader<TInfo, IAssetLoadParameters, IAssetUnloadParameters> where TInfo : class, IAssetInfo
    {
    }
}
