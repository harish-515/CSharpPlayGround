using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond.Collections.Generic
{
    internal class DictionaryDemo
    {
        public DictionaryDemo()
        {
            Dictionary<int, int> demo = new Dictionary<int, int>();
            demo.Add(1, 1);
            demo.Add(3, 3);
            demo.Add(2, 2);

            printDictionary(demo);

            try
            {
                demo.Add(3, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" adding duplicate key : {ex.Message}");
            }
        }

        private void printDictionary(Dictionary<int, int> dict)
        {
            Console.WriteLine(" ******************** ");
            foreach (KeyValuePair<int, int> pair in dict)
            {
                Console.WriteLine($" Keys : {pair.Key} -- Value : {pair.Value}");
            }
        }
    }

    internal class SortedSetDemo
    {
        public SortedSetDemo()
        {
            SortedSet<int> s = new SortedSet<int>();
            for (int i = 0; i < 10; i++)
            {
                s.Add(i);
            }
        }
    }

    internal class ListDemo
    {
        public ListDemo()
        {
            List<int> lt = new List<int>();

            lt = Enumerable.Range(0, 10).ToList();

            Console.WriteLine(string.Join(",", lt));

            List<string> digits = lt.ConvertAll(item => $"item : {item}");

            Console.WriteLine(string.Join(",", digits));

            Console.WriteLine($"Are there elements > 10 {lt.Exists(i => i > 10)}");
            Console.WriteLine($"Are there elements < 10 {lt.Exists(i => i < 10)}");
            Console.WriteLine($"Are there elements = 10 {lt.Exists(i => i == 10)}");
        }
    }

    internal class StackDemo
    {
        public StackDemo()
        {
            Stack<int> st = new Stack<int>();

            st.Push(1);
            st.Peek();
            st.Pop();
        }
    }

    internal class QueueDemo
    {
        public QueueDemo()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Peek();
            q.Dequeue();
        }
    }

    internal class HashSetDemo
    {
        public HashSetDemo()
        {
            HashSet<int> hs = new HashSet<int>();

            hs.Add(1);
            hs.Add(1);
            hs.Add(1);

            Console.WriteLine(string.Join(",", hs));
        }
    }

    internal class LinkedListDemo
    {
        public LinkedListDemo()
        {
            LinkedList<int> lk = new LinkedList<int>();
            lk.AddLast(1);
            LinkedListNode<int> Node1 = lk.First;
            lk.AddAfter(Node1, 2);
            lk.AddBefore(Node1, 0);

            lk.AddFirst(-1);
            lk.AddLast(5);

            Console.WriteLine(string.Join(",", lk));
        }
    }
}