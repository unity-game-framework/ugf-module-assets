using System;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetUnloadedHandler(string id, Type type, AssetUnloadParameters parameters);
}
