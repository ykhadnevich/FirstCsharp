using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCsharp
{
    public class Stack<T>
    {
        private const int Capacity = 100;
        private T[] array = new T[Capacity];
        private int Pointer;

        public int Count { get { return Pointer; } }

        public  void Push(T value)
        {
            if (Pointer == Capacity)
                throw new StackOverflowException("Stack overflowed");

            array[Pointer++] = value;
        }

        public T Pop()
        {
            return array[--Pointer];
        }
        public T Peek()
        {
            return array[Pointer - 1];
        }

        public bool IsEmpty()
        {
            return Pointer == 0;
        }
    }
}
