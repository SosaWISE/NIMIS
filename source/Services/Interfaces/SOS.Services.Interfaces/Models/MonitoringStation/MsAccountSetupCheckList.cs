using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountSetupCheckList : IMsAccountSetupCheckList
	{
		public long AccountID { get; set; }
		public bool Qualify { get; set; }
		public DateTime? QualifyDate { get; set; }

		public bool SalesInfo { get; set; }
		public DateTime? SalesInfoDate { get; set; }

		public bool PreSurvey { get; set; }
		public DateTime? PreSurveyDate { get; set; }

		public bool IndustryNumbers { get; set; }
		public DateTime? IndustryNumbersDate { get; set; }

		public bool EmergencyContacts { get; set; }
		public DateTime? EmergencyContactsDate { get; set; }

		public bool SystemDetails { get; set; }
		public DateTime? SystemDetailsDate { get; set; }

		public bool RegisterCell { get; set; }
		public DateTime? RegisterCellDate { get; set; }

		public bool SystemTest { get; set; }
		public DateTime? SystemTestDate { get; set; }

		public bool TechInspection { get; set; }
		public DateTime? TechInspectionDate { get; set; }

		public bool PostSurvey { get; set; }
		public DateTime? PostSurveyDate { get; set; }

		public bool InitialPayment { get; set; }
		public DateTime? InitialPaymentDate { get; set; }

		public bool SubmitAccountOnline { get; set; }
		public DateTime? SubmitAccountOnlineDate { get; set; }

	}

	public interface IMsAccountSetupCheckList
	{
		[DataMember]
		long AccountID { get; set; }

		[DataMember]
		bool Qualify { get; set; }

		[DataMember]
		DateTime? QualifyDate { get; set; }

		[DataMember]
		bool SalesInfo { get; set; }

		[DataMember]
		DateTime? SalesInfoDate { get; set; }

		[DataMember]
		bool PreSurvey { get; set; }

		[DataMember]
		DateTime? PreSurveyDate { get; set; }

		[DataMember]
		bool IndustryNumbers { get; set; }

		[DataMember]
		DateTime? IndustryNumbersDate { get; set; }

		[DataMember]
		bool EmergencyContacts { get; set; }

		[DataMember]
		DateTime? EmergencyContactsDate { get; set; }

		[DataMember]
		bool SystemDetails { get; set; }

		[DataMember]
		DateTime? SystemDetailsDate { get; set; }

		[DataMember]
		bool RegisterCell { get; set; }

		[DataMember]
		DateTime? RegisterCellDate { get; set; }

		[DataMember]
		bool SystemTest { get; set; }

		[DataMember]
		DateTime? SystemTestDate { get; set; }

		[DataMember]
		bool TechInspection { get; set; }

		[DataMember]
		DateTime? TechInspectionDate { get; set; }

		[DataMember]
		bool PostSurvey { get; set; }

		[DataMember]
		DateTime? PostSurveyDate { get; set; }

		[DataMember]
		bool InitialPayment { get; set; }

		[DataMember]
		DateTime? InitialPaymentDate { get; set; }

		[DataMember]
		bool SubmitAccountOnline { get; set; }

		[DataMember]
		DateTime? SubmitAccountOnlineDate { get; set; }
	}
}