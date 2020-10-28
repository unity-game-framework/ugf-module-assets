namespace UGF.Module.Assets.Runtime
{
    public interface IAssetGroupAssetEntry
    {
        string Id { get; }

        IAssetInfo GetInfo();
    }
}
