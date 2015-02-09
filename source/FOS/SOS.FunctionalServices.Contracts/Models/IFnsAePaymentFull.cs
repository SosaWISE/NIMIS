using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAePaymentFull
	{
		[DataMember]
		decimal Amount { get; set; }

		[DataMember]
		bool Approved { get; set; }

		[DataMember]
		string AuthorizationCode { get; set; }

		[DataMember]
		string CardNumber { get; set; }

		[DataMember]
		string InvoiceNumber { get; set; }

		[DataMember]
		long InvoiceID { get; set; }

		[DataMember]
		long PaymentID { get; set; }

		[DataMember]
		string PaymentMethod { get; set; }

		[DataMember]
		string PurchaseMessageDescription { get; set; }

		[DataMember]
		string ResponseCode { get; set; }

		[DataMember]
		string TransactionID { get; set; }

		[DataMember]
		decimal OriginalTransactionAmount { get; set; }

		[DataMember]
		decimal? CurrentTransactionAmount { get; set; }

		[DataMember]
		decimal TotalSum { get; set; }

		[DataMember]
		decimal TotalDiscount { get; set; }

		[DataMember]
		decimal TotalSub { get; set; }

		[DataMember]
		decimal SalesAmount { get; set; }

		[DataMember]
		decimal TaxAmount { get; set; }

		[DataMember]
		long InvoicePaymentJoinID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		bool? TransactionSuccess { get; set; }

		[DataMember]
		DateTime DocDate { get; set; }

		[DataMember]
		DateTime? PostedDate { get; set; }

		[DataMember]
		decimal? ActualTransactionAmount { get; set; }

		[DataMember]
		List<IFnsAePaymentItem> ItemsList { get; set; }

		[DataMember]
		IFnsAeCustomerInfo BillTo { get; set; }

		[DataMember]
		IFnsAeCustomerInfo SoldTo { get; set; }
	}

	public interface IFnsAeCustomerInfo
	{
		[DataMember]
		long CustomerID { get; set; }

		[DataMember]
		string CustomerName { get; set; }

		[DataMember]
		string AddressLine1 { get; set; }

		[DataMember]
		string AddressLine2 { get; set; }

		[DataMember]
		string Phone { get; set; }
	}

	public interface IFnsAePaymentItem
	{
		[DataMember]
		int Qty { get; set; }

		[DataMember]
		string Skw { get; set; }

		[DataMember]
		string LineDescription { get; set; }

		[DataMember]
		decimal UnitPrice { get; set; }

		[DataMember]
		decimal DiscountPrice { get; set; }

		[DataMember]
		decimal TotalLine { get; set; }

	}
}