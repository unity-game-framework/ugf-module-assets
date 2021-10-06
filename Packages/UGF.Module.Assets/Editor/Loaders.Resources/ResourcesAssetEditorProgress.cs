using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    internal static class ResourcesAssetEditorProgress
    {
        public static void StartUpdateAssetGroupAll()
        {
            int progressId = Progress.Start("Update All Resources Assets Group", string.Empty, Progress.Options.Indefinite);

            try
            {
                ResourcesAssetEditorUtility.UpdateAssetGroupAll();

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
