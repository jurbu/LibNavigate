using System;

namespace LibNavigate.Iterator
{

    public interface IIteratorBase : IDisposable
    {
        /// <summary>
        /// Move to the next element
        /// </summary>
        void MoveNext();
    }
}
