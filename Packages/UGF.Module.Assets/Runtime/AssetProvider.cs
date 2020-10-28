using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Module.Assets.Runtime
{
    public class AssetProvider : IAssetProvider
    {
        public IReadOnlyDictionary<string, IAssetLoader> Loaders { get; }
        public IReadOnlyDictionary<string, IAssetGroup> Groups { get; }

        private readonly Dictionary<string, IAssetLoader> m_loaders = new Dictionary<string, IAssetLoader>();
        private readonly Dictionary<string, IAssetGroup> m_groups = new Dictionary<string, IAssetGroup>();

        public AssetProvider()
        {
            Loaders = new ReadOnlyDictionary<string, IAssetLoader>(m_loaders);
            Groups = new ReadOnlyDictionary<string, IAssetGroup>(m_groups);
        }

        public void AddLoader(string id, IAssetLoader loader)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (loader == null) throw new ArgumentNullException(nameof(loader));

            m_loaders.Add(id, loader);
        }

        public bool RemoveLoader(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_loaders.Remove(id);
        }

        public void AddGroup(string id, IAssetGroup group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (group == null) throw new ArgumentNullException(nameof(group));

            m_groups.Add(id, group);
        }

        public bool RemoveGroup(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_groups.Remove(id);
        }

        public void ClearLoaders()
        {
            m_loaders.Clear();
        }

        public void ClearGroups()
        {
            m_groups.Clear();
        }

        public IAssetLoader GetLoader(string id)
        {
            return TryGetLoader(id, out IAssetLoader loader) ? loader : throw new ArgumentException($"Asset loader not found by the specified id: '{id}'.");
        }

        public bool TryGetLoader(string id, out IAssetLoader loader)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_loaders.TryGetValue(id, out loader);
        }

        public IAssetGroup GetGroupByAsset(string id)
        {
            return TryGetGroupByAsset(id, out IAssetGroup group) ? group : throw new ArgumentException($"Asset group not found by the specified asset id: '{id}'.");
        }

        public bool TryGetGroupByAsset(string id, out IAssetGroup group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            foreach (KeyValuePair<string, IAssetGroup> pair in m_groups)
            {
                if (pair.Value.Assets.ContainsKey(id))
                {
                    group = pair.Value;
                    return true;
                }
            }

            group = default;
            return false;
        }

        public IAssetGroup GetGroup(string id)
        {
            return TryGetGroup(id, out IAssetGroup group) ? group : throw new ArgumentException($"Asset group not found by the specified id: '{id}'.");
        }

        public bool TryGetGroup(string id, out IAssetGroup group)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_groups.TryGetValue(id, out group);
        }
    }
}
