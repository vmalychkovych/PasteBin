using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace PasteBinApi.Service
{
    public class AmazonS3Service
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public AmazonS3Service(IConfiguration configuration)
        {
            _s3Client = new AmazonS3Client(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"], RegionEndpoint.USEast1);
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
        {
            var uploadRequest = new PutObjectRequest { InputStream = fileStream, BucketName = _bucketName, Key = fileName };
            var response = await _s3Client.PutObjectAsync(uploadRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK ? fileName : null;
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var request = new GetObjectRequest { BucketName = _bucketName, Key = fileName };
            var response = await _s3Client.GetObjectAsync(request);
            return response.ResponseStream;
        }
    }
}
