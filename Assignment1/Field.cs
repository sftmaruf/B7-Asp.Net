using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Field : IJsonConvertible
    {
        private IObjectToJsonConverter _converter;
        private IEnumerableType _enumerableType;
        private bool isObject(FieldInfo field) => !Type.IsJsonableType(field);

        public Field(IObjectToJsonConverter converter, IEnumerableType enumerableType)
        {
            _converter = converter;
            _enumerableType = enumerableType;
        }

        public void ConvertToJson(object obj, ref StringBuilder _tempString, ref bool isObjectInsideArray, ref string? objectName)
        {
            FieldInfo[] fields = obj.GetType().GetFields();

            foreach (var field in fields)
            {
                if (Type.IsJsonableType(field))
                {
                    _tempString.Append(BuildJsonString(field, obj, isObjectInsideArray));
                }

                if (Type.isEnumerableType(field))
                {
                    _enumerableType.ConvertToJson(field, obj, ref _tempString, ref isObjectInsideArray);
                    continue;
                }

                if (isObject(field))
                {
                    string title = StringFormatter.WrapByQuotation(field.Name);
                    _tempString.Append(title).Append(": ").Append("{\n");
                    _converter.Convert(field.GetValue(obj));
                    _tempString.Append("},\n");
                }
            }
        }

        private string BuildJsonString(FieldInfo field, object? obj, bool isObjectInsideArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string? value = field.FieldType == typeof(string) ? StringFormatter.WrapByQuotation(field.GetValue(obj)?.ToString())  : field.GetValue(obj)?.ToString();
            string title = StringFormatter.WrapByQuotation(field.Name);

            stringBuilder.Append(title).Append(": ").Append(value).Append(", ");
            if (!isObjectInsideArray) stringBuilder.Append('\n');
            return stringBuilder.ToString();
        }
    }
}
