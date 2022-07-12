using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Assets.Runtime
{
    public interface IAssetInfo
    {
        GlobalId LoaderId { get; }
        string Address { get; }
    }
}
