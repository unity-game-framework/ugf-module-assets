using System;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public delegate void AssetLoadHandler(GlobalId id, Type type, IAssetLoadParameters parameters);
}
