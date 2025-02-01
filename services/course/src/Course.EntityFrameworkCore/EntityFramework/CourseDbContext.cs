using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Course.EntityFrameworkCore.EntityFramework;

using Course.Domain.Courses;

public sealed class CourseDbContext : DbContext
{
	public DbSet<Course> Courses { get; private set; }

	public CourseDbContext(DbContextOptions<CourseDbContext> options)
		: base (options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}