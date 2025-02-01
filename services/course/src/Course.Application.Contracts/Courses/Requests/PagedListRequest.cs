using Course.Domain.Shared.Courses;
using FluentValidation;

namespace Course.Application.Contracts.Courses.Requests;

public sealed class PagedListRequest
{
	public required int PageNumber { get; init; }
	public required int PageSize { get; init; }
}

public sealed class PageListRequestValidator : AbstractValidator<PagedListRequest>
{
	public PageListRequestValidator()
	{
		RuleFor(x => x.PageNumber)
			.GreaterThanOrEqualTo(CourseConstants.PageNumber)
			.WithMessage($"Номер страницы не может быть меньше {CourseConstants.PageNumber}");
		RuleFor(x => x.PageSize)
			.GreaterThanOrEqualTo(CourseConstants.PageSize)
			.WithMessage($"Размер страницы должен быть больше {CourseConstants.PageSize}");
	}
}