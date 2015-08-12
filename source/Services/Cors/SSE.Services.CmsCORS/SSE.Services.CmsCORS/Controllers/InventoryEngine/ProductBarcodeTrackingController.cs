using System;
using System.Collections.Generic;
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

namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
    [RoutePrefix("InventoryEngineSrv")]
	public class ProductBarcodeTrackingController : ApiController
	{
        //this method will return a ProductBarcodeTracking based on the ProductBarcodeTrackingID
        [Route("ProductBarcodeTracking/{id}")]
        [HttpGet]
        public CmsCORSResult<IeProductBarcodeTrackingView> Get(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get ProductBarcodeTracking";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(id, (id == 0), "<li>'ProductBarcodeTrackingId' must be passed.</li>"),
					};
                    CmsCORSResult<IeProductBarcodeTrackingView> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<IFnsIeProductBarcodeTrackingView> fnsResult = ieService.ProductBarcodeTrackingViewGetByPBTID(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIeProductBarcodeTrackingView((IFnsIeProductBarcodeTrackingView)fnsResult.GetValue());
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


        //[Route("ProductBarcodeTracking/{id}/PBID")]
        //[HttpGet]
        //public CmsCORSResult<IeProductBarcodeTracking> GetByProductBarcodeId(int id)
        //{
        //    #region Initialize

        //    /** Initialize. */
        //    const string METHOD_NAME = "Get ByProductBarcodeId";

        //    #endregion Initialize

        //    /** Authenticate session first. */
        //    return CORSSecurity.AuthenticationWrapper(METHOD_NAME
        //        , user =>
        //        {
        //            #region Parameter Validation

        //            var argArray = new List<CORSArg>
        //            {
        //                new CORSArg(id, (id == 0), "<li>'ProductBarcodeId' must be passed.</li>"),
        //            };
        //            CmsCORSResult<IeProductBarcodeTracking> result;
        //            if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

        //            #endregion Parameter Validation

        //            #region TRY

        //            try
        //            {
        //                // ** Create Service
        //                var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
        //                IFnsResult<IFnsIeProductBarcodeTracking> fnsResult = ieService.ProductBarcodeTrackingGetByPBID(id);

        //                // ** Save result
        //                result.Code = fnsResult.Code;
        //                result.SessionId = user.SessionID;
        //                result.Message = fnsResult.Message;

        //                if (result.Code == (int)ErrorCodes.Success)
        //                {
        //                    var resultValue = ConvertTo.CastFnsToIeProductBarcodeTracking((IFnsIeProductBarcodeTracking)fnsResult.GetValue());
        //                    result.Value = resultValue;
        //                }
                       
        //            }
        //            #endregion TRY

        //            #region CATCH

        //            catch (Exception ex)
        //            {
        //                result.Code = (int)CmsResultCodes.ExceptionThrown;
        //                result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
        //                    ex.Message);
        //            }

        //            #endregion CATCH

        //            #region Result

        //            return result;

        //            #endregion Result
        //        });
        //}

        // POST InventoryEngineSrv/ProductBarcodeTracking
        [Route("ProductBarcodeTracking/")]
		[HttpPost]
        public CmsCORSResult<IeProductBarcodeTrackingView> Post([FromBody]IeProductBarcodeTracking value)
		{
			#region Initialize

			/** Initialize. */
            const string METHOD_NAME = "Create ProductBarcodeTracking";

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
                    argArray.Add(new CORSArg(value.ProductBarcodeId, (value.ProductBarcodeId == ""), "<li>'ProductBarcodeId' was not passed.</li>"));
                    
                    //argArray.Add(new CORSArg(value.TransferToWarehouseSiteId, (value.TransferToWarehouseSiteId == ""), "<li>'TransferToWarehouseSiteId' was not passed.</li>"));
				}
                CmsCORSResult<IeProductBarcodeTrackingView> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();

					// ** Prepare arguents
					// ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsIeProductBarcodeTracking(
                       // value.ProductBarcodeTrackingTypeId,  
                       "REC",  // receive shipment - default ProductBarcodeTrackingTypeId
                       value.ProductBarcodeId, 
                       value.LocationTypeID,
                       value.LocationID,
                       /* value.TransferToWarehouseSiteId,
                        value.ReturnToVendorId,
                        value.AssignedToCustomerId,
                        value.AssignedToDealerId,
                        value.RtmaNumberId,*/
                        value.Comment

                        );
                    IFnsResult<IFnsIeProductBarcodeTracking> fnsResult = ieService.ProductBarcodeTrackingCreate(fnsHeader, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;
                    
					// ** Get Values
                    var fnsResultValue = (IFnsIeProductBarcodeTracking)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
                        IFnsResult<IFnsIeProductBarcodeTrackingView> fnsResultView = ieService.ProductBarcodeTrackingViewGetByPBTID(fnsResultValue.ProductBarcodeTrackingID);
						/*var resultValue = new IeProductBarcodeTracking
						{	
		                    ProductBarcodeTrackingTypeId =fnsResultValue.ProductBarcodeTrackingTypeId,
                            ProductBarcodeId = fnsResultValue.ProductBarcodeId,
                            TransferToWarehouseSiteId = fnsResultValue.TransferToWarehouseSiteId
						};
						result.Value = resultValue;
                         * */
                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIeProductBarcodeTrackingView((IFnsIeProductBarcodeTrackingView)fnsResultView.GetValue());
                            result.Value = resultValue;
                        }
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