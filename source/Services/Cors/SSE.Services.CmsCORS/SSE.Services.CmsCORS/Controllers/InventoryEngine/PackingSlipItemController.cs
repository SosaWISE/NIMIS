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
	public class PackingSlipItemController : ApiController
    {
        //this method will save the a new PackingSlipItem
        [Route("PackingSlipItem/")]
        [HttpPost]
        public CmsCORSResult<IePackingSlipItem> Post([FromBody]IePackingSlipItem value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Create PackingSlipItem";

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
                    argArray.Add(new CORSArg(value.PackingSlipId, (value.PackingSlipId == 0), "<li>'PackingSlipId' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.ItemId, (value.ItemId == ""), "<li>'ItemId' was not passed.</li>"));
                }
                CmsCORSResult<IePackingSlipItem> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IInventoryEngineService>();

                    // ** Prepare arguents
                    // ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsIePackingSlipItem(value.PackingSlipId, value.ProductSkwId, value.ItemId, value.Quantity);
                    IFnsResult<IFnsIePackingSlipItem> fnsResult = ieService.PackingSlipItemCreate(fnsHeader, user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsIePackingSlipItem)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new IePackingSlipItem
                        {
                            PackingSlipItemID = fnsResultValue.PackingSlipItemID,
                            PackingSlipId = fnsResultValue.PackingSlipId,
                            ProductSkwId = fnsResultValue.ProductSkwId,
                            ItemId = fnsResultValue.ItemId,
                            Quantity = fnsResultValue.Quantity,
              
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
