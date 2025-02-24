using Course.Application.Contracts.Courses.Commands;
using Course.Application.Contracts.Courses.Dto;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Contracts.Contracts.Dto;

namespace Course.Application.Contracts.Courses
{
	public interface ICourseService
	{
		Task<Result<CourseDto>> GetAsync(Guid id, CancellationToken cancellationToken);
		Task<Result<CourseDto>> CreateAsync(CreateCourseCommand command, CancellationToken cancellationToken);
		Task<Result<PagedResultDto<CourseListDto>>> GetListAsync(PagedListCommand request, CancellationToken cancellationToken);
		Task<Result<CourseDto>> UpdateAsync(UpdateCourseCommand request, CancellationToken cancellationToken);
		Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
	}
}