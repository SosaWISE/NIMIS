using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;
using SOS.Services.Interfaces.Models.InventoryEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
	[RoutePrefix("InventoryEngineSrv")]
	public class LocationController : ApiController
    {
  
        // GET api/Location/5     -- 5 here referring to the LocationTypeId
        // this method will return a list of Locatons based on the LocationTypeId
		[Route("Locations/{id}")]
		[HttpGet]
		public CmsCORSResult<List<IeLocation>> Get(string id)
        {
		#region Initialize

			/** Initialize. */
            const string METHOD_NAME = "Get Locations";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(id,
							(id == ""), "<li>'LocationTypeId' must be passed.</li>"),
					};
                    CmsCORSResult<List<IeLocation>> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY
                    try
                    {
                        // ** Create Service
      
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<List<IFnsIeLocation>> oFnsModel = ieService.LocationListGet(id);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oIeLocationList = ConvertTo.CastFnsToIeLocationList((List<IFnsIeLocation>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oIeLocationList;
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
