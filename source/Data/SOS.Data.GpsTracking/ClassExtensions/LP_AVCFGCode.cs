namespace SOS.Data.GpsTracking
{
// ReSharper disable InconsistentNaming
	public partial class LP_AVCFGCode
// ReSharper restore InconsistentNaming
	{
		public enum MetaDataEnum
		{
			DisableGSensor,
			EnableGSensor,
			DisableGPSReceiver,
			EnableGPSReceiver,
			EnableAutoAnswerMode,
			DisableAutoAnswerMode,
			AcknowledgeSOSPanicAlertMessage,
			EnableMonitorMode,
			DisableMonitorMode,
			EnableTamperDetection,
			DisableTamperDetection,
			QueryCurrentFeatureFlagStatus,
			AcknowledgeTamperDetectionAlertMessage,
			AcknowledgeGeoFenceAlertMessage,
			AcknowledgeLowBatteryAlertMessage
		}
	}
}
