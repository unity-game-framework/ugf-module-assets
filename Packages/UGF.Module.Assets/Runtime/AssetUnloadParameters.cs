using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public struct AssetUnloadParameters
    {
        [SerializeField] private AssetUnloadMode m_mode;
        [SerializeField] private bool m_force;

        public AssetUnloadMode Mode { get { return m_mode; } set { m_mode = value; } }
        public bool Force { get { return m_force; } set { m_force = value; } }

        public static AssetUnloadParameters Default { get; } = new AssetUnloadParameters(AssetUnloadMode.Track, false);
        public static AssetUnloadParameters DefaultTrackOnly { get; } = new AssetUnloadParameters(AssetUnloadMode.TrackOnly, false);
        public static AssetUnloadParameters DefaultForce { get; } = new AssetUnloadParameters(AssetUnloadMode.Track, true);
        public static AssetUnloadParameters Direct { get; } = new AssetUnloadParameters(AssetUnloadMode.Direct, false);

        public AssetUnloadParameters(AssetUnloadMode mode, bool force)
        {
            m_mode = mode;
            m_force = force;
        }
    }
}
