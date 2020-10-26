using System.Collections;
using NUnit.Framework;
using UGF.Application.Runtime;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Module.Assets.Runtime.Tests
{
    public class TestAssetsModule
    {
        [Test]
        public void LoadResources()
        {
            Object asset1 = Resources.Load("Asset", typeof(Material));
            Object asset2 = Resources.Load("Asset", typeof(Material));

            Assert.AreEqual(asset1, asset2);
        }

        [Test]
        public void UnloadResources()
        {
            Object asset1 = Resources.Load("Asset", typeof(Material));

            Resources.UnloadAsset(asset1);

            Object asset2 = Resources.Load("Asset", typeof(Material));

            Assert.AreNotEqual(asset1, asset2);
        }

        [Test]
        public void Load()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module", typeof(IApplicationModuleAsset))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("0", typeof(Material));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);
            Assert.AreEqual("Asset", ((Material)asset).name);
        }

        [Test]
        public void Unload()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module", typeof(IApplicationModuleAsset))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("0", typeof(Material));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload("0", asset);

            Assert.Null(asset);
        }

        [UnityTest]
        public IEnumerator UnloadWithUnused()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module", typeof(IApplicationModuleAsset))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("0", typeof(Material));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload("0", asset);

            yield return Resources.UnloadUnusedAssets();

            Assert.Null(asset);
        }

        [Test]
        public void Track()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module", typeof(IApplicationModuleAsset))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("0", typeof(Material));
            object asset2 = module.Load("0", typeof(Material));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Tracks);
            Assert.AreEqual(1, module.Tracker.Tracks.Count);
            Assert.AreEqual(2, module.Tracker.Get("0").Count);

            module.Unload("0", asset1);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Tracks);
            Assert.AreEqual(1, module.Tracker.Tracks.Count);
            Assert.AreEqual(1, module.Tracker.Get("0").Count);

            module.Unload("0", asset2);

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Tracks);
            Assert.AreEqual(0, module.Tracker.Tracks.Count);
            Assert.False(module.Tracker.TryGet("0", out _));
        }

        [Test]
        public void Uninitialize()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module", typeof(IApplicationModuleAsset))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("0", typeof(Material));
            object asset2 = module.Load("0", typeof(Material));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Tracks);
            Assert.AreEqual(1, module.Tracker.Tracks.Count);
            Assert.AreEqual(2, module.Tracker.Get("0").Count);

            application.Uninitialize();

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Tracks);
            Assert.AreEqual(0, module.Tracker.Tracks.Count);
            Assert.False(module.Tracker.TryGet("0", out _));
        }
    }
}
