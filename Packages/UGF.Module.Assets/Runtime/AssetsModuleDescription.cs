using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModuleDescription : IAssetsModuleDescription
    {
        public Dictionary<string, IAssetLoader> Loaders { get; } = new Dictionary<string, IAssetLoader>();
        public Dictionary<string, IAssetGroup> Groups { get; } = new Dictionary<string, IAssetGroup>();
        public bool UnloadTrackedAssetsOnUninitialize { get; set; }

        IReadOnlyDictionary<string, IAssetLoader> IAssetsModuleDescription.Loaders { get { return Loaders; } }
        IReadOnlyDictionary<string, IAssetGroup> IAssetsModuleDescription.Groups { get { return Groups; } }
    }
}
