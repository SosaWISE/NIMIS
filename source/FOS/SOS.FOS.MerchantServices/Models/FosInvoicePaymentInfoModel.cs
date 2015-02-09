using SOS.Data.SosCrm;
using SOS.FOS.MerchantServices.Interfaces;

namespace SOS.FOS.MerchantServices.Models
{
	public class FosInvoicePaymentInfoModel : IFosInvoicePaymentInfoModel
	{
		#region .ctor

		public FosInvoicePaymentInfoModel(AE_Invoice oInvoice, AE_Payment oPayment)
		{
			Invoice = oInvoice;
			Payment = oPayment;
		}

		#endregion .ctor

		#region Implementation of IFosInvoicePaymentInfoModel

		public AE_Invoice Invoice { get; private set; }
		public AE_Payment Payment { get; private set; }

		/** Credit Card info. */
		public string NameOnCard { get; set; }
		public string CardNumber { get; set; }
		public string Cvv { get; set; }
		public string ExpMonthYear { get; set; }

		/** Bank Information .*/
		public string BankABACode { get; set; }
		public string BankAccountNumber { get; set; }
		public BankAccountType BankAccountType { get; set; }
		public string BankName { get; set; }
		public string BankAccountName { get; set; }
		public EcheckType EcheckType { get; set; }
		public string BankCheckNumber { get; set; }

		public decimal Amount { get; set; }
		public string Description { get; set; }

		#endregion Implementation of IFosInvoicePaymentInfoModel
	}
}