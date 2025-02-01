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
		private readonly IValidator<CreateCourseRequest> _createCourseRequestValidator;

		public CourseController(
			ICourseService courseService,
			IValidator<PagedListRequest> pagedListRequestValidator,
			IValidator<CreateCourseRequest> createCourseRequestValidator)
		{
			_courseService = courseService;
			_pagedListRequestValidator = pagedListRequestValidator;
			_createCourseRequestValidator = createCourseRequestValidator;
		}

		[HttpPost]
		public async Task<CourseDto> CreateCourseAsync(
			[FromBody] CreateCourseRequest request,
			CancellationToken cancellationToken)
		{
			await _createCourseRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
			return await _courseService.CreateCourseAsync(request, cancellationToken);
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