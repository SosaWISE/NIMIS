namespace SOS.Lib.LaipacAPI.Models
{
	public class CurrentPositionSentence : Sentence
	{
		#region .ctor

		public CurrentPositionSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[++index];
			UTCTime = SentenceArray[++index];
			Status = SentenceArray[++index];
			Latitude = SentenceArray[++index];
			NSIndicator = SentenceArray[++index];
			Longitude = SentenceArray[++index];
			EWIndicator = SentenceArray[++index];
			Speed = SentenceArray[++index];
			Course = SentenceArray[++index];
			UTCDate = SentenceArray[++index];
			EventCode = SentenceArray[++index];
			BatteryVoltage = SentenceArray[++index];
			CurrentMileage = SentenceArray[++index];
			GPSOnOff = SentenceArray[++index];
			AnalogPort1 = SentenceArray[++index];
			AnalogPort2 = SentenceArray[++index];
		}

		#endregion .ctor

		#region Properties

		public string UnitID { get; private set; }
		public string UTCTime { get; private set; }
		public string Status { get; private set; }
		public string Latitude { get; private set; }
		public string NSIndicator { get; private set; }
		public string Longitude { get; private set; }
		public string EWIndicator { get; private set; }
		public string Speed { get; private set; }
		public string Course { get; private set; }
		public string UTCDate { get; private set; }
		public string EventCode { get; private set; }
		public string BatteryVoltage { get; private set; }
		public string CurrentMileage { get; private set; }
		public string GPSOnOff { get; private set; }
		public string AnalogPort1 { get; private set; }
		public string AnalogPort2 { get; private set; }

		#endregion Properties
	}
}
