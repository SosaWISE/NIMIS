


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

namespace NXS.Data.Accounting
{
	/// <summary>
	/// Strongly-typed collection for the AE_Office class.
	/// </summary>
	[DataContract]
	public partial class AE_OfficeCollection : ActiveList<AE_Office, AE_OfficeCollection>
	{
		public static AE_OfficeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AE_OfficeCollection result = new AE_OfficeCollection();
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
			foreach (AE_Office item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the AE_Offices table.
	/// </summary>
	[DataContract]
	public partial class AE_Office : ActiveRecord<AE_Office>, INotifyPropertyChanged
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

		public AE_Office()
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
				TableSchema.Table schema = new TableSchema.Table("AE_Offices", TableType.Table, DataService.GetInstance("NxsAccountingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarOfficeID = new TableSchema.TableColumn(schema);
				colvarOfficeID.ColumnName = "OfficeID";
				colvarOfficeID.DataType = DbType.Int32;
				colvarOfficeID.MaxLength = 0;
				colvarOfficeID.AutoIncrement = true;
				colvarOfficeID.IsNullable = false;
				colvarOfficeID.IsPrimaryKey = true;
				colvarOfficeID.IsForeignKey = false;
				colvarOfficeID.IsReadOnly = false;
				colvarOfficeID.DefaultSetting = @"";
				colvarOfficeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeID);

				TableSchema.TableColumn colvarTimeZoneId = new TableSchema.TableColumn(schema);
				colvarTimeZoneId.ColumnName = "TimeZoneId";
				colvarTimeZoneId.DataType = DbType.Int32;
				colvarTimeZoneId.MaxLength = 0;
				colvarTimeZoneId.AutoIncrement = false;
				colvarTimeZoneId.IsNullable = false;
				colvarTimeZoneId.IsPrimaryKey = false;
				colvarTimeZoneId.IsForeignKey = false;
				colvarTimeZoneId.IsReadOnly = false;
				colvarTimeZoneId.DefaultSetting = @"";
				colvarTimeZoneId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTimeZoneId);

				TableSchema.TableColumn colvarOfficeName = new TableSchema.TableColumn(schema);
				colvarOfficeName.ColumnName = "OfficeName";
				colvarOfficeName.DataType = DbType.AnsiString;
				colvarOfficeName.MaxLength = 50;
				colvarOfficeName.AutoIncrement = false;
				colvarOfficeName.IsNullable = false;
				colvarOfficeName.IsPrimaryKey = false;
				colvarOfficeName.IsForeignKey = false;
				colvarOfficeName.IsReadOnly = false;
				colvarOfficeName.DefaultSetting = @"";
				colvarOfficeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeName);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.AnsiString;
				colvarCity.MaxLength = 128;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = false;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarStateId = new TableSchema.TableColumn(schema);
				colvarStateId.ColumnName = "StateId";
				colvarStateId.DataType = DbType.AnsiStringFixedLength;
				colvarStateId.MaxLength = 2;
				colvarStateId.AutoIncrement = false;
				colvarStateId.IsNullable = false;
				colvarStateId.IsPrimaryKey = false;
				colvarStateId.IsForeignKey = false;
				colvarStateId.IsReadOnly = false;
				colvarStateId.DefaultSetting = @"";
				colvarStateId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateId);

				TableSchema.TableColumn colvarZip = new TableSchema.TableColumn(schema);
				colvarZip.ColumnName = "Zip";
				colvarZip.DataType = DbType.AnsiString;
				colvarZip.MaxLength = 10;
				colvarZip.AutoIncrement = false;
				colvarZip.IsNullable = false;
				colvarZip.IsPrimaryKey = false;
				colvarZip.IsForeignKey = false;
				colvarZip.IsReadOnly = false;
				colvarZip.DefaultSetting = @"";
				colvarZip.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZip);

				TableSchema.TableColumn colvarAbbreviation = new TableSchema.TableColumn(schema);
				colvarAbbreviation.ColumnName = "Abbreviation";
				colvarAbbreviation.DataType = DbType.AnsiString;
				colvarAbbreviation.MaxLength = 10;
				colvarAbbreviation.AutoIncrement = false;
				colvarAbbreviation.IsNullable = false;
				colvarAbbreviation.IsPrimaryKey = false;
				colvarAbbreviation.IsForeignKey = false;
				colvarAbbreviation.IsReadOnly = false;
				colvarAbbreviation.DefaultSetting = @"";
				colvarAbbreviation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAbbreviation);

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
				DataService.Providers["NxsAccountingProvider"].AddSchema("AE_Offices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static AE_Office LoadFrom(AE_Office item)
		{
			AE_Office result = new AE_Office();
			if (item.OfficeID != default(int)) {
				result.LoadByKey(item.OfficeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int OfficeID { 
			get { return GetColumnValue<int>(Columns.OfficeID); }
			set {
				SetColumnValue(Columns.OfficeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeID));
			}
		}
		[DataMember]
		public int TimeZoneId { 
			get { return GetColumnValue<int>(Columns.TimeZoneId); }
			set {
				SetColumnValue(Columns.TimeZoneId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TimeZoneId));
			}
		}
		[DataMember]
		public string OfficeName { 
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set {
				SetColumnValue(Columns.OfficeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeName));
			}
		}
		[DataMember]
		public string City { 
			get { return GetColumnValue<string>(Columns.City); }
			set {
				SetColumnValue(Columns.City, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.City));
			}
		}
		[DataMember]
		public string StateId { 
			get { return GetColumnValue<string>(Columns.StateId); }
			set {
				SetColumnValue(Columns.StateId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StateId));
			}
		}
		[DataMember]
		public string Zip { 
			get { return GetColumnValue<string>(Columns.Zip); }
			set {
				SetColumnValue(Columns.Zip, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Zip));
			}
		}
		[DataMember]
		public string Abbreviation { 
			get { return GetColumnValue<string>(Columns.Abbreviation); }
			set {
				SetColumnValue(Columns.Abbreviation, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Abbreviation));
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
		public DateTime CreatedOn { 
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
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


		public override string ToString()
		{
			return OfficeID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn OfficeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TimeZoneIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn StateIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ZipColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn AbbreviationColumn
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
			public static readonly string OfficeID = @"OfficeID";
			public static readonly string TimeZoneId = @"TimeZoneId";
			public static readonly string OfficeName = @"OfficeName";
			public static readonly string City = @"City";
			public static readonly string StateId = @"StateId";
			public static readonly string Zip = @"Zip";
			public static readonly string Abbreviation = @"Abbreviation";
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
			get { return OfficeID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the RM00201 class.
	/// </summary>
	[DataContract]
	public partial class RM00201Collection : ActiveList<RM00201, RM00201Collection>
	{
		public static RM00201Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			RM00201Collection result = new RM00201Collection();
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
			foreach (RM00201 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the RM00201 table.
	/// </summary>
	[DataContract]
	public partial class RM00201 : ActiveRecord<RM00201>, INotifyPropertyChanged
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

		public RM00201()
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
				TableSchema.Table schema = new TableSchema.Table("RM00201", TableType.Table, DataService.GetInstance("NxsAccountingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCLASSID = new TableSchema.TableColumn(schema);
				colvarCLASSID.ColumnName = "CLASSID";
				colvarCLASSID.DataType = DbType.AnsiStringFixedLength;
				colvarCLASSID.MaxLength = 15;
				colvarCLASSID.AutoIncrement = false;
				colvarCLASSID.IsNullable = false;
				colvarCLASSID.IsPrimaryKey = false;
				colvarCLASSID.IsForeignKey = false;
				colvarCLASSID.IsReadOnly = false;
				colvarCLASSID.DefaultSetting = @"";
				colvarCLASSID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCLASSID);

				TableSchema.TableColumn colvarCLASDSCR = new TableSchema.TableColumn(schema);
				colvarCLASDSCR.ColumnName = "CLASDSCR";
				colvarCLASDSCR.DataType = DbType.AnsiStringFixedLength;
				colvarCLASDSCR.MaxLength = 31;
				colvarCLASDSCR.AutoIncrement = false;
				colvarCLASDSCR.IsNullable = false;
				colvarCLASDSCR.IsPrimaryKey = false;
				colvarCLASDSCR.IsForeignKey = false;
				colvarCLASDSCR.IsReadOnly = false;
				colvarCLASDSCR.DefaultSetting = @"";
				colvarCLASDSCR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCLASDSCR);

				TableSchema.TableColumn colvarCRLMTTYP = new TableSchema.TableColumn(schema);
				colvarCRLMTTYP.ColumnName = "CRLMTTYP";
				colvarCRLMTTYP.DataType = DbType.Int16;
				colvarCRLMTTYP.MaxLength = 0;
				colvarCRLMTTYP.AutoIncrement = false;
				colvarCRLMTTYP.IsNullable = false;
				colvarCRLMTTYP.IsPrimaryKey = false;
				colvarCRLMTTYP.IsForeignKey = false;
				colvarCRLMTTYP.IsReadOnly = false;
				colvarCRLMTTYP.DefaultSetting = @"";
				colvarCRLMTTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCRLMTTYP);

				TableSchema.TableColumn colvarCRLMTAMT = new TableSchema.TableColumn(schema);
				colvarCRLMTAMT.ColumnName = "CRLMTAMT";
				colvarCRLMTAMT.DataType = DbType.Decimal;
				colvarCRLMTAMT.MaxLength = 0;
				colvarCRLMTAMT.AutoIncrement = false;
				colvarCRLMTAMT.IsNullable = false;
				colvarCRLMTAMT.IsPrimaryKey = false;
				colvarCRLMTAMT.IsForeignKey = false;
				colvarCRLMTAMT.IsReadOnly = false;
				colvarCRLMTAMT.DefaultSetting = @"";
				colvarCRLMTAMT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCRLMTAMT);

				TableSchema.TableColumn colvarCRLMTPER = new TableSchema.TableColumn(schema);
				colvarCRLMTPER.ColumnName = "CRLMTPER";
				colvarCRLMTPER.DataType = DbType.Int16;
				colvarCRLMTPER.MaxLength = 0;
				colvarCRLMTPER.AutoIncrement = false;
				colvarCRLMTPER.IsNullable = false;
				colvarCRLMTPER.IsPrimaryKey = false;
				colvarCRLMTPER.IsForeignKey = false;
				colvarCRLMTPER.IsReadOnly = false;
				colvarCRLMTPER.DefaultSetting = @"";
				colvarCRLMTPER.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCRLMTPER);

				TableSchema.TableColumn colvarCRLMTPAM = new TableSchema.TableColumn(schema);
				colvarCRLMTPAM.ColumnName = "CRLMTPAM";
				colvarCRLMTPAM.DataType = DbType.Decimal;
				colvarCRLMTPAM.MaxLength = 0;
				colvarCRLMTPAM.AutoIncrement = false;
				colvarCRLMTPAM.IsNullable = false;
				colvarCRLMTPAM.IsPrimaryKey = false;
				colvarCRLMTPAM.IsForeignKey = false;
				colvarCRLMTPAM.IsReadOnly = false;
				colvarCRLMTPAM.DefaultSetting = @"";
				colvarCRLMTPAM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCRLMTPAM);

				TableSchema.TableColumn colvarDEFLTCLS = new TableSchema.TableColumn(schema);
				colvarDEFLTCLS.ColumnName = "DEFLTCLS";
				colvarDEFLTCLS.DataType = DbType.Byte;
				colvarDEFLTCLS.MaxLength = 0;
				colvarDEFLTCLS.AutoIncrement = false;
				colvarDEFLTCLS.IsNullable = false;
				colvarDEFLTCLS.IsPrimaryKey = false;
				colvarDEFLTCLS.IsForeignKey = false;
				colvarDEFLTCLS.IsReadOnly = false;
				colvarDEFLTCLS.DefaultSetting = @"";
				colvarDEFLTCLS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEFLTCLS);

				TableSchema.TableColumn colvarBALNCTYP = new TableSchema.TableColumn(schema);
				colvarBALNCTYP.ColumnName = "BALNCTYP";
				colvarBALNCTYP.DataType = DbType.Int16;
				colvarBALNCTYP.MaxLength = 0;
				colvarBALNCTYP.AutoIncrement = false;
				colvarBALNCTYP.IsNullable = false;
				colvarBALNCTYP.IsPrimaryKey = false;
				colvarBALNCTYP.IsForeignKey = false;
				colvarBALNCTYP.IsReadOnly = false;
				colvarBALNCTYP.DefaultSetting = @"";
				colvarBALNCTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBALNCTYP);

				TableSchema.TableColumn colvarCHEKBKID = new TableSchema.TableColumn(schema);
				colvarCHEKBKID.ColumnName = "CHEKBKID";
				colvarCHEKBKID.DataType = DbType.AnsiStringFixedLength;
				colvarCHEKBKID.MaxLength = 15;
				colvarCHEKBKID.AutoIncrement = false;
				colvarCHEKBKID.IsNullable = false;
				colvarCHEKBKID.IsPrimaryKey = false;
				colvarCHEKBKID.IsForeignKey = false;
				colvarCHEKBKID.IsReadOnly = false;
				colvarCHEKBKID.DefaultSetting = @"";
				colvarCHEKBKID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCHEKBKID);

				TableSchema.TableColumn colvarBANKNAME = new TableSchema.TableColumn(schema);
				colvarBANKNAME.ColumnName = "BANKNAME";
				colvarBANKNAME.DataType = DbType.AnsiStringFixedLength;
				colvarBANKNAME.MaxLength = 31;
				colvarBANKNAME.AutoIncrement = false;
				colvarBANKNAME.IsNullable = false;
				colvarBANKNAME.IsPrimaryKey = false;
				colvarBANKNAME.IsForeignKey = false;
				colvarBANKNAME.IsReadOnly = false;
				colvarBANKNAME.DefaultSetting = @"";
				colvarBANKNAME.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBANKNAME);

				TableSchema.TableColumn colvarTAXSCHID = new TableSchema.TableColumn(schema);
				colvarTAXSCHID.ColumnName = "TAXSCHID";
				colvarTAXSCHID.DataType = DbType.AnsiStringFixedLength;
				colvarTAXSCHID.MaxLength = 15;
				colvarTAXSCHID.AutoIncrement = false;
				colvarTAXSCHID.IsNullable = false;
				colvarTAXSCHID.IsPrimaryKey = false;
				colvarTAXSCHID.IsForeignKey = false;
				colvarTAXSCHID.IsReadOnly = false;
				colvarTAXSCHID.DefaultSetting = @"";
				colvarTAXSCHID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTAXSCHID);

				TableSchema.TableColumn colvarSHIPMTHD = new TableSchema.TableColumn(schema);
				colvarSHIPMTHD.ColumnName = "SHIPMTHD";
				colvarSHIPMTHD.DataType = DbType.AnsiStringFixedLength;
				colvarSHIPMTHD.MaxLength = 15;
				colvarSHIPMTHD.AutoIncrement = false;
				colvarSHIPMTHD.IsNullable = false;
				colvarSHIPMTHD.IsPrimaryKey = false;
				colvarSHIPMTHD.IsForeignKey = false;
				colvarSHIPMTHD.IsReadOnly = false;
				colvarSHIPMTHD.DefaultSetting = @"";
				colvarSHIPMTHD.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSHIPMTHD);

				TableSchema.TableColumn colvarPYMTRMID = new TableSchema.TableColumn(schema);
				colvarPYMTRMID.ColumnName = "PYMTRMID";
				colvarPYMTRMID.DataType = DbType.AnsiStringFixedLength;
				colvarPYMTRMID.MaxLength = 21;
				colvarPYMTRMID.AutoIncrement = false;
				colvarPYMTRMID.IsNullable = false;
				colvarPYMTRMID.IsPrimaryKey = false;
				colvarPYMTRMID.IsForeignKey = false;
				colvarPYMTRMID.IsReadOnly = false;
				colvarPYMTRMID.DefaultSetting = @"";
				colvarPYMTRMID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPYMTRMID);

				TableSchema.TableColumn colvarCUSTDISC = new TableSchema.TableColumn(schema);
				colvarCUSTDISC.ColumnName = "CUSTDISC";
				colvarCUSTDISC.DataType = DbType.Int16;
				colvarCUSTDISC.MaxLength = 0;
				colvarCUSTDISC.AutoIncrement = false;
				colvarCUSTDISC.IsNullable = false;
				colvarCUSTDISC.IsPrimaryKey = false;
				colvarCUSTDISC.IsForeignKey = false;
				colvarCUSTDISC.IsReadOnly = false;
				colvarCUSTDISC.DefaultSetting = @"";
				colvarCUSTDISC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCUSTDISC);

				TableSchema.TableColumn colvarCSTPRLVL = new TableSchema.TableColumn(schema);
				colvarCSTPRLVL.ColumnName = "CSTPRLVL";
				colvarCSTPRLVL.DataType = DbType.AnsiStringFixedLength;
				colvarCSTPRLVL.MaxLength = 11;
				colvarCSTPRLVL.AutoIncrement = false;
				colvarCSTPRLVL.IsNullable = false;
				colvarCSTPRLVL.IsPrimaryKey = false;
				colvarCSTPRLVL.IsForeignKey = false;
				colvarCSTPRLVL.IsReadOnly = false;
				colvarCSTPRLVL.DefaultSetting = @"";
				colvarCSTPRLVL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCSTPRLVL);

				TableSchema.TableColumn colvarMINPYTYP = new TableSchema.TableColumn(schema);
				colvarMINPYTYP.ColumnName = "MINPYTYP";
				colvarMINPYTYP.DataType = DbType.Int16;
				colvarMINPYTYP.MaxLength = 0;
				colvarMINPYTYP.AutoIncrement = false;
				colvarMINPYTYP.IsNullable = false;
				colvarMINPYTYP.IsPrimaryKey = false;
				colvarMINPYTYP.IsForeignKey = false;
				colvarMINPYTYP.IsReadOnly = false;
				colvarMINPYTYP.DefaultSetting = @"";
				colvarMINPYTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMINPYTYP);

				TableSchema.TableColumn colvarMINPYDLR = new TableSchema.TableColumn(schema);
				colvarMINPYDLR.ColumnName = "MINPYDLR";
				colvarMINPYDLR.DataType = DbType.Decimal;
				colvarMINPYDLR.MaxLength = 0;
				colvarMINPYDLR.AutoIncrement = false;
				colvarMINPYDLR.IsNullable = false;
				colvarMINPYDLR.IsPrimaryKey = false;
				colvarMINPYDLR.IsForeignKey = false;
				colvarMINPYDLR.IsReadOnly = false;
				colvarMINPYDLR.DefaultSetting = @"";
				colvarMINPYDLR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMINPYDLR);

				TableSchema.TableColumn colvarMINPYPCT = new TableSchema.TableColumn(schema);
				colvarMINPYPCT.ColumnName = "MINPYPCT";
				colvarMINPYPCT.DataType = DbType.Int16;
				colvarMINPYPCT.MaxLength = 0;
				colvarMINPYPCT.AutoIncrement = false;
				colvarMINPYPCT.IsNullable = false;
				colvarMINPYPCT.IsPrimaryKey = false;
				colvarMINPYPCT.IsForeignKey = false;
				colvarMINPYPCT.IsReadOnly = false;
				colvarMINPYPCT.DefaultSetting = @"";
				colvarMINPYPCT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMINPYPCT);

				TableSchema.TableColumn colvarMXWOFTYP = new TableSchema.TableColumn(schema);
				colvarMXWOFTYP.ColumnName = "MXWOFTYP";
				colvarMXWOFTYP.DataType = DbType.Int16;
				colvarMXWOFTYP.MaxLength = 0;
				colvarMXWOFTYP.AutoIncrement = false;
				colvarMXWOFTYP.IsNullable = false;
				colvarMXWOFTYP.IsPrimaryKey = false;
				colvarMXWOFTYP.IsForeignKey = false;
				colvarMXWOFTYP.IsReadOnly = false;
				colvarMXWOFTYP.DefaultSetting = @"";
				colvarMXWOFTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMXWOFTYP);

				TableSchema.TableColumn colvarMXWROFAM = new TableSchema.TableColumn(schema);
				colvarMXWROFAM.ColumnName = "MXWROFAM";
				colvarMXWROFAM.DataType = DbType.Decimal;
				colvarMXWROFAM.MaxLength = 0;
				colvarMXWROFAM.AutoIncrement = false;
				colvarMXWROFAM.IsNullable = false;
				colvarMXWROFAM.IsPrimaryKey = false;
				colvarMXWROFAM.IsForeignKey = false;
				colvarMXWROFAM.IsReadOnly = false;
				colvarMXWROFAM.DefaultSetting = @"";
				colvarMXWROFAM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMXWROFAM);

				TableSchema.TableColumn colvarFINCHARG = new TableSchema.TableColumn(schema);
				colvarFINCHARG.ColumnName = "FINCHARG";
				colvarFINCHARG.DataType = DbType.Byte;
				colvarFINCHARG.MaxLength = 0;
				colvarFINCHARG.AutoIncrement = false;
				colvarFINCHARG.IsNullable = false;
				colvarFINCHARG.IsPrimaryKey = false;
				colvarFINCHARG.IsForeignKey = false;
				colvarFINCHARG.IsReadOnly = false;
				colvarFINCHARG.DefaultSetting = @"";
				colvarFINCHARG.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFINCHARG);

				TableSchema.TableColumn colvarFNCHATYP = new TableSchema.TableColumn(schema);
				colvarFNCHATYP.ColumnName = "FNCHATYP";
				colvarFNCHATYP.DataType = DbType.Int16;
				colvarFNCHATYP.MaxLength = 0;
				colvarFNCHATYP.AutoIncrement = false;
				colvarFNCHATYP.IsNullable = false;
				colvarFNCHATYP.IsPrimaryKey = false;
				colvarFNCHATYP.IsForeignKey = false;
				colvarFNCHATYP.IsReadOnly = false;
				colvarFNCHATYP.DefaultSetting = @"";
				colvarFNCHATYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFNCHATYP);

				TableSchema.TableColumn colvarFINCHDLR = new TableSchema.TableColumn(schema);
				colvarFINCHDLR.ColumnName = "FINCHDLR";
				colvarFINCHDLR.DataType = DbType.Decimal;
				colvarFINCHDLR.MaxLength = 0;
				colvarFINCHDLR.AutoIncrement = false;
				colvarFINCHDLR.IsNullable = false;
				colvarFINCHDLR.IsPrimaryKey = false;
				colvarFINCHDLR.IsForeignKey = false;
				colvarFINCHDLR.IsReadOnly = false;
				colvarFINCHDLR.DefaultSetting = @"";
				colvarFINCHDLR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFINCHDLR);

				TableSchema.TableColumn colvarFNCHPCNT = new TableSchema.TableColumn(schema);
				colvarFNCHPCNT.ColumnName = "FNCHPCNT";
				colvarFNCHPCNT.DataType = DbType.Int16;
				colvarFNCHPCNT.MaxLength = 0;
				colvarFNCHPCNT.AutoIncrement = false;
				colvarFNCHPCNT.IsNullable = false;
				colvarFNCHPCNT.IsPrimaryKey = false;
				colvarFNCHPCNT.IsForeignKey = false;
				colvarFNCHPCNT.IsReadOnly = false;
				colvarFNCHPCNT.DefaultSetting = @"";
				colvarFNCHPCNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFNCHPCNT);

				TableSchema.TableColumn colvarPRCLEVEL = new TableSchema.TableColumn(schema);
				colvarPRCLEVEL.ColumnName = "PRCLEVEL";
				colvarPRCLEVEL.DataType = DbType.AnsiStringFixedLength;
				colvarPRCLEVEL.MaxLength = 11;
				colvarPRCLEVEL.AutoIncrement = false;
				colvarPRCLEVEL.IsNullable = false;
				colvarPRCLEVEL.IsPrimaryKey = false;
				colvarPRCLEVEL.IsForeignKey = false;
				colvarPRCLEVEL.IsReadOnly = false;
				colvarPRCLEVEL.DefaultSetting = @"";
				colvarPRCLEVEL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPRCLEVEL);

				TableSchema.TableColumn colvarCURNCYID = new TableSchema.TableColumn(schema);
				colvarCURNCYID.ColumnName = "CURNCYID";
				colvarCURNCYID.DataType = DbType.AnsiStringFixedLength;
				colvarCURNCYID.MaxLength = 15;
				colvarCURNCYID.AutoIncrement = false;
				colvarCURNCYID.IsNullable = false;
				colvarCURNCYID.IsPrimaryKey = false;
				colvarCURNCYID.IsForeignKey = false;
				colvarCURNCYID.IsReadOnly = false;
				colvarCURNCYID.DefaultSetting = @"";
				colvarCURNCYID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCURNCYID);

				TableSchema.TableColumn colvarRATETPID = new TableSchema.TableColumn(schema);
				colvarRATETPID.ColumnName = "RATETPID";
				colvarRATETPID.DataType = DbType.AnsiStringFixedLength;
				colvarRATETPID.MaxLength = 15;
				colvarRATETPID.AutoIncrement = false;
				colvarRATETPID.IsNullable = false;
				colvarRATETPID.IsPrimaryKey = false;
				colvarRATETPID.IsForeignKey = false;
				colvarRATETPID.IsReadOnly = false;
				colvarRATETPID.DefaultSetting = @"";
				colvarRATETPID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRATETPID);

				TableSchema.TableColumn colvarDEFCACTY = new TableSchema.TableColumn(schema);
				colvarDEFCACTY.ColumnName = "DEFCACTY";
				colvarDEFCACTY.DataType = DbType.Int16;
				colvarDEFCACTY.MaxLength = 0;
				colvarDEFCACTY.AutoIncrement = false;
				colvarDEFCACTY.IsNullable = false;
				colvarDEFCACTY.IsPrimaryKey = false;
				colvarDEFCACTY.IsForeignKey = false;
				colvarDEFCACTY.IsReadOnly = false;
				colvarDEFCACTY.DefaultSetting = @"";
				colvarDEFCACTY.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEFCACTY);

				TableSchema.TableColumn colvarRMCSHACC = new TableSchema.TableColumn(schema);
				colvarRMCSHACC.ColumnName = "RMCSHACC";
				colvarRMCSHACC.DataType = DbType.Int32;
				colvarRMCSHACC.MaxLength = 0;
				colvarRMCSHACC.AutoIncrement = false;
				colvarRMCSHACC.IsNullable = false;
				colvarRMCSHACC.IsPrimaryKey = false;
				colvarRMCSHACC.IsForeignKey = false;
				colvarRMCSHACC.IsReadOnly = false;
				colvarRMCSHACC.DefaultSetting = @"";
				colvarRMCSHACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMCSHACC);

				TableSchema.TableColumn colvarRMARACC = new TableSchema.TableColumn(schema);
				colvarRMARACC.ColumnName = "RMARACC";
				colvarRMARACC.DataType = DbType.Int32;
				colvarRMARACC.MaxLength = 0;
				colvarRMARACC.AutoIncrement = false;
				colvarRMARACC.IsNullable = false;
				colvarRMARACC.IsPrimaryKey = false;
				colvarRMARACC.IsForeignKey = false;
				colvarRMARACC.IsReadOnly = false;
				colvarRMARACC.DefaultSetting = @"";
				colvarRMARACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMARACC);

				TableSchema.TableColumn colvarRMCOSACC = new TableSchema.TableColumn(schema);
				colvarRMCOSACC.ColumnName = "RMCOSACC";
				colvarRMCOSACC.DataType = DbType.Int32;
				colvarRMCOSACC.MaxLength = 0;
				colvarRMCOSACC.AutoIncrement = false;
				colvarRMCOSACC.IsNullable = false;
				colvarRMCOSACC.IsPrimaryKey = false;
				colvarRMCOSACC.IsForeignKey = false;
				colvarRMCOSACC.IsReadOnly = false;
				colvarRMCOSACC.DefaultSetting = @"";
				colvarRMCOSACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMCOSACC);

				TableSchema.TableColumn colvarRMIVACC = new TableSchema.TableColumn(schema);
				colvarRMIVACC.ColumnName = "RMIVACC";
				colvarRMIVACC.DataType = DbType.Int32;
				colvarRMIVACC.MaxLength = 0;
				colvarRMIVACC.AutoIncrement = false;
				colvarRMIVACC.IsNullable = false;
				colvarRMIVACC.IsPrimaryKey = false;
				colvarRMIVACC.IsForeignKey = false;
				colvarRMIVACC.IsReadOnly = false;
				colvarRMIVACC.DefaultSetting = @"";
				colvarRMIVACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMIVACC);

				TableSchema.TableColumn colvarRMSLSACC = new TableSchema.TableColumn(schema);
				colvarRMSLSACC.ColumnName = "RMSLSACC";
				colvarRMSLSACC.DataType = DbType.Int32;
				colvarRMSLSACC.MaxLength = 0;
				colvarRMSLSACC.AutoIncrement = false;
				colvarRMSLSACC.IsNullable = false;
				colvarRMSLSACC.IsPrimaryKey = false;
				colvarRMSLSACC.IsForeignKey = false;
				colvarRMSLSACC.IsReadOnly = false;
				colvarRMSLSACC.DefaultSetting = @"";
				colvarRMSLSACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMSLSACC);

				TableSchema.TableColumn colvarRMAVACC = new TableSchema.TableColumn(schema);
				colvarRMAVACC.ColumnName = "RMAVACC";
				colvarRMAVACC.DataType = DbType.Int32;
				colvarRMAVACC.MaxLength = 0;
				colvarRMAVACC.AutoIncrement = false;
				colvarRMAVACC.IsNullable = false;
				colvarRMAVACC.IsPrimaryKey = false;
				colvarRMAVACC.IsForeignKey = false;
				colvarRMAVACC.IsReadOnly = false;
				colvarRMAVACC.DefaultSetting = @"";
				colvarRMAVACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMAVACC);

				TableSchema.TableColumn colvarRMTAKACC = new TableSchema.TableColumn(schema);
				colvarRMTAKACC.ColumnName = "RMTAKACC";
				colvarRMTAKACC.DataType = DbType.Int32;
				colvarRMTAKACC.MaxLength = 0;
				colvarRMTAKACC.AutoIncrement = false;
				colvarRMTAKACC.IsNullable = false;
				colvarRMTAKACC.IsPrimaryKey = false;
				colvarRMTAKACC.IsForeignKey = false;
				colvarRMTAKACC.IsReadOnly = false;
				colvarRMTAKACC.DefaultSetting = @"";
				colvarRMTAKACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMTAKACC);

				TableSchema.TableColumn colvarRMFCGACC = new TableSchema.TableColumn(schema);
				colvarRMFCGACC.ColumnName = "RMFCGACC";
				colvarRMFCGACC.DataType = DbType.Int32;
				colvarRMFCGACC.MaxLength = 0;
				colvarRMFCGACC.AutoIncrement = false;
				colvarRMFCGACC.IsNullable = false;
				colvarRMFCGACC.IsPrimaryKey = false;
				colvarRMFCGACC.IsForeignKey = false;
				colvarRMFCGACC.IsReadOnly = false;
				colvarRMFCGACC.DefaultSetting = @"";
				colvarRMFCGACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMFCGACC);

				TableSchema.TableColumn colvarRMWRACC = new TableSchema.TableColumn(schema);
				colvarRMWRACC.ColumnName = "RMWRACC";
				colvarRMWRACC.DataType = DbType.Int32;
				colvarRMWRACC.MaxLength = 0;
				colvarRMWRACC.AutoIncrement = false;
				colvarRMWRACC.IsNullable = false;
				colvarRMWRACC.IsPrimaryKey = false;
				colvarRMWRACC.IsForeignKey = false;
				colvarRMWRACC.IsReadOnly = false;
				colvarRMWRACC.DefaultSetting = @"";
				colvarRMWRACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMWRACC);

				TableSchema.TableColumn colvarRMSORACC = new TableSchema.TableColumn(schema);
				colvarRMSORACC.ColumnName = "RMSORACC";
				colvarRMSORACC.DataType = DbType.Int32;
				colvarRMSORACC.MaxLength = 0;
				colvarRMSORACC.AutoIncrement = false;
				colvarRMSORACC.IsNullable = false;
				colvarRMSORACC.IsPrimaryKey = false;
				colvarRMSORACC.IsForeignKey = false;
				colvarRMSORACC.IsReadOnly = false;
				colvarRMSORACC.DefaultSetting = @"";
				colvarRMSORACC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMSORACC);

				TableSchema.TableColumn colvarSALSTERR = new TableSchema.TableColumn(schema);
				colvarSALSTERR.ColumnName = "SALSTERR";
				colvarSALSTERR.DataType = DbType.AnsiStringFixedLength;
				colvarSALSTERR.MaxLength = 15;
				colvarSALSTERR.AutoIncrement = false;
				colvarSALSTERR.IsNullable = false;
				colvarSALSTERR.IsPrimaryKey = false;
				colvarSALSTERR.IsForeignKey = false;
				colvarSALSTERR.IsReadOnly = false;
				colvarSALSTERR.DefaultSetting = @"";
				colvarSALSTERR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSALSTERR);

				TableSchema.TableColumn colvarSLPRSNID = new TableSchema.TableColumn(schema);
				colvarSLPRSNID.ColumnName = "SLPRSNID";
				colvarSLPRSNID.DataType = DbType.AnsiStringFixedLength;
				colvarSLPRSNID.MaxLength = 15;
				colvarSLPRSNID.AutoIncrement = false;
				colvarSLPRSNID.IsNullable = false;
				colvarSLPRSNID.IsPrimaryKey = false;
				colvarSLPRSNID.IsForeignKey = false;
				colvarSLPRSNID.IsReadOnly = false;
				colvarSLPRSNID.DefaultSetting = @"";
				colvarSLPRSNID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSLPRSNID);

				TableSchema.TableColumn colvarSTMTCYCL = new TableSchema.TableColumn(schema);
				colvarSTMTCYCL.ColumnName = "STMTCYCL";
				colvarSTMTCYCL.DataType = DbType.Int16;
				colvarSTMTCYCL.MaxLength = 0;
				colvarSTMTCYCL.AutoIncrement = false;
				colvarSTMTCYCL.IsNullable = false;
				colvarSTMTCYCL.IsPrimaryKey = false;
				colvarSTMTCYCL.IsForeignKey = false;
				colvarSTMTCYCL.IsReadOnly = false;
				colvarSTMTCYCL.DefaultSetting = @"";
				colvarSTMTCYCL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTMTCYCL);

				TableSchema.TableColumn colvarSNDSTMNT = new TableSchema.TableColumn(schema);
				colvarSNDSTMNT.ColumnName = "SNDSTMNT";
				colvarSNDSTMNT.DataType = DbType.Byte;
				colvarSNDSTMNT.MaxLength = 0;
				colvarSNDSTMNT.AutoIncrement = false;
				colvarSNDSTMNT.IsNullable = false;
				colvarSNDSTMNT.IsPrimaryKey = false;
				colvarSNDSTMNT.IsForeignKey = false;
				colvarSNDSTMNT.IsReadOnly = false;
				colvarSNDSTMNT.DefaultSetting = @"";
				colvarSNDSTMNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSNDSTMNT);

				TableSchema.TableColumn colvarINACTIVE = new TableSchema.TableColumn(schema);
				colvarINACTIVE.ColumnName = "INACTIVE";
				colvarINACTIVE.DataType = DbType.Byte;
				colvarINACTIVE.MaxLength = 0;
				colvarINACTIVE.AutoIncrement = false;
				colvarINACTIVE.IsNullable = false;
				colvarINACTIVE.IsPrimaryKey = false;
				colvarINACTIVE.IsForeignKey = false;
				colvarINACTIVE.IsReadOnly = false;
				colvarINACTIVE.DefaultSetting = @"";
				colvarINACTIVE.ForeignKeyTableName = "";
				schema.Columns.Add(colvarINACTIVE);

				TableSchema.TableColumn colvarKPCALHST = new TableSchema.TableColumn(schema);
				colvarKPCALHST.ColumnName = "KPCALHST";
				colvarKPCALHST.DataType = DbType.Byte;
				colvarKPCALHST.MaxLength = 0;
				colvarKPCALHST.AutoIncrement = false;
				colvarKPCALHST.IsNullable = false;
				colvarKPCALHST.IsPrimaryKey = false;
				colvarKPCALHST.IsForeignKey = false;
				colvarKPCALHST.IsReadOnly = false;
				colvarKPCALHST.DefaultSetting = @"";
				colvarKPCALHST.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKPCALHST);

				TableSchema.TableColumn colvarKPDSTHST = new TableSchema.TableColumn(schema);
				colvarKPDSTHST.ColumnName = "KPDSTHST";
				colvarKPDSTHST.DataType = DbType.Byte;
				colvarKPDSTHST.MaxLength = 0;
				colvarKPDSTHST.AutoIncrement = false;
				colvarKPDSTHST.IsNullable = false;
				colvarKPDSTHST.IsPrimaryKey = false;
				colvarKPDSTHST.IsForeignKey = false;
				colvarKPDSTHST.IsReadOnly = false;
				colvarKPDSTHST.DefaultSetting = @"";
				colvarKPDSTHST.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKPDSTHST);

				TableSchema.TableColumn colvarKPERHIST = new TableSchema.TableColumn(schema);
				colvarKPERHIST.ColumnName = "KPERHIST";
				colvarKPERHIST.DataType = DbType.Byte;
				colvarKPERHIST.MaxLength = 0;
				colvarKPERHIST.AutoIncrement = false;
				colvarKPERHIST.IsNullable = false;
				colvarKPERHIST.IsPrimaryKey = false;
				colvarKPERHIST.IsForeignKey = false;
				colvarKPERHIST.IsReadOnly = false;
				colvarKPERHIST.DefaultSetting = @"";
				colvarKPERHIST.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKPERHIST);

				TableSchema.TableColumn colvarKPTRXHST = new TableSchema.TableColumn(schema);
				colvarKPTRXHST.ColumnName = "KPTRXHST";
				colvarKPTRXHST.DataType = DbType.Byte;
				colvarKPTRXHST.MaxLength = 0;
				colvarKPTRXHST.AutoIncrement = false;
				colvarKPTRXHST.IsNullable = false;
				colvarKPTRXHST.IsPrimaryKey = false;
				colvarKPTRXHST.IsForeignKey = false;
				colvarKPTRXHST.IsReadOnly = false;
				colvarKPTRXHST.DefaultSetting = @"";
				colvarKPTRXHST.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKPTRXHST);

				TableSchema.TableColumn colvarNOTEINDX = new TableSchema.TableColumn(schema);
				colvarNOTEINDX.ColumnName = "NOTEINDX";
				colvarNOTEINDX.DataType = DbType.Decimal;
				colvarNOTEINDX.MaxLength = 0;
				colvarNOTEINDX.AutoIncrement = false;
				colvarNOTEINDX.IsNullable = false;
				colvarNOTEINDX.IsPrimaryKey = false;
				colvarNOTEINDX.IsForeignKey = false;
				colvarNOTEINDX.IsReadOnly = false;
				colvarNOTEINDX.DefaultSetting = @"";
				colvarNOTEINDX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNOTEINDX);

				TableSchema.TableColumn colvarMODIFDT = new TableSchema.TableColumn(schema);
				colvarMODIFDT.ColumnName = "MODIFDT";
				colvarMODIFDT.DataType = DbType.DateTime;
				colvarMODIFDT.MaxLength = 0;
				colvarMODIFDT.AutoIncrement = false;
				colvarMODIFDT.IsNullable = false;
				colvarMODIFDT.IsPrimaryKey = false;
				colvarMODIFDT.IsForeignKey = false;
				colvarMODIFDT.IsReadOnly = false;
				colvarMODIFDT.DefaultSetting = @"";
				colvarMODIFDT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMODIFDT);

				TableSchema.TableColumn colvarCREATDDT = new TableSchema.TableColumn(schema);
				colvarCREATDDT.ColumnName = "CREATDDT";
				colvarCREATDDT.DataType = DbType.DateTime;
				colvarCREATDDT.MaxLength = 0;
				colvarCREATDDT.AutoIncrement = false;
				colvarCREATDDT.IsNullable = false;
				colvarCREATDDT.IsPrimaryKey = false;
				colvarCREATDDT.IsForeignKey = false;
				colvarCREATDDT.IsReadOnly = false;
				colvarCREATDDT.DefaultSetting = @"";
				colvarCREATDDT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCREATDDT);

				TableSchema.TableColumn colvarRevalue_Customer = new TableSchema.TableColumn(schema);
				colvarRevalue_Customer.ColumnName = "Revalue_Customer";
				colvarRevalue_Customer.DataType = DbType.Byte;
				colvarRevalue_Customer.MaxLength = 0;
				colvarRevalue_Customer.AutoIncrement = false;
				colvarRevalue_Customer.IsNullable = false;
				colvarRevalue_Customer.IsPrimaryKey = false;
				colvarRevalue_Customer.IsForeignKey = false;
				colvarRevalue_Customer.IsReadOnly = false;
				colvarRevalue_Customer.DefaultSetting = @"";
				colvarRevalue_Customer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRevalue_Customer);

				TableSchema.TableColumn colvarPost_Results_To = new TableSchema.TableColumn(schema);
				colvarPost_Results_To.ColumnName = "Post_Results_To";
				colvarPost_Results_To.DataType = DbType.Int16;
				colvarPost_Results_To.MaxLength = 0;
				colvarPost_Results_To.AutoIncrement = false;
				colvarPost_Results_To.IsNullable = false;
				colvarPost_Results_To.IsPrimaryKey = false;
				colvarPost_Results_To.IsForeignKey = false;
				colvarPost_Results_To.IsReadOnly = false;
				colvarPost_Results_To.DefaultSetting = @"";
				colvarPost_Results_To.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPost_Results_To);

				TableSchema.TableColumn colvarDISGRPER = new TableSchema.TableColumn(schema);
				colvarDISGRPER.ColumnName = "DISGRPER";
				colvarDISGRPER.DataType = DbType.Int16;
				colvarDISGRPER.MaxLength = 0;
				colvarDISGRPER.AutoIncrement = false;
				colvarDISGRPER.IsNullable = false;
				colvarDISGRPER.IsPrimaryKey = false;
				colvarDISGRPER.IsForeignKey = false;
				colvarDISGRPER.IsReadOnly = false;
				colvarDISGRPER.DefaultSetting = @"";
				colvarDISGRPER.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDISGRPER);

				TableSchema.TableColumn colvarDUEGRPER = new TableSchema.TableColumn(schema);
				colvarDUEGRPER.ColumnName = "DUEGRPER";
				colvarDUEGRPER.DataType = DbType.Int16;
				colvarDUEGRPER.MaxLength = 0;
				colvarDUEGRPER.AutoIncrement = false;
				colvarDUEGRPER.IsNullable = false;
				colvarDUEGRPER.IsPrimaryKey = false;
				colvarDUEGRPER.IsForeignKey = false;
				colvarDUEGRPER.IsReadOnly = false;
				colvarDUEGRPER.DefaultSetting = @"";
				colvarDUEGRPER.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDUEGRPER);

				TableSchema.TableColumn colvarORDERFULFILLDEFAULT = new TableSchema.TableColumn(schema);
				colvarORDERFULFILLDEFAULT.ColumnName = "ORDERFULFILLDEFAULT";
				colvarORDERFULFILLDEFAULT.DataType = DbType.Int16;
				colvarORDERFULFILLDEFAULT.MaxLength = 0;
				colvarORDERFULFILLDEFAULT.AutoIncrement = false;
				colvarORDERFULFILLDEFAULT.IsNullable = false;
				colvarORDERFULFILLDEFAULT.IsPrimaryKey = false;
				colvarORDERFULFILLDEFAULT.IsForeignKey = false;
				colvarORDERFULFILLDEFAULT.IsReadOnly = false;
				colvarORDERFULFILLDEFAULT.DefaultSetting = @"";
				colvarORDERFULFILLDEFAULT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarORDERFULFILLDEFAULT);

				TableSchema.TableColumn colvarCUSTPRIORITY = new TableSchema.TableColumn(schema);
				colvarCUSTPRIORITY.ColumnName = "CUSTPRIORITY";
				colvarCUSTPRIORITY.DataType = DbType.Int16;
				colvarCUSTPRIORITY.MaxLength = 0;
				colvarCUSTPRIORITY.AutoIncrement = false;
				colvarCUSTPRIORITY.IsNullable = false;
				colvarCUSTPRIORITY.IsPrimaryKey = false;
				colvarCUSTPRIORITY.IsForeignKey = false;
				colvarCUSTPRIORITY.IsReadOnly = false;
				colvarCUSTPRIORITY.DefaultSetting = @"";
				colvarCUSTPRIORITY.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCUSTPRIORITY);

				TableSchema.TableColumn colvarRMOvrpymtWrtoffAcctIdx = new TableSchema.TableColumn(schema);
				colvarRMOvrpymtWrtoffAcctIdx.ColumnName = "RMOvrpymtWrtoffAcctIdx";
				colvarRMOvrpymtWrtoffAcctIdx.DataType = DbType.Int32;
				colvarRMOvrpymtWrtoffAcctIdx.MaxLength = 0;
				colvarRMOvrpymtWrtoffAcctIdx.AutoIncrement = false;
				colvarRMOvrpymtWrtoffAcctIdx.IsNullable = false;
				colvarRMOvrpymtWrtoffAcctIdx.IsPrimaryKey = false;
				colvarRMOvrpymtWrtoffAcctIdx.IsForeignKey = false;
				colvarRMOvrpymtWrtoffAcctIdx.IsReadOnly = false;
				colvarRMOvrpymtWrtoffAcctIdx.DefaultSetting = @"";
				colvarRMOvrpymtWrtoffAcctIdx.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMOvrpymtWrtoffAcctIdx);

				TableSchema.TableColumn colvarCBVAT = new TableSchema.TableColumn(schema);
				colvarCBVAT.ColumnName = "CBVAT";
				colvarCBVAT.DataType = DbType.Byte;
				colvarCBVAT.MaxLength = 0;
				colvarCBVAT.AutoIncrement = false;
				colvarCBVAT.IsNullable = false;
				colvarCBVAT.IsPrimaryKey = false;
				colvarCBVAT.IsForeignKey = false;
				colvarCBVAT.IsReadOnly = false;
				colvarCBVAT.DefaultSetting = @"";
				colvarCBVAT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCBVAT);

				TableSchema.TableColumn colvarINCLUDEINDP = new TableSchema.TableColumn(schema);
				colvarINCLUDEINDP.ColumnName = "INCLUDEINDP";
				colvarINCLUDEINDP.DataType = DbType.Byte;
				colvarINCLUDEINDP.MaxLength = 0;
				colvarINCLUDEINDP.AutoIncrement = false;
				colvarINCLUDEINDP.IsNullable = false;
				colvarINCLUDEINDP.IsPrimaryKey = false;
				colvarINCLUDEINDP.IsForeignKey = false;
				colvarINCLUDEINDP.IsReadOnly = false;
				colvarINCLUDEINDP.DefaultSetting = @"";
				colvarINCLUDEINDP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarINCLUDEINDP);

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = false;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["NxsAccountingProvider"].AddSchema("RM00201",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public string CLASSID { 
			get { return GetColumnValue<string>(Columns.CLASSID); }
			set {
				SetColumnValue(Columns.CLASSID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CLASSID));
			}
		}
		[DataMember]
		public string CLASDSCR { 
			get { return GetColumnValue<string>(Columns.CLASDSCR); }
			set {
				SetColumnValue(Columns.CLASDSCR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CLASDSCR));
			}
		}
		[DataMember]
		public short CRLMTTYP { 
			get { return GetColumnValue<short>(Columns.CRLMTTYP); }
			set {
				SetColumnValue(Columns.CRLMTTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CRLMTTYP));
			}
		}
		[DataMember]
		public decimal CRLMTAMT { 
			get { return GetColumnValue<decimal>(Columns.CRLMTAMT); }
			set {
				SetColumnValue(Columns.CRLMTAMT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CRLMTAMT));
			}
		}
		[DataMember]
		public short CRLMTPER { 
			get { return GetColumnValue<short>(Columns.CRLMTPER); }
			set {
				SetColumnValue(Columns.CRLMTPER, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CRLMTPER));
			}
		}
		[DataMember]
		public decimal CRLMTPAM { 
			get { return GetColumnValue<decimal>(Columns.CRLMTPAM); }
			set {
				SetColumnValue(Columns.CRLMTPAM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CRLMTPAM));
			}
		}
		[DataMember]
		public byte DEFLTCLS { 
			get { return GetColumnValue<byte>(Columns.DEFLTCLS); }
			set {
				SetColumnValue(Columns.DEFLTCLS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEFLTCLS));
			}
		}
		[DataMember]
		public short BALNCTYP { 
			get { return GetColumnValue<short>(Columns.BALNCTYP); }
			set {
				SetColumnValue(Columns.BALNCTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BALNCTYP));
			}
		}
		[DataMember]
		public string CHEKBKID { 
			get { return GetColumnValue<string>(Columns.CHEKBKID); }
			set {
				SetColumnValue(Columns.CHEKBKID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CHEKBKID));
			}
		}
		[DataMember]
		public string BANKNAME { 
			get { return GetColumnValue<string>(Columns.BANKNAME); }
			set {
				SetColumnValue(Columns.BANKNAME, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BANKNAME));
			}
		}
		[DataMember]
		public string TAXSCHID { 
			get { return GetColumnValue<string>(Columns.TAXSCHID); }
			set {
				SetColumnValue(Columns.TAXSCHID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TAXSCHID));
			}
		}
		[DataMember]
		public string SHIPMTHD { 
			get { return GetColumnValue<string>(Columns.SHIPMTHD); }
			set {
				SetColumnValue(Columns.SHIPMTHD, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SHIPMTHD));
			}
		}
		[DataMember]
		public string PYMTRMID { 
			get { return GetColumnValue<string>(Columns.PYMTRMID); }
			set {
				SetColumnValue(Columns.PYMTRMID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PYMTRMID));
			}
		}
		[DataMember]
		public short CUSTDISC { 
			get { return GetColumnValue<short>(Columns.CUSTDISC); }
			set {
				SetColumnValue(Columns.CUSTDISC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CUSTDISC));
			}
		}
		[DataMember]
		public string CSTPRLVL { 
			get { return GetColumnValue<string>(Columns.CSTPRLVL); }
			set {
				SetColumnValue(Columns.CSTPRLVL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CSTPRLVL));
			}
		}
		[DataMember]
		public short MINPYTYP { 
			get { return GetColumnValue<short>(Columns.MINPYTYP); }
			set {
				SetColumnValue(Columns.MINPYTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MINPYTYP));
			}
		}
		[DataMember]
		public decimal MINPYDLR { 
			get { return GetColumnValue<decimal>(Columns.MINPYDLR); }
			set {
				SetColumnValue(Columns.MINPYDLR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MINPYDLR));
			}
		}
		[DataMember]
		public short MINPYPCT { 
			get { return GetColumnValue<short>(Columns.MINPYPCT); }
			set {
				SetColumnValue(Columns.MINPYPCT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MINPYPCT));
			}
		}
		[DataMember]
		public short MXWOFTYP { 
			get { return GetColumnValue<short>(Columns.MXWOFTYP); }
			set {
				SetColumnValue(Columns.MXWOFTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MXWOFTYP));
			}
		}
		[DataMember]
		public decimal MXWROFAM { 
			get { return GetColumnValue<decimal>(Columns.MXWROFAM); }
			set {
				SetColumnValue(Columns.MXWROFAM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MXWROFAM));
			}
		}
		[DataMember]
		public byte FINCHARG { 
			get { return GetColumnValue<byte>(Columns.FINCHARG); }
			set {
				SetColumnValue(Columns.FINCHARG, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FINCHARG));
			}
		}
		[DataMember]
		public short FNCHATYP { 
			get { return GetColumnValue<short>(Columns.FNCHATYP); }
			set {
				SetColumnValue(Columns.FNCHATYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FNCHATYP));
			}
		}
		[DataMember]
		public decimal FINCHDLR { 
			get { return GetColumnValue<decimal>(Columns.FINCHDLR); }
			set {
				SetColumnValue(Columns.FINCHDLR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FINCHDLR));
			}
		}
		[DataMember]
		public short FNCHPCNT { 
			get { return GetColumnValue<short>(Columns.FNCHPCNT); }
			set {
				SetColumnValue(Columns.FNCHPCNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FNCHPCNT));
			}
		}
		[DataMember]
		public string PRCLEVEL { 
			get { return GetColumnValue<string>(Columns.PRCLEVEL); }
			set {
				SetColumnValue(Columns.PRCLEVEL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PRCLEVEL));
			}
		}
		[DataMember]
		public string CURNCYID { 
			get { return GetColumnValue<string>(Columns.CURNCYID); }
			set {
				SetColumnValue(Columns.CURNCYID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CURNCYID));
			}
		}
		[DataMember]
		public string RATETPID { 
			get { return GetColumnValue<string>(Columns.RATETPID); }
			set {
				SetColumnValue(Columns.RATETPID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RATETPID));
			}
		}
		[DataMember]
		public short DEFCACTY { 
			get { return GetColumnValue<short>(Columns.DEFCACTY); }
			set {
				SetColumnValue(Columns.DEFCACTY, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEFCACTY));
			}
		}
		[DataMember]
		public int RMCSHACC { 
			get { return GetColumnValue<int>(Columns.RMCSHACC); }
			set {
				SetColumnValue(Columns.RMCSHACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMCSHACC));
			}
		}
		[DataMember]
		public int RMARACC { 
			get { return GetColumnValue<int>(Columns.RMARACC); }
			set {
				SetColumnValue(Columns.RMARACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMARACC));
			}
		}
		[DataMember]
		public int RMCOSACC { 
			get { return GetColumnValue<int>(Columns.RMCOSACC); }
			set {
				SetColumnValue(Columns.RMCOSACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMCOSACC));
			}
		}
		[DataMember]
		public int RMIVACC { 
			get { return GetColumnValue<int>(Columns.RMIVACC); }
			set {
				SetColumnValue(Columns.RMIVACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMIVACC));
			}
		}
		[DataMember]
		public int RMSLSACC { 
			get { return GetColumnValue<int>(Columns.RMSLSACC); }
			set {
				SetColumnValue(Columns.RMSLSACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMSLSACC));
			}
		}
		[DataMember]
		public int RMAVACC { 
			get { return GetColumnValue<int>(Columns.RMAVACC); }
			set {
				SetColumnValue(Columns.RMAVACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMAVACC));
			}
		}
		[DataMember]
		public int RMTAKACC { 
			get { return GetColumnValue<int>(Columns.RMTAKACC); }
			set {
				SetColumnValue(Columns.RMTAKACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMTAKACC));
			}
		}
		[DataMember]
		public int RMFCGACC { 
			get { return GetColumnValue<int>(Columns.RMFCGACC); }
			set {
				SetColumnValue(Columns.RMFCGACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMFCGACC));
			}
		}
		[DataMember]
		public int RMWRACC { 
			get { return GetColumnValue<int>(Columns.RMWRACC); }
			set {
				SetColumnValue(Columns.RMWRACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMWRACC));
			}
		}
		[DataMember]
		public int RMSORACC { 
			get { return GetColumnValue<int>(Columns.RMSORACC); }
			set {
				SetColumnValue(Columns.RMSORACC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMSORACC));
			}
		}
		[DataMember]
		public string SALSTERR { 
			get { return GetColumnValue<string>(Columns.SALSTERR); }
			set {
				SetColumnValue(Columns.SALSTERR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SALSTERR));
			}
		}
		[DataMember]
		public string SLPRSNID { 
			get { return GetColumnValue<string>(Columns.SLPRSNID); }
			set {
				SetColumnValue(Columns.SLPRSNID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SLPRSNID));
			}
		}
		[DataMember]
		public short STMTCYCL { 
			get { return GetColumnValue<short>(Columns.STMTCYCL); }
			set {
				SetColumnValue(Columns.STMTCYCL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STMTCYCL));
			}
		}
		[DataMember]
		public byte SNDSTMNT { 
			get { return GetColumnValue<byte>(Columns.SNDSTMNT); }
			set {
				SetColumnValue(Columns.SNDSTMNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SNDSTMNT));
			}
		}
		[DataMember]
		public byte INACTIVE { 
			get { return GetColumnValue<byte>(Columns.INACTIVE); }
			set {
				SetColumnValue(Columns.INACTIVE, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.INACTIVE));
			}
		}
		[DataMember]
		public byte KPCALHST { 
			get { return GetColumnValue<byte>(Columns.KPCALHST); }
			set {
				SetColumnValue(Columns.KPCALHST, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.KPCALHST));
			}
		}
		[DataMember]
		public byte KPDSTHST { 
			get { return GetColumnValue<byte>(Columns.KPDSTHST); }
			set {
				SetColumnValue(Columns.KPDSTHST, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.KPDSTHST));
			}
		}
		[DataMember]
		public byte KPERHIST { 
			get { return GetColumnValue<byte>(Columns.KPERHIST); }
			set {
				SetColumnValue(Columns.KPERHIST, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.KPERHIST));
			}
		}
		[DataMember]
		public byte KPTRXHST { 
			get { return GetColumnValue<byte>(Columns.KPTRXHST); }
			set {
				SetColumnValue(Columns.KPTRXHST, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.KPTRXHST));
			}
		}
		[DataMember]
		public decimal NOTEINDX { 
			get { return GetColumnValue<decimal>(Columns.NOTEINDX); }
			set {
				SetColumnValue(Columns.NOTEINDX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NOTEINDX));
			}
		}
		[DataMember]
		public DateTime MODIFDT { 
			get { return GetColumnValue<DateTime>(Columns.MODIFDT); }
			set {
				SetColumnValue(Columns.MODIFDT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MODIFDT));
			}
		}
		[DataMember]
		public DateTime CREATDDT { 
			get { return GetColumnValue<DateTime>(Columns.CREATDDT); }
			set {
				SetColumnValue(Columns.CREATDDT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CREATDDT));
			}
		}
		[DataMember]
		public byte Revalue_Customer { 
			get { return GetColumnValue<byte>(Columns.Revalue_Customer); }
			set {
				SetColumnValue(Columns.Revalue_Customer, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Revalue_Customer));
			}
		}
		[DataMember]
		public short Post_Results_To { 
			get { return GetColumnValue<short>(Columns.Post_Results_To); }
			set {
				SetColumnValue(Columns.Post_Results_To, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Post_Results_To));
			}
		}
		[DataMember]
		public short DISGRPER { 
			get { return GetColumnValue<short>(Columns.DISGRPER); }
			set {
				SetColumnValue(Columns.DISGRPER, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DISGRPER));
			}
		}
		[DataMember]
		public short DUEGRPER { 
			get { return GetColumnValue<short>(Columns.DUEGRPER); }
			set {
				SetColumnValue(Columns.DUEGRPER, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DUEGRPER));
			}
		}
		[DataMember]
		public short ORDERFULFILLDEFAULT { 
			get { return GetColumnValue<short>(Columns.ORDERFULFILLDEFAULT); }
			set {
				SetColumnValue(Columns.ORDERFULFILLDEFAULT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ORDERFULFILLDEFAULT));
			}
		}
		[DataMember]
		public short CUSTPRIORITY { 
			get { return GetColumnValue<short>(Columns.CUSTPRIORITY); }
			set {
				SetColumnValue(Columns.CUSTPRIORITY, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CUSTPRIORITY));
			}
		}
		[DataMember]
		public int RMOvrpymtWrtoffAcctIdx { 
			get { return GetColumnValue<int>(Columns.RMOvrpymtWrtoffAcctIdx); }
			set {
				SetColumnValue(Columns.RMOvrpymtWrtoffAcctIdx, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMOvrpymtWrtoffAcctIdx));
			}
		}
		[DataMember]
		public byte CBVAT { 
			get { return GetColumnValue<byte>(Columns.CBVAT); }
			set {
				SetColumnValue(Columns.CBVAT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CBVAT));
			}
		}
		[DataMember]
		public byte INCLUDEINDP { 
			get { return GetColumnValue<byte>(Columns.INCLUDEINDP); }
			set {
				SetColumnValue(Columns.INCLUDEINDP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.INCLUDEINDP));
			}
		}
		[DataMember]
		public int DEX_ROW_ID { 
			get { return GetColumnValue<int>(Columns.DEX_ROW_ID); }
			set {
				SetColumnValue(Columns.DEX_ROW_ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_ID));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return CLASDSCR;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CLASSIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CLASDSCRColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CRLMTTYPColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CRLMTAMTColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CRLMTPERColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CRLMTPAMColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn DEFLTCLSColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn BALNCTYPColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CHEKBKIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn BANKNAMEColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn TAXSCHIDColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn SHIPMTHDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PYMTRMIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CUSTDISCColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CSTPRLVLColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn MINPYTYPColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn MINPYDLRColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn MINPYPCTColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn MXWOFTYPColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn MXWROFAMColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn FINCHARGColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn FNCHATYPColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn FINCHDLRColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn FNCHPCNTColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn PRCLEVELColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn CURNCYIDColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn RATETPIDColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn DEFCACTYColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn RMCSHACCColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn RMARACCColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn RMCOSACCColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn RMIVACCColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn RMSLSACCColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn RMAVACCColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn RMTAKACCColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn RMFCGACCColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn RMWRACCColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn RMSORACCColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn SALSTERRColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn SLPRSNIDColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn STMTCYCLColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn SNDSTMNTColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn INACTIVEColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn KPCALHSTColumn
		{
			get { return Schema.Columns[43]; }
		}
		public static TableSchema.TableColumn KPDSTHSTColumn
		{
			get { return Schema.Columns[44]; }
		}
		public static TableSchema.TableColumn KPERHISTColumn
		{
			get { return Schema.Columns[45]; }
		}
		public static TableSchema.TableColumn KPTRXHSTColumn
		{
			get { return Schema.Columns[46]; }
		}
		public static TableSchema.TableColumn NOTEINDXColumn
		{
			get { return Schema.Columns[47]; }
		}
		public static TableSchema.TableColumn MODIFDTColumn
		{
			get { return Schema.Columns[48]; }
		}
		public static TableSchema.TableColumn CREATDDTColumn
		{
			get { return Schema.Columns[49]; }
		}
		public static TableSchema.TableColumn Revalue_CustomerColumn
		{
			get { return Schema.Columns[50]; }
		}
		public static TableSchema.TableColumn Post_Results_ToColumn
		{
			get { return Schema.Columns[51]; }
		}
		public static TableSchema.TableColumn DISGRPERColumn
		{
			get { return Schema.Columns[52]; }
		}
		public static TableSchema.TableColumn DUEGRPERColumn
		{
			get { return Schema.Columns[53]; }
		}
		public static TableSchema.TableColumn ORDERFULFILLDEFAULTColumn
		{
			get { return Schema.Columns[54]; }
		}
		public static TableSchema.TableColumn CUSTPRIORITYColumn
		{
			get { return Schema.Columns[55]; }
		}
		public static TableSchema.TableColumn RMOvrpymtWrtoffAcctIdxColumn
		{
			get { return Schema.Columns[56]; }
		}
		public static TableSchema.TableColumn CBVATColumn
		{
			get { return Schema.Columns[57]; }
		}
		public static TableSchema.TableColumn INCLUDEINDPColumn
		{
			get { return Schema.Columns[58]; }
		}
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[59]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CLASSID = @"CLASSID";
			public static readonly string CLASDSCR = @"CLASDSCR";
			public static readonly string CRLMTTYP = @"CRLMTTYP";
			public static readonly string CRLMTAMT = @"CRLMTAMT";
			public static readonly string CRLMTPER = @"CRLMTPER";
			public static readonly string CRLMTPAM = @"CRLMTPAM";
			public static readonly string DEFLTCLS = @"DEFLTCLS";
			public static readonly string BALNCTYP = @"BALNCTYP";
			public static readonly string CHEKBKID = @"CHEKBKID";
			public static readonly string BANKNAME = @"BANKNAME";
			public static readonly string TAXSCHID = @"TAXSCHID";
			public static readonly string SHIPMTHD = @"SHIPMTHD";
			public static readonly string PYMTRMID = @"PYMTRMID";
			public static readonly string CUSTDISC = @"CUSTDISC";
			public static readonly string CSTPRLVL = @"CSTPRLVL";
			public static readonly string MINPYTYP = @"MINPYTYP";
			public static readonly string MINPYDLR = @"MINPYDLR";
			public static readonly string MINPYPCT = @"MINPYPCT";
			public static readonly string MXWOFTYP = @"MXWOFTYP";
			public static readonly string MXWROFAM = @"MXWROFAM";
			public static readonly string FINCHARG = @"FINCHARG";
			public static readonly string FNCHATYP = @"FNCHATYP";
			public static readonly string FINCHDLR = @"FINCHDLR";
			public static readonly string FNCHPCNT = @"FNCHPCNT";
			public static readonly string PRCLEVEL = @"PRCLEVEL";
			public static readonly string CURNCYID = @"CURNCYID";
			public static readonly string RATETPID = @"RATETPID";
			public static readonly string DEFCACTY = @"DEFCACTY";
			public static readonly string RMCSHACC = @"RMCSHACC";
			public static readonly string RMARACC = @"RMARACC";
			public static readonly string RMCOSACC = @"RMCOSACC";
			public static readonly string RMIVACC = @"RMIVACC";
			public static readonly string RMSLSACC = @"RMSLSACC";
			public static readonly string RMAVACC = @"RMAVACC";
			public static readonly string RMTAKACC = @"RMTAKACC";
			public static readonly string RMFCGACC = @"RMFCGACC";
			public static readonly string RMWRACC = @"RMWRACC";
			public static readonly string RMSORACC = @"RMSORACC";
			public static readonly string SALSTERR = @"SALSTERR";
			public static readonly string SLPRSNID = @"SLPRSNID";
			public static readonly string STMTCYCL = @"STMTCYCL";
			public static readonly string SNDSTMNT = @"SNDSTMNT";
			public static readonly string INACTIVE = @"INACTIVE";
			public static readonly string KPCALHST = @"KPCALHST";
			public static readonly string KPDSTHST = @"KPDSTHST";
			public static readonly string KPERHIST = @"KPERHIST";
			public static readonly string KPTRXHST = @"KPTRXHST";
			public static readonly string NOTEINDX = @"NOTEINDX";
			public static readonly string MODIFDT = @"MODIFDT";
			public static readonly string CREATDDT = @"CREATDDT";
			public static readonly string Revalue_Customer = @"Revalue_Customer";
			public static readonly string Post_Results_To = @"Post_Results_To";
			public static readonly string DISGRPER = @"DISGRPER";
			public static readonly string DUEGRPER = @"DUEGRPER";
			public static readonly string ORDERFULFILLDEFAULT = @"ORDERFULFILLDEFAULT";
			public static readonly string CUSTPRIORITY = @"CUSTPRIORITY";
			public static readonly string RMOvrpymtWrtoffAcctIdx = @"RMOvrpymtWrtoffAcctIdx";
			public static readonly string CBVAT = @"CBVAT";
			public static readonly string INCLUDEINDP = @"INCLUDEINDP";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
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
	/// Strongly-typed collection for the TX00201 class.
	/// </summary>
	[DataContract]
	public partial class TX00201Collection : ActiveList<TX00201, TX00201Collection>
	{
		public static TX00201Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			TX00201Collection result = new TX00201Collection();
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
			foreach (TX00201 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the TX00201 table.
	/// </summary>
	[DataContract]
	public partial class TX00201 : ActiveRecord<TX00201>, INotifyPropertyChanged
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

		public TX00201()
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
				TableSchema.Table schema = new TableSchema.Table("TX00201", TableType.Table, DataService.GetInstance("NxsAccountingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTAXDTLID = new TableSchema.TableColumn(schema);
				colvarTAXDTLID.ColumnName = "TAXDTLID";
				colvarTAXDTLID.DataType = DbType.AnsiStringFixedLength;
				colvarTAXDTLID.MaxLength = 15;
				colvarTAXDTLID.AutoIncrement = false;
				colvarTAXDTLID.IsNullable = false;
				colvarTAXDTLID.IsPrimaryKey = false;
				colvarTAXDTLID.IsForeignKey = false;
				colvarTAXDTLID.IsReadOnly = false;
				colvarTAXDTLID.DefaultSetting = @"";
				colvarTAXDTLID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTAXDTLID);

				TableSchema.TableColumn colvarTXDTLDSC = new TableSchema.TableColumn(schema);
				colvarTXDTLDSC.ColumnName = "TXDTLDSC";
				colvarTXDTLDSC.DataType = DbType.AnsiStringFixedLength;
				colvarTXDTLDSC.MaxLength = 31;
				colvarTXDTLDSC.AutoIncrement = false;
				colvarTXDTLDSC.IsNullable = false;
				colvarTXDTLDSC.IsPrimaryKey = false;
				colvarTXDTLDSC.IsForeignKey = false;
				colvarTXDTLDSC.IsReadOnly = false;
				colvarTXDTLDSC.DefaultSetting = @"";
				colvarTXDTLDSC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLDSC);

				TableSchema.TableColumn colvarTXDTLTYP = new TableSchema.TableColumn(schema);
				colvarTXDTLTYP.ColumnName = "TXDTLTYP";
				colvarTXDTLTYP.DataType = DbType.Int16;
				colvarTXDTLTYP.MaxLength = 0;
				colvarTXDTLTYP.AutoIncrement = false;
				colvarTXDTLTYP.IsNullable = false;
				colvarTXDTLTYP.IsPrimaryKey = false;
				colvarTXDTLTYP.IsForeignKey = false;
				colvarTXDTLTYP.IsReadOnly = false;
				colvarTXDTLTYP.DefaultSetting = @"";
				colvarTXDTLTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLTYP);

				TableSchema.TableColumn colvarACTINDX = new TableSchema.TableColumn(schema);
				colvarACTINDX.ColumnName = "ACTINDX";
				colvarACTINDX.DataType = DbType.Int32;
				colvarACTINDX.MaxLength = 0;
				colvarACTINDX.AutoIncrement = false;
				colvarACTINDX.IsNullable = false;
				colvarACTINDX.IsPrimaryKey = false;
				colvarACTINDX.IsForeignKey = false;
				colvarACTINDX.IsReadOnly = false;
				colvarACTINDX.DefaultSetting = @"";
				colvarACTINDX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarACTINDX);

				TableSchema.TableColumn colvarTXIDNMBR = new TableSchema.TableColumn(schema);
				colvarTXIDNMBR.ColumnName = "TXIDNMBR";
				colvarTXIDNMBR.DataType = DbType.AnsiStringFixedLength;
				colvarTXIDNMBR.MaxLength = 11;
				colvarTXIDNMBR.AutoIncrement = false;
				colvarTXIDNMBR.IsNullable = false;
				colvarTXIDNMBR.IsPrimaryKey = false;
				colvarTXIDNMBR.IsForeignKey = false;
				colvarTXIDNMBR.IsReadOnly = false;
				colvarTXIDNMBR.DefaultSetting = @"";
				colvarTXIDNMBR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXIDNMBR);

				TableSchema.TableColumn colvarTXDTLBSE = new TableSchema.TableColumn(schema);
				colvarTXDTLBSE.ColumnName = "TXDTLBSE";
				colvarTXDTLBSE.DataType = DbType.Int16;
				colvarTXDTLBSE.MaxLength = 0;
				colvarTXDTLBSE.AutoIncrement = false;
				colvarTXDTLBSE.IsNullable = false;
				colvarTXDTLBSE.IsPrimaryKey = false;
				colvarTXDTLBSE.IsForeignKey = false;
				colvarTXDTLBSE.IsReadOnly = false;
				colvarTXDTLBSE.DefaultSetting = @"";
				colvarTXDTLBSE.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLBSE);

				TableSchema.TableColumn colvarTXDTLPCT = new TableSchema.TableColumn(schema);
				colvarTXDTLPCT.ColumnName = "TXDTLPCT";
				colvarTXDTLPCT.DataType = DbType.Decimal;
				colvarTXDTLPCT.MaxLength = 0;
				colvarTXDTLPCT.AutoIncrement = false;
				colvarTXDTLPCT.IsNullable = false;
				colvarTXDTLPCT.IsPrimaryKey = false;
				colvarTXDTLPCT.IsForeignKey = false;
				colvarTXDTLPCT.IsReadOnly = false;
				colvarTXDTLPCT.DefaultSetting = @"";
				colvarTXDTLPCT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLPCT);

				TableSchema.TableColumn colvarTXDTLAMT = new TableSchema.TableColumn(schema);
				colvarTXDTLAMT.ColumnName = "TXDTLAMT";
				colvarTXDTLAMT.DataType = DbType.Decimal;
				colvarTXDTLAMT.MaxLength = 0;
				colvarTXDTLAMT.AutoIncrement = false;
				colvarTXDTLAMT.IsNullable = false;
				colvarTXDTLAMT.IsPrimaryKey = false;
				colvarTXDTLAMT.IsForeignKey = false;
				colvarTXDTLAMT.IsReadOnly = false;
				colvarTXDTLAMT.DefaultSetting = @"";
				colvarTXDTLAMT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLAMT);

				TableSchema.TableColumn colvarTDTLRNDG = new TableSchema.TableColumn(schema);
				colvarTDTLRNDG.ColumnName = "TDTLRNDG";
				colvarTDTLRNDG.DataType = DbType.Int16;
				colvarTDTLRNDG.MaxLength = 0;
				colvarTDTLRNDG.AutoIncrement = false;
				colvarTDTLRNDG.IsNullable = false;
				colvarTDTLRNDG.IsPrimaryKey = false;
				colvarTDTLRNDG.IsForeignKey = false;
				colvarTDTLRNDG.IsReadOnly = false;
				colvarTDTLRNDG.DefaultSetting = @"";
				colvarTDTLRNDG.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTLRNDG);

				TableSchema.TableColumn colvarTXDBODTL = new TableSchema.TableColumn(schema);
				colvarTXDBODTL.ColumnName = "TXDBODTL";
				colvarTXDBODTL.DataType = DbType.AnsiStringFixedLength;
				colvarTXDBODTL.MaxLength = 15;
				colvarTXDBODTL.AutoIncrement = false;
				colvarTXDBODTL.IsNullable = false;
				colvarTXDBODTL.IsPrimaryKey = false;
				colvarTXDBODTL.IsForeignKey = false;
				colvarTXDBODTL.IsReadOnly = false;
				colvarTXDBODTL.DefaultSetting = @"";
				colvarTXDBODTL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDBODTL);

				TableSchema.TableColumn colvarTDTABMIN = new TableSchema.TableColumn(schema);
				colvarTDTABMIN.ColumnName = "TDTABMIN";
				colvarTDTABMIN.DataType = DbType.Decimal;
				colvarTDTABMIN.MaxLength = 0;
				colvarTDTABMIN.AutoIncrement = false;
				colvarTDTABMIN.IsNullable = false;
				colvarTDTABMIN.IsPrimaryKey = false;
				colvarTDTABMIN.IsForeignKey = false;
				colvarTDTABMIN.IsReadOnly = false;
				colvarTDTABMIN.DefaultSetting = @"";
				colvarTDTABMIN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTABMIN);

				TableSchema.TableColumn colvarTDTABMAX = new TableSchema.TableColumn(schema);
				colvarTDTABMAX.ColumnName = "TDTABMAX";
				colvarTDTABMAX.DataType = DbType.Decimal;
				colvarTDTABMAX.MaxLength = 0;
				colvarTDTABMAX.AutoIncrement = false;
				colvarTDTABMAX.IsNullable = false;
				colvarTDTABMAX.IsPrimaryKey = false;
				colvarTDTABMAX.IsForeignKey = false;
				colvarTDTABMAX.IsReadOnly = false;
				colvarTDTABMAX.DefaultSetting = @"";
				colvarTDTABMAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTABMAX);

				TableSchema.TableColumn colvarTDTAXMIN = new TableSchema.TableColumn(schema);
				colvarTDTAXMIN.ColumnName = "TDTAXMIN";
				colvarTDTAXMIN.DataType = DbType.Decimal;
				colvarTDTAXMIN.MaxLength = 0;
				colvarTDTAXMIN.AutoIncrement = false;
				colvarTDTAXMIN.IsNullable = false;
				colvarTDTAXMIN.IsPrimaryKey = false;
				colvarTDTAXMIN.IsForeignKey = false;
				colvarTDTAXMIN.IsReadOnly = false;
				colvarTDTAXMIN.DefaultSetting = @"";
				colvarTDTAXMIN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTAXMIN);

				TableSchema.TableColumn colvarTDTAXMAX = new TableSchema.TableColumn(schema);
				colvarTDTAXMAX.ColumnName = "TDTAXMAX";
				colvarTDTAXMAX.DataType = DbType.Decimal;
				colvarTDTAXMAX.MaxLength = 0;
				colvarTDTAXMAX.AutoIncrement = false;
				colvarTDTAXMAX.IsNullable = false;
				colvarTDTAXMAX.IsPrimaryKey = false;
				colvarTDTAXMAX.IsForeignKey = false;
				colvarTDTAXMAX.IsReadOnly = false;
				colvarTDTAXMAX.DefaultSetting = @"";
				colvarTDTAXMAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTAXMAX);

				TableSchema.TableColumn colvarTDRNGTYP = new TableSchema.TableColumn(schema);
				colvarTDRNGTYP.ColumnName = "TDRNGTYP";
				colvarTDRNGTYP.DataType = DbType.Int16;
				colvarTDRNGTYP.MaxLength = 0;
				colvarTDRNGTYP.AutoIncrement = false;
				colvarTDRNGTYP.IsNullable = false;
				colvarTDRNGTYP.IsPrimaryKey = false;
				colvarTDRNGTYP.IsForeignKey = false;
				colvarTDRNGTYP.IsReadOnly = false;
				colvarTDRNGTYP.DefaultSetting = @"";
				colvarTDRNGTYP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDRNGTYP);

				TableSchema.TableColumn colvarTXDTQUAL = new TableSchema.TableColumn(schema);
				colvarTXDTQUAL.ColumnName = "TXDTQUAL";
				colvarTXDTQUAL.DataType = DbType.Int16;
				colvarTXDTQUAL.MaxLength = 0;
				colvarTXDTQUAL.AutoIncrement = false;
				colvarTXDTQUAL.IsNullable = false;
				colvarTXDTQUAL.IsPrimaryKey = false;
				colvarTXDTQUAL.IsForeignKey = false;
				colvarTXDTQUAL.IsReadOnly = false;
				colvarTXDTQUAL.DefaultSetting = @"";
				colvarTXDTQUAL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTQUAL);

				TableSchema.TableColumn colvarTDTAXTAX = new TableSchema.TableColumn(schema);
				colvarTDTAXTAX.ColumnName = "TDTAXTAX";
				colvarTDTAXTAX.DataType = DbType.Byte;
				colvarTDTAXTAX.MaxLength = 0;
				colvarTDTAXTAX.AutoIncrement = false;
				colvarTDTAXTAX.IsNullable = false;
				colvarTDTAXTAX.IsPrimaryKey = false;
				colvarTDTAXTAX.IsForeignKey = false;
				colvarTDTAXTAX.IsReadOnly = false;
				colvarTDTAXTAX.DefaultSetting = @"";
				colvarTDTAXTAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTAXTAX);

				TableSchema.TableColumn colvarTXDTLPDC = new TableSchema.TableColumn(schema);
				colvarTXDTLPDC.ColumnName = "TXDTLPDC";
				colvarTXDTLPDC.DataType = DbType.Byte;
				colvarTXDTLPDC.MaxLength = 0;
				colvarTXDTLPDC.AutoIncrement = false;
				colvarTXDTLPDC.IsNullable = false;
				colvarTXDTLPDC.IsPrimaryKey = false;
				colvarTXDTLPDC.IsForeignKey = false;
				colvarTXDTLPDC.IsReadOnly = false;
				colvarTXDTLPDC.DefaultSetting = @"";
				colvarTXDTLPDC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLPDC);

				TableSchema.TableColumn colvarTXDTLPCH = new TableSchema.TableColumn(schema);
				colvarTXDTLPCH.ColumnName = "TXDTLPCH";
				colvarTXDTLPCH.DataType = DbType.AnsiStringFixedLength;
				colvarTXDTLPCH.MaxLength = 1;
				colvarTXDTLPCH.AutoIncrement = false;
				colvarTXDTLPCH.IsNullable = false;
				colvarTXDTLPCH.IsPrimaryKey = false;
				colvarTXDTLPCH.IsForeignKey = false;
				colvarTXDTLPCH.IsReadOnly = false;
				colvarTXDTLPCH.DefaultSetting = @"";
				colvarTXDTLPCH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDTLPCH);

				TableSchema.TableColumn colvarTXDXDISC = new TableSchema.TableColumn(schema);
				colvarTXDXDISC.ColumnName = "TXDXDISC";
				colvarTXDXDISC.DataType = DbType.Byte;
				colvarTXDXDISC.MaxLength = 0;
				colvarTXDXDISC.AutoIncrement = false;
				colvarTXDXDISC.IsNullable = false;
				colvarTXDXDISC.IsPrimaryKey = false;
				colvarTXDXDISC.IsForeignKey = false;
				colvarTXDXDISC.IsReadOnly = false;
				colvarTXDXDISC.DefaultSetting = @"";
				colvarTXDXDISC.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXDXDISC);

				TableSchema.TableColumn colvarCMNYTXID = new TableSchema.TableColumn(schema);
				colvarCMNYTXID.ColumnName = "CMNYTXID";
				colvarCMNYTXID.DataType = DbType.AnsiStringFixedLength;
				colvarCMNYTXID.MaxLength = 15;
				colvarCMNYTXID.AutoIncrement = false;
				colvarCMNYTXID.IsNullable = false;
				colvarCMNYTXID.IsPrimaryKey = false;
				colvarCMNYTXID.IsForeignKey = false;
				colvarCMNYTXID.IsReadOnly = false;
				colvarCMNYTXID.DefaultSetting = @"";
				colvarCMNYTXID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCMNYTXID);

				TableSchema.TableColumn colvarNOTEINDX = new TableSchema.TableColumn(schema);
				colvarNOTEINDX.ColumnName = "NOTEINDX";
				colvarNOTEINDX.DataType = DbType.Decimal;
				colvarNOTEINDX.MaxLength = 0;
				colvarNOTEINDX.AutoIncrement = false;
				colvarNOTEINDX.IsNullable = false;
				colvarNOTEINDX.IsPrimaryKey = false;
				colvarNOTEINDX.IsForeignKey = false;
				colvarNOTEINDX.IsReadOnly = false;
				colvarNOTEINDX.DefaultSetting = @"";
				colvarNOTEINDX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNOTEINDX);

				TableSchema.TableColumn colvarNAME = new TableSchema.TableColumn(schema);
				colvarNAME.ColumnName = "NAME";
				colvarNAME.DataType = DbType.AnsiStringFixedLength;
				colvarNAME.MaxLength = 31;
				colvarNAME.AutoIncrement = false;
				colvarNAME.IsNullable = false;
				colvarNAME.IsPrimaryKey = false;
				colvarNAME.IsForeignKey = false;
				colvarNAME.IsReadOnly = false;
				colvarNAME.DefaultSetting = @"";
				colvarNAME.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNAME);

				TableSchema.TableColumn colvarCNTCPRSN = new TableSchema.TableColumn(schema);
				colvarCNTCPRSN.ColumnName = "CNTCPRSN";
				colvarCNTCPRSN.DataType = DbType.AnsiStringFixedLength;
				colvarCNTCPRSN.MaxLength = 61;
				colvarCNTCPRSN.AutoIncrement = false;
				colvarCNTCPRSN.IsNullable = false;
				colvarCNTCPRSN.IsPrimaryKey = false;
				colvarCNTCPRSN.IsForeignKey = false;
				colvarCNTCPRSN.IsReadOnly = false;
				colvarCNTCPRSN.DefaultSetting = @"";
				colvarCNTCPRSN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCNTCPRSN);

				TableSchema.TableColumn colvarADDRESS1 = new TableSchema.TableColumn(schema);
				colvarADDRESS1.ColumnName = "ADDRESS1";
				colvarADDRESS1.DataType = DbType.AnsiStringFixedLength;
				colvarADDRESS1.MaxLength = 61;
				colvarADDRESS1.AutoIncrement = false;
				colvarADDRESS1.IsNullable = false;
				colvarADDRESS1.IsPrimaryKey = false;
				colvarADDRESS1.IsForeignKey = false;
				colvarADDRESS1.IsReadOnly = false;
				colvarADDRESS1.DefaultSetting = @"";
				colvarADDRESS1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarADDRESS1);

				TableSchema.TableColumn colvarADDRESS2 = new TableSchema.TableColumn(schema);
				colvarADDRESS2.ColumnName = "ADDRESS2";
				colvarADDRESS2.DataType = DbType.AnsiStringFixedLength;
				colvarADDRESS2.MaxLength = 61;
				colvarADDRESS2.AutoIncrement = false;
				colvarADDRESS2.IsNullable = false;
				colvarADDRESS2.IsPrimaryKey = false;
				colvarADDRESS2.IsForeignKey = false;
				colvarADDRESS2.IsReadOnly = false;
				colvarADDRESS2.DefaultSetting = @"";
				colvarADDRESS2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarADDRESS2);

				TableSchema.TableColumn colvarADDRESS3 = new TableSchema.TableColumn(schema);
				colvarADDRESS3.ColumnName = "ADDRESS3";
				colvarADDRESS3.DataType = DbType.AnsiStringFixedLength;
				colvarADDRESS3.MaxLength = 61;
				colvarADDRESS3.AutoIncrement = false;
				colvarADDRESS3.IsNullable = false;
				colvarADDRESS3.IsPrimaryKey = false;
				colvarADDRESS3.IsForeignKey = false;
				colvarADDRESS3.IsReadOnly = false;
				colvarADDRESS3.DefaultSetting = @"";
				colvarADDRESS3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarADDRESS3);

				TableSchema.TableColumn colvarCITY = new TableSchema.TableColumn(schema);
				colvarCITY.ColumnName = "CITY";
				colvarCITY.DataType = DbType.AnsiStringFixedLength;
				colvarCITY.MaxLength = 35;
				colvarCITY.AutoIncrement = false;
				colvarCITY.IsNullable = false;
				colvarCITY.IsPrimaryKey = false;
				colvarCITY.IsForeignKey = false;
				colvarCITY.IsReadOnly = false;
				colvarCITY.DefaultSetting = @"";
				colvarCITY.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCITY);

				TableSchema.TableColumn colvarSTATE = new TableSchema.TableColumn(schema);
				colvarSTATE.ColumnName = "STATE";
				colvarSTATE.DataType = DbType.AnsiStringFixedLength;
				colvarSTATE.MaxLength = 29;
				colvarSTATE.AutoIncrement = false;
				colvarSTATE.IsNullable = false;
				colvarSTATE.IsPrimaryKey = false;
				colvarSTATE.IsForeignKey = false;
				colvarSTATE.IsReadOnly = false;
				colvarSTATE.DefaultSetting = @"";
				colvarSTATE.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTATE);

				TableSchema.TableColumn colvarZIPCODE = new TableSchema.TableColumn(schema);
				colvarZIPCODE.ColumnName = "ZIPCODE";
				colvarZIPCODE.DataType = DbType.AnsiStringFixedLength;
				colvarZIPCODE.MaxLength = 11;
				colvarZIPCODE.AutoIncrement = false;
				colvarZIPCODE.IsNullable = false;
				colvarZIPCODE.IsPrimaryKey = false;
				colvarZIPCODE.IsForeignKey = false;
				colvarZIPCODE.IsReadOnly = false;
				colvarZIPCODE.DefaultSetting = @"";
				colvarZIPCODE.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZIPCODE);

				TableSchema.TableColumn colvarCOUNTRY = new TableSchema.TableColumn(schema);
				colvarCOUNTRY.ColumnName = "COUNTRY";
				colvarCOUNTRY.DataType = DbType.AnsiStringFixedLength;
				colvarCOUNTRY.MaxLength = 61;
				colvarCOUNTRY.AutoIncrement = false;
				colvarCOUNTRY.IsNullable = false;
				colvarCOUNTRY.IsPrimaryKey = false;
				colvarCOUNTRY.IsForeignKey = false;
				colvarCOUNTRY.IsReadOnly = false;
				colvarCOUNTRY.DefaultSetting = @"";
				colvarCOUNTRY.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCOUNTRY);

				TableSchema.TableColumn colvarPHONE1 = new TableSchema.TableColumn(schema);
				colvarPHONE1.ColumnName = "PHONE1";
				colvarPHONE1.DataType = DbType.AnsiStringFixedLength;
				colvarPHONE1.MaxLength = 21;
				colvarPHONE1.AutoIncrement = false;
				colvarPHONE1.IsNullable = false;
				colvarPHONE1.IsPrimaryKey = false;
				colvarPHONE1.IsForeignKey = false;
				colvarPHONE1.IsReadOnly = false;
				colvarPHONE1.DefaultSetting = @"";
				colvarPHONE1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPHONE1);

				TableSchema.TableColumn colvarPHONE2 = new TableSchema.TableColumn(schema);
				colvarPHONE2.ColumnName = "PHONE2";
				colvarPHONE2.DataType = DbType.AnsiStringFixedLength;
				colvarPHONE2.MaxLength = 21;
				colvarPHONE2.AutoIncrement = false;
				colvarPHONE2.IsNullable = false;
				colvarPHONE2.IsPrimaryKey = false;
				colvarPHONE2.IsForeignKey = false;
				colvarPHONE2.IsReadOnly = false;
				colvarPHONE2.DefaultSetting = @"";
				colvarPHONE2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPHONE2);

				TableSchema.TableColumn colvarPHONE3 = new TableSchema.TableColumn(schema);
				colvarPHONE3.ColumnName = "PHONE3";
				colvarPHONE3.DataType = DbType.AnsiStringFixedLength;
				colvarPHONE3.MaxLength = 21;
				colvarPHONE3.AutoIncrement = false;
				colvarPHONE3.IsNullable = false;
				colvarPHONE3.IsPrimaryKey = false;
				colvarPHONE3.IsForeignKey = false;
				colvarPHONE3.IsReadOnly = false;
				colvarPHONE3.DefaultSetting = @"";
				colvarPHONE3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPHONE3);

				TableSchema.TableColumn colvarFAX = new TableSchema.TableColumn(schema);
				colvarFAX.ColumnName = "FAX";
				colvarFAX.DataType = DbType.AnsiStringFixedLength;
				colvarFAX.MaxLength = 21;
				colvarFAX.AutoIncrement = false;
				colvarFAX.IsNullable = false;
				colvarFAX.IsPrimaryKey = false;
				colvarFAX.IsForeignKey = false;
				colvarFAX.IsReadOnly = false;
				colvarFAX.DefaultSetting = @"";
				colvarFAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFAX);

				TableSchema.TableColumn colvarTXUSRDF1 = new TableSchema.TableColumn(schema);
				colvarTXUSRDF1.ColumnName = "TXUSRDF1";
				colvarTXUSRDF1.DataType = DbType.AnsiStringFixedLength;
				colvarTXUSRDF1.MaxLength = 21;
				colvarTXUSRDF1.AutoIncrement = false;
				colvarTXUSRDF1.IsNullable = false;
				colvarTXUSRDF1.IsPrimaryKey = false;
				colvarTXUSRDF1.IsForeignKey = false;
				colvarTXUSRDF1.IsReadOnly = false;
				colvarTXUSRDF1.DefaultSetting = @"";
				colvarTXUSRDF1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXUSRDF1);

				TableSchema.TableColumn colvarTXUSRDF2 = new TableSchema.TableColumn(schema);
				colvarTXUSRDF2.ColumnName = "TXUSRDF2";
				colvarTXUSRDF2.DataType = DbType.AnsiStringFixedLength;
				colvarTXUSRDF2.MaxLength = 21;
				colvarTXUSRDF2.AutoIncrement = false;
				colvarTXUSRDF2.IsNullable = false;
				colvarTXUSRDF2.IsPrimaryKey = false;
				colvarTXUSRDF2.IsForeignKey = false;
				colvarTXUSRDF2.IsReadOnly = false;
				colvarTXUSRDF2.DefaultSetting = @"";
				colvarTXUSRDF2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXUSRDF2);

				TableSchema.TableColumn colvarVATREGTX = new TableSchema.TableColumn(schema);
				colvarVATREGTX.ColumnName = "VATREGTX";
				colvarVATREGTX.DataType = DbType.Byte;
				colvarVATREGTX.MaxLength = 0;
				colvarVATREGTX.AutoIncrement = false;
				colvarVATREGTX.IsNullable = false;
				colvarVATREGTX.IsPrimaryKey = false;
				colvarVATREGTX.IsForeignKey = false;
				colvarVATREGTX.IsReadOnly = false;
				colvarVATREGTX.DefaultSetting = @"";
				colvarVATREGTX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVATREGTX);

				TableSchema.TableColumn colvarTaxInvReqd = new TableSchema.TableColumn(schema);
				colvarTaxInvReqd.ColumnName = "TaxInvReqd";
				colvarTaxInvReqd.DataType = DbType.Byte;
				colvarTaxInvReqd.MaxLength = 0;
				colvarTaxInvReqd.AutoIncrement = false;
				colvarTaxInvReqd.IsNullable = false;
				colvarTaxInvReqd.IsPrimaryKey = false;
				colvarTaxInvReqd.IsForeignKey = false;
				colvarTaxInvReqd.IsReadOnly = false;
				colvarTaxInvReqd.DefaultSetting = @"";
				colvarTaxInvReqd.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxInvReqd);

				TableSchema.TableColumn colvarTaxPostToAcct = new TableSchema.TableColumn(schema);
				colvarTaxPostToAcct.ColumnName = "TaxPostToAcct";
				colvarTaxPostToAcct.DataType = DbType.Int16;
				colvarTaxPostToAcct.MaxLength = 0;
				colvarTaxPostToAcct.AutoIncrement = false;
				colvarTaxPostToAcct.IsNullable = false;
				colvarTaxPostToAcct.IsPrimaryKey = false;
				colvarTaxPostToAcct.IsForeignKey = false;
				colvarTaxPostToAcct.IsReadOnly = false;
				colvarTaxPostToAcct.DefaultSetting = @"";
				colvarTaxPostToAcct.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxPostToAcct);

				TableSchema.TableColumn colvarTaxBoxes = new TableSchema.TableColumn(schema);
				colvarTaxBoxes.ColumnName = "TaxBoxes";
				colvarTaxBoxes.DataType = DbType.Binary;
				colvarTaxBoxes.MaxLength = 4;
				colvarTaxBoxes.AutoIncrement = false;
				colvarTaxBoxes.IsNullable = false;
				colvarTaxBoxes.IsPrimaryKey = false;
				colvarTaxBoxes.IsForeignKey = false;
				colvarTaxBoxes.IsReadOnly = false;
				colvarTaxBoxes.DefaultSetting = @"";
				colvarTaxBoxes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxBoxes);

				TableSchema.TableColumn colvarIGNRGRSSAMNT = new TableSchema.TableColumn(schema);
				colvarIGNRGRSSAMNT.ColumnName = "IGNRGRSSAMNT";
				colvarIGNRGRSSAMNT.DataType = DbType.Byte;
				colvarIGNRGRSSAMNT.MaxLength = 0;
				colvarIGNRGRSSAMNT.AutoIncrement = false;
				colvarIGNRGRSSAMNT.IsNullable = false;
				colvarIGNRGRSSAMNT.IsPrimaryKey = false;
				colvarIGNRGRSSAMNT.IsForeignKey = false;
				colvarIGNRGRSSAMNT.IsReadOnly = false;
				colvarIGNRGRSSAMNT.DefaultSetting = @"";
				colvarIGNRGRSSAMNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIGNRGRSSAMNT);

				TableSchema.TableColumn colvarTDTABPCT = new TableSchema.TableColumn(schema);
				colvarTDTABPCT.ColumnName = "TDTABPCT";
				colvarTDTABPCT.DataType = DbType.Decimal;
				colvarTDTABPCT.MaxLength = 0;
				colvarTDTABPCT.AutoIncrement = false;
				colvarTDTABPCT.IsNullable = false;
				colvarTDTABPCT.IsPrimaryKey = false;
				colvarTDTABPCT.IsForeignKey = false;
				colvarTDTABPCT.IsReadOnly = false;
				colvarTDTABPCT.DefaultSetting = @"";
				colvarTDTABPCT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDTABPCT);

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = false;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["NxsAccountingProvider"].AddSchema("TX00201",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public string TAXDTLID { 
			get { return GetColumnValue<string>(Columns.TAXDTLID); }
			set {
				SetColumnValue(Columns.TAXDTLID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TAXDTLID));
			}
		}
		[DataMember]
		public string TXDTLDSC { 
			get { return GetColumnValue<string>(Columns.TXDTLDSC); }
			set {
				SetColumnValue(Columns.TXDTLDSC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLDSC));
			}
		}
		[DataMember]
		public short TXDTLTYP { 
			get { return GetColumnValue<short>(Columns.TXDTLTYP); }
			set {
				SetColumnValue(Columns.TXDTLTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLTYP));
			}
		}
		[DataMember]
		public int ACTINDX { 
			get { return GetColumnValue<int>(Columns.ACTINDX); }
			set {
				SetColumnValue(Columns.ACTINDX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ACTINDX));
			}
		}
		[DataMember]
		public string TXIDNMBR { 
			get { return GetColumnValue<string>(Columns.TXIDNMBR); }
			set {
				SetColumnValue(Columns.TXIDNMBR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXIDNMBR));
			}
		}
		[DataMember]
		public short TXDTLBSE { 
			get { return GetColumnValue<short>(Columns.TXDTLBSE); }
			set {
				SetColumnValue(Columns.TXDTLBSE, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLBSE));
			}
		}
		[DataMember]
		public decimal TXDTLPCT { 
			get { return GetColumnValue<decimal>(Columns.TXDTLPCT); }
			set {
				SetColumnValue(Columns.TXDTLPCT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLPCT));
			}
		}
		[DataMember]
		public decimal TXDTLAMT { 
			get { return GetColumnValue<decimal>(Columns.TXDTLAMT); }
			set {
				SetColumnValue(Columns.TXDTLAMT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLAMT));
			}
		}
		[DataMember]
		public short TDTLRNDG { 
			get { return GetColumnValue<short>(Columns.TDTLRNDG); }
			set {
				SetColumnValue(Columns.TDTLRNDG, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTLRNDG));
			}
		}
		[DataMember]
		public string TXDBODTL { 
			get { return GetColumnValue<string>(Columns.TXDBODTL); }
			set {
				SetColumnValue(Columns.TXDBODTL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDBODTL));
			}
		}
		[DataMember]
		public decimal TDTABMIN { 
			get { return GetColumnValue<decimal>(Columns.TDTABMIN); }
			set {
				SetColumnValue(Columns.TDTABMIN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTABMIN));
			}
		}
		[DataMember]
		public decimal TDTABMAX { 
			get { return GetColumnValue<decimal>(Columns.TDTABMAX); }
			set {
				SetColumnValue(Columns.TDTABMAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTABMAX));
			}
		}
		[DataMember]
		public decimal TDTAXMIN { 
			get { return GetColumnValue<decimal>(Columns.TDTAXMIN); }
			set {
				SetColumnValue(Columns.TDTAXMIN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTAXMIN));
			}
		}
		[DataMember]
		public decimal TDTAXMAX { 
			get { return GetColumnValue<decimal>(Columns.TDTAXMAX); }
			set {
				SetColumnValue(Columns.TDTAXMAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTAXMAX));
			}
		}
		[DataMember]
		public short TDRNGTYP { 
			get { return GetColumnValue<short>(Columns.TDRNGTYP); }
			set {
				SetColumnValue(Columns.TDRNGTYP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDRNGTYP));
			}
		}
		[DataMember]
		public short TXDTQUAL { 
			get { return GetColumnValue<short>(Columns.TXDTQUAL); }
			set {
				SetColumnValue(Columns.TXDTQUAL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTQUAL));
			}
		}
		[DataMember]
		public byte TDTAXTAX { 
			get { return GetColumnValue<byte>(Columns.TDTAXTAX); }
			set {
				SetColumnValue(Columns.TDTAXTAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTAXTAX));
			}
		}
		[DataMember]
		public byte TXDTLPDC { 
			get { return GetColumnValue<byte>(Columns.TXDTLPDC); }
			set {
				SetColumnValue(Columns.TXDTLPDC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLPDC));
			}
		}
		[DataMember]
		public string TXDTLPCH { 
			get { return GetColumnValue<string>(Columns.TXDTLPCH); }
			set {
				SetColumnValue(Columns.TXDTLPCH, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDTLPCH));
			}
		}
		[DataMember]
		public byte TXDXDISC { 
			get { return GetColumnValue<byte>(Columns.TXDXDISC); }
			set {
				SetColumnValue(Columns.TXDXDISC, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXDXDISC));
			}
		}
		[DataMember]
		public string CMNYTXID { 
			get { return GetColumnValue<string>(Columns.CMNYTXID); }
			set {
				SetColumnValue(Columns.CMNYTXID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CMNYTXID));
			}
		}
		[DataMember]
		public decimal NOTEINDX { 
			get { return GetColumnValue<decimal>(Columns.NOTEINDX); }
			set {
				SetColumnValue(Columns.NOTEINDX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NOTEINDX));
			}
		}
		[DataMember]
		public string NAME { 
			get { return GetColumnValue<string>(Columns.NAME); }
			set {
				SetColumnValue(Columns.NAME, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NAME));
			}
		}
		[DataMember]
		public string CNTCPRSN { 
			get { return GetColumnValue<string>(Columns.CNTCPRSN); }
			set {
				SetColumnValue(Columns.CNTCPRSN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CNTCPRSN));
			}
		}
		[DataMember]
		public string ADDRESS1 { 
			get { return GetColumnValue<string>(Columns.ADDRESS1); }
			set {
				SetColumnValue(Columns.ADDRESS1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ADDRESS1));
			}
		}
		[DataMember]
		public string ADDRESS2 { 
			get { return GetColumnValue<string>(Columns.ADDRESS2); }
			set {
				SetColumnValue(Columns.ADDRESS2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ADDRESS2));
			}
		}
		[DataMember]
		public string ADDRESS3 { 
			get { return GetColumnValue<string>(Columns.ADDRESS3); }
			set {
				SetColumnValue(Columns.ADDRESS3, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ADDRESS3));
			}
		}
		[DataMember]
		public string CITY { 
			get { return GetColumnValue<string>(Columns.CITY); }
			set {
				SetColumnValue(Columns.CITY, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CITY));
			}
		}
		[DataMember]
		public string STATE { 
			get { return GetColumnValue<string>(Columns.STATE); }
			set {
				SetColumnValue(Columns.STATE, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STATE));
			}
		}
		[DataMember]
		public string ZIPCODE { 
			get { return GetColumnValue<string>(Columns.ZIPCODE); }
			set {
				SetColumnValue(Columns.ZIPCODE, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ZIPCODE));
			}
		}
		[DataMember]
		public string COUNTRY { 
			get { return GetColumnValue<string>(Columns.COUNTRY); }
			set {
				SetColumnValue(Columns.COUNTRY, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.COUNTRY));
			}
		}
		[DataMember]
		public string PHONE1 { 
			get { return GetColumnValue<string>(Columns.PHONE1); }
			set {
				SetColumnValue(Columns.PHONE1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PHONE1));
			}
		}
		[DataMember]
		public string PHONE2 { 
			get { return GetColumnValue<string>(Columns.PHONE2); }
			set {
				SetColumnValue(Columns.PHONE2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PHONE2));
			}
		}
		[DataMember]
		public string PHONE3 { 
			get { return GetColumnValue<string>(Columns.PHONE3); }
			set {
				SetColumnValue(Columns.PHONE3, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PHONE3));
			}
		}
		[DataMember]
		public string FAX { 
			get { return GetColumnValue<string>(Columns.FAX); }
			set {
				SetColumnValue(Columns.FAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FAX));
			}
		}
		[DataMember]
		public string TXUSRDF1 { 
			get { return GetColumnValue<string>(Columns.TXUSRDF1); }
			set {
				SetColumnValue(Columns.TXUSRDF1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXUSRDF1));
			}
		}
		[DataMember]
		public string TXUSRDF2 { 
			get { return GetColumnValue<string>(Columns.TXUSRDF2); }
			set {
				SetColumnValue(Columns.TXUSRDF2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXUSRDF2));
			}
		}
		[DataMember]
		public byte VATREGTX { 
			get { return GetColumnValue<byte>(Columns.VATREGTX); }
			set {
				SetColumnValue(Columns.VATREGTX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.VATREGTX));
			}
		}
		[DataMember]
		public byte TaxInvReqd { 
			get { return GetColumnValue<byte>(Columns.TaxInvReqd); }
			set {
				SetColumnValue(Columns.TaxInvReqd, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TaxInvReqd));
			}
		}
		[DataMember]
		public short TaxPostToAcct { 
			get { return GetColumnValue<short>(Columns.TaxPostToAcct); }
			set {
				SetColumnValue(Columns.TaxPostToAcct, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TaxPostToAcct));
			}
		}
		[DataMember]
		public byte[] TaxBoxes { 
			get { return GetColumnValue<byte[]>(Columns.TaxBoxes); }
			set {
				SetColumnValue(Columns.TaxBoxes, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TaxBoxes));
			}
		}
		[DataMember]
		public byte IGNRGRSSAMNT { 
			get { return GetColumnValue<byte>(Columns.IGNRGRSSAMNT); }
			set {
				SetColumnValue(Columns.IGNRGRSSAMNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IGNRGRSSAMNT));
			}
		}
		[DataMember]
		public decimal TDTABPCT { 
			get { return GetColumnValue<decimal>(Columns.TDTABPCT); }
			set {
				SetColumnValue(Columns.TDTABPCT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TDTABPCT));
			}
		}
		[DataMember]
		public int DEX_ROW_ID { 
			get { return GetColumnValue<int>(Columns.DEX_ROW_ID); }
			set {
				SetColumnValue(Columns.DEX_ROW_ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_ID));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return TXDTLDSC;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TAXDTLIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TXDTLDSCColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TXDTLTYPColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ACTINDXColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TXIDNMBRColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TXDTLBSEColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TXDTLPCTColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TXDTLAMTColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn TDTLRNDGColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TXDBODTLColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn TDTABMINColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TDTABMAXColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn TDTAXMINColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn TDTAXMAXColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn TDRNGTYPColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn TXDTQUALColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn TDTAXTAXColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn TXDTLPDCColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn TXDTLPCHColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn TXDXDISCColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CMNYTXIDColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn NOTEINDXColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn NAMEColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn CNTCPRSNColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn ADDRESS1Column
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn ADDRESS2Column
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn ADDRESS3Column
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn CITYColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn STATEColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn ZIPCODEColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn COUNTRYColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn PHONE1Column
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn PHONE2Column
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn PHONE3Column
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn FAXColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn TXUSRDF1Column
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn TXUSRDF2Column
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn VATREGTXColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn TaxInvReqdColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn TaxPostToAcctColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn TaxBoxesColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn IGNRGRSSAMNTColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn TDTABPCTColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[43]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TAXDTLID = @"TAXDTLID";
			public static readonly string TXDTLDSC = @"TXDTLDSC";
			public static readonly string TXDTLTYP = @"TXDTLTYP";
			public static readonly string ACTINDX = @"ACTINDX";
			public static readonly string TXIDNMBR = @"TXIDNMBR";
			public static readonly string TXDTLBSE = @"TXDTLBSE";
			public static readonly string TXDTLPCT = @"TXDTLPCT";
			public static readonly string TXDTLAMT = @"TXDTLAMT";
			public static readonly string TDTLRNDG = @"TDTLRNDG";
			public static readonly string TXDBODTL = @"TXDBODTL";
			public static readonly string TDTABMIN = @"TDTABMIN";
			public static readonly string TDTABMAX = @"TDTABMAX";
			public static readonly string TDTAXMIN = @"TDTAXMIN";
			public static readonly string TDTAXMAX = @"TDTAXMAX";
			public static readonly string TDRNGTYP = @"TDRNGTYP";
			public static readonly string TXDTQUAL = @"TXDTQUAL";
			public static readonly string TDTAXTAX = @"TDTAXTAX";
			public static readonly string TXDTLPDC = @"TXDTLPDC";
			public static readonly string TXDTLPCH = @"TXDTLPCH";
			public static readonly string TXDXDISC = @"TXDXDISC";
			public static readonly string CMNYTXID = @"CMNYTXID";
			public static readonly string NOTEINDX = @"NOTEINDX";
			public static readonly string NAME = @"NAME";
			public static readonly string CNTCPRSN = @"CNTCPRSN";
			public static readonly string ADDRESS1 = @"ADDRESS1";
			public static readonly string ADDRESS2 = @"ADDRESS2";
			public static readonly string ADDRESS3 = @"ADDRESS3";
			public static readonly string CITY = @"CITY";
			public static readonly string STATE = @"STATE";
			public static readonly string ZIPCODE = @"ZIPCODE";
			public static readonly string COUNTRY = @"COUNTRY";
			public static readonly string PHONE1 = @"PHONE1";
			public static readonly string PHONE2 = @"PHONE2";
			public static readonly string PHONE3 = @"PHONE3";
			public static readonly string FAX = @"FAX";
			public static readonly string TXUSRDF1 = @"TXUSRDF1";
			public static readonly string TXUSRDF2 = @"TXUSRDF2";
			public static readonly string VATREGTX = @"VATREGTX";
			public static readonly string TaxInvReqd = @"TaxInvReqd";
			public static readonly string TaxPostToAcct = @"TaxPostToAcct";
			public static readonly string TaxBoxes = @"TaxBoxes";
			public static readonly string IGNRGRSSAMNT = @"IGNRGRSSAMNT";
			public static readonly string TDTABPCT = @"TDTABPCT";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
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
	/// Strongly-typed collection for the UPR40700 class.
	/// </summary>
	[DataContract]
	public partial class UPR40700Collection : ActiveList<UPR40700, UPR40700Collection>
	{
		public static UPR40700Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			UPR40700Collection result = new UPR40700Collection();
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
			foreach (UPR40700 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the UPR40700 table.
	/// </summary>
	[DataContract]
	public partial class UPR40700 : ActiveRecord<UPR40700>, INotifyPropertyChanged
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

		public UPR40700()
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
				TableSchema.Table schema = new TableSchema.Table("UPR40700", TableType.Table, DataService.GetInstance("NxsAccountingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarWRKRCOMP = new TableSchema.TableColumn(schema);
				colvarWRKRCOMP.ColumnName = "WRKRCOMP";
				colvarWRKRCOMP.DataType = DbType.AnsiStringFixedLength;
				colvarWRKRCOMP.MaxLength = 7;
				colvarWRKRCOMP.AutoIncrement = false;
				colvarWRKRCOMP.IsNullable = false;
				colvarWRKRCOMP.IsPrimaryKey = true;
				colvarWRKRCOMP.IsForeignKey = false;
				colvarWRKRCOMP.IsReadOnly = false;
				colvarWRKRCOMP.DefaultSetting = @"";
				colvarWRKRCOMP.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWRKRCOMP);

				TableSchema.TableColumn colvarSTATECD = new TableSchema.TableColumn(schema);
				colvarSTATECD.ColumnName = "STATECD";
				colvarSTATECD.DataType = DbType.AnsiStringFixedLength;
				colvarSTATECD.MaxLength = 3;
				colvarSTATECD.AutoIncrement = false;
				colvarSTATECD.IsNullable = false;
				colvarSTATECD.IsPrimaryKey = false;
				colvarSTATECD.IsForeignKey = false;
				colvarSTATECD.IsReadOnly = false;
				colvarSTATECD.DefaultSetting = @"";
				colvarSTATECD.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTATECD);

				TableSchema.TableColumn colvarDSCRIPTN = new TableSchema.TableColumn(schema);
				colvarDSCRIPTN.ColumnName = "DSCRIPTN";
				colvarDSCRIPTN.DataType = DbType.AnsiStringFixedLength;
				colvarDSCRIPTN.MaxLength = 31;
				colvarDSCRIPTN.AutoIncrement = false;
				colvarDSCRIPTN.IsNullable = false;
				colvarDSCRIPTN.IsPrimaryKey = false;
				colvarDSCRIPTN.IsForeignKey = false;
				colvarDSCRIPTN.IsReadOnly = false;
				colvarDSCRIPTN.DefaultSetting = @"";
				colvarDSCRIPTN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDSCRIPTN);

				TableSchema.TableColumn colvarWKCMPMTH = new TableSchema.TableColumn(schema);
				colvarWKCMPMTH.ColumnName = "WKCMPMTH";
				colvarWKCMPMTH.DataType = DbType.Int16;
				colvarWKCMPMTH.MaxLength = 0;
				colvarWKCMPMTH.AutoIncrement = false;
				colvarWKCMPMTH.IsNullable = false;
				colvarWKCMPMTH.IsPrimaryKey = false;
				colvarWKCMPMTH.IsForeignKey = false;
				colvarWKCMPMTH.IsReadOnly = false;
				colvarWKCMPMTH.DefaultSetting = @"";
				colvarWKCMPMTH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWKCMPMTH);

				TableSchema.TableColumn colvarWKCMPAMT = new TableSchema.TableColumn(schema);
				colvarWKCMPAMT.ColumnName = "WKCMPAMT";
				colvarWKCMPAMT.DataType = DbType.Decimal;
				colvarWKCMPAMT.MaxLength = 0;
				colvarWKCMPAMT.AutoIncrement = false;
				colvarWKCMPAMT.IsNullable = false;
				colvarWKCMPAMT.IsPrimaryKey = false;
				colvarWKCMPAMT.IsForeignKey = false;
				colvarWKCMPAMT.IsReadOnly = false;
				colvarWKCMPAMT.DefaultSetting = @"";
				colvarWKCMPAMT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWKCMPAMT);

				TableSchema.TableColumn colvarWKCMPUNT = new TableSchema.TableColumn(schema);
				colvarWKCMPUNT.ColumnName = "WKCMPUNT";
				colvarWKCMPUNT.DataType = DbType.Decimal;
				colvarWKCMPUNT.MaxLength = 0;
				colvarWKCMPUNT.AutoIncrement = false;
				colvarWKCMPUNT.IsNullable = false;
				colvarWKCMPUNT.IsPrimaryKey = false;
				colvarWKCMPUNT.IsForeignKey = false;
				colvarWKCMPUNT.IsReadOnly = false;
				colvarWKCMPUNT.DefaultSetting = @"";
				colvarWKCMPUNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWKCMPUNT);

				TableSchema.TableColumn colvarWKCMPCNT = new TableSchema.TableColumn(schema);
				colvarWKCMPCNT.ColumnName = "WKCMPCNT";
				colvarWKCMPCNT.DataType = DbType.Int32;
				colvarWKCMPCNT.MaxLength = 0;
				colvarWKCMPCNT.AutoIncrement = false;
				colvarWKCMPCNT.IsNullable = false;
				colvarWKCMPCNT.IsPrimaryKey = false;
				colvarWKCMPCNT.IsForeignKey = false;
				colvarWKCMPCNT.IsReadOnly = false;
				colvarWKCMPCNT.DefaultSetting = @"";
				colvarWKCMPCNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWKCMPCNT);

				TableSchema.TableColumn colvarWCMOTMTH = new TableSchema.TableColumn(schema);
				colvarWCMOTMTH.ColumnName = "WCMOTMTH";
				colvarWCMOTMTH.DataType = DbType.Int16;
				colvarWCMOTMTH.MaxLength = 0;
				colvarWCMOTMTH.AutoIncrement = false;
				colvarWCMOTMTH.IsNullable = false;
				colvarWCMOTMTH.IsPrimaryKey = false;
				colvarWCMOTMTH.IsForeignKey = false;
				colvarWCMOTMTH.IsReadOnly = false;
				colvarWCMOTMTH.DefaultSetting = @"";
				colvarWCMOTMTH.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWCMOTMTH);

				TableSchema.TableColumn colvarWCWGLIMT = new TableSchema.TableColumn(schema);
				colvarWCWGLIMT.ColumnName = "WCWGLIMT";
				colvarWCWGLIMT.DataType = DbType.Decimal;
				colvarWCWGLIMT.MaxLength = 0;
				colvarWCWGLIMT.AutoIncrement = false;
				colvarWCWGLIMT.IsNullable = false;
				colvarWCWGLIMT.IsPrimaryKey = false;
				colvarWCWGLIMT.IsForeignKey = false;
				colvarWCWGLIMT.IsReadOnly = false;
				colvarWCWGLIMT.DefaultSetting = @"";
				colvarWCWGLIMT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWCWGLIMT);

				TableSchema.TableColumn colvarNOTEINDX = new TableSchema.TableColumn(schema);
				colvarNOTEINDX.ColumnName = "NOTEINDX";
				colvarNOTEINDX.DataType = DbType.Decimal;
				colvarNOTEINDX.MaxLength = 0;
				colvarNOTEINDX.AutoIncrement = false;
				colvarNOTEINDX.IsNullable = false;
				colvarNOTEINDX.IsPrimaryKey = false;
				colvarNOTEINDX.IsForeignKey = false;
				colvarNOTEINDX.IsReadOnly = false;
				colvarNOTEINDX.DefaultSetting = @"";
				colvarNOTEINDX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNOTEINDX);

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = false;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["NxsAccountingProvider"].AddSchema("UPR40700",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static UPR40700 LoadFrom(UPR40700 item)
		{
			UPR40700 result = new UPR40700();
			if (item.WRKRCOMP != default(string)) {
				result.LoadByKey(item.WRKRCOMP);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string WRKRCOMP { 
			get { return GetColumnValue<string>(Columns.WRKRCOMP); }
			set {
				SetColumnValue(Columns.WRKRCOMP, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WRKRCOMP));
			}
		}
		[DataMember]
		public string STATECD { 
			get { return GetColumnValue<string>(Columns.STATECD); }
			set {
				SetColumnValue(Columns.STATECD, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STATECD));
			}
		}
		[DataMember]
		public string DSCRIPTN { 
			get { return GetColumnValue<string>(Columns.DSCRIPTN); }
			set {
				SetColumnValue(Columns.DSCRIPTN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DSCRIPTN));
			}
		}
		[DataMember]
		public short WKCMPMTH { 
			get { return GetColumnValue<short>(Columns.WKCMPMTH); }
			set {
				SetColumnValue(Columns.WKCMPMTH, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WKCMPMTH));
			}
		}
		[DataMember]
		public decimal WKCMPAMT { 
			get { return GetColumnValue<decimal>(Columns.WKCMPAMT); }
			set {
				SetColumnValue(Columns.WKCMPAMT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WKCMPAMT));
			}
		}
		[DataMember]
		public decimal WKCMPUNT { 
			get { return GetColumnValue<decimal>(Columns.WKCMPUNT); }
			set {
				SetColumnValue(Columns.WKCMPUNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WKCMPUNT));
			}
		}
		[DataMember]
		public int WKCMPCNT { 
			get { return GetColumnValue<int>(Columns.WKCMPCNT); }
			set {
				SetColumnValue(Columns.WKCMPCNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WKCMPCNT));
			}
		}
		[DataMember]
		public short WCMOTMTH { 
			get { return GetColumnValue<short>(Columns.WCMOTMTH); }
			set {
				SetColumnValue(Columns.WCMOTMTH, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WCMOTMTH));
			}
		}
		[DataMember]
		public decimal WCWGLIMT { 
			get { return GetColumnValue<decimal>(Columns.WCWGLIMT); }
			set {
				SetColumnValue(Columns.WCWGLIMT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WCWGLIMT));
			}
		}
		[DataMember]
		public decimal NOTEINDX { 
			get { return GetColumnValue<decimal>(Columns.NOTEINDX); }
			set {
				SetColumnValue(Columns.NOTEINDX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NOTEINDX));
			}
		}
		[DataMember]
		public int DEX_ROW_ID { 
			get { return GetColumnValue<int>(Columns.DEX_ROW_ID); }
			set {
				SetColumnValue(Columns.DEX_ROW_ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_ID));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return STATECD;
		}

		#region Typed Columns

		public static TableSchema.TableColumn WRKRCOMPColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn STATECDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DSCRIPTNColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn WKCMPMTHColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn WKCMPAMTColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn WKCMPUNTColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn WKCMPCNTColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn WCMOTMTHColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn WCWGLIMTColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn NOTEINDXColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string WRKRCOMP = @"WRKRCOMP";
			public static readonly string STATECD = @"STATECD";
			public static readonly string DSCRIPTN = @"DSCRIPTN";
			public static readonly string WKCMPMTH = @"WKCMPMTH";
			public static readonly string WKCMPAMT = @"WKCMPAMT";
			public static readonly string WKCMPUNT = @"WKCMPUNT";
			public static readonly string WKCMPCNT = @"WKCMPCNT";
			public static readonly string WCMOTMTH = @"WCMOTMTH";
			public static readonly string WCWGLIMT = @"WCWGLIMT";
			public static readonly string NOTEINDX = @"NOTEINDX";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return WRKRCOMP; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the UPR41301 class.
	/// </summary>
	[DataContract]
	public partial class UPR41301Collection : ActiveList<UPR41301, UPR41301Collection>
	{
		public static UPR41301Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			UPR41301Collection result = new UPR41301Collection();
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
			foreach (UPR41301 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the UPR41301 table.
	/// </summary>
	[DataContract]
	public partial class UPR41301 : ActiveRecord<UPR41301>, INotifyPropertyChanged
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

		public UPR41301()
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
				TableSchema.Table schema = new TableSchema.Table("UPR41301", TableType.Table, DataService.GetInstance("NxsAccountingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTAXCODE = new TableSchema.TableColumn(schema);
				colvarTAXCODE.ColumnName = "TAXCODE";
				colvarTAXCODE.DataType = DbType.AnsiStringFixedLength;
				colvarTAXCODE.MaxLength = 7;
				colvarTAXCODE.AutoIncrement = false;
				colvarTAXCODE.IsNullable = false;
				colvarTAXCODE.IsPrimaryKey = false;
				colvarTAXCODE.IsForeignKey = false;
				colvarTAXCODE.IsReadOnly = false;
				colvarTAXCODE.DefaultSetting = @"";
				colvarTAXCODE.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTAXCODE);

				TableSchema.TableColumn colvarTXFLGSTS = new TableSchema.TableColumn(schema);
				colvarTXFLGSTS.ColumnName = "TXFLGSTS";
				colvarTXFLGSTS.DataType = DbType.AnsiStringFixedLength;
				colvarTXFLGSTS.MaxLength = 7;
				colvarTXFLGSTS.AutoIncrement = false;
				colvarTXFLGSTS.IsNullable = false;
				colvarTXFLGSTS.IsPrimaryKey = false;
				colvarTXFLGSTS.IsForeignKey = false;
				colvarTXFLGSTS.IsReadOnly = false;
				colvarTXFLGSTS.DefaultSetting = @"";
				colvarTXFLGSTS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTXFLGSTS);

				TableSchema.TableColumn colvarSTSDESCR = new TableSchema.TableColumn(schema);
				colvarSTSDESCR.ColumnName = "STSDESCR";
				colvarSTSDESCR.DataType = DbType.AnsiStringFixedLength;
				colvarSTSDESCR.MaxLength = 31;
				colvarSTSDESCR.AutoIncrement = false;
				colvarSTSDESCR.IsNullable = false;
				colvarSTSDESCR.IsPrimaryKey = false;
				colvarSTSDESCR.IsForeignKey = false;
				colvarSTSDESCR.IsReadOnly = false;
				colvarSTSDESCR.DefaultSetting = @"";
				colvarSTSDESCR.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTSDESCR);

				TableSchema.TableColumn colvarLINCLIM = new TableSchema.TableColumn(schema);
				colvarLINCLIM.ColumnName = "LINCLIM";
				colvarLINCLIM.DataType = DbType.Decimal;
				colvarLINCLIM.MaxLength = 0;
				colvarLINCLIM.AutoIncrement = false;
				colvarLINCLIM.IsNullable = false;
				colvarLINCLIM.IsPrimaryKey = false;
				colvarLINCLIM.IsForeignKey = false;
				colvarLINCLIM.IsReadOnly = false;
				colvarLINCLIM.DefaultSetting = @"";
				colvarLINCLIM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLINCLIM);

				TableSchema.TableColumn colvarPRSEXAMT = new TableSchema.TableColumn(schema);
				colvarPRSEXAMT.ColumnName = "PRSEXAMT";
				colvarPRSEXAMT.DataType = DbType.Decimal;
				colvarPRSEXAMT.MaxLength = 0;
				colvarPRSEXAMT.AutoIncrement = false;
				colvarPRSEXAMT.IsNullable = false;
				colvarPRSEXAMT.IsPrimaryKey = false;
				colvarPRSEXAMT.IsForeignKey = false;
				colvarPRSEXAMT.IsReadOnly = false;
				colvarPRSEXAMT.DefaultSetting = @"";
				colvarPRSEXAMT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPRSEXAMT);

				TableSchema.TableColumn colvarINCPSNEX = new TableSchema.TableColumn(schema);
				colvarINCPSNEX.ColumnName = "INCPSNEX";
				colvarINCPSNEX.DataType = DbType.Byte;
				colvarINCPSNEX.MaxLength = 0;
				colvarINCPSNEX.AutoIncrement = false;
				colvarINCPSNEX.IsNullable = false;
				colvarINCPSNEX.IsPrimaryKey = false;
				colvarINCPSNEX.IsForeignKey = false;
				colvarINCPSNEX.IsReadOnly = false;
				colvarINCPSNEX.DefaultSetting = @"";
				colvarINCPSNEX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarINCPSNEX);

				TableSchema.TableColumn colvarINCADALW = new TableSchema.TableColumn(schema);
				colvarINCADALW.ColumnName = "INCADALW";
				colvarINCADALW.DataType = DbType.Byte;
				colvarINCADALW.MaxLength = 0;
				colvarINCADALW.AutoIncrement = false;
				colvarINCADALW.IsNullable = false;
				colvarINCADALW.IsPrimaryKey = false;
				colvarINCADALW.IsForeignKey = false;
				colvarINCADALW.IsReadOnly = false;
				colvarINCADALW.DefaultSetting = @"";
				colvarINCADALW.ForeignKeyTableName = "";
				schema.Columns.Add(colvarINCADALW);

				TableSchema.TableColumn colvarINCLDEPN = new TableSchema.TableColumn(schema);
				colvarINCLDEPN.ColumnName = "INCLDEPN";
				colvarINCLDEPN.DataType = DbType.Byte;
				colvarINCLDEPN.MaxLength = 0;
				colvarINCLDEPN.AutoIncrement = false;
				colvarINCLDEPN.IsNullable = false;
				colvarINCLDEPN.IsPrimaryKey = false;
				colvarINCLDEPN.IsForeignKey = false;
				colvarINCLDEPN.IsReadOnly = false;
				colvarINCLDEPN.DefaultSetting = @"";
				colvarINCLDEPN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarINCLDEPN);

				TableSchema.TableColumn colvarFDTXPRCT = new TableSchema.TableColumn(schema);
				colvarFDTXPRCT.ColumnName = "FDTXPRCT";
				colvarFDTXPRCT.DataType = DbType.Int32;
				colvarFDTXPRCT.MaxLength = 0;
				colvarFDTXPRCT.AutoIncrement = false;
				colvarFDTXPRCT.IsNullable = false;
				colvarFDTXPRCT.IsPrimaryKey = false;
				colvarFDTXPRCT.IsForeignKey = false;
				colvarFDTXPRCT.IsReadOnly = false;
				colvarFDTXPRCT.DefaultSetting = @"";
				colvarFDTXPRCT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFDTXPRCT);

				TableSchema.TableColumn colvarFEDTXMAX = new TableSchema.TableColumn(schema);
				colvarFEDTXMAX.ColumnName = "FEDTXMAX";
				colvarFEDTXMAX.DataType = DbType.Decimal;
				colvarFEDTXMAX.MaxLength = 0;
				colvarFEDTXMAX.AutoIncrement = false;
				colvarFEDTXMAX.IsNullable = false;
				colvarFEDTXMAX.IsPrimaryKey = false;
				colvarFEDTXMAX.IsForeignKey = false;
				colvarFEDTXMAX.IsReadOnly = false;
				colvarFEDTXMAX.DefaultSetting = @"";
				colvarFEDTXMAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFEDTXMAX);

				TableSchema.TableColumn colvarFICATXPT = new TableSchema.TableColumn(schema);
				colvarFICATXPT.ColumnName = "FICATXPT";
				colvarFICATXPT.DataType = DbType.Int32;
				colvarFICATXPT.MaxLength = 0;
				colvarFICATXPT.AutoIncrement = false;
				colvarFICATXPT.IsNullable = false;
				colvarFICATXPT.IsPrimaryKey = false;
				colvarFICATXPT.IsForeignKey = false;
				colvarFICATXPT.IsReadOnly = false;
				colvarFICATXPT.DefaultSetting = @"";
				colvarFICATXPT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFICATXPT);

				TableSchema.TableColumn colvarFICATXMN = new TableSchema.TableColumn(schema);
				colvarFICATXMN.ColumnName = "FICATXMN";
				colvarFICATXMN.DataType = DbType.Decimal;
				colvarFICATXMN.MaxLength = 0;
				colvarFICATXMN.AutoIncrement = false;
				colvarFICATXMN.IsNullable = false;
				colvarFICATXMN.IsPrimaryKey = false;
				colvarFICATXMN.IsForeignKey = false;
				colvarFICATXMN.IsReadOnly = false;
				colvarFICATXMN.DefaultSetting = @"";
				colvarFICATXMN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFICATXMN);

				TableSchema.TableColumn colvarFLATAXRT = new TableSchema.TableColumn(schema);
				colvarFLATAXRT.ColumnName = "FLATAXRT";
				colvarFLATAXRT.DataType = DbType.Int32;
				colvarFLATAXRT.MaxLength = 0;
				colvarFLATAXRT.AutoIncrement = false;
				colvarFLATAXRT.IsNullable = false;
				colvarFLATAXRT.IsPrimaryKey = false;
				colvarFLATAXRT.IsForeignKey = false;
				colvarFLATAXRT.IsReadOnly = false;
				colvarFLATAXRT.DefaultSetting = @"";
				colvarFLATAXRT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFLATAXRT);

				TableSchema.TableColumn colvarSTDDMTHD = new TableSchema.TableColumn(schema);
				colvarSTDDMTHD.ColumnName = "STDDMTHD";
				colvarSTDDMTHD.DataType = DbType.Int16;
				colvarSTDDMTHD.MaxLength = 0;
				colvarSTDDMTHD.AutoIncrement = false;
				colvarSTDDMTHD.IsNullable = false;
				colvarSTDDMTHD.IsPrimaryKey = false;
				colvarSTDDMTHD.IsForeignKey = false;
				colvarSTDDMTHD.IsReadOnly = false;
				colvarSTDDMTHD.DefaultSetting = @"";
				colvarSTDDMTHD.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTDDMTHD);

				TableSchema.TableColumn colvarSTDDPCNT = new TableSchema.TableColumn(schema);
				colvarSTDDPCNT.ColumnName = "STDDPCNT";
				colvarSTDDPCNT.DataType = DbType.Int32;
				colvarSTDDPCNT.MaxLength = 0;
				colvarSTDDPCNT.AutoIncrement = false;
				colvarSTDDPCNT.IsNullable = false;
				colvarSTDDPCNT.IsPrimaryKey = false;
				colvarSTDDPCNT.IsForeignKey = false;
				colvarSTDDPCNT.IsReadOnly = false;
				colvarSTDDPCNT.DefaultSetting = @"";
				colvarSTDDPCNT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTDDPCNT);

				TableSchema.TableColumn colvarSTDDEDAM = new TableSchema.TableColumn(schema);
				colvarSTDDEDAM.ColumnName = "STDDEDAM";
				colvarSTDDEDAM.DataType = DbType.Decimal;
				colvarSTDDEDAM.MaxLength = 0;
				colvarSTDDEDAM.AutoIncrement = false;
				colvarSTDDEDAM.IsNullable = false;
				colvarSTDDEDAM.IsPrimaryKey = false;
				colvarSTDDEDAM.IsForeignKey = false;
				colvarSTDDEDAM.IsReadOnly = false;
				colvarSTDDEDAM.DefaultSetting = @"";
				colvarSTDDEDAM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTDDEDAM);

				TableSchema.TableColumn colvarSTDEDMIN = new TableSchema.TableColumn(schema);
				colvarSTDEDMIN.ColumnName = "STDEDMIN";
				colvarSTDEDMIN.DataType = DbType.Decimal;
				colvarSTDEDMIN.MaxLength = 0;
				colvarSTDEDMIN.AutoIncrement = false;
				colvarSTDEDMIN.IsNullable = false;
				colvarSTDEDMIN.IsPrimaryKey = false;
				colvarSTDEDMIN.IsForeignKey = false;
				colvarSTDEDMIN.IsReadOnly = false;
				colvarSTDEDMIN.DefaultSetting = @"";
				colvarSTDEDMIN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTDEDMIN);

				TableSchema.TableColumn colvarSTDEDMAX = new TableSchema.TableColumn(schema);
				colvarSTDEDMAX.ColumnName = "STDEDMAX";
				colvarSTDEDMAX.DataType = DbType.Decimal;
				colvarSTDEDMAX.MaxLength = 0;
				colvarSTDEDMAX.AutoIncrement = false;
				colvarSTDEDMAX.IsNullable = false;
				colvarSTDEDMAX.IsPrimaryKey = false;
				colvarSTDEDMAX.IsForeignKey = false;
				colvarSTDEDMAX.IsReadOnly = false;
				colvarSTDEDMAX.DefaultSetting = @"";
				colvarSTDEDMAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTDEDMAX);

				TableSchema.TableColumn colvarSPCLEXAM = new TableSchema.TableColumn(schema);
				colvarSPCLEXAM.ColumnName = "SPCLEXAM";
				colvarSPCLEXAM.DataType = DbType.Decimal;
				colvarSPCLEXAM.MaxLength = 0;
				colvarSPCLEXAM.AutoIncrement = false;
				colvarSPCLEXAM.IsNullable = false;
				colvarSPCLEXAM.IsPrimaryKey = false;
				colvarSPCLEXAM.IsForeignKey = false;
				colvarSPCLEXAM.IsReadOnly = false;
				colvarSPCLEXAM.DefaultSetting = @"";
				colvarSPCLEXAM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSPCLEXAM);

				TableSchema.TableColumn colvarSPCLSDED = new TableSchema.TableColumn(schema);
				colvarSPCLSDED.ColumnName = "SPCLSDED";
				colvarSPCLSDED.DataType = DbType.Decimal;
				colvarSPCLSDED.MaxLength = 0;
				colvarSPCLSDED.AutoIncrement = false;
				colvarSPCLSDED.IsNullable = false;
				colvarSPCLSDED.IsPrimaryKey = false;
				colvarSPCLSDED.IsForeignKey = false;
				colvarSPCLSDED.IsReadOnly = false;
				colvarSPCLSDED.DefaultSetting = @"";
				colvarSPCLSDED.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSPCLSDED);

				TableSchema.TableColumn colvarSPCLTXRT = new TableSchema.TableColumn(schema);
				colvarSPCLTXRT.ColumnName = "SPCLTXRT";
				colvarSPCLTXRT.DataType = DbType.Int32;
				colvarSPCLTXRT.MaxLength = 0;
				colvarSPCLTXRT.AutoIncrement = false;
				colvarSPCLTXRT.IsNullable = false;
				colvarSPCLTXRT.IsPrimaryKey = false;
				colvarSPCLTXRT.IsForeignKey = false;
				colvarSPCLTXRT.IsReadOnly = false;
				colvarSPCLTXRT.DefaultSetting = @"";
				colvarSPCLTXRT.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSPCLTXRT);

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = false;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["NxsAccountingProvider"].AddSchema("UPR41301",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public string TAXCODE { 
			get { return GetColumnValue<string>(Columns.TAXCODE); }
			set {
				SetColumnValue(Columns.TAXCODE, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TAXCODE));
			}
		}
		[DataMember]
		public string TXFLGSTS { 
			get { return GetColumnValue<string>(Columns.TXFLGSTS); }
			set {
				SetColumnValue(Columns.TXFLGSTS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TXFLGSTS));
			}
		}
		[DataMember]
		public string STSDESCR { 
			get { return GetColumnValue<string>(Columns.STSDESCR); }
			set {
				SetColumnValue(Columns.STSDESCR, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STSDESCR));
			}
		}
		[DataMember]
		public decimal LINCLIM { 
			get { return GetColumnValue<decimal>(Columns.LINCLIM); }
			set {
				SetColumnValue(Columns.LINCLIM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LINCLIM));
			}
		}
		[DataMember]
		public decimal PRSEXAMT { 
			get { return GetColumnValue<decimal>(Columns.PRSEXAMT); }
			set {
				SetColumnValue(Columns.PRSEXAMT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PRSEXAMT));
			}
		}
		[DataMember]
		public byte INCPSNEX { 
			get { return GetColumnValue<byte>(Columns.INCPSNEX); }
			set {
				SetColumnValue(Columns.INCPSNEX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.INCPSNEX));
			}
		}
		[DataMember]
		public byte INCADALW { 
			get { return GetColumnValue<byte>(Columns.INCADALW); }
			set {
				SetColumnValue(Columns.INCADALW, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.INCADALW));
			}
		}
		[DataMember]
		public byte INCLDEPN { 
			get { return GetColumnValue<byte>(Columns.INCLDEPN); }
			set {
				SetColumnValue(Columns.INCLDEPN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.INCLDEPN));
			}
		}
		[DataMember]
		public int FDTXPRCT { 
			get { return GetColumnValue<int>(Columns.FDTXPRCT); }
			set {
				SetColumnValue(Columns.FDTXPRCT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FDTXPRCT));
			}
		}
		[DataMember]
		public decimal FEDTXMAX { 
			get { return GetColumnValue<decimal>(Columns.FEDTXMAX); }
			set {
				SetColumnValue(Columns.FEDTXMAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FEDTXMAX));
			}
		}
		[DataMember]
		public int FICATXPT { 
			get { return GetColumnValue<int>(Columns.FICATXPT); }
			set {
				SetColumnValue(Columns.FICATXPT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FICATXPT));
			}
		}
		[DataMember]
		public decimal FICATXMN { 
			get { return GetColumnValue<decimal>(Columns.FICATXMN); }
			set {
				SetColumnValue(Columns.FICATXMN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FICATXMN));
			}
		}
		[DataMember]
		public int FLATAXRT { 
			get { return GetColumnValue<int>(Columns.FLATAXRT); }
			set {
				SetColumnValue(Columns.FLATAXRT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FLATAXRT));
			}
		}
		[DataMember]
		public short STDDMTHD { 
			get { return GetColumnValue<short>(Columns.STDDMTHD); }
			set {
				SetColumnValue(Columns.STDDMTHD, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STDDMTHD));
			}
		}
		[DataMember]
		public int STDDPCNT { 
			get { return GetColumnValue<int>(Columns.STDDPCNT); }
			set {
				SetColumnValue(Columns.STDDPCNT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STDDPCNT));
			}
		}
		[DataMember]
		public decimal STDDEDAM { 
			get { return GetColumnValue<decimal>(Columns.STDDEDAM); }
			set {
				SetColumnValue(Columns.STDDEDAM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STDDEDAM));
			}
		}
		[DataMember]
		public decimal STDEDMIN { 
			get { return GetColumnValue<decimal>(Columns.STDEDMIN); }
			set {
				SetColumnValue(Columns.STDEDMIN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STDEDMIN));
			}
		}
		[DataMember]
		public decimal STDEDMAX { 
			get { return GetColumnValue<decimal>(Columns.STDEDMAX); }
			set {
				SetColumnValue(Columns.STDEDMAX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.STDEDMAX));
			}
		}
		[DataMember]
		public decimal SPCLEXAM { 
			get { return GetColumnValue<decimal>(Columns.SPCLEXAM); }
			set {
				SetColumnValue(Columns.SPCLEXAM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SPCLEXAM));
			}
		}
		[DataMember]
		public decimal SPCLSDED { 
			get { return GetColumnValue<decimal>(Columns.SPCLSDED); }
			set {
				SetColumnValue(Columns.SPCLSDED, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SPCLSDED));
			}
		}
		[DataMember]
		public int SPCLTXRT { 
			get { return GetColumnValue<int>(Columns.SPCLTXRT); }
			set {
				SetColumnValue(Columns.SPCLTXRT, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SPCLTXRT));
			}
		}
		[DataMember]
		public int DEX_ROW_ID { 
			get { return GetColumnValue<int>(Columns.DEX_ROW_ID); }
			set {
				SetColumnValue(Columns.DEX_ROW_ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_ID));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return TXFLGSTS;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TAXCODEColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TXFLGSTSColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn STSDESCRColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LINCLIMColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PRSEXAMTColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn INCPSNEXColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn INCADALWColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn INCLDEPNColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FDTXPRCTColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn FEDTXMAXColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn FICATXPTColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn FICATXMNColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn FLATAXRTColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn STDDMTHDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn STDDPCNTColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn STDDEDAMColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn STDEDMINColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn STDEDMAXColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn SPCLEXAMColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn SPCLSDEDColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn SPCLTXRTColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[21]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TAXCODE = @"TAXCODE";
			public static readonly string TXFLGSTS = @"TXFLGSTS";
			public static readonly string STSDESCR = @"STSDESCR";
			public static readonly string LINCLIM = @"LINCLIM";
			public static readonly string PRSEXAMT = @"PRSEXAMT";
			public static readonly string INCPSNEX = @"INCPSNEX";
			public static readonly string INCADALW = @"INCADALW";
			public static readonly string INCLDEPN = @"INCLDEPN";
			public static readonly string FDTXPRCT = @"FDTXPRCT";
			public static readonly string FEDTXMAX = @"FEDTXMAX";
			public static readonly string FICATXPT = @"FICATXPT";
			public static readonly string FICATXMN = @"FICATXMN";
			public static readonly string FLATAXRT = @"FLATAXRT";
			public static readonly string STDDMTHD = @"STDDMTHD";
			public static readonly string STDDPCNT = @"STDDPCNT";
			public static readonly string STDDEDAM = @"STDDEDAM";
			public static readonly string STDEDMIN = @"STDEDMIN";
			public static readonly string STDEDMAX = @"STDEDMAX";
			public static readonly string SPCLEXAM = @"SPCLEXAM";
			public static readonly string SPCLSDED = @"SPCLSDED";
			public static readonly string SPCLTXRT = @"SPCLTXRT";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
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
