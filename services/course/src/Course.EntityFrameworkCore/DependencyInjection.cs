using Course.Domain;
using Course.Domain.Courses;
using Course.EntityFrameworkCore.Courses;
using Course.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.EntityFrameworkCore
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<CourseDbContext>(options =>
				options.UseSqlServer(configuration[CourseDbProperties.ConnectionString]));

			AddRepository(services);

			return services;
		}

		private static IServiceCollection AddRepository(IServiceCollection services)
		{
			services.AddScoped<ICourseRepository, CourseRepository>();

			return services;
		}
	}
}