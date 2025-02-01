using Course.Domain.Shared.Courses;
using Shared.Domain.Abstractions;
using Shared.Domain.Entities;

namespace Course.Domain.Courses
{
	public interface ICourseRepository : IBaseRepository<Course, Guid>
	{
		Task<PagedResult<Course>> GetPagedListAsync(
			int pageNumber = CourseConstants.PageNumber,
			int pageSize = CourseConstants.PageSize,
			CancellationToken cancellationToken = default);
	}
}