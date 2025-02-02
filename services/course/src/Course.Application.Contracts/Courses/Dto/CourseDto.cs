namespace Course.Application.Contracts.Courses.Dto
{
	public sealed class CourseDto
	{
		public required Guid Id { get; init; }
		public required string Title { get; init; }
		public required string? Description { get; init; }
	}
}