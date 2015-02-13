using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MonitoringStation
{
	[RoutePrefix("MonitoringStationSrv")]
	public class AccountStatusInfoController : ApiController
    {
        // GET api/accountstatusinfo/5
		[Route("MsAccountStatusInformations/{id}")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountStatusInfo>> Get(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetByAccountId";
			var result = new CmsCORSResult<List<MsAccountStatusInfo>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region TRY

				var ResultList = new List<MsAccountStatusInfo>();
				ResultList.Add(new MsAccountStatusInfo
				{
					KeyName = "Credit Rank",
					Value="Excellent",
					Status = "Green"
				});

				try
				{
					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = ResultList;
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
