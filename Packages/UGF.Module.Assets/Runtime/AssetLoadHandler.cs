using System;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetLoadHandler(string id, Type type, AssetLoadParameters parameters);
}
