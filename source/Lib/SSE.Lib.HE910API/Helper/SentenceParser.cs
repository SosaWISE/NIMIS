using System.Linq;
using System.Text;
using SSE.Lib.HE910API.ExceptionHandling;

namespace SSE.Lib.HE910API.Helper
{
	public static class SentenceParser
	{
		#region Properties
		public static class Fields
		{
			public static int Command;
			public static int Sentence;
			public static int ChkSum = Sentence + 1;
		}
		#endregion Properties

		#region Methods

		/// <summary>
		/// Given a raw sentence it returns the CheckSum of it.  This is a 2 char Hexidecimal number returnd as a string.
		/// </summary>
		/// <param name="rawSentence">string</param>
		/// <returns>string</returns>
		public static string GetCheckSumRawSentence(string rawSentence)
		{
			/** Initialize. */
			byte[] data = Encoding.Unicode.GetBytes(rawSentence);
			byte sum = 0;

			unchecked
			{
				/** Initialize. */
				var isFirst = true;
				foreach (var b in data)
				{
					/** Skip first item. */
					if (isFirst)
					{
						isFirst = false;
						continue;
					}

					if (Encoding.UTF8.GetString(new[] {b}).Equals("*"))
						break;

					/** Sum the bytes. */
					sum = (byte) (sum ^ b);
				}
			}

			/** Convert to hex. */
			var hex = new StringBuilder(2);
			hex.AppendFormat("{0:x2}", sum);

			/** Return result. */
			var resultValue = hex.ToString();
			return resultValue.ToUpper();
		}

		public static string GetCheckSum(string sentence)
		{
			/** Inititalize. */
			byte[] data = Encoding.Unicode.GetBytes(sentence);
			byte sum = 0;

			unchecked
			{
				/** Initialize. */
				sum = data.Aggregate(sum, (current, b) => (byte) (current ^ b));
			}

			/** Convert to hex. */
			var hex = new StringBuilder(2);
			hex.AppendFormat("{0:x2}", sum);

			/** Return result. */
			var resultValue = hex.ToString();
			return resultValue.ToUpper();
		}

		/// <summary>
		/// Given a raw sentence it strips out the first character and also the * and checksum from the end of the sentence.
		/// It returns an Array of two with first item being the raw sentence the second item the checksum.
		/// </summary>
		/// <param name="rawSentence"></param>
		/// <returns>string[]</returns>
		public static string[] RawSentenceToSentence(string rawSentence)
		{
			/** Validate raw sentence */
			if (rawSentence == null || rawSentence.Length <= 4)
				throw new HE910SentenceLengthException(string.Format("The raw sentence is too short: {0}.", rawSentence));
			if (rawSentence.IndexOf('$') != 0)
				throw new HE910SentenceMissingDollarSign(string.Format("The raw sentence being steralized is missing the '$': {0}.", rawSentence));
			if (rawSentence.IndexOf('*') == -1)
				throw new HE910SentenceMissingChkSum(string.Format("The raw sentence being steralized is missing the CheckSum: {0}.", rawSentence));

			/** Initialize. */
			var result = rawSentence.Split('*');

			/** Take out the $. */
			result[0] = result[0].Replace("$", "");

			/** Return result. */
			return result;
		}
			#endregion Methods
	}
}
