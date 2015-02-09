


using System;
using System.ComponentModel;
using System.Linq;
using SubSonic;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using SOS.Data;

namespace SOS.Data.AuthenticationControl
{
	/// <summary>
	/// Strongly-typed collection for the AC_Action class.
	/// </summary>
	[DataContract]
	public partial class AC_ActionCollection : ActiveList<AC_Action, AC_ActionCollection>
	{
		public static AC_ActionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_ActionCollection result = new AC_ActionCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_Action item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_Actions table.
	/// </summary>
	[DataContract]
	public partial class AC_Action : ActiveRecord<AC_Action>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string Hr_Team_EditID = "Hr_Team_Edit";
			[EnumMember()] public const string Hr_User_EditID = "Hr_User_Edit";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_Action()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_Actions", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarActionID = new TableSchema.TableColumn(schema);
				colvarActionID.ColumnName = "ActionID";
				colvarActionID.DataType = DbType.AnsiString;
				colvarActionID.MaxLength = 50;
				colvarActionID.AutoIncrement = false;
				colvarActionID.IsNullable = false;
				colvarActionID.IsPrimaryKey = true;
				colvarActionID.IsForeignKey = false;
				colvarActionID.IsReadOnly = false;
				colvarActionID.DefaultSetting = @"";
				colvarActionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActionID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_Actions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_Action LoadFrom(AC_Action item)
		{
			AC_Action result = new AC_Action();
			if (item.ActionID != default(string)) {
				result.LoadByKey(item.ActionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string ActionID {
			get { return GetColumnValue<string>(Columns.ActionID); }
			set {
				SetColumnValue(Columns.ActionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ActionID));
			}
		}
		[DataMember]
		public string Name {
			get { return GetColumnValue<string>(Columns.Name); }
			set {
				SetColumnValue(Columns.Name, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Name));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ActionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ActionID = @"ActionID";
			public static readonly string Name = @"Name";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ActionID; }
		}
		*/

		#region Foreign Collections

		private AC_GroupActionCollection _AC_GroupActionsCol;
		//Relationship: FK_ActionId_AC_GroupActions_AC_Actions
		public AC_GroupActionCollection AC_GroupActionsCol
		{
			get
			{
				if(_AC_GroupActionsCol == null) {
					_AC_GroupActionsCol = new AC_GroupActionCollection();
					_AC_GroupActionsCol.LoadAndCloseReader(AC_GroupAction.Query()
						.WHERE(AC_GroupAction.Columns.ActionId, ActionID).ExecuteReader());
				}
				return _AC_GroupActionsCol;
			}
		}

		private AC_UserActionCollection _AC_UserActionsCol;
		//Relationship: FK_ActionId_AC_UserActions_AC_Actions
		public AC_UserActionCollection AC_UserActionsCol
		{
			get
			{
				if(_AC_UserActionsCol == null) {
					_AC_UserActionsCol = new AC_UserActionCollection();
					_AC_UserActionsCol.LoadAndCloseReader(AC_UserAction.Query()
						.WHERE(AC_UserAction.Columns.ActionId, ActionID).ExecuteReader());
				}
				return _AC_UserActionsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the AC_Application class.
	/// </summary>
	[DataContract]
	public partial class AC_ApplicationCollection : ActiveList<AC_Application, AC_ApplicationCollection>
	{
		public static AC_ApplicationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_ApplicationCollection result = new AC_ApplicationCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_Application item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_Applications table.
	/// </summary>
	[DataContract]
	public partial class AC_Application : ActiveRecord<AC_Application>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string HiringManagerID = "HR_MAN";
			[EnumMember()] public const string NXSConnextCORSID = "NXS_CONNEXT_CORS";
			[EnumMember()] public const string SOSCRMID = "SOS_CRM";
			[EnumMember()] public const string SOSGPSClientID = "SOS_GPS_CLNT";
			[EnumMember()] public const string SSECmsCORSID = "SSE_CMS_CORS";
			[EnumMember()] public const string SSEMainPortalAppID = "SSE_MAIN_PORTAL";
			[EnumMember()] public const string SurveyManagerID = "SURVEY_MAN";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_Application()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_Applications", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarApplicationID = new TableSchema.TableColumn(schema);
				colvarApplicationID.ColumnName = "ApplicationID";
				colvarApplicationID.DataType = DbType.AnsiString;
				colvarApplicationID.MaxLength = 50;
				colvarApplicationID.AutoIncrement = false;
				colvarApplicationID.IsNullable = false;
				colvarApplicationID.IsPrimaryKey = true;
				colvarApplicationID.IsForeignKey = false;
				colvarApplicationID.IsReadOnly = false;
				colvarApplicationID.DefaultSetting = @"";
				colvarApplicationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApplicationID);

				TableSchema.TableColumn colvarApplicationName = new TableSchema.TableColumn(schema);
				colvarApplicationName.ColumnName = "ApplicationName";
				colvarApplicationName.DataType = DbType.AnsiString;
				colvarApplicationName.MaxLength = 100;
				colvarApplicationName.AutoIncrement = false;
				colvarApplicationName.IsNullable = false;
				colvarApplicationName.IsPrimaryKey = false;
				colvarApplicationName.IsForeignKey = false;
				colvarApplicationName.IsReadOnly = false;
				colvarApplicationName.DefaultSetting = @"";
				colvarApplicationName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApplicationName);

				TableSchema.TableColumn colvarApplicationDesc = new TableSchema.TableColumn(schema);
				colvarApplicationDesc.ColumnName = "ApplicationDesc";
				colvarApplicationDesc.DataType = DbType.AnsiString;
				colvarApplicationDesc.MaxLength = -1;
				colvarApplicationDesc.AutoIncrement = false;
				colvarApplicationDesc.IsNullable = true;
				colvarApplicationDesc.IsPrimaryKey = false;
				colvarApplicationDesc.IsForeignKey = false;
				colvarApplicationDesc.IsReadOnly = false;
				colvarApplicationDesc.DefaultSetting = @"";
				colvarApplicationDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApplicationDesc);

				TableSchema.TableColumn colvarWebUrl = new TableSchema.TableColumn(schema);
				colvarWebUrl.ColumnName = "WebUrl";
				colvarWebUrl.DataType = DbType.String;
				colvarWebUrl.MaxLength = 1000;
				colvarWebUrl.AutoIncrement = false;
				colvarWebUrl.IsNullable = true;
				colvarWebUrl.IsPrimaryKey = false;
				colvarWebUrl.IsForeignKey = false;
				colvarWebUrl.IsReadOnly = false;
				colvarWebUrl.DefaultSetting = @"";
				colvarWebUrl.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWebUrl);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_Applications",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_Application LoadFrom(AC_Application item)
		{
			AC_Application result = new AC_Application();
			if (item.ApplicationID != default(string)) {
				result.LoadByKey(item.ApplicationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string ApplicationID {
			get { return GetColumnValue<string>(Columns.ApplicationID); }
			set {
				SetColumnValue(Columns.ApplicationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationID));
			}
		}
		[DataMember]
		public string ApplicationName {
			get { return GetColumnValue<string>(Columns.ApplicationName); }
			set {
				SetColumnValue(Columns.ApplicationName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationName));
			}
		}
		[DataMember]
		public string ApplicationDesc {
			get { return GetColumnValue<string>(Columns.ApplicationDesc); }
			set {
				SetColumnValue(Columns.ApplicationDesc, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationDesc));
			}
		}
		[DataMember]
		public string WebUrl {
			get { return GetColumnValue<string>(Columns.WebUrl); }
			set {
				SetColumnValue(Columns.WebUrl, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WebUrl));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return ApplicationName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ApplicationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ApplicationNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ApplicationDescColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn WebUrlColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ApplicationID = @"ApplicationID";
			public static readonly string ApplicationName = @"ApplicationName";
			public static readonly string ApplicationDesc = @"ApplicationDesc";
			public static readonly string WebUrl = @"WebUrl";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ApplicationID; }
		}
		*/

		#region Foreign Collections

		private AC_SessionCollection _AC_SessionsCol;
		//Relationship: FK_AC_Sessions_AC_Applications
		public AC_SessionCollection AC_SessionsCol
		{
			get
			{
				if(_AC_SessionsCol == null) {
					_AC_SessionsCol = new AC_SessionCollection();
					_AC_SessionsCol.LoadAndCloseReader(AC_Session.Query()
						.WHERE(AC_Session.Columns.ApplicationId, ApplicationID).ExecuteReader());
				}
				return _AC_SessionsCol;
			}
		}

		private AC_UserACLCollection _AC_UserACLsCol;
		//Relationship: FK_AC_UserACLs_AC_Applications
		public AC_UserACLCollection AC_UserACLsCol
		{
			get
			{
				if(_AC_UserACLsCol == null) {
					_AC_UserACLsCol = new AC_UserACLCollection();
					_AC_UserACLsCol.LoadAndCloseReader(AC_UserACL.Query()
						.WHERE(AC_UserACL.Columns.ApplicationId, ApplicationID).ExecuteReader());
				}
				return _AC_UserACLsCol;
			}
		}

		private AC_GroupApplicationCollection _AC_GroupApplicationsCol;
		//Relationship: FK_ApplicationId_AC_GroupApplications_AC_Applications
		public AC_GroupApplicationCollection AC_GroupApplicationsCol
		{
			get
			{
				if(_AC_GroupApplicationsCol == null) {
					_AC_GroupApplicationsCol = new AC_GroupApplicationCollection();
					_AC_GroupApplicationsCol.LoadAndCloseReader(AC_GroupApplication.Query()
						.WHERE(AC_GroupApplication.Columns.ApplicationId, ApplicationID).ExecuteReader());
				}
				return _AC_GroupApplicationsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the AC_Authentication class.
	/// </summary>
	[DataContract]
	public partial class AC_AuthenticationCollection : ActiveList<AC_Authentication, AC_AuthenticationCollection>
	{
		public static AC_AuthenticationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_AuthenticationCollection result = new AC_AuthenticationCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_Authentication item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_Authentications table.
	/// </summary>
	[DataContract]
	public partial class AC_Authentication : ActiveRecord<AC_Authentication>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_Authentication()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_Authentications", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAuthenticationID = new TableSchema.TableColumn(schema);
				colvarAuthenticationID.ColumnName = "AuthenticationID";
				colvarAuthenticationID.DataType = DbType.Int64;
				colvarAuthenticationID.MaxLength = 0;
				colvarAuthenticationID.AutoIncrement = true;
				colvarAuthenticationID.IsNullable = false;
				colvarAuthenticationID.IsPrimaryKey = true;
				colvarAuthenticationID.IsForeignKey = false;
				colvarAuthenticationID.IsReadOnly = false;
				colvarAuthenticationID.DefaultSetting = @"";
				colvarAuthenticationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAuthenticationID);

				TableSchema.TableColumn colvarSessionId = new TableSchema.TableColumn(schema);
				colvarSessionId.ColumnName = "SessionId";
				colvarSessionId.DataType = DbType.Int64;
				colvarSessionId.MaxLength = 0;
				colvarSessionId.AutoIncrement = false;
				colvarSessionId.IsNullable = false;
				colvarSessionId.IsPrimaryKey = false;
				colvarSessionId.IsForeignKey = true;
				colvarSessionId.IsReadOnly = false;
				colvarSessionId.DefaultSetting = @"";
				colvarSessionId.ForeignKeyTableName = "AC_Sessions";
				schema.Columns.Add(colvarSessionId);

				TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
				colvarUserId.ColumnName = "UserId";
				colvarUserId.DataType = DbType.Int32;
				colvarUserId.MaxLength = 0;
				colvarUserId.AutoIncrement = false;
				colvarUserId.IsNullable = true;
				colvarUserId.IsPrimaryKey = false;
				colvarUserId.IsForeignKey = true;
				colvarUserId.IsReadOnly = false;
				colvarUserId.DefaultSetting = @"";
				colvarUserId.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarUserId);

				TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
				colvarUsername.ColumnName = "Username";
				colvarUsername.DataType = DbType.String;
				colvarUsername.MaxLength = 50;
				colvarUsername.AutoIncrement = false;
				colvarUsername.IsNullable = false;
				colvarUsername.IsPrimaryKey = false;
				colvarUsername.IsForeignKey = false;
				colvarUsername.IsReadOnly = false;
				colvarUsername.DefaultSetting = @"";
				colvarUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsername);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 20;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarIsSuccessful = new TableSchema.TableColumn(schema);
				colvarIsSuccessful.ColumnName = "IsSuccessful";
				colvarIsSuccessful.DataType = DbType.Boolean;
				colvarIsSuccessful.MaxLength = 0;
				colvarIsSuccessful.AutoIncrement = false;
				colvarIsSuccessful.IsNullable = false;
				colvarIsSuccessful.IsPrimaryKey = false;
				colvarIsSuccessful.IsForeignKey = false;
				colvarIsSuccessful.IsReadOnly = false;
				colvarIsSuccessful.DefaultSetting = @"((1))";
				colvarIsSuccessful.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsSuccessful);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_Authentications",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_Authentication LoadFrom(AC_Authentication item)
		{
			AC_Authentication result = new AC_Authentication();
			if (item.AuthenticationID != default(long)) {
				result.LoadByKey(item.AuthenticationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AuthenticationID {
			get { return GetColumnValue<long>(Columns.AuthenticationID); }
			set {
				SetColumnValue(Columns.AuthenticationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AuthenticationID));
			}
		}
		[DataMember]
		public long SessionId {
			get { return GetColumnValue<long>(Columns.SessionId); }
			set {
				SetColumnValue(Columns.SessionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SessionId));
			}
		}
		[DataMember]
		public int? UserId {
			get { return GetColumnValue<int?>(Columns.UserId); }
			set {
				SetColumnValue(Columns.UserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserId));
			}
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set {
				SetColumnValue(Columns.Username, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Username));
			}
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set {
				SetColumnValue(Columns.Password, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Password));
			}
		}
		[DataMember]
		public bool IsSuccessful {
			get { return GetColumnValue<bool>(Columns.IsSuccessful); }
			set {
				SetColumnValue(Columns.IsSuccessful, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsSuccessful));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Session _Session;
		//Relationship: FK_AC_Authentications_AC_Sessions
		public AC_Session Session
		{
			get
			{
				if(_Session == null) {
					_Session = AC_Session.FetchByID(this.SessionId);
				}
				return _Session;
			}
			set
			{
				SetColumnValue("SessionId", value.SessionID);
				_Session = value;
			}
		}

		private AC_User _User;
		//Relationship: FK_AC_Authentications_AC_Users
		public AC_User User
		{
			get
			{
				if(_User == null) {
					_User = AC_User.FetchByID(this.UserId);
				}
				return _User;
			}
			set
			{
				SetColumnValue("UserId", value.UserID);
				_User = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AuthenticationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AuthenticationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SessionIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UserIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsSuccessfulColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AuthenticationID = @"AuthenticationID";
			public static readonly string SessionId = @"SessionId";
			public static readonly string UserId = @"UserId";
			public static readonly string Username = @"Username";
			public static readonly string Password = @"Password";
			public static readonly string IsSuccessful = @"IsSuccessful";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AuthenticationID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the AC_GroupAction class.
	/// </summary>
	[DataContract]
	public partial class AC_GroupActionCollection : ActiveList<AC_GroupAction, AC_GroupActionCollection>
	{
		public static AC_GroupActionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_GroupActionCollection result = new AC_GroupActionCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_GroupAction item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_GroupActions table.
	/// </summary>
	[DataContract]
	public partial class AC_GroupAction : ActiveRecord<AC_GroupAction>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_GroupAction()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_GroupActions", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserActionID = new TableSchema.TableColumn(schema);
				colvarUserActionID.ColumnName = "UserActionID";
				colvarUserActionID.DataType = DbType.Int32;
				colvarUserActionID.MaxLength = 0;
				colvarUserActionID.AutoIncrement = true;
				colvarUserActionID.IsNullable = false;
				colvarUserActionID.IsPrimaryKey = true;
				colvarUserActionID.IsForeignKey = false;
				colvarUserActionID.IsReadOnly = false;
				colvarUserActionID.DefaultSetting = @"";
				colvarUserActionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserActionID);

				TableSchema.TableColumn colvarGroupName = new TableSchema.TableColumn(schema);
				colvarGroupName.ColumnName = "GroupName";
				colvarGroupName.DataType = DbType.String;
				colvarGroupName.MaxLength = 100;
				colvarGroupName.AutoIncrement = false;
				colvarGroupName.IsNullable = false;
				colvarGroupName.IsPrimaryKey = false;
				colvarGroupName.IsForeignKey = false;
				colvarGroupName.IsReadOnly = false;
				colvarGroupName.DefaultSetting = @"";
				colvarGroupName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupName);

				TableSchema.TableColumn colvarActionId = new TableSchema.TableColumn(schema);
				colvarActionId.ColumnName = "ActionId";
				colvarActionId.DataType = DbType.AnsiString;
				colvarActionId.MaxLength = 50;
				colvarActionId.AutoIncrement = false;
				colvarActionId.IsNullable = false;
				colvarActionId.IsPrimaryKey = false;
				colvarActionId.IsForeignKey = true;
				colvarActionId.IsReadOnly = false;
				colvarActionId.DefaultSetting = @"";
				colvarActionId.ForeignKeyTableName = "AC_Actions";
				schema.Columns.Add(colvarActionId);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.Int32;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = true;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.Int32;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = true;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarModifiedBy);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"(getdate())";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_GroupActions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_GroupAction LoadFrom(AC_GroupAction item)
		{
			AC_GroupAction result = new AC_GroupAction();
			if (item.UserActionID != default(int)) {
				result.LoadByKey(item.UserActionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int UserActionID {
			get { return GetColumnValue<int>(Columns.UserActionID); }
			set {
				SetColumnValue(Columns.UserActionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserActionID));
			}
		}
		[DataMember]
		public string GroupName {
			get { return GetColumnValue<string>(Columns.GroupName); }
			set {
				SetColumnValue(Columns.GroupName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GroupName));
			}
		}
		[DataMember]
		public string ActionId {
			get { return GetColumnValue<string>(Columns.ActionId); }
			set {
				SetColumnValue(Columns.ActionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ActionId));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public int CreatedBy {
			get { return GetColumnValue<int>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public int ModifiedBy {
			get { return GetColumnValue<int>(Columns.ModifiedBy); }
			set {
				SetColumnValue(Columns.ModifiedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedBy));
			}
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Action _Action;
		//Relationship: FK_ActionId_AC_GroupActions_AC_Actions
		public AC_Action Action
		{
			get
			{
				if(_Action == null) {
					_Action = AC_Action.FetchByID(this.ActionId);
				}
				return _Action;
			}
			set
			{
				SetColumnValue("ActionId", value.ActionID);
				_Action = value;
			}
		}

		private AC_User _CreatedByValue;
		//Relationship: FK_CreatedBy_AC_GroupActions_AC_Users
		public AC_User CreatedByValue
		{
			get
			{
				if(_CreatedByValue == null) {
					_CreatedByValue = AC_User.FetchByID(this.CreatedBy);
				}
				return _CreatedByValue;
			}
			set
			{
				SetColumnValue("CreatedBy", value.UserID);
				_CreatedByValue = value;
			}
		}

		private AC_User _ModifiedByValue;
		//Relationship: FK_ModifiedBy_AC_GroupActions_AC_Users
		public AC_User ModifiedByValue
		{
			get
			{
				if(_ModifiedByValue == null) {
					_ModifiedByValue = AC_User.FetchByID(this.ModifiedBy);
				}
				return _ModifiedByValue;
			}
			set
			{
				SetColumnValue("ModifiedBy", value.UserID);
				_ModifiedByValue = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return GroupName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserActionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GroupNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ActionIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UserActionID = @"UserActionID";
			public static readonly string GroupName = @"GroupName";
			public static readonly string ActionId = @"ActionId";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UserActionID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the AC_GroupApplication class.
	/// </summary>
	[DataContract]
	public partial class AC_GroupApplicationCollection : ActiveList<AC_GroupApplication, AC_GroupApplicationCollection>
	{
		public static AC_GroupApplicationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_GroupApplicationCollection result = new AC_GroupApplicationCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_GroupApplication item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_GroupApplications table.
	/// </summary>
	[DataContract]
	public partial class AC_GroupApplication : ActiveRecord<AC_GroupApplication>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_GroupApplication()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_GroupApplications", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserApplicationID = new TableSchema.TableColumn(schema);
				colvarUserApplicationID.ColumnName = "UserApplicationID";
				colvarUserApplicationID.DataType = DbType.Int32;
				colvarUserApplicationID.MaxLength = 0;
				colvarUserApplicationID.AutoIncrement = true;
				colvarUserApplicationID.IsNullable = false;
				colvarUserApplicationID.IsPrimaryKey = true;
				colvarUserApplicationID.IsForeignKey = false;
				colvarUserApplicationID.IsReadOnly = false;
				colvarUserApplicationID.DefaultSetting = @"";
				colvarUserApplicationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserApplicationID);

				TableSchema.TableColumn colvarGroupName = new TableSchema.TableColumn(schema);
				colvarGroupName.ColumnName = "GroupName";
				colvarGroupName.DataType = DbType.String;
				colvarGroupName.MaxLength = 100;
				colvarGroupName.AutoIncrement = false;
				colvarGroupName.IsNullable = false;
				colvarGroupName.IsPrimaryKey = false;
				colvarGroupName.IsForeignKey = false;
				colvarGroupName.IsReadOnly = false;
				colvarGroupName.DefaultSetting = @"";
				colvarGroupName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupName);

				TableSchema.TableColumn colvarApplicationId = new TableSchema.TableColumn(schema);
				colvarApplicationId.ColumnName = "ApplicationId";
				colvarApplicationId.DataType = DbType.AnsiString;
				colvarApplicationId.MaxLength = 50;
				colvarApplicationId.AutoIncrement = false;
				colvarApplicationId.IsNullable = false;
				colvarApplicationId.IsPrimaryKey = false;
				colvarApplicationId.IsForeignKey = true;
				colvarApplicationId.IsReadOnly = false;
				colvarApplicationId.DefaultSetting = @"";
				colvarApplicationId.ForeignKeyTableName = "AC_Applications";
				schema.Columns.Add(colvarApplicationId);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.Int32;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = true;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.Int32;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = true;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarModifiedBy);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"(getdate())";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_GroupApplications",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_GroupApplication LoadFrom(AC_GroupApplication item)
		{
			AC_GroupApplication result = new AC_GroupApplication();
			if (item.UserApplicationID != default(int)) {
				result.LoadByKey(item.UserApplicationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int UserApplicationID {
			get { return GetColumnValue<int>(Columns.UserApplicationID); }
			set {
				SetColumnValue(Columns.UserApplicationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserApplicationID));
			}
		}
		[DataMember]
		public string GroupName {
			get { return GetColumnValue<string>(Columns.GroupName); }
			set {
				SetColumnValue(Columns.GroupName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GroupName));
			}
		}
		[DataMember]
		public string ApplicationId {
			get { return GetColumnValue<string>(Columns.ApplicationId); }
			set {
				SetColumnValue(Columns.ApplicationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationId));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public int CreatedBy {
			get { return GetColumnValue<int>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public int ModifiedBy {
			get { return GetColumnValue<int>(Columns.ModifiedBy); }
			set {
				SetColumnValue(Columns.ModifiedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedBy));
			}
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Application _Application;
		//Relationship: FK_ApplicationId_AC_GroupApplications_AC_Applications
		public AC_Application Application
		{
			get
			{
				if(_Application == null) {
					_Application = AC_Application.FetchByID(this.ApplicationId);
				}
				return _Application;
			}
			set
			{
				SetColumnValue("ApplicationId", value.ApplicationID);
				_Application = value;
			}
		}

		private AC_User _CreatedByValue;
		//Relationship: FK_CreatedBy_AC_GroupApplications_AC_Users
		public AC_User CreatedByValue
		{
			get
			{
				if(_CreatedByValue == null) {
					_CreatedByValue = AC_User.FetchByID(this.CreatedBy);
				}
				return _CreatedByValue;
			}
			set
			{
				SetColumnValue("CreatedBy", value.UserID);
				_CreatedByValue = value;
			}
		}

		private AC_User _ModifiedByValue;
		//Relationship: FK_ModifiedBy_AC_GroupApplications_AC_Users
		public AC_User ModifiedByValue
		{
			get
			{
				if(_ModifiedByValue == null) {
					_ModifiedByValue = AC_User.FetchByID(this.ModifiedBy);
				}
				return _ModifiedByValue;
			}
			set
			{
				SetColumnValue("ModifiedBy", value.UserID);
				_ModifiedByValue = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return GroupName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserApplicationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GroupNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ApplicationIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UserApplicationID = @"UserApplicationID";
			public static readonly string GroupName = @"GroupName";
			public static readonly string ApplicationId = @"ApplicationId";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UserApplicationID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the AC_Session class.
	/// </summary>
	[DataContract]
	public partial class AC_SessionCollection : ActiveList<AC_Session, AC_SessionCollection>
	{
		public static AC_SessionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_SessionCollection result = new AC_SessionCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_Session item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_Sessions table.
	/// </summary>
	[DataContract]
	public partial class AC_Session : ActiveRecord<AC_Session>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_Session()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_Sessions", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSessionID = new TableSchema.TableColumn(schema);
				colvarSessionID.ColumnName = "SessionID";
				colvarSessionID.DataType = DbType.Int64;
				colvarSessionID.MaxLength = 0;
				colvarSessionID.AutoIncrement = true;
				colvarSessionID.IsNullable = false;
				colvarSessionID.IsPrimaryKey = true;
				colvarSessionID.IsForeignKey = false;
				colvarSessionID.IsReadOnly = false;
				colvarSessionID.DefaultSetting = @"";
				colvarSessionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSessionID);

				TableSchema.TableColumn colvarApplicationId = new TableSchema.TableColumn(schema);
				colvarApplicationId.ColumnName = "ApplicationId";
				colvarApplicationId.DataType = DbType.AnsiString;
				colvarApplicationId.MaxLength = 50;
				colvarApplicationId.AutoIncrement = false;
				colvarApplicationId.IsNullable = false;
				colvarApplicationId.IsPrimaryKey = false;
				colvarApplicationId.IsForeignKey = true;
				colvarApplicationId.IsReadOnly = false;
				colvarApplicationId.DefaultSetting = @"";
				colvarApplicationId.ForeignKeyTableName = "AC_Applications";
				schema.Columns.Add(colvarApplicationId);

				TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
				colvarUserId.ColumnName = "UserId";
				colvarUserId.DataType = DbType.Int32;
				colvarUserId.MaxLength = 0;
				colvarUserId.AutoIncrement = false;
				colvarUserId.IsNullable = true;
				colvarUserId.IsPrimaryKey = false;
				colvarUserId.IsForeignKey = false;
				colvarUserId.IsReadOnly = false;
				colvarUserId.DefaultSetting = @"";
				colvarUserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserId);

				TableSchema.TableColumn colvarIPAddress = new TableSchema.TableColumn(schema);
				colvarIPAddress.ColumnName = "IPAddress";
				colvarIPAddress.DataType = DbType.AnsiString;
				colvarIPAddress.MaxLength = 15;
				colvarIPAddress.AutoIncrement = false;
				colvarIPAddress.IsNullable = true;
				colvarIPAddress.IsPrimaryKey = false;
				colvarIPAddress.IsForeignKey = false;
				colvarIPAddress.IsReadOnly = false;
				colvarIPAddress.DefaultSetting = @"";
				colvarIPAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIPAddress);

				TableSchema.TableColumn colvarTimezoneOffset = new TableSchema.TableColumn(schema);
				colvarTimezoneOffset.ColumnName = "TimezoneOffset";
				colvarTimezoneOffset.DataType = DbType.Int32;
				colvarTimezoneOffset.MaxLength = 0;
				colvarTimezoneOffset.AutoIncrement = false;
				colvarTimezoneOffset.IsNullable = false;
				colvarTimezoneOffset.IsPrimaryKey = false;
				colvarTimezoneOffset.IsForeignKey = false;
				colvarTimezoneOffset.IsReadOnly = false;
				colvarTimezoneOffset.DefaultSetting = @"((0))";
				colvarTimezoneOffset.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTimezoneOffset);

				TableSchema.TableColumn colvarLastAccessedOn = new TableSchema.TableColumn(schema);
				colvarLastAccessedOn.ColumnName = "LastAccessedOn";
				colvarLastAccessedOn.DataType = DbType.DateTime;
				colvarLastAccessedOn.MaxLength = 0;
				colvarLastAccessedOn.AutoIncrement = false;
				colvarLastAccessedOn.IsNullable = false;
				colvarLastAccessedOn.IsPrimaryKey = false;
				colvarLastAccessedOn.IsForeignKey = false;
				colvarLastAccessedOn.IsReadOnly = false;
				colvarLastAccessedOn.DefaultSetting = @"(getdate())";
				colvarLastAccessedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAccessedOn);

				TableSchema.TableColumn colvarSessionTerminated = new TableSchema.TableColumn(schema);
				colvarSessionTerminated.ColumnName = "SessionTerminated";
				colvarSessionTerminated.DataType = DbType.Boolean;
				colvarSessionTerminated.MaxLength = 0;
				colvarSessionTerminated.AutoIncrement = false;
				colvarSessionTerminated.IsNullable = false;
				colvarSessionTerminated.IsPrimaryKey = false;
				colvarSessionTerminated.IsForeignKey = false;
				colvarSessionTerminated.IsReadOnly = false;
				colvarSessionTerminated.DefaultSetting = @"((0))";
				colvarSessionTerminated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSessionTerminated);

				TableSchema.TableColumn colvarGroups = new TableSchema.TableColumn(schema);
				colvarGroups.ColumnName = "Groups";
				colvarGroups.DataType = DbType.String;
				colvarGroups.MaxLength = -1;
				colvarGroups.AutoIncrement = false;
				colvarGroups.IsNullable = true;
				colvarGroups.IsPrimaryKey = false;
				colvarGroups.IsForeignKey = false;
				colvarGroups.IsReadOnly = false;
				colvarGroups.DefaultSetting = @"";
				colvarGroups.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroups);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_Sessions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_Session LoadFrom(AC_Session item)
		{
			AC_Session result = new AC_Session();
			if (item.SessionID != default(long)) {
				result.LoadByKey(item.SessionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long SessionID {
			get { return GetColumnValue<long>(Columns.SessionID); }
			set {
				SetColumnValue(Columns.SessionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SessionID));
			}
		}
		[DataMember]
		public string ApplicationId {
			get { return GetColumnValue<string>(Columns.ApplicationId); }
			set {
				SetColumnValue(Columns.ApplicationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationId));
			}
		}
		[DataMember]
		public int? UserId {
			get { return GetColumnValue<int?>(Columns.UserId); }
			set {
				SetColumnValue(Columns.UserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserId));
			}
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int TimezoneOffset {
			get { return GetColumnValue<int>(Columns.TimezoneOffset); }
			set {
				SetColumnValue(Columns.TimezoneOffset, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TimezoneOffset));
			}
		}
		[DataMember]
		public DateTime LastAccessedOn {
			get { return GetColumnValue<DateTime>(Columns.LastAccessedOn); }
			set {
				SetColumnValue(Columns.LastAccessedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAccessedOn));
			}
		}
		[DataMember]
		public bool SessionTerminated {
			get { return GetColumnValue<bool>(Columns.SessionTerminated); }
			set {
				SetColumnValue(Columns.SessionTerminated, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SessionTerminated));
			}
		}
		[DataMember]
		public string Groups {
			get { return GetColumnValue<string>(Columns.Groups); }
			set {
				SetColumnValue(Columns.Groups, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Groups));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Application _Application;
		//Relationship: FK_AC_Sessions_AC_Applications
		public AC_Application Application
		{
			get
			{
				if(_Application == null) {
					_Application = AC_Application.FetchByID(this.ApplicationId);
				}
				return _Application;
			}
			set
			{
				SetColumnValue("ApplicationId", value.ApplicationID);
				_Application = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ApplicationId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn SessionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ApplicationIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UserIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TimezoneOffsetColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LastAccessedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SessionTerminatedColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn GroupsColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SessionID = @"SessionID";
			public static readonly string ApplicationId = @"ApplicationId";
			public static readonly string UserId = @"UserId";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string TimezoneOffset = @"TimezoneOffset";
			public static readonly string LastAccessedOn = @"LastAccessedOn";
			public static readonly string SessionTerminated = @"SessionTerminated";
			public static readonly string Groups = @"Groups";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return SessionID; }
		}
		*/

		#region Foreign Collections

		private AC_AuthenticationCollection _AC_AuthenticationsCol;
		//Relationship: FK_AC_Authentications_AC_Sessions
		public AC_AuthenticationCollection AC_AuthenticationsCol
		{
			get
			{
				if(_AC_AuthenticationsCol == null) {
					_AC_AuthenticationsCol = new AC_AuthenticationCollection();
					_AC_AuthenticationsCol.LoadAndCloseReader(AC_Authentication.Query()
						.WHERE(AC_Authentication.Columns.SessionId, SessionID).ExecuteReader());
				}
				return _AC_AuthenticationsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the AC_UserACL class.
	/// </summary>
	[DataContract]
	public partial class AC_UserACLCollection : ActiveList<AC_UserACL, AC_UserACLCollection>
	{
		public static AC_UserACLCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UserACLCollection result = new AC_UserACLCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_UserACL item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_UserACLs table.
	/// </summary>
	[DataContract]
	public partial class AC_UserACL : ActiveRecord<AC_UserACL>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_UserACL()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_UserACLs", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarACLID = new TableSchema.TableColumn(schema);
				colvarACLID.ColumnName = "ACLID";
				colvarACLID.DataType = DbType.Int32;
				colvarACLID.MaxLength = 0;
				colvarACLID.AutoIncrement = true;
				colvarACLID.IsNullable = false;
				colvarACLID.IsPrimaryKey = true;
				colvarACLID.IsForeignKey = false;
				colvarACLID.IsReadOnly = false;
				colvarACLID.DefaultSetting = @"";
				colvarACLID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarACLID);

				TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
				colvarUserId.ColumnName = "UserId";
				colvarUserId.DataType = DbType.Int32;
				colvarUserId.MaxLength = 0;
				colvarUserId.AutoIncrement = false;
				colvarUserId.IsNullable = false;
				colvarUserId.IsPrimaryKey = false;
				colvarUserId.IsForeignKey = true;
				colvarUserId.IsReadOnly = false;
				colvarUserId.DefaultSetting = @"";
				colvarUserId.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarUserId);

				TableSchema.TableColumn colvarApplicationId = new TableSchema.TableColumn(schema);
				colvarApplicationId.ColumnName = "ApplicationId";
				colvarApplicationId.DataType = DbType.AnsiString;
				colvarApplicationId.MaxLength = 50;
				colvarApplicationId.AutoIncrement = false;
				colvarApplicationId.IsNullable = false;
				colvarApplicationId.IsPrimaryKey = false;
				colvarApplicationId.IsForeignKey = true;
				colvarApplicationId.IsReadOnly = false;
				colvarApplicationId.DefaultSetting = @"";
				colvarApplicationId.ForeignKeyTableName = "AC_Applications";
				schema.Columns.Add(colvarApplicationId);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.Int32;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"((0))";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.Int32;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"((0))";
				colvarModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedBy);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"(getdate())";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_UserACLs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_UserACL LoadFrom(AC_UserACL item)
		{
			AC_UserACL result = new AC_UserACL();
			if (item.ACLID != default(int)) {
				result.LoadByKey(item.ACLID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int ACLID {
			get { return GetColumnValue<int>(Columns.ACLID); }
			set {
				SetColumnValue(Columns.ACLID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ACLID));
			}
		}
		[DataMember]
		public int UserId {
			get { return GetColumnValue<int>(Columns.UserId); }
			set {
				SetColumnValue(Columns.UserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserId));
			}
		}
		[DataMember]
		public string ApplicationId {
			get { return GetColumnValue<string>(Columns.ApplicationId); }
			set {
				SetColumnValue(Columns.ApplicationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationId));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public int CreatedBy {
			get { return GetColumnValue<int>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public int ModifiedBy {
			get { return GetColumnValue<int>(Columns.ModifiedBy); }
			set {
				SetColumnValue(Columns.ModifiedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedBy));
			}
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Application _Application;
		//Relationship: FK_AC_UserACLs_AC_Applications
		public AC_Application Application
		{
			get
			{
				if(_Application == null) {
					_Application = AC_Application.FetchByID(this.ApplicationId);
				}
				return _Application;
			}
			set
			{
				SetColumnValue("ApplicationId", value.ApplicationID);
				_Application = value;
			}
		}

		private AC_User _User;
		//Relationship: FK_AC_UserACLs_AC_Users
		public AC_User User
		{
			get
			{
				if(_User == null) {
					_User = AC_User.FetchByID(this.UserId);
				}
				return _User;
			}
			set
			{
				SetColumnValue("UserId", value.UserID);
				_User = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ACLID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ACLIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ApplicationIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ACLID = @"ACLID";
			public static readonly string UserId = @"UserId";
			public static readonly string ApplicationId = @"ApplicationId";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ACLID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the AC_UserAction class.
	/// </summary>
	[DataContract]
	public partial class AC_UserActionCollection : ActiveList<AC_UserAction, AC_UserActionCollection>
	{
		public static AC_UserActionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UserActionCollection result = new AC_UserActionCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_UserAction item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_UserActions table.
	/// </summary>
	[DataContract]
	public partial class AC_UserAction : ActiveRecord<AC_UserAction>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_UserAction()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_UserActions", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserActionID = new TableSchema.TableColumn(schema);
				colvarUserActionID.ColumnName = "UserActionID";
				colvarUserActionID.DataType = DbType.Int32;
				colvarUserActionID.MaxLength = 0;
				colvarUserActionID.AutoIncrement = true;
				colvarUserActionID.IsNullable = false;
				colvarUserActionID.IsPrimaryKey = true;
				colvarUserActionID.IsForeignKey = false;
				colvarUserActionID.IsReadOnly = false;
				colvarUserActionID.DefaultSetting = @"";
				colvarUserActionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserActionID);

				TableSchema.TableColumn colvarUserId = new TableSchema.TableColumn(schema);
				colvarUserId.ColumnName = "UserId";
				colvarUserId.DataType = DbType.Int32;
				colvarUserId.MaxLength = 0;
				colvarUserId.AutoIncrement = false;
				colvarUserId.IsNullable = false;
				colvarUserId.IsPrimaryKey = false;
				colvarUserId.IsForeignKey = true;
				colvarUserId.IsReadOnly = false;
				colvarUserId.DefaultSetting = @"";
				colvarUserId.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarUserId);

				TableSchema.TableColumn colvarActionId = new TableSchema.TableColumn(schema);
				colvarActionId.ColumnName = "ActionId";
				colvarActionId.DataType = DbType.AnsiString;
				colvarActionId.MaxLength = 50;
				colvarActionId.AutoIncrement = false;
				colvarActionId.IsNullable = false;
				colvarActionId.IsPrimaryKey = false;
				colvarActionId.IsForeignKey = true;
				colvarActionId.IsReadOnly = false;
				colvarActionId.DefaultSetting = @"";
				colvarActionId.ForeignKeyTableName = "AC_Actions";
				schema.Columns.Add(colvarActionId);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.Int32;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = true;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.Int32;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = true;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "AC_Users";
				schema.Columns.Add(colvarModifiedBy);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"(getdate())";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_UserActions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_UserAction LoadFrom(AC_UserAction item)
		{
			AC_UserAction result = new AC_UserAction();
			if (item.UserActionID != default(int)) {
				result.LoadByKey(item.UserActionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int UserActionID {
			get { return GetColumnValue<int>(Columns.UserActionID); }
			set {
				SetColumnValue(Columns.UserActionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserActionID));
			}
		}
		[DataMember]
		public int UserId {
			get { return GetColumnValue<int>(Columns.UserId); }
			set {
				SetColumnValue(Columns.UserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserId));
			}
		}
		[DataMember]
		public string ActionId {
			get { return GetColumnValue<string>(Columns.ActionId); }
			set {
				SetColumnValue(Columns.ActionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ActionId));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public int CreatedBy {
			get { return GetColumnValue<int>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public int ModifiedBy {
			get { return GetColumnValue<int>(Columns.ModifiedBy); }
			set {
				SetColumnValue(Columns.ModifiedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedBy));
			}
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private AC_Action _Action;
		//Relationship: FK_ActionId_AC_UserActions_AC_Actions
		public AC_Action Action
		{
			get
			{
				if(_Action == null) {
					_Action = AC_Action.FetchByID(this.ActionId);
				}
				return _Action;
			}
			set
			{
				SetColumnValue("ActionId", value.ActionID);
				_Action = value;
			}
		}

		private AC_User _CreatedByValue;
		//Relationship: FK_CreatedBy_AC_UserActions_AC_Users
		public AC_User CreatedByValue
		{
			get
			{
				if(_CreatedByValue == null) {
					_CreatedByValue = AC_User.FetchByID(this.CreatedBy);
				}
				return _CreatedByValue;
			}
			set
			{
				SetColumnValue("CreatedBy", value.UserID);
				_CreatedByValue = value;
			}
		}

		private AC_User _ModifiedByValue;
		//Relationship: FK_ModifiedBy_AC_UserActions_AC_Users
		public AC_User ModifiedByValue
		{
			get
			{
				if(_ModifiedByValue == null) {
					_ModifiedByValue = AC_User.FetchByID(this.ModifiedBy);
				}
				return _ModifiedByValue;
			}
			set
			{
				SetColumnValue("ModifiedBy", value.UserID);
				_ModifiedByValue = value;
			}
		}

		private AC_User _User;
		//Relationship: FK_UserId_AC_UserActions_AC_Users
		public AC_User User
		{
			get
			{
				if(_User == null) {
					_User = AC_User.FetchByID(this.UserId);
				}
				return _User;
			}
			set
			{
				SetColumnValue("UserId", value.UserID);
				_User = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return UserActionID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserActionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ActionIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UserActionID = @"UserActionID";
			public static readonly string UserId = @"UserId";
			public static readonly string ActionId = @"ActionId";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UserActionID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the AC_User class.
	/// </summary>
	[DataContract]
	public partial class AC_UserCollection : ActiveList<AC_User, AC_UserCollection>
	{
		public static AC_UserCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UserCollection result = new AC_UserCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_User item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_Users table.
	/// </summary>
	[DataContract]
	public partial class AC_User : ActiveRecord<AC_User>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_User()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_Users", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = true;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = true;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarDealerId = new TableSchema.TableColumn(schema);
				colvarDealerId.ColumnName = "DealerId";
				colvarDealerId.DataType = DbType.Int32;
				colvarDealerId.MaxLength = 0;
				colvarDealerId.AutoIncrement = false;
				colvarDealerId.IsNullable = false;
				colvarDealerId.IsPrimaryKey = false;
				colvarDealerId.IsForeignKey = false;
				colvarDealerId.IsReadOnly = false;
				colvarDealerId.DefaultSetting = @"((5000))";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarHRUserId = new TableSchema.TableColumn(schema);
				colvarHRUserId.ColumnName = "HRUserId";
				colvarHRUserId.DataType = DbType.Int32;
				colvarHRUserId.MaxLength = 0;
				colvarHRUserId.AutoIncrement = false;
				colvarHRUserId.IsNullable = true;
				colvarHRUserId.IsPrimaryKey = false;
				colvarHRUserId.IsForeignKey = false;
				colvarHRUserId.IsReadOnly = false;
				colvarHRUserId.DefaultSetting = @"";
				colvarHRUserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHRUserId);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarSSID = new TableSchema.TableColumn(schema);
				colvarSSID.ColumnName = "SSID";
				colvarSSID.DataType = DbType.Guid;
				colvarSSID.MaxLength = 0;
				colvarSSID.AutoIncrement = false;
				colvarSSID.IsNullable = true;
				colvarSSID.IsPrimaryKey = false;
				colvarSSID.IsForeignKey = false;
				colvarSSID.IsReadOnly = false;
				colvarSSID.DefaultSetting = @"";
				colvarSSID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSSID);

				TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
				colvarUsername.ColumnName = "Username";
				colvarUsername.DataType = DbType.String;
				colvarUsername.MaxLength = 50;
				colvarUsername.AutoIncrement = false;
				colvarUsername.IsNullable = false;
				colvarUsername.IsPrimaryKey = false;
				colvarUsername.IsForeignKey = false;
				colvarUsername.IsReadOnly = false;
				colvarUsername.DefaultSetting = @"";
				colvarUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsername);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 60;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.Int32;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"((0))";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.Int32;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"((0))";
				colvarModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedBy);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"(getdate())";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_Users",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_User LoadFrom(AC_User item)
		{
			AC_User result = new AC_User();
			if (item.UserID != default(int)) {
				result.LoadByKey(item.UserID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set {
				SetColumnValue(Columns.UserID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UserID));
			}
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set {
				SetColumnValue(Columns.DealerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DealerId));
			}
		}
		[DataMember]
		public int? HRUserId {
			get { return GetColumnValue<int?>(Columns.HRUserId); }
			set {
				SetColumnValue(Columns.HRUserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.HRUserId));
			}
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set {
				SetColumnValue(Columns.GPEmployeeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GPEmployeeID));
			}
		}
		[DataMember]
		public Guid? SSID {
			get { return GetColumnValue<Guid?>(Columns.SSID); }
			set {
				SetColumnValue(Columns.SSID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SSID));
			}
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set {
				SetColumnValue(Columns.Username, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Username));
			}
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set {
				SetColumnValue(Columns.Password, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Password));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public int CreatedBy {
			get { return GetColumnValue<int>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public int ModifiedBy {
			get { return GetColumnValue<int>(Columns.ModifiedBy); }
			set {
				SetColumnValue(Columns.ModifiedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedBy));
			}
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return UserID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DealerIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn HRUserIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn SSIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[12]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UserID = @"UserID";
			public static readonly string DealerId = @"DealerId";
			public static readonly string HRUserId = @"HRUserId";
			public static readonly string GPEmployeeID = @"GPEmployeeID";
			public static readonly string SSID = @"SSID";
			public static readonly string Username = @"Username";
			public static readonly string Password = @"Password";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UserID; }
		}
		*/

		#region Foreign Collections

		private AC_AuthenticationCollection _AC_AuthenticationsCol;
		//Relationship: FK_AC_Authentications_AC_Users
		public AC_AuthenticationCollection AC_AuthenticationsCol
		{
			get
			{
				if(_AC_AuthenticationsCol == null) {
					_AC_AuthenticationsCol = new AC_AuthenticationCollection();
					_AC_AuthenticationsCol.LoadAndCloseReader(AC_Authentication.Query()
						.WHERE(AC_Authentication.Columns.UserId, UserID).ExecuteReader());
				}
				return _AC_AuthenticationsCol;
			}
		}

		private AC_UserACLCollection _AC_UserACLsCol;
		//Relationship: FK_AC_UserACLs_AC_Users
		public AC_UserACLCollection AC_UserACLsCol
		{
			get
			{
				if(_AC_UserACLsCol == null) {
					_AC_UserACLsCol = new AC_UserACLCollection();
					_AC_UserACLsCol.LoadAndCloseReader(AC_UserACL.Query()
						.WHERE(AC_UserACL.Columns.UserId, UserID).ExecuteReader());
				}
				return _AC_UserACLsCol;
			}
		}

		private AC_GroupActionCollection _AC_GroupActionsCol;
		//Relationship: FK_CreatedBy_AC_GroupActions_AC_Users
		public AC_GroupActionCollection AC_GroupActionsCol
		{
			get
			{
				if(_AC_GroupActionsCol == null) {
					_AC_GroupActionsCol = new AC_GroupActionCollection();
					_AC_GroupActionsCol.LoadAndCloseReader(AC_GroupAction.Query()
						.WHERE(AC_GroupAction.Columns.CreatedBy, UserID).ExecuteReader());
				}
				return _AC_GroupActionsCol;
			}
		}

		private AC_GroupApplicationCollection _AC_GroupApplicationsCol;
		//Relationship: FK_CreatedBy_AC_GroupApplications_AC_Users
		public AC_GroupApplicationCollection AC_GroupApplicationsCol
		{
			get
			{
				if(_AC_GroupApplicationsCol == null) {
					_AC_GroupApplicationsCol = new AC_GroupApplicationCollection();
					_AC_GroupApplicationsCol.LoadAndCloseReader(AC_GroupApplication.Query()
						.WHERE(AC_GroupApplication.Columns.CreatedBy, UserID).ExecuteReader());
				}
				return _AC_GroupApplicationsCol;
			}
		}

		private AC_UserActionCollection _AC_UserActionsCol;
		//Relationship: FK_CreatedBy_AC_UserActions_AC_Users
		public AC_UserActionCollection AC_UserActionsCol
		{
			get
			{
				if(_AC_UserActionsCol == null) {
					_AC_UserActionsCol = new AC_UserActionCollection();
					_AC_UserActionsCol.LoadAndCloseReader(AC_UserAction.Query()
						.WHERE(AC_UserAction.Columns.CreatedBy, UserID).ExecuteReader());
				}
				return _AC_UserActionsCol;
			}
		}

		private AC_GroupActionCollection _AC_GroupActions02Col;
		//Relationship: FK_ModifiedBy_AC_GroupActions_AC_Users
		public AC_GroupActionCollection AC_GroupActions02Col
		{
			get
			{
				if(_AC_GroupActions02Col == null) {
					_AC_GroupActions02Col = new AC_GroupActionCollection();
					_AC_GroupActions02Col.LoadAndCloseReader(AC_GroupAction.Query()
						.WHERE(AC_GroupAction.Columns.ModifiedBy, UserID).ExecuteReader());
				}
				return _AC_GroupActions02Col;
			}
		}

		private AC_GroupApplicationCollection _AC_GroupApplications02Col;
		//Relationship: FK_ModifiedBy_AC_GroupApplications_AC_Users
		public AC_GroupApplicationCollection AC_GroupApplications02Col
		{
			get
			{
				if(_AC_GroupApplications02Col == null) {
					_AC_GroupApplications02Col = new AC_GroupApplicationCollection();
					_AC_GroupApplications02Col.LoadAndCloseReader(AC_GroupApplication.Query()
						.WHERE(AC_GroupApplication.Columns.ModifiedBy, UserID).ExecuteReader());
				}
				return _AC_GroupApplications02Col;
			}
		}

		private AC_UserActionCollection _AC_UserActions02Col;
		//Relationship: FK_ModifiedBy_AC_UserActions_AC_Users
		public AC_UserActionCollection AC_UserActions02Col
		{
			get
			{
				if(_AC_UserActions02Col == null) {
					_AC_UserActions02Col = new AC_UserActionCollection();
					_AC_UserActions02Col.LoadAndCloseReader(AC_UserAction.Query()
						.WHERE(AC_UserAction.Columns.ModifiedBy, UserID).ExecuteReader());
				}
				return _AC_UserActions02Col;
			}
		}

		private AC_UserActionCollection _AC_UserActions03Col;
		//Relationship: FK_UserId_AC_UserActions_AC_Users
		public AC_UserActionCollection AC_UserActions03Col
		{
			get
			{
				if(_AC_UserActions03Col == null) {
					_AC_UserActions03Col = new AC_UserActionCollection();
					_AC_UserActions03Col.LoadAndCloseReader(AC_UserAction.Query()
						.WHERE(AC_UserAction.Columns.UserId, UserID).ExecuteReader());
				}
				return _AC_UserActions03Col;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the AC_UserSession class.
	/// </summary>
	[DataContract]
	public partial class AC_UserSessionCollection : ActiveList<AC_UserSession, AC_UserSessionCollection>
	{
		public static AC_UserSessionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UserSessionCollection result = new AC_UserSessionCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (AC_UserSession item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the AC_UserSessions table.
	/// </summary>
	[DataContract]
	public partial class AC_UserSession : ActiveRecord<AC_UserSession>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public AC_UserSession()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("AC_UserSessions", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarID = new TableSchema.TableColumn(schema);
				colvarID.ColumnName = "ID";
				colvarID.DataType = DbType.Int32;
				colvarID.MaxLength = 0;
				colvarID.AutoIncrement = true;
				colvarID.IsNullable = false;
				colvarID.IsPrimaryKey = true;
				colvarID.IsForeignKey = false;
				colvarID.IsReadOnly = false;
				colvarID.DefaultSetting = @"";
				colvarID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarID);

				TableSchema.TableColumn colvarSessionKey = new TableSchema.TableColumn(schema);
				colvarSessionKey.ColumnName = "SessionKey";
				colvarSessionKey.DataType = DbType.AnsiString;
				colvarSessionKey.MaxLength = 128;
				colvarSessionKey.AutoIncrement = false;
				colvarSessionKey.IsNullable = false;
				colvarSessionKey.IsPrimaryKey = false;
				colvarSessionKey.IsForeignKey = false;
				colvarSessionKey.IsReadOnly = false;
				colvarSessionKey.DefaultSetting = @"";
				colvarSessionKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSessionKey);

				TableSchema.TableColumn colvarUsername = new TableSchema.TableColumn(schema);
				colvarUsername.ColumnName = "Username";
				colvarUsername.DataType = DbType.String;
				colvarUsername.MaxLength = 50;
				colvarUsername.AutoIncrement = false;
				colvarUsername.IsNullable = true;
				colvarUsername.IsPrimaryKey = false;
				colvarUsername.IsForeignKey = false;
				colvarUsername.IsReadOnly = false;
				colvarUsername.DefaultSetting = @"";
				colvarUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUsername);

				TableSchema.TableColumn colvarLastAccessedOn = new TableSchema.TableColumn(schema);
				colvarLastAccessedOn.ColumnName = "LastAccessedOn";
				colvarLastAccessedOn.DataType = DbType.DateTime;
				colvarLastAccessedOn.MaxLength = 0;
				colvarLastAccessedOn.AutoIncrement = false;
				colvarLastAccessedOn.IsNullable = false;
				colvarLastAccessedOn.IsPrimaryKey = false;
				colvarLastAccessedOn.IsForeignKey = false;
				colvarLastAccessedOn.IsReadOnly = false;
				colvarLastAccessedOn.DefaultSetting = @"";
				colvarLastAccessedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAccessedOn);

				TableSchema.TableColumn colvarIPAddress = new TableSchema.TableColumn(schema);
				colvarIPAddress.ColumnName = "IPAddress";
				colvarIPAddress.DataType = DbType.AnsiString;
				colvarIPAddress.MaxLength = 15;
				colvarIPAddress.AutoIncrement = false;
				colvarIPAddress.IsNullable = false;
				colvarIPAddress.IsPrimaryKey = false;
				colvarIPAddress.IsForeignKey = false;
				colvarIPAddress.IsReadOnly = false;
				colvarIPAddress.DefaultSetting = @"";
				colvarIPAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIPAddress);

				TableSchema.TableColumn colvarTerminated = new TableSchema.TableColumn(schema);
				colvarTerminated.ColumnName = "Terminated";
				colvarTerminated.DataType = DbType.Boolean;
				colvarTerminated.MaxLength = 0;
				colvarTerminated.AutoIncrement = false;
				colvarTerminated.IsNullable = false;
				colvarTerminated.IsPrimaryKey = false;
				colvarTerminated.IsForeignKey = false;
				colvarTerminated.IsReadOnly = false;
				colvarTerminated.DefaultSetting = @"";
				colvarTerminated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminated);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("AC_UserSessions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AC_UserSession LoadFrom(AC_UserSession item)
		{
			AC_UserSession result = new AC_UserSession();
			if (item.ID != default(int)) {
				result.LoadByKey(item.ID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int ID {
			get { return GetColumnValue<int>(Columns.ID); }
			set {
				SetColumnValue(Columns.ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ID));
			}
		}
		[DataMember]
		public string SessionKey {
			get { return GetColumnValue<string>(Columns.SessionKey); }
			set {
				SetColumnValue(Columns.SessionKey, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SessionKey));
			}
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set {
				SetColumnValue(Columns.Username, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Username));
			}
		}
		[DataMember]
		public DateTime LastAccessedOn {
			get { return GetColumnValue<DateTime>(Columns.LastAccessedOn); }
			set {
				SetColumnValue(Columns.LastAccessedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAccessedOn));
			}
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public bool Terminated {
			get { return GetColumnValue<bool>(Columns.Terminated); }
			set {
				SetColumnValue(Columns.Terminated, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Terminated));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return SessionKey;
		}

		#region Typed Columns

		public static TableSchema.TableColumn IDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SessionKeyColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LastAccessedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TerminatedColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ID = @"ID";
			public static readonly string SessionKey = @"SessionKey";
			public static readonly string Username = @"Username";
			public static readonly string LastAccessedOn = @"LastAccessedOn";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Terminated = @"Terminated";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ID; }
		}
		*/


	}
}
