using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public interface IEnumerableType
    {
        public void ConvertToJson(FieldInfo field, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray);
        public void ConvertToJson(PropertyInfo property, object? obj, ref StringBuilder _tempString, ref bool isObjectInsideArray);
    }
}
