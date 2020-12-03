using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    [CreateAssetMenu(menuName = "UGF/Assets/Resources Asset Group", order = 2000)]
    public class ResourcesAssetGroupAsset : AssetGroupAsset
    {
        [SerializeField] private List<Entry> m_assets = new List<Entry>();

        public List<Entry> Assets { get { return m_assets; } }

        [Serializable]
        public struct Entry
        {
            [SerializeField] private string m_id;
            [SerializeField] private string m_address;

            public string Id { get { return m_id; } set { m_id = value; } }
            public string Address { get { return m_address; } set { m_address = value; } }
        }

        protected override void OnPopulateGroup(IAssetGroup group)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                Entry entry = m_assets[i];
                var info = new AssetInfo(entry.Address);

                group.Add(entry.Id, info);
            }
        }
    }
}
