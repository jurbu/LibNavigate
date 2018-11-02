using LibNavigate.Iterator.Helper;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    ///  Library implementation of IForwardIterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ForwardIterator<T> : IForwardIterator<T>,IAccessible<T>,
        IPartialClone,IShallowClone,IRemoveable,ICursor
    {
        private T[] dataArry;

        private int currentIndex;

        public ForwardIterator(T[] data)
        {
            this.dataArry = data;

            Begin();

        }

        public ForwardIterator(int capacity):this(new T[capacity])
        {

        }

        public ForwardIterator(IList<T> data):this(data.ToArray())
        {
        }

        public ForwardIterator(IEnumerable<T> data):this(data.ToArray())
        {

        }

        private ForwardIterator(ForwardIterator<T> obj, bool isDeepClone = true)
        {
            if (isDeepClone)
            {
                this.dataArry = obj.dataArry.DeepClone();
            }
            else
            {
                this.dataArry = obj.dataArry;
            }

            this.currentIndex = obj.currentIndex;

        }


        public void Dispose()
        {
            //no-op
        }

        public bool IsEnd()
        {
            return (currentIndex >= dataArry.Length);
        }

        public void MoveNext()
        {
            ++currentIndex;
        }

        public T Read()
        {
            return dataArry[currentIndex];
        }

        public void Write(T data)
        {
            dataArry[currentIndex] = data;
        }

        public void Remove()
        {
            dataArry = dataArry.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;
        }

        public void Begin()
        {
            this.currentIndex = 0;
        }

        public int Count()
        {
            return dataArry.Length;
        }

        public object PartialClone()
        {
            return new ForwardIterator<T>(this);
        }

        public void End()
        {
            currentIndex = dataArry.Length;
        }

        public object ShallowClone()
        {
            return new ForwardIterator<T>(this,false);
        }

        public int GetPosition()
        {
            return currentIndex;
        }

        public void SetPosition(int position)
        {
            this.currentIndex = position;
        }

        public T this[int index]
        {
            get
            {
                return dataArry[index];
            }
        }
    }
}
