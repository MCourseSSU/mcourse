using Course.Domain.Courses;
using Course.EntityFrameworkCore.EntityFramework;

namespace Course.EntityFrameworkCore.Courses
{
	internal sealed class CourseRepository : ICourseRepository
	{
		private readonly CourseDbContext _context;

		public CourseRepository(CourseDbContext context)
		{
			_context = context;
		}
	}
}