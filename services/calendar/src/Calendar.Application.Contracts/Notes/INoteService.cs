using Calendar.Application.Contracts.Notes.Commands;
using Calendar.Application.Contracts.Notes.Dto;
using Calendar.Application.Contracts.Notes.Queries;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Calendar.Application.Contracts.Notes
{
	public interface INoteService
	{
		Task<Result<NoteDto>> CreateAsync(CreateNoteCommandDto command, CancellationToken cancellationToken);
		Task<Result<NoteDto>> GetAsync(Guid id, CancellationToken cancellationToken);
		Task<Result<PagedResultDto<NoteListDto>>> GetListAsync(GetPagedListQueryDto query, CancellationToken cancellationToken);
	}
}