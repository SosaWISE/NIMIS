namespace SOS.Lib.LaipacAPI.Models
{
	public class CurrentStatusSentence : Sentence
	{
		#region .ctor
	
		public CurrentStatusSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			GeoFence = SentenceArray[++index];
			Panic = SentenceArray[++index];
			Opto1 = SentenceArray[++index];
			Opto2 = SentenceArray[++index];
			Relay2 = SentenceArray[++index];
			Relay1 = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string GeoFence { get; private set; }
		public string Panic { get; private set; }
		public string Opto2 { get; private set; }
		public string Opto1 { get; private set; }
		public string Relay2 { get; private set; }
		public string Relay1 { get; private set; }

		#endregion Properties
	}
}
