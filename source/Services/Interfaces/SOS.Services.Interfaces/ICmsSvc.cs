/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 02/22/12
 * Time: 11:24
 * 
 * Description:  Describes for the services interfaces will be.
 *********************************************************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.CmsModels;

namespace SOS.Services.Interfaces
{
	[ServiceContract(Namespace = "SosServices")]
	public interface ICmsSvc
	{
		[OperationContract]
		[WebGet(UriTemplate = "McAddressTypeGetAll", ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAddressType>> McAddressTypeGetAll();

		[OperationContract]
		[WebGet(UriTemplate = "McAddressStatusGetAll", ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAddressStatus>> McAddressStatusGetAll();

		[OperationContract]
		[WebGet(UriTemplate = "McAddressValidationVendorGetAll", ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAddressValidationVendors>> McAddressValidationVendorGetAll();

		[OperationContract]
		[WebGet(UriTemplate = "McAddressGetByPK?lAddressID={lAddressID}", ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAddress>> McAddressGetByPK(long lAddressID);

		[OperationContract]
		[WebGet(UriTemplate = "McAddressUpdate?lAddressID={lAddressID}&szValidationVendorId={szValidationVendorId}" +
			"&szAddressStatusId={szAddressStatusId}&szStateId={szStateId}&szCountryId={szCountryId}" +
			"&nTimeZoneId={nTimeZoneId}&cAddressTypeId={cAddressTypeId}&szStreetAddress={szStreetAddress}" +
			"&szStreetAddress2={szStreetAddress2}&szStreetNumber={szStreetNumber}&szStreetName={szStreetName}" +
			"&szStreetType={szStreetType}&szPreDirectional={szPreDirectional}&szPostDirectional={szPostDirectional}" +
			"&szExtension={szExtension}&szExtensionNumber={szExtensionNumber}&szCounty={szCounty}" +
			"&szCountyCode={szCountyCode}&szUrbanization={szUrbanization}&szUrbanizationCode={szUrbanizationCode}" +
			"&szCity={szCity}&szPostalCode={szPostalCode}&szPlusFour={szPlusFour}&szDeliveryPoint={szDeliveryPoint}" +
			"&fLatitude={fLatitude}&fLongitude={fLongitude}&nCongressionalDistric={nCongressionalDistric}&bDPV={bDPV}" +
			"&szDPVResponse={szDPVResponse}&szDPVFootNote={szDPVFootNote}&szCarrierRoute={szCarrierRoute}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAddress>> McAddressUpdate(long lAddressID, string szValidationVendorId, string szAddressStatusId
			, string szStateId, string szCountryId, int nTimeZoneId, char cAddressTypeId, string szStreetAddress
			, string szStreetAddress2, string szStreetNumber, string szStreetName, string szStreetType, string szPreDirectional
			, string szPostDirectional, string szExtension, string szExtensionNumber, string szCounty, string szCountyCode
			, string szUrbanization, string szUrbanizationCode, string szCity, string szPostalCode, string szPlusFour
			, string szDeliveryPoint, float fLatitude, float fLongitude, int nCongressionalDistric, bool bDPV
			, string szDPVResponse, string szDPVFootNote, string szCarrierRoute);

		[OperationContract]
		[WebGet(UriTemplate = "QlLeadBasicCreate?nDealerId={nDealerId}&szFirstName={szFirstName}&szLastName={szLastName}&szAddress={szAddress}" +
								"&szCity={szCity}&szState={szState}&szPostal={szPostal}&szEmail={szEmail}" +
								"&szPremisePhone={szPremisePhone}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<QlLeadBasicView> QlLeadBasicCreate(int nDealerId, string szFirstName, string szLastName, string szAddress
			, string szCity, string szState, string szPostal, string szEmail, string szPremisePhone);

		[OperationContract]
		[WebGet(UriTemplate = "QlSearch?sFirstName={szFirstName}&sLastName={szLastName}&sPhone={szPhone}" +
			"&nDealerId={nDealerId}&sEmail={szEmail}&nLeadId={nLeadId}&nDispositionId={nDispositionId}" +
			"&nSourceId={nSourceId}&nPageSize={nPageSize}&nPageNumber={nPageNumber}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<QlLeadSearchResultView>> QlSearch(string szFirstName, string szLastName, string szPhone, int? nDealerId, string szEmail,
					int? nLeadId, int? nDispositionId, int? nSourceId, int nPageSize, int nPageNumber);

		[OperationContract]
		[WebGet(UriTemplate = "QlGetLeadFull?lLeadId={lLeadId}&lCustomerMasterFileId={lCustomerMasterFileId}&bNoteAccount={bNoteAccount}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<QlLeadFullData> QlGetLeadFull(long lLeadId, long lCustomerMasterFileId, bool bNoteAccount = false);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "QlLeadCreateUpdate", ResponseFormat = WebMessageFormat.Json)]
		SosResult<QlLeadFullData> QlLeadCreateUpdate(QlLeadFullData oLeadFullData);

		[OperationContract]
		[WebGet(UriTemplate = "QlGetAccountNotesByID?lLeadId={lLeadId}&lCustomerId={lCustomerId}&lCMFId={lCMFId}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McAccountNotesFullInfoView>> QlGetAccountNotesByID(long? lLeadId, long? lCustomerId,
									long? lCMFId);

		[OperationContract]
		[WebGet(UriTemplate = "OptionItemsGet?szListName={szListName}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<OptionItemModel>> OptionItemsGet(string szListName);

		[OperationContract]
		[WebGet(UriTemplate = "OptionItemAdd?szListName={szListName}&szItemName={szItemName}"
			, ResponseFormat = WebMessageFormat.Json)]
		SosResult<OptionItemModel> OptionItemAdd(string szListName, string szItemName);
		
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "McAccountNotesCreate", ResponseFormat = WebMessageFormat.Json)]
		SosResult<McAccountNotesFullInfoView> McAccountNotesCreate(McAccountNote oNote);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "AeCreateNewCustomer", ResponseFormat = WebMessageFormat.Json)]
		SosResult<AeModels.Invoice> AeCreateNewCustomer(AeModels.AePaymentInformationCreateAccountModel oPaymentInformationCreateAccountModel);

		[OperationContract]
		[WebGet(UriTemplate = "DealersGet", ResponseFormat = WebMessageFormat.Json)]
		SosResult<List<McModels.DealerUser>> DealerUsersGet();

		[OperationContract]
		[WebGet(UriTemplate = "LeadDispositionUpdate?nLeadID={nLeadID}&nLeadDispositionId={nLeadDispositionId}", ResponseFormat = WebMessageFormat.Json)]
		SosResult<bool> LeadDispositionUpdate(int nLeadID, int nLeadDispositionId);

		[OperationContract]
		[WebGet(UriTemplate = "DealerUserAppointmentsGet?start={sStart}&end={sEnd}", ResponseFormat = WebMessageFormat.Json)]
		List<CaFullCalenderAppointmentModel> DealerUserAppointmentsGet(string sStart, string sEnd);

		[OperationContract]
		[WebGet(UriTemplate = "AeGetCustomerFull?lCustomerId={lCustomerId}&lCustomerMasterFileId={lCustomerMasterFileId}&bNoteAccount={bNoteAccount}", ResponseFormat = WebMessageFormat.Json)]
		List<CaFullCalenderAppointmentModel> AeGetCustomerFull(long lCustomerId, long lCustomerMasterFileId, bool bNoteAccount = false);

	}
}
