using Calendar.Application.Contracts.Notes;
using Calendar.Application.Contracts.Notes.Commands;
using Calendar.Application.Contracts.Notes.Dto;
using Calendar.Application.Contracts.Notes.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Calendar.HttpApi.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class NoteController : ControllerBase, INoteService
{
	private readonly INoteService _noteService;
	private readonly IValidator<GetPagedListQueryDto> _getPagedListQueryValidator;

	public NoteController(
		INoteService noteService,
		IValidator<GetPagedListQueryDto> getPagedListQueryValidator )
	{
		_noteService = noteService;
		_getPagedListQueryValidator = getPagedListQueryValidator;
	}

	[HttpPost]
	public async Task<Result<NoteDto>> CreateAsync(
		[FromBody] CreateNoteCommandDto command,
		CancellationToken cancellationToken)
	{
		return await _noteService.CreateAsync(command, cancellationToken);
	}

	[HttpGet("{id:guid}")]
	public async Task<Result<NoteDto>> GetAsync(
		[FromRoute] Guid id,
		CancellationToken cancellationToken)
	{
		return await _noteService.GetAsync(id, cancellationToken);
	}

	[HttpGet("List")]
	public async Task<Result<PagedResultDto<NoteListDto>>> GetListAsync(
		[FromQuery] GetPagedListQueryDto query,
		CancellationToken cancellationToken)
	{
		await _getPagedListQueryValidator.ValidateAndThrowAsync(query, cancellationToken);
		return await _noteService.GetListAsync(query, cancellationToken);
	}
}