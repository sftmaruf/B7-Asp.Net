using assignment7;
using assignment7.Entities;
using System.Text.Json;

var keyName = "Upload.txt";
var filePath = @"D:\\documents\Courses\ASP .NET\B7-Asp.Net\assignment7\Files\Upload.txt";

var s3Service = new S3Service();
//await s3Service.UploadAsync(filePath);
//await s3Service.DownloadAsync(filePath, keyName);
//await s3Service.DeleteAsync(keyName);

//var dynamoService = new DynamoDbService<Person>();
//await dynamoService.AddAsync(Person.DemoData());
//await dynamoService.DeleteAsync("Name2", "01710000002");
//foreach (var data in await dynamoService.LoadTenDataAsync())
//{
//    Console.WriteLine(JsonSerializer.Serialize(data));
//}

//var sqsService = new SqsService();
//await sqsService.Add("First");
//await sqsService.Add("Second");
//await sqsService.Add("Third");
//await sqsService.Add("Fourth");
//await sqsService.Add("Fifth");
//await sqsService.Add("Sixth");
//await sqsService.Add("Seventh");
//await sqsService.Add("Eight");
//await sqsService.Add("Ninth");
//await sqsService.Add("Tenth");
//await sqsService.Add("Eleventh");
//await sqsService.Add("Twelfth");

//await sqsService.Delete();

//var messages = await sqsService.GetTenMessage();
//foreach (var message in messages)
//{
//    Console.WriteLine($"The message '{message.MessageId}' body contains '{message.Body}'");
//};