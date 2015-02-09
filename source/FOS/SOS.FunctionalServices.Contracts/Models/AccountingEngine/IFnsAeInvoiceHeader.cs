using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeInvoiceHeader
	{
		[DataMember]
		long InvoiceID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string InvoiceTypeId { get; set; }

		[DataMember]
		int? ContractId { get; set; }

		[DataMember]
		int? TaxScheduleId { get; set; }

		[DataMember]
		int? PaymentTermId { get; set; }

		[DataMember]
		string DocDate { get; set; }

		[DataMember]
		string PostedDate { get; set; }

		[DataMember]
		string DueDate { get; set; }

		[DataMember]
		string GLPostDate { get; set; }

		[DataMember]
		decimal? CurrentTransactionAmount { get; set; }

		[DataMember]
		decimal SalesAmount { get; set; }

		[DataMember]
		decimal OriginalTransactionAmount { get; set; }

		[DataMember]
		decimal CostAmount { get; set; }

		[DataMember]
		decimal TaxAmount { get; set; }

		[DataMember]
		bool IsActive { get; set; }

		[DataMember]
		bool IsDelete { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		//[DataMember]
		//string BarcodeId { get; set; }
	}
}