﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Module.Assets.Runtime.Tests
{
    public class TestAssetModule
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
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);
            Assert.AreEqual("Asset", ((Material)asset).name);
        }

        [Test]
        public void LoadReferenced()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset = module.Load<Material>(new GlobalId("d307b79fb3863804f8298a0390544dc6"));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);
            Assert.AreEqual("AssetReferenced", ((Material)asset).name);
        }

        [Test]
        public void Unload()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), asset, AssetUnloadParameters.Empty);

            Assert.Null(asset);
        }

        [UnityTest]
        public IEnumerator UnloadWithUnused()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));

            Assert.NotNull(asset);
            Assert.IsInstanceOf<Material>(asset);

            module.Unload(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), asset, AssetUnloadParameters.Empty);

            yield return Resources.UnloadUnusedAssets();

            Assert.Null(asset);
        }

        [Test]
        public void Track()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset1 = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));
            object asset2 = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get(new GlobalId("7ab173a97bcf2bc44b710c33213fa557")).Count);

            module.Unload(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), asset1, AssetUnloadParameters.Empty);

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(1, module.Tracker.Get(new GlobalId("7ab173a97bcf2bc44b710c33213fa557")).Count);

            module.Unload(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), asset2, AssetUnloadParameters.Empty);

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), out _));
        }

        [UnityTest]
        public IEnumerator Preload()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module2") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();

            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.True(module.Tracker.Entries.ContainsKey(new GlobalId("6ecbdf2a84bc4b94794d0ccbb7164158")));
            Assert.False(module.Tracker.Entries.ContainsKey(new GlobalId("7532bc5c40ab10644812b87b664d33ba")));

            Task task = application.InitializeAsync();

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Assert.AreEqual(2, module.Tracker.Entries.Count);
            Assert.True(module.Tracker.Entries.ContainsKey(new GlobalId("6ecbdf2a84bc4b94794d0ccbb7164158")));
            Assert.True(module.Tracker.Entries.ContainsKey(new GlobalId("7532bc5c40ab10644812b87b664d33ba")));
        }

        [Test]
        public void Uninitialize()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("Module") }
            }));

            application.Initialize();

            var module = application.GetModule<IAssetModule>();
            object asset1 = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));
            object asset2 = module.Load<Material>(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"));

            Assert.NotNull(asset1);
            Assert.NotNull(asset2);
            Assert.AreEqual(asset1, asset2);
            Assert.IsNotEmpty(module.Tracker.Entries);
            Assert.AreEqual(1, module.Tracker.Entries.Count);
            Assert.AreEqual(2, module.Tracker.Get(new GlobalId("7ab173a97bcf2bc44b710c33213fa557")).Count);

            application.Uninitialize();

            Assert.AreEqual(null, asset1);
            Assert.AreEqual(null, asset2);
            Assert.IsEmpty(module.Tracker.Entries);
            Assert.AreEqual(0, module.Tracker.Entries.Count);
            Assert.False(module.Tracker.TryGet(new GlobalId("7ab173a97bcf2bc44b710c33213fa557"), out _));
        }
    }
}
