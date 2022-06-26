using System.Reflection;
using System.Text;

namespace Assignment1
{
    public interface IEnumerableType
    {
        public void ConvertToJson(FieldInfo field, object? obj, ref StringBuilder _json);
        public void ConvertToJson(PropertyInfo property, object? obj, ref StringBuilder _json);
    }
}
