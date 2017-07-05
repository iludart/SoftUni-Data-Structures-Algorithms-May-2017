namespace LimitedMemory
{
    public class Pair<K, V>
    {
        public Pair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public K Key { get; set; }

        public V Value { get; set; }
    }
}
