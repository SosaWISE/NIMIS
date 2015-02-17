using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.CmsModels;
using SOS.Services.Interfaces.Models.GpsTracking;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CORS.Helpers;
using SSE.Services.CORS.Models;

namespace SSE.Services.CORS.Controllers
{
	public class DeviceController : ApiController
	{
		#region Public Members

		/// <summary>
		/// Given the object GetListJsonParamBase the method uses the UniqueID property and uses it in the search of devices as the CustomerMasterFileID.
		/// </summary>
		/// <param name="jsonParamBase">GetListJsonParamBase</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<MsAccountClientsView>> AcquireList(GetListJsonParamBase jsonParamBase)
		{
			/** Authenticate. */
			const string METHOD_NAME = "AcquireList";
			var oResult = new SosCORSResult<List<MsAccountClientsView>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed");


			#region Parameter Validation

			if (jsonParamBase != null)
			{
				var aCORSArg = new List<CORSArg>
    			               	{
    			               		new CORSArg(jsonParamBase.UniqueID, (jsonParamBase.UniqueID == 0), "<li>'UniqueID' must be passed.</li>"),
    			               	};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;
			}
			else
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argument must be passed and must be in JSON format.";
				return oResult;
			}

			#endregion Parameter Validation

			/** Execute Call */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oCustomer =>
			{
				#region TRY
				/** Execute request.*/
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
					IFnsResult<List<IFnsMsAccountClientsView>> oSrvResult = oService.GetDevicesByCMFID(jsonParamBase.UniqueID, oCustomer.Username);

					/** Check corsResult. */
					if (oSrvResult.Code != 0)
					{
						oResult.Code = oSrvResult.Code;
						oResult.Message = oSrvResult.Message;
						return oResult;
					}

					/** Bundle corsResult for return. */
					List<MsAccountClientsView> oResultList = (from oItem in (List<IFnsMsAccountClientsView>)oSrvResult.GetValue()
																		select new MsAccountClientsView
																				{
																					CustomerMasterFileId = oItem.CustomerMasterFileId
																					, CustomerID = oItem.CustomerID
																					, AccountId = oItem.AccountId
																					, UnitID = oItem.UnitID
																					, AccountName = oItem.AccountName
																					, EventID = oItem.EventID
																					, EventDate = oItem.EventDate
																					, LastLatt = oItem.LastLatt
																					, LastLong = oItem.LastLong
																					, UIName = oItem.UIName
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
					oResult.Code = (int)SosResultCodes.Success;
					oResult.Message = "Successful";
					oResult.SessionId = oCustomer.SessionID;
					oResult.Value = oResultList;
				}
				#endregion TRY
				#region CATCH
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<List<MsAccountClientsView>>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
								, oEx.Message)
					, typeof(AeCustomer).ToString());
				}
				#endregion CATCH

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result

			});
		}

		/// <summary>
		/// Given a GetListJsonParamBase object serialized from a Json object it looks for the UniqueID property and uses it to bind it to the CustomerID for the query.  If
		/// devices are found it will return a list of them.
		/// </summary>
		/// <param name="jsonParamBase">GetListJsonParamBase</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<MsAccountClientsView>> AcquireListByCustomerID(GetListJsonParamBase jsonParamBase)
		{
			/** Authenticate. */
			const string METHOD_NAME = "SessionTerminate";
			var oResult = new SosCORSResult<List<MsAccountClientsView>>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(AeCustomer).ToString());

			#region Parameter Validation

			if (jsonParamBase != null)
			{
				var aCORSArg = new List<CORSArg>
    			               	{
    			               		new CORSArg(jsonParamBase.UniqueID, (jsonParamBase.UniqueID == 0), "<li>'UniqueID' must be passed.</li>"),
    			               	};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;
			}
			else
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argument must be passed and must be in JSON format.";
				return oResult;
			}

			#endregion Parameter Validation

			#region Execute call
			/** Execute call. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oCustomer =>
				{
					#region TRY
					try
					{
						/** Initialize. */
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
						IFnsResult<List<IFnsMsAccountClientsView>> oSrvResult = oService.GetDevicesByCustomerID(jsonParamBase.UniqueID, oCustomer.Username);

						/** Check corsResult. */
						if (oSrvResult.Code != 0)
						{
							oResult.Code = oSrvResult.Code;
							oResult.Message = oSrvResult.Message;
							return oResult;
						}

						/** Bundle corsResult for return. */
						List<MsAccountClientsView> oResultList = (from oItem in (List<IFnsMsAccountClientsView>)oSrvResult.GetValue()
																			select new MsAccountClientsView
																					{
																						CustomerMasterFileId = oItem.CustomerMasterFileId
																						, CustomerID = oItem.CustomerID
																						, AccountId = oItem.AccountId
																						, UnitID = oItem.UnitID
																						,
																						Username = oItem.Username
																						,
																						Password = oItem.Password
																						,
																						CustomerTypeId = oItem.CustomerTypeId
																						,
																						SystemTypeId = oItem.SystemTypeId
																						,
																						PanelTypeId = oItem.PanelTypeId
																						,
																						IndustryAccountId = oItem.IndustryAccountId
																						,
																						IndustryNumber = oItem.IndustryNumber
																						,
																						Designator = oItem.Designator
																						,
																						SubscriberNumber = oItem.SubscriberNumber
																					}).ToList();
						oResult.Code = (int)SosResultCodes.Success;
						oResult.Message = "Successful";
						oResult.Value = oResultList;
					}
					#endregion TRY
					#region CATCH
					catch (Exception oEx)
					{
						oResult = new SosCORSResult<List<MsAccountClientsView>>((int)SosResultCodes.GeneralError
						, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
						, typeof(AeCustomer).ToString());
					}
					#endregion CATCH

					#region Return Result

					/** Return corsResult. */
					return oResult;

					#endregion Return Result
				});

			#endregion Execute call

		}

		/// <summary>
		/// This method returns the detailed information for an object.
		/// </summary>
		/// <param name="jsonParamBase">GetDeviceDetailsJsonParamBase</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<MsAccountClientDetailsView> AcquireDeviceDetails(GetDeviceDetailsJsonParamBase jsonParamBase)
		{
			#region Argument Validation

			var oResult = new SosCORSResult<MsAccountClientDetailsView>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed", typeof(MsAccountClientDetailsView).ToString());

			if (jsonParamBase != null)
			{
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParamBase.AccountID, (jsonParamBase.AccountID == 0), "<li>'AccountID' must be passed.</li>"),
						new CORSArg(jsonParamBase.CustomerID, (jsonParamBase.CustomerID == 0), "<li>'CustomerID' must be passed.</li>")
					};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;
			}
			else
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argument must be passed and must be in JSON format.";
				return oResult;
			}

			#endregion Argument Validation

			#region Session Validation and Execute

			const string METHOD_NAME = "AcquireDeviceDetails";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				#region TRY
				/** Execute request.*/
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
					IFnsResult<IFnsMsAccountClientDetailsView> oSrvResult = oService.GetDeviceDetailsByAccountID(jsonParamBase.AccountID, jsonParamBase.CustomerID, oUser.Username);

					/** Check corsResult. */
					oResult.Code = oSrvResult.Code;
					oResult.Message = oSrvResult.Message;
					if (oSrvResult.Code == 0)
					{
						var oSrvValue = (IFnsMsAccountClientDetailsView)oSrvResult.GetValue();
						var oModel = new MsAccountClientDetailsView
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

						/** Get value for corsResult. */
						oResult.SessionId = oUser.SessionID;
						oResult.Value = oModel;
					}
				}
				#endregion TRY
				#region CATCH
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<MsAccountClientDetailsView>((int)SosResultCodes.GeneralError
						, string.Format("The following exception was thrown:\r\n{0}"
						, oEx.Message));
				}
				#endregion CATCH

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result

			});

			#endregion Session Validation and Execute
		}

		/// <summary>
		/// This method returns the geo fences that are associated with a device.  Below is the list of arguments or properties that arefound in the object:
		///		SessionID	{long}	required
		///		AccountID	{long}	required
		///		CustomerID	{long}	required
		/// </summary>
		/// <param name="jsonParam">GetDeviceDetailsJsonParamBase</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<GsAccountGeoFence>> AcquireDeviceGeoFences(GetDeviceDetailsJsonParamBase jsonParam)
		{
			#region Argument Validation

			var oResult = new SosCORSResult<List<GsAccountGeoFence>>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");

			if (jsonParam != null)
			{
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParam.CMFID, (jsonParam.CMFID == 0), "<li>'CMFID' must be passed.</li>")
						//new CORSArg(jsonParam.CustomerID, (jsonParam.CustomerID == 0), "<li>'CustomerID' must be passed.</li>")
					};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;
			}
			else
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argument must be passed and must be in JSON format.";
				return oResult;
			}

			#endregion Argument Validation

			#region Session Validation and Execute

			const string METHOD_NAME = "AcquireDeviceGeoFences";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region TRY
				/** Execute request.*/
				try
				{
					/** Init. */
					long? customerId = jsonParam.CustomerID == 0 ? (long?)null : jsonParam.CustomerID;
					/** Get GeoFences. */
					var oServiceGps = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<List<IFnsGeoFencesView>> oSrvGps = oServiceGps.GeoFencesByCMFID(jsonParam.CMFID, customerId);

					oResult.Code = oSrvGps.Code;
					oResult.Message = oSrvGps.Message;
					var oGeoFenceList = new List<GsAccountGeoFence>();
					if (oSrvGps.Code == 0)
					{
						/** Create List for corsResult. */
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
								, GeoFenceTypeUi = oFenceItem.GeoFenceTypeUi
								, ReportModeId = oFenceItem.ReportModeId
								, ReportModeUi = oFenceItem.ReportModeUi
								, AccountId = oFenceItem.AccountId
								, GeoFenceName = oFenceItem.GeoFenceName
								, GeoFenceDescription = oFenceItem.GeoFenceDescription
								, GeoFenceNameUi = oFenceItem.GeoFenceNameUi
								, MeanLattitude = oFenceItem.MeanLattitude
								, MeanLongitude = oFenceItem.MeanLongitude
								, ZoomLevel = oFenceItem.GoogleMapZoomLevel
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
								, ModifiedOn = oFenceItem.ModifiedOn
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
					oResult = new SosCORSResult<List<GsAccountGeoFence>>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
								, oEx.Message)
					, typeof(List<GsAccountGeoFence>).ToString());
				}
				#endregion CATCH

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result

			});

			#endregion Session Validation and Execute
		}


		/// <summary>
		/// This method returns the device events.  Below is a list of arguments that are passed:
		///		SessionID	{long}		required
		///		AccountID	{long}		required
		///		CustomerID	{long}		required
		///		StartDate	{DateTime}	required
		///		EndDate		{DateTime}	required
		///		PageSize	{int}
		///		PageNumber	{int}
		/// </summary>
		/// <param name="jsonParam"></param>
		/// <returns></returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<GsDeviceEvents>> AcquireDeviceEvents(GetDeviceEventJsonParamBase jsonParam)
		{
			#region Argument Validation
			/** Initialize */
			DateTime startDate;
			DateTime endDate;

			var oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");

			if (jsonParam != null)
			{
				if (jsonParam.PageSize == 0) jsonParam.PageSize = 5;
				if (jsonParam.PageNumber == 0) jsonParam.PageNumber = 1;
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParam.AccountID, (jsonParam.AccountID == 0), "<li>'AccountID' must be passed.</li>"),
						new CORSArg(jsonParam.CustomerID, (jsonParam.CustomerID == 0), "<li>'CustomerID' must be passed.</li>"),
						new CORSArg(jsonParam.StartDate, (!DateTime.TryParse(jsonParam.StartDate, out startDate)), "<li>StartDate value is not a DateTime type.</li>"),
						new CORSArg(jsonParam.StartDate, (!DateTime.TryParse(jsonParam.EndDate, out endDate)), "<li>EndDate value is not a DateTime type.</li>")
					};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;
			}
			else
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argument must be passed and must be in JSON format.";
				return oResult;
			}


			#endregion Argument Validation

			#region Session Validation and Execute

			const string METHOD_NAME = "AcquireDeviceGeoFences";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region TRY
				/** Execute request.*/
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<List<IFnsGsEventsView>> oResultValue = oService.EventDeviceEventsGet(jsonParam.AccountID, startDate, endDate.AddDays(1),
																							   jsonParam.PageSize, jsonParam.PageNumber);
					/** Check result. */
					if (oResultValue.Code != 0)
					{
						oResult.Code = oResultValue.Code;
						oResult.Message = oResultValue.Message;
						return oResult;
					}

					/** Create result package. */
					var oFnsList = (List<IFnsGsEventsView>)oResultValue.GetValue();
					List<GsDeviceEvents> oModelList = oFnsList.Select(oItem => new GsDeviceEvents
																				{
																					EventID = oItem.EventID
																					, EventTypeId = oItem.EventTypeId
																					, EventType = oItem.EventType
																					, AccountId = oItem.AccountId
																					, EventName = oItem.EventName
																					//, EventDate = oItem.EventDate.ToShortDateString() + ' ' + oItem.EventDate.ToShortTimeString()
																					, EventDate = oItem.EventDate
																					, Lattitude = oItem.Lattitude
																					, Longitude = oItem.Longitude
																				}).ToList();
					oResult.Code = (int)SosResultCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oModelList;
				}
				#endregion TRY

				#region CATCH
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
								, oEx.Message)
					, typeof(List<GsAccountGeoFence>).ToString());
				}
				#endregion CATCH

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result

			});

			#endregion Session Validation and Execute
		}

		/// <summary>
		/// This method returns the device events.  Below is a list of arguments that are passed:
		///		SessionID	{long}		required
		///		AccountID	{long}		required
		///		CustomerID	{long}		required
		///		StartDate	{DateTime}	required
		///		EndDate		{DateTime}	required
		///		PageSize	{int}
		///		PageNumber	{int}
		/// </summary>
		/// <param name="jsonParam"></param>
		/// <returns></returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<GsDeviceEvents>> AcquireMasterDeviceEvents(GetDeviceEventJsonParamBase jsonParam)
		{
			#region Argument Validation
			/** Initialize */
			DateTime startDate;
			DateTime endDate;
			long? customerId = jsonParam.CustomerID == 0 ? (long?)null : jsonParam.CustomerID;

			// ReSharper disable RedundantAssignment
			var oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");
			// ReSharper restore RedundantAssignment

			if (jsonParam.PageSize == 0) jsonParam.PageSize = 5;
			if (jsonParam.PageNumber == 0) jsonParam.PageNumber = 1;
			var aCORSArg = new List<CORSArg>
				{
					new CORSArg(jsonParam.CMFID, (jsonParam.CMFID == 0), "<li>'CMFID' must be passed.</li>"),
					//new CORSArg(jsonParam.CustomerID, (jsonParam.CustomerID == 0), "<li>'CustomerID' must be passed.</li>"),
					new CORSArg(jsonParam.StartDate, (!DateTime.TryParse(jsonParam.StartDate, out startDate)), "<li>StartDate value is not a DateTime type.</li>"),
					new CORSArg(jsonParam.EndDate, (!DateTime.TryParse(jsonParam.EndDate, out endDate)), "<li>EndDate value is not a DateTime type.</li>")
				};
			if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;


			#endregion Argument Validation

			#region Session Validation and Execute

			const string METHOD_NAME = "AcquireMasterDeviceEvents";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region TRY
				/** Execute request.*/
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<List<IFnsGsEventsView>> oResultValue = oService.EventDeviceEventsMasterGet(jsonParam.CMFID, customerId, startDate, endDate.AddDays(1),
																							   jsonParam.PageSize, jsonParam.PageNumber);
					/** Check result. */
					if (oResultValue.Code != 0)
					{
						oResult.Code = oResultValue.Code;
						oResult.Message = oResultValue.Message;
						return oResult;
					}

					/** Create result package. */
					var oFnsList = (List<IFnsGsEventsView>)oResultValue.GetValue();
					List<GsDeviceEvents> oModelList = oFnsList.Select(oItem => new GsDeviceEvents
																				{
																					EventID = oItem.EventID
																					, EventTypeId = oItem.EventTypeId
																					, EventType = oItem.EventType
																					, EventTypeUi = oItem.EventTypeUi
																					, EventShortDesc = oItem.EventShortDesc
																					, AccountId = oItem.AccountId
																					, CustomerId = oItem.CustomerId
																					, CustomerMasterFileId = oItem.CustomerMasterFileId
																					, AccountName = oItem.AccountName
																					, EventName = oItem.EventName
																					, EventDate = oItem.EventDate
																					//, EventDate = oItem.EventDate.ToShortDateString() + ' ' + oItem.EventDate.ToShortTimeString()
																					, Lattitude = oItem.Lattitude
																					, Longitude = oItem.Longitude
																				}).ToList();
					oResult.Code = (int)SosResultCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oModelList;
				}
				#endregion TRY

				#region CATCH
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
								, oEx.Message)
					, typeof(List<GsAccountGeoFence>).ToString());
				}
				#endregion CATCH

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result

			});

			#endregion Session Validation and Execute
		}

		#endregion Public Members

		#region Device Update

		public SosCORSResult<MsAccountClientsView> DeviceUpdate(DeviceParam jsonParam)
		{
			/** Authenticate. */
			const string METHOD_NAME = "DeviceUpdate";
			var oResult = new SosCORSResult<MsAccountClientsView>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed");

			#region Parameter Validation

			if (jsonParam == null)
			{
				oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
				oResult.Message = "Argmument must be passed and must be in JSON format.";
				return oResult;
			}

			var aCORSArg = new List<CORSArg>
			    {
					new CORSArg(jsonParam.AccountID, (jsonParam.AccountID == 0), "<li>'AccountID' must be passed.</li>"),
					new CORSArg(jsonParam.AccountName, (string.IsNullOrEmpty(jsonParam.AccountName)), "<li>'AccountName' must be passed.</li>")
			    };
			
			if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

			#endregion Parameter Validation

			/** Execute. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oCustomer =>
				{
					#region TRY
					try
					{
						/** Initialize. */
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
						IFnsResult<IFnsMsAccountClientsView> oSrvResult = oService.UpdateDevice(jsonParam.AccountID
							, jsonParam.AccountName, jsonParam.AccountDesc);

						/** Check result. */
						if (oSrvResult.Code != (int) SosResultCodes.Success)
						{
							oResult.Code = oSrvResult.Code;
							oResult.Message = oSrvResult.Message;
							return oResult;
						}

						/** Create envolope value to return */
						var oItem = (IFnsMsAccountClientsView) oSrvResult.GetValue();
						var oResultItem = new MsAccountClientsView
							{
								CustomerMasterFileId = oItem.CustomerMasterFileId
								, CustomerID = oItem.CustomerID
								, AccountId = oItem.AccountId
								, UnitID = oItem.UnitID
								, AccountName = oItem.AccountName
								, EventID = oItem.EventID
								, EventDate = oItem.EventDate
								, LastLatt = oItem.LastLatt
								, LastLong = oItem.LastLong
								, UIName = oItem.UIName
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
							};
						oResult.Code = (int) SosResultCodes.Success;
						oResult.Message = "Successful";
						oResult.SessionId = oCustomer.SessionID;
						oResult.Value = oResultItem;
					}
					#endregion TRY
					#region CATCH
					catch (Exception oEx)
					{
						oResult = new SosCORSResult<MsAccountClientsView>((int)SosResultCodes.GeneralError
						, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
						, typeof(MsAccountClientsView).ToString());
					}
					#endregion CATCH

					#region Return Result

					/** Return corsResult. */
					return oResult;

					#endregion Return Result
				});
		}

					#endregion Device Update

		#region EventTypes CRUD

		public SosCORSResult<List<GsEventTypeView>> EventTypesReadAll(EventTypeParam jsonParam)
		{
			/** Initialize. */
			const string METHOD_NAME = "EventTypesRead";
			var oResult = new SosCORSResult<List<GsEventTypeView>>((int)SosResultCodes.CookieInvalid,
			                                                 "Validating Authentication Failed.");
			/** Execute and Return. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
				{
					#region Parameter Validation
					if (jsonParam == null)
					{
						oResult.Code = (int)SosResultCodes.ArgumentValidationFailed;
						oResult.Message = "Argmument must be passed and must be in JSON format.";
						return oResult;
					}

					#endregion Parameter Validation

					#region TRY
					try
					{
						/** Initialize. */
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
						IFnsResult<List<IFnsGsEventTypeView>> fnsResult = oService.EventTypeReadAll(jsonParam.EventTypeID,
						                                                                         jsonParam.EventType);
						/** Save Result information. */
						oResult.Code = fnsResult.Code;
						oResult.Message = fnsResult.Message;
						if (fnsResult.Code != (int) SosResultCodes.Success)
						{
							return oResult;
						}

						/** Save list. */
						var resultList = (from fnsGsEventTypeView in (List<IFnsGsEventTypeView>) fnsResult.GetValue()
						                  select new GsEventTypeView
							                  {
								                  EventTypeID = fnsGsEventTypeView.EventTypeID, EventType = fnsGsEventTypeView.EventType
							                  }).ToList();

						oResult.Value = resultList;

					}
					#endregion TRY

					#region CATCH
					catch (Exception oEx)
					{
						oResult = new SosCORSResult<List<GsEventTypeView>>((int)SosResultCodes.GeneralError
						, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
						, typeof(GsEventTypeView).ToString());
					}
					#endregion CATCH

					#region Return Result

					/** Return corsResult. */
					return oResult;

					#endregion Return Result
				});
		}

		#endregion EventTypes CRUD

	}
}