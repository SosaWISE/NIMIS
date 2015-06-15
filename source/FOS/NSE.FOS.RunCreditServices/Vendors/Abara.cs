using System;
using System.Collections.Generic;
using NSE.FOS.RunCreditServices.Interfaces;
using NSE.Lib.Microbilt;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;
using NXS.Lib;

namespace NSE.FOS.RunCreditServices.Vendors
{
	public class Abara : IVendor
	{
		#region .ctor

		public Abara()
		{
			_isMock = !string.IsNullOrEmpty(WebConfig.Instance.GetConfig("CreditReport_Vendor_ABARA_Mock"));
		}

		#endregion .ctor

		#region Properties

		private readonly bool _isMock;

		//private enum MockScoreGrade
		//{
		//	Excellent = 0,
		//	Good = 1,
		//	Sub = 2,
		//	Poor = 3
		//}

		#endregion Properties

		#region Methods

		public bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username,
			ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
		{
			/** Check to see if we are mocking the data. */
			if (_isMock)
				return RunCreditMock(wsLead, wsAddress, season, username, ref messageList, out crInfo);

			/** Default path of execution. */
			var abaraMain = new Lib.Abara.Main();
			return abaraMain.RunService(wsLead, wsAddress, bureausList, season, username, ref messageList, out crInfo);
		}

		private bool RunCreditMock(IWSLead wsLead, IWSAddress wsAddress, IWSSeason season, string username,
			ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
		{
			/** Get a random result. */
			var seed = (int)DateTime.Now.Ticks;
			var r = new Random(seed);
			var rndNumber = r.Next(0, 3);
			var abaraReport = SosCrmDataContext.Instance.SAE_CreditReportAbaras.GetByRandomNumber(rndNumber);
			messageList.Add(new WSMessage{DisplayMessage = "Success", MessageType = ErrorMessageType.Success, Ex = null, Title = "Success"});

			/** Create the abara data. */
			var crAbara = new QL_CreditReportVendorAbara
			{
				BureauId = "TU",
				ReportID = abaraReport.ReportID,
				ReportGuid = abaraReport.ReportGuid,
				Result = abaraReport.Result,
				Score = abaraReport.Score,
				IsScored = abaraReport.IsScored,
				IsHit = abaraReport.IsHit,
				ReportHtml = abaraReport.ReportHtml,
				ReportXML = abaraReport.ReportXML,
				ErrorMessage = abaraReport.ErrorMessage,
				HitStatus = abaraReport.HitStatus,
				DecisionCode = abaraReport.DecisionCode,
				DecisionText = abaraReport.DecisionText,
				CreatedBy = username,
				CreatedOn = DateTime.UtcNow
			};
			crAbara.Save(username);

			var creditReport = new QL_CreditReport
			{
				LeadId = wsLead.LeadID,
				AddressId = wsAddress.AddressID,
				BureauId = crAbara.BureauId,
				SeasonId = season.SeasonID,
				CreditReportVendorId = QL_CreditReportVendor.MetaData.AbaraID,
				CreditReportVendorAbaraId = crAbara.CreditReportVendorAbaraID,
				Prefix = wsLead.Prefix,
				FirstName = wsLead.FirstName,
				MiddleName = wsLead.MiddleName,
				LastName = wsLead.LastName,
				Suffix = wsLead.Suffix,
				SSN = wsLead.SocialSecurity,
				DOB = string.IsNullOrEmpty(wsLead.DOB) ? (DateTime?) null : DateTime.Parse(wsLead.DOB),
				Score = crAbara.Score,
				IsScored = crAbara.IsScored,
				IsHit = crAbara.IsHit,
				IsActive = true,
				IsDeleted = false
			};
			creditReport.Save(username);

			/** Get the CreditReportID. */
			crAbara.CreditReportId = creditReport.CreditReportID;
			crAbara.Save(username);


			crInfo = new Microbilt.ResultWSCreditReportInfo(new WSCreditReportInfo
			{
				CreditReportID = creditReport.CreditReportID,
				LeadId = wsLead.LeadID,
//				AccountId = wsLead.AccountId,
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

			/** Return result. */
			return true;
		}

		#endregion Methods

		
		#region Supporting Classes

		public class ResultWSCreditReportInfo : IWSCreditReportInfo
		{
			#region .ctor

			public ResultWSCreditReportInfo(Lib.Abara.Main.WSCreditReportInfo wsInfo)
			{
				CreditReportID = wsInfo.CreditReportID;
				BureauName = wsInfo.BureauName;
				DOB = wsInfo.DOB;
				SSN = wsInfo.SSN;
				Score = wsInfo.Score;
				ScoreFound = wsInfo.ScoreFound;
				Phone = wsInfo.Phone;
				PhoneStatus = wsInfo.PhoneStatus;
				AnyError = wsInfo.AnyError;
				Messages = wsInfo.Messages;
				ReportFound = wsInfo.ScoreFound;
			}

			#endregion .ctor

			#region Properties
			public long CreditReportID { get; set; }
			public long LeadId { get; set; }
			public long? AccountId { get; set; }
			public string BureauName { get; set; }
			public string DOB { get; set; }
			public string SSN { get; set; }
			public int Score { get; set; }
			public bool ScoreFound { get; set; }
			public string Phone { get; set; }
			public int PhoneStatus { get; set; }
			public bool AnyError { get; set; }
			public string Messages { get; set; }
			public bool ReportFound { get; set; }
			public CreditScoreGroup ScoreGroup { get; set; }

			#endregion Properties
		}
		#endregion Supporting Classes

	}
}
