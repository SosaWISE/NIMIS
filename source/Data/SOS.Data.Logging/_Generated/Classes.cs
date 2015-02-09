


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

namespace SOS.Data.Logging
{
	/// <summary>
	/// Strongly-typed collection for the LG_ChangeLog class.
	/// </summary>
	[DataContract]
	public partial class LG_ChangeLogCollection : ActiveList<LG_ChangeLog, LG_ChangeLogCollection>
	{
		public static LG_ChangeLogCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_ChangeLogCollection result = new LG_ChangeLogCollection();
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
			foreach (LG_ChangeLog item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_ChangeLogs table.
	/// </summary>
	[DataContract]
	public partial class LG_ChangeLog : ActiveRecord<LG_ChangeLog>, INotifyPropertyChanged
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

		public LG_ChangeLog()
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
				TableSchema.Table schema = new TableSchema.Table("LG_ChangeLogs", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarChangeLogID = new TableSchema.TableColumn(schema);
				colvarChangeLogID.ColumnName = "ChangeLogID";
				colvarChangeLogID.DataType = DbType.Int64;
				colvarChangeLogID.MaxLength = 0;
				colvarChangeLogID.AutoIncrement = true;
				colvarChangeLogID.IsNullable = false;
				colvarChangeLogID.IsPrimaryKey = true;
				colvarChangeLogID.IsForeignKey = false;
				colvarChangeLogID.IsReadOnly = false;
				colvarChangeLogID.DefaultSetting = @"";
				colvarChangeLogID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChangeLogID);

				TableSchema.TableColumn colvarChangeLogTypeId = new TableSchema.TableColumn(schema);
				colvarChangeLogTypeId.ColumnName = "ChangeLogTypeId";
				colvarChangeLogTypeId.DataType = DbType.Int32;
				colvarChangeLogTypeId.MaxLength = 0;
				colvarChangeLogTypeId.AutoIncrement = false;
				colvarChangeLogTypeId.IsNullable = false;
				colvarChangeLogTypeId.IsPrimaryKey = false;
				colvarChangeLogTypeId.IsForeignKey = true;
				colvarChangeLogTypeId.IsReadOnly = false;
				colvarChangeLogTypeId.DefaultSetting = @"";
				colvarChangeLogTypeId.ForeignKeyTableName = "LG_ChangeLogTypes";
				schema.Columns.Add(colvarChangeLogTypeId);

				TableSchema.TableColumn colvarLogSourceId = new TableSchema.TableColumn(schema);
				colvarLogSourceId.ColumnName = "LogSourceId";
				colvarLogSourceId.DataType = DbType.Int32;
				colvarLogSourceId.MaxLength = 0;
				colvarLogSourceId.AutoIncrement = false;
				colvarLogSourceId.IsNullable = false;
				colvarLogSourceId.IsPrimaryKey = false;
				colvarLogSourceId.IsForeignKey = true;
				colvarLogSourceId.IsReadOnly = false;
				colvarLogSourceId.DefaultSetting = @"";
				colvarLogSourceId.ForeignKeyTableName = "LG_LogSources";
				schema.Columns.Add(colvarLogSourceId);

				TableSchema.TableColumn colvarTargetDatabase = new TableSchema.TableColumn(schema);
				colvarTargetDatabase.ColumnName = "TargetDatabase";
				colvarTargetDatabase.DataType = DbType.String;
				colvarTargetDatabase.MaxLength = 128;
				colvarTargetDatabase.AutoIncrement = false;
				colvarTargetDatabase.IsNullable = true;
				colvarTargetDatabase.IsPrimaryKey = false;
				colvarTargetDatabase.IsForeignKey = false;
				colvarTargetDatabase.IsReadOnly = false;
				colvarTargetDatabase.DefaultSetting = @"";
				colvarTargetDatabase.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetDatabase);

				TableSchema.TableColumn colvarTargetSchema = new TableSchema.TableColumn(schema);
				colvarTargetSchema.ColumnName = "TargetSchema";
				colvarTargetSchema.DataType = DbType.String;
				colvarTargetSchema.MaxLength = 128;
				colvarTargetSchema.AutoIncrement = false;
				colvarTargetSchema.IsNullable = true;
				colvarTargetSchema.IsPrimaryKey = false;
				colvarTargetSchema.IsForeignKey = false;
				colvarTargetSchema.IsReadOnly = false;
				colvarTargetSchema.DefaultSetting = @"";
				colvarTargetSchema.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetSchema);

				TableSchema.TableColumn colvarTargetTable = new TableSchema.TableColumn(schema);
				colvarTargetTable.ColumnName = "TargetTable";
				colvarTargetTable.DataType = DbType.String;
				colvarTargetTable.MaxLength = 128;
				colvarTargetTable.AutoIncrement = false;
				colvarTargetTable.IsNullable = true;
				colvarTargetTable.IsPrimaryKey = false;
				colvarTargetTable.IsForeignKey = false;
				colvarTargetTable.IsReadOnly = false;
				colvarTargetTable.DefaultSetting = @"";
				colvarTargetTable.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetTable);

				TableSchema.TableColumn colvarTargetPrimaryKey = new TableSchema.TableColumn(schema);
				colvarTargetPrimaryKey.ColumnName = "TargetPrimaryKey";
				colvarTargetPrimaryKey.DataType = DbType.Int32;
				colvarTargetPrimaryKey.MaxLength = 0;
				colvarTargetPrimaryKey.AutoIncrement = false;
				colvarTargetPrimaryKey.IsNullable = false;
				colvarTargetPrimaryKey.IsPrimaryKey = false;
				colvarTargetPrimaryKey.IsForeignKey = false;
				colvarTargetPrimaryKey.IsReadOnly = false;
				colvarTargetPrimaryKey.DefaultSetting = @"";
				colvarTargetPrimaryKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetPrimaryKey);

				TableSchema.TableColumn colvarSourceView = new TableSchema.TableColumn(schema);
				colvarSourceView.ColumnName = "SourceView";
				colvarSourceView.DataType = DbType.String;
				colvarSourceView.MaxLength = 255;
				colvarSourceView.AutoIncrement = false;
				colvarSourceView.IsNullable = false;
				colvarSourceView.IsPrimaryKey = false;
				colvarSourceView.IsForeignKey = false;
				colvarSourceView.IsReadOnly = false;
				colvarSourceView.DefaultSetting = @"";
				colvarSourceView.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSourceView);

				TableSchema.TableColumn colvarRequestedByID = new TableSchema.TableColumn(schema);
				colvarRequestedByID.ColumnName = "RequestedByID";
				colvarRequestedByID.DataType = DbType.String;
				colvarRequestedByID.MaxLength = 50;
				colvarRequestedByID.AutoIncrement = false;
				colvarRequestedByID.IsNullable = true;
				colvarRequestedByID.IsPrimaryKey = false;
				colvarRequestedByID.IsForeignKey = false;
				colvarRequestedByID.IsReadOnly = false;
				colvarRequestedByID.DefaultSetting = @"";
				colvarRequestedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestedByID);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.String;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"(N'SYSTEM')";
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
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_ChangeLogs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_ChangeLog LoadFrom(LG_ChangeLog item)
		{
			LG_ChangeLog result = new LG_ChangeLog();
			if (item.ChangeLogID != default(long)) {
				result.LoadByKey(item.ChangeLogID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ChangeLogID {
			get { return GetColumnValue<long>(Columns.ChangeLogID); }
			set {
				SetColumnValue(Columns.ChangeLogID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeLogID));
			}
		}
		[DataMember]
		public int ChangeLogTypeId {
			get { return GetColumnValue<int>(Columns.ChangeLogTypeId); }
			set {
				SetColumnValue(Columns.ChangeLogTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeLogTypeId));
			}
		}
		[DataMember]
		public int LogSourceId {
			get { return GetColumnValue<int>(Columns.LogSourceId); }
			set {
				SetColumnValue(Columns.LogSourceId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LogSourceId));
			}
		}
		[DataMember]
		public string TargetDatabase {
			get { return GetColumnValue<string>(Columns.TargetDatabase); }
			set {
				SetColumnValue(Columns.TargetDatabase, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetDatabase));
			}
		}
		[DataMember]
		public string TargetSchema {
			get { return GetColumnValue<string>(Columns.TargetSchema); }
			set {
				SetColumnValue(Columns.TargetSchema, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetSchema));
			}
		}
		[DataMember]
		public string TargetTable {
			get { return GetColumnValue<string>(Columns.TargetTable); }
			set {
				SetColumnValue(Columns.TargetTable, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetTable));
			}
		}
		[DataMember]
		public int TargetPrimaryKey {
			get { return GetColumnValue<int>(Columns.TargetPrimaryKey); }
			set {
				SetColumnValue(Columns.TargetPrimaryKey, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetPrimaryKey));
			}
		}
		[DataMember]
		public string SourceView {
			get { return GetColumnValue<string>(Columns.SourceView); }
			set {
				SetColumnValue(Columns.SourceView, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SourceView));
			}
		}
		[DataMember]
		public string RequestedByID {
			get { return GetColumnValue<string>(Columns.RequestedByID); }
			set {
				SetColumnValue(Columns.RequestedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestedByID));
			}
		}
		[DataMember]
		public string ModifiedBy {
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
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

		private LG_ChangeLogType _ChangeLogType;
		//Relationship: FK_LG_ChangeLogs_LG_ChangeLogTypes
		public LG_ChangeLogType ChangeLogType
		{
			get
			{
				if(_ChangeLogType == null) {
					_ChangeLogType = LG_ChangeLogType.FetchByID(this.ChangeLogTypeId);
				}
				return _ChangeLogType;
			}
			set
			{
				SetColumnValue("ChangeLogTypeId", value.ChangeLogTypeID);
				_ChangeLogType = value;
			}
		}

		private LG_LogSource _LogSource;
		//Relationship: FK_LG_ChangeLogs_LG_LogSources
		public LG_LogSource LogSource
		{
			get
			{
				if(_LogSource == null) {
					_LogSource = LG_LogSource.FetchByID(this.LogSourceId);
				}
				return _LogSource;
			}
			set
			{
				SetColumnValue("LogSourceId", value.LogSourceID);
				_LogSource = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ChangeLogID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ChangeLogIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ChangeLogTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LogSourceIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TargetDatabaseColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TargetSchemaColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TargetTableColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TargetPrimaryKeyColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SourceViewColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn RequestedByIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ChangeLogID = @"ChangeLogID";
			public static readonly string ChangeLogTypeId = @"ChangeLogTypeId";
			public static readonly string LogSourceId = @"LogSourceId";
			public static readonly string TargetDatabase = @"TargetDatabase";
			public static readonly string TargetSchema = @"TargetSchema";
			public static readonly string TargetTable = @"TargetTable";
			public static readonly string TargetPrimaryKey = @"TargetPrimaryKey";
			public static readonly string SourceView = @"SourceView";
			public static readonly string RequestedByID = @"RequestedByID";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ChangeLogID; }
		}
		*/

		#region Foreign Collections

		private LG_ChangeCollection _LG_ChangesCol;
		//Relationship: FK_LG_Changes_LG_ChangeLogs
		public LG_ChangeCollection LG_ChangesCol
		{
			get
			{
				if(_LG_ChangesCol == null) {
					_LG_ChangesCol = new LG_ChangeCollection();
					_LG_ChangesCol.LoadAndCloseReader(LG_Change.Query()
						.WHERE(LG_Change.Columns.ChangeLogId, ChangeLogID).ExecuteReader());
				}
				return _LG_ChangesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LG_ChangeLogType class.
	/// </summary>
	[DataContract]
	public partial class LG_ChangeLogTypeCollection : ActiveList<LG_ChangeLogType, LG_ChangeLogTypeCollection>
	{
		public static LG_ChangeLogTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_ChangeLogTypeCollection result = new LG_ChangeLogTypeCollection();
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
			foreach (LG_ChangeLogType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_ChangeLogTypes table.
	/// </summary>
	[DataContract]
	public partial class LG_ChangeLogType : ActiveRecord<LG_ChangeLogType>, INotifyPropertyChanged
	{
		#region Enum

		[DataContract]
		public enum ChangeLogTypeEnum : int
		{
		}

		//[DataMember]
		//public ChangeLogTypeEnum ChangeLogTypeCode
		//{
		//	get { return (ChangeLogTypeEnum)ChangeLogTypeID; }
		//	set { ChangeLogTypeID = (int)value; }
		//}

		#endregion //Enum


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LG_ChangeLogType()
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
				TableSchema.Table schema = new TableSchema.Table("LG_ChangeLogTypes", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarChangeLogTypeID = new TableSchema.TableColumn(schema);
				colvarChangeLogTypeID.ColumnName = "ChangeLogTypeID";
				colvarChangeLogTypeID.DataType = DbType.Int32;
				colvarChangeLogTypeID.MaxLength = 0;
				colvarChangeLogTypeID.AutoIncrement = false;
				colvarChangeLogTypeID.IsNullable = false;
				colvarChangeLogTypeID.IsPrimaryKey = true;
				colvarChangeLogTypeID.IsForeignKey = false;
				colvarChangeLogTypeID.IsReadOnly = false;
				colvarChangeLogTypeID.DefaultSetting = @"";
				colvarChangeLogTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChangeLogTypeID);

				TableSchema.TableColumn colvarChangeLogType = new TableSchema.TableColumn(schema);
				colvarChangeLogType.ColumnName = "ChangeLogType";
				colvarChangeLogType.DataType = DbType.String;
				colvarChangeLogType.MaxLength = 50;
				colvarChangeLogType.AutoIncrement = false;
				colvarChangeLogType.IsNullable = false;
				colvarChangeLogType.IsPrimaryKey = false;
				colvarChangeLogType.IsForeignKey = false;
				colvarChangeLogType.IsReadOnly = false;
				colvarChangeLogType.DefaultSetting = @"";
				colvarChangeLogType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChangeLogType);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_ChangeLogTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_ChangeLogType LoadFrom(LG_ChangeLogType item)
		{
			LG_ChangeLogType result = new LG_ChangeLogType();
			if (item.ChangeLogTypeID != default(int)) {
				result.LoadByKey(item.ChangeLogTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int ChangeLogTypeID {
			get { return GetColumnValue<int>(Columns.ChangeLogTypeID); }
			set {
				SetColumnValue(Columns.ChangeLogTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeLogTypeID));
			}
		}
		[DataMember]
		public string ChangeLogType {
			get { return GetColumnValue<string>(Columns.ChangeLogType); }
			set {
				SetColumnValue(Columns.ChangeLogType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeLogType));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return ChangeLogType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ChangeLogTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ChangeLogTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ChangeLogTypeID = @"ChangeLogTypeID";
			public static readonly string ChangeLogType = @"ChangeLogType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ChangeLogTypeID; }
		}
		*/

		#region Foreign Collections

		private LG_ChangeLogCollection _LG_ChangeLogsCol;
		//Relationship: FK_LG_ChangeLogs_LG_ChangeLogTypes
		public LG_ChangeLogCollection LG_ChangeLogsCol
		{
			get
			{
				if(_LG_ChangeLogsCol == null) {
					_LG_ChangeLogsCol = new LG_ChangeLogCollection();
					_LG_ChangeLogsCol.LoadAndCloseReader(LG_ChangeLog.Query()
						.WHERE(LG_ChangeLog.Columns.ChangeLogTypeId, ChangeLogTypeID).ExecuteReader());
				}
				return _LG_ChangeLogsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LG_Change class.
	/// </summary>
	[DataContract]
	public partial class LG_ChangeCollection : ActiveList<LG_Change, LG_ChangeCollection>
	{
		public static LG_ChangeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_ChangeCollection result = new LG_ChangeCollection();
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
			foreach (LG_Change item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_Changes table.
	/// </summary>
	[DataContract]
	public partial class LG_Change : ActiveRecord<LG_Change>, INotifyPropertyChanged
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

		public LG_Change()
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
				TableSchema.Table schema = new TableSchema.Table("LG_Changes", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarChangeID = new TableSchema.TableColumn(schema);
				colvarChangeID.ColumnName = "ChangeID";
				colvarChangeID.DataType = DbType.Int64;
				colvarChangeID.MaxLength = 0;
				colvarChangeID.AutoIncrement = true;
				colvarChangeID.IsNullable = false;
				colvarChangeID.IsPrimaryKey = true;
				colvarChangeID.IsForeignKey = false;
				colvarChangeID.IsReadOnly = false;
				colvarChangeID.DefaultSetting = @"";
				colvarChangeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChangeID);

				TableSchema.TableColumn colvarChangeLogId = new TableSchema.TableColumn(schema);
				colvarChangeLogId.ColumnName = "ChangeLogId";
				colvarChangeLogId.DataType = DbType.Int64;
				colvarChangeLogId.MaxLength = 0;
				colvarChangeLogId.AutoIncrement = false;
				colvarChangeLogId.IsNullable = false;
				colvarChangeLogId.IsPrimaryKey = false;
				colvarChangeLogId.IsForeignKey = true;
				colvarChangeLogId.IsReadOnly = false;
				colvarChangeLogId.DefaultSetting = @"";
				colvarChangeLogId.ForeignKeyTableName = "LG_ChangeLogs";
				schema.Columns.Add(colvarChangeLogId);

				TableSchema.TableColumn colvarTargetColumn = new TableSchema.TableColumn(schema);
				colvarTargetColumn.ColumnName = "TargetColumn";
				colvarTargetColumn.DataType = DbType.String;
				colvarTargetColumn.MaxLength = 128;
				colvarTargetColumn.AutoIncrement = false;
				colvarTargetColumn.IsNullable = true;
				colvarTargetColumn.IsPrimaryKey = false;
				colvarTargetColumn.IsForeignKey = false;
				colvarTargetColumn.IsReadOnly = false;
				colvarTargetColumn.DefaultSetting = @"";
				colvarTargetColumn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetColumn);

				TableSchema.TableColumn colvarOldValue = new TableSchema.TableColumn(schema);
				colvarOldValue.ColumnName = "OldValue";
				colvarOldValue.DataType = DbType.String;
				colvarOldValue.MaxLength = 1024;
				colvarOldValue.AutoIncrement = false;
				colvarOldValue.IsNullable = true;
				colvarOldValue.IsPrimaryKey = false;
				colvarOldValue.IsForeignKey = false;
				colvarOldValue.IsReadOnly = false;
				colvarOldValue.DefaultSetting = @"";
				colvarOldValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOldValue);

				TableSchema.TableColumn colvarNewValue = new TableSchema.TableColumn(schema);
				colvarNewValue.ColumnName = "NewValue";
				colvarNewValue.DataType = DbType.String;
				colvarNewValue.MaxLength = 1024;
				colvarNewValue.AutoIncrement = false;
				colvarNewValue.IsNullable = true;
				colvarNewValue.IsPrimaryKey = false;
				colvarNewValue.IsForeignKey = false;
				colvarNewValue.IsReadOnly = false;
				colvarNewValue.DefaultSetting = @"";
				colvarNewValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNewValue);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_Changes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_Change LoadFrom(LG_Change item)
		{
			LG_Change result = new LG_Change();
			if (item.ChangeID != default(long)) {
				result.LoadByKey(item.ChangeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ChangeID {
			get { return GetColumnValue<long>(Columns.ChangeID); }
			set {
				SetColumnValue(Columns.ChangeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeID));
			}
		}
		[DataMember]
		public long ChangeLogId {
			get { return GetColumnValue<long>(Columns.ChangeLogId); }
			set {
				SetColumnValue(Columns.ChangeLogId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ChangeLogId));
			}
		}
		[DataMember]
		public string TargetColumn {
			get { return GetColumnValue<string>(Columns.TargetColumn); }
			set {
				SetColumnValue(Columns.TargetColumn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetColumn));
			}
		}
		[DataMember]
		public string OldValue {
			get { return GetColumnValue<string>(Columns.OldValue); }
			set {
				SetColumnValue(Columns.OldValue, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OldValue));
			}
		}
		[DataMember]
		public string NewValue {
			get { return GetColumnValue<string>(Columns.NewValue); }
			set {
				SetColumnValue(Columns.NewValue, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NewValue));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LG_ChangeLog _ChangeLog;
		//Relationship: FK_LG_Changes_LG_ChangeLogs
		public LG_ChangeLog ChangeLog
		{
			get
			{
				if(_ChangeLog == null) {
					_ChangeLog = LG_ChangeLog.FetchByID(this.ChangeLogId);
				}
				return _ChangeLog;
			}
			set
			{
				SetColumnValue("ChangeLogId", value.ChangeLogID);
				_ChangeLog = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ChangeID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ChangeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ChangeLogIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TargetColumnColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn OldValueColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn NewValueColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ChangeID = @"ChangeID";
			public static readonly string ChangeLogId = @"ChangeLogId";
			public static readonly string TargetColumn = @"TargetColumn";
			public static readonly string OldValue = @"OldValue";
			public static readonly string NewValue = @"NewValue";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ChangeID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LG_LogSource class.
	/// </summary>
	[DataContract]
	public partial class LG_LogSourceCollection : ActiveList<LG_LogSource, LG_LogSourceCollection>
	{
		public static LG_LogSourceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_LogSourceCollection result = new LG_LogSourceCollection();
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
			foreach (LG_LogSource item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_LogSources table.
	/// </summary>
	[DataContract]
	public partial class LG_LogSource : ActiveRecord<LG_LogSource>, INotifyPropertyChanged
	{
		#region Enum

		[DataContract]
		public enum LogSourceEnum : int
		{
			[EnumMember()] DefaultX = 0,
			[EnumMember()] SOSServicesWcfCrm = 1,
			[EnumMember()] SOSClientsJSCorpSite = 2,
			[EnumMember()] SOSClientsJSDealerSite = 3,
			[EnumMember()] SOSClientsMVCDealerSite = 4,
			[EnumMember()] SOSClientsMVCCorpSite = 5,
			[EnumMember()] SOSClientsMVCCorpSite2 = 6,
			[EnumMember()] SOSClientsConsoleLaipacSocketServer = 7,
			[EnumMember()] NXSServicesCmsCORS = 8,
			[EnumMember()] NSEFOSRunCreditServices = 9,
			[EnumMember()] NXSClientsWpfLicensingManager = 10,
		}

		//[DataMember]
		//public LogSourceEnum LogSourceCode
		//{
		//	get { return (LogSourceEnum)LogSourceID; }
		//	set { LogSourceID = (int)value; }
		//}

		#endregion //Enum


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LG_LogSource()
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
				TableSchema.Table schema = new TableSchema.Table("LG_LogSources", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLogSourceID = new TableSchema.TableColumn(schema);
				colvarLogSourceID.ColumnName = "LogSourceID";
				colvarLogSourceID.DataType = DbType.Int32;
				colvarLogSourceID.MaxLength = 0;
				colvarLogSourceID.AutoIncrement = false;
				colvarLogSourceID.IsNullable = false;
				colvarLogSourceID.IsPrimaryKey = true;
				colvarLogSourceID.IsForeignKey = false;
				colvarLogSourceID.IsReadOnly = false;
				colvarLogSourceID.DefaultSetting = @"";
				colvarLogSourceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogSourceID);

				TableSchema.TableColumn colvarLogSource = new TableSchema.TableColumn(schema);
				colvarLogSource.ColumnName = "LogSource";
				colvarLogSource.DataType = DbType.String;
				colvarLogSource.MaxLength = 50;
				colvarLogSource.AutoIncrement = false;
				colvarLogSource.IsNullable = false;
				colvarLogSource.IsPrimaryKey = false;
				colvarLogSource.IsForeignKey = false;
				colvarLogSource.IsReadOnly = false;
				colvarLogSource.DefaultSetting = @"";
				colvarLogSource.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogSource);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_LogSources",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_LogSource LoadFrom(LG_LogSource item)
		{
			LG_LogSource result = new LG_LogSource();
			if (item.LogSourceID != default(int)) {
				result.LoadByKey(item.LogSourceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LogSourceID {
			get { return GetColumnValue<int>(Columns.LogSourceID); }
			set {
				SetColumnValue(Columns.LogSourceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LogSourceID));
			}
		}
		[DataMember]
		public string LogSource {
			get { return GetColumnValue<string>(Columns.LogSource); }
			set {
				SetColumnValue(Columns.LogSource, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LogSource));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return LogSource;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LogSourceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LogSourceColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LogSourceID = @"LogSourceID";
			public static readonly string LogSource = @"LogSource";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LogSourceID; }
		}
		*/

		#region Foreign Collections

		private LG_ChangeLogCollection _LG_ChangeLogsCol;
		//Relationship: FK_LG_ChangeLogs_LG_LogSources
		public LG_ChangeLogCollection LG_ChangeLogsCol
		{
			get
			{
				if(_LG_ChangeLogsCol == null) {
					_LG_ChangeLogsCol = new LG_ChangeLogCollection();
					_LG_ChangeLogsCol.LoadAndCloseReader(LG_ChangeLog.Query()
						.WHERE(LG_ChangeLog.Columns.LogSourceId, LogSourceID).ExecuteReader());
				}
				return _LG_ChangeLogsCol;
			}
		}

		private LG_MessageCollection _LG_MessagesCol;
		//Relationship: FK_LG_Messages_LG_LogSources
		public LG_MessageCollection LG_MessagesCol
		{
			get
			{
				if(_LG_MessagesCol == null) {
					_LG_MessagesCol = new LG_MessageCollection();
					_LG_MessagesCol.LoadAndCloseReader(LG_Message.Query()
						.WHERE(LG_Message.Columns.LogSourceID, LogSourceID).ExecuteReader());
				}
				return _LG_MessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LG_ManagerOverride class.
	/// </summary>
	[DataContract]
	public partial class LG_ManagerOverrideCollection : ActiveList<LG_ManagerOverride, LG_ManagerOverrideCollection>
	{
		public static LG_ManagerOverrideCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_ManagerOverrideCollection result = new LG_ManagerOverrideCollection();
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
			foreach (LG_ManagerOverride item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_ManagerOverrides table.
	/// </summary>
	[DataContract]
	public partial class LG_ManagerOverride : ActiveRecord<LG_ManagerOverride>, INotifyPropertyChanged
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

		public LG_ManagerOverride()
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
				TableSchema.Table schema = new TableSchema.Table("LG_ManagerOverrides", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarManagerOverrideId = new TableSchema.TableColumn(schema);
				colvarManagerOverrideId.ColumnName = "ManagerOverrideId";
				colvarManagerOverrideId.DataType = DbType.Int32;
				colvarManagerOverrideId.MaxLength = 0;
				colvarManagerOverrideId.AutoIncrement = false;
				colvarManagerOverrideId.IsNullable = false;
				colvarManagerOverrideId.IsPrimaryKey = false;
				colvarManagerOverrideId.IsForeignKey = false;
				colvarManagerOverrideId.IsReadOnly = false;
				colvarManagerOverrideId.DefaultSetting = @"";
				colvarManagerOverrideId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerOverrideId);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_ManagerOverrides",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public int ManagerOverrideId {
			get { return GetColumnValue<int>(Columns.ManagerOverrideId); }
			set {
				SetColumnValue(Columns.ManagerOverrideId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ManagerOverrideId));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return ManagerOverrideId.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ManagerOverrideIdColumn
		{
			get { return Schema.Columns[0]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ManagerOverrideId = @"ManagerOverrideId";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return null; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LG_Message class.
	/// </summary>
	[DataContract]
	public partial class LG_MessageCollection : ActiveList<LG_Message, LG_MessageCollection>
	{
		public static LG_MessageCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_MessageCollection result = new LG_MessageCollection();
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
			foreach (LG_Message item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_Messages table.
	/// </summary>
	[DataContract]
	public partial class LG_Message : ActiveRecord<LG_Message>, INotifyPropertyChanged
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

		public LG_Message()
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
				TableSchema.Table schema = new TableSchema.Table("LG_Messages", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarMessageID = new TableSchema.TableColumn(schema);
				colvarMessageID.ColumnName = "MessageID";
				colvarMessageID.DataType = DbType.Int32;
				colvarMessageID.MaxLength = 0;
				colvarMessageID.AutoIncrement = true;
				colvarMessageID.IsNullable = false;
				colvarMessageID.IsPrimaryKey = true;
				colvarMessageID.IsForeignKey = false;
				colvarMessageID.IsReadOnly = false;
				colvarMessageID.DefaultSetting = @"";
				colvarMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageID);

				TableSchema.TableColumn colvarMessageTypeId = new TableSchema.TableColumn(schema);
				colvarMessageTypeId.ColumnName = "MessageTypeId";
				colvarMessageTypeId.DataType = DbType.Int32;
				colvarMessageTypeId.MaxLength = 0;
				colvarMessageTypeId.AutoIncrement = false;
				colvarMessageTypeId.IsNullable = false;
				colvarMessageTypeId.IsPrimaryKey = false;
				colvarMessageTypeId.IsForeignKey = true;
				colvarMessageTypeId.IsReadOnly = false;
				colvarMessageTypeId.DefaultSetting = @"";
				colvarMessageTypeId.ForeignKeyTableName = "LG_MessageTypes";
				schema.Columns.Add(colvarMessageTypeId);

				TableSchema.TableColumn colvarLogSourceID = new TableSchema.TableColumn(schema);
				colvarLogSourceID.ColumnName = "LogSourceID";
				colvarLogSourceID.DataType = DbType.Int32;
				colvarLogSourceID.MaxLength = 0;
				colvarLogSourceID.AutoIncrement = false;
				colvarLogSourceID.IsNullable = false;
				colvarLogSourceID.IsPrimaryKey = false;
				colvarLogSourceID.IsForeignKey = true;
				colvarLogSourceID.IsReadOnly = false;
				colvarLogSourceID.DefaultSetting = @"";
				colvarLogSourceID.ForeignKeyTableName = "LG_LogSources";
				schema.Columns.Add(colvarLogSourceID);

				TableSchema.TableColumn colvarHeader = new TableSchema.TableColumn(schema);
				colvarHeader.ColumnName = "Header";
				colvarHeader.DataType = DbType.String;
				colvarHeader.MaxLength = 1024;
				colvarHeader.AutoIncrement = false;
				colvarHeader.IsNullable = false;
				colvarHeader.IsPrimaryKey = false;
				colvarHeader.IsForeignKey = false;
				colvarHeader.IsReadOnly = false;
				colvarHeader.DefaultSetting = @"";
				colvarHeader.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHeader);

				TableSchema.TableColumn colvarMessage = new TableSchema.TableColumn(schema);
				colvarMessage.ColumnName = "Message";
				colvarMessage.DataType = DbType.String;
				colvarMessage.MaxLength = -1;
				colvarMessage.AutoIncrement = false;
				colvarMessage.IsNullable = false;
				colvarMessage.IsPrimaryKey = false;
				colvarMessage.IsForeignKey = false;
				colvarMessage.IsReadOnly = false;
				colvarMessage.DefaultSetting = @"";
				colvarMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessage);

				TableSchema.TableColumn colvarTargetDatabaseServer = new TableSchema.TableColumn(schema);
				colvarTargetDatabaseServer.ColumnName = "TargetDatabaseServer";
				colvarTargetDatabaseServer.DataType = DbType.String;
				colvarTargetDatabaseServer.MaxLength = 128;
				colvarTargetDatabaseServer.AutoIncrement = false;
				colvarTargetDatabaseServer.IsNullable = true;
				colvarTargetDatabaseServer.IsPrimaryKey = false;
				colvarTargetDatabaseServer.IsForeignKey = false;
				colvarTargetDatabaseServer.IsReadOnly = false;
				colvarTargetDatabaseServer.DefaultSetting = @"";
				colvarTargetDatabaseServer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetDatabaseServer);

				TableSchema.TableColumn colvarTargetDatabase = new TableSchema.TableColumn(schema);
				colvarTargetDatabase.ColumnName = "TargetDatabase";
				colvarTargetDatabase.DataType = DbType.String;
				colvarTargetDatabase.MaxLength = 128;
				colvarTargetDatabase.AutoIncrement = false;
				colvarTargetDatabase.IsNullable = true;
				colvarTargetDatabase.IsPrimaryKey = false;
				colvarTargetDatabase.IsForeignKey = false;
				colvarTargetDatabase.IsReadOnly = false;
				colvarTargetDatabase.DefaultSetting = @"";
				colvarTargetDatabase.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetDatabase);

				TableSchema.TableColumn colvarTargetSchema = new TableSchema.TableColumn(schema);
				colvarTargetSchema.ColumnName = "TargetSchema";
				colvarTargetSchema.DataType = DbType.String;
				colvarTargetSchema.MaxLength = 128;
				colvarTargetSchema.AutoIncrement = false;
				colvarTargetSchema.IsNullable = true;
				colvarTargetSchema.IsPrimaryKey = false;
				colvarTargetSchema.IsForeignKey = false;
				colvarTargetSchema.IsReadOnly = false;
				colvarTargetSchema.DefaultSetting = @"";
				colvarTargetSchema.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetSchema);

				TableSchema.TableColumn colvarTargetTable = new TableSchema.TableColumn(schema);
				colvarTargetTable.ColumnName = "TargetTable";
				colvarTargetTable.DataType = DbType.String;
				colvarTargetTable.MaxLength = 128;
				colvarTargetTable.AutoIncrement = false;
				colvarTargetTable.IsNullable = true;
				colvarTargetTable.IsPrimaryKey = false;
				colvarTargetTable.IsForeignKey = false;
				colvarTargetTable.IsReadOnly = false;
				colvarTargetTable.DefaultSetting = @"";
				colvarTargetTable.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetTable);

				TableSchema.TableColumn colvarTargetPrimaryKey = new TableSchema.TableColumn(schema);
				colvarTargetPrimaryKey.ColumnName = "TargetPrimaryKey";
				colvarTargetPrimaryKey.DataType = DbType.AnsiString;
				colvarTargetPrimaryKey.MaxLength = 50;
				colvarTargetPrimaryKey.AutoIncrement = false;
				colvarTargetPrimaryKey.IsNullable = true;
				colvarTargetPrimaryKey.IsPrimaryKey = false;
				colvarTargetPrimaryKey.IsForeignKey = false;
				colvarTargetPrimaryKey.IsReadOnly = false;
				colvarTargetPrimaryKey.DefaultSetting = @"";
				colvarTargetPrimaryKey.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetPrimaryKey);

				TableSchema.TableColumn colvarMethodCall = new TableSchema.TableColumn(schema);
				colvarMethodCall.ColumnName = "MethodCall";
				colvarMethodCall.DataType = DbType.String;
				colvarMethodCall.MaxLength = 255;
				colvarMethodCall.AutoIncrement = false;
				colvarMethodCall.IsNullable = true;
				colvarMethodCall.IsPrimaryKey = false;
				colvarMethodCall.IsForeignKey = false;
				colvarMethodCall.IsReadOnly = false;
				colvarMethodCall.DefaultSetting = @"";
				colvarMethodCall.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMethodCall);

				TableSchema.TableColumn colvarComputerName = new TableSchema.TableColumn(schema);
				colvarComputerName.ColumnName = "ComputerName";
				colvarComputerName.DataType = DbType.String;
				colvarComputerName.MaxLength = 100;
				colvarComputerName.AutoIncrement = false;
				colvarComputerName.IsNullable = true;
				colvarComputerName.IsPrimaryKey = false;
				colvarComputerName.IsForeignKey = false;
				colvarComputerName.IsReadOnly = false;
				colvarComputerName.DefaultSetting = @"";
				colvarComputerName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComputerName);

				TableSchema.TableColumn colvarSourceView = new TableSchema.TableColumn(schema);
				colvarSourceView.ColumnName = "SourceView";
				colvarSourceView.DataType = DbType.String;
				colvarSourceView.MaxLength = 255;
				colvarSourceView.AutoIncrement = false;
				colvarSourceView.IsNullable = true;
				colvarSourceView.IsPrimaryKey = false;
				colvarSourceView.IsForeignKey = false;
				colvarSourceView.IsReadOnly = false;
				colvarSourceView.DefaultSetting = @"";
				colvarSourceView.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSourceView);

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
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_Messages",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_Message LoadFrom(LG_Message item)
		{
			LG_Message result = new LG_Message();
			if (item.MessageID != default(int)) {
				result.LoadByKey(item.MessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int MessageID {
			get { return GetColumnValue<int>(Columns.MessageID); }
			set {
				SetColumnValue(Columns.MessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageID));
			}
		}
		[DataMember]
		public int MessageTypeId {
			get { return GetColumnValue<int>(Columns.MessageTypeId); }
			set {
				SetColumnValue(Columns.MessageTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageTypeId));
			}
		}
		[DataMember]
		public int LogSourceID {
			get { return GetColumnValue<int>(Columns.LogSourceID); }
			set {
				SetColumnValue(Columns.LogSourceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LogSourceID));
			}
		}
		[DataMember]
		public string Header {
			get { return GetColumnValue<string>(Columns.Header); }
			set {
				SetColumnValue(Columns.Header, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Header));
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
		public string TargetDatabaseServer {
			get { return GetColumnValue<string>(Columns.TargetDatabaseServer); }
			set {
				SetColumnValue(Columns.TargetDatabaseServer, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetDatabaseServer));
			}
		}
		[DataMember]
		public string TargetDatabase {
			get { return GetColumnValue<string>(Columns.TargetDatabase); }
			set {
				SetColumnValue(Columns.TargetDatabase, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetDatabase));
			}
		}
		[DataMember]
		public string TargetSchema {
			get { return GetColumnValue<string>(Columns.TargetSchema); }
			set {
				SetColumnValue(Columns.TargetSchema, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetSchema));
			}
		}
		[DataMember]
		public string TargetTable {
			get { return GetColumnValue<string>(Columns.TargetTable); }
			set {
				SetColumnValue(Columns.TargetTable, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetTable));
			}
		}
		[DataMember]
		public string TargetPrimaryKey {
			get { return GetColumnValue<string>(Columns.TargetPrimaryKey); }
			set {
				SetColumnValue(Columns.TargetPrimaryKey, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TargetPrimaryKey));
			}
		}
		[DataMember]
		public string MethodCall {
			get { return GetColumnValue<string>(Columns.MethodCall); }
			set {
				SetColumnValue(Columns.MethodCall, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MethodCall));
			}
		}
		[DataMember]
		public string ComputerName {
			get { return GetColumnValue<string>(Columns.ComputerName); }
			set {
				SetColumnValue(Columns.ComputerName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ComputerName));
			}
		}
		[DataMember]
		public string SourceView {
			get { return GetColumnValue<string>(Columns.SourceView); }
			set {
				SetColumnValue(Columns.SourceView, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SourceView));
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

		#region ForeignKey Properties

		private LG_LogSource _LogSource;
		//Relationship: FK_LG_Messages_LG_LogSources
		public LG_LogSource LogSource
		{
			get
			{
				if(_LogSource == null) {
					_LogSource = LG_LogSource.FetchByID(this.LogSourceID);
				}
				return _LogSource;
			}
			set
			{
				SetColumnValue("LogSourceID", value.LogSourceID);
				_LogSource = value;
			}
		}

		private LG_MessageType _MessageType;
		//Relationship: FK_LG_Messages_LG_MessageTypes
		public LG_MessageType MessageType
		{
			get
			{
				if(_MessageType == null) {
					_MessageType = LG_MessageType.FetchByID(this.MessageTypeId);
				}
				return _MessageType;
			}
			set
			{
				SetColumnValue("MessageTypeId", value.MessageTypeID);
				_MessageType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return MessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn MessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn MessageTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LogSourceIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn HeaderColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MessageColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TargetDatabaseServerColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TargetDatabaseColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TargetSchemaColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn TargetTableColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TargetPrimaryKeyColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn MethodCallColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ComputerNameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn SourceViewColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string MessageID = @"MessageID";
			public static readonly string MessageTypeId = @"MessageTypeId";
			public static readonly string LogSourceID = @"LogSourceID";
			public static readonly string Header = @"Header";
			public static readonly string Message = @"Message";
			public static readonly string TargetDatabaseServer = @"TargetDatabaseServer";
			public static readonly string TargetDatabase = @"TargetDatabase";
			public static readonly string TargetSchema = @"TargetSchema";
			public static readonly string TargetTable = @"TargetTable";
			public static readonly string TargetPrimaryKey = @"TargetPrimaryKey";
			public static readonly string MethodCall = @"MethodCall";
			public static readonly string ComputerName = @"ComputerName";
			public static readonly string SourceView = @"SourceView";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return MessageID; }
		}
		*/

		#region Foreign Collections

		private LG_MessageStackFrameCollection _LG_MessageStackFramesCol;
		//Relationship: FK_LG_MessageStackFrames_LG_Messages
		public LG_MessageStackFrameCollection LG_MessageStackFramesCol
		{
			get
			{
				if(_LG_MessageStackFramesCol == null) {
					_LG_MessageStackFramesCol = new LG_MessageStackFrameCollection();
					_LG_MessageStackFramesCol.LoadAndCloseReader(LG_MessageStackFrame.Query()
						.WHERE(LG_MessageStackFrame.Columns.MessageId, MessageID).ExecuteReader());
				}
				return _LG_MessageStackFramesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LG_MessageStackFrame class.
	/// </summary>
	[DataContract]
	public partial class LG_MessageStackFrameCollection : ActiveList<LG_MessageStackFrame, LG_MessageStackFrameCollection>
	{
		public static LG_MessageStackFrameCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_MessageStackFrameCollection result = new LG_MessageStackFrameCollection();
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
			foreach (LG_MessageStackFrame item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_MessageStackFrames table.
	/// </summary>
	[DataContract]
	public partial class LG_MessageStackFrame : ActiveRecord<LG_MessageStackFrame>, INotifyPropertyChanged
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

		public LG_MessageStackFrame()
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
				TableSchema.Table schema = new TableSchema.Table("LG_MessageStackFrames", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarMessageStackFrameID = new TableSchema.TableColumn(schema);
				colvarMessageStackFrameID.ColumnName = "MessageStackFrameID";
				colvarMessageStackFrameID.DataType = DbType.Int32;
				colvarMessageStackFrameID.MaxLength = 0;
				colvarMessageStackFrameID.AutoIncrement = true;
				colvarMessageStackFrameID.IsNullable = false;
				colvarMessageStackFrameID.IsPrimaryKey = true;
				colvarMessageStackFrameID.IsForeignKey = false;
				colvarMessageStackFrameID.IsReadOnly = false;
				colvarMessageStackFrameID.DefaultSetting = @"";
				colvarMessageStackFrameID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageStackFrameID);

				TableSchema.TableColumn colvarMessageId = new TableSchema.TableColumn(schema);
				colvarMessageId.ColumnName = "MessageId";
				colvarMessageId.DataType = DbType.Int32;
				colvarMessageId.MaxLength = 0;
				colvarMessageId.AutoIncrement = false;
				colvarMessageId.IsNullable = false;
				colvarMessageId.IsPrimaryKey = false;
				colvarMessageId.IsForeignKey = true;
				colvarMessageId.IsReadOnly = false;
				colvarMessageId.DefaultSetting = @"";
				colvarMessageId.ForeignKeyTableName = "LG_Messages";
				schema.Columns.Add(colvarMessageId);

				TableSchema.TableColumn colvarMethod = new TableSchema.TableColumn(schema);
				colvarMethod.ColumnName = "Method";
				colvarMethod.DataType = DbType.String;
				colvarMethod.MaxLength = 255;
				colvarMethod.AutoIncrement = false;
				colvarMethod.IsNullable = true;
				colvarMethod.IsPrimaryKey = false;
				colvarMethod.IsForeignKey = false;
				colvarMethod.IsReadOnly = false;
				colvarMethod.DefaultSetting = @"";
				colvarMethod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMethod);

				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "FileName";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = 255;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = true;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);

				TableSchema.TableColumn colvarLineNumber = new TableSchema.TableColumn(schema);
				colvarLineNumber.ColumnName = "LineNumber";
				colvarLineNumber.DataType = DbType.Int32;
				colvarLineNumber.MaxLength = 0;
				colvarLineNumber.AutoIncrement = false;
				colvarLineNumber.IsNullable = true;
				colvarLineNumber.IsPrimaryKey = false;
				colvarLineNumber.IsForeignKey = false;
				colvarLineNumber.IsReadOnly = false;
				colvarLineNumber.DefaultSetting = @"";
				colvarLineNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLineNumber);

				TableSchema.TableColumn colvarColumnNumber = new TableSchema.TableColumn(schema);
				colvarColumnNumber.ColumnName = "ColumnNumber";
				colvarColumnNumber.DataType = DbType.Int32;
				colvarColumnNumber.MaxLength = 0;
				colvarColumnNumber.AutoIncrement = false;
				colvarColumnNumber.IsNullable = true;
				colvarColumnNumber.IsPrimaryKey = false;
				colvarColumnNumber.IsForeignKey = false;
				colvarColumnNumber.IsReadOnly = false;
				colvarColumnNumber.DefaultSetting = @"";
				colvarColumnNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarColumnNumber);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_MessageStackFrames",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_MessageStackFrame LoadFrom(LG_MessageStackFrame item)
		{
			LG_MessageStackFrame result = new LG_MessageStackFrame();
			if (item.MessageStackFrameID != default(int)) {
				result.LoadByKey(item.MessageStackFrameID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int MessageStackFrameID {
			get { return GetColumnValue<int>(Columns.MessageStackFrameID); }
			set {
				SetColumnValue(Columns.MessageStackFrameID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageStackFrameID));
			}
		}
		[DataMember]
		public int MessageId {
			get { return GetColumnValue<int>(Columns.MessageId); }
			set {
				SetColumnValue(Columns.MessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageId));
			}
		}
		[DataMember]
		public string Method {
			get { return GetColumnValue<string>(Columns.Method); }
			set {
				SetColumnValue(Columns.Method, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Method));
			}
		}
		[DataMember]
		public string FileName {
			get { return GetColumnValue<string>(Columns.FileName); }
			set {
				SetColumnValue(Columns.FileName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileName));
			}
		}
		[DataMember]
		public int? LineNumber {
			get { return GetColumnValue<int?>(Columns.LineNumber); }
			set {
				SetColumnValue(Columns.LineNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LineNumber));
			}
		}
		[DataMember]
		public int? ColumnNumber {
			get { return GetColumnValue<int?>(Columns.ColumnNumber); }
			set {
				SetColumnValue(Columns.ColumnNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ColumnNumber));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LG_Message _Message;
		//Relationship: FK_LG_MessageStackFrames_LG_Messages
		public LG_Message Message
		{
			get
			{
				if(_Message == null) {
					_Message = LG_Message.FetchByID(this.MessageId);
				}
				return _Message;
			}
			set
			{
				SetColumnValue("MessageId", value.MessageID);
				_Message = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return MessageStackFrameID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn MessageStackFrameIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn MessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn MethodColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FileNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LineNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ColumnNumberColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string MessageStackFrameID = @"MessageStackFrameID";
			public static readonly string MessageId = @"MessageId";
			public static readonly string Method = @"Method";
			public static readonly string FileName = @"FileName";
			public static readonly string LineNumber = @"LineNumber";
			public static readonly string ColumnNumber = @"ColumnNumber";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return MessageStackFrameID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LG_MessageType class.
	/// </summary>
	[DataContract]
	public partial class LG_MessageTypeCollection : ActiveList<LG_MessageType, LG_MessageTypeCollection>
	{
		public static LG_MessageTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LG_MessageTypeCollection result = new LG_MessageTypeCollection();
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
			foreach (LG_MessageType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LG_MessageTypes table.
	/// </summary>
	[DataContract]
	public partial class LG_MessageType : ActiveRecord<LG_MessageType>, INotifyPropertyChanged
	{
		#region Enum

		[DataContract]
		public enum MessageTypeEnum : int
		{
			[EnumMember()] Warning = 1,
			[EnumMember()] Critical = 2,
			[EnumMember()] Success = 3,
			[EnumMember()] Licensing = 4,
			[EnumMember()] CustomerPermit = 5,
			[EnumMember()] Exception = 6,
		}

		//[DataMember]
		//public MessageTypeEnum MessageTypeCode
		//{
		//	get { return (MessageTypeEnum)MessageTypeID; }
		//	set { MessageTypeID = (int)value; }
		//}

		#endregion //Enum


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LG_MessageType()
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
				TableSchema.Table schema = new TableSchema.Table("LG_MessageTypes", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarMessageTypeID = new TableSchema.TableColumn(schema);
				colvarMessageTypeID.ColumnName = "MessageTypeID";
				colvarMessageTypeID.DataType = DbType.Int32;
				colvarMessageTypeID.MaxLength = 0;
				colvarMessageTypeID.AutoIncrement = false;
				colvarMessageTypeID.IsNullable = false;
				colvarMessageTypeID.IsPrimaryKey = true;
				colvarMessageTypeID.IsForeignKey = false;
				colvarMessageTypeID.IsReadOnly = false;
				colvarMessageTypeID.DefaultSetting = @"";
				colvarMessageTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageTypeID);

				TableSchema.TableColumn colvarMessageType = new TableSchema.TableColumn(schema);
				colvarMessageType.ColumnName = "MessageType";
				colvarMessageType.DataType = DbType.String;
				colvarMessageType.MaxLength = 50;
				colvarMessageType.AutoIncrement = false;
				colvarMessageType.IsNullable = false;
				colvarMessageType.IsPrimaryKey = false;
				colvarMessageType.IsForeignKey = false;
				colvarMessageType.IsReadOnly = false;
				colvarMessageType.DefaultSetting = @"";
				colvarMessageType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageType);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("LG_MessageTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LG_MessageType LoadFrom(LG_MessageType item)
		{
			LG_MessageType result = new LG_MessageType();
			if (item.MessageTypeID != default(int)) {
				result.LoadByKey(item.MessageTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int MessageTypeID {
			get { return GetColumnValue<int>(Columns.MessageTypeID); }
			set {
				SetColumnValue(Columns.MessageTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageTypeID));
			}
		}
		[DataMember]
		public string MessageType {
			get { return GetColumnValue<string>(Columns.MessageType); }
			set {
				SetColumnValue(Columns.MessageType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageType));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return MessageType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn MessageTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn MessageTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string MessageTypeID = @"MessageTypeID";
			public static readonly string MessageType = @"MessageType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return MessageTypeID; }
		}
		*/

		#region Foreign Collections

		private LG_MessageCollection _LG_MessagesCol;
		//Relationship: FK_LG_Messages_LG_MessageTypes
		public LG_MessageCollection LG_MessagesCol
		{
			get
			{
				if(_LG_MessagesCol == null) {
					_LG_MessagesCol = new LG_MessageCollection();
					_LG_MessagesCol.LoadAndCloseReader(LG_Message.Query()
						.WHERE(LG_Message.Columns.MessageTypeId, MessageTypeID).ExecuteReader());
				}
				return _LG_MessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SubSonicSchemaInfo class.
	/// </summary>
	[DataContract]
	public partial class SubSonicSchemaInfoCollection : ActiveList<SubSonicSchemaInfo, SubSonicSchemaInfoCollection>
	{
		public static SubSonicSchemaInfoCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SubSonicSchemaInfoCollection result = new SubSonicSchemaInfoCollection();
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
			foreach (SubSonicSchemaInfo item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SubSonicSchemaInfo table.
	/// </summary>
	[DataContract]
	public partial class SubSonicSchemaInfo : ActiveRecord<SubSonicSchemaInfo>, INotifyPropertyChanged
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

		public SubSonicSchemaInfo()
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
				TableSchema.Table schema = new TableSchema.Table("SubSonicSchemaInfo", TableType.Table, DataService.GetInstance("SosLoggingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarversion = new TableSchema.TableColumn(schema);
				colvarversion.ColumnName = "version";
				colvarversion.DataType = DbType.Int32;
				colvarversion.MaxLength = 0;
				colvarversion.AutoIncrement = false;
				colvarversion.IsNullable = false;
				colvarversion.IsPrimaryKey = false;
				colvarversion.IsForeignKey = false;
				colvarversion.IsReadOnly = false;
				colvarversion.DefaultSetting = @"";
				colvarversion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarversion);

				BaseSchema = schema;
				DataService.Providers["SosLoggingProvider"].AddSchema("SubSonicSchemaInfo",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public int version {
			get { return GetColumnValue<int>(Columns.version); }
			set {
				SetColumnValue(Columns.version, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.version));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return version.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn versionColumn
		{
			get { return Schema.Columns[0]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string version = @"version";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return null; }
		}
		*/


	}
}
