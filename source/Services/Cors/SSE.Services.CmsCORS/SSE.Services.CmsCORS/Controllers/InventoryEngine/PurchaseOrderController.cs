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
using SOS.FunctionalServices.Contracts.Helper;

namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
	[RoutePrefix("InventoryEngineSrv")]
	public class PurchaseOrderController : ApiController
    {

        //this method will return a PurchaseOrder by GPPO Purchase Order Number
		[Route("PurchaseOrder/{id}/gppo")]
		[HttpGet]
		public CmsCORSResult<IePurchaseOrder> GetByGPPO(string id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get ByGPPO";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(id, string.IsNullOrEmpty(id), "<li>'Purchase Order Number' must be passed.</li>"),
					};
					CmsCORSResult<IePurchaseOrder> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
						IFnsResult<IFnsIePurchaseOrder> fnsResult = ieService.PurchaseOrderGetByGPPO(id, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						if (result.Code == (int)ErrorCodes.Success)
						{
							var resultValue = ConvertTo.CastFnsToIePurchaseOrder((IFnsIePurchaseOrder)fnsResult.GetValue());
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



        //this method will return a PurchaseOrder by POID PurchaseOrderID
		[Route("PurchaseOrder/{id}")]
        [HttpGet]
        public CmsCORSResult<IePurchaseOrder> Get(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get ByID";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(id, (id == 0), "<li>'Purchase Order ID' must be passed.</li>"),
					};
                    CmsCORSResult<IePurchaseOrder> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<IFnsIePurchaseOrder> fnsResult = ieService.PurchaseOrderGet(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIePurchaseOrder((IFnsIePurchaseOrder)fnsResult.GetValue());
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


        //this method will return PO number listings based on vendor id and count to be displayed.
        //vid = vendorid
        [Route("PurchaseOrder/{vid}/{count}")]
        [HttpGet]
        public CmsCORSResult<List<IePurchaseOrder>> Get(string vid, string count)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get Purchase Order List by vendor id";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(vid,(vid == ""), "<li>'VendorId' must be passed.</li>"),
                        new CORSArg(count, (count == ""), "<li>'Count' was not passed.</li>")
					};
      
                    CmsCORSResult<List<IePurchaseOrder>> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY
                    try
                    {
                        // ** Create Service

                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<List<IFnsIePurchaseOrder>> oFnsModel = ieService.PurchaseOrderListGetByVendorID(vid, count);



                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oIePurchaseOrderList = ConvertTo.CastFnsToIePurchaseOrderList((List<IFnsIePurchaseOrder>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oIePurchaseOrderList;
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
