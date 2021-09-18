using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond
{
    static class DelegateSamples
    {

        public delegate bool Filter(string name);

        public static List<string> NamesFilter(List<string> names, Filter filter)
        {
            List<string> result = new List<string>();
            foreach (var name in names)
            {
                if (filter(name))
                {
                    result.Add(name);
                }
            }

            return result;
        }

        public static void demo1()
        {
            List<string> names = new List<string> { "Abcd", "sdfksjglk", "weqw", "qqw", "efsaa", "fdsfwe", "awwa2", "2edqa" };

            List<string> LessThanFive = NamesFilter(names, (i) => i.Length < 5);
            List<string> GreaterThanFive = NamesFilter(names, (i) => i.Length > 5);
            List<string> EqualToFive = NamesFilter(names, (i) => i.Length == 5);

            Console.WriteLine($"LessthanFive : {string.Join(",", LessThanFive)}");
            Console.WriteLine($"GreaterThanFive : {string.Join(",", GreaterThanFive)}");
            Console.WriteLine($"EqualToFive : {string.Join(",", EqualToFive)}");
        }
    }
}

    
