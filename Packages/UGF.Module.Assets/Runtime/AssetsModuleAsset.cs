using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    public class AssetsModuleAsset : ApplicationModuleDescribedAsset<IAssetsModule, IAssetsModuleDescription>
    {
        [SerializeField] private List<AssetReference<AssetLoaderAssetBase>> m_loaders = new List<AssetReference<AssetLoaderAssetBase>>();

        public List<AssetReference<AssetLoaderAssetBase>> Loaders { get { return m_loaders; } }

        protected override IAssetsModuleDescription OnGetDescription(IApplication application)
        {
            var description = new AssetsModuleDescription();

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetReference<AssetLoaderAssetBase> asset = m_loaders[i];
                IAssetLoader loader = asset.Asset.Build();

                description.Loaders.Add(asset.Guid, loader);
            }

            return description;
        }

        protected override IAssetsModule OnBuild(IApplication application, IAssetsModuleDescription description)
        {
            return null;
        }
    }
}
