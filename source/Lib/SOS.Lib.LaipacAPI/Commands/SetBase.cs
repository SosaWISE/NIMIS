using SOS.Lib.LaipacAPI.ExceptionHandling;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class SetBase
	{
		#region Commands
		
		public const string AVCFG = "AVCFG,{0}";
		public const string AVCFG_SET_GPRS_PARAMETERES = "AVCFG, {PSW}, {c}, {APN}, {Username}, {Password}, {TCP Server}, {Port}, {DNS 1}, {DNS 2}";

		internal const string INVALID_LENGTH_MSG =
	"The following '{0}' sentence component '{1}' of {2} length {3} is (length:{4}) invalid.";

		#endregion Commands

		#region Methods

		public static string GetRequestWrapper(string sMessage)
		{
			return string.Format("${0}*{1}", sMessage, Helper.SentenceParser.GetCheckSum(sMessage));
		}

		#region Validation Functions

		public static void ValidateFixedLength(string sentenceName, string componentName, string value, short length)
		{
			if (value.Length != length) throw new LaipacSentenceLengthException(string.Format(INVALID_LENGTH_MSG
				, sentenceName, componentName, "fixed", length, value.Length));
		}

		#endregion Validation Functions

		#endregion Methods
	}
}
