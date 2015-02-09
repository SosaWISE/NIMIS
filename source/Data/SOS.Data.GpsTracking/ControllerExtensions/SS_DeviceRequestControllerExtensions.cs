using AR = SOS.Data.GpsTracking.SS_DeviceRequest;
using ARCollection = SOS.Data.GpsTracking.SS_DeviceRequestCollection;
using ARController = SOS.Data.GpsTracking.SS_DeviceRequestController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class SS_DeviceRequestControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetQueue(this ARController oCntlr, int attemptNumberPerCmd)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.SS_DeviceRequestGetQueue(attemptNumberPerCmd));
		}

		public static AR IncrementAttempt(this ARController oCntlr, long requestID, int incrementBy)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.SS_DeviceRequestIncrementAttempt(requestID, incrementBy));
		}

		public static AR Process(this ARController oCntlr, long requestID)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.SS_DeviceRequestProcess(requestID));
		}
	}
}
