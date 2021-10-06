using System;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    public static partial class ResourcesAssetEditorUtility
    {
        [Obsolete("UpdateAllAssetGroups has been deprecated. Use UpdateAssetGroupAll method instead.")]
        public static void UpdateAllAssetGroups()
        {
            ResourcesAssetEditorProgress.StartUpdateAssetGroupAll();
        }
    }
}
