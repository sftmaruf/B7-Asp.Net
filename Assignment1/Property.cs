using System.Reflection;
using System.Text;

namespace Assignment1
{
    public class Property : IJsonConvertible
    {
        private readonly IObjectToJsonConverter _converter;
        private readonly IEnumerableType _enumerableType;
        private readonly ITypeChecker _typeChecker;
        private bool isObject(PropertyInfo property) => !_typeChecker.IsJsonableType(property);

        public Property(IObjectToJsonConverter converter, IEnumerableType enumerableType, ITypeChecker typeChecker)
        {
            _converter = converter; 
            _enumerableType = enumerableType;
            _typeChecker = typeChecker;
        }

        public void ConvertToJson(object obj, ref StringBuilder _json)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                if (_typeChecker.IsJsonableType(property))
                {
                    if (!(property.GetIndexParameters().Length > 0))
                    {
                        _json.Append(BuildJsonString(property, obj));
                    }
                }
                else if (_typeChecker.isEnumerableType(property))
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
            string? value = _typeChecker.isQuotableType(property) ? StringFormatter.WrapByQuotation(property.GetValue(obj)?.ToString()) : property.GetValue(obj)?.ToString();
            string jsonKey = StringFormatter.WrapByQuotation(property.Name);

            stringBuilder.Append(jsonKey).Append(": ").Append(value).Append(", ").Append('\n');
            return stringBuilder.ToString();
        }
    }
}
