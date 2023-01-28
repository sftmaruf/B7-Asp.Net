using Amazon.DynamoDBv2.DataModel;

namespace assignment7.Entities
{
    public class Person
    {
        [DynamoDBHashKey]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public static IList<Person> DemoData()
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Name = "Name0",
                    PhoneNumber = "01710000000"
                },
                new Person
                {
                    Name = "Name1",
                    PhoneNumber = "01710000001"
                },
                new Person
                {
                    Name = "Name2",
                    PhoneNumber = "01710000002"
                },
                new Person
                {
                    Name = "Name3",
                    PhoneNumber = "01710000003"
                },
                new Person
                {
                    Name = "Name4",
                    PhoneNumber = "01710000004"
                },
                new Person
                {
                    Name = "Name5",
                    PhoneNumber = "01710000005"
                },
                new Person
                {
                    Name = "Name6",
                    PhoneNumber = "01710000006"
                },
                new Person
                {
                    Name = "Name7",
                    PhoneNumber = "01710000007"
                },
                new Person
                {
                    Name = "Name8",
                    PhoneNumber = "01710000008"
                },
                new Person
                {
                    Name = "Name9",
                    PhoneNumber = "01710000009"
                }
            };

            return persons;
        }
    }
}
