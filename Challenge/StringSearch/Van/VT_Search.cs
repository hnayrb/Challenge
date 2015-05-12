using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.StringSearch.Van
{
    public class VT_Search : ISearch
    {
        private string[] _array;

        public void LoadWordSet(IEnumerable<string> unorderedWordSet)
        {
            _array = unorderedWordSet.ToArray();
            VT_HeapSortSearch.HeapSort(_array, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<string> GetOrderedWordSet()
        {
            return _array;
        }

        public bool WordExistsInSet(string wordToFind)
        {
            return string.IsNullOrEmpty(VT_HeapSortSearch.BinarySearch(_array, wordToFind, StringComparer.OrdinalIgnoreCase)) ? false : true;
        }
    }

    public class VT_HeapSortSearch
    {
        public static void SiftDown<T>(T[] array, int start, int end, Comparison<T> comparison)
        {
            int root = start;

            // As long as the root has at least one child
            while (root * 2 + 1 < end)
            {
                int left = root * 2 + 1; // left child
                int right = left + 1; // right child

                // Left child has a sibling (right child), switch to the right child if the
                // left child's value is less than the right child
                if (right < end && comparison(array[left], array[right]) < 0)
                {
                    left = right;
                }
                // Make sure that the root maintains the max-heap order
                if (comparison(array[root], array[left]) < 0)
                {
                    // Swap the root value if it is less than the left child
                    T tmp = array[root];
                    array[root] = array[left];
                    array[left] = tmp;
                    root = left;  // Continue sifting down
                }
                else
                    return;
            }
        }

        public static void Heapify<T>(T[] array, int count, Comparison<T> comparison)
        {
            // Arrays in C# are zero-based, os the number of nodes in a heap are Floor(N/2),
            // In this case, its N-1 / 2
            int start = (count - 1) / 2;

            while (start >= 0)
            {
                SiftDown(array, start, count - 1, comparison);
                start--;
            }
        }
        public static void HeapSort<T>(T[] array)
        {
            HeapSort<T>(array, Comparer<T>.Default);
        }
        public static void HeapSort<T>(T[] array, IComparer<T> comparer)
        {
            int count = array.Length;

            Heapify<T>(array, count, comparer.Compare);

            int end = count - 1;
            while (end > 0)
            {
                // Swap the root(maximum value) of the heap with the last element of the heap
                T tmp = array[end];
                array[end] = array[0];
                array[0] = tmp;

                // Maintain max-heap order
                SiftDown(array, 0, end, comparer.Compare);
                // Decrement the size of the heap so that previous max value will stay in its proper place
                end--;
            }
        }

        public static T BinarySearch<T>(T[] array, T value)
        {
            return BinarySearch<T>(array, 0, array.Length - 1, value, Comparer<T>.Default);
        }
        public static T BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
        {
            return BinarySearch<T>(array, 0, array.Length - 1, value, comparer);
        }
        public static T BinarySearch<T>(T[] array, int min, int max, T value, IComparer<T> comparer)
        {
            int mid;
            while (max >= min)
            {
                mid = (min + max) / 2;

                // At this point, the min and max are equal and we have found
                // the value of array[mid] is just equal to the value we're looking for
                if (comparer.Compare(array[mid], value) == 0)
                {
                    return array[mid];
                }
                // The value we're looking for is located to the right from the mid point
                else if (comparer.Compare(array[mid], value) < 0)
                {
                    min = mid + 1;
                }
                // The value we're looking for is located to the left from the mid point
                else
                    max = mid - 1;
            }
            return default(T); // value not found
        }
    }
}
