using System;
using SOS.Data.GpsTracking;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class EAVACKRequest : Requests
	{
		/**
		 * 56. Special $AVRMC Message and Serve’s Acknowledgement (optional)
		 */
		#region .ctor

		public EAVACKRequest(string eventCode, string eventChkSum, LP_CommandMessageAVRMC oCommandMessageAVRMC)
			: base(DEFAULT_BLANK_PASSWRD)
		{
			EventCode = eventCode;
			EventChkSum = eventChkSum;
			CommandMessageAVRMC = oCommandMessageAVRMC;

			CreateCommandMessage();
		}

		#endregion .ctor

		#region Member Variables

		public string EventCode { get; private set; }
		public string EventChkSum { get; private set; }
		public LP_CommandMessageAVRMC CommandMessageAVRMC { get; private set; }
		public LP_CommandMessageEAVACK CommandMessageEAVACK { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }

		#endregion Member Variables

		#region Member Functions

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			{
				CommandTypeId = LP_CommandType.MetaData.OthersID,
				CommandNameId = LP_CommandName.MetaData.EAVACKID,
				MessageDate = DateTime.Now,
				Sentence = GetRequestWrapper(string.Format(REQ_EAVACK_SENT_COMMAND, EventCode, EventChkSum)),
				CreatedOn = DateTime.Now,
				DEX_ROW_TS = DateTime.UtcNow
			};
			CommandMessage.Save();

			CommandMessageEAVACK = new LP_CommandMessageEAVACK
			                       	{
			                       		CommandMessageID = CommandMessage.CommandMessageID,
			                       		ResponseToCommandMessageId = CommandMessageAVRMC.CommandMessageID,
			                       		AckCode = EventCode,
			                       		AckSum = EventChkSum,
			                       		CreatedOn = DateTime.Now
			                       	};
			CommandMessageEAVACK.Save();

		}

		public string GetRequest()
		{
			return GetRequestWrapper(string.Format(REQ_EAVACK_SENT_COMMAND, EventCode, EventChkSum));
		}

		#endregion Member Functions
	}
}
