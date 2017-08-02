using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, LinkedListNode<Pair<K, V>>> elements;
        private LinkedList<Pair<K, V>> priority;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.priority = new LinkedList<Pair<K, V>>();
            this.elements = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            foreach (var node in this.priority)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count { get { return this.elements.Count; } }

        public void Set(K key, V value)
        {
            if (!this.elements.ContainsKey(key)) // key not present
            {
                if (this.Count >= this.Capacity)
                {
                    RemoveOldestElement();
                }

                AddElement(key, value);
            }
            else // key present -> update key
            {
                var node = this.elements[key];
                this.priority.Remove(node);
                node.Value.Value = value;
                this.priority.AddFirst(node);
            }
        }

        private void AddElement(K key, V value)
        {
            LinkedListNode<Pair<K, V>> node = new LinkedListNode<Pair<K, V>>(new Pair<K, V>(key, value));
            this.elements.Add(key, node);
            this.priority.AddFirst(node);
        }

        private void RemoveOldestElement()
        {
            var node = this.priority.Last;
            this.elements.Remove(node.Value.Key);
            this.priority.RemoveLast();
        }

        public V Get(K key)
        {
            if (!this.elements.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var node = this.elements[key];
            this.priority.Remove(node);
            this.priority.AddFirst(node);
            return node.Value.Value;
        }
    }
}
