using AR = SLS.Data.Cms.XN_CustomerOpenTicket;
using ARCollection = SLS.Data.Cms.XN_CustomerOpenTicketCollection;
using ARController = SLS.Data.Cms.XN_CustomerOpenTicketController; 

namespace SLS.Data.Cms.ControllerExtensions
{
	public static class XN_CustomerOpenTicketControllerExtensions
	{
		public static ARCollection GetAllOpenTicketByUserId (this ARController oCntlr, int nUserID)
		{
			return oCntlr.LoadCollection(AR.Query()
			                             	.WHERE(AR.Columns.UserId, nUserID)
			                             	.ORDER_BY(AR.Columns.CustomerId)
			                             	.ORDER_BY(AR.Columns.NoteThreadId));
		}
	}
}
