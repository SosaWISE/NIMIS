/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/03/12
 * Time: 09:46
 * 
 * Description:  Implementes the MonitoringStation Services Service Engine
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using SOS.Data.GpsTracking;
using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.MonitoringStationServices;
using SOS.FOS.MonitoringStationServices.Contracts.Models;
using SOS.FOS.MonitoringStationServices.Monitronics;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Contracts.Models.Reporting;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.FunctionalServices.Models.HumanResource;
using SOS.FunctionalServices.Models.Reporting;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;
using Stages;
using System.Transactions;
using SubSonic;
using SOS.FunctionalServices.Helpers;

namespace SOS.FunctionalServices
{
	public class MonitoringStationService : IMonitoringStationService
	{
		#region Implementation of IMonitoringStationService

		public IFnsResult<IFnsAGResponseBase> CreateMobileAccount(string szTemplateTransmitter, string szStartTransmitter, string szEndTransmitter)
		{
			/** Initialize. */
			var oCentralStation = new FOS.MonitoringStationServices.AvantGuard.CentralStation();

			oCentralStation.SessionAuthenticate();
			DeviceCopyQueryResult oResult = oCentralStation.AccountCreate(szTemplateTransmitter, szStartTransmitter, szEndTransmitter);

			IFnsResult<IFnsAGResponseBase> oResultFinal = new FnsResult<IFnsAGResponseBase>
			{
				Code = (int)ErrorCodes.Success,
				Message = "Success",
				Value = new FnsAGResponseBase(oResult)
			};

			oCentralStation.SessionTermination(true, null);

			return oResultFinal;
		}

		public IFnsResult<IFnsAGResponseSignalBase> SendEmergencySignal(string sTransmitterCode, string sText
			, decimal? mLongitude, decimal? mLatitude, bool? bTestSignalFlag)
		{
			/** Initialize. */
			var oReceiver = new FOS.MonitoringStationServices.AvantGuard.Receiver();
			const string SZ_DISPLAY_FRASE = "Emergency Signal";

			/** Execute. */
			var oResult = oReceiver.SendSignal(
				false				//Poll PurchaseMessageDescription Flag
				, sTransmitterCode
				, "CID"				// Signal Format
				, "E100"			// Signal Code
				, "1"				// Point
				, null				// Area
				, null				// UserID the panel user
				, sText				// Any additional notes to store as part of the signal.
				, DateTime.Now
				, null
				, mLongitude		// GPS Longitude
				, mLatitude			// GPS Latitude
				, null
				, string.Format("http://maps.google.com/maps?q={0},+{1}({2})&hl=en&z=19", mLatitude, mLongitude, SZ_DISPLAY_FRASE)
				, null
				, bTestSignalFlag	// Test Flag Signal
				);

			/** Build response. */
			IFnsResult<IFnsAGResponseSignalBase> oResponse = new FnsResult<IFnsAGResponseSignalBase>
				{
					Code = oResult.ErrorNum,
					Message = oResult.ErrorMessage,
					Value = new FnsAGResponseSignalBase(oResult)
				};

			return oResponse;
		}

		public IFnsResult<IFnsAGResponseSignalBase> DispatchFromLaipacDeviceToAGCentralStation(string eventCodeId, decimal lattitude, decimal longitude, string dispatchMessage, string csid, bool? bTestSignalFlag)
		{
			/** Initialize. */
			var oReceiver = new FOS.MonitoringStationServices.AvantGuard.Receiver();
			const string SZ_DISPLAY_FRASE = "Emergency Signal";

			/** Determine signal code. */
			string sText;
			string szCode;
			switch (eventCodeId)
			{
				case LP_EventCode.MetaData.SOSbuttonpressedalertID:
				case LP_EventCode.MetaData.PanicSOSbuttonpressedalertID:
					sText = "SOS Button pressed.  Emergency Signal";
					szCode = "E100";
					break;
				case LP_EventCode.MetaData.G_Sensoralert1ID:
					sText = "Fall detection.  Emergency Signal";
					szCode = "E100";
					break;
				default:
					sText = "Emergency Signal";
					szCode = "E101";
					break;
			}

			/** Determine message to display to CS. */
			sText += string.IsNullOrEmpty(dispatchMessage)
				? ""
				: dispatchMessage;

			/** Execute. */
			var oResult = oReceiver.SendSignal(
				false				//Poll PurchaseMessageDescription Flag
				, csid
				, "CID"				// Signal Format
				, szCode			// Signal Code
				, "1"				// Point
				, null				// Area
				, null				// UserID the panel user
				, sText				// Any additional notes to store as part of the signal.
				, DateTime.Now
				, null
				, longitude		// GPS Longitude
				, lattitude			// GPS Latitude
				, null
				, string.Format("http://maps.google.com/maps?q={0},+{1}({2})&hl=en&z=19", lattitude, longitude, SZ_DISPLAY_FRASE)
				, null
				, bTestSignalFlag	// Test Flag Signal
				);

			/** Build response. */
			IFnsResult<IFnsAGResponseSignalBase> oResponse = new FnsResult<IFnsAGResponseSignalBase>
			{
				Code = oResult.ErrorNum,
				Message = oResult.ErrorMessage,
				Value = new FnsAGResponseSignalBase(oResult)
			};

			return oResponse;
		}

		public IFnsResult<IFnsAGResponseSignalBase> DispatchFromSSEDeviceToAGCentralStation(string eventCodeId, decimal lattitude, decimal longitude, string dispatchMessage, string csid, bool? bTestSignalFlag)
		{
			/** Initialize. */
			var oReceiver = new FOS.MonitoringStationServices.AvantGuard.Receiver();
			const string SZ_DISPLAY_FRASE = "Emergency Signal";

			/** Determine signal code. */
			string sText;
			string szCode;
			switch (eventCodeId)
			{
				case SS_CommandMessageName.MetaData.SOSID:
					sText = "SOS Button pressed.  Emergency Signal";
					szCode = "E100";
					break;
				case SS_CommandMessageName.MetaData.FDAID:
					sText = "Fall detection.  Emergency Signal";
					szCode = "E100";
					break;
				default:
					sText = "Emergency Signal";
					szCode = "E101";
					break;
			}

			/** Determine message to display to CS. */
			sText += string.IsNullOrEmpty(dispatchMessage)
				? ""
				: dispatchMessage;

			/** Execute. */
			var oResult = oReceiver.SendSignal(
				false				//Poll PurchaseMessageDescription Flag
				, csid
				, "CID"				// Signal Format
				, szCode			// Signal Code
				, "1"				// Point
				, null				// Area
				, null				// UserID the panel user
				, sText				// Any additional notes to store as part of the signal.
				, DateTime.Now
				, null
				, longitude		// GPS Longitude
				, lattitude			// GPS Latitude
				, null
				, string.Format("http://maps.google.com/maps?q={0},+{1}({2})&hl=en&z=19", lattitude, longitude, SZ_DISPLAY_FRASE)
				, null
				, bTestSignalFlag	// Test Flag Signal
				);

			/** Build response. */
			IFnsResult<IFnsAGResponseSignalBase> oResponse = new FnsResult<IFnsAGResponseSignalBase>
			{
				Code = oResult.ErrorNum,
				Message = oResult.ErrorMessage,
				Value = new FnsAGResponseSignalBase(oResult)
			};

			return oResponse;
		}

		#endregion Implementation of IMonitoringStationService

		#region Database CRUD
		#region Emergency Contacts
		public IFnsResult<IFnsMsEmergencyContact> EmergencyContactCreate(IFnsMsEmergencyContact contact, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactCreate";
			var result = new FnsResult<IFnsMsEmergencyContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (contact.CustomerId == null)
				{
					var mcAccountCustomer = SosCrmDataContext.Instance.AE_CustomerAccounts.ByAccountId(contact.AccountId);
					contact.CustomerId = mcAccountCustomer.CustomerId;
				}
				// ** Cast EMC's to database object.
				var newContact = (FnsMsEmergencyContact)contact;
				MS_EmergencyContact emc = newContact.GetMsEMC();
				emc.Save(gpEmployeeId);

				// update monitoring station
				var service = new Main(Main.MonitoringStations.AvantGuard);
				var updateContactsResult = service.UpdateContacts(contact.AccountId);
				// copy response states
				result.Message = updateContactsResult.Message;
				result.Code = updateContactsResult.Code;
				// check result
				if (updateContactsResult.Code == 0)
				{
					// ** Save result information
					result.Value = new FnsMsEmergencyContact(emc);
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsEmergencyContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMsEmergencyContact> EmergencyContactUpdate(IFnsMsEmergencyContact fnsMsEmergencyContact, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactUpdate";
			var result = new FnsResult<IFnsMsEmergencyContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Validate data. */
				if (fnsMsEmergencyContact.CustomerId == null)
				{
					var mcAccountCustomer = SosCrmDataContext.Instance.AE_CustomerAccounts.ByAccountId(fnsMsEmergencyContact.AccountId);
					fnsMsEmergencyContact.CustomerId = mcAccountCustomer.CustomerId;
				}

				// ** Cast EMC's to database object.
				var newContact = (FnsMsEmergencyContact)fnsMsEmergencyContact;
				MS_EmergencyContact emc = newContact.GetMsEMC();
				emc.Save(gpEmployeeId);

				// chose monitoring station
				bool shellAccount;
				var msChoice = Main.GetMsChoice(emc.Account.IndustryAccount.ReceiverLine, out shellAccount);

				// update monitoring station
				var service = new Main(msChoice);
				var updateContactsResult = service.UpdateContacts(fnsMsEmergencyContact.AccountId);
				// copy response states
				result.Message = updateContactsResult.Message;
				result.Code = updateContactsResult.Code;
				// check result
				if (updateContactsResult.Code == 0)
				{
					// ** Save result information
					result.Value = new FnsMsEmergencyContact(emc);
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsEmergencyContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMsEmergencyContact> EmergencyContactRead(long id)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactRead";
			var result = new FnsResult<IFnsMsEmergencyContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Cast EMC's to database object.
				var newContact = SosCrmDataContext.Instance.MS_EmergencyContacts.LoadByPrimaryKeySafe(id);

				// ** Build result
				if (newContact == null)
				{
					result.Code = (int)ErrorCodes.SqlResultIsEmpty;
					result.Message = string.Format("The EMC id you passed '{0}' does not exists.", id);
					return result;
				}

				// Get Fns value
				var fnsContact = new FnsMsEmergencyContact(newContact);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsContact;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsEmergencyContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMsEmergencyContact> EmergencyContactDelete(long id, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactDelete";
			var result = new FnsResult<IFnsMsEmergencyContact>
			{
				Code = (int)ErrorCodes.GeneralMessage,
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Cast EMC's to database object.
				var deletedContact = SosCrmDataContext.Instance.MS_EmergencyContacts.LoadByPrimaryKey(id);

				// ** Build result
				if (deletedContact == null)
				{
					result.Code = (int)ErrorCodes.SqlExceptions;
					result.Message = string.Format("The EMC id you passed '{0}' does not exists.", id);
					return result;
				}

				// ** Flag contact as deleted
				deletedContact.IsDeleted = true;
				deletedContact.Save(gpEmployeeId);

				// update monitoring station
				bool shellAccount;
				var msChoice = Main.GetMsChoice(deletedContact.Account.IndustryAccount.ReceiverLine, out shellAccount);

				var service = new Main(msChoice);
				var updateContactsResult = service.UpdateContacts(deletedContact.AccountId);
				// copy response states
				result.Message = updateContactsResult.Message;
				result.Code = updateContactsResult.Code;
				// check result
				if (updateContactsResult.Code == 0)
				{
					// ** Save result information
					result.Value = new FnsMsEmergencyContact(deletedContact);
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsEmergencyContact>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContactPhoneType>> EmergencyContactPhoneTypesGet(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactPhoneTypesGet";
			var result = new FnsResult<List<IFnsMsEmergencyContactPhoneType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Create new MsAccount. */
				MS_EmergencyContactPhoneTypeCollection phoneTypeCol = SosCrmDataContext.Instance.MS_EmergencyContactPhoneTypes.LoadAllSafe();

				// ** Build result
				var resultList = phoneTypeCol.Select(phoneType => new FnsMsEmergencyContactPhoneType(phoneType)).Cast<IFnsMsEmergencyContactPhoneType>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContactPhoneType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContactPhoneType>> EmergencyContactPhoneTypesGet(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactPhoneTypesGet";
			var result = new FnsResult<List<IFnsMsEmergencyContactPhoneType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var db = SosCrmDataContext.Instance;
				var account = db.MS_Accounts.LoadByPrimaryKey(accountId);
				if (account == null || account.IndustryAccount == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = string.Format("Account {0} not found or missing industry account", accountId);
				}
				else
				{
					var relCol =
						db.MS_EmergencyContactRelationships.ByMonitoringStation(account.IndustryAccount.ReceiverLine.MonitoringStationOSId);

					// ** Create new MsAccount. */
					MS_EmergencyContactPhoneTypeCollection phoneTypeCol =
						SosCrmDataContext.Instance.MS_EmergencyContactPhoneTypes.LoadSafeByMsosId(account.IndustryAccount.ReceiverLine.MonitoringStationOSId);

					// ** Build result
					var resultList =
						phoneTypeCol.Select(phoneType => new FnsMsEmergencyContactPhoneType(phoneType))
							.Cast<IFnsMsEmergencyContactPhoneType>()
							.ToList();

					// ** Save result information
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContactPhoneType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContactRelationship>> EmergencyContactRelationShipsGet(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactRelationShipsGet";
			var result = new FnsResult<List<IFnsMsEmergencyContactRelationship>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var db = SosCrmDataContext.Instance;
				var account = db.MS_Accounts.LoadByPrimaryKey(accountId);
				if (account == null || account.IndustryAccount == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = string.Format("Account {0} not found or missing industry account", accountId);
				}
				else
				{
					var relCol = db.MS_EmergencyContactRelationships.ByMonitoringStation(account.IndustryAccount.ReceiverLine.MonitoringStationOSId);

					// ** Build result
					var resultList = relCol.Select(relationshipItem => new FnsMsEmergencyContactRelationship(relationshipItem)).Cast<IFnsMsEmergencyContactRelationship>().ToList();

					// ** Save result information
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContactRelationship>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContactAuthority>> EmergencyContactAuthoritiesGet(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactAuthoritiesGet";
			var result = new FnsResult<List<IFnsMsEmergencyContactAuthority>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var db = SosCrmDataContext.Instance;
				var account = db.MS_Accounts.LoadByPrimaryKey(accountId);
				if (account == null || account.IndustryAccount == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = string.Format("Account {0} not found or missing industry account", accountId);
				}
				else
				{
					var relCol = db.MS_EmergencyContactAuthorities.ByMonitoringStation(account.IndustryAccount.ReceiverLine.MonitoringStationOSId);

					// ** Build result
					var resultList = relCol.Select(relationshipItem => new FnsMsEmergencyContactAuthority(relationshipItem)).Cast<IFnsMsEmergencyContactAuthority>().ToList();

					// ** Save result information
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContactAuthority>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContactType>> EmergencyContactTypesGet(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactTypesGet";
			var result = new FnsResult<List<IFnsMsEmergencyContactType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				var db = SosCrmDataContext.Instance;
				var account = db.MS_Accounts.LoadByPrimaryKey(accountId);
				if (account == null || account.IndustryAccount == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = string.Format("Account {0} not found or missing industry account", accountId);
				}
				else
				{
					var relCol = db.MS_EmergencyContactTypes.ByMonitoringStation(account.IndustryAccount.ReceiverLine.MonitoringStationOSId);

					// ** Build result
					var resultList = relCol.Select(relationshipItem => new FnsMsEmergencyContactType(relationshipItem)).Cast<IFnsMsEmergencyContactType>().ToList();

					// ** Save result information
					result.Code = (int)ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContactType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEmergencyContact>> EmergencyContactGetByAccountId(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EmergencyContactGetByAccountId";
			var result = new FnsResult<List<IFnsMsEmergencyContact>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Create new MsAccount. */
				MS_EmergencyContactCollection emcList = SosCrmDataContext.Instance.MS_EmergencyContacts.ByAccountId(accountId);

				// ** Build result
				var resultList = emcList.Select(emcItem => new FnsMsEmergencyContact(emcItem)).Cast<IFnsMsEmergencyContact>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEmergencyContact>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Emergency Contacts

		#region System Details


		public IFnsResult<IFnsMsAccount> SystemDetailsGet(long accountId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SystemDetailsGet";
			var result = new FnsResult<IFnsMsAccount>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Retrieve System Detail Info
				var account = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);

				// ** Check that there was an account found.
				if (account == null) throw new Exception(string.Format("The AccountId '{0}' passed did not find an account.", accountId));

				// ** Build result
				var resultValue = new FnsMsAccount(account);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsAccount>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMsAccount> SystemDetailsSave(IFnsMsAccount fnsMsAccount, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SystemDetailsSave";
			var result = new FnsResult<IFnsMsAccount>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Retrieve System Detail Info
				var account = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(fnsMsAccount.AccountID);

				// ** Check that there was an account found.
				if (account == null) throw new Exception(string.Format("The AccountId '{0}' passed did not find an account.", fnsMsAccount.AccountID));

				/** Bind new data. */
				if (!string.IsNullOrEmpty(fnsMsAccount.SystemTypeId))
					account.SystemTypeId = fnsMsAccount.SystemTypeId;
				if (!string.IsNullOrEmpty(fnsMsAccount.CellularTypeId))
					account.CellularTypeId = fnsMsAccount.CellularTypeId;
				if (!string.IsNullOrEmpty(fnsMsAccount.PanelTypeId))
					account.PanelTypeId = fnsMsAccount.PanelTypeId;
				if (!string.IsNullOrEmpty(fnsMsAccount.AccountPassword))
					account.AccountPassword = fnsMsAccount.AccountPassword;
				if (fnsMsAccount.DslSeizureId != 0)
					account.DslSeizureId = fnsMsAccount.DslSeizureId;
				account.Save(gpEmployeeID);

				// ** Build result
				var resultValue = new FnsMsAccount(account);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsAccount>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion System Details

		#region Account Details

		public IFnsResult<object> AccountDetails(long accountId)
		{
			var result = new FnsResult<object>
			{
				Code = (int)ErrorCodes.Success,
				Message = "",
			};

			var account = SosCrmDataContext.Instance.MS_AccountMonitorInformationsViews.LoadByPrimaryKey(accountId);
			if (account != null)
			{
				result.Value = new FnsMsAccountDetails(account);
			}
			return result;
		}

		#endregion //Account Details


		#region Account Validate

		public IFnsResult<object> AccountValidate(long accountId)
		{
			var result = new FnsResult<object>
			{
				Code = (int)ErrorCodes.Success,
				Message = "",
			};

			var account = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
			if (account == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "Account ID is invalid.";
				return result;
			}
			result.Value = new FnsMsAccount(account);
			return result;
		}

		#endregion //Account Validate

		#endregion Database CRUD

		#region MsAccounts

		//public IFnsResult<IFnsMsAccountLeadInfo> MsAccountCreate(long leadId, string gpEmployeeId)
		//{
		//	#region INITIALIZATION

		//	// ** Initialize 
		//	const string METHOD_NAME = "MsAccountCreate";
		//	var result = new FnsResult<IFnsMsAccountLeadInfo>
		//	{
		//		Code = (int)ErrorCodes.GeneralMessage,
		//		Message = string.Format("Initializing {0}", METHOD_NAME)
		//	};

		//	#endregion INITIALIZATION

		//	#region TRY
		//	try
		//	{
		//		// ** Create new MsAccount. */
		//		var accountLeadInfo = SosCrmDataContext.Instance.MS_AccountAndLeadInfoViews.Create(leadId,
		//			gpEmployeeId);

		//		// ** Build result
		//		var resultValue = new FnsMsAccountLeadInfo(accountLeadInfo);

		//		// ** Save result information
		//		result.Code = (int)ErrorCodes.Success;
		//		result.Message = "Success";
		//		result.Value = resultValue;
		//	}
		//	#endregion TRY

		//	#region CATCH
		//	catch (Exception ex)
		//	{
		//		result = new FnsResult<IFnsMsAccountLeadInfo>
		//		{
		//			Code = (int)ErrorCodes.UnexpectedException,
		//			Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
		//		};
		//	}
		//	#endregion CATCH

		//	// ** Return result
		//	return result;
		//}

		public IFnsResult<IFnsMsAccountLeadInfo> CreateMasterFileAccount(long cmfid, string gpEmployeeId)
		{
			var result = new FnsResult<IFnsMsAccountLeadInfo>();

			var masterLeads = SosCrmDataContext.Instance.QL_CustomerMasterLeads.ByCmfID(cmfid).ToList();
			var primaryMasterLeads = masterLeads.Where(IsPrimaryMasterLead).ToList();
			if (primaryMasterLeads.Count != 1)
			{
				result.Message = (primaryMasterLeads.Count == 0) ? "MasterFile has no primary lead" : "MasterFile has more than one primary lead";
				result.Code = -1;
				return result;
			}
			var monitoredMasterLeads = masterLeads.Where(IsMonitoredMasterLead).ToList();
			if (monitoredMasterLeads.Count > 1)
			{
				result.Message = "MasterFile has more than one monitored lead";
				result.Code = -1;
				return result;
			}

			long accountID = 0;
			DatabaseHelper.UseTransaction(Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				// if there are no MONI leads, use PRI lead
				QL_CustomerMasterLead monitoredMasterLead;
				if (monitoredMasterLeads.Count > 0)
				{
					monitoredMasterLead = monitoredMasterLeads[0];
				}
				else
				{
					// create moni master lead from primary master lead
					monitoredMasterLead = new QL_CustomerMasterLead()
					{
						CustomerMasterLeadID = Guid.NewGuid(),
						CustomerMasterFileId = cmfid,
						CustomerTypeId = "MONI",
						LeadId = primaryMasterLeads[0].LeadId,
					};
					monitoredMasterLead.Save();//gpEmployeeId);
					// add to list ...as if it was always there...
					masterLeads.Add(monitoredMasterLead);
				}

				// monitored lead - create account
				var mcAccount = CreateAlarmAccount(monitoredMasterLead, gpEmployeeId);
				accountID = mcAccount.AccountID;
				// all leads - add customer to account
				foreach (var masterLead in masterLeads)
				{
					AddCustomerToAlarmAccount(mcAccount, masterLead, gpEmployeeId);
				}
				// commit transaction
				return true;
			});

			var accountLeadInfo = SosCrmDataContext.Instance.MS_AccountAndLeadInfoViews.ByAccountID(accountID);
			result.Value = new FnsMsAccountLeadInfo(accountLeadInfo);

			return result;
		}
		private bool IsPrimaryMasterLead(QL_CustomerMasterLead masterLead)
		{
			var priIdList = new[] { "PRI", "LEAD" };
			return StringUtility.IsInList(priIdList, masterLead.CustomerTypeId, false);
		}
		private static bool IsMonitoredMasterLead(QL_CustomerMasterLead masterLead)
		{
			return string.Equals("MONI", masterLead.CustomerTypeId, StringComparison.OrdinalIgnoreCase);
		}
		private MC_Account CreateAlarmAccount(QL_CustomerMasterLead masterLead, string gpEmployeeId)
		{
			/** Create Customer */
			long mcAddressId;
			var monitoredCustomer = GetOrCreateAeCustomer(true, masterLead, gpEmployeeId, out mcAddressId);

			var lead = masterLead.Lead;
			/** Create the MC_Account */
			var mcAccount = new MC_Account
			{
				CustomerMasterFileId = lead.CustomerMasterFileId,
				AccountTypeId = "ALRM",
				DealerId = lead.DealerId,
				ShipContactId = monitoredCustomer.CustomerID,
				ShipContactSameAsCustomer = true,
				ShipAddressSameAsCustomer = true,
			};
			mcAccount.Save(gpEmployeeId);

			/** Create the MsAccount record. */
			var msAccount = new MS_Account
			{
				AccountID = mcAccount.AccountID,
				PremiseAddressId = monitoredCustomer.AddressId,
				SiteTypeId = MS_AccountSiteType.MetaData.BurgFireMed_ResID
			};
			msAccount.Save(gpEmployeeId);

			//
			new SAE_BillingInfoSummary
			{
				CustomerMasterFileId = lead.CustomerMasterFileId,
				AccountId = mcAccount.AccountID,
				AmountDue = 0,
			}.Save(gpEmployeeId);

			return mcAccount;
		}
		private AE_Customer AddCustomerToAlarmAccount(MC_Account mcAccount, QL_CustomerMasterLead masterLead, string gpEmployeeId)
		{
			// get or create customer
			long mcAddressId;
			AE_Customer customer = GetOrCreateAeCustomer(false, masterLead, gpEmployeeId, out mcAddressId);

			var lead = masterLead.Lead;
			if (ValidAccountCustomerTypeId(masterLead.CustomerTypeId))
			{
				//// only create if the customer type is valid for an MS_AccountCustomer
				//var accountCustomer = new MS_AccountCustomer
				//{
				//	AccountCustomerTypeId = masterLead.CustomerTypeId,
				//	LeadId = lead.LeadID,
				//	AccountId = mcAccount.AccountID,
				//	CustomerId = customer.CustomerID,
				//};
				//accountCustomer.Save(gpEmployeeId);
			}
			//
			var customerAccount = new AE_CustomerAccount
			{
				LeadId = lead.LeadID,
				AccountId = mcAccount.AccountID,
				CustomerId = customer.CustomerID,
				CustomerTypeId = masterLead.CustomerTypeId,
				AddressId = mcAddressId,
				CreatedBy = gpEmployeeId,
			};
			customerAccount.Save(gpEmployeeId);

			return customer;
		}
		private bool ValidAccountCustomerTypeId(string typeID)
		{
			var addressTypes = new[] { "PRI", "SEC", "MONI", };
			return StringUtility.IsInList(addressTypes, typeID, false);
		}

		private AE_Customer GetOrCreateAeCustomer(bool isMoniCust, QL_CustomerMasterLead masterLead, string gpEmployeeId, out long mcAddressId)
		{
			var lead = masterLead.Lead;

			var qlAddress = lead.Address;
			// map ql_address to mc_address 1 to 1
			// check if an address already exists for the lead qlAddress
			var mcAddress = SosCrmDataContext.Instance.MC_Addresses.ByQlAddressId(qlAddress.AddressID);
			if (mcAddress == null)
			{
				/** Save Data */
				mcAddress = new MC_Address
				{
					QlAddressId = lead.AddressId,
					DealerId = lead.DealerId,
					ValidationVendorId = qlAddress.ValidationVendorId,
					AddressValidationStateId = qlAddress.AddressValidationStateId,
					StateId = qlAddress.StateId,
					CountryId = qlAddress.CountryId,
					TimeZoneId = qlAddress.TimeZoneId,
					AddressTypeId = qlAddress.AddressTypeId,
					StreetAddress = qlAddress.StreetAddress,
					StreetAddress2 = qlAddress.StreetAddress2,
					StreetNumber = qlAddress.StreetNumber,
					StreetName = qlAddress.StreetName,
					StreetType = qlAddress.StreetType,
					PreDirectional = qlAddress.PreDirectional,
					PostDirectional = qlAddress.PostDirectional,
					Extension = qlAddress.Extension,
					ExtensionNumber = qlAddress.ExtensionNumber,
					County = qlAddress.County,
					CountyCode = qlAddress.CountyCode,
					Urbanization = qlAddress.Urbanization,
					UrbanizationCode = qlAddress.UrbanizationCode,
					City = qlAddress.City,
					PostalCode = qlAddress.PostalCode,
					PlusFour = qlAddress.PlusFour,
					Phone = qlAddress.Phone,
					DeliveryPoint = qlAddress.DeliveryPoint,
					CrossStreet = qlAddress.CrossStreet,
					Latitude = qlAddress.Latitude,
					Longitude = qlAddress.Longitude,
					CongressionalDistric = qlAddress.CongressionalDistric,
					DPV = qlAddress.DPV,
					DPVResponse = qlAddress.DPVResponse,
					DPVFootNote = qlAddress.DPVFootnote,
					CarrierRoute = qlAddress.CarrierRoute,
					IsActive = true,
					IsDeleted = false,
				};
				mcAddress.Save(gpEmployeeId);
			}
			mcAddressId = mcAddress.AddressID;

			/** Create Customer*/
			// map lead to customer 1 to 1
			// check if a customer already exists for the lead
			var customer = SosCrmDataContext.Instance.AE_Customers.ByLeadId(masterLead.LeadId);
			if (customer != null)
			{
				if (isMoniCust)
					throw new Exception("Unexpected state: A customer already exists, but this should be the very first customer...");
			}
			else
			{
				customer = new AE_Customer
				{
					LeadId = lead.LeadID,
					AddressId = mcAddress.AddressID,
					CustomerTypeId = lead.CustomerTypeId,
					CustomerMasterFileId = lead.CustomerMasterFileId,
					DealerId = lead.DealerId,
					LocalizationId = lead.LocalizationId,
					Prefix = lead.Salutation,
					FirstName = lead.FirstName,
					MiddleName = lead.MiddleName,
					LastName = lead.LastName,
					Postfix = lead.Suffix,
					Gender = lead.Gender,
					SSN = lead.SSN,
					DOB = lead.DOB,
					Email = lead.Email,
					PhoneHome = lead.PhoneHome ?? qlAddress.Phone, // This is needed because this PhoneHome maps to the panel phone.
					PhoneWork = lead.PhoneWork,
					PhoneMobile = lead.PhoneMobile,
					IsActive = true,
					IsDeleted = false,
				};
				customer.Save(gpEmployeeId);
			}

			//var customerAddressTypeID = isMoniCust ? AE_CustomerAddressType.MetaData.Premise_AddressID : masterLead.CustomerTypeId;
			//if (ValidCustomerAddressTypeID(customerAddressTypeID))
			//{
			//	// only create if the address type is valid for an AE_CustomerAddress
			//	new AE_CustomerAddress
			//	{
			//		CustomerId = customer.CustomerID,
			//		AddressId = mcAddress.AddressID,
			//		CustomerAddressTypeId = customerAddressTypeID,
			//	}.Save(gpEmployeeId);
			//}

			return customer;
		}
		private bool ValidCustomerAddressTypeID(string typeID)
		{
			var addressTypes = new[] {
				AE_CustomerAddressType.MetaData.Billing_AddressID,
				AE_CustomerAddressType.MetaData.OtherID,
				AE_CustomerAddressType.MetaData.Premise_AddressID,
				AE_CustomerAddressType.MetaData.Primary_Customer_AddressID,
				AE_CustomerAddressType.MetaData.Secondary_Customer_AddressID,
				AE_CustomerAddressType.MetaData.Shipping_AddressID,
			};
			return StringUtility.IsInList(addressTypes, typeID, false);
		}

		#endregion MsAccounts

		#region Premise Address

		public IFnsResult<IFnsMcAddressView> GetPremiseAddress(long accountId, string gpEmployeeId)
		{
			#region INITIALIZE

			const string METHOD_NAME = "GetPremiseAddress";
			var result = new FnsResult<IFnsMcAddressView>();
			#endregion INITIALIZE

			#region TRY
			try
			{
				var mcAddress = SosCrmDataContext.Instance.MC_AddressesViews.GetByAccountId(accountId);
				result.Value = new FnsMcAddressView(mcAddress);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMcAddressView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Premise Address

		#region Industry Accounts

		public IFnsResult<IFnsMsIndustryAccount> MsIndustryNumberGenerate(long accountId, bool isPrimary, string gpEmployeeId)
		{
			var result = new FnsResult<IFnsMsIndustryAccount>();

			// ** Create new MsAccount. */
			// TODO:  These values have to be set the AccountType and MonitoringStationOSID.
			var industryAccount = SosCrmDataContext.Instance.MS_IndustryAccountNumbersViews.Generate(accountId, "CELLDIGTWOTWAY", MS_MonitoringStationOss.MetaDataStatic.AG_ALARMSYS,
				gpEmployeeId, isPrimary);

			// set result value
			result.Value = new FnsMsIndustryAccount(industryAccount);

			// ** Check to see if this is an AG_ALARMSYS
			var receiverLine = SosCrmDataContext.Instance.MS_ReceiverLines.LoadByPrimaryKey(industryAccount.ReceiverLineId);
			if (receiverLine.MonitoringStationOSId.Equals(MS_MonitoringStationOss.MetaDataStatic.AG_ALARMSYS))
			{
				// ** Create the shell.
				var shellResult = ShellAccount(accountId, gpEmployeeId);
				if (shellResult.Code != 0)
				{
					result.Code = shellResult.Code;
					result.Message = shellResult.Message;
				}
			}

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsIndustryAccountNumbersWithReceiverLineInfoView>> MsIndustryNumberWithReceiverLineGet(long accountId, string gpEmployeeId)
		{
			// ** Initialize
			var result = new FnsResult<List<IFnsMsIndustryAccountNumbersWithReceiverLineInfoView>>();

			// ** Fetch data
			var msList = SosCrmDataContext.Instance.MS_IndustryAccountNumbersWithReceiverLineInfoViews.Get(accountId,
				gpEmployeeId);
			result.Value =
				msList.Select(
					item =>
						(IFnsMsIndustryAccountNumbersWithReceiverLineInfoView)
							new FnsMsIndustryAccountNumbersWithReceiverLineInfoView(item)).ToList();

			// ** Return result
			return result;
		}

		public IFnsResult<bool> MsIndustryNumberSetAsPrimary(long industryAccountId, string gpEmployeeId)
		{
			// ** Initialize
			var result = new FnsResult<bool>();

			// ** Execute
			try
			{
				var indNumber = SosCrmDataContext.Instance.MS_IndustryAccounts.LoadByPrimaryKey(industryAccountId);
				indNumber.Account.IndustryAccountId = industryAccountId;
				indNumber.Account.Save(gpEmployeeId);

				result.Value = true;
			}
			catch (Exception ex)
			{
				result.Code = BaseErrorCodes.ErrorCodes.ExceptionThrown.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.ExceptionThrown.Message(), ex.Message, "MsIndustryNumberSetAsPrimary");
				result.Value = false;
			}

			// ** Return result
			return result;

		}
		public IFnsResult<bool> MsIndustryNumberSetAsSecondary(long industryAccountId, string gpEmployeeId)
		{
			// ** Initialize
			var result = new FnsResult<bool>();

			// ** Execute
			try
			{
				var indNumber = SosCrmDataContext.Instance.MS_IndustryAccounts.LoadByPrimaryKey(industryAccountId);
				indNumber.Account.IndustryAccount2Id = industryAccountId;
				indNumber.Account.Save(gpEmployeeId);

				result.Value = true;
			}
			catch (Exception ex)
			{
				result.Code = BaseErrorCodes.ErrorCodes.ExceptionThrown.Code();
				result.Message = string.Format(BaseErrorCodes.ErrorCodes.ExceptionThrown.Message(), ex.Message, "MsIndustryNumberSetAsPrimary");
				result.Value = false;
			}

			// ** Return result
			return result;

		}

		public IFnsResult<List<IFnsMsIndustryAccount>> MsIndustryNumbersGet(long accountId, string gpEmployeeId)
		{
			var result = new FnsResult<List<IFnsMsIndustryAccount>>();
			// fetch data
			var aeList = SosCrmDataContext.Instance.MS_IndustryAccountNumbersViews.GetByAccountId(accountId, gpEmployeeId);
			// set result
			result.Value = aeList.Select(item => (IFnsMsIndustryAccount)new FnsMsIndustryAccount(item)).ToList();
			// return result
			return result;
		}

		#endregion Industry Accounts

		#region MsAccount Meta Data

		public IFnsResult<List<IFnsMsAccountDslSeizureType>> DslSeizureTypesGet(int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "DslSeizureTypesGet";
			var result = new FnsResult<List<IFnsMsAccountDslSeizureType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountDslSeizureTypeCollection aeList = SosCrmDataContext.Instance.MS_AccountDslSeizureTypes.LoadAll();
				var resultList = aeList.Select(item => new FnsMsAccountDslSeizureType(item)).Cast<IFnsMsAccountDslSeizureType>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountDslSeizureType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountEventZoneEventTypes>> ZoneEventTypesGet(string msoid, int equipmentTypeId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ZoneEventTypesGet";
			var result = new FnsResult<List<IFnsMsAccountEventZoneEventTypes>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountEventViewCollection aeList = SosCrmDataContext.Instance.MS_AccountEventViews.Get(msoid, equipmentTypeId, gpEmployeeId);
				var resultList = aeList.Select(item => new FnsMsAccountEventZoneEventTypes(item)).Cast<IFnsMsAccountEventZoneEventTypes>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountEventZoneEventTypes>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEquipmentLocation>> EquipmentLocationsGet(string msoid, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EquipmentLocationsGet";
			var result = new FnsResult<List<IFnsMsEquipmentLocation>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_EquipmentLocationsViewCollection aeList = SosCrmDataContext.Instance.MS_EquipmentLocationsViews.GetByMOSID(msoid, gpEmployeeId);
				var resultList = aeList.Select(item => new FnsMsEquipmentLocation(item)).Cast<IFnsMsEquipmentLocation>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEquipmentLocation>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountZoneType>> ZoneTypesGet(string msoid, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "ZoneTypesGet";
			var result = new FnsResult<List<IFnsMsAccountZoneType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountZoneTypeCollection aeList = SosCrmDataContext.Instance.MS_AccountZoneTypes.LoadAll();
				var resultList = aeList.Select(item => new FnsMsAccountZoneType(item)).Cast<IFnsMsAccountZoneType>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountZoneType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEquipmentsView>> MsEquipmentsGet(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "MsEquipmentsGet";
			var result = new FnsResult<List<IFnsMsEquipmentsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_EquipmentsViewCollection aeList = SosCrmDataContext.Instance.MS_EquipmentsViews.GetEquipmentList();
				var resultList = aeList.Select(item => new FnsMsEquipmentsView(item)).Cast<IFnsMsEquipmentsView>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEquipmentsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsEquipmentsView>> MsEquipmentExistingsGet(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "MsEquipmentExistingsGet";
			var result = new FnsResult<List<IFnsMsEquipmentsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_EquipmentsViewCollection aeList = SosCrmDataContext.Instance.MS_EquipmentsViews.GetEquipmentExistingList();
				var resultList = aeList.Select(item => new FnsMsEquipmentsView(item)).Cast<IFnsMsEquipmentsView>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEquipmentsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion MsAccount Meta Data

		#region Account Equipment

		public IFnsResult<IFnsMsAccountEquipmentsView> EquipmentUpdate(IFnsMsAccountEquipmentsView acctEquipment, string gpEmployeeID)
		{
			var result = new FnsResult<IFnsMsAccountEquipmentsView> { Message = "" };

			var db = SosCrmDataContext.Instance;
			IE_ProductBarcode productBarcode = null;
			IE_ProductBarcodeTracking barcodeTracking = null;

			var isNew = (acctEquipment.AccountEquipmentID == 0);
			var item = isNew ? new MS_AccountEquipment() : db.MS_AccountEquipments.LoadByPrimaryKey(acctEquipment.AccountEquipmentID);
			if (item.BarcodeId != acctEquipment.BarcodeId)
			{
				if (!string.IsNullOrWhiteSpace(item.BarcodeId))
				{
					result.Code = -1;
					result.Message = string.Format("Barcode cannot be changed. Current:{0} New:{1}", item.BarcodeId, acctEquipment.BarcodeId);
					return result;
				}

				item.BarcodeId = acctEquipment.BarcodeId;
				if (!string.IsNullOrWhiteSpace(acctEquipment.BarcodeId))
				{
					// check if barcode is valid
					productBarcode = db.IE_ProductBarcodes.LoadByPrimaryKey(item.BarcodeId);
					if (productBarcode == null)
					{
						result.Code = (int)ErrorCodes.SqlItemNotFound;
						result.Message = string.Format("Barcode {0} not found.", item.BarcodeId);
						return result;
					}
					else if (productBarcode.LastProductBarcodeTrackingId != null
						&& string.Compare(productBarcode.LastProductBarcodeTracking.LocationTypeID, "Sold", true) == 0)
					{
						result.Code = -1;
						result.Message = string.Format("Barcode {0} is already assigned to Account# {1}.", item.BarcodeId, productBarcode.LastProductBarcodeTracking.LocationID);
						return result;
					}
					// create barcode tracking
					barcodeTracking = new IE_ProductBarcodeTracking
					{
						ProductBarcodeTrackingTypeId = "CUST",
						ProductBarcodeId = acctEquipment.BarcodeId,
						LocationTypeID = "Sold",
						LocationID = acctEquipment.AccountId.ToString(CultureInfo.InvariantCulture),
					};
				}
			}
			MS_Equipment equipment = SosCrmDataContext.Instance.MS_Equipments.LoadByPrimaryKey(acctEquipment.EquipmentId);
			if (isNew)
			{
				item.IsActive = true;

			}
			item.AccountId = acctEquipment.AccountId;
			item.EquipmentId = acctEquipment.EquipmentId;
			item.EquipmentLocationId = acctEquipment.EquipmentLocationId;
			item.GPEmployeeId = acctEquipment.GPEmployeeId;
			//item.OfficeReconciliationItemId = acctEquipment.OfficeReconciliationItemId;
			item.AccountEquipmentUpgradeTypeId = acctEquipment.AccountEquipmentUpgradeTypeId;
			//item.CustomerLocation = acctEquipment.CustomerLocation;
			item.Points = acctEquipment.Points;
			item.ActualPoints = acctEquipment.ActualPoints;
			item.Price = acctEquipment.Price;
			item.IsExisting = acctEquipment.IsExisting;
			item.IsServiceUpgrade = acctEquipment.IsServiceUpgrade;
			item.IsExistingWiring = acctEquipment.IsExistingWiring;
			item.IsMainPanel = acctEquipment.IsMainPanel;

			isNew = (acctEquipment.AccountZoneAssignmentID == 0);
			var zone = isNew ? new MS_AccountZoneAssignment() : db.MS_AccountZoneAssignments.LoadByPrimaryKey(acctEquipment.AccountZoneAssignmentID);
			if (isNew)
			{
				zone.IsActive = true;
			}
			zone.AccountZoneTypeId = acctEquipment.AccountZoneTypeId ?? equipment.AccountZoneTypeId;
			zone.AccountEventId = acctEquipment.AccountEventId;
			zone.Zone = acctEquipment.Zone;
			zone.Comments = acctEquipment.Comments;
			zone.IsExisting = acctEquipment.IsExisting;

			DatabaseHelper.UseTransaction(Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
			{
				// save barcode tracking
				if (barcodeTracking != null)
				{
					barcodeTracking.Save(gpEmployeeID);
					// update last barcode tracking
					productBarcode.LastProductBarcodeTrackingId = barcodeTracking.ProductBarcodeTrackingID;
					productBarcode.Save(gpEmployeeID);
				}
				// save equipment item
				item.Save(gpEmployeeID);
				// set foreign key and save zone
				zone.AccountEquipmentId = item.AccountEquipmentID;
				zone.Save(gpEmployeeID);
				// commit transaction
				return true;
			});

			// load and return items just added
			//equipmentAndZone.AccountEquipment = new 
			result.Value = new FnsMsAccountEquipmentsView(db.MS_AccountEquipmentsViews.ByAccountZoneAssignment(zone.AccountZoneAssignmentID));
			return result;
		}

		public IFnsResult<bool> EquipmentDelete(long accountEquipmentID, string gpEmployeeID)
		{
			#region INITIALIZATION
			const string METHOD_NAME = "EquipmentDelete";
			var result = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			var tranOptions = new TransactionOptions
			{
				IsolationLevel = IsolationLevel.Serializable,
				Timeout = new TimeSpan(0, 0, 30)
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				using (var ts = new TransactionScope(TransactionScopeOption.Required, tranOptions))
				{
					using (new SharedDbConnectionScope(DataService.Providers[Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME]))
					{
						var accountEquipmentDelete = SosCrmDataContext.Instance.MS_AccountEquipments.Delete(accountEquipmentID, gpEmployeeID);
						var zoneAssignmentsDeleted = SosCrmDataContext.Instance.MS_AccountZoneAssignments.DeleteByAccountEquipmentId(accountEquipmentID, gpEmployeeID);
						// complete the transaction
						ts.Complete();
						// return results
						result.Value = (accountEquipmentDelete || zoneAssignmentsDeleted);
						if (!result.Value)
						{
							result.Code = (int)ErrorCodes.GeneralError;
							result.Message = string.Format("Unable to delete equipment.  The transaction did not finish completely.");
						}
						else
						{
							result.Code = (int)ErrorCodes.Success;
							result.Message = "Success";
						}
					}
				}
			}
			#endregion TRY
			#region CATCH
			catch (SqlException sqlEx)
			{
				var sqlUtil = MsSqlExceptionUtil.Parse(sqlEx.Message);
				result.Code = sqlUtil.MessageID;
				result.Message = string.Format("SQL Exception thrown at {0}: {1}", METHOD_NAME, sqlUtil.ErrorMessage);
			}
			catch (Exception ex)
			{
				result = new FnsResult<bool>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountEquipmentsView>> EquipmentByAccountId(long accountId)
		{
			var db = SosCrmDataContext.Instance;
			return new FnsResult<List<IFnsMsAccountEquipmentsView>>
			{
				Code = 0,
				Message = "",
				Value = db.MS_AccountEquipmentsViews.ByAccountId(accountId)
					.Select(a => (IFnsMsAccountEquipmentsView)new FnsMsAccountEquipmentsView(a)).ToList(),
			};
		}
		public IFnsResult<object> EquipmentAccountZoneTypes(string equipmentId)
		{
			var db = SosCrmDataContext.Instance;
			return new FnsResult<object>
			{
				Message = "",
				Value = db.MS_EquipmentAccountZoneTypesViews.Get(equipmentId).ToList(),
			};
		}
		public IFnsResult<object> EquipmentAccountZoneTypeEvents(string equipmentId, int equipmentAccountZoneTypeId, string monitoringStationOSId)
		{
			var db = SosCrmDataContext.Instance;
			return new FnsResult<object>
			{
				Message = "",
				Value = db.MS_EquipmentAccountZoneTypeEventsViews.Get(equipmentId, equipmentAccountZoneTypeId, monitoringStationOSId).ToList(),
			};
		}
		public IFnsResult<object> EquipmentByEquipmentID(string equipmentID)
		{
			var item = SosCrmDataContext.Instance.MS_EquipmentsViews.LoadByPrimaryKey(equipmentID);
			var found = (item != null);
			return new FnsResult<object>
			{
				Code = found ? 0 : (int)ErrorCodes.SqlItemNotFound,
				Message = found ? "" : "No equipment matching id " + equipmentID,
				Value = item,
			};
		}
		public IFnsResult<object> EquipmentByPartNumber(string partNumber)
		{
			var item = SosCrmDataContext.Instance.MS_EquipmentsViews.ByPartNumber(partNumber);
			var found = (item != null);
			return new FnsResult<object>
			{
				Code = found ? 0 : (int)ErrorCodes.SqlItemNotFound,
				Message = found ? "" : "No equipment matching part# " + partNumber,
				Value = item,
			};
		}
		public IFnsResult<object> EquipmentByBarcode(string barcode)
		{
			var item = SosCrmDataContext.Instance.MS_EquipmentsViews.ByBarcode(barcode);
			var found = (item != null);
			return new FnsResult<object>
			{
				Code = found ? 0 : (int)ErrorCodes.SqlItemNotFound,
				Message = found ? "" : "No equipment for barcode " + barcode,
				Value = item,
			};
		}

		public IFnsResult<IFnsMsAccountEquipmentsView> EquipmentExistingAdd(IFnsMsAccountEquipmentsView equipment, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "EquipmentExistingAdd";
			var result = new FnsResult<IFnsMsAccountEquipmentsView>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// TODO:  Fix this hack.
				int? accountZoneTypeId = int.Parse(equipment.AccountZoneTypeId);
				var equipZone = SosCrmDataContext.Instance.MS_AccountEquipmentsViews.ExistingEquipmentAdd(equipment.AccountId
					, equipment.EquipmentId, equipment.EquipmentLocationId, accountZoneTypeId, equipment.Zone, null
					, equipment.IsExisting, equipment.IsExistingWiring, equipment.IsMainPanel, gpEmployeeID);

				// ** Build result
				var resultValue = new FnsMsAccountEquipmentsView(equipZone);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsAccountEquipmentsView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Account Equipment

		#region Submit Account to Central Station
		public IFnsResult<IFnsMsAccountSubmit> SubmitOnline(long accountId, string gpEmployeeId)
		{
			return PutAccountOnline(accountId, gpEmployeeId, null);
		}
		private IFnsResult<IFnsMsAccountSubmit> ShellAccount(long accountId, string gpEmployeeId)
		{
			var result = PutAccountOnline(accountId, gpEmployeeId, "SHELL000");
			if (result.Code != 0)
			{
				result.Message = "Error shelling account: " + result.Message;
			}
			return result;
		}
		private FnsResult<IFnsMsAccountSubmit> PutAccountOnline(long accountId, string gpEmployeeId, string shellTechId)
		{
			var result = new FnsResult<IFnsMsAccountSubmit>();

			bool shellAccount = shellTechId != null;

			// ** Get the assigned tech.
			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId);
			var msAcctSlI = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(accountId);
			var idAccount = msAccount.IndustryAccount;

			var msAccountSubmit = new MS_AccountSubmit
			{
				AccountId = accountId,
				AccountSubmitTypeId = (short)MS_AccountSubmitType.AccountSubmitTypeEnum.Onboard_System,
				GPTechId = msAcctSlI.TechId ?? shellTechId,
				DateSubmitted = DateTime.UtcNow,
				WasSuccessfull = false,
				MonitoringStationOSId = idAccount.ReceiverLine.MonitoringStationOSId, // let the hardcoding continue...
			};
			msAccountSubmit.Save(gpEmployeeId);

			// ** Onboard account
			var msChoice = Main.GetMsChoice(idAccount.ReceiverLine, out shellAccount);

			var msXmlService = new Main(msChoice);
			FosResult<MS_AccountSubmit> fosResult = shellAccount ? msXmlService.AccountShell(msAccountSubmit, gpEmployeeId) : msXmlService.AccountCreate(msAccountSubmit, gpEmployeeId);

			// ** Build result
			result.Code = fosResult.Code;
			result.Message = fosResult.Message;
			result.Value = new FnsMsAccountSubmit(fosResult.Value);

			// return result
			return result;
		}
		#endregion Submit Account to Central Station

		#region GetSignalHistory
		public IFnsResult<List<IFnsSignalHistoryItemModel>> GetSignalHistory(long accountId, DateTime startDate, DateTime endDate, string gpEmployeeId)
		{
			var context = SosCrmDataContext.Instance;
			var msAccount = context.MS_Accounts.LoadByPrimaryKey(accountId);
			if (msAccount.IndustryAccount == null)
			{
				return new FnsResult<List<IFnsSignalHistoryItemModel>>
				{
					Code = -1,
					Message = "No Industry Account"
				};
			}

			var service = new Main(GetStationByIndustryAccountId(msAccount.IndustryAccount));
			var fosResult = service.GetSignalHistory(startDate, endDate, msAccount.IndustryAccount.Csid);
			var value = new List<IFnsSignalHistoryItemModel>();
			if (fosResult.Code == 0 && fosResult.Value.Count > 0)
			{
				value = fosResult.Value.Select(item => (IFnsSignalHistoryItemModel)new FnsSignalHistoryItemModel(item)).ToList();
			}
			return new FnsResult<List<IFnsSignalHistoryItemModel>>
			{
				Code = fosResult.Code,
				Message = fosResult.Message,
				Value = value,
			};
		}
		#endregion Submit Account to Central Station

		internal IFnsResult<T> CallMainFunc<T>(long accountId, Func<Main, FosResult<T>> fetchFunc)
		{
			var result = new FnsResult<T>();

			var context = SosCrmDataContext.Instance;
			var msAccount = context.MS_Accounts.LoadByPrimaryKey(accountId);
			if (msAccount.IndustryAccount == null)
			{
				result.Code = -1;
				result.Message = "No Industry Account";
				return result;
			}

			var main = new Main(GetStationByIndustryAccountId(msAccount.IndustryAccount));
			var fosResult = fetchFunc(main);
			result.Code = fosResult.Code;
			result.Message = fosResult.Message;
			result.Value = fosResult.Value;
			return result;
		}

		public IFnsResult<object> TwoWayTestData(long accountId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				return main.TwoWayTestData(accountId);
			});
		}
		public IFnsResult<object> InitTwoWayTest(long accountId, string gpEmployeeId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				return main.InitTwoWayTest(accountId, gpEmployeeId);
			});
		}
		public IFnsResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId)
		{
			confirmedBy = (confirmedBy ?? "").Trim();
			if (string.IsNullOrEmpty(confirmedBy))
			{
				return new FnsResult<object>
				{
					Code = -1,
					Message = "ConfirmedBy must have a value",
				};
			}

			return CallMainFunc(accountId, (main) =>
			{
				return main.CompleteTwoWayTest(accountId, confirmedBy, gpEmployeeId);
			});
		}

		public IFnsResult<object> ActiveTests(long accountId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				var result = main.ActiveTests(accountId);
				return new FosResult<object>
				{
					Code = result.Code,
					Message = result.Message,
					Value = result.Value,
				};
			});
		}
		public IFnsResult<bool> ClearActiveTests(long accountId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				return main.ClearActiveTests(accountId);
			});
		}
		public IFnsResult<bool> ClearTest(long accountId, int testNum)
		{
			return CallMainFunc(accountId, (main) =>
			{
				return main.ClearTest(accountId, testNum);
			});
		}
		public IFnsResult<IFnsMsSystemStatusInfo> ServiceStatus(long accountId, string gpEmployeeId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				var wrappedResult = main.ServiceStatus(accountId, gpEmployeeId);
				var newResult = new FosResult<IFnsMsSystemStatusInfo>
				{
					Code = wrappedResult.Code,
					Message = wrappedResult.Message,
					Value = new FnsMsSystemStatusInfo(wrappedResult.Value.InService, wrappedResult.Value.OnTest)
				};
				return newResult;
			});
		}
		public IFnsResult<string> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId)
		{
			return CallMainFunc(accountId, (main) =>
			{
				return main.SetServiceStatus(accountId, oosCat, startDate, comment, gpEmployeeId);
			});
		}

		#region GetTechDetails

		public IFnsResult<IFnsSalesRepInfo> GetTechDetails(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "GetTechDetails";
			var result = new FnsResult<IFnsSalesRepInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				MS_AccountSalesInformation msAccount = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(accountId);

				/** Check to see if the account is set. */
				if (string.IsNullOrEmpty(msAccount.TechId))
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = string.Format("This account has not had a technician set.");
					return result;
				}

				RU_User item = HumanResourceDataContext.Instance.RU_Users.LoadBySalesRepId(msAccount.TechId);

				if (item != null && item.IsLoaded)
				{
					// ** Get the seasons for the rep
					RU_SeasonCollection seasons = HumanResourceDataContext.Instance.RU_Seasons.GetAllSeasonsByUserID(item.UserID);

					/** Init. */
					FnsSalesRepInfo resultValue;
					// ** Get Default TeamLocation
					if (seasons.Count > 0)
					{
						RU_TeamLocation ruTeamLocation =
							HumanResourceDataContext.Instance.RU_TeamLocations.GetBySeasonIdAndGPEmployeeID(seasons[0].SeasonID, msAccount.TechId);

						// ** Create the result object.
						resultValue = new FnsSalesRepInfo(item, seasons, ruTeamLocation);
					}
					else
					{
						// ** Create the result object.
						resultValue = new FnsSalesRepInfo(item, seasons);
					}

					// ** Bind value to result transport.
					result.Value = resultValue;
				}

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsSalesRepInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion GetTechDetails

		#region GetSalesInfo

		public IFnsResult<IFnsMsAccountSalesInformation> SalesInformationRead(long? accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "SalesInformationRead";
			var result = new FnsResult<IFnsMsAccountSalesInformation>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get tuple
				var tuple = SosCrmDataContext.Instance.MS_AccountSalesInformationsViews.Read(accountId);

				if (!tuple.IsLoaded)
					return new FnsResult<IFnsMsAccountSalesInformation>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = "Sorry but that AccountID did not return any sales information."
					};

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = new FnsMsAccountSalesInformation(tuple);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsAccountSalesInformation>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion GetSalesIfno

		#region Figure out Central Station

		private Main.MonitoringStations GetStationByIndustryAccountId(MS_IndustryAccount industryAccount)
		{
			// ** Init
			var msOSID = industryAccount.ReceiverLine.MonitoringStationOS.MonitoringStationOSID;

			switch (msOSID)
			{
				case MS_MonitoringStationOss.MetaDataStatic.AG_ALARMSYS:
				case MS_MonitoringStationOss.MetaDataStatic.AG_GPSTRACK:
					return Main.MonitoringStations.AvantGuard;
				case MS_MonitoringStationOss.MetaDataStatic.MI_MASTER:
					return Main.MonitoringStations.Monitronics;
				case MS_MonitoringStationOss.MetaDataStatic.MI_DICE:
					throw new NotImplementedException("Monitronics on DICE has not been implemented");
				case MS_MonitoringStationOss.MetaDataStatic.CC_MASTER:
					throw new NotImplementedException("Criticom on MASTERMind has not been implemented");
				default:
					throw new NotImplementedException(string.Format("This MonitoringStationOSID '{0}' has not been implemented."));
			}
		}

		#endregion Figure out Central Station

		#region Dispatch Agencies

		public IFnsResult<List<IFnsMsDispatchAgencyView>> GetDispatchAgencies(string city, string state, string zip, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "GetDispatchAgencies";
			var result = new FnsResult<List<IFnsMsDispatchAgencyView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get tuple
				var col = SosCrmDataContext.Instance.MS_DispatchAgenciesViews.GetDispatchAgencies(city, state, zip);


				if (col == null || col.Count < 3)
				{
					col = SosCrmDataContext.Instance.MS_DispatchAgenciesViews.GetMonitronicsEntityAgencies(city, state, zip);
					if (col == null || col.Count == 0)
						return new FnsResult<List<IFnsMsDispatchAgencyView>>
						{
							Code = (int)ErrorCodes.SqlItemNotFound,
							Message = string.Format("Sorry but given a city of '{0}', a state of '{1}', and a zip of '{2}' returned nothing.", city, state, zip)
						};
				}

				// ** Build result value
				var fnsList = col.Select(viewItem => new FnsMsDispatchAgencyView(viewItem)).Cast<IFnsMsDispatchAgencyView>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsDispatchAgencyView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsDispatchAgencyView>> GetDispatchAgenciesByAgencyTypeId(string city, string state, string zip, int agencyTypeId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "GetDispatchAgenciesByAgencyTypeId";
			var result = new FnsResult<List<IFnsMsDispatchAgencyView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get tuple
				var col = SosCrmDataContext.Instance.MS_DispatchAgenciesViews.GetDispatchAgenciesByAgencyTypeId(city, state, zip, agencyTypeId);

				if (col == null || col.Count == 0)
					return new FnsResult<List<IFnsMsDispatchAgencyView>>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = string.Format("Sorry but given a city of '{0}', a state of '{1}', a zip of '{2}', and agency type ID of '{3}' returned nothing.", city, state, zip, agencyTypeId)
					};

				// ** Build result value
				var fnsList = col.Select(viewItem => new FnsMsDispatchAgencyView(viewItem)).Cast<IFnsMsDispatchAgencyView>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsDispatchAgencyView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsDispatchAgencyType>> GetDispatchAgencyTypes(string monitoringStationsOSId, string gpEmployeeId)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "GetDispatchAgencyTypes";
			var result = new FnsResult<List<IFnsMsDispatchAgencyType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get tuple
				var qry = MS_DispatchAgencyType.Query().WHERE(MS_DispatchAgencyType.Columns.MonitoringStationOSId, monitoringStationsOSId);
				var col = SosCrmDataContext.Instance.MS_DispatchAgencyTypes.LoadCollection(qry);

				if (col == null || col.Count == 0)
					return new FnsResult<List<IFnsMsDispatchAgencyType>>
					{
						Code = (int)ErrorCodes.SqlItemNotFound,
						Message = string.Format("Sorry but given the MSOSID of '{0}' returned nothing.", monitoringStationsOSId)
					};

				// ** Build result value
				var fnsList = col.Select(viewItem => new FnsMsDispatchAgencyType(viewItem)).Cast<IFnsMsDispatchAgencyType>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsDispatchAgencyType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Dispatch Agencies

		#region Dispatch Agency Assignments

		public IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> SaveDaAssignmentsList(long accountId, List<int> list, string gpEmployeeId)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "SaveDaAssignmentsList";
			var result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				// Get MSOSID
				var msosId =
					SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(accountId)
						.IndustryAccount.ReceiverLine.MonitoringStationOSId;
				var assignedCol = new MS_AccountDispatchAgencyAssignmentViewCollection();

				foreach (var dispatchAgencyId in list)
				{
					assignedCol.Add(SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignmentViews.LoadSingle(SosCrmDataStoredProcedureManager.MS_AccountDispatchAgencyAssignmentSave(accountId, dispatchAgencyId, msosId, gpEmployeeId)));
				}


				// ** Build result value
				var fnsList = assignedCol.Select(viewItem => new FnsMsAccountDispatchAgencyAssignmentView(viewItem)).Cast<IFnsMsAccountDispatchAgencyAssignmentView>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsMsAccountDispatchAgencyAssignmentView> SaveDaAssignments(long accountDispatchAgencyAssignmentId, IFnsMsAccountDispatchAgencyAssignmentView dispatchAgency, string gpEmployeeId)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "SaveDaAssignments";
			var result = new FnsResult<IFnsMsAccountDispatchAgencyAssignmentView>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				// Get MSOSID
				var receiverLine =
					SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(dispatchAgency.AccountId)
						.IndustryAccount.ReceiverLine;
				var assignedCol = new MS_AccountDispatchAgencyAssignmentViewCollection();

				// ** Check if this is a new DA.
				if (accountDispatchAgencyAssignmentId != 0) // Then update
				{
					var accountDa =
						SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignments.LoadByPrimaryKey(accountDispatchAgencyAssignmentId);
					accountDa.PermitNumber = dispatchAgency.PermitNumber;
					accountDa.PermitEffectiveDate = dispatchAgency.PermitEffectiveDate;
					accountDa.PermitExpireDate = dispatchAgency.PermitExpireDate;
					accountDa.IsVerified = dispatchAgency.IsVerified;
					accountDa.IsActive = dispatchAgency.IsActive;
					accountDa.Save(gpEmployeeId);
					var qry = MS_AccountDispatchAgencyAssignmentView.Query()
						.WHERE(MS_AccountDispatchAgencyAssignmentView.Columns.DispatchAgencyAssignmentID, accountDispatchAgencyAssignmentId);
					var accountDaView = SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignmentViews.LoadSingle(qry);
					assignedCol.Add(accountDaView);
				}
				else
				{
					// Check to see if this is a selected DA or a new one
					if (dispatchAgency.DispatchAgencyId == 0)
					{
						// ** Have to check with Central Station.
						var centralStation = new CentralStation();
						var daType =
							SosCrmDataContext.Instance.MS_DispatchAgencyTypes.LoadByPrimaryKey(dispatchAgency.DispatchAgencyTypeId);
						var daResult = centralStation.FindDispatchAgency(daType.MsAgencyTypeNo, dispatchAgency.Phone1, dispatchAgency.CityName, dispatchAgency.StateAB, dispatchAgency.ZipCode,
							gpEmployeeId);

						if (daResult.Code != BaseErrorCodes.ErrorCodes.Success.Code())
						{
							result.Code = daResult.Code;
							result.Message = daResult.Message;
							return result;
						}

						// ** Create the assignments
						var filtereDispatchAgencies = from cust in daResult.Value
													  where
														  cust.DispatchAgencyTypeId == dispatchAgency.DispatchAgencyTypeId && cust.Phone1.Equals(dispatchAgency.Phone1)
													  select cust;
						// ** // ** Check that there is something to save
						if (Equals(filtereDispatchAgencies.Count(), 0))
						{
							result.Code = BaseErrorCodes.ErrorCodes.CSDispatchAgencyNotFound.Code();
							result.Message = string.Format(BaseErrorCodes.ErrorCodes.CSDispatchAgencyNotFound.Message(),
								receiverLine.MonitoringStationOS.MonitoringStation.MonitoringStationName,
								string.Format("AgencyTypeId={0};Phone={1};ZipCode={2}", daType.MsAgencyTypeNo, dispatchAgency.Phone1, dispatchAgency.ZipCode),
								receiverLine.MonitoringStationOS.MonitoringStation.ContactPhoneNumber);
							return result;
						}
						foreach (MS_DispatchAgency msDispatchAgency in filtereDispatchAgencies)
						{
							assignedCol.Add(SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignmentViews.AssignDa(msDispatchAgency.DispatchAgencyID, dispatchAgency.AccountId, dispatchAgency.IndustryAccountID, gpEmployeeId));
						}

					}
					else
					{
						assignedCol.Add(SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignmentViews.AssignDa(dispatchAgency.DispatchAgencyId, dispatchAgency.AccountId, dispatchAgency.IndustryAccountID, gpEmployeeId));
					}
				}

				// ** Build result value
				var fnsList = assignedCol.Select(viewItem => new FnsMsAccountDispatchAgencyAssignmentView(viewItem)).Cast<IFnsMsAccountDispatchAgencyAssignmentView>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList.First();
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsMsAccountDispatchAgencyAssignmentView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> ReadDaAssignments(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "ReadDaAssignments";
			var result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Build list of assignments
				var qry = MS_AccountDispatchAgencyAssignmentView.Query()
					.WHERE(MS_AccountDispatchAgencyAssignmentView.Columns.AccountId, accountId)
					.WHERE(MS_AccountDispatchAgencyAssignmentView.Columns.IsActive, true);
				var assignedCol = SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignmentViews.LoadCollection(qry);

				// ** Build result value
				var fnsList = assignedCol.Select(viewItem => new FnsMsAccountDispatchAgencyAssignmentView(viewItem)).Cast<IFnsMsAccountDispatchAgencyAssignmentView>().ToList();

				// ** Set result values
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = fnsList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at '{1}': {0}", ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> DeleteDaAssignments(int dispatchAgencyAssignmentID, string gpEmployeeId)
		{
			#region INITIALIZATION
			// ** Initialize 
			const string METHOD_NAME = "DeleteDaAssignments";
			var result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing '{0}'", METHOD_NAME)
			};
			#endregion INITIALIZATION

			#region TRY
			try
			{
				// Get Row
				var daAssignment = SosCrmDataContext.Instance.MS_AccountDispatchAgencyAssignments.LoadByPrimaryKey(dispatchAgencyAssignmentID);

				// ** Check that there is a value
				if (daAssignment == null || !daAssignment.IsLoaded)
				{
					result.Code = BaseErrorCodes.ErrorCodes.InvalidPrimaryKeyId.Code();
					result.Message = string.Format(BaseErrorCodes.ErrorCodes.InvalidPrimaryKeyId.Message(), dispatchAgencyAssignmentID);
					return result;
				}

				// ** Delete assignment
				daAssignment.IsDeleted = true;
				daAssignment.IsActive = false;
				daAssignment.Save(gpEmployeeId);

				// ** Set result values
				return ReadDaAssignments(daAssignment.AccountId, gpEmployeeId);
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>>
				{
					Code = BaseErrorCodes.ErrorCodes.UnexpectedException.Code(),
					Message = string.Format(BaseErrorCodes.ErrorCodes.UnexpectedException.Message(), ex.Message, METHOD_NAME)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Dispatch Agency Assignments
	}
}