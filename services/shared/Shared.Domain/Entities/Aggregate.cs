namespace Shared.Domain.Entities
{
	public class Aggregate<TKey>
		where TKey : struct
	{
		public TKey Id { get; set; } 
		public DateTime CreationTime { get; set; }
		public DateTime UpdatedTime { get; set; }
		public TKey CreatorId { get; set; }
	}
}