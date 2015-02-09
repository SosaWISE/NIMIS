namespace SOS.Lib.LaipacAPI.Models
{
	public class GPRSParametersSentence : Sentence
	{
		#region .ctor
		
		public GPRSParametersSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			APN = SentenceArray[++index];
			Username = SentenceArray[++index];
			Password = SentenceArray[++index];
			TCPServer = SentenceArray[++index];
			Port = SentenceArray[++index];
			DNS1 = SentenceArray[++index];
			DNS2 = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string APN { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public string TCPServer { get; private set; }
		public string Port { get; private set; }
		public string DNS1 { get; private set; }
		public string DNS2 { get; private set; }

		#endregion Properties
	}
}
