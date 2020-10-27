using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public struct AssetUnloadParameters
    {
        [SerializeField] private AssetLoadMode m_mode;
        [SerializeField] private bool m_force;

        public AssetLoadMode Mode { get { return m_mode; } set { m_mode = value; } }
        public bool Force { get { return m_force; } set { m_force = value; } }

        public static AssetUnloadParameters Default { get; } = new AssetUnloadParameters(AssetLoadMode.Track, false);
        public static AssetUnloadParameters DefaultForce { get; } = new AssetUnloadParameters(AssetLoadMode.Track, true);
        public static AssetUnloadParameters Direct { get; } = new AssetUnloadParameters(AssetLoadMode.Direct, false);

        public AssetUnloadParameters(AssetLoadMode mode, bool force)
        {
            m_mode = mode;
            m_force = force;
        }
    }
}
