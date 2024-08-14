using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Referenced
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Referenced Asset Group", order = 2000)]
    public class ReferencedAssetGroupAsset : AssetGroupAsset
    {
        [AssetId(typeof(AssetLoaderAsset))]
        [SerializeField] private Hash128 m_loader;
        [SerializeField] private List<AssetIdReference<Object>> m_assets = new List<AssetIdReference<Object>>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public List<AssetIdReference<Object>> Assets { get { return m_assets; } }

        protected override void OnGetAssets(IDictionary<GlobalId, IAssetInfo> assets)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                AssetIdReference<Object> reference = m_assets[i];

                var info = new ReferencedAssetInfo(m_loader, reference.Guid.ToString(), reference.Asset);

                assets.Add(reference.Guid, info);
            }
        }
    }
}
