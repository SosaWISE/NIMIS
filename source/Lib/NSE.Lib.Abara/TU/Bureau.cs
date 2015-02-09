﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using NSE.Lib.Abara.com.abarasoftware.testblinkws.TU;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;

namespace NSE.Lib.Abara.TU
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
		/// <param name="season"></param>
		/// <param name="oRepInfo">ref WSCreditReportInfo</param>
		/// <param name="szAdUsername">string</param>
		/// <param name="oMessageList"></param>
		/// <returns>InqueryResponse</returns>
		public InqueryResponse Execute(IWSLead oWSLead, IWSAddress oWSAddress, IWSSeason season, string szAdUsername, ref List<WSMessage> oMessageList, out IWSCreditReportInfo oRepInfo)
		{
			// Locals
			var oResult = new InqueryResponse { ReportOutcome = InqueryResponse.IqResultValue.NEVER_QUERY };
			//var oEndPointAddress = new System.ServiceModel.EndpointAddress(Credentials.EndPointTuUri);
			//var oService = new TransUnionWSSoapClient("TransUnionWSSoap", oEndPointAddress);
			var oService = new TransUnionWS ();
			oService.Url = Credentials.EndPointTuUri;
			oRepInfo = new Main.WSCreditReportInfo();

			#region Build Data

			var oInputData = new InputData
			{
				RequestedByID = szAdUsername,
				SystemCustomerID = oWSLead.LeadID.ToString(CultureInfo.InvariantCulture),
				FirstName = oWSLead.FirstName,
				LastName = oWSLead.LastName,
				AddressType = Main.GetAddressType(),
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
				oInputData.SocialSecurityNumber = oWSLead.SocialSecurity.Replace("-","");

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
				//oService.ClientCredentials.Windows.ClientCredential.UserName = Credentials.WinUsername;
				//oService.ClientCredentials.Windows.ClientCredential.Password = Credentials.WinPassword;
				var winCredentials = new CredentialCache();
				winCredentials.Add(new Uri(oService.Url),
					"Negotiate",
					new NetworkCredential(Credentials.WinUsername, Credentials.WinPassword, Credentials.EndPointTuUri));
				oService.Credentials = winCredentials;
				var oResponse = oService.GetNewReport(Credentials.CrUsername, Credentials.CrPassword, oInputData,
													  Services.CreditReport);
				// Handle response
				BindData(oResult, oResponse, ref oMessageList);
				SaveCr(oResult, oWSLead, oWSAddress, season, szAdUsername, out oRepInfo, ref oMessageList);

			}
			catch (Exception oEx)
			{
				oMessageList.Add(new WSMessage
				{
					Title = "Exception thrown",
					MessageType = ErrorMessageType.Exception,
					DisplayMessage = string.Format("The following exception was thrown:{0}", oEx.Message),
					Ex = oEx
				});
			}

			// Return result
			return oResult;
		}

		#endregion Public

		#region Private

		private static void BindData(InqueryResponse oIqResponse, ReportResponse oResponse, ref List<WSMessage> oMessageList)
		{
			oIqResponse.ReportOutcome = Main.GetReportOutcomeTU(oResponse.ReportOutcome);
			oIqResponse.Result = Main.GetResultTU(oResponse.Result);
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

		private static void SaveCr(InqueryResponse oIqResponse, IWSLead oWsLead, IWSAddress oWsAddress, IWSSeason season, string szAdUsername, out IWSCreditReportInfo oRepInfo, ref List<WSMessage> oMessageList)
		{
			try
			{
				// Locals
				var oCr = new QL_CreditReport
				{
					BureauId = QL_CreditReportBureau.MetaData.TransUnionID,
					LeadId = oWsLead.LeadID,
					AddressId = oWsAddress.AddressID,
					CreditReportVendorId = QL_CreditReportVendor.MetaData.AbaraID,
					SeasonId = season.SeasonID,
					Prefix = oWsLead.Prefix,
					FirstName = oWsLead.FirstName,
					MiddleName = oWsLead.MiddleName,
					LastName = oWsLead.LastName,
					Suffix = oWsLead.Suffix,
					SSN = string.IsNullOrEmpty(oWsLead.SocialSecurity) ? null : SOS.Lib.Util.Cryptography.TripleDES.EncryptString(oWsLead.SocialSecurity, null),
					DOB = string.IsNullOrEmpty(oWsLead.DOB) ? null : (DateTime?)DateTime.Parse(oWsLead.DOB),
					Score = (short)oIqResponse.Score,
					IsScored = oIqResponse.IsScored,
					IsHit = oIqResponse.IsHit,
					IsActive =  true,
					IsDeleted = false,
					CreatedBy = szAdUsername,
					CreatedOn = DateTime.Now
				};
				oCr.Save(szAdUsername);

				// Create the Abara Data
				var oCrAbara = new QL_CreditReportVendorAbara
				{
					CreditReportId = oCr.CreditReportID,
					ReportID = oIqResponse.ReportID,
					BureauId = QL_CreditReportBureau.MetaData.TransUnionID,
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

				// ** Save Abara Id in Cr
				oCr.CreditReportVendorAbaraId = oCrAbara.CreditReportVendorAbaraID;
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
				oRepInfo = new Main.WSCreditReportInfo();
				oRepInfo.LeadId = oWsLead.LeadID;
				oRepInfo.CreditReportID = oCr.CreditReportID;
				oRepInfo.BureauName = oCr.Bureau.BureauName;
				oRepInfo.DOB = oCr.DOB != null ? oCr.DOB.Value.ToString(CultureInfo.InvariantCulture) : null;
				oRepInfo.SSN = !string.IsNullOrEmpty(oCr.SSN) ? oCr.SSN : null;
				oRepInfo.Score = oCr.Score;
				oRepInfo.ScoreFound = oCrAbara.IsScored;
				oRepInfo.ReportFound = oCrAbara.IsHit;
				oRepInfo.Phone = oWsLead.HomePhone;
				oRepInfo.AnyError = WSMessage.HasCriticalError(oMessageList);
				oRepInfo.Messages = oIqResponse.ErrorMessage;
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
