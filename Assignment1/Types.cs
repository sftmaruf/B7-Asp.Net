using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public static class Types 
    {
        public static bool isEnumerableType(FieldInfo field)
        {
            return field.FieldType.GetInterface("ICollection") != null || field.FieldType.IsArray;
        }

        public static bool isEnumerableType(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.PropertyType.GetInterface("ICollection") != null;
        }

        public static bool isJsonableType(FieldInfo field)
        {
            return field.FieldType.IsValueType
                || field.FieldType.Name == "String"
                || field.FieldType.GetInterface("ICollection") != null
                || field.FieldType.IsArray;
        }

        public static bool isJsonableType(TypeInfo type)
        {
            return type.IsValueType
                || type.Name == "String"
                || type.GetInterface("ICollection") != null
                || type.IsArray;
        }

        public static bool isJsonableType(PropertyInfo property)
        {
            return property.PropertyType.IsValueType
                || property.PropertyType.Name == "String"
                || property.PropertyType.GetInterface("ICollection") != null
                || property.PropertyType.IsArray;
        }
    }
}
