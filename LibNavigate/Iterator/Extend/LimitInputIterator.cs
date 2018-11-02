using LibNavigate.Iterator.Helper;

namespace LibNavigate.Iterator.Extend
{
    public sealed class LimitInputIterator<T> : IInputIterator<T>,
        IPartialClone, IShallowClone, ICursor
    {
        private IInputIterator<T> inputIterator;

        private readonly IInputIterator<T> beginIterator;

        private readonly int count;

        private int currentIndex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputIterator">
        /// InputIterator is set to the location we wanted to start with
        /// Requires IShallowClone</param>
        /// <param name="count">how many element starting from begin position </param>
        public LimitInputIterator(IInputIterator<T> inputIterator,int count)
        {
            this.inputIterator = inputIterator;

            beginIterator = ShallowClone() as IInputIterator<T>;

            this.count = count-1;

            if(count<0)
            {
                count = 0;
            }

            this.currentIndex = 0;

            Begin();

        }

        public void Begin()
        {
            inputIterator = beginIterator;
        }

        public void Dispose()
        {
            inputIterator.Dispose();
        }

        public void End()
        {
            inputIterator.End();
        }

        public bool IsEnd()
        {
            return (currentIndex >= count || inputIterator.IsEnd());
        }

        public void MoveNext()
        {
            inputIterator.MoveNext();

            ++currentIndex;
        }

        public object PartialClone()
        {
            return ((IPartialClone)inputIterator).PartialClone();
        }

        public T Read()
        {
            return inputIterator.Read();
        }

        public object ShallowClone()
        {
            return ((IShallowClone)inputIterator).ShallowClone();
        }

        public int GetPosition()
        {
            return ((ICursor)inputIterator).GetPosition();
        }

        public void SetPosition(int position)
        {
            ((ICursor)inputIterator).SetPosition(position);
        }
    }
}
