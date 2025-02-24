using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Commands
{
	public sealed class CreateChapterCommand
	{
		public required string Title { get; init; }
	}

	public sealed class CreateChapterCommandValidator : AbstractValidator<CreateChapterCommand>
	{
		public CreateChapterCommandValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.MaximumLength(CourseConstants.MaxChapterTitleLength)
				.WithMessage($"Максимальная длина названия раздела {CourseConstants.MaxChapterTitleLength} символов");
		}
	}
}