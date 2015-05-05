


using System;
using System.ComponentModel;
using System.Linq;
using SubSonic;
using SubSonic.Utilities;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace SOS.Data.AuthenticationControl
{
	/// <summary>
	/// Strongly-typed collection for the AC_DateTimeView class.
	/// </summary>
	[DataContract]
	public partial class AC_DateTimeViewCollection : ReadOnlyList<AC_DateTimeView, AC_DateTimeViewCollection>
	{
		public static AC_DateTimeViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_DateTimeViewCollection result = new AC_DateTimeViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAC_DateTime view.
	/// </summary>
	[DataContract]
	public partial class AC_DateTimeView : ReadOnlyRecord<AC_DateTimeView>
	{
		#region Default Settings
		protected static void SetSQLProps() { GetTableSchema(); }
		#endregion

		#region Schema Accessor
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
				TableSchema.Table schema = new TableSchema.Table("vwAC_DateTime", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLocalDateTime = new TableSchema.TableColumn(schema);
				colvarLocalDateTime.ColumnName = "LocalDateTime";
				colvarLocalDateTime.DataType = DbType.DateTime;
				colvarLocalDateTime.MaxLength = 0;
				colvarLocalDateTime.AutoIncrement = false;
				colvarLocalDateTime.IsNullable = false;
				colvarLocalDateTime.IsPrimaryKey = false;
				colvarLocalDateTime.IsForeignKey = false;
				colvarLocalDateTime.IsReadOnly = false;
				colvarLocalDateTime.DefaultSetting = @"";
				colvarLocalDateTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocalDateTime);

				TableSchema.TableColumn colvarUTCDateTime = new TableSchema.TableColumn(schema);
				colvarUTCDateTime.ColumnName = "UTCDateTime";
				colvarUTCDateTime.DataType = DbType.DateTime;
				colvarUTCDateTime.MaxLength = 0;
				colvarUTCDateTime.AutoIncrement = false;
				colvarUTCDateTime.IsNullable = false;
				colvarUTCDateTime.IsPrimaryKey = false;
				colvarUTCDateTime.IsForeignKey = false;
				colvarUTCDateTime.IsReadOnly = false;
				colvarUTCDateTime.DefaultSetting = @"";
				colvarUTCDateTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUTCDateTime);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("vwAC_DateTime",schema);
			}
		}
		#endregion //Schema Accessor

		#region Query Accessor
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}
		#endregion //Query Accessor

		#region .ctors
		public AC_DateTimeView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public DateTime LocalDateTime {
			get { return GetColumnValue<DateTime>(Columns.LocalDateTime); }
			set { SetColumnValue(Columns.LocalDateTime, value); }
		}
		[DataMember]
		public DateTime UTCDateTime {
			get { return GetColumnValue<DateTime>(Columns.UTCDateTime); }
			set { SetColumnValue(Columns.UTCDateTime, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return LocalDateTime.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LocalDateTimeColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UTCDateTimeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string LocalDateTime = @"LocalDateTime";
			public const string UTCDateTime = @"UTCDateTime";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the AC_UserGeneralAuthenticationView class.
	/// </summary>
	[DataContract]
	public partial class AC_UserGeneralAuthenticationViewCollection : ReadOnlyList<AC_UserGeneralAuthenticationView, AC_UserGeneralAuthenticationViewCollection>
	{
		public static AC_UserGeneralAuthenticationViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UserGeneralAuthenticationViewCollection result = new AC_UserGeneralAuthenticationViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAC_UserGeneralAuthentication view.
	/// </summary>
	[DataContract]
	public partial class AC_UserGeneralAuthenticationView : ReadOnlyRecord<AC_UserGeneralAuthenticationView>
	{
		#region Default Settings
		protected static void SetSQLProps() { GetTableSchema(); }
		#endregion

		#region Schema Accessor
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
				TableSchema.Table schema = new TableSchema.Table("vwAC_UserGeneralAuthentication", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

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

				TableSchema.TableColumn colvarDealerId = new TableSchema.TableColumn(schema);
				colvarDealerId.ColumnName = "DealerId";
				colvarDealerId.DataType = DbType.Int32;
				colvarDealerId.MaxLength = 0;
				colvarDealerId.AutoIncrement = false;
				colvarDealerId.IsNullable = false;
				colvarDealerId.IsPrimaryKey = false;
				colvarDealerId.IsForeignKey = false;
				colvarDealerId.IsReadOnly = false;
				colvarDealerId.DefaultSetting = @"";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarID = new TableSchema.TableColumn(schema);
				colvarID.ColumnName = "ID";
				colvarID.DataType = DbType.Int64;
				colvarID.MaxLength = 0;
				colvarID.AutoIncrement = false;
				colvarID.IsNullable = false;
				colvarID.IsPrimaryKey = false;
				colvarID.IsForeignKey = false;
				colvarID.IsReadOnly = false;
				colvarID.DefaultSetting = @"";
				colvarID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarID);

				TableSchema.TableColumn colvarApplicationID = new TableSchema.TableColumn(schema);
				colvarApplicationID.ColumnName = "ApplicationID";
				colvarApplicationID.DataType = DbType.AnsiString;
				colvarApplicationID.MaxLength = 50;
				colvarApplicationID.AutoIncrement = false;
				colvarApplicationID.IsNullable = false;
				colvarApplicationID.IsPrimaryKey = false;
				colvarApplicationID.IsForeignKey = false;
				colvarApplicationID.IsReadOnly = false;
				colvarApplicationID.DefaultSetting = @"";
				colvarApplicationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApplicationID);

				TableSchema.TableColumn colvarMdlUsername = new TableSchema.TableColumn(schema);
				colvarMdlUsername.ColumnName = "MdlUsername";
				colvarMdlUsername.DataType = DbType.String;
				colvarMdlUsername.MaxLength = 500;
				colvarMdlUsername.AutoIncrement = false;
				colvarMdlUsername.IsNullable = true;
				colvarMdlUsername.IsPrimaryKey = false;
				colvarMdlUsername.IsForeignKey = false;
				colvarMdlUsername.IsReadOnly = false;
				colvarMdlUsername.DefaultSetting = @"";
				colvarMdlUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMdlUsername);

				TableSchema.TableColumn colvarMdlPassword = new TableSchema.TableColumn(schema);
				colvarMdlPassword.ColumnName = "MdlPassword";
				colvarMdlPassword.DataType = DbType.String;
				colvarMdlPassword.MaxLength = 50;
				colvarMdlPassword.AutoIncrement = false;
				colvarMdlPassword.IsNullable = true;
				colvarMdlPassword.IsPrimaryKey = false;
				colvarMdlPassword.IsForeignKey = false;
				colvarMdlPassword.IsReadOnly = false;
				colvarMdlPassword.DefaultSetting = @"";
				colvarMdlPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMdlPassword);

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

				TableSchema.TableColumn colvarLastLoginOn = new TableSchema.TableColumn(schema);
				colvarLastLoginOn.ColumnName = "LastLoginOn";
				colvarLastLoginOn.DataType = DbType.DateTime;
				colvarLastLoginOn.MaxLength = 0;
				colvarLastLoginOn.AutoIncrement = false;
				colvarLastLoginOn.IsNullable = true;
				colvarLastLoginOn.IsPrimaryKey = false;
				colvarLastLoginOn.IsForeignKey = false;
				colvarLastLoginOn.IsReadOnly = false;
				colvarLastLoginOn.DefaultSetting = @"";
				colvarLastLoginOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastLoginOn);

				TableSchema.TableColumn colvarDlrUsername = new TableSchema.TableColumn(schema);
				colvarDlrUsername.ColumnName = "DlrUsername";
				colvarDlrUsername.DataType = DbType.String;
				colvarDlrUsername.MaxLength = 500;
				colvarDlrUsername.AutoIncrement = false;
				colvarDlrUsername.IsNullable = true;
				colvarDlrUsername.IsPrimaryKey = false;
				colvarDlrUsername.IsForeignKey = false;
				colvarDlrUsername.IsReadOnly = false;
				colvarDlrUsername.DefaultSetting = @"";
				colvarDlrUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDlrUsername);

				TableSchema.TableColumn colvarDlrPassword = new TableSchema.TableColumn(schema);
				colvarDlrPassword.ColumnName = "DlrPassword";
				colvarDlrPassword.DataType = DbType.String;
				colvarDlrPassword.MaxLength = 20;
				colvarDlrPassword.AutoIncrement = false;
				colvarDlrPassword.IsNullable = true;
				colvarDlrPassword.IsPrimaryKey = false;
				colvarDlrPassword.IsForeignKey = false;
				colvarDlrPassword.IsReadOnly = false;
				colvarDlrPassword.DefaultSetting = @"";
				colvarDlrPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDlrPassword);

				TableSchema.TableColumn colvarClnUsername = new TableSchema.TableColumn(schema);
				colvarClnUsername.ColumnName = "ClnUsername";
				colvarClnUsername.DataType = DbType.String;
				colvarClnUsername.MaxLength = 50;
				colvarClnUsername.AutoIncrement = false;
				colvarClnUsername.IsNullable = true;
				colvarClnUsername.IsPrimaryKey = false;
				colvarClnUsername.IsForeignKey = false;
				colvarClnUsername.IsReadOnly = false;
				colvarClnUsername.DefaultSetting = @"";
				colvarClnUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClnUsername);

				TableSchema.TableColumn colvarClnPassword = new TableSchema.TableColumn(schema);
				colvarClnPassword.ColumnName = "ClnPassword";
				colvarClnPassword.DataType = DbType.String;
				colvarClnPassword.MaxLength = 50;
				colvarClnPassword.AutoIncrement = false;
				colvarClnPassword.IsNullable = true;
				colvarClnPassword.IsPrimaryKey = false;
				colvarClnPassword.IsForeignKey = false;
				colvarClnPassword.IsReadOnly = false;
				colvarClnPassword.DefaultSetting = @"";
				colvarClnPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClnPassword);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("vwAC_UserGeneralAuthentication",schema);
			}
		}
		#endregion //Schema Accessor

		#region Query Accessor
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}
		#endregion //Query Accessor

		#region .ctors
		public AC_UserGeneralAuthenticationView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set { SetColumnValue(Columns.Username, value); }
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set { SetColumnValue(Columns.DealerId, value); }
		}
		[DataMember]
		public long ID {
			get { return GetColumnValue<long>(Columns.ID); }
			set { SetColumnValue(Columns.ID, value); }
		}
		[DataMember]
		public string ApplicationID {
			get { return GetColumnValue<string>(Columns.ApplicationID); }
			set { SetColumnValue(Columns.ApplicationID, value); }
		}
		[DataMember]
		public string MdlUsername {
			get { return GetColumnValue<string>(Columns.MdlUsername); }
			set { SetColumnValue(Columns.MdlUsername, value); }
		}
		[DataMember]
		public string MdlPassword {
			get { return GetColumnValue<string>(Columns.MdlPassword); }
			set { SetColumnValue(Columns.MdlPassword, value); }
		}
		[DataMember]
		public string ApplicationName {
			get { return GetColumnValue<string>(Columns.ApplicationName); }
			set { SetColumnValue(Columns.ApplicationName, value); }
		}
		[DataMember]
		public string WebUrl {
			get { return GetColumnValue<string>(Columns.WebUrl); }
			set { SetColumnValue(Columns.WebUrl, value); }
		}
		[DataMember]
		public DateTime? LastLoginOn {
			get { return GetColumnValue<DateTime?>(Columns.LastLoginOn); }
			set { SetColumnValue(Columns.LastLoginOn, value); }
		}
		[DataMember]
		public string DlrUsername {
			get { return GetColumnValue<string>(Columns.DlrUsername); }
			set { SetColumnValue(Columns.DlrUsername, value); }
		}
		[DataMember]
		public string DlrPassword {
			get { return GetColumnValue<string>(Columns.DlrPassword); }
			set { SetColumnValue(Columns.DlrPassword, value); }
		}
		[DataMember]
		public string ClnUsername {
			get { return GetColumnValue<string>(Columns.ClnUsername); }
			set { SetColumnValue(Columns.ClnUsername, value); }
		}
		[DataMember]
		public string ClnPassword {
			get { return GetColumnValue<string>(Columns.ClnPassword); }
			set { SetColumnValue(Columns.ClnPassword, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return Username;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DealerIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ApplicationIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn MdlUsernameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn MdlPasswordColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ApplicationNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn WebUrlColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn LastLoginOnColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn DlrUsernameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn DlrPasswordColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn ClnUsernameColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn ClnPasswordColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string Username = @"Username";
			public const string Password = @"Password";
			public const string DealerId = @"DealerId";
			public const string ID = @"ID";
			public const string ApplicationID = @"ApplicationID";
			public const string MdlUsername = @"MdlUsername";
			public const string MdlPassword = @"MdlPassword";
			public const string ApplicationName = @"ApplicationName";
			public const string WebUrl = @"WebUrl";
			public const string LastLoginOn = @"LastLoginOn";
			public const string DlrUsername = @"DlrUsername";
			public const string DlrPassword = @"DlrPassword";
			public const string ClnUsername = @"ClnUsername";
			public const string ClnPassword = @"ClnPassword";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the AC_UsersAppAuthenticationView class.
	/// </summary>
	[DataContract]
	public partial class AC_UsersAppAuthenticationViewCollection : ReadOnlyList<AC_UsersAppAuthenticationView, AC_UsersAppAuthenticationViewCollection>
	{
		public static AC_UsersAppAuthenticationViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UsersAppAuthenticationViewCollection result = new AC_UsersAppAuthenticationViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAC_UsersAppAuthentication view.
	/// </summary>
	[DataContract]
	public partial class AC_UsersAppAuthenticationView : ReadOnlyRecord<AC_UsersAppAuthenticationView>
	{
		#region Default Settings
		protected static void SetSQLProps() { GetTableSchema(); }
		#endregion

		#region Schema Accessor
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
				TableSchema.Table schema = new TableSchema.Table("vwAC_UsersAppAuthentication", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
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
				colvarDealerId.DefaultSetting = @"";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarHRUserId = new TableSchema.TableColumn(schema);
				colvarHRUserId.ColumnName = "HRUserId";
				colvarHRUserId.DataType = DbType.Int32;
				colvarHRUserId.MaxLength = 0;
				colvarHRUserId.AutoIncrement = false;
				colvarHRUserId.IsNullable = false;
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
				colvarGPEmployeeID.IsNullable = false;
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

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = true;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = true;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarSessionId = new TableSchema.TableColumn(schema);
				colvarSessionId.ColumnName = "SessionId";
				colvarSessionId.DataType = DbType.Int32;
				colvarSessionId.MaxLength = 0;
				colvarSessionId.AutoIncrement = false;
				colvarSessionId.IsNullable = true;
				colvarSessionId.IsPrimaryKey = false;
				colvarSessionId.IsForeignKey = false;
				colvarSessionId.IsReadOnly = false;
				colvarSessionId.DefaultSetting = @"";
				colvarSessionId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSessionId);

				TableSchema.TableColumn colvarUserEmployeeTypeID = new TableSchema.TableColumn(schema);
				colvarUserEmployeeTypeID.ColumnName = "UserEmployeeTypeID";
				colvarUserEmployeeTypeID.DataType = DbType.AnsiString;
				colvarUserEmployeeTypeID.MaxLength = 20;
				colvarUserEmployeeTypeID.AutoIncrement = false;
				colvarUserEmployeeTypeID.IsNullable = false;
				colvarUserEmployeeTypeID.IsPrimaryKey = false;
				colvarUserEmployeeTypeID.IsForeignKey = false;
				colvarUserEmployeeTypeID.IsReadOnly = false;
				colvarUserEmployeeTypeID.DefaultSetting = @"";
				colvarUserEmployeeTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserEmployeeTypeID);

				TableSchema.TableColumn colvarUserEmployeeTypeName = new TableSchema.TableColumn(schema);
				colvarUserEmployeeTypeName.ColumnName = "UserEmployeeTypeName";
				colvarUserEmployeeTypeName.DataType = DbType.String;
				colvarUserEmployeeTypeName.MaxLength = 50;
				colvarUserEmployeeTypeName.AutoIncrement = false;
				colvarUserEmployeeTypeName.IsNullable = false;
				colvarUserEmployeeTypeName.IsPrimaryKey = false;
				colvarUserEmployeeTypeName.IsForeignKey = false;
				colvarUserEmployeeTypeName.IsReadOnly = false;
				colvarUserEmployeeTypeName.DefaultSetting = @"";
				colvarUserEmployeeTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserEmployeeTypeName);

				TableSchema.TableColumn colvarSecurityLevel = new TableSchema.TableColumn(schema);
				colvarSecurityLevel.ColumnName = "SecurityLevel";
				colvarSecurityLevel.DataType = DbType.Byte;
				colvarSecurityLevel.MaxLength = 0;
				colvarSecurityLevel.AutoIncrement = false;
				colvarSecurityLevel.IsNullable = true;
				colvarSecurityLevel.IsPrimaryKey = false;
				colvarSecurityLevel.IsForeignKey = false;
				colvarSecurityLevel.IsReadOnly = false;
				colvarSecurityLevel.DefaultSetting = @"";
				colvarSecurityLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSecurityLevel);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"";
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
				colvarIsDeleted.DefaultSetting = @"";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("vwAC_UsersAppAuthentication",schema);
			}
		}
		#endregion //Schema Accessor

		#region Query Accessor
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}
		#endregion //Query Accessor

		#region .ctors
		public AC_UsersAppAuthenticationView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set { SetColumnValue(Columns.DealerId, value); }
		}
		[DataMember]
		public int HRUserId {
			get { return GetColumnValue<int>(Columns.HRUserId); }
			set { SetColumnValue(Columns.HRUserId, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public Guid? SSID {
			get { return GetColumnValue<Guid?>(Columns.SSID); }
			set { SetColumnValue(Columns.SSID, value); }
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set { SetColumnValue(Columns.Username, value); }
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		[DataMember]
		public int? SessionId {
			get { return GetColumnValue<int?>(Columns.SessionId); }
			set { SetColumnValue(Columns.SessionId, value); }
		}
		[DataMember]
		public string UserEmployeeTypeID {
			get { return GetColumnValue<string>(Columns.UserEmployeeTypeID); }
			set { SetColumnValue(Columns.UserEmployeeTypeID, value); }
		}
		[DataMember]
		public string UserEmployeeTypeName {
			get { return GetColumnValue<string>(Columns.UserEmployeeTypeName); }
			set { SetColumnValue(Columns.UserEmployeeTypeName, value); }
		}
		[DataMember]
		public byte? SecurityLevel {
			get { return GetColumnValue<byte?>(Columns.SecurityLevel); }
			set { SetColumnValue(Columns.SecurityLevel, value); }
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set { SetColumnValue(Columns.IsActive, value); }
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
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
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SessionIdColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn UserEmployeeTypeIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn UserEmployeeTypeNameColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn SecurityLevelColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[15]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string DealerId = @"DealerId";
			public const string HRUserId = @"HRUserId";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string SSID = @"SSID";
			public const string Username = @"Username";
			public const string Password = @"Password";
			public const string FullName = @"FullName";
			public const string FirstName = @"FirstName";
			public const string LastName = @"LastName";
			public const string SessionId = @"SessionId";
			public const string UserEmployeeTypeID = @"UserEmployeeTypeID";
			public const string UserEmployeeTypeName = @"UserEmployeeTypeName";
			public const string SecurityLevel = @"SecurityLevel";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the AC_UsersDealerUsersAuthenticateView class.
	/// </summary>
	[DataContract]
	public partial class AC_UsersDealerUsersAuthenticateViewCollection : ReadOnlyList<AC_UsersDealerUsersAuthenticateView, AC_UsersDealerUsersAuthenticateViewCollection>
	{
		public static AC_UsersDealerUsersAuthenticateViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AC_UsersDealerUsersAuthenticateViewCollection result = new AC_UsersDealerUsersAuthenticateViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAC_UsersDealerUsersAuthenticate view.
	/// </summary>
	[DataContract]
	public partial class AC_UsersDealerUsersAuthenticateView : ReadOnlyRecord<AC_UsersDealerUsersAuthenticateView>
	{
		#region Default Settings
		protected static void SetSQLProps() { GetTableSchema(); }
		#endregion

		#region Schema Accessor
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
				TableSchema.Table schema = new TableSchema.Table("vwAC_UsersDealerUsersAuthenticate", TableType.Table, DataService.GetInstance("SosAuthControlProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

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

				TableSchema.TableColumn colvarAuIsActive = new TableSchema.TableColumn(schema);
				colvarAuIsActive.ColumnName = "AuIsActive";
				colvarAuIsActive.DataType = DbType.Boolean;
				colvarAuIsActive.MaxLength = 0;
				colvarAuIsActive.AutoIncrement = false;
				colvarAuIsActive.IsNullable = false;
				colvarAuIsActive.IsPrimaryKey = false;
				colvarAuIsActive.IsForeignKey = false;
				colvarAuIsActive.IsReadOnly = false;
				colvarAuIsActive.DefaultSetting = @"";
				colvarAuIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAuIsActive);

				TableSchema.TableColumn colvarSessionID = new TableSchema.TableColumn(schema);
				colvarSessionID.ColumnName = "SessionID";
				colvarSessionID.DataType = DbType.Int64;
				colvarSessionID.MaxLength = 0;
				colvarSessionID.AutoIncrement = false;
				colvarSessionID.IsNullable = false;
				colvarSessionID.IsPrimaryKey = false;
				colvarSessionID.IsForeignKey = false;
				colvarSessionID.IsReadOnly = false;
				colvarSessionID.DefaultSetting = @"";
				colvarSessionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSessionID);

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

				TableSchema.TableColumn colvarDealerUserID = new TableSchema.TableColumn(schema);
				colvarDealerUserID.ColumnName = "DealerUserID";
				colvarDealerUserID.DataType = DbType.Int32;
				colvarDealerUserID.MaxLength = 0;
				colvarDealerUserID.AutoIncrement = false;
				colvarDealerUserID.IsNullable = false;
				colvarDealerUserID.IsPrimaryKey = false;
				colvarDealerUserID.IsForeignKey = false;
				colvarDealerUserID.IsReadOnly = false;
				colvarDealerUserID.DefaultSetting = @"";
				colvarDealerUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerUserID);

				TableSchema.TableColumn colvarDealerUserTypeId = new TableSchema.TableColumn(schema);
				colvarDealerUserTypeId.ColumnName = "DealerUserTypeId";
				colvarDealerUserTypeId.DataType = DbType.Byte;
				colvarDealerUserTypeId.MaxLength = 0;
				colvarDealerUserTypeId.AutoIncrement = false;
				colvarDealerUserTypeId.IsNullable = false;
				colvarDealerUserTypeId.IsPrimaryKey = false;
				colvarDealerUserTypeId.IsForeignKey = false;
				colvarDealerUserTypeId.IsReadOnly = false;
				colvarDealerUserTypeId.DefaultSetting = @"";
				colvarDealerUserTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerUserTypeId);

				TableSchema.TableColumn colvarDealerUserType = new TableSchema.TableColumn(schema);
				colvarDealerUserType.ColumnName = "DealerUserType";
				colvarDealerUserType.DataType = DbType.String;
				colvarDealerUserType.MaxLength = 50;
				colvarDealerUserType.AutoIncrement = false;
				colvarDealerUserType.IsNullable = false;
				colvarDealerUserType.IsPrimaryKey = false;
				colvarDealerUserType.IsForeignKey = false;
				colvarDealerUserType.IsReadOnly = false;
				colvarDealerUserType.DefaultSetting = @"";
				colvarDealerUserType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerUserType);

				TableSchema.TableColumn colvarDealerId = new TableSchema.TableColumn(schema);
				colvarDealerId.ColumnName = "DealerId";
				colvarDealerId.DataType = DbType.Int32;
				colvarDealerId.MaxLength = 0;
				colvarDealerId.AutoIncrement = false;
				colvarDealerId.IsNullable = false;
				colvarDealerId.IsPrimaryKey = false;
				colvarDealerId.IsForeignKey = false;
				colvarDealerId.IsReadOnly = false;
				colvarDealerId.DefaultSetting = @"";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarDealerName = new TableSchema.TableColumn(schema);
				colvarDealerName.ColumnName = "DealerName";
				colvarDealerName.DataType = DbType.String;
				colvarDealerName.MaxLength = 150;
				colvarDealerName.AutoIncrement = false;
				colvarDealerName.IsNullable = false;
				colvarDealerName.IsPrimaryKey = false;
				colvarDealerName.IsForeignKey = false;
				colvarDealerName.IsReadOnly = false;
				colvarDealerName.DefaultSetting = @"";
				colvarDealerName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerName);

				TableSchema.TableColumn colvarMcUserId = new TableSchema.TableColumn(schema);
				colvarMcUserId.ColumnName = "McUserId";
				colvarMcUserId.DataType = DbType.String;
				colvarMcUserId.MaxLength = 562;
				colvarMcUserId.AutoIncrement = false;
				colvarMcUserId.IsNullable = true;
				colvarMcUserId.IsPrimaryKey = false;
				colvarMcUserId.IsForeignKey = false;
				colvarMcUserId.IsReadOnly = false;
				colvarMcUserId.DefaultSetting = @"";
				colvarMcUserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMcUserId);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 100;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = false;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarFirstname = new TableSchema.TableColumn(schema);
				colvarFirstname.ColumnName = "Firstname";
				colvarFirstname.DataType = DbType.String;
				colvarFirstname.MaxLength = 50;
				colvarFirstname.AutoIncrement = false;
				colvarFirstname.IsNullable = true;
				colvarFirstname.IsPrimaryKey = false;
				colvarFirstname.IsForeignKey = false;
				colvarFirstname.IsReadOnly = false;
				colvarFirstname.DefaultSetting = @"";
				colvarFirstname.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstname);

				TableSchema.TableColumn colvarMiddlename = new TableSchema.TableColumn(schema);
				colvarMiddlename.ColumnName = "Middlename";
				colvarMiddlename.DataType = DbType.String;
				colvarMiddlename.MaxLength = 50;
				colvarMiddlename.AutoIncrement = false;
				colvarMiddlename.IsNullable = true;
				colvarMiddlename.IsPrimaryKey = false;
				colvarMiddlename.IsForeignKey = false;
				colvarMiddlename.IsReadOnly = false;
				colvarMiddlename.DefaultSetting = @"";
				colvarMiddlename.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddlename);

				TableSchema.TableColumn colvarLastname = new TableSchema.TableColumn(schema);
				colvarLastname.ColumnName = "Lastname";
				colvarLastname.DataType = DbType.String;
				colvarLastname.MaxLength = 50;
				colvarLastname.AutoIncrement = false;
				colvarLastname.IsNullable = true;
				colvarLastname.IsPrimaryKey = false;
				colvarLastname.IsForeignKey = false;
				colvarLastname.IsReadOnly = false;
				colvarLastname.DefaultSetting = @"";
				colvarLastname.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastname);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 500;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = false;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarPhoneWork = new TableSchema.TableColumn(schema);
				colvarPhoneWork.ColumnName = "PhoneWork";
				colvarPhoneWork.DataType = DbType.AnsiString;
				colvarPhoneWork.MaxLength = 30;
				colvarPhoneWork.AutoIncrement = false;
				colvarPhoneWork.IsNullable = true;
				colvarPhoneWork.IsPrimaryKey = false;
				colvarPhoneWork.IsForeignKey = false;
				colvarPhoneWork.IsReadOnly = false;
				colvarPhoneWork.DefaultSetting = @"";
				colvarPhoneWork.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneWork);

				TableSchema.TableColumn colvarPhoneCell = new TableSchema.TableColumn(schema);
				colvarPhoneCell.ColumnName = "PhoneCell";
				colvarPhoneCell.DataType = DbType.AnsiString;
				colvarPhoneCell.MaxLength = 20;
				colvarPhoneCell.AutoIncrement = false;
				colvarPhoneCell.IsNullable = true;
				colvarPhoneCell.IsPrimaryKey = false;
				colvarPhoneCell.IsForeignKey = false;
				colvarPhoneCell.IsReadOnly = false;
				colvarPhoneCell.DefaultSetting = @"";
				colvarPhoneCell.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCell);

				TableSchema.TableColumn colvarADUsername = new TableSchema.TableColumn(schema);
				colvarADUsername.ColumnName = "ADUsername";
				colvarADUsername.DataType = DbType.String;
				colvarADUsername.MaxLength = 200;
				colvarADUsername.AutoIncrement = false;
				colvarADUsername.IsNullable = true;
				colvarADUsername.IsPrimaryKey = false;
				colvarADUsername.IsForeignKey = false;
				colvarADUsername.IsReadOnly = false;
				colvarADUsername.DefaultSetting = @"";
				colvarADUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarADUsername);

				TableSchema.TableColumn colvarMcUsername = new TableSchema.TableColumn(schema);
				colvarMcUsername.ColumnName = "McUsername";
				colvarMcUsername.DataType = DbType.String;
				colvarMcUsername.MaxLength = 500;
				colvarMcUsername.AutoIncrement = false;
				colvarMcUsername.IsNullable = false;
				colvarMcUsername.IsPrimaryKey = false;
				colvarMcUsername.IsForeignKey = false;
				colvarMcUsername.IsReadOnly = false;
				colvarMcUsername.DefaultSetting = @"";
				colvarMcUsername.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMcUsername);

				TableSchema.TableColumn colvarMcPassword = new TableSchema.TableColumn(schema);
				colvarMcPassword.ColumnName = "McPassword";
				colvarMcPassword.DataType = DbType.String;
				colvarMcPassword.MaxLength = 20;
				colvarMcPassword.AutoIncrement = false;
				colvarMcPassword.IsNullable = false;
				colvarMcPassword.IsPrimaryKey = false;
				colvarMcPassword.IsForeignKey = false;
				colvarMcPassword.IsReadOnly = false;
				colvarMcPassword.DefaultSetting = @"";
				colvarMcPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMcPassword);

				TableSchema.TableColumn colvarLastLoginOn = new TableSchema.TableColumn(schema);
				colvarLastLoginOn.ColumnName = "LastLoginOn";
				colvarLastLoginOn.DataType = DbType.DateTime;
				colvarLastLoginOn.MaxLength = 0;
				colvarLastLoginOn.AutoIncrement = false;
				colvarLastLoginOn.IsNullable = true;
				colvarLastLoginOn.IsPrimaryKey = false;
				colvarLastLoginOn.IsForeignKey = false;
				colvarLastLoginOn.IsReadOnly = false;
				colvarLastLoginOn.DefaultSetting = @"";
				colvarLastLoginOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastLoginOn);

				TableSchema.TableColumn colvarMduIsActive = new TableSchema.TableColumn(schema);
				colvarMduIsActive.ColumnName = "MduIsActive";
				colvarMduIsActive.DataType = DbType.Boolean;
				colvarMduIsActive.MaxLength = 0;
				colvarMduIsActive.AutoIncrement = false;
				colvarMduIsActive.IsNullable = false;
				colvarMduIsActive.IsPrimaryKey = false;
				colvarMduIsActive.IsForeignKey = false;
				colvarMduIsActive.IsReadOnly = false;
				colvarMduIsActive.DefaultSetting = @"";
				colvarMduIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMduIsActive);

				BaseSchema = schema;
				DataService.Providers["SosAuthControlProvider"].AddSchema("vwAC_UsersDealerUsersAuthenticate",schema);
			}
		}
		#endregion //Schema Accessor

		#region Query Accessor
		public static Query CreateQuery()
		{
			return new Query(Schema);
		}
		#endregion //Query Accessor

		#region .ctors
		public AC_UsersDealerUsersAuthenticateView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public int? HRUserId {
			get { return GetColumnValue<int?>(Columns.HRUserId); }
			set { SetColumnValue(Columns.HRUserId, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string Username {
			get { return GetColumnValue<string>(Columns.Username); }
			set { SetColumnValue(Columns.Username, value); }
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		[DataMember]
		public bool AuIsActive {
			get { return GetColumnValue<bool>(Columns.AuIsActive); }
			set { SetColumnValue(Columns.AuIsActive, value); }
		}
		[DataMember]
		public long SessionID {
			get { return GetColumnValue<long>(Columns.SessionID); }
			set { SetColumnValue(Columns.SessionID, value); }
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set { SetColumnValue(Columns.IPAddress, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public DateTime LastAccessedOn {
			get { return GetColumnValue<DateTime>(Columns.LastAccessedOn); }
			set { SetColumnValue(Columns.LastAccessedOn, value); }
		}
		[DataMember]
		public int DealerUserID {
			get { return GetColumnValue<int>(Columns.DealerUserID); }
			set { SetColumnValue(Columns.DealerUserID, value); }
		}
		[DataMember]
		public byte DealerUserTypeId {
			get { return GetColumnValue<byte>(Columns.DealerUserTypeId); }
			set { SetColumnValue(Columns.DealerUserTypeId, value); }
		}
		[DataMember]
		public string DealerUserType {
			get { return GetColumnValue<string>(Columns.DealerUserType); }
			set { SetColumnValue(Columns.DealerUserType, value); }
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set { SetColumnValue(Columns.DealerId, value); }
		}
		[DataMember]
		public string DealerName {
			get { return GetColumnValue<string>(Columns.DealerName); }
			set { SetColumnValue(Columns.DealerName, value); }
		}
		[DataMember]
		public string McUserId {
			get { return GetColumnValue<string>(Columns.McUserId); }
			set { SetColumnValue(Columns.McUserId, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string Firstname {
			get { return GetColumnValue<string>(Columns.Firstname); }
			set { SetColumnValue(Columns.Firstname, value); }
		}
		[DataMember]
		public string Middlename {
			get { return GetColumnValue<string>(Columns.Middlename); }
			set { SetColumnValue(Columns.Middlename, value); }
		}
		[DataMember]
		public string Lastname {
			get { return GetColumnValue<string>(Columns.Lastname); }
			set { SetColumnValue(Columns.Lastname, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public string PhoneWork {
			get { return GetColumnValue<string>(Columns.PhoneWork); }
			set { SetColumnValue(Columns.PhoneWork, value); }
		}
		[DataMember]
		public string PhoneCell {
			get { return GetColumnValue<string>(Columns.PhoneCell); }
			set { SetColumnValue(Columns.PhoneCell, value); }
		}
		[DataMember]
		public string ADUsername {
			get { return GetColumnValue<string>(Columns.ADUsername); }
			set { SetColumnValue(Columns.ADUsername, value); }
		}
		[DataMember]
		public string McUsername {
			get { return GetColumnValue<string>(Columns.McUsername); }
			set { SetColumnValue(Columns.McUsername, value); }
		}
		[DataMember]
		public string McPassword {
			get { return GetColumnValue<string>(Columns.McPassword); }
			set { SetColumnValue(Columns.McPassword, value); }
		}
		[DataMember]
		public DateTime? LastLoginOn {
			get { return GetColumnValue<DateTime?>(Columns.LastLoginOn); }
			set { SetColumnValue(Columns.LastLoginOn, value); }
		}
		[DataMember]
		public bool MduIsActive {
			get { return GetColumnValue<bool>(Columns.MduIsActive); }
			set { SetColumnValue(Columns.MduIsActive, value); }
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
		public static TableSchema.TableColumn HRUserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
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
		public static TableSchema.TableColumn AuIsActiveColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SessionIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn LastAccessedOnColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn DealerUserIDColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn DealerUserTypeIdColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn DealerUserTypeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn DealerIdColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn DealerNameColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn McUserIdColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn FirstnameColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn MiddlenameColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn LastnameColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn PhoneWorkColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn PhoneCellColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn ADUsernameColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn McUsernameColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn McPasswordColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn LastLoginOnColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn MduIsActiveColumn
		{
			get { return Schema.Columns[27]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string HRUserId = @"HRUserId";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string Username = @"Username";
			public const string Password = @"Password";
			public const string AuIsActive = @"AuIsActive";
			public const string SessionID = @"SessionID";
			public const string IPAddress = @"IPAddress";
			public const string CreatedOn = @"CreatedOn";
			public const string LastAccessedOn = @"LastAccessedOn";
			public const string DealerUserID = @"DealerUserID";
			public const string DealerUserTypeId = @"DealerUserTypeId";
			public const string DealerUserType = @"DealerUserType";
			public const string DealerId = @"DealerId";
			public const string DealerName = @"DealerName";
			public const string McUserId = @"McUserId";
			public const string FullName = @"FullName";
			public const string Firstname = @"Firstname";
			public const string Middlename = @"Middlename";
			public const string Lastname = @"Lastname";
			public const string Email = @"Email";
			public const string PhoneWork = @"PhoneWork";
			public const string PhoneCell = @"PhoneCell";
			public const string ADUsername = @"ADUsername";
			public const string McUsername = @"McUsername";
			public const string McPassword = @"McPassword";
			public const string LastLoginOn = @"LastLoginOn";
			public const string MduIsActive = @"MduIsActive";
		}
		#endregion Columns Struct
	}
}
