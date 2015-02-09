using System;
using System.Globalization;

namespace SOS.FOS.ReceiverEngineServices.DeviceProtocols
{
	public class LaipacDevice
	{
		#region .ctor

		//public LaipacDevice()
		//{
		//}

		#endregion .ctor

		#region Methods

		/// <summary>
		/// Parses the following stream:
		/// S911 BL belongs to Owner (ID:80004280),has Panic Alert on 10:19pm 2012-11-06,Location:32.939363/-80.115652
		/// </summary>
		/// <param name="szMessage"></param>
		/// <returns></returns>
		public static LaipacDeviceEvent ParseMessageVersion002(string szMessage)
		{
			/** Initialize. */
			var oEvent = new LaipacDeviceEvent();
			var szaParts = szMessage.Split(',');

			/** Parse message. */
			oEvent.DeviceType = szaParts[(int)LaipacDeviceEvent.FieldsV002.DeviceType];
			var start = oEvent.DeviceType.IndexOf("(ID:", StringComparison.Ordinal) + 4;
			var strLength = oEvent.DeviceType.IndexOf(")", StringComparison.Ordinal) - start;
			oEvent.DeviceID = oEvent.DeviceType.Substring(start, strLength);

			/** Get Event Message: 'has Panic Alert on 10:19pm 2012-11-06' */
			string eventMessage = szaParts[(int) LaipacDeviceEvent.FieldsV002.EventMessage];
			oEvent.Event = eventMessage.IndexOf("Panic Alert", StringComparison.Ordinal) > 0
							? LaipacDeviceEvent.EventTypes.PANIC_ALERT
							: LaipacDeviceEvent.EventTypes.G_SENCER;

			/** Get event date and time. */
			var oDateAndTime = DateTime.Now;
			if (eventMessage.IndexOf("Panic Alert", StringComparison.Ordinal) > 0)
			{
				var saTimeDate = eventMessage.Substring(19).Split(' ');
				var ukCulture = new CultureInfo("en-GB");
				if (DateTime.TryParse(
						string.Format("{0} {1}", saTimeDate[1], saTimeDate[0]), ukCulture, DateTimeStyles.None,
						out oDateAndTime))
					oEvent.DateAndTime = oDateAndTime;
			}
			oEvent.DateAndTime = oDateAndTime;

			/** Get GPS Coords. */
			var saLocation = szaParts[(int) LaipacDeviceEvent.FieldsV002.Coords].Substring(9).Split('/');
			decimal dLatitude;
			if (decimal.TryParse(saLocation[0], out dLatitude))
			{
				oEvent.Latitude = dLatitude;

				decimal dLongitude;
				if (decimal.TryParse(saLocation[1], out dLongitude))
				{
					oEvent.Longitude = dLongitude;
				}
			}

			/** Return result. */
			return oEvent;
		}


		/// <summary>
		/// Parse the message from LAIPAC.
		/// i.e. "Alert from S911 BL, ID 80003902 ,01:15pm,16/04/12,Panic Alert, LAT North 40.314965 & LON West 111.675312"
		/// i.e. "Alert from S911 BL, ID 80003902 ,01:17pm,16/04/12,Fence Enter !, LAT North 40.319132 & LON West 111.675717"
		/// i.e. "Alert from S911 BL, ID 80003902 ,01:27pm,16/04/12,Fence Exit !, LAT North 40.319367 & LON West 111.676585"
		/// </summary>
		/// <param name="szMessage">string</param>
		/// <returns>LaipacDeviceEvent</returns>
		public static LaipacDeviceEvent ParseMessageVersion001(string szMessage)
		{
			/** Initialize. */
			var oEvent = new LaipacDeviceEvent();
			var szaParts = szMessage.Split(',');

			/** Parse message. */
			oEvent.DeviceType = szaParts[(int)LaipacDeviceEvent.FieldsV001.DeviceType];
			oEvent.DeviceID = szaParts[(int)LaipacDeviceEvent.FieldsV001.DeviceID].Replace("ID", string.Empty).Trim();

			DateTime oDateAndTime;
			var ukCulture = new CultureInfo("en-GB");
			if (DateTime.TryParse(string.Format("{0} {1}", szaParts[(int)LaipacDeviceEvent.FieldsV001.EventDate], szaParts[(int)LaipacDeviceEvent.FieldsV001.EventTime]), ukCulture, DateTimeStyles.None, out oDateAndTime))
				oEvent.DateAndTime = oDateAndTime;

			oEvent.Event = szaParts[(int)LaipacDeviceEvent.FieldsV001.Event];

			decimal? dLongitude;
			decimal? dLatitude;
			ParseGpsCoord(szaParts[5], out dLatitude, out dLongitude);
			oEvent.Latitude = dLatitude;
			oEvent.Longitude = dLongitude;

			/** Return result. */
			return oEvent;
		}

		public static LaipacDeviceEvent ParseSocketMessageAVRMC(string sMessage)
		{
			/** Initialize. */
			var oEvent = new LaipacDeviceEvent();
			string[] socketMessage = sMessage.Split(',');

			/** Bind information. */
			oEvent.DeviceType = "S911 BL";
			oEvent.DeviceID = socketMessage[(int) LaipacDeviceEvent.FieldsSockets.UnitID];

			oEvent.Event = LaipacDeviceEvent.EventCodeTanslation (socketMessage[(int)LaipacDeviceEvent.FieldsSockets.EventCode]);
			
			// ** Get Event Time
			var ukCulture = new CultureInfo("en-GB");
			DateTime oUtcDateTime;
			var dateTimeString = string.Format("{0}/{1}/20{2} {3}:{4}:{5}"
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCDate].Substring(0, 2)
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCDate].Substring(2, 2)
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCDate].Substring(4, 2)
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCTime].Substring(0, 2)
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCTime].Substring(2, 2)
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.UTCTime].Substring(4, 2)
				);
			if (!DateTime.TryParse(dateTimeString, ukCulture, DateTimeStyles.None, out oUtcDateTime))
				oUtcDateTime = DateTime.UtcNow;
			oEvent.DateAndTime = TimeZone.CurrentTimeZone.ToLocalTime(oUtcDateTime);

			/** Get coordinates. */
			decimal? dLongitude;
			decimal? dLatitude;
			ParseGpsCoord(socketMessage[(int)LaipacDeviceEvent.FieldsSockets.Latitude]
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.NSIndicator]
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.Longitude]
				, socketMessage[(int)LaipacDeviceEvent.FieldsSockets.EWIndicator]
				, out dLatitude, out dLongitude);
			oEvent.Latitude = dLatitude;
			oEvent.Longitude = dLongitude;

			/** Return result. */
			return oEvent;
		}

		private static void ParseGpsCoord(string sLatitude, string sNS, string sLongitude, string sEW, out decimal? dLatitude, out decimal? dLongitude)
		{
			/** Calculate Latitude. */
			dLatitude = decimal.Parse(sLatitude);
			if (sNS.Equals("S")) dLatitude *= (-1);

			/** Calculate Longitude. */
			dLongitude = decimal.Parse(sLongitude);
			if (sEW.Equals("W")) dLongitude *= (-1);
		}

		private static void ParseGpsCoord(string szRawGoords, out decimal? dLatitude, out decimal? dLongitude)
		{
			/** Initialize. */
			string[] szaCoords = szRawGoords.Split('&');
			dLatitude = dLongitude = null;

			foreach (var szCoord in szaCoords)
			{
				/** Check which type of coord it is. */
				bool isLat = szCoord.IndexOf("LAT", StringComparison.Ordinal) > -1;
				bool isNegative = szCoord.IndexOf("South", StringComparison.Ordinal) > -1 ||
				                  szCoord.IndexOf("West", StringComparison.Ordinal) > -1;
				var szNewCoord = szCoord.Replace("LAT", string.Empty);
				szNewCoord = szNewCoord.Replace("LON", string.Empty);
				szNewCoord = szNewCoord.Replace("North", string.Empty);
				szNewCoord = szNewCoord.Replace("South", string.Empty);
				szNewCoord = szNewCoord.Replace("West", string.Empty);
				szNewCoord = szNewCoord.Replace("East", string.Empty);

				/** Check that there are GPS Coords. */
				decimal dNewCoord;
				if (!decimal.TryParse(szNewCoord, out dNewCoord))
				{
					dLatitude = 0;
					dLongitude = 0;
					break;
				}

				/** Default path of execution. */
				if (isLat)
				{
					dLatitude = decimal.Parse(szNewCoord);
					if (isNegative) dLatitude = (-1) * dLatitude;
				}
				else
				{
					dLongitude = decimal.Parse(szNewCoord);
					if (isNegative) dLongitude = (-1) * dLongitude;
				}
			}
		}

		#endregion Methods

		#region Devices

		public static class Devices
		{
			public const string S911_BL = "S911 BL";
			public const string ALERT_FROM_S911_BL = "Alert from S911 BL";
		}

		#endregion Devices

		#region SocketMessageTypes

		public static class SocketMessageTypes
		{
			public const string AVRMC = "$AVRMC";
		}

		#endregion SocketMessageTypes
	}
}
