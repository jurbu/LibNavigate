namespace LibNavigate.Iterator
{
    /// <summary>
    /// Interface which allow reading of data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInputIterator<T> : IIteratorBase
    {
        /// <summary>
        /// Read the current element
        /// </summary>
        /// <returns></returns>
        T Read();

  
        /// <summary>
        /// Whether we reach the end of data
        /// Reading it is at IsEnd() produce error.
        /// Implmentation Note :IsEnd()=End+1
        /// </summary>
        /// <returns></returns>
        bool IsEnd();

        /// <summary>
        /// Make iterator goes to the beginning
        /// </summary>
        void Begin();

        /// <summary>
        /// Make iterator goes to the ending
        /// Reading when is it at End() produce error
        /// </summary>
        void End();
    }
}
