namespace Assignment4
{
    public interface IDBManager
    {
        Task CreateDatabaseAsync(string? name);
        Task<bool> isDatabaseExistAsync();
        Task CheckAndCreateDatabase();
    }
}