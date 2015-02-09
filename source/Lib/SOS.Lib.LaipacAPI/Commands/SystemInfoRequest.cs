using System;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Lib.LaipacAPI.Models;

namespace SOS.Lib.LaipacAPI.Commands
{
	/********************************************
	 * 1. Request System Information
	 *******************************************/
	public class SystemInfoRequest : Requests
	{
		#region .ctor

		public SystemInfoRequest(EndPoint remoteEndPoint)
			: base(DEFAULT_BLANK_PASSWRD)
		{
			IPAddress = ((IPEndPoint)(remoteEndPoint)).Address.ToString();
			Port = ((IPEndPoint)(remoteEndPoint)).Port;

			CreateCommandMessage();
		}

		#endregion .ctor

		#region Member Properties

		protected string IPAddress { get; private set; }
		protected int Port { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }

		#endregion Member Properties

		#region Member Functions

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			{
				CommandTypeId = LP_CommandType.MetaData.OthersID,
				CommandNameId = LP_CommandName.MetaData.ECHKID,
				IPAddress = IPAddress,
				Port = Port,
				MessageDate = DateTime.Now,
				Sentence = GetRequestWrapper(string.Format(AVREQ_NO_PASSWORD, "?")),
				CreatedOn = DateTime.Now
			};
			CommandMessage.Save();

		}

		#endregion Member Functions

		#region System Information

		public string GetRequest()
		{
			return GetRequestWrapper(string.Format(AVREQ_NO_PASSWORD, "?"));
		}

		public static string GetRequestStatic()
		{
			return GetRequestWrapper(string.Format(AVREQ_NO_PASSWORD, "?"));
		}

		[Obsolete("This is no longer used.", true)]
		public static SystemInfoSentence SystemInfoResponse(string rawSentence)
		{
			/** Initialize. */
			var oSentence = new Sentence(rawSentence);
			string[] compt = oSentence.SentenceArray;
			string sUnitID = compt[1];
			string sFirmwareVersion = compt[2];
			string sSerialNumber = compt[3];
			string sMemorySize = compt[4];
			string sChkSum = oSentence.ChkSum;

			/** Create result. */
			var oSysInfoResult = new SystemInfoSentence(sUnitID, sFirmwareVersion, sSerialNumber, sMemorySize, sChkSum);

			/** Validate it */
			oSysInfoResult.ValidateFixedLength();

			/** Return result. */
			return oSysInfoResult;
		}

		[Obsolete("This is no longer used.", true)]
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
	}
}
