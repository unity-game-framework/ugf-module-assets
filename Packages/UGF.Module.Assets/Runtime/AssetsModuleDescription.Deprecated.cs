using System;

namespace UGF.Module.Assets.Runtime
{
    public partial class AssetsModuleDescription
    {
        [Obsolete("AssetsModuleDescription constructor with 'registerType' argument has been deprecated. Use default constructor and properties initialization instead.")]
        public AssetsModuleDescription(Type registerType) : base(registerType)
        {
        }
    }
}
