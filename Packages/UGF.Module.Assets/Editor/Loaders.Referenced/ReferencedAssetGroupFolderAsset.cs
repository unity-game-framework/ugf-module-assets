using UGF.Assets.Editor;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime.Loaders.Referenced;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Referenced
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Referenced Asset Group Folder", order = 2000)]
    public class ReferencedAssetGroupFolderAsset : AssetFolderAsset<ReferencedAssetGroupAsset, Object>
    {
        protected override void OnUpdate()
        {
            Collection.Assets.Clear();

            string[] guids = FindAssetsAsGuids();

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);

                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                Collection.Assets.Add(new AssetIdReference<Object>(new GlobalId(guid), asset));
            }

            EditorUtility.SetDirty(Collection);
        }
    }
}
