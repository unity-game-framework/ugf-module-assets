using UGF.Module.Assets.Runtime.Loaders.AssetDatabases;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.AssetDatabases
{
    [CustomEditor(typeof(AssetDatabaseAssetLoaderAsset), true)]
    internal class AssetDatabaseAssetLoaderAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("AssetDatabase loader supports Editor platform only.", MessageType.Info);
        }
    }
}
