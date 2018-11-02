namespace LibNavigate.Iterator
{
    /// <summary>
    /// Iterator which can move both way
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBidirectionalIterator<T> : IForwardIterator<T>
    {
        /// <summary>
        /// Move to previous element
        /// </summary>
        void MovePrev();
    }
}
