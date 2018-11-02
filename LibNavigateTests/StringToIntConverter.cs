using LibNavigate.Converter;

namespace LibNavigateTests
{
    public sealed class StringToIntConverter : IConverter<string, int>
    {
        public int Convert(string data)
        {
            return int.Parse(data);
        }
    }
}
