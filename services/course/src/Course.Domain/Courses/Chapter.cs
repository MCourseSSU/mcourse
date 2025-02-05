namespace Course.Domain.Courses
{
	public sealed class Chapter
	{
		public Guid Id { get; private set; }
		public Guid CourseId { get; private set; }
		public string Title { get; private set; }

		public Chapter(
			Guid id,
			Guid courseId,
			string title)
		{
			Id = id;
			CourseId = courseId;
			Title = title;
		}
	}
}