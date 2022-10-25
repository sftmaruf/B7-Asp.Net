using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Database;

namespace Assignment4.Migration
{
    public interface IDecoder
    {
        public IDBManager _dbManager { get; }
        Task Decode(Type type);
    }
}
