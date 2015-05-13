using SOS.Data.HumanResource;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.Lib.Core.CreditReportService;

namespace SOS.FunctionalServices.Models.QualifyLead
{
	public class FnsQlCreditReport : IFnsQlCreditReport
	{
		#region .ctor

		public FnsQlCreditReport(IWSCreditReportInfo crInfo,QL_CreditReport report, RU_Season season = null, MS_LeadTakeOver leadTakeOver = null)
		{
			if (crInfo.CreditReportID != report.CreditReportID)
			{
				throw new System.Exception("CreditReportIDs don't match");
			}

			BureauName = crInfo.BureauName;
			LeadId = crInfo.LeadId;
			AccountId = crInfo.AccountId;
			DOB = crInfo.DOB;
			SSN = crInfo.SSN;
			Phone = crInfo.Phone;
			PhoneStatus = crInfo.PhoneStatus;
			AnyError = crInfo.AnyError;
			Messages = crInfo.Messages;
			ReportFound = crInfo.ReportFound;
			CreditGroup = crInfo.ScoreGroup.ToString();

			CreditReportID = report.CreditReportID;
			LeadId = report.LeadId;
			BureauId = report.BureauId;
			SeasonId = report.SeasonId;
			CreditReportVendorId = report.CreditReportVendorId;
			CreditReportVendorAbaraId = report.CreditReportVendorAbaraId;
			CreditReportVendorMicrobiltId = report.CreditReportVendorMicrobiltId;
			CreditReportVendorEasyAccessId = report.CreditReportVendorEasyAccessId;
			CreditReportVendorManualId = report.CreditReportVendorManualId;
			Score = report.Score;
			IsScored = report.IsScored;
			IsHit = report.IsHit;

			if (season != null)
			{
				SeasonId = season.SeasonID;
			}
			if (leadTakeOver != null)
				TakeOverFromCompanyName = leadTakeOver.AlarmCompany.AlarmCompanyName;
		}
		#endregion .ctor

		#region Properties
		public long CreditReportID { get; set; }
		public long LeadId { get; set; }
		public long? AccountId { get; set; }
		public string BureauId { get; set; }
		public int SeasonId { get; set; }
		public string CreditReportVendorId { get; set; }
		public long? CreditReportVendorAbaraId { get; set; }
		public long? CreditReportVendorMicrobiltId { get; set; }
		public long? CreditReportVendorEasyAccessId { get; set; }
		public long? CreditReportVendorManualId { get; set; }
		public string BureauName { get; set; }
		public string DOB { get; set; }
		public string SSN { get; set; }
		public string Phone { get; set; }
		public int PhoneStatus { get; set; }
		public bool AnyError { get; set; }
		public string Messages { get; set; }
		public bool ReportFound { get; set; }
		public int Score { get; set; }
		public bool IsScored { get; set; }
		public bool IsHit { get; set; }
		public string CreditGroup { get; set; }
		public string TakeOverFromCompanyName { get; set; }
		#endregion Properties
	}
}
