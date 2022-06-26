using System.Text;

namespace Assignment1
{
    public class ObjectToJsonConverter : IObjectToJsonConverter
    {
        private ICodeIndentor _indentor;
        private StringBuilder _json;
        private IJsonConvertible _field;
        private IJsonConvertible _property;
        
        public ObjectToJsonConverter(ICodeIndentor indentMechanism)
        {
            _indentor = indentMechanism;
            _json = new StringBuilder().Append('{').Append('\n');
            _field = new Field(this, new EnumerableType(this));
            _property = new Property(this, new EnumerableType(this));
        }

        public void Convert(object? obj)
        {
            if (obj != null)
            {
                _field.ConvertToJson(obj, ref _json);
                _property.ConvertToJson(obj, ref _json);
            }
        }

        private void IndentJson()
        {
             _json.Append('}');
            _json = _indentor.Indent(_json);
        }

        public string? GetJson()
        {
            IndentJson();
            return _json?.ToString();
        }

        public void Print()
        {
            Console.WriteLine(GetJson());
        }
    }
}
