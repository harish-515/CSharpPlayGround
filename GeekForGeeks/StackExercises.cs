using System;
using System.Collections.Generic;

namespace CSharpPlayGrond.GeekForGeeks
{
    internal static class StackExercises
    {
        public static void PrintStack(Stack<int> stk)
        {
            Console.WriteLine($"Top to bottom : {string.Join(",", stk)}");
        }

        private static void insertAtBottom(Stack<int> stk, int value)
        {
            if (stk.Count == 0)
            {
                stk.Push(value);
            }
            else
            {
                int top = stk.Peek();
                stk.Pop();
                insertAtBottom(stk, value);

                stk.Push(top);
            }
        }

        public static void ReverseStack(Stack<int> stk)
        {
            if (!(stk.Count == 0))
            {
                int top = stk.Peek();
                stk.Pop();
                ReverseStack(stk);

                insertAtBottom(stk, top);
            }
        }

        private static void sortStackHelper(Stack<int> stk, int value)
        {
            if (stk.Count == 0)
            {
                stk.Push(value);
            }
            else
            {
                int top = stk.Peek();
                if (top > value)
                {
                    stk.Pop();
                    sortStackHelper(stk, value);

                    stk.Push(top);
                }
            }
        }

        public static void SortStack(Stack<int> stk)
        {
            if (!(stk.Count == 0))
            {
                int top = stk.Peek();
                stk.Pop();
                ReverseStack(stk);

                insertAtBottom(stk, top);
            }
        }
    }
}