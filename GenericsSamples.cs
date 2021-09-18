using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond
{

    public class GenericEx<T>
    {
        private T[] items;
        public int Count { get; private set; }

        public int Capacity { get; private set; }

        public T this[int x]
        {
            get
            {
                return this.items[x];
            }
        }

        public T[] this[int x, int y]
        {
            get
            {
                T[] result = new T[y - x];
                Array.Copy(this.items, x, result, 0, y - x);
                return result;
            }
        }

        public GenericEx()
        {
            this.items = new T[2];
            this.Capacity = 2;
            this.Count = 0;
        }

        public void Add(T item)
        {
            if (this.Capacity == this.Count)
            {
                this.Capacity *= 2;
                T[] clone = (T[])items.Clone();
                this.items = new T[this.Capacity];
                Array.Copy(clone, 0, this.items, 0, clone.Length);
            }
            this.items[this.Count] = item;
            this.Count++;
        }


        public static GenericEx<T> operator+ (GenericEx<T> Col1, GenericEx<T> Col2)
        {
            GenericEx<T> result = new GenericEx<T>();

            if(Col1.Count != Col2.Count)
            {
                throw new InvalidOperationException("Collection count not matching.");
            }
            else
            {
                for(int i = 0; i < Col1.Count; i++)
                {
                    result.Add((dynamic)Col1[i] + (dynamic)Col2[i]);
                }
            }
        
            return result;
        }

    }


    public class ArrayListExample 
    {
        public ArrayList ArrayListSample { get; set; }

        public ArrayListExample()
        {
            ArrayListSample = new ArrayList();            
        }

        private void PrintArrayList()
        {
            Console.WriteLine("Array List Items : ");
            foreach(object item in ArrayListSample)
            {
                Console.WriteLine($"{ArrayListSample.IndexOf(item)}. {item}");
            }
        }

        public void RunOnSample()
        {
            ArrayListSample.Add(0);
            ArrayListSample.Add("Zero");

            Console.WriteLine($"Present Items Count : {ArrayListSample.Count}");
            Console.WriteLine($"Present Capacity : {ArrayListSample.Capacity}");

            PrintArrayList();

            ArrayListSample.AddRange(new object[] { 1, '2', "3", true, false });

            Console.WriteLine($"Present Items Count : {ArrayListSample.Count}");
            Console.WriteLine($"Present Capacity : {ArrayListSample.Capacity}");

            PrintArrayList();

            ArrayListSample.Sort();

            PrintArrayList();
        }
    }



}
