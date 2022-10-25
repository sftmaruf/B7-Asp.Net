using System.Reflection;
using Assignment4.Enum;

namespace Assignment4
{
    public interface ITypeResolver
    {
        DataType Resolve(FieldInfo field);
        DataType Resolve(PropertyInfo field);
        bool isEnumerableType(Type type);
        bool isListType(Type type);
        string ConvertToDatabaseType(FieldInfo property);
        string ConvertToDatabaseType(PropertyInfo property);
        bool IsListOfObject(PropertyInfo property);
        bool IsListOfObject(FieldInfo field);
    }
}