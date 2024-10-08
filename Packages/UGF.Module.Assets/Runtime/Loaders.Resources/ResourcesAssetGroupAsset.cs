﻿using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Resources Asset Group", order = 2000)]
    public class ResourcesAssetGroupAsset : AssetGroupAsset
    {
        [AssetId(typeof(AssetLoaderAsset))]
        [SerializeField] private Hash128 m_loader;
        [SerializeField] private List<Entry> m_assets = new List<Entry>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public List<Entry> Assets { get { return m_assets; } }

        [Serializable]
        public struct Entry
        {
            [SerializeField] private Hash128 m_id;
            [SerializeField] private string m_address;

            public GlobalId Id { get { return m_id; } set { m_id = value; } }
            public string Address { get { return m_address; } set { m_address = value; } }
        }

        protected override void OnGetAssets(IDictionary<GlobalId, IAssetInfo> assets)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                Entry entry = m_assets[i];

                var info = new AssetInfo(m_loader, entry.Address);

                assets.Add(entry.Id, info);
            }
        }
    }
}
