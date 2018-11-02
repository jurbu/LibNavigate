using LibNavigate.Converter;
using LibNavigate.Iterator.Helper;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Adapter which convert one input data type to another
    /// </summary>
    /// <typeparam name="From"></typeparam>
    /// <typeparam name="To"></typeparam>
    public sealed class InputIteratorAdapter<From, To> : IInputIterator<To>,IRemoveable,
        IPartialClone,IShallowClone,ICursor
    {
        private readonly Iterator.IInputIterator<From> inputIterator;

        private readonly IConverter<From, To> converter;

        public InputIteratorAdapter(Iterator.IInputIterator<From> inputIterator,
            IConverter<From,To> converter)
        {
            this.inputIterator = inputIterator;

            this.converter = converter;
        }
        public void Dispose()
        {
            inputIterator.Dispose();
        }

        public bool IsEnd()
        {
            return inputIterator.IsEnd();
        }

        public void MoveNext()
        {
            inputIterator.MoveNext();
        }

        public To Read()
        {
            return converter.Convert(inputIterator.Read());
        }

        public void Begin()
        {
            inputIterator.Begin();
        }

        public void Remove()
        {
            ((IRemoveable)inputIterator).Remove();
        }

        public void End()
        {
            inputIterator.End();
        }

        public object PartialClone()
        {
            return ((IPartialClone)inputIterator).PartialClone();
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
