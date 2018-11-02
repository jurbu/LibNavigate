namespace LibNavigate.Iterator
{
    /// <summary>
    /// Interface which allow both reading and writing of data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IForwardIterator<T> : IInputIterator<T>, IOutputIterator<T>
    {
    }
}
