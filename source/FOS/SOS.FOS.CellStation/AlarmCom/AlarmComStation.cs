using System.Globalization;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.CellStation.AlarmComWebService;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;

namespace SOS.FOS.CellStation.AlarmCom
{
	public class AlarmComStation
	{
		public AlarmComStation(ICustomerManagementSoapClient customerClient, AlarmComAccount account)
		{
			_customerClient = customerClient;
			Account = account;
			CellStation = CellularStationFactory.CellStationEnum.AlarmCom;
		}

		#region Properties

		//private const int LENTH_OF_SERIALNUMBER = 10;
		/*
				private const int _MASTER_USER_CODE = 1234;
		*/
		private const string _PREFIX = "11317";
		//public MS_Account MsAccount { get; private set; }
		public AlarmComAccount Account { get; private set; }
		readonly ICustomerManagementSoapClient _customerClient;
		public CellularStationFactory.CellStationEnum CellStation { get; private set; }

		#endregion Properties

		#region Methods

		#region Private

		private Authentication GetAuth()
		{
			return new Authentication { Password = Account.Password, User = Account.Username };
		}

		private Result<bool> ValidateData(string serialNumber)
		{
			var result = ValidateSerialNumber(serialNumber);
			if (result.Code != 0)
			{
				return result;
			}

			var alarmComAccount = Account;
			if (alarmComAccount.IndustryAccount == null)
			{
				// check for IndustryAccount
				result.Code = -1;
				result.Message = "No Industry Account";
			}
			else
			{
				// it's valid
				result.Code = 0;
				result.Value = true;
			}
			return result;
		}
		private static Result<bool> ValidateSerialNumber(string serialNumber)
		{
			var result = new Result<bool>();
			long num;
			if (serialNumber != null && (!long.TryParse(serialNumber, out num) || num.ToString(CultureInfo.InvariantCulture).Length != 15))
			{
				// Check the length of the number
				result.Code = -1;
				result.Message = string.Format("Invalid Serial Number: {0}. A valid Alarm.com serial number is a 15 digit number.", serialNumber);
			}
			else
			{
				// it's valid
				result.Code = 0;
				result.Value = true;
			}
			return result;
		}

		private static void EnsureSerialNumberPrefix(ref string serialNumber)
		{
			// prefix serial with Alarm.Com number
			if (serialNumber != null && serialNumber.Length == 10)
				serialNumber = _PREFIX + serialNumber;
		}

		#endregion Private

		#region Public

		/// <summary>
		/// Given an AlarmComAccount with an AccountID assigned to it, the method will return an
		/// Alarm.com Customer ID.
		/// </summary>
		/// <param name="oAcct">AlarmComAccount</param>
		/// <returns>int</returns>
		public Result<int> GetCustomerIDByDealerCustomerId(AlarmComAccount oAcct)
		{
			// Locals
			var result = new Result<int>();

			// Execute Retrieve
			try
			{
				result.Value = _customerClient.LookupCustomerIdFromDealerCustomerId(GetAuth(), oAcct.AccountID.ToString(CultureInfo.InvariantCulture));
				result.Code = 0;
				// Check result
				if (result.Value <= 0)
				{
					// set warning message
					result.Message = string.Format("No CustomerID associated with Account # '{0}'.", oAcct.AccountID);
				}
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Retrieving Customer ID: {0}", ex.Message);
			}

			// Return result
			return result;
		}

		//public int GetCustomerIDByDealerCSID(AlarmComAccount oAcct) {
		//    // Locals
		//    var nCustomerId = 0;

		//    // Execute Retrieve
		//    try {
		//        nCustomerId = oCustomer.LookupCustomerId(GetAuth()
		//            , oAcct.ReceiverLine.ReceiverNumber
		//            , oAcct.CsId);

		//        // Check for result
		//        if (nCustomerId > 0)
		//            ErrorManager.AddSuccessMessage("Successful Transaction"
		//                , string.Format("Successfully retrieved Customer ID: {0}.", nCustomerId));
		//        else
		//            ErrorManager.AddCriticalMessage("Error Retrieving Customer ID"
		//                , string.Format("Sorry there is not customer with Account # '{0}'.", oAcct.AccountID));
		//    } catch (Exception ex) {
		//        ErrorManager.AddCriticalMessage(ex
		//            , "Error Retrieving Customer ID on GetCustomerIDByDealerCSID"
		//            , string.Format("This is the error: {0}", ex.Message));
		//    }

		//    // Return result
		//    return nCustomerId;
		//}
		/// <summary>
		/// Given an AlarmComAccount with a CSID assigned to it, the method will return an
		/// Alarm.com Customer ID.
		/// </summary>
		/// <returns>int</returns>
		/// <summary>
		/// Given an AlarmComAccount with a CSID assigned to it, and an Alarm.com Serial #
		/// , the method will return an Alarm.com Customer ID.
		/// </summary>
		/// <param name="oAcct">
		///     AlarmComAccount
		///     AlarmComAccount
		/// </param>
		/// <param name="serialNumber">string</param>
		/// <returns>int</returns>
		public Result<int> GetCustomerIDBySerialNumber(AlarmComAccount oAcct, string serialNumber)
		{
			var result = new Result<int>();
			try
			{
				result.Value = _customerClient.LookupCustomerIdFromModemSerial(GetAuth(), serialNumber);
				if (result.Value > 0)
				{
					// got a result back
					result.Code = 0;
				}
				else
				{
					// no customerID found
					result.Code = -1;
					result.Message = string.Format("No CustomerID associated with '{0}' Serial Number", serialNumber);
				}
			}
			catch (Exception ex)
			{
				result.Message = string.Format("Error Retrieving CustomerID: {0}", ex.Message);
			}
			return result;
		}

		/// <summary>
		/// Given an AlarmComAccount and the Customer ID, it will return an EquipmentList
		/// </summary>
		/// <returns>EquipmentList</returns>
		//public EquipmentList GetDeviceList(AlarmComAccount oAcct, int nCustomerID) {
		//    // Locals
		//    EquipmentList oResultList = null;

		//    // Execute Retrieve
		//    try {
		//        var oPanelList = oCustomer.GetDeviceList(GetAuth(), nCustomerID);

		//        foreach (var oDevice in oPanelList) {
		//            // Check that there is a EquipmentList instance
		//            if (oResultList == null)
		//                oResultList = new EquipmentList();

		//            // Bind data
		//            var oRw = oResultList.Equipment.NewEquipmentRow();
		//            oRw.DeviceId = oDevice.DeviceId;
		//            //oRw.Group = oDevice.Group;//@DO:
		//            oRw.InstallDate = oDevice.InstallDate;
		//            if (oDevice.MaintainDate != null)
		//                oRw.MaintainDate = (DateTime)oDevice.MaintainDate;
		//            oRw.MonitoredForNormalActivity = oDevice.MonitoredForNormalActivity;
		//            oRw.Partition = oDevice.Partition;
		//            if (oDevice.StatusDate != null)
		//                oRw.StatusDate = (DateTime)oDevice.StatusDate;
		//            oRw.WebSiteDeviceName = oDevice.WebSiteDeviceName;

		//            // Add to List
		//            oResultList.Equipment.AddEquipmentRow(oRw);
		//        }

		//        // Check for result
		//        if (oResultList == null)
		//            ErrorManager.AddSuccessMessage("Successful Transaction"
		//                , string.Format("Successfully retrieved a list of Devices."));
		//        else
		//            ErrorManager.AddCriticalMessage("Error Retrieving Device List"
		//                , string.Format("Sorry no data was returned."));
		//    } catch (Exception ex) {`
		//        ErrorManager.AddCriticalMessage(ex
		//            , "Error Retrieving Device List on GetDeviceList"
		//            , string.Format("This is the error: {0}", ex.Message));
		//    }

		//    // Return result
		//    return oResultList;
		//}

		/// <summary>
		/// Given an AlarmComAccount and a CustomerID it will request from the Device to send device list
		/// to server.
		/// </summary>
		/// <param name="waitUntilPanelConnects">bool</param>
		/// <returns></returns>
		public Result<bool> RequestEquipmentList(bool waitUntilPanelConnects)
		{
			var result = new Result<bool>();
			var alarmComAccount = Account;
			try
			{
				// ReSharper disable once PossibleInvalidOperationException
				if (_customerClient.RequestSensorNames(GetAuth(), alarmComAccount.CustomerID.Value, waitUntilPanelConnects))
				{
					result.Code = 0;
					result.Value = true;
				}
				else
				{
					result.Code = -1;
					result.Message = "Failed to Request Equipment List";
				}
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Retrieving Equipment List: {0}", ex.Message);
			}
			return result;
		}

		/// <summary>
		/// This is a unique method for Alarm.com accounts.  Given an AlarmComAccount object
		/// it will return an EquipmentList so the data can be bound to a grid.
		/// </summary>
		/// <returns>EquipmentList</returns>
		public Result<List<PanelDevice>> GetEquipmentList()
		{
			var result = new Result<List<PanelDevice>>(value: new List<PanelDevice>());
			var alarmComAccount = Account;
			try
			{
				// ReSharper disable once PossibleInvalidOperationException
				result.Value.AddRange(_customerClient.GetDeviceList(GetAuth(), alarmComAccount.CustomerID.Value));
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Retrieving Device List: {0}", ex.Message);
			}
			return result;
		}

		/// <summary>
		/// Given an AlarmComAccount object and a szNewSerialNumber it will swap the old serial number 
		/// with the new one on the CustomerID account.  Error messages will be saved in the ErrorManager
		/// object.
		/// </summary>
		/// <param name="newSerialNumber"></param>
		/// <param name="swapReason"></param>
		/// <param name="specialRequest"></param>
		/// <param name="restoreBackedUpSettingsAfterSwap"></param>
		/// <returns></returns>
		public Result<bool> SwapModem(string newSerialNumber, string swapReason = null, string specialRequest = null, bool restoreBackedUpSettingsAfterSwap = false)
		{
			//
			EnsureSerialNumberPrefix(ref newSerialNumber);

			// Locals
			var alarmComAccount = Account;
			var result = ValidateSerialNumber(newSerialNumber);
			if (result.Code != 0)
			{
				return result;
			}

			try
			{
				if (alarmComAccount.SerialNumber == string.Empty)
				{
					result.Code = -1;
					result.Message = "Unable to swap modem - Missing current Serial Number.";
					return result;
				}

				if (alarmComAccount.SerialNumber == newSerialNumber)
				{
					result.Code = -1;
					result.Message = "Attempted to swap to same Serial Number.";
					return result;
				}

				var swapModemOutput = _customerClient.SwapModem(GetAuth(), new SwapModemInput
				{
					NewSerialNumber = newSerialNumber,
					SwapReason = swapReason,
					SpecialRequest = specialRequest,
					// ReSharper disable once PossibleInvalidOperationException
					CustomerId = alarmComAccount.CustomerID.Value,
					RestoreBackedUpSettingsAfterSwap = restoreBackedUpSettingsAfterSwap,
				});

				// Check result
				if (swapModemOutput.Success)
				{
					try
					{
						var oAlarmCom = new MS_ReceiverLineBlockAlarmCom();
						oAlarmCom.LoadByKey(alarmComAccount.IndustryAccount.ReceiverLineBlockId);
						oAlarmCom.SerialNumber = newSerialNumber;
						oAlarmCom.CustomerId = alarmComAccount.CustomerID;
						oAlarmCom.Save();

						// Set Success
						result.Code = 0;
						result.Value = true;
					}
					catch (Exception ex)
					{
						result.Code = -1;
						result.Message = string.Format("Error Saving Successful Modem Swap: {0}", ex.Message);
					}
				}
				else
				{
					result.Code = -1;
					result.Message = string.Format("Alarm.com Error Swapping Modem: {0}", swapModemOutput.ErrorMessage);
				}
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Swaping Modem: {0}", ex.Message);
			}

			// Return result
			return result;
		}

		public Result<bool> ChangeServicePackage(string gpEmployeeID, string cellPackageItemId)
		{
			var alarmComAccount = Account;
			var result = new Result<bool>();
			try
			{
				var servicePackage = AlarmComAccount.ToEnumServicePackage(cellPackageItemId);
				if (servicePackage == AlarmComAccount.EnumServicePackage.NotSet)
				{
					result.Code = -1;
					result.Message = string.Format("'{0}' is not an Alarm.com package", cellPackageItemId);
					return result;
				}

				// ReSharper disable once PossibleInvalidOperationException
				var changePlanOutput = _customerClient.ChangeServicePlan(GetAuth(), alarmComAccount.CustomerID.Value, (int)servicePackage,
					(alarmComAccount.EnableTwoWay ? new[] { AddOnFeatureEnum.TwoWayVoice } : new AddOnFeatureEnum[0]));
				// Check result
				if (changePlanOutput.Success)
				{
					// save new package
					var account = alarmComAccount.MsAccount;
					account.CellPackageItemId = cellPackageItemId;
					account.Save(gpEmployeeID);

					// Set Success
					result.Value = true;
				}
				else
				{
					result.Code = -1;
					result.Message = string.Format("Alarm.com Error Changing Service Package: {0}", changePlanOutput.ErrorMessage);
				}
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Changing Service Package: {0}", ex.Message);
			}
			return result;
		}

		//public bool GetAndSaveCustomerInfo(AlarmComAccount alarmDotComAccount, MS_Account account)
		//{
		//    bool customerFound = false;
		//    CustomerInfo custInfo;

		//    int alarmDotComCustID = GetCustomerIDByDealerAccountNumber(alarmDotComAccount);
		//    ErrorManager.MessageList.Clear();

		//    if (alarmDotComCustID > 0)
		//    {
		//        try
		//        {
		//            custInfo = oCustomer.GetCustomerInfo(GetAuth(), alarmDotComCustID);
		//            customerFound = true;

		//             MS_ReceiverLineBlockAlarmCom alarmComReceveirLine =  MS_ReceiverLineBlockAlarmCom.FetchByID(account.CellIndustryAccount.BlockAccountId);

		//            if (custInfo.ModemInfo != null &&
		//                custInfo.ModemInfo.ModemSerial == alarmComReceveirLine.SerialNumber)
		//            {
		//                alarmComReceveirLine.IsTwoWay = custInfo.CentralStationInfo.TwoWayVoiceEnabled;
		//                alarmComReceveirLine.SerialNumber = custInfo.ModemInfo.ModemSerial;
		//                alarmComReceveirLine.CustomerId = custInfo.CustomerId;
		//                alarmComReceveirLine.Save();
		//            }
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorManager.AddCriticalMessage(ex
		//                , "Error Retrieving Customer ID on GetCustomerIDBySerialNumber"
		//                , string.Format("This is the error: {0}", ex.Message));
		//        }
		//    }

		//    return customerFound;
		//}
		#endregion Public

		#region Events

		//private static IMonitoredMessage ErrorManagerOnNewMessage(MonitoredMessageType eMessageType, Exception ex, string szTitle, string szMessage) {
		//    // Locals
		//    return new AlarmComMonitoredMessage {
		//        Title = szTitle,
		//        DisplayMessage = szMessage,
		//        MessageType = eMessageType,
		//        Ex = ex
		//    };
		//}

		#endregion Events

		#endregion Methods

		/// <summary>
		/// Given an ICellularAccount object it will terminate the Alarm.com account.  The messages
		/// will be saved in the ErrorManager field of this object.
		/// </summary>
		/// <returns>bool</returns>
		public Result<bool> TerminateAccount()
		{
			var result = new Result<bool>();
			var alarmComAccount = Account;

			// ReSharper disable once PossibleInvalidOperationException
			if (_customerClient.TerminateCustomer(GetAuth(), alarmComAccount.CustomerID.Value))
			{
				result.Code = 0;
				result.Value = true;
				// Set the termination date on the account
				SetTerminationDateOnBlock();
			}
			else
			{
				result.Code = -1;
				result.Message = string.Format("Unable to Terminate Account with Customer ID '{0}'.", alarmComAccount.CustomerID);
			}
			return result;
		}
		private void SetTerminationDateOnBlock()
		{
			var alarmComAccount = Account;
			var lineBlock = SosCrmDataContext.Instance.MS_ReceiverLineBlockAlarmComs.LoadByPrimaryKey(alarmComAccount.IndustryAccount.ReceiverLineBlockId);

			// Check if there.
			// Check if it is loaded
			if (lineBlock == null || !lineBlock.IsLoaded)
			{
				lineBlock = new MS_ReceiverLineBlockAlarmCom { ReceiverLineBlockID = alarmComAccount.IndustryAccount.ReceiverLineBlockId };
			}

			// Save Changes
			lineBlock.CustomerId = alarmComAccount.CustomerID;
			lineBlock.UnRegisteredDate = DateTime.Now;
			lineBlock.Save();
		}

		/// <summary>
		/// Given a ICelluar Account it will create an account for the caller.
		/// If there are any errors they are stored in the ErrorManager property.
		/// </summary>
		/// <param name="serialNumber">string</param>
		/// <param name="enableTwoWay">bool</param>
		/// <param name="gpTechId">string</param>
		/// <param name="gpEmployeeId">string</param>
		/// <returns>bool</returns>
		public Result<CustomerOutput> CreateAccount(string serialNumber, bool enableTwoWay, string gpTechId, string gpEmployeeId)
		{
			// ** Initialize.
			var adcSubmit = new MS_AccountCellularSubmit();
			adcSubmit.AccountCellularSubmitTypeId = (short)MS_AccountCellularSubmitType.AccountCellularSubmitTypeEnum.Register_Unit;
			adcSubmit.AccountCellularSubmitVendorId = (short)MS_AccountCellularSubmitVendor.AccountCellularSubmitVendorEnum.Alarmcom;
			adcSubmit.AccountId = Account.AccountID;
			adcSubmit.IndustryAccountId = Account.IndustryAccountID;
			adcSubmit.MonitoringStationOSId = Account.IndustryAccount.ReceiverLine.MonitoringStationOSId;
			adcSubmit.GPTechId = gpTechId;
			adcSubmit.DateSubmitted = DateTime.UtcNow;
			adcSubmit.Save(gpEmployeeId);


			// ** Check prefix
			EnsureSerialNumberPrefix(ref serialNumber);

			var result = new Result<CustomerOutput>();
			var alarmComAccount = Account;

			// Validate Data
			var validationResult = ValidateData(serialNumber);
			if (validationResult.Code != 0)
			{
				result.Code = validationResult.Code;
				result.Message = validationResult.Message;
				return result;
			}

			// Save Serial Number
			var receiverLineBlock = SosCrmDataContext.Instance.MS_ReceiverLineBlockAlarmComs.EnsureLoadByReceiverLineBlockID(alarmComAccount.IndustryAccount.ReceiverLineBlockId);
			try
			{
				receiverLineBlock.SerialNumber = serialNumber;
				receiverLineBlock.CustomerId = alarmComAccount.CustomerID;
				receiverLineBlock.IsTwoWay = enableTwoWay;
				receiverLineBlock.RegisteredDate = DateTime.UtcNow;
				receiverLineBlock.UnRegisteredDate = null;
				receiverLineBlock.Save();
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = "Error saving MS_ReceiverLineBlockAlarmcom: " + ex.Message;
				return result;
			}
			// re-set customer properties after saving new data
			alarmComAccount.SetCustomerProperties();

			// Create Account
			var csid = alarmComAccount.IndustryAccount.Csid.Trim();
			var address = alarmComAccount.Customer.Address;
			var premiseAddress = new AddressWithName
			{
				Street1 = address.StreetAddress,
				City = address.City,
				State = address.StateId,
				Zip = address.PostalCode,
				FirstName = alarmComAccount.Customer.FirstName,
				LastName = alarmComAccount.Customer.LastName,
			};
			if (address.StreetAddress2 != null)
			{
				premiseAddress.Street2 = address.StreetAddress2;
			}
			var oInput = new CreateCustomerInput
			{
				CustomerAccountAddress = premiseAddress,
				CustomerAccountEmail = alarmComAccount.CustomerAccountEmail,
				CustomerAccountPhone = alarmComAccount.CustomerAccountPhone,
				DealerCustomerId = alarmComAccount.AccountID.ToString(CultureInfo.InvariantCulture),
				//DesiredLoginName = alarmComAccount.CustomerAccountEmail,
				DesiredLoginName = serialNumber,
				DesiredPassword = "n" + csid, // alarm.com now wants 7 or more characters. //.Substring(csid.Length - 6), // last six digits of CSID???
				InstallationAddress = premiseAddress,
				InstallationTimeZone = TimeZoneEnum.NotSet,
				Culture = CultureEnum.Unknown,
				PanelType = PanelTypeEnum.NotSet, // set below
				PanelVersion = PanelVersionEnum.Unknown,
				ModemSerialNumber = alarmComAccount.SerialNumber.Substring(alarmComAccount.SerialNumber.Length - 10), // last 10 digits
				UnitDescription = null,
				CsEventGroupsToForward = null,
				PhoneLinePresent = false, // set below
				CentralStationForwardingOption = CentralStationForwardingOptionEnum.NotSet, // set below
				CentralStationAccountNumber = csid.Substring(csid.Length - 4), // User Story 359:Only pass the last 4 digits of the CSID
				CentralStationReceiverNumber = alarmComAccount.IndustryAccount.ReceiverLine.ReceiverNumber,

				/*
				 * WSF -- Wireless Signal Forwarding (4697).
				 * BI -- Basic Interactive (4698).
				 * AI -- Advanced Interactive (4699).
				 */
				PackageId = (int)alarmComAccount.ServicePackageID,
				AddOnFeatures = (alarmComAccount.EnableTwoWay) ? new[] { AddOnFeatureEnum.TwoWayVoice, AddOnFeatureEnum.WeatherToPanel } : null,
				IgnoreLowCoverageErrors = true, // This means to ignore the lowcoverage errors since we have already checked...
				BranchId = 0,
				LeadId = 0,
				CustomerNotifications = null,
			};


			// set panel type;
			switch (alarmComAccount.MsAccount.PanelTypeId)
			{
				case MS_AccountPanelType.MetaData.SimonID:
					oInput.PanelType = PanelTypeEnum.Simon;
					break;
				case MS_AccountPanelType.MetaData.ConcordID:
					oInput.PanelType = PanelTypeEnum.Concord;
					break;
				case MS_AccountPanelType.MetaData._2_Gig_ID:
					oInput.PanelType = PanelTypeEnum.TwoG;
					break;
			}


			// Check if this is Cell Primary or Backup. 
			// - If Cell primary then PhoneLinePresent is always false and CentralStaetionForwarding is Always;
			// - If Cell Backup then PhoneLinePresent is True and CentralStationForwarding is OnlyIfPhoneFails.
			switch (alarmComAccount.MsAccount.CellularTypeId)
			{
				case "CELLPRI":
					oInput.PhoneLinePresent = false;
					oInput.CentralStationForwardingOption = CentralStationForwardingOptionEnum.Always;
					break;
				case "CELLSEC":
					oInput.PhoneLinePresent = true;
					oInput.CentralStationForwardingOption = CentralStationForwardingOptionEnum.OnlyIfPhoneFails;
					break;
				default:
					throw new Exception(string.Format("Invalid cellular type '{0}'", alarmComAccount.MsAccount.CellularTypeId));
			}

			// Execute Creation
			var createCustomerOutput = _customerClient.CreateCustomer(GetAuth(), oInput);
			adcSubmit.Message = createCustomerOutput.ErrorMessage;

			// check results
			if (!createCustomerOutput.Success)
			{
				// ** Save action information
				adcSubmit.WasSuccessfull = false;
				adcSubmit.Save(gpEmployeeId);

				var adcFail = new MS_AccountCellularADCRegister();
				adcFail.AccountCellularSubmitID = adcSubmit.AccountCellularSubmitID;
				adcFail.CustomerID = createCustomerOutput.CustomerId;
				adcFail.ErrorMessage = createCustomerOutput.ErrorMessage;
				adcFail.LoginName = createCustomerOutput.LoginName;
				adcFail.Password = createCustomerOutput.Password;
				adcFail.Success = createCustomerOutput.Success;
				adcFail.Save(gpEmployeeId);

				result.Code = -1;
				result.Message = string.Format("Alarm.com Error Submitting Account: {0}", createCustomerOutput.ErrorMessage);
				return result;
			}

			// ** Save action information
			adcSubmit.WasSuccessfull = true;
			adcSubmit.Save(gpEmployeeId);

			var adcSubmitInfo = new MS_AccountCellularADCRegister();
			adcSubmitInfo.AccountCellularSubmitID = adcSubmit.AccountCellularSubmitID;
			adcSubmitInfo.CustomerID = createCustomerOutput.CustomerId;
			adcSubmitInfo.ErrorMessage = createCustomerOutput.ErrorMessage;
			adcSubmitInfo.LoginName = createCustomerOutput.LoginName;
			adcSubmitInfo.Password = createCustomerOutput.Password;
			adcSubmitInfo.Success = createCustomerOutput.Success;
			adcSubmitInfo.Save(gpEmployeeId);

			try
			{

				// save results
				try
				{
					receiverLineBlock.CustomerId = createCustomerOutput.CustomerId;
					receiverLineBlock.Save();
				}
				catch (Exception ex)
				{
					result.Code = -1;
					result.Message = "Error saving CustomerId in MS_ReceiverLineBlockAlarmcom: " + ex.Message;
					return result;
				}

				// return results
				result.Code = 0;
				result.Value = new CustomerOutput
				{
					CustomerId = createCustomerOutput.CustomerId,
					Password = createCustomerOutput.Password,
					LoginName = createCustomerOutput.LoginName,
				};
			}
			catch (Exception ex)
			{
				result.Code = -1;
				result.Message = string.Format("Error Saving Successful Account Creation: {0}", ex.Message);
			}

			// Return result
			return result;
		}

		/// <summary>
		/// Given a ICellularAccount it will return a ICellularAccountStatus.  This is used to see
		/// what the status of a cellular account is.
		/// </summary>
		/// <returns>ICellularAccountStatus</returns>
		public Result<AlarmComAccountStatus> AccountStatus()
		{
			// Locals
			var alarmComAccount = Account;
			var result = new Result<AlarmComAccountStatus>(value: new AlarmComAccountStatus());

			// Execute Request
			try
			{
				var customerID = alarmComAccount.CustomerID.HasValue ? alarmComAccount.CustomerID.Value : 0;
				//if (customerID == 0) {
				//    result.Value = new AlarmComAccountStatus(EnumCellularAccountStatus.Unregistered);
				//    return result;
				//}

				if (customerID <= 0)
				{
					// try to get customerID using accountID
					var customerIDResult = GetCustomerIDByDealerCustomerId(alarmComAccount);
					if (customerIDResult.Code != 0)
					{
						result.Code = customerIDResult.Code;
						result.Value.Message = "Error getting CustomerID from Alarm.com";
						result.Message = customerIDResult.Message;
						return result;
					}
					customerID = customerIDResult.Value;
					if (customerID <= 0)
					{
						result.Code = 0; // This is not an error. The account is just not registered with Alarm.com
						result.Value.Message = "Account missing CustomerID and could not find it on Alarm.com's system.";
						result.Message = customerIDResult.Message;
						return result;
					}
				}

				var customerInfo = _customerClient.GetCustomerInfo(GetAuth(), customerID);
				// Bind Results to AccountStatus
				result.Value.SetCustInfo(customerInfo, alarmComAccount);
				result.Value.SaveAccountInfo(customerInfo, alarmComAccount);
			}
			catch (Exception ex)
			{
				result.Value.Message = "AccountStatus Error";
				result.Code = -1;
				result.Message = string.Format("Error occured: {0}", ex.Message);
				return result;
			}

			// Return result
			return result;
		}

		//public CustomerInfo GetCustomerInfoByIndustryAccount(MS_IndustryAccount oIndAcct) {
		//    // Locals
		//    var lCellAccount = new AlarmComAccount(oIndAcct);

		//    // Execute Request
		//    try {
		//        if (lCellAccount.CustomerID == null || lCellAccount.CustomerID.Value == 0) return null;

		//        CustomerInfo oCustInfo;
		//        if (lCellAccount.CustomerID.Value > 0)
		//            oCustInfo = oCustomer.GetCustomerInfo(GetAuth(), lCellAccount.CustomerID.Value);
		//        else {
		//            var customerID = GetCustomerIDByDealerAccountNumber(lCellAccount).Value;
		//            if (customerID > 0) {
		//                oCustInfo = oCustomer.GetCustomerInfo(GetAuth(), customerID);
		//            } else {
		//                ErrorManager.AddCriticalMessage(string.Format("Unable to Find CustomerID on AccountID\\CSID ({0}\\{1})", oIndAcct.AccountId, oIndAcct.Csid)
		//                        , "Sorry but this account is missing the Customer ID and we were not able to find it on Alarm.com's system.");
		//                return null;
		//            }
		//        }

		//        // Return the customerInfo result
		//        return oCustInfo;
		//    } catch (Exception ex) {
		//        ErrorManager.AddCriticalMessage(ex
		//            , "Error On GetAccountStatus"
		//            , string.Format("The following error occured on AccountID\\CSID ({0}\\{1}): {2}"
		//            , oIndAcct.AccountId
		//            , oIndAcct.Csid
		//            , ex.Message));

		//        return null;
		//    }
		//}
	}
}