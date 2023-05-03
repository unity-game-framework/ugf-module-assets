using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public class AssetModuleDescription : ApplicationModuleDescription, IAssetModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, IAssetLoader> Loaders { get; }
        public IReadOnlyDictionary<GlobalId, IAssetInfo> Assets { get; }
        public IReadOnlyList<GlobalId> PreloadAssets { get; }
        public IReadOnlyList<GlobalId> PreloadAssetsAsync { get; }
        public bool UnloadTrackedAssetsOnUninitialize { get; }

        public AssetModuleDescription(Type registerType,
            IReadOnlyDictionary<GlobalId, IAssetLoader> loaders,
            IReadOnlyDictionary<GlobalId, IAssetInfo> assets,
            IReadOnlyList<GlobalId> preloadAssets,
            IReadOnlyList<GlobalId> preloadAssetsAsync,
            bool unloadTrackedAssetsOnUninitialize) : base(registerType)
        {
            Loaders = loaders ?? throw new ArgumentNullException(nameof(loaders));
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            PreloadAssets = preloadAssets ?? throw new ArgumentNullException(nameof(preloadAssets));
            PreloadAssetsAsync = preloadAssetsAsync ?? throw new ArgumentNullException(nameof(preloadAssetsAsync));
            UnloadTrackedAssetsOnUninitialize = unloadTrackedAssetsOnUninitialize;
        }
    }
}
