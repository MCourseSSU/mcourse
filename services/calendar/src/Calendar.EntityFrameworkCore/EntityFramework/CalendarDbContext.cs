using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Calendar.Domain.Notes;

namespace Calendar.EntityFrameworkCore.EntityFramework;
public sealed class CalendarDbContext : DbContext
{
	public DbSet<Note> Notes { get; private set; }

	public CalendarDbContext(DbContextOptions<CalendarDbContext> options) 
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}