using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpPlayGrond.Linq
{
    internal class Params : IEnumerable<int>
    {
        private readonly int a;
        private readonly int b;
        private readonly int c;

        public Params(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return a;
            yield return b;
            yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class Person
    {
        private string firstname, middlename, lastname;

        public Person(string firstname, string middlename, string lastname)
        {
        }

        public IEnumerable<String> Names
        {
            get
            {
                yield return firstname;
                yield return middlename;
                yield return lastname;
            }
        }
    }

    public static class IEnumeratorDemo
    {
        public static void Demo()
        {
            var p = new Params(1, 2, 3);
            foreach (var x in p)
            {
                Console.WriteLine(x);
            }

            var person = new Person("f1", "m1", "l1");
            foreach (var name in person.Names)
            {
                Console.WriteLine(name);
            }
        }
    }
}