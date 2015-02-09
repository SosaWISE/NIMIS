using AR = SOS.Data.GpsTracking.LP_Request;
using ARCollection = SOS.Data.GpsTracking.LP_RequestCollection;
using ARController = SOS.Data.GpsTracking.LP_RequestController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class LP_RequestControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetQueue(this ARController oCntlr, int attemptNumberPerCmd)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.LP_RequestGetQueue(attemptNumberPerCmd));
		}

		public static AR IncrementAttempt(this ARController oCntlr, long requestID, int incrementBy)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_RequestIncrementAttempt(requestID, incrementBy));
		}

		public static AR Process(this ARController oCntlr, long requestID)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.LP_RequestProcess(requestID));
		}
	}
}
