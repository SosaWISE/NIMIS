using System;
using System.Web.Http;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers
{
	public class QualifyController : ApiController
	{
		#region Create Customer

		public CmsCORSResult<AeCustomer> CreateCustomer(LeadParam jsonParam)
		{
			#region Initialize

			const string METHOD_NAME = "CreateCustomer";
			var oResult = new CmsCORSResult<AeCustomer>((int) CmsResultCodes.CookieInvalid, "Session has expired.")
			{
				Value = null
			};

			#endregion Initialize
			
			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region TRY

				try
				{
				
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Not implemented";
				}
				#endregion TRY

				#region CATCH

				catch (Exception ex)
				{
					oResult.Code = (int)ErrorCodes.ExceptionThrown;
					oResult.Message = ex.Message;
				}

				#endregion CATCH

				#region Return result

				return oResult;

				#endregion Return result
			});
		}
		#endregion Create Customer
	}
}

