using System;
using SOS.Data.GpsTracking;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class ECHKRequest : Requests
	{
		#region .ctor

		public ECHKRequest(long lUnitID, string sSeqNo, long lResponseToCommandMessageECHKId)
			: base(DEFAULT_BLANK_PASSWRD)
		{
			UnitID = lUnitID;
			SeqNo = sSeqNo;
			ResponseToCommandMessageECHKId = lResponseToCommandMessageECHKId;

			CreateCommandMessage();
		}

		#endregion .ctor

		#region Member Variables

		public long UnitID { get; private set; }
		public string SeqNo { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }
		public long ResponseToCommandMessageECHKId { get; private set; }

		#endregion Member Variables

		#region Member Functions

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			                 	{
			                 		CommandTypeId = LP_CommandType.MetaData.OthersID,
									CommandNameId = LP_CommandName.MetaData.ECHKID,
									UnitID = UnitID,
									MessageDate = DateTime.Now,
									Sentence = GetRequestWrapper(string.Format(REQ_ECHK_SENTNC_COMMAND, UnitID, SeqNo)),
									CreatedOn = DateTime.Now,
									DEX_ROW_TS = DateTime.UtcNow
			                 	};
			CommandMessage.Save();

		}

		public string GetRequest()
		{
			/** Save information to the database. */
			var commandMessage = new LP_CommandMessageECHK
			                     	{
										CommandMessageId = CommandMessage.CommandMessageID,
										ResponseToCommandMessageECHKId = ResponseToCommandMessageECHKId,
										CommandTypeId = LP_CommandType.MetaData.OthersID,
										UnitID = UnitID,
			                     		SeqNo = SeqNo,
										CreatedOn = DateTime.Now
			                     	};
			commandMessage.Save();

			/** Return request to socket. */
			return GetRequestWrapper(string.Format(REQ_ECHK_SENTNC_COMMAND, UnitID, SeqNo));
		}

		#endregion Member Functions
	}
}
