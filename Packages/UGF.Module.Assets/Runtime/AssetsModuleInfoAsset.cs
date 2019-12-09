using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Assets/AssetsModuleInfo", order = 2000)]
    public class AssetsModuleInfoAsset : ApplicationModuleInfoAsset<IAssetsModule>
    {
        protected override IApplicationModule OnBuild(IApplication application)
        {
            return new AssetsResourcesModule();
        }
    }
}
