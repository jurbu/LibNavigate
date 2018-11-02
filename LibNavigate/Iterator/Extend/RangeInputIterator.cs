using LibNavigate.Iterator.Helper;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Input iterator which read the data only within a certain range
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RangeInputIterator<T> : IInputIterator<T>,
        IPartialClone,IShallowClone,ICursor
    {
        private T[] dataArry;

        private int maximumLength;

        private int currentIndex;

        private readonly int startIndex;

        public RangeInputIterator(T[] data, int startIndex, int count)
        {
            this.dataArry = data;

            this.startIndex = startIndex;

            Begin();

            maximumLength = CalculateMaximumLength(startIndex, count);

        }

        public RangeInputIterator(T[] data,int count):this(data,0,count)
        {
        }

        public RangeInputIterator(ICollection<T> data, int startIndex, int count)
            :this(data.ToArray(),startIndex,count)
        {

        }

        public RangeInputIterator(ICollection<T> data,int count)
            :this(data.ToArray(),0,count)
        {

        }

        private RangeInputIterator(RangeInputIterator<T> obj,bool isDeepClone=false)
        {
            if (isDeepClone)
            {
                this.dataArry = obj.dataArry.DeepClone();
            }
            else
            {
                this.dataArry = obj.dataArry;
            }

            this.maximumLength = obj.maximumLength;

            this.currentIndex = obj.currentIndex;

            this.startIndex = obj.startIndex;
        }


        private int CalculateMaximumLength(int startIndex,int count)
        {
            return startIndex + count;
        }

        public void Dispose()
        {
            //no-op
        }

        public bool IsEnd()
        {
            return (currentIndex >= maximumLength);
        }

        public void MoveNext()
        {
            ++currentIndex;
        }

        public T Read()
        {
            return dataArry[currentIndex];
        }

        public void Begin()
        {
            currentIndex = startIndex;
        }

        public void Remove()
        {
            dataArry = dataArry.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;

        }

        public void End()
        {
            currentIndex = maximumLength;
        }

        public object ShallowClone()
        {
            return new RangeInputIterator<T>(this, false);
        }

        public object PartialClone()
        {
            return new RangeInputIterator<T>(this);
        }

        public int GetPosition()
        {
            return currentIndex;
        }

        public void SetPosition(int position)
        {
            currentIndex = position;
        }
    }
}
