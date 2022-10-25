using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Exceptions
{
    public class TableNotFoundException: Exception
    {
        public TableNotFoundException() : this("Table doesn't exist.")
        { }

        public TableNotFoundException(string message) : base(message)
        { }
    }
}
