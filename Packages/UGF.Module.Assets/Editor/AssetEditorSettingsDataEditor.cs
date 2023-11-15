using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetEditorSettingsData), true)]
    internal class AssetEditorSettingsDataEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyFoldersUpdateOnPostprocess;
        private ReorderableListDrawer m_listFolders;
        private ReorderableListSelectionDrawerByElement m_listFoldersSelection;

        private void OnEnable()
        {
            m_propertyFoldersUpdateOnPostprocess = serializedObject.FindProperty("m_foldersUpdateOnPostprocess");

            m_listFolders = new ReorderableListDrawer(serializedObject.FindProperty("m_folders"));

            m_listFoldersSelection = new ReorderableListSelectionDrawerByElement(m_listFolders)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listFolders.Enable();
            m_listFoldersSelection.Enable();
        }

        private void OnDisable()
        {
            m_listFolders.Disable();
            m_listFoldersSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyFoldersUpdateOnPostprocess);

                m_listFolders.DrawGUILayout();
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listFolders.List.selectedIndices.Count == 0))
                {
                    if (GUILayout.Button("Update"))
                    {
                        OnUpdate();
                    }
                }

                using (new EditorGUI.DisabledScope(m_listFolders.SerializedProperty.arraySize == 0))
                {
                    if (GUILayout.Button("Update All"))
                    {
                        OnUpdateAll();
                    }
                }

                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();

            m_listFoldersSelection.DrawGUILayout();
        }

        private void OnUpdate()
        {
            foreach (int index in m_listFolders.List.selectedIndices)
            {
                SerializedProperty propertyElement = m_listFolders.SerializedProperty.GetArrayElementAtIndex(index);

                if (propertyElement.objectReferenceValue is AssetFolderAsset assetFolder && !AssetFolderEditorUtility.TryUpdate(assetFolder))
                {
                    Debug.LogWarning($"Asset folder is invalid: '{assetFolder}'.", assetFolder);
                }
            }
        }

        private void OnUpdateAll()
        {
            if (!AssetEditorSettings.UpdateFoldersAll())
            {
                Debug.LogWarning("Asset folder collection has an invalid entry.");
            }
        }
    }
}
