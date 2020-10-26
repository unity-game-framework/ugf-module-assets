using System;
using System.Collections.Generic;

namespace UGF.Module.Assets.Runtime
{
    public class AssetTracker : IAssetTracker
    {
        public IReadOnlyDictionary<string, AssetTrack> Tracks { get; }

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

        public AssetTrack Add(string id, object asset)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            var track = new AssetTrack(asset);

            m_tracks.Add(id, track);

            return track;
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

        public void Update(string id, AssetTrack track)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (track.Asset == null) throw new ArgumentException($"Asset not found in specified track: '{track}'.");

            m_tracks[id] = track;
        }

        public AssetTrack Get(string id)
        {
            return TryGet(id, out AssetTrack asset) ? asset : throw new ArgumentException($"Asset track not found by the specified id: '{id}'.");
        }

        public bool TryGet(string id, out AssetTrack track)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_tracks.TryGetValue(id, out track);
        }
    }
}
