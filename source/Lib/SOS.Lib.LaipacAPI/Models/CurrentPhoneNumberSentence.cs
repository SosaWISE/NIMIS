namespace SOS.Lib.LaipacAPI.Models
{
	public class CurrentPhoneNumberSentence : Sentence
	{
		#region .ctor

		public CurrentPhoneNumberSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			Phone0 = SentenceArray[++index];
			Phone1 = SentenceArray[++index];
			Phone2 = SentenceArray[++index];
			Phone3 = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string Phone0 { get; private set; }
		public string Phone1 { get; private set; }
		public string Phone2 { get; private set; }
		public string Phone3 { get; private set; }
		
		#endregion Properties
	}
}
