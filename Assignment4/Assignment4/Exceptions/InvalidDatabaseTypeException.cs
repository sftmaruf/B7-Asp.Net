using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Exceptions
{
    public class InvalidDatabaseTypeException: Exception
    {
        public InvalidDatabaseTypeException() : this("The provided column type is invalid.")
        { }

        public InvalidDatabaseTypeException(string message) : base(message)
        { }
    }
}
