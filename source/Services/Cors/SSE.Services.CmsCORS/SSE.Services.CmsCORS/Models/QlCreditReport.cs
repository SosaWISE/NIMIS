using System.Runtime.Serialization;

namespace SSE.Services.CmsCORS.Models
{
	public class QlCreditReport
	{
		[DataMember]
		public long CreditReportID { get; set; }
		[DataMember]
		public long CustomerMasterFileId { get; set; }
		[DataMember]
		public long LeadId { get; set; }
		[DataMember]
		public long? AccountId { get; set; }
		[DataMember]
		public string BureauId { get; set; }
		[DataMember]
		public string BureauName { get; set; }
		[DataMember]
		public int SeasonId { get; set; }
		[DataMember]
		public string CreditReportVendorid { get; set; }
		[DataMember]
		public long? CreditReportVendorAbaraId { get; set; }
		[DataMember]
		public long? CreditReportVendorMicrobiltId { get; set; }
		[DataMember]
		public int CreditVendorEasyAccessId { get; set; }
		[DataMember]
		public long? CreditReportVendorManualId { get; set; }
		[DataMember]
		public int Score { get; set; }
		[DataMember]
		public bool IsScored { get; set; }
		[DataMember]
		public bool IsHit { get; set; }
		[DataMember]
		public string CreditGroup { get; set; }
	}
}