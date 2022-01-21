using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCsharp
{
    public class Queue<T>
    {
        private const int Capacity = 100;
        private T[] Array = new T[Capacity];
        private int Pointer;

        public int Count { get { return Pointer; } }

        public void Enqueue(T value)
        {
            if (Pointer == Capacity)
                throw new StackOverflowException("Stack overflowed");

            Array[Pointer++] = value;
        }

        public T Dequeue()
        {
            if (Pointer == 0) return default(T);
            var value = Array[0];
            Pointer--;

            for (var i = 0; i < Pointer; i++)
                Array[i] = Array[i + 1];

            return value;
        }

        public bool IsEmpty()
        {
            return Pointer == 0;
        }
    }
}
