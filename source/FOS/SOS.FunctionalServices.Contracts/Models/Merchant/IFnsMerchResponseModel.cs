﻿namespace SOS.FunctionalServices.Contracts.Models.Merchant
{
	public interface IFnsMerchResponseModel
	{
		#region Properties

		decimal Amount { get; set; }
		bool Approved { get; set; }
		string AuthorizationCode { get; set; }
		string CardNumber { get; set; }
		string InvoiceNumber { get; set; }
		string Message { get; set; }
		string ResponseCode { get; set; }
		string TransactionID { get; set; }

		#endregion Properties
	}
}