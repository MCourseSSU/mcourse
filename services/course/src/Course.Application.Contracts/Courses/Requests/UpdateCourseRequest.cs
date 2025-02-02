using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Requests;

public sealed class UpdateCourseRequest
{
	public required Guid Id { get; init; }
	public string? Description { get; init; }
}

public sealed class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
{
	public UpdateCourseRequestValidator()
	{
		RuleFor(x => x.Description)
			.MaximumLength(CourseConstants.MaxDescriptionLength)
			.When(x => !string.IsNullOrEmpty(x.Description))
			.WithMessage($"Максимальная длина названия курса {CourseConstants.MaxDescriptionLength} символов");
	}
}