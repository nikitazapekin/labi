using System;

namespace lab6
{
    public class CustomSet : IComparable
    {
        private int[] elements = new int[0]; // Use a simple array for dynamic resizing

        public int Count => elements.Length;

        public int this[int index] => elements[index];

        public void Add(int element)
        {
            if (Array.IndexOf(elements, element) < 0) // Check if element already exists
            {
                Array.Resize(ref elements, elements.Length + 1);
                elements[^1] = element; // Add the element at the end
            }
        }
        public void Update(int index, int newValue)
        {
            if (index >= 0 && index < elements.Length) // Ensure index is valid
            {
                elements[index] = newValue;
            }
        }

        public void Remove(int element)
        {
            int index = Array.IndexOf(elements, element);
            if (index >= 0) // If the element exists
            {
                for (int i = index; i < elements.Length - 1; i++)
                {
                    elements[i] = elements[i + 1]; // Shift elements left
                }
                Array.Resize(ref elements, elements.Length - 1); // Resize array to remove the last element
            }
        }

        public CustomSet Union(CustomSet other)
        {
            var result = new CustomSet();
            foreach (var elem in elements)
            {
                result.Add(elem);
            }
            foreach (var elem in other.elements)
            {
                result.Add(elem);
            }
            return result;
        }

        public CustomSet Intersect(CustomSet other)
        {
            var result = new CustomSet();
            foreach (var elem in elements)
            {
                if (Array.IndexOf(other.elements, elem) >= 0) // Check if element exists in the other set
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
                if (Array.IndexOf(other.elements, elem) < 0) // Element doesn't exist in the other set
                {
                    result.Add(elem);
                }
            }
            return result;
        }

        public void Sort()
        {
            // Simple Bubble Sort for sorting the array
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
    }
}
 