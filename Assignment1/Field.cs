﻿using System.Reflection;
using System.Text;

namespace Assignment1
{
    public class Field : IJsonConvertible
    {
        private readonly IObjectToJsonConverter _converter;
        private readonly IEnumerableType _enumerableType;
        private readonly ITypeChecker _typeChecker;
        private bool IsObject(FieldInfo field) => !_typeChecker.IsJsonableType(field);

        public Field(IObjectToJsonConverter converter, IEnumerableType enumerableType, ITypeChecker typeChecker)
        {
            _converter = converter;
            _enumerableType = enumerableType;
            _typeChecker = typeChecker;
        }

        public void ConvertToJson(object obj, ref StringBuilder _json)
        {
            FieldInfo[] fields = obj.GetType().GetFields();

            foreach (var field in fields)
            {
                if (_typeChecker.IsJsonableType(field))
                {
                    _json.Append(BuildJsonString(field, obj));
                }
                else if (_typeChecker.isEnumerableType(field))
                {
                    _enumerableType.ConvertToJson(field, obj, ref _json);
                    continue;
                }
                else if (IsObject(field))
                {
                    string key = StringFormatter.WrapByQuotation(field.Name);
                    var value = field.GetValue(obj);

                    AppendValueToJsonObject(ref _json, key, value);
                }
            }
        }

        private void AppendValueToJsonObject(ref StringBuilder _json, string key, object? value)
        {
            _json?.Append(key).Append(": ");
            if (value == null)
            {
                _json?.Append("null").Append('\n');
            }
            else
            { 
                _json?.Append('{').Append('\n');
                _converter.Convert(value);
                _json?.Append('}').Append(',').Append('\n');
            }
        }

        private string BuildJsonString(FieldInfo field, object? obj)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string? value = _typeChecker.isQuotableType(field) ? StringFormatter.WrapByQuotation(field.GetValue(obj)?.ToString())  : field.GetValue(obj)?.ToString();
            string jsonKey = StringFormatter.WrapByQuotation(field.Name);

            stringBuilder.Append(jsonKey).Append(": ").Append(value).Append(", ").Append('\n');
            return stringBuilder.ToString();
        }
    }
}
