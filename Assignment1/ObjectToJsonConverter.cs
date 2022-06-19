using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class ObjectToJsonConverter : IObjectToJsonConverter
    {
        private StringBuilder _json = new StringBuilder();
        private object _object;

        public string ConvertToJson(object obj)
        {
            if (obj == null) throw new ArgumentException("Object needs to be instantiated");
            _object = obj;

            Type type = _object.GetType();

            handleFields(type);
            return "";
        }

        private void handleFields(Type type)
        {
            FieldInfo[] fields = type.GetFields();

            foreach (var field in fields)
            {
                if (field.FieldType.IsValueType)
                {
                    _json.Append(field.Name).Append(": ").Append(field.GetValue(_object)).Append("\n");
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(_json.ToString());
        }
    }
}
