using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public class AssetModuleDescription : ApplicationModuleDescription, IAssetModuleDescription
    {
        public Dictionary<string, IAssetLoader> Loaders { get; } = new Dictionary<string, IAssetLoader>();
        public Dictionary<string, IAssetInfo> Assets { get; } = new Dictionary<string, IAssetInfo>();
        public List<string> PreloadAssets { get; } = new List<string>();
        public List<string> PreloadAssetsAsync { get; } = new List<string>();
        public bool UnloadTrackedAssetsOnUninitialize { get; set; } = true;

        IReadOnlyDictionary<string, IAssetLoader> IAssetModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<string, IAssetInfo> IAssetModuleDescription.Assets { get { return Assets; } }
        IReadOnlyList<string> IAssetModuleDescription.PreloadAssets { get { return PreloadAssets; } }
        IReadOnlyList<string> IAssetModuleDescription.PreloadAssetsAsync { get { return PreloadAssetsAsync; } }
    }
}
