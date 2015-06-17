using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountSetupCheckList : IMsAccountSetupCheckList
	{
		public long AccountID { get; set; }
		public DateTime? Qualify { get; set; }
		public DateTime? SalesInfo { get; set; }
		public DateTime? PreSurvey { get; set; }
		public DateTime? IndustryNumbers { get; set; }
		public DateTime? EmergencyContacts { get; set; }
		public DateTime? SystemDetails { get; set; }
		public DateTime? RegisterCell { get; set; }
		public DateTime? SystemTest { get; set; }
		public DateTime? TechInspection { get; set; }
		public DateTime? PostSurvey { get; set; }
		public DateTime? InitialPayment { get; set; }
		public DateTime? SubmitAccountOnline { get; set; }
	}

	public interface IMsAccountSetupCheckList
	{
		[DataMember]
		long AccountID { get; set; }
		[DataMember]
		DateTime? Qualify { get; set; }
		[DataMember]
		DateTime? SalesInfo { get; set; }

		[DataMember]
		DateTime? PreSurvey { get; set; }

		[DataMember]
		DateTime? IndustryNumbers { get; set; }

		[DataMember]
		DateTime? EmergencyContacts { get; set; }

		[DataMember]
		DateTime? SystemDetails { get; set; }

		[DataMember]
		DateTime? RegisterCell { get; set; }

		[DataMember]
		DateTime? SystemTest { get; set; }

		[DataMember]
		DateTime? TechInspection { get; set; }

		[DataMember]
		DateTime? PostSurvey { get; set; }

		[DataMember]
		DateTime? InitialPayment { get; set; }

		[DataMember]
		DateTime? SubmitAccountOnline { get; set; }
	}
}
