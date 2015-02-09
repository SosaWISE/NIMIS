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
using SOS.FunctionalServices.Models.InventoryEngine;

namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
	[RoutePrefix("InventoryEngineSrv")]
	public class PackingSlipController : ApiController
    {
        //this method will return a PackingSLip based on POID (PurchaseOrderID)
        [Route("PackingSlip/{id}/POID")]
        [HttpGet]
        public CmsCORSResult<IePackingSlip> GetByPuchaseOrderId(int id)
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
						new CORSArg(id, (id == 0), "<li>'Purchase Order Number' must be passed.</li>"),
					};
                    CmsCORSResult<IePackingSlip> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<IFnsIePackingSlip> fnsResult = ieService.PackingSlipGetByPOID(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIePackingSlip((IFnsIePackingSlip)fnsResult.GetValue());
                            result.Value = resultValue;
                        }
                       /* else { 
                            //not sure if this will be the correct code // to put logic in here
                            //if not found call PackingSlipCreate
                            ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                            fnsResult = ieService.PackingSlipCreate(id, user.GPEmployeeID);

                            // ** Save result
                            result.Code = fnsResult.Code;
                            result.SessionId = user.SessionID;
                            result.Message = fnsResult.Message;
                            var resultValue = ConvertTo.CastFnsToIePackingSlip((IFnsIePackingSlip)fnsResult.GetValue());
                            result.Value = resultValue;
                        
                        }*/
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

        //this method will return a PackingSLip based on GPPONumber (GPPONumber)
        [Route("PackingSlip/{id}/GPPON")]
        [HttpGet]
        public CmsCORSResult<List<IePackingSlip>> GetByGPPONumber(string id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get GPPONumber";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>
					{
						new CORSArg(id, (id == ""), "<li>'GPPONumber' must be passed.</li>"),
					};
                    CmsCORSResult<List<IePackingSlip>> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();
                        IFnsResult<List<IFnsIePackingSlip>> fnsResult = ieService.PackingSlipGetByGPPON(id);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        if (result.Code == (int)ErrorCodes.Success)
                        {
                            var resultValue = ConvertTo.CastFnsToIePackingSlip((List<IFnsIePackingSlip>)fnsResult.GetValue());

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



        //this method will save a new IePackingSlip record
        [Route("PackingSlip/")]
        [HttpPost]
        public CmsCORSResult<IePackingSlip> Post([FromBody]IePackingSlip value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Create PackingSlip";

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
                    argArray.Add(new CORSArg(value.PackingSlipNumber, (value.PackingSlipNumber == ""), "<li>'PackingSlipNumber' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.PurchaseOrderId, (value.PurchaseOrderId == 0), "<li>'PurchaseOrderId' was not passed.</li>"));
                }
                CmsCORSResult<IePackingSlip> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();

                    // ** Prepare arguents
                    // ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsIePackingSlip(value.PackingSlipNumber, value.PurchaseOrderId);
                    IFnsResult<IFnsIePackingSlip> fnsResult = ieService.PackingSlipCreate(fnsHeader, user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsIePackingSlip)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new IePackingSlip
                        {
                            PackingSlipID = fnsResultValue.PackingSlipID,
                            PackingSlipNumber = fnsResultValue.PackingSlipNumber,
                            PurchaseOrderId = fnsResultValue.PurchaseOrderId
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
