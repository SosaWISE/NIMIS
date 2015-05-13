/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/17/12
 * Time: 08:42
 * 
 * Description:  Service that manages the CRM information.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.AuthenticationControl;
using SOS.Data.HumanResource;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.CentralStation;
using SOS.FunctionalServices.Models.Cms;
using SOS.FunctionalServices.Models.QualifyLead;
using SOS.FunctionalServices.Models.Receiver;
using SOS.Lib.Core.CreditReportService;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using WSLead = NSE.FOS.RunCreditServices.Models.WSLead;
using WSAddress = NSE.FOS.RunCreditServices.Models.WSAddress;
using WSSeason = NSE.FOS.RunCreditServices.Models.WSSeason;
using SOS.FunctionalServices.Helpers;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Models.AccountingEngine;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class WiseCrmService : IWiseCrmService
	{
		#region Implementation of IWiseCrmService

		public IFnsResult<IFnsWiseCrmDealerUserModel> AuthenticateDealerUser(long lSessionId, long lDealerId, string szUsername, string szPassword)
		{
			/** Initialize. */
			IFnsResult<IFnsWiseCrmDealerUserModel> oResult;

			try
			{
				AC_UsersDealerUsersAuthenticateView oAuthDealerUser = SosAuthControlDataContext.Instance.AC_UsersDealerUsersAuthenticateViews.Authenticate(lSessionId, lDealerId, szUsername, szPassword);
				/*AC_Authentication oAuth = */
				SosAuthControlDataContext.Instance.AC_Authentications.SaveEvent(
					oAuthDealerUser.SessionID, oAuthDealerUser.UserID, szUsername, szPassword);
				IFnsWiseCrmDealerUserModel oAuthResult = new FnsWiseCrmDealerUserModel(oAuthDealerUser);

				oResult = new FnsResult<IFnsWiseCrmDealerUserModel>
					{
						Code = (int)ErrorCodes.Success,
						Message = "Success",
						Value = oAuthResult
					};
			}
			catch (Exception oEx)
			{
				/** Look for key words. */
				var eCode = ErrorCodes.LoginFailure;

				if (oEx.Message.IndexOf("EXP SESSION", StringComparison.Ordinal) > -1)
					eCode = ErrorCodes.SessionExp;

				/** Init Return result. */
				oResult = new FnsResult<IFnsWiseCrmDealerUserModel>
					{
						Code = (int)eCode,
						Message = string.Format("{0} | {1}", ErrorCodes.LoginFailure.Message(), oEx.Message)
					};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsLeadModel> CreateLeadBasic(int nDealerId, string szFirstName, string szLastName, string szAddress, string szCity, string szState, string szPostal, string szEmail, string szPremisePhone, string szMessage, int nLeadSourceId = 10, int nLeadDispositionId = 1)
		{
			/** Initialize. */
			IFnsResult<IFnsLeadModel> oResult;

			try
			{
				/** Execute. */
				QL_LeadBasicInfoView oView = SosCrmDataContext.Instance.QL_LeadBasicInfoViews.CreateLeadBasicInfo(nDealerId
					, null, szFirstName, null, szLastName, null, szEmail, szAddress, szCity, szState, szPostal, szPremisePhone, nLeadSourceId, nLeadDispositionId);
				var oLeadModel = new FnsLeadModel(oView);

				/** Note the lead. */
				McAccountNoteCreate(new FnsMcAccountNoteModel
					{
						NoteTypeID = "STANDARD",
						NoteCategory1ID = MC_AccountNoteCat1.LeadGeneration.ID,
						NoteCategory2ID = MC_AccountNoteCat1.GetCat2IdFromSourceId(nLeadSourceId),
						CustomerMasterFileId = oView.CustomerMasterFileID,
						LeadId = oView.LeadID,
						Note = string.IsNullOrWhiteSpace(szMessage)
						  ? "Lead was created with CreateLeadBasie method."
						  : string.Format("Lead was created with CreateLeadBasie method with message: <br /><blockquote>{0}</blockquote>", szMessage)
					}, "SYSTEM");


				/** Init Result. */
				oResult = new FnsResult<IFnsLeadModel>
					{
						Code = (int)ErrorCodes.Success,
						Message = "Success",
						Value = oLeadModel
					};
			}
			catch (SqlException oEx)
			{
				/** Init return result. */
				oResult = new FnsResult<IFnsLeadModel>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oEx.Message
				};
			}
			catch (Exception oEx)
			{
				/** Init return result. */
				oResult = new FnsResult<IFnsLeadModel>
					{
						Code = (int)ErrorCodes.UnexpectedException,
						Message = oEx.Message
					};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsLeadSearchResult>> QlSearch(string szFirstName, string szLastName, string szPhone, int? nDealerId, string szEmail, int? nLeadId, int? nDispositionId, int? nSourceId, int nPageSize, int nPageNumber)
		{
			/** Initialize. */
			var oResult = new FnsResult<List<IFnsLeadSearchResult>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing QlSearch"
			};

			/** Execute find. */
			try
			{
				QL_LeadSearchResultViewCollection oQryResultCol = SosCrmDataContext.Instance.QL_LeadSearchResultViews.Search(szFirstName
					, szLastName, szPhone, nDealerId, szEmail, nLeadId, nDispositionId, nSourceId, nPageSize, nPageNumber);
				List<FnsLeadSearchResult> oList = oQryResultCol.Select(oItem => new FnsLeadSearchResult(oItem)).ToList();

				/** Save Successfull result. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsLeadSearchResult>(oList);
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsLeadSearchResult>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsLeadFullDataModel> QlGetLeadFull(long lLeadId, long lCustomerMasterFileId, int nDealerId, string szUserId, bool bNoteRecord = false)
		{
			return QlGetLeadFullPrivate(lLeadId, lCustomerMasterFileId, nDealerId, szUserId, bNoteRecord);
		}

		private IFnsResult<IFnsLeadFullDataModel> QlGetLeadFullPrivate(long lLeadId, long lCustomerMasterFileId, int nDealerId, string szUserId, bool bNoteRecord = false)
		{
			/** Initialize. */
			var oResult = new FnsResult<IFnsLeadFullDataModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing QlGetLeadFull"
			};

			/** Execute find. */
			try
			{
				QL_Lead oItemResult = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKeyAndCMFId(lLeadId, lCustomerMasterFileId, nDealerId);

				if (!oItemResult.IsLoaded)
				{
					oResult.Message =
						string.Format("Sorry, the data LeadId: {0}, CMFID: {1}, and DealerId: {2} does not make for a hit."
							, lLeadId, lCustomerMasterFileId, nDealerId);
					return oResult;
				}
				var oModel = new FnsLeadFullDataModel(oItemResult);

				/** Check to see if there are any product offers. */
				QL_LeadProductOffersViewCollection offerCollection = SosCrmDataContext.Instance.QL_LeadProductOffersViews.LoadByLeadId(oItemResult.LeadID);
				var oProdList = new List<FosLeadProductOffer>();
				if (offerCollection.Count > 0)
				{
					oProdList.AddRange(offerCollection.Select(oOffer => new FosLeadProductOffer(oOffer)));
					oModel.ProductSkwIdList = new List<IFosLeadProductOffer>(oProdList);
				}

				/** Note the account that it was created. */
				if (bNoteRecord)
				{
					McAccountNoteCreate(new FnsMcAccountNoteModel
						{
							NoteTypeID = "STANDARD",
							NoteCategory1ID = 1,
							NoteCategory2ID = 1,
							CustomerMasterFileId = oModel.CustomerMasterFileId,
							LeadId = oModel.LeadID,
							Note = "Lead was accessed."
						}, szUserId);
				}

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oModel;

			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsLeadFullDataModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsLeadFullDataModel> QlCreateUpdateLeadFull(IFnsLeadFullDataModel oFnsLeadFullDataModel, string szUserId)
		{
			/** Initialize. */
			var oResult = new FnsResult<IFnsLeadFullDataModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing QlCreateUpdateLeadFull"
			};

			/** Execute CreateUpdate. */
			try
			{
				// ** Save Address
				QL_Address oAddress;
				Helper.BindToQlAddress(out oAddress, oFnsLeadFullDataModel.Address);
				oAddress.Save(szUserId);
				oFnsLeadFullDataModel.Address.AddressID = oAddress.AddressID;

				// ** Save Lead
				QL_Lead oLead;
				Helper.BindToQlLead(out oLead, oFnsLeadFullDataModel, szUserId);
				oLead.Save(szUserId);

				// ** Update product list.
				foreach (IFosLeadProductOffer leadProductOffer in oFnsLeadFullDataModel.ProductSkwIdList)
				{
					if (leadProductOffer.LeadProductOfferedId == 0) // Only save new ones.
					{
						/** Initialize. */
						leadProductOffer.LeadId = oLead.LeadID;
						QL_LeadProductOffer oOffer;
						Helper.BindToQlLeadProductOffer(out oOffer, leadProductOffer, szUserId);
						oOffer.Save(szUserId);
					}
				}

				/** Note the account that it was created. */
				McAccountNoteCreate(new FnsMcAccountNoteModel
					{
						NoteTypeID = "STANDARD",
						NoteCategory1ID = 1,
						NoteCategory2ID = 5,
						CustomerMasterFileId = oLead.CustomerMasterFileId,
						LeadId = oLead.LeadID,
						Note = "Lead was created."
					}, szUserId);

				// ** Retrieve from database and return result.
				var oValueResult = (FnsResult<IFnsLeadFullDataModel>)QlGetLeadFull(oLead.LeadID, oLead.CustomerMasterFileId, oLead.DealerId, szUserId);
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oValueResult.Value;
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsLeadFullDataModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsIndustryAccountModel> FindIndustryAccountByCallerId(string szPhone)
		{
			/** Initialize. */
			IFnsResult<IFnsIndustryAccountModel> oResult;

			/** Execute find. */
			try
			{
				var oIndAcct = new MS_IndustryAccount();

				oResult = new FnsResult<IFnsIndustryAccountModel>
					{
						Code = (int)ErrorCodes.Success,
						Message = "Success",
						Value = new FnsIndustryAccountModel(oIndAcct)
					};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsIndustryAccountModel>
					{
						Code = (int)ErrorCodes.UnexpectedException,
						Message = oEx.Message
					};
			}

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsMcAccountNotesFull>> FindAccountNotesByAnyID(long? lLeadId, long? lCustomerId, long? lCMFId)
		{
			/** Initialize. */
			var oResult = new FnsResult<List<IFnsMcAccountNotesFull>>
							{
								Code = (int)ErrorCodes.GeneralMessage,
								Message = "Initializing FindAccountNotesByAnyID"
							};

			/** Validate. */
			if (lLeadId == null && lCustomerId == null && lCMFId == null)
			{
				return new FnsResult<List<IFnsMcAccountNotesFull>>
						{
							Code = (int)ErrorCodes.GeneralMessage,
							Message = "All arguments passed are null"
						};
			}

			/** Execute. */
			try
			{
				/** Initialize. */
				MC_AccountNotesAllInfoViewCollection oQryResultCol;
				// ** Check to see which id to use
				if (lLeadId != null)
				{
					oQryResultCol = SosCrmDataContext.Instance.MC_AccountNotesAllInfoViews.GetByLeadId(lLeadId.Value);
				}
				else if (lCustomerId != null)
				{
					oQryResultCol = SosCrmDataContext.Instance.MC_AccountNotesAllInfoViews.GetByCustomerId(lCustomerId.Value);
				}
				else
				{
					oQryResultCol = SosCrmDataContext.Instance.MC_AccountNotesAllInfoViews.GetByCustomerMasterFileId(lCMFId.Value);
				}
				List<FnsMcAccountNotesFull> oList = oQryResultCol.Select(oItem => new FnsMcAccountNotesFull(oItem)).ToList();

				/** Return a successfull envilop. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsMcAccountNotesFull>(oList);

			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsMcAccountNotesFull>>
							{
								Code = (int)ErrorCodes.UnexpectedException,
								Message = oEx.Message
							};
			}
			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsMcAccountNotesFull> McAccountNoteCreate(IFnsMcAccountNoteModel oFnsNote, string szUserId)
		{
			/** Initialize. */
			var oResult = new FnsResult<IFnsMcAccountNotesFull>
							{
								Code = (int)ErrorCodes.GeneralMessage,
								Message = "Initializing McAccountNoteCreate"
							};

			/** Validate. */
			if (oFnsNote == null)
			{
				return new FnsResult<IFnsMcAccountNotesFull>
						{
							Code = (int)ErrorCodes.GeneralMessage,
							Message = "McAccountNoteCreate is missing oFnsNote argument."
						};
			}

			/** Execute. */
			try
			{
				/** Save note */
				var oNote = new MC_AccountNote
					{
						NoteTypeId = oFnsNote.NoteTypeID,
						CustomerMasterFileId = oFnsNote.CustomerMasterFileId,
						LeadId = oFnsNote.LeadId,
						NoteCategory1Id = oFnsNote.NoteCategory1ID,
						NoteCategory2Id = oFnsNote.NoteCategory2ID,
						Note = oFnsNote.Note
					};
				oNote.Save(szUserId);

				/** Bind to new result. */
				var oNoteResult = new FnsMcAccountNotesFull(SosCrmDataContext.Instance.MC_AccountNotesAllInfoViews.GetByNoteId(oNote.NoteID));
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oNoteResult;
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsMcAccountNotesFull>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsOptionItem> DispositionListAddByDealerId(int nDealerId, string szItemName, string szUserId)
		{
			/** Initialize. */
			var oResult = new FnsResult<IFnsOptionItem>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing DispositionListAddByDealerId"
			};

			#region Try
			try
			{
				QL_LeadDispositionCollection oColl = SosCrmDataContext.Instance.QL_LeadDispositions.GetByDealerId(nDealerId);
				/** Look for the item if it's there already. */
				foreach (var oItem in oColl)
				{
					if (oItem.LeadDisposition.Equals(szItemName))
					{
						return new FnsResult<IFnsOptionItem>
							{
								Code = (int)ErrorCodes.DuplicateItem,
								Message = string.Format("The new disposition name '{0}' is already in use.", oItem.LeadDisposition),
								Value = new FnsOptionItem(oItem)
							};
					}
				}

				/** Since not found we must create. */
				var oDispItem = new QL_LeadDisposition
					{
						DealerId = nDealerId,
						LeadDisposition = szItemName,
						IsActive = true,
						IsDeleted = false
					};
				oDispItem.Save(szUserId);

				var oResultItem = new FnsOptionItem(oDispItem);
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oResultItem;
			}
			#endregion Try
			#region Exceptions
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<IFnsOptionItem>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsOptionItem>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion Exceptions

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsOptionItem> SourceListAddByDealerId(int nDealerId, string szItemName, string szUserId)
		{
			/** Initialize. */
			var oResult = new FnsResult<IFnsOptionItem>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing SourceListAddByDealerId"
			};

			#region Try
			try
			{
				QL_LeadSourceCollection oColl = SosCrmDataContext.Instance.QL_LeadSources.GetByDealerId(nDealerId);
				/** Look for the item if it's there already. */
				foreach (var oItem in oColl)
				{
					if (oItem.LeadSource.Equals(szItemName))
					{
						return new FnsResult<IFnsOptionItem>
						{
							Code = (int)ErrorCodes.DuplicateItem,
							Message = string.Format("The new source name '{0}' is already in use.", oItem.LeadSource),
							Value = new FnsOptionItem(oItem)
						};
					}
				}

				/** Since not found we must create. */
				var oSourceItem = new QL_LeadSource
				{
					DealerId = nDealerId,
					LeadSource = szItemName,
					IsActive = true,
					IsDeleted = false
				};
				oSourceItem.Save(szUserId);

				var oResultItem = new FnsOptionItem(oSourceItem);
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oResultItem;
			}
			#endregion Try
			#region Exceptions
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<IFnsOptionItem>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsOptionItem>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion Exceptions

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsOptionItem>> DispositionListGetByDealerId(int nDealerId)
		{
			/** Initialize. */
			var oResultList = new List<FnsOptionItem>();
			var oResult = new FnsResult<List<IFnsOptionItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing DispositionListGetByDealerId"
			};

			#region Try
			try
			{
				QL_LeadDispositionCollection oColl = SosCrmDataContext.Instance.QL_LeadDispositions.GetByDealerId(nDealerId);
				oResultList.AddRange(oColl.Select(oItem => new FnsOptionItem(oItem)));

				/** Return successfull result. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsOptionItem>(oResultList);
			}
			#endregion Try
			#region Exceptions
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion Exceptions

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsOptionItem>> SourceListGetByDealerId(int nDealerId)
		{
			/** Initialize. */
			var oResultList = new List<FnsOptionItem>();
			var oResult = new FnsResult<List<IFnsOptionItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing SourceListGetByDealerId"
			};

			#region Try
			try
			{
				QL_LeadSourceCollection oColl = SosCrmDataContext.Instance.QL_LeadSources.GetByDealerId(nDealerId);
				oResultList.AddRange(oColl.Select(oItem => new FnsOptionItem(oItem)));

				/** Return successfull result. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsOptionItem>(oResultList);
			}
			#endregion Try
			#region Exceptions
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion Exceptions

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsOptionItem>> AppointmentMetaDataListGet(string szListName)
		{
			#region Initialize.
			/** Initialize. */
			var oResultList = new List<FnsOptionItem>();
			var oResult = new FnsResult<List<IFnsOptionItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AppointmentMetaDataListGet"
			};
			#endregion Initialize.

			#region Try
			try
			{
				switch (szListName)
				{
					case "AppointmentType":
						CA_AppointmentTypeCollection oAptColl = SosCrmDataContext.Instance.CA_AppointmentTypes.GetOptionsList();
						oResultList.AddRange(oAptColl.Select(oItem => new FnsOptionItem(oItem)));
						break;
					case "ReminderDelayType":
						CA_ReminderDelyTypeCollection oRemDelayColl = SosCrmDataContext.Instance.CA_ReminderDelyTypes.GetOptionsList();
						oResultList.AddRange(oRemDelayColl.Select(oItem => new FnsOptionItem(oItem)));
						break;
					case "ReminderMediaType":
						CA_ReminderMediaTypeCollection oRemMediaColl = SosCrmDataContext.Instance.CA_ReminderMediaTypes.GetOptionsList();
						oResultList.AddRange(oRemMediaColl.Select(oItem => new FnsOptionItem(oItem)));
						break;
					default:
						return new FnsResult<List<IFnsOptionItem>>
						{
							Code = (int)ErrorCodes.GeneralMessage,
							Message = string.Format("The List Name passed '{0}' has not been implemented in the Fns layer for the AppointmentMetaDataListGet."
								, szListName)
						};
				}

				/** Return successfull result. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsOptionItem>(oResultList);
			}
			#endregion Try
			#region Exceptions
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsOptionItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion Exceptions

			#region Return result

			/** Return result. */
			return oResult;

			#endregion Return result
		}

		//public IFnsResult<IFnsAePaymentFull> AeCustomerCreateFromLead(IFnsAePaymentInformationCreateAccountModel oFnsPaymentInfo, string szUserId)
		//{
		//	/** Initialize. */
		//	var oResult = new FnsResult<IFnsAePaymentFull>
		//					{
		//						Code = (int)ErrorCodes.GeneralMessage,
		//						Message = "Initializing McAccountNoteCreate"
		//					};

		//	/** Validate. */
		//	if (oFnsPaymentInfo == null)
		//	{
		//		return new FnsResult<IFnsAePaymentFull>
		//				{
		//					Code = (int)ErrorCodes.GeneralMessage,
		//					Message = "AeCustomerCreateFromLead is missing oFnsNote argument."
		//				};
		//	}

		//	/** Execute. */
		//	try
		//	{
		//		/** Create new Customer first. */
		//		AE_Customer oCustomer = SosCrmDataContext.Instance.AE_Customers.CreateFromLeadID(oFnsPaymentInfo.LeadId
		//			, AE_CustomerType.MetaData.PrimaryContractedCustomerID, AE_CustomerAddressType.MetaData.PrimaryCustomerAddressID);

		//		/** Check to see if there is a result. */
		//		if (oCustomer == null)
		//			return new FnsResult<IFnsAePaymentFull>
		//				{
		//					Code = (int)ErrorCodes.GeneralMessage,
		//					Message = string.Format("Error creating the Customer from the Lead.")
		//				};
		//		/** Note the account. */
		//		IFnsMcAccountNoteModel oFnsNote = new FnsMcAccountNoteModel
		//		{
		//			NoteTypeID = MC_AccountNoteType.MetaData.StandardNoteID,
		//			CustomerMasterFileId = oFnsPaymentInfo.CustomerMasterFileId,
		//			CustomerId = oCustomer.CustomerID,
		//			LeadId = oFnsPaymentInfo.LeadId,
		//			NoteCategory1ID = 4, // 3	Customer
		//			NoteCategory2ID = 7, // 6	Customer New From Lead
		//			Note = string.Format("New customer created 'ID: {0}-{1}'"
		//			  , oCustomer.CustomerID, oCustomer.CustomerMasterFileId)
		//		};
		//		McAccountNoteCreate(oFnsNote, szUserId);

		//		/** Create BillingAddress. */
		//		MC_Address oBillingAddress = oFnsPaymentInfo.BillingAddress.SameAsCustomer
		//			? SosCrmDataContext.Instance.MC_Addresses.CreateFromAddressID(oCustomer.AddressId)
		//			: ((FnsAePaymentInformationCreateAccountModel)oFnsPaymentInfo).BindToMcAddress(szUserId);

		//		/** Create the Billing Customer. */
		//		AE_Customer oCustBilling = oFnsPaymentInfo.BillingInfo.SameAsCustomer
		//				? SosCrmDataContext.Instance.AE_Customers.CreateFromCustomerID(oCustomer.CustomerID, AE_CustomerType.MetaData.BillingCustomerID, oBillingAddress.AddressID)
		//				: ((FnsAePaymentInformationCreateAccountModel)oFnsPaymentInfo).BindToAeCustomer(oBillingAddress.AddressID, szUserId);

		//		/** Create a Contract entity for this product purchase. */
		//		AE_ContractTemplate oContTemplate =
		//			SosCrmDataContext.Instance.AE_ContractTemplates.LoadByPrimaryKey(oFnsPaymentInfo.ContractTemplateID);
		//		AE_Contract oContract = SosCrmDataContext.Instance.AE_Contracts.CreateContractTemplate(oContTemplate, szUserId);

		//		/** Save Payment Information. */
		//		string sBillingTypeId = oFnsPaymentInfo.PaymentInformation.PaymentMethod.Message().Equals("CreditCard")
		//				? AE_BillingType.MetaData.CreditCardID
		//				: string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.CheckNumber)
		//					? AE_BillingType.MetaData.ElectronicFundsTransferID
		//					: AE_BillingType.MetaData.CheckID;
		//		var oBillingInfo = new AE_BillingCustomer
		//				{
		//					BillingTypeId = sBillingTypeId,
		//					ContractId = oContract.ContractID,
		//					CustomerId = oCustBilling.CustomerID,
		//					MonthlyFee = oContract.MonthlyFee,
		//					CcNameOnRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.NameOnCard)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.NameOnCard,
		//					CcNumberRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.CCNumber)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.CCNumber,
		//					CcvRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.CCV)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.CCV,
		//					CcExpMonth = oFnsPaymentInfo.PaymentInformation.ExpMonth,
		//					CcExpYear = oFnsPaymentInfo.PaymentInformation.ExpYear,
		//					BkAccountNumberRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.AccountNumber)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.AccountNumber,
		//					BkRoutingNumberRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.RoutingNumber)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.RoutingNumber,
		//					BkCheckNumberRaw = string.IsNullOrWhiteSpace(oFnsPaymentInfo.PaymentInformation.CheckNumber)
		//						  ? null
		//						  : oFnsPaymentInfo.PaymentInformation.CheckNumber
		//				};
		//		oBillingInfo.Save(szUserId);

		//		/** Create an MC_Account. */
		//		var oMcAccount = new MC_Account
		//			{
		//				DealerAccountId = oFnsPaymentInfo.DealerAccountID,
		//				ShipContactSameAsCustomer = true,
		//				ShipAddressSameAsCustomer = true
		//			};
		//		oMcAccount.Save(szUserId);

		//		/** Create an invoice for this purchase. */
		//		AE_Invoice oInvoice = SosCrmDataContext.Instance.AE_Invoices.CreateInvoiceHeader(oMcAccount.AccountID
		//			, AE_InvoiceType.MetaData.SetupandInstallationID
		//			, null, null, null, null, null, null, null
		//			, AE_Contract.DefaultContracts.FULL_3_YEARS_THEN_MONTH_TO_MONTH
		//			, null, null, null, null);

		//		/** Create Invoice Items. */
		//		var oFnsAePaymentItems = new List<FnsAePaymentItem>();
		//		foreach (var oItem in oFnsPaymentInfo.ProductSkws.Select(productSkw => SosCrmDataContext.Instance.AE_Items.LoadByPrimaryKey(productSkw)))
		//		{
		//			AE_InvoiceItem oInvItem = SosCrmDataContext.Instance.AE_InvoiceItems.CreateFromAeItem(1, oItem, oInvoice.InvoiceID, szUserId);
		//			Debug.WriteLine("InventoryItemId: {0} | Cost: {1} | Retail: {2}", oInvItem.InvoiceItemID, oInvItem.Cost, oInvItem.RetailPrice);

		//			/** Create item for the response list. */
		//			oFnsAePaymentItems.Add(new FnsAePaymentItem
		//				{
		//					Qty = oInvItem.Qty,
		//					DiscountPrice = oInvItem.Item.ItemType.ItemTypeID.Equals(AE_ItemType.MetaData.PromotionDiscountsID)
		//					  ? oInvItem.RetailPrice
		//					  : 0,
		//					LineDescription = oInvItem.Item.ItemDesc,
		//					Skw = oInvItem.Item.ItemSKU,
		//					UnitPrice = oInvItem.RetailPrice,
		//					TotalLine = oInvItem.Qty * oInvItem.RetailPrice
		//				});
		//		}

		//		/** Calculate Sales Price and Taxes on Invoice. */
		//		oInvoice = SosCrmDataContext.Instance.AE_Invoices.CalculatePrices(oInvoice, oBillingAddress.StateId, oBillingAddress.PostalCode);

		//		/** Execute Payment through MerchantServices. */
		//		string sPaymentTypeId;
		//		switch (oFnsPaymentInfo.PaymentInformation.PaymentMethod)
		//		{
		//			case EnumFnsPaymentMethod.FnsCreditCard:
		//				sPaymentTypeId = AE_PaymentType.MetaData.CreditCardID;
		//				break;
		//			case EnumFnsPaymentMethod.FnsCheck:
		//				sPaymentTypeId = AE_PaymentType.MetaData.CheckID;
		//				break;
		//			default:
		//				sPaymentTypeId = AE_PaymentType.MetaData.CreditCardID;
		//				break;
		//		}
		//		var oAePayment = new AE_Payment
		//			{
		//				AccountId = oMcAccount.AccountID,
		//				PaymentTypeId = sPaymentTypeId,
		//				DocDate = DateTime.Now,
		//				DueDate = DateTime.Now,
		//				OriginalTransactionAmount = oInvoice.OriginalTransactionAmount
		//			};
		//		oAePayment.Save(szUserId);

		//		/** Tie Payment to Invoice. */
		//		var oAeInvPayment = new AE_InvoicePaymentJoin
		//			{
		//				InvoiceId = oInvoice.InvoiceID,
		//				PaymentId = oAePayment.PaymentID
		//			};
		//		oAeInvPayment.Save(szUserId);

		//		/** CHeck to see if method is cc or eft. */
		//		var oMg = new MerchantService();
		//		var oFnsInvPayModel = new FnsInvoicePaymentInfoModel(oInvoice, oAePayment) { Amount = oInvoice.OriginalTransactionAmount };
		//		IFnsResult<IFnsMerchResponseModel> oFnsResult;
		//		if (sPaymentTypeId.Equals(AE_PaymentType.MetaData.CreditCardID))
		//		{
		//			/** Execute Merchant. */
		//			// ReSharper disable SpecifyACultureInStringConversionExplicitly
		//			var sExpMonth = oFnsPaymentInfo.PaymentInformation.ExpMonth.ToString().PadLeft(2, '0');
		//			var sExpYear = oFnsPaymentInfo.PaymentInformation.ExpYear.ToString().PadLeft(2, '0');
		//			// ReSharper restore SpecifyACultureInStringConversionExplicitly
		//			oFnsInvPayModel.CardNumber = oFnsPaymentInfo.PaymentInformation.CCNumber;
		//			oFnsInvPayModel.Cvv = oFnsPaymentInfo.PaymentInformation.CCV;
		//			oFnsInvPayModel.ExpMonthYear = string.Format("{0}{1}"
		//				, sExpMonth
		//				, sExpYear);
		//			// ** Process
		//			oFnsResult = oMg.CcProcess(oFnsInvPayModel, szUserId);
		//		}
		//		else
		//		{
		//			/** Process ECheck if API is available. */
		//			oFnsInvPayModel.BankCheckNumber = oFnsPaymentInfo.PaymentInformation.CheckNumber;
		//			oFnsInvPayModel.BankABACode = oFnsPaymentInfo.PaymentInformation.RoutingNumber;
		//			oFnsInvPayModel.BankAccountNumber = oFnsPaymentInfo.PaymentInformation.AccountNumber;
		//			oFnsResult = oMg.ECheck(oFnsInvPayModel, szUserId);
		//		}

		//		if (oFnsResult.Code == 0)
		//		{
		//			/** Note the account. */
		//			oFnsNote = new FnsMcAccountNoteModel
		//			{
		//				NoteTypeID = MC_AccountNoteType.MetaData.BillingEngineID,
		//				CustomerMasterFileId = oFnsPaymentInfo.CustomerMasterFileId,
		//				CustomerId = oCustomer.CustomerID,
		//				LeadId = oFnsPaymentInfo.LeadId,
		//				NoteCategory1ID = 5, // 5	Product Purchase
		//				NoteCategory2ID = 8, // 8	Billing Successful First Time Purchase
		//				Note = string.Format("Frist product purchase.  Invoice ID:{0} | Payment ID: {1}'"
		//				  , oInvoice.InvoiceID, oAePayment.PaymentID)
		//			};
		//			McAccountNoteCreate(oFnsNote, szUserId);
		//		}
		//		else
		//		{
		//			/** Note the account. */
		//			oFnsNote = new FnsMcAccountNoteModel
		//			{
		//				NoteTypeID = MC_AccountNoteType.MetaData.BillingEngineID,
		//				CustomerMasterFileId = oFnsPaymentInfo.CustomerMasterFileId,
		//				CustomerId = oCustomer.CustomerID,
		//				LeadId = oFnsPaymentInfo.LeadId,
		//				NoteCategory1ID = 5, // 5	Product Purchase
		//				NoteCategory2ID = 9, // 8	Billing Successful First Time Purchase
		//				Note = string.Format("FAILED frist product purchase.  Invoice ID:{0} | Payment ID: {1}'"
		//				  , oInvoice.InvoiceID, oAePayment.PaymentID)
		//			};
		//			McAccountNoteCreate(oFnsNote, szUserId);
		//		}

		//		/** Get Result and return. */
		//		oResult.Code = oFnsResult.Code;
		//		oResult.Message = oFnsResult.Message;
		//		var oValue = (FnsMerchResponseModel)oFnsResult.GetValue();

		//		/** Return value must be of type IFnsAePaymentFull. */
		//		oResult.Value = oValue.CastToFnsAePaymentFull(oAePayment.PaymentID, oInvoice, oAePayment, oFnsInvPayModel, oFnsAePaymentItems, oCustomer, oCustBilling);

		//	}
		//	#region Exceptions
		//	catch (SqlException oSqlEx)
		//	{
		//		oResult = new FnsResult<IFnsAePaymentFull>
		//		{
		//			Code = (int)ErrorCodes.SqlExceptions,
		//			Message = oSqlEx.Message
		//		};
		//	}
		//	catch (Exception oEx)
		//	{
		//		oResult = new FnsResult<IFnsAePaymentFull>
		//		{
		//			Code = (int)ErrorCodes.UnexpectedException,
		//			Message = oEx.Message
		//		};
		//	}
		//	#endregion Exceptions

		//	/** Return result. */
		//	return oResult;
		//}

		public IFnsResult<List<IFnsMcDealerUser>> DealerUsersGet(int nDealerId)
		{
			/** Initialize. */
			var oResult = new FnsResult<List<IFnsMcDealerUser>>
							{
								Code = (int)ErrorCodes.GeneralMessage,
								Message = "Initializing DealerUsersGet"
							};

			/** Validate. */
			if (nDealerId == 0)
			{
				return new FnsResult<List<IFnsMcDealerUser>>
						{
							Code = (int)ErrorCodes.GeneralMessage,
							Message = "DealerUsersGet is missing nDealerId argument."
						};
			}

			/** Execute. */
			#region TRY
			try
			{
				/** Get list. */
				MC_DealerUserCollection oColl = SosCrmDataContext.Instance.MC_DealerUsers.GetByDealerID(nDealerId);
				List<FnsMcDealerUser> oList = oColl.Select(oItem => new FnsMcDealerUser(oItem)).ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsMcDealerUser>(oList);

			}
			#endregion TRY
			#region CATCH
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<List<IFnsMcDealerUser>>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsMcDealerUser>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<bool> LeadDispositionUpdate(int nDealerId, long nLeadID, int nLeadDispositionId, string szUserId)
		{
			/** Initialize. */
			#region INITIALIZATION
			var oResult = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing LeadDispositionUpdate",
				Value = false
			};

			/** Validate. */
			if (nDealerId == 0)
			{
				return new FnsResult<bool>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "LeadDispositionUpdate is missing nDealerId argument.",
					Value = false
				};
			}
			#endregion INITIALIZATION

			/** Execute. */
			#region TRY
			try
			{
				/** Check that this lead belongs to this dealer. */
				var oLead = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(nLeadID);
				if (oLead.DealerId != nDealerId)
				{
					return new FnsResult<bool>
					{
						Code = (int)ErrorCodes.InvalidCredentials,
						Message = "Sorry the dealer id associated with this session does not have rights for this Lead.",
						Value = false
					};
				}

				/** Update the leadDisposition. */
				var szOldDisp = oLead.LeadDisposition.LeadDisposition;
				oLead.LeadDispositionId = nLeadDispositionId;
				oLead.LeadDispositionDateChange = DateTime.Now;
				oLead.Save(szUserId);
				oLead = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(nLeadID);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = true;

				/** Note the account. */
				IFnsMcAccountNoteModel oFnsNote = new FnsMcAccountNoteModel
				{
					NoteTypeID = MC_AccountNoteType.MetaData.Standard_NoteID,
					CustomerMasterFileId = oLead.CustomerMasterFileId,
					LeadId = oLead.LeadID,
					NoteCategory1ID = 3, // 3	Lead Disposition
					NoteCategory2ID = 6, // 6	Changed
					Note = string.Format("Disposition Changed from '{0}' to '{1}'"
					  , szOldDisp, oLead.LeadDisposition.LeadDisposition)
				};

				McAccountNoteCreate(oFnsNote, szUserId);

			}
			#endregion TRY
			#region CATCH
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<bool>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message,
					Value = false
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<bool>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message,
					Value = false
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsCaAppointmentModel>> DealerUserAppointmentsGet(int nDealerUserId, DateTime dStartDate, DateTime dEndDate)
		{
			/** Initialize. */
			#region INITIALIZATION
			var oResult = new FnsResult<List<IFnsCaAppointmentModel>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing DealerUserAppointmentsGet"
			};

			/** Validate. */
			if (nDealerUserId == 0)
			{
				return new FnsResult<List<IFnsCaAppointmentModel>>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "DealerUserAppointmentsGet is missing nDealerId argument."
				};
			}
			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Execute inquery. */
				CA_AppointmentCollection oCollResult = SosCrmDataContext.Instance.CA_Appointments.GetByUserIdAndDateRange(nDealerUserId
					, dStartDate, dEndDate);

				/** Cast to FnsModel */
				var oResultList = oCollResult.Select(caAppointment => new FnsCaAppointmentModel(caAppointment)).ToList();
				// ** Save results and return.
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsCaAppointmentModel>(oResultList);

			}
			#endregion TRY
			#region CATCH
			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<List<IFnsCaAppointmentModel>>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsCaAppointmentModel>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsCustomerFullDataModel> AeCustomerRead(long lCustomerId, bool bNoteAccount = false)
		{
			/** Initialize. */
			#region INITIALIZATION
			var oResult = new FnsResult<IFnsCustomerFullDataModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AeCustomerRead"
			};

			/** Validate. */
			if (lCustomerId == 0)
			{
				return new FnsResult<IFnsCustomerFullDataModel>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "AeCustomerRead is missing sUsername argument."
				};
			}
			#endregion INITIALIZATION

			/** Read Customer. */
			#region TRY
			try
			{
				/** Initialize. */
				AE_Customer oCustomer = SosCrmDataContext.Instance.AE_Customers.LoadByPrimaryKey(lCustomerId);

				var oModel = new FnsCustomerFullDataModel(oCustomer);
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oModel;

			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsCustomerFullDataModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> AeCustomerUpdate(IFnsAeCustomerGpsClientsViewModel customerInfo, string szUserId = "SYSTEM")
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AeCustomerUpdate"
			};

			/** Validate. */
			if (customerInfo.CustomerID == 0)
			{
				return new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "AeCustomerUpdate is missing its primary key."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Load customer info. */
				var customer = SosCrmDataContext.Instance.AE_Customers.LoadByPrimaryKey(customerInfo.CustomerID);
				if (customer == null) return new FnsResult<IFnsAeCustomerGpsClientsViewModel>
					{
						Code = (int)ErrorCodes.GeneralError,
						Message = string.Format("Unable to find customer with ID '{0}'", customerInfo.CustomerID)
					};

				/** Update information and make sure there are changes. */
				FieldAssignment.AssignIfDirty(customerInfo.CustomerTypeId, customer.CustomerTypeId, () => { customer.CustomerTypeId = customerInfo.CustomerTypeId; });
				FieldAssignment.AssignIfDirty(customerInfo.CustomerMasterFileId, customer.CustomerMasterFileId, () => { customer.CustomerMasterFileId = customerInfo.CustomerMasterFileId; });
				FieldAssignment.AssignIfDirty(customerInfo.DealerId, customer.DealerId, () => { customer.DealerId = customerInfo.DealerId; });
				FieldAssignment.AssignIfDirty(customerInfo.AddressId, customer.AddressId, () => { customer.AddressId = customerInfo.AddressId; });
				FieldAssignment.AssignIfDirty(customerInfo.LocalizationId, customer.LocalizationId, () => { customer.LocalizationId = customerInfo.LocalizationId; });
				FieldAssignment.AssignIfDirty(customerInfo.Prefix, customer.Prefix, () => { customer.Prefix = customerInfo.Prefix; });
				FieldAssignment.AssignIfDirty(customerInfo.FirstName, customer.FirstName, () => { customer.FirstName = customerInfo.FirstName; });
				FieldAssignment.AssignIfDirty(customerInfo.MiddleName, customer.MiddleName, () => { customer.MiddleName = customerInfo.MiddleName; });
				FieldAssignment.AssignIfDirty(customerInfo.LastName, customer.LastName, () => { customer.LastName = customerInfo.LastName; });
				FieldAssignment.AssignIfDirty(customerInfo.Postfix, customer.Postfix, () => { customer.Postfix = customerInfo.Postfix; });
				FieldAssignment.AssignIfDirty(customerInfo.Gender, customer.Gender, () => { customer.Gender = customerInfo.Gender; });
				FieldAssignment.AssignIfDirty(customerInfo.PhoneHome, customer.PhoneHome, () => { customer.PhoneHome = customerInfo.PhoneHome; });
				FieldAssignment.AssignIfDirty(customerInfo.PhoneWork, customer.PhoneWork, () => { customer.PhoneWork = customerInfo.PhoneWork; });
				FieldAssignment.AssignIfDirty(customerInfo.PhoneMobile, customer.PhoneMobile, () => { customer.PhoneMobile = customerInfo.PhoneMobile; });
				FieldAssignment.AssignIfDirty(customerInfo.Email, customer.Email, () => { customer.Email = customerInfo.Email; });
				FieldAssignment.AssignIfDirty(customerInfo.DOB, customer.DOB, () => { customer.DOB = customerInfo.DOB; });
				FieldAssignment.AssignIfDirty(customerInfo.SSN, customer.SSN, () => { customer.SSN = customerInfo.SSN; });
				FieldAssignment.AssignIfDirty(customerInfo.Username, customer.Username, () => { customer.Username = customerInfo.Username; });
				FieldAssignment.AssignIfDirty(customerInfo.Password, customer.Password, () => { customer.Password = customerInfo.Password; });
				FieldAssignment.AssignIfDirty(customerInfo.IsActive, customer.IsActive, () => { customer.IsActive = customerInfo.IsActive; });

				/** Update customer info if necessary. */
				if (!customer.IsDirty)
				{
					oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
						{
							Code = (int)ErrorCodes.GeneralError,
							Message = "Nothing was changed."
						};
				}
				else
				{
					customer.Save(szUserId);
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(customer);
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}

			}
			#endregion TRY
			#region CATCH

			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> AeCustomerAuthenticate(string sUsername, string sPassword)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing AeCustomerAuthenticate"
			};

			/** Validate. */
			if (string.IsNullOrEmpty(sUsername))
			{
				return new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "AeCustomerAuthenticate is missing sUsername argument."
				};
			}
			if (string.IsNullOrEmpty(sPassword))
			{
				return new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "AeCustomerAuthenticate is missing sPassword argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				/** Execute inquery. */
				AE_CustomerGpsClientsView oAuthlResult = SosCrmDataContext.Instance.AE_CustomerGpsClientsViews.Authenticate(sUsername, sPassword);

				/** Check result. */
				if (oAuthlResult == null)
				{
					oResult.Code = (int)ErrorCodes.LoginFailure;
					oResult.Message = "Invalid credentials passed";
				}
				else
				{
					/** Cast to FnsModel */
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(oAuthlResult);
					// ** Save results and return.
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}
			}

			#endregion TRY
			#region CATCH

			catch (SqlException oSqlEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.SqlExceptions,
					Message = oSqlEx.Message
				};
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		//public IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerSignup(IFnsAeCustomerGpsClientsViewModel customerInfoArg)
		//{
		//	throw new NotImplementedException();
		//}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerSignup(IFnsAeCustomerGpsClientsViewModel customerInfoArg)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing CustomerSignup"
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AE_CustomerGpsClientsView oCust =
					SosCrmDataContext.Instance.AE_CustomerGpsClientsViews.GpsClientSignup(customerInfoArg.DealerId,
																						  customerInfoArg.SalesRepId,
																						  customerInfoArg.LocalizationId,
																						  customerInfoArg.FirstName,
																						  customerInfoArg.LastName,
																						  customerInfoArg.Gender,
																						  customerInfoArg.PhoneHome,
																						  customerInfoArg.PhoneWork,
																						  customerInfoArg.PhoneMobile,
																						  customerInfoArg.Email,
																						  customerInfoArg.Username,
																						  customerInfoArg.Password,
																						  customerInfoArg.LeadSourceId,
																						  customerInfoArg.LeadDispositionId);

				if (oCust == null)
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = "There was an error signing up.  Please contact us.";
				}
				else
				{
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(oCust);
					/** Save Result and return. */
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}
			}
			#endregion TRY

			#region CATCH

			catch (SqlException oSqlEx)
			{
				MsSqlException sqlException = MsSqlExceptionUtil.Parse(oSqlEx.Message);
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
					{
						Code = (int)ErrorCodes.UnexpectedException,
						Message = sqlException.ErrorMessage
					};
				Debug.WriteLineIf(oSqlEx.ErrorCode != 0, string.Format("Here is the error message: Line #: {0} | Error Code: {1} | Message: {2} | Number: {3}", oSqlEx.LineNumber, oSqlEx.ErrorCode, oSqlEx.Message, oSqlEx.Number));
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
					{
						Code = (int)ErrorCodes.UnexpectedException,
						Message = string.Format("Exception thrown at the CustomerSignup: {0}", oEx.Message)
					};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerUpdate(IFnsAeCustomerGpsClientsViewModel customerInfoArg)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing CustomerUpdate"
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AE_CustomerGpsClientsView oCust =
					SosCrmDataContext.Instance.AE_CustomerGpsClientsViews.GpsClientUpdate(customerInfoArg.LocalizationId,
																						  customerInfoArg.Prefix,
																						  customerInfoArg.FirstName,
																						  customerInfoArg.LastName,
																						  customerInfoArg.Gender,
																						  customerInfoArg.PhoneHome,
																						  customerInfoArg.PhoneWork,
																						  customerInfoArg.PhoneMobile,
																						  customerInfoArg.Email,
																						  customerInfoArg.DOB,
																						  customerInfoArg.SSN,
																						  customerInfoArg.Username,
																						  customerInfoArg.Password,
																						  customerInfoArg.StateId,
																						  customerInfoArg.CountryId,
																						  customerInfoArg.TimezoneId,
																						  customerInfoArg.StreetAddress,
																						  customerInfoArg.StreetAddress2,
																						  customerInfoArg.County,
																						  customerInfoArg.City,
																						  customerInfoArg.PostalCode,
																						  customerInfoArg.PlusFour,
																						  customerInfoArg.PhoneHome);

				if (oCust == null)
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = "There was an error updating.  Please contact us.";
				}
				else
				{
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(oCust);
					/** Save Result and return. */
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}
			}
			#endregion TRY

			#region CATCH

			catch (SqlException oSqlEx)
			{
				MsSqlException sqlException = MsSqlExceptionUtil.Parse(oSqlEx.Message);
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = sqlException.ErrorMessage
				};
				Debug.WriteLineIf(oSqlEx.ErrorCode != 0, string.Format("Here is the error message: Line #: {0} | Error Code: {1} | Message: {2} | Number: {3}", oSqlEx.LineNumber, oSqlEx.ErrorCode, oSqlEx.Message, oSqlEx.Number));
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at the CustomerUpdate: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerRead(long lCustomerID)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing CustomerRead"
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AE_CustomerGpsClientsView oCust =
					SosCrmDataContext.Instance.AE_CustomerGpsClientsViews.GpsClientRead(lCustomerID);

				if (oCust == null)
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = "There was an error reading.  Please contact us.";
				}
				else
				{
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(oCust);
					/** Save Result and return. */
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}
			}
			#endregion TRY

			#region CATCH

			catch (SqlException oSqlEx)
			{
				MsSqlException sqlException = MsSqlExceptionUtil.Parse(oSqlEx.Message);
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = sqlException.ErrorMessage
				};
				Debug.WriteLineIf(oSqlEx.ErrorCode != 0, string.Format("Here is the error message: Line #: {0} | Error Code: {1} | Message: {2} | Number: {3}", oSqlEx.LineNumber, oSqlEx.ErrorCode, oSqlEx.Message, oSqlEx.Number));
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at the CustomerRead: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAeCustomerGpsClientsViewModel> CustomerDelete(long lCustomerID)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing CustomerDelete"
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AE_CustomerGpsClientsView oCust =
					SosCrmDataContext.Instance.AE_CustomerGpsClientsViews.GpsClientDelete(lCustomerID);

				if (oCust == null)
				{
					oResult.Code = (int)ErrorCodes.GeneralError;
					oResult.Message = "There was an error during deletion.  Please contact us.";
				}
				else
				{
					var oResultValue = new FnsAeCustomerGpsClientsViewModel(oCust);
					/** Save Result and return. */
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oResultValue;
				}
			}
			#endregion TRY

			#region CATCH

			catch (SqlException oSqlEx)
			{
				MsSqlException sqlException = MsSqlExceptionUtil.Parse(oSqlEx.Message);
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = sqlException.ErrorMessage
				};
				Debug.WriteLineIf(oSqlEx.ErrorCode != 0, string.Format("Here is the error message: Line #: {0} | Error Code: {1} | Message: {2} | Number: {3}", oSqlEx.LineNumber, oSqlEx.ErrorCode, oSqlEx.Message, oSqlEx.Number));
			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAeCustomerGpsClientsViewModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at the CustomerSignup: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsQlDealerLeadModel> DealerLeadCreateUpdate(IFnsQlDealerLeadModel oDealerLead, string szUserId = "SYSTEM")
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsQlDealerLeadModel>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing DealerLeadCreateUpdate"
			};

			/** Validate. */
			if (oDealerLead == null)
			{
				return new FnsResult<IFnsQlDealerLeadModel>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "DealerLeadCreateUpdate is missing oDealerLead argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				/** Execute the save. */
				QL_DealerLead oDealerLeadDb = oDealerLead.DealerLeadID > 0
					? SosCrmDataContext.Instance.QL_DealerLeads.LoadByPrimaryKey(oDealerLead.DealerLeadID)
					: new QL_DealerLead { DealerLeadID = oDealerLead.DealerLeadID };

				/** Bind values and save. */
				//oDealerLeadDb.DealerLeadID = oDealerLead.DealerLeadID;
				oDealerLeadDb.DealerLeadTypeId = oDealerLead.DealerLeadTypeId;
				oDealerLeadDb.DealerName = oDealerLead.DealerName;
				oDealerLeadDb.ContactFirstName = oDealerLead.ContactFirstName;
				oDealerLeadDb.ContactLastName = oDealerLead.ContactLastName;
				oDealerLeadDb.ContactEmail = oDealerLead.ContactEmail;
				oDealerLeadDb.PhoneWork = oDealerLead.PhoneWork;
				oDealerLeadDb.PhoneMobile = oDealerLead.PhoneMobile;
				oDealerLeadDb.PhoneFax = oDealerLead.PhoneFax;
				oDealerLeadDb.Address = oDealerLead.Address;
				oDealerLeadDb.Address2 = oDealerLead.Address2;
				oDealerLeadDb.City = oDealerLead.City;
				oDealerLeadDb.StateAB = oDealerLead.StateAB;
				oDealerLeadDb.PostalCode = oDealerLead.PostalCode;
				oDealerLeadDb.PlusFour = oDealerLead.PlusFour;
				oDealerLeadDb.Memo = oDealerLead.Memo;
				oDealerLeadDb.Username = oDealerLead.Username;
				oDealerLeadDb.Password = oDealerLead.Password;
				oDealerLeadDb.IsActive = oDealerLead.IsActive;
				oDealerLeadDb.IsDeleted = oDealerLead.IsDeleted;
				//oDealerLeadDb.ModifiedOn = oDealerLead.ModifiedOn;
				//oDealerLeadDb.ModifiedBy = oDealerLead.ModifiedBy;
				//oDealerLeadDb.CreatedOn = oDealerLead.CreatedOn;
				//oDealerLeadDb.CreatedBy = oDealerLead.CreatedBy;
				//oDealerLeadDb.DEX_ROW_TS = oDealerLead.DEX_ROW_TS;
				oDealerLeadDb.Save(szUserId);

				/** Create result. */
				oResult.Value = new FnsQlDealerLeadModel(oDealerLeadDb);
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsQlDealerLeadModel>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = oEx.Message
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsMsAccountClientsView>> GetDevicesByCMFID(long? lCMFID, string szUserId = "SYSTEM")
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsMsAccountClientsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing GetDevicesByCMFID"
			};

			/** Validate. */
			if (lCMFID == null || lCMFID.Value == 0)
			{
				return new FnsResult<List<IFnsMsAccountClientsView>>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "GetDevicesByCMFID is missing lCMFID argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				MS_AccountClientsViewCollection oCol = SosCrmDataContext.Instance.MS_AccountClientsViews.GetAccountsByCMFID(lCMFID.Value);
				var oResultList = oCol.Select(oItem => new FnsMsAccountClientsView(oItem)).ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsMsAccountClientsView>(oResultList);
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsMsAccountClientsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at GetDevicesByCMFID: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsMsAccountClientsView>> GetDevicesByCustomerID(long? lCustomerID, string szUserId = "SYSTEM")
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsMsAccountClientsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing GetDevicesByCustomerID"
			};

			/** Validate. */
			if (lCustomerID == null || lCustomerID.Value == 0)
			{
				return new FnsResult<List<IFnsMsAccountClientsView>>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "GetDevicesByCustomerID is missing lCMFID argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				MS_AccountClientsViewCollection oCol = SosCrmDataContext.Instance.MS_AccountClientsViews.GetAccountsByCustomerID(lCustomerID.Value);
				var oResultList = oCol.Select(oItem => new FnsMsAccountClientsView(oItem)).ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsMsAccountClientsView>(oResultList);
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsMsAccountClientsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at GetDevicesByCustomerID: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsMsAccountClientDetailsView> GetDeviceDetailsByAccountID(long? lAccountID, long? lCustomerID, string szUserID = "SYSTEM")
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsMsAccountClientDetailsView>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing GetDeviceDetailsByAccountID"
			};

			/** Validate. */
			if (lAccountID == null || lAccountID.Value == 0)
			{
				return new FnsResult<IFnsMsAccountClientDetailsView>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "GetDeviceDetailsByAccountID is missing lAccountID argument."
				};
			}
			if (lCustomerID == null || lCustomerID.Value == 0)
			{
				return new FnsResult<IFnsMsAccountClientDetailsView>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "GetDeviceDetailsByAccountID is missing lCustomerID argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				MS_AccountClientDetailsView oResulItem = SosCrmDataContext.Instance.MS_AccountClientDetailsViews.GetDeviceDetailsByAccountId(lAccountID.Value, lCustomerID.Value);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new FnsMsAccountClientDetailsView(oResulItem);
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsMsAccountClientDetailsView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at GetDeviceDetailsByAccountID: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsMsAccountClientsView> UpdateDevice(long lAccountID, string sAccountName, string sAccountDesc)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsMsAccountClientsView>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "Initializing UpdateDevice"
				};
			/** Validate. */
			if (lAccountID == 0)
				return new FnsResult<IFnsMsAccountClientsView>
					{
						Code = (int)ErrorCodes.GeneralMessage,
						Message = "UpdateDevice is missing lAccountID argument."
					};
			if (string.IsNullOrEmpty(sAccountName))
				return new FnsResult<IFnsMsAccountClientsView>
					{
						Code = (int)ErrorCodes.GeneralMessage,
						Message = "UpdateDevice is missing sAccountName argument."
					};
			if (string.IsNullOrEmpty(sAccountDesc))
				return new FnsResult<IFnsMsAccountClientsView>
					{
						Code = (int)ErrorCodes.GeneralMessage,
						Message = "UpdateDevice is missing sAccountDesc argument."
					};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				MS_AccountClientsView oResultItem = SosCrmDataContext.Instance.MS_AccountClientsViews.UpdateDevice(lAccountID,
																												   sAccountName,
																												   sAccountDesc);
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new FnsMsAccountClientsView(oResultItem);
			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsMsAccountClientsView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at UpdateDevice: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsVerifyAddress> AddressRead(long addressId)
		{
			var result = new FnsResult<IFnsVerifyAddress>();

			var address = SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(addressId);
			if (address == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "No address found.";
				return result;
			}
			result.Value = new FnsVerifyAddress(address);
			return result;
		}

		public IFnsResult<IFnsVerifyAddress> AddressDelete(long addressId)
		{
			throw new NotImplementedException();
		}

		public IFnsResult<IFnsVerifyAddress> AddressUpdate(IFnsVerifyAddress address, string userId)
		{
			throw new NotImplementedException();
		}

		public IFnsResult<IFnsVerifyAddress> AddressCreate(IFnsVerifyAddress address, string userId)
		{
			throw new NotImplementedException();
		}

		public IFnsResult<IFnsVerifyAddress> SaveAddress(IFnsVerifyAddress ifnsAddress, string userId)
		{
			var fnsAddress = (FnsVerifyAddress)ifnsAddress;

			var result = new FnsResult<IFnsVerifyAddress>();

			QL_Address address;
			if (fnsAddress.AddressID > 0)
			{
				address = SosCrmDataContext.Instance.QL_Addresses.LoadByPrimaryKey(fnsAddress.AddressID);
				if (address == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "No address found to update.";
					return result;
				}
			}
			else
			{
				address = new QL_Address
				{
					//AddressID = fnsAddress.AddressID,
					CreatedOn = DateTime.UtcNow,
					CreatedBy = userId,
				};
			}
			fnsAddress.ToDb(address);
			address.Save(userId);

			result.Value = new FnsVerifyAddress(address);
			return result;
		}

		public IFnsResult<IFnsVerifyAddress> AddressVerify(IFnsVerifyAddress address, int seasonId, int teamLocationId, string salesRepId, string userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<IFnsVerifyAddress>
				{
					Code = (int)ErrorCodes.GeneralMessage,
					Message = "Initializing AddressVerify"
				};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				var avService = new SSE.FOS.AddressVerification.Main();
				var areaCode = StringUtility.GetAreaCode(address.Phone);
				IFosQlAddress fosAddress = new FosQlAddress
					{
						AddressLine1 = address.StreetAddress,
						AddressLine2 = address.StreetAddress2,
						StreetNumber = address.StreetNumber,
						StreetName = address.StreetName,
						City = string.IsNullOrEmpty(address.City)
							? string.Empty
							: address.City,
						StateId = string.IsNullOrEmpty(address.StateId)
							? string.Empty
							: address.StateId,
						PostalCode = string.IsNullOrEmpty(address.PostalCode)
							? string.Empty
							: address.PostalCode,
						County = address.County,
						PreDirectional = address.PreDirectional,
						PostDirectional = address.PostDirectional,
						StreetType = address.StreetType,
						Extension = address.Extension,
						ExtensionNumber = address.ExtensionNumber,
						CarrierRoute = address.CarrierRoute,
						DPVResponse = address.DPVResponse,
						TimeZoneId = address.TimeZoneId,
						Phone = address.Phone,
					};
				IFosAVResult<IFosAddressVerified> avResult = avService.VerifyAddress(fosAddress, areaCode, address.DealerId, seasonId, teamLocationId, salesRepId, userId);

				if (avResult.Code == (int)ErrorCodes.Success)
				{
					// ** Bind address to correct return value.
					address.AddressID = avResult.Value.AddressID;
					address.StreetAddress = avResult.Value.StreetAddress;
					address.StreetAddress2 = avResult.Value.StreetAddress2;
					address.StreetNumber = avResult.Value.StreetNumber;
					address.StreetName = avResult.Value.StreetName;
					address.City = avResult.Value.City;
					address.StateId = avResult.Value.StateId;
					address.PostalCode = avResult.Value.PostalCode;
					address.PlusFour = avResult.Value.PlusFour;
					address.County = avResult.Value.County;
					address.PreDirectional = avResult.Value.PreDirectional;
					address.PostDirectional = avResult.Value.PostDirectional;
					address.StreetType = avResult.Value.StreetType;
					address.Extension = avResult.Value.Extension;
					address.ExtensionNumber = avResult.Value.ExtensionNumber;
					address.CarrierRoute = avResult.Value.CarrierRoute;
					address.DPVResponse = avResult.Value.DPVResponse;
					address.SalesRepId = salesRepId;
					address.SeasonId = seasonId;
					address.TeamLocationId = teamLocationId;
					address.TimeZoneId = avResult.Value.TimeZoneId;
					//address.TimeZone = avResult.Value.TimeZone;
					address.Latitude = avResult.Value.Lattitude;
					address.Longitude = avResult.Value.Longitude;
					address.DPV = avResult.Value.DPV;
					address.IsActive = avResult.Value.IsActive;
					address.CreatedOn = avResult.Value.CreatedOn;
					address.CreatedBy = avResult.Value.CreatedBy;
					result.Value = address;
				}

				// ** Save result information
				result.Code = avResult.Code;
				result.Message = avResult.Message;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsVerifyAddress>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at AddressVerify: {0}", ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsQlLead> ReadLead(long leadID)
		{
			var result = new FnsResult<IFnsQlLead> { Message = "" };

			// update lead
			var lead = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(leadID);
			if (lead == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "Lead not found";
				return result;
			}
			result.Value = FnsQlLead.FromQL_Lead(lead);
			return result;
		}

		public IFnsResult<IFnsQlLead> SaveLead(IFnsQlLead fnsLead, string userId, bool createMasterLead)
		{
			var result = new FnsResult<IFnsQlLead> { Message = "" };

			// set defaults
			if (fnsLead.DealerId == 0)
			{
				// default to our dealer
				fnsLead.DealerId = 5000;
			}

			var instance = SosCrmDataContext.Instance;
			if (fnsLead.LeadID == 0)
			{
				DatabaseHelper.UseTransaction(Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
				{
					var cmfid = fnsLead.CustomerMasterFileId;
					if (cmfid == 0)
					{
						// create CMF
						var masterFile = new AE_CustomerMasterFile
						{
							DealerId = fnsLead.DealerId,
						};
						masterFile.Save(userId);
						cmfid = masterFile.CustomerMasterFileID;
					}

					// create lead
					var lead = new QL_Lead
					{
						CustomerMasterFileId = cmfid,
						DealerId = fnsLead.DealerId,
						AddressId = fnsLead.AddressId,
						SalesRepId = fnsLead.SalesRepId,
						IsActive = true,
						CreatedOn = DateTime.UtcNow,
						DEX_ROW_TS = DateTime.UtcNow,
					};
					SaveQlLead(lead, fnsLead, userId);

					if (createMasterLead)
					{
						// create QL_CustomerMasterLeads for LeadId and CMFID
						new QL_CustomerMasterLead
						{
							CustomerMasterFileId = cmfid,
							LeadId = lead.LeadID,
							CustomerTypeId = fnsLead.CustomerTypeId,
						}.Save(userId);
					}

					// create QL_LeadAddress for LeadId and AddressId
					new QL_LeadAddress
					{
						LeadId = lead.LeadID,
						AddressId = lead.AddressId,
					}.Save(userId);

					// create QL_LeadProductOffers for LeadId, ProductSkwId, and SalesRepId
					if (string.IsNullOrEmpty(fnsLead.ProductSkwId))
					{
						fnsLead.ProductSkwId = "HSSS001";
					}
					new QL_LeadProductOffer
					{
						LeadId = lead.LeadID,
						ProductSkwId = fnsLead.ProductSkwId,
						SalesRepId = fnsLead.SalesRepId,
						OfferDate = DateTime.UtcNow,
					}.Save(userId);

					result.Value = FnsQlLead.FromQL_Lead(lead);
					return true;
				});
			}
			else
			{
				DatabaseHelper.UseTransaction(Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
				{
					// update lead
					var lead = instance.QL_Leads.LoadByPrimaryKey(fnsLead.LeadID);
					if (lead == null)
					{
						result.Code = (int)ErrorCodes.SqlItemNotFound;
						result.Message = "Lead not found";
						return false;
					}
					if (fnsLead.CustomerMasterFileId != 0 && lead.CustomerMasterFileId != fnsLead.CustomerMasterFileId)
					{
						result.Code = -1;
						result.Message = "Lead CustomerMasterFileId cannot be changed";
						return false;
					}
					SaveQlLead(lead, fnsLead, userId);

					result.Value = FnsQlLead.FromQL_Lead(lead);
					return true;
				});
			}
			return result;
		}
		private void SaveQlLead(QL_Lead lead, IFnsQlLead fnsLead, string userId)
		{
			//
			// set lead fields
			//
			//lead.LeadID = fnsLead.LeadID; // don't set PK
			//lead.AddressId = fnsLead.AddressID; // don't change Address on edit
			lead.CustomerTypeId = fnsLead.CustomerTypeId;
			//lead.CustomerMasterFileId = fnsLead.CustomerMasterFileId; // don't change Master File on edit
			//lead.DealerId = fnsLead.DealerId; // don't change Dealer on edit
			lead.LocalizationId = fnsLead.LocalizationId;
			lead.TeamLocationId = fnsLead.TeamLocationId;
			lead.SeasonId = fnsLead.SeasonId;
			//lead.SalesRepId = fnsLead.SalesRepId; // don't change SalesRep on edit
			lead.LeadSourceId = fnsLead.LeadSourceId;
			lead.LeadDispositionId = fnsLead.LeadDispositionId;
			lead.LeadDispositionDateChange = fnsLead.LeadDispositionDateChange;
			lead.Salutation = fnsLead.Salutation;
			lead.FirstName = fnsLead.FirstName;
			lead.MiddleName = fnsLead.MiddleName;
			lead.LastName = fnsLead.LastName;
			lead.Suffix = fnsLead.Suffix;
			lead.Gender = fnsLead.Gender;
			lead.SSN = string.IsNullOrEmpty(fnsLead.SSN) ? null : Lib.Util.Cryptography.TripleDES.EncryptString(fnsLead.SSN, null);
			lead.DOB = fnsLead.DOB;
			lead.DL = fnsLead.DL;
			lead.DLStateID = fnsLead.DLStateId;
			lead.Email = fnsLead.Email;
			lead.PhoneHome = fnsLead.PhoneHome;
			lead.PhoneWork = fnsLead.PhoneWork;
			lead.PhoneMobile = fnsLead.PhoneMobile;
			//lead.InsideSalesId = fnsLead.InsideSalesId;
			//lead.IsActive = true; // don't change active status

			// save lead
			lead.Save(userId);
		}

		public IFnsResult<IFnsQlCreditReport> RunCredit(long leadID, bool bypass, string userId)
		{
			#region INITIALIZATION

			// ** Initialize
			var result = new FnsResult<IFnsQlCreditReport>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing RunCredit"
			};
			MS_LeadTakeOver leadTakeover = null;

			#endregion INITIALIZATION

			try
			{
				QL_Lead lead = SosCrmDataContext.Instance.QL_Leads.LoadByPrimaryKey(leadID);
				if (lead == null)
				{
					result.Code = (int)ErrorCodes.SqlItemNotFound;
					result.Message = "Lead not found";
					return result;
				}

				/** Call Monitronics and Check for Slammed Account. */
				var centralStation = new FOS.MonitoringStationServices.Monitronics.CentralStation();
				var csResult = centralStation.IsNotSlammedAccount(lead.Address, lead);
				if (csResult.Code != BaseErrorCodes.ErrorCodes.Success.Code())
				{
					//TODO:  Flag this account as slammed account
					leadTakeover = new MS_LeadTakeOver
					{
						LeadId = leadID,
						AddressId = lead.AddressId,
						AlarmCompanyId = (int) MS_AlarmCompany.AlarmCompanyEnum.Monitronics
					};
					leadTakeover.Save(userId);
				}

				IWSLead wsLead = new WSLead(lead);
				IWSAddress wsAddress = new WSAddress(lead.Address);
				string[] bureausList = NSE.FOS.RunCreditServices.Main.GetBureausList();
				var ruSeason = HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(lead.SeasonId);
				IWSSeason season = new WSSeason(ruSeason);
				var messageList = new List<WSMessage>();
				IWSCreditReportInfo crInfo;
				bool success;

				if (bypass)
				{
					success = new NSE.FOS.RunCreditServices.Vendors.Manual(
						score: 999,
						isHit: true,
						isScored: true,
						report: "Bypass RunCredit"
					).RunCredit(wsLead, wsAddress, bureausList, season, userId, ref messageList, out crInfo);
				}
				else
				{
					success = new NSE.FOS.RunCreditServices.Main(
					).RunCredit(wsLead, wsAddress, bureausList, season, userId, ref messageList, out crInfo);
				}

				var creditReport = SosCrmDataContext.Instance.QL_CreditReports.LoadByPrimaryKey(crInfo.CreditReportID);
				var fnsCreditReport = new FnsQlCreditReport(crInfo, creditReport, ruSeason, leadTakeover);
				result.Code = (int)(success ? ErrorCodes.Success : ErrorCodes.CreditReportError);
				result.Message = NSE.FOS.RunCreditServices.Helpers.WSMessage.PartitionList(messageList);
				result.Value = fnsCreditReport;
			}
			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsQlCreditReport>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at RunCredit: {0}", ex.Message)
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsAeCustomerCardInfo>> MasterFileCustomers(long cmfid)
		{
			var result = new FnsResult<List<IFnsAeCustomerCardInfo>>
			{
				Value = new List<IFnsAeCustomerCardInfo>(),
			};

			var customers = SosCrmDataContext.Instance.AE_Customers.ByCmfid(cmfid).ToList();
			foreach (var c in customers)
			{
				QL_CreditReport creditReport = SosCrmDataContext.Instance.QL_CreditReports.MaxScoreByCmfID(c.CustomerMasterFileId);
				RU_Season season = null;
				if (creditReport != null)
				{
					season = HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(creditReport.SeasonId);
				}
				result.Value.Add(new FnsAeCustomerCardInfo(c, creditReport, season));
			}
			return result;
		}

		public IFnsResult<bool> MasterFileHasCustomer(long cmfid)
		{
			var result = new FnsResult<bool> { Message = "" };
			result.Value = SosCrmDataContext.Instance.AE_Customers.ExistsForCmfid(cmfid);
			return result;
		}

		#endregion Implementation of IWiseCrmService
	}
}