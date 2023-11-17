using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.Assets.Editor
{
    public static class AssetEditorSettings
    {
        public static CustomSettingsEditorPackage<AssetEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<AssetEditorSettingsData>
        (
            "UGF.Module.Assets",
            nameof(AssetEditorSettings)
        );

        public static bool UpdateFoldersAll()
        {
            AssetEditorSettingsData data = Settings.GetData();

            return AssetFolderEditorUtility.TryUpdate(data.Folders);
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<AssetEditorSettingsData>("Project/Unity Game Framework/Assets", Settings, SettingsScope.Project);
        }
    }
}
