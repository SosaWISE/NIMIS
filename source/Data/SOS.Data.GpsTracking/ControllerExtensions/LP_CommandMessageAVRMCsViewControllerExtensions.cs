using AR = SOS.Data.GpsTracking.LP_CommandMessageAVRMCsView;
using ARCollection = SOS.Data.GpsTracking.LP_CommandMessageAVRMCsViewCollection;
using ARController = SOS.Data.GpsTracking.LP_CommandMessageAVRMCsViewController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class LP_CommandMessageAVRMCsViewControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR ProcessByUnitIDAndReqCommandID(this ARController oCntlr, long lUnitID, long reqCommandMessageId, string eventCodeId)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_CommandMessageAVRMCProcessByUnitIdAndReqCommandID(lUnitID, reqCommandMessageId, eventCodeId));
		}
	}
}
