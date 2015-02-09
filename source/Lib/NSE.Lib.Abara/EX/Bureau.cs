using System;
using System.Collections.Generic;
using System.Globalization;
using NSE.Lib.Abara.com.abarasoftware.blinkws.EX;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NSE.Lib.Abara.EX
{
	public class Bureau
	{
		#region .ctor
		#endregion .ctor

		#region Methods

		#region Public

		/// <summary>
		/// Given the arguments it will inquire for the TU report.
		/// </summary>
		/// <param name="oWSLead">WSLead</param>
		/// <param name="oWSAddress">WSAddress</param>
		/// <param name="oSeason"></param>
		/// <param name="oRepInfo">ref WSCreditReportInfo</param>
		/// <param name="szAdUsername">string</param>
		/// <param name="oMessageList"></param>
		/// <returns>InqueryResponse</returns>
		public InqueryResponse Execute(IWSLead oWSLead, IWSAddress oWSAddress, IWSSeason oSeason, string szAdUsername, ref List<WSMessage> oMessageList, out IWSCreditReportInfo oRepInfo)
		{
			// Locals
			var oResult = new InqueryResponse { ReportOutcome = InqueryResponse.IqResultValue.NEVER_QUERY };
			var oEndPointAddress = new System.ServiceModel.EndpointAddress(Credentials.EndPointExUri);
			var oService = new ExperianWSSoapClient("ExperianWSSoap", oEndPointAddress);

			#region Build Data

			var oInputData = new InputData
			{
				RequestedByID = szAdUsername,
				SystemCustomerID = oWSLead.LeadID.ToString(CultureInfo.InvariantCulture),
				FirstName = oWSLead.FirstName,
				LastName = oWSLead.LastName,
				/* Not Required for Eq and Ex*/
				//AddressType = Main.GetAddressType(),
				Apartment = oWSAddress.AptNumber,
				City = oWSAddress.City,
				CountryCode = "USA",
				State = oWSAddress.State,
				ZipCode = oWSAddress.PostalCode,
				StreetName = oWSAddress.StreetName,
				StreetNumber = oWSAddress.HouseNumber,
			};

			// Middle Name
			if (!String.IsNullOrEmpty(oWSLead.MiddleName)) oInputData.MiddleName = oWSLead.MiddleName;

			// Street Type
			if (!String.IsNullOrEmpty(oWSAddress.StreetType)) oInputData.StreetType = oWSAddress.StreetType;

			// Apt #
			if (!String.IsNullOrEmpty(oWSAddress.AptNumber)) oInputData.Apartment = oWSAddress.AptNumber;

			// Check for Social Security 
			if (!String.IsNullOrEmpty(oWSLead.SocialSecurity))
				oInputData.SocialSecurityNumber = oWSLead.SocialSecurity;

			// DOB
			if (!String.IsNullOrEmpty(oWSLead.DOB))
			{
				DateTime oDob;
				DateTime.TryParse(oWSLead.DOB, out oDob);
				oInputData.DOBDay = oDob.Day.ToString(CultureInfo.InvariantCulture);
				oInputData.DOBMonth = oDob.Month.ToString(CultureInfo.InvariantCulture);
				oInputData.DOBYear = oDob.Year.ToString(CultureInfo.InvariantCulture);
			}

			#endregion Build Data

			try
			{
// ReSharper disable once PossibleNullReferenceException
				oService.ClientCredentials.Windows.ClientCredential.UserName = Credentials.WinUsername;
				oService.ClientCredentials.Windows.ClientCredential.Password = Credentials.WinPassword;
				var oResponse = oService.GetNewReport(Credentials.CrUsername, Credentials.CrPassword, oInputData,
													  Services.CreditReport);
				// Handle response
				BindData(oResult, oResponse, ref oMessageList);
				SaveCr(oResult, oWSLead, oWSAddress, oSeason, szAdUsername, ref oMessageList, out oRepInfo);

			}
			catch (Exception oEx)
			{
				oMessageList.Add(new WSMessage
				{
					Title = "Exception Thrown",
					MessageType = ErrorMessageType.Exception,
					DisplayMessage = string.Format("The following exception was thrown:{0}", oEx.Message),
					Ex = oEx
				});
			}

			// Return result
			oRepInfo = new Main.WSCreditReportInfo();
			return oResult;
		}

		#endregion Public

		#region Private

		private static void BindData(InqueryResponse oIqResponse, ReportResponse oResponse, ref List<WSMessage> oMessageList)
		{
			//oIqResponse.ReportOutcome = Main.GetReportOutcomeTU(oResponse.ReportOutcome);
			oIqResponse.ReportOutcome = Main.GetReportOutcomeEX(oResponse.ReportOutcome);
			oIqResponse.Result = Main.GetResultEX(oResponse.Result);
			oIqResponse.ReportID = oResponse.ReportID;
			oIqResponse.ReportGuid = oResponse.ReportGuid;
			oIqResponse.IsScored = oResponse.IsScored;
			oIqResponse.Score = oResponse.Score;
			oIqResponse.IsHit = oResponse.IsHit;
			oIqResponse.ReportHtml = oResponse.HTML;
			oIqResponse.ReportXml = oResponse.XML;
			// Check for error message
			if (!String.IsNullOrEmpty(oResponse.ErrorMessage))
			{
				oIqResponse.ErrorMessage = oResponse.ErrorMessage;
				oMessageList.Add(new WSMessage
				{
					Title = "Error From Abara",
					MessageType = ErrorMessageType.Critical,
					DisplayMessage = string.Format("Message:\r\n{0}", oResponse.ErrorMessage),
				});

			}
			oIqResponse.HitStatus = oResponse.HitStatus;
			oIqResponse.DecisionCode = oResponse.DecisionCode;
			oIqResponse.DecisionText = oResponse.DecisionText;
		}

		private static void SaveCr(InqueryResponse oIqResponse, IWSLead oWsLead, IWSAddress oWsAddress, IWSSeason oSeason, string szAdUsername, ref List<WSMessage> oMessageList, out IWSCreditReportInfo oRepInfo)
		{
			try
			{
				// Create the Abara Data
				var oCrAbara = new QL_CreditReportVendorAbara
				{
					ReportID = oIqResponse.ReportID,
					BureauId = QL_CreditReportBureau.MetaData.EquafaxID,
					ReportGuid = oIqResponse.ReportGuid,
					Result = InqueryResponse.GetHandshakeResultString(oIqResponse.Result),
					Score = oIqResponse.Score,
					IsScored = oIqResponse.IsScored,
					IsHit = oIqResponse.IsHit,
					ReportHtml = oIqResponse.ReportHtml,
					ReportXML = oIqResponse.ReportXml,
					ErrorMessage = oIqResponse.ErrorMessage,
					HitStatus = oIqResponse.HitStatus,
					DecisionCode = oIqResponse.DecisionCode,
					DecisionText = oIqResponse.DecisionText
				};
				oCrAbara.Save(szAdUsername);

				// Locals
				var oCr = new QL_CreditReport
				{
					BureauId = QL_CreditReportBureau.MetaData.ExperianID,
					LeadId = oWsLead.LeadID,
					AddressId = oWsAddress.AddressID,
					CreditReportVendorId = QL_CreditReportVendor.MetaData.AbaraID,
					SeasonId = oSeason.SeasonID,
					Prefix = oWsLead.Prefix,
					FirstName = oWsLead.FirstName,
					MiddleName = oWsLead.MiddleName,
					LastName = oWsLead.LastName,
					Suffix = oWsLead.Suffix,
					SSN = string.IsNullOrEmpty(oWsLead.SocialSecurity) ? null : SOS.Lib.Util.Cryptography.TripleDES.EncryptString(oWsLead.SocialSecurity, null),
					DOB = string.IsNullOrEmpty(oWsLead.DOB) ? null : (DateTime?)DateTime.Parse(oWsLead.DOB),
					Score = (short)oIqResponse.Score,
					IsDeleted = false,
					CreatedBy = szAdUsername,
					CreatedOn = DateTime.Now
				};
				oCr.Save(szAdUsername);

				// Save Data to ReportInfo for returning to the Client
				/*
					public int CreditReportID;
					public string BureauName;
					public string DOB;
					public string SSN;
					public int Score;
					public bool ScoreFound;
					public bool ReportFound;
					public string Phone;
					public int PhoneStatus;
					public bool AnyError;
					public string Messages;
				 */
				oRepInfo = new Main.WSCreditReportInfo
				{
					CreditReportID = oCr.CreditReportID,
					BureauName = QL_CreditReportBureau.MetaData.ExperianID,
					DOB = oCr.DOB != null ? oCr.DOB.Value.ToString(CultureInfo.InvariantCulture) : null,
					SSN = !string.IsNullOrEmpty(oCr.SSN) ? oCr.SSN : null,
					Score = oCr.Score,
					ScoreFound = oCrAbara.IsScored,
					ReportFound = oCrAbara.IsHit,
					Phone = oWsLead.HomePhone,
					AnyError = WSMessage.HasCriticalError(oMessageList),
					Messages = oIqResponse.ErrorMessage
				};
			}
			catch (Exception oEx)
			{
				oMessageList.Add(new WSMessage
				{
					Title = "Exception Saving Credit Report",
					MessageType = ErrorMessageType.Exception,
					DisplayMessage = string.Format("The following error occurred:\r\n{0}", oEx.Message),
					Ex = oEx
				});
				throw;
			}
		}

		#endregion Private

		#endregion Methods

	}
}
