using Course.Application.Contracts.Courses.Dto;
using System.Collections.Immutable;

namespace Course.Application.Contracts.Courses
{
	public interface ICourseService
	{
		Task<IImmutableList<CourseListDto>> GetListAsync(CancellationToken cancellationToken);
	}
}