using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
//using CC;
using SOS.Data.SosCrm;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util.Configuration;
using SOS.Lib.Util.Cryptography;

namespace NSE.Lib.Microbilt
{
	public class HTTPPost
	{
		#region Properties

		private readonly string _urlPost = "HTTPS://{0}/SDK/PostData.asp";
		private readonly string _urlGet = "HTTPS://{0}/SDK/GetData.asp";
		private readonly string _username = "SDKPLATPRO";
		private readonly string _password = "JS1023PPRO";

		#region Constants

		private const int _MAX_NUMBER_OF_TRIES = 10;
		private const int _PAUSE_BETWEEN_TRY = 3000;

		#endregion Constants

		#endregion Properties

		#region Events
		public event CreditRan OnCreditRan;
		public delegate void CreditRan(object sender, WSCreditReportInfo oRepInfo);
		#endregion Events

		#region Constructors
		/// <summary>
		/// Default constructors
		/// </summary>
		public HTTPPost()
		{
			// Set the hostname and credentials.
			string hostServer = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltHOSTNAME"), null);
			_username = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltMEMBERID"), null);
			_password = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltPASSWORD"), null);
			_urlPost = string.Format(_urlPost, hostServer);
			_urlGet = string.Format(_urlGet, hostServer);
		}

		//public HTTPPost(string szEnvironment)
		//{
		//    // Locals
		//    ConfigurationSettings oConf = new ConfigurationSettings("Preferences", szEnvironment);


		//    // Set the hostname and credentials.
		//    _hostServer = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltHOSTNAME"), null);
		//    _username = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltMEMBERID"), null);
		//    _password = TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("MicroBiltPASSWORD"), null);
		//    _URLPost = string.Format(_URLPost, _hostServer);
		//    _URLGet = string.Format(_URLGet, _hostServer);
		//}
		#endregion Constructors

		#region Methods

		public bool RunCredit(CreditBureaus eBureau, WSLead oWSLead, WSAddress oWSAddress, string logonUser, out WSCreditReportInfo crInfo, out QL_CreditReportVendorMicrobilt creditReportVendorMicrobilt, out List<WSMessage> messageList)
		{
			// Locals 
			string szBureau;
			string szProduct;
			var oCRC = new CreditReportClass();
			crInfo = new WSCreditReportInfo();
			messageList = new List<WSMessage>();

			// Init Method
			var szMessage = string.Empty;

			// Find eBureau and Product
			switch (eBureau)
			{
				case CreditBureaus.Transunion:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Transunion                                             │\r\n");
					szBureau = "TU";
					szProduct = "STD";
					crInfo.BureauName = "TransUnion";
					break;
				case CreditBureaus.Equifax:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Equifax                                                │\r\n");
					szBureau = "EQ";
					szProduct = "STD";
					crInfo.BureauName = "Equifax";
					break;
				case CreditBureaus.Experian:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Experian                                               │\r\n");
					szBureau = "EX";
					szProduct = "STD";
					crInfo.BureauName = "Experian";
					break;
				default:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START DEFAULT TU                                             │\r\n");
					szBureau = "TU";
					szProduct = "STD";
					crInfo.BureauName = "Default eBureau: TransUnion";
					break;
			}
			szMessage +=
				string.Format(
					"│ Username: {0,-20}                                                                        │\r\n",
					logonUser);

			// Get Credit Report
			string szGUID = null;
			string szRawData = _getRawReport(szBureau, szProduct, oWSLead, oWSAddress, ref szGUID);

			string szErrMsg;
			long lResult = oCRC.CreateCreditReport(szRawData, out szErrMsg);

			/*
			 * Detect any errors on the result string from Microbilt.  First we will check to see if the
			 * data returned is not formated in the correct way.
			 */
			// Check the Result and also make sure that there is a subject object to be able to retrieve the score.
			crInfo.ScoreFound = false;
			if (lResult == 0 && oCRC.Subjects != null)
			{
				// See if we can retrieve personal information and Credit Score
				//int nNumberOfSubjects = oCRC.Subjects.Count;
				//if (nNumberOfSubjects > 0)
				//{
				//	var oSubject = (Subject)oCRC.Subjects.Items[0];
				//	// There should only be information for one person.
				//	// foreach(PersonalClass oPerInfo in oSubject.Personals.get_Items(1))
				//	for (int nIndx = 0; nIndx < oSubject.Personals.Count; nIndx++)
				//	{
				//		var oPerInfo = (Personal)oSubject.Personals.Items[nIndx];
				//		szMessage +=
				//			string.Format("\r\n DOB: {0,20} | SSN: {1,11} | Phone#: {2,20} | PhoneStatus: {3,30}",
				//						  oPerInfo.DOB, oPerInfo.SSN, oPerInfo.Phone, oPerInfo.PhoneStatus);
				//		crInfo.DOB = oPerInfo.DOB;
				//		crInfo.SSN = oPerInfo.SSN;
				//		crInfo.Phone = oPerInfo.Phone;
				//		crInfo.PhoneStatus = oPerInfo.PhoneStatus;
				//	}

				//	// Get Credit Score or Beacon
				//	crInfo.Score = 0; // This means that there is no beacon found yet.
				//	// foreach (ScoreClass oScore in oSubject.Scores.get_Items)
				//	for (int nIndx = 0; nIndx < oSubject.Scores.Count; nIndx++)
				//	{
				//		var oScore = (Score)oSubject.Scores.Items[nIndx];
				//		crInfo.Score = oScore.Score;
				//		crInfo.ScoreFound = true;
				//		szMessage += string.Format("\n{0} returned a score of {1}", eBureau, oScore.Score);
				//		szMessage +=
				//			string.Format("\nReason1: {0}, Code: {1}.", oScore.Reason1.Description, oScore.Reason1.Code);
				//		szMessage +=
				//			string.Format("\nReason2: {0}, Code: {1}.", oScore.Reason2.Description, oScore.Reason2.Code);
				//		szMessage +=
				//			string.Format("\nReason3: {0}, Code: {1}.", oScore.Reason3.Description, oScore.Reason3.Code);
				//		szMessage +=
				//			string.Format("\nReason4: {0}, Code: {1}.", oScore.Reason4.Description, oScore.Reason4.Code);
				//	}

				//	crInfo.Messages = szMessage;
				//}

				Debug.WriteLine(szMessage);
				messageList.Add(new WSMessage
				{
					Title = "General CR Message",
					DisplayMessage = szMessage,
					MessageType = crInfo.ScoreFound ? ErrorMessageType.Success : ErrorMessageType.Warning
				});
			}

			// Print and return any error
			if (!string.IsNullOrEmpty(szErrMsg))
			{
				crInfo.AnyError = true;
				crInfo.Messages += string.Format("\r\nINTERPRATIVE ERROR MESSAGE: \r\n{0}", szErrMsg);
			}

			// Save Report to Database
			var creditReport = new QL_CreditReport
			{
				LeadId = oWSLead.LeadID,
				BureauId = GetBureauIdString(eBureau),
				Score = short.Parse(crInfo.Score.ToString(CultureInfo.InvariantCulture)),
				IsHit = crInfo.ScoreFound,
				IsActive = true,
				IsDeleted = false,
				CreatedBy = logonUser,
				CreatedOn = DateTime.Now
			};
			creditReportVendorMicrobilt = new QL_CreditReportVendorMicrobilt
			{
				BureauId = GetBureauIdString(eBureau),
				Score = short.Parse(crInfo.Score.ToString(CultureInfo.InvariantCulture)),
				IsHit = crInfo.ScoreFound,
				CreditReport = szRawData,
				MicroBiltGUID = szGUID,
				CreatedBy = logonUser,
				CreatedOn = DateTime.Now
			};

			//if (oWSLead.DOB != null && !oWSLead.DOB.Equals(string.Empty))
			//	oDBCreditReport.DOB = DateTime.Parse(oWSLead.DOB);
			creditReportVendorMicrobilt.Save(logonUser);

			// ** Tie parent table to microbilt table
			creditReport.CreditReportVendorMicrobiltId = creditReportVendorMicrobilt.CreditReportVendorMicrobiltID;
			creditReport.Save(logonUser);

			crInfo.CreditReportID = creditReport.CreditReportID;

			if (OnCreditRan != null)
				OnCreditRan(this, crInfo);

			return crInfo.ScoreFound;
		}

		/// <summary>
		/// Given the necessary information, this method calls Micobilt and retrieves a report.
		/// </summary>
		/// <param name="eBureau">Bureaus</param>
		/// <param name="oWSLead">WSLead</param>
		/// <param name="oWSAddress">WSAddress</param>
		/// <param name="oRepInfo">WSCreditReportInfo</param>
		/// <param name="szLogonUser">string</param>
		/// <param name="szGUID">string</param>
		/// <param name="szMessage">string</param>
		public QL_CreditReportVendorMicrobilt RunCredit(CreditBureaus eBureau, WSLead oWSLead, WSAddress oWSAddress, ref WSCreditReportInfo oRepInfo, string szLogonUser,
							  out string szGUID, out string szMessage)
		{
			// Locals 
			string szBureau;
			string szProduct;
			var oCRC = new CreditReportClass();

			// Init Method
			szMessage = string.Empty;

			// Find eBureau and Product
			switch (eBureau)
			{
				case CreditBureaus.Transunion:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Transunion                                             │\r\n");
					szBureau = "TU";
					szProduct = "STD";
					oRepInfo.BureauName = "TransUnion";
					break;
				case CreditBureaus.Equifax:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Equifax                                                │\r\n");
					szBureau = "EQ";
					szProduct = "STD";
					oRepInfo.BureauName = "Equifax";
					break;
				case CreditBureaus.Experian:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START Experian                                               │\r\n");
					szBureau = "EX";
					szProduct = "STD";
					oRepInfo.BureauName = "Experian";
					break;
				default:
					szMessage +=
						string.Format(
							"╒════════════════════════════════════════════════════════════════════════════════════════════════════╕\r\n");
					szMessage +=
						string.Format(
							"│                                       START DEFAULT TU                                             │\r\n");
					szBureau = "TU";
					szProduct = "STD";
					oRepInfo.BureauName = "Default eBureau: TransUnion";
					break;
			}
			szMessage +=
				string.Format(
					"│ Username: {0,-20}                                                                        │\r\n",
					szLogonUser);

			// Get Credit Report
			szGUID = null;
			string szRawData = _getRawReport(szBureau, szProduct, oWSLead, oWSAddress, ref szGUID);

			string szErrMsg;
			long lResult = oCRC.CreateCreditReport(szRawData, out szErrMsg);

			/*
			 * Detect any errors on the result string from Microbilt.  First we will check to see if the
			 * data returned is not formated in the correct way.
			 */
			// Check the Result and also make sure that there is a subject object to be able to retrieve the score.
			oRepInfo.ScoreFound = false;
			if (lResult == 0 && oCRC.Subjects != null)
			{
				//// See if we can retrieve personal information and Credit Score
				//int nNumberOfSubjects = oCRC.Subjects.Count;
				//if (nNumberOfSubjects > 0)
				//{
				//	var oSubject = (Subject)oCRC.Subjects.Items[0];
				//	// There should only be information for one person.
				//	// foreach(PersonalClass oPerInfo in oSubject.Personals.get_Items(1))
				//	for (int nIndx = 0; nIndx < oSubject.Personals.Count; nIndx++)
				//	{
				//		var oPerInfo = (Personal)oSubject.Personals.Items[nIndx];
				//		szMessage +=
				//			string.Format("\r\n DOB: {0,20} | SSN: {1,11} | Phone#: {2,20} | PhoneStatus: {3,30}",
				//						  oPerInfo.DOB, oPerInfo.SSN, oPerInfo.Phone, oPerInfo.PhoneStatus);
				//		oRepInfo.DOB = oPerInfo.DOB;
				//		oRepInfo.SSN = oPerInfo.SSN;
				//		oRepInfo.Phone = oPerInfo.Phone;
				//		oRepInfo.PhoneStatus = oPerInfo.PhoneStatus;
				//	}

				//	// Get Credit Score or Beacon
				//	oRepInfo.Score = 0; // This means that there is no beacon found yet.
				//	// foreach (ScoreClass oScore in oSubject.Scores.get_Items)
				//	for (int nIndx = 0; nIndx < oSubject.Scores.Count; nIndx++)
				//	{
				//		var oScore = (Score)oSubject.Scores.Items[nIndx];
				//		oRepInfo.Score = oScore.Score;
				//		oRepInfo.ScoreFound = true;
				//		szMessage += string.Format("\n{0} returned a score of {1}", eBureau, oScore.Score);
				//		szMessage +=
				//			string.Format("\nReason1: {0}, Code: {1}.", oScore.Reason1.Description, oScore.Reason1.Code);
				//		szMessage +=
				//			string.Format("\nReason2: {0}, Code: {1}.", oScore.Reason2.Description, oScore.Reason2.Code);
				//		szMessage +=
				//			string.Format("\nReason3: {0}, Code: {1}.", oScore.Reason3.Description, oScore.Reason3.Code);
				//		szMessage +=
				//			string.Format("\nReason4: {0}, Code: {1}.", oScore.Reason4.Description, oScore.Reason4.Code);
				//	}

				//	oRepInfo.Messages = szMessage;
				//}

				Debug.WriteLine(szMessage);
			}

			// Print and return any error
			if (szErrMsg != null && !szErrMsg.Equals(string.Empty))
			{
				oRepInfo.AnyError = true;
				oRepInfo.Messages += string.Format("\r\nINTERPRATIVE ERROR MESSAGE: \r\n{0}", szErrMsg);
			}

			// Save Report to Database
			var creditReport = new QL_CreditReport
			{
				LeadId = oWSLead.LeadID,
				BureauId = GetBureauIdString(eBureau),
				Score = short.Parse(oRepInfo.Score.ToString(CultureInfo.InvariantCulture)),
				IsHit = true,  // TODO:  This needs to be implemented.
				IsActive = true,
				IsDeleted = false,
				CreatedBy = szLogonUser,
				CreatedOn = DateTime.Now
			};
			var creditReportVendorMicrobilt = new QL_CreditReportVendorMicrobilt
			{
				BureauId = GetBureauIdString(eBureau),
				Score = short.Parse(oRepInfo.Score.ToString(CultureInfo.InvariantCulture)),
				IsHit = true,
				CreditReport = szRawData,
				MicroBiltGUID = szGUID,
				CreatedBy = szLogonUser,
				CreatedOn = DateTime.Now
			};

			//if (oWSLead.DOB != null && !oWSLead.DOB.Equals(string.Empty))
			//	oDBCreditReport.DOB = DateTime.Parse(oWSLead.DOB);
			creditReportVendorMicrobilt.Save(szLogonUser);

			// ** Tie parent table to microbilt table
			creditReport.CreditReportVendorMicrobiltId = creditReportVendorMicrobilt.CreditReportVendorMicrobiltID;
			creditReport.Save(szLogonUser);

			oRepInfo.CreditReportID = creditReport.CreditReportID;

			if (OnCreditRan != null)
				OnCreditRan(this, oRepInfo);

			return creditReportVendorMicrobilt;
		}

		/// <summary>
		/// This method makes the call to Microbilt for retrieving the Credit Report based on the product type that is being passed.
		/// </summary>
		/// <param name="szBureau">string</param>
		/// <param name="szProduct">string</param>
		/// <param name="lead">WSLead</param>
		/// <param name="address">WSAddress</param>
		/// <param name="szGUID">string</param>
		/// <returns>string</returns>
		private string _getRawReport(string szBureau, string szProduct, WSLead lead, WSAddress address, ref string szGUID)
		{
			// Locals
			string szResult;
			var client = new WebClient();
			var valCol = new NameValueCollection();
			int nCounter = 0;

			/** Argument Validation
			 */
			if (string.IsNullOrEmpty(lead.FirstName)) throw new Exception("FirstName must be passed.");
			if (string.IsNullOrEmpty(lead.LastName)) throw new Exception("LastName must be passed.");

			if (szGUID.Equals(string.Empty))
			{
				// Begin the Post request
				Debug.WriteLine("|= START Request Post: {0,30}", _urlPost);
				Debug.WriteLine("|- START Create Form to post... {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

				valCol.Add("Member_ID", _username);
				valCol.Add("Password", _password);
				valCol.Add("Bureau", szBureau);
				valCol.Add("Product", szProduct);
				valCol.Add("FirstName", lead.FirstName.ToUpper());
				valCol.Add("LastName", lead.LastName.ToUpper());
				if (!string.IsNullOrEmpty(lead.MiddleName))
					valCol.Add("MiddleName", lead.MiddleName.ToUpper());

				// Validate and make sure that there is either a SSN and/or a DOB.
				/*            if (lead.SocialSecurity.Equals(string.Empty) && lead.DOB.Equals(string.Empty))
								throw new CrExceptionFailure("Validation Failure: In '_getRawReport' SSN and DOB is missing.");
				*/
				// Add SSN and/or DOB
				if (!lead.SocialSecurity.Equals(string.Empty)) valCol.Add("SSN", lead.SocialSecurity);
				if (!lead.DOB.Equals(string.Empty)) valCol.Add("DOB", lead.DOB);

				// Add adddress information
				/*
				 * Pass the address type
				 *  S - Standard
				 *  P - PO Box          (We don't use)
				 *  R - Rural Route     (We don't use)
				 */
				valCol.Add("AddrType", address.AddressType);
				valCol.Add("StreetNum", !string.IsNullOrEmpty(address.HouseNumber) ? address.HouseNumber.ToUpper() : string.Empty);
				valCol.Add("StreetName", !string.IsNullOrEmpty(address.StreetName) ? address.StreetName.ToUpper() : string.Empty);
				if ((address.StreetType != null) && (!address.StreetType.Equals(string.Empty)))
					valCol.Add("StreetType", address.StreetType);
				valCol.Add("City", address.City.ToUpper());
				valCol.Add("State", address.State.ToUpper());
				valCol.Add("Zip", address.PostalCode.ToUpper());
				if (!address.AptNumber.Equals(string.Empty))
					valCol.Add("Apt", address.AptNumber.ToUpper());

				// Submit and acquire GUID from Microbilt.
				Debug.WriteLine("|- START Acquireing GUID... {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

				Byte[] postResponse = client.UploadValues(_urlPost, valCol);
				szGUID = Encoding.ASCII.GetString(postResponse);

				// Show the query string submitted
				Debug.WriteLine("|- QueryString: {0}", client.QueryString.ToString());
				Debug.WriteLine("|- END   Acquireing GUID... {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

				Debug.WriteLine("|- END   Create Form to post... {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("|= END   Request Post: {0,30}", _urlPost);

				// Check to make sure that we got a GUID
				if (szGUID.Equals(string.Empty))
				{
					throw new CrExceptionGUIDAcquiring(
						"Failure Acquiring GUID...  After submitting the post no GUID was returned.");
				}
			}

			// Print Guid
			Debug.WriteLine("|-       GUID: {0}", szGUID);

			/*
			 * GET DATA
			 * This step is to acquire the actual report.  Since Microbilt has issues we will loop
			 * x number of times to get it working correctly.
			 */

			// Clear all key/value pairs from the collection and add the ID for the GUID.
			valCol.Clear();
			valCol.Add("ID", szGUID);

			Debug.WriteLine("|= START Looping to get Raw Data");

			do
			{
				// Increment counter
				nCounter++;

				// Post Request
				Byte[] getResponse = client.UploadValues(_urlGet, valCol);
				szResult = Encoding.ASCII.GetString(getResponse);

				// Check result
				if (!szResult.Equals("ZZZ"))
				{
					Debug.WriteLine("|= TRY ({0} of {1}) SUCCEEDED.", nCounter, _MAX_NUMBER_OF_TRIES);
					break;
				}
				
				/** Default path of execution */
				// szResult = string.Empty;
				// Remove the 'ZZZ' other wise the calling method will think there is something there.
				Debug.WriteLine("|= TRY ({0} of {1} FAILED)", nCounter, _MAX_NUMBER_OF_TRIES);

				// Pause
				Thread.Sleep(_PAUSE_BETWEEN_TRY);
			} while (nCounter <= _MAX_NUMBER_OF_TRIES);

			Debug.WriteLine("|= END   Looping to get Raw Data");

			// Check to see if the last request went through
			if (szResult.Equals("ZZZ"))
			{
				// Through exception
				throw new CrExceptionRequestTimeOut(string.Format("Request to Bureau '{0}' timed out. &nbsp;This happens when Microbilts queue is full and can't process all request."
					, szBureau)
					, szBureau
					, szGUID);
			}

			// Return result
			return szResult;
		}

		/** Return appropriet bureau ID's */

		public static string GetBureauIdString(CreditBureaus bureau)
		{
			switch (bureau)
			{
				case CreditBureaus.Transunion:
					return QL_CreditReportBureau.MetaData.TransUnionID;
				case CreditBureaus.Experian:
					return QL_CreditReportBureau.MetaData.ExperianID;
				case CreditBureaus.Equifax:
					return QL_CreditReportBureau.MetaData.EquafaxID;
				case CreditBureaus.Manual:
					return QL_CreditReportBureau.MetaData.ManualID;
			}

			// ** Default path of execution
			return QL_CreditReportBureau.MetaData.ManualID;
		}

		public static CreditBureaus GetBureauIdEnum(string bureau)
		{
			switch (bureau)
			{
				case QL_CreditReportBureau.MetaData.TransUnionID:
					return CreditBureaus.Transunion;
				case QL_CreditReportBureau.MetaData.ExperianID:
					return CreditBureaus.Experian;
				case QL_CreditReportBureau.MetaData.EquafaxID:
					return CreditBureaus.Equifax;
				default:
					return CreditBureaus.Manual;
			}
		}

		#endregion Methods
	}

	public class CreditReportClass
	{
		public long CreateCreditReport(string szRawData, out string szErrMsg)
		{
			throw new NotImplementedException();
		}

		public object Subjects { get; set; }
	}

	public enum CreditBureaus
	{
		Manual = 0,
		Transunion = 2,
		Equifax = 3,
		Experian = 4
	}

	#region Custom Exceptions Classes

	/// <summary>
	/// This class is used to pass messages back to the calling UI and let the user be aware of any problems.
	/// </summary>
	[Serializable]
	public class CrExceptionFailure : Exception
	{
		#region Constructors

		/// <summary>
		/// Override the base constructor to pass the message to display to the user.
		/// </summary>
		/// <param name="szMessage"></param>
		public CrExceptionFailure(string szMessage) : base(szMessage)
		{
		}

		#endregion Constructors
	}

	/// <summary>
	/// This class is used when 
	/// </summary>
	public class CrExceptionRequestTimeOut : Exception
	{
		#region Constructors

		public CrExceptionRequestTimeOut(string szMessage, string szBureau, string szGUID)
			: base(szMessage)
		{
			GUID = szGUID;
			// Get Bureau

			switch (szBureau)
			{
				case "TU":
					Bureau = CreditBureaus.Transunion;
					break;
				case "EX":
					Bureau = CreditBureaus.Experian;
					break;
				case "EQ":
					Bureau = CreditBureaus.Equifax;
					break;
			}
		}

		#endregion Constructors

		#region Properties

		public string GUID;
		public CreditBureaus Bureau;

		#endregion Properties

	}

	/// <summary>
	/// This exception is thrown when no GUID is obtained.  This could be because Microbilt is down.
	/// </summary>
	[Serializable]
	public class CrExceptionGUIDAcquiring : Exception
	{
		#region Constructors

		/// <summary>
		/// Override the base constructor to pass the message to display to the user.
		/// </summary>
		/// <param name="szMessage"></param>
		public CrExceptionGUIDAcquiring(string szMessage)
			: base(szMessage)
		{
		}

		#endregion Constructors
	}

	#endregion Custom Exceptions Classes


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
	public struct WSCreditReportInfo
	{
		public long CreditReportID;
		public long LeadId;
		public long? AccountId;
		public string BureauName;
		public string DOB;
		public string SSN;
		public int Score;
		public bool ScoreFound;
		public string Phone;
		public int PhoneStatus;
		public bool AnyError;
		public string Messages;
	}

	#endregion Serializable Structures
}
