using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Application Modules/Assets Module", order = 2000)]
    public class AssetsModuleAsset : ApplicationModuleDescribedAsset<IAssetsModule, AssetsModuleDescription>
    {
        [SerializeField] private AssetProviderAssetBase m_assetProvider;
        [SerializeField] private bool m_unloadTrackedAssetsOnUninitialize = true;
        [SerializeField] private List<AssetReference<AssetLoaderAssetBase>> m_loaders = new List<AssetReference<AssetLoaderAssetBase>>();
        [SerializeField] private List<AssetReference<AssetGroupAssetBase>> m_groups = new List<AssetReference<AssetGroupAssetBase>>();
        [AssetGuid(typeof(Object))]
        [SerializeField] private List<string> m_preload = new List<string>();
        [AssetGuid(typeof(Object))]
        [SerializeField] private List<string> m_preloadAsync = new List<string>();

        public AssetProviderAssetBase AssetProvider { get { return m_assetProvider; } set { m_assetProvider = value; } }
        public bool UnloadTrackedAssetsOnUninitialize { get { return m_unloadTrackedAssetsOnUninitialize; } set { m_unloadTrackedAssetsOnUninitialize = value; } }
        public List<AssetReference<AssetLoaderAssetBase>> Loaders { get { return m_loaders; } }
        public List<AssetReference<AssetGroupAssetBase>> Groups { get { return m_groups; } }
        public List<string> Preload { get { return m_preload; } }
        public List<string> PreloadAsync { get { return m_preloadAsync; } }

        protected override AssetsModuleDescription OnGetDescription(IApplication application)
        {
            var description = new AssetsModuleDescription
            {
                UnloadTrackedAssetsOnUninitialize = m_unloadTrackedAssetsOnUninitialize
            };

            description.PreloadAssets.AddRange(m_preload);
            description.PreloadAssetsAsync.AddRange(m_preloadAsync);

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetReference<AssetLoaderAssetBase> asset = m_loaders[i];
                IAssetLoader loader = asset.Asset.Build();

                description.Loaders.Add(asset.Guid, loader);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetReference<AssetGroupAssetBase> asset = m_groups[i];
                IAssetGroup group = asset.Asset.Build();

                description.Groups.Add(asset.Guid, group);
            }

            return description;
        }

        protected override IAssetsModule OnBuild(IApplication application, AssetsModuleDescription description)
        {
            IAssetProvider provider = m_assetProvider != null ? m_assetProvider.Build() : throw new ArgumentException("Asset provider not specified.");

            return new AssetsModule(application, description, provider);
        }
    }
}
