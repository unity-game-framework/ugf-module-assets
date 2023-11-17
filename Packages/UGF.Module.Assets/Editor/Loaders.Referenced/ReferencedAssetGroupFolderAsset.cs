using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime.Loaders.Referenced;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Referenced
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Referenced Asset Group Folder", order = 2000)]
    public class ReferencedAssetGroupFolderAsset : AssetFolderAsset
    {
        [SerializeField] private ReferencedAssetGroupAsset m_group;

        public ReferencedAssetGroupAsset Group { get { return m_group; } set { m_group = value; } }

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

                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                m_group.Assets.Add(new AssetIdReference<Object>(new GlobalId(guid), asset));
            }

            EditorUtility.SetDirty(m_group);
        }
    }
}
