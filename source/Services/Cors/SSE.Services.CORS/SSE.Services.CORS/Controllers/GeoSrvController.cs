
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.Lib.Util.Extensions;
using SOS.Services.Interfaces.Models.GpsTracking;
using SSE.Services.CORS.Helpers;
using SSE.Services.CORS.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SSE.Services.CORS.Controllers
{
    public class GeoSrvController : ApiController
	{

		#region Point CRUD

		/// <summary>
		/// Given the param object it will save the geo point information.  Below is the list of arguments:
		///		SessionID	{long}		Required
		///		AccountId	{long}		Required
		///		CustomerId	{long}		Required
		///		GeoFenceID	{long}		Optional	If the argument is not passed it creates a new geo point.
		///		GeoFenceName	{string}	Required
		///		GeoFenceDescription	{string}	
		///		Lattitude	{double}	Required
		///		Longitude	{double}	Required
		/// </summary>
		/// <param name="jsonParam">PointJsonParam</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<GsAccountGeoFencePoints> PointCreateUpdate(PointJsonParam jsonParam)
        {
			#region Argument Validation
			/** Initialize */
			var oResult = new SosCORSResult<GsAccountGeoFencePoints>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");

			if (jsonParam != null)
			{
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParam.AccountId, (jsonParam.AccountId == 0), "<li>'AccountId' must be passed.</li>"),
						new CORSArg(jsonParam.CustomerId, (jsonParam.CustomerId == 0), "<li>'CustomerId' must be passed.</li>"),
						new CORSArg(jsonParam.GeoFenceName, (string.IsNullOrEmpty(jsonParam.GeoFenceName)), "<li>'GeoFenceName' must be passed.</li>"),
						new CORSArg(jsonParam.Lattitude, (jsonParam.Lattitude.IsZero()), "<li>'Lattitude' must be passed.</li>"),
						new CORSArg(jsonParam.Lattitude, (jsonParam.Longitude.IsZero()), "<li>'Longitude' must be passed.</li>")
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

			#region Session Validation and Execution

			const string METHOD_NAME = "PointCreateUpdate";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region Execute Try
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<IFnsGsAccountGeoFencePoints> oResultValue = oService.GeoPointCreateUpdate(jsonParam.GeoFenceID, jsonParam.AccountId, jsonParam.GeoFenceName, jsonParam.GeoFenceDescription, jsonParam.Lattitude, jsonParam.Longitude, oUser.Username);

					oResult.Code = oResultValue.Code;
					oResult.Message = oResultValue.Message;
					if (oResult.Code == (int)SosResultCodes.Success)
					{
						var oFnsValue = (IFnsGsAccountGeoFencePoints)oResultValue.GetValue();
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
					oResult = new SosCORSResult<GsAccountGeoFencePoints>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message));
				}
				#endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result
				
			});

			#endregion Session Validation and Execution
		}

		/// <summary>
		/// This method deletes the point from the users account.  Below is the list of arguments:
		///		SessionID	{long}		Required
		///		AccountId	{long}		Required
		///		CustomerId	{long}		Required
		///		GeoFenceID	{long}		Required
		///		GeoFenceName	{string}	Ignored
		///		GeoFenceDescription	{string}	Ignored
		///		Lattitude	{double}	Ignored
		///		Longitude	{double}	Ignored
		/// </summary>
		/// <param name="jsonParam">PointJsonParam</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
	    [System.Web.Mvc.HttpOptions]
	    public SosCORSResult<bool> PointDelete(PointJsonParam jsonParam)
	    {
			#region Argument Validation
			/** Initialize */
			var oResult = new SosCORSResult<bool>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");

			if (jsonParam != null)
			{
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParam.AccountId, (jsonParam.AccountId == 0), "<li>'AccountId' must be passed.</li>"),
						new CORSArg(jsonParam.CustomerId, (jsonParam.CustomerId == 0), "<li>'CustomerId' must be passed.</li>"),
						new CORSArg(jsonParam.GeoFenceID, (jsonParam.GeoFenceID == 0), "<li>'GeoFenceID' must be passed.</li>")
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

			#region Session Validation and Execution

			const string METHOD_NAME = "PointCreateUpdate";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region Execute Try
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<bool> oResultValue = oService.GeoPointDelete(jsonParam.GeoFenceID, jsonParam.AccountId, oUser.Username);

					oResult.Code = oResultValue.Code;
					oResult.Message = oResultValue.Message;
					oResult.Value = (oResult.Code == (int) SosResultCodes.Success);
				}
				#endregion Execute Try

				#region Execute Catch
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<bool>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message));
				}
				#endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result
			});
			#endregion Session Validation and Execution

	    }

		/// <summary>
		/// Given the param object it will save the geo point information.  Below is the list of arguments:
		///		SessionID	{long}		Required
		///		AccountId	{long}		Required
		///		CustomerId	{long}		Ignored
		///		GeoFenceID	{long}		Required
		///		GeoFenceName	{string}	Ignored
		///		GeoFenceDescription	{string}	Ignored
		///		Lattitude	{double}	Ignored
		///		Longitude	{double}	Ignored
		/// </summary>
		/// <param name="jsonParam">PointJsonParam</param>
		/// <returns>SosCORSResult</returns>
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<GsAccountGeoFencePoints> PointRead(PointJsonParam jsonParam)
		{
			#region Argument Validation
			/** Initialize */
			var oResult = new SosCORSResult<GsAccountGeoFencePoints>((int)SosResultCodes.ArgumentValidationFailed
				, "Validating Arguments Failed");

			if (jsonParam != null)
			{
				var aCORSArg = new List<CORSArg>
					{
						new CORSArg(jsonParam.GeoFenceID, (jsonParam.GeoFenceID == 0), "<li>'GeoFenceID' must be passed.</li>"),
						new CORSArg(jsonParam.AccountId, (jsonParam.AccountId == 0), "<li>'AccountId' must be passed.</li>")
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

			#region Session Validation and Execution

			const string METHOD_NAME = "PointRead";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region Execute Try
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<IFnsGsAccountGeoFencePoints> oResultValue = oService.GeoPointRead(jsonParam.GeoFenceID, jsonParam.AccountId);

					oResult.Code = oResultValue.Code;
					oResult.Message = oResultValue.Message;
					if (oResult.Code == (int)SosResultCodes.Success)
					{
						var oFnsValue = (IFnsGsAccountGeoFencePoints)oResultValue.GetValue();
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
					oResult = new SosCORSResult<GsAccountGeoFencePoints>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message));
				}
				#endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result

			});

			#endregion Session Validation and Execution
		}

	    #endregion Point CRUD

		#region Circle CRUD
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<GsAccountGeoFenceCircles> GeoCircleSave(CircleJsonParam jsonParam)
		{
			#region Authentication
			/** Authenticate. */
			const string METHOD_NAME = "GeoCircleSave";

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				#region Parameter Validation

				var oResult = new SosCORSResult<GsAccountGeoFenceCircles>((int)SosResultCodes.ArgumentValidationFailed
					, "Validating Arguments");

				/** Initialize parameters. */
				if (jsonParam != null)
				{
					var oCORSArg = new List<CORSArg>
						{
							new CORSArg(jsonParam.AccountId, (jsonParam.AccountId == 0), "<li>'AccountId' must be passed</li>"),
							new CORSArg(jsonParam.CustomerId, (jsonParam.CustomerId == 0), "<li>'CustomerId' must be passed</li>"),
							new CORSArg(jsonParam.GeoFenceName, (string.IsNullOrEmpty(jsonParam.GeoFenceName)), "<li>'GeoFenceName' must be passed</li>"),
							new CORSArg(jsonParam.Radius, (jsonParam.Radius.IsZero()), "<li>'Radius' must be passed.</li>"),
							new CORSArg(jsonParam.CenterLattitude, (jsonParam.CenterLattitude.IsZero()), "<li>'CenterLatitude' must be passed.</li>"),
							new CORSArg(jsonParam.CenterLongitude, (jsonParam.CenterLongitude.IsZero()), "<li>'CenterLongitude' must be passed.</li>")
						};
					if (!CORSArg.ArgumentValidation(oCORSArg, out oResult)) return oResult;
				}
				else
				{
					oResult.Code = (int) SosResultCodes.ArgumentValidationFailed;
					oResult.Message = "Argument must be passed amd must be in JSON format.";
					return oResult;
				}

				#endregion Parameter Validation

				#region Execute Try

				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<IFnsGsAccountGeoFenceCircles> oCircleResult = oService.GeoFenceCircleUpdate(jsonParam.GeoFenceID, jsonParam.Radius, jsonParam.CenterLattitude,
																					   jsonParam.CenterLongitude, oUser.Username);
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
					oResult = new SosCORSResult<GsAccountGeoFenceCircles>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(GsAccountGeoFenceCircles).ToString());
				}
				#endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result
					
			});

			#endregion Authentication
		}

		#endregion Circle CRUD

		#region Rectangle CRUD

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<GsAccountGeoFenceRectangles> GeoRectangleSave(RectangleJsonParam jsonParam)
		{
			#region Authentication
			/** Authenticate. */
			const string METHOD_NAME = "GeoRectangleSave";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region Parameter Validation

				var oResult = new SosCORSResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.ArgumentValidationFailed
					, "Validating Arguments");

				/** Initialize parameters. */
				if (jsonParam != null)
				{
					var aCORSArg = new List<CORSArg>
						{
							new CORSArg(jsonParam.AccountId, (jsonParam.AccountId == 0), "<li>'AccountId' must be passed</li>"),
							new CORSArg(jsonParam.MaxLattitude, (jsonParam.MaxLattitude.IsZero()), "<li>'MaxLattitude' must be passed</li>"),
							new CORSArg(jsonParam.MinLongitude, (jsonParam.MinLongitude.IsZero()), "<li>'MinLongitude' must be passed</li>"),
							new CORSArg(jsonParam.MaxLongitude, (jsonParam.MaxLongitude.IsZero()), "<li>'MaxLongitude' must be passed</li>"),
							new CORSArg(jsonParam.MinLattitude, (jsonParam.MinLattitude.IsZero()), "<li>'MinLattitude' must be passed</li>"),
							new CORSArg(jsonParam.ItemId, (string.IsNullOrEmpty(jsonParam.ItemId)), "<li>'ItemId' must be passed</li>"),
							new CORSArg(jsonParam.ReportMode, (string.IsNullOrEmpty(jsonParam.ReportMode)), "<li>'ReportMode' nust be passed</li>"),
							new CORSArg(jsonParam.GeoFenceName, (string.IsNullOrEmpty(jsonParam.GeoFenceName)), "<li>'GeoFenceName' nust be passed</li>")
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

				#region Execute Try

				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
					IFnsResult<IFnsGsAccountGeoFenceRectangles> oCircleResult = (jsonParam.GeoFenceID <= 0)
						? oService.GeoFenceRectangleCreate(jsonParam.AccountId, jsonParam.ItemId, jsonParam.ReportMode, jsonParam.GeoFenceName, jsonParam.GeoFenceDescription, jsonParam.MaxLattitude, jsonParam.MaxLongitude, jsonParam.MinLattitude, jsonParam.MinLongitude, jsonParam.ZoomLevel, oUser.Username)
						: oService.GeoFenceRectangleUpdate(jsonParam.GeoFenceID, jsonParam.ItemId, jsonParam.ReportMode, jsonParam.GeoFenceName, jsonParam.GeoFenceDescription, jsonParam.MaxLattitude, jsonParam.MaxLongitude, jsonParam.MinLattitude, jsonParam.MinLongitude, jsonParam.ZoomLevel, oUser.Username);
					oResult.Code = oCircleResult.Code;
					oResult.Message = oCircleResult.Message;

					/** Save value if successful. */
                    if (oCircleResult.Code == (int)SosResultCodes.Success)
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
							ZoomLevel = oFnsValue.ZoomLevel,
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
					oResult = new SosCORSResult<GsAccountGeoFenceRectangles>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(GsAccountGeoFenceRectangles).ToString());
				}
				#endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result
			});
			#endregion Authentication
		}

	    [System.Web.Mvc.HttpPost]
	    [System.Web.Mvc.HttpOptions]
		public SosCORSResult<bool> GeoRectangleDelete(RectangleJsonParam jsonParam)
	    {
			#region Authentication
			/** Authenticate. */
			const string METHOD_NAME = "GeoRectangleSave";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
            {
                #region Paramater Validation
                var oResult = new SosCORSResult<bool>((int)SosResultCodes.ArgumentValidationFailed
                                   , "Validating Arguments");
                #endregion Paramater Validation

                #region Execute Try

                try
                {
                    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
                    IFnsResult<bool> oRecResult = oService.GeoFenceRectangleDelete(jsonParam.GeoFenceID, oUser.Username);
                    oResult.Code = oRecResult.Code;
                    oResult.Message = oRecResult.Message;
                    oResult.Value = (bool) oRecResult.GetValue();

                }
                #endregion Execute Try

                #region Execute Catch
                catch (Exception oEx)
				{
					oResult = new SosCORSResult<bool>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(bool).ToString());
				}
                #endregion Execute Catch

				#region Return Result

				/** Return result. */
				return oResult;

				#endregion Return Result
			});
			#endregion Authentication
		}

	    #endregion Rectangle CRUD

		#region Report Events

	    [System.Web.Mvc.HttpPost]
	    [System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<GsDeviceEvents>> ReportEvents(ReportJsonParam jsonParam)
	    {

		    #region Authentication

		    /** Authenticate. */
		    const string METHOD_NAME = "Report Events";
		    return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			    , oUser =>
			    {
					#region Paramater Validation
					var oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.ArgumentValidationFailed
									   , "Validating Arguments");
					#endregion Paramater Validation
					#region Execute Try

				    try
				    {
					    var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IGpsTrackingSerivces>();
						IFnsResult < List < IFnsGsDeviceEvents >> oRecResult = oService.ReportEvents(oUser.CustomerMasterFileId, jsonParam.DeviceId, jsonParam.EventType, jsonParam.Location, jsonParam.StartDate, jsonParam.EndDate, oUser.Username, jsonParam.PageSize, jsonParam.PageNumber);
					    oResult.Code = oRecResult.Code;
					    oResult.Message = oRecResult.Message;
						
						/** Cast from FNS. */
						var tempList = new List<GsDeviceEvents>();
					    foreach (var fnsGsDeviceEvent in (List<IFnsGsDeviceEvents>) oResult.GetValue())
					    {
						    tempList.Add(new GsDeviceEvents
						    {
							    AccountId = fnsGsDeviceEvent.AccountId,
								AccountName = fnsGsDeviceEvent.AccountName,
								CustomerId = fnsGsDeviceEvent.CustomerId,
								Lattitude = fnsGsDeviceEvent.Lattitude,
								Longitude = fnsGsDeviceEvent.Longitude,
								EventName = fnsGsDeviceEvent.EventName,
								EventDate = fnsGsDeviceEvent.EventDate,
								EventID = fnsGsDeviceEvent.EventID,
								EventTypeId = fnsGsDeviceEvent.EventTypeId,
								EventType = fnsGsDeviceEvent.EventType,
								EventTypeUi = fnsGsDeviceEvent.EventTypeUi,
								EventShortDesc = fnsGsDeviceEvent.EventShortDesc,
								CustomerMasterFileId = fnsGsDeviceEvent.CustomerMasterFileId


						    });
					    }

					}
					#endregion Execute Try

					#region Execute Catch
					catch (Exception oEx)
					{
						oResult = new SosCORSResult<List<GsDeviceEvents>>((int)SosResultCodes.ExceptionThrown
							, string.Format("The following exception was thrown: {0}", oEx.Message), typeof(List<GsDeviceEvents>).ToString());
					}

				    #endregion Execute Catch

					#region Return Result

					/** Return result. */
					return oResult;

					#endregion Return Result
			    }
			    );

		    #endregion Authentication
	    }

	    #endregion Report Events
	}


}