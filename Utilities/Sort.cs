namespace Utilities
{
    public static class Sort
    {
        public static void BubbleSort(int[] array)
        {
            bool swapped = true;
            int n = array.Length;
            while (swapped)
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        Swap(ref array[i-1], ref array[i]);
                        swapped = true;
                    }
                }
                n--;
            }
        }
        public static void SelectionSort(int[] array)
        {
            for (int currentIndex = 0; currentIndex < array.Length - 1; currentIndex++)
            {
                for (int i = currentIndex + 1; i < array.Length; i++)
                {
                    if (array[i] < array[currentIndex])
                    {
                        Swap(ref array[currentIndex], ref array[i]);
                    }
                }
            }
        }

        public static void InsertionSort(int[] array)
        {
            for (int currentIndex = 1; currentIndex < array.Length; currentIndex++)
            {
                for (int i = currentIndex; i > 0; i--)      
                {                                    
                    if (array[i] < array[i - 1])  
                    {                             
                        Swap(ref array[i], ref array[i-1]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static void InsertionSort(List<float> list)
        {      
            for (int currentIndex = 1; currentIndex < list.Count; currentIndex++)
            {
                int i = currentIndex;
                while (i > 0 && (list[i] < list[i - 1]))
                {
                    SwapList(list, i, i - 1);
                    i--;       
                }
            }
        }

        public static int[] MergeSort(int[] array)
        {                    
            if (array.Length <= 2) 
            {
                if ((array.Length == 2) && (array[0] > array[1]))
                {
                    Swap(ref array[0], ref array[1]);
                }
                return array;
            }
            int mid = (array.Length / 2) + (array.Length % 2); 
            int[] left = array[..mid];
            int[] right = array[mid..];
            int[] sortedLeft = MergeSort(left);
            int[] sortedRight = MergeSort(right);
            return Merge(sortedLeft, sortedRight);

            int[] Merge(int[] array1, int[] array2)
            {
                int length1 = array1.Length;
                int length2 = array2.Length;
                int[] result = new int[length1 + length2];
                int n = 0;
                while (length1 > 0 && length2 > 0)
                {
                    if (array1[^length1] < array2[^length2])
                    {
                        result[n++] = array1[^length1];
                        length1--;
                    }
                    else
                    {
                        result[n++] = array2[^length2];
                        length2--;
                    }
                }
                while (length1 > 0)
                {
                    result[n++] = array1[^length1];
                    length1--;
                }
                while (length2 > 0)
                {
                    result[n++] = array2[^length2];
                    length2--;
                }
                return result;
            }
        }

        public static void QuickSort(int[] array)                
        {
            QuickSortAlgo(array, 0, array.Length - 1);

            void QuickSortAlgo(int[] array, int start, int end)
        {
                if (end - start <= 1) return;
                Swap(ref array[PickPivot(array, start, end)], ref array[end]);
                int left = start;
                int right = end - 1;
                bool swapLeft = false;
                bool swapRight = false;
                while (left <= right)
                {
                    if (array[left] > array[end])
                    {
                        swapLeft = true;
                    }
                    else
                    {
                        swapLeft = false;
                        left++;
                    }
                    if (array[right] < array[end])
                    {
                        swapRight = true;
                    }
                    else
                    {
                        swapRight = false;
                        right--;
                    }
                    if (swapLeft && swapRight)
                    {
                        Swap(ref array[left], ref array[right]);
                    }
                }
                Swap(ref array[left], ref array[end]);
                QuickSortAlgo(array, start, left - 1);
                QuickSortAlgo(array, left + 1, end);
            }

            int PickPivot(int[] array, int start, int end)
            {
                if (array.Length <= 2) return start;
                int mid = (start + end) / 2;
                if (array[start] < array[end] && array[start] > array[mid]) return start;
                if (array[start] > array[end] && array[start] < array[mid]) return start;
                if (array[end] < array[start] && array[end] > array[mid]) return end;
                if (array[end] > array[start] && array[end] < array[mid]) return end;
                return mid;
            }
        }

        public static int[] CountingSort(int[] array)  
        {                                   
            int n = array.Length;          
            int k = GetMax(array) + 1;       
            int[] frequency = new int[k];      
            int[] result = new int[n];      
            for (int i = 0; i < n; i++)        
            {                              
                frequency[array[i]]++;          
            }
            frequency[0]--;
            for (int i = 1; i < k; i++)                 
            {                                       
                frequency[i] += frequency[i - 1];     
            }                               
            for (int i = n - 1; i >= 0; i--)
            {                                             
                result[frequency[array[i]]--] = array[i];  
            }
            return result;

            int GetMax(int[] array)
            {
                int max = array[0];
                for (int i = 1; i < array.Length; i++)
                {
                    max = array[i] > max ? array[i] : max;
                }
                return max;
            }
        }

        public static int[] RadixSort(int[] array)      
        {                                    
            int n = array.Length;           
            int m = GetMax(array);         
            int b = CalculateBase(n, m);      
            int[] result = array;     
            for (int s = 1; m / s > 0; s *= b) 
            {
                result = CountingSortAlgo(result, n, b, s);
            }                     
            return result;

            int GetMax(int[] array)
            {
                int max = array[0];
                for (int i = 1; i < array.Length; i++)
                {
                    max = array[i] > max ? array[i] : max;
                }
                return max;
            }

            int CalculateBase(int arrayLength, int maxElement)
            {
                if (arrayLength <= 1 || maxElement <= 1) return 10;
                float length = (float)arrayLength;
                int resultLength = 1;
                while (length > 1)
                {
                    length = length / 10;
                    resultLength = resultLength * 10;
                }
                if (Math.Round(length, MidpointRounding.AwayFromZero) < 1)
                {
                    resultLength = resultLength / 10;
                }
                int resultMax = 1;
                while (maxElement >= 1)
                {
                    maxElement = maxElement / 10;
                    resultMax = resultMax * 10;
                }
                return resultLength < resultMax ? resultLength : resultMax;
            }

            int[] CountingSortAlgo(int[] array, int n, int b, int s)
            {
                int[] frequency = new int[b];
                int[] result = new int[n];
                for (int i = 0; i < n; i++)
                {
                    frequency[(array[i] / s) % b]++;
                }
                frequency[0]--;
                for (int i = 1; i < b; i++)
                {
                    frequency[i] += frequency[i - 1];
                }
                for (int i = n - 1; i >= 0; i--)
                {
                    result[frequency[(array[i] / s) % b]--] = array[i];
                }
                return result;
            }
        }

        public static void BucketSort(float[] array)  
        {                                    
            const int amount = 10;            
            List<float>[] buckets = new List<float>[amount];  
            for (int n = 0; n < amount; n++) 
            {
                buckets[n] = new List<float>();
            }
            foreach (float number in array)      
            {
                buckets[(int)(number * amount)].Add(number);
            }
            foreach (List<float> bucket in buckets) 
            {
                InsertionSort(bucket);
            }
            int i = 0;
            while (i < array.Length)      
            {
                foreach (List<float> bucket in buckets)
                {
                    foreach (float number in bucket)
                    {
                        array[i++] = number;
                    }
                }
            }
        }

        public static void HeapSortMax<T>(T[] array) where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            int lastNode = array.Length - 1;  
            BuildHeap(array, lastNode);

            void BuildHeap<T>(T[] array, int lastNode) where T : unmanaged, IComparable<T>, IEquatable<T>
            {
                int startNode = GetParentNode(lastNode);
                int stopNode = GetParentNode(startNode);
                for (int i = startNode; i > stopNode; i--)
                {
                    Heapify(array, i, lastNode);
                }
                Swap(ref array[0], ref array[lastNode]);
                if (lastNode > 1)
                    BuildHeap(array, lastNode - 1);
            }

            void Heapify<T>(T[] array, int parentNode, int lastNode) where T : unmanaged, IComparable<T>, IEquatable<T>
            {
                int leftNode = parentNode * 2 + 1;
                int rightNode = parentNode * 2 + 2;
                if (array[leftNode].CompareTo(array[parentNode]) >= 0)
                {
                    Swap(ref array[leftNode], ref array[parentNode]);
                }
                if (rightNode <= lastNode && array[rightNode].CompareTo(array[parentNode]) >= 0)
                {
                    Swap(ref array[rightNode], ref array[parentNode]);
                }
                if (parentNode != 0)
                {
                    Heapify(array, GetParentNode(parentNode), lastNode);
                }
            }

            int GetParentNode(int child)
            {
                if (child == 1 || child == 2)
                    return 0;
                if (child % 2 == 0)
                    return (child - 2) / 2;
                else
                    return (child - 1) / 2;
            }
        }

        public static List<T> BinarySort<T>(List<T> array) where T : unmanaged, IComparable<T>, IEquatable<T>
        {                                      
            var result = new List<T>();          
            result.Add(array[0]);            
            for (int i = 1; i < array.Count; i++)   
            {                                
                result.Insert(BinarySortSearch(result, array[i]), array[i]); 
            }                    
            return result;

            static int BinarySortSearch<T>(List<T> list, T element) where T : unmanaged, IComparable<T>, IEquatable<T>
            {
                int start = 0, end = list.Count - 1, mid;
                while (start <= end)
                {
                    mid = start + (end - start) / 2;

                    if (list[mid].CompareTo(element) > 0)
                    {
                        end = mid - 1;
                    }
                    else if (list[mid].CompareTo(element) < 0)
                    {
                        start = mid + 1;
                    }
                    else if (list[mid].Equals(element))
                    {
                        return mid + 1;
                    }
                }
                return start;
            }
        }

        private static void SwapList<T>(List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }

        private static void Swap<T>(ref T item1, ref T item2)
        {
            (item1, item2) = (item2, item1);
        }
    }
}