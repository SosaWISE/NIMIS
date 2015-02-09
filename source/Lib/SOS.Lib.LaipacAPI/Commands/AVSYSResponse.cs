using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Data.SosCrm;
using SOS.Lib.LaipacAPI.Helper;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class AVSYSResponse : Response
	{
		#region .ctor

		public AVSYSResponse(string rawSentence) 
			: base(rawSentence, LP_CommandType.MetaData.RequestCommandsID, CommandDef.AVSYS)
		{
			/** Initialize. */
			Parse();
		}

		#endregion .ctor

		#region Member Variables

		protected string FirmwareVersion { get; private set; }
		protected string SerialNumber { get; private set; }
		protected string MemorySize { get; private set; }

		#endregion Member Variables

		#region Member Functions

		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');

			UnitIDSet(aSplitString[FieldsAVSYS.UnitID]);
			FirmwareVersion = aSplitString[FieldsAVSYS.FirmwareVersion];
			SerialNumber = aSplitString[FieldsAVSYS.SerialNumber];
			MemorySize = aSplitString[FieldsAVSYS.MemorySize];
			
		}

		public void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage)
		{
			/** Initialize. */
			GS_Account oAccount = GPSUnit.GetMsAccount(UnitID.ToString(CultureInfo.InvariantCulture));
			var lpDevice = GpsTrackingDataContext.Instance.LP_Devices.LoadByPrimaryKey(UnitID) ??
						   new LP_Device { AccountID = oAccount.AccountID, UnitID = UnitID };

			/** Bind Device Info to object. */
			BindDeviceInfo(remoteEndPoint, lpDevice);
			/** Bind base command information. */
			base.SaveInfo(remoteEndPoint, commandMessage);

			/** Bind new data. */
			lpDevice.FirmwareVersion = FirmwareVersion;
			lpDevice.SerialNumber = SerialNumber;
			lpDevice.MemorySize = MemorySize;

			/** Save it. */
			lpDevice.Save();

			/** Save Command information. */
			commandMessage.CommandTypeId = CommandTypeID;
			commandMessage.CommandNameId = LP_CommandName.MetaData.AVSYSID;
			commandMessage.UnitID = UnitID;
			commandMessage.CreatedOn = DateTime.Now;
			commandMessage.DEX_ROW_TS = DateTime.UtcNow;
			commandMessage.Save();

			/** Attach it to the base class. */
			CommandMessageSet(commandMessage);
		}

		#endregion Member Functions
	}
}
