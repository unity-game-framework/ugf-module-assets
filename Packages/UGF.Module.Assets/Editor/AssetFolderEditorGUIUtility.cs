using System;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor
{
    public static class AssetFolderEditorGUIUtility
    {
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
