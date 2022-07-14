using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public class AssetModuleDescription : ApplicationModuleDescription, IAssetModuleDescription
    {
        public Dictionary<GlobalId, IAssetLoader> Loaders { get; } = new Dictionary<GlobalId, IAssetLoader>();
        public Dictionary<GlobalId, IAssetInfo> Assets { get; } = new Dictionary<GlobalId, IAssetInfo>();
        public List<GlobalId> PreloadAssets { get; } = new List<GlobalId>();
        public List<GlobalId> PreloadAssetsAsync { get; } = new List<GlobalId>();
        public bool UnloadTrackedAssetsOnUninitialize { get; set; } = true;

        IReadOnlyDictionary<GlobalId, IAssetLoader> IAssetModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<GlobalId, IAssetInfo> IAssetModuleDescription.Assets { get { return Assets; } }
        IReadOnlyList<GlobalId> IAssetModuleDescription.PreloadAssets { get { return PreloadAssets; } }
        IReadOnlyList<GlobalId> IAssetModuleDescription.PreloadAssetsAsync { get { return PreloadAssetsAsync; } }
    }
}
