using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Services.Interfaces.Models.HumanResources;
using SOS.FunctionalServices.Contracts.Models.HumanResource;



namespace SSE.Services.CmsCORS.Controllers.InventoryEngine
{
	[RoutePrefix("HumanResourceSrv")]
	public class RuTeamLocationController : ApiController
    {
        [Route("RuTeamLocationList")]
        [HttpGet]
        public CmsCORSResult<List<RuTeamLocation>> RuTeamLocationList()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get RuTeamLocationList";
            var result = new CmsCORSResult<List<RuTeamLocation>>((int)CmsResultCodes.Initializing
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
                        IFnsResult<List<IFnsRuTeamLocation>> oFnsModel = oService.GetRuTeamLocationList();
                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oRuTeamLocationList = ConvertTo.CastFnsToRuTeamLocationList((List<IFnsRuTeamLocation>)oFnsModel.GetValue());


                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oRuTeamLocationList;
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
