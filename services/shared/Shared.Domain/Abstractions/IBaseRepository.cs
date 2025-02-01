namespace Shared.Domain.Abstractions
{
	public interface IBaseRepository<TEntity, TKey>
		where TEntity : class
		where TKey : struct
	{
		Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
		Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
		Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
		Task RemoveAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}