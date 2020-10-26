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
    }
}
