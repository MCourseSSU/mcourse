using Calendar.Domain.Shared.Notes;
using FluentValidation;

namespace Calendar.Application.Contracts.Notes.Queries
{
	public sealed class GetPagedListQueryDto
	{
		public required int PageNumber { get; init; }
		public required int PageSize { get; init; }
	}

	public class GetPagedListQueryDtoValidator : AbstractValidator<GetPagedListQueryDto>
	{
        public GetPagedListQueryDtoValidator()
        {
			RuleFor(x => x.PageNumber)
				.GreaterThanOrEqualTo(NoteConstants.PageNumber)
				.WithMessage($"Номер страницы не может быть меньше {NoteConstants.PageNumber}");
			RuleFor(x => x.PageSize)
				.GreaterThanOrEqualTo(NoteConstants.PageSize)
				.WithMessage($"Размер страницы должен быть больше {NoteConstants.PageSize}");
		}

	}
}