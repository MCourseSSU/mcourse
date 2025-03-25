using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Calendar.EntityFrameworkCore;
using Shared.Application;
using Calendar.Application.Contracts.Notes;
using Calendar.Application.Notes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Calendar.Application.Contracts.Notes.Queries;

namespace Calendar.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddEntityFrameworkCore(configuration);
			services.AddSharedApplication();
			services.AddAutoMapper(typeof(CalendarAutoMapperProfile));

			AddServices(services);
			AddValidators(services);

			return services;
		}

		private static void AddServices(IServiceCollection services)
		{
			services.AddScoped<INoteService, NoteService>();
		}

		private static void AddValidators(IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();

			services.AddScoped<IValidator<GetPagedListQueryDto>, GetPagedListQueryDtoValidator>();
		}
	}
}