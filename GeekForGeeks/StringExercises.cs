using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.GeekForGeeks
{
    internal class StringExercises
    {
        public static int NativeStringSearch(string input, string pattern)
        {
            Console.WriteLine("0123456789");
            Console.WriteLine(input);
            Console.WriteLine(pattern);

            if (pattern.Length > input.Length)
                return -1;

            for (int i = 0; i <= input.Length - pattern.Length; i++)
            {
                int j;
                for (j = 0; j < pattern.Length; j++)
                {
                    if (input[i + j] != pattern[j])
                        break;
                }

                if (j == pattern.Length)
                    return i;
            }

            return -1;
        }

        public static int GetSmaller(string s, int start, bool ignoresame = true)
        {
            int count = 0;
            for (int i = start + 1; i < s.Length; i++)
            {
                if (ignoresame)
                {
                    if ((s[start] - s[i]) >= 0)
                        count++;
                }
                else
                {
                    if ((s[start] - s[i]) > 0)
                        count++;
                }
            }
            return count;
        }

        public static int Fact(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            else
                return n * Fact(n - 1);
        }

        public static int LexiographicRank(string s)
        {
            int mul = Fact(s.Length);
            int rank = 1;

            for (int i = 0; i < s.Length; i++)
            {
                mul /= (s.Length - i);
                int small = GetSmaller(s, i);

                rank += (mul * small);
            }

            return rank;
        }

        public static int LongestSubStringDistinctCharacters(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            foreach (char ch in s)
            {
                if (!map.Keys.Contains(ch))
                {
                    map.Add(ch, -1);
                }
            }

            int start = 0;
            int maxlen, result;
            result = 0;
            for (int j = 0; j < s.Length; j++)
            {
                start = Math.Max(start, map[s[j]] + 1);
                maxlen = j - start + 1;
                result = Math.Max(maxlen, result);
                map[s[j]] = j;
            }

            return result;
        }

        public static bool AreRotations(string s1, string s2)
        {
            //lengths must be equal if they are just rotations
            if (s1.Length != s2.Length)
                return false;

            return (s1 + s1).IndexOf(s2) >= 0;
        }

        public static bool AreAnagarm(string s, string p, int start)
        {
            int[] parr = Enumerable.Repeat(0, 26).ToArray();

            for (int i = 0; i < p.Length; i++)
            {
                parr[p[i] - 'a']++;
                parr[s[start + i] - 'a']--;
            }

            return parr.All((ele) => ele == 0);
        }

        public static bool AnagarmSearch(string s, string p)
        {
            if (s.Length < p.Length)
                return false;

            for (int i = 0; i <= s.Length - p.Length; i++)
            {
                if (AreAnagarm(s, p, i))
                    return true;
            }

            return false;
        }

        public static string ReverseWordsInString(string s)
        {
            var words = s.Split();
            words = words.Reverse().ToArray();
            return string.Join(" ", words);
        }

        private static void Reverse(List<char> s, int start, int end)
        {
            while (start <= end)
            {
                char ch = s[start];
                s[start] = s[end];
                s[end] = ch;
                start++;
                end--;
            }
        }

        public static string ReverseWordsInStringInPlace(List<char> s)
        {
            int start = 0;
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == ' ')
                {
                    Reverse(s, start, i - 1);
                    start = i + 1;
                }
            }

            Reverse(s, start, s.Count - 1);
            Reverse(s, 0, s.Count - 1);

            return string.Join("", s);
        }

        public static int LeftMostNonRepeatingCharacter(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            foreach (char ch in s)
            {
                if (!map.Keys.Contains(ch))
                {
                    map.Add(ch, 1);
                }
                else
                {
                    map[ch]++;
                }
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (map[s[i]] == 1)
                    return i;
            }

            return -1;
        }

        public static int LeftMostRepeatingCharacter(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!map.Keys.Contains(s[i]))
                {
                    map.Add(s[i], i);
                }
                else
                {
                    map[s[i]] *= -1;
                }
            }

            var res = map.Values.Where((ele) => ele <= 0).ToList();

            if (res.Count > 0)
                return -1 * res.Max();

            return -1;
        }

        public static bool IsSubSequence(string s1, string s2)
        {
            int i = 0;
            int j = s2.Length;
            while (i < s1.Length && j < s2.Length)
            {
                if (s1[i] == s2[j])
                {
                    i++;
                    j++;
                }
                else
                    j++;
            }

            return i == s1.Length;
        }
    }
}