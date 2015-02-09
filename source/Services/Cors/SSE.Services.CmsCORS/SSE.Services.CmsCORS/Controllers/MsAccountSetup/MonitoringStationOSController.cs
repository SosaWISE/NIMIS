using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class MonitoringStationOSController : ApiController
    {
		[Route("MonitoringStationOS/{msoid}/ZoneEventTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountEventZoneEventTypes>> ZoneEventTypes(string msoid, int equipmentTypeId)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get ZoneEventTypes";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region ARG VALIDATION

				var argArray = new List<CORSArg>
				{
					new CORSArg(msoid, string.IsNullOrEmpty(msoid), "<li>'MonitoringStationId' Has to be passed.</li>"),
					new CORSArg(equipmentTypeId, (equipmentTypeId == 0), "<li>'EquipmentTypeId' Has to be passed.</li>"),
				};
				//CmsCORSResult<List<MsAccountEventZoneEventTypes>> result;
				CmsCORSResult<List<MsAccountEventZoneEventTypes>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;


				#endregion ARG VALIDATION

				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsAccountEventZoneEventTypes>> oFnsModel = oService.ZoneEventTypesGet(msoid, equipmentTypeId, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oMsPanelTypeList = ConvertTo.CastFnsToMsAccountZoneEventTypeList((List<IFnsMsAccountEventZoneEventTypes>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oMsPanelTypeList;
				}

				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result

			});
		}

		[Route("MonitoringStationOS/{msoid}/EquipmentLocations")]
		[HttpGet]
		public CmsCORSResult<List<MsEquipmentLocation>> EquipmentLocations(string msoid)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get ZoneEventTypes";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region ARG VALIDATION

				var argArray = new List<CORSArg>
				{
					new CORSArg(msoid, string.IsNullOrEmpty(msoid), "<li>'MonitoringStationId' Has to be passed.</li>"),
				};

				CmsCORSResult<List<MsEquipmentLocation>> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;


				#endregion ARG VALIDATION

				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<List<IFnsMsEquipmentLocation>> oFnsModel = oService.EquipmentLocationsGet(msoid, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var oMsPanelTypeList = ConvertTo.CastFnsToMsEquipmentLocationList((List<IFnsMsEquipmentLocation>)oFnsModel.GetValue());

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = oMsPanelTypeList;
				}

				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					result.Code = (int)CmsResultCodes.ExceptionThrown;
					result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
						ex.Message);
				}

				#endregion CATCH

				#region Result

				return result;

				#endregion Result

			});
		}


		[Route("MonitoringStationOS/{msosid}/AccountZoneTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountZoneType>> AccountZoneTypes(string msosid)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get AccountZoneTypes";
			var result = new CmsCORSResult<List<MsAccountZoneType>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<List<IFnsMsAccountZoneType>> oFnsModel = oService.ZoneTypesGet(msosid, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsAccountZoneTypesList = ConvertTo.CastFnsToMsAccountZoneTypesList((List<IFnsMsAccountZoneType>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsAccountZoneTypesList;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result

				});
		}
        

        //added by reagan - not sure if this is the right place for the equipment list retrieval to be used by add existing equipment dialog
        [Route("MonitoringStationOS/EquipmentList")]
        [HttpGet]
        public CmsCORSResult<List<MsEquipment>> EquipmentList()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get EquipmentList";
            var result = new CmsCORSResult<List<MsEquipment>>((int)CmsResultCodes.Initializing
                , string.Format("Initializing {0}.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region TRY

                    try
                    {
                        // ** Create Service
                        var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
                        IFnsResult<List<IFnsMsEquipmentsView>> oFnsModel = oService.MsEquipmentsGet(user.GPEmployeeID);
                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oMsEquipmentsList = ConvertTo.CastFnsToMsEquipmentList((List<IFnsMsEquipmentsView>)oFnsModel.GetValue());


                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oMsEquipmentsList;
                    }
                    #endregion TRY

                    #region CATCH

                    catch (Exception ex)
                    {
                        result.Code = (int)CmsResultCodes.ExceptionThrown;
                        result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
                            ex.Message);
                    }

                    #endregion CATCH

                    #region Result

                    return result;

                    #endregion Result

                });
        }

		//added by reagan - not sure if this is the right place for the equipment list retrieval to be used by add existing equipment dialog
		[Route("MonitoringStationOS/EquipmentExistingList")]
		[HttpGet]
		public CmsCORSResult<List<MsEquipment>> EquipmentExistingList()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get EquipmentList";
			var result = new CmsCORSResult<List<MsEquipment>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<List<IFnsMsEquipmentsView>> oFnsModel = oService.MsEquipmentExistingsGet(user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsEquipmentsList = ConvertTo.CastFnsToMsEquipmentList((List<IFnsMsEquipmentsView>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsEquipmentsList;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result

				});
		}


	}
}