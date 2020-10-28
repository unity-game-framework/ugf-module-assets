using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public abstract class AssetGroupAssetEntry : IAssetGroupAssetEntry
    {
        [SerializeField] private string m_id;

        public string Id { get { return m_id; } set { m_id = value; } }

        public IAssetInfo GetInfo()
        {
            return OnGetInfo();
        }

        protected abstract IAssetInfo OnGetInfo();
    }
}
