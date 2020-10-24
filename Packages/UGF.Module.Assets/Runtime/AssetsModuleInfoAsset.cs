using UGF.Application.Runtime;
using UnityEngine;

namespace UGF.Module.Assets.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Assets/AssetsModuleInfo", order = 2000)]
    public class AssetsModuleInfoAsset : ApplicationModuleAsset<IAssetsModule>
    {
        protected override IAssetsModule OnBuildTyped(IApplication application)
        {
            return new AssetsResourcesModule(application);
        }
    }
}
