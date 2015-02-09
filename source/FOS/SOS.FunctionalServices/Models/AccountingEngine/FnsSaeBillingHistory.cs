using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsSaeBillingHistory : IFnsSaeBillingHistory
	{
		#region .ctor

		public FnsSaeBillingHistory(SAE_BillingHistoryView item)
		{
            //BillingHistoryID = item.BillingHistoryID;
            CustomerMasterFileId = item.CustomerMasterFileId;
            BillingType = item.BillingType;
            BillingDate = item.BillingDate;
            BillingNumber = item.BillingNumber;
            BillingAmount = item.BillingAmount;
            //AccountId = item.AccountId;
            //BillingDate = item.BillingDate;
            //InvoiceNumber = item.InvoiceNumber;
            //Amount = item.Amount;
            //PaymentDate = item.PaymentDate;
            //PaymentNumber = item.PaymentNumber;
            //PaymentAmount = item.PaymentAmount;
            //AmountRemain = item.AmountRemain;
		}

		#endregion .ctor

        #region Properties
        //public long BillingHistoryID { get; private set; }
        public long CustomerMasterFileId { get; private set; }
        public string BillingType { get; private set; }
        public DateTime? BillingDate { get; private set; }
        public string BillingNumber { get; private set; }
        public decimal? BillingAmount { get; private set; }
        //public long? AccountId { get; private set; }
        //public DateTime BillingDate { get; private set; }
        //public string InvoiceNumber { get; private set; }
        //public decimal Amount { get; private set; }
        //public DateTime? PaymentDate { get; private set; }
        //public string PaymentNumber { get; private set; }
        //public decimal? PaymentAmount { get; private set; }
        //public decimal? AmountRemain { get; private set; }
        #endregion Properties
	}
}
