using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Assets Module", order = 2000)]
    public class AssetsModuleAsset : ApplicationModuleAsset<IAssetsModule, AssetsModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedAssetsOnUninitialize = true;
        [SerializeField] private List<AssetReference<AssetLoaderAsset>> m_loaders = new List<AssetReference<AssetLoaderAsset>>();
        [SerializeField] private List<AssetEntry> m_assets = new List<AssetEntry>();
        [SerializeField] private List<AssetReference<AssetGroupAsset>> m_groups = new List<AssetReference<AssetGroupAsset>>();
        [SerializeField, AssetGuid] private List<string> m_preload = new List<string>();
        [SerializeField, AssetGuid] private List<string> m_preloadAsync = new List<string>();

        public bool UnloadTrackedAssetsOnUninitialize { get { return m_unloadTrackedAssetsOnUninitialize; } set { m_unloadTrackedAssetsOnUninitialize = value; } }
        public List<AssetReference<AssetLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetReference<AssetGroupAsset>> Groups { get { return m_groups; } }
        public List<AssetEntry> Assets { get { return m_assets; } }
        public List<string> Preload { get { return m_preload; } }
        public List<string> PreloadAsync { get { return m_preloadAsync; } }

        [Serializable]
        public struct AssetEntry
        {
            [AssetGuid(typeof(AssetLoaderAsset))]
            [SerializeField] private string m_loader;
            [SerializeField, AssetGuid] private string m_asset;

            public string Loader { get { return m_loader; } set { m_loader = value; } }
            public string Asset { get { return m_asset; } set { m_asset = value; } }
        }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new AssetsModuleDescription
            {
                RegisterType = typeof(IAssetsModule),
                UnloadTrackedAssetsOnUninitialize = m_unloadTrackedAssetsOnUninitialize
            };

            description.PreloadAssets.AddRange(m_preload);
            description.PreloadAssetsAsync.AddRange(m_preloadAsync);

            for (int i = 0; i < m_loaders.Count; i++)
            {
                AssetReference<AssetLoaderAsset> asset = m_loaders[i];
                IAssetLoader loader = asset.Asset.Build();

                description.Loaders.Add(asset.Guid, loader);
            }

            for (int i = 0; i < m_assets.Count; i++)
            {
                AssetEntry asset = m_assets[i];
                var info = new AssetInfo(asset.Loader, asset.Asset);

                description.Assets.Add(asset.Asset, info);
            }

            for (int i = 0; i < m_groups.Count; i++)
            {
                AssetGroupAsset group = m_groups[i].Asset;

                group.GetAssets(description.Assets);
            }

            return description;
        }

        protected override IAssetsModule OnBuild(AssetsModuleDescription description, IApplication application)
        {
            return new AssetsModule(description, application);
        }
    }
}
