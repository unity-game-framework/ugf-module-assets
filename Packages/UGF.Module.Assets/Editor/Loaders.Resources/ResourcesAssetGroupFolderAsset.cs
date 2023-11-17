using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime.Loaders.Resources;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Resources Asset Group Folder", order = 2000)]
    public class ResourcesAssetGroupFolderAsset : AssetFolderAsset
    {
        [SerializeField] private ResourcesAssetGroupAsset m_group;

        public ResourcesAssetGroupAsset Group { get { return m_group; } set { m_group = value; } }

        protected override bool OnIsValid()
        {
            return m_group != null;
        }

        protected override void OnUpdate()
        {
            m_group.Assets.Clear();

            string[] guids = FindAssetsAsGuids();

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = AssetDatabase.GUIDToAssetPath(guid);

                if (!string.IsNullOrEmpty(path) && AssetsEditorUtility.TryGetResourcesRelativePath(path, out string resourcesPath))
                {
                    m_group.Assets.Add(new ResourcesAssetGroupAsset.Entry
                    {
                        Id = new GlobalId(guid),
                        Address = resourcesPath
                    });
                }
            }

            EditorUtility.SetDirty(m_group);
        }
    }
}
