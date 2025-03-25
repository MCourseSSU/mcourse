using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Calendar.Domain.Notes;
using Calendar.Domain.Shared.Notes;
using Calendar.Domain;

namespace Calendar.EntityFrameworkCore.EntityFramework.Configurations;
internal sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
{
	public void Configure(EntityTypeBuilder<Note> builder)
	{
		builder.ToTable(CalendarDbProperties.NoteTableName, CalendarDbProperties.DbSchema);

		builder.HasKey(x => x.Id);

		builder
			.Property(x => x.Id)
			.IsRequired()
			.ValueGeneratedNever();
		builder
			.Property(x => x.UserId)
			.IsRequired();
		builder
			.Property(x => x.Title)
			.IsRequired()
			.HasMaxLength(NoteConstants.TitleMaxLength);
		builder
			.Property(x => x.Description)
			.HasMaxLength(NoteConstants.DescriptionMaxLength);
		builder
			.Property(x => x.StartTime)
			.IsRequired();
		builder
			.Property(x => x.EndTime)
			.IsRequired();
	}
}