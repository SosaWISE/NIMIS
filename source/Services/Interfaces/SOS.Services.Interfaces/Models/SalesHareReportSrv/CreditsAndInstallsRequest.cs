using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.SalesHareReportSrv
{
	public class CreditsAndInstallsRequest : ICreditsAndInstallsRequest
	{
		public int? OfficeID { get; set; } // Optional
		public string SalesRepId { get; set; } // Optional (for future use)
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	public interface ICreditsAndInstallsRequest
	{
		#region Properties
		[DataMember]
		int? OfficeID { get; set; }
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		DateTime StartDate { get; set; }
		[DataMember]
		DateTime EndDate { get; set; }
		#endregion Properties
	}
}
