using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Editor
{
    public static class AssetFolderEditorUtility
    {
        private static readonly string[] m_searchInFolders = new string[1];

        public static bool TryUpdate(IReadOnlyList<AssetFolderAsset> assetFolders)
        {
            if (assetFolders == null) throw new ArgumentNullException(nameof(assetFolders));

            bool all = true;

            for (int i = 0; i < assetFolders.Count; i++)
            {
                AssetFolderAsset assetFolder = assetFolders[i];

                if (!TryUpdate(assetFolder))
                {
                    all = false;
                }
            }

            return all;
        }

        public static bool TryUpdate(AssetFolderAsset assetFolder)
        {
            if (assetFolder == null) throw new ArgumentNullException(nameof(assetFolder));

            if (assetFolder.IsValid())
            {
                assetFolder.Update();
                return true;
            }

            return false;
        }

        public static string[] FindAssetAsGuids(string folderPath, Type assetType)
        {
            if (string.IsNullOrEmpty(folderPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(folderPath));
            if (assetType == null) throw new ArgumentNullException(nameof(assetType));

            m_searchInFolders[0] = folderPath;

            string[] guids = AssetDatabase.FindAssets($"t:{assetType.Name}", m_searchInFolders);

            m_searchInFolders[0] = null;

            return guids;
        }

        public static void TryGetAssetFolderPaths(ICollection<string> paths, IReadOnlyList<string> assetPaths)
        {
            if (paths == null) throw new ArgumentNullException(nameof(paths));
            if (assetPaths == null) throw new ArgumentNullException(nameof(assetPaths));

            for (int i = 0; i < assetPaths.Count; i++)
            {
                string assetPath = assetPaths[i];

                if (TryGetAssetFolderPath(assetPath, out string path))
                {
                    paths.Add(path);
                }
            }
        }

        public static bool TryGetAssetFolderPath(Object asset, out string path)
        {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            path = default;

            string assetPath = AssetDatabase.GetAssetPath(asset);

            return !string.IsNullOrEmpty(assetPath) && TryGetAssetFolderPath(assetPath, out path);
        }

        public static bool TryGetAssetFolderPath(string assetPath, out string path)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetPath));

            string name = Path.GetDirectoryName(assetPath);

            if (!string.IsNullOrEmpty(name))
            {
                path = name.Replace('\\', '/');
                return true;
            }

            path = default;
            return false;
        }
    }
}
