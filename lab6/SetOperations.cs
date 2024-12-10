using System;

namespace lab6
{
    public class CustomSet : IComparable
    {
        private int[] elements = new int[0];  

        public int Count => elements.Length;

        public int this[int index] => elements[index];


        public CustomSet(string firstName, string lastName, DateTime dateOfBirth)
        {
            
        }

        public CustomSet()
        {
            
        }


        public static bool operator ==(CustomSet left, CustomSet right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;

            if (left.Count != right.Count) return false;

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] != right[i]) return false;
            }
            return true;
        }

        public static bool operator !=(CustomSet left, CustomSet right)
        {
            return !(left == right);
        }

        public static bool operator >(CustomSet left, CustomSet right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(CustomSet left, CustomSet right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >=(CustomSet left, CustomSet right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(CustomSet left, CustomSet right)
        {
            return left.CompareTo(right) <= 0;
        }



        public void Add(int element)
        {
            if (Array.IndexOf(elements, element) < 0)
            {
                Array.Resize(ref elements, elements.Length + 1);  
                elements[elements.Length - 1] = element; 
            }
        }
        public void Update(int index, int newValue)
        {
            if (index >= 0 && index < elements.Length) 
            {
                elements[index] = newValue;
            }
        }
        /*
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
        */

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < elements.Length)
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
    }
}
 