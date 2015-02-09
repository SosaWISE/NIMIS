namespace SOS.Lib.LaipacAPI.Models
{
	public class Sentence
	{
		#region .ctor
		
		public Sentence (string rawSentence)
		{
			/** Initialize. */
			string[] parsedSentence = Helper.SentenceParser.RawSentenceToSentence(rawSentence);

			/** Set properties. */
			SentenceValue = parsedSentence[Helper.SentenceParser.Fields.Sentence];
			SentenceArray = SentenceValue.Split(',');
			Command = SentenceArray[Helper.SentenceParser.Fields.Command];
			ChkSum = parsedSentence[Helper.SentenceParser.Fields.ChkSum];
			ChkSumIsValid = this.ValidateChkSum();
		}

		#endregion .ctor

		#region Member Variables

		public string SentenceValue { get; private set; }
		public string[] SentenceArray { get; private set; }
		public string Command { get; private set; }
		public string ChkSum { get; private set; }
		public bool ChkSumIsValid { get; private set; }

		#endregion Member Variables

		#region Member Functions


		#endregion Member Functions
	}

	public static class SentenceValidator
	{
		public static bool ValidateChkSum(this Sentence oSentence)
		{
			/** Initialize. */
			return Helper.SentenceParser.GetCheckSum(oSentence.SentenceValue).Equals(oSentence.ChkSum);
		}
	}
}
