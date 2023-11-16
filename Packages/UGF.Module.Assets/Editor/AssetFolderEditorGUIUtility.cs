using System;
using UGF.EditorTools.Editor.Serialized;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Editor
{
    public static class AssetFolderEditorGUIUtility
    {
        private static readonly int m_objectPickFieldHash = nameof(m_objectPickFieldHash).GetHashCode();

        public static void DrawObjectPickFieldLayout(SerializedProperty serializedProperty, string filter = "")
        {
            if (serializedProperty == null) throw new ArgumentNullException(nameof(serializedProperty));

            Type targetType = SerializedPropertyEditorUtility.GetFieldType(serializedProperty);
            bool allowSceneObjects = EditorUtility.IsPersistent(serializedProperty.serializedObject.targetObject);

            serializedProperty.objectReferenceValue = DrawObjectPickFieldLayout(new GUIContent(serializedProperty.displayName), serializedProperty.objectReferenceValue, targetType, filter, allowSceneObjects);
        }

        public static Object DrawObjectPickFieldLayout(GUIContent label, Object target, Type targetType, string filter = "", bool allowSceneObjects = false)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));

            Rect position = EditorGUILayout.GetControlRect(label != GUIContent.none);

            return DrawObjectPickField(position, label, target, targetType, filter, allowSceneObjects);
        }

        public static Object DrawObjectPickField(Rect position, GUIContent label, Object target, Type targetType, string filter = "", bool allowSceneObjects = false)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            int controlId = GUIUtility.GetControlID(m_objectPickFieldHash, FocusType.Keyboard, position);
            var rectButton = new Rect(position.xMax - 20F, position.y + 1F, 19F, position.height - 2F);

            if (GUI.Button(rectButton, GUIContent.none, GUIStyle.none))
            {
                EditorGUIUtility.ShowObjectPicker<Object>(target, allowSceneObjects, filter, controlId);
                GUIUtility.ExitGUI();
            }

            EditorGUI.ObjectField(position, label, target, targetType, allowSceneObjects);

            if (Event.current.type == EventType.ExecuteCommand && EditorGUIUtility.GetObjectPickerControlID() == controlId)
            {
                target = EditorGUIUtility.GetObjectPickerObject();

                Event.current.Use();
            }

            return target;
        }

        public static void DrawControlsGUILayout(SerializedObject serializedObject)
        {
            if (serializedObject == null) throw new ArgumentNullException(nameof(serializedObject));

            if (serializedObject.targetObject is not AssetFolderAsset assetFolder)
            {
                throw new ArgumentException($"Serialized object target must be type of '{nameof(AssetFolderAsset)}'.");
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(!assetFolder.IsValid()))
                {
                    if (GUILayout.Button("Update"))
                    {
                        assetFolder.Update();
                    }
                }
            }

            EditorGUILayout.Space();

            if (!assetFolder.IsValid())
            {
                EditorGUILayout.HelpBox("Asset folder is invalid, please check that all fields specified correctly.", MessageType.Warning);
            }
        }
    }
}
