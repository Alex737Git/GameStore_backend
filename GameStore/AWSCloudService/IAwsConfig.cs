namespace AWSCloudService;

public interface IAwsConfig
{
     string? BucketName { get; set; }
     string? AccessKey { get; set; }
     string? SecretKey { get; set; }
     string? Region { get; set; }
}