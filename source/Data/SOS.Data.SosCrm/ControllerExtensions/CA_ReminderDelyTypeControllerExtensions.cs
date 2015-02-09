using AR = SOS.Data.SosCrm.CA_ReminderDelyType;
using ARCollection = SOS.Data.SosCrm.CA_ReminderDelyTypeCollection;
using ARController = SOS.Data.SosCrm.CA_ReminderDelyTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class CA_ReminderDelyTypeControllerExtensions
	{
		public static ARCollection GetOptionsList(this ARController oCntlr)
		{
			/** Initialize. */
			var oQry = AR.Query().ORDER_BY(AR.Columns.SortOrder);

			/** Return result. */
			return oCntlr.LoadCollection(oQry);
		}
	}
}
