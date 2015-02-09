using AR = SOS.Data.GpsTracking.KW_Request;
using ARCollection = SOS.Data.GpsTracking.KW_RequestCollection;
using ARController = SOS.Data.GpsTracking.KW_RequestController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class KW_RequestControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static ARCollection GetQueue(this ARController oCntlr, int attemptNumberPerCmd)
		{
			return oCntlr.LoadCollection(GpsTrackingDataStoredProcedureManager.KW_RequestGetQueue(attemptNumberPerCmd));
		}

		public static AR IncrementAttempt(this ARController oCntlr, long requestID, int incrementBy)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.KW_RequestIncrementAttempt(requestID, incrementBy));
		}

		public static AR Process(this ARController oCntlr, long requestID)
		{
			return oCntlr.LoadSingle(GpsTrackingDataStoredProcedureManager.KW_RequestProcess(requestID));
		}
	}
}
