namespace Shared.Application.Contracts.Contracts.Dto
{
	public sealed class PagedResultDto<TEntity>
	{
		public required IEnumerable<TEntity> Items { get; init; }
		public required int TotalCount { get; init; }
	}
}