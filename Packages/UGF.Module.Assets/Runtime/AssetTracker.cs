using System;
using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public class AssetTracker : IAssetTracker
    {
        public int Count { get { return m_tracks.Count; } }
        public IReadOnlyDictionary<string, AssetTrack> Tracks { get; set; }

        private readonly Dictionary<string, AssetTrack> m_tracks = new Dictionary<string, AssetTrack>();

        public AssetTracker()
        {
            Tracks = new Dictionary<string, AssetTrack>(m_tracks);
        }

        public bool Contains(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_tracks.ContainsKey(id);
        }

        public void Add(string id, object asset)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            m_tracks.Add(id, new AssetTrack(asset));
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_tracks.Remove(id);
        }

        public void Clear()
        {
            m_tracks.Clear();
        }

        public uint Increment(string id, uint value = 1)
        {
            return TryIncrement(id, out uint count, value) ? count : throw new ArgumentException($"Asset track not found by the specified id: '{id}'.");
        }

        public bool TryIncrement(string id, out uint count, uint value = 1)
        {
            if (m_tracks.TryGetValue(id, out AssetTrack track))
            {
                track += value;

                m_tracks[id] = track;

                count = track.Count;
                return true;
            }

            count = default;
            return false;
        }

        public uint Decrement(string id, uint value = 1)
        {
            return TryDecrement(id, out uint count) ? count : throw new ArgumentException($"Asset track not found by the specified id: '{id}'.");
        }

        public bool TryDecrement(string id, out uint count, uint value = 1)
        {
            if (m_tracks.TryGetValue(id, out AssetTrack track))
            {
                track -= value;

                m_tracks[id] = track;

                count = track.Count;
                return true;
            }

            count = default;
            return false;
        }

        public uint GetCount(string id)
        {
            return TryGetCount(id, out uint count) ? count : throw new ArgumentException($"Asset track not found by the specified id: '{id}'.");
        }

        public bool TryGetCount(string id, out uint count)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_tracks.TryGetValue(id, out AssetTrack track))
            {
                count = track.Count;
                return true;
            }

            count = default;
            return false;
        }

        public T Get<T>(string id) where T : class
        {
            return (T)Get(id);
        }

        public object Get(string id)
        {
            return TryGet(id, out object asset) ? asset : throw new ArgumentException($"Asset not found by the specified id: '{id}'.");
        }

        public bool TryGet<T>(string id, out T asset) where T : class
        {
            if (TryGet(id, out object value))
            {
                asset = (T)value;
                return true;
            }

            asset = default;
            return false;
        }

        public bool TryGet(string id, out object asset)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_tracks.TryGetValue(id, out AssetTrack track))
            {
                asset = track.Asset;
                return true;
            }

            asset = default;
            return false;
        }
    }
}
