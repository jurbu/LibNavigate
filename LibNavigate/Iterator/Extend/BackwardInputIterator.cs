using LibNavigate.Iterator.Helper;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigate.Iterator.Extend
{
    public sealed class BackwardInputIterator<T> : IIntRandomAccessIterator<T>, IAccessible<T>,
        IPartialClone, IShallowClone, IRemoveable, ICursor
    {
        private T[] dataArry;

        private int currentIndex;

        private bool isDataRemoved;

        public BackwardInputIterator(T[] data)
        {
            this.dataArry = data;

            Begin();

        }

        public BackwardInputIterator(int capacity):this(new T[capacity])
        {
        }

        public BackwardInputIterator(IList<T> data) : this(data.ToArray())
        {
        }

        public BackwardInputIterator(IEnumerable<T> data) : this(data.ToArray())
        {

        }

        private BackwardInputIterator(BackwardInputIterator<T> obj, bool isDeepClone = true)
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

            this.isDataRemoved = obj.isDataRemoved;

        }

        public void Dispose()
        {
            //no-op
        }

        public bool IsEnd()
        {
            return (currentIndex < 0);
        }

        public void MoveNext()
        {
            //if the data is previously removed
            //we don't need to change the index

            if (isDataRemoved)
            {
                isDataRemoved = false;

                return;
            }

            --currentIndex;
        }

        public T Read()
        {
            return dataArry[currentIndex];
        }

        public void Begin()
        {
            currentIndex = dataArry.Length-1;
        }

        public int Count()
        {
            return dataArry.Length;
        }

        public void Remove()
        {
            dataArry = dataArry.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;

            isDataRemoved = true;
        }

        public void End()
        {
            currentIndex = 0;
        }

        public void Write(T data)
        {
            dataArry[currentIndex] = data;
        }

        public T OffsetBy(int index)
        {
            //since this is a backward iterator 
            //we have to change the numeric sign

            index = index * -1;

            SetPosition(currentIndex + index);

            return Read();
        }

        public void MovePrev()
        {
            ++currentIndex;
        }

        public object PartialClone()
        {
            return new BackwardInputIterator<T>(this);
        }

        public object ShallowClone()
        {
            return new BackwardInputIterator<T>(this, false);
        }

        public T this[int index]
        {
            get
            {
                return dataArry[index];
            }
        }


        public int GetPosition()
        {
            return currentIndex;
        }

        public void SetPosition(int position)
        {
            this.currentIndex = position;
        }

       
    }
    
}
