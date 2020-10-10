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
            
            ExpandSpaceForNewItemIfNeeded();

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
            if (arrayIndex + this.Count > array.Length)
                throw new ArgumentException();

            var i = arrayIndex;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            var index = this.IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);

            return true;
        }

        public int IndexOf(T item)
        {
            var comparer = Comparer<T>.Default;
            for (var index = 0; index < Items.Length; index++)
            {
                var currentItem = Items[index];
                if (comparer.Compare(currentItem, item) == 0)
                    return index;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (this.Count < index)
                throw new ArgumentOutOfRangeException(nameof(index));

            ExpandSpaceForNewItemIfNeeded();

            for (int i = index + 1; i < LastItemIndex; i++)
            {
                Items[i] = Items[i - 1];
            }

            Items[index] = item;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (index > LastItemIndex)
                throw new ArgumentOutOfRangeException(nameof(index));

            LastItemIndex--;
            for (int i = index; i < LastItemIndex; i++)
            {
                Items[i] = Items[i + 1];
            }
        }

        public T this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        private int ClosestPowerOfTwo(int value)
        {
            var current = 1;
            while (current < value)
                current *= 2;
            return current;
        }

        private void ExpandSpaceForNewItemIfNeeded()
        {
            if (LastItemIndex == Items.Length - 1)
            {
                var newItems = new T[ClosestPowerOfTwo(Items.Length + 1)];
                for (int i = 0; i < Items.Length; i++)
                {
                    newItems[i] = Items[i];
                }

                Items = newItems;
            }
        }
    }
}