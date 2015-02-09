namespace SOS.Lib.Core.CreditReportService
{
	public interface IWSCreditReportInfo
	{
		long CreditReportID { get; set; }
		long LeadId { get; set; }
		long? AccountId { get; set; }
		string BureauName{ get; set; }
		string DOB{ get; set; }
		string SSN{ get; set; }
		int Score{ get; set; }
		bool ScoreFound{ get; set; }
		string Phone{ get; set; }
		int PhoneStatus{ get; set; }
		bool AnyError{ get; set; }
		string Messages{ get; set; }
		bool ReportFound { get; set; }
		CreditScoreGroup ScoreGroup { get; set; }
	}

	public enum CreditScoreGroup
	{
		Excellent = 0,
		Good,
		Sub,
		Poor,
		NotFound
	}
}