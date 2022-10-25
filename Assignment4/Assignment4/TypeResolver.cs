using System.Collections;
using System.Reflection;
using Assignment4.Enum;

namespace Assignment4
{
    public class TypeResolver : ITypeResolver
    {
        public bool isEnumerableType(Type type) => typeof(IEnumerable).IsAssignableFrom(type);
        public bool isListType(Type type) =>
            type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>)
            || type.GetGenericTypeDefinition() == typeof(IList<>));

        public string ConvertToDatabaseType(PropertyInfo property)
        {
            if (Resolve(property) == DataType.Primitive)
            {
                if (property.PropertyType == typeof(int)) return DatabaseTypes.Int.ToString().ToLower();
                if (property.PropertyType == typeof(string)) return DatabaseTypes.Varchar.ToString().ToLower();
                if (property.PropertyType == typeof(float)) return DatabaseTypes.Decimal.ToString().ToLower();
                if (property.PropertyType == typeof(double)) return DatabaseTypes.Decimal.ToString().ToLower();
                if (property.PropertyType == typeof(Guid)) return DatabaseTypes.Uniqueidentifier.ToString().ToLower();
                if (property.PropertyType == typeof(DateTime)) return DatabaseTypes.Datetime.ToString().ToLower();
            }

            return string.Empty;
        }

        public string ConvertToDatabaseType(FieldInfo property)
        {
            if (Resolve(property) == DataType.Primitive)
            {
                if (property.FieldType == typeof(int)) return "int";
                if (property.FieldType == typeof(string)) return "varchar(MAX)";
                if (property.FieldType == typeof(float)) return "decimal";
                if (property.FieldType == typeof(double)) return "decimal";
            }
            return string.Empty;
        }

        public DataType Resolve(FieldInfo field)
        {
            if (IsPrimitive(field.FieldType))
                return DataType.Primitive;

            return DataType.Object;
        }

        public DataType Resolve(PropertyInfo property)
        {
            if (IsPrimitive(property.PropertyType))
                return DataType.Primitive;

            return DataType.Object;

        }

        private bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive)
                return true;

            string propertyName = type.Name;
            if (propertyName.Equals(PrimitiveType.String.ToString()) ||
                propertyName.Equals(PrimitiveType.Guid.ToString()) ||
                propertyName.Equals(PrimitiveType.DateTime.ToString()))
                return true;

            return false;
        }

        public bool IsListOfObject(PropertyInfo property)
        {
            if (!isEnumerableType(property.PropertyType)) return false;
            if (!isListType(property.PropertyType)) return false;

            if (property.PropertyType.IsGenericType)
            {
                return !IsPrimitive(property.PropertyType.GetGenericArguments()[0]);
            }

            return true;
        }

        public bool IsListOfObject(FieldInfo field)
        {
            if (!isEnumerableType(field.FieldType)) return false;
            if (!isListType(field.FieldType)) return false;

            if (field.FieldType.IsGenericType)
            {
                return !IsPrimitive(field.FieldType.GetGenericArguments()[0]);
            }

            return true;
        }
    }
}
