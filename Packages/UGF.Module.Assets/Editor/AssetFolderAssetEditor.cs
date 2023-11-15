using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetFolderAsset), true)]
    public class AssetFolderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyFolder;

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyFolder);

                OnDrawGUILayout();
            }
        }

        protected virtual void OnDrawGUILayout()
        {
        }
    }
}
