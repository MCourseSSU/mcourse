using AutoMapper;
using Calendar.Application.Contracts.Notes;
using Calendar.Application.Contracts.Notes.Commands;
using Calendar.Application.Contracts.Notes.Dto;
using Calendar.Application.Contracts.Notes.Queries;
using Calendar.Domain.Notes;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;
using Shared.Domain.Entities;

namespace Calendar.Application.Notes
{
	internal sealed class NoteService : INoteService
	{
		private readonly INoteRepository _noteRepository;
		private readonly IGuidGenerator _guidGenerator;
		private readonly IClockService _clockService;
		private readonly IMapper _mapper;

		public NoteService(
			INoteRepository noteRepository,
			IGuidGenerator guidGenerator,
			IClockService clockService,
			IMapper mapper)
		{
			_noteRepository = noteRepository;
			_guidGenerator = guidGenerator;
			_clockService = clockService;
			_mapper = mapper;
		}

		public async Task<Result<NoteDto>> CreateAsync(CreateNoteCommandDto command, CancellationToken cancellationToken)
		{
			var note = new Note(
				id: _guidGenerator.Create(),
				userId: _guidGenerator.Create(),
				title: command.Title,
				description: command.Description,
				startTime: command.StartTime,
				endTime: command.EndTime);

			await _noteRepository.InsertAsync(
				entity: note,
				autoSave: true,
				cancellationToken: cancellationToken);

			return new Result<NoteDto>(
				data: _mapper.Map<NoteDto>(note));
		}

		public async Task<Result<NoteDto>> GetAsync(Guid id, CancellationToken cancellationToken)
		{
			var note = await _noteRepository.GetAsync(id, cancellationToken);

			if (note is null)
			{
				return new Result<NoteDto>(errorMessage: "Заметка не найдена");
			}

			return new Result<NoteDto>(
				data: _mapper.Map<NoteDto>(note));
		}

		public async Task<Result<PagedResultDto<NoteListDto>>> GetListAsync(GetPagedListQueryDto query, CancellationToken cancellationToken)
		{
			var notes = await _noteRepository.GetPagedListAsync(
				pageNumber: query.PageNumber,
				pageSize: query.PageSize,
				cancellationToken: cancellationToken);

			var noteListDto = _mapper.Map<IReadOnlyCollection<NoteListDto>>(notes.Items);

			var pagedResultDto = new PagedResultDto<NoteListDto>
			{
				Items = noteListDto,
				TotalCount = notes.TotalCount
			};

			return new Result<PagedResultDto<NoteListDto>>(
				data: pagedResultDto);
		}
	}
}