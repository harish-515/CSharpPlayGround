using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond
{
    public class Obj
    {
        public string str { get; set; }
    }

    // Extension Methods

    public static class ExtensionMethods
    {
  
        public static void changeObj(this Obj o, string someStr)
        {
            o.str = someStr;
        }


        public static void add1(ref this int num)
        {
            num++;
        }

        static void change(this string str)
        {
            str = "fseee";
        }

        public static void Print<T>(T value)
        {
            Console.WriteLine(value.ToString());
        }
    }
}
