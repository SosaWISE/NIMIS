namespace SOS.Lib.LaipacAPI
{
	public static class EventCodes
	{
		public const string GEOFENCE_ENTER_ALERT = "X";
		public const string TAMPER_DETECTION_SWITCH_IS_OPEN_ALERT = "T";
		public const string TAMPER_DETECTION_SWITCH_IS_CLOSE_ALERT = "S";
		public const string UNIT_IS_POWERED_OFF_OR_CHARGER_IS_PLUGGED_ID = "H";
		public const string GSM_CONNECTION_CHANGED_TO_ROAMING = "F";
		public const string GSP_CONNECTION_BACK_TO_HOME_NETWORK = "E";
		public const string G_SENSOR_ALERT_1 = "8";
		public const string INSTANCE_GEO_FENCE_EXIT_ALERT = "7";
		public const string OVER_SPEED_ALERT = "6";
		public const string GEO_FENCE_EXITS_ALERT = "4";
		public const string PANIC_SOS_BUTTON_PRESSED_ALERT = "3";
		public const string SOS_BUTTON_PRESSED_ALERT = "1";
		public const string REGULAR_REPORT = "0";
	}
}
