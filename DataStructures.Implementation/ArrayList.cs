using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Implementation
{
    public class ArrayList<T> : IList<T>
    {
        private T[] DefaultArray => new T[8];
        private T[] Items { get; set; }
        private int LastItemIndex { get; set; } = -1;

        public int Count => Math.Max(LastItemIndex, 0);
        public bool IsReadOnly { get; } = false;

        public ArrayList()
        {
            Items = DefaultArray;
        }

        public ArrayList(int capacity)
        {
            Items = new T[ClosestPowerOfTwo(capacity)];
        }

        public ArrayList(IEnumerable<T> items)
        {
            var itemsArray = items.ToArray();
            Items = new T[ClosestPowerOfTwo(itemsArray.Length)];
            LastItemIndex = itemsArray.Length;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = Items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (T) enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            
            if (LastItemIndex == Items.Length - 1)
            {
                var newItems = new T[ClosestPowerOfTwo(Items.Length + 1)];
                for (int i = 0; i < Items.Length; i++)
                {
                    newItems[i] = Items[i];
                }
                
                Items = newItems;
            }

            Items[++LastItemIndex] = item;
        }

        public void Clear()
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            
            Items = DefaultArray;
            LastItemIndex = -1;
        }

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(array), arrayIndex, "Invalid index");
            
            foreach (var item in array)
            {
                
            }
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        private int ClosestPowerOfTwo(int value)
        {
            var current = 1;
            while (current < value)
                current *= 2;
            return current;
        }
    }
}