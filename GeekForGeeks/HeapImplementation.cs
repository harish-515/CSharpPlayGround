using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.GeekForGeeks
{
    class MinHeap
    {
        private int capacity = 10;
        private int size = 10;

        int[] items = new int[10];

        private int getLeftChildIndex(int parentIdx) { return (parentIdx * 2 ) + 1; }
        private int getRightChildIndex(int parentIdx) { return (parentIdx * 2) + 2; }
        private int getParentIndex(int childIdx) { return (childIdx -1)* 2; }

        private bool hasLeftChild(int index) { return getLeftChildIndex(index) < size; }
        private bool hasRightChild(int index) { return getLeftChildIndex(index) < size; }
        private bool hasParent(int index) { return getLeftChildIndex(index) >= 0; }

        private int LeftChild(int index) { return items[getLeftChildIndex(index)]; }
        private int RightChild(int index) { return items[getRightChildIndex(index)]; }
        private int Parent(int index) { return items[getParentIndex(index)]; }

        private void swap(int idx1,int idx2)
        {
            int temp = items[idx1];
            items[idx1] = items[idx2];
            items[idx2] = temp;
        }

        private void IncreaseCapacity()
        {
            if(size == capacity)
            {
                Array.Resize(ref items,capacity*2);
                capacity *= 2;
            }
        }

        public int peek()
        {
            if (size == 0)
                throw new Exception("Heap is empty");
            else
                return items[0];
        }


        public void Insert(int item)
        {
            IncreaseCapacity();
            items[size] = item;
            size++;
            HeapifyUp();

        }

        public int Remove()
        {
            if (size == 0)
                throw new Exception("Heap is empty");
            int item = items[0];
            items[0] = items[size - 1];
            size--;
            HeapifyDown();
            return item;
        }

        private void HeapifyUp()
        {
            int index = size - 1;
            while(hasParent(index) && Parent(index) > items[index])
            {
                swap(getParentIndex(index), index);
                index = getParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            while (hasLeftChild(index))
            {
                int smallerChildindex = getLeftChildIndex(index);
                if(hasRightChild(index) && RightChild(index) < LeftChild(index))
                {
                    smallerChildindex = getRightChildIndex(index);
                }

                if (items[index] < items[smallerChildindex])
                {
                    break;
                }
                else
                {
                    swap(index, smallerChildindex);
                }

                index = smallerChildindex;
            }
        }
    }
}
