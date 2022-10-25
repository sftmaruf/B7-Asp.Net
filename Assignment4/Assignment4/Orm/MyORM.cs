using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Database;

namespace Assignment4.Orm
{
    public class MyORM<G, T> : IMyORM<G, T>
        where T : class, IEntity<G>
    {
        private readonly IDBManager _dbManager;
        private readonly IParser _parser;

        public MyORM(IDBManager dbManager, IParser parser)
        {
            _dbManager = dbManager;
            _parser = parser;
        }

        public MyORM(IDBManager dbManager)
        {
            _dbManager = dbManager;

            var typeResolver = new TypeResolver();
            _parser = new Parser(typeResolver, 
                _dbManager, 
                new TableManager(new SQLGenerator(_dbManager.ConnectionString),
                typeResolver));
        }

        public MyORM(string connectionString) : this(new DBManager(connectionString, new SQLGenerator(connectionString)))
        {

        }

        public MyORM() : this("Server=.\\SQLEXPRESS;Database=Assignment4;User Id=aspnetb7;Password=123456;")
        {

        }

        public async Task Insert(T item)
        {
            await _parser.ParseAndInsert(item);
        }

        public async Task Update(T item)
        {
            await _parser.ParseAndUpdate(item);
        }

        public async Task Delete(T item)
        {
            await _parser.ParseAndDelete(item);
        }
        public async Task Delete(G id)
        {
            await _parser.ParseAndDeleteById(id, typeof(T));
        }

        public async Task<dynamic> GetById(G id)
        {
            return await _parser.ParseAndGetById(id, typeof(T));
        }

        public async Task<IList> GetAll()
        {
            return await _parser.ParseAndGetAll(typeof(T));
        }
    }
}
