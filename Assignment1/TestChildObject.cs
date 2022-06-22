using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class TestChildObject
    {
        public string name;
        public int age;
        public ArrayList toys = new ArrayList() { "car", "Robot", 1 };
        public string[] numbers = new string[] { "1", "2", "3" };
        public Toy[] ttoys = new Toy[] { new Toy() { name = "car"}, new Toy() { name = "robot" } };
    }
}
