
namespace Assignment1
{
    public class JsonFormatter
    {
        public static string? Convert(object? obj)
        {
            IObjectToJsonConverter _converter = new ObjectToJsonConverter(new CodeIndentor());
            _converter.Convert(obj);
            return _converter.GetJson();
        }
    }
}
