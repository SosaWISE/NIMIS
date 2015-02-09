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
    public class ScheduleBlockController : ApiController
    {



        //this method will save a new ScheduleBlock Record
        [Route("SeScheduleBlock/")]
        [HttpPost]
        public CmsCORSResult<SeScheduleBlock> Post([FromBody]SeScheduleBlock value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Create SeScheduleBlock";

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
                    argArray.Add(new CORSArg(value.Block, (value.Block == ""), "<li>'Block' was not passed.</li>"));
                    //argArray.Add(new CORSArg(value.ZipCode, (value.ZipCode == ""), "<li>'ZipCode' was not passed.</li>"));

                }
                CmsCORSResult<SeScheduleBlock> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    // ** Prepare arguents
                    // ReSharper disable once PossibleNullReferenceException
                    var fnsHeader = new FnsSeScheduleBlock(
                            value.BlockID,
                            value.Block,
                            value.ZipCode,
                            value.MaxRadius,
                            value.Distance,
                            value.StartTime,
                            value.EndTime,
                            value.AvailableSlots,
                            value.TechnicianId,
                            value.IsTechConfirmed,
                            value.CurrentTicketId,
                            value.DateTechConfirmed,
                            value.IsRed,
                            value.Color,
                            value.IsBlocked
                            );
                    IFnsResult<IFnsSeScheduleBlock> fnsResult = seService.SeScheduleBlockCreate(fnsHeader);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsSeScheduleBlock)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new SeScheduleBlock
                        {
                            BlockID = fnsResultValue.BlockID,
                            Block = fnsResultValue.Block,
                            ZipCode = fnsResultValue.ZipCode,
                            MaxRadius = fnsResultValue.MaxRadius,
                            Distance = fnsResultValue.Distance,
                            StartTime = fnsResultValue.StartTime,
                            EndTime = fnsResultValue.EndTime,
                            AvailableSlots = fnsResultValue.AvailableSlots,
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

        //this method will return a ScheduleBlock based on the SeScheduleBlockId  SSBID
        [Route("SeScheduleBlock/{id}/SSBID")]
        [HttpPost]
        public CmsCORSResult<SeScheduleBlock> GetSeScheduleBlockBySSBID(long id)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get SeScheduleBlock";
            var result = new CmsCORSResult<SeScheduleBlock>((int)CmsResultCodes.Initializing
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
                        IFnsResult<IFnsSeScheduleBlock> oFnsModel = ieService.SeScheduleBlockBySSBID(id);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oSeScheduleBlock = ConvertTo.CastFnsToSeScheduleBlock((IFnsSeScheduleBlock)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oSeScheduleBlock;
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




        //this method will return the list of ScheduleBlocks based on the given dates - ScheduleParam contains date range
        [Route("SeScheduleBlockList")]
        [HttpPost]
        public CmsCORSResult<List<SeScheduleBlock>> SeScheduleBlockList([FromBody]ScheduleParam scheduleParam)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get SeScheduleBlockList";
            var result = new CmsCORSResult<List<SeScheduleBlock>>((int)CmsResultCodes.Initializing
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
                        IFnsResult<List<IFnsSeScheduleBlock>> oFnsModel = ieService.SeScheduleBlockList(scheduleParam.DateFrom, scheduleParam.DateTo);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oTsScheduleBlockList = ConvertTo.CastFnsToSeScheduleBlockList((List<IFnsSeScheduleBlock>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oTsScheduleBlockList;
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


        //this method will return the list of SeTechnicianScheduleBlockList based on the given dates - ScheduleParam contains date range
        [Route("SeTechnicianScheduleBlockList")]
        [HttpPost]
        public CmsCORSResult<List<SeScheduleBlock>> SeTechnicianScheduleBlockList([FromBody]ScheduleParam scheduleParam)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Get SeTechnicianScheduleBlockList";
            var result = new CmsCORSResult<List<SeScheduleBlock>>((int)CmsResultCodes.Initializing
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
                        IFnsResult<List<IFnsSeScheduleBlock>> oFnsModel = ieService.SeTechnicianScheduleBlockList(scheduleParam.DateFrom, scheduleParam.DateTo);

                        /** Check corsResult. */
                        if (oFnsModel.Code != 0)
                        {
                            result.Code = oFnsModel.Code;
                            result.Message = oFnsModel.Message;
                            return result;
                        }

                        /** Setup return corsResult. */
                        var oTsScheduleBlockList = ConvertTo.CastFnsToSeScheduleBlockList((List<IFnsSeScheduleBlock>)oFnsModel.GetValue());

                        /** Save success results. */
                        result.Code = (int)CmsResultCodes.Success;
                        result.SessionId = user.SessionID;
                        result.Message = "Success";
                        result.Value = oTsScheduleBlockList;
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



        //this method will update SeScheduleBlock based in BlockId
        [Route("SeScheduleBlock/{id}")]
        [HttpPost]
        public CmsCORSResult<SeScheduleBlock> Update(long id, [FromBody]SeScheduleBlock scheduleBlock)
        {


            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Update SeScheduleBlock";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (scheduleBlock == null)
                {
                    argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                }
                else
                {
                    argArray.Add(new CORSArg("", (scheduleBlock.BlockID == 0), "BlockID is required."));
                    argArray.Add(new CORSArg("", string.IsNullOrEmpty(scheduleBlock.Block), "Block is required."));
                    argArray.Add(new CORSArg("", (scheduleBlock.StartTime == null), "StartTime is required."));
                    argArray.Add(new CORSArg("", (scheduleBlock.EndTime == null), "EndTime is required."));


                }
                CmsCORSResult<SeScheduleBlock> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {

                    var service = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    var fnsHeader = new FnsSeScheduleBlock(
                          scheduleBlock.BlockID,
                          scheduleBlock.Block,
                          scheduleBlock.ZipCode,
                          scheduleBlock.MaxRadius,
                          scheduleBlock.Distance,
                          scheduleBlock.StartTime,
                          scheduleBlock.EndTime,
                          scheduleBlock.AvailableSlots,
                          scheduleBlock.TechnicianId,
                          scheduleBlock.IsTechConfirmed,
                          scheduleBlock.CurrentTicketId,
                          scheduleBlock.DateTechConfirmed,
                          scheduleBlock.IsRed,
                          scheduleBlock.Color,
                          scheduleBlock.IsBlocked
                          );

                    IFnsResult<IFnsSeScheduleBlock> fnsResult = service.SeScheduleBlockUpdate(fnsHeader, user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsSeScheduleBlock)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new SeScheduleBlock
                        {
                            BlockID = fnsResultValue.BlockID,
                            Block = fnsResultValue.Block,
                            ZipCode = fnsResultValue.ZipCode,
                            MaxRadius = fnsResultValue.MaxRadius,
                            Distance = fnsResultValue.Distance,
                            StartTime = fnsResultValue.StartTime,
                            EndTime = fnsResultValue.EndTime,
                            AvailableSlots = fnsResultValue.AvailableSlots,
                            TechnicianId = fnsResultValue.TechnicianId,
                            DateTechConfirmed = fnsResultValue.DateTechConfirmed,
                            CurrentTicketId = fnsResultValue.CurrentTicketId,
                            IsTechConfirmed = fnsResultValue.IsTechConfirmed,
                            IsRed = fnsResultValue.IsRed
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


        //this method will update SeScheduleBlock StartTime and EndTime (SE)
        [Route("SeScheduleBlock/{id}/SE")]
        [HttpPost]
        public CmsCORSResult<SeScheduleBlock> UpdateSE(long id, [FromBody]SeScheduleBlock scheduleBlock)
        {


            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "Update SeScheduleBlock from TA";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (scheduleBlock == null)
                {
                    argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                }
                else
                {
                    argArray.Add(new CORSArg("", (scheduleBlock.BlockID == 0), "BlockID is required."));
                    argArray.Add(new CORSArg("", string.IsNullOrEmpty(scheduleBlock.Block), "Block is required."));
                    argArray.Add(new CORSArg("", (scheduleBlock.StartTime == null), "StartTime is required."));
                    argArray.Add(new CORSArg("", (scheduleBlock.EndTime == null), "EndTime is required."));

                }
                CmsCORSResult<SeScheduleBlock> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {

                    var service = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();

                    var fnsHeader = new FnsSeScheduleBlock(
                          scheduleBlock.BlockID,
                          scheduleBlock.Block,
                          scheduleBlock.ZipCode,
                          scheduleBlock.MaxRadius,
                          scheduleBlock.Distance,
                          scheduleBlock.StartTime,
                          scheduleBlock.EndTime,
                          scheduleBlock.AvailableSlots,
                          scheduleBlock.TechnicianId,
                          scheduleBlock.IsTechConfirmed,
                          scheduleBlock.CurrentTicketId,
                          scheduleBlock.DateTechConfirmed,
                          scheduleBlock.IsRed,
                          scheduleBlock.Color,
                          scheduleBlock.IsBlocked
                          );

                    IFnsResult<IFnsSeScheduleBlock> fnsResult = service.SeScheduleBlockUpdateSE(fnsHeader, user.GPEmployeeID);

                    // ** Save result
                    result.Code = fnsResult.Code;
                    result.SessionId = user.SessionID;
                    result.Message = fnsResult.Message;

                    // ** Get Values
                    var fnsResultValue = (IFnsSeScheduleBlock)fnsResult.GetValue();
                    if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                    {
                        var resultValue = new SeScheduleBlock
                        {
                            BlockID = fnsResultValue.BlockID,
                            Block = fnsResultValue.Block,
                            ZipCode = fnsResultValue.ZipCode,
                            MaxRadius = fnsResultValue.MaxRadius,
                            Distance = fnsResultValue.Distance,
                            StartTime = fnsResultValue.StartTime,
                            EndTime = fnsResultValue.EndTime,
                            AvailableSlots = fnsResultValue.AvailableSlots,
                            TechnicianId = fnsResultValue.TechnicianId,
                            DateTechConfirmed = fnsResultValue.DateTechConfirmed,
                            IsTechConfirmed = fnsResultValue.IsTechConfirmed,
                            IsRed = fnsResultValue.IsRed
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



        
        [Route("SeScheduleBlock/{blockId}")]
        [HttpDelete]
        public CmsCORSResult<bool> ScheduleBlockDelete(long blockId)
        {
            #region Initialization

            const string METHOD_NAME = "ScheduleBlockDelete";

            #endregion Initialization

            #region Execute

            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
            , user =>
            {
                #region Parameter Validation

                var argArray = new List<CORSArg>
				{
					new CORSArg(blockId, (blockId == 0), "<li>'blockId' Has to be passed.</li>")
				};
                CmsCORSResult<bool> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var seService = SosServiceEngine.Instance.FunctionalServices.Instance<IScheduleEngineService>();
                    IFnsResult fnsResult = seService.ScheduleBlockDelete(blockId, user.GPEmployeeID);

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
