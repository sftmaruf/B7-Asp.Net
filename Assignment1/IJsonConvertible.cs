using System.Text;

namespace Assignment1
{
    public interface IJsonConvertible
    {
        public void ConvertToJson(object obj, ref StringBuilder _json);
    }
}
