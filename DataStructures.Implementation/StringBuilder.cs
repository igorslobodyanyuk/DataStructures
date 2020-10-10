using System;
using System.Linq;
using System.Runtime.Serialization;

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
            return this;
        }

        public StringBuilder Replace(string s1, string s2)
        {
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
    }
}