namespace SOS.Lib.LaipacAPI.Models
{
	public class CurrentSettingsSentence : Sentence
	{
		#region .ctor
		
		public CurrentSettingsSentence(string rawSentence) : base(rawSentence)
		{			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			LogTimeInterval = SentenceArray[++index];
			LogDistInterval = SentenceArray[++index];
			LogEventMask = SentenceArray[++index];
			ReportTimeInterval = SentenceArray[++index];
			DistInterval = SentenceArray[++index];
			ReportEventMask = SentenceArray[++index];
			GeoCentLat1 = SentenceArray[++index];
			GeoCentLon1 = SentenceArray[++index];
			GeoDeviation1 = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string LogTimeInterval { get; private set; }
		public string LogDistInterval { get; private set; }
		public string LogEventMask { get; private set; }
		public string ReportTimeInterval { get; private set; }
		public string DistInterval { get; private set; }
		public string ReportEventMask { get; private set; }
		public string GeoCentLat1 { get; private set; }
		public string GeoCentLon1 { get; private set; }
		public string GeoDeviation1 { get; private set; }

		#endregion Properties
	}
}
