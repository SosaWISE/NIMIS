using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;
using System;


namespace SOS.FunctionalServices.Contracts
{
	public interface IScheduleEngineService : IFunctionalService
	{
        IFnsResult<List<IFnsSeTicketStatusCode>> SeTicketStatusCodeList();
        IFnsResult<List<IFnsSeTicketType>> SeTicketTypeList();

        IFnsResult<IFnsSeTicket> SeTicketCreate(IFnsSeTicket fnsHeader, string gPEmployeeID);
        IFnsResult<IFnsSeTicket> SeTicketGetByMonitoringStationNo(long monitronicsNumber);

        IFnsResult<IFnsSeTicket> SeTicketGetByScheduleTicketId(long scheduleTicketId);
        IFnsResult<IFnsSeTicket> SeTicketUpdate(IFnsSeTicket fnsHeader, string gPEmployeeID);

        IFnsResult<IFnsSeTicket> SeTicketUpdateITER(long ticketId, string gPEmployeeID);

        IFnsResult<IFnsSeTicket> SeTicketUpdateITD(long ticketId, string gPEmployeeID);

        IFnsResult<IFnsSeTicket> SeTicketUpdateITC(IFnsSeTicket fnsHeader, string gPEmployeeID);



        //IFnsResult<IFnsSeTechnicianAvailability> SeTechnicianAvailabilityCreate(IFnsSeTechnicianAvailability fnsHeader, string gPEmployeeID);

        //IFnsResult<IFnsSeTechnicianAvailability> SeTechnicianAvailabilityUpdate(IFnsSeTechnicianAvailability fnsHeader, string gPEmployeeID);

        //IFnsResult<List<IFnsSeTechnicianAvailability>> SeTechnicianAvailabilityListByTechnicianId(string technicianId);
 
        IFnsResult<List<IFnsSeTicket>> SeTicketList();

        IFnsResult<List<IFnsSeTicket>> SeTicketReScheduleList(int hoursPassed);

        IFnsResult<List<IFnsSeTicket>> SeTicketListByStatusCodeId(int ticketStatusCodeId);

        IFnsResult<List<IFnsSeTicket>> SeTicketListByTechnicianId(string technicianId);
        IFnsResult<List<IFnsSeTicket>> SeTicketListByAccountId(long accountId);
        IFnsResult<List<IFnsSeTicket>> SeTicketListByBlockId(long blockId);

        IFnsResult<IFnsSeTicket> SeTicketByTicketId(long ticketId);
        IFnsResult<IFnsSeScheduleTicket> SeScheduleTicketCreate(IFnsSeScheduleTicket fnsHeader, string gpEmployeeID);
        IFnsResult<IFnsSeScheduleTicket> SeScheduleTicketGetByTicketId(long ticketId);

        IFnsResult<List<IFnsSeScheduleTicket>> SeScheduleTicketList(DateTime appointmentDateFrom, DateTime appointmentDateTo);

        IFnsResult<List<IFnsSeScheduleBlock>> SeScheduleBlockList(DateTime dateFrom, DateTime dateTo);

        IFnsResult<List<IFnsSeScheduleBlock>> SeTechnicianScheduleBlockList(DateTime dateFrom, DateTime dateTo);

        IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockBySSBID(long scheduleBlockID);
        IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockCreate(IFnsSeScheduleBlock fnsHeader);

        IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockUpdateSE(IFnsSeScheduleBlock fnsHeader, string gPEmployeeID);
        IFnsResult<IFnsSeScheduleBlock> SeScheduleBlockUpdate(IFnsSeScheduleBlock fnsHeader, string gPEmployeeID);

        IFnsResult<IFnsSeZipCode> SeZipCodeByZC(string zipCode);

        IFnsResult ScheduleBlockDelete(long blockID, string gpEmployeeId);
        IFnsResult ScheduleTicketDelete(long scheduleTicketID, string gpEmployeeId);
	}
}