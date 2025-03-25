using Calendar.Domain;
using Calendar.Domain.Notes;
using Calendar.EntityFrameworkCore.EntityFramework;
using Calendar.EntityFrameworkCore.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calendar.EntityFrameworkCore
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<CalendarDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString(CalendarDbProperties.ConnectionString)));

			AddRepository(services);

			return services;
		}

		private static void AddRepository(IServiceCollection services)
		{
			services.AddScoped<INoteRepository, NoteRepository>();
		}
	}
}