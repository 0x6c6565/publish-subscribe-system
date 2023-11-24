using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace PublishSubscribeSystem
{
    internal class MessageHeap<T> where T : unmanaged, IEquatable<T>, IComparable<T>
    {
        const int children = 4;

        List<T> messages = new List<T>();

        public int Count => messages.Count;

        internal void Clear()
        {
            messages.Clear();
        }

        internal void Enqueue(in T message)
        {
            int index = Count;

            messages.Add(message);

            while (0 < index && 0 > messages[index].CompareTo(messages[Parent(index)]))
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        internal T Dequeue()
        {
            T item = messages[0];

            messages[0] = messages[Count - 1];
            messages.RemoveAt(Count - 1);

            int index = 0;
            int minIndex;

            do
            {
                minIndex = index;
                for (int n = 0; n < 3; ++n)
                {
                    int child = Child(index, n + 1);
                    if (child < Count && 0 > messages[child].CompareTo(messages[minIndex]))
                    {
                        minIndex = child;
                    }
                }

                if (minIndex != index)
                {
                    Swap(index, minIndex);
                    index = minIndex;

                    minIndex = -1;
                }
            } while (minIndex != index);

            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] int Parent(int index) => (index - 1) / children;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] int Child(int index, int child) => (index * children) + child;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Swap(int a, int b)
        {
            T temp = messages[a];
            messages[a] = messages[b];
            messages[b] = temp;
        }
    }
}
