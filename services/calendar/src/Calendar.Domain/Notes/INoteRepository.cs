using Calendar.Domain.Shared.Notes;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;

namespace Calendar.Domain.Notes
{
	public interface INoteRepository : IBaseRepository<Note, Guid>
	{
		Task<PagedResult<Note>> GetPagedListAsync(
			int pageNumber = NoteConstants.PageNumber,
			int pageSize = NoteConstants.PageSize,
			CancellationToken cancellationToken = default);
	}
}