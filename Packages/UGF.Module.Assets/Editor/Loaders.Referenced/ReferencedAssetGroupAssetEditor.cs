using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime.Loaders.Referenced;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Referenced
{
    [CustomEditor(typeof(ReferencedAssetGroupAsset), true)]
    internal class ReferencedAssetGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private AssetIdReferenceListDrawer m_listAssets;

        private void OnEnable()
        {
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_listAssets = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_assets"));
            m_listAssets.Enable();
        }

        private void OnDisable()
        {
            m_listAssets.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyLoader);

                m_listAssets.DrawGUILayout();
            }
        }
    }
}
