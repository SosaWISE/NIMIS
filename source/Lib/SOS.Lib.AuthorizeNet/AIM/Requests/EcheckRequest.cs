﻿using System.Globalization;
using SOS.Lib.AuthorizeNet.Utility;

namespace SOS.Lib.AuthorizeNet.AIM.Requests
{
	/// <summary>
	/// Receives a Check and processes it.
	/// </summary>
	public class EcheckRequest : GatewayRequest
	{

		/// <summary>
		/// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="bankABACode">The valid routing number of the customer’s bank</param>
		/// <param name="bankAccountNumber">The customer’s valid bank account number</param>
		/// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
		/// <param name="bankName">The name of the bank that holds the customer’s account</param>
		/// <param name="acctName">The name associated with the bank account</param>
		/// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
		public EcheckRequest(decimal amount, string bankABACode, string bankAccountNumber,
			BankAccountType acctType, string bankName, string acctName, string bankCheckNumber) :
			this(EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName, bankCheckNumber) { }


		/// <summary>
		/// Creates an ECheck transaction request for use with the AIM gateway
		/// </summary>
		/// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
		/// <param name="amount"></param>
		/// <param name="bankABACode">The valid routing number of the customer’s bank</param>
		/// <param name="bankAccountNumber">The customer’s valid bank account number</param>
		/// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
		/// <param name="bankName">The name of the bank that holds the customer’s account</param>
		/// <param name="acctName">The name associated with the bank account</param>
		/// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
		public EcheckRequest(EcheckType type, decimal amount, string bankABACode, string bankAccountNumber,
			BankAccountType acctType, string bankName, string acctName, string bankCheckNumber)
		{

			Queue(ApiFields.Method, "ECHECK");
			EcheckType = type;
			BankABACode = bankABACode;
			BankName = bankName;
			BankAccountName = acctName;
			BankAccountNumber = bankAccountNumber;
			BankAccountType = acctType;
			BankCheckNumber = bankCheckNumber;
			Amount = amount.ToString(CultureInfo.InvariantCulture);

		}

		/// <summary>
		/// Sets the eCheck request as a recurring payment
		/// </summary>
		/// <returns></returns>
		public EcheckRequest AsRecurring()
		{
			RecurringBilling = "Y";
			return this;
		}

	}
}
