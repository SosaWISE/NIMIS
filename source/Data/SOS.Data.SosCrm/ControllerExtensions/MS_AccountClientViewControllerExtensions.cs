using AR = SOS.Data.SosCrm.MS_AccountClientsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountClientsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountClientsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class MS_AccountClientViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetAccountsByCMFID(this ARController oCntlr, long lCMFID)
		{
			/** Initialize. */
			var oQry = AR.Query().WHERE(AR.Columns.CustomerMasterFileId, lCMFID);

			/** Return result. */
			return oCntlr.LoadCollection(oQry);
		}

		public static ARCollection GetAccountsByCustomerID(this ARController oCntlr, long lCustomerID)
		{
			/** Initialize. */
			var oQry = AR.Query().WHERE(AR.Columns.CustomerID, lCustomerID);

			/** Return result. */
			return oCntlr.LoadCollection(oQry);
		}

		public static AR UpdateDevice(this ARController oCntlr, long lAccountID, string sAccountName, string sAccountDesc)
		{
			return oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.MC_AccountUpdate(lAccountID, sAccountName, sAccountDesc));
		}
	}
}
