using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_ScheduleTicket;
using ARCollection = SOS.Data.SosCrm.SE_ScheduleTicketCollection;
using ARController = SOS.Data.SosCrm.SE_ScheduleTicketController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_ScheduleTicketControllerExtensions
	{

        public static AR SeScheduleTicketCreate(this ARController cntlr, 
                       long ticketId,
                       long? blockId,
                       DateTime appointmentDate,
                       int travelTime
            )
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SE_ScheduleTicketCreate(
                        ticketId,
                        blockId,
                        appointmentDate,
                        travelTime
                    )
                );
        }


        public static ARCollection GetScheduleTicketList(this ARController oCntlr, DateTime appointmentDateStart, DateTime appointmentDateEnd)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);
        
            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static AR GetByTicketId(this ARController oCntlr, long ticketId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.TicketId, ticketId);

            /** Return result. */
            return oCntlr.LoadSingle(oQry);
        }



	}
}
