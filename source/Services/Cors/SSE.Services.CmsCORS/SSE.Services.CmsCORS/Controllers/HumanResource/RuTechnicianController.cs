using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using SOS.Services.Interfaces.Models.HumanResources;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.HumanResource
{

    /*this controller will cater the need of Extracting Technician info
     * primarily used on Scheduling module
     * i'm a bit confused on how it differs from Qualify/TecnicianController  (this controller uses companyId)
     * but this RuTechnicianController will be using gpemployeeid
     * 
     * also RuTechInfo is binded with AccountId , RuTechnician is not
     * by reagan 09/01/2014
     */
    [RoutePrefix("HumanResourceSrv")]
	public class RuTechnicianController : ApiController
    {
        [Route("RuTechnicianList")]
        [HttpGet]
        public CmsCORSResult<List<RuTechnician>> RuTechnicianList()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get RuTechnicianList";
            var result = new CmsCORSResult<List<RuTechnician>>((int)CmsResultCodes.Initializing
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
                        var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
                        IFnsResult<List<IFnsRuTechnician>> oFnsModel = oService.GetRuTechnicianList();
                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oRuTechnicianList = ConvertTo.CastFnsToRuTechnicianList((List<IFnsRuTechnician>)oFnsModel.GetValue());


                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oRuTechnicianList;
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

        //Retieve Technician by TechnicianId TID
        [Route("RuTechnician/{id}/TID")]
        [HttpGet]
        public CmsCORSResult<RuTechnician> GetRuTechnicianByTechnicianId(string id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get GetRuTechnicianByTechnicianId";
            var result = new CmsCORSResult<RuTechnician>((int)CmsResultCodes.Initializing
                , string.Format("Initializing {0}.", METHOD_NAME));

            #endregion Initializes

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {

                    #region TRY
                    try
                    {
                        // ** Create Service

                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IHumanResourceService>();
                        IFnsResult<IFnsRuTechnician> oFnsModel = ieService.RuTechnicianGetByTechnicianId(id);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oRuTechnician = ConvertTo.CastFnsToRuTechnician((IFnsRuTechnician)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oRuTechnician;
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
