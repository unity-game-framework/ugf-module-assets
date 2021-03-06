﻿using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Module", order = 2000)]
    public class AssetModuleAsset : ApplicationModuleAsset<IAssetModule, AssetModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedAssetsOnUninitialize = true;
        [SerializeField] private List<AssetReference<AssetLoaderAsset>> m_loaders = new List<AssetReference<AssetLoaderAsset>>();
        [SerializeField] private List<AssetReference<AssetGroupAsset>> m_groups = new List<AssetReference<AssetGroupAsset>>();
        [SerializeField, AssetGuid] private List<string> m_preload = new List<string>();
        [SerializeField, AssetGuid] private List<string> m_preloadAsync = new List<string>();

        public bool UnloadTrackedAssetsOnUninitialize { get { return m_unloadTrackedAssetsOnUninitialize; } set { m_unloadTrackedAssetsOnUninitialize = value; } }
        public List<AssetReference<AssetLoaderAsset>> Loaders { get { return m_loaders; } }
        public List<AssetReference<AssetGroupAsset>> Groups { get { return m_groups; } }
        public List<string> Preload { get { return m_preload; } }
        public List<string> PreloadAsync { get { return m_preloadAsync; } }

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
                AssetReference<AssetLoaderAsset> asset = m_loaders[i];
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
