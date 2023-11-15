using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Editor
{
    public class AssetEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_foldersUpdateOnPostprocess = true;
        [SerializeField] private List<AssetFolderAsset> m_folders = new List<AssetFolderAsset>();

        public bool FoldersUpdateOnPostprocess { get { return m_foldersUpdateOnPostprocess; } set { m_foldersUpdateOnPostprocess = value; } }
        public List<AssetFolderAsset> Folders { get { return m_folders; } }
    }
}
