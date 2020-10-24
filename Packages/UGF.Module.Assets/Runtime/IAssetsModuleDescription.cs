using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetsModuleDescription : IApplicationModuleDescription
    {
        IReadOnlyDictionary<string, IAssetLoader> Loaders { get; }
    }
}
