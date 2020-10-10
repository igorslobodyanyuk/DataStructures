using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Implementation
{
    public class StringBuilder
    {
        private ArrayList<char> String { get; }

        public StringBuilder(string initialString = "")
        {
            String = new ArrayList<char>(initialString);
        }

        public StringBuilder Append(StringBuilder sb)
        {
            return this.Append(sb.ToString());
        }


        public StringBuilder Append(string s)
        {
            String.AddRange(s);

            return this;
        }

        public StringBuilder Append(char c)
        {
            String.Add(c);

            return this;
        }

        public StringBuilder Insert(int index, string str, int instances = 1)
        {
            if (index > String.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (instances < 0)
                throw new ArgumentOutOfRangeException(nameof(instances));
            if (instances == 0)
                return this;
            
            var charsToAdd = new ArrayList<char>(instances * str.Length);
            for (int i = 0; i < instances; i++)
            {
                charsToAdd.AddRange(str);
            }

            String.InsertRange(index, charsToAdd);

            return this;
        }

        public StringBuilder Remove(int startIndex, int length)
        {
            if (startIndex > String.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (startIndex + length > String.Count)
                throw new ArgumentOutOfRangeException(nameof(length));

            String.RemoveRange(startIndex, length);

            return this;
        }

        public StringBuilder Replace(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                return this;

            var replacementIndexes = new List<int>();
            var replacementArray = s2.ToCharArray();
            for (int i = 0; i < String.Count; i++)
            {
                if (IsSubstringEqual(String, i, replacementArray))
                {
                    replacementIndexes.Add(i);
                }
            }

            // Reverse the order in order not to affect indexes for replacement.
            // Suboptimal solution. Has plenty of room for improvement.
            foreach (var replacementIndex in replacementIndexes.OrderByDescending(i => i))
            {
                String.RemoveRange(replacementIndex, s2.Length);
                String.InsertRange(replacementIndex, s2);
            }

            return this;
        }


        public StringBuilder Clear()
        {
            String.Clear();

            return this;
        }

        public override string ToString()
        {
            return new string(String.ToArray());
        }

        private bool IsSubstringEqual(IList<char> source, int startIndex, IList<char> target)
        {
            if (startIndex + target.Count > source.Count)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < target.Count; i++)
            {
                if (source[i + startIndex] != target[i])
                    return false;
            }

            return true;
        }
    }
}