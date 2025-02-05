using Shared.Domain.Entities;
using System.Collections.Immutable;

namespace Course.Domain.Courses
{
	public sealed class Course : Aggregate<Guid>
	{
		public string Title { get; private set; }
		public string? Description { get; private set; }
		public IImmutableList<Chapter> Chapters { get; private set; }

		public Course(
			Guid id,
			string title,
			DateTime creationTime,
			Guid creatorId,
			string? description = null)
		{
			Id = id;
			Title = title;
			CreationTime = creationTime;
			CreatorId = creatorId;
			Description = description;
			Chapters = [];
		}

		public Course AddChapters(IImmutableList<Chapter> chapters)
		{
			Chapters.AddRange(chapters);
			return this;
		}

		public Course Update(
			string? description,
			DateTime updatedTime)
		{
			Description = description;
			UpdatedTime = updatedTime;

			return this;
		}
	}
}