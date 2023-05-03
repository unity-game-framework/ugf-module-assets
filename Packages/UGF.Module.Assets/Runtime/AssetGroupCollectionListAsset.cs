using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Group Collection List", order = 2000)]
    public class AssetGroupCollectionListAsset : AssetGroupAsset
    {
        [SerializeField] private List<AssetGroupAsset> m_groups = new List<AssetGroupAsset>();

        public List<AssetGroupAsset> Groups { get { return m_groups; } }

        protected override void OnGetAssets(IDictionary<GlobalId, IAssetInfo> assets)
        {
            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetGroupAsset group = m_groups[i];

                group.GetAssets(assets);
            }
        }
    }
}
