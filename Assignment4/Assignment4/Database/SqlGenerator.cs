using System.Data.SqlClient;
using System.Text;

namespace Assignment4.Database
{
    public class SQLGenerator : ISQLGenerator
    {
        private string _connectionString;

        public SQLGenerator(string conenctionString)
        {
            _connectionString = conenctionString;
        }

        public SqlCommand GetIsDatabaseExistCommand(string conenctionString, string dbName)
        {
            string query = $"SELECT db_id('{dbName}')";
            using var command = CreateCommand(conenctionString, query, null);

            return command;
        }

        public SqlCommand GetCreateDatabaseCommand(string conenctionString, string dbName)
        {
            string query = "exec ('CREATE DATABASE ' + @databaseName)";
            var parameters = new Dictionary<string, object>()
            {
                { "databaseName", dbName }
            };

            return CreateCommand(conenctionString, query, parameters);
        }

        public SqlCommand GetIsTableExistCommand(string tableName)
        {
            string query = "If EXISTS (SELECT * FROM sys.tables WHERE name = @dbName) SELECT 1 ELSE SELECT 0;";
            var parameters = new Dictionary<string, object>()
            {
                { "dbName", tableName }
            };

            return CreateCommand(query, parameters);
        }

        public SqlCommand GetCreateTableCommand(string tableName, Dictionary<string, string> columnsInfo)
        {
            StringBuilder query = new StringBuilder("CREATE TABLE ");

            query.Append($"{tableName} (");
            foreach (var columnInfo in columnsInfo)
            {
                query.Append($"{columnInfo.Key} {columnInfo.Value}");
                query.Append(',');
            }
            query.Remove(query.Length - 1, 1);
            query.Append(");");

            return CreateCommand(query.ToString(), null);
        }

        public SqlCommand GetCreateTableWithForeignKeyCommand(string tableName,
            Dictionary<string, string> columnsInfo, (string TableName, string ColumnName, string Type) foreignTableInfo, string foreignColumnName)
        {
            StringBuilder query = new StringBuilder("CREATE TABLE ");

            query.Append($"{tableName} (");
            foreach (var columnInfo in columnsInfo)
            {
                query.Append($"{columnInfo.Key} {columnInfo.Value}");
                query.Append(',');
            }

            query.Append($"{foreignColumnName} {foreignTableInfo.Type} " +
                $"FOREIGN KEY REFERENCES {foreignTableInfo.TableName}({foreignTableInfo.ColumnName})");
            query.Append(");");

            return CreateCommand(query.ToString(), null);
        }

        public SqlCommand GetInsertCommand(string tableName, Dictionary<string, object> columnsNameValue)
        {
            StringBuilder query = new StringBuilder("insert into ");
            query.Append($"{tableName} (");

            foreach (var columnName in columnsNameValue.Keys)
            {
                query.Append(columnName);
                query.Append(",");
            }
            query.Remove(query.Length - 1, 1);
            query.Append(") ");
            query.Append("Values (");

            foreach (var columnName in columnsNameValue.Keys)
            {
                query.Append($"@{columnName}");
                query.Append(",");
            }
            query.Remove(query.Length - 1, 1);
            query.Append(");");

            var parameters = new Dictionary<string, object>();
            foreach (var column in columnsNameValue)
            {
                parameters.Add($"@{column.Key}", column.Value);
            }


            return CreateCommand(query.ToString(), parameters);
        }
        public SqlCommand GetUpdateCommand(string tableName, Dictionary<string, object> columnsNameValue, string primaryKeyColumnName, object primaryKey)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"UPDATE {tableName} SET ");

            foreach (var column in columnsNameValue)
            {
                query.Append($"{column.Key} = @{column.Key}");
                query.Append(",");
            }
            query.Remove(query.Length - 1, 1);
            query.Append($" WHERE {primaryKeyColumnName} = @primaryKeyValue");


            var parameters = new Dictionary<string, object>()
            {
                { "primaryKeyValue", primaryKey }
            };

            foreach (var column in columnsNameValue)
            {
                parameters.Add($"@{column.Key}", column.Value);
            }

            return CreateCommand(query.ToString(), parameters);
        }

        private SqlCommand CreateCommand(string query, IDictionary<string, object>? parameters)
        {
            return CreateCommand(_connectionString, query, parameters);
        }

        private SqlCommand CreateCommand(string connectionString, string query, IDictionary<string, object>? parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = query;

            if (parameters != null && parameters?.Count() != 0)
            {
                foreach (var parameter in parameters!)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            return command;
        }

        public SqlCommand GetDeleteCommand(string tableName, string columnName, object value)
        {
            var query = $"DELETE FROM {tableName} WHERE {columnName} = @value";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "value", value }
            };

            return CreateCommand(query, parameters);
        }

        public SqlCommand GetIdCommand(string tableName, string idColumnName, string foreignColumnName, object foreignKey)
        {
            var query = $"SELECT {idColumnName} FROM {tableName} WHERE {foreignColumnName} = @foreignKey;";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "foreignKey", foreignKey }
            };

            return CreateCommand(query, parameters);
        }

        public SqlCommand GetDataById(object id, string columnName, string tableName)
        {
            var query = $"SELECT * FROM {tableName} WHERE {columnName} = @columnName";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "columnName", id }
            };

            return CreateCommand(query, parameters);
        }

        public SqlCommand GetAllId(string columnName, string tableName)
        {
            var query = $"SELECT {columnName} FROM {tableName}";
            return CreateCommand(query, null);
        }

        public SqlCommand GetIdExistCommand(object id, string tableName, string primaryColumnname)
        {
            var query = $"SELECT {primaryColumnname} FROM {tableName} WHERE {primaryColumnname} = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "id", id }
            };

            return CreateCommand(query, parameters);
        }
    }
}
