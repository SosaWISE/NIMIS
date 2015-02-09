using System;
using System.Net;
using SOS.Data.GpsTracking;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class EAVRSP3Response : EAVRSPResponse
	{
		#region .ctor
		public EAVRSP3Response(string rawSentence) : base(rawSentence)
		{
			Parse();
		}

		#endregion .ctor

		#region Member Variables

		protected int RESPCodeId { get; private set; }

		protected LP_CommandMessageEAVRSP3 CommandMessageEAVRSP3 { get; private set; }

		#endregion Member Variables

		#region Member Functions

		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');

			UnitIDSet(aSplitString[FieldsEAVRSP.UnitID]);
			RESPCodeId = Convert.ToInt32(aSplitString[FieldsEAVRSP3.RESPCode]);
		}

		public new void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage)
		{
			/** Call base first. */
			base.SaveInfo(remoteEndPoint, commandMessage);

			/** Save this command to the database. */
			CommandMessageEAVRSP3 = new LP_CommandMessageEAVRSP3
			{
				CommandMessageID = commandMessage.CommandMessageID,
				UnitID = UnitID,
				RESPCode = RESPCodeId,
				CreatedOn = DateTime.Now,
				DEX_ROW_TS = DateTime.UtcNow
			};
			if (commandMessage.UnitID != null) CommandMessageEAVRSP3.UnitID = (long)commandMessage.UnitID;
			CommandMessageEAVRSP3.Save();
		}

		#endregion Member Functions
	}
}
