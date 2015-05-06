using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.QualifyLead
{
	public interface IFnsQlCreditReport
	{
		[DataMember]
		long CreditReportID { get; set; }
		[DataMember]
		long LeadId { get; set; }
		[DataMember]
		long? AccountId { get; set; }
		[DataMember]
		string BureauId { get; set; }
		[DataMember]
		int SeasonId { get; set; }
		[DataMember]
		string CreditReportVendorId { get; set; }
		[DataMember]
		long? CreditReportVendorAbaraId { get; set; }
		[DataMember]
		long? CreditReportVendorMicrobiltId { get; set; }
		[DataMember]
		long? CreditReportVendorEasyAccessId { get; set; }
		[DataMember]
		long? CreditReportVendorManualId { get; set; }
		[DataMember]
		string BureauName { get; set; }
		[DataMember]
		string DOB { get; set; }
		[DataMember]
		string SSN { get; set; }
		[DataMember]
		string Phone { get; set; }
		[DataMember]
		int PhoneStatus { get; set; }
		[DataMember]
		bool AnyError { get; set; }
		[DataMember]
		string Messages { get; set; }
		[DataMember]
		bool ReportFound { get; set; }
		[DataMember]
		int Score { get; set; }
		[DataMember]
		bool IsScored { get; set; }
		[DataMember]
		bool IsHit { get; set; }
		[DataMember]
		string CreditGroup { get; set; }
		[DataMember]
		string TakeOverFromCompanyName { get; set; }
	}
}
