using System;
using System.Linq;
using System.Text;
using SOS.Lib.LaipacAPI.ExceptionHandling;

namespace SOS.Lib.LaipacAPI.Helper
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
		/// Given a rawSentence it returns the CheckSum of it.  This is a 2 char Hexidecimal number returned as a string.
		/// </summary>
		/// <param name="rawSentence">string</param>
		/// <returns>string</returns>
		public static string GetCheckSumRawSentence(string rawSentence)
		{
			/** Initialize. */
			byte[] data = Encoding.Unicode.GetBytes(rawSentence);
			//byte[] data = Encoding.Unicode.GetBytes("$GPRMC,235947.000,V,0000.0000,N,00000.0000,E,,,041299,,*");
			byte sum = 0;

			unchecked
			{
				/** Initialize. */
				var isFirst = true;
				foreach (byte b in data)
				{
					/** Skip first item. */
					if (isFirst) { isFirst = false; continue; }

					/** Check that the last charcter is present. */
					if (Encoding.UTF8.GetString(new[] { b }).Equals("*"))
						break;

					sum = (byte)(sum ^ b);
				}
			}

			/** Convert to hex. */
			var hex = new StringBuilder(2);
			hex.AppendFormat("{0:x2}", sum);

			/** Return result. */
			var resultValue = hex.ToString();
			return resultValue.ToUpper();
		}

		/// <summary>
		/// Given a sentence it will return the CheckSUm.
		/// </summary>
		/// <param name="sentence">string</param>
		/// <returns>string</returns>
		public static string GetCheckSum(string sentence)
		{
			/** Initialize. */
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
				throw new LaipacSentenceLengthException(string.Format("The raw sentence is too short: {0}.", rawSentence));
			if (rawSentence.IndexOf('$') != 0)
				throw new LaipacSentenceMissingDollarSign(string.Format("The raw sentence being steralized is missing the '$': {0}.", rawSentence));
			if (rawSentence.IndexOf('*') == -1)
				throw new LaipacSentenceMissingChkSum(string.Format("The raw sentence being steralized is missing the CheckSum: {0}.", rawSentence));

			/** Initialize. */
			var result = rawSentence.Split('*');

			/** Take out the $. */
			result[0] = result[0].Replace("$", "");
			
			/** Return result. */
			return rawSentence.Split('*');
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sUTCDate">string</param>
		/// <param name="sUTCTime">string</param>
		/// <returns>DateTime</returns>
		public static DateTime ConvertStringToDateTime(string sUTCDate, string sUTCTime)
		{
			/** Initialize. */
			DateTime result;
			string hour = sUTCTime.Substring(0, 2);
			string mint = sUTCTime.Substring(2, 2);
			string secd = sUTCTime.Substring(4, 2);
			string day = sUTCDate.Substring(0, 2);
			string mnth = sUTCDate.Substring(2, 2);
			string year = "20" + sUTCDate.Substring(4, 2);

			if(DateTime.TryParse(string.Format("{0}/{1}/{2} {3}:{4}:{5}"
				, mnth, day, year, hour, mint, secd), out result))
				return result;

			/** return default path */
			return DateTime.MinValue;
		}

		#endregion Methods
	}
}