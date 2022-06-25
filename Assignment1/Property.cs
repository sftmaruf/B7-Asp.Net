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
        private IObjectToJsonConverter _converter;
        private IEnumerableType _enumerableType;
        private bool isObject(PropertyInfo property) => !Type.IsJsonableType(property);

        public Property(IObjectToJsonConverter converter, IEnumerableType enumerableType)
        {
            _converter = converter; 
            _enumerableType = enumerableType;
        }

        public void ConvertToJson(object obj, ref StringBuilder _tempString, ref bool isObjectInsideArray, ref string? objectName)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                if (Type.IsJsonableType(property))
                {
                    if (!(property.GetIndexParameters().Length > 0))
                    {
                        _tempString.Append(BuildJsonString(property, obj, isObjectInsideArray));
                    }
                }

                if (Type.isEnumerableType(property))
                {
                    _enumerableType.ConvertToJson(property, obj, ref _tempString, ref isObjectInsideArray);
                    continue;
                }

                if (isObject(property))
                {
                    string title = StringFormatter.WrapByQuotation(property.Name);
                    _tempString.Append(title).Append(": ").Append("{\n");
                    _converter.Convert(property.GetValue(obj));
                    _tempString.Append("},\n");
                }
            }
        }

        private string BuildJsonString(PropertyInfo property, object? obj, bool isObjectInsideArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string? value = property.PropertyType == typeof(string) ? StringFormatter.WrapByQuotation(property.GetValue(obj)?.ToString()) : property.GetValue(obj)?.ToString();
            string title = StringFormatter.WrapByQuotation(property.Name);

            stringBuilder.Append(title).Append(": ").Append(value).Append(", ");
            if (!isObjectInsideArray) stringBuilder.Append('\n');
            return stringBuilder.ToString();
        }
    }
}
