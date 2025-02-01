using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using Shared.Application.Contracts.Contracts.Dto;

namespace Course.Application.Contracts.Courses
{
	public interface ICourseService
	{
		Task<CourseDto> CreateCourseAsync(CreateCourseRequest request, CancellationToken cancellationToken);
		Task<PagedResultDto<CourseListDto>> GetListAsync(PagedListRequest request, CancellationToken cancellationToken);
	}
}