using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SalesHareReportSrv
{
	public class MsAccountCreditsAndInstalls : IMsAccountCreditsAndInstalls
	{
		public long? LeadID { get; set; }
		public long? OfficeId { get; set; }
		public string OfficeName { get; set; }
		public int? TeamID { get; set; }
		public string TeamName { get; set; }
		public string SalesRepID { get; set; }
		public string SalesRepName { get; set; }
		public string Active { get; set; }
		public int? NumInstalls { get; set; }
		public int? NumCredits { get; set; }
		public DateTime? InstallDate { get; set; }
		public DateTime? CreditDate { get; set; }
	}

	public interface IMsAccountCreditsAndInstalls
	{
		[DataMember]
		long? LeadID { get; set; }
		[DataMember]
		long? OfficeId { get; set; }
		[DataMember]
		string OfficeName { get; set; }
		[DataMember]
		int? TeamID { get; set; }
		[DataMember]
		string TeamName { get; set; }
		[DataMember]
		string SalesRepID { get; set; }
		[DataMember]
		string SalesRepName { get; set; }
		[DataMember]
		string Active { get; set; }
		[DataMember]
		int? NumInstalls { get; set; }
		[DataMember]
		int? NumCredits { get; set; }
		[DataMember]
		DateTime? InstallDate { get; set; }
		[DataMember]
		DateTime? CreditDate { get; set; }
		
	}
}
