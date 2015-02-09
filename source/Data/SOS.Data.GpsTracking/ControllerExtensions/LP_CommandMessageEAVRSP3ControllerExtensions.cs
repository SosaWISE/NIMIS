using AR = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP3;
using ARCollection = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP3Collection;
using ARController = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP3Controller;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class LP_CommandMessageEAVRSP3ControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static AR ProcessByUnitID(this ARController oCntlr, long lUnitID)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_CommandMessageEAVRSP3ProcessByUnitID(lUnitID));
		}
	}
}
