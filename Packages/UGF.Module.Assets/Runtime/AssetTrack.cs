using System;

namespace UGF.Module.Assets.Runtime
{
    public readonly struct AssetTrack : IComparable<AssetTrack>
    {
        public object Asset { get; }
        public uint Count { get; }
        public bool Positive { get { return Count > 0; } }
        public bool Zero { get { return Count == 0; } }

        public AssetTrack(object asset, uint count = 0)
        {
            Asset = asset ?? throw new ArgumentNullException(nameof(asset));
            Count = count;
        }

        public AssetTrack Increment(uint value = 1)
        {
            if (value == 0) throw new ArgumentOutOfRangeException(nameof(value));

            return new AssetTrack(Asset, checked(Count + value));
        }

        public AssetTrack Decrement(uint value = 1)
        {
            if (value == 0) throw new ArgumentOutOfRangeException(nameof(value));

            return new AssetTrack(Asset, checked(Count - value));
        }

        public AssetTrack Change(uint value)
        {
            return new AssetTrack(Asset, value);
        }

        public AssetTrack Reset()
        {
            return new AssetTrack(Asset);
        }

        public bool Equals(AssetTrack other)
        {
            return Equals(Asset, other.Asset) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is AssetTrack other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Asset != null ? Asset.GetHashCode() : 0) * 397) ^ (int)Count;
            }
        }

        public int CompareTo(AssetTrack other)
        {
            return Count.CompareTo(other.Count);
        }

        public static bool operator ==(AssetTrack first, AssetTrack second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(AssetTrack first, AssetTrack second)
        {
            return !first.Equals(second);
        }

        public static AssetTrack operator ++(AssetTrack track)
        {
            return track.Increment();
        }

        public static AssetTrack operator +(AssetTrack track, uint value)
        {
            return track.Increment(value);
        }

        public static AssetTrack operator --(AssetTrack track)
        {
            return track.Decrement();
        }

        public static AssetTrack operator -(AssetTrack track, uint value)
        {
            return track.Decrement(value);
        }

        public override string ToString()
        {
            return $"{nameof(Asset)}: {Asset}, {nameof(Count)}: {Count}";
        }
    }
}
