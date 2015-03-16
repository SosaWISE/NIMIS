using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Reporting;
using SOS.Services.Interfaces.Models.SalesHareReportSrv;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.SalesHareReportSrv
{
	[RoutePrefix("SalesHareReportSrv")]
	public class CreditsAndInstallsController : ApiController
    {

        // POST api/creditsandinstalls
		[Route("CreditsAndInstalls")]
		[HttpPost]
		public CmsCORSResult<List<MsAccountCreditsAndInstalls>> Post([FromBody]CreditsAndInstallsRequest value)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "CreditsAndInstalls POST";
			var result = new CmsCORSResult<List<MsAccountCreditsAndInstalls>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IReportingService>();
						IFnsResult<List<IFnsMsAccountCreditsAndInstalls>> oFnsModel = oService.GetCreditAndInstallsByOfficeIdAndRepId(value.OfficeID, value.SalesRepId, user.GPEmployeeID, value.StartDate, value.EndDate);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var fnsValue = (List<IFnsMsAccountCreditsAndInstalls>)oFnsModel.GetValue();
						var resultValue = fnsValue.Select(fnsMsAccountCreditsAndInstallse => ConvertTo.CastFnsToMsAccountCreditsAndInstalls(fnsMsAccountCreditsAndInstallse)).ToList();

						/** Save success results. */
						result.Code = oFnsModel.Code;
						result.SessionId = user.SessionID;
						result.Message = oFnsModel.Message;
						result.Value = resultValue;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}",
							METHOD_NAME,
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
