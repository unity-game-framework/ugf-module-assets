using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetModuleAsset), true)]
    internal class AssetModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyUnloadTrackedAssetsOnUninitialize;
        private AssetReferenceListDrawer m_listLoaders;
        private AssetReferenceListDrawer m_listGroups;
        private ReorderableListDrawer m_listPreloadAssets;
        private ReorderableListDrawer m_listPreloadAssetsAsync;

        private void OnEnable()
        {
            m_propertyUnloadTrackedAssetsOnUninitialize = serializedObject.FindProperty("m_unloadTrackedAssetsOnUninitialize");

            m_listLoaders = new AssetReferenceListDrawer(serializedObject.FindProperty("m_loaders"));
            m_listGroups = new AssetReferenceListDrawer(serializedObject.FindProperty("m_groups"));
            m_listPreloadAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_preload"));
            m_listPreloadAssetsAsync = new ReorderableListDrawer(serializedObject.FindProperty("m_preloadAsync"));

            m_listLoaders.Enable();
            m_listGroups.Enable();
            m_listPreloadAssets.Enable();
            m_listPreloadAssetsAsync.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
            m_listGroups.Disable();
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
            }
        }
    }
}
