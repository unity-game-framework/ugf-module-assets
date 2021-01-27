using System;

namespace UGF.Module.Assets.Runtime
{
    [Serializable]
    public struct AssetLoadParameters
    {
        public static AssetLoadParameters Default { get; } = new AssetLoadParameters();
    }
}
