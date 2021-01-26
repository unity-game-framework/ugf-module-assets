using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAsset : ScriptableObject
    {
        public abstract void GetAssets(IDictionary<string, IAssetInfo> assets);
    }
}
