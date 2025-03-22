using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Commands;

public sealed class UpdateCourseCommand
{
	public required Guid Id { get; init; }
	public string? Description { get; init; }
}

public sealed class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseCommand>
{
	public UpdateCourseRequestValidator()
	{
		RuleFor(x => x.Description)
			.MaximumLength(CourseConstants.MaxDescriptionLength)
			.When(x => !string.IsNullOrEmpty(x.Description))
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxDescriptionLength} символов");
	}
}