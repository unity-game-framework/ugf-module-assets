using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    public static class ResourcesAssetEditorSettings
    {
        public static CustomSettingsEditorPackage<ResourcesAssetEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<ResourcesAssetEditorSettingsData>
        (
            "UGF.Module.Assets",
            "ResourcesAssetEditorSettings"
        );

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<ResourcesAssetEditorSettingsData>("Project/UGF/Assets Resources", Settings, SettingsScope.Project);
        }
    }
}
