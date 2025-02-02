using Course.EntityFrameworkCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using System.Collections.Immutable;

namespace Course.EntityFrameworkCore.Courses;

using Course.Domain.Courses;

internal sealed class CourseRepository : ICourseRepository
{
	private readonly CourseDbContext _context;

	public CourseRepository(CourseDbContext context)
	{
		_context = context;
	}

	public async Task<bool> CheckCourseForExistenceAsync(string title, CancellationToken cancellationToken)
	{
		return await _context.Courses.AnyAsync(x => x.Title == title);
	}

	public async Task<Course?> GetAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Courses
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public async Task<PagedResult<Course>> GetPagedListAsync(
		int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return new PagedResult<Course>
		{
			Items = _context.Courses
				.AsNoTracking()
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToImmutableList(),
			TotalCount = await _context.Courses.CountAsync(cancellationToken)
		};
	}

	public async Task<Course> InsertAsync(Course entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		await _context.Courses.AddAsync(entity, cancellationToken);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		return entity;
	}

	public async Task RemoveAsync(Course entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		_context.Courses.Remove(entity);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<Course> UpdateAsync(Course entity, bool autoSave = false, CancellationToken cancellationToken = default)
	{
		_context.Courses.Update(entity);

		if (autoSave == true)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		return entity;
	}
}