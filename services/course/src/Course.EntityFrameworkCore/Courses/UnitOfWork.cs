using Course.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Course.EntityFrameworkCore.Courses
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly CourseDbContext _context;
		private IDbContextTransaction? _transaction;

		public UnitOfWork(CourseDbContext context)
		{
			_context = context;
		}

		public async Task BeginTransaction(IsolationLevel isolationLevel, CancellationToken cancellationToken)
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("Транзакция уже начата");
			}

			_transaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
		}

		public async Task CommitAsync(CancellationToken cancellationToken)
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("Транзакция не начата");
			}

			try
			{
				await _context.SaveChangesAsync(cancellationToken);
				await _transaction.CommitAsync(cancellationToken);
			}
			finally
			{
				DisposeTransaction();
			}
		}

		public async Task RollbackAsync(CancellationToken cancellationToken)
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("Транзакция не начата");
			}

			try
			{
				await _transaction.RollbackAsync(cancellationToken);
			}
			finally
			{
				DisposeTransaction();
			}
		}

		private void DisposeTransaction()
		{
			_transaction?.Dispose();
			_transaction = null;
		}
	}
}