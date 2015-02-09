using AR = SOS.Data.SosCrm.AE_GpsClientToCustomerMasterView;
using ARCollection = SOS.Data.SosCrm.AE_GpsClientToCustomerMasterViewCollection;
using ARController = SOS.Data.SosCrm.AE_GpsClientToCustomerMasterViewController;

// ReSharper disable CheckNamespace
namespace SOS.Data.SosCrm
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming
	public static class AE_GpsClientToCustomerMasterViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection Read(this ARController oCntlr, long? customerMasterFileId, long? customerID)
		{
			/** Initialize. */
			var oResultCol =
				oCntlr.LoadCollection(
					SosCrmDataStoredProcedureManager.AE_GpsClientToCustomerMasterGetByCMFIdAndCustomerID(customerMasterFileId,
					                                                                                     customerID));

			/** Return result. */
			return oResultCol;
		}
	}
}
