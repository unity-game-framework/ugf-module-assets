using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAsset : ScriptableObject
    {
        public void GetAssets(IDictionary<GlobalId, IAssetInfo> assets)
        {
            if (assets == null) throw new ArgumentNullException(nameof(assets));

            OnGetAssets(assets);
        }

        protected abstract void OnGetAssets(IDictionary<GlobalId, IAssetInfo> assets);
    }
}
