using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class SaeBillingInfoSummary : ISaeBillingInfoSummary
	{
		#region Properties
		public long SummaryID { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long AccountId { get; set; }
		public string AccountName { get; set; }
		public string AccountDesc { get; set; }
		public decimal? AmountDue { get; set; }
		public DateTime? DueDate { get; set; }
		public short? NumberOfUnites { get; set; }
		#endregion Properties
	}


	public interface ISaeBillingInfoSummary
	{
		[DataMember]
		long SummaryID { get; set; }

		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		long AccountId { get; set; }
		
		[DataMember]
		string AccountName { get; set; }

		[DataMember]
		string AccountDesc { get; set; }

		[DataMember]
		decimal? AmountDue { get; set; }

		[DataMember]
		DateTime? DueDate { get; set; }

		[DataMember]
		short? NumberOfUnites { get; set; }
	}
}
