using Course.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Course.EntityFrameworkCore.EntityFramework
{
	public sealed class CourseDbContext : DbContext
	{
		private readonly IConfiguration _configuration;

		public DbSet<Domain.Courses.Course> Courses { get; }

		public CourseDbContext(
			DbContextOptions<CourseDbContext> options,
			IConfiguration configuration)
			: base (options)
		{
			_configuration = configuration;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}