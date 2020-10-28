using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Module.Assets.Runtime
{
    public class AssetGroup : IAssetGroup
    {
        public string LoaderId { get; }
        public IReadOnlyDictionary<string, IAssetInfo> Assets { get; }

        private readonly Dictionary<string, IAssetInfo> m_assets = new Dictionary<string, IAssetInfo>();

        public AssetGroup(string loaderId)
        {
            if (string.IsNullOrEmpty(loaderId)) throw new ArgumentException("Value cannot be null or empty.", nameof(loaderId));

            LoaderId = loaderId;
            Assets = new ReadOnlyDictionary<string, IAssetInfo>(m_assets);
        }

        public void Add(string id, IAssetInfo info)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (info == null) throw new ArgumentNullException(nameof(info));

            m_assets.Add(id, info);
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_assets.Remove(id);
        }

        public void Clear()
        {
            m_assets.Clear();
        }

        public T GetInfo<T>(string id) where T : class, IAssetInfo
        {
            return (T)GetInfo(id);
        }

        public IAssetInfo GetInfo(string id)
        {
            return TryGetInfo(id, out IAssetInfo value) ? value : throw new ArgumentException($"Asset info not found by the specified id: '{id}'.");
        }

        public bool TryGetInfo<T>(string id, out T info) where T : class, IAssetInfo
        {
            if (m_assets.TryGetValue(id, out IAssetInfo value))
            {
                info = (T)value;
                return true;
            }

            info = default;
            return false;
        }

        public bool TryGetInfo(string id, out IAssetInfo info)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_assets.TryGetValue(id, out info);
        }
    }
}
