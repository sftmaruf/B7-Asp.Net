using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class StringFormatter
    {
        public static StringBuilder RemoveExtraCommas(StringBuilder _json)
        {
            int commaPosition = -1;
            for (int i = 0; i < _json.Length; i++)
            {
                if (_json[i] == ',') commaPosition = i;
                if (commaPosition != -1 && (_json[i] != ' ' && _json[i] != '}' && _json[i] != ']' && _json[i] != ',')) commaPosition = -1;
                if (commaPosition != -1 && (_json[i] == '}' || _json[i] == ']'))
                {
                    _json = _json.Remove(commaPosition, 1);
                    i--;
                }
            }
            return _json;
        }

        public static void FormatTempString(bool isObjectType, string? objectName, ref StringBuilder _tempString)
        {
            if (isObjectType)
            {
                _tempString.Insert(0, objectName + "{\n").Append("}\n");
            }
        }
    }
}
