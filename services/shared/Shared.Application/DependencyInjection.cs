using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Contracts.Contracts;
using Shared.Application.Implementations;

namespace Shared.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddSharedApplication(this IServiceCollection services)
		{
			services.AddScoped<IGuidGenerator, GuidGenerator>();
			services.AddScoped<IClockService, ClockService>();

			return services;
		}
	}
}