namespace Calendar.Application.Contracts.Notes.Dto
{
	public sealed class NoteListDto
	{
		public required Guid Id { get; init; }
		public required string Title { get; init; }
		public required DateTime StartTime { get; init; }
		public required DateTime EndTime { get; init; }
	}
}