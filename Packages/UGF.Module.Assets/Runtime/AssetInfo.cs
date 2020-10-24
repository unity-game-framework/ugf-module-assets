using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public class AssetInfo : IAssetInfo
    {
        [SerializeField] private string m_address;

        public string Address { get { return m_address; } set { m_address = value; } }
    }
}
