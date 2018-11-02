namespace LibNavigate.Iterator
{
    /// <summary>
    /// Interface which allow writing of data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOutputIterator<T> : IIteratorBase
    {
        void Write(T data);
    }
}
