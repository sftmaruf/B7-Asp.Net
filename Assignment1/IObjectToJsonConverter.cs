using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public  interface IObjectToJsonConverter
    {
        public string ConvertToJson(object obj);
        public void Print();
    }
}
