using System;
using SOS.Lib.LaipacAPI.Commands;
using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI
{
	public static class Validator
	{
		[Obsolete("This has been depricated.", true)]
		public static void ValidateFixedLength(this SystemInfoRequest.SystemInfoSentence oSentence)
		{
			/** Check to see if Unit is right in length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVSYS, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.FirmwareVersion.Length > 5 || oSentence.FirmwareVersion.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVSYS, "Firmware Version", Requests.VARIABLE, oSentence.FirmwareVersion, 5));
			if (oSentence.SerialNumber.Length > 10 || oSentence.SerialNumber.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVSYS, "Serial Number", Requests.VARIABLE, oSentence.SerialNumber, 10));
			if (oSentence.MemorySize.Length > 5 || oSentence.MemorySize.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVSYS, "Memory Size", Requests.VARIABLE, oSentence.MemorySize, 5));
			if (oSentence.ChkSum.Length > 2 || oSentence.ChkSum.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVSYS, "Chk Sum", Requests.VARIABLE, oSentence.ChkSum, 2));
		}

		public static void ValidateFixedLength(this LoggedDataSentence oSentence)
		{
			/** Check to see that values are the right length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.RESPCode.Length == 2) throw new LaipacParameterFixedLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "RESP Code", Requests.FIXED, oSentence.RESPCode, 2));
			if (oSentence.NumberOfDataLogsSet.Length > 3) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Number of Data Logs sent", Requests.VARIABLE, oSentence.NumberOfDataLogsSet, 3));
		}

		public static void ValidateFixedLength(this CurrentPositionSentence oSentence)
		{
			/** Check to see that values are the right length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.UTCTime.Length > 6 || oSentence.UTCTime.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "UTC Time", Requests.VARIABLE, oSentence.UnitID, 6));
			if (oSentence.Status.Length > 1 || oSentence.Status.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Status", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Latitude.Length > 9 || oSentence.Latitude.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Latitude", Requests.VARIABLE, oSentence.UnitID, 9));
			if (oSentence.NSIndicator.Length > 1 || oSentence.NSIndicator.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "NS Indicator", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Longitude.Length > 10 || oSentence.Longitude.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Longitude", Requests.VARIABLE, oSentence.UnitID, 10));
			if (oSentence.EWIndicator.Length > 1 || oSentence.EWIndicator.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "EW Indicator", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Speed.Length > 8 || oSentence.Speed.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Speed", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.Course.Length > 6 || oSentence.Course.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Course", Requests.VARIABLE, oSentence.UnitID, 6));
			if (oSentence.UTCDate.Length > 6 || oSentence.UTCDate.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "UTC Date", Requests.VARIABLE, oSentence.UnitID, 6));
			if (oSentence.EventCode.Length > 1 || oSentence.EventCode.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Event Code", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.BatteryVoltage.Length > 4 || oSentence.BatteryVoltage.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Battery Voltage", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.CurrentMileage.Length > 7 || oSentence.CurrentMileage.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Current Mileage", Requests.VARIABLE, oSentence.UnitID, 7));
			if (oSentence.GPSOnOff.Length > 1 || oSentence.GPSOnOff.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "GPS On Off", Requests.VARIABLE, oSentence.UnitID, 1));
			if (oSentence.AnalogPort1.Length > 4 || oSentence.AnalogPort1.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Analog Port 1", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.AnalogPort2.Length > 4 || oSentence.AnalogPort2.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Analog Port 2", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.ChkSum.Length > 2 || oSentence.ChkSum.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Check Sum", Requests.FIXED, oSentence.UnitID, 2));
		}

		public static void ValidateFixedLength(this CurrentStatusSentence oSentence)
		{
			/** Check to see that values are the right length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.GeoFence.Length > 1 || oSentence.GeoFence.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Geo-fence", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Panic.Length > 1 || oSentence.Panic.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Panic", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Opto1.Length > 1 || oSentence.Opto1.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Opto1", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Opto2.Length > 1 || oSentence.Opto2.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Opto2", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Relay2.Length > 1 || oSentence.Relay2.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Relay1", Requests.FIXED, oSentence.UnitID, 1));
			if (oSentence.Relay1.Length > 1 || oSentence.Relay1.Length == 1) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Relay2", Requests.FIXED, oSentence.UnitID, 1));
		}

		public static void ValidateFixedLength(this CurrentSettingsSentence oSentence)
		{
			/** Check to see that values are the right length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.LogTimeInterval.Length > 4 || oSentence.LogTimeInterval.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Log Time Interval", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.LogDistInterval.Length > 4 || oSentence.LogDistInterval.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Log Dist Interval", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.LogEventMask.Length != 2) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Log Event Mask", Requests.FIXED, oSentence.UnitID, 2));
			if (oSentence.ReportTimeInterval.Length > 4 || oSentence.ReportTimeInterval.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Report Time Interval", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.DistInterval.Length > 4 || oSentence.DistInterval.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Dist Interval", Requests.VARIABLE, oSentence.UnitID, 4));
			if (oSentence.ReportEventMask.Length != 2) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Report Event Mask", Requests.FIXED, oSentence.UnitID, 2));
			if (oSentence.GeoCentLat1.Length < 2 || oSentence.GeoCentLat1.Length > 10) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Geo Cent Lat 1", Requests.VARIABLE, oSentence.UnitID, 2));
			if (oSentence.GeoCentLon1.Length < 2 || oSentence.GeoCentLon1.Length > 11) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Geo Cent Lon 1", Requests.VARIABLE, oSentence.UnitID, 2));
			if (oSentence.GeoDeviation1.Length < 2 || oSentence.GeoDeviation1.Length > 5) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Geo Deviation 1", Requests.VARIABLE, oSentence.UnitID, 2));
		}

		public static void ValidateFixedLength(this CurrentPhoneNumberSentence oSentence)
		{
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
			if (oSentence.Phone0.Length > 31 || oSentence.Phone0.Length == 31) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Phone 0", Requests.VARIABLE, oSentence.UnitID, 31));
			if (oSentence.Phone1.Length > 31 || oSentence.Phone1.Length == 31) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Phone 1", Requests.VARIABLE, oSentence.UnitID, 31));
			if (oSentence.Phone2.Length > 31 || oSentence.Phone2.Length == 31) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Phone 2", Requests.VARIABLE, oSentence.UnitID, 31));
			if (oSentence.Phone3.Length > 31 || oSentence.Phone3.Length == 31) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Phone 3", Requests.VARIABLE, oSentence.UnitID, 31));
		}

		public static void ValidateFixedLength(this DeleteAllLoggedDataSentence oSentence)
		{
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
		}

		public static void ValidateFixedLength(this GPRSParametersSentence oSentence)
		{
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new LaipacParameterVariableLengthException(
				String.Format(Requests.INVALID_LENGTH_MSG, Requests.AVALL, "Unit ID", Requests.VARIABLE, oSentence.UnitID, 8));
		}
	}
}
