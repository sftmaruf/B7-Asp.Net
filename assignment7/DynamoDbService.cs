using Amazon.DynamoDBv2;
using assignment7.Entities;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;

namespace assignment7
{
    public class DynamoDbService<T> : AwsService
    {
        private readonly string dbName;
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public DynamoDbService() : this("aspnetb7-shafayet-assignment7")
        {
        }

        public DynamoDbService(string name)
        {
            dbName = name;
            _client = new AmazonDynamoDBClient(_accessKey, _secretKey, _region);
            _context = new DynamoDBContext(_client);
        }

        public async Task AddDemoAsync()
        {
            var persons = Person.DemoData();

            await AddAsync((IList<T>)persons);
           
        }
            
        public async Task AddAsync(IList<T> persons)
        {
            var batch = _context.CreateBatchWrite<T>(new DynamoDBOperationConfig
            {
                OverrideTableName = dbName
            });

            batch.AddPutItems(persons);
            Console.WriteLine($"Added {persons.Count} data into the Database");
            await batch.ExecuteAsync();
        }

        public async Task DeleteAsync(string hashKey, string rangeKey)
        {
            if(!await IsExist(hashKey, rangeKey))
            {
                Console.WriteLine($"{hashKey} doesn't exist in the database");
                return;
            }

            await _context.DeleteAsync<T>(hashKey, rangeKey, new DynamoDBOperationConfig
            {
                OverrideTableName = dbName
            });
            
            if(!await IsExist(hashKey, rangeKey))
            {
                Console.WriteLine($"'{hashKey}' deleted successfully");
            }
        }

        private async Task<bool> IsExist(string hashKey, string rangeKey)
        {
            try
            {
                var deletedItem = await LoadDataAsync(hashKey, rangeKey);
                if (deletedItem != null) return true;
            }
            catch(AmazonDynamoDBException exception)
            {
                if(exception.ErrorCode != "ResourceNotFoundException")
                    throw;
            }
            
            return false;
        }

        private async Task<T> LoadDataAsync(string hashKey, string rangeKey)
        {
            return await _context.LoadAsync<T>(hashKey, rangeKey, new DynamoDBOperationConfig
            {
                OverrideTableName = dbName
            });
        }

        public async Task <List<T>> LoadTenDataAsync()
        {
            return await LoadDataAsync(10);
        }

        public async Task<List<T>> LoadDataAsync(int range)
        {
            var request = new ScanRequest
            {
                TableName = dbName,
                Limit = range
            };

            var response = await _client.ScanAsync(request);

            var persons = new List<T>();

            foreach(var record in response.Items)
            {
                var person = Activator.CreateInstance(typeof(T));
                foreach (var r in record)
                {
                    person?.GetType()?.GetProperty(r.Key)?.SetValue(person, r.Value.S);
                }
                persons.Add((T)person!);
            }

            return persons;
        }
    }
}
