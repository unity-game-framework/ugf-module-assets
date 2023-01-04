using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetModuleAsset), true)]
    internal class AssetModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyUnloadTrackedAssetsOnUninitialize;
        private AssetIdReferenceListDrawer m_listLoaders;
        private ReorderableListSelectionDrawerByPath m_listLoadersSelection;
        private AssetIdReferenceListDrawer m_listGroups;
        private ReorderableListSelectionDrawerByPath m_listGroupsSelection;
        private ReorderableListDrawer m_listPreloadAssets;
        private ReorderableListDrawer m_listPreloadAssetsAsync;

        private void OnEnable()
        {
            m_propertyUnloadTrackedAssetsOnUninitialize = serializedObject.FindProperty("m_unloadTrackedAssetsOnUninitialize");

            m_listLoaders = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_loaders"))
            {
                DisplayAsSingleLine = true
            };

            m_listLoadersSelection = new ReorderableListSelectionDrawerByPath(m_listLoaders, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listGroups = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_groups"))
            {
                DisplayAsSingleLine = true
            };

            m_listGroupsSelection = new ReorderableListSelectionDrawerByPath(m_listGroups, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listPreloadAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_preload"))
            {
                DisplayAsSingleLine = true
            };

            m_listPreloadAssetsAsync = new ReorderableListDrawer(serializedObject.FindProperty("m_preloadAsync"))
            {
                DisplayAsSingleLine = true
            };

            m_listLoaders.Enable();
            m_listLoadersSelection.Enable();
            m_listGroups.Enable();
            m_listGroupsSelection.Enable();
            m_listPreloadAssets.Enable();
            m_listPreloadAssetsAsync.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
            m_listLoadersSelection.Disable();
            m_listGroups.Disable();
            m_listGroupsSelection.Disable();
            m_listPreloadAssets.Disable();
            m_listPreloadAssetsAsync.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyUnloadTrackedAssetsOnUninitialize);

                m_listLoaders.DrawGUILayout();
                m_listGroups.DrawGUILayout();
                m_listPreloadAssets.DrawGUILayout();
                m_listPreloadAssetsAsync.DrawGUILayout();

                m_listLoadersSelection.DrawGUILayout();
                m_listGroupsSelection.DrawGUILayout();
            }
        }
    }
}
