namespace SOS.Lib.LaipacAPI.Models
{
	public class LoggedDataSentence : Sentence
	{
		#region .ctor

		public LoggedDataSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			RESPCode = SentenceArray[++index];
			NumberOfDataLogsSet = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string RESPCode { get; private set; }
		public string NumberOfDataLogsSet { get; private set; }

		#endregion Properties
	}
}
