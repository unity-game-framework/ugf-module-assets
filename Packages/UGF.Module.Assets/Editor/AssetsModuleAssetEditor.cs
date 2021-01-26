using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    [CustomEditor(typeof(AssetsModuleAsset), true)]
    internal class AssetsModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyUnloadTrackedAssetsOnUninitialize;
        private AssetReferenceListDrawer m_listLoaders;
        private AssetsModuleAssetListDrawer m_listAssets;
        private AssetReferenceListDrawer m_listGroups;
        private ReorderableListDrawer m_listPreloadAssets;
        private ReorderableListDrawer m_listPreloadAssetsAsync;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyUnloadTrackedAssetsOnUninitialize = serializedObject.FindProperty("m_unloadTrackedAssetsOnUninitialize");

            m_listLoaders = new AssetReferenceListDrawer(serializedObject.FindProperty("m_loaders"));
            m_listAssets = new AssetsModuleAssetListDrawer(serializedObject.FindProperty("m_assets"));
            m_listGroups = new AssetReferenceListDrawer(serializedObject.FindProperty("m_groups"));
            m_listPreloadAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_preload"));
            m_listPreloadAssetsAsync = new ReorderableListDrawer(serializedObject.FindProperty("m_preloadAsync"));

            m_listLoaders.Enable();
            m_listAssets.Enable();
            m_listGroups.Enable();
            m_listPreloadAssets.Enable();
            m_listPreloadAssetsAsync.Enable();
        }

        private void OnDisable()
        {
            m_listLoaders.Disable();
            m_listAssets.Disable();
            m_listGroups.Disable();
            m_listPreloadAssets.Disable();
            m_listPreloadAssetsAsync.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyUnloadTrackedAssetsOnUninitialize);

                m_listLoaders.DrawGUILayout();
                m_listAssets.DrawGUILayout();
                m_listGroups.DrawGUILayout();
                m_listPreloadAssets.DrawGUILayout();
                m_listPreloadAssetsAsync.DrawGUILayout();
            }
        }
    }
}
