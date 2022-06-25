using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class EnumerableType : IEnumerableType
    {
        private IObjectToJsonConverter _converter;
        private bool isObject(TypeInfo type) => !Type.IsJsonableType(type);
        private bool isString(dynamic value) => value.GetType() == typeof(string);

        public EnumerableType(IObjectToJsonConverter _converter)
        {
            this._converter = _converter;
        }

        public void ConvertToJson(FieldInfo field, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            dynamic? values = field.GetValue(obj);
           
            if (Type.isDictionaryType(field))
            {
                ConvertDictionaryTypetoJson(values, field.Name, ref _tempString);
            }
            else
            {
                ConvertToJsonArray(values, field.Name, ref _tempString, ref isObjectInsideArray);
            }
        }

        public void ConvertToJson(PropertyInfo property, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            dynamic? values = property.GetValue(obj);

            if (Type.isDictionaryType(property))
            {
                ConvertDictionaryTypetoJson(values, property.Name, ref _tempString);
            }
            else
            {
                ConvertToJsonArray(values, property.Name, ref _tempString, ref isObjectInsideArray);
            }
        }

        private void ConvertDictionaryTypetoJson(dynamic values, string name, ref StringBuilder _tempString)
        {
            string jsonKey = StringFormatter.WrapByQuotation(name);

            _tempString.Append(jsonKey).Append(": ").Append(" {").Append('\n');
            AppendValuesToJsonDictionary(values, ref _tempString);
            _tempString.Append("},").Append('\n');
        }

        private void AppendValuesToJsonDictionary(dynamic values, ref StringBuilder _tempString)
        {
            dynamic? dictionaryKeys = values.Keys;

            foreach (var dictionaryKey in dictionaryKeys)
            {
                string jsonKey = StringFormatter.WrapByQuotation(dictionaryKey);
                string value = isString(values[dictionaryKey]) ? StringFormatter.WrapByQuotation(values[dictionaryKey].ToString()) : values[dictionaryKey].ToString();
                _tempString.Append(jsonKey).Append(": ").Append(value).Append(',').Append("\n ");
            }
        }

        private void ConvertToJsonArray(dynamic values, string name, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            string key = StringFormatter.WrapByQuotation(name);

            _tempString.Append(key).Append(": ").Append("[ ");
            AppendValuesToJsonArray(values, ref _tempString, ref isObjectInsideArray);
            _tempString.Append("]").Append(",").Append("\n");
        }

        private void AppendValuesToJsonArray(dynamic values, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            foreach (var value in values)
            {
                if (isObject(value.GetType()))
                {
                    BuildJsonObject(ref _tempString, value, ref isObjectInsideArray);
                }
                else
                {
                    string stringOfValue = value.GetType() == typeof(string) ? StringFormatter.WrapByQuotation(value.ToString()) : value.ToString();
                    _tempString.Append(stringOfValue).Append(", ");
                }
            }
        }

        private void BuildJsonObject(ref StringBuilder _tempString, object item, ref bool isObjectInsideArray)
        {
            isObjectInsideArray = true;
            _tempString.Append("{ ");
            _converter.DismantleObject(item);
            _tempString.Append("},");
            isObjectInsideArray = false;
        }
    }
}
