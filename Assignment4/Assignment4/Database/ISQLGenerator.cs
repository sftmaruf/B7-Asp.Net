using System.Data.SqlClient;

namespace Assignment4.Database
{
    public interface ISQLGenerator
    {
        SqlCommand GetIsTableExistCommand(string tableName);
        SqlCommand GetIsDatabaseExistCommand(string connectionString, string tableName);
        SqlCommand GetCreateDatabaseCommand(string conenctionString, string dbName);
        SqlCommand GetCreateTableWithForeignKeyCommand(string tableName,
            Dictionary<string, string> columnsInfo, (string TableName, string ColumnName, string Type) foreignTableInfo, string foreignColumnName);
        SqlCommand GetCreateTableCommand(string tableName, Dictionary<string, string> columnsInfo);
        SqlCommand GetInsertCommand(string tableName, Dictionary<string, object> columnsNameValue);
        SqlCommand GetUpdateCommand(string tableName, Dictionary<string, object> columnsNameValue, string primaryKeyColumnName, object primaryKey);
        SqlCommand GetDeleteCommand(string tableName, string columnName, object value);
        SqlCommand GetIdCommand(string tableName, string idColumnName, string foreignColumnName, object foreignKey);
        SqlCommand GetDataById(object id, string primaryColumnName, string tableName);
        SqlCommand GetAllId(string columnName, string tableName);
        SqlCommand GetIdExistCommand(object id, string tableName, string primaryColumnname);
    }
}