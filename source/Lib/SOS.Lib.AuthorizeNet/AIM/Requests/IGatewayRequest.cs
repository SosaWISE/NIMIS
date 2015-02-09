namespace SOS.Lib.AuthorizeNet.AIM.Requests
{
	#region Enums

	/// <summary>
	/// The type of API Request being performed
	/// </summary>
	public enum RequestAction
	{
		/// <summary>
		/// Credit Card authorization
		/// </summary>
		Authorize,

		/// <summary>
		/// Settlement of a previously authorized transaction
		/// </summary>
		Capture,

		/// <summary>
		/// An authorization and capture all in one
		/// </summary>
		AuthorizeAndCapture,

		/// <summary>
		/// A Credit
		/// </summary>
		Credit,

		/// <summary>
		/// Voiding of a previously authorized transaction
		/// </summary>
		Void,

		/// <summary>
		/// Capturing of a prior authorization
		/// </summary>
		PriorAuthCapture,

		/// <summary>
		/// Issue a credit for a transaction not based with the API
		/// </summary>
		UnlinkedCredit
	}

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

	#endregion Enums

	public interface IGatewayRequest
	{
		IGatewayRequest AddCardCode(string cardCode);
		IGatewayRequest AddCustomer(string id, string first, string last, string address, string state, string zip);
		IGatewayRequest AddDuty(decimal amount, string name, string description);
		IGatewayRequest AddDuty(decimal amount);
		IGatewayRequest AddFraudCheck();
		IGatewayRequest AddFraudCheck(string customerIP);
		IGatewayRequest AddFreight(decimal amount, string name, string description);
		IGatewayRequest AddFreight(decimal amount);
		IGatewayRequest AddInvoice(string invoiceNumber);
		IGatewayRequest AddLineItem(string itemID, string name, string description, int quantity, decimal price, bool taxable);
		IGatewayRequest AddMerchantValue(string key, string value);
		string Address { get; set; }
		IGatewayRequest AddShipping(string id, string first, string last, string address, string state, string zip);
		IGatewayRequest AddTax(decimal amount, string name, string description);
		IGatewayRequest AddTax(decimal amount);
		string AllowPartialAuth { get; set; }
		string Amount { get; set; }
		RequestAction ApiAction { get; }
		string AuthCode { get; set; }
		string AuthenticationIndicator { get; set; }
		string BankABACode { get; set; }
		string BankAccountName { get; set; }
		string BankAccountNumber { get; set; }
		BankAccountType BankAccountType { get; set; }
		string BankCheckNumber { get; set; }
		string BankName { get; set; }
		string CardCode { get; set; }
		string CardholderAuthenticationValue { get; set; }
		string CardNum { get; set; }
		string City { get; set; }
		string Company { get; set; }
		string Country { get; set; }
		string CustId { get; set; }
		string CustomerIp { get; set; }
		string DelimChar { get; set; }
		string DelimData { get; set; }
		string Description { get; set; }
		string DuplicateWindow { get; set; }
		string Duty { get; set; }
		EcheckType EcheckType { get; set; }
		string Email { get; set; }
		string EmailCustomer { get; set; }
		string EncapChar { get; set; }
		string ExpDate { get; set; }
		string Fax { get; set; }
		string FirstName { get; set; }
		string FooterEmailReceipt { get; set; }
		string Freight { get; set; }
		string Get(string key);
		string HeaderEmailReceipt { get; set; }
		string InvoiceNum { get; set; }
		string LastName { get; set; }
		string LineItem { get; set; }
		string Login { get; set; }
		string Method { get; set; }
		string Phone { get; set; }
		string PoNum { get; set; }
		System.Collections.Specialized.NameValueCollection Post { get; set; }
		void Queue(string key, string value);
		string RecurringBilling { get; set; }
		string RelayResponse { get; set; }
		string ShipToAddress { get; set; }
		string ShipToCity { get; set; }
		string ShipToCompany { get; set; }
		string ShipToCountry { get; set; }
		string ShipToFirstName { get; set; }
		string ShipToLastName { get; set; }
		string ShipToState { get; set; }
		string ShipToZip { get; set; }
		string SplitTenderId { get; set; }
		string State { get; set; }
		string Tax { get; set; }
		string TaxExempt { get; set; }
		string TestRequest { get; set; }
		string ToPostString();
		string TranKey { get; set; }
		string TransId { get; set; }
		string Type { get; set; }
		string Version { get; set; }
		string Zip { get; set; }
	}
}