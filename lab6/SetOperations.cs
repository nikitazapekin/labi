using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class CustomSet : IComparable
    {
        private int[] elements = new int[0];

        public int Count => elements.Length;

        public int this[int index] => elements[index];

        public void Add(int element)
        {
            if (Array.IndexOf(elements, element) < 0)
            {
                Array.Resize(ref elements, elements.Length + 1);
                elements[^1] = element;
            }
        }

        public void Remove(int element)
        {
            int index = Array.IndexOf(elements, element);
            if (index >= 0)
            {
                for (int i = index; i < elements.Length - 1; i++)
                {
                    elements[i] = elements[i + 1];
                }
                Array.Resize(ref elements, elements.Length - 1);
            }
        }

        public CustomSet Union(CustomSet other)
        {
            var result = new CustomSet();
            foreach (var elem in elements) result.Add(elem);
            foreach (var elem in other.elements) result.Add(elem);
            return result;
        }

        public CustomSet Intersect(CustomSet other)
        {
            var result = new CustomSet();
            foreach (var elem in elements)
            {
                if (Array.IndexOf(other.elements, elem) >= 0)
                {
                    result.Add(elem);
                }
            }
            return result;
        }

        public CustomSet Difference(CustomSet other)
        {
            var result = new CustomSet();
            foreach (var elem in elements)
            {
                if (Array.IndexOf(other.elements, elem) < 0)
                {
                    result.Add(elem);
                }
            }
            return result;
        }

        public void Sort()
        {
            for (int i = 0; i < elements.Length - 1; i++)
            {
                for (int j = i + 1; j < elements.Length; j++)
                {
                    if (elements[i] > elements[j])
                    {
                        int temp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = temp;
                    }
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is CustomSet other)
            {
                return Count.CompareTo(other.Count);
            }
            throw new ArgumentException("Object is not a CustomSet");
        }

        public override string ToString()
        {
            return string.Join(", ", elements);
        }

        public int[] ToArray()
        {
            return (int[])elements.Clone();
        }


        /*   internal int[] ToArray()
           {
               throw new NotImplementedException();
           } */
    }
}


