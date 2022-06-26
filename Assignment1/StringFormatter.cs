using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
