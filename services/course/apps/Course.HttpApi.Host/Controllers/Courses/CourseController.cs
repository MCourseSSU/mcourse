using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Commands;
using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Course.HttpApi.Host.Controllers.Courses
{
    [ApiController]
	[Route("api/[controller]")]
	public sealed class CourseController : ControllerBase
	{
		private readonly ICourseService _courseService;
		private readonly IValidator<PagedListQuery> _pagedListRequestValidator;
		private readonly IValidator<CreateCourseCommand> _createCourseRequestValidator;
		private readonly IValidator<UpdateCourseCommand> _updateCourseRequestValidator;

		public CourseController(
			ICourseService courseService,
			IValidator<PagedListQuery> pagedListRequestValidator,
			IValidator<CreateCourseCommand> createCourseRequestValidator,
			IValidator<UpdateCourseCommand> updateCourseRequestValidator)
		{
			_courseService = courseService;
			_pagedListRequestValidator = pagedListRequestValidator;
			_createCourseRequestValidator = createCourseRequestValidator;
			_updateCourseRequestValidator = updateCourseRequestValidator;
		}

		[HttpPost]
		public async Task<ActionResult<Result<CourseDto>>> CreateAsync(
			[FromBody] CreateCourseCommand command,
			CancellationToken cancellationToken)
		{
			await _createCourseRequestValidator.ValidateAndThrowAsync(command, cancellationToken);
			return await _courseService.CreateAsync(command, cancellationToken);
		}

		[HttpGet("{id:guid}")]
		public async Task<Result<CourseDto>> GetAsync(
			[FromRoute] Guid id,
			CancellationToken cancellationToken)
		{
			return await _courseService.GetAsync(id, cancellationToken);
		}

		[HttpGet("List")]
		public async Task<ActionResult<Result<PagedResultDto<CourseListDto>>>> GetListAsync(
			[FromQuery] PagedListQuery request,
			CancellationToken cancellationToken)
		{
			await _pagedListRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
			return await _courseService.GetListAsync(request, cancellationToken);
		}

		[HttpPut]
		public async Task<ActionResult<Result<CourseDto>>> UpdateAsync(
			[FromBody] UpdateCourseCommand request,
			CancellationToken cancellationToken)
		{
			await _updateCourseRequestValidator.ValidateAndThrowAsync(request, cancellationToken);
			return await _courseService.UpdateAsync(request, cancellationToken);
		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult<Result>> DeleteAsync(
			[FromRoute] Guid id,
			CancellationToken cancellationToken)
		{
			return await _courseService.DeleteAsync(id, cancellationToken);
		}
	}
}