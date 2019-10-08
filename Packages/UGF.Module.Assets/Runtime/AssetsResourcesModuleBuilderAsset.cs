using UGF.Application.Runtime;
using UGF.Module.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Assets/AssetsResourcesModuleBuilder", order = 2000)]
    public class AssetsResourcesModuleBuilderAsset : ModuleBuilderAsset<IAssetsModule>
    {
        protected override IApplicationModule OnBuild(IApplication application, IModuleBuildArguments<object> arguments)
        {
            return new AssetsResourcesModule();
        }
    }
}
