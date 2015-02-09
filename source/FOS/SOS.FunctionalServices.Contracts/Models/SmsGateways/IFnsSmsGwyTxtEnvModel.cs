namespace SOS.FunctionalServices.Contracts.Models.SmsGateways
{
	public interface IFnsSmsGwyTxtEnvModel
	{
		string Title { get; }
		string Code { get; }
		string ShortCode { get; }
		string Message { get; }
		string Phone { get; }
		string Carrier { get; }
		string Keyword { get; }
		string GroupName { get; }
		string CustomTicket { get; }
		string DefaultKeyword { get; }
		string Username { get; }
		string Password { get; }
		string ApiKey { get; }
	}
}