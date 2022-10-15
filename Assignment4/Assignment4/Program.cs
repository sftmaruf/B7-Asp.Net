using Assignment4;
using Assignment4.TestObject;

// To create database automatically this code block needed.
string connectionString = "Server=.\\SQLEXPRESS;Database=Assignment4;User Id=aspnetb7;Password=123456;";
IDBManager dbManager = new DBManager(connectionString);
await dbManager.CheckAndCreateDatabase();

var orm = new MyORM<Guid, Course>();
orm.Insert(new Course());
