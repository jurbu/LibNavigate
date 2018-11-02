using LibNavigate.Converter;

namespace LibNavigateTests
{
    public sealed class IntToStringConverter : IConverter<int, string>
    {
        public string Convert(int data)
        {
            return data.ToString();
        }
    }
}
