namespace SOS.FunctionalServices.Contracts.Models.Merchant
{
	/// <summary>
	/// The type of BankAccount to be used
	/// </summary>
	public enum BankAccountType
	{
		/// <summary>
		/// Checking Account
		/// </summary>
		Checking,
		/// <summary>
		/// Business Checking account
		/// </summary>
		BusinessChecking,
		/// <summary>
		/// Savings Account
		/// </summary>
		Savings
	}
	/// <summary>
	/// The type of eCheck transaction
	/// </summary>
	public enum EcheckType
	{
		/// <summary>
		/// Accounts Receivable Conversion
		/// </summary>
		ARC,
		/// <summary>
		/// Back Office Conversion
		/// </summary>
		BOC,
		/// <summary>
		/// Cash Concentration or Disbursement
		/// </summary>
		CCD,
		/// <summary>
		/// Prearranged Payment and Deposit Entry
		/// </summary>
		PPD,
		/// <summary>
		/// Telephone-Initiated Entry
		/// </summary>
		TEL,
		/// <summary>
		/// Internet-Initiated Entry
		/// </summary>
		WEB
	}

	public interface IFnsInvoicePaymentInfoModel
	{

		#region Properties

		object AeInvoice { get; }
		object AePayment { get; }

		/** Credit Card Info. */
		string NameOnCard { get; set; }
		string CardNumber { get; set; }
		string ExpMonthYear { get; set; }
		decimal Amount { get; set; }
		string Description { get; set; }
		string Cvv { get; set; }

		/** Bank Information .*/
		string BankABACode { get; set; }
		string BankAccountNumber { get; set; }
		BankAccountType BankAccountType { get; set; }
		string BankName { get; set; }
		string BankAccountName { get; set; }
		EcheckType EcheckType { get; set; }
		string BankCheckNumber { get; set; }

		#endregion Properties

	}
}