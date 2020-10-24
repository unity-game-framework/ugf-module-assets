using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModuleDescription : IAssetsModuleDescription
    {
        public Dictionary<string, IAssetLoader> Loaders { get; } = new Dictionary<string, IAssetLoader>();

        IReadOnlyDictionary<string, IAssetLoader> IAssetsModuleDescription.Loaders { get { return Loaders; } }
    }
}
