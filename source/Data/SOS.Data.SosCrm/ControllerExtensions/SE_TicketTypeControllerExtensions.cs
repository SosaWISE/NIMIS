using AR = SOS.Data.SosCrm.SE_TicketType;
using ARCollection = SOS.Data.SosCrm.SE_TicketTypeCollection;
using ARController = SOS.Data.SosCrm.SE_TicketTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_TicketTypeControllerExtensions
	{

        public static ARCollection GetTicketTypeList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.IsDeleted,false).ORDER_BY(AR.Columns.TicketTypeName);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }
	}
}
