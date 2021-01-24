using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetGroupProvider : IProvider<string, IAssetGroup>
    {
        IAssetGroup GetByAsset(string id);
        bool TryGetByAsset(string id, out IAssetGroup group);
    }
}
