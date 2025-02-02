namespace Shared.Application.Contracts.Contracts
{
	public sealed class Result<T> : Result
	{
		public T? Data { get; init; }

		public Result(T data)
			: base(null)
		{
			Data = data;

		}

		public Result(string? errorMessage)
			: base(errorMessage)
		{
			Data = default;
			ErrorMessage = errorMessage;
		}
	}

	public class Result
	{
		public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage) ? true : false;
		public string? ErrorMessage { get; init; }

		public Result()
		{

		}

		public Result(string? errorMessage)
		{
			ErrorMessage = errorMessage;
		}
	}
}