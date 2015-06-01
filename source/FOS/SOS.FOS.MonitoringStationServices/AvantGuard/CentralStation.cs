using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NSE.FOS.Contracts.Models;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.MonitoringStationServices.AGSiteService;
using SOS.FOS.MonitoringStationServices.AvantGuard.Models;
using SOS.FOS.MonitoringStationServices.Contracts.Models;
using SOS.Lib.Core.ErrorHandling;
using Stages;
using NXS.Lib;

namespace SOS.FOS.MonitoringStationServices.AvantGuard
{
	public class CentralStation : IMonitoringStation
	{
		#region .ctor

		public CentralStation()
		{
			_username = WebConfig.Instance.GetConfig("AG_USERNAME");
			_password = WebConfig.Instance.GetConfig("AG_PASSWORD");
			_applname = WebConfig.Instance.GetConfig("AG_APPLNAME");
			_clenplat = WebConfig.Instance.GetConfig("AG_CLIENTPLATFORM");
			_appvrson = WebConfig.Instance.GetConfig("AG_APPLIC_VERISON");
			_urledpnt = WebConfig.Instance.GetConfig("AG_GATEWAY_ENDPNT");

			_stagesApiClient = new StagesGateway(_urledpnt);
		}

		#endregion .ctor

		#region Member Variables

		public SessionInfo SessionInfo { get; private set; }

		private const string _NOT_IMPLEMENTED = "[Not Implemented]";
		private const string _NO_GENDER_SPECIFIED = "N";
		private const string _CONTACT_AUTHORITY_ALLACCESS = "12";
		private const string _CONTACT_AUTHORITY_CONTONLY = "10";
		private readonly string _username;
		private readonly string _password;
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly string _applname;
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly string _clenplat;
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly string _appvrson;
		private readonly string _urledpnt;

		private readonly StagesGateway _stagesApiClient;

		private MS_Account _account;

		#endregion Member Variables

		#region Member Functions.

		#region Public

		public SessionInfo SessionAuthenticate()
		{
			/** Initialize. */
			var oWatch = new Stopwatch();

			oWatch.Start();
			//QueryResult<EmptyResult, SessionInfo> oQryResult =
			//    _stagesAPIClient.Login(_username, _password, null, _applname, _appvrson, _clenplat);
			QueryResult<EmptyResult, SessionInfo> oQryResult =
				_stagesApiClient.Login(_username, _password);
			oWatch.Stop();
			Debug.WriteLine("AG Login time: {0}", oWatch.Elapsed);

			/** Get result. */
			SessionInfo = oQryResult.OutputParameter;

			/** Check for a successfull validation. */
			if (oQryResult.ErrorTypeNum != 0)
				throw new AGExceptions.AGExceptionInvalidAuthentication(oQryResult.ErrorTypeNum, oQryResult.UserErrorMessage);

			/** Return result. */
			return SessionInfo;
		}

		public void SessionTermination(bool bIsForced, string slogContext)
		{
			/** Check that there is an active session. */
			if (SessionInfo != null)
			{
				/** Initialize. */
				_stagesApiClient.Logout(SessionInfo.SessionNum, ConvertPassword()
					, bIsForced, slogContext);

				/** Check result. */
				//if (!oResult.Success)
				//    throw new AGExceptions.AGExceptionErrorTerminationSession();

				/** Set SessionInfo to null. */
				SessionInfo = null;
			}
		}

		#region AccountCreate
		public IFosResult<MS_AccountSubmit> AccountCreate(MS_AccountSubmit msAccountSubmit, string gpEmployeeId, bool? isInService = true)
		{
			// ** Initialize
			SiteGroupGatewaySoap clientSoap = new SiteGroupGatewaySoapClient();

			var siteInfo = new Site();
			var signals = new Signals();
			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(msAccountSubmit.AccountId);
			var mcAddress = SosCrmDataContext.Instance.MC_AddressesViews.GetByAccountId(msAccountSubmit.AccountId);
			var aeCustomer = SosCrmDataContext.Instance.AE_CustomerInformationViews.GetByAccountId(msAccountSubmit.AccountId);

			#region Set Premise Address

			siteInfo.SiteName = string.Format("{0} ({1})", aeCustomer.FullName, msAccount.AccountID);
			siteInfo.SiteAddress = mcAddress.StreetAddress;
			siteInfo.SiteAddr2 = mcAddress.StreetAddress2;
			siteInfo.City = mcAddress.City;
			siteInfo.State = mcAddress.StateAB;
			siteInfo.ZipCode = string.IsNullOrEmpty(mcAddress.PlusFour)
				? mcAddress.PostalCode
				: string.Format("{0}-{1}", mcAddress.PostalCode, mcAddress.PlusFour);
			siteInfo.County = mcAddress.County;
			siteInfo.SiteType = "R";  // M: Pers -- R: Residential -- C: Comercial

			// ** Dispatch Types
			// // ** Get system type or default it.
			if (string.IsNullOrEmpty(msAccount.SystemTypeId) ||
				msAccount.SystemTypeId.Equals(MS_AccountSystemType.MetaData.Two_WayID) ||
				msAccount.SystemTypeId.Equals(MS_AccountSystemType.MetaData.Two_Way_Over_CellularID))
			{
				var dispatchTypes = new List<DispatchType>
				{
					new DispatchType
					{
						DispatchType1 = "2W"
					}
				};
				siteInfo.DispatchTypes = dispatchTypes.ToArray();
			}

			#endregion Set Premise Address

			#region Phones
			// ** Get monitored parties
			AE_CustomerAccountCollection moniParties =
				SosCrmDataContext.Instance.AE_CustomerAccounts.GetByAccountId(_account.AccountID);

			// ** Set Site phones 
			var phones = new List<Phone>();
			foreach (var moniParty in moniParties)
			{
				if (!string.IsNullOrEmpty(moniParty.Customer.PhoneMobile))
				{
					phones.Add(new Phone
					{
						AutoNotifyFlag = true,
						PhoneNumber = moniParty.Customer.PhoneMobile,
						PhoneType = "C" // Cellular
					});
				}
				if (!string.IsNullOrEmpty(moniParty.Customer.PhoneHome))
				{
					phones.Add(new Phone
					{
						AutoNotifyFlag = true,
						PhoneNumber = moniParty.Customer.PhoneHome,
						PhoneType = "H" // Home
					});
				}
				if (!string.IsNullOrEmpty(moniParty.Customer.PhoneWork))
				{
					phones.Add(new Phone
					{
						AutoNotifyFlag = false,
						PhoneNumber = moniParty.Customer.PhoneWork,
						PhoneType = "W" // Work
					});
				}
			}

			siteInfo.Phones = phones.ToArray();
			#endregion Phones

			#region Code Words
			// ** Set Codewords. */
			var passPhrases = string.IsNullOrEmpty(_account.AccountPassword) ? "[Nexsense Shell];".Split(';') : _account.AccountPassword.Split(';');
			Codeword[] codewords = passPhrases.Select(passPhrase => new Codeword
			{
				Codeword1 = passPhrase
			}).ToArray();
			siteInfo.Codewords = codewords;
			#endregion Code Words

			#region Contacts
			//// ** Contacts
			//var contactsList = new List<Contact>();
			//var msContactsList = SosCrmDataContext.Instance.MS_EmergencyContacts.ByAccountId(_account.AccountID);
			//List<Allergies> allergiesList = null;
			//foreach (var msContact in msContactsList)
			//{
			//	if (msContact.Allergies != null)
			//	{
			//		var allergies = msContact.Allergies.Split(';');
			//		allergiesList = allergies.Select(allergy => new Allergies { AllergyCode = allergy }).ToList();
			//	}
			//	var emails = msContact.Email.Split(';');
			//	var emailsList = emails.Select(email => new EmailAddress { EmailAddress1 = email, AutoNotifyFlag = true });
			//	var deviceUsersList = new List<DeviceUser>();
			//	deviceUsersList.Add(new DeviceUser { TransmitterCode = _NOT_IMPLEMENTED, UserId = _NOT_IMPLEMENTED });
			//	//var someContactList = new List<ContactListMember>();
			//	//someContactList.Add(new ContactListMember { ContactListType = _NOT_IMPLEMENTED, OrderNum = 0 });
			//	//var medicalConditionsList = new List<MedicalConditions>();
			//	//medicalConditionsList.Add(new MedicalConditions { MedicalConditionCode = _NOT_IMPLEMENTED, Comment = _NOT_IMPLEMENTED });
			//
			//	/** Finalize all databindings. */
			//	contactsList.Add(new Contact
			//	{
			//		OrderNum = msContact.OrderNumber,
			//		FirstName = msContact.FirstName,
			//		LastName = msContact.LastName,
			//		EmailAddresses = emailsList.ToArray(),
			//		// Pin = msContact.Password,
			//		Authority = msContact.Relationship.IsEVC ? _CONTACT_AUTHORITY_ALLACCESS : _CONTACT_AUTHORITY_CONTONLY,
			//		Relation = msContact.Relationship.MsRelationshipId,
			//		EcvFlag = msContact.Relationship.IsEVC,
			//		KeysFlag = msContact.HasKey,
			//		// DeviceUsers = deviceUsersList.ToArray(),
			//		// ContactList = someContactList.ToArray(),
			//		DateOfBirth = msContact.DOB,
			//		Gender = _NO_GENDER_SPECIFIED,
			//		// HospitalPreference = _NOT_IMPLEMENTED,
			//		// PatientComment = _NOT_IMPLEMENTED,
			//		Allergies = allergiesList == null ? null : allergiesList.ToArray(),
			//		// MedicalConditions = medicalConditionsList.ToArray()
			//	});
			//}
			//
			//Contact[] contacts = contactsList.ToArray();
			//siteInfo.Contacts = contacts;
			siteInfo.Contacts = GetContacts(_account.AccountID).ToArray();
			#endregion Contacts

			#region Devices

			var devicesList = new List<Device>();
			var pointsList = new List<Point>();
			// ** Build Equipments list
			MS_AccountZoneAssignmentCollection zoneAssignments =
				SosCrmDataContext.Instance.MS_AccountZoneAssignments.GetZoneAssignmentsByAccountId(_account.AccountID);
			foreach (var zoneAssignment in zoneAssignments)
			{
				/** TODO: Figure out if the equipment has a location. 
				var equipLoc = (zoneAssignment.AccountEquipment.EquipmentLocation == null)
					? "LOC NOT SET"
					: zoneAssignment.AccountEquipment.EquipmentLocation.AvantGuardCode;*/
				/** TODO: Figure out if the equipment has an event type. 
				var eventType = (zoneAssignment.AccountEvent == null)
					? "No Event"
					: zoneAssignment.AccountEvent.AvantGuardEvent.event_id;*/
				pointsList.Add(new Point
				{
					Point1 = zoneAssignment.Zone,
					// AreaNum = (int?)zoneAssignment.AccountZoneAssignmentID,
					Description = zoneAssignment.Comments,
					// EqLoc = equipLoc,
					EqType = zoneAssignment.AccountEquipment.Equipment.EquipmentType.EquipmentType,
					// EventCode = eventType,
					// SignalCode = zoneAssignment, TODO: -- This is for figuring out what language the device will be communicating to the CS. Either CID or SIA.  Based on the panel type.
					// SignalStatus = "[Field Not needed.]"
				});
			}

			// ** Get Transmitter code
			var transmistterCode = msAccountSubmit.IndustryAccount != null
				? msAccountSubmit.IndustryAccount.Csid
				: msAccount.IndustryAccount.Csid;
			// ** Identify Device type
			var deviceType = "MISC";
			if (string.IsNullOrEmpty(msAccount.SystemTypeId) ||
				msAccount.SystemTypeId.Equals(MS_AccountSystemType.MetaData.Two_WayID) ||
				msAccount.SystemTypeId.Equals(MS_AccountSystemType.MetaData.Two_Way_Over_CellularID))
			{
				deviceType = "2WBURG";
			}
			devicesList.Add(new Device
				{
					TransmitterCode = transmistterCode,
					DeviceType = deviceType,
					Points = pointsList.ToArray(),
					ListenInDeviceType = deviceType.Equals("2WBURG") ? "2W1" : null,
					InServiceFlag = isInService
				});
			siteInfo.Devices = devicesList.ToArray();
			#endregion Devices

			// ** Execute the call.
			Result clientResult = clientSoap.ImportSite(_username, _password, siteInfo, signals);

			// ** Check the result.
			msAccountSubmit.DateSubmitted = DateTime.UtcNow;
			msAccountSubmit.WasSuccessfull = (clientResult.ErrorNum == 0);
			msAccountSubmit.Message = clientResult.ErrorMessage;
			msAccountSubmit.Save(gpEmployeeId);

			// ** Create AvantGuard record
			var msAccountSubAG = new MS_AccountSubmitAG();
			msAccountSubAG.AccountSubmitAGID = msAccountSubmit.AccountSubmitID;
			msAccountSubAG.ErrorTypeNum = clientResult.ErrorNum;
			msAccountSubAG.Success = (clientResult.ErrorNum == 0);
			msAccountSubAG.UserErrorMessage = clientResult.ErrorMessage;
			msAccountSubAG.Save(gpEmployeeId);

			// ** Save sales information.
			if (msAccountSubmit.WasSuccessfull)
			{
				var msAccountSalesInformation =
					SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(msAccountSubmit.AccountId);
				msAccountSalesInformation.AccountSubmitId = msAccountSubmit.AccountSubmitID;
				msAccountSalesInformation.SubmittedToCSDate = DateTime.UtcNow;
				msAccountSalesInformation.Save(gpEmployeeId);
			}

			// ** Create result envelop.
			var result = new FosResult<MS_AccountSubmit>
			{
				Code = clientResult.ErrorNum,
				Message = clientResult.ErrorMessage,
				Value = msAccountSubmit
			};

			// ** Return result.
			return result;
		}

		public DeviceCopyQueryResult AccountCreate(string szTemplateTransmitter, string szStartTransmitterCode, string szEndTransmitterCode
			, bool? bCopySiteGroupsFlag, bool? bCopySiteAgenciesFlag, bool? bCopyContactsFlag, bool? bCopyMailAddresFlag
			, bool? bCopyPremisePhoneFlag, bool? bCopySchedulesFlag, bool? bCopyDeviceConfigFlag
			, bool? bCopySiteDispatchTypesFlag, bool? bCopySiteHoldaysFlag, bool? bCopySiteRulesFlag
			, bool? bCopyRulesFlag, bool? bCopySiteAutoProcessRulesFlag, bool? bCopyCodeWordsFlag
			, bool? bCopyContractItemsFlag, bool? bCopyEventRulesFlag, bool? bCopySiteUDFFlag, bool? bCopyDeviceUDDFFlag)
		{
			DeviceCopyQueryResult oResult = _stagesApiClient.DeviceCopy(SessionInfo.SessionNum
							, SessionInfo.SessionPassword
							, szTemplateTransmitter
							, szStartTransmitterCode
							, szEndTransmitterCode
							, bCopySiteGroupsFlag
							, bCopySiteAgenciesFlag
							, bCopyContactsFlag
							, bCopyMailAddresFlag
							, bCopyPremisePhoneFlag
							, bCopySchedulesFlag
							, bCopyDeviceConfigFlag
							, bCopySiteDispatchTypesFlag
							, bCopySiteHoldaysFlag
							, bCopySiteRulesFlag
							, bCopyRulesFlag
							, bCopySiteAutoProcessRulesFlag
							, bCopyCodeWordsFlag
							, bCopyContractItemsFlag
							, bCopyEventRulesFlag
							, bCopySiteUDFFlag
							, bCopyDeviceUDDFFlag);

			/** Check result. */
			if (oResult.ErrorTypeNum == (int)AGErrorCodes.InvalidSession)
				throw new AGExceptions.AGExceptionInvalidSession("Occurred at AccountCreate");

			/** Check for failure on the call. */
			if (oResult.ErrorTypeNum != 0
				|| !oResult.Success
				|| !string.IsNullOrWhiteSpace(oResult.UserErrorMessage))
				throw new AGExceptions.AGExceptionDeviceCopy(oResult.ErrorTypeNum, oResult.Success, oResult.UserErrorMessage);

			/** Return result. */
			return oResult;
		}

		public DeviceCopyQueryResult AccountCreate(string szTemplateTransmitter, string szStartTransmitter, string szEndTransmitter)
		{
			return AccountCreate(szTemplateTransmitter, szStartTransmitter, szEndTransmitter
				, null, null, null, null, null, null, null, null, null, null,
							null, null, null, null, null, null, null);
		}
		#endregion AccountCreate

		#region AccountServiceStatusSet

		public IFosResult<MS_AccountSubmit> AccountServiceStatusSet(MS_AccountSubmit msSubmit, string oosCat, DateTime? startDate, string startTime, string commentBig, string gpEmployeeId)
		{
			/** Initialize. */
			var fosResult = new FosResult<MS_AccountSubmit>();
			var stagesAPI = new StagesGateway(_urledpnt);
			int? devNum, siteNum;

			/** Authenticate. */
			var authResult = stagesAPI.Login(_username, _password);
			var session = authResult.OutputParameter;

			// Check that there is an AG information.
			GetDevNumAndSiteNumByXmit(msSubmit, stagesAPI, session, out devNum, out siteNum);

			DeviceInOutServiceQueryResult response = stagesAPI.DeviceInOutService(session.SessionNum, session.SessionPassword, devNum, siteNum, oosCat, startDate, startTime,
				commentBig, msSubmit.IndustryAccount.Csid);

			// Capture result.
			fosResult = AGEnvelops.GetResult(fosResult, response);

			msSubmit.WasSuccessfull = fosResult.Code == (int)AGErrorCodes.Success;
			switch (oosCat)
			{
				case AGOOSTypes.ACTIVE:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On;
					break;
				case AGOOSTypes.PENDING:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On_Pending;
					break;
				case AGOOSTypes.CANCEL:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On_Cancel;
					break;
			}
			msSubmit.Save();

			var msSubmitAG = new MS_AccountSubmitAG
			{
				AccountSubmitAGID = msSubmit.AccountSubmitID,
				ErrorTypeNum = response.ErrorTypeNum,
				SqlConnectionLost = response.SqlConnectionLost,
				Success = response.Success,
				UserErrorMessage = response.UserErrorMessage
			};
			msSubmitAG.Save(gpEmployeeId);

			/** Return result. */
			return fosResult;
		}

		#endregion AccountServiceStatusSet

		#endregion Public

		#region Private
		private byte[] ConvertPassword()
		{
			/** Initialize. */
			var encoding = new System.Text.ASCIIEncoding();
			Byte[] bytes = encoding.GetBytes(_password);

			/** Return result. */
			return bytes;
		}

		private void GetDevNumAndSiteNumByXmit(MS_AccountSubmit msSubmit, StagesGateway stagesAPI, SessionInfo session, out int? devNum, out int? siteNum)
		{
			/** Initlialize. */
			var msAccountAG = SosCrmDataContext.Instance.MS_AccountAGs.LoadByPrimaryKey(msSubmit.AccountId);
			devNum = null;
			siteNum = null;

			// Check that there is an AG information.
			if (msAccountAG != null && msAccountAG.IsLoaded)
			{
				devNum = msAccountAG.DevNum;
				siteNum = msAccountAG.SiteNum;
				return;
			}

			/** Execute the search. */
			XtAdvancedSearchQueryResult searchResponse = stagesAPI.XtAdvancedSearch(session.SessionNum, session.SessionPassword, null, null, msSubmit.IndustryAccount.Csid, null, null, null, null, null, null, null, null, null
				, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1);

			/** Get Result. */
			var fosResult = AGEnvelops.GetResult(new FosResult<bool>(), searchResponse);

			/** Check result. */
			if (fosResult.Code != (int)AGErrorCodes.Success) return;

			/** Save values */
			devNum = searchResponse.ResultSet[0].DevNum;
			siteNum = searchResponse.ResultSet[0].SiteNum;

			/** Save info */
			// ReSharper disable once PossibleNullReferenceException
			msAccountAG.AccountID = msSubmit.AccountId;
			msAccountAG.XmitCode = msSubmit.IndustryAccount.Csid;
			if (devNum != null) msAccountAG.DevNum = devNum.Value;
			if (siteNum != null) msAccountAG.SiteNum = siteNum.Value;

			msAccountAG.Save();
		}

		#endregion Private

		#region Implemented Members

		public IFosResult<MS_AccountSubmit> AccountShell(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			// ** Load account info.
			_account = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(msAccountSubmit.AccountId);

			// ** Validate the load.
			if (!_account.IsLoaded) throw new Exception(string.Format("The 'AccountID' passed does not have an MS protion associated with it."));

			return AccountCreate(msAccountSubmit, gpEmployeeId, false);
		}

		public IFosResult<MS_AccountSubmit> AccountOnboard(MS_AccountSubmit msAccountSubmit, string gpEmployeeId)
		{
			// ** Load account info.
			_account = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(msAccountSubmit.AccountId);

			// ** Validate the load.
			if (!_account.IsLoaded) throw new Exception(string.Format("The 'AccountID' passed does not have an MS protion associated with it."));

			return AccountCreate(msAccountSubmit, gpEmployeeId);
		}

		public IFosResult<MS_AccountSubmit> AccountUpdate(long accountID, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}

		public FosResult<List<IFosSignalHistoryItem>> GetSignalHistory(DateTime startDate, DateTime endDate, string transmitterCode)
		{
			/** Initialize. */
			var history = new List<History>();
			var clientSoap = new SiteGroupGatewaySoapClient();

			/** Execute */
			DataResultOfListOfHistory result = clientSoap.GetHistory(_username, _password, transmitterCode, startDate, endDate);

			/** return error if not successful. */
			if (result.ErrorNum != 0)
			{
				return new FosResult<List<IFosSignalHistoryItem>>()
				{
					Code = result.ErrorNum, //TODO:  Error codes need to be defined.
					Message = string.Format("AvantGuard error: \"{0}\"", result.ErrorMessage)
				};
			}

			/** Build history. */
			history = result.Result1.ToList();
			var fosAgSignalHistoryList = history.Select(histItem => new FosAgSignalHistoryItem(histItem)).ToList();

			/** Return result. */
			return new FosResult<List<IFosSignalHistoryItem>>()
			{
				Code = 0,
				Message = "Success",
				Value = new List<IFosSignalHistoryItem>(fosAgSignalHistoryList)
			};
		}

		public FosResult<bool> UpdateContacts(long accountId)
		{
			// initialize
			var result = new FosResult<bool>()
			{
				Code = 0,
				Message = "",
				Value = true,
			};
			var clientSoap = new SiteGroupGatewaySoapClient();

			var successfulSubmissions = SosCrmDataContext.Instance.MS_AccountSubmits.GetSuccessfulForAccount(accountId);
			if (successfulSubmissions.Count <= 0)
			{
				// this is only a warning
				result.Code = 0;
				result.Message = string.Format("Account {0} has not been submitted to a monitoring station," +
					" so contacts were not pushed to a monitoring station.", accountId);
				result.Value = false;
			}
			else
			{
				var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);

				//clientSoap.GetSiteContacts(_username, _password, msAccount.IndustryAccount.Csid).Result1;

				var contacts = GetContacts(accountId).ToArray();
				// update contacts
				var updateResult = clientSoap.UpdateSiteContacts(_username, _password, msAccount.IndustryAccount.Csid, new Contacts
				{
					Contacts1 = contacts,
				});
				// check update result
				if (updateResult.ErrorNum != 0)
				{
					result.Code = updateResult.ErrorNum; //TODO:  Error codes need to be defined.
					result.Message = string.Format("Failed to update contacts. AvantGuard error: \"{0}\"", updateResult.ErrorMessage);
					result.Value = false;
				}
			}

			return result;
		}

		private List<Contact> GetContacts(long accountId)
		{
			var clientSoap = new SiteGroupGatewaySoapClient();

			var contacts = new List<Contact>();
			foreach (var msContact in SosCrmDataContext.Instance.MS_EmergencyContacts.ByAccountId(accountId))
			{
				var deviceUsersList = new List<DeviceUser>();
				deviceUsersList.Add(new DeviceUser { TransmitterCode = _NOT_IMPLEMENTED, UserId = _NOT_IMPLEMENTED });
				//var someContactList = new List<ContactListMember>();
				//someContactList.Add(new ContactListMember { ContactListType = _NOT_IMPLEMENTED, OrderNum = 0 });
				//var medicalConditionsList = new List<MedicalConditions>();
				//medicalConditionsList.Add(new MedicalConditions { MedicalConditionCode = _NOT_IMPLEMENTED, Comment = _NOT_IMPLEMENTED });

				var phones = new List<Phone>();
				TryAddPhone(phones, msContact.Phone1, msContact.Phone1Type);
				TryAddPhone(phones, msContact.Phone2, msContact.Phone2Type);
				TryAddPhone(phones, msContact.Phone3, msContact.Phone3Type);

				contacts.Add(new Contact
				{
					OrderNum = msContact.OrderNumber,
					FirstName = msContact.FirstName,
					LastName = msContact.LastName,
					EmailAddresses = msContact.Email == null ? (EmailAddress[])null : msContact.Email
							.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(email => new EmailAddress
							{
								EmailAddress1 = email,
								AutoNotifyFlag = true,
							}).ToArray(),
					// Pin = msContact.Password,
					Authority = msContact.Relationship.IsEVC ? _CONTACT_AUTHORITY_ALLACCESS : _CONTACT_AUTHORITY_CONTONLY,
					Relation = msContact.Relationship.MsRelationshipId,
					EcvFlag = msContact.Relationship.IsEVC,
					KeysFlag = msContact.HasKey,
					// DeviceUsers = deviceUsersList.ToArray(),
					// ContactList = someContactList.ToArray(),
					DateOfBirth = msContact.DOB,
					Gender = _NO_GENDER_SPECIFIED,
					// HospitalPreference = _NOT_IMPLEMENTED,
					// PatientComment = _NOT_IMPLEMENTED,
					Allergies = msContact.Allergies == null ? (Allergies[])null : msContact.Allergies
							.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(allergy => new Allergies
							{
								AllergyCode = allergy,
							}).ToArray(),
					// MedicalConditions = medicalConditionsList.ToArray()
					Phones = phones.ToArray(),
				});
			}
			return contacts;
		}
		private void TryAddPhone(List<Phone> phones, string phone, MS_EmergencyContactPhoneType phoneType)
		{
			phone = SOS.Lib.Util.StringHelper.NullIfWhiteSpace(phone);
			if (phoneType != null && phone != null)
			{
				phones.Add(new Phone
				{
					PhoneNumber = phone,
					PhoneType = phoneType.MsPhoneTypeId,
				});
			}
		}

		public FosResult<object> TwoWayTestData(long accountId)
		{
			var result = new FosResult<object>();
			var acctState = SosCrmDataContext.Instance.MS_AvantGuardAccountStates.LoadByPrimaryKey(accountId);
			if (acctState != null)
			{
				result.Value = acctState;
			}
			return result;
		}
		public FosResult<object> InitTwoWayTest(long accountId, string gpEmployeeId)
		{
			var result = new FosResult<object>();
			SessionInfo sess = null;
			MS_Account msAccount;
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			var effectiveDate = DateTime.Now; //DateTime.UtcNow; // UTC sets the wrong date
			var expireDate = effectiveDate.AddHours(1);
			var deviceOnTestResp = _gateway.DeviceOnTest(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum,
				TestCategory: "2wMed",
				TestType: "ALL",
				EffectiveDate: effectiveDate.Date,
				EffectiveTime: effectiveDate.TimeOfDay.ToString(),
				ExpireDate: expireDate.Date,
				ExpireTime: expireDate.TimeOfDay.ToString(),
				CommentBig: "Initiate Two-Way Test"
			);
			if (AGEnvelops.GetResult(result, deviceOnTestResp).Code != 0)
			{
				return result;
			}

			var acctState = SosCrmDataContext.Instance.MS_AvantGuardAccountStates.LoadByPrimaryKey(accountId);
			if (acctState == null)
			{
				acctState = new MS_AvantGuardAccountState
				{
					AccountID = accountId,
				};
			}
			// overwrite all previous data
			acctState.TwoWayTestStartedOn = DateTime.UtcNow;
			acctState.ConfirmedOn = null;
			acctState.ConfirmedBy = null;
			acctState.CreatedOn = DateTime.UtcNow;
			acctState.CreatedBy = gpEmployeeId;
			acctState.Save();

			result.Value = acctState;
			return result;

			//var clientSoap = new SiteGroupGatewaySoapClient();
			//var result = clientSoap.OnTest(
			//	UserName: _username,
			//	Password: _password,
			//	TransmitterCode: msAccount.IndustryAccount.Csid,
			//	TestCategory: testCategory, // "2wMed",
			//	StartDate: null,
			//	Hours: null,
			//	Minutes: null,
			//	Comment: comment
			//);
			//if (result.ErrorNum != 0)
			//{
			//	return new FosResult<bool>()
			//	{
			//		Code = result.ErrorNum, //TODO:  Error codes need to be defined.
			//		Message = string.Format("AvantGuard error: \"{0}\"", result.ErrorMessage)
			//	};
			//}

			//var app2PermissionListResp = _gateway.App2PermissionList(sess.SessionNum, sess.SessionPassword);
			//if (AGEnvelops.GetResult(new FosResult<bool>(), app2PermissionListResp).Code != 0)
			//{
			//	int t = 0;
			//}
			//var testCategoryInsertResp = stagesAPI.TestCategoryInsert(session.SessionNum, session.SessionPassword,
			//	TestCategory: "2wMed",
			//	TestCategoryDescription: "2-Way Medical Testing",
			//	DefaultHours: 1,
			//	LogEventsFlag: true,
			//	RunawayFlag: false,
			//	ExternalFlag: true,
			//	ExpiredTestEvent: "1090" // Clear Test
			//);
			//if (AGEnvelops.GetResult(new FosResult<bool>(), testCategoryInsertResp).Code != 0)
			//{
			//	int t = 0;
			//}
		}
		public FosResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId)
		{
			var result = new FosResult<object>();

			var acctState = SosCrmDataContext.Instance.MS_AvantGuardAccountStates.LoadByPrimaryKey(accountId);
			if (acctState == null)
			{
				result.Code = -1;
				result.Message = "Two-Way test not started";
				return result;
			}
			if (acctState.ConfirmedBy != null)
			{
				result.Code = -1;
				result.Message = string.Format("Two-Way already confirmed by '{0}' on {1}", acctState.ConfirmedBy, acctState.ConfirmedOn);
				return result;
			}

			acctState.ConfirmedOn = DateTime.UtcNow;
			acctState.ConfirmedBy = confirmedBy;
			acctState.Save();

			result.Value = acctState;
			return result;
		}
		public FosResult<List<IFosDeviceTest>> ActiveTests(long accountId)
		{
			var result = new FosResult<List<IFosDeviceTest>>();
			SessionInfo sess = null;
			MS_Account msAccount;
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			//var deviceTestInEffectListResp = _gateway.DeviceTestInEffectList(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum);
			//if (AGEnvelops.GetResult(new FosResult<bool>(), deviceTestInEffectListResp).Code != 0)
			//{
			//	int t = 0;
			//}

			//var testCategoryListResp = _gateway.TestCategoryList(sess.SessionNum, sess.SessionPassword);
			//if (AGEnvelops.GetResult(result, testCategoryListResp).Code != 0)
			//{
			//	return result;
			//}
			//var testTypeListResp = _gateway.TestTypeList(sess.SessionNum, sess.SessionPassword);
			//if (AGEnvelops.GetResult(result, testTypeListResp).Code != 0)
			//{
			//	return result;
			//}
			var resp = _gateway.DeviceTestsList(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum);
			if (AGEnvelops.GetResult(result, resp).Code != 0)
			{
				return result;
			}

			var list = new List<IFosDeviceTest>();
			foreach (var test in resp.ResultSet)
			{
				if (test.InEffect != "Y")
				{
					continue;
				}

				list.Add(new FosDeviceTest
				{
					TestNum = test.TestNum,
					TestCategory = test.TestCategory,
					TestCategoryDescription = test.TestCategoryDescription,
					TestType = test.TestType,

					// When we put a device on test we use local time,
					// but when the times are deserialized they are UTC.
					// This ensures the Kind is Local.
					EffectiveOn = SpecifyLocalKind(test.TestEffectiveDate),
					ExpiresOn = SpecifyLocalKind(test.TestExpireDate),
				});
			}

			result.Value = list;
			return result;
		}
		public FosResult<bool> ClearActiveTests(long accountId)
		{
			var result = new FosResult<bool>();
			SessionInfo sess = null;
			MS_Account msAccount;
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			var resp = _gateway.DeviceTestInEffectClear(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum);
			result.Value = AGEnvelops.GetResult(result, resp).Code == 0;
			return result;
		}
		public FosResult<bool> ClearTest(long accountId, int testNum)
		{
			return ClearTest(accountId, testNum, null);
		}

		private FosResult<bool> ClearTest(long accountId, int testNum, SessionInfo sess)
		{
			var result = new FosResult<bool>();
			MS_Account msAccount;
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			var resp = _gateway.DeviceClearTest(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum,
				TestNum: testNum,
				AuthorizationOverrideFlag: false
			);
			result.Value = AGEnvelops.GetResult(result, resp).Code == 0;
			return result;
		}
		public FosResult<ISystemStatusInfo> ServiceStatus(long accountId, string gpEmployeeId)
		{
			var result = new FosResult<ISystemStatusInfo>();
			SessionInfo sess = null;
			MS_Account msAccount;
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			//var resp = _gateway.OOSCategoryList(sess.SessionNum, sess.SessionPassword);
			var resp = _gateway.DeviceDetail(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum);
			if (resp.ResultSet.Count > 0)
			{
				var systemInfo = new SystemStatusInfo(string.IsNullOrEmpty(resp.ResultSet[0].OOSCat),
					string.IsNullOrEmpty(resp.ResultSet[0].OOSCat));
				result.Value = systemInfo;
			}
			return result;
		}
		public FosResult<string> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId)
		{
			var result = new FosResult<string>();
			SessionInfo sess = null;
			MS_Account msAccount;
			var msAccountSalesInfo = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(accountId);
			int devNum, siteNum;
			if (GetAvantGuardNums(result, accountId, ref sess, out msAccount, out siteNum, out devNum).Code != 0)
			{
				return result;
			}

			var app2PermissionListResp = _gateway.App2PermissionList(sess.SessionNum, sess.SessionPassword);
			if (AGEnvelops.GetResult(new FosResult<bool>(), app2PermissionListResp).Code != 0)
			{
				int t = 0;
			}

			var resp = _gateway.DeviceInOutService(sess.SessionNum, sess.SessionPassword, SiteNum: siteNum, DevNum: devNum,
				OOSCat: oosCat,
				StartDate: startDate.Date,
				StartTime: startDate.TimeOfDay.ToString(),
				CommentBig: comment,
				ConfirmXmit: msAccount.IndustryAccount.Csid
			);
			AGEnvelops.GetResult(result, resp);

			// create MS_AccountSubmit
			var msSubmit = new MS_AccountSubmit
			{
				AccountId = msAccount.AccountID,
				IndustryAccountId = msAccount.IndustryAccountId,
				AccountSubmitTypeId = (short)MS_AccountSubmitType.AccountSubmitTypeEnum.Undefined,
				GPTechId = msAccountSalesInfo.TechId ?? gpEmployeeId, //@REVIEW: default to gpEmployeeId?
				MonitoringStationOSId = msAccount.IndustryAccount.ReceiverLine.MonitoringStationOSId,
				DateSubmitted = DateTime.UtcNow,
				WasSuccessfull = result.Code == (int)AGErrorCodes.Success,
			};
			switch (oosCat)
			{
				case AGOOSTypes.ACTIVE:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On;
					break;
				case AGOOSTypes.PENDING:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On_Pending;
					break;
				case AGOOSTypes.CANCEL:
					msSubmit.AccountSubmitTypeId = (int)MS_AccountSubmitType.AccountSubmitTypeEnum.Turn_Service_On_Cancel;
					break;
			}
			msSubmit.Save(gpEmployeeId);

			// create MS_AccountSubmitAG
			var msSubmitAG = new MS_AccountSubmitAG
			{
				AccountSubmitAGID = msSubmit.AccountSubmitID,
				ErrorTypeNum = resp.ErrorTypeNum,
				SqlConnectionLost = resp.SqlConnectionLost,
				Success = resp.Success,
				UserErrorMessage = resp.UserErrorMessage
			};
			msSubmitAG.Save(gpEmployeeId);

			result.Value = oosCat;
			return result;
		}


		public FosResult<bool> GenerateMetaData(long? accountId = null, string username = "SYSTEM")
		{
			throw new NotImplementedException();
		}

		public FosResult<List<MS_DispatchAgency>> FindDispatchAgency(string agencyTypeId, string phone, string city, string state, string zip, string gpEmployeeId)
		{
			throw new NotImplementedException();
		}

		public FosResult<bool> IsNotSlammedAccount(QL_Address address, QL_Lead lead)
		{
			return new FosResult<bool>
			{
				Code = BaseErrorCodes.ErrorCodes.Success.Code(),
				Message = "Avantguard won't care if this is one of their existing customers.",
				Value = true
			};
		}

		#endregion Implemented Members

		#endregion Member Functions.


		// use when datetime is correct local time but the Kind is not Local
		private DateTime? SpecifyLocalKind(DateTime? dt)
		{
			if (!dt.HasValue) return dt;

			return DateTime.SpecifyKind(dt.Value, DateTimeKind.Local).ToUniversalTime();
		}

		private FosResult<T> GetAvantGuardNums<T>(FosResult<T> fosResult, long accountId,
			ref SessionInfo sess, out MS_Account msAccount, out int siteNum, out int devNum)
		{
			msAccount = null;
			devNum = siteNum = 0;

			if (sess == null)
			{
				var authResp = _gateway.Login(_user, _pass);
				if (AGEnvelops.GetResult(fosResult, authResp).Code != (int)AGErrorCodes.Success)
				{
					return fosResult;
				}
				sess = authResp.OutputParameter;
			}

			msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
			if (msAccount == null)
			{
				fosResult.Code = -1;
				fosResult.Message = "No Account";
				return fosResult;
			}
			if (msAccount.IndustryAccount == null)
			{
				fosResult.Code = -1;
				fosResult.Message = "No Industry Account";
				return fosResult;
			}
			var csid = msAccount.IndustryAccount.Csid;

			var msAccountAG = SosCrmDataContext.Instance.MS_AccountAGs.LoadByPrimaryKey(accountId);
			// check for correct AG info
			if (msAccountAG != null && msAccountAG.XmitCode == csid)
			{
				siteNum = msAccountAG.SiteNum;
				devNum = msAccountAG.DevNum;
				return fosResult;
			}

			/** Execute the search. */
			XtAdvancedSearchQueryResult searchResponse = _gateway.XtAdvancedSearch(sess.SessionNum, sess.SessionPassword,
				TransmitterMatchFlag: true, // allow only exact matches
				TransmitterCode: csid,
				MaxRows: 1,
				IncludeOOSFlag: true
			);
			if (AGEnvelops.GetResult(fosResult, searchResponse).Code != (int)AGErrorCodes.Success)
			{
				return fosResult;
			}

			if (searchResponse.ResultSet.Count == 0)
			{
				fosResult.Code = -1;
				fosResult.Message = "Failed to find device on AvantGuard";
				return fosResult;
			}
			int? tmpSiteNum, tmpDevNum;
			tmpSiteNum = searchResponse.ResultSet[0].SiteNum;
			tmpDevNum = searchResponse.ResultSet[0].DevNum;
			if (!tmpSiteNum.HasValue || !tmpDevNum.HasValue)
			{
				fosResult.Code = -2;
				fosResult.Message = "Failed to find SiteNum and DevNum";
				return fosResult;
			}
			siteNum = tmpSiteNum.Value;
			devNum = tmpDevNum.Value;

			if (msAccountAG == null)
			{
				msAccountAG = new MS_AccountAG
				{
					AccountID = accountId,
				};
			}
			msAccountAG.XmitCode = csid;
			msAccountAG.SiteNum = siteNum;
			msAccountAG.DevNum = devNum;

			try
			{
				msAccountAG.Save();
			}
			catch (Exception ex)
			{
				var msAccountAG2 = SosCrmDataContext.Instance.MS_AccountAGs.LoadByPrimaryKey(accountId);
				if (msAccountAG2 == null)
				{
					throw ex;
				}

				// Another thread saved MS_AccountAG before this thread. I was going to use locking to prevent these exceptions,
				// but that's not a good idea for web apps that could be running on multiple servers.
				// Example of errors from stg: Violation of PRIMARY KEY constraint 'PK_MS_AccountAG'. Cannot insert duplicate key in object 'dbo.MS_AccountAG'. The duplicate key value is (150927).
				siteNum = msAccountAG2.SiteNum;
				devNum = msAccountAG2.DevNum;
			}

			return fosResult;
		}



		private static readonly string _user;
		private static readonly string _pass;
		private static readonly StagesGateway _gateway;
		static CentralStation()
		{
			_user = WebConfig.Instance.GetConfig("AG_USERNAME");
			_pass = WebConfig.Instance.GetConfig("AG_PASSWORD");

			var endpoint = WebConfig.Instance.GetConfig("AG_GATEWAY_ENDPNT");
			_gateway = new StagesGateway(endpoint);
		}
		//private static SessionInfo Session
		//{
		//	get
		//	{
		//		var authResult = _gateway.Login(_user, _pass);
		//		return authResult.OutputParameter;
		//	}
		//}
		//private static SessionInfo _sess;
		//private static SessionInfo Session
		//{
		//	get
		//	{
		//		lock (_gateway)
		//		{
		//			if (_sess == null || AGEnvelops.GetResult(new FosResult<bool>(),
		//					_gateway.LoginCheck(_sess.SessionNum, _sess.SessionPassword)).Code != 0 // i thought this was to check the login status, but it always return permission denied...
		//				)
		//			{
		//				var authResult = _gateway.Login(_user, _pass);
		//				_sess = authResult.OutputParameter;
		//			}
		//			return _sess;
		//		}
		//	}
		//}
	}
}
