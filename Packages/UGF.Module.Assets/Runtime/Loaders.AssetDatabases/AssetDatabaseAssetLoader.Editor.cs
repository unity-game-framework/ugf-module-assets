#if UNITY_EDITOR
using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime.Loaders.AssetDatabases
{
    public partial class AssetDatabaseAssetLoader
    {
        private partial object OnLoadAssetDatabase(IAssetInfo info, GlobalId id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            Object asset = AssetDatabase.LoadAssetAtPath(info.Address, type);

            if (asset == null)
            {
                asset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(info.Address), type);
            }

            return Task.FromResult<object>(asset ? asset : throw new NullReferenceException($"AssetDatabase load result is null by the specified address and type: '{info.Address}', '{type}'."));
        }
    }
}
#endif
