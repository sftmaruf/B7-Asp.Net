using System.Reflection;

namespace Assignment1
{
    public static class Type 
    {
        public static bool isDictionaryType(FieldInfo field)
        {
            if(isEnumerableType(field))
            {
                System.Type? enumerableType = field.FieldType.GetInterface("IEnumerable`1");
                if (enumerableType is null) return false;
                return enumerableType.GetGenericArguments().Any(t => t.Name == "KeyValuePair`2");
            }
            return false;
        }

        public static bool isDictionaryType(PropertyInfo property)
        {
            if (isEnumerableType(property))
            {
                System.Type? enumerableType = property.PropertyType.GetInterface("IEnumerable`1");
                if (enumerableType is null) return false;
                return enumerableType.GetGenericArguments().Any(t => t.Name == "KeyValuePair`2");
            }
            return false;
        }

        public static bool isEnumerableType(FieldInfo field)
        {
            return (field.FieldType.GetInterface("ICollection") != null && field.FieldType.GetInterface("IEnumerable") != null)
                || field.FieldType.IsInterface && field.FieldType.GetInterface("IEnumerable") != null
                || field.FieldType.IsArray;
        }

        public static bool isEnumerableType(PropertyInfo property)
        {

            return (property.PropertyType.GetInterface("ICollection") != null && property.PropertyType.GetInterface("IEnumerable") != null)
                || property.PropertyType.IsInterface && property.PropertyType.GetInterface("IEnumerable") != null
                || property.PropertyType.IsArray;
        }

        public static bool IsJsonableType(FieldInfo field)
        {
            return field.FieldType.IsValueType
                || field.FieldType.Name == "String";
        }

        public static bool IsJsonableType(TypeInfo type)
        {
            return type.IsValueType
                || type.Name == "String";
        }

        public static bool IsJsonableType(PropertyInfo property)
        {
            return property.PropertyType.IsValueType
                || property.PropertyType.Name == "String";
        }

        public static bool isQuotableType(FieldInfo field) => field.FieldType == typeof(string) || field.FieldType == typeof(DateTime);

        public static bool isQuotableType(PropertyInfo property) => property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime);
    }
}
