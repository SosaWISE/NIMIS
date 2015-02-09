


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

namespace SOS.Data.ReceiverEngine
{
	/// <summary>
	/// Strongly-typed collection for the RE_RequestsRaw class.
	/// </summary>
	[DataContract]
	public partial class RE_RequestsRawCollection : ActiveList<RE_RequestsRaw, RE_RequestsRawCollection>
	{
		public static RE_RequestsRawCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RE_RequestsRawCollection result = new RE_RequestsRawCollection();
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
			foreach (RE_RequestsRaw item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the RE_RequestsRaw table.
	/// </summary>
	[DataContract]
	public partial class RE_RequestsRaw : ActiveRecord<RE_RequestsRaw>, INotifyPropertyChanged
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

		public RE_RequestsRaw()
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
				TableSchema.Table schema = new TableSchema.Table("RE_RequestsRaw", TableType.Table, DataService.GetInstance("SosReceiverEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequestID = new TableSchema.TableColumn(schema);
				colvarRequestID.ColumnName = "RequestID";
				colvarRequestID.DataType = DbType.Int64;
				colvarRequestID.MaxLength = 0;
				colvarRequestID.AutoIncrement = true;
				colvarRequestID.IsNullable = false;
				colvarRequestID.IsPrimaryKey = true;
				colvarRequestID.IsForeignKey = false;
				colvarRequestID.IsReadOnly = false;
				colvarRequestID.DefaultSetting = @"";
				colvarRequestID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestID);

				TableSchema.TableColumn colvarRawQS = new TableSchema.TableColumn(schema);
				colvarRawQS.ColumnName = "RawQS";
				colvarRawQS.DataType = DbType.String;
				colvarRawQS.MaxLength = -1;
				colvarRawQS.AutoIncrement = false;
				colvarRawQS.IsNullable = false;
				colvarRawQS.IsPrimaryKey = false;
				colvarRawQS.IsForeignKey = false;
				colvarRawQS.IsReadOnly = false;
				colvarRawQS.DefaultSetting = @"";
				colvarRawQS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRawQS);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"(N'SYSTEM')";
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

				BaseSchema = schema;
				DataService.Providers["SosReceiverEngineProvider"].AddSchema("RE_RequestsRaw",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static RE_RequestsRaw LoadFrom(RE_RequestsRaw item)
		{
			RE_RequestsRaw result = new RE_RequestsRaw();
			if (item.RequestID != default(long)) {
				result.LoadByKey(item.RequestID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long RequestID {
			get { return GetColumnValue<long>(Columns.RequestID); }
			set {
				SetColumnValue(Columns.RequestID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestID));
			}
		}
		[DataMember]
		public string RawQS {
			get { return GetColumnValue<string>(Columns.RawQS); }
			set {
				SetColumnValue(Columns.RawQS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RawQS));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
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

		#endregion //Properties


		public override string ToString()
		{
			return RawQS;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RawQSColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequestID = @"RequestID";
			public static readonly string RawQS = @"RawQS";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequestID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the RE_TxtWireRequest class.
	/// </summary>
	[DataContract]
	public partial class RE_TxtWireRequestCollection : ActiveList<RE_TxtWireRequest, RE_TxtWireRequestCollection>
	{
		public static RE_TxtWireRequestCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RE_TxtWireRequestCollection result = new RE_TxtWireRequestCollection();
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
			foreach (RE_TxtWireRequest item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the RE_TxtWireRequests table.
	/// </summary>
	[DataContract]
	public partial class RE_TxtWireRequest : ActiveRecord<RE_TxtWireRequest>, INotifyPropertyChanged
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

		public RE_TxtWireRequest()
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
				TableSchema.Table schema = new TableSchema.Table("RE_TxtWireRequests", TableType.Table, DataService.GetInstance("SosReceiverEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTxtWireRequestID = new TableSchema.TableColumn(schema);
				colvarTxtWireRequestID.ColumnName = "TxtWireRequestID";
				colvarTxtWireRequestID.DataType = DbType.Int64;
				colvarTxtWireRequestID.MaxLength = 0;
				colvarTxtWireRequestID.AutoIncrement = true;
				colvarTxtWireRequestID.IsNullable = false;
				colvarTxtWireRequestID.IsPrimaryKey = true;
				colvarTxtWireRequestID.IsForeignKey = false;
				colvarTxtWireRequestID.IsReadOnly = false;
				colvarTxtWireRequestID.DefaultSetting = @"";
				colvarTxtWireRequestID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTxtWireRequestID);

				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
				colvarTitle.DataType = DbType.String;
				colvarTitle.MaxLength = 500;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = true;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				colvarTitle.DefaultSetting = @"";
				colvarTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitle);

				TableSchema.TableColumn colvarCode = new TableSchema.TableColumn(schema);
				colvarCode.ColumnName = "Code";
				colvarCode.DataType = DbType.String;
				colvarCode.MaxLength = 50;
				colvarCode.AutoIncrement = false;
				colvarCode.IsNullable = true;
				colvarCode.IsPrimaryKey = false;
				colvarCode.IsForeignKey = false;
				colvarCode.IsReadOnly = false;
				colvarCode.DefaultSetting = @"";
				colvarCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCode);

				TableSchema.TableColumn colvarShortCode = new TableSchema.TableColumn(schema);
				colvarShortCode.ColumnName = "ShortCode";
				colvarShortCode.DataType = DbType.AnsiString;
				colvarShortCode.MaxLength = 50;
				colvarShortCode.AutoIncrement = false;
				colvarShortCode.IsNullable = true;
				colvarShortCode.IsPrimaryKey = false;
				colvarShortCode.IsForeignKey = false;
				colvarShortCode.IsReadOnly = false;
				colvarShortCode.DefaultSetting = @"";
				colvarShortCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShortCode);

				TableSchema.TableColumn colvarMessage = new TableSchema.TableColumn(schema);
				colvarMessage.ColumnName = "Message";
				colvarMessage.DataType = DbType.String;
				colvarMessage.MaxLength = 300;
				colvarMessage.AutoIncrement = false;
				colvarMessage.IsNullable = true;
				colvarMessage.IsPrimaryKey = false;
				colvarMessage.IsForeignKey = false;
				colvarMessage.IsReadOnly = false;
				colvarMessage.DefaultSetting = @"";
				colvarMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessage);

				TableSchema.TableColumn colvarPhone = new TableSchema.TableColumn(schema);
				colvarPhone.ColumnName = "Phone";
				colvarPhone.DataType = DbType.String;
				colvarPhone.MaxLength = 30;
				colvarPhone.AutoIncrement = false;
				colvarPhone.IsNullable = true;
				colvarPhone.IsPrimaryKey = false;
				colvarPhone.IsForeignKey = false;
				colvarPhone.IsReadOnly = false;
				colvarPhone.DefaultSetting = @"";
				colvarPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone);

				TableSchema.TableColumn colvarCarrier = new TableSchema.TableColumn(schema);
				colvarCarrier.ColumnName = "Carrier";
				colvarCarrier.DataType = DbType.String;
				colvarCarrier.MaxLength = 50;
				colvarCarrier.AutoIncrement = false;
				colvarCarrier.IsNullable = true;
				colvarCarrier.IsPrimaryKey = false;
				colvarCarrier.IsForeignKey = false;
				colvarCarrier.IsReadOnly = false;
				colvarCarrier.DefaultSetting = @"";
				colvarCarrier.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCarrier);

				TableSchema.TableColumn colvarKeyword = new TableSchema.TableColumn(schema);
				colvarKeyword.ColumnName = "Keyword";
				colvarKeyword.DataType = DbType.String;
				colvarKeyword.MaxLength = 50;
				colvarKeyword.AutoIncrement = false;
				colvarKeyword.IsNullable = true;
				colvarKeyword.IsPrimaryKey = false;
				colvarKeyword.IsForeignKey = false;
				colvarKeyword.IsReadOnly = false;
				colvarKeyword.DefaultSetting = @"";
				colvarKeyword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKeyword);

				TableSchema.TableColumn colvarGroupName = new TableSchema.TableColumn(schema);
				colvarGroupName.ColumnName = "GroupName";
				colvarGroupName.DataType = DbType.String;
				colvarGroupName.MaxLength = 50;
				colvarGroupName.AutoIncrement = false;
				colvarGroupName.IsNullable = true;
				colvarGroupName.IsPrimaryKey = false;
				colvarGroupName.IsForeignKey = false;
				colvarGroupName.IsReadOnly = false;
				colvarGroupName.DefaultSetting = @"";
				colvarGroupName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupName);

				TableSchema.TableColumn colvarCustomTicket = new TableSchema.TableColumn(schema);
				colvarCustomTicket.ColumnName = "CustomTicket";
				colvarCustomTicket.DataType = DbType.String;
				colvarCustomTicket.MaxLength = 500;
				colvarCustomTicket.AutoIncrement = false;
				colvarCustomTicket.IsNullable = true;
				colvarCustomTicket.IsPrimaryKey = false;
				colvarCustomTicket.IsForeignKey = false;
				colvarCustomTicket.IsReadOnly = false;
				colvarCustomTicket.DefaultSetting = @"";
				colvarCustomTicket.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomTicket);

				TableSchema.TableColumn colvarDefaultKeyword = new TableSchema.TableColumn(schema);
				colvarDefaultKeyword.ColumnName = "DefaultKeyword";
				colvarDefaultKeyword.DataType = DbType.String;
				colvarDefaultKeyword.MaxLength = 50;
				colvarDefaultKeyword.AutoIncrement = false;
				colvarDefaultKeyword.IsNullable = true;
				colvarDefaultKeyword.IsPrimaryKey = false;
				colvarDefaultKeyword.IsForeignKey = false;
				colvarDefaultKeyword.IsReadOnly = false;
				colvarDefaultKeyword.DefaultSetting = @"";
				colvarDefaultKeyword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDefaultKeyword);

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

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 50;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = true;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarApiKey = new TableSchema.TableColumn(schema);
				colvarApiKey.ColumnName = "ApiKey";
				colvarApiKey.DataType = DbType.String;
				colvarApiKey.MaxLength = 50;
				colvarApiKey.AutoIncrement = false;
				colvarApiKey.IsNullable = true;
				colvarApiKey.IsPrimaryKey = false;
				colvarApiKey.IsForeignKey = false;
				colvarApiKey.IsReadOnly = false;
				colvarApiKey.DefaultSetting = @"";
				colvarApiKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApiKey);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"(N'SYSTEM')";
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

				BaseSchema = schema;
				DataService.Providers["SosReceiverEngineProvider"].AddSchema("RE_TxtWireRequests",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static RE_TxtWireRequest LoadFrom(RE_TxtWireRequest item)
		{
			RE_TxtWireRequest result = new RE_TxtWireRequest();
			if (item.TxtWireRequestID != default(long)) {
				result.LoadByKey(item.TxtWireRequestID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long TxtWireRequestID {
			get { return GetColumnValue<long>(Columns.TxtWireRequestID); }
			set {
				SetColumnValue(Columns.TxtWireRequestID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TxtWireRequestID));
			}
		}
		[DataMember]
		public string Title {
			get { return GetColumnValue<string>(Columns.Title); }
			set {
				SetColumnValue(Columns.Title, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Title));
			}
		}
		[DataMember]
		public string Code {
			get { return GetColumnValue<string>(Columns.Code); }
			set {
				SetColumnValue(Columns.Code, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Code));
			}
		}
		[DataMember]
		public string ShortCode {
			get { return GetColumnValue<string>(Columns.ShortCode); }
			set {
				SetColumnValue(Columns.ShortCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ShortCode));
			}
		}
		[DataMember]
		public string Message {
			get { return GetColumnValue<string>(Columns.Message); }
			set {
				SetColumnValue(Columns.Message, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Message));
			}
		}
		[DataMember]
		public string Phone {
			get { return GetColumnValue<string>(Columns.Phone); }
			set {
				SetColumnValue(Columns.Phone, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Phone));
			}
		}
		[DataMember]
		public string Carrier {
			get { return GetColumnValue<string>(Columns.Carrier); }
			set {
				SetColumnValue(Columns.Carrier, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Carrier));
			}
		}
		[DataMember]
		public string Keyword {
			get { return GetColumnValue<string>(Columns.Keyword); }
			set {
				SetColumnValue(Columns.Keyword, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Keyword));
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
		public string CustomTicket {
			get { return GetColumnValue<string>(Columns.CustomTicket); }
			set {
				SetColumnValue(Columns.CustomTicket, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CustomTicket));
			}
		}
		[DataMember]
		public string DefaultKeyword {
			get { return GetColumnValue<string>(Columns.DefaultKeyword); }
			set {
				SetColumnValue(Columns.DefaultKeyword, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DefaultKeyword));
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
		public string ApiKey {
			get { return GetColumnValue<string>(Columns.ApiKey); }
			set {
				SetColumnValue(Columns.ApiKey, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApiKey));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
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

		#endregion //Properties


		public override string ToString()
		{
			return Title;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TxtWireRequestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TitleColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CodeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ShortCodeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MessageColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PhoneColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CarrierColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn KeywordColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn GroupNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CustomTicketColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn DefaultKeywordColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn UsernameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn ApiKeyColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[15]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TxtWireRequestID = @"TxtWireRequestID";
			public static readonly string Title = @"Title";
			public static readonly string Code = @"Code";
			public static readonly string ShortCode = @"ShortCode";
			public static readonly string Message = @"Message";
			public static readonly string Phone = @"Phone";
			public static readonly string Carrier = @"Carrier";
			public static readonly string Keyword = @"Keyword";
			public static readonly string GroupName = @"GroupName";
			public static readonly string CustomTicket = @"CustomTicket";
			public static readonly string DefaultKeyword = @"DefaultKeyword";
			public static readonly string Username = @"Username";
			public static readonly string Password = @"Password";
			public static readonly string ApiKey = @"ApiKey";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return TxtWireRequestID; }
		}
		*/


	}
}
