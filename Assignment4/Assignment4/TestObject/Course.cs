using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.TestObject
{
    public class Course : IEntity
    {
        public Guid Id { get; }
        public string? Name { get; set; }
        public Instructor Teacher { get; set; }
    }

    public class Instructor
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
