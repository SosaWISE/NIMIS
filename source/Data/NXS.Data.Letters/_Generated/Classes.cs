


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

namespace NXS.Data.Letters
{
	/// <summary>
	/// Strongly-typed collection for the LD_CreditRequestReason class.
	/// </summary>
	[DataContract]
	public partial class LD_CreditRequestReasonCollection : ActiveList<LD_CreditRequestReason, LD_CreditRequestReasonCollection>
	{
		public static LD_CreditRequestReasonCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_CreditRequestReasonCollection result = new LD_CreditRequestReasonCollection();
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
			foreach (LD_CreditRequestReason item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_CreditRequestReason table.
	/// </summary>
	[DataContract]
	public partial class LD_CreditRequestReason : ActiveRecord<LD_CreditRequestReason>, INotifyPropertyChanged
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

		public LD_CreditRequestReason()
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
				TableSchema.Table schema = new TableSchema.Table("LD_CreditRequestReason", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCreditRequestReasonID = new TableSchema.TableColumn(schema);
				colvarCreditRequestReasonID.ColumnName = "CreditRequestReasonID";
				colvarCreditRequestReasonID.DataType = DbType.Int32;
				colvarCreditRequestReasonID.MaxLength = 0;
				colvarCreditRequestReasonID.AutoIncrement = true;
				colvarCreditRequestReasonID.IsNullable = false;
				colvarCreditRequestReasonID.IsPrimaryKey = true;
				colvarCreditRequestReasonID.IsForeignKey = false;
				colvarCreditRequestReasonID.IsReadOnly = false;
				colvarCreditRequestReasonID.DefaultSetting = @"";
				colvarCreditRequestReasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreditRequestReasonID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_CreditRequestReason",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_CreditRequestReason LoadFrom(LD_CreditRequestReason item)
		{
			LD_CreditRequestReason result = new LD_CreditRequestReason();
			if (item.CreditRequestReasonID != default(int)) {
				result.LoadByKey(item.CreditRequestReasonID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int CreditRequestReasonID { 
			get { return GetColumnValue<int>(Columns.CreditRequestReasonID); }
			set {
				SetColumnValue(Columns.CreditRequestReasonID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreditRequestReasonID));
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
		public string Description { 
			get { return GetColumnValue<string>(Columns.Description); }
			set {
				SetColumnValue(Columns.Description, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Description));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CreditRequestReasonIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CreditRequestReasonID = @"CreditRequestReasonID";
			public static readonly string Name = @"Name";
			public static readonly string Description = @"Description";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CreditRequestReasonID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_Department class.
	/// </summary>
	[DataContract]
	public partial class LD_DepartmentCollection : ActiveList<LD_Department, LD_DepartmentCollection>
	{
		public static LD_DepartmentCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_DepartmentCollection result = new LD_DepartmentCollection();
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
			foreach (LD_Department item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Department table.
	/// </summary>
	[DataContract]
	public partial class LD_Department : ActiveRecord<LD_Department>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum DepartmentEnum : int
		{
			[EnumMember()] Customer_Service__________________________________ = 1,
			[EnumMember()] Retention_________________________________________ = 2,
			[EnumMember()] Collections_______________________________________ = 3,
			[EnumMember()] Service___________________________________________ = 4,
			[EnumMember()] Inventory_________________________________________ = 5,
			[EnumMember()] Billing___________________________________________ = 6,
			[EnumMember()] Licensing_________________________________________ = 7,
		}
		
		//[DataMember]
		//public DepartmentEnum DepartmentCode
		//{
		//	get { return (DepartmentEnum)DepartmentID; }
		//	set { DepartmentID = (int)value; }
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

		public LD_Department()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Department", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDepartmentID = new TableSchema.TableColumn(schema);
				colvarDepartmentID.ColumnName = "DepartmentID";
				colvarDepartmentID.DataType = DbType.Int32;
				colvarDepartmentID.MaxLength = 0;
				colvarDepartmentID.AutoIncrement = true;
				colvarDepartmentID.IsNullable = false;
				colvarDepartmentID.IsPrimaryKey = true;
				colvarDepartmentID.IsForeignKey = false;
				colvarDepartmentID.IsReadOnly = false;
				colvarDepartmentID.DefaultSetting = @"";
				colvarDepartmentID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
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
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Department",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Department LoadFrom(LD_Department item)
		{
			LD_Department result = new LD_Department();
			if (item.DepartmentID != default(int)) {
				result.LoadByKey(item.DepartmentID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int DepartmentID { 
			get { return GetColumnValue<int>(Columns.DepartmentID); }
			set {
				SetColumnValue(Columns.DepartmentID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DepartmentID));
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

		public static TableSchema.TableColumn DepartmentIDColumn
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
			public static readonly string DepartmentID = @"DepartmentID";
			public static readonly string Name = @"Name";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DepartmentID; }
		}
		*/

		#region Foreign Collections

		private LD_LetterCollection _LD_LettersCol;
		//Relationship: FK_L_Letter_L_Department
		public LD_LetterCollection LD_LettersCol
		{
			get
			{
				if(_LD_LettersCol == null) {
					_LD_LettersCol = new LD_LetterCollection();
					_LD_LettersCol.LoadAndCloseReader(LD_Letter.Query()
						.WHERE(LD_Letter.Columns.DepartmentID, DepartmentID).ExecuteReader());
				}
				return _LD_LettersCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_DocType class.
	/// </summary>
	[DataContract]
	public partial class LD_DocTypeCollection : ActiveList<LD_DocType, LD_DocTypeCollection>
	{
		public static LD_DocTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_DocTypeCollection result = new LD_DocTypeCollection();
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
			foreach (LD_DocType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_DocTypes table.
	/// </summary>
	[DataContract]
	public partial class LD_DocType : ActiveRecord<LD_DocType>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum DocTypeEnum : int
		{
			[EnumMember()] Account = 1,
			[EnumMember()] Recruit = 2,
			[EnumMember()] RecruitRegistration = 4,
		}
		
		//[DataMember]
		//public DocTypeEnum DocTypeCode
		//{
		//	get { return (DocTypeEnum)DocTypeID; }
		//	set { DocTypeID = (int)value; }
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

		public LD_DocType()
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
				TableSchema.Table schema = new TableSchema.Table("LD_DocTypes", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDocTypeID = new TableSchema.TableColumn(schema);
				colvarDocTypeID.ColumnName = "DocTypeID";
				colvarDocTypeID.DataType = DbType.Int32;
				colvarDocTypeID.MaxLength = 0;
				colvarDocTypeID.AutoIncrement = true;
				colvarDocTypeID.IsNullable = false;
				colvarDocTypeID.IsPrimaryKey = true;
				colvarDocTypeID.IsForeignKey = false;
				colvarDocTypeID.IsReadOnly = false;
				colvarDocTypeID.DefaultSetting = @"";
				colvarDocTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocTypeID);

				TableSchema.TableColumn colvarDocType = new TableSchema.TableColumn(schema);
				colvarDocType.ColumnName = "DocType";
				colvarDocType.DataType = DbType.String;
				colvarDocType.MaxLength = 50;
				colvarDocType.AutoIncrement = false;
				colvarDocType.IsNullable = false;
				colvarDocType.IsPrimaryKey = false;
				colvarDocType.IsForeignKey = false;
				colvarDocType.IsReadOnly = false;
				colvarDocType.DefaultSetting = @"";
				colvarDocType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocType);

				TableSchema.TableColumn colvarQuery = new TableSchema.TableColumn(schema);
				colvarQuery.ColumnName = "Query";
				colvarQuery.DataType = DbType.String;
				colvarQuery.MaxLength = -1;
				colvarQuery.AutoIncrement = false;
				colvarQuery.IsNullable = false;
				colvarQuery.IsPrimaryKey = false;
				colvarQuery.IsForeignKey = false;
				colvarQuery.IsReadOnly = false;
				colvarQuery.DefaultSetting = @"";
				colvarQuery.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuery);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_DocTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_DocType LoadFrom(LD_DocType item)
		{
			LD_DocType result = new LD_DocType();
			if (item.DocTypeID != default(int)) {
				result.LoadByKey(item.DocTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int DocTypeID { 
			get { return GetColumnValue<int>(Columns.DocTypeID); }
			set {
				SetColumnValue(Columns.DocTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DocTypeID));
			}
		}
		[DataMember]
		public string DocType { 
			get { return GetColumnValue<string>(Columns.DocType); }
			set {
				SetColumnValue(Columns.DocType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DocType));
			}
		}
		[DataMember]
		public string Query { 
			get { return GetColumnValue<string>(Columns.Query); }
			set {
				SetColumnValue(Columns.Query, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Query));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return DocType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn DocTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DocTypeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn QueryColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DocTypeID = @"DocTypeID";
			public static readonly string DocType = @"DocType";
			public static readonly string Query = @"Query";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DocTypeID; }
		}
		*/

		#region Foreign Collections

		private LD_ExtraInfoCollection _LD_ExtraInfosCol;
		//Relationship: FK_LD_ExtraInfo_LD_DocTypes
		public LD_ExtraInfoCollection LD_ExtraInfosCol
		{
			get
			{
				if(_LD_ExtraInfosCol == null) {
					_LD_ExtraInfosCol = new LD_ExtraInfoCollection();
					_LD_ExtraInfosCol.LoadAndCloseReader(LD_ExtraInfo.Query()
						.WHERE(LD_ExtraInfo.Columns.DocTypeID, DocTypeID).ExecuteReader());
				}
				return _LD_ExtraInfosCol;
			}
		}

		private LD_TemplateCollection _LD_TemplatesCol;
		//Relationship: FK_LD_Templates_LD_DocTypes
		public LD_TemplateCollection LD_TemplatesCol
		{
			get
			{
				if(_LD_TemplatesCol == null) {
					_LD_TemplatesCol = new LD_TemplateCollection();
					_LD_TemplatesCol.LoadAndCloseReader(LD_Template.Query()
						.WHERE(LD_Template.Columns.DocTypeID, DocTypeID).ExecuteReader());
				}
				return _LD_TemplatesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_ExtraInfo class.
	/// </summary>
	[DataContract]
	public partial class LD_ExtraInfoCollection : ActiveList<LD_ExtraInfo, LD_ExtraInfoCollection>
	{
		public static LD_ExtraInfoCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_ExtraInfoCollection result = new LD_ExtraInfoCollection();
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
			foreach (LD_ExtraInfo item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_ExtraInfo table.
	/// </summary>
	[DataContract]
	public partial class LD_ExtraInfo : ActiveRecord<LD_ExtraInfo>, INotifyPropertyChanged
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

		public LD_ExtraInfo()
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
				TableSchema.Table schema = new TableSchema.Table("LD_ExtraInfo", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarExtraInfoID = new TableSchema.TableColumn(schema);
				colvarExtraInfoID.ColumnName = "ExtraInfoID";
				colvarExtraInfoID.DataType = DbType.Int32;
				colvarExtraInfoID.MaxLength = 0;
				colvarExtraInfoID.AutoIncrement = true;
				colvarExtraInfoID.IsNullable = false;
				colvarExtraInfoID.IsPrimaryKey = true;
				colvarExtraInfoID.IsForeignKey = false;
				colvarExtraInfoID.IsReadOnly = false;
				colvarExtraInfoID.DefaultSetting = @"";
				colvarExtraInfoID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExtraInfoID);

				TableSchema.TableColumn colvarForeignKeyID = new TableSchema.TableColumn(schema);
				colvarForeignKeyID.ColumnName = "ForeignKeyID";
				colvarForeignKeyID.DataType = DbType.Int32;
				colvarForeignKeyID.MaxLength = 0;
				colvarForeignKeyID.AutoIncrement = false;
				colvarForeignKeyID.IsNullable = false;
				colvarForeignKeyID.IsPrimaryKey = false;
				colvarForeignKeyID.IsForeignKey = false;
				colvarForeignKeyID.IsReadOnly = false;
				colvarForeignKeyID.DefaultSetting = @"";
				colvarForeignKeyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarForeignKeyID);

				TableSchema.TableColumn colvarDocTypeID = new TableSchema.TableColumn(schema);
				colvarDocTypeID.ColumnName = "DocTypeID";
				colvarDocTypeID.DataType = DbType.Int32;
				colvarDocTypeID.MaxLength = 0;
				colvarDocTypeID.AutoIncrement = false;
				colvarDocTypeID.IsNullable = false;
				colvarDocTypeID.IsPrimaryKey = false;
				colvarDocTypeID.IsForeignKey = true;
				colvarDocTypeID.IsReadOnly = false;
				colvarDocTypeID.DefaultSetting = @"";
				colvarDocTypeID.ForeignKeyTableName = "LD_DocTypes";
				schema.Columns.Add(colvarDocTypeID);

				TableSchema.TableColumn colvarCurrentUser = new TableSchema.TableColumn(schema);
				colvarCurrentUser.ColumnName = "CurrentUser";
				colvarCurrentUser.DataType = DbType.String;
				colvarCurrentUser.MaxLength = 50;
				colvarCurrentUser.AutoIncrement = false;
				colvarCurrentUser.IsNullable = true;
				colvarCurrentUser.IsPrimaryKey = false;
				colvarCurrentUser.IsForeignKey = false;
				colvarCurrentUser.IsReadOnly = false;
				colvarCurrentUser.DefaultSetting = @"";
				colvarCurrentUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentUser);

				TableSchema.TableColumn colvarMoniAccountChangeDescription = new TableSchema.TableColumn(schema);
				colvarMoniAccountChangeDescription.ColumnName = "MoniAccountChangeDescription";
				colvarMoniAccountChangeDescription.DataType = DbType.String;
				colvarMoniAccountChangeDescription.MaxLength = -1;
				colvarMoniAccountChangeDescription.AutoIncrement = false;
				colvarMoniAccountChangeDescription.IsNullable = true;
				colvarMoniAccountChangeDescription.IsPrimaryKey = false;
				colvarMoniAccountChangeDescription.IsForeignKey = false;
				colvarMoniAccountChangeDescription.IsReadOnly = false;
				colvarMoniAccountChangeDescription.DefaultSetting = @"";
				colvarMoniAccountChangeDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoniAccountChangeDescription);

				TableSchema.TableColumn colvarIndustryNumber = new TableSchema.TableColumn(schema);
				colvarIndustryNumber.ColumnName = "IndustryNumber";
				colvarIndustryNumber.DataType = DbType.String;
				colvarIndustryNumber.MaxLength = 50;
				colvarIndustryNumber.AutoIncrement = false;
				colvarIndustryNumber.IsNullable = true;
				colvarIndustryNumber.IsPrimaryKey = false;
				colvarIndustryNumber.IsForeignKey = false;
				colvarIndustryNumber.IsReadOnly = false;
				colvarIndustryNumber.DefaultSetting = @"";
				colvarIndustryNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIndustryNumber);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_ExtraInfo",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_ExtraInfo LoadFrom(LD_ExtraInfo item)
		{
			LD_ExtraInfo result = new LD_ExtraInfo();
			if (item.ExtraInfoID != default(int)) {
				result.LoadByKey(item.ExtraInfoID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int ExtraInfoID { 
			get { return GetColumnValue<int>(Columns.ExtraInfoID); }
			set {
				SetColumnValue(Columns.ExtraInfoID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ExtraInfoID));
			}
		}
		[DataMember]
		public int ForeignKeyID { 
			get { return GetColumnValue<int>(Columns.ForeignKeyID); }
			set {
				SetColumnValue(Columns.ForeignKeyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ForeignKeyID));
			}
		}
		[DataMember]
		public int DocTypeID { 
			get { return GetColumnValue<int>(Columns.DocTypeID); }
			set {
				SetColumnValue(Columns.DocTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DocTypeID));
			}
		}
		[DataMember]
		public string CurrentUser { 
			get { return GetColumnValue<string>(Columns.CurrentUser); }
			set {
				SetColumnValue(Columns.CurrentUser, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CurrentUser));
			}
		}
		[DataMember]
		public string MoniAccountChangeDescription { 
			get { return GetColumnValue<string>(Columns.MoniAccountChangeDescription); }
			set {
				SetColumnValue(Columns.MoniAccountChangeDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MoniAccountChangeDescription));
			}
		}
		[DataMember]
		public string IndustryNumber { 
			get { return GetColumnValue<string>(Columns.IndustryNumber); }
			set {
				SetColumnValue(Columns.IndustryNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IndustryNumber));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LD_DocType _DocType;
		//Relationship: FK_LD_ExtraInfo_LD_DocTypes
		public LD_DocType DocType
		{
			get
			{
				if(_DocType == null) {
					_DocType = LD_DocType.FetchByID(this.DocTypeID);
				}
				return _DocType;
			}
			set
			{
				SetColumnValue("DocTypeID", value.DocTypeID);
				_DocType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ExtraInfoID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ExtraInfoIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ForeignKeyIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DocTypeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CurrentUserColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MoniAccountChangeDescriptionColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IndustryNumberColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ExtraInfoID = @"ExtraInfoID";
			public static readonly string ForeignKeyID = @"ForeignKeyID";
			public static readonly string DocTypeID = @"DocTypeID";
			public static readonly string CurrentUser = @"CurrentUser";
			public static readonly string MoniAccountChangeDescription = @"MoniAccountChangeDescription";
			public static readonly string IndustryNumber = @"IndustryNumber";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ExtraInfoID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_Field class.
	/// </summary>
	[DataContract]
	public partial class LD_FieldCollection : ActiveList<LD_Field, LD_FieldCollection>
	{
		public static LD_FieldCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_FieldCollection result = new LD_FieldCollection();
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
			foreach (LD_Field item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Fields table.
	/// </summary>
	[DataContract]
	public partial class LD_Field : ActiveRecord<LD_Field>, INotifyPropertyChanged
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

		public LD_Field()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Fields", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarFieldID = new TableSchema.TableColumn(schema);
				colvarFieldID.ColumnName = "FieldID";
				colvarFieldID.DataType = DbType.Int32;
				colvarFieldID.MaxLength = 0;
				colvarFieldID.AutoIncrement = true;
				colvarFieldID.IsNullable = false;
				colvarFieldID.IsPrimaryKey = true;
				colvarFieldID.IsForeignKey = false;
				colvarFieldID.IsReadOnly = false;
				colvarFieldID.DefaultSetting = @"";
				colvarFieldID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFieldID);

				TableSchema.TableColumn colvarTemplateID = new TableSchema.TableColumn(schema);
				colvarTemplateID.ColumnName = "TemplateID";
				colvarTemplateID.DataType = DbType.Int32;
				colvarTemplateID.MaxLength = 0;
				colvarTemplateID.AutoIncrement = false;
				colvarTemplateID.IsNullable = false;
				colvarTemplateID.IsPrimaryKey = false;
				colvarTemplateID.IsForeignKey = true;
				colvarTemplateID.IsReadOnly = false;
				colvarTemplateID.DefaultSetting = @"";
				colvarTemplateID.ForeignKeyTableName = "LD_Templates";
				schema.Columns.Add(colvarTemplateID);

				TableSchema.TableColumn colvarSubsequentFieldID = new TableSchema.TableColumn(schema);
				colvarSubsequentFieldID.ColumnName = "SubsequentFieldID";
				colvarSubsequentFieldID.DataType = DbType.Int32;
				colvarSubsequentFieldID.MaxLength = 0;
				colvarSubsequentFieldID.AutoIncrement = false;
				colvarSubsequentFieldID.IsNullable = true;
				colvarSubsequentFieldID.IsPrimaryKey = false;
				colvarSubsequentFieldID.IsForeignKey = true;
				colvarSubsequentFieldID.IsReadOnly = false;
				colvarSubsequentFieldID.DefaultSetting = @"";
				colvarSubsequentFieldID.ForeignKeyTableName = "LD_Fields";
				schema.Columns.Add(colvarSubsequentFieldID);

				TableSchema.TableColumn colvarFieldName = new TableSchema.TableColumn(schema);
				colvarFieldName.ColumnName = "FieldName";
				colvarFieldName.DataType = DbType.String;
				colvarFieldName.MaxLength = -1;
				colvarFieldName.AutoIncrement = false;
				colvarFieldName.IsNullable = true;
				colvarFieldName.IsPrimaryKey = false;
				colvarFieldName.IsForeignKey = false;
				colvarFieldName.IsReadOnly = false;
				colvarFieldName.DefaultSetting = @"";
				colvarFieldName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFieldName);

				TableSchema.TableColumn colvarDBColumnName = new TableSchema.TableColumn(schema);
				colvarDBColumnName.ColumnName = "DBColumnName";
				colvarDBColumnName.DataType = DbType.String;
				colvarDBColumnName.MaxLength = -1;
				colvarDBColumnName.AutoIncrement = false;
				colvarDBColumnName.IsNullable = false;
				colvarDBColumnName.IsPrimaryKey = false;
				colvarDBColumnName.IsForeignKey = false;
				colvarDBColumnName.IsReadOnly = false;
				colvarDBColumnName.DefaultSetting = @"";
				colvarDBColumnName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDBColumnName);

				TableSchema.TableColumn colvarFormatString = new TableSchema.TableColumn(schema);
				colvarFormatString.ColumnName = "FormatString";
				colvarFormatString.DataType = DbType.String;
				colvarFormatString.MaxLength = 50;
				colvarFormatString.AutoIncrement = false;
				colvarFormatString.IsNullable = false;
				colvarFormatString.IsPrimaryKey = false;
				colvarFormatString.IsForeignKey = false;
				colvarFormatString.IsReadOnly = false;
				colvarFormatString.DefaultSetting = @"('{0}')";
				colvarFormatString.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFormatString);

				TableSchema.TableColumn colvarPreprocessFormatString = new TableSchema.TableColumn(schema);
				colvarPreprocessFormatString.ColumnName = "PreprocessFormatString";
				colvarPreprocessFormatString.DataType = DbType.String;
				colvarPreprocessFormatString.MaxLength = 50;
				colvarPreprocessFormatString.AutoIncrement = false;
				colvarPreprocessFormatString.IsNullable = true;
				colvarPreprocessFormatString.IsPrimaryKey = false;
				colvarPreprocessFormatString.IsForeignKey = false;
				colvarPreprocessFormatString.IsReadOnly = false;
				colvarPreprocessFormatString.DefaultSetting = @"";
				colvarPreprocessFormatString.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreprocessFormatString);

				TableSchema.TableColumn colvarIsEncrypted = new TableSchema.TableColumn(schema);
				colvarIsEncrypted.ColumnName = "IsEncrypted";
				colvarIsEncrypted.DataType = DbType.Boolean;
				colvarIsEncrypted.MaxLength = 0;
				colvarIsEncrypted.AutoIncrement = false;
				colvarIsEncrypted.IsNullable = false;
				colvarIsEncrypted.IsPrimaryKey = false;
				colvarIsEncrypted.IsForeignKey = false;
				colvarIsEncrypted.IsReadOnly = false;
				colvarIsEncrypted.DefaultSetting = @"((0))";
				colvarIsEncrypted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsEncrypted);

				TableSchema.TableColumn colvarIsSubstring = new TableSchema.TableColumn(schema);
				colvarIsSubstring.ColumnName = "IsSubstring";
				colvarIsSubstring.DataType = DbType.Boolean;
				colvarIsSubstring.MaxLength = 0;
				colvarIsSubstring.AutoIncrement = false;
				colvarIsSubstring.IsNullable = false;
				colvarIsSubstring.IsPrimaryKey = false;
				colvarIsSubstring.IsForeignKey = false;
				colvarIsSubstring.IsReadOnly = false;
				colvarIsSubstring.DefaultSetting = @"((0))";
				colvarIsSubstring.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsSubstring);

				TableSchema.TableColumn colvarSubstringStart = new TableSchema.TableColumn(schema);
				colvarSubstringStart.ColumnName = "SubstringStart";
				colvarSubstringStart.DataType = DbType.Int32;
				colvarSubstringStart.MaxLength = 0;
				colvarSubstringStart.AutoIncrement = false;
				colvarSubstringStart.IsNullable = true;
				colvarSubstringStart.IsPrimaryKey = false;
				colvarSubstringStart.IsForeignKey = false;
				colvarSubstringStart.IsReadOnly = false;
				colvarSubstringStart.DefaultSetting = @"";
				colvarSubstringStart.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubstringStart);

				TableSchema.TableColumn colvarSubstringLength = new TableSchema.TableColumn(schema);
				colvarSubstringLength.ColumnName = "SubstringLength";
				colvarSubstringLength.DataType = DbType.Int32;
				colvarSubstringLength.MaxLength = 0;
				colvarSubstringLength.AutoIncrement = false;
				colvarSubstringLength.IsNullable = true;
				colvarSubstringLength.IsPrimaryKey = false;
				colvarSubstringLength.IsForeignKey = false;
				colvarSubstringLength.IsReadOnly = false;
				colvarSubstringLength.DefaultSetting = @"";
				colvarSubstringLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubstringLength);

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
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Fields",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Field LoadFrom(LD_Field item)
		{
			LD_Field result = new LD_Field();
			if (item.FieldID != default(int)) {
				result.LoadByKey(item.FieldID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int FieldID { 
			get { return GetColumnValue<int>(Columns.FieldID); }
			set {
				SetColumnValue(Columns.FieldID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FieldID));
			}
		}
		[DataMember]
		public int TemplateID { 
			get { return GetColumnValue<int>(Columns.TemplateID); }
			set {
				SetColumnValue(Columns.TemplateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TemplateID));
			}
		}
		[DataMember]
		public int? SubsequentFieldID { 
			get { return GetColumnValue<int?>(Columns.SubsequentFieldID); }
			set {
				SetColumnValue(Columns.SubsequentFieldID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubsequentFieldID));
			}
		}
		[DataMember]
		public string FieldName { 
			get { return GetColumnValue<string>(Columns.FieldName); }
			set {
				SetColumnValue(Columns.FieldName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FieldName));
			}
		}
		[DataMember]
		public string DBColumnName { 
			get { return GetColumnValue<string>(Columns.DBColumnName); }
			set {
				SetColumnValue(Columns.DBColumnName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DBColumnName));
			}
		}
		[DataMember]
		public string FormatString { 
			get { return GetColumnValue<string>(Columns.FormatString); }
			set {
				SetColumnValue(Columns.FormatString, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FormatString));
			}
		}
		[DataMember]
		public string PreprocessFormatString { 
			get { return GetColumnValue<string>(Columns.PreprocessFormatString); }
			set {
				SetColumnValue(Columns.PreprocessFormatString, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PreprocessFormatString));
			}
		}
		[DataMember]
		public bool IsEncrypted { 
			get { return GetColumnValue<bool>(Columns.IsEncrypted); }
			set {
				SetColumnValue(Columns.IsEncrypted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsEncrypted));
			}
		}
		[DataMember]
		public bool IsSubstring { 
			get { return GetColumnValue<bool>(Columns.IsSubstring); }
			set {
				SetColumnValue(Columns.IsSubstring, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsSubstring));
			}
		}
		[DataMember]
		public int? SubstringStart { 
			get { return GetColumnValue<int?>(Columns.SubstringStart); }
			set {
				SetColumnValue(Columns.SubstringStart, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubstringStart));
			}
		}
		[DataMember]
		public int? SubstringLength { 
			get { return GetColumnValue<int?>(Columns.SubstringLength); }
			set {
				SetColumnValue(Columns.SubstringLength, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubstringLength));
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

		#region ForeignKey Properties

		private LD_Field _SubsequentField;
		//Relationship: FK_LD_Fields_LD_Fields
		public LD_Field SubsequentField
		{
			get
			{
				if(_SubsequentField == null) {
					_SubsequentField = LD_Field.FetchByID(this.SubsequentFieldID);
				}
				return _SubsequentField;
			}
			set
			{
				SetColumnValue("SubsequentFieldID", value.FieldID);
				_SubsequentField = value;
			}
		}

		private LD_Template _Template;
		//Relationship: FK_LD_Fields_LD_Templates
		public LD_Template Template
		{
			get
			{
				if(_Template == null) {
					_Template = LD_Template.FetchByID(this.TemplateID);
				}
				return _Template;
			}
			set
			{
				SetColumnValue("TemplateID", value.TemplateID);
				_Template = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return FieldID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn FieldIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SubsequentFieldIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FieldNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn DBColumnNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn FormatStringColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PreprocessFormatStringColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn IsEncryptedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsSubstringColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn SubstringStartColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SubstringLengthColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string FieldID = @"FieldID";
			public static readonly string TemplateID = @"TemplateID";
			public static readonly string SubsequentFieldID = @"SubsequentFieldID";
			public static readonly string FieldName = @"FieldName";
			public static readonly string DBColumnName = @"DBColumnName";
			public static readonly string FormatString = @"FormatString";
			public static readonly string PreprocessFormatString = @"PreprocessFormatString";
			public static readonly string IsEncrypted = @"IsEncrypted";
			public static readonly string IsSubstring = @"IsSubstring";
			public static readonly string SubstringStart = @"SubstringStart";
			public static readonly string SubstringLength = @"SubstringLength";
			public static readonly string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return FieldID; }
		}
		*/

		#region Foreign Collections

		private LD_FieldCollection _ChildLD_FieldsCol;
		//Relationship: FK_LD_Fields_LD_Fields
		public LD_FieldCollection ChildLD_FieldsCol
		{
			get
			{
				if(_ChildLD_FieldsCol == null) {
					_ChildLD_FieldsCol = new LD_FieldCollection();
					_ChildLD_FieldsCol.LoadAndCloseReader(LD_Field.Query()
						.WHERE(LD_Field.Columns.SubsequentFieldID, FieldID).ExecuteReader());
				}
				return _ChildLD_FieldsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_Insert class.
	/// </summary>
	[DataContract]
	public partial class LD_InsertCollection : ActiveList<LD_Insert, LD_InsertCollection>
	{
		public static LD_InsertCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_InsertCollection result = new LD_InsertCollection();
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
			foreach (LD_Insert item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Insert table.
	/// </summary>
	[DataContract]
	public partial class LD_Insert : ActiveRecord<LD_Insert>, INotifyPropertyChanged
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

		public LD_Insert()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Insert", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarInsertID = new TableSchema.TableColumn(schema);
				colvarInsertID.ColumnName = "InsertID";
				colvarInsertID.DataType = DbType.Int32;
				colvarInsertID.MaxLength = 0;
				colvarInsertID.AutoIncrement = true;
				colvarInsertID.IsNullable = false;
				colvarInsertID.IsPrimaryKey = true;
				colvarInsertID.IsForeignKey = false;
				colvarInsertID.IsReadOnly = false;
				colvarInsertID.DefaultSetting = @"";
				colvarInsertID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsertID);

				TableSchema.TableColumn colvarLetterID = new TableSchema.TableColumn(schema);
				colvarLetterID.ColumnName = "LetterID";
				colvarLetterID.DataType = DbType.Int32;
				colvarLetterID.MaxLength = 0;
				colvarLetterID.AutoIncrement = false;
				colvarLetterID.IsNullable = false;
				colvarLetterID.IsPrimaryKey = false;
				colvarLetterID.IsForeignKey = true;
				colvarLetterID.IsReadOnly = false;
				colvarLetterID.DefaultSetting = @"";
				colvarLetterID.ForeignKeyTableName = "LD_Letter";
				schema.Columns.Add(colvarLetterID);

				TableSchema.TableColumn colvarStringOrder = new TableSchema.TableColumn(schema);
				colvarStringOrder.ColumnName = "StringOrder";
				colvarStringOrder.DataType = DbType.Int32;
				colvarStringOrder.MaxLength = 0;
				colvarStringOrder.AutoIncrement = false;
				colvarStringOrder.IsNullable = false;
				colvarStringOrder.IsPrimaryKey = false;
				colvarStringOrder.IsForeignKey = false;
				colvarStringOrder.IsReadOnly = false;
				colvarStringOrder.DefaultSetting = @"";
				colvarStringOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStringOrder);

				TableSchema.TableColumn colvarDefaultEntry = new TableSchema.TableColumn(schema);
				colvarDefaultEntry.ColumnName = "DefaultEntry";
				colvarDefaultEntry.DataType = DbType.String;
				colvarDefaultEntry.MaxLength = -1;
				colvarDefaultEntry.AutoIncrement = false;
				colvarDefaultEntry.IsNullable = false;
				colvarDefaultEntry.IsPrimaryKey = false;
				colvarDefaultEntry.IsForeignKey = false;
				colvarDefaultEntry.IsReadOnly = false;
				colvarDefaultEntry.DefaultSetting = @"";
				colvarDefaultEntry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDefaultEntry);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Insert",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Insert LoadFrom(LD_Insert item)
		{
			LD_Insert result = new LD_Insert();
			if (item.InsertID != default(int)) {
				result.LoadByKey(item.InsertID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int InsertID { 
			get { return GetColumnValue<int>(Columns.InsertID); }
			set {
				SetColumnValue(Columns.InsertID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.InsertID));
			}
		}
		[DataMember]
		public int LetterID { 
			get { return GetColumnValue<int>(Columns.LetterID); }
			set {
				SetColumnValue(Columns.LetterID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LetterID));
			}
		}
		[DataMember]
		public int StringOrder { 
			get { return GetColumnValue<int>(Columns.StringOrder); }
			set {
				SetColumnValue(Columns.StringOrder, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StringOrder));
			}
		}
		[DataMember]
		public string DefaultEntry { 
			get { return GetColumnValue<string>(Columns.DefaultEntry); }
			set {
				SetColumnValue(Columns.DefaultEntry, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DefaultEntry));
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

		#endregion //Properties

		#region ForeignKey Properties

		private LD_Letter _Letter;
		//Relationship: FK_L_Insert_L_Letter
		public LD_Letter Letter
		{
			get
			{
				if(_Letter == null) {
					_Letter = LD_Letter.FetchByID(this.LetterID);
				}
				return _Letter;
			}
			set
			{
				SetColumnValue("LetterID", value.LetterID);
				_Letter = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return InsertID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn InsertIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LetterIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn StringOrderColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DefaultEntryColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string InsertID = @"InsertID";
			public static readonly string LetterID = @"LetterID";
			public static readonly string StringOrder = @"StringOrder";
			public static readonly string DefaultEntry = @"DefaultEntry";
			public static readonly string Name = @"Name";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return InsertID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_Letter class.
	/// </summary>
	[DataContract]
	public partial class LD_LetterCollection : ActiveList<LD_Letter, LD_LetterCollection>
	{
		public static LD_LetterCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_LetterCollection result = new LD_LetterCollection();
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
			foreach (LD_Letter item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Letter table.
	/// </summary>
	[DataContract]
	public partial class LD_Letter : ActiveRecord<LD_Letter>, INotifyPropertyChanged
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

		public LD_Letter()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Letter", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLetterID = new TableSchema.TableColumn(schema);
				colvarLetterID.ColumnName = "LetterID";
				colvarLetterID.DataType = DbType.Int32;
				colvarLetterID.MaxLength = 0;
				colvarLetterID.AutoIncrement = true;
				colvarLetterID.IsNullable = false;
				colvarLetterID.IsPrimaryKey = true;
				colvarLetterID.IsForeignKey = false;
				colvarLetterID.IsReadOnly = false;
				colvarLetterID.DefaultSetting = @"";
				colvarLetterID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLetterID);

				TableSchema.TableColumn colvarDepartmentID = new TableSchema.TableColumn(schema);
				colvarDepartmentID.ColumnName = "DepartmentID";
				colvarDepartmentID.DataType = DbType.Int32;
				colvarDepartmentID.MaxLength = 0;
				colvarDepartmentID.AutoIncrement = false;
				colvarDepartmentID.IsNullable = false;
				colvarDepartmentID.IsPrimaryKey = false;
				colvarDepartmentID.IsForeignKey = true;
				colvarDepartmentID.IsReadOnly = false;
				colvarDepartmentID.DefaultSetting = @"";
				colvarDepartmentID.ForeignKeyTableName = "LD_Department";
				schema.Columns.Add(colvarDepartmentID);

				TableSchema.TableColumn colvarLetter = new TableSchema.TableColumn(schema);
				colvarLetter.ColumnName = "Letter";
				colvarLetter.DataType = DbType.String;
				colvarLetter.MaxLength = -1;
				colvarLetter.AutoIncrement = false;
				colvarLetter.IsNullable = true;
				colvarLetter.IsPrimaryKey = false;
				colvarLetter.IsForeignKey = false;
				colvarLetter.IsReadOnly = false;
				colvarLetter.DefaultSetting = @"";
				colvarLetter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLetter);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarPDFReadOnlyID = new TableSchema.TableColumn(schema);
				colvarPDFReadOnlyID.ColumnName = "PDFReadOnlyID";
				colvarPDFReadOnlyID.DataType = DbType.Int32;
				colvarPDFReadOnlyID.MaxLength = 0;
				colvarPDFReadOnlyID.AutoIncrement = false;
				colvarPDFReadOnlyID.IsNullable = true;
				colvarPDFReadOnlyID.IsPrimaryKey = false;
				colvarPDFReadOnlyID.IsForeignKey = true;
				colvarPDFReadOnlyID.IsReadOnly = false;
				colvarPDFReadOnlyID.DefaultSetting = @"";
				colvarPDFReadOnlyID.ForeignKeyTableName = "LD_PDFReadOnly";
				schema.Columns.Add(colvarPDFReadOnlyID);

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
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

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
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Letter",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Letter LoadFrom(LD_Letter item)
		{
			LD_Letter result = new LD_Letter();
			if (item.LetterID != default(int)) {
				result.LoadByKey(item.LetterID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LetterID { 
			get { return GetColumnValue<int>(Columns.LetterID); }
			set {
				SetColumnValue(Columns.LetterID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LetterID));
			}
		}
		[DataMember]
		public int DepartmentID { 
			get { return GetColumnValue<int>(Columns.DepartmentID); }
			set {
				SetColumnValue(Columns.DepartmentID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DepartmentID));
			}
		}
		[DataMember]
		public string Letter { 
			get { return GetColumnValue<string>(Columns.Letter); }
			set {
				SetColumnValue(Columns.Letter, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Letter));
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
		public string Description { 
			get { return GetColumnValue<string>(Columns.Description); }
			set {
				SetColumnValue(Columns.Description, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Description));
			}
		}
		[DataMember]
		public int? PDFReadOnlyID { 
			get { return GetColumnValue<int?>(Columns.PDFReadOnlyID); }
			set {
				SetColumnValue(Columns.PDFReadOnlyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFReadOnlyID));
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
		public string CreatedBy { 
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
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

		private LD_Department _Department;
		//Relationship: FK_L_Letter_L_Department
		public LD_Department Department
		{
			get
			{
				if(_Department == null) {
					_Department = LD_Department.FetchByID(this.DepartmentID);
				}
				return _Department;
			}
			set
			{
				SetColumnValue("DepartmentID", value.DepartmentID);
				_Department = value;
			}
		}

		private LD_PDFReadOnly _PDFReadOnly;
		//Relationship: FK_LD_PDFReadOnly_LD_Letter
		public LD_PDFReadOnly PDFReadOnly
		{
			get
			{
				if(_PDFReadOnly == null) {
					_PDFReadOnly = LD_PDFReadOnly.FetchByID(this.PDFReadOnlyID);
				}
				return _PDFReadOnly;
			}
			set
			{
				SetColumnValue("PDFReadOnlyID", value.PDFReadOnlyID);
				_PDFReadOnly = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LetterID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LetterIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DepartmentIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LetterColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PDFReadOnlyIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LetterID = @"LetterID";
			public static readonly string DepartmentID = @"DepartmentID";
			public static readonly string Letter = @"Letter";
			public static readonly string Name = @"Name";
			public static readonly string Description = @"Description";
			public static readonly string PDFReadOnlyID = @"PDFReadOnlyID";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LetterID; }
		}
		*/

		#region Foreign Collections

		private LD_InsertCollection _LD_InsertsCol;
		//Relationship: FK_L_Insert_L_Letter
		public LD_InsertCollection LD_InsertsCol
		{
			get
			{
				if(_LD_InsertsCol == null) {
					_LD_InsertsCol = new LD_InsertCollection();
					_LD_InsertsCol.LoadAndCloseReader(LD_Insert.Query()
						.WHERE(LD_Insert.Columns.LetterID, LetterID).ExecuteReader());
				}
				return _LD_InsertsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_LettersToPrint class.
	/// </summary>
	[DataContract]
	public partial class LD_LettersToPrintCollection : ActiveList<LD_LettersToPrint, LD_LettersToPrintCollection>
	{
		public static LD_LettersToPrintCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_LettersToPrintCollection result = new LD_LettersToPrintCollection();
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
			foreach (LD_LettersToPrint item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_LettersToPrint table.
	/// </summary>
	[DataContract]
	public partial class LD_LettersToPrint : ActiveRecord<LD_LettersToPrint>, INotifyPropertyChanged
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

		public LD_LettersToPrint()
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
				TableSchema.Table schema = new TableSchema.Table("LD_LettersToPrint", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLetterID = new TableSchema.TableColumn(schema);
				colvarLetterID.ColumnName = "LetterID";
				colvarLetterID.DataType = DbType.Int32;
				colvarLetterID.MaxLength = 0;
				colvarLetterID.AutoIncrement = true;
				colvarLetterID.IsNullable = false;
				colvarLetterID.IsPrimaryKey = true;
				colvarLetterID.IsForeignKey = false;
				colvarLetterID.IsReadOnly = false;
				colvarLetterID.DefaultSetting = @"";
				colvarLetterID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLetterID);

				TableSchema.TableColumn colvarAccountID = new TableSchema.TableColumn(schema);
				colvarAccountID.ColumnName = "AccountID";
				colvarAccountID.DataType = DbType.Int32;
				colvarAccountID.MaxLength = 0;
				colvarAccountID.AutoIncrement = false;
				colvarAccountID.IsNullable = false;
				colvarAccountID.IsPrimaryKey = false;
				colvarAccountID.IsForeignKey = false;
				colvarAccountID.IsReadOnly = false;
				colvarAccountID.DefaultSetting = @"";
				colvarAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountID);

				TableSchema.TableColumn colvarLetterName = new TableSchema.TableColumn(schema);
				colvarLetterName.ColumnName = "LetterName";
				colvarLetterName.DataType = DbType.String;
				colvarLetterName.MaxLength = 50;
				colvarLetterName.AutoIncrement = false;
				colvarLetterName.IsNullable = true;
				colvarLetterName.IsPrimaryKey = false;
				colvarLetterName.IsForeignKey = false;
				colvarLetterName.IsReadOnly = false;
				colvarLetterName.DefaultSetting = @"";
				colvarLetterName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLetterName);

				TableSchema.TableColumn colvarLetter = new TableSchema.TableColumn(schema);
				colvarLetter.ColumnName = "Letter";
				colvarLetter.DataType = DbType.String;
				colvarLetter.MaxLength = -1;
				colvarLetter.AutoIncrement = false;
				colvarLetter.IsNullable = false;
				colvarLetter.IsPrimaryKey = false;
				colvarLetter.IsForeignKey = false;
				colvarLetter.IsReadOnly = false;
				colvarLetter.DefaultSetting = @"";
				colvarLetter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLetter);

				TableSchema.TableColumn colvarPriority = new TableSchema.TableColumn(schema);
				colvarPriority.ColumnName = "Priority";
				colvarPriority.DataType = DbType.String;
				colvarPriority.MaxLength = 50;
				colvarPriority.AutoIncrement = false;
				colvarPriority.IsNullable = false;
				colvarPriority.IsPrimaryKey = false;
				colvarPriority.IsForeignKey = false;
				colvarPriority.IsReadOnly = false;
				colvarPriority.DefaultSetting = @"";
				colvarPriority.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPriority);

				TableSchema.TableColumn colvarCorrespondanceNotes = new TableSchema.TableColumn(schema);
				colvarCorrespondanceNotes.ColumnName = "CorrespondanceNotes";
				colvarCorrespondanceNotes.DataType = DbType.String;
				colvarCorrespondanceNotes.MaxLength = -1;
				colvarCorrespondanceNotes.AutoIncrement = false;
				colvarCorrespondanceNotes.IsNullable = true;
				colvarCorrespondanceNotes.IsPrimaryKey = false;
				colvarCorrespondanceNotes.IsForeignKey = false;
				colvarCorrespondanceNotes.IsReadOnly = false;
				colvarCorrespondanceNotes.DefaultSetting = @"";
				colvarCorrespondanceNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCorrespondanceNotes);

				TableSchema.TableColumn colvarIsPrinted = new TableSchema.TableColumn(schema);
				colvarIsPrinted.ColumnName = "IsPrinted";
				colvarIsPrinted.DataType = DbType.Boolean;
				colvarIsPrinted.MaxLength = 0;
				colvarIsPrinted.AutoIncrement = false;
				colvarIsPrinted.IsNullable = false;
				colvarIsPrinted.IsPrimaryKey = false;
				colvarIsPrinted.IsForeignKey = false;
				colvarIsPrinted.IsReadOnly = false;
				colvarIsPrinted.DefaultSetting = @"((0))";
				colvarIsPrinted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsPrinted);

				TableSchema.TableColumn colvarDatePrinted = new TableSchema.TableColumn(schema);
				colvarDatePrinted.ColumnName = "DatePrinted";
				colvarDatePrinted.DataType = DbType.DateTime;
				colvarDatePrinted.MaxLength = 0;
				colvarDatePrinted.AutoIncrement = false;
				colvarDatePrinted.IsNullable = true;
				colvarDatePrinted.IsPrimaryKey = false;
				colvarDatePrinted.IsForeignKey = false;
				colvarDatePrinted.IsReadOnly = false;
				colvarDatePrinted.DefaultSetting = @"(getdate())";
				colvarDatePrinted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDatePrinted);

				TableSchema.TableColumn colvarIsInsuranceLetter = new TableSchema.TableColumn(schema);
				colvarIsInsuranceLetter.ColumnName = "IsInsuranceLetter";
				colvarIsInsuranceLetter.DataType = DbType.Boolean;
				colvarIsInsuranceLetter.MaxLength = 0;
				colvarIsInsuranceLetter.AutoIncrement = false;
				colvarIsInsuranceLetter.IsNullable = false;
				colvarIsInsuranceLetter.IsPrimaryKey = false;
				colvarIsInsuranceLetter.IsForeignKey = false;
				colvarIsInsuranceLetter.IsReadOnly = false;
				colvarIsInsuranceLetter.DefaultSetting = @"";
				colvarIsInsuranceLetter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsInsuranceLetter);

				TableSchema.TableColumn colvarIsPlatinumCheck = new TableSchema.TableColumn(schema);
				colvarIsPlatinumCheck.ColumnName = "IsPlatinumCheck";
				colvarIsPlatinumCheck.DataType = DbType.Boolean;
				colvarIsPlatinumCheck.MaxLength = 0;
				colvarIsPlatinumCheck.AutoIncrement = false;
				colvarIsPlatinumCheck.IsNullable = true;
				colvarIsPlatinumCheck.IsPrimaryKey = false;
				colvarIsPlatinumCheck.IsForeignKey = false;
				colvarIsPlatinumCheck.IsReadOnly = false;
				colvarIsPlatinumCheck.DefaultSetting = @"((0))";
				colvarIsPlatinumCheck.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsPlatinumCheck);

				TableSchema.TableColumn colvarIsPaymentAuthorization = new TableSchema.TableColumn(schema);
				colvarIsPaymentAuthorization.ColumnName = "IsPaymentAuthorization";
				colvarIsPaymentAuthorization.DataType = DbType.Boolean;
				colvarIsPaymentAuthorization.MaxLength = 0;
				colvarIsPaymentAuthorization.AutoIncrement = false;
				colvarIsPaymentAuthorization.IsNullable = false;
				colvarIsPaymentAuthorization.IsPrimaryKey = false;
				colvarIsPaymentAuthorization.IsForeignKey = false;
				colvarIsPaymentAuthorization.IsReadOnly = false;
				colvarIsPaymentAuthorization.DefaultSetting = @"";
				colvarIsPaymentAuthorization.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsPaymentAuthorization);

				TableSchema.TableColumn colvarPDFReadOnlyID = new TableSchema.TableColumn(schema);
				colvarPDFReadOnlyID.ColumnName = "PDFReadOnlyID";
				colvarPDFReadOnlyID.DataType = DbType.Int32;
				colvarPDFReadOnlyID.MaxLength = 0;
				colvarPDFReadOnlyID.AutoIncrement = false;
				colvarPDFReadOnlyID.IsNullable = true;
				colvarPDFReadOnlyID.IsPrimaryKey = false;
				colvarPDFReadOnlyID.IsForeignKey = true;
				colvarPDFReadOnlyID.IsReadOnly = false;
				colvarPDFReadOnlyID.DefaultSetting = @"";
				colvarPDFReadOnlyID.ForeignKeyTableName = "LD_PDFReadOnly";
				schema.Columns.Add(colvarPDFReadOnlyID);

				TableSchema.TableColumn colvarTemplateID = new TableSchema.TableColumn(schema);
				colvarTemplateID.ColumnName = "TemplateID";
				colvarTemplateID.DataType = DbType.Int32;
				colvarTemplateID.MaxLength = 0;
				colvarTemplateID.AutoIncrement = false;
				colvarTemplateID.IsNullable = true;
				colvarTemplateID.IsPrimaryKey = false;
				colvarTemplateID.IsForeignKey = true;
				colvarTemplateID.IsReadOnly = false;
				colvarTemplateID.DefaultSetting = @"";
				colvarTemplateID.ForeignKeyTableName = "LD_Templates";
				schema.Columns.Add(colvarTemplateID);

				TableSchema.TableColumn colvarFaxNumber = new TableSchema.TableColumn(schema);
				colvarFaxNumber.ColumnName = "FaxNumber";
				colvarFaxNumber.DataType = DbType.String;
				colvarFaxNumber.MaxLength = 50;
				colvarFaxNumber.AutoIncrement = false;
				colvarFaxNumber.IsNullable = true;
				colvarFaxNumber.IsPrimaryKey = false;
				colvarFaxNumber.IsForeignKey = false;
				colvarFaxNumber.IsReadOnly = false;
				colvarFaxNumber.DefaultSetting = @"";
				colvarFaxNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFaxNumber);

				TableSchema.TableColumn colvarFaxRecipient = new TableSchema.TableColumn(schema);
				colvarFaxRecipient.ColumnName = "FaxRecipient";
				colvarFaxRecipient.DataType = DbType.String;
				colvarFaxRecipient.MaxLength = 50;
				colvarFaxRecipient.AutoIncrement = false;
				colvarFaxRecipient.IsNullable = true;
				colvarFaxRecipient.IsPrimaryKey = false;
				colvarFaxRecipient.IsForeignKey = false;
				colvarFaxRecipient.IsReadOnly = false;
				colvarFaxRecipient.DefaultSetting = @"";
				colvarFaxRecipient.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFaxRecipient);

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
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

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

				TableSchema.TableColumn colvarMonetaryAmount = new TableSchema.TableColumn(schema);
				colvarMonetaryAmount.ColumnName = "MonetaryAmount";
				colvarMonetaryAmount.DataType = DbType.Currency;
				colvarMonetaryAmount.MaxLength = 0;
				colvarMonetaryAmount.AutoIncrement = false;
				colvarMonetaryAmount.IsNullable = true;
				colvarMonetaryAmount.IsPrimaryKey = false;
				colvarMonetaryAmount.IsForeignKey = false;
				colvarMonetaryAmount.IsReadOnly = false;
				colvarMonetaryAmount.DefaultSetting = @"";
				colvarMonetaryAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonetaryAmount);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_LettersToPrint",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_LettersToPrint LoadFrom(LD_LettersToPrint item)
		{
			LD_LettersToPrint result = new LD_LettersToPrint();
			if (item.LetterID != default(int)) {
				result.LoadByKey(item.LetterID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LetterID { 
			get { return GetColumnValue<int>(Columns.LetterID); }
			set {
				SetColumnValue(Columns.LetterID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LetterID));
			}
		}
		[DataMember]
		public int AccountID { 
			get { return GetColumnValue<int>(Columns.AccountID); }
			set {
				SetColumnValue(Columns.AccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountID));
			}
		}
		[DataMember]
		public string LetterName { 
			get { return GetColumnValue<string>(Columns.LetterName); }
			set {
				SetColumnValue(Columns.LetterName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LetterName));
			}
		}
		[DataMember]
		public string Letter { 
			get { return GetColumnValue<string>(Columns.Letter); }
			set {
				SetColumnValue(Columns.Letter, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Letter));
			}
		}
		[DataMember]
		public string Priority { 
			get { return GetColumnValue<string>(Columns.Priority); }
			set {
				SetColumnValue(Columns.Priority, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Priority));
			}
		}
		[DataMember]
		public string CorrespondanceNotes { 
			get { return GetColumnValue<string>(Columns.CorrespondanceNotes); }
			set {
				SetColumnValue(Columns.CorrespondanceNotes, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CorrespondanceNotes));
			}
		}
		[DataMember]
		public bool IsPrinted { 
			get { return GetColumnValue<bool>(Columns.IsPrinted); }
			set {
				SetColumnValue(Columns.IsPrinted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsPrinted));
			}
		}
		[DataMember]
		public DateTime? DatePrinted { 
			get { return GetColumnValue<DateTime?>(Columns.DatePrinted); }
			set {
				SetColumnValue(Columns.DatePrinted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DatePrinted));
			}
		}
		[DataMember]
		public bool IsInsuranceLetter { 
			get { return GetColumnValue<bool>(Columns.IsInsuranceLetter); }
			set {
				SetColumnValue(Columns.IsInsuranceLetter, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsInsuranceLetter));
			}
		}
		[DataMember]
		public bool? IsPlatinumCheck { 
			get { return GetColumnValue<bool?>(Columns.IsPlatinumCheck); }
			set {
				SetColumnValue(Columns.IsPlatinumCheck, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsPlatinumCheck));
			}
		}
		[DataMember]
		public bool IsPaymentAuthorization { 
			get { return GetColumnValue<bool>(Columns.IsPaymentAuthorization); }
			set {
				SetColumnValue(Columns.IsPaymentAuthorization, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsPaymentAuthorization));
			}
		}
		[DataMember]
		public int? PDFReadOnlyID { 
			get { return GetColumnValue<int?>(Columns.PDFReadOnlyID); }
			set {
				SetColumnValue(Columns.PDFReadOnlyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFReadOnlyID));
			}
		}
		[DataMember]
		public int? TemplateID { 
			get { return GetColumnValue<int?>(Columns.TemplateID); }
			set {
				SetColumnValue(Columns.TemplateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TemplateID));
			}
		}
		[DataMember]
		public string FaxNumber { 
			get { return GetColumnValue<string>(Columns.FaxNumber); }
			set {
				SetColumnValue(Columns.FaxNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FaxNumber));
			}
		}
		[DataMember]
		public string FaxRecipient { 
			get { return GetColumnValue<string>(Columns.FaxRecipient); }
			set {
				SetColumnValue(Columns.FaxRecipient, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FaxRecipient));
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
		public string CreatedBy { 
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
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
		[DataMember]
		public decimal? MonetaryAmount { 
			get { return GetColumnValue<decimal?>(Columns.MonetaryAmount); }
			set {
				SetColumnValue(Columns.MonetaryAmount, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MonetaryAmount));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LD_PDFReadOnly _PDFReadOnly;
		//Relationship: FK_LD_PDFReadOnly_LD_LettersToPrint
		public LD_PDFReadOnly PDFReadOnly
		{
			get
			{
				if(_PDFReadOnly == null) {
					_PDFReadOnly = LD_PDFReadOnly.FetchByID(this.PDFReadOnlyID);
				}
				return _PDFReadOnly;
			}
			set
			{
				SetColumnValue("PDFReadOnlyID", value.PDFReadOnlyID);
				_PDFReadOnly = value;
			}
		}

		private LD_Template _Template;
		//Relationship: FK_LD_LettersToPrint_LD_Templates
		public LD_Template Template
		{
			get
			{
				if(_Template == null) {
					_Template = LD_Template.FetchByID(this.TemplateID);
				}
				return _Template;
			}
			set
			{
				SetColumnValue("TemplateID", value.TemplateID);
				_Template = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LetterID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LetterIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LetterNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LetterColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PriorityColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CorrespondanceNotesColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IsPrintedColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DatePrintedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsInsuranceLetterColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn IsPlatinumCheckColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsPaymentAuthorizationColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PDFReadOnlyIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn FaxNumberColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn FaxRecipientColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn MonetaryAmountColumn
		{
			get { return Schema.Columns[18]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LetterID = @"LetterID";
			public static readonly string AccountID = @"AccountID";
			public static readonly string LetterName = @"LetterName";
			public static readonly string Letter = @"Letter";
			public static readonly string Priority = @"Priority";
			public static readonly string CorrespondanceNotes = @"CorrespondanceNotes";
			public static readonly string IsPrinted = @"IsPrinted";
			public static readonly string DatePrinted = @"DatePrinted";
			public static readonly string IsInsuranceLetter = @"IsInsuranceLetter";
			public static readonly string IsPlatinumCheck = @"IsPlatinumCheck";
			public static readonly string IsPaymentAuthorization = @"IsPaymentAuthorization";
			public static readonly string PDFReadOnlyID = @"PDFReadOnlyID";
			public static readonly string TemplateID = @"TemplateID";
			public static readonly string FaxNumber = @"FaxNumber";
			public static readonly string FaxRecipient = @"FaxRecipient";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedByDate = @"CreatedByDate";
			public static readonly string MonetaryAmount = @"MonetaryAmount";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LetterID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_PDFField class.
	/// </summary>
	[DataContract]
	public partial class LD_PDFFieldCollection : ActiveList<LD_PDFField, LD_PDFFieldCollection>
	{
		public static LD_PDFFieldCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_PDFFieldCollection result = new LD_PDFFieldCollection();
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
			foreach (LD_PDFField item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_PDFFields table.
	/// </summary>
	[DataContract]
	public partial class LD_PDFField : ActiveRecord<LD_PDFField>, INotifyPropertyChanged
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

		public LD_PDFField()
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
				TableSchema.Table schema = new TableSchema.Table("LD_PDFFields", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPDFFieldID = new TableSchema.TableColumn(schema);
				colvarPDFFieldID.ColumnName = "PDFFieldID";
				colvarPDFFieldID.DataType = DbType.Int32;
				colvarPDFFieldID.MaxLength = 0;
				colvarPDFFieldID.AutoIncrement = true;
				colvarPDFFieldID.IsNullable = false;
				colvarPDFFieldID.IsPrimaryKey = true;
				colvarPDFFieldID.IsForeignKey = false;
				colvarPDFFieldID.IsReadOnly = false;
				colvarPDFFieldID.DefaultSetting = @"";
				colvarPDFFieldID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFFieldID);

				TableSchema.TableColumn colvarPDFTemplateID = new TableSchema.TableColumn(schema);
				colvarPDFTemplateID.ColumnName = "PDFTemplateID";
				colvarPDFTemplateID.DataType = DbType.Int32;
				colvarPDFTemplateID.MaxLength = 0;
				colvarPDFTemplateID.AutoIncrement = false;
				colvarPDFTemplateID.IsNullable = false;
				colvarPDFTemplateID.IsPrimaryKey = false;
				colvarPDFTemplateID.IsForeignKey = true;
				colvarPDFTemplateID.IsReadOnly = false;
				colvarPDFTemplateID.DefaultSetting = @"";
				colvarPDFTemplateID.ForeignKeyTableName = "LD_PDFTemplates";
				schema.Columns.Add(colvarPDFTemplateID);

				TableSchema.TableColumn colvarPDFFieldTypeID = new TableSchema.TableColumn(schema);
				colvarPDFFieldTypeID.ColumnName = "PDFFieldTypeID";
				colvarPDFFieldTypeID.DataType = DbType.Int32;
				colvarPDFFieldTypeID.MaxLength = 0;
				colvarPDFFieldTypeID.AutoIncrement = false;
				colvarPDFFieldTypeID.IsNullable = false;
				colvarPDFFieldTypeID.IsPrimaryKey = false;
				colvarPDFFieldTypeID.IsForeignKey = true;
				colvarPDFFieldTypeID.IsReadOnly = false;
				colvarPDFFieldTypeID.DefaultSetting = @"";
				colvarPDFFieldTypeID.ForeignKeyTableName = "LD_PDFFieldType";
				schema.Columns.Add(colvarPDFFieldTypeID);

				TableSchema.TableColumn colvarPDFFieldName = new TableSchema.TableColumn(schema);
				colvarPDFFieldName.ColumnName = "PDFFieldName";
				colvarPDFFieldName.DataType = DbType.String;
				colvarPDFFieldName.MaxLength = -1;
				colvarPDFFieldName.AutoIncrement = false;
				colvarPDFFieldName.IsNullable = false;
				colvarPDFFieldName.IsPrimaryKey = false;
				colvarPDFFieldName.IsForeignKey = false;
				colvarPDFFieldName.IsReadOnly = false;
				colvarPDFFieldName.DefaultSetting = @"";
				colvarPDFFieldName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFFieldName);

				TableSchema.TableColumn colvarModifiedByID = new TableSchema.TableColumn(schema);
				colvarModifiedByID.ColumnName = "ModifiedByID";
				colvarModifiedByID.DataType = DbType.String;
				colvarModifiedByID.MaxLength = -1;
				colvarModifiedByID.AutoIncrement = false;
				colvarModifiedByID.IsNullable = false;
				colvarModifiedByID.IsPrimaryKey = false;
				colvarModifiedByID.IsForeignKey = false;
				colvarModifiedByID.IsReadOnly = false;
				colvarModifiedByID.DefaultSetting = @"";
				colvarModifiedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByID);

				TableSchema.TableColumn colvarModifiedByDate = new TableSchema.TableColumn(schema);
				colvarModifiedByDate.ColumnName = "ModifiedByDate";
				colvarModifiedByDate.DataType = DbType.DateTime;
				colvarModifiedByDate.MaxLength = 0;
				colvarModifiedByDate.AutoIncrement = false;
				colvarModifiedByDate.IsNullable = false;
				colvarModifiedByDate.IsPrimaryKey = false;
				colvarModifiedByDate.IsForeignKey = false;
				colvarModifiedByDate.IsReadOnly = false;
				colvarModifiedByDate.DefaultSetting = @"";
				colvarModifiedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByDate);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = -1;
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
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_PDFFields",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_PDFField LoadFrom(LD_PDFField item)
		{
			LD_PDFField result = new LD_PDFField();
			if (item.PDFFieldID != default(int)) {
				result.LoadByKey(item.PDFFieldID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PDFFieldID { 
			get { return GetColumnValue<int>(Columns.PDFFieldID); }
			set {
				SetColumnValue(Columns.PDFFieldID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFieldID));
			}
		}
		[DataMember]
		public int PDFTemplateID { 
			get { return GetColumnValue<int>(Columns.PDFTemplateID); }
			set {
				SetColumnValue(Columns.PDFTemplateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFTemplateID));
			}
		}
		[DataMember]
		public int PDFFieldTypeID { 
			get { return GetColumnValue<int>(Columns.PDFFieldTypeID); }
			set {
				SetColumnValue(Columns.PDFFieldTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFieldTypeID));
			}
		}
		[DataMember]
		public string PDFFieldName { 
			get { return GetColumnValue<string>(Columns.PDFFieldName); }
			set {
				SetColumnValue(Columns.PDFFieldName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFieldName));
			}
		}
		[DataMember]
		public string ModifiedByID { 
			get { return GetColumnValue<string>(Columns.ModifiedByID); }
			set {
				SetColumnValue(Columns.ModifiedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByID));
			}
		}
		[DataMember]
		public DateTime ModifiedByDate { 
			get { return GetColumnValue<DateTime>(Columns.ModifiedByDate); }
			set {
				SetColumnValue(Columns.ModifiedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByDate));
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

		private LD_PDFTemplate _PDFTemplate;
		//Relationship: FK_LD_PDFFields_LD_PDFTemplates
		public LD_PDFTemplate PDFTemplate
		{
			get
			{
				if(_PDFTemplate == null) {
					_PDFTemplate = LD_PDFTemplate.FetchByID(this.PDFTemplateID);
				}
				return _PDFTemplate;
			}
			set
			{
				SetColumnValue("PDFTemplateID", value.PDFTemplateID);
				_PDFTemplate = value;
			}
		}

		private LD_PDFFieldType _PDFFieldType;
		//Relationship: FK_LD_PDFFields_LD_PDFFieldType1
		public LD_PDFFieldType PDFFieldType
		{
			get
			{
				if(_PDFFieldType == null) {
					_PDFFieldType = LD_PDFFieldType.FetchByID(this.PDFFieldTypeID);
				}
				return _PDFFieldType;
			}
			set
			{
				SetColumnValue("PDFFieldTypeID", value.PDFFieldTypeID);
				_PDFFieldType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PDFFieldID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PDFFieldIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PDFTemplateIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PDFFieldTypeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PDFFieldNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
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
			public static readonly string PDFFieldID = @"PDFFieldID";
			public static readonly string PDFTemplateID = @"PDFTemplateID";
			public static readonly string PDFFieldTypeID = @"PDFFieldTypeID";
			public static readonly string PDFFieldName = @"PDFFieldName";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PDFFieldID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_PDFFieldType class.
	/// </summary>
	[DataContract]
	public partial class LD_PDFFieldTypeCollection : ActiveList<LD_PDFFieldType, LD_PDFFieldTypeCollection>
	{
		public static LD_PDFFieldTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_PDFFieldTypeCollection result = new LD_PDFFieldTypeCollection();
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
			foreach (LD_PDFFieldType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_PDFFieldType table.
	/// </summary>
	[DataContract]
	public partial class LD_PDFFieldType : ActiveRecord<LD_PDFFieldType>, INotifyPropertyChanged
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

		public LD_PDFFieldType()
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
				TableSchema.Table schema = new TableSchema.Table("LD_PDFFieldType", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPDFFieldTypeID = new TableSchema.TableColumn(schema);
				colvarPDFFieldTypeID.ColumnName = "PDFFieldTypeID";
				colvarPDFFieldTypeID.DataType = DbType.Int32;
				colvarPDFFieldTypeID.MaxLength = 0;
				colvarPDFFieldTypeID.AutoIncrement = true;
				colvarPDFFieldTypeID.IsNullable = false;
				colvarPDFFieldTypeID.IsPrimaryKey = true;
				colvarPDFFieldTypeID.IsForeignKey = false;
				colvarPDFFieldTypeID.IsReadOnly = false;
				colvarPDFFieldTypeID.DefaultSetting = @"";
				colvarPDFFieldTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFFieldTypeID);

				TableSchema.TableColumn colvarPDFFieldType = new TableSchema.TableColumn(schema);
				colvarPDFFieldType.ColumnName = "PDFFieldType";
				colvarPDFFieldType.DataType = DbType.String;
				colvarPDFFieldType.MaxLength = -1;
				colvarPDFFieldType.AutoIncrement = false;
				colvarPDFFieldType.IsNullable = false;
				colvarPDFFieldType.IsPrimaryKey = false;
				colvarPDFFieldType.IsForeignKey = false;
				colvarPDFFieldType.IsReadOnly = false;
				colvarPDFFieldType.DefaultSetting = @"";
				colvarPDFFieldType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFFieldType);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_PDFFieldType",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_PDFFieldType LoadFrom(LD_PDFFieldType item)
		{
			LD_PDFFieldType result = new LD_PDFFieldType();
			if (item.PDFFieldTypeID != default(int)) {
				result.LoadByKey(item.PDFFieldTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PDFFieldTypeID { 
			get { return GetColumnValue<int>(Columns.PDFFieldTypeID); }
			set {
				SetColumnValue(Columns.PDFFieldTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFieldTypeID));
			}
		}
		[DataMember]
		public string PDFFieldType { 
			get { return GetColumnValue<string>(Columns.PDFFieldType); }
			set {
				SetColumnValue(Columns.PDFFieldType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFieldType));
			}
		}
		[DataMember]
		public string Description { 
			get { return GetColumnValue<string>(Columns.Description); }
			set {
				SetColumnValue(Columns.Description, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Description));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return PDFFieldType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PDFFieldTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PDFFieldTypeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PDFFieldTypeID = @"PDFFieldTypeID";
			public static readonly string PDFFieldType = @"PDFFieldType";
			public static readonly string Description = @"Description";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PDFFieldTypeID; }
		}
		*/

		#region Foreign Collections

		private LD_PDFFieldCollection _LD_PDFFieldsCol;
		//Relationship: FK_LD_PDFFields_LD_PDFFieldType1
		public LD_PDFFieldCollection LD_PDFFieldsCol
		{
			get
			{
				if(_LD_PDFFieldsCol == null) {
					_LD_PDFFieldsCol = new LD_PDFFieldCollection();
					_LD_PDFFieldsCol.LoadAndCloseReader(LD_PDFField.Query()
						.WHERE(LD_PDFField.Columns.PDFFieldTypeID, PDFFieldTypeID).ExecuteReader());
				}
				return _LD_PDFFieldsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_PDFReadOnly class.
	/// </summary>
	[DataContract]
	public partial class LD_PDFReadOnlyCollection : ActiveList<LD_PDFReadOnly, LD_PDFReadOnlyCollection>
	{
		public static LD_PDFReadOnlyCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_PDFReadOnlyCollection result = new LD_PDFReadOnlyCollection();
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
			foreach (LD_PDFReadOnly item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_PDFReadOnly table.
	/// </summary>
	[DataContract]
	public partial class LD_PDFReadOnly : ActiveRecord<LD_PDFReadOnly>, INotifyPropertyChanged
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

		public LD_PDFReadOnly()
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
				TableSchema.Table schema = new TableSchema.Table("LD_PDFReadOnly", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPDFReadOnlyID = new TableSchema.TableColumn(schema);
				colvarPDFReadOnlyID.ColumnName = "PDFReadOnlyID";
				colvarPDFReadOnlyID.DataType = DbType.Int32;
				colvarPDFReadOnlyID.MaxLength = 0;
				colvarPDFReadOnlyID.AutoIncrement = true;
				colvarPDFReadOnlyID.IsNullable = false;
				colvarPDFReadOnlyID.IsPrimaryKey = true;
				colvarPDFReadOnlyID.IsForeignKey = false;
				colvarPDFReadOnlyID.IsReadOnly = false;
				colvarPDFReadOnlyID.DefaultSetting = @"";
				colvarPDFReadOnlyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFReadOnlyID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 100;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarPDFFile = new TableSchema.TableColumn(schema);
				colvarPDFFile.ColumnName = "PDFFile";
				colvarPDFFile.DataType = DbType.Binary;
				colvarPDFFile.MaxLength = 2147483647;
				colvarPDFFile.AutoIncrement = false;
				colvarPDFFile.IsNullable = false;
				colvarPDFFile.IsPrimaryKey = false;
				colvarPDFFile.IsForeignKey = false;
				colvarPDFFile.IsReadOnly = false;
				colvarPDFFile.DefaultSetting = @"";
				colvarPDFFile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFFile);

				TableSchema.TableColumn colvarPages = new TableSchema.TableColumn(schema);
				colvarPages.ColumnName = "Pages";
				colvarPages.DataType = DbType.Int32;
				colvarPages.MaxLength = 0;
				colvarPages.AutoIncrement = false;
				colvarPages.IsNullable = false;
				colvarPages.IsPrimaryKey = false;
				colvarPages.IsForeignKey = false;
				colvarPages.IsReadOnly = false;
				colvarPages.DefaultSetting = @"";
				colvarPages.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPages);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = true;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = true;
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
				colvarCreatedByDate.IsNullable = true;
				colvarCreatedByDate.IsPrimaryKey = false;
				colvarCreatedByDate.IsForeignKey = false;
				colvarCreatedByDate.IsReadOnly = false;
				colvarCreatedByDate.DefaultSetting = @"";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_PDFReadOnly",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_PDFReadOnly LoadFrom(LD_PDFReadOnly item)
		{
			LD_PDFReadOnly result = new LD_PDFReadOnly();
			if (item.PDFReadOnlyID != default(int)) {
				result.LoadByKey(item.PDFReadOnlyID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PDFReadOnlyID { 
			get { return GetColumnValue<int>(Columns.PDFReadOnlyID); }
			set {
				SetColumnValue(Columns.PDFReadOnlyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFReadOnlyID));
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
		public byte[] PDFFile { 
			get { return GetColumnValue<byte[]>(Columns.PDFFile); }
			set {
				SetColumnValue(Columns.PDFFile, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFFile));
			}
		}
		[DataMember]
		public int Pages { 
			get { return GetColumnValue<int>(Columns.Pages); }
			set {
				SetColumnValue(Columns.Pages, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Pages));
			}
		}
		[DataMember]
		public bool? IsDeleted { 
			get { return GetColumnValue<bool?>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
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
		public DateTime? CreatedByDate { 
			get { return GetColumnValue<DateTime?>(Columns.CreatedByDate); }
			set {
				SetColumnValue(Columns.CreatedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedByDate));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PDFReadOnlyIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PDFFileColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PagesColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PDFReadOnlyID = @"PDFReadOnlyID";
			public static readonly string Name = @"Name";
			public static readonly string PDFFile = @"PDFFile";
			public static readonly string Pages = @"Pages";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PDFReadOnlyID; }
		}
		*/

		#region Foreign Collections

		private LD_LetterCollection _LD_LettersCol;
		//Relationship: FK_LD_PDFReadOnly_LD_Letter
		public LD_LetterCollection LD_LettersCol
		{
			get
			{
				if(_LD_LettersCol == null) {
					_LD_LettersCol = new LD_LetterCollection();
					_LD_LettersCol.LoadAndCloseReader(LD_Letter.Query()
						.WHERE(LD_Letter.Columns.PDFReadOnlyID, PDFReadOnlyID).ExecuteReader());
				}
				return _LD_LettersCol;
			}
		}

		private LD_LettersToPrintCollection _LD_LettersToPrintsCol;
		//Relationship: FK_LD_PDFReadOnly_LD_LettersToPrint
		public LD_LettersToPrintCollection LD_LettersToPrintsCol
		{
			get
			{
				if(_LD_LettersToPrintsCol == null) {
					_LD_LettersToPrintsCol = new LD_LettersToPrintCollection();
					_LD_LettersToPrintsCol.LoadAndCloseReader(LD_LettersToPrint.Query()
						.WHERE(LD_LettersToPrint.Columns.PDFReadOnlyID, PDFReadOnlyID).ExecuteReader());
				}
				return _LD_LettersToPrintsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_PDFTemplate class.
	/// </summary>
	[DataContract]
	public partial class LD_PDFTemplateCollection : ActiveList<LD_PDFTemplate, LD_PDFTemplateCollection>
	{
		public static LD_PDFTemplateCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_PDFTemplateCollection result = new LD_PDFTemplateCollection();
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
			foreach (LD_PDFTemplate item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_PDFTemplates table.
	/// </summary>
	[DataContract]
	public partial class LD_PDFTemplate : ActiveRecord<LD_PDFTemplate>, INotifyPropertyChanged
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

		public LD_PDFTemplate()
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
				TableSchema.Table schema = new TableSchema.Table("LD_PDFTemplates", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPDFTemplateID = new TableSchema.TableColumn(schema);
				colvarPDFTemplateID.ColumnName = "PDFTemplateID";
				colvarPDFTemplateID.DataType = DbType.Int32;
				colvarPDFTemplateID.MaxLength = 0;
				colvarPDFTemplateID.AutoIncrement = true;
				colvarPDFTemplateID.IsNullable = false;
				colvarPDFTemplateID.IsPrimaryKey = true;
				colvarPDFTemplateID.IsForeignKey = false;
				colvarPDFTemplateID.IsReadOnly = false;
				colvarPDFTemplateID.DefaultSetting = @"";
				colvarPDFTemplateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFTemplateID);

				TableSchema.TableColumn colvarPDFTemplateName = new TableSchema.TableColumn(schema);
				colvarPDFTemplateName.ColumnName = "PDFTemplateName";
				colvarPDFTemplateName.DataType = DbType.String;
				colvarPDFTemplateName.MaxLength = -1;
				colvarPDFTemplateName.AutoIncrement = false;
				colvarPDFTemplateName.IsNullable = false;
				colvarPDFTemplateName.IsPrimaryKey = false;
				colvarPDFTemplateName.IsForeignKey = false;
				colvarPDFTemplateName.IsReadOnly = false;
				colvarPDFTemplateName.DefaultSetting = @"";
				colvarPDFTemplateName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPDFTemplateName);

				TableSchema.TableColumn colvarTemplate = new TableSchema.TableColumn(schema);
				colvarTemplate.ColumnName = "Template";
				colvarTemplate.DataType = DbType.Binary;
				colvarTemplate.MaxLength = 2147483647;
				colvarTemplate.AutoIncrement = false;
				colvarTemplate.IsNullable = false;
				colvarTemplate.IsPrimaryKey = false;
				colvarTemplate.IsForeignKey = false;
				colvarTemplate.IsReadOnly = false;
				colvarTemplate.DefaultSetting = @"";
				colvarTemplate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplate);

				TableSchema.TableColumn colvarNumberOfPages = new TableSchema.TableColumn(schema);
				colvarNumberOfPages.ColumnName = "NumberOfPages";
				colvarNumberOfPages.DataType = DbType.Int32;
				colvarNumberOfPages.MaxLength = 0;
				colvarNumberOfPages.AutoIncrement = false;
				colvarNumberOfPages.IsNullable = false;
				colvarNumberOfPages.IsPrimaryKey = false;
				colvarNumberOfPages.IsForeignKey = false;
				colvarNumberOfPages.IsReadOnly = false;
				colvarNumberOfPages.DefaultSetting = @"";
				colvarNumberOfPages.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberOfPages);

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

				TableSchema.TableColumn colvarModifiedByID = new TableSchema.TableColumn(schema);
				colvarModifiedByID.ColumnName = "ModifiedByID";
				colvarModifiedByID.DataType = DbType.String;
				colvarModifiedByID.MaxLength = -1;
				colvarModifiedByID.AutoIncrement = false;
				colvarModifiedByID.IsNullable = false;
				colvarModifiedByID.IsPrimaryKey = false;
				colvarModifiedByID.IsForeignKey = false;
				colvarModifiedByID.IsReadOnly = false;
				colvarModifiedByID.DefaultSetting = @"";
				colvarModifiedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByID);

				TableSchema.TableColumn colvarModifiedByDate = new TableSchema.TableColumn(schema);
				colvarModifiedByDate.ColumnName = "ModifiedByDate";
				colvarModifiedByDate.DataType = DbType.DateTime;
				colvarModifiedByDate.MaxLength = 0;
				colvarModifiedByDate.AutoIncrement = false;
				colvarModifiedByDate.IsNullable = false;
				colvarModifiedByDate.IsPrimaryKey = false;
				colvarModifiedByDate.IsForeignKey = false;
				colvarModifiedByDate.IsReadOnly = false;
				colvarModifiedByDate.DefaultSetting = @"";
				colvarModifiedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByDate);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = -1;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = false;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"";
				colvarCreatedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByID);

				TableSchema.TableColumn colvarCreatedbyDate = new TableSchema.TableColumn(schema);
				colvarCreatedbyDate.ColumnName = "CreatedbyDate";
				colvarCreatedbyDate.DataType = DbType.DateTime;
				colvarCreatedbyDate.MaxLength = 0;
				colvarCreatedbyDate.AutoIncrement = false;
				colvarCreatedbyDate.IsNullable = false;
				colvarCreatedbyDate.IsPrimaryKey = false;
				colvarCreatedbyDate.IsForeignKey = false;
				colvarCreatedbyDate.IsReadOnly = false;
				colvarCreatedbyDate.DefaultSetting = @"";
				colvarCreatedbyDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedbyDate);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_PDFTemplates",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_PDFTemplate LoadFrom(LD_PDFTemplate item)
		{
			LD_PDFTemplate result = new LD_PDFTemplate();
			if (item.PDFTemplateID != default(int)) {
				result.LoadByKey(item.PDFTemplateID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PDFTemplateID { 
			get { return GetColumnValue<int>(Columns.PDFTemplateID); }
			set {
				SetColumnValue(Columns.PDFTemplateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFTemplateID));
			}
		}
		[DataMember]
		public string PDFTemplateName { 
			get { return GetColumnValue<string>(Columns.PDFTemplateName); }
			set {
				SetColumnValue(Columns.PDFTemplateName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PDFTemplateName));
			}
		}
		[DataMember]
		public byte[] Template { 
			get { return GetColumnValue<byte[]>(Columns.Template); }
			set {
				SetColumnValue(Columns.Template, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Template));
			}
		}
		[DataMember]
		public int NumberOfPages { 
			get { return GetColumnValue<int>(Columns.NumberOfPages); }
			set {
				SetColumnValue(Columns.NumberOfPages, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NumberOfPages));
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
		public string ModifiedByID { 
			get { return GetColumnValue<string>(Columns.ModifiedByID); }
			set {
				SetColumnValue(Columns.ModifiedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByID));
			}
		}
		[DataMember]
		public DateTime ModifiedByDate { 
			get { return GetColumnValue<DateTime>(Columns.ModifiedByDate); }
			set {
				SetColumnValue(Columns.ModifiedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByDate));
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
		public DateTime CreatedbyDate { 
			get { return GetColumnValue<DateTime>(Columns.CreatedbyDate); }
			set {
				SetColumnValue(Columns.CreatedbyDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedbyDate));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return PDFTemplateName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PDFTemplateIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PDFTemplateNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TemplateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn NumberOfPagesColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedbyDateColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PDFTemplateID = @"PDFTemplateID";
			public static readonly string PDFTemplateName = @"PDFTemplateName";
			public static readonly string Template = @"Template";
			public static readonly string NumberOfPages = @"NumberOfPages";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedbyDate = @"CreatedbyDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PDFTemplateID; }
		}
		*/

		#region Foreign Collections

		private LD_PDFFieldCollection _LD_PDFFieldsCol;
		//Relationship: FK_LD_PDFFields_LD_PDFTemplates
		public LD_PDFFieldCollection LD_PDFFieldsCol
		{
			get
			{
				if(_LD_PDFFieldsCol == null) {
					_LD_PDFFieldsCol = new LD_PDFFieldCollection();
					_LD_PDFFieldsCol.LoadAndCloseReader(LD_PDFField.Query()
						.WHERE(LD_PDFField.Columns.PDFTemplateID, PDFTemplateID).ExecuteReader());
				}
				return _LD_PDFFieldsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LD_Priority class.
	/// </summary>
	[DataContract]
	public partial class LD_PriorityCollection : ActiveList<LD_Priority, LD_PriorityCollection>
	{
		public static LD_PriorityCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_PriorityCollection result = new LD_PriorityCollection();
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
			foreach (LD_Priority item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Priority table.
	/// </summary>
	[DataContract]
	public partial class LD_Priority : ActiveRecord<LD_Priority>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum PriorityEnum : int
		{
			[EnumMember()] Regular___________________________________________ = 1,
			[EnumMember()] Priority__________________________________________ = 2,
		}
		
		//[DataMember]
		//public PriorityEnum PriorityCode
		//{
		//	get { return (PriorityEnum)PriorityID; }
		//	set { PriorityID = (int)value; }
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

		public LD_Priority()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Priority", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPriorityID = new TableSchema.TableColumn(schema);
				colvarPriorityID.ColumnName = "PriorityID";
				colvarPriorityID.DataType = DbType.Int32;
				colvarPriorityID.MaxLength = 0;
				colvarPriorityID.AutoIncrement = true;
				colvarPriorityID.IsNullable = false;
				colvarPriorityID.IsPrimaryKey = true;
				colvarPriorityID.IsForeignKey = false;
				colvarPriorityID.IsReadOnly = false;
				colvarPriorityID.DefaultSetting = @"";
				colvarPriorityID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPriorityID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Priority",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Priority LoadFrom(LD_Priority item)
		{
			LD_Priority result = new LD_Priority();
			if (item.PriorityID != default(int)) {
				result.LoadByKey(item.PriorityID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PriorityID { 
			get { return GetColumnValue<int>(Columns.PriorityID); }
			set {
				SetColumnValue(Columns.PriorityID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PriorityID));
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

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PriorityIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PriorityID = @"PriorityID";
			public static readonly string Name = @"Name";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PriorityID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_StandardInsert class.
	/// </summary>
	[DataContract]
	public partial class LD_StandardInsertCollection : ActiveList<LD_StandardInsert, LD_StandardInsertCollection>
	{
		public static LD_StandardInsertCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_StandardInsertCollection result = new LD_StandardInsertCollection();
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
			foreach (LD_StandardInsert item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_StandardInserts table.
	/// </summary>
	[DataContract]
	public partial class LD_StandardInsert : ActiveRecord<LD_StandardInsert>, INotifyPropertyChanged
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

		public LD_StandardInsert()
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
				TableSchema.Table schema = new TableSchema.Table("LD_StandardInserts", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarInsertID = new TableSchema.TableColumn(schema);
				colvarInsertID.ColumnName = "InsertID";
				colvarInsertID.DataType = DbType.Int32;
				colvarInsertID.MaxLength = 0;
				colvarInsertID.AutoIncrement = true;
				colvarInsertID.IsNullable = false;
				colvarInsertID.IsPrimaryKey = true;
				colvarInsertID.IsForeignKey = false;
				colvarInsertID.IsReadOnly = false;
				colvarInsertID.DefaultSetting = @"";
				colvarInsertID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsertID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarDefaultEntry = new TableSchema.TableColumn(schema);
				colvarDefaultEntry.ColumnName = "DefaultEntry";
				colvarDefaultEntry.DataType = DbType.String;
				colvarDefaultEntry.MaxLength = -1;
				colvarDefaultEntry.AutoIncrement = false;
				colvarDefaultEntry.IsNullable = false;
				colvarDefaultEntry.IsPrimaryKey = false;
				colvarDefaultEntry.IsForeignKey = false;
				colvarDefaultEntry.IsReadOnly = false;
				colvarDefaultEntry.DefaultSetting = @"";
				colvarDefaultEntry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDefaultEntry);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = true;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				BaseSchema = schema;
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_StandardInserts",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_StandardInsert LoadFrom(LD_StandardInsert item)
		{
			LD_StandardInsert result = new LD_StandardInsert();
			if (item.InsertID != default(int)) {
				result.LoadByKey(item.InsertID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int InsertID { 
			get { return GetColumnValue<int>(Columns.InsertID); }
			set {
				SetColumnValue(Columns.InsertID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.InsertID));
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
		public string DefaultEntry { 
			get { return GetColumnValue<string>(Columns.DefaultEntry); }
			set {
				SetColumnValue(Columns.DefaultEntry, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DefaultEntry));
			}
		}
		[DataMember]
		public bool? IsActive { 
			get { return GetColumnValue<bool?>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn InsertIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DefaultEntryColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string InsertID = @"InsertID";
			public static readonly string Name = @"Name";
			public static readonly string DefaultEntry = @"DefaultEntry";
			public static readonly string IsActive = @"IsActive";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return InsertID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LD_Template class.
	/// </summary>
	[DataContract]
	public partial class LD_TemplateCollection : ActiveList<LD_Template, LD_TemplateCollection>
	{
		public static LD_TemplateCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LD_TemplateCollection result = new LD_TemplateCollection();
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
			foreach (LD_Template item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LD_Templates table.
	/// </summary>
	[DataContract]
	public partial class LD_Template : ActiveRecord<LD_Template>, INotifyPropertyChanged
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

		public LD_Template()
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
				TableSchema.Table schema = new TableSchema.Table("LD_Templates", TableType.Table, DataService.GetInstance("NxsLettersProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTemplateID = new TableSchema.TableColumn(schema);
				colvarTemplateID.ColumnName = "TemplateID";
				colvarTemplateID.DataType = DbType.Int32;
				colvarTemplateID.MaxLength = 0;
				colvarTemplateID.AutoIncrement = true;
				colvarTemplateID.IsNullable = false;
				colvarTemplateID.IsPrimaryKey = true;
				colvarTemplateID.IsForeignKey = false;
				colvarTemplateID.IsReadOnly = false;
				colvarTemplateID.DefaultSetting = @"";
				colvarTemplateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplateID);

				TableSchema.TableColumn colvarDocTypeID = new TableSchema.TableColumn(schema);
				colvarDocTypeID.ColumnName = "DocTypeID";
				colvarDocTypeID.DataType = DbType.Int32;
				colvarDocTypeID.MaxLength = 0;
				colvarDocTypeID.AutoIncrement = false;
				colvarDocTypeID.IsNullable = false;
				colvarDocTypeID.IsPrimaryKey = false;
				colvarDocTypeID.IsForeignKey = true;
				colvarDocTypeID.IsReadOnly = false;
				colvarDocTypeID.DefaultSetting = @"";
				colvarDocTypeID.ForeignKeyTableName = "LD_DocTypes";
				schema.Columns.Add(colvarDocTypeID);

				TableSchema.TableColumn colvarTemplate = new TableSchema.TableColumn(schema);
				colvarTemplate.ColumnName = "Template";
				colvarTemplate.DataType = DbType.Binary;
				colvarTemplate.MaxLength = 2147483647;
				colvarTemplate.AutoIncrement = false;
				colvarTemplate.IsNullable = false;
				colvarTemplate.IsPrimaryKey = false;
				colvarTemplate.IsForeignKey = false;
				colvarTemplate.IsReadOnly = false;
				colvarTemplate.DefaultSetting = @"";
				colvarTemplate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplate);

				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "FileName";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = 100;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = true;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);

				TableSchema.TableColumn colvarPages = new TableSchema.TableColumn(schema);
				colvarPages.ColumnName = "Pages";
				colvarPages.DataType = DbType.Int32;
				colvarPages.MaxLength = 0;
				colvarPages.AutoIncrement = false;
				colvarPages.IsNullable = false;
				colvarPages.IsPrimaryKey = false;
				colvarPages.IsForeignKey = false;
				colvarPages.IsReadOnly = false;
				colvarPages.DefaultSetting = @"";
				colvarPages.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPages);

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

				TableSchema.TableColumn colvarModifiedByID = new TableSchema.TableColumn(schema);
				colvarModifiedByID.ColumnName = "ModifiedByID";
				colvarModifiedByID.DataType = DbType.String;
				colvarModifiedByID.MaxLength = 100;
				colvarModifiedByID.AutoIncrement = false;
				colvarModifiedByID.IsNullable = false;
				colvarModifiedByID.IsPrimaryKey = false;
				colvarModifiedByID.IsForeignKey = false;
				colvarModifiedByID.IsReadOnly = false;
				colvarModifiedByID.DefaultSetting = @"";
				colvarModifiedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByID);

				TableSchema.TableColumn colvarModifiedByDate = new TableSchema.TableColumn(schema);
				colvarModifiedByDate.ColumnName = "ModifiedByDate";
				colvarModifiedByDate.DataType = DbType.DateTime;
				colvarModifiedByDate.MaxLength = 0;
				colvarModifiedByDate.AutoIncrement = false;
				colvarModifiedByDate.IsNullable = false;
				colvarModifiedByDate.IsPrimaryKey = false;
				colvarModifiedByDate.IsForeignKey = false;
				colvarModifiedByDate.IsReadOnly = false;
				colvarModifiedByDate.DefaultSetting = @"";
				colvarModifiedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByDate);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 100;
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
				DataService.Providers["NxsLettersProvider"].AddSchema("LD_Templates",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LD_Template LoadFrom(LD_Template item)
		{
			LD_Template result = new LD_Template();
			if (item.TemplateID != default(int)) {
				result.LoadByKey(item.TemplateID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int TemplateID { 
			get { return GetColumnValue<int>(Columns.TemplateID); }
			set {
				SetColumnValue(Columns.TemplateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TemplateID));
			}
		}
		[DataMember]
		public int DocTypeID { 
			get { return GetColumnValue<int>(Columns.DocTypeID); }
			set {
				SetColumnValue(Columns.DocTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DocTypeID));
			}
		}
		[DataMember]
		public byte[] Template { 
			get { return GetColumnValue<byte[]>(Columns.Template); }
			set {
				SetColumnValue(Columns.Template, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Template));
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
		public int Pages { 
			get { return GetColumnValue<int>(Columns.Pages); }
			set {
				SetColumnValue(Columns.Pages, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Pages));
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
		public string ModifiedByID { 
			get { return GetColumnValue<string>(Columns.ModifiedByID); }
			set {
				SetColumnValue(Columns.ModifiedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByID));
			}
		}
		[DataMember]
		public DateTime ModifiedByDate { 
			get { return GetColumnValue<DateTime>(Columns.ModifiedByDate); }
			set {
				SetColumnValue(Columns.ModifiedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByDate));
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

		private LD_DocType _DocType;
		//Relationship: FK_LD_Templates_LD_DocTypes
		public LD_DocType DocType
		{
			get
			{
				if(_DocType == null) {
					_DocType = LD_DocType.FetchByID(this.DocTypeID);
				}
				return _DocType;
			}
			set
			{
				SetColumnValue("DocTypeID", value.DocTypeID);
				_DocType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return TemplateID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DocTypeIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TemplateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FileNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PagesColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TemplateID = @"TemplateID";
			public static readonly string DocTypeID = @"DocTypeID";
			public static readonly string Template = @"Template";
			public static readonly string FileName = @"FileName";
			public static readonly string Pages = @"Pages";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return TemplateID; }
		}
		*/

		#region Foreign Collections

		private LD_FieldCollection _LD_FieldsCol;
		//Relationship: FK_LD_Fields_LD_Templates
		public LD_FieldCollection LD_FieldsCol
		{
			get
			{
				if(_LD_FieldsCol == null) {
					_LD_FieldsCol = new LD_FieldCollection();
					_LD_FieldsCol.LoadAndCloseReader(LD_Field.Query()
						.WHERE(LD_Field.Columns.TemplateID, TemplateID).ExecuteReader());
				}
				return _LD_FieldsCol;
			}
		}

		private LD_LettersToPrintCollection _LD_LettersToPrintsCol;
		//Relationship: FK_LD_LettersToPrint_LD_Templates
		public LD_LettersToPrintCollection LD_LettersToPrintsCol
		{
			get
			{
				if(_LD_LettersToPrintsCol == null) {
					_LD_LettersToPrintsCol = new LD_LettersToPrintCollection();
					_LD_LettersToPrintsCol.LoadAndCloseReader(LD_LettersToPrint.Query()
						.WHERE(LD_LettersToPrint.Columns.TemplateID, TemplateID).ExecuteReader());
				}
				return _LD_LettersToPrintsCol;
			}
		}

		#endregion Foreign Collections

	}
}
