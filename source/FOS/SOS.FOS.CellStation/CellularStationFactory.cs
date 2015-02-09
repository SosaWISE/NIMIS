using SOS.FOS.CellStation.AlarmCom;
//using PPro.Data.InterimCrm;
//using PPro.Lib.Core.Monitoring;
using SOS.Data.SosCrm;
using SSE.Lib.Interfaces.FOS;
using SOS.Lib.Core;

namespace SOS.FOS.CellStation
{
	public class CellularStationFactory
	{
		#region Properties
		public enum CellStationEnum
		{
			AlarmNet = 1,
			AlarmCom = 2,
			Tellular = 3,
			Inconclusive = 4
		}
		#endregion Properties

		#region Methods

		#region Public

		/// <summary>
		/// Given an MS_Account this function determins which cellular station to use based on the panel type.
		/// </summary>
		/// <returns>ICellularStation</returns>
		//public ICellularStation GetStation(MS_Account aMsAccount)//, MonitoredMessageList oErrorManager)
		//{
		//    // Locals
		//    // Check that this is a cellular type of an account
		//    if (aMsAccount.CellularTypeId == "NOCELL") {//@REVIEW: make "NOCELL" a static readonly variable
		//        //oErrorManager.AddCriticalMessage("No Cellular Account"
		//        //    , string.Format("This account '{0}' is flagged as a No Cell Account.", aMsAccount.AccountID));
		//        return null;
		//    }

		//    //// Check that the account has been assigned a panel
		//    //if (aMsAccount.PanelAccountInventory == null)
		//    //{
		//    //    //oErrorManager.AddCriticalMessage("Panel Not Assigned"
		//    //    //    , string.Format("This account has not been assigned a panel yet.  Please assign a panel and then try again."));
		//    //    return null;
		//    //}

		//    // Check That the equipment has a Panel Type
		//    if (aMsAccount.PanelType == null) {
		//        //oErrorManager.AddCriticalMessage("Invalid Panel Equipment Type"
		//        //    , string.Format("The panel assigned to this account '{0}' does not have a Panel Type assigned to it.  Please contact network administrator with this error message."
		//        //        , aMsAccount.PanelAccountInventory.Equipment.ItemDescription));
		//        return null;
		//    }

		//    // Get station
		//    var oResult = GetStation(GetCellStationEnum(aMsAccount), aMsAccount.IndustryAccount);//, oErrorManager);

		//    // Return result
		//    return oResult;
		//}


		public Result<AlarmComStation> GetAlarmComStation(MS_Account msAccount, AE_Customer customer, MS_VendorAlarmComAccount vendorAccount, MS_IndustryAccount industryAccount)
		{
			var result = new Result<AlarmComStation>();
			if (msAccount == null)
			{
				result.Code = -1;
				result.Message = "Invalid AccountID";
			}
			else if (vendorAccount == null)
			{
				result.Code = -1;
				result.Message = "No Alarm.com vendor account for AccountID";
			}
			else if (customer == null)
			{
				result.Code = -1;
				result.Message = "No customer for AccountID";
			}
			else if (msAccount.Account.AccountTypeId != "ALRM")
			{
				// check account type????
				result.Code = -1;
				result.Message = string.Format("Expected ALRM AccountType, not '{0}'", msAccount.Account.AccountTypeId);
			}
			else if (msAccount.CellularTypeId == "NOCELL")
			{//@REVIEW: make "NOCELL" a static readonly variable
				// Check that this is a cellular type of an account
				result.Code = -1;
				result.Message = string.Format("No Cellular Account. '{0}' account is flagged as a No Cell Account.", msAccount.AccountID);
			}
			else if (msAccount.PanelType == null)
			{
				// Check that the account has been assigned a panel
				result.Code = -1;
				result.Message = "Panel Not Assigned. This account has not been assigned a panel yet.  Please assign a panel and then try again.";
			//}
			//else if (GetCellStationEnum(msAccount.PanelTypeId) != CellStationEnum.AlarmCom)
			//{
			//	// check correct panel type
			//	result.Code = -1;
			//	result.Message = string.Format("Invalid Panel Type. '{0}' is not an Alarm.com panel.", msAccount.PanelType.PanelTypeName);
			}
			else
			{
				// set value
				// Get station
				var client = new AlarmComWebService.CustomerManagementSoapClient();
				//var client = new SOS.FOS.CellStation.FauxCustomerManagementClient
				result.Value = new AlarmComStation(client, new AlarmComAccount(msAccount, customer, vendorAccount, industryAccount));
			}
			return result;
		}

		//public ICellularStation GetStation(MS_IndustryAccount oIndAcct) {//, MonitoredMessageList oErrorManager) {
		//    return GetStation(GetCellStationEnum(oIndAcct), oIndAcct);//, oErrorManager);
		//}

		//public ICellularStation GetStation(CellStationEnum eVendor, string szCsid) {//, MonitoredMessageList oErrorManager) {
		//    // Locals
		//    var oIndAcct = GetIndustryAccountByCsid(szCsid);

		//    // Get result
		//    return GetStation(eVendor, oIndAcct);//, oErrorManager);
		//}

		///// <summary>
		///// Given a Vendor Cellular steation and an MS_IndustryAccount it will returned the selected CellularStation
		///// </summary>
		///// <param name="eVendor">CellStationEnum</param>
		///// <param name="oIndAcct">MS_IndustryAccount</param>
		///// <param name="oErrorManager">MonitoredMessageList</param>
		///// <returns>ICellularStation</returns>
		//public ICellularStation GetStation(CellStationEnum eVendor, MS_IndustryAccount oIndAcct)//, MonitoredMessageList oErrorManager)
		//{
		//    // Locals
		//    ICellularStation oResult;
		//    CellStationEnum eAcctVendor;
		//    switch (oIndAcct.Account.PanelTypeId) {
		//        case MS_AccountPanelType.MetaData.LynxID:
		//        case MS_AccountPanelType.MetaData.VistaID:
		//            eAcctVendor = CellStationEnum.AlarmNet;
		//            break;
		//        case MS_AccountPanelType.MetaData.SimonIIIID:
		//        case MS_AccountPanelType.MetaData.ConcordID:
		//            eAcctVendor = CellStationEnum.AlarmCom;
		//            break;
		//        default:
		//            eAcctVendor = CellStationEnum.Inconclusive;
		//            break;
		//    }

		//    // Check that there is a difference
		//    if (eVendor != eAcctVendor) {
		//        //oErrorManager.AddWarningMessage("Mismatch of Cellular Vendor"
		//        //    , string.Format("There was a mismatch request was '{0}' Csid Receiver is '{1}'", eVendor, eAcctVendor));
		//    }

		//    switch (eVendor) {
		//        //case CellStationEnum.AlarmNet:
		//        //    oResult = new AlarmNet.AlarmNetStation(oIndAcct.Account);
		//        //    break;
		//        case CellStationEnum.AlarmCom:
		//            oResult = new AlarmCom.AlarmComStation(oIndAcct.Account);
		//            break;
		//        default:
		//            oResult = null;
		//            break;
		//    }

		//    // Return result
		//    return oResult;
		//}

		//public CellStationEnum GetCellStationEnum(MS_Account aMsAccount) {
		//    // Check that there is a primary csid
		//    return aMsAccount.IndustryAccount == null ? CellStationEnum.Inconclusive : GetCellStationEnum(aMsAccount.IndustryAccount);
		//}

		//public CellStationEnum GetCellStationEnum(string szCsid) {
		//    // Locals
		//    var oIndAcct = GetIndustryAccountByCsid(szCsid);

		//    // Check that there is an MS_IndustryAccount object and return
		//    return oIndAcct == null ? CellStationEnum.Inconclusive : GetCellStationEnum(oIndAcct);
		//}

		public CellStationEnum GetCellStationEnum(MS_IndustryAccount industryAccount)
		{
			// Locals
			CellStationEnum oTemp;

			// Look at the receiverline
			//@TODO: ReceiverLine VendorName - something like "industryAccount.ReceiverLine.OSCellVendor.MCellVendor.VendorName"
			switch ("Alarm.Com")
			{
				case "Alarmnet":
					oTemp = CellStationEnum.AlarmNet;
					break;
				case "Alarm.Com":
					oTemp = CellStationEnum.AlarmCom;
					break;
				case "Tellular":
					oTemp = CellStationEnum.Tellular;
					break;
				default:
					oTemp = CellStationEnum.Inconclusive;
					break;
			}

			// Check that the stations are the same, if not return inconclusive.
			return oTemp == GetCellStationEnumByPanelType(industryAccount) ? oTemp : CellStationEnum.Inconclusive;
		}

		#endregion Public

		#region Private

		//private static MS_IndustryAccount GetIndustryAccountByCsid(string szCsid) {
		//    // Locals
		//    var oIndAcct = SosCrmDataContext.Instance.MS_IndustryAccounts.GetByCSID(szCsid);
		//
		//    // Check that it's loaded and return result
		//    return oIndAcct.IsLoaded ? oIndAcct : null;
		//}

		private static CellStationEnum GetCellStationEnumByPanelType(MS_IndustryAccount oIndAcct)
		{
			switch (oIndAcct.Account.PanelTypeId)
			{
				case MS_AccountPanelType.MetaData.LynxID:
				case MS_AccountPanelType.MetaData.VistaID:
					return CellStationEnum.AlarmNet;
				case MS_AccountPanelType.MetaData.SimonID:
				case MS_AccountPanelType.MetaData.ConcordID:
					return CellStationEnum.AlarmCom;
				default:
					return CellStationEnum.Inconclusive;
			}
		}

		private static CellStationEnum GetCellStationEnum(string panelTypeId)
		{
			switch (panelTypeId)
			{
				case MS_AccountPanelType.MetaData.LynxID:
				case MS_AccountPanelType.MetaData.VistaID:
					return CellStationEnum.AlarmNet;
				case MS_AccountPanelType.MetaData.SimonID:
				case MS_AccountPanelType.MetaData.ConcordID:
					return CellStationEnum.AlarmCom;
				default:
					return CellStationEnum.Inconclusive;
			}
		}

		#endregion Private

		#endregion Methods
	}
}
