using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsSaeBillingInfoSummary
	{
		[DataMember]
		long SummaryID { get; }

		[DataMember]
		long CustomerMasterFileId { get; }

		[DataMember]
		long AccountId { get; }

		[DataMember]
		string AccountName { get; }

		[DataMember]
		string AccountDescription { get; }

		[DataMember]
		Decimal? AmountDue { get; }

		[DataMember]
		DateTime? DueDate { get; }

		[DataMember]
		short? NumberOfUnites { get; }
	}
}