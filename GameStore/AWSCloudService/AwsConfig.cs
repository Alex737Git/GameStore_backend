namespace AWSCloudService;

public class AwsConfig:IAwsConfig
{
    public string? BucketName { get; set; }
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
    public string? Region { get; set; }
}