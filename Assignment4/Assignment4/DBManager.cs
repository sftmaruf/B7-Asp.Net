using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Assignment4
{
    public class DBManager : IDBManager
    {
        public static string? ConnectionString;
       
        public DBManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task CheckAndCreateDatabase()
        {
            var isExist = await isDatabaseExistAsync();
            if (!isExist)
            {
                Console.Write("Please enter a database name: ");
                await CreateDatabaseAsync(Console.ReadLine());
            }
        }

        private SqlCommand CreateCommand(string query, IDictionary<string, object>? parameters)
        {
            return CreateCommand(ConnectionString, query, parameters);
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

        public async Task CreateDatabaseAsync(string? name)
        {
            if (name == null || name == string.Empty)
            {
                name = "Assignment4";
                Console.WriteLine("You haven't provide any name. " +
                    "Default name of the Database will be Assignment4");
            }

            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
            connectionBuilder.InitialCatalog = "master";

            string query = "exec ('CREATE DATABASE ' + @databaseName)";
            var parameters = new Dictionary<string, object>()
            {
                { "databaseName", name }
            };

            var command = CreateCommand(connectionBuilder.ConnectionString, query, parameters);
            await ExecuteNonQueryAsync(command);
        }

        public async Task<bool> isDatabaseExistAsync()
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string name = connectionBuilder.InitialCatalog;
            connectionBuilder.InitialCatalog = "master";

            string query = $"SELECT db_id('{name}')";
            using var command = CreateCommand(connectionBuilder.ConnectionString, query, null);

            var impact = await ExecuteScalarAsync(command);
            if (impact != DBNull.Value) return true;

            return false;
        }

        private async Task ExecuteNonQueryAsync(SqlCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                command.Dispose();
            }
        }

        private async Task<object?> ExecuteScalarAsync(SqlCommand command)
        {
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                command.Dispose();
            }

            return null;
        }
    }
}
