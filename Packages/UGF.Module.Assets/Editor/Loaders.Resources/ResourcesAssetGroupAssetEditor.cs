using UGF.EditorTools.Editor.IMGUI;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    [CustomEditor(typeof(ResourcesAssetGroupAsset), true)]
    internal class ResourcesAssetGroupAssetEditor : AssetGroupAssetEditor
    {
        protected override ReorderableListDrawer OnCreateListDrawer(SerializedProperty serializedProperty)
        {
            return new ResourcesAssetGroupAssetListDrawer(serializedProperty);
        }
    }
}
