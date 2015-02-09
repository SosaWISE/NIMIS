using AR = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP4;
using ARCollection = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP4Collection;
using ARController = SOS.Data.GpsTracking.LP_CommandMessageEAVRSP4Controller;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class LP_CommandMessageEAVRSP4ControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection ProcessByUnitID(this ARController oCntlr, long lUnitID)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.LP_CommandMessageEAVRSP4ProcessByUnitID(lUnitID));
		}
	}
}
