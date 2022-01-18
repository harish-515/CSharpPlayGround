using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPlayGrond.GeekForGeeks
{
    internal class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val)
        {
            this.val = val;
            this.next = null;
        }
    }

    internal class LinkedList
    {
        public LinkedList()
        {
            _head = null;
        }

        private ListNode _head;

        public ListNode Head
        {
            get
            {
                return _head;
            }
            set
            {
                _head = value;
            }
        }

        public void AddBegin(int val)
        {
            if (_head == null)
            {
                _head = new ListNode(val);
            }
            else
            {
                ListNode temp = new ListNode(val);
                temp.next = _head;
                _head = temp;
            }
        }

        public void print()
        {
            StringBuilder sb = new StringBuilder();
            ListNode ptr = _head;

            while (ptr != null)
            {
                sb.Append(ptr.val);
                sb.Append("->");
                ptr = ptr.next;
            }

            string final = sb.ToString();
            Console.WriteLine(string.IsNullOrEmpty(final) ? "Linked list is empty" : final);
        }
    }

    internal static class LinkedListExercises
    {
        public static ListNode DeleteDuplicates(ListNode head)
        {
            ListNode curr = head;
            ListNode next = head.next;

            if (head == null)
                return head;

            while (curr.next != null)
            {
                if (curr.val == curr.next.val)
                {
                    next = curr.next.next;
                    curr.next = null;
                    curr.next = next;
                }
                else
                {
                    curr = curr.next;
                }
            }

            return head;
        }

        public static ListNode EliminateAllDuplicates(ListNode head)
        {
            if (head == null)
                return head;

            ListNode dummy = new ListNode(0);
            dummy.next = head;

            ListNode prev = dummy;
            ListNode curr = dummy.next;

            while (curr.next != null)
            {
                if (prev.next.val == curr.next.val)
                {
                    prev.next = curr.next.next;
                    curr = curr.next;
                }
                else
                {
                    prev = prev.next;
                }
            }

            return dummy.next;
        }

        public static int GetDecimalValue(ListNode head)
        {
            int result = 0;
            if (head != null)
            {
                Stack<int> stk = new Stack<int>();

                while (head != null)
                {
                    stk.Push(head.val);
                    head = head.next;
                }

                int temp;
                int factor = 1;

                if (stk.Count > 0)
                {
                    temp = stk.Pop();
                    result += (temp * factor);
                    factor = factor << 1;
                }
            }

            return result;
        }

        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode head = null;
            ListNode curr = null;

            while (l1 != null || l2 != null)
            {
                if (l1.val > l2.val)
                {
                    if (head == null)
                    {
                        head = l2;
                        curr = l2;
                        l2 = l2.next;
                        curr = curr.next;
                    }
                    else
                    {
                        curr.next = l2;
                        l2 = l2.next;
                        curr = curr.next;
                    }
                }
                else
                {
                    if (head == null)
                    {
                        head = l1;
                        curr = l1;
                        l1 = l1.next;
                        curr = curr.next;
                    }
                    else
                    {
                        curr.next = l1;
                        l1 = l1.next;
                        curr = curr.next;
                    }
                }
            }

            while (l1 != null)
            {
                if (head == null)
                {
                    head = l1;
                    curr = l1;
                    l1 = l1.next;
                    curr = curr.next;
                }
                else
                {
                    curr.next = l1;
                    l1 = l1.next;
                    curr = curr.next;
                }
            }

            while (l2 != null)
            {
                if (head == null)
                {
                    head = l2;
                    curr = l2;
                    l2 = l2.next;
                    curr = curr.next;
                }
                else
                {
                    curr.next = l2;
                    l2 = l2.next;
                    curr = curr.next;
                }
            }

            return head;
        }

        public static void demo()
        {
            LinkedList s1 = new LinkedList();
            s1.AddBegin(1);
            s1.AddBegin(1);
            s1.AddBegin(1);

            //Console.Write(GetDecimalValue(s1.Head));

            s1.Head = EliminateAllDuplicates(s1.Head);
            s1.print();

            //    LinkedList s1 = new LinkedList();
            //    s1.AddBegin(8);
            //    s1.AddBegin(3);
            //    s1.AddBegin(1);
            //    s1.AddBegin(0);
            //    LinkedList s2 = new LinkedList();
            //    s2.AddBegin(9);
            //    s2.AddBegin(5);
            //    s2.AddBegin(4);
            //    s2.AddBegin(2);

            //    s1.print();
            //    s2.print();

            //    LinkedList res = new LinkedList();
            //    res.Head = MergeTwoLists(s1.Head, s2.Head);
            //}
        }
    }
}