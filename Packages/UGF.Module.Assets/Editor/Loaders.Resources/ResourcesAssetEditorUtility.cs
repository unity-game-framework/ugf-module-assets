using System;
using UGF.EditorTools.Editor.Assets;
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

                if (!string.IsNullOrEmpty(entry.Id) && !string.IsNullOrEmpty(entry.Address))
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

        public static void UpdateAssetGroupEntries(ResourcesAssetGroupAsset group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            for (int i = 0; i < group.Assets.Count; i++)
            {
                ResourcesAssetGroupAsset.Entry entry = group.Assets[i];

                if (!string.IsNullOrEmpty(entry.Id))
                {
                    string path = AssetDatabase.GUIDToAssetPath(entry.Id);

                    if (!string.IsNullOrEmpty(path) && AssetsEditorUtility.TryGetResourcesRelativePath(path, out string resourcesPath))
                    {
                        entry.Address = resourcesPath;

                        group.Assets[i] = entry;
                    }
                }
            }
        }

        public static void UpdateAllAssetGroups()
        {
            int progressId = Progress.Start("Update All Resources Assets Group");

            try
            {
                string[] guids = AssetDatabase.FindAssets($"t:{nameof(ResourcesAssetGroupAsset)}");

                for (int i = 0; i < guids.Length; i++)
                {
                    Progress.Report(progressId, i, guids.Length);

                    string guid = guids[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<ResourcesAssetGroupAsset>(path);

                    if (asset != null)
                    {
                        UpdateAssetGroupEntries(asset);

                        EditorUtility.SetDirty(asset);
                    }
                }

                Progress.Finish(progressId);
            }
            catch
            {
                Progress.Finish(progressId, Progress.Status.Failed);
                throw;
            }
            finally
            {
                AssetDatabase.SaveAssets();
            }
        }
    }
}
