namespace SOS.Lib.LaipacAPI.Models
{
	public class DeleteAllLoggedDataSentence : Sentence
	{
		#region .ctor

		public DeleteAllLoggedDataSentence (string rawSentence) : base(rawSentence)
		{
			/** Initialization. */
			var index = 1;
			UnitID = SentenceArray[index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }

		#endregion Properties
	}
}
