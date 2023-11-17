using UGF.CustomSettings.Editor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Module.Assets.Editor
{
    internal class AssetFolderAssetBuildPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            CustomSettingsEditorPackage<AssetEditorSettingsData> settings = AssetEditorSettings.Settings;

            if (settings.Exists() && settings.GetData().FoldersUpdateOnBuild)
            {
                AssetEditorSettings.UpdateFoldersAll();
            }
        }
    }
}
