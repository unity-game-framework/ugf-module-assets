using System.Collections;
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
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

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
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload("7ab173a97bcf2bc44b710c33213fa557", asset, AssetUnloadParameters.Default);

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
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload("7ab173a97bcf2bc44b710c33213fa557", asset, AssetUnloadParameters.Default);

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
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);
            object asset2 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            module.Unload("7ab173a97bcf2bc44b710c33213fa557", asset1, AssetUnloadParameters.Default);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(1, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            module.Unload("7ab173a97bcf2bc44b710c33213fa557", asset2, AssetUnloadParameters.Default);

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet("7ab173a97bcf2bc44b710c33213fa557", out _));
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
                        (IApplicationModuleBuilder)Resources.Load("Module2", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();

            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.True(module.Tracker.Entries.ContainsKey("6ecbdf2a84bc4b94794d0ccbb7164158"));
            Assert.False(module.Tracker.Entries.ContainsKey("7532bc5c40ab10644812b87b664d33ba"));

            Task task = application.InitializeAsync();

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Assert.AreEqual(2, module.Tracker.Entries.Count);
            Assert.True(module.Tracker.Entries.ContainsKey("6ecbdf2a84bc4b94794d0ccbb7164158"));
            Assert.True(module.Tracker.Entries.ContainsKey("7532bc5c40ab10644812b87b664d33ba"));
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
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);
            object asset2 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            application.Uninitialize();

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet("7ab173a97bcf2bc44b710c33213fa557", out _));
        }

        [Test]
        public void UnloadUnused()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);
            object asset2 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            module.Tracker.Update("7ab173a97bcf2bc44b710c33213fa557", new AssetTrack(asset1));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(0, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            module.UnloadUnused(false);

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet("7ab173a97bcf2bc44b710c33213fa557", out _));
        }

        [UnityTest]
        public IEnumerator UnloadUnusedAsync()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load("Module", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<IAssetsModule>();
            object asset1 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);
            object asset2 = module.Load("7ab173a97bcf2bc44b710c33213fa557", typeof(Material), AssetLoadParameters.Default);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            module.Tracker.Update("7ab173a97bcf2bc44b710c33213fa557", new AssetTrack(asset1));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(0, module.Tracker.Get("7ab173a97bcf2bc44b710c33213fa557").Count);

            Task task = module.UnloadUnusedAsync(false);

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet("7ab173a97bcf2bc44b710c33213fa557", out _));
        }
    }
}
