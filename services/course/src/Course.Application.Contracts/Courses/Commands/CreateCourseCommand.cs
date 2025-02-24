using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Commands;
public sealed class CreateCourseCommand
{
	public required string Title { get; init; }
	public string? Description { get; init; }
	public required IEnumerable<CreateChapterCommand> Chapters { get; init; }
}

public sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
	public CreateCourseCommandValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(CourseConstants.MaxCourseTitleLength)
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxCourseTitleLength} символов");
		RuleFor(x => x.Description)
			.MaximumLength(CourseConstants.MaxDescriptionLength)
			.When(x => !string.IsNullOrEmpty(x.Description))
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxDescriptionLength} символов");
		RuleForEach(x => x.Chapters)
			.SetValidator(new CreateChapterCommandValidator());
	}
}