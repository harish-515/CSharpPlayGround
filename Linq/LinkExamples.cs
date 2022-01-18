using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.Linq
{
    internal class Person1
    {
        public string Name;
        public int Age;
    }

    internal class LinkExamples
    {
        public static void LinkExampleDemo()
        {
            Console.WriteLine(Enumerable.Empty<int>());

            Console.WriteLine(Enumerable.Repeat("Hello", 3));

            Console.WriteLine(Enumerable.Range(1, 10));

            Console.WriteLine(Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c));

            Console.WriteLine(Enumerable.Range(1, 10).Select(i => new string('x', i)));

            // Mode of Operations
            // Immediate
            // Deferred

            // Projection Operator
            // Calculations will only happens when you iterate the collection
            //Ex Enumerable.Range(1,10).Select(rand.Next(10));

            // Deferred operations
            // streaming -- do not have to read all data befre elemets are yielded
            // non streaming -- must read all source data before they yeild an element

            var list = new System.Collections.ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Console.WriteLine(list.Select(i => (int)i).Sum());

            // Cast Linq Operators
            Console.WriteLine(list.Cast<int>().Average());

            //Materializing an array
            var numbers = Enumerable.Range(1, 10);
            var arr = numbers.ToArray(); //ToList()

            var dict = numbers.ToDictionary(i => (double)i / 10, i => i % 2 == 0);
            Console.WriteLine(dict);

            var arr2 = new[] { 1, 2, 3 };
            IEnumerable<int> arre = arr2.AsEnumerable();

            // AsParallel -- for parallel computation
            // AsQueryable --

            // Projector Operations
            //Select
            //SelectMany

            var nums = Enumerable.Range(1, 4);
            var squares = nums.Select(i => i * i); //map
            Console.WriteLine(squares);

            string sentance = "This is a nice sentance";
            var wordlens = sentance.Split().Select(word => word.Length);
            Console.WriteLine(wordlens);

            // anonymous elements
            var wordswithlength = sentance.Split().Select(w => new { Word = w, Size = w.Length });
            Console.WriteLine(wordswithlength);

            Random rand = new Random();
            var randnums = Enumerable.Range(1, 10).Select(_ => rand.Next(10));
            Console.WriteLine(randnums);

            var sequences = new[] { "red,green,blue", "oranage", "white,pink" };
            //var allwords = sequences.Select(s=>s.Split(','));
            var allwords = sequences.SelectMany(s1 => s1.Split(','));

            Console.WriteLine(allwords);
            // flatten the collection of collections (spread operator in javascript)

            string[] objs = { "house", "car", "dog" };
            string[] colors = { "red", "green", "black" };

            var pairs = colors.SelectMany(_ => objs, (c, o) => new { Color = c, Obj = o });
            Console.WriteLine(pairs);

            //Filtering Operations

            var ns = Enumerable.Range(1, 10);
            var evenNums = ns.Where(n => n % 2 == 0);
            Console.WriteLine(evenNums);

            // COmbination of Projection & Filtering

            var oddSquares = ns.Select(x => x * x).Where(y => y % 2 == 1);
            Console.WriteLine(oddSquares);

            object[] values = { 1, 2.5, 3, 4.56f };
            Console.WriteLine(values.OfType<int>());
            Console.WriteLine(values.OfType<double>());
            Console.WriteLine(values.OfType<float>());

            var randomValues = Enumerable.Range(1, 10).Select(_ => rand.Next(10) - 5).ToArray();

            var csvString = new Func<IEnumerable<int>, string>(vs =>
            {
                return string.Join(",", vs.ToArray());
            });

            /*
            Console.WriteLine(csvString(randomValues));
            Console.WriteLine(csvString(randomValues.OrderBy(x => x)));
            Console.WriteLine(csvString(randomValues.OrderByDescending(x => x)));

            Console.WriteLine(randomValues);
            */
            var people = new List<Person1>{
                new Person1 {Name ="Adam",Age=20},
                new Person1 {Name ="Adam",Age=36},
                new Person1 {Name ="Boris",Age=18},
                new Person1 {Name ="Claire",Age=36},
                new Person1 {Name ="Adam",Age=20},
                new Person1 {Name ="Jack",Age=20}
            };
            /*
            Console.WriteLine(people);

            */

            // the results of orederby is not iEnumerable by it is IOrderedEnumerable
            Console.WriteLine(people.OrderBy(p => p.Name));
            IOrderedEnumerable<Person1> sortedPeople = people.OrderBy(p => p.Name);

            Console.WriteLine(people.OrderBy(p => p.Age).ThenByDescending(p => p.Name));

            //Reversing a Collection
            string s = "this is a test";
            Console.WriteLine(new string(s.Reverse().ToArray()));

            var byName = people.GroupBy(p => p.Name);
            Console.WriteLine(byName);
            // GroupBy give IEnumerable<IGrouping<T,U>>

            Console.WriteLine(people.GroupBy(p => p.Age < 30));

            // adding a select criteria in groupby
            var byAgeNames = people.GroupBy(p => p.Age < 30, p => p.Name);
            Console.WriteLine(byAgeNames);

            foreach (var item in byAgeNames)
            {
                Console.WriteLine($"These people are {item.Key} years old");
                foreach (var name in item)
                    Console.WriteLine($" -{name}");
            }

            //Quantifiers & Partitioning

            string word1 = "helloo";
            string word2 = "help";

            Console.WriteLine(word1.Distinct());

            Console.WriteLine(word1.Intersect(word2));

            Console.WriteLine(word1.Union(word2));

            Console.WriteLine(word1.Except(word2));

            Console.WriteLine(word2.Except(word1));

            var a = new[] { 1, 2, 3, 4, 5 };
            var b = new[] { 1, 3, 5, 7, 9 };

            Console.WriteLine(a.Union(b).Where(x => !a.Intersect(b).Contains(x)));

            int[] numbers1 = { 1, 2, 3, 4, 5 };
            Console.WriteLine("Are all numbers greater than 0 " + numbers1.All(x => x > 0));
            Console.WriteLine("Are all numbers odd numbers " + numbers1.All(x => x % 2 == 1));
            Console.WriteLine("Any number less than 2 " + numbers1.Any(x => x < 2));

            Console.WriteLine(new int[] { }.Any());
            Console.WriteLine(new int[] { 1 }.Any());

            //skip & take

            var inputs = new[] { 3, 3, 2, 2, 1, 1, 2, 2, 3, 3 };
            Console.WriteLine(inputs.Skip(2));
            Console.WriteLine(inputs.Skip(2).Take(4));

            // skips initial occurances of 3's
            Console.WriteLine(nums.SkipWhile(i => i == 3));

            // take while takes all occurances from start until the condition fails
            Console.WriteLine(nums.TakeWhile(i => i > 1));

            var input = new[] { -3, -1, 3, 7, 1, -3, 7 };
            Console.WriteLine(input.Where(i => i > 0).Distinct().Count());
        }
    }
}