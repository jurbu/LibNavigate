namespace LibNavigate.Converter
{

    /// <summary>
    /// Convert from one data type to another
    /// </summary>
    /// <typeparam name="From"></typeparam>
    /// <typeparam name="To"></typeparam>
    public interface IConverter<From,To>
    {
        To Convert(From data);
    }
}
