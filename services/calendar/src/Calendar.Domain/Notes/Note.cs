namespace Calendar.Domain.Notes
{
	public sealed class Note
	{
		public Guid Id { get; private set; }
		public Guid UserId { get; private set; }
		public string Title { get; private set; }
		public string Description { get; private set; }
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }

		public Note(
			Guid id,
			Guid userId,
			string title,
			string description,
			DateTime startTime,
			DateTime endTime)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Description = description;
			StartTime = startTime;
			EndTime = endTime;
		}
	}
}