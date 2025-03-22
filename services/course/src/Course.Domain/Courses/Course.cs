using Shared.Domain.Entities;

namespace Course.Domain.Courses
{
	public sealed class Course : Aggregate<Guid>
	{
		public string Title { get; private set; }
		public string? Description { get; private set; }
		public IList<Chapter> Chapters { get; private set; }

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

		public Course Update(
			string? description,
			DateTime updatedTime)
		{
			Description = description;
			UpdatedTime = updatedTime;

			return this;
		}

		public Course AddChapter(Chapter chapters)
		{
			Chapters.Add(chapters);
			return this;
		}

		public Course UpdateChapters(Chapter chapter)
		{
			return this;
		}
	}
}