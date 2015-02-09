using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_Ticket;
using ARCollection = SOS.Data.SosCrm.SE_TicketCollection;
using ARController = SOS.Data.SosCrm.SE_TicketController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_TicketControllerExtensions
	{

        public static AR SeTicketCreate(this ARController cntlr, 
                        long accountId,
                        long? moniNumber,
                        int ticketTypeId ,
                        int statusCodeId,
                        string moniConfirmation,
                        string technicianId ,
                        decimal? tripCharges,
                        string appointment,
                        string agentConfirmation,
                        DateTime? expirationDate,
                        string notes
                
            )
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.SE_TicketCreate(
                         accountId,
                         moniNumber,
                         ticketTypeId ,
                         statusCodeId,
                         moniConfirmation,
                         technicianId ,
                         tripCharges,
                         appointment,
                         agentConfirmation,
                         expirationDate,
                         notes
                    )
                );
        }


        public static ARCollection GetTicketListByStatusCodeId(this ARController oCntlr, int ticketStatusCodeId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.StatusCodeId, ticketStatusCodeId);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }


        public static ARCollection GetTicketList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

	}
}
