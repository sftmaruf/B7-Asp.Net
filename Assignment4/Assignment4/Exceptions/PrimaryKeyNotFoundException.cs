using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Exceptions
{
    public class PrimaryKeyNotFoundException: Exception
    {
        public PrimaryKeyNotFoundException() : this("Primary doesn't exist in the provided class")
        { }

        public PrimaryKeyNotFoundException(string message) : base(message)
        { }
    }
}
