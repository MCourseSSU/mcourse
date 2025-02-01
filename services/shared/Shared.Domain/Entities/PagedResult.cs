using System.Collections.Immutable;

namespace Shared.Domain.Entities
{
	public sealed class PagedResult<TEntity>
		where TEntity : class
	{
		public required IImmutableList<TEntity>? Items { get; init; }
		public required int TotalCount { get; init; }
	}
}