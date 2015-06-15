using System;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Lib.LaipacAPI.Helper;
using NXS.Lib;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class Response : IResponse
	{

		#region Commands
		public const string AVSYS = "AVSYS";
		public const string AVSYS_TEMPLATE = "AVSYS,{0}";
		public const string AVRMC = "AVRMC";
		public const string ECHK = "ECHK";
		public const string EAVRSP = "EAVRSP";
		#endregion Commands

		#region .ctor

		public Response(string rawSentence, string commandTypeID = LP_CommandType.MetaData.UndefinedID, CommandDef commandName = CommandDef.Undefined)
		{
			CommandTypeID = commandTypeID;
			CommandName = commandName;
			string[] sentenceArray = SentenceParser.RawSentenceToSentence(rawSentence);
			SentenceNet = sentenceArray[SentenceParser.Fields.Sentence];
			CheckSum = sentenceArray[SentenceParser.Fields.ChkSum];
		}

		#endregion .ctor

		#region Member Variables

		public string CommandTypeID { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }
		public long UnitID { get; private set; }
		public string SentenceNet { get; private set; }
		public string CheckSum { get; private set; }
		public CommandDef CommandName { get; private set; }
		public bool MonitoringEnvironmentIsLive
		{
			get
			{
				if (string.IsNullOrEmpty(WebConfig.Instance.GetConfig("LAIPACAPI.MonitoringStation.Environmet"))
					&& WebConfig.Instance.GetConfig("LAIPACAPI.MonitoringStation.Environmet").Equals("DEV"))
				{
					return true;
				}

				/** Default path of execution. */
				return false;
			}
		}

		public static class FieldsAVSYS
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int FirmwareVersion = Command + 2;
			public static int SerialNumber = Command + 3;
			public static int MemorySize = Command + 4;
		}

		public static class FieldsAVRMC
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int UTCTime = Command + 2;
			public static int Status = Command + 3;
			public static int Latitude = Command + 4;
			public static int NSIndicator = Command + 5;
			public static int Longitude = Command + 6;
			public static int EWIndicator = Command + 7;
			public static int Speed = Command + 8;
			public static int Course = Command + 9;
			public static int UTCDate = Command + 10;
			public static int EventCode = Command + 11;
			public static int BatteryVoltage = Command + 12;
			public static int CurrentMilage = Command + 13;
			public static int GPSStatus = Command + 14;
			public static int AnalogPort1 = Command + 15;
			public static int AnalogPort2 = Command + 15;
		}

		public static class FieldsECHK
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int SeqNo = Command + 2;
		}

		public static class FieldsEAVRSP
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int Type = Command + 2;
			//public static int GeoFence = Command + 2;
			//public static int ReportMode = Command + 3;
			//public static int Latitude1 = Command + 4;
			//public static int Longitude1 = Command + 5;
			//public static int Latitude2 = Command + 6;
			//public static int Longitude2 = Command + 7;
		}

		public static class FieldsEAVRSP3
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int Type = Command + 2;
			public static int RESPCode = Command + 3;
		}

		public static class FieldsEAVRSP4
		{
			public static int Command;
			public static int UnitID = Command + 1;
			public static int Type = Command + 2;
			public static int GeoFence = Command + 3;
			public static int ReportMode = Command + 4;
			public static int Latitude1 = Command + 5;
			public static int Longitude1 = Command + 6;
			public static int Latitude2 = Command + 7;
			public static int Longitude2 = Command + 8;
		}

		public static class RESPCode
		{
			public const int OK = 0;
			public const int INVALID_COMMAND = 1;
			public const int INVALID_PASSWORD = 2;
			public const int INVALID_CHECKSUM = 3;
			public const int INVALID_PARAMETER = 4;
			public const int SENTENCE_TOO_LONG = 5;
		}

		#endregion Member Variables

		#region Member Functions

		protected void UnitIDSet(string sUnitID)
		{
			UnitID = long.Parse(sUnitID);
		}
		protected void CommandMessageSet(LP_CommandMessage commandMessage)
		{
			CommandMessage = commandMessage;
		}

		public static object Factory(string sentence, EndPoint remoteEndPoint, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			return SaveSentance(sentence, remoteEndPoint, dispatchToCs, GetCommandObject);
		}

		private static object SaveSentance(string sentence, EndPoint remoteEndPoint, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, LP_CommandMessage, Func<string, decimal, decimal, string, string, bool?, bool>, object> action)
		{
			/** Initialize. */
			var commandMessage = new LP_CommandMessage {Sentence = sentence, MessageDate = DateTime.Now, CreatedOn = DateTime.Now, DEX_ROW_TS = DateTime.UtcNow};

			var command = action(sentence, remoteEndPoint, commandMessage, dispatchToCs);

			/** Return message object. */
			return command;
		}

		private static object GetCommandObject(string sentence, EndPoint remoteEndPoint, LP_CommandMessage commandMessage, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			/** Check for AVSYS. */
			if (sentence.IndexOf(AVSYS, StringComparison.Ordinal) == 1)
			{
				var avSYSResponse = new AVSYSResponse(sentence);
				avSYSResponse.SaveInfo(remoteEndPoint, commandMessage);
				return avSYSResponse;
			}

			/** Check for AVRMC. */
			if (sentence.IndexOf(AVRMC, StringComparison.Ordinal) == 1)
			{
				var avrmcCommand = new AVRMCResponse(sentence, dispatchToCs);
				avrmcCommand.SaveInfo(remoteEndPoint, commandMessage);
				return avrmcCommand;
			}

			/** Check for ECHK. */
			if (sentence.IndexOf(ECHK, StringComparison.Ordinal) == 1)
			{
				var echkCommand = new ECHKResponse(sentence);
				echkCommand.SaveInfo(remoteEndPoint, commandMessage);
				return echkCommand;
			}
			/** Check for EAVRSP. */
			if (sentence.IndexOf(EAVRSP, StringComparison.Ordinal) == 1)
			{
				switch (EAVRSPResponse.GetResponseType(sentence))
				{
					case EAVRSPResponse.EAVRSPTypes.Type4Response:
						var eavrsp4Command = new EAVRSP4Response(sentence);
						eavrsp4Command.SaveInfo(remoteEndPoint, commandMessage);
						return eavrsp4Command;
					case EAVRSPResponse.EAVRSPTypes.Type3Response:
						var eavrsp3Command = new EAVRSP3Response(sentence);
						eavrsp3Command.SaveInfo(remoteEndPoint, commandMessage);
						return eavrsp3Command;
					default:
						var eavrspCommand = new EAVRSPResponse(sentence);
						eavrspCommand.SaveInfo(remoteEndPoint, commandMessage);
						return eavrspCommand;
				}
			}

			/** Default path of execution. */
			// ** Save unfound sentence. */
			var oResponse = new Response(sentence);
			oResponse.SaveInfo(remoteEndPoint, commandMessage, forceSave: true);
			return oResponse;
		}

		public void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage, bool forceSave = false)
		{
			/** Bind information to the Response. */
			commandMessage.CommandTypeId = LP_CommandType.MetaData.UndefinedID;
			commandMessage.IPAddress = ((IPEndPoint) (remoteEndPoint)).Address.ToString();
			commandMessage.Port = ((IPEndPoint) (remoteEndPoint)).Port;
			if (forceSave) commandMessage.Save();

			/** Get a handle on the command message for this base class. */
			CommandMessage = commandMessage;
		}

		protected void BindDeviceInfo(EndPoint remoteEndPoint, LP_Device lpDevice)
		{
			lpDevice.IPAddress = ((IPEndPoint)(remoteEndPoint)).Address.ToString();
			lpDevice.Port = ((IPEndPoint)(remoteEndPoint)).Port;
			lpDevice.LastAccessDate = DateTime.Now;
		}

		public string GetResponseBack()
		{
			return string.Empty;
		}

		#endregion Member Functions
	}
}
