using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class TestObject
    {
        public string property1;
        public string property2;
        public int property3;
        public double property4;
        public long property5;
        public IList<string> strs = new List<string>() { "sft", "maruf", null };
        public IDictionary<string, int> data = new Dictionary<string, int>() { { "sft", 1 }, { "Maruf", 20 } };
        public TestChildObject t;
        public TestChildObject tProp { get; set; }
        public DateTime date = DateTime.Now;

        public void TestMethod()
        {
            Console.WriteLine("This is a test method");
        }
    }
}
