using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Funding;
using SOS.Services.Interfaces.Models.Funding;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.Funding
{
	[RoutePrefix("FundingSrv")]
	public class CriteriasController : ApiController
    {
        // GET api/criterias
		[Route("Criterias")]
		[HttpGet]
		public CmsCORSResult<List<FeCriteria>> Get()
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Criterias ALL";
			var result = new CmsCORSResult<List<FeCriteria>>((int)CmsResultCodes.Initializing
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
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IFundingServices>();
					IFnsResult<List<IFnsFeCriteria>> oFnsModel = oService.CriteriaReadAll(user.GPEmployeeID);
					
					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsCriteriaList = (List<IFnsFeCriteria>)oFnsModel.GetValue();
					var criteriaList = fnsCriteriaList.Select(fnsFeCriteria => ConvertTo.CastFnsToFeCriteria(fnsFeCriteria)).ToList();

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = criteriaList;
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
