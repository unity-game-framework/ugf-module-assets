﻿using System.Collections;
using System.Threading.Tasks;
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

        [UnityTest]
        public IEnumerator Preload()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleAsset)Resources.Load("Module2", typeof(IApplicationModuleAsset))
                    }
                }
            });

            var module = application.GetModule<IAssetsModule>();

            Assert.IsEmpty(module.Tracker.Tracks);

            application.Initialize();

            Assert.AreEqual(1, module.Tracker.Tracks.Count);
            Assert.True(module.Tracker.Contains("6ecbdf2a84bc4b94794d0ccbb7164158"));
            Assert.False(module.Tracker.Contains("7532bc5c40ab10644812b87b664d33ba"));

            Task task = application.InitializeAsync();

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Assert.AreEqual(2, module.Tracker.Tracks.Count);
            Assert.True(module.Tracker.Contains("6ecbdf2a84bc4b94794d0ccbb7164158"));
            Assert.True(module.Tracker.Contains("7532bc5c40ab10644812b87b664d33ba"));
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