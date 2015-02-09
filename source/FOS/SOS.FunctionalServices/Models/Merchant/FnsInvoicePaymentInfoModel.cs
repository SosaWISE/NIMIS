using SOS.Data.SosCrm;
using SOS.FOS.MerchantServices.Models;
using SOS.FunctionalServices.Contracts.Models.Merchant;

namespace SOS.FunctionalServices.Models.Merchant
{
	public class FnsInvoicePaymentInfoModel : IFnsInvoicePaymentInfoModel
	{
		#region .ctor

		public FnsInvoicePaymentInfoModel(AE_Invoice oInvoice, AE_Payment oPayment)
		{
			AeInvoice = oInvoice;
			AePayment = oPayment;
		}

		#endregion .ctor

		#region Methods
		public FosInvoicePaymentInfoModel CastToFos()
		{
			/* Init. */
			var oFos = new FosInvoicePaymentInfoModel(Invoice, Payment)
			{
				NameOnCard = NameOnCard
				, CardNumber = CardNumber
				, ExpMonthYear = ExpMonthYear
				, Amount = Amount
				, Description = Description
				, Cvv = Cvv
				, BankABACode = BankABACode
				, BankAccountName = BankAccountName
				, BankAccountNumber = BankAccountNumber
				, BankAccountType = BankAccountTypeCastToFos(BankAccountType)
				, BankName = BankName
				, EcheckType = EcheckTypeCastToFos(EcheckType)
				, BankCheckNumber = BankCheckNumber
			};

			/** Return result. */
			return oFos;
		}
		private FOS.MerchantServices.Interfaces.BankAccountType BankAccountTypeCastToFos(BankAccountType eBankAccountType)
		{
			switch (eBankAccountType)
			{
				case BankAccountType.Checking:
					return FOS.MerchantServices.Interfaces.BankAccountType.Checking;
				case BankAccountType.BusinessChecking:
					return FOS.MerchantServices.Interfaces.BankAccountType.BusinessChecking;
				case BankAccountType.Savings:
					return FOS.MerchantServices.Interfaces.BankAccountType.Savings;
				default:
					return FOS.MerchantServices.Interfaces.BankAccountType.Checking;
			}
		}

		private FOS.MerchantServices.Interfaces.EcheckType EcheckTypeCastToFos(EcheckType eEcheckType)
		{
			switch (eEcheckType)
			{
				case EcheckType.CCD:
					return FOS.MerchantServices.Interfaces.EcheckType.CCD;
				case EcheckType.BOC:
					return FOS.MerchantServices.Interfaces.EcheckType.BOC;
				case EcheckType.ARC:
					return FOS.MerchantServices.Interfaces.EcheckType.ARC;
				case EcheckType.WEB:
					return FOS.MerchantServices.Interfaces.EcheckType.WEB;
				case EcheckType.TEL:
					return FOS.MerchantServices.Interfaces.EcheckType.TEL;
				case EcheckType.PPD:
					return FOS.MerchantServices.Interfaces.EcheckType.PPD;
				default:
					return FOS.MerchantServices.Interfaces.EcheckType.CCD;
			}
		}

		#endregion Methods

		#region Implementation of IFnsInvoicePaymentInfoModel

		public object AeInvoice { get; private set; }
		public AE_Invoice Invoice { get { return (AE_Invoice) AeInvoice; } }
		public object AePayment { get; private set; }
		public AE_Payment Payment { get { return (AE_Payment) AePayment; } }
		public string NameOnCard { get; set; }
		public string CardNumber { get; set; }
		public string ExpMonthYear { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public string Cvv { get; set; }
		public string BankABACode { get; set; }
		public string BankAccountNumber { get; set; }
		public BankAccountType BankAccountType { get; set; }
		public string BankName { get; set; }
		public string BankAccountName { get; set; }
		public EcheckType EcheckType { get; set; }
		public string BankCheckNumber { get; set; }

		#endregion Implementation of IFnsInvoicePaymentInfoModel
	}
}