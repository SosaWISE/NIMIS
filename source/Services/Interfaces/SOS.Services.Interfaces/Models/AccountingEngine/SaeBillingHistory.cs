using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class SaeBillingHistory : ISaeBillingHistory
	{
		#region Properties
        public long CustomerMasterFileId { get; set; }
        public string BillingType { get; set; }
        public DateTime? BillingDate { get; set; }
        public string BillingNumber { get; set; }
        public decimal? BillingAmount { get; set; }
        //public long BillingHistoryID { get; set; }
        //public long CustomerMasterFileId { get; set; }
        //public long? AccountId { get; set; }
        //public DateTime BillingDate { get; set; }
        //public string InvoiceNumber { get; set; }
        //public decimal Amount { get; set; }
        //public DateTime? PaymentDate { get; set; }
        //public string PaymentNumber { get; set; }
        //public decimal? PaymentAmount { get; set; }
        //public decimal? AmountRemain { get; set; }

		#endregion Properties
	}

	public interface ISaeBillingHistory
	{
        //[DataMember]
        //long BillingHistoryID { get; set; }

        [DataMember]
        long CustomerMasterFileId { get; }
        string BillingType { get; }
        DateTime? BillingDate { get; }
        string BillingNumber { get; }
        decimal? BillingAmount { get; }
        //[DataMember]
        //long? AccountId { get; set; }

        //[DataMember]
        //DateTime BillingDate { get; set; }

        //[DataMember]
        //string InvoiceNumber { get; set; }

        //[DataMember]
        //decimal Amount { get; set; }

        //[DataMember]
        //DateTime? PaymentDate { get; set; }

        //[DataMember]
        //string PaymentNumber { get; set; }

        //[DataMember]
        //decimal? PaymentAmount { get; set; }

        //[DataMember]
        //decimal? AmountRemain { get; set; }
	}
}
