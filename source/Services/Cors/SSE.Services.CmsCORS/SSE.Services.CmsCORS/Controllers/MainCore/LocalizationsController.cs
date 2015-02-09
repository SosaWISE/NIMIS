using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models.CmsModels;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MainCore
{
	[RoutePrefix("MainCoreSrv")]
	public class LocalizationsController : ApiController
    {
        // GET api/localizations
		[Route("Localizations")]
		[HttpGet]
		[HttpOptions]
		public CmsCORSResult<List<McLocalization>> Get()
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Localizations";
			var result = new CmsCORSResult<List<McLocalization>>((int)CmsResultCodes.Initializing
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
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IMainCoreService>();
					IFnsResult<List<IFnsMcLocalization>> fnsResult = mcService.LocalizationsGet(user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (List<IFnsMcLocalization>)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = (from fnsToken in fnsResultValue
											select new McLocalization
											{
												LocalizationID = fnsToken.LocalizationID,
												MSLocalId = fnsToken.MSLocalId,
												LocalizationName = fnsToken.LocalizationName
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
