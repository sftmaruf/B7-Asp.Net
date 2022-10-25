using System.Data.SqlClient;

namespace Assignment4.Database
{
    public interface IDBManager
    {
        public string ConnectionString { get; }
        Task CreateDatabaseIfNotExist();
        Task<bool> IsDatabaseExistAsync();
        Task ExecuteNonQueryAsync(SqlCommand command);
        Task<object?> ExecuteScalarAsync(SqlCommand command);
    }
}