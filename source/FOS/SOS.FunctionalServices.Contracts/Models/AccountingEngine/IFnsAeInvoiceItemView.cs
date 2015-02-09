using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeInvoiceItemView
	{
		[DataMember]
		long InvoiceItemID { get; set; }

		[DataMember]
		long InvoiceId { get; set; }

		[DataMember]
		string ItemId { get; set; }

		[DataMember]
		string ProductBarcodeId { get; set; }

		[DataMember]
		string ItemSKU { get; set; }

		[DataMember]
		string ItemDesc { get; set; }

		[DataMember]
		string TaxOptionId { get; set; }

		[DataMember]
		short Qty { get; set; }

		[DataMember]
		decimal Cost { get; set; }

		[DataMember]
		decimal RetailPrice { get; set; }

		[DataMember]
		decimal? PriceWithTax { get; set; }

		[DataMember]
		decimal SystemPoints { get; set; }
		
		[DataMember]
		string SalesmanID { get; set; }

		[DataMember]
		string TechnicianID { get; set; }
			
		[DataMember]
		DateTime ModifiedOn { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}
}