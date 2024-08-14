using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Attributes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    internal class ResourcesAssetGroupAssetListDrawer : ReorderableListDrawer
    {
        public ResourcesAssetGroupAssetListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyId = serializedProperty.FindPropertyRelative("m_id");
            SerializedProperty propertyAddress = serializedProperty.FindPropertyRelative("m_address");

            AttributeEditorGUIUtility.DrawResourcesPathField(position, propertyAddress, GUIContent.none, typeof(Object));

            Object asset = UnityEngine.Resources.Load(propertyAddress.stringValue);

            if (asset != null)
            {
                string path = AssetDatabase.GetAssetPath(asset);
                string guid = AssetDatabase.AssetPathToGUID(path);

                propertyId.hash128Value = Hash128.Parse(guid);
            }
        }

        protected override float OnElementHeightContent(SerializedProperty serializedProperty, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected override bool OnElementHasVisibleChildren(SerializedProperty serializedProperty)
        {
            return false;
        }
    }
}
