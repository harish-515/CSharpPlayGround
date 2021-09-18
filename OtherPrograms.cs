using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond
{
    static class OtherPrograms
    {

        public static bool ValidParantesis(string input)
        {
            Dictionary<char, char> parnths = new Dictionary<char, char>();
            parnths.Add(')', '(');
            parnths.Add(']', '[');
            parnths.Add('}', '{');

            Stack<char> stk = new Stack<char>();

            foreach(char ele in input)
            {
                if (parnths.Values.Contains(ele))
                {
                    stk.Push(ele);
                }
                if (parnths.Keys.Contains(ele))
                {
                    if(stk.Peek() == parnths[ele])
                    {
                        stk.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return stk.Count == 0;
        }

        public static string RearrangeGetSmallest(string input)
        {
            char[] digits = input.ToArray();
            Array.Sort(digits);
            int i = 0;
            while (digits[i] == '0')
            {
                i++;
            }

            char temp = digits[0];
            digits[0] = digits[i];
            digits[i] = temp;

            return string.Join("",digits);
        }

        public static int BinarySearch(List<int> nums, int k)
        {

            int l = 0;
            int r = nums.Count - 1;

            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if (nums[mid] == k)
                    return mid;
                else if (nums[mid] > k)
                    r = mid - 1;
                else
                    l = mid + 1;
            }

            return -1;

        }

        public static int MaxSumTriplet(List<int> nums)
        {

            int[] max = new int[nums.Count];
            for (int i = nums.Count - 2; i >= 0; i--)
            {
                int m = Math.Max(max[i + 1], nums[i + 1]);
                if (m > nums[i]) max[i] = m;
            }

            SortedList<int, bool> sortednums = new SortedList<int,bool>();
            sortednums.Add(nums[0], true);
            int result = 0;

            for (int i = 1; i < nums.Count; i++)
            {
                if (!sortednums.Keys.Contains(nums[i]))
                    sortednums.Add(nums[i], true);

                //sortednums.Sort();    
                if (max[i] != 0)
                {
                    int idx = sortednums.IndexOfKey(nums[i]);
                    if (idx != 0)
                    {
                        result = Math.Max(result, (nums[i] + max[i] + sortednums.Keys[idx - 1] ));
                    }
                }
            }

            return result;
        }



    }
}
