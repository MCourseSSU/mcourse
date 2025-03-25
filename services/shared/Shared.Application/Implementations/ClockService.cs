using Shared.Application.Contracts.Contracts;

namespace Shared.Application.Implementations
{
	internal sealed class ClockService : IClockService
	{
		public DateTime UtcNow() => DateTime.UtcNow;
	}
}