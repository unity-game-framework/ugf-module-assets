using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Referenced
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Referenced Asset Group", order = 2000)]
    public class ReferencedAssetGroupAsset : AssetGroupAsset
    {
        [AssetGuid(typeof(AssetLoaderAsset))]
        [SerializeField] private string m_loader;
        [SerializeField] private List<AssetReference<Object>> m_assets = new List<AssetReference<Object>>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public List<AssetReference<Object>> Assets { get { return m_assets; } }

        protected override void OnGetAssets(IDictionary<string, IAssetInfo> assets)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                AssetReference<Object> reference = m_assets[i];
                var info = new ReferencedAssetInfo(m_loader, reference.Guid, reference.Asset);

                assets.Add(reference.Guid, info);
            }
        }
    }
}
