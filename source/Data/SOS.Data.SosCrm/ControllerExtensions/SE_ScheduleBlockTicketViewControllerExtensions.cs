using SOS.Lib.Util;
using System.Collections.Generic;
using AR = SOS.Data.SosCrm.SE_ScheduleBlockTicketsView;
using ARCollection = SOS.Data.SosCrm.SE_ScheduleBlockTicketsViewCollection;
using ARController = SOS.Data.SosCrm.SE_ScheduleBlockTicketsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_ScheduleBlockTicketViewControllerExtensions
	{

        public static ARCollection GetTicketListByTechnicianId(this ARController oCntlr, string technicianId)
        {
      
           // object [] notCompletedValues = new object [2];
           // notCompletedValues[0] = false;
          //  notCompletedValues[1] = null;

            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.TechnicianId, technicianId)
                .WHERE(AR.Columns.IsTechCompleted, false);
                 
                //.IN(AR.Columns.IsTechCompleted, notCompletedValues);
             
       

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static AR GetByTicketId(this ARController oCntlr, long ticketId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.TicketID, ticketId);

            /** Return result. */
            return oCntlr.LoadSingle(oQry);
        }

        public static ARCollection GetTicketListByBlockId(this ARController oCntlr, long blockId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.BlockId, blockId)
                .WHERE(AR.Columns.ScheduleTicketDeleted, false);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static ARCollection GetScheduleBlockTicketList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }


        public static AR GetByScheduleTicketId(this ARController oCntlr, long scheduleTicketId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.ScheduleTicketId, scheduleTicketId)
                .WHERE(AR.Columns.ScheduleTicketDeleted, false);

            /** Return result. */
            return oCntlr.LoadSingle(oQry);
        }


        public static ARCollection SeTicketReScheduleList(this ARController cntlr, int hoursPassed)
        {
            return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.SE_TicketReScheduleList(hoursPassed));
        }


	}
}
