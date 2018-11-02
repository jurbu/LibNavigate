using System.Collections;
using System.Collections.Generic;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Adapter class to allow multiple iterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MultiOutputIterator<T> : IList<IOutputIterator<T>>, IOutputIterator<T>
    {
        private readonly List<IOutputIterator<T>> iters;

        public MultiOutputIterator()
        {
            iters = new List<IOutputIterator<T>>();

        }

        public IOutputIterator<T> this[int index]
        {
            get
            {
                return iters[index];
            }

            set
            {
                iters[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return iters.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(IOutputIterator<T> item)
        {
            iters.Add(item);
        }

        public void Clear()
        {
            iters.Clear();
        }

        public bool Contains(IOutputIterator<T> item)
        {
            return iters.Contains(item);
        }

        public void CopyTo(IOutputIterator<T>[] array, int arrayIndex)
        {
            iters.CopyTo(array);
        }

        public void Dispose()
        {
            foreach (IOutputIterator<T> iter in iters)
            {
                iter.Dispose();
            }

            iters.Clear();
        }

        public IEnumerator<IOutputIterator<T>> GetEnumerator()
        {
            return iters.GetEnumerator();
        }

        public int IndexOf(IOutputIterator<T> item)
        {
            return iters.IndexOf(item);
        }

        public void Insert(int index, IOutputIterator<T> item)
        {
            iters.Insert(index, item);
        }

        public void MoveNext()
        {
            foreach (IOutputIterator<T> iter in iters)
            {
                iter.MoveNext();
            }
        }


        public bool Remove(IOutputIterator<T> item)
        {
            return iters.Remove(item);
        }

        public void RemoveAt(int index)
        {
            iters.RemoveAt(index);
        }

        public void Write(T data)
        {
            foreach (IOutputIterator<T> iter in iters)
            {
                iter.Write(data);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return iters.GetEnumerator();
        }
    }
}
