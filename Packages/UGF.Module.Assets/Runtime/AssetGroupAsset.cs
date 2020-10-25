using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAsset<TEntry> : AssetGroupAssetBase where TEntry : class, IAssetGroupAssetEntry
    {
        [AssetGuid(typeof(AssetLoaderAssetBase))]
        [SerializeField] private string m_loader;
        [SerializeField] private List<TEntry> m_assets = new List<TEntry>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public List<TEntry> Assets { get { return m_assets; } }

        protected override IAssetGroup OnBuild()
        {
            var group = new AssetGroup(m_loader);

            for (int i = 0; i < m_assets.Count; i++)
            {
                TEntry entry = m_assets[i];
                IAssetInfo info = entry.GetInfo();

                group.Add(entry.Id, info);
            }

            return group;
        }
    }
}
