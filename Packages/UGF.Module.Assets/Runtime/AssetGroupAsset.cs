using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.EditorTools.Runtime.IMGUI.References;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Assets/Asset Group", order = 2000)]
    public class AssetGroupAsset : AssetGroupAssetBase
    {
        [AssetGuid(typeof(AssetLoaderAssetBase))]
        [SerializeField] private string m_loader;
        [SerializeField] private List<AssetEntry> m_assets = new List<AssetEntry>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public List<AssetEntry> Assets { get { return m_assets; } }

        [Serializable]
        public class AssetEntry
        {
            [SerializeField, AssetGuid] private string m_guid;
            [SerializeReference, ManagedReference(typeof(IAssetInfo))]
            private IAssetInfo m_info;

            public string Guid { get { return m_guid; } set { m_guid = value; } }
            public IAssetInfo Info { get { return m_info; } set { m_info = value; } }
        }

        protected override IAssetGroup OnBuild()
        {
            var group = new AssetGroup(m_loader);

            for (int i = 0; i < m_assets.Count; i++)
            {
                AssetEntry info = m_assets[i];

                group.Add(info.Guid, info.Info);
            }

            return group;
        }
    }
}
