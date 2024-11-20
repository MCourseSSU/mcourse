namespace MiniO.Application.Contracts.Buckets.Dto
{
    public sealed class UploadFileDto
    {
        public required string BucketName { get; init; }
        public required string ObjectName { get; init; }
        public required string FileName { get; init; }
        public required long Size { get; init; }
        public required string ContentType { get; init; }
    }
}
