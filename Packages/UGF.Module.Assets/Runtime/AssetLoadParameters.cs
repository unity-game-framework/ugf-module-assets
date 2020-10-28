using System;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public struct AssetLoadParameters
    {
        [SerializeField] private AssetLoadMode m_mode;

        public AssetLoadMode Mode { get { return m_mode; } set { m_mode = value; } }

        public static AssetLoadParameters Default { get; } = new AssetLoadParameters(AssetLoadMode.Track);
        public static AssetLoadParameters Direct { get; } = new AssetLoadParameters(AssetLoadMode.Direct);

        public AssetLoadParameters(AssetLoadMode mode)
        {
            m_mode = mode;
        }
    }
}
