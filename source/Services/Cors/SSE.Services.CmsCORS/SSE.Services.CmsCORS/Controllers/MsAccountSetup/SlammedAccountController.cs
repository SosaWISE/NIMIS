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
	public class SlammedAccountController : ApiController
    {

		[Route("SlammedAccounts/{id}")]
		[HttpGet]
		public CmsCORSResult<MsLeadTakeOver> Get(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "SlammedAccounts Get";
			int countNumbers = 23;
			var result = new CmsCORSResult<MsLeadTakeOver>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				argArray.Add(new CORSArg(id, (id == 0), "<li>'id' was not passed.</li>"));

				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
					IFnsResult<IFnsMsLeadTakeOver> oFnsModel = oService.SlammedAccountsCheck(id, user.GPEmployeeID);
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsLeadTakeOver = (IFnsMsLeadTakeOver)oFnsModel.GetValue();
					var msLeadTakeover = new MsLeadTakeOver
					{
						AccountId = fnsLeadTakeOver.AccountId,
						LeadID = fnsLeadTakeOver.LeadID,
						FullName = fnsLeadTakeOver.FullName,
						StreetAddress = fnsLeadTakeOver.StreetAddress,
						CityStZip = fnsLeadTakeOver.CityStZip,
						AlarmCompanyId = fnsLeadTakeOver.AlarmCompanyId,
						AlarmCompanyName = fnsLeadTakeOver.AlarmCompanyName
					};

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = msLeadTakeover;
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
