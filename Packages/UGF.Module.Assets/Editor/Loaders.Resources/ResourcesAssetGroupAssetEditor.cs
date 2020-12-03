using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    [CustomEditor(typeof(ResourcesAssetGroupAsset), true)]
    internal class ResourcesAssetGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private ReorderableListDrawer m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_list = new ResourcesAssetGroupAssetListDrawer(serializedObject.FindProperty("m_assets"));

            m_list.Enable();
        }

        private void OnDisable()
        {
            m_list.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyLoader);

                m_list.DrawGUILayout();
            }
        }
    }
}
