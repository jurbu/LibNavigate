using LibNavigate.Iterator.Helper;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Library implementation of IInputIterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class InputIterator<T> : IInputIterator<T>,IAccessible<T>,
        IPartialClone,IShallowClone,IRemoveable,ICursor
    {
        private T[] dataArry;

        private int currentIndex;

        public InputIterator(T[] data)
        {
            this.dataArry = data;

            Begin();

        }

        public InputIterator(int capacity):this(new T[capacity])
        {
        }

        public InputIterator(IList<T> data) : this(data.ToArray())
        {
        }

        public InputIterator(IEnumerable<T> data) : this(data.ToArray())
        {

        }

        private InputIterator(InputIterator<T> obj,bool isDeepClone=true)
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

        public void Begin()
        {
            currentIndex = 0;
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
        }

        public void End()
        {
            currentIndex = dataArry.Length;
        }

        public object PartialClone()
        {
            return new InputIterator<T>(this);
        }

        public object ShallowClone()
        {
            return new InputIterator<T>(this,false);
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
