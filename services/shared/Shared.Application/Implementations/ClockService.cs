using Shared.Application.Contracts.Contracts;

namespace Shared.Application.Implementations
{
	public sealed class ClockService : IClockService
	{
		public DateTime Now() => DateTime.UtcNow;
	}
}