using AR = SOS.Data.SosCrm.MS_AccountClientDetailsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountClientDetailsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountClientDetailsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MS_AccountClientDetailsViewExtensions
	{
		public static AR GetDeviceDetailsByAccountId(this ARController oCntlr, long lAccountID, long lCustomerID)
		{
			/** Initialize. */
			var oQry = AR.Query()
				.WHERE(AR.Columns.AccountId, lAccountID)
				.WHERE(AR.Columns.CustomerId, lCustomerID);

			AR oResult = oCntlr.LoadSingle(oQry);

			/** Return result. */
			return oResult;
		}
	}
}
