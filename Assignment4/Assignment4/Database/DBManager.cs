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

namespace Assignment4.Database
{
    public class DBManager : IDBManager
    {
        public string ConnectionString { get; }
        private ISQLGenerator _sqlGenerator;

        public DBManager(string connectionString, ISQLGenerator sqlGenerator)
        {
            ConnectionString = connectionString;
            _sqlGenerator = sqlGenerator;
        }

        public async Task CreateDatabaseIfNotExist()
        {
            var isExist = await IsDatabaseExistAsync();
            if (!isExist)
            {
                Console.Write("Please enter a database name: ");
                await CreateDatabaseAsync(Console.ReadLine());
            }
        }

        public async Task<bool> IsDatabaseExistAsync()
        {
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
            string name = connectionBuilder.InitialCatalog;
            connectionBuilder.InitialCatalog = "master";

            using var command = _sqlGenerator.GetIsDatabaseExistCommand(connectionBuilder.ConnectionString, name);

            var impact = await ExecuteScalarAsync(command);
            if (impact != DBNull.Value) return true;

            return false;
        }

        private async Task CreateDatabaseAsync(string? name)
        {
            if (name == null || name == string.Empty)
            {
                name = "Assignment4";
                Console.WriteLine("You haven't provide any name. " +
                    "Default name of the Database will be Assignment4");
            }

            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
            connectionBuilder.InitialCatalog = "master";

            using var command = _sqlGenerator.GetCreateDatabaseCommand(connectionBuilder.ConnectionString, name);
            await ExecuteNonQueryAsync(command);
        }


        public async Task ExecuteNonQueryAsync(SqlCommand command)
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

        public async Task<object?> ExecuteScalarAsync(SqlCommand command)
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
