namespace LibNavigate.Iterator
{
    public interface IRandomAccessIterator<T, Offset> : IBidirectionalIterator<T>
    {
        T OffsetBy(Offset index);
    }
}
