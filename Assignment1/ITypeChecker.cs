using System.Reflection;

namespace Assignment1
{
    public interface ITypeChecker
    {
        public bool isDictionaryType(FieldInfo field);
        public bool isDictionaryType(PropertyInfo property);
        public bool isEnumerableType(FieldInfo field);
        public bool isEnumerableType(PropertyInfo property);
        public  bool IsJsonableType(FieldInfo field);
        public  bool IsJsonableType(TypeInfo type);
        public bool IsJsonableType(PropertyInfo property);
        public bool isQuotableType(FieldInfo field);
        public bool isQuotableType(PropertyInfo property);
    }
}