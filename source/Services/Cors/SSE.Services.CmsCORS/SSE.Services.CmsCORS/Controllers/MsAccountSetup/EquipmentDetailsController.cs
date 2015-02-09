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
	public class EquipmentDetailsController : ApiController
    {
		[Route("EquipmentTypes/{equipmentTypeId}/zoneEventTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountEventZoneEventTypes>> ZoneEventTypes(int equipmentTypeId, string msoid)
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

		public CmsCORSResult<List<MsAccountLocation>> AccountLocations
	}
}
