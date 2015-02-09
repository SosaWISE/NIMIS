using System;
using SOS.Data.GpsTracking;

namespace SOS.Lib.LaipacAPI.Commands
{
	/********************************************
	 * 2. Request Logged Data (optional)
	 * 3. Request Current Position
	 * 4. Request Current Status
	 * 5. Request Current Settings
	 * 6. Request Current Mileage and Speed Limits
	 * 7. Request and Clear Logged Data
	 * 8. Request Current Phone Number
	 * 8.1 Delete All Logged Data
	 * 9. Request GPRS Parameters
	 *******************************************/
	public class AVREQRequest : Requests
	{
		#region .ctor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="lUnitID">long</param>
		/// <param name="eReqCommandID">AVREQCommandIDs</param>
		public AVREQRequest(string sPassword, long lUnitID, AVREQCommandIDs eReqCommandID) : base(sPassword)
		{
			UnitID = lUnitID;
			ReqCommandID = eReqCommandID;
			CreateCommandMessage();
		}
		#endregion .ctor

		#region Member Properties

		public long UnitID { get; private set; }
		public LP_CommandMessage CommandMessage { get; private set; }
		public AVREQCommandIDs ReqCommandID { get; private set; }
		public string Sentence
		{
			get { return GetRequestWrapper(string.Format(AVREQ, Password, (int) ReqCommandID)); }
		}

		#endregion Member Properties

		#region Member Functions

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			{
				UnitID = UnitID,
				CommandTypeId = LP_CommandType.MetaData.RequestCommandsID,
				CommandNameId = LP_CommandName.MetaData.AVREQID,
				MessageDate = DateTime.Now,
				Sentence = Sentence,
				CreatedOn = DateTime.Now,
				DEX_ROW_TS = DateTime.UtcNow
			};
			CommandMessage.Save();
		}

		public string GetRequest()
		{
			return Sentence;
		}

		public LP_Request QueueRequest(string sUsername, long lAccountId)
		{
			var lpRequest = new LP_Request
			{
				RequestNameId = LP_CommandName.MetaData.AVREQID,
				CommandMessageId = CommandMessage.CommandMessageID,
				AccountId = lAccountId,
				UnitID = UnitID,
				Sentence = Sentence,
				Attempts = 0,
				CreatedOn = DateTime.UtcNow
			};
			lpRequest.Save(sUsername);

			/** Save RequestID into CommandMessage. */
			CommandMessage.RequestId = lpRequest.RequestID;
			CommandMessage.Save(sUsername);

			/** Return queue item. */
			return lpRequest;
		}

		#endregion Member Functions
	}

	public enum AVREQCommandIDs
	{
		RequestCurrentPosition = 1,
		RequestCurrentstatus = 2,
		RequestCurrentSettings = 3,
		RequestCurrentMilageAndSpeedLimits = 4,
		RequestAndClearLoggedData = 5,
		RequestGPRSParameters = 7,
		DeleteAllLoggedData = 8,
		RequestCurrentPhoneNumber = 9
	}

}
