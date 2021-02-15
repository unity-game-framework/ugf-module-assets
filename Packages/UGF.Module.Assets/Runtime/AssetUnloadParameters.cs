namespace UGF.Module.Assets.Runtime
{
    public class AssetUnloadParameters : IAssetUnloadParameters
    {
        public static AssetUnloadParameters Empty { get; } = new AssetUnloadParameters();
    }
}
