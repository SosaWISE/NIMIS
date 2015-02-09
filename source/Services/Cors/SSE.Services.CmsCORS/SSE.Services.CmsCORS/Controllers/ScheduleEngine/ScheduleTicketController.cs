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
using SSE.Services.CmsCORS.Models.ScheduleEngine;
using SSE.Services.CmsCORS.Models;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Models.ScheduleEngine;

namespace SSE.Services.CmsCORS.Controllers.ScheduleEngine
{
    [RoutePrefix("ScheduleEngineSrv")]
	public class ScheduleTicketController : ApiController
    {
       
        //this method will save a new ScheduleTicket Record
        [Route("SeScheduleTicket/")]
        [HttpPost]
        public CmsCORSResult<SeScheduleTicket> Post([FromBody]SeScheduleTicket value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Create SeScheduleTicket";

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
                    argArray.Add(new CORSArg(value.AppointmentDate, (value.AppointmentDate == null), "<li>'AppointmentDate' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.TicketId, (value.TicketId == 0), "<li>'TicketId' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.BlockId, (value.BlockId == 0), "<li>'BlockId' was not passed.</li>"));
                
                }
                CmsCORSResult<SeScheduleTicket> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    // ** Prepare arguents
                    // ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsSeScheduleTicket(                        
                            value.TicketId,
                            value.BlockId,
                            value.AppointmentDate,
                            value.TravelTime
                            );
                    IFnsResult<IFnsSeScheduleTicket> fnsResult = seService.SeScheduleTicketCreate(fnsHeader,user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsSeScheduleTicket)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new SeScheduleTicket
                        {
                            ScheduleTicketID = fnsResultValue.ScheduleTicketID,
                            TicketId = fnsResultValue.TicketId,
                            BlockId = fnsResultValue.BlockId,
                            AppointmentDate = fnsResultValue.AppointmentDate,
                            TravelTime = fnsResultValue.TravelTime
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

        //this method will return the list of ScheduleTickets based on the given dates - ScheduleParam contains date range
        [Route("SeScheduleTicketList")]
        [HttpPost]
        public CmsCORSResult<List<SeScheduleTicket>> SeScheduleTicketList([FromBody]ScheduleParam scheduleParam)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get SeScheduleTicketList";
            var result = new CmsCORSResult<List<SeScheduleTicket>>((int)CmsResultCodes.Initializing
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
                        IFnsResult<List<IFnsSeScheduleTicket>> oFnsModel = ieService.SeScheduleTicketList(scheduleParam.AppointmentDateStart, scheduleParam.AppointmentDateEnd);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oTsScheduleTicketList = ConvertTo.CastFnsToSeScheduleTicketList((List<IFnsSeScheduleTicket>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oTsScheduleTicketList;
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



        //this method will return ScheduleTicket object based on TicketId TID
        [Route("SeScheduleTicket/{ticketId}/TID")]
        [HttpGet]
        public CmsCORSResult<SeScheduleTicket> SeScheduleTicketGetByTicketId(long ticketId)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "SeScheduleTicketGetByTicketId";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (ticketId == 0)
                {
                    argArray.Add(new CORSArg(null, (ticketId==0), "<li>No TicketId where passed.</li>"));
                }

                CmsCORSResult<SeScheduleTicket> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    IFnsResult<IFnsSeScheduleTicket> fnsResult = seService.SeScheduleTicketGetByTicketId(ticketId);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var oSeScheduleTicket = ConvertTo.CastFnsToSeScheduleTicket((IFnsSeScheduleTicket)fnsResult.GetValue());
                    if (result.Code == (int)CmsResultCodes.Success && oSeScheduleTicket != null)
                    {
                        result.Value = oSeScheduleTicket;
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



        [Route("SeScheduleTicket/{scheduleTicketId}")]
        [HttpDelete]
        public CmsCORSResult<bool> ScheduleTicketDelete(long scheduleTicketId)
        {
            #region Initialization

            const string METHOD_NAME = "ScheduleTicketDelete";

            #endregion Initialization

            #region Execute

            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>
				{
					new CORSArg(scheduleTicketId, (scheduleTicketId == 0), "<li>'scheduleTicketId' must be passed.</li>")
				};
                CmsCORSResult<bool> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
                    IFnsResult fnsResult = seService.ScheduleTicketDelete(scheduleTicketId, user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;
                    result.Value = true;
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
                // Return result
                return result;
                #endregion Result
            });

            #endregion Executea
        }


    

    }
}
