using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public struct AssetUnloadParameters
    {
        [SerializeField] private bool m_force;

        public bool Force { get { return m_force; } set { m_force = value; } }

        public static AssetUnloadParameters Default { get; } = new AssetUnloadParameters(false);
        public static AssetUnloadParameters DefaultForce { get; } = new AssetUnloadParameters(true);

        public AssetUnloadParameters(bool force)
        {
            m_force = force;
        }
    }
}
