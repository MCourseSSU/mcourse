using Course.Application.Contracts.Courses;
using Course.Application.Contracts.Courses.Requests;
using Course.Application.Courses;
using Course.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Course.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddEntityFrameworkCore(configuration);
		services.AddSharedApplication();
		services.AddAutoMapper(typeof(CourseAutoMapperProfile));

		AddServices(services);
		AddValidators(services);

		return services;
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<ICourseService, CourseService>();
	}

	private static void AddValidators(IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();

		services.AddScoped<IValidator<PagedListRequest>, PageListRequestValidator>();
		services.AddScoped<IValidator<CreateCourseRequest>, CreateCourseRequestValidator>();
		services.AddScoped<IValidator<UpdateCourseRequest>, UpdateCourseRequestValidator>();
	}
}