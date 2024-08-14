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
        [SerializeField] private List<Hash128> m_preload = new List<Hash128>();
        [AssetId]
        [SerializeField] private List<Hash128> m_preloadAsync = new List<Hash128>();

        public bool UnloadTrackedAssetsOnUninitialize { get { return m_unloadTrackedAssetsOnUninitialize; } set { m_unloadTrackedAssetsOnUninitialize = value; } }
        public List<AssetIdReference<AssetLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetIdReference<AssetGroupAsset>> Groups { get { return m_groups; } }
        public List<Hash128> Preload { get { return m_preload; } }
        public List<Hash128> PreloadAsync { get { return m_preloadAsync; } }

        protected override AssetModuleDescription OnBuildDescription()
        {
            var loaders = new Dictionary<GlobalId, IAssetLoader>();
            var assets = new Dictionary<GlobalId, IAssetInfo>();
            var preload = new GlobalId[m_preload.Count];
            var preloadAsync = new GlobalId[m_preloadAsync.Count];

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetIdReference<AssetLoaderAsset> reference = m_loaders[i];

                loaders.Add(reference.Guid, reference.Asset.Build());
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetIdReference<AssetGroupAsset> reference = m_groups[i];

                reference.Asset.GetAssets(assets);
            }

            for (int i = 0; i < m_preload.Count; i++)
            {
                preload[i] = m_preload[i];
            }

            for (int i = 0; i < m_preloadAsync.Count; i++)
            {
                preloadAsync[i] = m_preloadAsync[i];
            }

            return new AssetModuleDescription(
                loaders,
                assets,
                preload,
                preloadAsync,
                m_unloadTrackedAssetsOnUninitialize
            );
        }

        protected override IAssetModule OnBuild(AssetModuleDescription description, IApplication application)
        {
            return new AssetModule(description, application);
        }
    }
}
