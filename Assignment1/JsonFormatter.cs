using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
