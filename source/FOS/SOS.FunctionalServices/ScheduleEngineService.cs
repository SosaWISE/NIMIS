using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.ScheduleEngine;
using SOS.FunctionalServices.Helpers;


namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class ScheduleEngineService : IScheduleEngineService
	{


        public IFnsResult<List<IFnsSeTicketStatusCode>> SeTicketStatusCodeList()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketStatusCodeList";
            var result = new FnsResult<List<IFnsSeTicketStatusCode>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_TicketStatusCodeCollection tsList = SosCrmDataContext.Instance.SE_TicketStatusCodes.GetTicketStatusCodeList();

                var resultList = tsList.Select(item => new FnsSeTicketStatusCode(item)).Cast<IFnsSeTicketStatusCode>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicketStatusCode>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsSeTicketType>> SeTicketTypeList()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketTypeList";
            var result = new FnsResult<List<IFnsSeTicketType>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_TicketTypeCollection tsList = SosCrmDataContext.Instance.SE_TicketTypes.GetTicketTypeList();

                var resultList = tsList.Select(item => new FnsSeTicketType(item)).Cast<IFnsSeTicketType>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicketType>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<IFnsSeTicket> SeTicketCreate(IFnsSeTicket fnsHeader, string gpEmployeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketCreate";
            var result = new FnsResult<IFnsSeTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var seTicket = SosCrmDataContext.Instance.SE_TicketsViews.SeTicketCreate(
               // var seTicket = SosCrmDataContext.Instance.SE_AccountTicketsViews.SeTicketCreate(
                
                        fnsHeader.AccountId,
                        fnsHeader.MonitoringStationNo,
                        fnsHeader.TicketTypeId ,
                        fnsHeader.StatusCodeId,
                        fnsHeader.MoniConfirmation,
                        fnsHeader.TechnicianId ,
                        fnsHeader.TripCharges,
                        fnsHeader.Appointment,
                        fnsHeader.AgentConfirmation ,
                        fnsHeader.ExpirationDate,
                        fnsHeader.Notes
                    );

                // ** Build result
                var resultValue = new FnsSeTicket(seTicket);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }


        public IFnsResult<IFnsSeTicket> SeTicketUpdate(IFnsSeTicket seTicket, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeTicket> { Message = "" };

            var db = SosCrmDataContext.Instance;


            var oSeTicket = db.SE_Tickets.LoadByPrimaryKey(seTicket.TicketID);
            //need to review on fileds that need to be updated
            oSeTicket.AccountId = seTicket.AccountId;
            oSeTicket.MonitoringStationNo = seTicket.MonitoringStationNo;
            oSeTicket.TicketTypeId = seTicket.TicketTypeId;
            oSeTicket.StatusCodeId = seTicket.StatusCodeId;
            oSeTicket.MoniConfirmation = seTicket.MoniConfirmation;
            oSeTicket.TechnicianId = seTicket.TechnicianId;
            oSeTicket.TripCharges = seTicket.TripCharges;
            oSeTicket.Appointment = seTicket.Appointment;
            oSeTicket.AgentConfirmation = seTicket.AgentConfirmation;
            oSeTicket.ExpirationDate = seTicket.ExpirationDate;
            oSeTicket.Notes = seTicket.Notes;

            // save scheduble block
            oSeTicket.Save(gpEmployeeID);

           // result.Value = new FnsSeTicket(oSeTicket);
            result.Value = new FnsSeTicket(db.SE_TicketsViews.GetByTicketID(oSeTicket.TicketID));
            //result.Value = new FnsSeTicket(db.SE_AccountTicketsViews.GetByTicketID(oSeTicket.TicketID));


            return result;
        }


        //to be used by Technician - to set Technician EnRoute to true
        public IFnsResult<IFnsSeTicket> SeTicketUpdateITER(long ticketId, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeTicket> { Message = "" };

            var db = SosCrmDataContext.Instance;

            var oSeTicket = db.SE_Tickets.LoadByPrimaryKey(ticketId);
            //need to review on fileds that need to be updated
            oSeTicket.IsTechEnRoute = true;

            // save scheduble block
            oSeTicket.Save(gpEmployeeID);


            //SEND EMAIL TO CUSTOMER HERE


            // result.Value = new FnsSeTicket(oSeTicket);
            result.Value = new FnsSeTicket(db.SE_TicketsViews.GetByTicketID(oSeTicket.TicketID));
            return result;
        }


        //to be used by Technician - to IsTechnicianDelayed to true
        public IFnsResult<IFnsSeTicket> SeTicketUpdateITD(long ticketId, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeTicket> { Message = "" };

            var db = SosCrmDataContext.Instance;

            var oSeTicket = db.SE_Tickets.LoadByPrimaryKey(ticketId);
            //need to review on fileds that need to be updated
            oSeTicket.IsTechDelayed = true;

            // save scheduble block
            oSeTicket.Save(gpEmployeeID);


            //SEND EMAIL TO CUSTOMER HERE


            // result.Value = new FnsSeTicket(oSeTicket);
            result.Value = new FnsSeTicket(db.SE_TicketsViews.GetByTicketID(oSeTicket.TicketID));
            return result;
        }

        public IFnsResult<IFnsSeTicket> SeTicketUpdateITC(IFnsSeTicket seTicket, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeTicket> { Message = "" };

            var db = SosCrmDataContext.Instance;

            var oSeTicket = db.SE_Tickets.LoadByPrimaryKey(seTicket.TicketID);
            //need to review on fileds that need to be updated
            oSeTicket.IsTechCompleted = true;
            oSeTicket.ClosingNote = seTicket.ClosingNote;
            oSeTicket.ConfirmationNo = seTicket.ConfirmationNo;
            oSeTicket.StatusCodeId = 5;  //completed from SE_TicketStatusCodes


            // save scheduble block
            oSeTicket.Save(gpEmployeeID);


            //SEND EMAIL TO CUSTOMER HERE


            // result.Value = new FnsSeTicket(oSeTicket);
            result.Value = new FnsSeTicket(db.SE_TicketsViews.GetByTicketID(oSeTicket.TicketID));
            return result;
        }





        public IFnsResult<List<IFnsSeTicket>> SeTicketListByStatusCodeId(int ticketStatusCodeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketListByStatusCodeId";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_TicketsViewCollection tList = SosCrmDataContext.Instance.SE_TicketsViews.GetTicketListByTicketStatusCodeId(ticketStatusCodeId);

                var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }



        public IFnsResult<List<IFnsSeTicket>> SeTicketListByTechnicianId(string technicianId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketListByTechnicianId";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_ScheduleBlockTicketsViewCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.GetTicketListByTechnicianId(technicianId);

                var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<List<IFnsSeTicket>> SeTicketListByAccountId(long accountId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketListByAccountId";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
               // SE_TicketsViewCollection tList = SosCrmDataContext.Instance.SE_TicketsViews.GetTicketListByAccountId(accountId);
                 SE_AccountTicketsViewCollection tList = SosCrmDataContext.Instance.SE_AccountTicketsViews.GetTicketListByAccountId(accountId);
                 var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                
                //var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<List<IFnsSeTicket>> SeTicketListByBlockId(long blockId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketListByBlockId";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_ScheduleBlockTicketsViewCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.GetTicketListByBlockId(blockId);

                var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<IFnsSeTicket> SeTicketGetByScheduleTicketId(long scheduleTicketId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketGetByScheduleTicketId";
            var result = new FnsResult<IFnsSeTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_ScheduleBlockTicketsView oSeTicketView = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.GetByScheduleTicketId(scheduleTicketId);
                var fnsSeTicket = new FnsSeTicket(oSeTicketView);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeTicket;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<IFnsSeTicket> SeTicketGetByMonitoringStationNo(long monitronicsNumber)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketGetByMonitronicsNumber";
            var result = new FnsResult<IFnsSeTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_TicketsView oSeTicketView = SosCrmDataContext.Instance.SE_TicketsViews.GetByMonitoringStationNo(monitronicsNumber);
                var fnsSeTicket = new FnsSeTicket(oSeTicketView);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeTicket;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<IFnsSeZipCode> SeZipCodeByZC(string zipCode)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeZipCodeGetByZC";
            var result = new FnsResult<IFnsSeZipCode>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_ZipCode oSeZipCode = SosCrmDataContext.Instance.SE_ZipCodes.LoadByPrimaryKey(zipCode);
                var fnsSeZipCode = new FnsSeZipCode(oSeZipCode);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeZipCode;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeZipCode>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsSeTicket>> SeTicketList()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketList";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_TicketsViewCollection tList = SosCrmDataContext.Instance.SE_TicketsViews.GetTicketList();

                var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<List<IFnsSeTicket>> SeTicketReScheduleList(int hoursPassed)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketList";
            var result = new FnsResult<List<IFnsSeTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_ScheduleBlockTicketsViewCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.SeTicketReScheduleList(hoursPassed);

                var resultList = tList.Select(item => new FnsSeTicket(item)).Cast<IFnsSeTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }




        public IFnsResult<IFnsSeScheduleTicket> SeScheduleTicketCreate(IFnsSeScheduleTicket fnsHeader, string gpEmployeeID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleTicketCreate";
            var result = new FnsResult<IFnsSeScheduleTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                var db = SosCrmDataContext.Instance;

                // ** Get instance of the ie service.
                var seScheduleTicket = db.SE_ScheduleTickets.SeScheduleTicketCreate(
                        fnsHeader.TicketId,
                        fnsHeader.BlockId,
                        fnsHeader.AppointmentDate,
                        fnsHeader.TravelTime
                    );

                //update scheduleblock distance with the newly added tickets coordinates
                var oScheduleBlockView = db.SE_ScheduleBlocksViews.GetByBlockID(fnsHeader.BlockId);
                var oScheduleBlock = db.SE_ScheduleBlocks.LoadByPrimaryKey(fnsHeader.BlockId);
                //compute for distance
                oScheduleBlock.Distance = CustomGeoCoordinate.GetDistance(
                   oScheduleBlockView.BlockLatitude??0,
                   oScheduleBlockView.BlockLongitude??0,
                   oScheduleBlockView.TicketLatitude??0,
                   oScheduleBlockView.TicketLongitude??0);
               //uppdate schedule block with the new distance
                // save scheduble block
                oScheduleBlock.Save(gpEmployeeID);


                // ** Build result
                var resultValue = new FnsSeScheduleTicket(seScheduleTicket);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<IFnsSeScheduleTicket> SeScheduleTicketGetByTicketId(long ticketId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleTicketGetByTicketId";
            var result = new FnsResult<IFnsSeScheduleTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_ScheduleTicket oSeScheduleTicket = SosCrmDataContext.Instance.SE_ScheduleTickets.GetByTicketId(ticketId);
                var fnsSeScheduleTicket = new FnsSeScheduleTicket(oSeScheduleTicket);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeScheduleTicket;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<IFnsSeTicket> SeTicketByTicketId(long ticketId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTicketGetByTicketId";
            var result = new FnsResult<IFnsSeTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_TicketsView oSeTicket = SosCrmDataContext.Instance.SE_TicketsViews.GetByTicketId(ticketId);
                var fnsSeTicket = new FnsSeTicket(oSeTicket);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeTicket;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsSeScheduleTicket>> SeScheduleTicketList(DateTime appointmentDateStart, DateTime appointmentDateEnd)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleTicketList";
            var result = new FnsResult<List<IFnsSeScheduleTicket>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                SE_ScheduleTicketCollection tList = SosCrmDataContext.Instance.SE_ScheduleTickets.GetScheduleTicketList(appointmentDateStart, appointmentDateEnd);

                var resultList = tList.Select(item => new FnsSeScheduleTicket(item)).Cast<IFnsSeScheduleTicket>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeScheduleTicket>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockBySSBID(long scheduleBlockID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleBlockBySSBID";
            var result = new FnsResult<IFnsSeScheduleBlock>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                //  SE_ScheduleBlockCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlocks.GetScheduleBlockList(dateFrom, dateTo);
                SE_ScheduleBlocksView oSeScheduleBlockView = SosCrmDataContext.Instance.SE_ScheduleBlocksViews.GetByBlockID(scheduleBlockID);

                var fnsSeScheduleBlock =new FnsSeScheduleBlock(oSeScheduleBlockView);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = fnsSeScheduleBlock;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleBlock>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<List<IFnsSeScheduleBlock>> SeScheduleBlockList(DateTime dateFrom, DateTime dateTo)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleBlockList";
            var result = new FnsResult<List<IFnsSeScheduleBlock>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


              //  SE_ScheduleBlockCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlocks.GetScheduleBlockList(dateFrom, dateTo);
              SE_ScheduleBlocksViewCollection sbList = SosCrmDataContext.Instance.SE_ScheduleBlocksViews.GetScheduleBlockList(dateFrom, dateTo);

              SE_ScheduleBlockTicketsViewCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.GetScheduleBlockTicketList();
              
              //var resultList = sbList.Select(item => new FnsSeScheduleBlock(item)).Cast<IFnsSeScheduleBlock>().ToList();
              List<IFnsSeScheduleBlock> scheduleBlockList = sbList.Select(item => new FnsSeScheduleBlock(item, 
                  tList.Where(q=>q.BlockId==item.BlockID).Select(ticket=>new FnsSeTicket(ticket)).Cast<IFnsSeTicket>().ToList())).Cast<IFnsSeScheduleBlock>().ToList();
              //foreach (var sb in resultList)
              //{
              //    sb.TicketList = tList.Where(q => q.BlockId == sb.BlockID).ToList().Cast<List<IFnsSeScheduleTicket>>();
              //}

              //foreach (var sb in resultList)
              //{
              //    sb.TicketList = tList.Where(q => q.BlockId == sb.BlockID).Cast<IFnsSeScheduleTicket>();
              //}


                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                //result.Value = resultList;
                result.Value = scheduleBlockList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeScheduleBlock>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsSeScheduleBlock>> SeTechnicianScheduleBlockList(DateTime dateFrom, DateTime dateTo)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTechnicianScheduleBlockList";
            var result = new FnsResult<List<IFnsSeScheduleBlock>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                //  SE_ScheduleBlockCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlocks.GetScheduleBlockList(dateFrom, dateTo);
                SE_TechnicianScheduleBlocksViewCollection sbList = SosCrmDataContext.Instance.SE_TechnicianScheduleBlocksViews.GetTechnicianScheduleBlockList(dateFrom, dateTo);

              //  SE_ScheduleBlockTicketsViewCollection tList = SosCrmDataContext.Instance.SE_ScheduleBlockTicketsViews.GetScheduleBlockTicketList();

                var resultList = sbList.Select(item => new FnsSeScheduleBlock(item)).Cast<IFnsSeScheduleBlock>().ToList();
                //List<IFnsSeScheduleBlock> scheduleBlockList = sbList.Select(item => new FnsSeScheduleBlock(item,
                //    tList.Where(q => q.BlockId == item.BlockID).Select(ticket => new FnsSeTicket(ticket)).Cast<IFnsSeTicket>().ToList())).Cast<IFnsSeScheduleBlock>().ToList();
                

                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                //result.Value = resultList;
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeScheduleBlock>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockCreate(IFnsSeScheduleBlock fnsHeader)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeScheduleBlockCreate";
            var result = new FnsResult<IFnsSeScheduleBlock>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var seScheduleBlock = SosCrmDataContext.Instance.SE_ScheduleBlocks.SeScheduleBlockCreate(
                            fnsHeader.Block,
                            fnsHeader.ZipCode,
                            fnsHeader.MaxRadius,
                            fnsHeader.Distance,
                            fnsHeader.StartTime,
                            fnsHeader.EndTime,
                            fnsHeader.AvailableSlots,
                            fnsHeader.TechnicianId,
                            fnsHeader.IsTechConfirmed,
                            fnsHeader.Color,
                            fnsHeader.IsBlocked
                    );

                // ** Build result
                var resultValue = new FnsSeScheduleBlock(seScheduleBlock);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleBlock>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockUpdate(IFnsSeScheduleBlock scheduleBlock, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeScheduleBlock> { Message = "" };

            var db = SosCrmDataContext.Instance;
          

            var oScheduleBlock = db.SE_ScheduleBlocks.LoadByPrimaryKey(scheduleBlock.BlockID);
            var blockZipCode = db.SE_ZipCodes.LoadByPrimaryKey(scheduleBlock.ZipCode);    
            //compute for distance
            if (blockZipCode != null && scheduleBlock.CurrentTicketId != null)
            {
                var oSeTicket = db.SE_TicketsViews.GetByTicketID(scheduleBlock.CurrentTicketId ?? 0);
                oScheduleBlock.Distance = CustomGeoCoordinate.GetDistance(
                    blockZipCode.Latitude,
                    blockZipCode.Longitude,
                    oSeTicket.Latitude,
                    oSeTicket.Longitude

                );
            }
            else {
                oScheduleBlock.Distance = 0; 
            }

            //check if there is a change in techid
            if (oScheduleBlock.TechnicianId != scheduleBlock.TechnicianId && scheduleBlock.TechnicianId != "") {
                    db.SE_ScheduleBlocks.SeScheduleTicketTechUpdate(oScheduleBlock.BlockID, scheduleBlock.TechnicianId);
            }

            if (oScheduleBlock.StartTime != scheduleBlock.StartTime || oScheduleBlock.EndTime != scheduleBlock.EndTime)
            {
                int timeZone = blockZipCode.TimeZone??0;

                DateTime startTime = (DateTime)scheduleBlock.StartTime;
                DateTime endTime = (DateTime)scheduleBlock.EndTime;
                oScheduleBlock.StartTime = startTime.AddHours(timeZone);
                oScheduleBlock.EndTime = endTime.AddHours(timeZone);
            }

            oScheduleBlock.Block = scheduleBlock.Block;
            oScheduleBlock.TechnicianId = scheduleBlock.TechnicianId;
            oScheduleBlock.IsTechConfirmed = scheduleBlock.IsTechConfirmed;
            oScheduleBlock.DateTechConfirmed = scheduleBlock.DateTechConfirmed;
            oScheduleBlock.CurrentTicketId = scheduleBlock.CurrentTicketId;
            oScheduleBlock.IsRed = scheduleBlock.IsRed;
            oScheduleBlock.CurrentTicketId = scheduleBlock.CurrentTicketId;
            oScheduleBlock.ZipCode = scheduleBlock.ZipCode;
            oScheduleBlock.MaxRadius = scheduleBlock.MaxRadius;
            oScheduleBlock.AvailableSlots = scheduleBlock.AvailableSlots;

			// save scheduble block
			oScheduleBlock.Save(gpEmployeeID);

            result.Value = new FnsSeScheduleBlock(db.SE_ScheduleBlocksViews.GetByBlockID(oScheduleBlock.BlockID));
            return result;
        }

        //This method will update only a specific SeScheduleBlock - StartTime and EndTime
        public IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockUpdateSE(IFnsSeScheduleBlock scheduleBlock, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeScheduleBlock> { Message = "" };

            var db = SosCrmDataContext.Instance;


            var oScheduleBlock = db.SE_ScheduleBlocks.LoadByPrimaryKey(scheduleBlock.BlockID);

            var blockZipCode = db.SE_ZipCodes.LoadByPrimaryKey(oScheduleBlock.ZipCode);   
            
            int timeZone = blockZipCode.TimeZone ?? 0;

            DateTime startTime = (DateTime)scheduleBlock.StartTime;
            DateTime endTime = (DateTime)scheduleBlock.EndTime;
            oScheduleBlock.StartTime = startTime.AddHours(timeZone);
            oScheduleBlock.EndTime = endTime.AddHours(timeZone);
            

            //oScheduleBlock.StartTime = scheduleBlock.StartTime;
            //oScheduleBlock.EndTime = scheduleBlock.EndTime;
            oScheduleBlock.Block = scheduleBlock.Block;
            oScheduleBlock.IsTechConfirmed = scheduleBlock.IsTechConfirmed;
            oScheduleBlock.AvailableSlots = scheduleBlock.AvailableSlots;

            // save scheduble block
            oScheduleBlock.Save(gpEmployeeID);


            result.Value = new FnsSeScheduleBlock(db.SE_ScheduleBlocksViews.GetByBlockID(oScheduleBlock.BlockID));
            return result;
        }


        /// <summary>
        /// This is a logical delete
        /// </summary>
        /// <param name="blockID"></param>
        /// <param name="gpEmployeeId"></param>
        /// <returns>IFnsResult</returns>
        public IFnsResult ScheduleBlockDelete(long blockID, string gpEmployeeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ScheduleBlockDelete";
            var result = new FnsResult<IFnsSeScheduleBlock>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                var scheduleBlock = SosCrmDataContext.Instance.SE_ScheduleBlocks.LoadByPrimaryKey(blockID);

                // ** Make the logical delete
                scheduleBlock.IsDeleted = true;
                scheduleBlock.Save(gpEmployeeId);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleBlock>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }



        /// <summary>
        /// This is a logical delete
        /// </summary>
        /// <param name="scheduleTicketID"></param>
        /// <param name="gpEmployeeId"></param>
        /// <returns>IFnsResult</returns>
        public IFnsResult ScheduleTicketDelete(long scheduleTicketID, string gpEmployeeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ScheduleTicketDelete";
            var result = new FnsResult<IFnsSeScheduleTicket>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                var scheduleTicket = SosCrmDataContext.Instance.SE_ScheduleTickets.LoadByPrimaryKey(scheduleTicketID);

                // ** Make the logical delete
                scheduleTicket.IsDeleted = true;
                scheduleTicket.Save(gpEmployeeId);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeScheduleTicket>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }



        /*
        public IFnsResult<IFnsSeTechnicianAvailability> SeTechnicianAvailabilityCreate(IFnsSeTechnicianAvailability fnsHeader, string gpEmployeeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTechnicianAvailabilityCreate";
            var result = new FnsResult<IFnsSeTechnicianAvailability>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var seTechnicianAvailability = SosCrmDataContext.Instance.SE_TechnicianAvailabilities.SeTechnicianAvailabilityCreate(
                            fnsHeader.TechnicianId,
                            fnsHeader.StartDateTime,
                            fnsHeader.EndDateTime
                    );

                // ** Build result
                var resultValue = new FnsSeTechnicianAvailability(seTechnicianAvailability);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsSeTechnicianAvailability>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<IFnsSeTechnicianAvailability> SeTechnicianAvailabilityUpdate(IFnsSeTechnicianAvailability technicianAvailability, string gpEmployeeID)
        {
            var result = new FnsResult<IFnsSeTechnicianAvailability> { Message = "" };

            var db = SosCrmDataContext.Instance;


            var oTechnicianAvailability = db.SE_TechnicianAvailabilities.LoadByPrimaryKey(technicianAvailability.TechnicianAvailabilityID);

            oTechnicianAvailability.StartDateTime = technicianAvailability.StartDateTime;
            oTechnicianAvailability.EndDateTime = technicianAvailability.EndDateTime;


            DatabaseHelper.Transaction(Data.SubSonicConfigHelper.SOS_CRM_PROVIDER_NAME, () =>
            {
                // save scheduble block
                oTechnicianAvailability.Save(gpEmployeeID);

                return true;
            });


            result.Value = new FnsSeTechnicianAvailability(oTechnicianAvailability);
            return result;
        }


        public IFnsResult<List<IFnsSeTechnicianAvailability>> SeTechnicianAvailabilityListByTechnicianId(string technicianId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "SeTechnicianAvailabilityListByTechnicianId";
            var result = new FnsResult<List<IFnsSeTechnicianAvailability>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                SE_TechnicianAvailabilityCollection tList = SosCrmDataContext.Instance.SE_TechnicianAvailabilities.GetTechnicianAvailabilityByTechId(technicianId);

                var resultList = tList.Select(item => new FnsSeTechnicianAvailability(item)).Cast<IFnsSeTechnicianAvailability>().ToList();
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsSeTechnicianAvailability>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }
        */


      }
    
}
