using System.Text;

namespace Assignment1
{
    public class StringFormatter
    {
        public static string WrapByQuotation(string? placeholder)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('"').Append(placeholder).Append('"');
            return stringBuilder.ToString();
        }
    }
}
