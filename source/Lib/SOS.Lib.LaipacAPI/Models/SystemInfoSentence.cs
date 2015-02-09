namespace SOS.Lib.LaipacAPI.Models
{
	public class SystemInfoSentence : Sentence
	{
		/// <summary>
		/// Create a SystemInfoSentence class
		/// </summary>
		/// <param name="rawSentence">string</param>
		public SystemInfoSentence(string rawSentence) : base(rawSentence)
		{
			/** Initialize. */
			var index = 1;
			UnitID = SentenceArray[index];
			FirmwareVersion = SentenceArray[++index];
			SerialNumber = SentenceArray[++index];
			MemorySize = SentenceArray[++index];
		}

		public string UnitID { get; private set; }
		public string FirmwareVersion { get; private set; }
		public string SerialNumber { get; private set; }
		public string MemorySize { get; private set; }
	}
}
