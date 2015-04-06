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
	public class BundleItemsController : ApiController
    {

		[Route("BundleItems/{id}")]
		[HttpGet]
		// GET api/bundleitems/5
		public CmsCORSResult<List<FeBundleItems>> Get(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get BundleItems BY BundleId";
			var result = new CmsCORSResult<List<FeBundleItems>>((int)CmsResultCodes.Initializing
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
					IFnsResult<List<IFnsFeBundleItem>> oFnsModel = oService.BundleItemsRead(id, user.GPEmployeeID);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsBundleItemList = (List<IFnsFeBundleItem>)oFnsModel.GetValue();
					var packetItemList = fnsBundleItemList.Select(fnsFeBundleItem => ConvertTo.CastFnsToFeBundleItems(fnsFeBundleItem)).ToList();

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = packetItemList;
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
