using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime.Loaders.Resources
{
    [Serializable]
    public class ResourcesAssetGroupEntry : AssetGroupAssetEntry
    {
        [SerializeField] private string m_address;

        public string Address { get { return m_address; } set { m_address = value; } }

        protected override IAssetInfo OnGetInfo()
        {
            return new AssetInfo(m_address);
        }
    }
}
