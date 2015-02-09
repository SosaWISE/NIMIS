using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;
using SOS.Services.Interfaces.Models.InventoryEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.FunctionalServices.Contracts.Helper;

namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
	[RoutePrefix("InventoryEngineSrv")]
	public class WarehouseSiteController : ApiController
    {
  
        // GET api/PurchaseOrderItem/5     -- 5 here referring to the PurchaseOrderID
        //this method will return a list of WarehourseSites
		[Route("WarehouseSiteList/")]
		[HttpGet]
        public CmsCORSResult<List<IeWarehouseSite>> WarehouseSiteList()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get WarehouseSiteList";
            var result = new CmsCORSResult<List<IeWarehouseSite>>((int)CmsResultCodes.Initializing
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
      
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<List<IFnsIeWarehouseSite>> oFnsModel = ieService.WarehouseSiteListGet();

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oIeWarehouseSiteList = ConvertTo.CastFnsToIeWarehouseSiteList((List<IFnsIeWarehouseSite>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oIeWarehouseSiteList;
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
