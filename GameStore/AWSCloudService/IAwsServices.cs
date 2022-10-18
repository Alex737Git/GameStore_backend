using Microsoft.AspNetCore.Http;

namespace AWSCloudService;

public interface IAwsServices
{
    Task<string> UploadPhoto(IAwsConfig config,IFormFile file);
    Task<byte[]> TransferFileToBytesArray(IFormFile file);
}