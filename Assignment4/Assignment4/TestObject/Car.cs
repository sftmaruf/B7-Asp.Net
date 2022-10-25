using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.TestObject
{
    public class Car : IEntity<int>
    {
        public int Id { get; set; } 
        public Brand Brand;
        public string Model;
        public double Price;
        public List<Wheel> Wheels;
        public List<Door> Door;
    }

    public class Wheel
    {
        public int Id { get; set; }
        public Brand Brand;
    }

    public class Door : IEntity<int>
    {
        public int Id { get; set; }
        public Brand Brand;
    }

    public class Brand
    {
        public string Name;
        public string Country;
    }
}
