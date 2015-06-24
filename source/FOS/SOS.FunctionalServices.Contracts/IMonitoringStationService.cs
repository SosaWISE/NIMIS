/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/03/12
 * Time: 09:53
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/

using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using System;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.FunctionalServices.Contracts.Models.Reporting;

namespace SOS.FunctionalServices.Contracts
{
	public interface IMonitoringStationService : IFunctionalService
	{
		IFnsResult<IFnsAGResponseBase> CreateMobileAccount(string szTemplateTransmitter, string szStartTransmitter, string szEndTransmitter);

		IFnsResult<IFnsAGResponseSignalBase> SendEmergencySignal(string sTransmitterCode, string sText
			, decimal? mLongitude, decimal? mLatitude, bool? bTestSignalFlag);

		IFnsResult<IFnsAGResponseSignalBase> DispatchFromLaipacDeviceToAGCentralStation(string eventCodeId, decimal lattitude,
		                                                                                decimal longitude,
		                                                                                string dispatchMessage, string csid,
		                                                                                bool? bTestSignalFlag);

		IFnsResult<IFnsAGResponseSignalBase> DispatchFromSSEDeviceToAGCentralStation(string eventCodeId, decimal lattitude,
		                                                                             decimal longitude, string dispatchMessage,
		                                                                             string csid, bool? bTestSignalFlag);

		IFnsResult<IFnsMsEmergencyContact> EmergencyContactCreate(IFnsMsEmergencyContact fnsMsEmergencyContact, string gpEmployeeId);
        IFnsResult<IFnsMsEmergencyContact> EmergencyContactUpdate(IFnsMsEmergencyContact fnsMsEmergencyContact, string gpEmployeeId);
        IFnsResult<IFnsMsEmergencyContact> EmergencyContactRead(long id);
		IFnsResult<IFnsMsEmergencyContact> EmergencyContactDelete(long id, string gpEmployeeId);
		IFnsResult<List<IFnsMsEmergencyContactPhoneType>> EmergencyContactPhoneTypesGet(string gpEmployeeId);
		IFnsResult<List<IFnsMsEmergencyContactPhoneType>> EmergencyContactPhoneTypesGet(long accountId, string gpEmployeeId);
		IFnsResult<List<IFnsMsEmergencyContactRelationship>> EmergencyContactRelationShipsGet(long accountId, string gpEmployeeId);

	    IFnsResult<List<IFnsMsEmergencyContactAuthority>> EmergencyContactAuthoritiesGet(long accountId,
	        string gpEmployeeId);
        IFnsResult<List<IFnsMsEmergencyContactType>> EmergencyContactTypesGet(long accountId,
            string gpEmployeeId);
        IFnsResult<List<IFnsMsEmergencyContact>> EmergencyContactGetByAccountId(long accountId, string gpEmployeeId);
		
		IFnsResult<IFnsMsAccount> SystemDetailsGet(long accountId, string gpEmployeeID);
		IFnsResult<IFnsMsAccount> SystemDetailsSave(IFnsMsAccount fnsMsAccount, string gpEmployeeID);

		IFnsResult<object> AccountDetails(long accountId);

        IFnsResult<object> AccountValidate(long accountId);

		//IFnsResult<IFnsMsAccountLeadInfo> MsAccountCreate(long leadId, string gpEmployeeId);
		IFnsResult<IFnsMsAccountLeadInfo> CreateMasterFileAccount(long cmfid, string gpEmployeeId);

		#region Industry Accounts

        IFnsResult<IFnsMsIndustryAccount> MsIndustryNumberGenerate(long accountId, bool isPrimary, string gpEmployeeId);
		IFnsResult<List<IFnsMsIndustryAccountNumbersWithReceiverLineInfoView>> MsIndustryNumberWithReceiverLineGet(
			long accountId, string gpEmployeeId);

		IFnsResult<bool> MsIndustryNumberSetAsPrimary(long industryAccountId, string gpEmployeeId);
		IFnsResult<bool> MsIndustryNumberSetAsSecondary(long industryAccountId, string gpEmployeeId);
		IFnsResult<List<IFnsMsIndustryAccount>> MsIndustryNumbersGet(long accountId, string gpEmployeeId);
		
		#endregion Industry Accounts

		#region MsAccount Meta Data

		IFnsResult<List<IFnsMsAccountDslSeizureType>> DslSeizureTypesGet(int userID);
		IFnsResult<List<IFnsMsAccountEventZoneEventTypes>> ZoneEventTypesGet(string msoid, int equipmentTypeId, string gpEmployeeId);
		IFnsResult<List<IFnsMsEquipmentLocation>> EquipmentLocationsGet(string msoid, string gpEmployeeId);
		IFnsResult<List<IFnsMsAccountZoneType>> ZoneTypesGet(string msoid, string gpEmployeeId);
		IFnsResult<List<IFnsMsEquipmentsView>> MsEquipmentsGet(string gpEmployeeId);
		IFnsResult<List<IFnsMsEquipmentsView>> MsEquipmentExistingsGet(string gpEmployeeId);



		#endregion MsAccount Meta Data

		#region Premise Address
		IFnsResult<IFnsMcAddressView> GetPremiseAddress(long accountId, string gpEmployeeId);
		#endregion Premise Address

		#region Account Equipment

		IFnsResult<IFnsMsAccountEquipmentsView> EquipmentUpdate(IFnsMsAccountEquipmentsView equipment, string gpEmployeeID);
		IFnsResult<bool> EquipmentDelete(long accountEquipmentID, string gpEmployeeID);
		IFnsResult<List<IFnsMsAccountEquipmentsView>> EquipmentByAccountId(long accountId);
		IFnsResult<object> EquipmentAccountZoneTypes(string equipmentId);
		IFnsResult<object> EquipmentAccountZoneTypeEvents(string equipmentId, int equipmentAccountZoneTypeId, string monitoringStationOSId);
		IFnsResult<object> EquipmentByEquipmentID(string equipmentID);
		IFnsResult<object> EquipmentByPartNumber(string partNumber);
		IFnsResult<object> EquipmentByBarcode(string barcode);

		#endregion Account Equipment

		#region Submit Account to Central Station
		IFnsResult<IFnsMsAccountSubmit> SubmitOnline(long accountId, string gpEmployeeId);
		#endregion Submit Account to Central Station

		#region Signal History
		IFnsResult<List<IFnsSignalHistoryItemModel>> GetSignalHistory(long accountId, DateTime startDate, DateTime endDate, string gpEmployeeId);

		#endregion //Signal History

		IFnsResult<object> TwoWayTestData(long accountId);
		IFnsResult<object> InitTwoWayTest(long accountId, string gpEmployeeId);
		IFnsResult<object> CompleteTwoWayTest(long accountId, string confirmedBy, string gpEmployeeId);
		IFnsResult<object> ActiveTests(long accountId);
		IFnsResult<bool> ClearActiveTests(long accountId);
		IFnsResult<bool> ClearTest(long accountId, int testNum);
		IFnsResult<IFnsMsSystemStatusInfo> ServiceStatus(long accountId, string gpEmployeeId);
		IFnsResult<IFnsMsSystemStatusInfo> SetServiceStatus(long accountId, string oosCat, DateTime startDate, string comment, string gpEmployeeId);

		#region GetTechDetails

		IFnsResult<IFnsSalesRepInfo> GetTechDetails(long accountId, string gpEmployeeId);

		#endregion GetTechDetails

		#region GetSalesInformation
		IFnsResult<IFnsMsAccountSalesInformation> SalesInformationRead(long? accountId, string gpEmployeeId);
		#endregion GetSalesInformation

		#region Dispatch Agencies

		IFnsResult<List<IFnsMsDispatchAgencyView>> GetDispatchAgencies(string city, string state, string zip, string gpEmployeeId);

		IFnsResult<List<IFnsMsDispatchAgencyView>> GetDispatchAgenciesByAgencyTypeId(string city, string state, string zip, int agencyTypeId, string gpEmployeeId);

		IFnsResult<List<IFnsMsDispatchAgencyType>> GetDispatchAgencyTypes(string monitoringStationsOSId, string gpEmployeeId);

		#endregion Dispatch Agencies

		#region Dispatch Agency Assignments

		IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> SaveDaAssignmentsList(long accountId, List<int> list, string gpEmployeeId);
		IFnsResult<IFnsMsAccountDispatchAgencyAssignmentView> SaveDaAssignments(long accountDispatchAgencyAssignmentId, IFnsMsAccountDispatchAgencyAssignmentView dispatchAgency, string gpEmployeeId);
		IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> ReadDaAssignments(long accountId, string gpEmployeeId);
		IFnsResult<List<IFnsMsAccountDispatchAgencyAssignmentView>> DeleteDaAssignments(int dispatchAgencyAssignmentID, string gpEmployeeId);

		#endregion Dispatch Agency Assignments

		#region Slammed Accounts
		IFnsResult<IFnsMsLeadTakeOver> SlammedAccountsCheck(long? accountId, string gpEmployeeId);
		#endregion Slammed Accounts

		#region SetupCheckLists
		IFnsResult<IFnsMsAccountSetupCheckList> GetMsAccountSetupCheckList(long accountID, string gpEmployeeID);
		#endregion SetupCheckLists
	}
}