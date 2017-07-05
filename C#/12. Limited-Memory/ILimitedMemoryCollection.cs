using System.Collections.Generic;

namespace LimitedMemory
{
    public interface ILimitedMemoryCollection<K, V> : IEnumerable<Pair<K, V>>
    {
        int Capacity { get; }

        int Count { get; }

        void Set(K key, V value);

        V Get(K key);
    }
}
