using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;
using SOS.FunctionalServices.Models.InventoryEngine;
using SOS.Services.Interfaces.Models.InventoryEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
{
    [RoutePrefix("InventoryEngineSrv")]
	public class ProductBarcodeController : ApiController
	{
        //this method will return a ProductBarcode based on the PBID ProductBarcodeID
        [Route("ProductBarcode/{id}/PBID")]
        [HttpGet]
        public CmsCORSResult<IeProductBarcode> GetByProductBarcodeId(string id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get ByProductBarcodeId";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(id, (id == ""), "<li>'Product Barcode Id' must be passed.</li>"),
					};
                    CmsCORSResult<IeProductBarcode> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<IFnsIeProductBarcode> fnsResult = ieService.ProductBarcodeGetByPBID(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIeProductBarcode((IFnsIeProductBarcode)fnsResult.GetValue());
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


        // POST InventoryEngineSrv/productBarcode
        // this method will create a new IeProductBarcode record
        [Route("ProductBarcode/")]
		[HttpPost]
        public CmsCORSResult<IeProductBarcode> Post([FromBody]IeProductBarcode value)
		{
			#region Initialize

			/** Initialize. */
            const string METHOD_NAME = "Create ProductBarcode";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (value == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					argArray.Add(new CORSArg(value.ProductBarcodeID, (value.ProductBarcodeID == ""), "<li>'ProductBarcodeID' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.PurchaseOrderItemId, (value.PurchaseOrderItemId==0), "<li>'PurchaseOrderItemId' was not passed.</li>"));
				}
				CmsCORSResult<IeProductBarcode> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();

					// ** Prepare arguents
					// ReSharper disable once PossibleNullReferenceException
					var fnsHeader = new FnsIeProductBarcode(value.ProductBarcodeID, value.PurchaseOrderItemId);
                    IFnsResult<IFnsIeProductBarcode> fnsResult = ieService.ProductBarcodeCreate(fnsHeader, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
                    var fnsResultValue = (IFnsIeProductBarcode)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = new IeProductBarcode
						{	
		                    ProductBarcodeID = fnsResultValue.ProductBarcodeID,
                            PurchaseOrderItemId = fnsResultValue.PurchaseOrderItemId,
                            LastProductBarcodeTrackingId = fnsResultValue.LastProductBarcodeTrackingId,
                            ProductBarcodeBundleId = fnsResultValue.ProductBarcodeBundleId
						};
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