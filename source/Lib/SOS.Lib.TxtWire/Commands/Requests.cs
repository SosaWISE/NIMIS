using System;
using SOS.Lib.TxtWire.ErrorHandling;

namespace SOS.Lib.TxtWire.Commands
{
	[Obsolete("This class is not used anymore", true)]
	public static class Requests
	{
		#region Commands

		public const string AVREQ = "$AVREQ,{0}";
		internal const string SYSTEM_INFO_VALIDATION_MESSAGE_1 = "Request System Info requires a command of {0} byte(s).";
		public const string REQ_LOGGED_DATA_COMMAND = "$AVREQ,{0},0";
		
		internal const string VARIALBE = "Variable";
		internal const string FIXED = "Fixed";
		internal const string AVSYS = "AVSYS";
		internal const string AVALL = "AVALL";
		internal const string INVALID_LENGTH_MSG =
			"The following '{0}' sentence component '{1}' of {2} length {3} is (length:{4}) invalid.";

		#endregion Commands

		#region Methods

		#region Validation Functions

		public static void ValidateFixedLength(string sentenceName, string componentName, string value, short length)
		{
			if (value.Length != length) throw new TxtWireExceptionInvalidLengthExceptions(string.Format(INVALID_LENGTH_MSG
				, sentenceName, componentName, "fixed", length, value.Length ));
		}
		public static void ValidateFixedLength(this SystemInfoSentence oSentence)
		{
			/** Check to see if Unit is right in length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVSYS, "Unit ID", VARIALBE, oSentence.UnitID, 8));
			if (oSentence.FirmwareVersion.Length > 5 || oSentence.FirmwareVersion.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVSYS, "Firmware Version", VARIALBE, oSentence.FirmwareVersion, 5));
			if (oSentence.SerialNumber.Length > 10 || oSentence.SerialNumber.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVSYS, "Serial Number", VARIALBE, oSentence.SerialNumber, 10));
			if (oSentence.MemorySize.Length > 5 || oSentence.MemorySize.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVSYS, "Memory Size", VARIALBE, oSentence.MemorySize, 5));
			if (oSentence.ChkSum.Length > 2 || oSentence.ChkSum.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVSYS, "Chk Sum", VARIALBE, oSentence.ChkSum, 2));
		}

		public static void ValidateFixedLength(this LoggedDataSentence oSentence)
		{
			/** Check to see that values are the right length. */
			if (oSentence.UnitID.Length > 8 || oSentence.UnitID.Length == 0) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVALL, "Unit ID", VARIALBE, oSentence.UnitID, 8));
			if (oSentence.RESPCode.Length == 2) throw new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVALL, "RESP Code", FIXED, oSentence.RESPCode, 2));
			if (oSentence.NumberOfDataLogsSet.Length > 3) throw  new TxtWireExceptionInvalidLengthExceptions(
				string.Format(INVALID_LENGTH_MSG, AVALL, "Number of Data Logs sent", VARIALBE, oSentence.NumberOfDataLogsSet, 3));
		}

		#endregion Validation Functions

		/********************************************
		 * 1. Request System Information
		 *******************************************/
		#region System Information

		public static string SystemInfoRequest(string sCommand)
		{
			if (sCommand.Length != 1)
				throw new TxtWireExceptionInvalidLengthExceptions(string.Format(SYSTEM_INFO_VALIDATION_MESSAGE_1, 1));
			return string.Format(AVREQ, sCommand);
		}

		public static SystemInfoSentence SystemInfoResponse(string rawSentence)
		{
			/** Initialize. */
			string[] compt = Helper.SentenceParser.RawSentenceToSentence(rawSentence);
			string sUnitID = compt[1];
			string sFirmwareVersion = compt[2];
			string sSerialNumber = compt[3];
			string sMemorySize = compt[4];
			string sChkSum = compt[5];

			/** Create result. */
			var oSysInfoResult = new SystemInfoSentence(sUnitID, sFirmwareVersion, sSerialNumber, sMemorySize, sChkSum);

			/** Validate it */
			oSysInfoResult.ValidateFixedLength();

			/** Return result. */
			return oSysInfoResult;
		}

		public class SystemInfoSentence
		{
			/// <summary>
			/// Create a SystemInfoSentence class
			/// </summary>
			/// <param name="sUnitID"></param>
			/// <param name="sFirmwareVersion"></param>
			/// <param name="sSerialNumber"></param>
			/// <param name="sMemorySize"></param>
			/// <param name="sChkSum"></param>
			public SystemInfoSentence(string sUnitID, string sFirmwareVersion, string sSerialNumber, string sMemorySize, string sChkSum)
			{
				UnitID = sUnitID;
				FirmwareVersion = sFirmwareVersion;
				SerialNumber = sSerialNumber;
				MemorySize = sMemorySize;
				ChkSum = sChkSum;
			}

			public string UnitID { get; private set; }
			public string FirmwareVersion { get; private set; }
			public string SerialNumber { get; private set; }
			public string MemorySize { get; private set; }
			public string ChkSum { get; private set; }
		}

		#endregion System Information

		/********************************************
		 * 2. Request Logged Data
		 *******************************************/
		#region Logged Data

		public static string LoggedDataRequest(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new TxtWireExceptionInvalidLengthExceptions(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			return string.Format(REQ_LOGGED_DATA_COMMAND, sPassword);
		}

		public static LoggedDataSentence LoggedDataResponse(string sentence)
		{
			/** Inititalize. */
			var oSentence = new LoggedDataSentence(sentence);

			/** Validate. */
			oSentence.ValidateFixedLength();

			/** Return result. */
			return oSentence;
		}

		public class LoggedDataSentence
		{
			#region .ctor
			
			public LoggedDataSentence(string sentence)
			{
				/** Initialize. */
				string[] saSentence = sentence.Split(',');
				Command = saSentence[0];
				UnitID = saSentence[1];
				RESPCode = saSentence[2];
				NumberOfDataLogsSet = saSentence[3];
			}

			#endregion .ctor

			#region Properties

			public string Command { get; private set; }
			public string UnitID { get; private set; }
			public string RESPCode { get; private set; }
			public string NumberOfDataLogsSet { get; private set; }
			public string CheckSum { get; private set; }

			#endregion Properties
		}

		#endregion Logged Data

		/********************************************
		 * 3. Request Current Position
		 *******************************************/

		public class CurrentPositionSentence
		{
			#region .ctor
			#endregion .ctor

			#region Properties

			public string Command { get; private set; }
			public string UnitID { get; private set; }
			public string UTCTime { get; private set; }
			public string Status { get; private set; }
			public string Latitude { get; private set; }
			public string NSIndicator { get; private set; }
			public string Longitude { get; private set; }
			public string EWIndicator { get; private set; }
			public string Speed { get; private set; }
			public string Course { get; private set; }
			public string UTCDate { get; private set; }
			public string EventCode { get; private set; }
			public string BatteryVoltage { get; private set; }
			public string CurrentMileage { get; private set; }
			public string GPSOnOff { get; private set; }
			public string AnalogPort1 { get; private set; }
			public string AnalogPort2 { get; private set; }
			public string CheckSum { get; private set; }

			#endregion Properties
		}

		#endregion Methods

		#region Exceptions
		#endregion Exceptions
	}
}
