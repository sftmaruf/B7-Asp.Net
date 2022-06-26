using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Property : IJsonConvertible
    {
        private readonly IObjectToJsonConverter _converter;
        private readonly IEnumerableType _enumerableType;
        private bool isObject(PropertyInfo property) => !Type.IsJsonableType(property);

        public Property(IObjectToJsonConverter converter, IEnumerableType enumerableType)
        {
            _converter = converter; 
            _enumerableType = enumerableType;
        }

        public void ConvertToJson(object obj, ref StringBuilder _json)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                if (Type.IsJsonableType(property))
                {
                    if (!(property.GetIndexParameters().Length > 0))
                    {
                        _json.Append(BuildJsonString(property, obj));
                    }
                }
                else if (Type.isEnumerableType(property))
                {
                    _enumerableType.ConvertToJson(property, obj, ref _json);
                    continue;
                }
                else if (isObject(property))
                {
                    string key = StringFormatter.WrapByQuotation(property.Name);
                    var value = property.GetValue(obj);

                    AppendValueToJsonObject(ref _json, key, value);
                }
            }
        }

        private void AppendValueToJsonObject(ref StringBuilder _json, string key, object? value)
        {
            _json.Append(key).Append(": ");
            if (value == null)
            {
                _json?.Append("null").Append('\n');
            }
            else
            {
                _json.Append('{').Append('\n');
                _converter.Convert(value);
                _json.Append('}').Append(',').Append('\n');
            }
        }

        private string BuildJsonString(PropertyInfo property, object? obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string? value = Type.isQuotableType(property) ? StringFormatter.WrapByQuotation(property.GetValue(obj)?.ToString()) : property.GetValue(obj)?.ToString();
            string jsonKey = StringFormatter.WrapByQuotation(property.Name);

            stringBuilder.Append(jsonKey).Append(": ").Append(value).Append(", ").Append('\n');
            return stringBuilder.ToString();
        }
    }
}
