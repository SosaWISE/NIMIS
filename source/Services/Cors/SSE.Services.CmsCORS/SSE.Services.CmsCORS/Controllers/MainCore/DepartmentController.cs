using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.MainCore;
using SOS.Services.Interfaces.Models.MainCore;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SSE.Services.CmsCORS.Controllers.MainCore
{
	[RoutePrefix("MainCoreSrv")]
	public class DepartmentController : ApiController
    {

		[Route("Departments")]
		[HttpGet]
		[HttpOptions]
		public CmsCORSResult<List<McDepartment>> GetAll()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Department";
			var result = new CmsCORSResult<List<McDepartment>>((int)CmsResultCodes.Initializing
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
						IFnsResult<List<IFnsMcDepartment>> fnsResult = mcService.DepartmentGet(user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsMcDepartment>) fnsResult.GetValue();
						if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsToken in fnsResultValue
								select new McDepartment
								{
									DepartmentID = fnsToken.DepartmentID,
									DepartmentName = fnsToken.DepartmentName,
									

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
