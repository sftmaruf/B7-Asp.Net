using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public  interface IObjectToJsonConverter
    {
        public void Convert(object? obj);
        public string? GetJson();
        public void Print();
    }
}
