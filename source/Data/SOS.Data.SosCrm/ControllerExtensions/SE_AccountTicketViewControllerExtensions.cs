using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.SE_AccountTicketsView;
using ARCollection = SOS.Data.SosCrm.SE_AccountTicketsViewCollection;
using ARController = SOS.Data.SosCrm.SE_AccountTicketsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_AccountTicketViewControllerExtensions
	{

        public static ARCollection GetTicketListByAccountId(this ARController oCntlr, long accountId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.AccountId, accountId);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }

        public static AR GetByTicketID(this ARController oCntlr, long ticketId)
        {
            /** Initialize. */
            var oQry = AR.Query()
                .WHERE(AR.Columns.IsDeleted, false)
                .WHERE(AR.Columns.TicketID, ticketId);

            /** Return result. */
            return oCntlr.LoadSingle(oQry);
        }


        public static AR SeTicketCreate(this ARController cntlr,
                     long accountId,
                     long? moniNumber,
                     int ticketTypeId,
                     int statusCodeId,
                     string moniConfirmation,
                     string technicianId,
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
                         ticketTypeId,
                         statusCodeId,
                         moniConfirmation,
                         technicianId,
                         tripCharges,
                         appointment,
                         agentConfirmation,
                         expirationDate,
                         notes
                    )
                );
        }


	}
}
