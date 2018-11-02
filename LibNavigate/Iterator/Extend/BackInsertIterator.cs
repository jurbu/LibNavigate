using System.Collections.Generic;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Output iterator which add data to the collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BackInsertIterator<T> : IOutputIterator<T>
    {
        private readonly ICollection<T> collection;

        public BackInsertIterator(ICollection<T> collection)
        {
            this.collection = collection;
        }

        public void Dispose()
        {
            //no-op
        }

        public void MoveNext()
        {
            //no-op
        }

        public void Write(T data)
        {
            collection.Add(data);
        }
    }
}
