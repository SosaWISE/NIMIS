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

namespace SSE.Services.CmsCORS.Controllers.ScheduleEngine
{
	[RoutePrefix("ScheduleEngineSrv")]
	public class TicketTypeController : ApiController
    {
        //this method will return a list of TicketTypes (Service, Offline etc)
        [Route("TicketTypeList/")]
		[HttpGet]
        public CmsCORSResult<List<SeTicketType>> TicketTypeList()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get TicketTypeList";
            var result = new CmsCORSResult<List<SeTicketType>>((int)CmsResultCodes.Initializing
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
      
                        var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
                        IFnsResult<List<IFnsSeTicketType>> oFnsModel = ieService.SeTicketTypeList();

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oTsTicketTypeList = ConvertTo.CastFnsToSeTicketTypeList((List<IFnsSeTicketType>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oTsTicketTypeList;
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
