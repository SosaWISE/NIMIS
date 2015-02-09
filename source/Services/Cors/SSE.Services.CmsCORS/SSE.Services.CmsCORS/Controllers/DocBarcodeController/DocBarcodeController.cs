using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.DocBarcode;
using SOS.FunctionalServices.Models.DocBarcode;
using SOS.Services.Interfaces.Models.DocBarcode;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace SSE.Services.CmsCORS.Controllers.DocBarcodeController
{
    public class DocBarcodeController : Controller
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpOptions]

        public CmsCORSResult<BxBarcode> BarcodeGenerate([FromBody]BxBarcode barcodeInfo)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Post Barcode";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>

                        {
                            new CORSArg(barcodeInfo.BarcodeTypeId,
                                (string.IsNullOrEmpty(barcodeInfo.BarcodeTypeId)), "<li>'BarcodeTypeId' can not be blank.</li>"),

                            new CORSArg(barcodeInfo.ForeignKey,
                                (string.IsNullOrEmpty(barcodeInfo.ForeignKey)), "<li>'ForeignKey' can not be blank.</li>"),

						

                        };
                        CmsCORSResult<BxBarcode> result;
                        if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                        #endregion Parameter Validation

                        #region TRY

                        try
                        {
                            // ** Create Service
                            var barcodeService = SosServiceEngine.Instance.FunctionalServices.Instance<IBarcodeService>();

                            // ** Bind new data
                            var fnsBxBarcode = new FnsBxBarcode
                            {
                                BarcodeID = barcodeInfo.BarcodeID,
                                BarcodeTypeId = barcodeInfo.BarcodeTypeId,
                                ForeignKey = barcodeInfo.ForeignKey,
                                BarcodeNumber = barcodeInfo.BarcodeNumber,

                            };

                            IFnsResult<IFnsBxBarcode> fnsResult = barcodeService.BarcodeCreate(fnsBxBarcode, user.GPEmployeeID);

                            // ** Save result
                            result.Code = fnsResult.Code;
                            result.SessionId = user.SessionID;
                            result.Message = fnsResult.Message;

                            // ** Get Values
                            if (result.Code == (int) CmsResultCodes.Success)
                            {
                                fnsBxBarcode = (FnsBxBarcode) fnsResult.GetValue();
                                var resultValue = new BxBarcode
                                {
                                    BarcodeID = fnsBxBarcode.BarcodeID,
                                    BarcodeTypeId = fnsBxBarcode.BarcodeTypeId,
                                    ForeignKey = fnsBxBarcode.ForeignKey,
                                    BarcodeNumber = fnsBxBarcode.BarcodeNumber,

                                };

                                result.Value = resultValue;
                            }
                        }
                            #endregion TRY

                        #region CATCH

                        catch (Exception ex)
                        {
                            result.Code = (int) CmsResultCodes.ExceptionThrown;
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
