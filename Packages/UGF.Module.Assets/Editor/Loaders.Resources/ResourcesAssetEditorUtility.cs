using System;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    public static class ResourcesAssetEditorUtility
    {
        public static bool IsAssetGroupHasMissingEntries(ResourcesAssetGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Assets.Count; i++)
            {
                ResourcesAssetGroupAsset.Entry entry = group.Assets[i];

                if (entry.Id != GlobalId.Empty && !string.IsNullOrEmpty(entry.Address))
                {
                    Object asset = UnityEngine.Resources.Load(entry.Address);

                    if (asset == null)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static void UpdateAssetGroupAll()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(ResourcesAssetGroupAsset)}");

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ResourcesAssetGroupAsset>(path);

                if (asset != null)
                {
                    UpdateAssetGroupEntries(asset);

                    EditorUtility.SetDirty(asset);
                }
            }
        }

        public static void UpdateAssetGroupEntries(ResourcesAssetGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Assets.Count; i++)
            {
                ResourcesAssetGroupAsset.Entry entry = group.Assets[i];

                if (entry.Id != GlobalId.Empty)
                {
                    string path = AssetDatabase.GUIDToAssetPath(entry.Id.ToString());

                    if (!string.IsNullOrEmpty(path) && AssetsEditorUtility.TryGetResourcesRelativePath(path, out string resourcesPath))
                    {
                        entry.Address = resourcesPath;

                        group.Assets[i] = entry;
                    }
                }
            }
        }
    }
}
