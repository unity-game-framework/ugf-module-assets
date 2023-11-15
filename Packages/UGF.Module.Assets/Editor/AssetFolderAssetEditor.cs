using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetFolderAsset), true)]
    public class AssetFolderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyFolder;
        private readonly string[] m_propertyExclude = { "m_Script", "m_folder" };

        private void OnEnable()
        {
            m_propertyFolder = serializedObject.FindProperty("m_folder");
        }

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
            DrawPropertiesExcluding(serializedObject, m_propertyExclude);
        }
    }
}
