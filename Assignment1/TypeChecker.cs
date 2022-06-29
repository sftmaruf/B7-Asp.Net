using System.Reflection;

namespace Assignment1
{
    public class TypeChecker : ITypeChecker
    {
        public bool isDictionaryType(FieldInfo field)
        {
            if(isEnumerableType(field))
            {
                Type? enumerableType = field.FieldType.GetInterface("IEnumerable`1");
                if (enumerableType is null) return false;
                return enumerableType.GetGenericArguments().Any(t => t.Name == "KeyValuePair`2");
            }
            return false;
        }

        public bool isDictionaryType(PropertyInfo property)
        {
            if (isEnumerableType(property))
            {
                System.Type? enumerableType = property.PropertyType.GetInterface("IEnumerable`1");
                if (enumerableType is null) return false;
                return enumerableType.GetGenericArguments().Any(t => t.Name == "KeyValuePair`2");
            }
            return false;
        }

        public bool isEnumerableType(FieldInfo field)
        {
            return (field.FieldType.GetInterface("ICollection") != null && field.FieldType.GetInterface("IEnumerable") != null)
                || field.FieldType.IsInterface && field.FieldType.GetInterface("IEnumerable") != null
                || field.FieldType.IsArray;
        }

        public bool isEnumerableType(PropertyInfo property)
        {

            return (property.PropertyType.GetInterface("ICollection") != null && property.PropertyType.GetInterface("IEnumerable") != null)
                || property.PropertyType.IsInterface && property.PropertyType.GetInterface("IEnumerable") != null
                || property.PropertyType.IsArray;
        }

        public bool IsJsonableType(FieldInfo field)
        {
            return field.FieldType.IsValueType
                || field.FieldType.Name == "String";
        }

        public bool IsJsonableType(TypeInfo type)
        {
            return type.IsValueType
                || type.Name == "String";
        }

        public bool IsJsonableType(PropertyInfo property)
        {
            return property.PropertyType.IsValueType
                || property.PropertyType.Name == "String";
        }

        public bool isQuotableType(FieldInfo field) => field.FieldType == typeof(string) || field.FieldType == typeof(DateTime);

        public bool isQuotableType(PropertyInfo property) => property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime);
    }
}
