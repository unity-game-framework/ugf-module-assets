using System;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public class AssetGroupAssetInfo : IAssetInfo
    {
        [SerializeField, AssetGuid] private string m_asset;
        [SerializeField] private string m_address;
        [SerializeField] private string m_path;

        public string Asset { get { return m_asset; } set { m_asset = value; } }
        public string Address { get { return m_address; } set { m_address = value; } }
        public string Path { get { return m_path; } set { m_path = value; } }
    }
}
