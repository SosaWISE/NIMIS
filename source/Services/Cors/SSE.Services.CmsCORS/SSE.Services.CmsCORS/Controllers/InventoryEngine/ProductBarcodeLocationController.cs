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
	public class ProductBarcodeLocationController : ApiController
    {
  
        // GET api/ProductBarcodeLocation/5     -- 5 here referring to the LocationId
        // this method will return a list of items with productbarcode based on the specified locationid
		[Route("ProductBarcodeLocations/{id}")]
		[HttpGet]
		public CmsCORSResult<List<IeProductBarcodeLocation>> Get(string id)
        {
		#region Initialize

			/** Initialize. */
            const string METHOD_NAME = "Get ProductBarcodeLocations";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(id,
							(id == ""), "<li>'ProductBarcodeLocationTypeId' must be passed.</li>"),
					};
                    CmsCORSResult<List<IeProductBarcodeLocation>> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY
                    try
                    {
                        // ** Create Service
      
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<List<IFnsIeProductBarcodeLocation>> oFnsModel = ieService.ProductBarcodeLocationListGet(id);



                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oIeProductBarcodeLocationList = ConvertTo.CastFnsToIeProductBarcodeLocationList((List<IFnsIeProductBarcodeLocation>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oIeProductBarcodeLocationList;
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



        // GET api/ProductBarcodeLocation/1000/PBID     -- 1000 here referring to the ProductBarcodeID
        // this method will return an item with productbarcode and location based on productbarcodeid
        [Route("ProductBarcodeLocations/{id}/PBID")]
        [HttpGet]
        public CmsCORSResult<IeProductBarcodeLocation> GetByPBID(string id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get ProductBarcodeLocation by PBID";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(id,
							(id == ""), "<li>'ProductBarcodeId' must be passed.</li>"),
					};
                    CmsCORSResult<IeProductBarcodeLocation> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY
                    try
                    {
                        // ** Create Service

                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<IFnsIeProductBarcodeLocation> oFnsModel = ieService.ProductBarcodeLocationGetByPBID(id);



                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oIeProductBarcodeLocation = ConvertTo.CastFnsToIeProductBarcodeLocation((IFnsIeProductBarcodeLocation)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oIeProductBarcodeLocation;
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
