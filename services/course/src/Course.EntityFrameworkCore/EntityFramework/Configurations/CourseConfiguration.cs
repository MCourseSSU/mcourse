using Course.Domain;
using Course.Domain.Shared.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.EntityFrameworkCore.EntityFramework.Configurations
{
	internal sealed class CourseConfiguration : IEntityTypeConfiguration<Domain.Courses.Course>
	{
		public void Configure(EntityTypeBuilder<Domain.Courses.Course> builder)
		{
			builder.ToTable(CourseDbProperties.CourseTableName, CourseDbProperties.DbSchema);

			builder.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.IsRequired()
				.ValueGeneratedNever();
			builder
				.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(CourseConstants.MaxTitleLength);
			builder
				.Property(x => x.Description)
				.HasMaxLength(CourseConstants.MaxDescriptionLength);
		}
	}
}