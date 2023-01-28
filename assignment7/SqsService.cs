using Amazon.SQS;
using Amazon.SQS.Model;

namespace assignment7
{
    public class SqsService : AwsService
    {
        private IAmazonSQS _sqsClient;
        public string _sqsUrl { get; set; }
        
        private const string defaultURL = "https://sqs.us-east-1.amazonaws.com/847888492411/aspnetb7-shafayet-assignment7";

        public SqsService() : this(defaultURL)
        {
        }

        public SqsService(string url)
        {
            _sqsUrl = url;
            _sqsClient = new AmazonSQSClient(_accessKey, _secretKey, _region);
        }

        public async Task Add(string message)
        {
            await Add(null, -1, message);
        }

        public async Task Add(Dictionary<string, MessageAttributeValue>? attributes, int delay, string messageBody = "")
        {
            if (string.IsNullOrEmpty(_sqsUrl)) throw new ArgumentNullException(_sqsUrl, "queue url is missing");
            
            var sendMessageRequest = new SendMessageRequest();
            if (delay > -1) sendMessageRequest.DelaySeconds = delay;
            if (attributes != null) sendMessageRequest.MessageAttributes = attributes;
            if (!string.IsNullOrEmpty(messageBody)) sendMessageRequest.MessageBody = messageBody;
            sendMessageRequest.QueueUrl = _sqsUrl;

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("Sent a message with id : {0}", response.MessageId);
        }

        public async Task<List<Message>> GetTenMessage()
        {
            return await GetMessage(20, 10);
        }

        public async Task<List<Message>> GetMessage(int waitTime, int maxMessages = 0)
        {
            if (string.IsNullOrEmpty(_sqsUrl)) throw new ArgumentNullException(_sqsUrl, "queue url is missing");

            var receiveRequest = new ReceiveMessageRequest();
            receiveRequest.QueueUrl = _sqsUrl;
            if(maxMessages != 0) receiveRequest.MaxNumberOfMessages = maxMessages;
            receiveRequest.AttributeNames = new List<string> { MessageSystemAttributeName.ApproximateReceiveCount.ToString() };
            if (waitTime > -1) receiveRequest.WaitTimeSeconds = waitTime;

            var responses = await _sqsClient.ReceiveMessageAsync(receiveRequest);

            return responses.Messages;
        }

        public async Task Delete()
        {
            var messages = await GetMessage(20);

            foreach(var message in messages)
            {
                if(int.Parse(message.Attributes["ApproximateReceiveCount"]) > 0)
                {
                    await _sqsClient.DeleteMessageAsync(_sqsUrl, message.ReceiptHandle);
                    Console.WriteLine($"'{message.MessageId}' message is deleted");
                }
            }
        }
    }
}
