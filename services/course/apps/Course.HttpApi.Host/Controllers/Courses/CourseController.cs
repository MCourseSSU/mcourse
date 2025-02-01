using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Contracts.Dto;

namespace Course.HttpApi.Host.Controllers.Courses
{
	[ApiController]
	[Route("api/[controller]")]
	public sealed class CourseController : ControllerBase, ICourseService
	{
		private readonly ICourseService _courseService;
		private readonly IValidator<PagedListRequest> _pagedListRequestValidator;
		public CourseController(
			ICourseService courseService,
			IValidator<PagedListRequest> pagedListRequestValidator)
		{
			_courseService = courseService;
			_pagedListRequestValidator = pagedListRequestValidator;
		}

		[HttpGet("List")]
		public async Task<PagedResultDto<CourseListDto>> GetListAsync(
			[FromQuery] PagedListRequest request,
			CancellationToken cancellationToken)
		{
			await _pagedListRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
			return await _courseService.GetListAsync(request, cancellationToken);
		}
	}
}