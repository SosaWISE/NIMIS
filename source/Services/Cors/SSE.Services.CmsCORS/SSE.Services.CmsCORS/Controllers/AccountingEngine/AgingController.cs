using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
{
	[RoutePrefix("AccountingEngineSrv")]
	public class AgingController : ApiController
    {
        // GET api/aging/5
		[Route("Aging/{id}")]
		[HttpGet]
		public CmsCORSResult<List<AeAging>> Get(long id)
        {
			//return new CmsCORSResult<List<AeAging>>((int)CmsResultCodes.ImplementationMissing, "This methos has not been implemented.");
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Department";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(id,
							(id == 0), "<li>'CMFID' must be passed.</li>"),
					};
					CmsCORSResult<List<AeAging>> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsAeAging>> fnsResult = mcService.AgingGetByCMFID(id, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsAeAging>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsToken in fnsResultValue
											   select new AeAging
											   {
												   CMFID = fnsToken.CustomerMasterFileID,
												   Age = fnsToken.AgingStep,
												   Value = fnsToken.Value
											   }).ToList();

							result.Value = resultValue;
						}
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
