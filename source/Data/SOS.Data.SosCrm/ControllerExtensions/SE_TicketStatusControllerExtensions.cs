using AR = SOS.Data.SosCrm.SE_TicketStatusCode;
using ARCollection = SOS.Data.SosCrm.SE_TicketStatusCodeCollection;
using ARController = SOS.Data.SosCrm.SE_TicketStatusCodeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class SE_TicketStatusCodeControllerExtensions
	{

        public static ARCollection GetTicketStatusCodeList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.IsDeleted,false).ORDER_BY(AR.Columns.StatusCode);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }
	}
}
