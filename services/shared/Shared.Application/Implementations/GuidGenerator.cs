using Shared.Application.Contracts.Contracts;

namespace Shared.Application.Implementations
{
	internal sealed class GuidGenerator : IGuidGenerator
	{
		public Guid Create() => Guid.NewGuid();
	}
}