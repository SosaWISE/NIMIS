using SOS.Lib.LaipacAPI.ExceptionHandling;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class Requests
	{
		#region .ctor
		public Requests(string sPassword)
		{
			if (sPassword.Length != 8)
				throw new LaipacParameterFixedLengthException(string.Format(INVALID_LENGTH_MSG
					, "AVREQ", "PSW", FIXED, 8, sPassword.Length));
			Password = sPassword;
		}

		#endregion .ctor

		#region Member Properties

		public string Password { get; private set; }

		#endregion Member Properties

		#region Commands

		public const string AVREQ_NO_PASSWORD = "AVREQ,{0}";
		public const string AVREQ = "AVREQ,{0},{1}";
		internal const string SYSTEM_INFO_VALIDATION_MESSAGE_1 = "Request System Info requires a command of {0} byte(s).";
		public const string REQ_LOGGED_DATA_COMMAND = "AVREQ,{0},0";
		public const string REQ_CURRENT_POS_COMMAND = "AVREQ,{0},1";
		public const string REQ_CURRENT_STA_COMMAND = "AVREQ,{0},2";
		public const string REQ_CURRENT_SET_COMMAND = "AVREQ,{0},3";
		public const string REQ_DEL_ALL_DAT_COMMAND = "AVREQ,{0},8";
		public const string REQ_CURRENT_PHN_COMMAND = "AVREQ,{0},9";
		public const string REQ_FEATURE_FLAG_CONFIG = "AVCFG,{0},{1}";
		public const string REQ_ECHK_SENTNC_COMMAND = "ECHK,{0},{1}";
		public const string REQ_EAVACK_SENT_COMMAND = "EAVACK,{0},{1}";
		public const string REQ_EAVGOF_GEO2_COMMAND = "EAVGOF,{PWD},2,{Report Mode}";
		public const string REQ_EAVGOF_GEO3_COMMAND = "EAVGOF,{PWD},3,{Total}{GEOFENCE_ITEM}";
		public const string GEOFENCE_ITEM = ",{Geo-fencei},{Report Modei},{Latitudei1},{Longtitudei1},{Latitudei2},{Longtitudei2}";
		public const string REQ_EAVGOF_GEO4_COMMAND = "EAVGOF,{PWD},4,{Geo-fencei}";
		public const string REQ_EAVGOF_GEO5_COMMAND = "EAVGOF,{PWD},5,{Total}{,Geo-fencei1,…,Geo-fenceik},{Report Mode}";
		public const string REQ_EAVGOF_GEO6_COMMAND = "EAVGOF,{PWD},6,{Geo-fencei},{Report Modei},{Latitudei1},{Longtitudei1},{Latitudei2},{Longtitudei2}";
		
		internal const string VARIABLE = "Variable";
		internal const string FIXED = "Fixed";
		internal const string AVSYS = "AVSYS";
		internal const string AVALL = "AVALL";
		internal const string INVALID_LENGTH_MSG =
			"The following '{0}' sentence component '{1}' of {2} length {3} is (length:{4}) invalid.";
		internal const string DEFAULT_BLANK_PASSWRD = "XXXXXXXX";

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
				, sentenceName, componentName, "fixed", length, value.Length ));
		}

		#endregion Validation Functions

		#endregion Methods
	}
}
