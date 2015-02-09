using System;
using System.Collections.Generic;
using NSE.FOS.RunCreditServices.Interfaces;
using NSE.Lib.Microbilt;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NSE.FOS.RunCreditServices.Vendors
{
	public class Microbilt : IVendor
	{
		#region Methods
		public bool RunCredit(IWSLead wsLead, IWSAddress wsAddress, string[] bureausList, IWSSeason season, string username,
			ref List<WSMessage> messageList, out IWSCreditReportInfo crInfo)
		{
			/** Init method. */
			var wsCrInfo = new WSCreditReportInfo();
			var result = true;
			var service = new HTTPPost();

			try
			{
				foreach (var bureau in bureausList)
				{
					QL_CreditReportVendorMicrobilt qlCrVendorMicrobilt;
					if (!service.RunCredit(HTTPPost.GetBureauIdEnum(bureau), GetFromInterface(wsLead), GetFromInterface(wsAddress), username, out wsCrInfo, out qlCrVendorMicrobilt, out messageList))
					{
					}
				}
			}
			catch (Exception ex)
			{
				result = false;
				messageList.Add(new WSMessage
				{
					Title = string.Format("Exception was caught at the root of the Microbilt class."),
					DisplayMessage = ex.Message,
					MessageType = ErrorMessageType.Critical,
					Ex = ex
				});
			}


			// ** Return result 
			// ** Init the crInfo report
			crInfo = new ResultWSCreditReportInfo(wsCrInfo);
			return result;
		}

		public static WSLead GetFromInterface(IWSLead iLead)
		{
			// ** Init
			var result = new WSLead
			{
				LeadID = iLead.LeadID,
				FirstName = iLead.FirstName,
				MiddleName = iLead.MiddleName,
				LastName = iLead.LastName,
				SocialSecurity = iLead.SocialSecurity,
				DOB = iLead.DOB,
				Generation = iLead.Generation,
			};

			// ** Return result
			return result;
		}

		public static WSAddress GetFromInterface(IWSAddress iAddress)
		{
			// ** Init.
			var result = new WSAddress
			{
				AddressID = iAddress.AddressID,
				AddressType = iAddress.AddressType,
				Address1 = iAddress.Address1,
				StreetName = iAddress.StreetName,
				HouseNumber = iAddress.HouseNumber,
				AptNumber = iAddress.AptNumber,
				Direction = iAddress.Direction,
				StreetType = iAddress.StreetType,
				City = iAddress.City,
				State = iAddress.State,
				PostalCode = iAddress.PostalCode,
				IsVerified = iAddress.IsVerified 
			};

			// ** return result
			return result;
		}

		public static WSCreditReportInfo GetFromInterface(IWSCreditReportInfo crInfo)
		{
			// ** Initialize 
			var result = new WSCreditReportInfo
			{
				CreditReportID = crInfo.CreditReportID,
				BureauName = crInfo.BureauName,
				DOB = crInfo.DOB,
				SSN = crInfo.SSN,
				Score = crInfo.Score,
				ScoreFound = crInfo.ScoreFound,
				Phone = crInfo.Phone,
				PhoneStatus = crInfo.PhoneStatus,
				AnyError = crInfo.AnyError,
				Messages = crInfo.Messages
			};

			// ** Return result.
			return result;
		}

		#endregion Methods

		#region Supporting Classes

		public class ResultWSCreditReportInfo : IWSCreditReportInfo
		{
			#region .ctor

			public ResultWSCreditReportInfo(WSCreditReportInfo wsInfo)
			{
				CreditReportID = wsInfo.CreditReportID;
				LeadId = wsInfo.LeadId;
				AccountId = wsInfo.AccountId;
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