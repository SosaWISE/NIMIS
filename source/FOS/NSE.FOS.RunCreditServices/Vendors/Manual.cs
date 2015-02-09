using System;
using System.Collections.Generic;
using NSE.FOS.RunCreditServices.Interfaces;
using NSE.Lib.Microbilt;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NSE.FOS.RunCreditServices.Vendors
{
	public class Manual : IVendor
	{
		#region .ctor

		short score;
		bool isScored;
		bool isHit;
		string report;
		public Manual(short score, bool isHit, bool isScored, string report)
		{
			this.score = score;
			this.isScored = isScored;
			this.isHit = isHit;
			this.report = report;
		}

		#endregion // .ctor

		#region Methods

		public bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username,
			ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
		{
			messageList.Add(new WSMessage { DisplayMessage = "Success", MessageType = ErrorMessageType.Success, Ex = null, Title = "Success" });

			/** Create the abara data. */
			var crVendor = new QL_CreditReportVendorManual
			{
				BureauId = QL_CreditReportBureau.MetaData.ManualID,
				Score = this.score,
				IsScored = this.isScored,
				IsHit = this.isHit,
				Report = this.report,
				CreatedBy = username,
				CreatedOn = DateTime.UtcNow
			};
			crVendor.Save(username);

			var creditReport = new QL_CreditReport
			{
				CreditReportVendorId = QL_CreditReportVendor.MetaData.ManualID,
				CreditReportVendorManualId = crVendor.CreditReportVendorManualID,
				LeadId = wsLead.LeadID,
				AddressId = wsAddress.AddressID,
				BureauId = crVendor.BureauId,
				SeasonId = season.SeasonID,
				Prefix = wsLead.Prefix,
				FirstName = wsLead.FirstName,
				MiddleName = wsLead.MiddleName,
				LastName = wsLead.LastName,
				Suffix = wsLead.Suffix,
				SSN = wsLead.SocialSecurity,
				DOB = string.IsNullOrEmpty(wsLead.DOB) ? (DateTime?)null : DateTime.Parse(wsLead.DOB),
				Score = crVendor.Score,
				IsScored = crVendor.IsScored,
				IsHit = crVendor.IsHit,
				IsActive = true,
				IsDeleted = false
			};
			creditReport.Save(username);

			crInfo = new Microbilt.ResultWSCreditReportInfo(new WSCreditReportInfo
			{
				CreditReportID = creditReport.CreditReportID,
				LeadId = wsLead.LeadID,
				// AccountId = wsLead.AccountId,
				BureauName = creditReport.Bureau.BureauName,
				DOB = creditReport.DOB.ToString(),
				SSN = creditReport.SSN,
				Score = creditReport.Score,
				ScoreFound = creditReport.IsScored,
				Phone = wsLead.HomePhone,
				PhoneStatus = 1,
				AnyError = false,
				Messages = "Success"
			});
			/** Calculate the ScoreGroup. */
			crInfo.ScoreGroup = CreditHelper.GetCreditScoreGroup(crInfo.Score, crInfo.ReportFound,
				excellentCreditScore: season.ExcellentCreditScoreThreshold,
				passCreditScore: season.PassCreditScoreThreshold,
				subCreditScore: season.SubCreditScoreThreshold);

			/** Return result. */
			return true;
		}

		#endregion // Methods
	}
}
