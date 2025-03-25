using Shared.Domain.Entities;
using Calendar.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Calendar.EntityFrameworkCore.Notes;

using Calendar.Domain.Notes;

internal sealed class NoteRepository : INoteRepository
{
	private readonly CalendarDbContext _context;

	public NoteRepository(CalendarDbContext context)
	{
		_context = context;
	}

	public async Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		return await _context.Notes
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<PagedResult<Note>> GetPagedListAsync(
		int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return new PagedResult<Note>
		{
			Items = _context.Notes
				.AsNoTracking()
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToImmutableList(),
			TotalCount = await _context.Notes.CountAsync(cancellationToken)
		};
	}

	public async Task<Note> InsertAsync(Note entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		await _context.Notes.AddAsync(entity, cancellationToken);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		return entity;
	}

	public async Task RemoveAsync(Note entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		_context.Notes.Remove(entity);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<Note> UpdateAsync(Note entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		_context.Notes.Update(entity);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		return entity;
	}
}