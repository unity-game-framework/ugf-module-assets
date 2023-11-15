using System.Collections.Generic;
using UGF.CustomSettings.Editor;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor
{
    internal class AssetFolderAssetPostprocessor : AssetPostprocessor
    {
        private static readonly HashSet<string> m_paths = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            CustomSettingsEditorPackage<AssetEditorSettingsData> settings = AssetEditorSettings.Settings;

            if (settings.Exists())
            {
                AssetFolderEditorUtility.TryGetAssetFolderPaths(m_paths, importedAssets);
                AssetFolderEditorUtility.TryGetAssetFolderPaths(m_paths, deletedAssets);
                AssetFolderEditorUtility.TryGetAssetFolderPaths(m_paths, movedAssets);
                AssetFolderEditorUtility.TryGetAssetFolderPaths(m_paths, movedFromAssetPaths);

                if (m_paths.Count > 0)
                {
                    List<AssetFolderAsset> assetFolders = settings.GetData().Folders;

                    try
                    {
                        for (int i = 0; i < assetFolders.Count; i++)
                        {
                            AssetFolderAsset assetFolder = assetFolders[i];

                            if (assetFolder.IsValid())
                            {
                                string assetFolderPath = AssetDatabase.GetAssetPath(assetFolder.Folder);

                                if (m_paths.Contains(assetFolderPath))
                                {
                                    assetFolder.Update();
                                }
                            }
                            else
                            {
                                Debug.LogWarning($"Asset folder is invalid: '{assetFolder}'.", assetFolder);
                            }
                        }
                    }
                    finally
                    {
                        m_paths.Clear();
                    }

                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}
