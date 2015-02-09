using AR = SOS.Data.SosCrm.CA_ReminderMediaType;
using ARCollection = SOS.Data.SosCrm.CA_ReminderMediaTypeCollection;
using ARController = SOS.Data.SosCrm.CA_ReminderMediaTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class CA_ReminderMediaTypeControllerExtensions
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
