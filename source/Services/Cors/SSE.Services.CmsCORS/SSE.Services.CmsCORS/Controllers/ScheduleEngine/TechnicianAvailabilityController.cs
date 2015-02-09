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
	public class TechnicianAvailabilityController : ApiController
    {

        //this method will save a new SeTechnicianAvailability record
        [Route("SeTechnicianAvailability/")]
        [HttpPost]
        public CmsCORSResult<SeTechnicianAvailability> Create([FromBody]SeTechnicianAvailability seTechnicianAvailability)
        {
            //call update 
            return this.Update((seTechnicianAvailability != null) ? seTechnicianAvailability.TechnicianAvailabilityID : 0, seTechnicianAvailability);
        }


        [Route("SeTechnicianAvailability/{id}")]
        [HttpPost]
        public CmsCORSResult<SeTechnicianAvailability> Update(long id, [FromBody]SeTechnicianAvailability seTechnicianAvailability)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Create/Update SeTechnicianAvailability";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (seTechnicianAvailability == null)
                {
                    argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                }
                else
                {
                    //need to add validation here

                    argArray.Add(new CORSArg(seTechnicianAvailability.TechnicianId, (seTechnicianAvailability.TechnicianId == ""), "<li>'TechnicianIde' was not passed.</li>"));
                    argArray.Add(new CORSArg(seTechnicianAvailability.StartDateTime, (seTechnicianAvailability.StartDateTime == null), "<li>'Start Date' was not passed.</li>"));
                    argArray.Add(new CORSArg(seTechnicianAvailability.EndDateTime, (seTechnicianAvailability.EndDateTime == null), "<li>'End Date' was not passed.</li>"));

                }
                CmsCORSResult<SeTechnicianAvailability> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    // ** Prepare arguents
                    seTechnicianAvailability.TechnicianId = user.GPEmployeeID;

                    // ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsSeTechnicianAvailability(
                            seTechnicianAvailability.TechnicianAvailabilityID,
                            seTechnicianAvailability.TechnicianId,
                            seTechnicianAvailability.StartDateTime,
                            seTechnicianAvailability.EndDateTime
                            );
                    IFnsResult<IFnsSeTechnicianAvailability> fnsResult = null;

                    if (seTechnicianAvailability.TechnicianAvailabilityID == 0)
                    {
                        fnsResult = seService.SeTechnicianAvailabilityCreate(fnsHeader, user.GPEmployeeID);
                    }
                    else {
                        fnsResult = seService.SeTechnicianAvailabilityUpdate(fnsHeader, user.GPEmployeeID);
                    }

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsSeTechnicianAvailability)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new SeTechnicianAvailability
                        {
                            TechnicianAvailabilityID = fnsResultValue.TechnicianAvailabilityID,
                            StartDateTime = fnsResultValue.StartDateTime,
                            EndDateTime = fnsResultValue.EndDateTime,
                            TechnicianId = fnsResultValue.TechnicianId

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

        //this method will return the list of TechAvailability based on the given TechnicianID
        [Route("SeTechnicianAvailabilityList")]
        [HttpGet]
        public CmsCORSResult<List<SeTechnicianAvailability>> SeTechnicianAvailabilityByTechnicianId()
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get SeTechnicianAvailabilityByTechId";
            var result = new CmsCORSResult<List<SeTechnicianAvailability>>((int)CmsResultCodes.Initializing
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
                        IFnsResult<List<IFnsSeTechnicianAvailability>> oFnsModel = ieService.SeTechnicianAvailabilityListByTechnicianId(user.GPEmployeeID);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                       //var oTsTechnicianAvailabilityList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTechnicianAvailability>)oFnsModel.GetValue());
                        var oTsTechnicianAvailabilityList = ConvertTo.CastFnsToSeTechnicianAvailabilityList((List<IFnsSeTechnicianAvailability>)oFnsModel.GetValue());


                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oTsTechnicianAvailabilityList;
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




        //this method will return the list of TechAvailability based on the given TechnicianID
        //[Route("SeTechnicianAvailabilityList/{id}/TID")]
        //[HttpGet]
        //public CmsCORSResult<List<SeTechnicianAvailability>> SeTechnicianAvailabilityByTechnicianId(string id)
        //{
        //    #region Initialize

        //    /** Initialize. */
        //    const string METHOD_NAME = "Get SeTechnicianAvailabilityByTechId";
        //    var result = new CmsCORSResult<List<SeTechnicianAvailability>>((int)CmsResultCodes.Initializing
        //        , string.Format("Initializing {0}.", METHOD_NAME));

        //    #endregion Initializes

        //    /** Authenticate session first. */
        //    return CORSSecurity.AuthenticationWrapper(METHOD_NAME
        //        , user =>
        //        {

        //            #region TRY
        //            try
        //            {
        //                // ** Create Service

        //                var ieService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
        //                IFnsResult<List<IFnsSeTechnicianAvailability>> oFnsModel = ieService.SeTechnicianAvailabilityListByTechnicianId(id);

        //                /** Check corsResult. */
        //                if (oFnsModel.Code != 0)
        //                {
        //                    result.Code = oFnsModel.Code;
        //                    result.Message = oFnsModel.Message;
        //                    return result;
        //                }

        //                /** Setup return corsResult. */
        //               //var oTsTechnicianAvailabilityList = ConvertTo.CastFnsToSeTicketList((List<IFnsSeTechnicianAvailability>)oFnsModel.GetValue());
        //                var oTsTechnicianAvailabilityList = ConvertTo.CastFnsToSeTechnicianAvailabilityList((List<IFnsSeTechnicianAvailability>)oFnsModel.GetValue());


        //                /** Save success results. */
        //                result.Code = (int)CmsResultCodes.Success;
        //                result.SessionId = user.SessionID;
        //                result.Message = "Success";
        //                result.Value = oTsTechnicianAvailabilityList;
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



    }
}
