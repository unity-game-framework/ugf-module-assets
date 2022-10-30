#if !UNITY_EDITOR
using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.Assets.Runtime.Loaders.AssetDatabases
{
    public partial class AssetDatabaseAssetLoader
    {
        private partial object OnLoadAssetDatabase(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            throw new PlatformNotSupportedException("AssetDatabase loader supports Editor platform only.");
        }
    }
}
#endif
