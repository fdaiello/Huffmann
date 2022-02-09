using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffmann
{
    public class Minheap<T> where T : IComparable
    {
        private T[] heap;
        private int size = 0;

        public Minheap(int capacity)
        {
            heap = new T[capacity];
        }
        public bool Any()
        {
            return size > 0;
        }
        public int Count()
        {
            return size;
        }
        public void Insert(T value)
        {
            if (size < heap.Length)
            {
                heap[size] = value;
                size++;
                SwapUp();
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        public T Peek()
        {
            return heap[0];
        }
        public T Remove()
        {
            if (size > 0)
            {
                T min = heap[0];
                heap[0] = heap[size - 1];
                SwapDown();
                size--;
                return min;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        int ParentIndex(int x) => (x - 1) / 2;
        int LeftChildIndex(int x) => x * 2 + 1;
        int RightChildIndex(int x) => x * 2 + 2;
        bool HasLeftChild(int x) => LeftChildIndex(x) < size;
        bool HasRightChild(int x) => RightChildIndex(x) < size;
        T Parent(int x) => heap[ParentIndex(x)];
        T LeftChild(int x) => heap[LeftChildIndex(x)];
        T RightChild(int x) => heap[RightChildIndex(x)];
        void Swap(int x, int y)
        {
            T tmp = heap[y];
            heap[y] = heap[x];
            heap[x] = tmp;
        }
        void SwapUp()
        {
            int i = size - 1;

            while (i > 0 && Parent(i).CompareTo(heap[i]) > 0)
            {
                Swap(i, ParentIndex(i));
                i = ParentIndex(i);
            }
        }
        void SwapDown()
        {
            int i = 0;
            while ((HasLeftChild(i) && LeftChild(i).CompareTo(heap[i]) < 0) || (HasRightChild(i) && RightChild(i).CompareTo(heap[i]) < 0))
            {
                if (!HasRightChild(i) || LeftChild(i).CompareTo(RightChild(i)) < 0)
                {
                    Swap(i, LeftChildIndex(i));
                    i = LeftChildIndex(i);
                }
                else
                {
                    Swap(i, RightChildIndex(i));
                    i = RightChildIndex(i);
                }
            }
        }
    }
}
