using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<GlobalId, IAssetLoader> Loaders { get; }
        IReadOnlyDictionary<GlobalId, IAssetInfo> Assets { get; }
        IReadOnlyList<GlobalId> PreloadAssets { get; }
        IReadOnlyList<GlobalId> PreloadAssetsAsync { get; }
        bool UnloadTrackedAssetsOnUninitialize { get; }
    }
}
