using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
    public class AeInvoiceItem : IAeInvoiceItem
    {
        #region Properties
        public long InvoiceItemID { get; set; }
        public long InvoiceId { get; set; }
		public string ProductBarcodeId { get; set; }
        public string ItemId { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
        public string TaxOptionId { get; set; }
        public short Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal? PriceWithTax { get; set; }
		public decimal SystemPoints { get; set; }
		public string SalesmanID { get; set; }
		public string TechnicianID { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

	    #endregion Properties
    }

    public interface IAeInvoiceItem
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