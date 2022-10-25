using Assignment4.Enum;
using Assignment4.Exceptions;
using System.Data;
using System.Reflection;
using Mono.Reflection;

namespace Assignment4.Database
{
    public class TableManager : ITableManager
    {
        private ISQLGenerator _sqlGenerator;
        private ITypeResolver _typeResolver;

        public string GetPrimaryKeyName { get { return "Id"; } }

        public TableManager(ISQLGenerator sqlGenerator, ITypeResolver resolver)
        {
            _sqlGenerator = sqlGenerator;
            _typeResolver = resolver;
        }

        public async Task<bool> IsTableExist(string? tableName)
        {
            if (tableName == null || tableName.Equals(string.Empty))
                throw new InvalidTableNameException();

            using var command = _sqlGenerator.GetIsTableExistCommand(tableName);
            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();
            var result = await command.ExecuteScalarAsync();

            return (int?)result == 1 ? true : false;
        }

        public async Task CreateTableWithForeignKey(Type type, Type foreignTable, string tableName, string foreignTableName)
        {
            if (tableName == null || tableName.Equals(string.Empty))
                throw new InvalidTableNameException();

            var columnsInfo = ResolveColumnNameAndType(type);
            AddPrimaryKeyInfo(columnsInfo);
            SetLengthToDatabaseTypes(columnsInfo);
            var foreignTableInfo = ResolveForeignKeyInfo(foreignTable, foreignTableName);

            var foreignColumnName = foreignTableInfo.TableName + "Id";
            var command = _sqlGenerator.GetCreateTableWithForeignKeyCommand(tableName, columnsInfo, foreignTableInfo, foreignColumnName);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            await command.ExecuteNonQueryAsync();
        }

        public async Task CreateTable(Type type, string tableName)
        {
            if (tableName == null || tableName.Equals(string.Empty))
                throw new InvalidTableNameException();

            var columnsInfo = ResolveColumnNameAndType(type);
            AddPrimaryKeyInfo(columnsInfo);
            SetLengthToDatabaseTypes(columnsInfo);
            var command = _sqlGenerator.GetCreateTableCommand(tableName, columnsInfo);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            await command.ExecuteNonQueryAsync();
        }

        public async Task InsertData(string tableName, object obj)
        {
            try
            {
                if (hasPrimaryKey(obj)) SetPrimaryKeyValue(obj);
                var columnsNameValue = ResolveColumnNameAndValue(obj);
                var command = _sqlGenerator.GetInsertCommand(tableName, columnsNameValue);

                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InsertDataInForeignTable(string tableName, object obj, (string Name, object Value) primaryKeyNameValue)
        {
            try
            {
                if (hasPrimaryKey(obj)) SetPrimaryKeyValue(obj);
                var columnsNameValue = ResolveColumnNameAndValue(obj);
                columnsNameValue.Add(primaryKeyNameValue.Name, primaryKeyNameValue.Value);

                var command = _sqlGenerator.GetInsertCommand(tableName, columnsNameValue);
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateData(string tableName, object obj)
        {
            try
            {
                var columnsNameValue = ResolveColumnNameAndValue(obj);

                object primaryKey = null;

                if (columnsNameValue.ContainsKey(GetPrimaryKeyName))
                {
                    primaryKey = columnsNameValue[GetPrimaryKeyName];
                    columnsNameValue.Remove(GetPrimaryKeyName);
                }
                var command = _sqlGenerator.GetUpdateCommand(tableName, columnsNameValue, GetPrimaryKeyName, primaryKey);

                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateDataInForeignTable(string tableName, object obj, (string Name, object Value) primaryKeyNameValue)
        {
            try
            {
                var columnsNameValue = ResolveColumnNameAndValue(obj);

                if (columnsNameValue.ContainsKey(GetPrimaryKeyName))
                {
                    columnsNameValue.Remove(GetPrimaryKeyName);
                }

                if (columnsNameValue.Count == 0) return;

                var command = _sqlGenerator.GetUpdateCommand(tableName, columnsNameValue,
                    primaryKeyNameValue.Name, primaryKeyNameValue.Value);

                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<object> GetId(string tableName, string idColumnName, string foreignColumnName, object foreignKey)
        {
            var command = _sqlGenerator.GetIdCommand(tableName, idColumnName, foreignColumnName, foreignKey);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            return await command.ExecuteScalarAsync();
        }

        public async Task<List<object>> GetIdE(string tableName, string idColumnName, string foreignColumnName, object foreignKey)
        {
            var command = _sqlGenerator.GetIdCommand(tableName, idColumnName, foreignColumnName, foreignKey);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var reader = await command.ExecuteReaderAsync();
            var values = new List<object>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                }
            }
            return values;
        }

        public async Task DeleteData(string tableName, string columnName, object value)
        {
            var command = _sqlGenerator.GetDeleteCommand(tableName, columnName, value);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            await command.ExecuteNonQueryAsync();
        }

        private void SetLengthToDatabaseTypes(Dictionary<string, string> columnsInfo)
        {
            foreach (var columnInfo in columnsInfo)
            {
                if (columnInfo.Value == DatabaseTypes.Varchar.ToString().ToLower())
                {
                    columnsInfo[columnInfo.Key] = columnInfo.Value + "(MAX)";
                }

                if (columnInfo.Value == DatabaseTypes.Decimal.ToString().ToLower())
                {
                    columnsInfo[columnInfo.Key] = columnInfo.Value + "(20, 8)";
                }
            }
        }

        public object GetPrimaryKeyValue(object obj)
        {
            var type = obj.GetType();
            foreach (var field in type.GetFields())
            {
                if (field.Name == GetPrimaryKeyName)
                {
                    return field.GetValue(obj);
                }
            }

            foreach (var property in type.GetProperties())
            {
                if (property.Name == GetPrimaryKeyName)
                {
                    return property.GetValue(obj);
                }
            }

            return null;
        }

        private bool hasPrimaryKey(object obj)
        {
            return HasPrimaryKey(obj.GetType());
        }

        public bool HasPrimaryKey(Type type)
        {
            foreach (var field in type.GetFields())
            {
                if (field.Name == GetPrimaryKeyName) return true;
            }

            foreach (var property in type.GetProperties())
            {
                if (property.Name == GetPrimaryKeyName) return true;
            }

            return false;
        }

        private void SetPrimaryKeyValue(object obj)
        {
            var primaryKeyInfo = ResolvePrimaryKeyNameAndType(obj.GetType());
            var type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                if (field.Name == primaryKeyInfo.Name)
                {
                    if (field.FieldType == typeof(Guid))
                    {
                        if (field.GetValue(obj).ToString() == Guid.Empty.ToString())
                            field.SetValue(obj, Guid.NewGuid());
                        return;
                    }

                    if (field.GetValue(obj) == null ||
                        field.GetValue(obj).ToString() == string.Empty)
                    {
                        throw new ArgumentNullException("Id must have to contains data.");
                    }
                }
            }

            foreach (var property in type.GetProperties())
            {
                if (property.Name == primaryKeyInfo.Name)
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        if (property.GetValue(obj).ToString() == Guid.Empty.ToString())
                            property.GetBackingField().SetValue(obj, Guid.NewGuid());
                        return;
                    }

                    var value = property.GetValue(obj);
                    if (property.PropertyType == typeof(int))
                    {
                        if (value.ToString() == "0")
                        {
                            throw new ArgumentException("Id value needs to be greater than 0.");
                        }
                    }

                    if (value == null ||
                        value.ToString() == string.Empty)
                    {
                        throw new ArgumentNullException("Id must have to contains data.");
                    }
                }
            }
        }

        private void AddPrimaryKeyInfo(Dictionary<string, string> columnsInfo)
        {
            foreach (var columnInfo in columnsInfo)
            {
                if (columnInfo.Key == GetPrimaryKeyName)
                {
                    columnsInfo[columnInfo.Key] = columnInfo.Value + " Primary Key";
                    break;
                }
            }
        }

        private (string TableName, string ColumnName, string Type) ResolveForeignKeyInfo(Type foreignTable, string foreignTableName)
        {
            (string ColumnName, string Type) = ResolvePrimaryKeyNameAndType(foreignTable);
            return (foreignTableName, ColumnName, Type);
        }

        private Dictionary<string, object> ResolveColumnNameAndValue(object obj)
        {
            var type = obj.GetType();
            var fields = type.GetFields();
            var properties = type.GetProperties();

            var columnNameValues = new Dictionary<string, object>();

            foreach (var field in fields)
            {
                if (_typeResolver.Resolve(field) == DataType.Primitive)
                {
                    var value = field.GetValue(obj);
                    columnNameValues.Add(field.Name, value);
                }
            }

            foreach (var property in properties)
            {
                if (_typeResolver.Resolve(property) == DataType.Primitive)
                {
                    var value = property.GetValue(obj);
                    columnNameValues.Add(property.Name, value);
                }
            }

            return columnNameValues;
        }

        private Dictionary<string, string> ResolveColumnNameAndType(Type type)
        {
            var fields = type.GetFields();
            var properties = type.GetProperties();

            var columnInfos = new Dictionary<string, string>();

            foreach (var field in fields)
            {
                if (_typeResolver.Resolve(field) == DataType.Primitive)
                {
                    var columnType = _typeResolver.ConvertToDatabaseType(field);
                    if (columnType == string.Empty) throw new InvalidDatabaseTypeException();
                    columnInfos.Add(field.Name, columnType);
                }
            }

            foreach (var property in properties)
            {
                if (_typeResolver.Resolve(property) == DataType.Primitive)
                {
                    string columnType = _typeResolver.ConvertToDatabaseType(property);
                    if (columnType == string.Empty) throw new InvalidDatabaseTypeException();
                    columnInfos.Add(property.Name, columnType);
                }
            }

            return columnInfos;
        }

        private (string Name, string Type) ResolvePrimaryKeyNameAndType(Type type)
        {
            foreach (var field in type.GetFields())
            {
                if (field.Name == GetPrimaryKeyName) return (field.Name, Type: _typeResolver.ConvertToDatabaseType(field));
            }

            foreach (var property in type.GetProperties())
            {
                if (property.Name == GetPrimaryKeyName) return (property.Name, Type: _typeResolver.ConvertToDatabaseType(property));
            }

            throw new PrimaryKeyNotFoundException();
        }

        public Type? GetTableType(Type propertyType)
        {
            if (!_typeResolver.isEnumerableType(propertyType)) return propertyType;
            if (_typeResolver.isListType(propertyType)) return propertyType.GetGenericArguments()[0];

            throw new InvalidDatabaseTypeException($"The type {propertyType.Name} " +
                $"can't be stored in the database");
        }

        public string ResolveAndPluralizeTableName(Type type)
        {
            return ResolveAndPluralizeTableName(type.Name);
        }

        public string ResolveAndPluralizeTableName(PropertyInfo property)
        {
            var tableName = property.Name;
            if (_typeResolver.isEnumerableType(property.PropertyType)) return tableName;
            return ResolveAndPluralizeTableName(tableName);
        }

        public string ResolveAndPluralizeTableName(FieldInfo field)
        {
            var tableName = field.Name;
            if (_typeResolver.isEnumerableType(field.FieldType)) return tableName;
            return ResolveAndPluralizeTableName(tableName);
        }

        public string ResolveAndPluralizeTableName(string tableName)
        {
            if (tableName.Equals(string.Empty))
                throw new InvalidTableNameException("The resolved name is invalid.");

            tableName = tableName.EndsWith("s")
                ? string.Concat(tableName, "es")
                : string.Concat(tableName, "s");
            return tableName;
        }

        public async Task<IList<Dictionary<string, object>>> GetDataById(object id, string columnName, string tableName)
        {
            var command = _sqlGenerator.GetDataById(id, columnName, tableName);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var reader = await command.ExecuteReaderAsync();
            var values = new List<Dictionary<string, object>>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var nameValues = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        nameValues.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    values.Add(nameValues);
                }
            }
            return values;
        }

        public async Task<List<object>> GetAllId(string columnName, string tableName)
        {
            var command = _sqlGenerator.GetAllId(columnName, tableName);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var reader = await command.ExecuteReaderAsync();
            var values = new List<object>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                }
            }
            return values;
        }

        public async Task<bool> IsIdExist(object id, string tableName)
        {
            var command = _sqlGenerator.GetIdExistCommand(id, tableName, GetPrimaryKeyName);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var result = await command.ExecuteScalarAsync();

            if (result == null) return false;
            return true;
        }
    }
}
