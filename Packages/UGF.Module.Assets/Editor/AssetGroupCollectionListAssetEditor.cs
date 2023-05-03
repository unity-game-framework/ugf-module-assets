using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetGroupCollectionListAsset), true)]
    internal class AssetGroupCollectionListAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listGroups;
        private ReorderableListSelectionDrawerByElement m_listGroupsSelection;

        private void OnEnable()
        {
            m_listGroups = new ReorderableListDrawer(serializedObject.FindProperty("m_groups"));

            m_listGroupsSelection = new ReorderableListSelectionDrawerByElement(m_listGroups)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listGroups.Enable();
            m_listGroupsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listGroups.Disable();
            m_listGroupsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listGroups.DrawGUILayout();
                m_listGroupsSelection.DrawGUILayout();
            }
        }
    }
}
