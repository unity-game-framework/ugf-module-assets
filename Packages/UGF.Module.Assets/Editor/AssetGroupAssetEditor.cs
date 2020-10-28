using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    public abstract class AssetGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private ReorderableListDrawer m_list;

        protected virtual void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_list = OnCreateListDrawer(serializedObject.FindProperty("m_assets"));

            m_list.Enable();
        }

        protected virtual void OnDisable()
        {
            m_list.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            OnDrawProperties();
            OnDrawAssetsList(m_list);

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnDrawProperties()
        {
            EditorGUILayout.PropertyField(m_propertyLoader);
        }

        protected virtual void OnDrawAssetsList(ReorderableListDrawer drawer)
        {
            drawer.DrawGUILayout();
        }

        protected virtual ReorderableListDrawer OnCreateListDrawer(SerializedProperty serializedProperty)
        {
            return new ReorderableListDrawer(serializedProperty);
        }
    }
}
