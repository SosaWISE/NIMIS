namespace SSE.Services.CmsCORS.Models
{
	public interface ICmsResult
	{
		int Code { get; set; }
		string Message { get; set; }
		string ValueType { get; set; }
		long SessionId { get; set; }

		object GetValue();
	}

	public interface ICmsResult<T> :ICmsResult {}
}