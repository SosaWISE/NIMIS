namespace Nxs.Services.CorsConnext.Models
{
	public interface ICorsConnextResult
	{
		int Code { get; set; }
		string Message { get; set; }
		string ValueType { get; set; }
		long SessionId { get; set; }

		object GetValue();		 
	}

	public interface ICorsConnextResult<T> : ICorsConnextResult { }

}