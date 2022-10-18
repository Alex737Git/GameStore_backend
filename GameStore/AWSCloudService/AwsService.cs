using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace AWSCloudService;

public class AwsService : IAwsServices
{
 
    public async Task<string> UploadPhoto(IAwsConfig config, IFormFile file)
    {
        #region Init Aws Service

        var newRegion = RegionEndpoint.GetBySystemName(config.Region);
        var credentials = new BasicAWSCredentials(config.AccessKey, config.SecretKey);
        var client = new AmazonS3Client(credentials, newRegion);

        #endregion


        #region GetPresignedUrl

        GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
        {
            BucketName = config.BucketName,
            Key = Guid.NewGuid().ToString(),
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(5),
        };

        string path = client.GetPreSignedURL(request);

        #endregion


        #region Upload Photo to Aws

        byte[] data = await TransferFileToBytesArray(file);

        using (var uploadRequest = new HttpRequestMessage(HttpMethod.Put, path))
        {
            uploadRequest.Content = new ByteArrayContent(data);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(uploadRequest);
                if (response.IsSuccessStatusCode)
                {
                    return path.Split('?')[0];
                }


                throw new Exception("Could not upload file");
            }
        }

        #endregion
    }

    public async Task<byte[]> TransferFileToBytesArray(IFormFile file)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}