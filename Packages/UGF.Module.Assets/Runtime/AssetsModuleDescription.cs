using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModuleDescription : ApplicationModuleDescription, IAssetsModuleDescription
    {
        public Dictionary<string, IAssetLoader> Loaders { get; } = new Dictionary<string, IAssetLoader>();
        public Dictionary<string, IAssetGroup> Groups { get; } = new Dictionary<string, IAssetGroup>();
        public List<string> PreloadAssets { get; } = new List<string>();
        public List<string> PreloadAssetsAsync { get; } = new List<string>();
        public bool UnloadTrackedAssetsOnUninitialize { get; set; } = true;

        IReadOnlyDictionary<string, IAssetLoader> IAssetsModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<string, IAssetGroup> IAssetsModuleDescription.Groups { get { return Groups; } }
        IReadOnlyList<string> IAssetsModuleDescription.PreloadAssets { get { return PreloadAssets; } }
        IReadOnlyList<string> IAssetsModuleDescription.PreloadAssetsAsync { get { return PreloadAssetsAsync; } }
    }
}
