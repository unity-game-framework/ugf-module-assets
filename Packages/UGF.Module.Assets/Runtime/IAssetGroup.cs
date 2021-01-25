using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetGroup : IProvider<string, IAssetInfo>
    {
        string LoaderId { get; }
    }
}
