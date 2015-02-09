


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

namespace NXS.Data.FileStore
{
	/// <summary>
	/// Strongly-typed collection for the FS_File class.
	/// </summary>
	[DataContract]
	public partial class FS_FileCollection : ActiveList<FS_File, FS_FileCollection>
	{
		public static FS_FileCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FS_FileCollection result = new FS_FileCollection();
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
			foreach (FS_File item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the FS_Files table.
	/// </summary>
	[DataContract]
	public partial class FS_File : ActiveRecord<FS_File>, INotifyPropertyChanged
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

		public FS_File()
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
				TableSchema.Table schema = new TableSchema.Table("FS_Files", TableType.Table, DataService.GetInstance("FileStoreProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarFileID = new TableSchema.TableColumn(schema);
				colvarFileID.ColumnName = "FileID";
				colvarFileID.DataType = DbType.Int32;
				colvarFileID.MaxLength = 0;
				colvarFileID.AutoIncrement = true;
				colvarFileID.IsNullable = false;
				colvarFileID.IsPrimaryKey = true;
				colvarFileID.IsForeignKey = false;
				colvarFileID.IsReadOnly = false;
				colvarFileID.DefaultSetting = @"";
				colvarFileID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileID);

				TableSchema.TableColumn colvarFileSourceID = new TableSchema.TableColumn(schema);
				colvarFileSourceID.ColumnName = "FileSourceID";
				colvarFileSourceID.DataType = DbType.Int32;
				colvarFileSourceID.MaxLength = 0;
				colvarFileSourceID.AutoIncrement = false;
				colvarFileSourceID.IsNullable = false;
				colvarFileSourceID.IsPrimaryKey = false;
				colvarFileSourceID.IsForeignKey = true;
				colvarFileSourceID.IsReadOnly = false;
				colvarFileSourceID.DefaultSetting = @"";
				colvarFileSourceID.ForeignKeyTableName = "FS_FileSources";
				schema.Columns.Add(colvarFileSourceID);

				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "FileName";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = 1024;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = false;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);

				TableSchema.TableColumn colvarFileSize = new TableSchema.TableColumn(schema);
				colvarFileSize.ColumnName = "FileSize";
				colvarFileSize.DataType = DbType.Int64;
				colvarFileSize.MaxLength = 0;
				colvarFileSize.AutoIncrement = false;
				colvarFileSize.IsNullable = false;
				colvarFileSize.IsPrimaryKey = false;
				colvarFileSize.IsForeignKey = false;
				colvarFileSize.IsReadOnly = false;
				colvarFileSize.DefaultSetting = @"";
				colvarFileSize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileSize);

				TableSchema.TableColumn colvarMimeType = new TableSchema.TableColumn(schema);
				colvarMimeType.ColumnName = "MimeType";
				colvarMimeType.DataType = DbType.String;
				colvarMimeType.MaxLength = 100;
				colvarMimeType.AutoIncrement = false;
				colvarMimeType.IsNullable = false;
				colvarMimeType.IsPrimaryKey = false;
				colvarMimeType.IsForeignKey = false;
				colvarMimeType.IsReadOnly = false;
				colvarMimeType.DefaultSetting = @"";
				colvarMimeType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMimeType);

				TableSchema.TableColumn colvarFileData = new TableSchema.TableColumn(schema);
				colvarFileData.ColumnName = "FileData";
				colvarFileData.DataType = DbType.Binary;
				colvarFileData.MaxLength = 2147483647;
				colvarFileData.AutoIncrement = false;
				colvarFileData.IsNullable = false;
				colvarFileData.IsPrimaryKey = false;
				colvarFileData.IsForeignKey = false;
				colvarFileData.IsReadOnly = false;
				colvarFileData.DefaultSetting = @"";
				colvarFileData.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileData);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = false;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"";
				colvarCreatedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByID);

				TableSchema.TableColumn colvarCreatedByDate = new TableSchema.TableColumn(schema);
				colvarCreatedByDate.ColumnName = "CreatedByDate";
				colvarCreatedByDate.DataType = DbType.DateTime;
				colvarCreatedByDate.MaxLength = 0;
				colvarCreatedByDate.AutoIncrement = false;
				colvarCreatedByDate.IsNullable = false;
				colvarCreatedByDate.IsPrimaryKey = false;
				colvarCreatedByDate.IsForeignKey = false;
				colvarCreatedByDate.IsReadOnly = false;
				colvarCreatedByDate.DefaultSetting = @"";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["FileStoreProvider"].AddSchema("FS_Files",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FS_File LoadFrom(FS_File item)
		{
			FS_File result = new FS_File();
			if (item.FileID != default(int)) {
				result.LoadByKey(item.FileID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int FileID { 
			get { return GetColumnValue<int>(Columns.FileID); }
			set {
				SetColumnValue(Columns.FileID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileID));
			}
		}
		[DataMember]
		public int FileSourceID { 
			get { return GetColumnValue<int>(Columns.FileSourceID); }
			set {
				SetColumnValue(Columns.FileSourceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileSourceID));
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
		public long FileSize { 
			get { return GetColumnValue<long>(Columns.FileSize); }
			set {
				SetColumnValue(Columns.FileSize, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileSize));
			}
		}
		[DataMember]
		public string MimeType { 
			get { return GetColumnValue<string>(Columns.MimeType); }
			set {
				SetColumnValue(Columns.MimeType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MimeType));
			}
		}
		[DataMember]
		public byte[] FileData { 
			get { return GetColumnValue<byte[]>(Columns.FileData); }
			set {
				SetColumnValue(Columns.FileData, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileData));
			}
		}
		[DataMember]
		public string CreatedByID { 
			get { return GetColumnValue<string>(Columns.CreatedByID); }
			set {
				SetColumnValue(Columns.CreatedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedByID));
			}
		}
		[DataMember]
		public DateTime CreatedByDate { 
			get { return GetColumnValue<DateTime>(Columns.CreatedByDate); }
			set {
				SetColumnValue(Columns.CreatedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedByDate));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FS_FileSource _FileSource;
		//Relationship: FK_FS_Files_FS_FileSources
		public FS_FileSource FileSource
		{
			get
			{
				if(_FileSource == null) {
					_FileSource = FS_FileSource.FetchByID(this.FileSourceID);
				}
				return _FileSource;
			}
			set
			{
				SetColumnValue("FileSourceID", value.FileSourceID);
				_FileSource = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return FileID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn FileIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FileSourceIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn FileNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FileSizeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MimeTypeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn FileDataColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string FileID = @"FileID";
			public static readonly string FileSourceID = @"FileSourceID";
			public static readonly string FileName = @"FileName";
			public static readonly string FileSize = @"FileSize";
			public static readonly string MimeType = @"MimeType";
			public static readonly string FileData = @"FileData";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return FileID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the FS_FileSource class.
	/// </summary>
	[DataContract]
	public partial class FS_FileSourceCollection : ActiveList<FS_FileSource, FS_FileSourceCollection>
	{
		public static FS_FileSourceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FS_FileSourceCollection result = new FS_FileSourceCollection();
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
			foreach (FS_FileSource item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the FS_FileSources table.
	/// </summary>
	[DataContract]
	public partial class FS_FileSource : ActiveRecord<FS_FileSource>, INotifyPropertyChanged
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

		public FS_FileSource()
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
				TableSchema.Table schema = new TableSchema.Table("FS_FileSources", TableType.Table, DataService.GetInstance("FileStoreProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarFileSourceID = new TableSchema.TableColumn(schema);
				colvarFileSourceID.ColumnName = "FileSourceID";
				colvarFileSourceID.DataType = DbType.Int32;
				colvarFileSourceID.MaxLength = 0;
				colvarFileSourceID.AutoIncrement = false;
				colvarFileSourceID.IsNullable = false;
				colvarFileSourceID.IsPrimaryKey = true;
				colvarFileSourceID.IsForeignKey = false;
				colvarFileSourceID.IsReadOnly = false;
				colvarFileSourceID.DefaultSetting = @"";
				colvarFileSourceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileSourceID);

				TableSchema.TableColumn colvarFileSourceName = new TableSchema.TableColumn(schema);
				colvarFileSourceName.ColumnName = "FileSourceName";
				colvarFileSourceName.DataType = DbType.String;
				colvarFileSourceName.MaxLength = 100;
				colvarFileSourceName.AutoIncrement = false;
				colvarFileSourceName.IsNullable = false;
				colvarFileSourceName.IsPrimaryKey = false;
				colvarFileSourceName.IsForeignKey = false;
				colvarFileSourceName.IsReadOnly = false;
				colvarFileSourceName.DefaultSetting = @"";
				colvarFileSourceName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileSourceName);

				BaseSchema = schema;
				DataService.Providers["FileStoreProvider"].AddSchema("FS_FileSources",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FS_FileSource LoadFrom(FS_FileSource item)
		{
			FS_FileSource result = new FS_FileSource();
			if (item.FileSourceID != default(int)) {
				result.LoadByKey(item.FileSourceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int FileSourceID { 
			get { return GetColumnValue<int>(Columns.FileSourceID); }
			set {
				SetColumnValue(Columns.FileSourceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileSourceID));
			}
		}
		[DataMember]
		public string FileSourceName { 
			get { return GetColumnValue<string>(Columns.FileSourceName); }
			set {
				SetColumnValue(Columns.FileSourceName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileSourceName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return FileSourceName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn FileSourceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FileSourceNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string FileSourceID = @"FileSourceID";
			public static readonly string FileSourceName = @"FileSourceName";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return FileSourceID; }
		}
		*/
	}
}

