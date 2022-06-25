using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public interface IJsonConvertible
    {
        public void ConvertToJson(object obj, ref StringBuilder _tempString, ref bool isObjectInsideArray, ref string? objectName);
    }
}
