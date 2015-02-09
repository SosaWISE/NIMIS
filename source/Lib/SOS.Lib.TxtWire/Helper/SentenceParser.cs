using System;
using System.Text;
using SOS.Lib.TxtWire.ErrorHandling;

namespace SOS.Lib.TxtWire.Helper
{
	[Obsolete("Not used anymore", true)]
	public static class SentenceParser
	{
		#region Methods

		/// <summary>
		/// Given a sentence it returns the CheckSum of it.  This is a 2 char Hexidecimal number returned as a string.
		/// </summary>
		/// <param name="sentence">string</param>
		/// <returns>string</returns>
		public static string GetCheckSum(string sentence)
		{
			/** Initialize. */
			byte[] data = Encoding.Unicode.GetBytes(sentence);
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
		/// Given a raw sentence it strips out the first character and also the * and checksum from the end of the sentence.
		/// It returns an Array of two with first item being the raw sentence the second item the checksum.
		/// </summary>
		/// <param name="rawSentence"></param>
		/// <returns>string[]</returns>
		public static string[] RawSentenceToSentence(string rawSentence)
		{
			/** Validate raw sentence */
			if (rawSentence == null || rawSentence.Length <= 4)
				throw new TxtWireExceptionInvalidLengthExceptions(string.Format("The raw sentence is too short: {0}.", rawSentence));
			if (rawSentence.IndexOf('$') != 0)
				throw new TxtWireExceptionInvalidLengthExceptions(string.Format("The raw sentence being steralized is missing the '$': {0}.", rawSentence));
			if (rawSentence.IndexOf('*') != -1)
				throw new TxtWireExceptionInvalidLengthExceptions(string.Format("The raw sentence being steralized is missing the CheckSum: {0}.", rawSentence));

			/** Initialize. */
			var result = rawSentence.Split('*');

			/** Take out the $. */
			result[0] = result[0].Replace("$", "");
			
			/** Return result. */
			return rawSentence.Split('*');
		}

		#endregion Methods
	}
}
