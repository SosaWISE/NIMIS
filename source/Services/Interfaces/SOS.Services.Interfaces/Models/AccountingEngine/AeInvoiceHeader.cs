using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
    public class AeInvoiceHeader : IAeInvoiceHeader
    {
        #region Properties
        public long InvoiceID { get; set; }
        public long AccountId { get; set; }
        public string InvoiceTypeId { get; set; }
        public int? ContractId { get; set; }
        public int? TaxScheduleId { get; set; }
        public int? PaymentTermId { get; set; }
        public string DocDate { get; set; }
		public string PostedDate { get; set; }
		public string DueDate { get; set; }
        public string GLPostDate { get; set; }
        public decimal? CurrentTransactionAmount { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal OriginalTransactionAmount { get; set; }
        public decimal CostAmount { get; set; }
        public decimal TaxAmount { get; set; }
		//public string BarcodeId { get; set; }

        #endregion Properties
    }

    public interface IAeInvoiceHeader
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

		//[DataMember]
		//string BarcodeId { get; set; }
    }
}