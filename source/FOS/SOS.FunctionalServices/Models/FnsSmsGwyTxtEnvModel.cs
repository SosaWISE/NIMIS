using SOS.FunctionalServices.Contracts.Models.SmsGateways;

namespace SOS.FunctionalServices.Models
{
	public class FnsSmsGwyTxtEnvModel : IFnsSmsGwyTxtEnvModel
	{
		#region .ctor

		public FnsSmsGwyTxtEnvModel()
		{}

		#endregion .ctor

		#region Implementation of IFnsSmsGwyTxtEnvModel

		public string Title { get; private set; }
		public string Code { get; private set; }
		public string ShortCode { get; private set; }
		public string Message { get; private set; }
		public string Phone { get; private set; }
		public string Carrier { get; private set; }
		public string Keyword { get; private set; }
		public string GroupName { get; private set; }
		public string CustomTicket { get; private set; }
		public string DefaultKeyword { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public string ApiKey { get; private set; }

		#endregion
	}
}
