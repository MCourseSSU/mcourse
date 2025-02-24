using System.Data;

namespace Course.EntityFrameworkCore.Courses
{
	public interface IUnitOfWork
	{
		Task BeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken);
		Task CommitAsync(CancellationToken cancellationToken);
		Task RollbackAsync(CancellationToken cancellationToken);
	}
}