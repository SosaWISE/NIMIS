using System.Collections.Generic;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.MerchantServices.Interfaces;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Merchant;

namespace SOS.FunctionalServices.Models.Merchant
{
	public class FnsMerchResponseModel : IFnsMerchResponseModel
	{
		#region .ctor

		public FnsMerchResponseModel(IFosMerchResponseModel oValue)
		{
			Amount = oValue.Amount;
			Approved = oValue.Approved;
			AuthorizationCode = oValue.AuthorizationCode;
			CardNumber = oValue.CardNumber;
			InvoiceNumber = oValue.InvoiceNumber;
			Message = oValue.Message;
			ResponseCode = oValue.ResponseCode;
			TransactionID = oValue.TransactionID;
		}

		#endregion .ctor

		#region Methods

		public IFnsAePaymentFull CastToFnsAePaymentFull(long lPaymentID, AE_Invoice oInvoice, AE_Payment oAePayment, FnsInvoicePaymentInfoModel oFnsInvPayModel, List<FnsAePaymentItem> oFnsAePaymentItems, AE_Customer oCustomer, AE_Customer oCustBilling)
		{
			/** Inits. */
			AE_PaymentFullView oFullView = SosCrmDataContext.Instance.AE_PaymentFullViews.GetByInvoiceIDAndPaymentID(InvoiceID, lPaymentID);
			var oResult = new FnsAePaymentFull
				{
					Amount = Amount
					, Approved = Approved
					, AuthorizationCode = AuthorizationCode
					, PaymentMethod = oAePayment.PaymentType.PaymentTypeName
					, CardNumber = oAePayment.PaymentTypeId.Equals(AE_PaymentType.MetaData.Credit_CardID)
						? string.Format("XXX...{0}", oFnsInvPayModel.CardNumber.Substring(oFnsInvPayModel.CardNumber.Length-4))
						: string.Format(":XXXXX{0}: {1}", oFnsInvPayModel.BankABACode.Substring(4)
							, string.Format("...XX{0}", oFnsInvPayModel.BankAccountNumber.Substring(oFnsInvPayModel.BankAccountNumber.Length - 4)))
					, InvoiceNumber = string.Format("P:{0} (Inv:{1})", InvoiceNumber, oAePayment.PaymentID)
					, InvoiceID = oInvoice.InvoiceID
					, PaymentID = oAePayment.PaymentID
					, PurchaseMessageDescription = Message
					, ResponseCode = ResponseCode
					, TransactionID = TransactionID
					, OriginalTransactionAmount = oFullView.OriginalTransactionAmount
					, CurrentTransactionAmount = oFullView.CurrentTransactionAmount
					, SalesAmount = oFullView.SalesAmount
					, TaxAmount = oFullView.TaxAmount
					, InvoicePaymentJoinID = oFullView.InvoicePaymentJoinID
					, AccountId = oFullView.AccountId
					, TransactionSuccess = oFullView.TransactionSuccess
					, DocDate = oFullView.DocDate
					, PostedDate = oFullView.PostedDate
					, ActualTransactionAmount = oFullView.ActualTransactionAmount
					, SoldTo = new FnsAeCustomerInfo
						{
							CustomerID = oCustomer.CustomerID
							, CustomerName = string.Format("{0} {1}", oCustomer.FirstName, oCustomer.LastName)
							, AddressLine1 = oCustomer.Address.StreetAddress
							, AddressLine2 = string.Format("{0} {1} {2}"
								, oCustomer.Address.City, oCustomer.Address.State.StateAB, oCustomer.Address.PostalCode)
							, Phone = oCustomer.PhoneHome
						}
					, BillTo = new FnsAeCustomerInfo
						{
							CustomerID = oCustBilling.CustomerID
							, CustomerName = string.Format("{0} {1}", oCustBilling.FirstName, oCustBilling.LastName)
							, AddressLine1 = oCustBilling.Address.StreetAddress
							, AddressLine2 = string.Format("{0} {1} {2}"
								, oCustBilling.Address.City, oCustBilling.Address.State.StateAB, oCustBilling.Address.PostalCode)
							, Phone = oCustBilling.PhoneHome
						}
				};

			oResult.ItemsList = new List<IFnsAePaymentItem>(oFnsAePaymentItems);

			/** Return result. */
			return oResult;
		}

		#endregion Methods

		#region Properties

		public long InvoiceID
		{
			get { return long.Parse(InvoiceNumber); }
		}

		#endregion Properties

		#region Implementation of IFnsMerchResponseModel

		public decimal Amount { get; set; }
		public bool Approved { get; set; }
		public string AuthorizationCode { get; set; }
		public string CardNumber { get; set; }
		public string InvoiceNumber { get; set; }
		public string Message { get; set; }
		public string ResponseCode { get; set; }
		public string TransactionID { get; set; }

		#endregion Implementation of IFnsMerchResponseModel
	}
}