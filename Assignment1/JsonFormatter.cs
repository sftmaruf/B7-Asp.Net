using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class JsonFormatter : IJsonFormatter
    {
        private static JsonFormatter _jsonFormatter = new JsonFormatter();
        CustomObjectType customObjectType = new CustomObjectType();

        private static StringBuilder _json = new StringBuilder();
        private static StringBuilder _tempString = new StringBuilder();
        private static bool isObjType = false;
        private static bool isObjectInsideArray = false;
        private static string? objectName;
        public static string Convert(object? obj)
        {
            _jsonFormatter.DismantleObject(obj);
            StringFormatter.FormatTempString(isObjType, objectName, ref _tempString);
            _json.Insert(0, _tempString.ToString());
            _tempString.Clear();
            isObjType = false;

            return "";
        }

        private void DismantleObject(object? obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                customObjectType.HandleCustomObjectType(type, obj, ref isObjType, ref objectName);
                handleFields(type.GetFields(), obj);
                handleProperties(type.GetProperties(BindingFlags.Instance | BindingFlags.Public), obj);
            }
        }

        //private void handleCustomObjectType(Type type, object? obj)
        //{
        //    FieldInfo[] fields = type.GetFields();
        //    PropertyInfo[] properties = type.GetProperties();
            
        //    foreach (var field in fields)
        //    {
        //        if (!Types.isJsonableType(field))
        //        {
        //            isObjType = true;
        //            objectName = field.Name + ": ";
        //            Convert(field.GetValue(obj));
        //        }
        //    }

        //    foreach (var property in properties)
        //    {
        //        if (!Types.isJsonableType(property))
        //        {
        //            isObjType = true;
        //            objectName = property.Name + ": ";
        //            Convert(property.GetValue(obj));
        //        }
        //    }
        //}

        private void handleFields(FieldInfo[] fields, object? obj)
        {
            foreach (var field in fields)
            {
                if(Types.isEnumerableType(field))
                {
                    HandleEnumerableType(field, obj);
                    continue;
                }

                if (Types.isJsonableType(field))
                {

                    _tempString.Append("\"").Append(field.Name).Append("\"").Append(": ");
                    if (field.FieldType == typeof(string)) _tempString.Append("\"");
                    _tempString.Append(field.GetValue(obj));
                    if (field.FieldType == typeof(string)) _tempString.Append("\"");
                    _tempString.Append(", ");

                    if (!isObjectInsideArray) _tempString.Append("\n");
                }
            }
        }

        // public object?[] getValue(object? obj)
        //{
        //    object? count = null;
        //    PropertyInfo? indexer = null;
        //    object?[] values = { };
 
        //    foreach(var item in obj.GetType().GetProperties())
        //    {
        //        if (item.Name == "Count") count = item.GetValue(obj);
        //        if (item.GetIndexParameters().Length > 0) indexer = item;
        //    }

        //    if(count != null && indexer != null)
        //    {
        //        values = new object?[(int)count];
        //        for (int i = 0; i < (int)count; i++)
        //        {
        //            values[i] = indexer?.GetValue(obj, new object[] { i });
        //        }
        //    }

        //    return values;
        //}

        private void HandleEnumerableType(FieldInfo field, object? obj)
        {
            dynamic? d = field.GetValue(obj);

            _tempString.Append("\"").Append(field.Name).Append("\"").Append(": [ ");
            foreach (var item in d)
            {
                if (!Types.isJsonableType(item.GetType()))
                {
                    isObjectInsideArray = true;
                    _tempString.Append("{");
                    DismantleObject(item);
                    _tempString.Append("}, ");
                    isObjectInsideArray = false;
                }
                else
                {
                    if (item.GetType() == typeof(string)) _tempString.Append("\"");
                    _tempString.Append(item.ToString());
                    if (item.GetType() == typeof(string)) _tempString.Append("\"");
                    _tempString.Append(", ");
                }
            }
            _tempString.Append("],\n");
        }
        private void handleProperties(PropertyInfo[] properties, object obj)
        {
            foreach (var property in properties)
            {
                if (Types.isEnumerableType(property))
                {
                    dynamic? d = property.GetValue(obj);

                    _tempString.Append(property.Name).Append(": [ ");
                    foreach (var item in d)
                    {
                        _tempString.Append(item.ToString() + ", ");
                    }
                    _tempString.Append("]\n");
                    return;
                }

                if (Types.isJsonableType(property))
                {
                    if (!(property.GetIndexParameters().Length > 0))
                    {
                        _tempString.Append("\"").Append(property.Name).Append("\"").Append(": ").Append(property.GetValue(obj)).Append("\n");
                    }
                }
            }
        }
        private void formatTempString()
        {
            if(isObjType)
            {
                _tempString.Insert(0, objectName + "{\n").Append("}\n");
            }
        }
        private void RemoveExtraCommas()
        {
            int commaPosition = -1;
            for (int i = 0; i < _json.Length; i++)
            {
                if (_json[i] == ',') commaPosition = i;
                if (commaPosition != -1 && (_json[i] != ' ' && _json[i] != '}' && _json[i] != ']' && _json[i] != ',')) commaPosition = -1;
                if(commaPosition != -1 && (_json[i] == '}' || _json[i] == ']'))
                {
                    _json = _json.Remove(commaPosition, 1);
                    i--;
                }
            }
        }
        public void Print()
        {
            _json = StringFormatter.RemoveExtraCommas(_json);
            Console.WriteLine(_json.ToString());
        }
    }
}
