namespace UGF.Module.Assets.Runtime
{
    public interface IAssetTracker
    {
        int Count { get; }

        bool Contains(string id);
        void Add(string id, object asset);
        bool Remove(string id);
        void Clear();
        uint Increment(string id, uint value = 1);
        bool TryIncrement(string id, out uint count, uint value = 1);
        uint Decrement(string id, uint value = 1);
        bool TryDecrement(string id, out uint count, uint value = 1);
        uint GetCount(string id);
        bool TryGetCount(string id, out uint count);
        T Get<T>(string id) where T : class;
        object Get(string id);
        bool TryGet<T>(string id, out T asset) where T : class;
        bool TryGet(string id, out object asset);
    }
}
