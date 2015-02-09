using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;
using SOS.Services.Interfaces.Models.ScheduleEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Models.ScheduleEngine;

namespace SSE.Services.CmsCORS.Controllers.ScheduleEngine
{
    [RoutePrefix("ScheduleEngineSrv")]
	public class ZipCodeController : ApiController
    {


        //this method will return SeZipCode based on ZipCode
        [Route("SeZipCode/{id}/ZC")]
        [HttpGet]
        public CmsCORSResult<SeZipCode> SeZipCodeGetByZipCode(string id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "SeZipCodeGetByZipCode";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (id == null)
                {
                    argArray.Add(new CORSArg(null, (true), "<li>No ZipCode where passed.</li>"));
                }

                CmsCORSResult<SeZipCode> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    IFnsResult<IFnsSeZipCode> fnsResult = seService.SeZipCodeByZC(id);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var oSeZipCode = ConvertTo.CastFnsToSeZipCode((IFnsSeZipCode)fnsResult.GetValue());
                    if (result.Code == (int)CmsResultCodes.Success && oSeZipCode != null)
                    {
                        result.Value = oSeZipCode;
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
