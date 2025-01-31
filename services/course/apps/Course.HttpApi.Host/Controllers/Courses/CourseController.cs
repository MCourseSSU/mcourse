using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Course.HttpApi.Host.Controllers.Courses
{
	[ApiController]
	[Route("api/[controller]")]
	public sealed class CourseController : ControllerBase
	{
		private readonly ICourseService _courseService;

		public CourseController(
			ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet("List")]
		public async Task<IImmutableList<CourseListDto>> GetListAsync(CancellationToken cancellationToken)
		{
			return await _courseService.GetListAsync(cancellationToken);
		}
	}
}