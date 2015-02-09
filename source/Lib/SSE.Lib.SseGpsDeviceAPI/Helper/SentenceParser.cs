using System.Linq;
using System.Text;
using SOS.Data.GpsTracking;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.ExceptionHandling;

namespace SSE.Lib.SseGpsDeviceAPI.Helper
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

		private const int _COMMAND_NAME_START_POS = 1;
		private const int _COMMAND_NAME_LENGTH = 3;

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
				throw new SseGpsDeviceSentenceLengthException(string.Format("The raw sentence is too short: {0}.", rawSentence));
			if (rawSentence.IndexOf('$') != 0)
				throw new SseGpsDeviceSentenceMissingDollarSign(string.Format("The raw sentence being steralized is missing the '$': {0}.", rawSentence));
			if (rawSentence.IndexOf('*') == -1)
				throw new SseGpsDeviceSentenceMissingChkSum(string.Format("The raw sentence being steralized is missing the CheckSum: {0}.", rawSentence));

			/** Initialize. */
			var result = rawSentence.Split('*');

			/** Take out the $. */
			result[0] = result[0].Replace("$", "");

			/** Return result. */
			return result;
		}
			#endregion Methods

		public static CommandDef GetCommandName(string rawSentence)
		{
			/** Get the command name. */
			var commandName = rawSentence.Substring(_COMMAND_NAME_START_POS, _COMMAND_NAME_LENGTH);

			switch (commandName)
			{
				case "BSA":
					return CommandDef.BSA;
				case "BTA":
					return CommandDef.BTA;
				case "ERR":
					return CommandDef.ERR;
				case "EZB":
					return CommandDef.EZB;
				case "FDA":
					return CommandDef.FDA;
				case "IZB":
					return CommandDef.IZB;
				case "LBA":
					return CommandDef.LBA;
				case "PDE":
					return CommandDef.PDE;
				case "SIR":
					return CommandDef.SIR;
				case "SOS":
					return CommandDef.SOS;
				default:
					throw new SseInvalidCommandName(commandName);
			}
		}

		public static string GetCommandMessageNameID(string rawSentence)
		{
			// ** Get the command name.
			var commandName = rawSentence.Substring(_COMMAND_NAME_START_POS, _COMMAND_NAME_LENGTH);

			switch (commandName)
			{
				case "BTA":
					return SS_CommandMessageName.MetaData.BTAID;
				case "ERR":
					return SS_CommandMessageName.MetaData.ERRID;
				case "EZB":
					return SS_CommandMessageName.MetaData.EZBID;
				case "FDA":
					return SS_CommandMessageName.MetaData.FDAID;
				case "IZB":
					return SS_CommandMessageName.MetaData.IZBID;
				case "LBA":
					return SS_CommandMessageName.MetaData.LBAID;
				case "PDE":
					return SS_CommandMessageName.MetaData.PDEID;
				case "SIR":
					return SS_CommandMessageName.MetaData.SIRID;
				case "SOS":
					return SS_CommandMessageName.MetaData.SOSID;
				default:
					throw new SseInvalidCommandName(commandName);
			}
		}

		public static void CheckSumValidation(string sentence, string checkSum)
		{
			// ** Init
			var genCheckSum = GetCheckSum(sentence);

			// ** Check the checksum
			if (!checkSum.Equals(genCheckSum))
			{
				throw new SseInvalidCheckSum(sentence, checkSum, genCheckSum);
			}
		}
	}
}
