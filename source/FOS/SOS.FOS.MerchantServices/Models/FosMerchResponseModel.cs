using SOS.FOS.MerchantServices.Interfaces;

namespace SOS.FOS.MerchantServices.Models
{
	public class FosMerchResponseModel : IFosMerchResponseModel
	{
		public decimal Amount { get; set; }

		public bool Approved { get; set; }

		public string AuthorizationCode { get; set; }

		public string CardNumber { get; set; }

		public string InvoiceNumber { get; set; }

		public string Message { get; set; }

		public string ResponseCode { get; set; }

		public string TransactionID { get; set; }
	}
}
