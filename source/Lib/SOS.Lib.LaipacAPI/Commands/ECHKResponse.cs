using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Data.SosCrm;
using SOS.Lib.LaipacAPI.Helper;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class ECHKResponse : Response
	{
		#region .ctor
		public ECHKResponse(string rawSentence)
			: base(rawSentence, LP_CommandType.MetaData.RequestCommandsID, CommandDef.ECHK)
		{
			Parse();
		}
		#endregion .ctor

		#region Member Variables

		public string SeqNo { get; private set; }
		public ECHKRequest CommandECHKRequest { get; private set; }
		public long CommandMessageID { get; private set; }

		#endregion Member Variables

		#region Member Functions

		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');
			UnitIDSet(aSplitString[FieldsECHK.UnitID]);
			SeqNo = aSplitString[FieldsECHK.SeqNo];
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
			commandMessage.UnitID = UnitID;
			SaveInfo(remoteEndPoint, commandMessage, true);

			/** 
			 * Bind new data. 
			 * (Only if the sentence passed has info that could bind to LP_Devices.)
			 */

			/** Save it. */
			lpDevice.Save();
			/** Initialize. */
			var oECHKResponse = new LP_CommandMessageECHK
			                    	{
			                    		CommandMessageId = commandMessage.CommandMessageID,
										CommandTypeId = CommandTypeID,
										UnitID = UnitID,
										SeqNo = SeqNo,
										CreatedOn = DateTime.Now
			                    	};
			oECHKResponse.Save();

			/** Get Command ID. */
			CommandMessageID = oECHKResponse.CommandMessageId;

			/** Create the request command. */
			CommandECHKRequest = new ECHKRequest(UnitID, SeqNo, oECHKResponse.CommandMessageECHKID);
		}

		public new string GetResponseBack()
		{
			if (CommandECHKRequest != null)
				return CommandECHKRequest.GetRequest();

			/** Default path of execution */
			return string.Empty;
		}

		#endregion Member Functions
	}
}