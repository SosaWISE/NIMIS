using AR = SOS.Data.GpsTracking.LP_AVCFGCode;
using ARCollection = SOS.Data.GpsTracking.LP_AVCFGCodeCollection;
using ARController = SOS.Data.GpsTracking.LP_AVCFGCodeController;

namespace SOS.Data.GpsTracking.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class LP_AVCFGCodeControllerExtensions
// ReSharper restore InconsistentNaming
	{
		public static string GetMetaData(this ARController oCntlr, AR.MetaDataEnum eCode)
		{
			switch (eCode)
			{
				case AR.MetaDataEnum.DisableGSensor:
					return AR.MetaData.DisableGSensorID;
				case AR.MetaDataEnum.EnableGSensor:
					return AR.MetaData.EnableGSensorID;
				case AR.MetaDataEnum.DisableGPSReceiver:
					return AR.MetaData.DisableGPSReceiverID;
				case AR.MetaDataEnum.EnableGPSReceiver:
					return AR.MetaData.EnableGPSReceiverID;
				case AR.MetaDataEnum.EnableAutoAnswerMode:
					return AR.MetaData.EnableAutoAnswerModeID;
				case AR.MetaDataEnum.DisableAutoAnswerMode:
					return AR.MetaData.DisableAutoAnswerModeID;
				case AR.MetaDataEnum.AcknowledgeSOSPanicAlertMessage:
					return AR.MetaData.AcknowledgeSOSPanicAlertMessageID;
				case AR.MetaDataEnum.EnableMonitorMode:
					return AR.MetaData.EnableMonitorModeID;
				case AR.MetaDataEnum.DisableMonitorMode:
					return AR.MetaData.DisableMonitorModeID;
				case AR.MetaDataEnum.EnableTamperDetection:
					return AR.MetaData.EnableTamperDetectionID;
				case AR.MetaDataEnum.DisableTamperDetection:
					return AR.MetaData.DisableTamperDetectionID;
				case AR.MetaDataEnum.QueryCurrentFeatureFlagStatus:
					return "?";
				case AR.MetaDataEnum.AcknowledgeTamperDetectionAlertMessage:
					return AR.MetaData.AcknowledgeTamperDetectionAlertMessageID;
				case AR.MetaDataEnum.AcknowledgeGeoFenceAlertMessage:
					return AR.MetaData.AcknowledgeGeoFenceAlertMessageID;
				case AR.MetaDataEnum.AcknowledgeLowBatteryAlertMessage:
					return AR.MetaData.AcknowledgeLowBatteryAlertMessageID;
				default:
					return "?";
			}
		}
	}
}
