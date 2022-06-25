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

        public EnumerableType(IObjectToJsonConverter _converter)
        {
            this._converter = _converter;
        }

        public void ConvertToJson(FieldInfo field, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            dynamic? values = field.GetValue(obj);
            Convert(values, field.Name, ref _tempString, ref isObjectInsideArray);
        }

        public void ConvertToJson(PropertyInfo property, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            dynamic? values = property.GetValue(obj);
            Convert(values, property.Name, ref _tempString, ref isObjectInsideArray);
        }

        private void Convert(dynamic values, string name, ref StringBuilder _tempString, ref bool isObjectInsideArray)
        {
            string title = StringFormatter.WrapByQuotation(name);

            _tempString.Append(title).Append(": ").Append("[ ");
            AppendValues(values, ref _tempString, ref isObjectInsideArray);
            _tempString.Append("],\n");
        }

        private void AppendValues(dynamic values, ref StringBuilder _tempString, ref bool isObjectInsideArray)
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
            _tempString.Append("}, ");
            isObjectInsideArray = false;
        }
    }
}
