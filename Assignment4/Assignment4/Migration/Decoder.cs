using Assignment4.Database;
using Assignment4.Enum;
using Assignment4.Exceptions;
using System.Reflection;

namespace Assignment4.Migration
{
    public class Decoder : IDecoder
    {
        public IDBManager _dbManager { get; }

        private ITypeResolver _typeResolver;
        private TableManager _tableManager;

        public Decoder(ITypeResolver resolver, IDBManager dbManager)
        {
            _typeResolver = resolver;
            _dbManager = dbManager;
            _tableManager = new TableManager(new SQLGenerator(_dbManager.ConnectionString), _typeResolver);
        }

        public async Task Decode(Type type)
        {
            if (type == null) return;
            try
            {
                string tableName = _tableManager.ResolveAndPluralizeTableName(type);
                bool isTableExist = await _tableManager.IsTableExist(tableName);
                if (!isTableExist) await _tableManager.CreateTable(type, tableName);
                await DecodeAgain(type, tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DecodeAgain(Type type, string foreignTableName)
        {
            await DecodeFields(type, foreignTableName);
            await DecodeProperties(type, foreignTableName);
        }

        private async Task DecodeFields(Type type, string foreignTableName)
        {
            var fields = type.GetFields();
            if (fields == null) return;

            foreach (var field in fields)
            {
                var dataType = _typeResolver.Resolve(field);

                if (dataType.Equals(DataType.Object))
                {
                    try
                    {
                        var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(field);
                        var fieldType = _tableManager.GetTableType(field.FieldType);

                        bool isTableExist = await _tableManager.IsTableExist(tableName);
                        if (!isTableExist) await _tableManager.CreateTableWithForeignKey(fieldType, type, tableName, foreignTableName);
                        await DecodeAgain(fieldType, tableName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private async Task DecodeProperties(Type? type, string foreignTableName)
        {
            var properties = type?.GetProperties();
            if (properties == null) return;

            foreach (var property in properties)
            {
                var dataType = _typeResolver.Resolve(property);

                if (dataType.Equals(DataType.Object))
                {
                    try
                    {
                        var tableName = type.Name + _tableManager.ResolveAndPluralizeTableName(property);
                        var propertyType = _tableManager.GetTableType(property.PropertyType);

                        bool isTableExist = await _tableManager.IsTableExist(tableName);
                        if (!isTableExist) await _tableManager.CreateTableWithForeignKey(propertyType, type, tableName, foreignTableName);
                        await DecodeAgain(propertyType, tableName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
