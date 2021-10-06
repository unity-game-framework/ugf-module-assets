using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    [CustomEditor(typeof(ResourcesAssetEditorSettingsData), true)]
    internal class ResourcesAssetEditorSettingsDataEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyUpdateAllGroupsOnBuild;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent RefreshAllContent { get; } = new GUIContent("Refresh All", "Refresh all groups in project to update address for each entry.");
        }

        private void OnEnable()
        {
            m_propertyUpdateAllGroupsOnBuild = serializedObject.FindProperty("m_updateAllGroupsOnBuild");
        }

        public override void OnInspectorGUI()
        {
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyUpdateAllGroupsOnBuild);
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(m_styles.RefreshAllContent))
                {
                    ResourcesAssetEditorProgress.StartUpdateAssetGroupAll();
                }

                EditorGUILayout.Space();
            }
        }
    }
}
