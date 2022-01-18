using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.GeekForGeeks
{
    internal static class ArraysExercises
    {
        //public static int[] Rearrange(int[] arr)
        //{
        //    if (arr.Length <= 1)
        //        return arr;
        //    else
        //    {
        //        for(int i = 0; i < arr.Length; i++)
        //        {
        //        }
        //    }
        //}

        public static int maxOccured(int[] L, int[] R, int n)
        {
            int maxx = R.Max() + 1;
            int[] arr = Enumerable.Repeat(0, maxx + 1).ToArray();

            for (int i = 0; i < n; i++)
            {
                for (int j = L[i]; j <= R[i]; j++)
                {
                    arr[j]++;
                }
            }

            int maxValue = arr.Max();
            int maxIndex = arr.ToList().IndexOf(maxValue);

            return maxIndex;
        }

        public static int[] GetMaxAndSecondMax(int[] nums)
        {
            int max1, max2;
            max1 = max2 = -1;
            foreach (int ele in nums)
            {
                if (max1 < ele)
                {
                    max2 = max1;
                    max1 = ele;
                }
                else if (max1 != ele && max2 < ele)
                {
                    max2 = ele;
                }
            }

            return new int[] { max1, max2 };
        }

        public static int[] StrongestNeighbour(int[] nums)
        {
            int l, r;
            List<int> result = new List<int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                l = i - 1 >= 0 ? nums[i - 1] : -1;
                r = i + 1 < nums.Length ? nums[i + 1] : -1;
                result.Add(Math.Max(l, r));
            }

            return result.ToArray();
        }

        public static int MinAdjDiffCircularArray(List<int> nums)
        {
            int i = 0;
            int mindiff = int.MaxValue;

            while (i + 1 < nums.Count())
            {
                mindiff = Math.Min(mindiff, Math.Abs(nums[i] - nums[i + 1]));
                i++;
            }

            mindiff = Math.Min(mindiff, Math.Abs(nums[i] - nums[0]));

            return mindiff;
        }

        public static void rotateArray(List<int> nums, int k)
        {
            reverseArray(nums, 0, k - 1);
            reverseArray(nums, k, nums.Count() - 1);
            reverseArray(nums, 0, nums.Count() - 1);
        }

        public static void reverseInGroups(List<int> nums, int k)
        {
            for (int i = 0; i < nums.Count; i = i + k)
            {
                reverseArray(nums, i, (i + k - 1) > nums.Count - 1 ? nums.Count - 1 : (i + k - 1));
            }
        }

        private static void reverseArray(List<int> nums, int start, int end)
        {
            while (start < end)
            {
                int temp = nums[end];
                nums[end] = nums[start];
                nums[start] = temp;
                start++;
                end--;
            }
        }

        public static int FindRepeat(int[] numbers)
        {
            int floor = 1;
            int ceiling = numbers.Length - 1;

            while (floor < ceiling)
            {
                // Divide our range 1..n into an upper range and lower range
                // (such that they don't overlap)
                // Lower range is floor..midpoint
                // Upper range is midpoint+1..ceiling
                int midpoint = floor + (ceiling - floor) / 2;
                int lowerRangeFloor = floor;
                int lowerRangeCeiling = midpoint;
                int upperRangeFloor = midpoint + 1;
                int upperRangeCeiling = ceiling;

                // Count number of items in lower range
                int itemsInLowerRange = numbers.Count(item => item >= lowerRangeFloor && item <= lowerRangeCeiling);

                int distinctPossibleIntegersInLowerRange = lowerRangeCeiling - lowerRangeFloor + 1;

                if (itemsInLowerRange > distinctPossibleIntegersInLowerRange)
                {
                    // There must be a duplicate in the lower range
                    // so use the same approach iteratively on that range
                    floor = lowerRangeFloor;
                    ceiling = lowerRangeCeiling;
                }
                else
                {
                    // There must be a duplicate in the upper range
                    // so use the same approach iteratively on that range
                    floor = upperRangeFloor;
                    ceiling = upperRangeCeiling;
                }
            }

            // Floor and ceiling have converged
            // We found a number that repeats!
            return floor;
        }

        public static int FindMissingInArray(int[] arr)
        {
            int len = arr.Length;

            int s = 0;
            int e = len - 1;
            int m = 0;

            while ((e - s) > 1)
            {
                m = s + (e - s) / 2;

                if ((arr[s] - s) != (arr[m] - m))
                {
                    e = m;
                }
                if ((arr[e] - e) != (arr[m] - m))
                {
                    s = m;
                }
            }

            return arr[s] + 1;
        }

        public static int FindMissing(int[] numbers, int len)
        {
            int floor = 1;
            int ceiling = len;

            while (floor < ceiling)
            {
                // Divide our range 1..n into an upper range and lower range
                // (such that they don't overlap)
                // Lower range is floor..midpoint
                // Upper range is midpoint+1..ceiling
                int midpoint = floor + (ceiling - floor) / 2;
                int lowerRangeFloor = floor;
                int lowerRangeCeiling = midpoint;
                int upperRangeFloor = midpoint + 1;
                int upperRangeCeiling = ceiling;

                // Count number of items in lower range
                int itemsInLowerRange = numbers.Count(item => item >= lowerRangeFloor && item <= lowerRangeCeiling);

                int distinctPossibleIntegersInLowerRange = lowerRangeCeiling - lowerRangeFloor + 1;

                if (itemsInLowerRange < distinctPossibleIntegersInLowerRange)
                {
                    // There must be a duplicate in the lower range
                    // so use the same approach iteratively on that range
                    floor = lowerRangeFloor;
                    ceiling = lowerRangeCeiling;
                }
                else
                {
                    // There must be a duplicate in the upper range
                    // so use the same approach iteratively on that range
                    floor = upperRangeFloor;
                    ceiling = upperRangeCeiling;
                }
            }

            // Floor and ceiling have converged
            // We found a number that repeats!
            return floor;
        }

        public static int MajorityElement(int[] arr)
        {
            int maj, count;
            maj = 0;
            count = 1;

            for (int i = 1; i < arr.Length; i++)
            {
                count += (arr[maj] == arr[i]) ? 1 : -1;

                if (count == 0)
                {
                    maj = ++i;
                    count = 1;
                }
            }

            return arr[maj];
        }

        public static bool CanReach(int[] jumps)
        {
            int i = 0;
            for (; i < jumps.Length;)
            {
                if (jumps[i] != 0)
                {
                    i += jumps[i];
                }
                else
                    return false;
            }

            return i == jumps.Length;
        }

        public static int MaxLengthAfterFlip(int[] arr, int m)
        {
            int l, r, max_len, zeros;
            l = 0; r = 0; max_len = 0; zeros = 0;

            while (r < arr.Length)
            {
                if (zeros <= m)
                {
                    if (arr[r] == 0)
                    {
                        zeros++;
                    }
                    r++;
                }

                if (zeros > m)
                {
                    if (arr[l] == 0)
                    {
                        zeros--;
                    }
                    l++;
                }

                if ((r - l) > max_len && (zeros <= m))
                {
                    max_len = r - l;
                }
            }

            return max_len;
        }

        public static List<(int, int)> GetProfitableStockDays(int[] s)
        {
            int curr = 0;
            List<(int, int)> res = new List<(int, int)>();
            int i;
            for (i = 1; i < s.Length; i++)
            {
                if (s[i] <= s[curr] && i - 1 != curr)
                {
                    res.Add((curr, i - 1));
                    curr = i;
                }
            }
            if (s[s.Length - 1] > s[curr])
            {
                res.Add((curr, s.Length - 1));
            }

            return res;
        }

        public static void ConvertToWave(int[] arr)
        {
            Console.WriteLine($"Original {string.Join(",", arr)}");

            Array.Sort(arr);

            for (int i = 1; i < arr.Length; i += 2)
            {
                int temp = arr[i - 1];
                arr[i - 1] = arr[i];
                arr[i] = temp;
            }

            Console.WriteLine($"Result {string.Join(",", arr)}");
        }

        public static int LongestConsecutiveSequence(int[] s)
        {
            Array.Sort(s);
            int curr = 0;
            int res = 0;
            int count = 1;
            int i;
            for (i = 1; i < s.Length; i++)
            {
                if (s[i] - s[curr] != count)
                {
                    res = Math.Max(i - curr, res);
                    count = 1;
                    curr = i;
                }
                else
                    count++;
            }
            if (s[s.Length - 1] - s[curr] == count - 1)
            {
                res = Math.Max(s.Length - curr, res);
            }

            return res;
        }

        public static void MaxOccuredInteger(int[] l, int[] r)
        {
            int max = r.Max();
            var result = Enumerable.Repeat(0, max + 1).ToList();

            for (int i = 0; i < l.Length; i++)
            {
                for (int j = l[i]; j <= r[i]; j++)
                {
                    result[j]++;
                }
            }

            Console.WriteLine(result.IndexOf(result.Max()));
        }

        public static void TrappedRainWater(int[] walls)
        {
            int[] left = Enumerable.Repeat(0, walls.Length).ToArray();
            int[] right = Enumerable.Repeat(0, walls.Length).ToArray();

            left[0] = walls[0];
            for (int i = 1; i < walls.Length - 1; i++)
            {
                left[i] = Math.Max(left[i - 1], walls[i]);
            }

            right[walls.Length - 1] = walls[walls.Length - 1];
            for (int i = walls.Length - 2; i >= 0; i--)
            {
                right[i] = Math.Max(right[i + 1], walls[i]);
            }

            int water = 0;

            for (int i = 0; i < walls.Length; i++)
            {
                water += Math.Max(0, Math.Min(left[i], right[i]) - walls[i]);
            }

            Console.WriteLine($"Trapped Water : {water}");
        }

        public static int[] GetLeaders(int[] arr)
        {
            List<int> result = new List<int>();
            int max = arr[arr.Length - 1];
            result.Add(max);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (max < arr[i])
                {
                    result.Add(arr[i]);
                    max = arr[i];
                }
            }
            result.Reverse();
            return result.ToArray();
        }

        //Function that puts all non-positive (0 and negative) numbers on left
        //side of arr[] and return count of such numbers.
        private static int segregateArr(int[] arr, int n)
        {
            int j = 0, i;
            for (i = 0; i < n; i++)
            {
                if (arr[i] <= 0)
                {
                    //Changing the position of negative numbers and 0.
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;

                    //Incrementing count of non-positive integers.
                    j++;
                }
            }
            return j;
        }

        //Finding the smallest positive missing number in an array that contains
        //all positive integers.
        private static int findMissingPositive(int[] arr, int n)
        {
            //Marking arr[i] as visited by making arr[arr[i] - 1] negative.
            //Note that 1 is subtracted because index starts from 0 and
            //positive numbers start from 1.
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(arr[i]) - 1 < n && arr[Math.Abs(arr[i]) - 1] > 0)
                    arr[Math.Abs(arr[i]) - 1] = -arr[Math.Abs(arr[i]) - 1];
            }

            for (int i = 0; i < n; i++)
                if (arr[i] > 0)
                    //Returning the first index where value is positive.
                    // 1 is added because index starts from 0.
                    return i + 1;

            return n + 1;
        }

        //Function to find the smallest positive number missing from the array.
        public static int SmallestPositiveMissingNumber(int[] arr, int n)
        {
            // First separating positive and negative numbers.
            int shift = segregateArr(arr, n);
            //Shifting the array and calling function to find result in the positive part.
            //returning the result.
            return findMissingPositive(arr.Skip(shift).ToArray(), n - shift);
        }

        public class Interval
        {
            public int buy;
            public int sell;
        }

        public static List<Interval> StockBuyAndSell(int[] prices)
        {
            int i = 0;
            int n = prices.Length;
            var list = new List<Interval>();
            while (i < n - 1)
            {
                var pair = new Interval();
                //find local minima
                while (i < n - 1 && prices[i + 1] <= prices[i])
                {
                    i++;
                }

                if (i == n - 1)
                    return list;

                pair.buy = i;

                //find local maxima
                while ((i < n) && (prices[i] >= prices[i - 1]))
                {
                    i++;
                }

                pair.sell = i - 1;

                list.Add(pair);
            }

            return list;
        }

        public static int BinarySearch(int[] arr, int q)
        {
            int s = 0;
            int e = arr.Length - 1;
            int mid = -1;
            while (s <= e)
            {
                mid = s + (e - s) / 2;

                if (arr[mid] > q)
                {
                    e = mid - 1;
                }
                else if (arr[mid] < q)
                {
                    s = mid + 1;
                }
                else
                {
                    break;
                }
            }

            return mid;
        }

        public static void FirstOccurance(int[] arr, int q)
        {
            Console.WriteLine(BinarySearch(arr, q));
        }

        public static void LastOccurance(int[] arr, int q)
        {
            int x = BinarySearch(arr, q);
        }
    }
}