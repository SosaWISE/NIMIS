namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsREResult
	{
		int Code { get; }
		string Message { get; }
		object Value { get; }
	}
}