namespace LibNavigate.Iterator.Helper
{
    /// <summary>
    /// Interface to access internal data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAccessible<T>
    {
        int Count();

        T this[int index]
        {
            get;
        }
    }
}
