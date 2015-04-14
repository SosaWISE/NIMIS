using System;
using NXS.Data;

namespace NXS.Data.AuthenticationControl
{
	public partial class AC_Action // AC_Actions
	{
		public static class MetaData
		{
			public const string Hr_Team_EditID = "Hr_Team_Edit";
			public const string Hr_User_EditID = "Hr_User_Edit";
		}
		[IgnorePropertyAttribute(true)] public string ID { get { return ActionID; } set { ActionID = value; } }
		public string ActionID { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
	public partial class AC_Application // AC_Applications
	{
		public static class MetaData
		{
			public const string AdminID = "ADMIN";
			public const string ContractAdministrationID = "CONTRACT_ADMIN";
			public const string FundingAdminID = "FUNDING_ADMIN";
			public const string HiringManagerID = "HR_MAN";
			public const string NXSConnextCORSID = "NXS_CONNEXT_CORS";
			public const string SOSCRMID = "SOS_CRM";
			public const string SOSGPSClientID = "SOS_GPS_CLNT";
			public const string SSECmsCORSID = "SSE_CMS_CORS";
			public const string SSEMainPortalAppID = "SSE_MAIN_PORTAL";
			public const string SurveyManagerID = "SURVEY_MAN";
		}
		[IgnorePropertyAttribute(true)] public string ID { get { return ApplicationID; } set { ApplicationID = value; } }
		public string ApplicationID { get; set; }
		public string ApplicationName { get; set; }
		public string ApplicationDesc { get; set; }
		public string WebUrl { get; set; }
	}
	public partial class AC_KeyValue // AC_KeyValues
	{
		public int ID { get; set; }
		public string KeyValue { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
