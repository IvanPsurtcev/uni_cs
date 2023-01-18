using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace test1
{
    class MyListEnumerator<T> : IEnumerator<T>
    {
        private T[] buffer;
        private int size;
        private int position;

        public MyListEnumerator(T[] buf, int _size)
        {
            buffer = buf;
            size = _size;
            position = -1;
        }

        public T Current => position == -1 || position >= size ? throw new ArgumentException() : buffer[position];

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (position + 1 >= size)
            {
                return false;
            }
            position++;
            return true;
        }

        public void Reset()
        {
            position = -1;
        }
    }
    public class MyList<T> : IEnumerable<T>
    {
        private T[] buffer;
        private int capacity;
        private int size;
        private const int minCapacity = 4;
        private const int backlashConst = 2; //константа для подсчета люфта для уменьшения массива
        public int Count { get { return size; } }

        private int CalcBacklash()
        {
            return capacity * 2 / 5 - backlashConst;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator<T>(buffer, size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        public MyList()
        {
            capacity = minCapacity;
            size = 0;
            buffer = new T[capacity];
        }

        public MyList(params T[] input)
        {
            for (int i = 0; i < input.Length; i++) 
            {
                Add(input[i]);
            }
        }

        public void Add(T item)
        {
            if (size + 1 == capacity)
            {
                Resize(capacity << 1);
            }
            buffer[size] = item;
            size++;
        }

        public void Clear()
        {
            buffer = new T[minCapacity];
            capacity = minCapacity;
            size = 0;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            for (int i = index; i < size - 1; i++)
            {
                buffer[i] = buffer[i + 1];
            }
            size -= 1;
            if (CalcBacklash() >= size)
            {
                Resize(capacity >> 1);
            }
        }

        public void Insert(int index, T item)
        {
            if (size + 1 == capacity)
            {
                Resize(capacity << 1);
            }
            for (int i = size; i > index; i--)
            {
                buffer[i] = buffer[i - 1];
            }
            buffer[index] = item;
            size++;
        }

        public T this[int index]
        {
            get { if (index < 0 || index >= size) { throw new ArgumentOutOfRangeException(); } else { return buffer[index]; } }
            set { if (index < 0 || index >= size) { throw new ArgumentOutOfRangeException(); } else { buffer[index] = value; } }
        }
        private void Resize(int newCap)
        {
            if (size > newCap)
            {
                throw new Exception("There are more elements in the list then can be put in new capacity");
            }
            T[] newBuf = new T[newCap];
            for (int i = 0; i < size; i++)
            {
                newBuf[i] = buffer[i];
            }
            buffer = newBuf;
        }

        public MyList<T> Where(Func<T, bool> func)
        {
            MyList<T> resultList = new MyList<T>();
            for (int i = 0; i < size; i++) 
            {
                if (func(buffer[i])) 
                {
                    resultList.Add(buffer[i]);
                }
            }
            return resultList;
        }

        public MyList<TResult> Select<TResult>(Func<T, TResult> func)
        {
            MyList<TResult> resultList = new MyList<TResult>();
            for (int i = 0; i < size; i++)
            {
                resultList.Add(func(buffer[i]));
            }
            return resultList;
        }

        public List<T> ToList()
        {
            List<T> resultList = new List<T>();
            for (int i = 0; i < size; i++)
            {
                resultList.Add(buffer[i]);
            }
            return resultList;
        }
    }

    class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Group { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> testList = new MyList<int> { 1, 2, 3 };
            testList.Add(4);
            foreach (var item in testList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----");
            testList.Insert(0, 0);
            testList.RemoveAt(1);
            foreach (var item in testList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----");
            Console.WriteLine(testList[2]);
            Console.WriteLine("-----");

            var students = new MyList<Student>
            {
                new Student { FirstName="Илья", LastName="Петров", Group="БИТ201" },
                new Student { FirstName="Пётр", LastName="Ильин", Group="БИВ192" },
                new Student { FirstName="Софья", LastName="Козлова", Group="БПМ191"},
                new Student { FirstName="Мария", LastName="Степанова", Group="БПИ194"},
                new Student { FirstName="Николай", LastName="Кошкин", Group="БИБ202"},
                new Student { FirstName="Владимир", LastName="Крутой", Group="БИТ201"},
                new Student { FirstName="Александра",LastName="Милая", Group="БИВ192" }
            };
            var result = students.
            Where(z => z.Group == "БИВ192").
            Select(z => z.FirstName).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
            Console.ReadKey();
        }
    }

}


