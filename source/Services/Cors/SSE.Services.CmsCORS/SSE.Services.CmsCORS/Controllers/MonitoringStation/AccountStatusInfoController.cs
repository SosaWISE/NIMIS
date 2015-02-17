using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models.Reporting;
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
		public CmsCORSResult<List<MsAccountOnlineStatusInfo>> Get(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetByAccountId";
			var result = new CmsCORSResult<List<MsAccountOnlineStatusInfo>>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region TRY

				//var resultList = new List<MsAccountOnlineStatusInfo>();
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Credit Rank: Excellent",
				//	Value="Excellent",
				//	Status = "Good"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Contract: Active",
				//	Value = "Last Date: 1/29/2015 | Months Left: 18",
				//	Status = "Good"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Billing:  Past Due",
				//	Value = "21 days past due, $57.99 due.",
				//	Status = "Warning"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Funding: In House",
				//	Value = "Install Date: 02/02/2015",
				//	Status = "Good"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Monitoring Station: Monitronics",
				//	Value = "Online Date: 02/01/2015 Monitronics Confirmation #: 4323423",
				//	Status = "Good"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "CS Status: Out of Service",
				//	Value = "Online Date: 02/01/2015 | Nexsense Confirmation #: 1234",
				//	Status = "Critical"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Cell Provider: Alarm.com",
				//	Value = "ADC Customer #: 2423423",
				//	Status = "Good"
				//});
				//resultList.Add(new MsAccountOnlineStatusInfo
				//{
				//	KeyName = "Cellular: Unregistered",
				//	Value = "Serial #: 9516541236",
				//	Status = "Critical"
				//});

				try
				{
					// ** Init Service. */
					var service = SosServiceEngine.Instance.FunctionalServices.Instance<IReportingService>();

					// ** Call Service
					var fnsResult = service.GetAccountOnlineStatusInfoByAccountId(id, user.GPEmployeeID);

					// ** Check result
					if (fnsResult.Code == (int) CmsResultCodes.Success)
					{
						var keyValueList = (List<IFnsMsAccountOnlineStatusInfo>)fnsResult.GetValue();
						var keyValueListFinised = keyValueList.Select(ConvertTo.CastFnsToMsAccountOnlineStatusInfo).ToList();
						result.Value = keyValueListFinised;
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
