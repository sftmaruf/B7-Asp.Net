using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class CustomObjectType
    {
        public void HandleCustomObjectType(Type type, object? obj, ref bool isObjectType, ref string objectName)
        {
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            checkFields(fields, obj, ref isObjectType, ref objectName);
            checkProperties(properties, obj, ref isObjectType, ref objectName);
        }

        private void checkFields(FieldInfo[] fields, object? obj, ref bool isObjectType, ref string objectName)
        {
            foreach (var field in fields)
            {
                if (!Types.isJsonableType(field))
                {
                    isObjectType = true;
                    objectName = field.Name + ": ";
                    JsonFormatter.Convert(field.GetValue(obj));
                }
            }
        }

        private void checkProperties(PropertyInfo[] properties, object? obj, ref bool isObjectType, ref string objectName)
        {
            foreach (var property in properties)
            {
                if (!Types.isJsonableType(property))
                {
                    isObjectType = true;
                    objectName = property.Name + ": ";
                    JsonFormatter.Convert(property.GetValue(obj));
                }
            }
        }
    }
}
