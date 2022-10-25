using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Exceptions
{
    public class InvalidTableNameException: Exception
    {
        public InvalidTableNameException() : this("The provided table name doesn't follow the convention")
        { }

        public InvalidTableNameException(string message) : base(message)
        { }
    }
}
