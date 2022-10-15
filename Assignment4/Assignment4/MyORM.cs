using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class MyORM<G, T> : IMyORM<G, T>
        where T : class, IEntity
    {
        private static string defaultConnectionString = "Server=.\\SQLEXPRESS;Database=Assignment4;User Id=aspnetb7;Password=123456;";
        
        private IDBManager _dbManager;
        private IDecoder decoder;

        public MyORM(IDBManager dbManager)
        {
            _dbManager = dbManager;
            decoder = new Decoder();
        }

        public MyORM(string connectionString) : this(new DBManager(connectionString))
        {
            
        }

        public MyORM() : this(defaultConnectionString)
        {

        }

        public void Insert(T item) 
        {
            decoder.Decode(item);
        }

        public void Update(T item) 
        { 
        
        }

        public void Delete(T item) { }
        public void Delete(G id) { }
        public void GetById(G id) { }
        public void GetAll(G id) { }
    }
}
