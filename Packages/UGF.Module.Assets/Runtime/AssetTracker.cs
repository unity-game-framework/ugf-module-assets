using System;
using System.Collections.Generic;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.Assets.Runtime
{
    public class AssetTracker : Provider<string, AssetTrack>, IAssetTracker
    {
        public AssetTrack Add(string id, object asset)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            var track = new AssetTrack(asset);

            base.Add(id, track);

            return track;
        }

        public void Update(string id, AssetTrack track)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (track.Asset == null) throw new ArgumentException($"Asset not found in specified track: '{track}'.");

            Remove(id);
            base.Add(id, track);
        }

        /// <summary>
        /// Tracks specified asset, increments count when asset already tracked or creates new track of asset otherwise.
        /// </summary>
        /// <remarks>
        /// When asset already tracked, the specified asset must be the same as asset tracked by the specified id.
        /// </remarks>
        /// <param name="id">The id of the asset.</param>
        /// <param name="asset">The asset to track.</param>
        /// <param name="track">The modified or create asset track as result.</param>
        /// <returns>Returns True when asset was not already present, otherwise False.</returns>
        public bool Track(string id, object asset, out AssetTrack track)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            if (TryGet(id, out track))
            {
                if (track.Asset != asset) throw new ArgumentException($"Asset track not the same as specified asset: track:'{track.Asset}', asset:'{asset}'.");

                track = Increment(id);
                return false;
            }

            track = Add(id, asset);
            track = Increment(id);
            return true;
        }

        /// <summary>
        /// UnTracks specified asset, decrements count when asset already tracked and removes when asset track count reached zero.
        /// </summary>
        /// <param name="id">The id of the asset.</param>
        /// <param name="asset">The asset to untrack.</param>
        /// <param name="track">The modified asset track as result.</param>
        /// <returns>Returns True when asset track count reached zero and was removed, otherwise False.</returns>
        public bool UnTrack(string id, object asset, out AssetTrack track)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (TryGet(id, out track))
            {
                if (track.Asset != asset) throw new ArgumentException($"Asset track not the same as specified asset: track:'{track.Asset}', asset:'{asset}'.");

                track = Decrement(id);

                if (track.Zero)
                {
                    Remove(id);
                    return true;
                }
            }

            return false;
        }

        public AssetTrack Increment(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            AssetTrack track = Get(id);

            track = track.Increment();

            Update(id, track);

            return track;
        }

        public AssetTrack Decrement(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            AssetTrack track = Get(id);

            track = track.Decrement();

            Update(id, track);

            return track;
        }

        public AssetTrack GetByAsset(object asset)
        {
            return TryGetByAsset(asset, out AssetTrack track) ? track : throw new ArgumentException($"Asset track not found by the specified asset: '{asset}'.");
        }

        public bool TryGetByAsset(object asset, out AssetTrack track)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            foreach (KeyValuePair<string, AssetTrack> pair in this)
            {
                if (pair.Value.Asset == asset)
                {
                    track = pair.Value;
                    return true;
                }
            }

            track = default;
            return false;
        }
    }
}
