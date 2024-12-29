using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Mvc;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BucketController : Controller
    {
        private readonly IAmazonS3 _s3Client;

        public BucketController(IAmazonS3 amazonS3)
        {
            _s3Client = amazonS3;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBucketAsync(string bucketName)
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (bucketExists)
                return BadRequest($"Bucket {bucketName} alreadyexists");
            var value = await _s3Client.PutBucketAsync(bucketName);
            return Created("bucket", $"Bucket {bucketName} created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBucketAsync()
        {
            var data = await _s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => {  return b.BucketName; });
            return Ok(buckets);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBucketAsync(string bucketName)
        {
            await _s3Client.DeleteBucketAsync(bucketName);
            return NoContent();
        }

    }
}
