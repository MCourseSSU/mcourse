namespace Course.Application.Contracts.Courses.Dto
{
	public sealed class CourseListDto
	{
		public required Guid Id { get; init; }
		public required string Title { get; init; }
		public string? Description { get; init; }
	}
}