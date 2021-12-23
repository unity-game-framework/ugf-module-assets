using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.Assets.Runtime.Loaders.Referenced;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Referenced
{
    [CustomEditor(typeof(ReferencedAssetGroupAsset), true)]
    internal class ReferencedAssetGroupAssetEditor : UnityEditor.Editor
    {
        private AssetReferenceListDrawer m_listAssets;

        private void OnEnable()
        {
            m_listAssets = new AssetReferenceListDrawer(serializedObject.FindProperty("m_assets"));
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

                m_listAssets.DrawGUILayout();
            }
        }
    }
}
