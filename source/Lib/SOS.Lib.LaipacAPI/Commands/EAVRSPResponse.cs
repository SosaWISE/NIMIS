using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Data.SosCrm;
using SOS.Lib.LaipacAPI.Helper;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class EAVRSPResponse : Response
	{
		#region .ctor
		public EAVRSPResponse(string rawSentence)
			: base(rawSentence, LP_CommandType.MetaData.RequestCommandsID, CommandDef.EAVRSP)
		{
			Parse();
		}
		#endregion .ctor

		#region Member Variables

		public enum EAVRSPTypes
		{
			TypeDefaultResponse = 0,
			Type2Response = 2,
			Type3Response = 3,
			Type4Response = 4,
			Type5Response = 5
		}

		protected string EAVRSPType { get; private set; }
		protected GS_Account MsAccount { get; private set; }

		#endregion Member Variables

		#region Member Functions

		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');

			UnitIDSet(aSplitString[FieldsEAVRSP.UnitID]);
			EAVRSPType = aSplitString[FieldsEAVRSP.Type];
		}

		public void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage)
		{
			/** Initialize. */
			MsAccount = GPSUnit.GetMsAccount(UnitID.ToString(CultureInfo.InvariantCulture));
			var lpDevice = GpsTrackingDataContext.Instance.LP_Devices.LoadByPrimaryKey(UnitID) ??
						   new LP_Device { AccountID = MsAccount.AccountID, UnitID = UnitID };

			/** Bind Device Info to object. */
			BindDeviceInfo(remoteEndPoint, lpDevice);
			/** Bind base command information. */
			base.SaveInfo(remoteEndPoint, commandMessage);

			/** 
			 * Bind new data. 
			 * (Only if the sentence passed has info that could bind to LP_Devices.)
			 */

			/** Save it. */
			lpDevice.Save();

			/** Save Command information. */
			commandMessage.CommandTypeId = CommandTypeID;
			switch (EAVRSPType)
			{
				case "4":
					commandMessage.CommandNameId = LP_CommandName.MetaData.EAVRSP4ID;
					break;
				case "2":
				case "3":
				case "5":
					commandMessage.CommandNameId = LP_CommandName.MetaData.EAVRSP235ID;
					break;
				default:
					commandMessage.CommandNameId = LP_CommandName.MetaData.EAVRSPID;
					break;
			}
			commandMessage.CommandNameId = LP_CommandName.MetaData.EAVRSPID;
			commandMessage.UnitID = UnitID;
			commandMessage.CreatedOn = DateTime.Now;
			commandMessage.DEX_ROW_TS = DateTime.UtcNow;
			commandMessage.Save();

			/** Attach it to the base class. */
			CommandMessageSet(commandMessage);
		}

		public static EAVRSPTypes GetResponseType(string rawSentence)
		{
			var command = new EAVRSPResponse(rawSentence);

			/** Initialize. */
			switch (command.EAVRSPType)
			{
				case "2":
					return EAVRSPTypes.Type2Response;
				case "3":
					return EAVRSPTypes.Type3Response;
				case "4":
					return EAVRSPTypes.Type4Response;
				case "5":
					return EAVRSPTypes.Type5Response;
				default:
					return EAVRSPTypes.TypeDefaultResponse;
			}
		}

		#endregion Member Functions
	}
}