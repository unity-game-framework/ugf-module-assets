using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Module", order = 2000)]
    public class AssetModuleAsset : ApplicationModuleAsset<IAssetModule, AssetModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedAssetsOnUninitialize = true;
        [SerializeField] private List<AssetIdReference<AssetLoaderAsset>> m_loaders = new List<AssetIdReference<AssetLoaderAsset>>();
        [SerializeField] private List<AssetIdReference<AssetGroupAsset>> m_groups = new List<AssetIdReference<AssetGroupAsset>>();
        [AssetId]
        [SerializeField] private List<GlobalId> m_preload = new List<GlobalId>();
        [AssetId]
        [SerializeField] private List<GlobalId> m_preloadAsync = new List<GlobalId>();

        public bool UnloadTrackedAssetsOnUninitialize { get { return m_unloadTrackedAssetsOnUninitialize; } set { m_unloadTrackedAssetsOnUninitialize = value; } }
        public List<AssetIdReference<AssetLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetIdReference<AssetGroupAsset>> Groups { get { return m_groups; } }
        public List<GlobalId> Preload { get { return m_preload; } }
        public List<GlobalId> PreloadAsync { get { return m_preloadAsync; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new AssetModuleDescription
            {
                RegisterType = typeof(IAssetModule),
                UnloadTrackedAssetsOnUninitialize = m_unloadTrackedAssetsOnUninitialize
            };

            description.PreloadAssets.AddRange(m_preload);
            description.PreloadAssetsAsync.AddRange(m_preloadAsync);

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetIdReference<AssetLoaderAsset> asset = m_loaders[i];
                IAssetLoader loader = asset.Asset.Build();

                description.Loaders.Add(asset.Guid, loader);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetGroupAsset group = m_groups[i].Asset;

                group.GetAssets(description.Assets);
            }

            return description;
        }

        protected override IAssetModule OnBuild(AssetModuleDescription description, IApplication application)
        {
            return new AssetModule(description, application);
        }
    }
}
