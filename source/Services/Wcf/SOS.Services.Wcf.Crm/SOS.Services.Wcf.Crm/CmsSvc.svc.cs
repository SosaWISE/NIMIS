using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Lib.RestCake;
using SOS.Lib.RestCake.Attributes;
using SOS.Services.Interfaces;
using SOS.Services.Interfaces.Models;
using SOS.Services.Wcf.Crm.Helper;
using iTextSharp.text.pdf;

namespace SOS.Services.Wcf.Crm
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CmsSvc" in code, svc and config file together.
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	[RestService(Namespace = "SOS", ServiceContract = typeof(ICmsSvc))]
	public class CmsSvc : RestHttpHandler, ICmsSvc
	{
		public void DoWork()
		{
		}

		#region Implementation of ICmsSvc

		public SosResult<List<CmsModels.McAddressType>> McAddressTypeGetAll()
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAddressType>>((int)SosResultCodes.GeneralError
				, "Not Implemented", typeof(List<CmsModels.McAddressType>).ToString());

			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.McAddressStatus>> McAddressStatusGetAll()
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAddressStatus>>((int)SosResultCodes.GeneralError
				, "Not Implemented", typeof(List<CmsModels.McAddressStatus>).ToString());

			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.McAddressValidationVendors>> McAddressValidationVendorGetAll()
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAddressValidationVendors>>((int)SosResultCodes.GeneralError
				, "Not Implemented", typeof(List<CmsModels.McAddressValidationVendors>).ToString());

			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.McAddress>> McAddressGetByPK(long lAddressID)
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAddress>>((int)SosResultCodes.GeneralError
				, "Not Implemented", typeof(List<CmsModels.McAddress>).ToString());

			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.McAddress>> McAddressUpdate(long lAddressID, string szValidationVendorId, string szAddressStatusId, string szStateId, string szCountryId, int nTimeZoneId, char cAddressTypeId, string szStreetAddress, string szStreetAddress2, string szStreetNumber, string szStreetName, string szStreetType, string szPreDirectional, string szPostDirectional, string szExtension, string szExtensionNumber, string szCounty, string szCountyCode, string szUrbanization, string szUrbanizationCode, string szCity, string szPostalCode, string szPlusFour, string szDeliveryPoint, float fLatitude, float fLongitude, int nCongressionalDistric, bool bDPV, string szDPVResponse, string szDPVFootNote, string szCarrierRoute)
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAddress>>((int)SosResultCodes.GeneralError
				, "Not Implemented", typeof(List<CmsModels.McAddress>).ToString());

			/** Return result. */
			return oResult;
		}

		public SosResult<CmsModels.QlLeadBasicView> QlLeadBasicCreate(int nDealerId, string szFirstName, string szLastName, string szAddress, string szCity, string szState, string szPostal, string szEmail, string szPremisePhone)
		{
			/** Initialize. */
			var oResult = new SosResult<CmsModels.QlLeadBasicView>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(CmsModels.QlLeadBasicView).ToString());

			/** Get new Session from database. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			try
			{
				IFnsResult<IFnsLeadModel> oSrvResult = oService.CreateLeadBasic(nDealerId, szFirstName, szLastName, szAddress, szCity, szState, szPostal, szEmail,
										szPremisePhone, null);
				/** Validate services result. */
				if (oSrvResult == null)
				{
					return new SosResult<CmsModels.QlLeadBasicView>((int)SosResultCodes.GeneralError
							, string.Format("No value was returned.")
							, null)
						{ Value = null };
				}
				/** Check that there was an error code returned. */
				if (oSrvResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<CmsModels.QlLeadBasicView>(oSrvResult.Code
						, oSrvResult.Message
						, null);
				}

				/** Bind result from service to return service. */
				var oLeadModel = (IFnsLeadModel)oSrvResult.GetValue();
				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				oResult.Value = new CmsModels.QlLeadBasicView
					{
						LeadID = oLeadModel.LeadID,
						AddressId = oLeadModel.AddressId,
						CustomerMasterFileId = oLeadModel.CustomerMasterFileId,
						CustomerTypeId = oLeadModel.CustomerTypeId,
						DealerId = oLeadModel.DealerId,
						SalesRepId = oLeadModel.SalesRepId,
						TeamLocationId = oLeadModel.TeamLocationId,
						LocalizationId = oLeadModel.LocalizationId,
						SeasonId = oLeadModel.SeasonId,
						DL = oLeadModel.DL,
						DLStateID = oLeadModel.DLStateID,
						DOB = oLeadModel.DOB,
						Email =  oLeadModel.Email,
						FirstName = oLeadModel.FirstName,
						MiddleName = oLeadModel.MiddleName,
						LastName = oLeadModel.LastName,
						Suffix = oLeadModel.Suffix,
						PhoneHome = oLeadModel.PhoneHome,
						PhoneMobile = oLeadModel.PhoneMobile,
						PhoneWork = oLeadModel.PhoneWork,
						PremisePhone = oLeadModel.PremisePhone,
						StreetAddress = oLeadModel.StreetAddress,
						City = oLeadModel.City,
						StateId = oLeadModel.StateId,
						Postal = oLeadModel.Postal,
						IsActive = oLeadModel.IsActive
					};
			}
			catch (Exception oEx)
			{
				return new SosResult<CmsModels.QlLeadBasicView>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
					, typeof(CmsModels.QlLeadBasicView).ToString()) { Value = null };
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.QlLeadSearchResultView>> QlSearch(string szFirstName, string szLastName, string szPhone, int? nDealerId, string szEmail, int? nLeadId, int? nDispositionId, int? nSourceId, int nPageSize, int nPageNumber)
		{
			/** Check authentication. */
			var oResult = new SosResult<List<CmsModels.QlLeadSearchResultView>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<CmsModels.QlLeadSearchResultView>).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialization */
			oResult = new SosResult<List<CmsModels.QlLeadSearchResultView>>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(List<CmsModels.QlLeadSearchResultView>).ToString());
			szPhone = Lib.Util.StringUtility.TrimPhoneNumber(szPhone);

			/** Execute search. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			try
			{
				IFnsResult<List<IFnsLeadSearchResult>> oSrvResult = oService.QlSearch(szFirstName, szLastName, szPhone, nDealerId, szEmail, nLeadId, nDispositionId, nSourceId, nPageSize, nPageNumber);
				/** Validate services result. */
				if (oSrvResult == null)
				{
					return new SosResult<List<CmsModels.QlLeadSearchResultView>>((int)SosResultCodes.GeneralError
							, string.Format("No value was returned.")
							, null) { Value = null };
				}
				/** Check for a result. */
				if (oSrvResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<List<CmsModels.QlLeadSearchResultView>>(oSrvResult.Code
						, oSrvResult.Message
						, null);
				}

				/** Build list result. */
				var oItemsList = (List<IFnsLeadSearchResult>)oSrvResult.GetValue();

				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				var oResultsList = oItemsList.Select(oItem => new CmsModels.QlLeadSearchResultView
						{
							CustomerMasterFileId = oItem.CustomerMasterFileId,
							LeadId = oItem.LeadId,
							DealerId = oItem.DealerId,
							LocalizationId = oItem.LocalizationId,
							DispositionId = oItem.DispositionId,
							Disposition = oItem.Disposition,
							SourceId = oItem.SourceId,
							Source = oItem.Source,
							FirstName = oItem.FirstName,
							LastName = oItem.LastName,
							PhoneHome = oItem.PhoneHome,
							PhoneWork = oItem.PhoneWork,
							PhoneMobile = oItem.PhoneMobile,
							DOB = oItem.DOB,
							SalesRepId = oItem.SalesRepId,
							SSN = oItem.SSN,
							DL = oItem.DL,
							DLStateID = oItem.DLStateID,
							Email = oItem.Email,
							IsCustomer = oItem.IsCustomer,
							RowNum = oItem.RowNum
						}).ToList();

				/** Save list result. */
				oResult.Value = oResultsList;
			}
			catch (Exception oEx)
			{
				return new SosResult<List<CmsModels.QlLeadSearchResultView>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(List<CmsModels.QlLeadSearchResultView>).ToString());
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<CmsModels.QlLeadFullData> QlGetLeadFull(long lLeadId, long lCustomerMasterFileId, bool bNoteAccount)
		{
			/** Check authentication. */
			var oResult = new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.QlLeadFullData).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialization */
			oResult = new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(CmsModels.QlLeadFullData).ToString());

			/** Execute search. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			#region BEGIN TRY
			try
			{
				IFnsResult<IFnsLeadFullDataModel> oSrvResult = oService.QlGetLeadFull(lLeadId, lCustomerMasterFileId, oUser.DealerId, oUser.Username, bNoteAccount);
				/** Validate services result. */
				if (oSrvResult == null)
				{
					return new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.GeneralError
							, string.Format("No value was returned.")
							, null) { Value = null };
				}
				/** Check for a result. */
				if (oSrvResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<CmsModels.QlLeadFullData>(oSrvResult.Code
						, oSrvResult.Message
						, null);
				}
				/** Build result. */
				var oItem = (IFnsLeadFullDataModel)oSrvResult.GetValue();
				// Bind data 
				CmsModels.QlLeadFullData oResultValue = CmsSvcHelper.BindToQlLeadFullData(oItem);

				/** Save list result. */
				oResult.Code = (int)SosResultCodes.Success;
				oResult.Message = "Successfully acquired full lead data.";
				oResult.Value = oResultValue;
			}
			#endregion END TRY
			#region BEGIN CATCH
			catch (Exception oEx)
			{
				return new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.QlLeadFullData).ToString());
			}
			#endregion END CATCH

			/** Return result. */
			return oResult;
		}

		public SosResult<CmsModels.QlLeadFullData> QlLeadCreateUpdate(CmsModels.QlLeadFullData oLeadFullData)
		{
			/** Authenticate user. */
			var oResult = new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.QlLeadFullData).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialization */
			oResult = new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(CmsModels.QlLeadFullData).ToString());
			/** Set some bogus LeadID.
			oLeadFullData.LeadID = 9999999;
			oResult.Value = oLeadFullData;
			 */

			/** Init Service Engine. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			try
			{
				FunctionalServices.Models.FnsLeadFullDataModel oFnsLeadFullDataModel = CmsSvcHelper.BindToLeadFullDataModel(oLeadFullData);

				/** Pass the correct data to the calling method. */
				var oServiceResult = oService.QlCreateUpdateLeadFull(oFnsLeadFullDataModel, oUser.SalesRepId);

				/** Check that the operation worked. */
				if (oServiceResult.Code > 0)
				{
					oResult.Code = oServiceResult.Code;
					oResult.Message = oServiceResult.Message;
					return oResult;
				}
				/** Build result. */
				var oItem = (IFnsLeadFullDataModel)oServiceResult.GetValue();
				// Bind data 
				CmsModels.QlLeadFullData oResultValue = CmsSvcHelper.BindToQlLeadFullData(oItem);

				/** Save list result. */
				oResult.Code = (int)SosResultCodes.Success;
				oResult.Message = "Successfully created/updated full lead data.";
				oResult.Value = oResultValue;
			}
			catch (Exception oEx)
			{
				return new SosResult<CmsModels.QlLeadFullData>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.QlLeadFullData).ToString());
			}


			/** Return result. */
			return oResult;
		}

		public SosResult<List<CmsModels.McAccountNotesFullInfoView>> QlGetAccountNotesByID(long? lLeadId, long? lCustomerId, long? lCMFId)
		{
			/** Initialize. */
			var oResult = new SosResult<List<CmsModels.McAccountNotesFullInfoView>>((int)SosResultCodes.GeneralError
				, "Initilizing", typeof(List<CmsModels.McAccountNotesFullInfoView>).ToString());
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute. */
			#region Try
			try
			{
				IFnsResult<List<IFnsMcAccountNotesFull>> oSrvResult = oService.FindAccountNotesByAnyID(lLeadId, lCustomerId, lCMFId);
				/** Validate services result. */
				if (oSrvResult == null)
				{
					return new SosResult<List<CmsModels.McAccountNotesFullInfoView>>((int)SosResultCodes.GeneralError
							, string.Format("No value was returned.")
							, null) { Value = null };
				}
				/** Check for a result. */
				if (oSrvResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<List<CmsModels.McAccountNotesFullInfoView>>(oSrvResult.Code
						, oSrvResult.Message
						, null);
				}

				/** Build list result. */
				var oItemsList = (List<IFnsMcAccountNotesFull>) oSrvResult.GetValue();

				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				var oResultList = oItemsList.Select(oItem => new CmsModels.McAccountNotesFullInfoView
										{
											NoteID = oItem.NoteID,
											NoteTypeID = oItem.NoteTypeID,
											NoteType = oItem.NoteType,
											CustomerMasterFileId = oItem.CustomerMasterFileId,
											CustomerId = oItem.CustomerId,
											LeadId = oItem.LeadId,
											NoteCategory1ID = oItem.NoteCategory1ID,
											Category1 = oItem.Category1,
											Desc1 = oItem.Desc1,
											NoteCategory2ID = oItem.NoteCategory2ID,
											Category2 = oItem.Category2,
											Desc2 = oItem.Desc2,
											Note = oItem.Note,
											CreatedBy = oItem.CreatedBy,
											CreatedOn = oItem.CreatedOn,
										}).ToList();

				/** Save result. */
				oResult.Value = oResultList;
			}
			#endregion Try
			catch (Exception oEx)
			{
				return new SosResult<List<CmsModels.McAccountNotesFullInfoView>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(List<CmsModels.McAccountNotesFullInfoView>).ToString());
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<OptionItemModel> OptionItemAdd(string szListName, string szItemName)
		{
			/** Authenticate user. */
			var oResult = new SosResult<OptionItemModel>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(OptionItemModel).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<OptionItemModel>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(OptionItemModel).ToString());
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute request. */
			#region Try
			try
			{
				/** Init. */
				IFnsResult<IFnsOptionItem> oItemResult;
				switch (szListName)
				{
					case "Disposition":
						oItemResult = oService.DispositionListAddByDealerId(oUser.DealerId, szItemName, oUser.Username);
						break;
					case "Source":
						oItemResult = oService.SourceListAddByDealerId(oUser.DealerId, szItemName, oUser.Username);
						break;
					default:
						return new SosResult<OptionItemModel>((int)SosResultCodes.GeneralError
								, string.Format("Argument value '{0}' not supported.", szListName)
								, typeof(OptionItemModel).ToString());
				}

				/** Check result value. */
				if (oItemResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<OptionItemModel>(oItemResult.Code
						, oItemResult.Message
						, typeof(OptionItemModel).ToString());
				}

				/** Convert to the returning value. */
				/** Init. */
				var oFnsItem = (IFnsOptionItem) oItemResult.GetValue();
				var oItem = new OptionItemModel(oFnsItem.Value, oFnsItem.Text);
				oResult.Value = oItem;

				/** Return success. */
				oResult.Code = (int)SosResultCodes.Success;
				oResult.Message = "Success";

			}
			#endregion Try
			#region Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<OptionItemModel>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}", oEx.Message)
					, typeof(OptionItemModel).ToString());
			}
			#endregion Catch

			/** Return result. */
			return oResult;
		}

		public SosResult<List<OptionItemModel>> OptionItemsGet(string szListName)
		{
			/** Authenticate user. */
			var oResult = new SosResult<List<OptionItemModel>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<OptionItemModel>).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<List<OptionItemModel>>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(List<OptionItemModel>).ToString());
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute request. */
			#region Try
			try
			{
				/** Init. */
				IFnsResult<List<IFnsOptionItem>> oListResult;
				switch (szListName)
				{
					case "Disposition":
						oListResult = oService.DispositionListGetByDealerId(oUser.DealerId);
						break;
					case "Source":
						oListResult = oService.SourceListGetByDealerId(oUser.DealerId);
						break;
					case "AppointmentType":
						oListResult = oService.AppointmentMetaDataListGet("AppointmentType");
						break;
					case "ReminderDelayType":
						oListResult = oService.AppointmentMetaDataListGet("ReminderDelayType");
						break;
					case "ReminderMediaType":
						oListResult = oService.AppointmentMetaDataListGet("ReminderMediaType");
						break;
					default:
						return new SosResult<List<OptionItemModel>>((int)SosResultCodes.GeneralError
								, string.Format("Argument value '{0}' not supported.", szListName)
								, typeof(List<OptionItemModel>).ToString());
				}

				/** Check result value. */
				if (oListResult.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<List<OptionItemModel>>(oListResult.Code
						, oListResult.Message
						, typeof(List<OptionItemModel>).ToString());
				}

				/** Convert to the returning value. */
				oResult.Value = new List<OptionItemModel>();
				foreach (var oFnsItem in (List<IFnsOptionItem>)oListResult.GetValue())
				{
					/** Init. */
					var oItem = new OptionItemModel(oFnsItem.Value, oFnsItem.Text);
					oResult.Value.Add(oItem);
				}

				/** Return success. */
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Success";

			}
			#endregion Try
			#region Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<List<OptionItemModel>>((int) SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}", oEx.Message)
					, typeof (List<OptionItemModel>).ToString());
			}
			#endregion Catch

			/** Return result. */
			return oResult;
		}

		public SosResult<CmsModels.McAccountNotesFullInfoView> McAccountNotesCreate(CmsModels.McAccountNote oNote)
		{
			/** Authenticate user. */
			var oResult = new SosResult<CmsModels.McAccountNotesFullInfoView>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.McAccountNotesFullInfoView).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<CmsModels.McAccountNotesFullInfoView>((int)SosResultCodes.GeneralError
				, "Initilizing", typeof(CmsModels.McAccountNotesFullInfoView).ToString());

			/** Init Service Engine. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			try
			{
				/** Convert passed model to the FOS Model. */
				FunctionalServices.Models.Cms.FnsMcAccountNoteModel oNoteModel = CmsSvcHelper.BindToFosMcAccountNoteModel(oNote);

				/** Pass the right datatype and execute the Create. */
				var oServiceResult = oService.McAccountNoteCreate(oNoteModel, oUser.Username);

				/** Check the result of the call. */
				if (oServiceResult.Code > 0)
				{
					oResult.Code = oServiceResult.Code;
					oResult.Message = oServiceResult.Message;
					return oResult;
				}

				/** Build result and return it. */
				//TODO: This does not return anything.
			}
			catch (Exception oEx)
			{
				return new SosResult<CmsModels.McAccountNotesFullInfoView>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.McAccountNotesFullInfoView).ToString());
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<CmsModels.AeInvoice> AeCreateNewCustomer(CmsModels.AePaymentInformationCreateAccountModel oPaymentInformationCreateAccountModel)
		{
			/** Authenticate user. */
			var oResult = new SosResult<CmsModels.AeInvoice>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.AeInvoice).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<CmsModels.AeInvoice>((int)SosResultCodes.GeneralError
				, "Initilizing", typeof(CmsModels.AeInvoice).ToString());

			/** Initialize services. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			oPaymentInformationCreateAccountModel.DealerId = oUser.DealerId; // Get the dealer from the user.

			/** Process Payment. */
			try
			{
				/** Process Payment. */
				IFnsAePaymentInformationCreateAccountModel oFnsPaymentInfo = CmsSvcHelper.BindToFnsAePaymentInformationCreateAccountModel(oPaymentInformationCreateAccountModel);
				IFnsResult<IFnsAePaymentFull> oFosResponse = oService.AeCustomerCreateFromLead(oFnsPaymentInfo, oUser.Username);

				// ** Check the result
				if (oFosResponse.Code > (int)SosResultCodes.Success)
				{
					return new SosResult<CmsModels.AeInvoice>(oFosResponse.Code
						, oFosResponse.Message
						, typeof(CmsModels.AeInvoice).ToString());
				}

				/** Generate PDF. */
				var oAePaymentFull = (IFnsAePaymentFull)oFosResponse.GetValue();
				var pdfReader = new PdfReader(Request.MapPath("~/Assets/PdfTemplates/SalesReceipt.pdf"));
				var szFilePath = Request.MapPath(string.Format("~/Assets/Output/Invoices/{0}_{1}.pdf"
					, oAePaymentFull.InvoiceID, oAePaymentFull.PaymentID));
				szFilePath = szFilePath.Replace(" ", "");
				var oFileStream = new FileStream(szFilePath, FileMode.CreateNew);

				var pdfStamper = new PdfStamper(pdfReader, oFileStream)
					{
						FormFlattening = true // generate a flat PDF
					};
				AcroFields pdfForm = pdfStamper.AcroFields;
				pdfForm.SetField("Invoice ID", oAePaymentFull.InvoiceNumber);
				pdfForm.SetField("Purchase Date", oAePaymentFull.DocDate.ToShortDateString());
				pdfForm.SetField("Customer ID", string.Format("{0}-{1}"
					, oPaymentInformationCreateAccountModel.LeadId
					, oPaymentInformationCreateAccountModel.CustomerMasterFileId));
				pdfForm.SetField("Customer Name", oAePaymentFull.BillTo.CustomerName);
				pdfForm.SetField("Customer Address 1", oAePaymentFull.BillTo.AddressLine1);
				pdfForm.SetField("Customer Address 2", oAePaymentFull.BillTo.AddressLine2);
				pdfForm.SetField("Customer Phone", oAePaymentFull.BillTo.Phone);
				pdfForm.SetField("Sold To Name", oAePaymentFull.SoldTo.CustomerName);
				pdfForm.SetField("Sold To Address 1", oAePaymentFull.SoldTo.AddressLine1);
				pdfForm.SetField("Sold To Address 2", oAePaymentFull.SoldTo.AddressLine2);
				pdfForm.SetField("Sold To Phone", oAePaymentFull.SoldTo.Phone);

				pdfForm.SetField("Payment Method", oAePaymentFull.PaymentMethod); //"Credit Card (VISA)"
				pdfForm.SetField("Check Number", oAePaymentFull.CardNumber); // This should be a CC# or a Bank Routing Number with Account#
				pdfForm.SetField("Job", oAePaymentFull.PurchaseMessageDescription); //"Purchase a new Product"
				foreach (IFnsAePaymentItem oAePaymentIem in oAePaymentFull.ItemsList)
				{
					pdfForm.SetField("Qty Line 1", oAePaymentIem.Qty.ToString(CultureInfo.InvariantCulture).PadLeft(3,'0'));
					pdfForm.SetField("Item Skw 1", oAePaymentIem.Skw);
					pdfForm.SetField("Desc Line 1", oAePaymentIem.LineDescription);
					pdfForm.SetField("Unit Price 1", string.Format("{0:C}", oAePaymentIem.UnitPrice));
					pdfForm.SetField("Discount Line 1", string.Format("{0:C}", oAePaymentIem.DiscountPrice));
					pdfForm.SetField("Line Total 1", string.Format("{0:C}", oAePaymentIem.TotalLine));
				}
				pdfForm.SetField("SUM TOTAL", string.Format("{0:C}", oAePaymentFull.TotalSum));
				pdfForm.SetField("TOTAL DISCOUNT", string.Format("{0:C}", oAePaymentFull.TotalDiscount));
				pdfForm.SetField("SUBTOTAL TOTAL", string.Format("{0:C}", oAePaymentFull.TotalSub));
				pdfForm.SetField("SUBTOTAL SALES TAX TOTAL", string.Format("{0:C}", oAePaymentFull.TaxAmount));
				pdfForm.SetField("TOTAL SALE", string.Format("{0:C}", oAePaymentFull.SalesAmount));

				pdfStamper.Close();

				/** Return result. */
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new CmsModels.AeInvoice{InvoiceFilePath = string.Format("Assets/Output/Invoices/{0}_{1}.pdf"
					, oAePaymentFull.InvoiceID, oAePaymentFull.PaymentID)
				};
			}
			catch (Exception oEx)
			{
				return new SosResult<CmsModels.AeInvoice>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeInvoice).ToString());
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<List<McModels.DealerUser>> DealerUsersGet()
		{
			/** Authenticate user. */
			var oResult = new SosResult<List<McModels.DealerUser>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<McModels.DealerUser>).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<List<McModels.DealerUser>>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(List<McModels.DealerUser>).ToString());
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute request. */
			#region Try
			try
			{
				/** Initialize. */
				IFnsResult<List<IFnsMcDealerUser>> oFnsList = oService.DealerUsersGet(oUser.DealerId);
				var oResultValue = (from oItem in (
					List<IFnsMcDealerUser>) oFnsList.GetValue() 
									select McModelHelper.CastMcDealerUserFromFns(oItem)).ToList();

				/** Package result and return. */
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oResultValue;
			}
			#endregion Try
			#region Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<List<McModels.DealerUser>>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}", oEx.Message)
					, typeof(List<McModels.DealerUser>).ToString());
			}
			#endregion Catch

			/** Return result. */
			return oResult;
		}

		public SosResult<bool> LeadDispositionUpdate(int nLeadID, int nLeadDispositionId)
		{
			/** Authenticate user. */
			var oResult = new SosResult<bool>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(bool).ToString())
				{ Value = false};
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			oResult = new SosResult<bool>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(bool).ToString())
				{ Value = false };

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute request. */
			#region Try
			try
			{
				/** Initialize. */
				IFnsResult<bool> oFnsList = oService.LeadDispositionUpdate(oUser.DealerId, nLeadID, nLeadDispositionId, oUser.Username);

				/** Package result and return. */
				oResult.Code = oFnsList.Code;
				oResult.Message = oFnsList.Message;
				oResult.Value = (bool) oFnsList.GetValue();

			}
			#endregion Try
			#region Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<bool>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}", oEx.Message)
					, typeof(bool).ToString()){ Value = false };
			}
			#endregion Catch

			/** Return result. */
			return oResult;
		}

		public List<CaFullCalenderAppointmentModel> DealerUserAppointmentsGet(string sStart, string sEnd)
		{
			#region INITIALIZATION
			/** Initialize. */
			var oResult = new List<CaFullCalenderAppointmentModel>();
			/** Authenticate user. */
			//var oResult = new SosResult<bool>((int)SosResultCodes.CookieInvalid
			//    , "Validating Authentication Failed", typeof(bool).ToString()) { Value = false };
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialize. */
			//oResult = new SosResult<bool>((int)SosResultCodes.GeneralError
			//    , "Initializing", typeof(bool).ToString()) { Value = false };

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			#endregion INITIALIZATION
			#region Try

			try
			{
				double mStartUnixDate, mEndUnixDate;
				if (!double.TryParse(sStart, out mStartUnixDate)) return oResult;
				if (!double.TryParse(sEnd, out mEndUnixDate)) return oResult;
				DateTime dStartDate = Lib.Util.StringHelper.UnixTimeStampToDateTime(mStartUnixDate);
				DateTime dEndDate = Lib.Util.StringHelper.UnixTimeStampToDateTime(mEndUnixDate);

				IFnsResult<List<IFnsCaAppointmentModel>> oFnsResult = oService.DealerUserAppointmentsGet(oUser.DealerUserId, dStartDate, dEndDate);

				/** Check result. */
				if (oFnsResult.Code != (int)SosResultCodes.Success) return oResult;

				/** Cast to returning type. */
				oResult.AddRange(from fnsApointment in (List<IFnsCaAppointmentModel>) oFnsResult.GetValue() select CaModelHelper.CastToCaFullCalendarAppointmentModel(fnsApointment));
			}
				#endregion Try
			#region Catch
			catch (Exception oEx)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("The following exception was thrown: {0}"
					, oEx.Message));
			}
			#endregion Catch

			/** Return result. */
			return oResult;
		}

		public List<CaFullCalenderAppointmentModel> AeGetCustomerFull(long lCustomerId, long lCustomerMasterFileId, bool bNoteAccount = false)
		{
			return null;
		}

		#endregion Implementation of ICmsSvc
	}
}