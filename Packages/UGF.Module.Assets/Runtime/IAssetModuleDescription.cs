using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<string, IAssetLoader> Loaders { get; }
        IReadOnlyDictionary<string, IAssetInfo> Assets { get; }
        IReadOnlyList<string> PreloadAssets { get; }
        IReadOnlyList<string> PreloadAssetsAsync { get; }
        bool UnloadTrackedAssetsOnUninitialize { get; }
    }
}
