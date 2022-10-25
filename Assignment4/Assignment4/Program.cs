using Assignment4;
using Assignment4.Database;
using Assignment4.Migration;
using Assignment4.Orm;
using Assignment4.TestObject;
using System.Reflection;
using System.Text.Json;

// To create database automatically this code block needed.
// The user must have the permission to create a database to create database automatically with the code.
string connectionString = "Server=.\\SQLEXPRESS;Database=Assignment4;User Id=aspnetb7;Password=123456;";
IDBManager dbManager = new DBManager(connectionString, new SQLGenerator(connectionString));
await dbManager.CreateDatabaseIfNotExist();

var typeResolver = new TypeResolver();

var migration = new Migration(new Decoder(typeResolver, dbManager));
await migration.Run(Assembly.GetExecutingAssembly()
    .GetType("Assignment4.TestObject.Course")!);

var parser = new Parser(typeResolver,
        dbManager,
        new TableManager(new SQLGenerator(dbManager.ConnectionString),
        typeResolver));

var orm = new MyORM<int, Course>(dbManager, parser);
await orm.Insert(TestObject.GetCourse());
//await orm.Update(TestObject.GetCourse());
//var result = await orm.GetById(3);
//showOutput(result);
//await orm.Delete(TestObject.GetCar());
//await orm.Delete(1);
var results = await orm.GetAll();
foreach (var result in results)
{
    showOutput(result);
}

void showOutput(object obj)
{
    Console.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions
    {
        WriteIndented = true
    }));
}