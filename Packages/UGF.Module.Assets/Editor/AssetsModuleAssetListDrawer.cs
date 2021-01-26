using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor
{
    internal class AssetsModuleAssetListDrawer : ReorderableListDrawer
    {
        public AssetsModuleAssetListDrawer(SerializedProperty serializedProperty) : base(serializedProperty)
        {
        }

        protected override void OnDrawElementContent(Rect position, SerializedProperty serializedProperty, int index, bool isActive, bool isFocused)
        {
            SerializedProperty propertyLoader = serializedProperty.FindPropertyRelative("m_loader");
            SerializedProperty propertyAsset = serializedProperty.FindPropertyRelative("m_asset");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float indent = EditorIMGUIUtility.IndentPerLevel;

            var rectLoader = new Rect(position.x, position.y, EditorGUIUtility.labelWidth - space + indent, position.height);
            var rectAsset = new Rect(rectLoader.xMax + space, position.y, position.width - rectLoader.width, position.height);

            EditorGUI.PropertyField(rectLoader, propertyLoader, GUIContent.none);
            EditorGUI.PropertyField(rectAsset, propertyAsset, GUIContent.none);
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
