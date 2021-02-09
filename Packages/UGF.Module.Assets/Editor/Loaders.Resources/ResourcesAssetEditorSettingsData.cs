using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Editor.Loaders.Resources
{
    public class ResourcesAssetEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_updateAllGroupsOnBuild = true;

        public bool UpdateAllGroupsOnBuild { get { return m_updateAllGroupsOnBuild; } set { m_updateAllGroupsOnBuild = value; } }
    }
}
