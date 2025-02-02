using Course.Application.Contracts.Courses.Dto;
using Course.Application.Contracts.Courses.Requests;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Course.Application.Contracts.Courses
{
	public interface ICourseService
	{
		Task<Result<CourseDto>> GetAsync(Guid id, CancellationToken cancellationToken);
		Task<Result<CourseDto>> CreateAsync(CreateCourseRequest request, CancellationToken cancellationToken);
		Task<Result<PagedResultDto<CourseListDto>>> GetListAsync(PagedListRequest request, CancellationToken cancellationToken);
		Task<Result<CourseDto>> UpdateAsync(UpdateCourseRequest request, CancellationToken cancellationToken);
		Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
	}
}