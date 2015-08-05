using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{
	[RoutePrefix("HumanResourceSrv")]
	public class RuSalesRepController : ApiController
    {
		[Route("RuSalesRepList")]
		[HttpGet]
		public CmsCORSResult<List<RuSalesRep>> RuSalesRepList()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get RuSalesRepList";
			var result = new CmsCORSResult<List<RuSalesRep>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
						IFnsResult<List<IFnsRuSalesRep>> oFnsModel = oService.GetRuSalesRepList();
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oRuSalesRepList = ConvertTo.CastFnsToRuSalesRepList((List<IFnsRuSalesRep>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oRuSalesRepList;
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
