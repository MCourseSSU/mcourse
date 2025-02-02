namespace Course.Domain.Courses
{
	public sealed class Course
	{
		public Guid Id { get; private set; }
		public string Title { get; private set; }
		public string? Description { get; private set; }

		public Course(
			Guid id,
			string title,
			string? description = null)
		{
			Id = id;
			Title = title;
			Description = description;
		}

		public Course Update(
			string? description)
		{
			Description = description;

			return this;
		}
	}
}