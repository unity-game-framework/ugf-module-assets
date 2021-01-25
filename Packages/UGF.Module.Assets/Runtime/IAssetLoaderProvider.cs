using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetLoaderProvider : IProvider<string, IAssetLoader>
    {
    }
}
