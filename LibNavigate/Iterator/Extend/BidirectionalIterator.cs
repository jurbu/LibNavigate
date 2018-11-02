using LibNavigate.Iterator.Helper;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Library implementation of IBidirectionalIterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BidirectionalIterator<T> : IBidirectionalIterator<T>,IAccessible<T>,
        IPartialClone,IShallowClone,IRemoveable,ICursor
    {
        private T[] dataArry;

        private int currentIndex;

        private bool isDataRemoved;

        public BidirectionalIterator(T[] data)
        {
            this.dataArry = data;

            Begin();

        }

        public BidirectionalIterator(int capacity):this(new T[capacity])
        {

        }


        public BidirectionalIterator(IList<T> data):this(data.ToArray())
        {

        }

        public BidirectionalIterator(IEnumerable<T> data):this(data.ToArray())
        {

        }

        private BidirectionalIterator(BidirectionalIterator<T> obj,bool isDeepClone=true)
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
            return (currentIndex >= dataArry.Length);
        }

        public void MoveNext()
        {
            ++currentIndex;
        }

        public void MovePrev()
        {
            //if the data is previously removed
            //we don't need to change the index

            if(isDataRemoved)
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

        public void Write(T data)
        {
            dataArry[currentIndex] = data;
        }

        public void Remove()
        {
            dataArry=dataArry.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;

            isDataRemoved = true;
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
            return new BidirectionalIterator<T>(this);
        }

        public void End()
        {
            currentIndex = dataArry.Length;
        }

        public object ShallowClone()
        {
            return new BidirectionalIterator<T>(this,false);
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
