using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientRequestSIR : ClientRequest
	{
		#region .ctor

		public ClientRequestSIR() : base(CommandDef.SIR)
		{
		}

		#endregion .ctor

		#region Contants

		//private const string _SENTENCE_RAW = "{AccountID},{Password}";
		private const string _SENTENCE_RAW = "?";
		private const string _SENTENCE = "SIR,[DATA_ENVELOP]";
		//private const int _MAX_ACCOUNTID_LENGTH = 8;
		//private const int _MAX_PASSWORD_LENGTH = 8;

		#endregion Contants

		#region Member Variables

		//public string AccountID { get; private set; }
		//public string Password { get; private set; }

		#endregion Member Variables

		#region Methods

		private void ValidateProperties()
		{
			//if (string.IsNullOrEmpty(AccountID)) throw new ClientRequestMissingProperty("AccountID");
			//if (AccountID.Length != _MAX_ACCOUNTID_LENGTH) throw new ClientRequestLengthProperty("AccountID", _MAX_ACCOUNTID_LENGTH, AccountID.Length);

			//if (string.IsNullOrEmpty(Password)) throw new ClientRequestMissingProperty("Password");
			//if (Password.Length != _MAX_PASSWORD_LENGTH) throw new ClientRequestLengthProperty("Password", _MAX_PASSWORD_LENGTH, Password.Length);
		}

		protected string GetSentenceRaw()
		{
			/** Inititalize. */
			//var result = _SENTENCE_RAW.Replace("{AccountID}", AccountID);
			//result = result.Replace("{Password}", Password);
			var result = _SENTENCE_RAW;

			/** Return result. */
			return result;
		}

		public string GetSentence()
		{
			/** Validate Properties. */
			ValidateProperties();

			/** Build sentence raw. */
			var rawSentence = _SENTENCE.Replace("[DATA_ENVELOP]", GetSentenceRaw());

			CheckSumSet(SentenceParser.GetCheckSum(rawSentence));

			/** Return result. */
			return string.Format("${0}*{1}", rawSentence, CheckSum);
		}

		#endregion Methods
	}
}
