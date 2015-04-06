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
	public class BundlesController : ApiController
    {
		[Route("Bundles")]
		[HttpGet]
		// GET api/bundles
		public CmsCORSResult<List<FeBundle>> Get()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Bundles ALL";
			var result = new CmsCORSResult<List<FeBundle>>((int)CmsResultCodes.Initializing
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
					IFnsResult<List<IFnsFeBundle>> oFnsModel = oService.BundleReadAll(user.GPEmployeeID);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsBundleList = (List<IFnsFeBundle>)oFnsModel.GetValue();
					var bundleList = fnsBundleList.Select(fnsFeBundle => ConvertTo.CastFnsToFeBundle(fnsFeBundle)).ToList();

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = bundleList;
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

		[Route("Bundles/{id}")]
		[HttpGet]
		// GET api/bundles/5
		public CmsCORSResult<FeBundle> Get(int id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Bundle";
			var result = new CmsCORSResult<FeBundle>((int)CmsResultCodes.Initializing
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
					IFnsResult<IFnsFeBundle> oFnsModel = oService.BundleRead(id, user.GPEmployeeID);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						result.Code = oFnsModel.Code;
						result.Message = oFnsModel.Message;
						return result;
					}

					/** Setup return corsResult. */
					var fnsFeBundle = (IFnsFeBundle) oFnsModel.GetValue();
					var bundle = ConvertTo.CastFnsToFeBundle(fnsFeBundle);

					/** Save success results. */
					result.Code = (int)CmsResultCodes.Success;
					result.SessionId = user.SessionID;
					result.Message = "Success";
					result.Value = bundle;
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
