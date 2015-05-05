using NXS.Data;
using System;

namespace NXS.Data.AuthenticationControl
{
	public partial class AC_ActionRequest // AC_ActionRequests
	{
		public int ID { get; set; }
		public string ApplicationId { get; set; }
		public string ActionId { get; set; }
		public int UserId { get; set; }
		public int RequestReasonId { get; set; }
		public string RequestReason { get; set; }
		public string OnBehalfOf { get; set; }
		public string ActionKey { get; set; }
		public DateTime? SignedOn { get; set; }
		public string SignedBy { get; set; }
		public int? DeniedReasonId { get; set; }
		public string DeniedReason { get; set; }
		public DateTime? UsedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class AC_Action // AC_Actions
	{
		public static class MetaData
		{
			public const string CRM_ByPassCredit_RepExceptionID = "CRM_ByPassCredit_RepException";
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
			public const string FundingAdministratorID = "FUNDING_ADMIN";
			public const string HiringManagerID = "HR_MAN";
			public const string InventoryScreenID = "INV";
			public const string NXSConnextCORSID = "NXS_CONNEXT_CORS";
			public const string ScheduleAdminID = "SCHED_MAN";
			public const string SOSCRMID = "SOS_CRM";
			public const string SOSGPSClientID = "SOS_GPS_CLNT";
			public const string SSECmsCORSID = "SSE_CMS_CORS";
			public const string SSEMainPortalAppID = "SSE_MAIN_PORTAL";
			public const string SurveyManagerID = "SURVEY_MAN";
			public const string SwingScreenID = "SWING";
		}
		[IgnorePropertyAttribute(true)] public string ID { get { return ApplicationID; } set { ApplicationID = value; } }
		public string ApplicationID { get; set; }
		public string ApplicationName { get; set; }
		public string ApplicationDesc { get; set; }
		public string WebUrl { get; set; }
	}
	public partial class AC_DeniedReason // AC_DeniedReasons
	{
		public enum IDEnum : int
		{
			Not_Supported = 1,
			Surpassed_Quota = 2,
		}
		public int ID { get; set; }
		public string Name { get; set; }
	}
	public partial class AC_GroupAction // AC_GroupActions
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return UserActionID; } set { UserActionID = value; } }
		public int UserActionID { get; set; }
		public string GroupName { get; set; }
		public string ActionId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class AC_GroupApplication // AC_GroupApplications
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return UserApplicationID; } set { UserApplicationID = value; } }
		public int UserApplicationID { get; set; }
		public string GroupName { get; set; }
		public string ApplicationId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public partial class AC_KeyValue // AC_KeyValues
	{
		public int ID { get; set; }
		public string KeyValue { get; set; }
		public DateTime CreatedOn { get; set; }
	}
	public partial class AC_RequestReason // AC_RequestReasons
	{
		public enum IDEnum : int
		{
			Rep_Exception_By_Pass_Credit = 1,
		}
		public int ID { get; set; }
		public string Name { get; set; }
	}
	public partial class AC_User // AC_Users
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return UserID; } set { UserID = value; } }
		public int UserID { get; set; }
		public int DealerId { get; set; }
		public int? HRUserId { get; set; }
		public string GPEmployeeID { get; set; }
		public Guid? SSID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
}
