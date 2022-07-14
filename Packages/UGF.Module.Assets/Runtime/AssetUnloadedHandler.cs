using System;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetUnloadedHandler(GlobalId id, Type type, IAssetUnloadParameters parameters);
}
