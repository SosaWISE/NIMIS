using System;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MonitoringStation
{
	[RoutePrefix("MonitoringStationSrv")]
	public class AccountSalesInformationController : ApiController
    {
        // GET api/accountsalesinformation/5
		[Route("MsAccountSalesInformations/{id}")]
		[HttpGet]
		public CmsCORSResult<MsAccountSalesInformation> Get(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetByAccountId";
			var result = new CmsCORSResult<MsAccountSalesInformation>((int)CmsResultCodes.Initializing
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
					IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var msAccountSalesInformation = ConvertTo.CastFnsToMsAccountSalesInformation((IFnsMsAccountSalesInformation)oFnsModel.GetValue());


					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = msAccountSalesInformation;
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


        // POST api/accountsalesinformation
		[Route("MsAccountSalesInformations")]
		public void Post([FromBody]MsAccountSalesInformation msInfo)
        {
        }

    }
}
