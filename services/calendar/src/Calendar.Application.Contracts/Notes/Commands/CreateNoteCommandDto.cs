using FluentValidation;

namespace Calendar.Application.Contracts.Notes.Commands;
public sealed class CreateNoteCommandDto
{
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required DateTime StartTime { get; init; }
	public required DateTime EndTime { get; init; }
}

public sealed class CreateCalendarCommandDtoValidator : AbstractValidator<CreateNoteCommandDto>
{
	public CreateCalendarCommandDtoValidator()
	{

	}
}