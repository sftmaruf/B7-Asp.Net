using System.Reflection;
using System.Text;

namespace Assignment1
{
    public class EnumerableType : IEnumerableType
    {
        private readonly IObjectToJsonConverter _converter;
        private static bool IsObject(TypeInfo type) => !Type.IsJsonableType(type);
        private static bool IsString(dynamic value) => value.GetType() == typeof(string);

        public EnumerableType(IObjectToJsonConverter _converter)
        {
            this._converter = _converter;
        }

        public void ConvertToJson(FieldInfo field, object? obj, ref StringBuilder _json)
        {
            dynamic? values = field.GetValue(obj);
           
            if (Type.isDictionaryType(field))
            {
                ConvertDictionaryTypetoJson(values, field.Name, ref _json);
            }
            else
            {
                ConvertToJsonArray(values, field.Name, ref _json);
            }
        }

        public void ConvertToJson(PropertyInfo property, object? obj, ref StringBuilder _json)
        {
            dynamic? values = property.GetValue(obj);

            if (Type.isDictionaryType(property))
            {
                ConvertDictionaryTypetoJson(values, property.Name, ref _json);
            }
            else
            {
                ConvertToJsonArray(values, property.Name, ref _json);
            }
        }

        private void ConvertDictionaryTypetoJson(dynamic values, string name, ref StringBuilder _json)
        {
            string jsonKey = StringFormatter.WrapByQuotation(name);

            _json.Append(jsonKey).Append(": ").Append(" {").Append('\n');
            AppendValuesToJsonDictionary(values, ref _json);
            _json.Append('}').Append(',').Append('\n');
        }

        private void AppendValuesToJsonDictionary(dynamic values, ref StringBuilder _json)
        {
            dynamic? dictionaryKeys = values.Keys;

            foreach (var dictionaryKey in dictionaryKeys)
            {
                string jsonKey = StringFormatter.WrapByQuotation(dictionaryKey);
                string value = IsString(values[dictionaryKey]) ? StringFormatter.WrapByQuotation(values[dictionaryKey].ToString()) : values[dictionaryKey].ToString();
                _json.Append(jsonKey).Append(": ").Append(value).Append(',').Append('\n');
            }
        }

        private void ConvertToJsonArray(dynamic values, string name, ref StringBuilder _json)
        {
            string key = StringFormatter.WrapByQuotation(name);

            _json.Append(key).Append(": ").Append("[ ");
            AppendValuesToJsonArray(values, ref _json);
            _json.Append(']').Append(',').Append('\n');
        }

        private void AppendValuesToJsonArray(dynamic values, ref StringBuilder _json)
        {
            foreach (var value in values)
            {
                if (value != null)
                { 
                    if (IsObject(value.GetType()))
                    {
                        BuildJsonObject(ref _json, value);
                    }
                    else
                    {
                        string stringOfValue = value.GetType() == typeof(string) ? StringFormatter.WrapByQuotation(value.ToString()) : value.ToString();
                        _json.Append(stringOfValue).Append(", ");
                    }
                }
                else
                {
                    _json.Append("null").Append(", ");
                }
            }
        }

        private void BuildJsonObject(ref StringBuilder _tempString, object item)
        {
            _tempString.Append("{\n");
            _converter.Convert(item);
            _tempString.Append('}').Append(", ");
        }
    }
}
