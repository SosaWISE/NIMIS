using System;
using System.Collections.Generic;
using NSE.Lib.Abara.com.abarasoftware.testblinkws.TU;
using SOS.Data.Logging;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util.Cryptography;

namespace NSE.Lib.Abara
{
	public static class Credentials
	{
		#region Fields

		#region EndPointTuUri
		private static readonly object SyncEndPointTuUri = new object();
		private static string _endPointTuUri;
		public static string EndPointTuUri
		{
			get
			{
				if (String.IsNullOrEmpty(_endPointTuUri))
				{
					lock (SyncEndPointTuUri)
					{
						if (String.IsNullOrEmpty(_endPointTuUri))
						{
							_endPointTuUri = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_TU_ENDPOINT"), null);
						}
					}
				}

				// Return result
				return _endPointTuUri;
			}
		}

		#endregion EndPointTuUri

		#region EndPointEqUri
		private static readonly object SyncEndPointEqUri = new object();
		private static string _endPointEqUri;
		public static string EndPointEqUri
		{
			get
			{
				if (String.IsNullOrEmpty(_endPointEqUri))
				{
					lock (SyncEndPointEqUri)
					{
						if (String.IsNullOrEmpty(_endPointEqUri))
						{
							_endPointEqUri = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_EQ_ENDPOINT"), null);
						}
					}
				}

				// Return result
				return _endPointEqUri;
			}
		}

		#endregion EndPointEqUri

		#region EndPointExUri
		private static readonly object SyncEndPointExUri = new object();
		private static string _endPointExUri;
		public static string EndPointExUri
		{
			get
			{
				if (String.IsNullOrEmpty(_endPointExUri))
				{
					lock (SyncEndPointExUri)
					{
						if (String.IsNullOrEmpty(_endPointExUri))
						{
							_endPointExUri = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_EQ_ENDPOINT"), null);
						}
					}
				}

				// Return result
				return _endPointExUri;
			}
		}

		#endregion EndPointExUri

		#region WinUsername
		private static readonly object SyncWinUsernameCredentials = new object();
		private static string _winUsername;
		public static string WinUsername
		{
			get
			{
				if (String.IsNullOrEmpty(_winUsername))
				{
					lock (SyncWinUsernameCredentials)
					{
						if (String.IsNullOrEmpty(_winUsername))
						{
							_winUsername = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_WIN_UID"), null);
						}
					}
				}

				// Return result
				return _winUsername;
			}
		}
		#endregion WinUsername

		#region WinPassword
		private static readonly object SyncWinPasswordCredentials = new object();
		private static string _winPassword;
		public static string WinPassword
		{
			get
			{
				if (String.IsNullOrEmpty(_winPassword))
				{
					lock (SyncWinPasswordCredentials)
					{
						if (String.IsNullOrEmpty(_winPassword))
						{
							_winPassword = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_WIN_PWD"), null);
						}
					}
				}

				// Return result
				return _winPassword;
			}
		}
		#endregion WinPassword

		#region CrUsername
		private static readonly object SyncCrUsernameCredentials = new object();
		private static string _crUsername;
		public static string CrUsername
		{
			get
			{
				if (String.IsNullOrEmpty(_crUsername))
				{
					lock (SyncCrUsernameCredentials)
					{
						if (String.IsNullOrEmpty(_crUsername))
						{
							_crUsername = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_CRP_UID"), null);
						}
					}
				}

				// Return result
				return _crUsername;
			}
		}
		#endregion CrUsername

		#region CrPassword
		private static readonly object SyncCrPasswordCredentials = new object();
		private static string _crPassword;
		public static string CrPassword
		{
			get
			{
				if (String.IsNullOrEmpty(_crPassword))
				{
					lock (SyncCrPasswordCredentials)
					{
						if (String.IsNullOrEmpty(_crPassword))
						{
							_crPassword = TripleDES.DecryptString(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditReport_Vendor_ABARA_CRP_PWD"), null);
						}
					}
				}

				// Return result
				return _crPassword;
			}
		}
		#endregion CrPassword

		#endregion Fields
	}
	public class Main
	{
		#region Methods

		#region Private
		internal static AddressTypes GetAddressType()
		{
			return AddressTypes.ConventionalAddress;
		}

		#region Helper Methods TU
		internal static InqueryResponse.IqResultValue GetReportOutcomeTU(ResultValue oValue)
		{
			switch (oValue)
			{
				case ResultValue.NotSet:
					return InqueryResponse.IqResultValue.NotSet;
				case ResultValue.Scored:
					return InqueryResponse.IqResultValue.Scored;
				case ResultValue.NotScored:
					return InqueryResponse.IqResultValue.NotScored;
				case ResultValue.Declined:
					return InqueryResponse.IqResultValue.Declined;
				case ResultValue.NoHit:
					return InqueryResponse.IqResultValue.NoHit;
				default:
					return InqueryResponse.IqResultValue.UNDEFINED;
			}
		}
		internal static InqueryResponse.HandshakeResult GetResultTU(PossibleResults oValue)
		{
			switch (oValue)
			{
				case PossibleResults.SUCCESS:
					return InqueryResponse.HandshakeResult.SUCCESS;
				case PossibleResults.FAILURE:
					return InqueryResponse.HandshakeResult.FAILURE;
				case PossibleResults.UNAUTHORIZED:
					return InqueryResponse.HandshakeResult.UNAUTHORIZED;
				case PossibleResults.ERROR:
					return InqueryResponse.HandshakeResult.ERROR;
				default:
					return InqueryResponse.HandshakeResult.UNDEFINED;
			}
		}
		#endregion Helper Methods TU

		#region Helper Methods EQ
		internal static InqueryResponse.IqResultValue GetReportOutcomeEQ(com.abarasoftware.blinkws.EQ.ResultValue oValue)
		{
			switch (oValue)
			{
				case com.abarasoftware.blinkws.EQ.ResultValue.NotSet:
					return InqueryResponse.IqResultValue.NotSet;
				case com.abarasoftware.blinkws.EQ.ResultValue.Scored:
					return InqueryResponse.IqResultValue.Scored;
				case com.abarasoftware.blinkws.EQ.ResultValue.NotScored:
					return InqueryResponse.IqResultValue.NotScored;
				case com.abarasoftware.blinkws.EQ.ResultValue.Declined:
					return InqueryResponse.IqResultValue.Declined;
				case com.abarasoftware.blinkws.EQ.ResultValue.NoHit:
					return InqueryResponse.IqResultValue.NoHit;
				default:
					return InqueryResponse.IqResultValue.UNDEFINED;
			}
		}
		internal static InqueryResponse.HandshakeResult GetResultEQ(com.abarasoftware.blinkws.EQ.PossibleResults oValue)
		{
			switch (oValue)
			{
				case com.abarasoftware.blinkws.EQ.PossibleResults.SUCCESS:
					return InqueryResponse.HandshakeResult.SUCCESS;
				case com.abarasoftware.blinkws.EQ.PossibleResults.FAILURE:
					return InqueryResponse.HandshakeResult.FAILURE;
				case com.abarasoftware.blinkws.EQ.PossibleResults.UNAUTHORIZED:
					return InqueryResponse.HandshakeResult.UNAUTHORIZED;
				case com.abarasoftware.blinkws.EQ.PossibleResults.ERROR:
					return InqueryResponse.HandshakeResult.ERROR;
				default:
					return InqueryResponse.HandshakeResult.UNDEFINED;
			}
		}
		#endregion Helper Methods EQ

		#region Helper Methods EX
		internal static InqueryResponse.IqResultValue GetReportOutcomeEX(com.abarasoftware.blinkws.EX.ResultValue oValue)
		{
			switch (oValue)
			{
				case com.abarasoftware.blinkws.EX.ResultValue.NotSet:
					return InqueryResponse.IqResultValue.NotSet;
				case com.abarasoftware.blinkws.EX.ResultValue.Scored:
					return InqueryResponse.IqResultValue.Scored;
				case com.abarasoftware.blinkws.EX.ResultValue.NotScored:
					return InqueryResponse.IqResultValue.NotScored;
				case com.abarasoftware.blinkws.EX.ResultValue.Declined:
					return InqueryResponse.IqResultValue.Declined;
				case com.abarasoftware.blinkws.EX.ResultValue.NoHit:
					return InqueryResponse.IqResultValue.NoHit;
				default:
					return InqueryResponse.IqResultValue.UNDEFINED;
			}
		}
		internal static InqueryResponse.HandshakeResult GetResultEX(com.abarasoftware.blinkws.EX.PossibleResults oValue)
		{
			switch (oValue)
			{
				case com.abarasoftware.blinkws.EX.PossibleResults.SUCCESS:
					return InqueryResponse.HandshakeResult.SUCCESS;
				case com.abarasoftware.blinkws.EX.PossibleResults.FAILURE:
					return InqueryResponse.HandshakeResult.FAILURE;
				case com.abarasoftware.blinkws.EX.PossibleResults.UNAUTHORIZED:
					return InqueryResponse.HandshakeResult.UNAUTHORIZED;
				case com.abarasoftware.blinkws.EX.PossibleResults.ERROR:
					return InqueryResponse.HandshakeResult.ERROR;
				default:
					return InqueryResponse.HandshakeResult.UNDEFINED;
			}
		}
		#endregion Helper Methods EX

		#endregion Private

		#endregion Methods

		public bool RunService(IWSLead oWSLead, IWSAddress oWSAddress, string[] szaBureaus, IWSSeason oSeason, string szAdUsername, ref List<WSMessage> oMessageList, out IWSCreditReportInfo oRepInfo)
		{
			// Locals
			var bResult = true;
			oRepInfo = new WSCreditReportInfo();

			// Check to see if there is an override of the config file
			if (szaBureaus == null || szaBureaus.Length == 0)
				szaBureaus = SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("CreditService_BureauListOrder").Split(',');

			try
			{
				// Loop through
				foreach (var szBureau in szaBureaus)
				{
					var oIqResponse = new InqueryResponse { Result = InqueryResponse.HandshakeResult.UNDEFINED, Score = 0 };
					switch (szBureau)
					{
						case "EQ":
							// Execute EQ
							var oEq = new EQ.Bureau();
							oIqResponse = oEq.Execute(oWSLead, oWSAddress, oSeason, szAdUsername, ref oMessageList, out oRepInfo);
							break;
						case "EX":
							// Execute EX
							var oEx = new EX.Bureau();
							oIqResponse = oEx.Execute(oWSLead, oWSAddress, oSeason, szAdUsername, ref oMessageList, out oRepInfo);
							break;
						case "TU":
							// Execute TU
							var oTu = new TU.Bureau();
							oIqResponse = oTu.Execute(oWSLead, oWSAddress, oSeason, szAdUsername, ref oMessageList, out oRepInfo);
							break;
						default:
							oRepInfo = new WSCreditReportInfo();
							break;
					}

					// Check that there is no critical errors
					if (WSMessage.HasCriticalError(oMessageList))
					{
						bResult = false;
						break;
					}

					// Stop running if the report at least passed
					if (oIqResponse.Score >= oSeason.ExcellentCreditScoreThreshold) break;
				}
			}
			catch (Exception oEx)
			{
				oMessageList.Add(new WSMessage
				{
					Title = "Exception Thrown",
					MessageType = ErrorMessageType.Exception,
					DisplayMessage = string.Format("The following error occurred:\r\n{0}", oEx.Message),
					Ex = oEx
				});
				bResult = false;
			}

			// Check that there are no critical errors
			if (bResult) bResult = !WSMessage.HasCriticalError(oMessageList);

			foreach (var oMsgItem in oMessageList)
			{
				var oMsgNew = new LG_Message
				{
					MessageTypeId = (int)LG_MessageType.MessageTypeEnum.Critical,
					LogSourceID = (int)LG_LogSource.LogSourceEnum.NSEFOSRunCreditServices,
					Header = oMsgItem.Title,
					Message =
						"LeadID: " + oWSLead.LeadID + ". AddressID: " + oWSAddress.AddressID + ". MESSAGE: " + oMsgItem.DisplayMessage + ". MESSAGE TYPE: " +
						oMsgItem.MessageType,
					CreatedBy = szAdUsername,
					CreatedOn = DateTime.Now
				};
				oMsgNew.Save(szAdUsername);
			}

			// Resturn result
			return bResult;
		}


		#region Serializable Structures

		/// <summary>
		/// Structure used to pass lead information.
		/// </summary>
		[Serializable]
		public struct WSLead
		{
			public long LeadID;
			public string FirstName;
			public string MiddleName;
			public string LastName;
			public string SocialSecurity;
			public string DOB;
			public string Generation;
		}

		/// <summary>
		/// Structure used for passing address information.
		/// </summary>
		[Serializable]
		public struct WSAddress
		{
			public long AddressID;
			public string AddressType;
			public string Address1;
			public string StreetName;
			public string HouseNumber;
			public string AptNumber;
			public string Direction;
			public string StreetType;
			public string City;
			public string State;
			public string PostalCode;
			public bool IsVerified;
		}

		/// <summary>
		/// Structure used to store information on the Credit Report.
		/// </summary>
		[Serializable]
		public struct WSCreditReportInfo : IWSCreditReportInfo
		{
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
		}

		#endregion Serializable Structures


	}
}
