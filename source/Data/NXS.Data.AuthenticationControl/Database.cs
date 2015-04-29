using System;
using NXS.Data;

namespace NXS.Data.AuthenticationControl
{
	public partial class AuthControlDb : Database<AuthControlDb>
	{
		public AC_ActionRequestTable AC_ActionRequests { get; set; }
		public AC_ActionTable AC_Actions { get; set; }
		public AC_ApplicationTable AC_Applications { get; set; }
		public AC_DeniedReasonTable AC_DeniedReasons { get; set; }
		public AC_GroupActionTable AC_GroupActions { get; set; }
		public AC_GroupApplicationTable AC_GroupApplications { get; set; }
		public AC_KeyValueTable AC_KeyValues { get; set; }
		public AC_RequestReasonTable AC_RequestReasons { get; set; }
		public AC_UserTable AC_Users { get; set; }

		public partial class AC_ActionRequestTable : Table<AC_ActionRequest, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_ActionRequestTable(AuthControlDb db) : base(db, "AcAR", "[dbo].[AC_ActionRequests]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string ApplicationId { get { return _alias + "[ApplicationId]"; } }
			public string ActionId { get { return _alias + "[ActionId]"; } }
			public string UserId { get { return _alias + "[UserId]"; } }
			public string RequestReasonId { get { return _alias + "[RequestReasonId]"; } }
			public string RequestReason { get { return _alias + "[RequestReason]"; } }
			public string OnBehalfOf { get { return _alias + "[OnBehalfOf]"; } }
			public string ActionKey { get { return _alias + "[ActionKey]"; } }
			public string SignedOn { get { return _alias + "[SignedOn]"; } }
			public string SignedBy { get { return _alias + "[SignedBy]"; } }
			public string DeniedReasonId { get { return _alias + "[DeniedReasonId]"; } }
			public string DeniedReason { get { return _alias + "[DeniedReason]"; } }
			public string UsedOn { get { return _alias + "[UsedOn]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class AC_ActionTable : Table<AC_Action, string>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_ActionTable(AuthControlDb db) : base(db, "AcA", "[dbo].[AC_Actions]", "ActionID", "varchar", false) { }
			public string ActionID { get { return _alias + "[ActionID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
		}
		public partial class AC_ApplicationTable : Table<AC_Application, string>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_ApplicationTable(AuthControlDb db) : base(db, "AcAp", "[dbo].[AC_Applications]", "ApplicationID", "varchar", false) { }
			public string ApplicationID { get { return _alias + "[ApplicationID]"; } }
			public string ApplicationName { get { return _alias + "[ApplicationName]"; } }
			public string ApplicationDesc { get { return _alias + "[ApplicationDesc]"; } }
			public string WebUrl { get { return _alias + "[WebUrl]"; } }
		}
		public partial class AC_DeniedReasonTable : Table<AC_DeniedReason, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_DeniedReasonTable(AuthControlDb db) : base(db, "AcDR", "[dbo].[AC_DeniedReasons]", "ID", "int", false) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
		}
		public partial class AC_GroupActionTable : Table<AC_GroupAction, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_GroupActionTable(AuthControlDb db) : base(db, "AcGA", "[dbo].[AC_GroupActions]", "UserActionID", "int", true) { }
			public string UserActionID { get { return _alias + "[UserActionID]"; } }
			public string GroupName { get { return _alias + "[GroupName]"; } }
			public string ActionId { get { return _alias + "[ActionId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class AC_GroupApplicationTable : Table<AC_GroupApplication, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_GroupApplicationTable(AuthControlDb db) : base(db, "AcGrAp", "[dbo].[AC_GroupApplications]", "UserApplicationID", "int", true) { }
			public string UserApplicationID { get { return _alias + "[UserApplicationID]"; } }
			public string GroupName { get { return _alias + "[GroupName]"; } }
			public string ApplicationId { get { return _alias + "[ApplicationId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}
		public partial class AC_KeyValueTable : Table<AC_KeyValue, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_KeyValueTable(AuthControlDb db) : base(db, "AcKV", "[dbo].[AC_KeyValues]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string KeyValue { get { return _alias + "[KeyValue]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}
		public partial class AC_RequestReasonTable : Table<AC_RequestReason, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_RequestReasonTable(AuthControlDb db) : base(db, "AcRR", "[dbo].[AC_RequestReasons]", "ID", "int", false) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
		}
		public partial class AC_UserTable : Table<AC_User, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_UserTable(AuthControlDb db) : base(db, "AcU", "[dbo].[AC_Users]", "UserID", "int", true) { }
			public string UserID { get { return _alias + "[UserID]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string HRUserId { get { return _alias + "[HRUserId]"; } }
			public string GPEmployeeID { get { return _alias + "[GPEmployeeID]"; } }
			public string SSID { get { return _alias + "[SSID]"; } }
			public string Username { get { return _alias + "[Username]"; } }
			public string Password { get { return _alias + "[Password]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
		}

	}
}
