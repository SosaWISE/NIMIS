using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsSaeBillingHistory
	{
        long CustomerMasterFileId { get; }
        string BillingType { get;  }
        DateTime? BillingDate { get;  }
        string BillingNumber { get;  }
        decimal? BillingAmount { get;  }
 
        //[DataMember]
        //long BillingHistoryID { get; }

        //[DataMember]
        //long CustomerMasterFileId { get; }

        //[DataMember]
        //long? AccountId { get; }

        //[DataMember]
        //DateTime BillingDate { get; }

        //[DataMember]
        //string InvoiceNumber { get; }

        //[DataMember]
        //Decimal Amount { get; }

        //[DataMember]
        //DateTime? PaymentDate { get; }

        //[DataMember]
        //string PaymentNumber { get; }

        //[DataMember]
        //decimal? PaymentAmount { get; }

        //[DataMember]
        //decimal? AmountRemain { get; }
	}
}