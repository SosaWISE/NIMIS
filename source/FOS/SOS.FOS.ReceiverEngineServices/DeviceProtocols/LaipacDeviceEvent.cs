using System;

namespace SOS.FOS.ReceiverEngineServices.DeviceProtocols
{
	public class LaipacDeviceEvent
	{
		#region Properties

		public enum FieldsV002
		{
			DeviceType = 0,
			EventMessage = 1,
			Coords = 2
		}

		public enum FieldsV001
		{
			DeviceType = 0,
			DeviceID = 1,
			EventTime = 2,
			EventDate = 3,
			Event = 4
		}

		public enum FieldsSockets
		{
			MessageType = 0,
			UnitID = 1,
			UTCTime = 2,
			Status = 3, /** A, V, or R */
			Latitude = 4,
			NSIndicator = 5,
			Longitude = 6,
			EWIndicator = 7,
			Speed = 8,
			Course = 9,
			UTCDate = 10,
			EventCode = 11,
			BatteryVoltage = 12, /** mV (milli Volts) */
			CurrentMileage = 13, /** Km */
			GPSOnOff = 14, /** 1 = on ; 0 = off) */
			AnalogPort1 = 15, /** (0-300mV) */
			AnalogPort2 = 16, /** (0-300mV) */
			CHKSUM = 17
		}

		public static class EventTypes
		{
			public const string PANIC_ALERT = "Panic Alert";
			public const string FENCE_ENTER = "Fence Enter !";
			public const string FENCE_EXIT = "Fence Exit !";
			public const string G_SENCER = "G sensor";
			public const string TAMPER_DETECT_OPEN = "Tamper Detected Open";
			public const string TAMPER_DETECT_CLOSE = "Tamper Detected Close";
			public const string UNIT_POWER_OFF_OR_CHARGIN = "Unit Powered Off or Charging";
			public const string GSM_CONN_CHANGE_ROAMING = "GSM CONN CHNG ROAM";
			public const string GSM_CONN_CHANGE_HOME_NETWORK = "GSM CONN CHNG HOME NTW";
			public const string G_SENSOR_ALERT_1 = "G-Sensor Alert 1";
			public const string SPEED_ALERT_OVER = "Over Speed Alert";
			public const string GEO_FENCE_EXITS_ALERT = "Geo-Fence Exits Alert";
			public const string SOS_BUTTON_PRESS_ALERT = "SOS Button Press Alert";
			public const string REGULAR_REPORT = "Regular Report";
			public const string UNDEFINED_CODE = "[Undefined Code]";
		}

		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public string Event { get; set; }

		public string DeviceType { get; set; }
		public string DeviceID { get; set; }

		public DateTime DateAndTime { get; set; }

		#endregion Properties


		#region Methods

		public static string EventCodeTanslation(string eventCode)
		{
			switch (eventCode.ToUpper())
			{
				case "X":
					return EventTypes.FENCE_ENTER;
				case "T":
					return EventTypes.TAMPER_DETECT_OPEN;
				case "S":
					return EventTypes.TAMPER_DETECT_CLOSE;
				case "H":
					return EventTypes.UNIT_POWER_OFF_OR_CHARGIN;
				case "F":
					return EventTypes.GSM_CONN_CHANGE_ROAMING;
				case "E":
					return EventTypes.GSM_CONN_CHANGE_HOME_NETWORK;
				case "8":
					return EventTypes.G_SENSOR_ALERT_1;
				case "7":
					return EventTypes.FENCE_EXIT;
				case "6":
					return EventTypes.SPEED_ALERT_OVER;
				case "4":
					return EventTypes.GEO_FENCE_EXITS_ALERT;
				case "3":
					return EventTypes.PANIC_ALERT;
				case "1":
					return EventTypes.SOS_BUTTON_PRESS_ALERT;
				case "0":
					return EventTypes.REGULAR_REPORT;
				default:
					return EventTypes.UNDEFINED_CODE;
			}
		}

		#endregion Methods
	}
}
