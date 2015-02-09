using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsSaeBillingInfoSummary : IFnsSaeBillingInfoSummary
	{
		#region .ctor

		public FnsSaeBillingInfoSummary(SAE_BillingInfoSummaryView bis)
		{
			SummaryID = bis.SummaryID;
			CustomerMasterFileId = bis.CustomerMasterFileId;
			AccountId = bis.AccountId;
			AccountName = bis.AccountName;
			AccountDescription = bis.AccountDesc;
			AmountDue = bis.AmountDue;
			DueDate = bis.DueDate;
			NumberOfUnites = bis.NumberOfUnites;
		}

		#endregion .ctor

		#region Properties
	
		public long SummaryID { get; private set; }
		public long CustomerMasterFileId { get; private set; }
		public long AccountId { get; private set; }
		public string AccountName { get; private set; }
		public string AccountDescription { get; private set; }
		public decimal? AmountDue { get; private set; }
		public DateTime? DueDate { get; private set; }

		public short? NumberOfUnites { get; private set; }

		#endregion Properties
	}
}
