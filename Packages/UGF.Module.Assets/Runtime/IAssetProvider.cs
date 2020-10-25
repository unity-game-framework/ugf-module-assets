﻿using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetProvider
    {
        IReadOnlyDictionary<string, IAssetLoader> Loaders { get; }
        IReadOnlyDictionary<string, IAssetGroup> Groups { get; }

        IAssetLoader GetLoader(string id);
        bool TryGetLoader(string id, out IAssetLoader loader);
        IAssetGroup GetGroupByAsset(string id);
        bool TryGetGroupByAsset(string id, out IAssetGroup group);
        IAssetGroup GetGroup(string id);
        bool TryGetGroup(string id, out IAssetGroup group);
    }
}