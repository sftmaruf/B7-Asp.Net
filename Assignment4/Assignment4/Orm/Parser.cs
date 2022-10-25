using Assignment4.Database;
using Assignment4.Enum;
using Assignment4.Exceptions;
using Mono.Reflection;
using System.Collections;
using System.Reflection;

namespace Assignment4.Orm
{
    public class Parser : IParser
    {
        public IDBManager _dbManager { get; }

        private readonly ITypeResolver _typeResolver;
        private readonly ITableManager _tableManager;

        public Parser(ITypeResolver resolver, IDBManager dbManager, ITableManager tableManager)
        {
            _typeResolver = resolver;
            _dbManager = dbManager;
            _tableManager = tableManager;
        }

        public async Task ParseAndInsert(object obj)
        {
            if (obj == null) return;
            try
            {
                string tableName = _tableManager.ResolveAndPluralizeTableName(obj.GetType());
                bool isTableExist = await _tableManager.IsTableExist(tableName);
                if (!isTableExist) throw new TableNotFoundException();

                var id = _tableManager.GetPrimaryKeyValue(obj);
                var isIdExist = await _tableManager.IsIdExist(id, tableName);
                if (isIdExist)
                {
                    Console.WriteLine($"Id = {id} already exists in the {tableName} table.");
                    return;
                }

                await _tableManager.InsertData(tableName, obj);

                var primaryKeyName = tableName + _tableManager.GetPrimaryKeyName;
                await ParseAndInsertAgain(obj, primaryKeyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ParseAndUpdate(object obj)
        {
            if (obj == null) return;
            try
            {
                string tableName = _tableManager.ResolveAndPluralizeTableName(obj.GetType());
                bool isTableExist = await _tableManager.IsTableExist(tableName);
                if (!isTableExist) throw new TableNotFoundException();
                await _tableManager.UpdateData(tableName, obj);

                var primaryKeyName = tableName + _tableManager.GetPrimaryKeyName;
                await ParseAndUpdateAgain(obj, primaryKeyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ParseAndDelete(object obj)
        {
            var primaryKeyValue = _tableManager.GetPrimaryKeyValue(obj);
            await ParseAndDeleteById(primaryKeyValue, obj.GetType());
        }

        public async Task ParseAndDeleteById(object id, Type type)
        {
            string tableName = _tableManager.ResolveAndPluralizeTableName(type);

            var isIdExist = await _tableManager.IsIdExist(id, tableName);
            if (!isIdExist)
            {
                Console.WriteLine($"Invalid Id. {tableName} table doesn't have id = {id}.");
                return;
            }

            await ParseAndDeleteAgain(type, tableName, _tableManager.GetPrimaryKeyName,
                id, _tableManager.ResolveAndPluralizeTableName(type.Name), string.Empty);
        }

        public async Task<dynamic> ParseAndGetById<G>(G? id, Type type)
        {
            var tableName = _tableManager.ResolveAndPluralizeTableName(type);

            var isIdExist = await _tableManager.IsIdExist(id, tableName);
            if (!isIdExist)
            {
                Console.WriteLine($"Invalid Id. {tableName} table doesn't have id = {id}.");
                return null;
            }

            var obj = Activator.CreateInstance(type);
            var rows = await _tableManager.GetDataById(id, _tableManager.GetPrimaryKeyName, tableName);

            foreach (var row in rows)
            {
                InsertDataFromRow(obj, row);
            }

            var foreignColumnName = tableName + _tableManager.GetPrimaryKeyName;
            await GetDataByIdAgain(id, obj, foreignColumnName);

            return obj;
        }

        public async Task<IList> ParseAndGetAll(Type type)
        {
            var tableName = _tableManager.ResolveAndPluralizeTableName(type);
            var listOfId = await _tableManager.GetAllId(_tableManager.GetPrimaryKeyName, tableName);
            var listType = typeof(List<>).MakeGenericType(type);
            var listOfObject = (IList)Activator.CreateInstance(listType)!;
            foreach (var id in listOfId)
            {
                var obj = await ParseAndGetById(id, type);
                listOfObject.Add(obj);
            }

            return listOfObject;
        }

        private async Task ParseAndInsertAgain(object obj, string primaryKeyName)
        {
            var properties = obj.GetType().GetProperties();
            var fields = obj.GetType().GetFields();

            foreach (var property in properties)
            {
                var dataType = _typeResolver.Resolve(property);

                if (dataType.Equals(DataType.Object))
                {
                    if (_typeResolver.IsListOfObject(property))
                    {
                        await InsertListOfData(obj, primaryKeyName, property);
                    }
                    else
                    {
                        await InsertData(obj, primaryKeyName, property);
                    }
                }
            }

            foreach (var field in fields)
            {
                var dataType = _typeResolver.Resolve(field);

                if (dataType.Equals(DataType.Object))
                {
                    if (_typeResolver.IsListOfObject(field))
                    {
                        await InsertListOfData(obj, primaryKeyName, field);
                    }
                    else
                    {
                        await InsertData(obj, primaryKeyName, field);
                    }
                }
            }
        }

        private async Task InsertData(object obj, string primaryKeyName, dynamic member)
        {
            try
            {
                var tableName = obj.GetType().Name + _tableManager.ResolveAndPluralizeTableName(member);
                bool isTableExist = await _tableManager.IsTableExist(tableName);
                if (!isTableExist) throw new TableNotFoundException();

                var primaryKeyValue = _tableManager.GetPrimaryKeyValue(obj);

                await _tableManager.InsertDataInForeignTable(tableName,
                    member.GetValue(obj), (Name: primaryKeyName, Value: primaryKeyValue));

                var keyName = tableName + _tableManager.GetPrimaryKeyName;
                await ParseAndInsertAgain(member.GetValue(obj), keyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task InsertListOfData(object obj, string primaryKeyName, dynamic member)
        {
            var tableName = obj.GetType().Name + _tableManager.ResolveAndPluralizeTableName(member);
            bool isTableExist = await _tableManager.IsTableExist(tableName);
            if (!isTableExist) throw new TableNotFoundException();

            var primaryKeyValue = _tableManager.GetPrimaryKeyValue(obj);

            var items = member.GetValue(obj) as IEnumerable;
            var keyName = tableName + _tableManager.GetPrimaryKeyName;
            foreach (var item in items)
            {
                try
                {
                    await _tableManager.InsertDataInForeignTable(tableName,
                        item, (Name: primaryKeyName, Value: primaryKeyValue));

                    await ParseAndInsertAgain(item, keyName);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private async Task ParseAndDeleteAgain(Type type, string tableName, string columnName,
            object value, string name, string foreignColumnName)
        {
            var id = value;
            if (foreignColumnName != string.Empty && _tableManager.HasPrimaryKey(type))
            {
                var ids = await _tableManager.GetIdE(tableName,
                    _tableManager.GetPrimaryKeyName, foreignColumnName, value);

                foreach (var i in ids)
                {
                    await ParseAndDeleteFields(type, name + _tableManager.GetPrimaryKeyName, i);
                    await ParseAndDeleteProperties(type, name + _tableManager.GetPrimaryKeyName, i);
                }
            }
            else
            {
                await ParseAndDeleteFields(type, name + _tableManager.GetPrimaryKeyName, id);
                await ParseAndDeleteProperties(type, name + _tableManager.GetPrimaryKeyName, id);
            }

            await _tableManager.DeleteData(tableName, columnName, value);
        }

        private async Task ParseAndDeleteProperties(Type type, string foreignColumnName, object value)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var dataType = _typeResolver.Resolve(property);
                if (dataType.Equals(DataType.Object))
                {
                    var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(property);
                    var propertyType = _tableManager.GetTableType(property.PropertyType);
                    await ParseAndDeleteAgain(propertyType, tableName, foreignColumnName, value,
                        tableName, foreignColumnName);
                }
            }
        }

        private async Task ParseAndDeleteFields(Type type, string foreignColumnName, object value)
        {
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var dataType = _typeResolver.Resolve(field);
                if (dataType.Equals(DataType.Object))
                {
                    var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(field);
                    var propertyType = _tableManager.GetTableType(field.FieldType);
                    await ParseAndDeleteAgain(propertyType, tableName, foreignColumnName, value,
                        tableName, foreignColumnName);
                }
            }
        }

        private async Task ParseAndUpdateAgain(object obj, string primaryKeyName)
        {
            var properties = obj.GetType().GetProperties();
            var fields = obj.GetType().GetFields();

            foreach (var property in properties)
            {
                var dataType = _typeResolver.Resolve(property);

                if (dataType.Equals(DataType.Object))
                {
                    if (_typeResolver.IsListOfObject(property))
                    {
                        await UpdateListOfData(obj, primaryKeyName, property);
                    }
                    else
                    {
                        await UpdateData(obj, primaryKeyName, property);
                    }
                }
            }

            foreach (var field in fields)
            {
                var dataType = _typeResolver.Resolve(field);

                if (dataType.Equals(DataType.Object))
                {
                    if (_typeResolver.IsListOfObject(field))
                    {
                        await UpdateListOfData(obj, primaryKeyName, field);
                    }
                    else
                    {
                        await UpdateData(obj, primaryKeyName, field);
                    }
                }
            }
        }

        private async Task UpdateData(object obj, string primaryKeyName, dynamic member)
        {
            try
            {
                var tableName = obj.GetType().Name + _tableManager.ResolveAndPluralizeTableName(member);
                bool isTableExist = await _tableManager.IsTableExist(tableName);
                if (!isTableExist) throw new TableNotFoundException();

                var primaryKeyValue = _tableManager.GetPrimaryKeyValue(obj);

                await _tableManager.UpdateDataInForeignTable(tableName,
                    member.GetValue(obj), (Name: primaryKeyName, Value: primaryKeyValue));

                var keyName = tableName + _tableManager.GetPrimaryKeyName;
                await ParseAndUpdateAgain(member.GetValue(obj), keyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task UpdateListOfData(object obj, string primaryKeyName, dynamic member)
        {
            var tableName = obj.GetType().Name + _tableManager.ResolveAndPluralizeTableName(member);
            bool isTableExist = await _tableManager.IsTableExist(tableName);
            if (!isTableExist) throw new TableNotFoundException();

            var primaryKeyValue = _tableManager.GetPrimaryKeyValue(obj);

            var items = member.GetValue(obj) as IEnumerable;
            var keyName = tableName + _tableManager.GetPrimaryKeyName;
            foreach (var item in items)
            {
                try
                {
                    await _tableManager.UpdateDataInForeignTable(tableName,
                        item, (Name: primaryKeyName, Value: primaryKeyValue));

                    await ParseAndUpdateAgain(item, keyName);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void InsertDataFromRow(object? obj, Dictionary<string, object> row)
        {
            foreach (var column in row)
            {
                var property = obj.GetType().GetProperty(column.Key);
                var fields = obj.GetType().GetField(column.Key);

                if (property != null)
                {
                    if (!property.CanWrite)
                    {
                        property.GetBackingField().SetValue(obj,
                            Convert.ChangeType(column.Value, property.PropertyType));
                    }
                    else
                    {
                        property.SetValue(obj,
                            Convert.ChangeType(column.Value, property.PropertyType));
                    }
                }

                if (fields != null)
                {
                    fields.SetValue(obj,
                        Convert.ChangeType(column.Value, fields.FieldType));
                }
            }
        }

        private async Task GetDataByIdAgain(object id, object obj, string foreignColumnName)
        {
            var type = obj.GetType();
            await GetDataByIdFromProperties(type, id, obj, foreignColumnName);
            await GetDataByIdFromFields(type, id, obj, foreignColumnName);
        }

        private async Task GetDataByIdFromProperties(Type type, object id, object obj, string foreignColumnName)
        {
            foreach (var property in type.GetProperties())
            {
                var dataType = _typeResolver.Resolve(property);

                if (dataType.Equals(DataType.Object))
                {
                    var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(property);
                    var rows = await _tableManager.GetDataById(id, foreignColumnName, tableName);

                    if (_typeResolver.IsListOfObject(property))
                    {
                        var genericType = property.PropertyType.GetGenericArguments()[0];
                        var listType = typeof(List<>).MakeGenericType(genericType);
                        var listOfObject = (IList)Activator.CreateInstance(listType)!;

                        foreach (var row in rows)
                        {
                            var tempObj = Activator.CreateInstance(genericType);
                            InsertDataFromRow(tempObj, row);
                            (object tempId, string columnName) IdColumn = await RefreshIdAndColumnName(tempObj, id, tableName, foreignColumnName, property);
                            await GetDataByIdAgain(IdColumn.tempId, tempObj, IdColumn.columnName);

                            listOfObject.Add(tempObj);
                        }

                        InsertDataFromRow(obj, new Dictionary<string, object>()
                        {
                            { property.Name, listOfObject }
                        });
                    }
                    else
                    {
                        var tempObj = Activator.CreateInstance(property.PropertyType);
                        foreach (var row in rows) InsertDataFromRow(tempObj, row);

                        InsertDataFromRow(obj, new Dictionary<string, object>()
                        {
                            { property.Name, tempObj }
                        });

                        (object tempId, string columnName) IdColumn = await RefreshIdAndColumnName(tempObj, id, tableName, foreignColumnName, property);
                        await GetDataByIdAgain(IdColumn.tempId, tempObj, IdColumn.columnName);
                    }
                }
            }
        }

        private async Task GetDataByIdFromFields(Type type, object id, object obj, string foreignColumnName)
        {
            foreach (var field in type.GetFields())
            {
                var dataType = _typeResolver.Resolve(field);

                if (dataType.Equals(DataType.Object))
                {
                    var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(field);
                    var rows = await _tableManager.GetDataById(id, foreignColumnName, tableName);

                    if (_typeResolver.IsListOfObject(field))
                    {
                        var genericType = field.FieldType.GetGenericArguments()[0];
                        var listType = typeof(List<>).MakeGenericType(genericType);
                        var listOfObject = (IList)Activator.CreateInstance(listType)!;

                        foreach (var row in rows)
                        {
                            var tempObj = Activator.CreateInstance(genericType);
                            InsertDataFromRow(tempObj, row);
                            (object tempId, string columnName) IdColumn = await RefreshIdAndColumnName(tempObj, _tableManager.GetPrimaryKeyValue(tempObj), tableName, _tableManager.GetPrimaryKeyName, field);
                            await GetDataByIdAgain(IdColumn.tempId, tempObj, IdColumn.columnName);

                            listOfObject.Add(tempObj);
                        }

                        InsertDataFromRow(obj, new Dictionary<string, object>()
                        {
                            { field.Name, listOfObject }
                        });
                    }
                    else
                    {
                        var tempObj = Activator.CreateInstance(field.FieldType);
                        foreach (var row in rows) InsertDataFromRow(tempObj, row);

                        InsertDataFromRow(obj, new Dictionary<string, object>()
                        {
                            { field.Name, tempObj }
                        });

                        (object tempId, string columnName) IdColumn = await RefreshIdAndColumnName(tempObj, id, tableName, foreignColumnName, field);
                        await GetDataByIdAgain(IdColumn.tempId, tempObj, IdColumn.columnName);
                    }
                }
            }
        }

        private async Task<(object tempId, string columnName)> RefreshIdAndColumnName(object obj, object id, string tableName, string foreignColumnName, MemberInfo member)
        {
            var tempId = id;
            if (_tableManager.HasPrimaryKey(obj.GetType()))
            {
                tempId = await _tableManager.GetId(tableName, _tableManager.GetPrimaryKeyName, foreignColumnName, tempId);
            }

            string columnName = string.Empty;
            if (member.MemberType == MemberTypes.Field)
            {
                columnName = tableName + _tableManager.GetPrimaryKeyName;
                return (tempId, columnName);
            }

            columnName = tableName + _tableManager.GetPrimaryKeyName;
            return (tempId, columnName);
        }

        
    }
}
