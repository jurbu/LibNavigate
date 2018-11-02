using LibNavigate.Converter;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Adapter which convert one output data type to another
    /// </summary>
    /// <typeparam name="From"></typeparam>
    /// <typeparam name="To"></typeparam>
    public sealed class OutputIteratorAdapter<From, To> : IOutputIterator<To>
    {
        private readonly IOutputIterator<From> outputIterator;

        private readonly IConverter<To, From> converter;

        public OutputIteratorAdapter(IOutputIterator<From> outputIterator,
            IConverter<To,From> converter)
        {
            this.outputIterator = outputIterator;

            this.converter = converter;
        }

        public void Dispose()
        {
            outputIterator.Dispose();
        }

        public void MoveNext()
        {
            outputIterator.MoveNext();
        }

        public void Write(To data)
        {
            outputIterator.Write(converter.Convert(data));
        }
    }
}
