using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAsset : ScriptableObject
    {
        public void GetAssets(IDictionary<string, IAssetInfo> assets)
        {
            if (assets == null) throw new ArgumentNullException(nameof(assets));

            OnGetAssets(assets);
        }

        protected abstract void OnGetAssets(IDictionary<string, IAssetInfo> assets);
    }
}
