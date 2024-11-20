using MiniO.Application.Contracts.Buckets.Dto;

namespace MiniO.Application.Contracts.Buckets
{
    public interface IBucketService
    {
        Task InsertFileAsync(UploadFileDto dto, CancellationToken cancellationToken);
    }
}