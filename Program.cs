using CSharpPlayGrond.DesignPatterns.Behavioral;
using CSharpPlayGrond.DesignPatterns.Creational;
using CSharpPlayGrond.DesignPatterns.Structural;
using CSharpPlayGrond.DesignPatterns.Structural.Decorator;
using CSharpPlayGrond.GeekForGeeks;
using CSharpPlayGrond.Multithreading;
using CSharpPlayGrond.Multithreading.Tasks;
using CSharpPlayGrond.Others;
using System;
using System.Collections.Generic;
using System.Threading;
using static Coding.Exercise.FieldElement;
using static CSharpPlayGrond.DesignPatterns.Behavioral.MoneyTransferCommand;

namespace CSharpPlayGrond
{
    public class Person
    {
        public string Name  { get; set; }
        public static implicit operator int(Person p)
        {
            return p.Name.Length; 
        }
    }


    public static class StringExtensions
    {
        public static int StringLength(this string str)
        {
            return str.Length; 
        }

    }


    struct MyStruct
    {
        static int x = 25;
        static int y = 50;
        public void SetXY(int i, int j)
        {
            x = i;
            y = j;
        }
        public static void ShowSum()
        {
            int sum = x + y;
            Console.WriteLine("The sum is {0}", sum);
        }
    }

    static class Program
    {

        public static int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
        {
            int initialColor = image[sr][sc];
            int l = image.Length;
            int b = image[0].Length;

            Queue<int[]> qu = new Queue<int[]>();
            qu.Enqueue(new int[] { sr, sc });

            while (qu.Count > 0)
            {
                Console.WriteLine("-----------------------------------------------");

                //check the exisitng color, if equal replace the color
                int[] coods = qu.Dequeue();
                Console.WriteLine("x : {0} & y : {1}", coods[0], coods[1]);

                if (0 <= coods[0] && coods[0] < l && 0 <= coods[1] && coods[1] < b)
                {
                    int color = image[coods[0]][coods[1]];
                    if (color == initialColor)
                    {
                        image[coods[0]][coods[1]] = newColor;
                        // add the pixels in other direction to the queue.
                        qu.Enqueue(new int[] { coods[0], coods[1] + 1 });
                        qu.Enqueue(new int[] { coods[0], coods[1] - 1 });
                        qu.Enqueue(new int[] { coods[0] + 1, coods[1] });
                        qu.Enqueue(new int[] { coods[0] - 1, coods[1] });
                    }
                }

                PrintImage(image);

            }
            return image;
        }

        public static void PrintImage(int[][] image)
        {
            for(int i = 0; i < image.Length; i++)
            {
                Console.WriteLine(String.Join(", ", image[i]));
            }
        }
        public delegate void Del();

        public static void del1()
        {
            Console.WriteLine($"{DateTime.Now.Ticks}  Del1 called !!");
            Thread.Sleep(1000);
            Console.WriteLine("del1 done");
        }

        public static void del2()
        {
            Console.WriteLine($"{DateTime.Now.Ticks}  Del2 called !!");
            Thread.Sleep(1000);
            Console.WriteLine("del2 done");
        }

        public static void del3()
        {
            Console.WriteLine($"{DateTime.Now.Ticks}  Del3 called !!");
            Thread.Sleep(1000);
            Console.WriteLine("del3 done");
        }

        public static bool AreEqual(int[] arr1, int[] arr2)
        {
            bool res = true;

            for (int i = 0; i < 26; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    res = false;
                    break;
                }

            }

            return res;
        }

        public static IList<int> FindAnagrams(string s, string p)
        {
            int[] parr = new int[26];
            int[] sarr = new int[26];


            foreach (char ch in p)
            {
                parr[ch - 'a']++;
            }

            for (int i = 0; i < p.Length; i++)
            {
                sarr[s[i] - 'a']++;
            }

            List<int> result = new List<int>();

            for (int i = 0; i < s.Length - p.Length+1; i++)
            {
                if (AreEqual(parr, sarr))
                {
                    result.Add(i);
                }

                sarr[s[i] - 'a']--;
                sarr[s[i + p.Length] - 'a']++;
            }

            return result;
        }

        class A
        {
            public int MyProperty { get; set; }

            public int MyProperty1 { get; set; }
            public void call()
            {
                Console.WriteLine("a");
            }
        }

        class Coupa
        {
            public static int GetPossibleIndex(int[] A,int k)
            {
                int lo, hi, mid;
                lo = 0;
                hi = A.Length-1;
                mid = 0;

                while (lo <= hi)
                {
                    mid = lo + (hi - lo) / 2;

                    if(A[mid] < k)
                    {
                        lo = mid + 1; 
                    }
                    else if(A[mid] > k)
                    {
                        hi = mid - 1;
                    }
                    else
                    {
                        return mid;
                    }
                }

                return lo;
            }

            public static int NextGreatestElement(int[]A,int k)
            {
                Array.Sort(A);
                int i = GetPossibleIndex(A, k);

                if (i + 1 >= A.Length)
                    return k + 1;

                int j = i;
                while(j+1 < A.Length && (A[j+1] - A[j]) == 1)
                {
                    j++;
                }

                return A[j]+1;
            }


        }

        public static int GetRandom(Random rn)
        {
            Console.WriteLine("Called");
            return rn.Next() % 10;
        }
        static void Main(string[] args)
        {

            //int[] list1 = new int[3] { 0,0,0 };
            //int[] list2 = new int[3] { 0,1,1 };
            ////int[] list3 = new int[3] { 1, 0, 1 };

            //int[][] image = new int[][] { list1, list2 };

            ////PrintImage(image);

            //int[][] newimage = FloodFill(image, 1, 1, 2);


            //ThreadStart d = new ThreadStart(del1);
            //d += del2;
            //d += del3;

            //Thread t1 = new Thread(d);

            //t1.Start();
            //t1.Join();


            //Console.WriteLine(String.Join(",",FindAnagrams("abab", "ab")));

            //BuilderDemo.Demo();

            //PrintImage(newimage);

            //SingletonTester.TestSingleton();
            //SingletonTester.TestPerThreadSingleton();


            //TaskIntroductionDemo.Demo();
            //TaskCancellationDemo.Demo();
            //CompositeCancellationDemo.Demo();

            //WaitingInTasks.TaskWaitingDemo();

            //ExceptionHandlingTask.ExceptionDemo();

            //TaskDataSharing.ReadWriteLockDemo();


            //Console.WriteLine("result : {0}",Coupa.NextGreatestElement(new int[] { 1,  3, 2, 4, 5 },-2));


            //Console.WriteLine(ArraysExercises.MajorityElement(new int[] { 2, 2, 3,2, 5 }));
            //Console.WriteLine($"{ArraysExercises.MaxLengthAfterFlip(new int[] { 1, 0, 0, 1,0},2)}");

            //AmbientContextDemo.Demo();

            //DecoratorDemo.ShapeDecoratorDemo();

            //var sent = new Sentence("this is a new String");
            //sent[1].Capitalize = true;
            //sent[2].Capitalize = true;
            //sent[3].Capitalize = true;

            //Console.WriteLine(sent.ToString());


            //DesignPatterns.Behavioral.CQSDemo.Demo();

            //CommandDemo.CompositeCommandDemo();
            //CommandDemo.MoneyTransferCommand();


            //Console.WriteLine(
            //    String.Join(",",ArraysExercises.GetLeaders(
            //        new int[] { 16, 17, 4, 3, 5, 2 }
            //        )
            //    ));


            //Console.WriteLine(
            //   StringExercises.LeftMostRepeatingCharacter("geeksforgeeks")
            //    );


            //LinkedListExercises.demo();


            int[] arr = { 2, 3, 3, 5, 5, 5, 6, 6 };
            Console.WriteLine(ArraysExercises.BinarySearch(arr, 6));

            var a = System.Linq.Enumerable.Repeat(GetRandom(new Random(2)), 10);

            Console.WriteLine(string.Join(" ", a));

            //Console.WriteLine($"{ArraysExercises.LongestConsecutiveSequence(new int[] { 1,2,3,4,5,6,7 })}");
            Console.ReadKey();
        }

    }



    #region "------------"

  namespace Coding.Exercise
    {
        public interface IRenderer
        {
            string WhatToRenderAs { get; }
        }

        public abstract class Shape
        {
            public Shape(IRenderer rendrer)
            {
            }

            public string Name { get; set; }

            public override string ToString() => "Drawing {Name} as {rendrer.WhatToRenderAs}";
        }

        public class Triangle : Shape
        {
            public Triangle(IRenderer rendrer) : base(rendrer)
            {
                Name = "Triangle";
            }

        }

        public class Square : Shape
        {
            public Square(IRenderer rendrer) : base(rendrer)
            {
                Name = "Square";
            }
        }

        public class VectorRenderer : IRenderer
        {
            public string WhatToRenderAs => "lines";
        }

        public class RasterRenderer : IRenderer
        {
            public string WhatToRenderAs => "pixels";
        }

    }




    #endregion


}

