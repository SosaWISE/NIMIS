/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/17/12
 * Time: 08:43
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Contracts
{
	public interface IWiseCrmService : IFunctionalService
	{
		IFnsResult<IFnsWiseCrmDealerUserModel> AuthenticateDealerUser(long lSessionId, long lDealerId, string szUsername, string szPassword);

		IFnsResult<IFnsLeadModel> CreateLeadBasic(int nDealerId, string szFirstName, string szLastName, string szAddress, string szCity,
						string szState, string szPostal, string szEmail, string szPremisePhone, string szMessage, int nLeadSourceId = 10, int nLeadDispositionId = 1);

		IFnsResult<List<IFnsLeadSearchResult>> QlSearch(string szFirstName, string szLastName, string szPhone, int? nDealerId, string szEmail, int? nLeadId, int? nDispositionId, int? nSourceId, int nPageSize, int nPageNumber);

		IFnsResult<IFnsLeadFullDataModel> QlGetLeadFull(long lLeadId, long lCustomerMasterFileId, int nDealerId, string szUserId, bool bNoteRecord = false);

		IFnsResult<IFnsLeadFullDataModel> QlCreateUpdateLeadFull(IFnsLeadFullDataModel oFnsLeadFullDataModel, string szUserId);

		IFnsResult<List<IFnsMcAccountNotesFull>> FindAccountNotesByAnyID(long? lLeadId, long? lCustomerId, long? lCMFId);

		IFnsResult<IFnsMcAccountNotesFull> McAccountNoteCreate(IFnsMcAccountNoteModel oFnsNote, string szUserId);

		IFnsResult<IFnsOptionItem> DispositionListAddByDealerId(int nDealerId, string szItemName, string szUserId);

		IFnsResult<IFnsOptionItem> SourceListAddByDealerId(int nDealerId, string szItemName, string szUserId);

		IFnsResult<List<IFnsOptionItem>> DispositionListGetByDealerId(int nDealerId);

		IFnsResult<List<IFnsOptionItem>> SourceListGetByDealerId(int nDealerId);

		IFnsResult<List<IFnsOptionItem>> AppointmentMetaDataListGet(string szListName);

		//IFnsResult<IFnsAePaymentFull> AeCustomerCreateFromLead(IFnsAePaymentInformationCreateAccountModel oFnsPaymentInfo, string szUserId);

		IFnsResult<List<IFnsMcDealerUser>> DealerUsersGet(int nDealerId);

		IFnsResult<bool> LeadDispositionUpdate(int nDealerId, long nLeadID, int nLeadDispositionId, string szUserId);

		IFnsResult<List<IFnsCaAppointmentModel>> DealerUserAppointmentsGet(int nDealerUserId, DateTime dStartDate, DateTime dEndDate);

		IFnsResult<IFnsCustomerFullDataModel> AeCustomerRead(long lCustomerId, bool bNoteAccount = false);

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> AeCustomerUpdate(IFnsAeCustomerGpsClientsViewModel customerInfo, string szUserId = "SYSTEM");

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> AeCustomerAuthenticate(string sUsername, string sPassword);

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerSignup(IFnsAeCustomerGpsClientsViewModel customerInfoArg);

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerUpdate(IFnsAeCustomerGpsClientsViewModel customerInfoArg);

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerRead(long lCustomerID);

		IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerDelete(long lCustomerID);

		IFnsResult<IFnsQlDealerLeadModel> DealerLeadCreateUpdate(IFnsQlDealerLeadModel oDealerLead, string szUserId = "SYSTEM");

		IFnsResult<List<IFnsMsAccountClientsView>> GetDevicesByCMFID(long? lCMFID, string szUserId = "SYSTEM");

		IFnsResult<List<IFnsMsAccountClientsView>> GetDevicesByCustomerID(long? lCustomerID, string szUserId = "SYSTEM");

		IFnsResult<IFnsMsAccountClientDetailsView> GetDeviceDetailsByAccountID(long? lAccountID, long? lCustomerID, string szUserID = "SYSTEM");

		IFnsResult<IFnsMsAccountClientsView> UpdateDevice(long lAccountID, string sAccountName, string sAccountDesc);

		IFnsResult<IFnsVerifyAddress> AddressRead(long addressId);

		IFnsResult<IFnsVerifyAddress> AddressDelete(long addressId);

		IFnsResult<IFnsVerifyAddress> AddressUpdate(IFnsVerifyAddress address, string userId);

		IFnsResult<IFnsVerifyAddress> AddressCreate(IFnsVerifyAddress address, string userId);

		IFnsResult<IFnsVerifyAddress> SaveAddress(IFnsVerifyAddress ifnsAddress, string userId);
		IFnsResult<IFnsVerifyAddress> AddressVerify(IFnsVerifyAddress address, int seasonId, int teamLocationId, string salesRepId, string userId);

		IFnsResult<IFnsQlLead> ReadLead(long leadID);
		IFnsResult<IFnsQlLead> SaveLead(IFnsQlLead fnsLead, string userId, bool createMasterLead);
		IFnsResult<IFnsQlCreditReport> RunCredit(long leadID, bool bypass, string userId);

		IFnsResult<List<IFnsAeCustomerCardInfo>> MasterFileCustomers(long cmfid);
		IFnsResult<bool> MasterFileHasCustomer(long cmfid);
	}

}