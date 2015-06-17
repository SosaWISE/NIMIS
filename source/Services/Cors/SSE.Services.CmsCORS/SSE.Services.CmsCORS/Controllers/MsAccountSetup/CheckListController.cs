using System;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("CheckListSrv")]
	public class CheckListController : ApiController
    {
		[HttpGet, Route("CheckList/{id}/MsAccount")]
		public CmsCORSResult<MsAccountSetupCheckList> Get(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get CheckList for MsAccount";
			var result = new CmsCORSResult<MsAccountSetupCheckList>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region TRY
				try
				{
					// ** Init Service. */
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();

					// ** Call Service
					var fnsResult = service.GetMsAccountSetupCheckList(id, user.GPEmployeeID);

					// ** Check result
					if (fnsResult.Code == (int)CmsResultCodes.Success)
					{
						var keyValues = (IFnsMsAccountSetupCheckList)fnsResult.GetValue();
						var keyValueFinised = ConvertTo.CastFnsToMsAccountSetupCheckList(keyValues);
						result.Value = keyValueFinised;
					}

					/** Save success results. */
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;
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
