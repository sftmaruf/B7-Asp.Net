using Amazon.S3;
using System.Net;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace assignment7
{
    public class S3Service : AwsService
    {
        private IAmazonS3 s3Client;
        private readonly string filePath;

        private readonly string bucketName;

        public S3Service() : this(
            @"D:\\documents\Courses\ASP .NET\B7-Asp.Net\assignment7\Files\Upload.txt", 
            "aspnetb7-shafayet-assignment7")
        {
        }

        public S3Service(string path, string bucket)
        {
            filePath = path;
            bucketName = bucket;

            s3Client = new AmazonS3Client(_accessKey, _secretKey, _region);
        }

        private string FileDoesntExistMessage(string fileName) =>
            $"'{fileName}' file doesn't exist. Please provide a valid name.";

        public async Task UploadAsync()
        {
            var fileTransferUtility = new TransferUtility(s3Client);
            await fileTransferUtility.UploadAsync(filePath, bucketName);
        }

        public async Task DownloadAsync(string destinationFullPath, string fileName)
        {
            try
            {
                if (!await IsObjectExist(fileName))
                {
                    Console.WriteLine(FileDoesntExistMessage(fileName));
                    return;
                }

                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.DownloadAsync(destinationFullPath, bucketName, fileName);
                
                Console.WriteLine($"{fileName} is downloaded in {destinationFullPath} path");
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteAsync(string fileName)
        {
            try
            {
                if(!await IsObjectExist(fileName))
                {
                    Console.WriteLine(FileDoesntExistMessage(fileName));
                    return;
                }

                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                var response = await s3Client.DeleteObjectAsync(deleteObjectRequest);
                Console.WriteLine($"Successfully deleted '{fileName}' object");
            }
            catch(AmazonS3Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<bool> IsObjectExist(string fileName)
        {
            try
            {
                var request = new GetObjectMetadataRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                var response = await s3Client.GetObjectMetadataAsync(request);
                return true;
            }
            catch(AmazonS3Exception ex)
            {
                if (ex.StatusCode != HttpStatusCode.NotFound)
                    throw new AmazonS3Exception("Forbidden operation");

                return false;
            }
        }
    }
}
