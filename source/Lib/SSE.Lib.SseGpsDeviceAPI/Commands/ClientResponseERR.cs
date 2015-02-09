using System;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientResponseERR : Response
	{
		#region .ctor

		public ClientResponseERR(string sentence) : base(sentence, CommandDef.ERR) { }

		public ClientResponseERR(string accountID, ErrorCode code, string message, MessageState msgState, DateTime eventDateTime)
			: base(string.Empty, CommandDef.ERR)
		{
			AccountID = accountID;
			Message = message;
			Code = code;
			MessageState = msgState;
			UTCEventDateTime = eventDateTime;
		}

		#endregion .ctor

		#region Properties

		private const string _SENTENCE_RAW = "ERR,{AccountID},{UTCTime},{Code},{Message},{MessageState}";
		private const string _SENTENCE = "${0}*{1}";

		public enum ErrorCode
		{
			General = 100,
			FailedCheckSum = 200
		}

		public string AccountID { get; set; }
		public string Message { get; set; }
		public ErrorCode Code { get; set; }
		public MessageState MessageState { get; set; }
		public DateTime UTCEventDateTime { get; set; }
		public string CheckSum { get; private set; }

		#endregion Properties

		#region Methods

		public string GetSentence()
		{
			/** Initialize. */
			var rawSentence = _SENTENCE_RAW.Replace("{AccountID}", AccountID);
			rawSentence = rawSentence.Replace("{UTCTime}", DateTimeConversions.GetTimeDeivceFormat(UTCEventDateTime));

			/** Get CheckSum. */
			CheckSum = SentenceParser.GetCheckSum(rawSentence);

			return string.Format(_SENTENCE, rawSentence, CheckSum);
		}

		public string GetUTCDate()
		{
			return DateTimeConversions.GetDateDeivceFormat(UTCEventDateTime);
		}

		public string GetUTCTime()
		{
			return DateTimeConversions.GetTimeDeivceFormat(UTCEventDateTime);
		}

		#endregion Methods
	}

}
