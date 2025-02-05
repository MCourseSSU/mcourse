using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Course.Domain;
using Course.Domain.Courses;
using Course.Domain.Shared.Courses;

namespace Course.EntityFrameworkCore.EntityFramework.Configurations;
internal sealed class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
	public void Configure(EntityTypeBuilder<Chapter> builder)
	{
		builder.ToTable(CourseDbProperties.ChapterTableName, CourseDbProperties.DbSchema);
		builder.HasKey(x => x.Id);

		builder
			.Property(x => x.CourseId)
			.IsRequired();
		builder
			.Property(x => x.Title)
			.IsRequired()
			.HasMaxLength(CourseConstants.MaxChapterTitleLength);
	}
}