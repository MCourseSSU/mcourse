using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using Shared.Contracts.Contracts.Dto;

namespace Course.Application.Contracts.Courses
{
	public interface ICourseService
	{
		Task<PagedResultDto<CourseListDto>> GetListAsync(PagedListRequest request, CancellationToken cancellationToken);
	}
}