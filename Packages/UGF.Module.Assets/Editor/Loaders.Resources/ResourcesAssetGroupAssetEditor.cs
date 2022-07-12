using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    [CustomEditor(typeof(ResourcesAssetGroupAsset), true)]
    internal class ResourcesAssetGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private ResourcesAssetGroupAssetListDrawer m_list;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent RefreshContent { get; } = new GUIContent("Refresh", "Refresh all entries to update address for each entry.");
            public GUIContent RefreshAllContent { get; } = new GUIContent("Refresh All", "Refresh all groups in project to update address for each entry.");
            public string MissingEntryMessage { get; } = "Group contains entries with missing or invalid address.";
        }

        private void OnEnable()
        {
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
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyLoader);

                m_list.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(m_styles.RefreshAllContent))
                {
                    ResourcesAssetEditorProgress.StartUpdateAssetGroupAll();
                }

                if (GUILayout.Button(m_styles.RefreshContent))
                {
                    ResourcesAssetEditorUtility.UpdateAssetGroupEntries((ResourcesAssetGroupAsset)target);
                    EditorUtility.SetDirty(target);
                }
            }

            EditorGUILayout.Space();

            if (ResourcesAssetEditorUtility.IsAssetGroupHasMissingEntries((ResourcesAssetGroupAsset)target))
            {
                EditorGUILayout.HelpBox(m_styles.MissingEntryMessage, MessageType.Warning);
            }
        }
    }
}
