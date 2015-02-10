namespace SOS.Clients.MVC.CorpSite2.Models
{
	public interface ISosResult
	{
		int Code { get; set; }
		string Message { get; set; }
		string ValueType { get; set; }
		long SessionId { get; set; }

		object GetValue();
	}

	public interface ISosResult<T> : ISosResult { }

}