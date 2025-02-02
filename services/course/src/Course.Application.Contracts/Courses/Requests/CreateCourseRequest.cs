using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Requests;

public sealed class CreateCourseRequest
{
	public required string Title { get; init; }
	public string? Description { get; init; }
}

public sealed class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
{
	public CreateCourseRequestValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(CourseConstants.MaxTitleLength)
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxTitleLength} символов");
		RuleFor(x => x.Description)
			.MaximumLength(CourseConstants.MaxDescriptionLength)
			.When(x => !string.IsNullOrEmpty(x.Description))
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxDescriptionLength} символов");
	}
}