using System;
using System.Collections.Generic;

namespace DataStructures.Implementation
{
    public class HashTable<K,V>
    {
        private readonly int size;
        private readonly LinkedList<KeyValue<K,V>>[] items;

        public HashTable(int size)
        {
            this.size = size;
            items = new LinkedList<KeyValue<K,V>>[size];
        }

        public V Find(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            foreach (KeyValue<K,V> item in linkedList)
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return default;
        }

        public void Add(K key, V value)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
            linkedList.AddLast(item);
        }

        public void Remove(K key)
        {
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(GetArrayPosition(key));
            var foundItem = Find(key);

            if (!foundItem.Equals(default))
            {
                linkedList.Remove(new KeyValue<K, V> {Key = key, Value = foundItem});
            }
        }

        private int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }

        protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
        {
            LinkedList<KeyValue<K, V>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<KeyValue<K, V>>();
                items[position] = linkedList;
            }

            return linkedList;
        }

        protected struct KeyValue<K, V>
        {
            public K Key { get; set; }
            public V Value { get; set; }
        }
    }
}