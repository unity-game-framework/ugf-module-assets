namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetLoader<TInfo> : AssetLoader<TInfo, IAssetLoadParameters, IAssetUnloadParameters> where TInfo : class, IAssetInfo
    {
        protected AssetLoader() : base(AssetLoadParameters.Empty, AssetUnloadParameters.Empty)
        {
        }

        protected AssetLoader(IAssetLoadParameters defaultLoadParameters, IAssetUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }
    }
}
