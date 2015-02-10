using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using SOS.Clients.MVC.CorpSite2.Helpers;
using SOS.Framework.Mvc.ActionResults;
using SOS.Framework.Mvc.Controllers;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.Lib.Util.Extensions;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.GpsTracking;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
    public class ClientGpsTrackSrvController : Controller
    {
		#region Private Member Properties

    	private const string _SOS_GPS_CLNT = "SOS_GPS_CLNT";

		#endregion Private Member Properties

		//
		// GET: /ClientGpsTrackSrv/
		#region Need to Organize
		public JsonpResult Authenticate(string szUsername, string szPassword, long lSessionID)
        {
			/** Authenticate user. */
			var oResult = new SosResult<CmsModels.AeCustomer>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.AeCustomer).ToString());
			//SosUser oUser;
			// Check user
			//if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, out oUser)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			/** Execute authentication. */
        	try
        	{
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
        		IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsModel = oService.AeCustomerAuthenticate(szUsername, szPassword);

				/** Check result. */
				if (oFnsModel.Code != 0)
				{
					oResult.Code = oFnsModel.Code;
					oResult.Message = oFnsModel.Message;
					return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
				}

				/** Setup return result. */
				var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel) oFnsModel.GetValue());

				/** Save session cookie. */
        		var oSosCustomer = new SosCustomer
        		    {
        		        CustomerID = oAeCustomer.CustomerID
						, SessionID = lSessionID
						, CustomerMasterFileId = oAeCustomer.CustomerMasterFileId
						, CustomerTypeId = oAeCustomer.CustomerTypeId
						, DealerId = oAeCustomer.DealerId
						, LocalizationId = oAeCustomer.LocalizationId
						, Prefix = oAeCustomer.Prefix
						, Firstname = oAeCustomer.FirstName
						, MiddleName = oAeCustomer.MiddleName
						, Lastname = oAeCustomer.LastName
						, Postfix = oAeCustomer.Postfix
						, Gender = oAeCustomer.Gender
						, PhoneHome = oAeCustomer.PhoneHome
						, PhoneWork = oAeCustomer.PhoneWork
						, PhoneCell = oAeCustomer.PhoneMobile
						, Email = oAeCustomer.Email
						, DOB = oAeCustomer.DOB
						, SSN = oAeCustomer.SSN
						, Username = oAeCustomer.Username
        		    };
        		SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

        		oResult.Code = (int) SosResultCodes.Success;
        		oResult.Message = "Success";
				oResult.Value = oAeCustomer;

        	}
        	catch (Exception oEx)
        	{
				oResult = new SosResult<CmsModels.AeCustomer>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeCustomer).ToString());
        	}

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
        }

		public JsonpResult GetListOfDevicesByCMFID(long lCMFID)
		{
			/** Authenticate. */
			var oResult = new SosResult<List<CmsModels.MsAccountClientsView>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.AeCustomer).ToString());
			SosCustomer oCustomer;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oCustomer)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			/** Execute request.*/
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<List<IFnsMsAccountClientsView>> oSrvResult = oService.GetDevicesByCMFID(lCMFID, oCustomer.Username);

				/** Check result. */
				if (oSrvResult.Code != 0)
				{
					oResult.Code = oSrvResult.Code;
					oResult.Message = oSrvResult.Message;
					return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
				}

				/** Bundle result for return. */
				List<CmsModels.MsAccountClientsView> oResultList = (from oItem in (List<IFnsMsAccountClientsView>)oSrvResult.GetValue()
				        select new CmsModels.MsAccountClientsView
				                {
				                    CustomerMasterFileId = oItem.CustomerMasterFileId
									, CustomerID = oItem.CustomerID
									, AccountId = oItem.AccountId
									, UnitID = oItem.UnitID
									, Username = oItem.Username
									, Password = oItem.Password
									, CustomerTypeId = oItem.CustomerTypeId
									, SystemTypeId = oItem.SystemTypeId
									, PanelTypeId = oItem.PanelTypeId
									, InvItemId = oItem.InvItemId
									, IndustryAccountId = oItem.IndustryAccountId
									, IndustryNumber = oItem.IndustryNumber
									, Designator = oItem.Designator
									, SubscriberNumber = oItem.SubscriberNumber
				                }).ToList();
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Successful";
				oResult.Value = oResultList;
			}
			catch (Exception oEx)
			{
				oResult = new SosResult<List<CmsModels.MsAccountClientsView>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeCustomer).ToString());
			}

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}

		public JsonpResult GetListOfDevicesByCustomerID(long lCustomerID)
		{
			/** Authenticate. */
			var oResult = new SosResult<List<CmsModels.MsAccountClientsView>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<CmsModels.MsAccountClientsView>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			/** Execute request.*/
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<List<IFnsMsAccountClientsView>> oSrvResult = oService.GetDevicesByCustomerID(lCustomerID, oUser.Username);

				/** Check result. */
				if (oSrvResult.Code == 0)
				{
					oResult.Code = oSrvResult.Code;
					oResult.Message = oSrvResult.Message;
					return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
				}

				/** Bundle result for return. */
				List<CmsModels.MsAccountClientsView> oResultList = (from oItem in (List<IFnsMsAccountClientsView>)oSrvResult.GetValue()
				        select new CmsModels.MsAccountClientsView
				                {
				                    CustomerMasterFileId = oItem.CustomerMasterFileId
									, CustomerID = oItem.CustomerID
									, AccountId = oItem.AccountId
									, UnitID = oItem.UnitID
									, Username = oItem.Username
									, Password = oItem.Password
									, CustomerTypeId = oItem.CustomerTypeId
									, SystemTypeId = oItem.SystemTypeId
									, PanelTypeId = oItem.PanelTypeId
									, IndustryAccountId = oItem.IndustryAccountId
									, IndustryNumber = oItem.IndustryNumber
									, Designator = oItem.Designator
									, SubscriberNumber = oItem.SubscriberNumber
				                }).ToList();
				oResult.Value = oResultList;
			}
			catch (Exception oEx)
			{
				oResult = new SosResult<List<CmsModels.MsAccountClientsView>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeCustomer).ToString());
			}

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}

		public JsonpResult GetDeviceDetails(long lAccountID, long lCustomerID)
		{
			/** Authenticate. */
			var oResult = new SosResult<CmsModels.MsAccountClientDetailsView>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.MsAccountClientDetailsView).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			/** Execute request.*/
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<IFnsMsAccountClientDetailsView> oSrvResult = oService.GetDeviceDetailsByAccountID(lAccountID, lCustomerID, oUser.Username);

				/** Check result. */
				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				if (oSrvResult.Code == 0)
				{
					var oSrvValue = (IFnsMsAccountClientDetailsView) oSrvResult.GetValue();
					var oModel = new CmsModels.MsAccountClientDetailsView
					             	{
					             		CustomerAccountID = oSrvValue.CustomerAccountID,
					             		CustomerId = oSrvValue.CustomerId,
					             		AccountId = oSrvValue.AccountId,
					             		CustomerTypeId = oSrvValue.CustomerTypeId,
					             		CustomerMasterFileId = oSrvValue.CustomerMasterFileId,
					             		DealerId = oSrvValue.DealerId,
					             		AddressId = oSrvValue.AddressId,
										StreetAddress = oSrvValue.StreetAddress,
										StreetAddress2 = oSrvValue.StreetAddress2,
										City = oSrvValue.City,
										StateId = oSrvValue.StateId,
										PostalCode = oSrvValue.PostalCode,
										PlusFour = oSrvValue.PlusFour,
										County = oSrvValue.County,
										CountryId = oSrvValue.CountryId,
					             		LeadId = oSrvValue.LeadId,
					             		LocalizationId = oSrvValue.LocalizationId,
					             		Prefix = oSrvValue.Prefix,
					             		FirstName = oSrvValue.FirstName,
					             		MiddleName = oSrvValue.MiddleName,
					             		LastName = oSrvValue.LastName,
					             		Postfix = oSrvValue.Postfix,
					             		Gender = oSrvValue.Gender,
					             		PhoneHome = oSrvValue.PhoneHome,
					             		PhoneWork = oSrvValue.PhoneWork,
					             		PhoneMobile = oSrvValue.PhoneMobile,
					             		Email = oSrvValue.Email,
					             		DOB = oSrvValue.DOB,
					             		SSN = oSrvValue.SSN,
					             		Username = oSrvValue.Username,
					             		Password = oSrvValue.Password,
					             		CustomerIsActive = oSrvValue.CustomerIsActive,
					             		IndustryAccountId = oSrvValue.IndustryAccountId,
					             		SystemTypeId = oSrvValue.SystemTypeId,
					             		CellularTypeId = oSrvValue.CellularTypeId,
					             		PanelTypeId = oSrvValue.PanelTypeId,
					             		SimProductBarcodeId = oSrvValue.SimProductBarcodeId,
					             		GpsWatchProductBarcodeId = oSrvValue.GpsWatchProductBarcodeId,
					             		GpsWatchPhoneNumber = oSrvValue.GpsWatchPhoneNumber,
					             		MsAccountIsActive = oSrvValue.MsAccountIsActive,
					             		IndustryNumber = oSrvValue.IndustryNumber,
					             		Designator = oSrvValue.Designator,
					             		SubscriberNumber = oSrvValue.SubscriberNumber
					             	};

					/** Get value for result. */
					oResult.SessionId = oUser.SessionID;
					oResult.Value = oModel;
				}

			}
			catch (Exception oEx)
			{
				oResult = new SosResult<CmsModels.MsAccountClientDetailsView>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeCustomer).ToString());
			}

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}

		//[AllowCrossDomain]
		public JsonResult GetDeviceDetailsJson(long lAccountID, long lCustomerID)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<CmsModels.MsAccountClientDetailsView>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.MsAccountClientDetailsView).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) 
				return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication

			#region TRY
			/** Execute request.*/
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<IFnsMsAccountClientDetailsView> oSrvResult = oService.GetDeviceDetailsByAccountID(lAccountID, lCustomerID, oUser.Username);

				/** Check result. */
				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				if (oSrvResult.Code == 0)
				{
					var oSrvValue = (IFnsMsAccountClientDetailsView)oSrvResult.GetValue();
					var oModel = new CmsModels.MsAccountClientDetailsView
					#region Bind Data To Model
					{
						CustomerAccountID = oSrvValue.CustomerAccountID,
						CustomerId = oSrvValue.CustomerId,
						AccountId = oSrvValue.AccountId,
						CustomerTypeId = oSrvValue.CustomerTypeId,
						CustomerMasterFileId = oSrvValue.CustomerMasterFileId,
						DealerId = oSrvValue.DealerId,
						AddressId = oSrvValue.AddressId,
						StreetAddress = oSrvValue.StreetAddress,
						StreetAddress2 = oSrvValue.StreetAddress2,
						City = oSrvValue.City,
						StateId = oSrvValue.StateId,
						PostalCode = oSrvValue.PostalCode,
						PlusFour = oSrvValue.PlusFour,
						County = oSrvValue.County,
						CountryId = oSrvValue.CountryId,
						LeadId = oSrvValue.LeadId,
						LocalizationId = oSrvValue.LocalizationId,
						Prefix = oSrvValue.Prefix,
						FirstName = oSrvValue.FirstName,
						MiddleName = oSrvValue.MiddleName,
						LastName = oSrvValue.LastName,
						Postfix = oSrvValue.Postfix,
						Gender = oSrvValue.Gender,
						PhoneHome = oSrvValue.PhoneHome,
						PhoneWork = oSrvValue.PhoneWork,
						PhoneMobile = oSrvValue.PhoneMobile,
						Email = oSrvValue.Email,
						DOB = oSrvValue.DOB,
						SSN = oSrvValue.SSN,
						Username = oSrvValue.Username,
						Password = oSrvValue.Password,
						CustomerIsActive = oSrvValue.CustomerIsActive,
						IndustryAccountId = oSrvValue.IndustryAccountId,
						SystemTypeId = oSrvValue.SystemTypeId,
						CellularTypeId = oSrvValue.CellularTypeId,
						PanelTypeId = oSrvValue.PanelTypeId,
						SimProductBarcodeId = oSrvValue.SimProductBarcodeId,
						GpsWatchProductBarcodeId = oSrvValue.GpsWatchProductBarcodeId,
						GpsWatchPhoneNumber = oSrvValue.GpsWatchPhoneNumber,
						GpsWatchUnitID = oSrvValue.GpsWatchUnitID,
						MsAccountIsActive = oSrvValue.MsAccountIsActive,
						IndustryNumber = oSrvValue.IndustryNumber,
						Designator = oSrvValue.Designator,
						SubscriberNumber = oSrvValue.SubscriberNumber
					};
					#endregion Bind Data To Model

					/** Get value for result. */
					oResult.SessionId = oUser.SessionID;
					oResult.Value = oModel;
				}
			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new SosResult<CmsModels.MsAccountClientDetailsView>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(CmsModels.AeCustomer).ToString());
			}
			#endregion CATCH

			#region Result
			/** Return result. */
			return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Result
		}
		
		public JsonResult GetDeviceFencesJson(long lAccountID)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<List<GsAccountGeoFence>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<GsAccountGeoFence>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) 
				return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication

			#region TRY
			/** Execute request.*/
			try
			{

				/** Get GeoFences. */
				var oServiceGps = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<List<IFnsGeoFencesView>> oSrvGps = oServiceGps.GeoFencesRead(lAccountID);

				oResult.Code = oSrvGps.Code;
				oResult.Message = oSrvGps.Message;
				var oGeoFenceList = new List<GsAccountGeoFence>();
				if (oSrvGps.Code == 0)
				{
					/** Create List for result. */
					foreach (var oFenceItem in (List<IFnsGeoFencesView>)oSrvGps.GetValue())
					{
						/** Initialize. */
						var oPolyPointsList = new List<IGsAccountGeoFencePolygons>();
						/** Check to see if this is a Poly. */
						if (oFenceItem.GeoFenceTypeId.Equals("POLY"))
						{
							oPolyPointsList.AddRange(oFenceItem.PolyPointsList.Select(oPolyPoint => new GsAccountGeoFencePolygons
							{
								GeoFencePolygonID = oPolyPoint.GeoFencePolygonID
								, GeoFenceId = oPolyPoint.GeoFenceId
								, Sequence = oPolyPoint.Sequence
								, Lattitude = oPolyPoint.Lattitude
								, Longitude = oPolyPoint.Longitude
								, CreatedBy = oPolyPoint.CreatedBy
								, CreatedOn = oPolyPoint.CreatedOn
							}));
						}

						/** Create Fence List. */
						oGeoFenceList.Add(new GsAccountGeoFence
						{
						    GeoFenceID = oFenceItem.GeoFenceID
							, GeoFenceTypeId = oFenceItem.GeoFenceTypeId
							, AccountId = oFenceItem.AccountId
							, GeoFenceName = oFenceItem.GeoFenceName
							, GeoFenceDescription = oFenceItem.GeoFenceDescription
							, MeanLattitude = oFenceItem.MeanLattitude
							, MeanLongitude = oFenceItem.MeanLongitude
							, Area = oFenceItem.Area
							, MinLattitude = oFenceItem.MinLattitude
							, MinLongitude = oFenceItem.MinLongitude
							, MaxLattitude = oFenceItem.MaxLattitude
							, MaxLongitude = oFenceItem.MaxLongitude
							, GeoFenceType = oFenceItem.GeoFenceType
							, PointLatitude = oFenceItem.PointLatitude
							, PointLongitude = oFenceItem.PointLongitude
							, CenterLattitude = oFenceItem.CenterLattitude
							, CenterLongitude = oFenceItem.CenterLongitude
							, Radius = oFenceItem.Radius
							, PolyPointsList = oPolyPointsList
						});
					}

					/** Bind the GeoFences to the model object. */
					oResult.Value = oGeoFenceList;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				oResult = new SosResult<List<GsAccountGeoFence>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(List<GsAccountGeoFence>).ToString());
			}
			#endregion CATCH

			#region Result
			/** Return result. */
			return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Result
		}

    	public JsonpResult GetDeviceEvents(long lAccountID, string sStartDate, string sEndDate, int nPageSize = 5, int nPageNumber = 1)
		{
			/** Authenticate. */
			var oResult = new SosResult<List<GsDeviceEvents>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<GsDeviceEvents>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			/** Initialize parameters. */
			DateTime dStartDate;
			if (!DateTime.TryParse(sStartDate, out dStartDate))
			{
				oResult = new SosResult<List<GsDeviceEvents>>((int)SosResultCodes.CookieInvalid
					, string.Format("sStartDate: {0} is not a date.", sStartDate), typeof(List<GsDeviceEvents>).ToString());
			}
			DateTime dEndDate;
			if (!DateTime.TryParse(sEndDate, out dEndDate))
			{
				oResult = new SosResult<List<GsDeviceEvents>>((int)SosResultCodes.CookieInvalid
					, string.Format("sEndDate: {0} is not a date.", sEndDate), typeof(List<GsDeviceEvents>).ToString());
			}
			
			/** Execute Statement. */
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<List<IFnsGsEventsView>> oResultValue = oService.EventDeviceEventsGet(lAccountID, dStartDate, dEndDate.AddDays(1),
				                                                                           nPageSize, nPageNumber);
				/** Check result. */
				if (oResultValue.Code != 0)
				{
					oResult.Code = oResultValue.Code;
					oResult.Message = oResultValue.Message;
					return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
				}

				/** Create result package. */
				var oFnsList = (List<IFnsGsEventsView>) oResultValue.GetValue();
				List<GsDeviceEvents> oModelList = oFnsList.Select(oItem => new GsDeviceEvents
				                                                           	{
																				EventID = oItem.EventID
																				, EventTypeId = oItem.EventTypeId
																				, EventType = oItem.EventType
																				, AccountId = oItem.AccountId
																				, EventName = oItem.EventName
																				, EventDate = oItem.EventDate.ToShortDateString() + ' ' + oItem.EventDate.ToShortTimeString()
																				, Lattitude = oItem.Lattitude
																				, Longitude = oItem.Longitude
				                                                           	}).ToList();
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oModelList;
			}
			catch (Exception oEx)
			{
				oResult = new SosResult<List<GsDeviceEvents>>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(List<GsDeviceEvents>).ToString());
			}
			
			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GeoPointSave(long lGeoFenceID, long lAccountId, string sPlaceName, string sPlaceDescription, double sLattitude, double sLongitude)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(GsAccountGeoFencePoints).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult);
			#endregion Authentication

			#region Parameter Validation
			/** Initialize parameters. */
			bool bValidationFailed = false;
			var sbValidationMessage = new StringBuilder();
			if (lAccountId == 0)
			{
				sbValidationMessage.Append(string.Format("<li>The argument lAccountId with value '{0}' is invalid.</li>", lGeoFenceID));
				bValidationFailed = true;
			}
			if (string.IsNullOrWhiteSpace(sPlaceName))
			{
				sbValidationMessage.Append(string.Format("<li>The argument sPlaceName with value '{0}' is invalid.</li>", sPlaceName));
				bValidationFailed = true;
			}
			if (string.IsNullOrWhiteSpace(sPlaceDescription))
			{
				sbValidationMessage.Append(string.Format("<li>The argument sPlaceDescription with value '{0}' is invalid.</li>", sPlaceName));
				bValidationFailed = true;
			}
			if (sLattitude.IsZero())
			{
				sbValidationMessage.Append(string.Format("<li>The argument sLattitude with value '{0}' is invalid.</li>", sPlaceName));
				bValidationFailed = true;
			}
			if (sLongitude.IsZero())
			{
				sbValidationMessage.Append(string.Format("<li>The argument sLongitude with value '{0}' is invalid.</li>", sPlaceName));
				bValidationFailed = true;
			}

			if (bValidationFailed)
			{
				return Json(new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.ArgumentValidationFailed
					, sbValidationMessage.ToString(), typeof(GsAccountGeoFencePoints).ToString()));
			}

			#endregion Parameter Validation

			#region Execute Try
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<IFnsGsAccountGeoFencePoints> oResultValue = oService.GeoPointCreateUpdate(lGeoFenceID, lAccountId, sPlaceName, sPlaceDescription, sLattitude, sLongitude, oUser.Username);

				oResult.Code = oResultValue.Code;
				oResult.Message = oResultValue.Message;
				if (oResult.Code == (int)SosResultCodes.Success)
				{
					var oFnsValue = (IFnsGsAccountGeoFencePoints) oResultValue.GetValue();
					oResult.Value = new GsAccountGeoFencePoints
					                	{
											GeoFenceId = oFnsValue.GeoFenceID,
											PlaceName = oFnsValue.PlaceName,
											PaceDescription = oFnsValue.PlaceDescription,
											Lattitude = oFnsValue.Lattitude,
											Longitude = oFnsValue.Longitude,
											CreatedBy = oFnsValue.CreatedBy,
											CreatedOn = oFnsValue.CreatedOn
					                	};
				}
			}
			#endregion Execute Try

			#region Execute Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.ExceptionThrown
					, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(GsAccountGeoFencePoints).ToString());
			}
			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result
		}

		public JsonResult GeoPointDelete(long lGeoFencePointID)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(GsAccountGeoFencePoints).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication

			#region Return Result

			/** Return result. */
			return Json(oResult, JsonRequestBehavior.AllowGet);

			#endregion Return Result
		}

		/// <summary>
		/// oArgs = { AccountId: 1000001, CoordList: 
		/// [{ "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 1, "Longitude": "-111.896009", "Lattitude": "40.768454" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 2, "Longitude": "-111.896031", "Lattitude": "40.768324" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 3, "Longitude": "-111.89485", "Lattitude": "40.766228" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 4, "Longitude": "-111.893134", "Lattitude": "40.766017" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 5, "Longitude": "-111.890902", "Lattitude": "40.766553" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 6, "Longitude": "-111.889958", "Lattitude": "40.767772" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 7, "Longitude": "-111.889829", "Lattitude": "40.769868" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 8, "Longitude": "-111.893134", "Lattitude": "40.770323" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 9, "Longitude": "-111.894657", "Lattitude": "40.770258" },
		/// { "GeoFencePolygonID":0, "GeoFenceId": 0, "Sequence": 10, "Longitude": "-111.896009", "Lattitude": "40.768454" }]}
		/// </summary>
		/// <param name="lGeoFenceId"></param>
		/// <param name="lAccountId"></param>
		/// <param name="oCoordList"></param>
		/// <returns></returns>
		public JsonResult GeoPolygonSave(long lGeoFenceId, long lAccountId, ICollection<GsAccountGeoFencePolygons> oCoordList)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<List<FnsGsAccountGeoFencePolygons>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<FnsGsAccountGeoFencePolygons>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult);
			#endregion Authentication

			#region Parameter Validation
			/** Initialize parameters. */
			bool bValidationFailed = false;
			var sbValidationMessage = new StringBuilder();

			if (oCoordList == null)
			{
				sbValidationMessage.Append(string.Format("<li>The argument coordList was not passed.</li>"));
				bValidationFailed = true;
			}

			if (bValidationFailed)
			{
				return Json(new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.ArgumentValidationFailed
					, sbValidationMessage.ToString(), typeof(GsAccountGeoFencePoints).ToString()));
			}

			#endregion Parameter Validation

			#region Execute Try
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				var listOfCoords = oCoordList.Select(oPointItem => new FnsGsAccountGeoFencePolygons
				                                                   	{
				                                                   		GeoFencePolygonID = oPointItem.GeoFencePolygonID, GeoFenceId = oPointItem.GeoFenceId, Sequence = oPointItem.Sequence, Lattitude = oPointItem.Lattitude, Longitude = oPointItem.Longitude
				                                                   	}).ToList();

				/** Build the list. */

				IFnsResult<List<IFnsGsAccountGeoFencePolygons>> oResultValue = oService.GeoPolygonUpdate(lGeoFenceId, lAccountId, new List<IFnsGsAccountGeoFencePolygons>(listOfCoords), oUser.Username);

				oResult.Code = oResultValue.Code;
				oResult.Message = oResultValue.Message;
				if (oResult.Code == (int)SosResultCodes.Success)
				{
					var oFnsList = (List<IFnsGsAccountGeoFencePolygons>)oResultValue.GetValue();
					listOfCoords.AddRange(oFnsList.Select(oItem => new FnsGsAccountGeoFencePolygons
					{
					    GeoFencePolygonID = oItem.GeoFencePolygonID, 
						GeoFenceId = oItem.GeoFenceId, 
						Sequence = oItem.Sequence, 
						Lattitude = oItem.Lattitude, 
						Longitude = oItem.Longitude, 
						CreatedOn = oItem.CreatedOn, 
						CreatedBy = oItem.CreatedBy
					}));
					oResult.Value = listOfCoords;
				}
			}
			#endregion Execute Try

			#region Execute Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<List<FnsGsAccountGeoFencePolygons>>((int)SosResultCodes.ExceptionThrown
					, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(List<FnsGsAccountGeoFencePolygons>).ToString());
			}
			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result
		}

    	/// <summary>
    	/// oArgs = { AccountId: 1000001, CoordList: 
    	///   [{ "GeoFencePolygonID":1, "GeoFenceId": 1 },
    	///    { "GeoFencePolygonID":2, "GeoFenceId": 2 },
    	///    { "GeoFencePolygonID":3, "GeoFenceId": 3 },
    	///    { "GeoFencePolygonID":4, "GeoFenceId": 4 },
    	///    { "GeoFencePolygonID":5, "GeoFenceId": 5 }]}
    	/// </summary>
    	/// <param name="lGeoFenceId"></param>
    	/// <param name="lAccountId"></param>
    	///<param name="oCoordList"></param>
    	///<returns></returns>
    	///[AllowCrossDomain("http://sos.clientgpstracksite.local")]
    	public JsonResult GeoSimpleSave(long lGeoFenceId, long lAccountId, GsSimplePoly[] oCoordList)
		{
			#region Authentication

			/** Authenticate. */
			var oResult = new SosResult<List<FnsGsAccountGeoFencePolygons>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<FnsGsAccountGeoFencePolygons>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult);
	
			#endregion Authentication

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result

		}

    	public JsonpResult GeoPolygonDelete(long lGeoFenceID)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(GsAccountGeoFencePoints).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication

			#region Return Result

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			#endregion Return Result
		}

		public JsonResult GeoFencesRead(long? lAccountID)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<List<GsAccountGeoFence>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(List<GsAccountGeoFence>).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication


			#region Parameter Validation
			if (lAccountID == null || lAccountID.Value == 0)
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = string.Format("Validation failed on AccountID of '{0}'", lAccountID);
				return Json(oResult);
			}

			#endregion Parameter Validation

			#region Execute Read

			try
			{
				/** Initialize. */
				var oResultList = new List<GsAccountGeoFence>();
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<List<IFnsGeoFencesView>> fenceCol = oService.GeoFencesRead(lAccountID.Value);

				/** Get result values. */
				oResult.Code = fenceCol.Code;
				oResult.Message = fenceCol.Message;
				if (fenceCol.Code == 0)
				{
					oResultList.AddRange(from oFenceItem in (List<IFnsGeoFencesView>) fenceCol.GetValue()
					    select new GsAccountGeoFence
					            {
					                GeoFenceID = oFenceItem.GeoFenceID, 
									AccountId = oFenceItem.AccountId, 
									GeoFenceTypeId = oFenceItem.GeoFenceTypeId, 
									GeoFenceType = oFenceItem.GeoFenceType, 
									PointLatitude = oFenceItem.PointLatitude, 
									PointLongitude = oFenceItem.PointLongitude, 
									CenterLattitude = oFenceItem.CenterLattitude, 
									CenterLongitude = oFenceItem.CenterLongitude, 
									//PolySequence = oFenceItem.PolySequence, 
									//PolyLattitude = oFenceItem.PolyLattitude, 
									//PolyLongitude = oFenceItem.PolyLongitude, 
									IsActive = oFenceItem.IsActive, 
									IsDeleted = oFenceItem.IsDeleted
					            });
				}
				oResult.Value = oResultList;
			}
			catch (Exception oEx)
			{
				oResult.Code = (int) SosResultCodes.ExceptionThrown;
				oResult.Message = string.Format("The following exception was thrown on GeoFenceRead: {0}"
				                                , oEx.Message);
			}
			#endregion Execute Read

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result

		}

		#endregion Need to Organize

		#region Circle CRUD

		public JsonResult GeoCircleSave(long lGeoFenceID, double fRadius, double fCenterLattitude, double fCenterLongitude)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<GsAccountGeoFenceCircles>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(GsAccountGeoFenceCircles).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult);
			#endregion Authentication

			#region Parameter Validation

			oResult = new SosResult<GsAccountGeoFenceCircles>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments", typeof(GsAccountGeoFenceCircles).ToString());

			/** Initialize parameters. */
			bool bValidationFailed = false;
			var sbValidationMessage = new StringBuilder();

			if (lGeoFenceID == 0)
			{
				sbValidationMessage.Append(string.Format("<li>The argument 'lGeoFenceID' was not passed.</li>"));
				bValidationFailed = true;
			}
			if (Math.Abs(fRadius - 0) < 1E-10)
			{
				sbValidationMessage.Append(string.Format("<li>The argument 'fRadius' was not passed.</li>"));
				bValidationFailed = true;
			}
			if (Math.Abs(fCenterLattitude - 0) < 1E-10)
			{
				sbValidationMessage.Append(string.Format("<li>The argument 'fCenterLattitude' was not passed.</li>"));
				bValidationFailed = true;
			}
			if (Math.Abs(fCenterLongitude - 0) < 1E-10)
			{
				sbValidationMessage.Append(string.Format("<li>The argument 'fCenterLongitude' was not passed.</li>"));
				bValidationFailed = true;
			}

			if (bValidationFailed)
			{
				return Json(new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.ArgumentValidationFailed
					, sbValidationMessage.ToString(), typeof(GsAccountGeoFenceCircles).ToString()));
			}

			#endregion Parameter Validation

			#region Execute Try

			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<IFnsGsAccountGeoFenceCircles> oCircleResult = oService.GeoFenceCircleUpdate(lGeoFenceID, fRadius, fCenterLattitude,
																				   fCenterLongitude, oUser.Username);
				oResult.Code = oCircleResult.Code;
				oResult.Message = oCircleResult.Message;

				/** Save value if successful. */
				if (oResult.Code == (int)SosResultCodes.Success)
				{
					var oFnsValue = (IFnsGsAccountGeoFenceCircles)oCircleResult.GetValue();
					var resultValue = new GsAccountGeoFenceCircles
					{
						GeoFenceID = oFnsValue.GeoFenceID,
						Radius = oFnsValue.Radius,
						CenterLattitude = oFnsValue.CenterLattitude,
						CenterLongitude = oFnsValue.CenterLongitude,
						CreatedOn = oFnsValue.CreatedOn,
						CreatedBy = oFnsValue.CreatedBy
					};
					oResult.Value = resultValue;
				}
			}

			#endregion Execute Try

			#region Execute Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<GsAccountGeoFenceCircles>((int)SosResultCodes.ExceptionThrown
					, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(GsAccountGeoFenceCircles).ToString());
			}
			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result
		}

		#endregion Circle CRUD

		#region Rectangle CRUD

		public JsonResult GeoRectangleSave(long lGeoFenceID, long lAccountId, string sItemId, string sReportMode, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMinLongitude, double dMaxLongitude, double dMinLattitude)
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(GsAccountGeoFenceRectangles).ToString());
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser)) return Json(oResult);
			#endregion Authentication

			#region Parameter Validation

			oResult = new SosResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments", typeof(GsAccountGeoFenceRectangles).ToString());

			/** Initialize parameters. */
			bool bValidationFailed = false;
			var sbValidationMessage = new StringBuilder();

			if (lAccountId == 0)
			{
				sbValidationMessage.Append(string.Format("<li>'lAccountId' argument must be passed.</li>"));
				bValidationFailed = true;
			}
			if (DoubleExtensions.IsZero(dMaxLattitude) || dMinLongitude.IsZero() || dMaxLongitude.IsZero() || dMinLattitude.IsZero())
			{
				sbValidationMessage.Append(string.Format("<li>One of the coordinates passed is zero.</li>"));
				bValidationFailed = true;
			}

			if (bValidationFailed)
			{
				return Json(new SosResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.ArgumentValidationFailed
					, sbValidationMessage.ToString(), typeof(GsAccountGeoFenceRectangles).ToString()));
			}

			#endregion Parameter Validation

			#region Execute Try

			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
				IFnsResult<IFnsGsAccountGeoFenceRectangles> oCircleResult = (lGeoFenceID <= 0)
					? oService.GeoFenceRectangleCreate(lAccountId, sItemId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMaxLongitude, dMinLattitude, dMinLongitude, oUser.Username)
					: oService.GeoFenceRectangleUpdate(lGeoFenceID, sItemId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMaxLongitude, dMinLattitude, dMinLongitude, oUser.Username);
				oResult.Code = oCircleResult.Code;
				oResult.Message = oCircleResult.Message;

				/** Save value if successful. */
				if (oResult.Code == (int)SosResultCodes.Success)
				{
					var oFnsValue = (IFnsGsAccountGeoFenceRectangles)oCircleResult.GetValue();
					var resultValue = new GsAccountGeoFenceRectangles
					{
						GeoFenceID = oFnsValue.GeoFenceID,
						AccountId = oFnsValue.AccountId,
						Area = oFnsValue.Area,
						MeanLattitude = oFnsValue.MeanLattitude,
						MeanLongitude = oFnsValue.MeanLongitude,
						MaxLattitude = oFnsValue.MaxLattitude,
						MinLattitude = oFnsValue.MinLattitude,
						MaxLongitude = oFnsValue.MaxLongitude,
						MinLongitude = oFnsValue.MinLongitude,
						CreatedOn = oFnsValue.CreatedOn,
						CreatedBy = oFnsValue.CreatedBy
					};
					oResult.Value = resultValue;
				}
			}

			#endregion Execute Try

			#region Execute Catch
			catch (Exception oEx)
			{
				oResult = new SosResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.ExceptionThrown
					, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(GsAccountGeoFenceRectangles).ToString());
			}
			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return Json(oResult);

			#endregion Return Result
		}

    	#endregion Rectangle CRUD

		#region GeoFence Requests

		public JsonResult GetLaipacS911GeoFences(long lAccountID)
		{
			/** Initialize. */
			const string METHOD_NAME = "GetLaipacS911GeoFences";
			return Json(AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				/** Initialize. */
				SosResult<List<GsAccountGeoFence>> result;

				#region Parameter Validation

				var aCORSArg = new List<CORSArg>
				{
				    new CORSArg(lAccountID, (lAccountID == 0), "<li>'lAccountID' argument must be passed.</li>"),
				};
				if (!ArgumentValidation(aCORSArg, out result)) return result;
				
				#endregion Parameter Validation

				#region Execute Try
				try
				{	/** Init. */
					var oServices = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<List<IFnsGsAccountGeoFenceRectangles>> fnsResult = oServices.GetLaipacS911GeoFences(lAccountID, oUser.Username);

					/** Check out the results. */
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					if (result.Code == 0)
					{
						/** Convert to GsAccountGeoFence. */
						var fnsResultList = (List<IFnsGsAccountGeoFenceRectangles>) fnsResult.GetValue();
						var resultList = fnsResultList.Select(geoFenceItem => new GsAccountGeoFence
						{
							GeoFenceID = geoFenceItem.GeoFenceID, 
							AccountId = lAccountID, 
							Area = geoFenceItem.Area, 
							MeanLattitude = geoFenceItem.MeanLattitude, 
							MeanLongitude = geoFenceItem.MeanLongitude, 
							MinLattitude = geoFenceItem.MinLattitude, 
							MinLongitude = geoFenceItem.MinLongitude, 
							MaxLattitude = geoFenceItem.MaxLattitude, 
							MaxLongitude = geoFenceItem.MaxLongitude
						}).ToList();
						result.Value = resultList;
					}

				}
				#endregion Execute Try

				#region Execute Catch
				catch (Exception ex)
				{
					result = new SosResult<List<GsAccountGeoFence>>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown in '{0}': {1}", METHOD_NAME, ex.Message), typeof(GsAccountGeoFenceRectangles).ToString());
				}
				#endregion Execute Catch

				#region Return result
				return result;
				#endregion Return result

			}));
		}

    	#endregion GeoFence Requests

		#region Request Device Location

		public JsonResult GetLaipacS911CurrentLocation(long lAccountID)
		{
			/** Initialize. */
			const string METHOD_NAME = "GetLaipacS911CurrentLocation";
			return Json(AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
			  	/** Initialize. */
				SosResult<GsAccountGeoFencePoints> result;

				#region Parameter Validation
				var aCORSArg = new List<CORSArg>
				{
				    new CORSArg(lAccountID, (lAccountID == 0), "<li>'lAccountID' argument must be passed.</li>"),
				};
				if (!ArgumentValidation(aCORSArg, out result)) return result;

				#endregion Parameter Validation

				#region Execute Try
				try
				{	/** Init. */
					var oServices = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					var fnsResult = oServices.GetLaipacS911CurrentLocation(lAccountID, oUser.Username);

					/** Check out the results. */
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					result.ValueType = typeof(GsAccountGeoFencePoints).ToString();
					if (result.Code == 0)
					{
						/** Convert to GsAccountGeoFence. */
						var fnsResultValue = (IFnsGsAccountGeoFencePoints) fnsResult.GetValue();
						var resultValue = new GsAccountGeoFencePoints
						{
							PlaceName = fnsResultValue.PlaceName,
							PaceDescription = fnsResultValue.PlaceDescription,
							Lattitude = fnsResultValue.Lattitude,
							Longitude = fnsResultValue.Longitude,
							CreatedOn = fnsResultValue.CreatedOn
						};
						result.Value = resultValue;
					}
				}
				#endregion Execute Try
				#region Execute Catch
				catch (Exception ex)
				{
					result = new SosResult<GsAccountGeoFencePoints>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown in '{0}': {1}", METHOD_NAME, ex.Message), typeof(GsAccountGeoFencePoints).ToString());
				}
				#endregion Execute Catch

				#region Return result
				return result;
				#endregion Return result
			}));
		}

    	#endregion Request Device Location

		#region Wrappers

		public SosResult<T> AuthenticationWrapper<T>(string functionName, Func<SosCustomer, SosResult<T>> action)
		{
			#region Authentication
			/** Authenticate. */
			SosCustomer oUser;
			var oResult = new SosResult<T>((int)SosResultCodes.CookieInvalid
				, string.Format("Validating Authentication Failed for '{0}'", functionName), typeof(T).ToString());
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, AuthSrvController.ValidateSession, _SOS_GPS_CLNT, out oUser))
				return oResult;
			#endregion Authentication

			/** Perform action. */
			return action(oUser);
		}

    	public class CORSArg
		{
			#region .ctor
			public CORSArg(object argument, bool falsePredicate, string message)
			{
				Argument = argument;
				FalsePredicate = falsePredicate;
				Message = message;
				BindValues(() => argument);
			}

    		#endregion .ctor

			public object Argument { get; private set; }
			public bool FalsePredicate { get; private set; }
			public string Message { get; private set; }
			public string Name { get; private set; }
			public object Value { get; private set; } 
			private void BindValues (Expression<Func<object>> expr)
			{
				var body = ((MemberExpression)expr.Body);
				Name = body.Member.Name;
				Value = ((FieldInfo)body.Member)
				            	.GetValue(((ConstantExpression)body.Expression).Value);
			}
    	}

		public bool ArgumentValidation<T>(List<CORSArg> argArray, out SosResult<T> result)
		{
			/** Initialize. */
			var sb = new StringBuilder();
			var passesValidation = true;
			result = new SosResult<T>((int)SosResultCodes.ArgumentValidating
				, "Validating Arguments", typeof(GsAccountGeoFenceRectangles).ToString());

			foreach (CORSArg corsArg in argArray)
			{
				if (!corsArg.FalsePredicate) continue;

				/** Default path of execution. */
				sb.AppendFormat(corsArg.Message, corsArg.Name, corsArg.Value);
				passesValidation = false;
			}

			if (!passesValidation)
			{
				result = new SosResult<T>((int)SosResultCodes.ArgumentValidationFailed
					, sb.ToString(), typeof(T).ToString());

			}

			/** Return validation result. */
			return passesValidation;
		}

    	#endregion Wrappers

	}
}