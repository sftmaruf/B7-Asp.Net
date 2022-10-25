using System.Reflection;

namespace Assignment4.Database
{
    public interface ITableManager
    {
        string GetPrimaryKeyName { get; }
        Task CreateTable(Type type, string tableName);
        Task CreateTableWithForeignKey(Type type, Type foreignTable, string tableName, string foreignTableName);
        Task DeleteData(string tableName, string columnName, object value);
        Task<List<object>> GetAllId(string columnName, string tableName);
        Task<IList<Dictionary<string, object>>> GetDataById(object id, string columnName, string tableName);
        Task<object> GetId(string tableName, string idColumnName, string foreignColumnName, object foreignKey);
        Task<List<object>> GetIdE(string tableName, string idColumnName, string foreignColumnName, object foreignKey);
        object GetPrimaryKeyValue(object obj);
        Type? GetTableType(Type propertyType);
        bool HasPrimaryKey(Type type);
        Task InsertData(string tableName, object obj);
        Task InsertDataInForeignTable(string tableName, object obj, (string Name, object Value) primaryKeyNameValue);
        Task<bool> IsIdExist(object id, string tableName);
        Task<bool> IsTableExist(string? tableName);
        string ResolveAndPluralizeTableName(FieldInfo field);
        string ResolveAndPluralizeTableName(PropertyInfo property);
        string ResolveAndPluralizeTableName(string tableName);
        string ResolveAndPluralizeTableName(Type type);
        Task UpdateData(string tableName, object obj);
        Task UpdateDataInForeignTable(string tableName, object obj, (string Name, object Value) primaryKeyNameValue);
    }
}