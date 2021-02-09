using UGF.CustomSettings.Editor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    internal class ResourcesAssetEditorBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            CustomSettingsEditorPackage<ResourcesAssetEditorSettingsData> settings = ResourcesAssetEditorSettings.Settings;

            if (settings.Exists() && settings.GetData().UpdateAllGroupsOnBuild)
            {
                ResourcesAssetEditorUtility.UpdateAllAssetGroups();
            }
        }
    }
}
