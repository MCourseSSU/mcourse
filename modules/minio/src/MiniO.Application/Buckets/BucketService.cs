using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using MiniO.Application.Contracts.Buckets;
using MiniO.Application.Contracts.Buckets.Dto;

namespace MiniO.Application.Buckets
{
    internal sealed class BucketService : IBucketService
    {
        private readonly IMinioClient _minio = new MinioClient()
            .WithEndpoint("http://localhost:9001")
            .WithCredentials("drews16", "Secret_1")
            .WithSSL()
            .Build();    

        public async Task InsertFileAsync(UploadFileDto dto, CancellationToken cancellationToken)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(dto.BucketName);

            bool isExistBucket = await _minio.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (!isExistBucket)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(dto.BucketName);

                await _minio.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            try
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(dto.BucketName)
                    .WithObject(dto.ObjectName)
                    .WithFileName(dto.FileName)
                    .WithObjectSize(dto.Size)
                    .WithContentType(dto.ContentType);

                await _minio.PutObjectAsync(putObjectArgs, cancellationToken);
            }
            catch(MinioException ex)
            {
                // TODO: Add logger.
            }
        }
    }
}
