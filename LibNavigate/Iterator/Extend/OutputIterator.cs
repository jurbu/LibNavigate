using System;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Library implementation of IOutputIterator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OutputIterator<T> : IOutputIterator<T>
    {
        private readonly Action<T> writeAction;

        private readonly Action disposeAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeAction">How the data are output.Object.ToString() are provided as input</param>
        /// <param name="disposeAction"></param>
        public OutputIterator(Action<T> writeAction, Action disposeAction = null)
        {
            this.writeAction = writeAction;

            this.disposeAction = disposeAction;

        }

        public void Dispose()
        {
            if(disposeAction!=null)
            {
                disposeAction();
            }
        }

        public void MoveNext()
        {
            //no-op
        }

        public void Write(T data)
        {
            if (writeAction != null)
            {
                writeAction.Invoke(data);
            }
        }
    }
}
