


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

namespace NXS.Data.Licensing
{
	/// <summary>
	/// Strongly-typed collection for the LM_Agency class.
	/// </summary>
	[DataContract]
	public partial class LM_AgencyCollection : ActiveList<LM_Agency, LM_AgencyCollection>
	{
		public static LM_AgencyCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_AgencyCollection result = new LM_AgencyCollection();
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
			foreach (LM_Agency item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Agencies table.
	/// </summary>
	[DataContract]
	public partial class LM_Agency : ActiveRecord<LM_Agency>, INotifyPropertyChanged
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

		public LM_Agency()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Agencies", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAgencyID = new TableSchema.TableColumn(schema);
				colvarAgencyID.ColumnName = "AgencyID";
				colvarAgencyID.DataType = DbType.Int32;
				colvarAgencyID.MaxLength = 0;
				colvarAgencyID.AutoIncrement = true;
				colvarAgencyID.IsNullable = false;
				colvarAgencyID.IsPrimaryKey = true;
				colvarAgencyID.IsForeignKey = false;
				colvarAgencyID.IsReadOnly = false;
				colvarAgencyID.DefaultSetting = @"";
				colvarAgencyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAgencyID);

				TableSchema.TableColumn colvarLocationID = new TableSchema.TableColumn(schema);
				colvarLocationID.ColumnName = "LocationID";
				colvarLocationID.DataType = DbType.Int32;
				colvarLocationID.MaxLength = 0;
				colvarLocationID.AutoIncrement = false;
				colvarLocationID.IsNullable = false;
				colvarLocationID.IsPrimaryKey = false;
				colvarLocationID.IsForeignKey = true;
				colvarLocationID.IsReadOnly = false;
				colvarLocationID.DefaultSetting = @"";
				colvarLocationID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarLocationID);

				TableSchema.TableColumn colvarAgencyName = new TableSchema.TableColumn(schema);
				colvarAgencyName.ColumnName = "AgencyName";
				colvarAgencyName.DataType = DbType.String;
				colvarAgencyName.MaxLength = 100;
				colvarAgencyName.AutoIncrement = false;
				colvarAgencyName.IsNullable = false;
				colvarAgencyName.IsPrimaryKey = false;
				colvarAgencyName.IsForeignKey = false;
				colvarAgencyName.IsReadOnly = false;
				colvarAgencyName.DefaultSetting = @"";
				colvarAgencyName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAgencyName);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarContact = new TableSchema.TableColumn(schema);
				colvarContact.ColumnName = "Contact";
				colvarContact.DataType = DbType.String;
				colvarContact.MaxLength = -1;
				colvarContact.AutoIncrement = false;
				colvarContact.IsNullable = true;
				colvarContact.IsPrimaryKey = false;
				colvarContact.IsForeignKey = false;
				colvarContact.IsReadOnly = false;
				colvarContact.DefaultSetting = @"";
				colvarContact.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContact);

				TableSchema.TableColumn colvarWebsite = new TableSchema.TableColumn(schema);
				colvarWebsite.ColumnName = "Website";
				colvarWebsite.DataType = DbType.String;
				colvarWebsite.MaxLength = 100;
				colvarWebsite.AutoIncrement = false;
				colvarWebsite.IsNullable = true;
				colvarWebsite.IsPrimaryKey = false;
				colvarWebsite.IsForeignKey = false;
				colvarWebsite.IsReadOnly = false;
				colvarWebsite.DefaultSetting = @"";
				colvarWebsite.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWebsite);

				TableSchema.TableColumn colvarPhone1 = new TableSchema.TableColumn(schema);
				colvarPhone1.ColumnName = "Phone1";
				colvarPhone1.DataType = DbType.String;
				colvarPhone1.MaxLength = 20;
				colvarPhone1.AutoIncrement = false;
				colvarPhone1.IsNullable = true;
				colvarPhone1.IsPrimaryKey = false;
				colvarPhone1.IsForeignKey = false;
				colvarPhone1.IsReadOnly = false;
				colvarPhone1.DefaultSetting = @"";
				colvarPhone1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone1);

				TableSchema.TableColumn colvarPhone2 = new TableSchema.TableColumn(schema);
				colvarPhone2.ColumnName = "Phone2";
				colvarPhone2.DataType = DbType.String;
				colvarPhone2.MaxLength = 20;
				colvarPhone2.AutoIncrement = false;
				colvarPhone2.IsNullable = true;
				colvarPhone2.IsPrimaryKey = false;
				colvarPhone2.IsForeignKey = false;
				colvarPhone2.IsReadOnly = false;
				colvarPhone2.DefaultSetting = @"";
				colvarPhone2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone2);

				TableSchema.TableColumn colvarFax = new TableSchema.TableColumn(schema);
				colvarFax.ColumnName = "Fax";
				colvarFax.DataType = DbType.String;
				colvarFax.MaxLength = 50;
				colvarFax.AutoIncrement = false;
				colvarFax.IsNullable = true;
				colvarFax.IsPrimaryKey = false;
				colvarFax.IsForeignKey = false;
				colvarFax.IsReadOnly = false;
				colvarFax.DefaultSetting = @"";
				colvarFax.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFax);

				TableSchema.TableColumn colvarStreetAddress = new TableSchema.TableColumn(schema);
				colvarStreetAddress.ColumnName = "StreetAddress";
				colvarStreetAddress.DataType = DbType.String;
				colvarStreetAddress.MaxLength = 250;
				colvarStreetAddress.AutoIncrement = false;
				colvarStreetAddress.IsNullable = true;
				colvarStreetAddress.IsPrimaryKey = false;
				colvarStreetAddress.IsForeignKey = false;
				colvarStreetAddress.IsReadOnly = false;
				colvarStreetAddress.DefaultSetting = @"";
				colvarStreetAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress);

				TableSchema.TableColumn colvarStreetAddress2 = new TableSchema.TableColumn(schema);
				colvarStreetAddress2.ColumnName = "StreetAddress2";
				colvarStreetAddress2.DataType = DbType.String;
				colvarStreetAddress2.MaxLength = 250;
				colvarStreetAddress2.AutoIncrement = false;
				colvarStreetAddress2.IsNullable = true;
				colvarStreetAddress2.IsPrimaryKey = false;
				colvarStreetAddress2.IsForeignKey = false;
				colvarStreetAddress2.IsReadOnly = false;
				colvarStreetAddress2.DefaultSetting = @"";
				colvarStreetAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress2);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarCity2 = new TableSchema.TableColumn(schema);
				colvarCity2.ColumnName = "City2";
				colvarCity2.DataType = DbType.String;
				colvarCity2.MaxLength = 50;
				colvarCity2.AutoIncrement = false;
				colvarCity2.IsNullable = true;
				colvarCity2.IsPrimaryKey = false;
				colvarCity2.IsForeignKey = false;
				colvarCity2.IsReadOnly = false;
				colvarCity2.DefaultSetting = @"";
				colvarCity2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity2);

				TableSchema.TableColumn colvarStateProvince = new TableSchema.TableColumn(schema);
				colvarStateProvince.ColumnName = "StateProvince";
				colvarStateProvince.DataType = DbType.String;
				colvarStateProvince.MaxLength = 50;
				colvarStateProvince.AutoIncrement = false;
				colvarStateProvince.IsNullable = true;
				colvarStateProvince.IsPrimaryKey = false;
				colvarStateProvince.IsForeignKey = false;
				colvarStateProvince.IsReadOnly = false;
				colvarStateProvince.DefaultSetting = @"";
				colvarStateProvince.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateProvince);

				TableSchema.TableColumn colvarStateProvince2 = new TableSchema.TableColumn(schema);
				colvarStateProvince2.ColumnName = "StateProvince2";
				colvarStateProvince2.DataType = DbType.String;
				colvarStateProvince2.MaxLength = 50;
				colvarStateProvince2.AutoIncrement = false;
				colvarStateProvince2.IsNullable = true;
				colvarStateProvince2.IsPrimaryKey = false;
				colvarStateProvince2.IsForeignKey = false;
				colvarStateProvince2.IsReadOnly = false;
				colvarStateProvince2.DefaultSetting = @"";
				colvarStateProvince2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateProvince2);

				TableSchema.TableColumn colvarZipCode = new TableSchema.TableColumn(schema);
				colvarZipCode.ColumnName = "ZipCode";
				colvarZipCode.DataType = DbType.String;
				colvarZipCode.MaxLength = 20;
				colvarZipCode.AutoIncrement = false;
				colvarZipCode.IsNullable = true;
				colvarZipCode.IsPrimaryKey = false;
				colvarZipCode.IsForeignKey = false;
				colvarZipCode.IsReadOnly = false;
				colvarZipCode.DefaultSetting = @"";
				colvarZipCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZipCode);

				TableSchema.TableColumn colvarZipCode2 = new TableSchema.TableColumn(schema);
				colvarZipCode2.ColumnName = "ZipCode2";
				colvarZipCode2.DataType = DbType.String;
				colvarZipCode2.MaxLength = 20;
				colvarZipCode2.AutoIncrement = false;
				colvarZipCode2.IsNullable = true;
				colvarZipCode2.IsPrimaryKey = false;
				colvarZipCode2.IsForeignKey = false;
				colvarZipCode2.IsReadOnly = false;
				colvarZipCode2.DefaultSetting = @"";
				colvarZipCode2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZipCode2);

				TableSchema.TableColumn colvarCountry = new TableSchema.TableColumn(schema);
				colvarCountry.ColumnName = "Country";
				colvarCountry.DataType = DbType.String;
				colvarCountry.MaxLength = 50;
				colvarCountry.AutoIncrement = false;
				colvarCountry.IsNullable = true;
				colvarCountry.IsPrimaryKey = false;
				colvarCountry.IsForeignKey = false;
				colvarCountry.IsReadOnly = false;
				colvarCountry.DefaultSetting = @"";
				colvarCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountry);

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
				colvarIsDeleted.IsNullable = true;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

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

				TableSchema.TableColumn colvarCreatedbyDate = new TableSchema.TableColumn(schema);
				colvarCreatedbyDate.ColumnName = "CreatedbyDate";
				colvarCreatedbyDate.DataType = DbType.DateTime;
				colvarCreatedbyDate.MaxLength = 0;
				colvarCreatedbyDate.AutoIncrement = false;
				colvarCreatedbyDate.IsNullable = false;
				colvarCreatedbyDate.IsPrimaryKey = false;
				colvarCreatedbyDate.IsForeignKey = false;
				colvarCreatedbyDate.IsReadOnly = false;
				colvarCreatedbyDate.DefaultSetting = @"(getdate())";
				colvarCreatedbyDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedbyDate);

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

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Agencies",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Agency LoadFrom(LM_Agency item)
		{
			LM_Agency result = new LM_Agency();
			if (item.AgencyID != default(int)) {
				result.LoadByKey(item.AgencyID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int AgencyID { 
			get { return GetColumnValue<int>(Columns.AgencyID); }
			set {
				SetColumnValue(Columns.AgencyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AgencyID));
			}
		}
		[DataMember]
		public int LocationID { 
			get { return GetColumnValue<int>(Columns.LocationID); }
			set {
				SetColumnValue(Columns.LocationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationID));
			}
		}
		[DataMember]
		public string AgencyName { 
			get { return GetColumnValue<string>(Columns.AgencyName); }
			set {
				SetColumnValue(Columns.AgencyName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AgencyName));
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
		public string Contact { 
			get { return GetColumnValue<string>(Columns.Contact); }
			set {
				SetColumnValue(Columns.Contact, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Contact));
			}
		}
		[DataMember]
		public string Website { 
			get { return GetColumnValue<string>(Columns.Website); }
			set {
				SetColumnValue(Columns.Website, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Website));
			}
		}
		[DataMember]
		public string Phone1 { 
			get { return GetColumnValue<string>(Columns.Phone1); }
			set {
				SetColumnValue(Columns.Phone1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Phone1));
			}
		}
		[DataMember]
		public string Phone2 { 
			get { return GetColumnValue<string>(Columns.Phone2); }
			set {
				SetColumnValue(Columns.Phone2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Phone2));
			}
		}
		[DataMember]
		public string Fax { 
			get { return GetColumnValue<string>(Columns.Fax); }
			set {
				SetColumnValue(Columns.Fax, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Fax));
			}
		}
		[DataMember]
		public string StreetAddress { 
			get { return GetColumnValue<string>(Columns.StreetAddress); }
			set {
				SetColumnValue(Columns.StreetAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetAddress));
			}
		}
		[DataMember]
		public string StreetAddress2 { 
			get { return GetColumnValue<string>(Columns.StreetAddress2); }
			set {
				SetColumnValue(Columns.StreetAddress2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetAddress2));
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
		public string City2 { 
			get { return GetColumnValue<string>(Columns.City2); }
			set {
				SetColumnValue(Columns.City2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.City2));
			}
		}
		[DataMember]
		public string StateProvince { 
			get { return GetColumnValue<string>(Columns.StateProvince); }
			set {
				SetColumnValue(Columns.StateProvince, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StateProvince));
			}
		}
		[DataMember]
		public string StateProvince2 { 
			get { return GetColumnValue<string>(Columns.StateProvince2); }
			set {
				SetColumnValue(Columns.StateProvince2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StateProvince2));
			}
		}
		[DataMember]
		public string ZipCode { 
			get { return GetColumnValue<string>(Columns.ZipCode); }
			set {
				SetColumnValue(Columns.ZipCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ZipCode));
			}
		}
		[DataMember]
		public string ZipCode2 { 
			get { return GetColumnValue<string>(Columns.ZipCode2); }
			set {
				SetColumnValue(Columns.ZipCode2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ZipCode2));
			}
		}
		[DataMember]
		public string Country { 
			get { return GetColumnValue<string>(Columns.Country); }
			set {
				SetColumnValue(Columns.Country, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Country));
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
		public DateTime CreatedbyDate { 
			get { return GetColumnValue<DateTime>(Columns.CreatedbyDate); }
			set {
				SetColumnValue(Columns.CreatedbyDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedbyDate));
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

		#endregion //Properties

		#region ForeignKey Properties

		private LM_Location _Location;
		//Relationship: FK_LM_Agencies_LM_Locations
		public LM_Location Location
		{
			get
			{
				if(_Location == null) {
					_Location = LM_Location.FetchByID(this.LocationID);
				}
				return _Location;
			}
			set
			{
				SetColumnValue("LocationID", value.LocationID);
				_Location = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AgencyID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AgencyIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AgencyNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ContactColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn WebsiteColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn Phone1Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn Phone2Column
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FaxColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn StreetAddressColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn StreetAddress2Column
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn City2Column
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn StateProvinceColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn StateProvince2Column
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ZipCodeColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn ZipCode2Column
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CountryColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn CreatedbyDateColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[23]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AgencyID = @"AgencyID";
			public static readonly string LocationID = @"LocationID";
			public static readonly string AgencyName = @"AgencyName";
			public static readonly string Description = @"Description";
			public static readonly string Contact = @"Contact";
			public static readonly string Website = @"Website";
			public static readonly string Phone1 = @"Phone1";
			public static readonly string Phone2 = @"Phone2";
			public static readonly string Fax = @"Fax";
			public static readonly string StreetAddress = @"StreetAddress";
			public static readonly string StreetAddress2 = @"StreetAddress2";
			public static readonly string City = @"City";
			public static readonly string City2 = @"City2";
			public static readonly string StateProvince = @"StateProvince";
			public static readonly string StateProvince2 = @"StateProvince2";
			public static readonly string ZipCode = @"ZipCode";
			public static readonly string ZipCode2 = @"ZipCode2";
			public static readonly string Country = @"Country";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedbyDate = @"CreatedbyDate";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AgencyID; }
		}
		*/

		#region Foreign Collections

		private LM_RequirementCollection _LM_RequirementsCol;
		//Relationship: FK_LM_Licenses_LM_Agencies
		public LM_RequirementCollection LM_RequirementsCol
		{
			get
			{
				if(_LM_RequirementsCol == null) {
					_LM_RequirementsCol = new LM_RequirementCollection();
					_LM_RequirementsCol.LoadAndCloseReader(LM_Requirement.Query()
						.WHERE(LM_Requirement.Columns.AgencyID, AgencyID).ExecuteReader());
				}
				return _LM_RequirementsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_Attachment class.
	/// </summary>
	[DataContract]
	public partial class LM_AttachmentCollection : ActiveList<LM_Attachment, LM_AttachmentCollection>
	{
		public static LM_AttachmentCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_AttachmentCollection result = new LM_AttachmentCollection();
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
			foreach (LM_Attachment item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Attachments table.
	/// </summary>
	[DataContract]
	public partial class LM_Attachment : ActiveRecord<LM_Attachment>, INotifyPropertyChanged
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

		public LM_Attachment()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Attachments", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAttachmentID = new TableSchema.TableColumn(schema);
				colvarAttachmentID.ColumnName = "AttachmentID";
				colvarAttachmentID.DataType = DbType.Int32;
				colvarAttachmentID.MaxLength = 0;
				colvarAttachmentID.AutoIncrement = true;
				colvarAttachmentID.IsNullable = false;
				colvarAttachmentID.IsPrimaryKey = true;
				colvarAttachmentID.IsForeignKey = false;
				colvarAttachmentID.IsReadOnly = false;
				colvarAttachmentID.DefaultSetting = @"";
				colvarAttachmentID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttachmentID);

				TableSchema.TableColumn colvarForeignKeyID = new TableSchema.TableColumn(schema);
				colvarForeignKeyID.ColumnName = "ForeignKeyID";
				colvarForeignKeyID.DataType = DbType.Int32;
				colvarForeignKeyID.MaxLength = 0;
				colvarForeignKeyID.AutoIncrement = false;
				colvarForeignKeyID.IsNullable = true;
				colvarForeignKeyID.IsPrimaryKey = false;
				colvarForeignKeyID.IsForeignKey = false;
				colvarForeignKeyID.IsReadOnly = false;
				colvarForeignKeyID.DefaultSetting = @"((0))";
				colvarForeignKeyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarForeignKeyID);

				TableSchema.TableColumn colvarAttachmentTypeID = new TableSchema.TableColumn(schema);
				colvarAttachmentTypeID.ColumnName = "AttachmentTypeID";
				colvarAttachmentTypeID.DataType = DbType.Int32;
				colvarAttachmentTypeID.MaxLength = 0;
				colvarAttachmentTypeID.AutoIncrement = false;
				colvarAttachmentTypeID.IsNullable = true;
				colvarAttachmentTypeID.IsPrimaryKey = false;
				colvarAttachmentTypeID.IsForeignKey = true;
				colvarAttachmentTypeID.IsReadOnly = false;
				colvarAttachmentTypeID.DefaultSetting = @"((0))";
				colvarAttachmentTypeID.ForeignKeyTableName = "LM_AttachmentTypes";
				schema.Columns.Add(colvarAttachmentTypeID);

				TableSchema.TableColumn colvarTemplateID = new TableSchema.TableColumn(schema);
				colvarTemplateID.ColumnName = "TemplateID";
				colvarTemplateID.DataType = DbType.Int32;
				colvarTemplateID.MaxLength = 0;
				colvarTemplateID.AutoIncrement = false;
				colvarTemplateID.IsNullable = true;
				colvarTemplateID.IsPrimaryKey = false;
				colvarTemplateID.IsForeignKey = false;
				colvarTemplateID.IsReadOnly = false;
				colvarTemplateID.DefaultSetting = @"";
				colvarTemplateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplateID);

				TableSchema.TableColumn colvarAttachmentName = new TableSchema.TableColumn(schema);
				colvarAttachmentName.ColumnName = "AttachmentName";
				colvarAttachmentName.DataType = DbType.String;
				colvarAttachmentName.MaxLength = 100;
				colvarAttachmentName.AutoIncrement = false;
				colvarAttachmentName.IsNullable = true;
				colvarAttachmentName.IsPrimaryKey = false;
				colvarAttachmentName.IsForeignKey = false;
				colvarAttachmentName.IsReadOnly = false;
				colvarAttachmentName.DefaultSetting = @"('Attachment')";
				colvarAttachmentName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttachmentName);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarFileType = new TableSchema.TableColumn(schema);
				colvarFileType.ColumnName = "FileType";
				colvarFileType.DataType = DbType.String;
				colvarFileType.MaxLength = 50;
				colvarFileType.AutoIncrement = false;
				colvarFileType.IsNullable = true;
				colvarFileType.IsPrimaryKey = false;
				colvarFileType.IsForeignKey = false;
				colvarFileType.IsReadOnly = false;
				colvarFileType.DefaultSetting = @"('txt')";
				colvarFileType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileType);

				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "FileName";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = -1;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = true;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"('File Name')";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);

				TableSchema.TableColumn colvarFileImage = new TableSchema.TableColumn(schema);
				colvarFileImage.ColumnName = "FileImage";
				colvarFileImage.DataType = DbType.Binary;
				colvarFileImage.MaxLength = 2147483647;
				colvarFileImage.AutoIncrement = false;
				colvarFileImage.IsNullable = true;
				colvarFileImage.IsPrimaryKey = false;
				colvarFileImage.IsForeignKey = false;
				colvarFileImage.IsReadOnly = false;
				colvarFileImage.DefaultSetting = @"";
				colvarFileImage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileImage);

				TableSchema.TableColumn colvarSize = new TableSchema.TableColumn(schema);
				colvarSize.ColumnName = "Size";
				colvarSize.DataType = DbType.Int32;
				colvarSize.MaxLength = 0;
				colvarSize.AutoIncrement = false;
				colvarSize.IsNullable = true;
				colvarSize.IsPrimaryKey = false;
				colvarSize.IsForeignKey = false;
				colvarSize.IsReadOnly = false;
				colvarSize.DefaultSetting = @"((0))";
				colvarSize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSize);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = true;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 100;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = true;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"('default')";
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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				TableSchema.TableColumn colvarModifiedByID = new TableSchema.TableColumn(schema);
				colvarModifiedByID.ColumnName = "ModifiedByID";
				colvarModifiedByID.DataType = DbType.String;
				colvarModifiedByID.MaxLength = 100;
				colvarModifiedByID.AutoIncrement = false;
				colvarModifiedByID.IsNullable = true;
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
				colvarModifiedByDate.IsNullable = true;
				colvarModifiedByDate.IsPrimaryKey = false;
				colvarModifiedByDate.IsForeignKey = false;
				colvarModifiedByDate.IsReadOnly = false;
				colvarModifiedByDate.DefaultSetting = @"";
				colvarModifiedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Attachments",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Attachment LoadFrom(LM_Attachment item)
		{
			LM_Attachment result = new LM_Attachment();
			if (item.AttachmentID != default(int)) {
				result.LoadByKey(item.AttachmentID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int AttachmentID { 
			get { return GetColumnValue<int>(Columns.AttachmentID); }
			set {
				SetColumnValue(Columns.AttachmentID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AttachmentID));
			}
		}
		[DataMember]
		public int? ForeignKeyID { 
			get { return GetColumnValue<int?>(Columns.ForeignKeyID); }
			set {
				SetColumnValue(Columns.ForeignKeyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ForeignKeyID));
			}
		}
		[DataMember]
		public int? AttachmentTypeID { 
			get { return GetColumnValue<int?>(Columns.AttachmentTypeID); }
			set {
				SetColumnValue(Columns.AttachmentTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AttachmentTypeID));
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
		public string AttachmentName { 
			get { return GetColumnValue<string>(Columns.AttachmentName); }
			set {
				SetColumnValue(Columns.AttachmentName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AttachmentName));
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
		public string FileType { 
			get { return GetColumnValue<string>(Columns.FileType); }
			set {
				SetColumnValue(Columns.FileType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileType));
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
		public byte[] FileImage { 
			get { return GetColumnValue<byte[]>(Columns.FileImage); }
			set {
				SetColumnValue(Columns.FileImage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FileImage));
			}
		}
		[DataMember]
		public int? Size { 
			get { return GetColumnValue<int?>(Columns.Size); }
			set {
				SetColumnValue(Columns.Size, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Size));
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
		[DataMember]
		public string ModifiedByID { 
			get { return GetColumnValue<string>(Columns.ModifiedByID); }
			set {
				SetColumnValue(Columns.ModifiedByID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByID));
			}
		}
		[DataMember]
		public DateTime? ModifiedByDate { 
			get { return GetColumnValue<DateTime?>(Columns.ModifiedByDate); }
			set {
				SetColumnValue(Columns.ModifiedByDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedByDate));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LM_AttachmentType _AttachmentType;
		//Relationship: FK_LM_Attachments_LM_AttachmentTypes
		public LM_AttachmentType AttachmentType
		{
			get
			{
				if(_AttachmentType == null) {
					_AttachmentType = LM_AttachmentType.FetchByID(this.AttachmentTypeID);
				}
				return _AttachmentType;
			}
			set
			{
				SetColumnValue("AttachmentTypeID", value.AttachmentTypeID);
				_AttachmentType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AttachmentID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AttachmentIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ForeignKeyIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AttachmentTypeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AttachmentNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn FileTypeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn FileNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FileImageColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn SizeColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AttachmentID = @"AttachmentID";
			public static readonly string ForeignKeyID = @"ForeignKeyID";
			public static readonly string AttachmentTypeID = @"AttachmentTypeID";
			public static readonly string TemplateID = @"TemplateID";
			public static readonly string AttachmentName = @"AttachmentName";
			public static readonly string Description = @"Description";
			public static readonly string FileType = @"FileType";
			public static readonly string FileName = @"FileName";
			public static readonly string FileImage = @"FileImage";
			public static readonly string Size = @"Size";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AttachmentID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LM_AttachmentType class.
	/// </summary>
	[DataContract]
	public partial class LM_AttachmentTypeCollection : ActiveList<LM_AttachmentType, LM_AttachmentTypeCollection>
	{
		public static LM_AttachmentTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_AttachmentTypeCollection result = new LM_AttachmentTypeCollection();
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
			foreach (LM_AttachmentType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_AttachmentTypes table.
	/// </summary>
	[DataContract]
	public partial class LM_AttachmentType : ActiveRecord<LM_AttachmentType>, INotifyPropertyChanged
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

		public LM_AttachmentType()
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
				TableSchema.Table schema = new TableSchema.Table("LM_AttachmentTypes", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAttachmentTypeID = new TableSchema.TableColumn(schema);
				colvarAttachmentTypeID.ColumnName = "AttachmentTypeID";
				colvarAttachmentTypeID.DataType = DbType.Int32;
				colvarAttachmentTypeID.MaxLength = 0;
				colvarAttachmentTypeID.AutoIncrement = true;
				colvarAttachmentTypeID.IsNullable = false;
				colvarAttachmentTypeID.IsPrimaryKey = true;
				colvarAttachmentTypeID.IsForeignKey = false;
				colvarAttachmentTypeID.IsReadOnly = false;
				colvarAttachmentTypeID.DefaultSetting = @"";
				colvarAttachmentTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttachmentTypeID);

				TableSchema.TableColumn colvarAttachmentTypeName = new TableSchema.TableColumn(schema);
				colvarAttachmentTypeName.ColumnName = "AttachmentTypeName";
				colvarAttachmentTypeName.DataType = DbType.String;
				colvarAttachmentTypeName.MaxLength = 50;
				colvarAttachmentTypeName.AutoIncrement = false;
				colvarAttachmentTypeName.IsNullable = false;
				colvarAttachmentTypeName.IsPrimaryKey = false;
				colvarAttachmentTypeName.IsForeignKey = false;
				colvarAttachmentTypeName.IsReadOnly = false;
				colvarAttachmentTypeName.DefaultSetting = @"";
				colvarAttachmentTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttachmentTypeName);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_AttachmentTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_AttachmentType LoadFrom(LM_AttachmentType item)
		{
			LM_AttachmentType result = new LM_AttachmentType();
			if (item.AttachmentTypeID != default(int)) {
				result.LoadByKey(item.AttachmentTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int AttachmentTypeID { 
			get { return GetColumnValue<int>(Columns.AttachmentTypeID); }
			set {
				SetColumnValue(Columns.AttachmentTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AttachmentTypeID));
			}
		}
		[DataMember]
		public string AttachmentTypeName { 
			get { return GetColumnValue<string>(Columns.AttachmentTypeName); }
			set {
				SetColumnValue(Columns.AttachmentTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AttachmentTypeName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return AttachmentTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn AttachmentTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AttachmentTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AttachmentTypeID = @"AttachmentTypeID";
			public static readonly string AttachmentTypeName = @"AttachmentTypeName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AttachmentTypeID; }
		}
		*/

		#region Foreign Collections

		private LM_AttachmentCollection _LM_AttachmentsCol;
		//Relationship: FK_LM_Attachments_LM_AttachmentTypes
		public LM_AttachmentCollection LM_AttachmentsCol
		{
			get
			{
				if(_LM_AttachmentsCol == null) {
					_LM_AttachmentsCol = new LM_AttachmentCollection();
					_LM_AttachmentsCol.LoadAndCloseReader(LM_Attachment.Query()
						.WHERE(LM_Attachment.Columns.AttachmentTypeID, AttachmentTypeID).ExecuteReader());
				}
				return _LM_AttachmentsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_LicenseItem class.
	/// </summary>
	[DataContract]
	public partial class LM_LicenseItemCollection : ActiveList<LM_LicenseItem, LM_LicenseItemCollection>
	{
		public static LM_LicenseItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LicenseItemCollection result = new LM_LicenseItemCollection();
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
			foreach (LM_LicenseItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_LicenseItems table.
	/// </summary>
	[DataContract]
	public partial class LM_LicenseItem : ActiveRecord<LM_LicenseItem>, INotifyPropertyChanged
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

		public LM_LicenseItem()
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
				TableSchema.Table schema = new TableSchema.Table("LM_LicenseItems", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLicenseItemID = new TableSchema.TableColumn(schema);
				colvarLicenseItemID.ColumnName = "LicenseItemID";
				colvarLicenseItemID.DataType = DbType.Int32;
				colvarLicenseItemID.MaxLength = 0;
				colvarLicenseItemID.AutoIncrement = true;
				colvarLicenseItemID.IsNullable = false;
				colvarLicenseItemID.IsPrimaryKey = true;
				colvarLicenseItemID.IsForeignKey = false;
				colvarLicenseItemID.IsReadOnly = false;
				colvarLicenseItemID.DefaultSetting = @"";
				colvarLicenseItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseItemID);

				TableSchema.TableColumn colvarLicenseID = new TableSchema.TableColumn(schema);
				colvarLicenseID.ColumnName = "LicenseID";
				colvarLicenseID.DataType = DbType.Int32;
				colvarLicenseID.MaxLength = 0;
				colvarLicenseID.AutoIncrement = false;
				colvarLicenseID.IsNullable = false;
				colvarLicenseID.IsPrimaryKey = false;
				colvarLicenseID.IsForeignKey = true;
				colvarLicenseID.IsReadOnly = false;
				colvarLicenseID.DefaultSetting = @"";
				colvarLicenseID.ForeignKeyTableName = "LM_Licenses";
				schema.Columns.Add(colvarLicenseID);

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

				TableSchema.TableColumn colvarIsCompleted = new TableSchema.TableColumn(schema);
				colvarIsCompleted.ColumnName = "IsCompleted";
				colvarIsCompleted.DataType = DbType.Boolean;
				colvarIsCompleted.MaxLength = 0;
				colvarIsCompleted.AutoIncrement = false;
				colvarIsCompleted.IsNullable = false;
				colvarIsCompleted.IsPrimaryKey = false;
				colvarIsCompleted.IsForeignKey = false;
				colvarIsCompleted.IsReadOnly = false;
				colvarIsCompleted.DefaultSetting = @"((0))";
				colvarIsCompleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsCompleted);

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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_LicenseItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_LicenseItem LoadFrom(LM_LicenseItem item)
		{
			LM_LicenseItem result = new LM_LicenseItem();
			if (item.LicenseItemID != default(int)) {
				result.LoadByKey(item.LicenseItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LicenseItemID { 
			get { return GetColumnValue<int>(Columns.LicenseItemID); }
			set {
				SetColumnValue(Columns.LicenseItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseItemID));
			}
		}
		[DataMember]
		public int LicenseID { 
			get { return GetColumnValue<int>(Columns.LicenseID); }
			set {
				SetColumnValue(Columns.LicenseID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseID));
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
		public bool IsCompleted { 
			get { return GetColumnValue<bool>(Columns.IsCompleted); }
			set {
				SetColumnValue(Columns.IsCompleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsCompleted));
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

		private LM_License _License;
		//Relationship: FK_LM_LicenseItems_LM_Licenses
		public LM_License License
		{
			get
			{
				if(_License == null) {
					_License = LM_License.FetchByID(this.LicenseID);
				}
				return _License;
			}
			set
			{
				SetColumnValue("LicenseID", value.LicenseID);
				_License = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LicenseItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LicenseItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LicenseIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsCompletedColumn
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
			public static readonly string LicenseItemID = @"LicenseItemID";
			public static readonly string LicenseID = @"LicenseID";
			public static readonly string Name = @"Name";
			public static readonly string Description = @"Description";
			public static readonly string IsCompleted = @"IsCompleted";
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
			get { return LicenseItemID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LM_License class.
	/// </summary>
	[DataContract]
	public partial class LM_LicenseCollection : ActiveList<LM_License, LM_LicenseCollection>
	{
		public static LM_LicenseCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LicenseCollection result = new LM_LicenseCollection();
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
			foreach (LM_License item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Licenses table.
	/// </summary>
	[DataContract]
	public partial class LM_License : ActiveRecord<LM_License>, INotifyPropertyChanged
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

		public LM_License()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Licenses", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLicenseID = new TableSchema.TableColumn(schema);
				colvarLicenseID.ColumnName = "LicenseID";
				colvarLicenseID.DataType = DbType.Int32;
				colvarLicenseID.MaxLength = 0;
				colvarLicenseID.AutoIncrement = true;
				colvarLicenseID.IsNullable = false;
				colvarLicenseID.IsPrimaryKey = true;
				colvarLicenseID.IsForeignKey = false;
				colvarLicenseID.IsReadOnly = false;
				colvarLicenseID.DefaultSetting = @"";
				colvarLicenseID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseID);

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = false;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = false;
				colvarRequirementID.IsForeignKey = true;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "LM_Requirements";
				schema.Columns.Add(colvarRequirementID);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 20;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarAccountID = new TableSchema.TableColumn(schema);
				colvarAccountID.ColumnName = "AccountID";
				colvarAccountID.DataType = DbType.Int32;
				colvarAccountID.MaxLength = 0;
				colvarAccountID.AutoIncrement = false;
				colvarAccountID.IsNullable = true;
				colvarAccountID.IsPrimaryKey = false;
				colvarAccountID.IsForeignKey = false;
				colvarAccountID.IsReadOnly = false;
				colvarAccountID.DefaultSetting = @"";
				colvarAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountID);

				TableSchema.TableColumn colvarSubmissionDate = new TableSchema.TableColumn(schema);
				colvarSubmissionDate.ColumnName = "SubmissionDate";
				colvarSubmissionDate.DataType = DbType.DateTime;
				colvarSubmissionDate.MaxLength = 0;
				colvarSubmissionDate.AutoIncrement = false;
				colvarSubmissionDate.IsNullable = true;
				colvarSubmissionDate.IsPrimaryKey = false;
				colvarSubmissionDate.IsForeignKey = false;
				colvarSubmissionDate.IsReadOnly = false;
				colvarSubmissionDate.DefaultSetting = @"";
				colvarSubmissionDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmissionDate);

				TableSchema.TableColumn colvarIssueDate = new TableSchema.TableColumn(schema);
				colvarIssueDate.ColumnName = "IssueDate";
				colvarIssueDate.DataType = DbType.DateTime;
				colvarIssueDate.MaxLength = 0;
				colvarIssueDate.AutoIncrement = false;
				colvarIssueDate.IsNullable = true;
				colvarIssueDate.IsPrimaryKey = false;
				colvarIssueDate.IsForeignKey = false;
				colvarIssueDate.IsReadOnly = false;
				colvarIssueDate.DefaultSetting = @"";
				colvarIssueDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIssueDate);

				TableSchema.TableColumn colvarExpirationDate = new TableSchema.TableColumn(schema);
				colvarExpirationDate.ColumnName = "ExpirationDate";
				colvarExpirationDate.DataType = DbType.DateTime;
				colvarExpirationDate.MaxLength = 0;
				colvarExpirationDate.AutoIncrement = false;
				colvarExpirationDate.IsNullable = true;
				colvarExpirationDate.IsPrimaryKey = false;
				colvarExpirationDate.IsForeignKey = false;
				colvarExpirationDate.IsReadOnly = false;
				colvarExpirationDate.DefaultSetting = @"";
				colvarExpirationDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExpirationDate);

				TableSchema.TableColumn colvarLicenseNumber = new TableSchema.TableColumn(schema);
				colvarLicenseNumber.ColumnName = "LicenseNumber";
				colvarLicenseNumber.DataType = DbType.String;
				colvarLicenseNumber.MaxLength = 50;
				colvarLicenseNumber.AutoIncrement = false;
				colvarLicenseNumber.IsNullable = true;
				colvarLicenseNumber.IsPrimaryKey = false;
				colvarLicenseNumber.IsForeignKey = false;
				colvarLicenseNumber.IsReadOnly = false;
				colvarLicenseNumber.DefaultSetting = @"";
				colvarLicenseNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseNumber);

				TableSchema.TableColumn colvarRequirementsAreMet = new TableSchema.TableColumn(schema);
				colvarRequirementsAreMet.ColumnName = "RequirementsAreMet";
				colvarRequirementsAreMet.DataType = DbType.Boolean;
				colvarRequirementsAreMet.MaxLength = 0;
				colvarRequirementsAreMet.AutoIncrement = false;
				colvarRequirementsAreMet.IsNullable = false;
				colvarRequirementsAreMet.IsPrimaryKey = false;
				colvarRequirementsAreMet.IsForeignKey = false;
				colvarRequirementsAreMet.IsReadOnly = false;
				colvarRequirementsAreMet.DefaultSetting = @"((0))";
				colvarRequirementsAreMet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementsAreMet);

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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Licenses",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_License LoadFrom(LM_License item)
		{
			LM_License result = new LM_License();
			if (item.LicenseID != default(int)) {
				result.LoadByKey(item.LicenseID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LicenseID { 
			get { return GetColumnValue<int>(Columns.LicenseID); }
			set {
				SetColumnValue(Columns.LicenseID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseID));
			}
		}
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set {
				SetColumnValue(Columns.RequirementID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementID));
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
		public int? AccountID { 
			get { return GetColumnValue<int?>(Columns.AccountID); }
			set {
				SetColumnValue(Columns.AccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountID));
			}
		}
		[DataMember]
		public DateTime? SubmissionDate { 
			get { return GetColumnValue<DateTime?>(Columns.SubmissionDate); }
			set {
				SetColumnValue(Columns.SubmissionDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmissionDate));
			}
		}
		[DataMember]
		public DateTime? IssueDate { 
			get { return GetColumnValue<DateTime?>(Columns.IssueDate); }
			set {
				SetColumnValue(Columns.IssueDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IssueDate));
			}
		}
		[DataMember]
		public DateTime? ExpirationDate { 
			get { return GetColumnValue<DateTime?>(Columns.ExpirationDate); }
			set {
				SetColumnValue(Columns.ExpirationDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ExpirationDate));
			}
		}
		[DataMember]
		public string LicenseNumber { 
			get { return GetColumnValue<string>(Columns.LicenseNumber); }
			set {
				SetColumnValue(Columns.LicenseNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseNumber));
			}
		}
		[DataMember]
		public bool RequirementsAreMet { 
			get { return GetColumnValue<bool>(Columns.RequirementsAreMet); }
			set {
				SetColumnValue(Columns.RequirementsAreMet, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementsAreMet));
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

		private LM_Requirement _Requirement;
		//Relationship: FK_LM_Permits_LM_Licenses
		public LM_Requirement Requirement
		{
			get
			{
				if(_Requirement == null) {
					_Requirement = LM_Requirement.FetchByID(this.RequirementID);
				}
				return _Requirement;
			}
			set
			{
				SetColumnValue("RequirementID", value.RequirementID);
				_Requirement = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LicenseID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LicenseIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn SubmissionDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IssueDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ExpirationDateColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LicenseNumberColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn RequirementsAreMetColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LicenseID = @"LicenseID";
			public static readonly string RequirementID = @"RequirementID";
			public static readonly string GPEmployeeID = @"GPEmployeeID";
			public static readonly string AccountID = @"AccountID";
			public static readonly string SubmissionDate = @"SubmissionDate";
			public static readonly string IssueDate = @"IssueDate";
			public static readonly string ExpirationDate = @"ExpirationDate";
			public static readonly string LicenseNumber = @"LicenseNumber";
			public static readonly string RequirementsAreMet = @"RequirementsAreMet";
			public static readonly string IsActive = @"IsActive";
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
			get { return LicenseID; }
		}
		*/

		#region Foreign Collections

		private LM_LicenseItemCollection _LM_LicenseItemsCol;
		//Relationship: FK_LM_LicenseItems_LM_Licenses
		public LM_LicenseItemCollection LM_LicenseItemsCol
		{
			get
			{
				if(_LM_LicenseItemsCol == null) {
					_LM_LicenseItemsCol = new LM_LicenseItemCollection();
					_LM_LicenseItemsCol.LoadAndCloseReader(LM_LicenseItem.Query()
						.WHERE(LM_LicenseItem.Columns.LicenseID, LicenseID).ExecuteReader());
				}
				return _LM_LicenseItemsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_LicenseStatus class.
	/// </summary>
	[DataContract]
	public partial class LM_LicenseStatusCollection : ActiveList<LM_LicenseStatus, LM_LicenseStatusCollection>
	{
		public static LM_LicenseStatusCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LicenseStatusCollection result = new LM_LicenseStatusCollection();
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
			foreach (LM_LicenseStatus item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_LicenseStatuses table.
	/// </summary>
	[DataContract]
	public partial class LM_LicenseStatus : ActiveRecord<LM_LicenseStatus>, INotifyPropertyChanged
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

		public LM_LicenseStatus()
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
				TableSchema.Table schema = new TableSchema.Table("LM_LicenseStatuses", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLicenseStatusID = new TableSchema.TableColumn(schema);
				colvarLicenseStatusID.ColumnName = "LicenseStatusID";
				colvarLicenseStatusID.DataType = DbType.Int16;
				colvarLicenseStatusID.MaxLength = 0;
				colvarLicenseStatusID.AutoIncrement = false;
				colvarLicenseStatusID.IsNullable = false;
				colvarLicenseStatusID.IsPrimaryKey = true;
				colvarLicenseStatusID.IsForeignKey = false;
				colvarLicenseStatusID.IsReadOnly = false;
				colvarLicenseStatusID.DefaultSetting = @"";
				colvarLicenseStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseStatusID);

				TableSchema.TableColumn colvarLicenseStatus = new TableSchema.TableColumn(schema);
				colvarLicenseStatus.ColumnName = "LicenseStatus";
				colvarLicenseStatus.DataType = DbType.AnsiString;
				colvarLicenseStatus.MaxLength = 100;
				colvarLicenseStatus.AutoIncrement = false;
				colvarLicenseStatus.IsNullable = false;
				colvarLicenseStatus.IsPrimaryKey = false;
				colvarLicenseStatus.IsForeignKey = false;
				colvarLicenseStatus.IsReadOnly = false;
				colvarLicenseStatus.DefaultSetting = @"";
				colvarLicenseStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseStatus);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_LicenseStatuses",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_LicenseStatus LoadFrom(LM_LicenseStatus item)
		{
			LM_LicenseStatus result = new LM_LicenseStatus();
			if (item.LicenseStatusID != default(short)) {
				result.LoadByKey(item.LicenseStatusID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public short LicenseStatusID { 
			get { return GetColumnValue<short>(Columns.LicenseStatusID); }
			set {
				SetColumnValue(Columns.LicenseStatusID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseStatusID));
			}
		}
		[DataMember]
		public string LicenseStatus { 
			get { return GetColumnValue<string>(Columns.LicenseStatus); }
			set {
				SetColumnValue(Columns.LicenseStatus, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LicenseStatus));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return LicenseStatus;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LicenseStatusIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LicenseStatusColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LicenseStatusID = @"LicenseStatusID";
			public static readonly string LicenseStatus = @"LicenseStatus";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LicenseStatusID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LM_Location class.
	/// </summary>
	[DataContract]
	public partial class LM_LocationCollection : ActiveList<LM_Location, LM_LocationCollection>
	{
		public static LM_LocationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LocationCollection result = new LM_LocationCollection();
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
			foreach (LM_Location item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Locations table.
	/// </summary>
	[DataContract]
	public partial class LM_Location : ActiveRecord<LM_Location>, INotifyPropertyChanged
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

		public LM_Location()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Locations", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLocationID = new TableSchema.TableColumn(schema);
				colvarLocationID.ColumnName = "LocationID";
				colvarLocationID.DataType = DbType.Int32;
				colvarLocationID.MaxLength = 0;
				colvarLocationID.AutoIncrement = true;
				colvarLocationID.IsNullable = false;
				colvarLocationID.IsPrimaryKey = true;
				colvarLocationID.IsForeignKey = false;
				colvarLocationID.IsReadOnly = false;
				colvarLocationID.DefaultSetting = @"";
				colvarLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationID);

				TableSchema.TableColumn colvarLocationTypeID = new TableSchema.TableColumn(schema);
				colvarLocationTypeID.ColumnName = "LocationTypeID";
				colvarLocationTypeID.DataType = DbType.Int32;
				colvarLocationTypeID.MaxLength = 0;
				colvarLocationTypeID.AutoIncrement = false;
				colvarLocationTypeID.IsNullable = false;
				colvarLocationTypeID.IsPrimaryKey = false;
				colvarLocationTypeID.IsForeignKey = true;
				colvarLocationTypeID.IsReadOnly = false;
				colvarLocationTypeID.DefaultSetting = @"";
				colvarLocationTypeID.ForeignKeyTableName = "LM_LocationTypes";
				schema.Columns.Add(colvarLocationTypeID);

				TableSchema.TableColumn colvarParentLocationID = new TableSchema.TableColumn(schema);
				colvarParentLocationID.ColumnName = "ParentLocationID";
				colvarParentLocationID.DataType = DbType.Int32;
				colvarParentLocationID.MaxLength = 0;
				colvarParentLocationID.AutoIncrement = false;
				colvarParentLocationID.IsNullable = true;
				colvarParentLocationID.IsPrimaryKey = false;
				colvarParentLocationID.IsForeignKey = true;
				colvarParentLocationID.IsReadOnly = false;
				colvarParentLocationID.DefaultSetting = @"";
				colvarParentLocationID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarParentLocationID);

				TableSchema.TableColumn colvarParentCountryID = new TableSchema.TableColumn(schema);
				colvarParentCountryID.ColumnName = "ParentCountryID";
				colvarParentCountryID.DataType = DbType.Int32;
				colvarParentCountryID.MaxLength = 0;
				colvarParentCountryID.AutoIncrement = false;
				colvarParentCountryID.IsNullable = true;
				colvarParentCountryID.IsPrimaryKey = false;
				colvarParentCountryID.IsForeignKey = true;
				colvarParentCountryID.IsReadOnly = false;
				colvarParentCountryID.DefaultSetting = @"((0))";
				colvarParentCountryID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarParentCountryID);

				TableSchema.TableColumn colvarParentStateID = new TableSchema.TableColumn(schema);
				colvarParentStateID.ColumnName = "ParentStateID";
				colvarParentStateID.DataType = DbType.Int32;
				colvarParentStateID.MaxLength = 0;
				colvarParentStateID.AutoIncrement = false;
				colvarParentStateID.IsNullable = true;
				colvarParentStateID.IsPrimaryKey = false;
				colvarParentStateID.IsForeignKey = true;
				colvarParentStateID.IsReadOnly = false;
				colvarParentStateID.DefaultSetting = @"((0))";
				colvarParentStateID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarParentStateID);

				TableSchema.TableColumn colvarParentCountyID = new TableSchema.TableColumn(schema);
				colvarParentCountyID.ColumnName = "ParentCountyID";
				colvarParentCountyID.DataType = DbType.Int32;
				colvarParentCountyID.MaxLength = 0;
				colvarParentCountyID.AutoIncrement = false;
				colvarParentCountyID.IsNullable = true;
				colvarParentCountyID.IsPrimaryKey = false;
				colvarParentCountyID.IsForeignKey = true;
				colvarParentCountyID.IsReadOnly = false;
				colvarParentCountyID.DefaultSetting = @"((0))";
				colvarParentCountyID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarParentCountyID);

				TableSchema.TableColumn colvarParentCityID = new TableSchema.TableColumn(schema);
				colvarParentCityID.ColumnName = "ParentCityID";
				colvarParentCityID.DataType = DbType.Int32;
				colvarParentCityID.MaxLength = 0;
				colvarParentCityID.AutoIncrement = false;
				colvarParentCityID.IsNullable = true;
				colvarParentCityID.IsPrimaryKey = false;
				colvarParentCityID.IsForeignKey = true;
				colvarParentCityID.IsReadOnly = false;
				colvarParentCityID.DefaultSetting = @"((0))";
				colvarParentCityID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarParentCityID);

				TableSchema.TableColumn colvarLocationName = new TableSchema.TableColumn(schema);
				colvarLocationName.ColumnName = "LocationName";
				colvarLocationName.DataType = DbType.String;
				colvarLocationName.MaxLength = 100;
				colvarLocationName.AutoIncrement = false;
				colvarLocationName.IsNullable = false;
				colvarLocationName.IsPrimaryKey = false;
				colvarLocationName.IsForeignKey = false;
				colvarLocationName.IsReadOnly = false;
				colvarLocationName.DefaultSetting = @"";
				colvarLocationName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationName);

				TableSchema.TableColumn colvarAbbreviation = new TableSchema.TableColumn(schema);
				colvarAbbreviation.ColumnName = "Abbreviation";
				colvarAbbreviation.DataType = DbType.String;
				colvarAbbreviation.MaxLength = 10;
				colvarAbbreviation.AutoIncrement = false;
				colvarAbbreviation.IsNullable = true;
				colvarAbbreviation.IsPrimaryKey = false;
				colvarAbbreviation.IsForeignKey = false;
				colvarAbbreviation.IsReadOnly = false;
				colvarAbbreviation.DefaultSetting = @"";
				colvarAbbreviation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAbbreviation);

				TableSchema.TableColumn colvarCanSolicit = new TableSchema.TableColumn(schema);
				colvarCanSolicit.ColumnName = "CanSolicit";
				colvarCanSolicit.DataType = DbType.Boolean;
				colvarCanSolicit.MaxLength = 0;
				colvarCanSolicit.AutoIncrement = false;
				colvarCanSolicit.IsNullable = false;
				colvarCanSolicit.IsPrimaryKey = false;
				colvarCanSolicit.IsForeignKey = false;
				colvarCanSolicit.IsReadOnly = false;
				colvarCanSolicit.DefaultSetting = @"((1))";
				colvarCanSolicit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCanSolicit);

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
				colvarCreatedByID.DefaultSetting = @"(getdate())";
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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Locations",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Location LoadFrom(LM_Location item)
		{
			LM_Location result = new LM_Location();
			if (item.LocationID != default(int)) {
				result.LoadByKey(item.LocationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LocationID { 
			get { return GetColumnValue<int>(Columns.LocationID); }
			set {
				SetColumnValue(Columns.LocationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationID));
			}
		}
		[DataMember]
		public int LocationTypeID { 
			get { return GetColumnValue<int>(Columns.LocationTypeID); }
			set {
				SetColumnValue(Columns.LocationTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationTypeID));
			}
		}
		[DataMember]
		public int? ParentLocationID { 
			get { return GetColumnValue<int?>(Columns.ParentLocationID); }
			set {
				SetColumnValue(Columns.ParentLocationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentLocationID));
			}
		}
		[DataMember]
		public int? ParentCountryID { 
			get { return GetColumnValue<int?>(Columns.ParentCountryID); }
			set {
				SetColumnValue(Columns.ParentCountryID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentCountryID));
			}
		}
		[DataMember]
		public int? ParentStateID { 
			get { return GetColumnValue<int?>(Columns.ParentStateID); }
			set {
				SetColumnValue(Columns.ParentStateID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentStateID));
			}
		}
		[DataMember]
		public int? ParentCountyID { 
			get { return GetColumnValue<int?>(Columns.ParentCountyID); }
			set {
				SetColumnValue(Columns.ParentCountyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentCountyID));
			}
		}
		[DataMember]
		public int? ParentCityID { 
			get { return GetColumnValue<int?>(Columns.ParentCityID); }
			set {
				SetColumnValue(Columns.ParentCityID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentCityID));
			}
		}
		[DataMember]
		public string LocationName { 
			get { return GetColumnValue<string>(Columns.LocationName); }
			set {
				SetColumnValue(Columns.LocationName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationName));
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
		public bool CanSolicit { 
			get { return GetColumnValue<bool>(Columns.CanSolicit); }
			set {
				SetColumnValue(Columns.CanSolicit, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CanSolicit));
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

		private LM_Location _ParentCity;
		//Relationship: FK_LM_Locations_LM_LocationsParentCity
		public LM_Location ParentCity
		{
			get
			{
				if(_ParentCity == null) {
					_ParentCity = LM_Location.FetchByID(this.ParentCityID);
				}
				return _ParentCity;
			}
			set
			{
				SetColumnValue("ParentCityID", value.LocationID);
				_ParentCity = value;
			}
		}

		private LM_Location _ParentCountry;
		//Relationship: FK_LM_Locations_LM_LocationsParentCountry
		public LM_Location ParentCountry
		{
			get
			{
				if(_ParentCountry == null) {
					_ParentCountry = LM_Location.FetchByID(this.ParentCountryID);
				}
				return _ParentCountry;
			}
			set
			{
				SetColumnValue("ParentCountryID", value.LocationID);
				_ParentCountry = value;
			}
		}

		private LM_Location _ParentCounty;
		//Relationship: FK_LM_Locations_LM_LocationsParentCounty
		public LM_Location ParentCounty
		{
			get
			{
				if(_ParentCounty == null) {
					_ParentCounty = LM_Location.FetchByID(this.ParentCountyID);
				}
				return _ParentCounty;
			}
			set
			{
				SetColumnValue("ParentCountyID", value.LocationID);
				_ParentCounty = value;
			}
		}

		private LM_Location _ParentLocation;
		//Relationship: FK_LM_Locations_LM_LocationsParentLocation
		public LM_Location ParentLocation
		{
			get
			{
				if(_ParentLocation == null) {
					_ParentLocation = LM_Location.FetchByID(this.ParentLocationID);
				}
				return _ParentLocation;
			}
			set
			{
				SetColumnValue("ParentLocationID", value.LocationID);
				_ParentLocation = value;
			}
		}

		private LM_Location _ParentState;
		//Relationship: FK_LM_Locations_LM_LocationsParentState
		public LM_Location ParentState
		{
			get
			{
				if(_ParentState == null) {
					_ParentState = LM_Location.FetchByID(this.ParentStateID);
				}
				return _ParentState;
			}
			set
			{
				SetColumnValue("ParentStateID", value.LocationID);
				_ParentState = value;
			}
		}

		private LM_LocationType _LocationType;
		//Relationship: FK_LM_Locations_LM_LocationTypes
		public LM_LocationType LocationType
		{
			get
			{
				if(_LocationType == null) {
					_LocationType = LM_LocationType.FetchByID(this.LocationTypeID);
				}
				return _LocationType;
			}
			set
			{
				SetColumnValue("LocationTypeID", value.LocationTypeID);
				_LocationType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LocationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LocationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LocationTypeIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ParentLocationIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ParentCountryIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ParentStateIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ParentCountyIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ParentCityIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LocationNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn AbbreviationColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CanSolicitColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[15]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LocationID = @"LocationID";
			public static readonly string LocationTypeID = @"LocationTypeID";
			public static readonly string ParentLocationID = @"ParentLocationID";
			public static readonly string ParentCountryID = @"ParentCountryID";
			public static readonly string ParentStateID = @"ParentStateID";
			public static readonly string ParentCountyID = @"ParentCountyID";
			public static readonly string ParentCityID = @"ParentCityID";
			public static readonly string LocationName = @"LocationName";
			public static readonly string Abbreviation = @"Abbreviation";
			public static readonly string CanSolicit = @"CanSolicit";
			public static readonly string IsActive = @"IsActive";
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
			get { return LocationID; }
		}
		*/

		#region Foreign Collections

		private LM_AgencyCollection _LM_AgenciesCol;
		//Relationship: FK_LM_Agencies_LM_Locations
		public LM_AgencyCollection LM_AgenciesCol
		{
			get
			{
				if(_LM_AgenciesCol == null) {
					_LM_AgenciesCol = new LM_AgencyCollection();
					_LM_AgenciesCol.LoadAndCloseReader(LM_Agency.Query()
						.WHERE(LM_Agency.Columns.LocationID, LocationID).ExecuteReader());
				}
				return _LM_AgenciesCol;
			}
		}

		private LM_RequirementCollection _LM_RequirementsCol;
		//Relationship: FK_LM_Licenses_LM_Locations
		public LM_RequirementCollection LM_RequirementsCol
		{
			get
			{
				if(_LM_RequirementsCol == null) {
					_LM_RequirementsCol = new LM_RequirementCollection();
					_LM_RequirementsCol.LoadAndCloseReader(LM_Requirement.Query()
						.WHERE(LM_Requirement.Columns.LocationID, LocationID).ExecuteReader());
				}
				return _LM_RequirementsCol;
			}
		}

		private LM_LocationCollection _ChildLM_LocationsCol;
		//Relationship: FK_LM_Locations_LM_LocationsParentCity
		public LM_LocationCollection ChildLM_LocationsCol
		{
			get
			{
				if(_ChildLM_LocationsCol == null) {
					_ChildLM_LocationsCol = new LM_LocationCollection();
					_ChildLM_LocationsCol.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.ParentCityID, LocationID).ExecuteReader());
				}
				return _ChildLM_LocationsCol;
			}
		}

		private LM_LocationCollection _ChildLM_Locations02Col;
		//Relationship: FK_LM_Locations_LM_LocationsParentCountry
		public LM_LocationCollection ChildLM_Locations02Col
		{
			get
			{
				if(_ChildLM_Locations02Col == null) {
					_ChildLM_Locations02Col = new LM_LocationCollection();
					_ChildLM_Locations02Col.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.ParentCountryID, LocationID).ExecuteReader());
				}
				return _ChildLM_Locations02Col;
			}
		}

		private LM_LocationCollection _ChildLM_Locations03Col;
		//Relationship: FK_LM_Locations_LM_LocationsParentCounty
		public LM_LocationCollection ChildLM_Locations03Col
		{
			get
			{
				if(_ChildLM_Locations03Col == null) {
					_ChildLM_Locations03Col = new LM_LocationCollection();
					_ChildLM_Locations03Col.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.ParentCountyID, LocationID).ExecuteReader());
				}
				return _ChildLM_Locations03Col;
			}
		}

		private LM_LocationCollection _ChildLM_Locations04Col;
		//Relationship: FK_LM_Locations_LM_LocationsParentLocation
		public LM_LocationCollection ChildLM_Locations04Col
		{
			get
			{
				if(_ChildLM_Locations04Col == null) {
					_ChildLM_Locations04Col = new LM_LocationCollection();
					_ChildLM_Locations04Col.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.ParentLocationID, LocationID).ExecuteReader());
				}
				return _ChildLM_Locations04Col;
			}
		}

		private LM_LocationCollection _ChildLM_Locations05Col;
		//Relationship: FK_LM_Locations_LM_LocationsParentState
		public LM_LocationCollection ChildLM_Locations05Col
		{
			get
			{
				if(_ChildLM_Locations05Col == null) {
					_ChildLM_Locations05Col = new LM_LocationCollection();
					_ChildLM_Locations05Col.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.ParentStateID, LocationID).ExecuteReader());
				}
				return _ChildLM_Locations05Col;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_LocationType class.
	/// </summary>
	[DataContract]
	public partial class LM_LocationTypeCollection : ActiveList<LM_LocationType, LM_LocationTypeCollection>
	{
		public static LM_LocationTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LocationTypeCollection result = new LM_LocationTypeCollection();
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
			foreach (LM_LocationType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_LocationTypes table.
	/// </summary>
	[DataContract]
	public partial class LM_LocationType : ActiveRecord<LM_LocationType>, INotifyPropertyChanged
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

		public LM_LocationType()
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
				TableSchema.Table schema = new TableSchema.Table("LM_LocationTypes", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLocationTypeID = new TableSchema.TableColumn(schema);
				colvarLocationTypeID.ColumnName = "LocationTypeID";
				colvarLocationTypeID.DataType = DbType.Int32;
				colvarLocationTypeID.MaxLength = 0;
				colvarLocationTypeID.AutoIncrement = true;
				colvarLocationTypeID.IsNullable = false;
				colvarLocationTypeID.IsPrimaryKey = true;
				colvarLocationTypeID.IsForeignKey = false;
				colvarLocationTypeID.IsReadOnly = false;
				colvarLocationTypeID.DefaultSetting = @"";
				colvarLocationTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationTypeID);

				TableSchema.TableColumn colvarLocationTypeName = new TableSchema.TableColumn(schema);
				colvarLocationTypeName.ColumnName = "LocationTypeName";
				colvarLocationTypeName.DataType = DbType.String;
				colvarLocationTypeName.MaxLength = 50;
				colvarLocationTypeName.AutoIncrement = false;
				colvarLocationTypeName.IsNullable = false;
				colvarLocationTypeName.IsPrimaryKey = false;
				colvarLocationTypeName.IsForeignKey = false;
				colvarLocationTypeName.IsReadOnly = false;
				colvarLocationTypeName.DefaultSetting = @"";
				colvarLocationTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationTypeName);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_LocationTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_LocationType LoadFrom(LM_LocationType item)
		{
			LM_LocationType result = new LM_LocationType();
			if (item.LocationTypeID != default(int)) {
				result.LoadByKey(item.LocationTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LocationTypeID { 
			get { return GetColumnValue<int>(Columns.LocationTypeID); }
			set {
				SetColumnValue(Columns.LocationTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationTypeID));
			}
		}
		[DataMember]
		public string LocationTypeName { 
			get { return GetColumnValue<string>(Columns.LocationTypeName); }
			set {
				SetColumnValue(Columns.LocationTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationTypeName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return LocationTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LocationTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LocationTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LocationTypeID = @"LocationTypeID";
			public static readonly string LocationTypeName = @"LocationTypeName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LocationTypeID; }
		}
		*/

		#region Foreign Collections

		private LM_LocationCollection _LM_LocationsCol;
		//Relationship: FK_LM_Locations_LM_LocationTypes
		public LM_LocationCollection LM_LocationsCol
		{
			get
			{
				if(_LM_LocationsCol == null) {
					_LM_LocationsCol = new LM_LocationCollection();
					_LM_LocationsCol.LoadAndCloseReader(LM_Location.Query()
						.WHERE(LM_Location.Columns.LocationTypeID, LocationTypeID).ExecuteReader());
				}
				return _LM_LocationsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_Lock class.
	/// </summary>
	[DataContract]
	public partial class LM_LockCollection : ActiveList<LM_Lock, LM_LockCollection>
	{
		public static LM_LockCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_LockCollection result = new LM_LockCollection();
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
			foreach (LM_Lock item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Locks table.
	/// </summary>
	[DataContract]
	public partial class LM_Lock : ActiveRecord<LM_Lock>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum LockEnum : int
		{
			[EnumMember()] No_Lock = 1,
			[EnumMember()] Soft_Lock = 2,
			[EnumMember()] Hard_Lock = 3,
		}
		
		//[DataMember]
		//public LockEnum LockCode
		//{
		//	get { return (LockEnum)LockID; }
		//	set { LockID = (int)value; }
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

		public LM_Lock()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Locks", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLockID = new TableSchema.TableColumn(schema);
				colvarLockID.ColumnName = "LockID";
				colvarLockID.DataType = DbType.Int32;
				colvarLockID.MaxLength = 0;
				colvarLockID.AutoIncrement = true;
				colvarLockID.IsNullable = false;
				colvarLockID.IsPrimaryKey = true;
				colvarLockID.IsForeignKey = false;
				colvarLockID.IsReadOnly = false;
				colvarLockID.DefaultSetting = @"";
				colvarLockID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLockID);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 50;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Locks",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Lock LoadFrom(LM_Lock item)
		{
			LM_Lock result = new LM_Lock();
			if (item.LockID != default(int)) {
				result.LoadByKey(item.LockID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int LockID { 
			get { return GetColumnValue<int>(Columns.LockID); }
			set {
				SetColumnValue(Columns.LockID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LockID));
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
			return Description;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LockIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LockID = @"LockID";
			public static readonly string Description = @"Description";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LockID; }
		}
		*/

		#region Foreign Collections

		private LM_RequirementCollection _LM_RequirementsCol;
		//Relationship: FK_LM_Requirements_LM_Locks
		public LM_RequirementCollection LM_RequirementsCol
		{
			get
			{
				if(_LM_RequirementsCol == null) {
					_LM_RequirementsCol = new LM_RequirementCollection();
					_LM_RequirementsCol.LoadAndCloseReader(LM_Requirement.Query()
						.WHERE(LM_Requirement.Columns.LockID, LockID).ExecuteReader());
				}
				return _LM_RequirementsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_Note class.
	/// </summary>
	[DataContract]
	public partial class LM_NoteCollection : ActiveList<LM_Note, LM_NoteCollection>
	{
		public static LM_NoteCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_NoteCollection result = new LM_NoteCollection();
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
			foreach (LM_Note item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Notes table.
	/// </summary>
	[DataContract]
	public partial class LM_Note : ActiveRecord<LM_Note>, INotifyPropertyChanged
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

		public LM_Note()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Notes", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarNoteID = new TableSchema.TableColumn(schema);
				colvarNoteID.ColumnName = "NoteID";
				colvarNoteID.DataType = DbType.Int32;
				colvarNoteID.MaxLength = 0;
				colvarNoteID.AutoIncrement = true;
				colvarNoteID.IsNullable = false;
				colvarNoteID.IsPrimaryKey = true;
				colvarNoteID.IsForeignKey = false;
				colvarNoteID.IsReadOnly = false;
				colvarNoteID.DefaultSetting = @"";
				colvarNoteID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoteID);

				TableSchema.TableColumn colvarNoteTypeID = new TableSchema.TableColumn(schema);
				colvarNoteTypeID.ColumnName = "NoteTypeID";
				colvarNoteTypeID.DataType = DbType.Int32;
				colvarNoteTypeID.MaxLength = 0;
				colvarNoteTypeID.AutoIncrement = false;
				colvarNoteTypeID.IsNullable = false;
				colvarNoteTypeID.IsPrimaryKey = false;
				colvarNoteTypeID.IsForeignKey = true;
				colvarNoteTypeID.IsReadOnly = false;
				colvarNoteTypeID.DefaultSetting = @"";
				colvarNoteTypeID.ForeignKeyTableName = "LM_NoteTypes";
				schema.Columns.Add(colvarNoteTypeID);

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

				TableSchema.TableColumn colvarNote = new TableSchema.TableColumn(schema);
				colvarNote.ColumnName = "Note";
				colvarNote.DataType = DbType.String;
				colvarNote.MaxLength = -1;
				colvarNote.AutoIncrement = false;
				colvarNote.IsNullable = false;
				colvarNote.IsPrimaryKey = false;
				colvarNote.IsForeignKey = false;
				colvarNote.IsReadOnly = false;
				colvarNote.DefaultSetting = @"";
				colvarNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNote);

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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Notes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Note LoadFrom(LM_Note item)
		{
			LM_Note result = new LM_Note();
			if (item.NoteID != default(int)) {
				result.LoadByKey(item.NoteID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int NoteID { 
			get { return GetColumnValue<int>(Columns.NoteID); }
			set {
				SetColumnValue(Columns.NoteID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NoteID));
			}
		}
		[DataMember]
		public int NoteTypeID { 
			get { return GetColumnValue<int>(Columns.NoteTypeID); }
			set {
				SetColumnValue(Columns.NoteTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NoteTypeID));
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
		public string Note { 
			get { return GetColumnValue<string>(Columns.Note); }
			set {
				SetColumnValue(Columns.Note, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Note));
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

		private LM_NoteType _NoteType;
		//Relationship: FK_LM_Notes_LM_NoteTypes
		public LM_NoteType NoteType
		{
			get
			{
				if(_NoteType == null) {
					_NoteType = LM_NoteType.FetchByID(this.NoteTypeID);
				}
				return _NoteType;
			}
			set
			{
				SetColumnValue("NoteTypeID", value.NoteTypeID);
				_NoteType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return NoteID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn NoteIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NoteTypeIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ForeignKeyIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn NoteColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string NoteID = @"NoteID";
			public static readonly string NoteTypeID = @"NoteTypeID";
			public static readonly string ForeignKeyID = @"ForeignKeyID";
			public static readonly string Note = @"Note";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return NoteID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LM_NoteType class.
	/// </summary>
	[DataContract]
	public partial class LM_NoteTypeCollection : ActiveList<LM_NoteType, LM_NoteTypeCollection>
	{
		public static LM_NoteTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_NoteTypeCollection result = new LM_NoteTypeCollection();
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
			foreach (LM_NoteType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_NoteTypes table.
	/// </summary>
	[DataContract]
	public partial class LM_NoteType : ActiveRecord<LM_NoteType>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum NoteTypeEnum : int
		{
			[EnumMember()] Agency = 1,
			[EnumMember()] Requirement = 2,
			[EnumMember()] License = 3,
			[EnumMember()] User = 4,
		}
		
		//[DataMember]
		//public NoteTypeEnum NoteTypeCode
		//{
		//	get { return (NoteTypeEnum)NoteTypeID; }
		//	set { NoteTypeID = (int)value; }
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

		public LM_NoteType()
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
				TableSchema.Table schema = new TableSchema.Table("LM_NoteTypes", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarNoteTypeID = new TableSchema.TableColumn(schema);
				colvarNoteTypeID.ColumnName = "NoteTypeID";
				colvarNoteTypeID.DataType = DbType.Int32;
				colvarNoteTypeID.MaxLength = 0;
				colvarNoteTypeID.AutoIncrement = true;
				colvarNoteTypeID.IsNullable = false;
				colvarNoteTypeID.IsPrimaryKey = true;
				colvarNoteTypeID.IsForeignKey = false;
				colvarNoteTypeID.IsReadOnly = false;
				colvarNoteTypeID.DefaultSetting = @"";
				colvarNoteTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoteTypeID);

				TableSchema.TableColumn colvarNoteTypeName = new TableSchema.TableColumn(schema);
				colvarNoteTypeName.ColumnName = "NoteTypeName";
				colvarNoteTypeName.DataType = DbType.String;
				colvarNoteTypeName.MaxLength = 50;
				colvarNoteTypeName.AutoIncrement = false;
				colvarNoteTypeName.IsNullable = false;
				colvarNoteTypeName.IsPrimaryKey = false;
				colvarNoteTypeName.IsForeignKey = false;
				colvarNoteTypeName.IsReadOnly = false;
				colvarNoteTypeName.DefaultSetting = @"";
				colvarNoteTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoteTypeName);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_NoteTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_NoteType LoadFrom(LM_NoteType item)
		{
			LM_NoteType result = new LM_NoteType();
			if (item.NoteTypeID != default(int)) {
				result.LoadByKey(item.NoteTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int NoteTypeID { 
			get { return GetColumnValue<int>(Columns.NoteTypeID); }
			set {
				SetColumnValue(Columns.NoteTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NoteTypeID));
			}
		}
		[DataMember]
		public string NoteTypeName { 
			get { return GetColumnValue<string>(Columns.NoteTypeName); }
			set {
				SetColumnValue(Columns.NoteTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NoteTypeName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return NoteTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn NoteTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NoteTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string NoteTypeID = @"NoteTypeID";
			public static readonly string NoteTypeName = @"NoteTypeName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return NoteTypeID; }
		}
		*/

		#region Foreign Collections

		private LM_NoteCollection _LM_NotesCol;
		//Relationship: FK_LM_Notes_LM_NoteTypes
		public LM_NoteCollection LM_NotesCol
		{
			get
			{
				if(_LM_NotesCol == null) {
					_LM_NotesCol = new LM_NoteCollection();
					_LM_NotesCol.LoadAndCloseReader(LM_Note.Query()
						.WHERE(LM_Note.Columns.NoteTypeID, NoteTypeID).ExecuteReader());
				}
				return _LM_NotesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_RequirementItem class.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementItemCollection : ActiveList<LM_RequirementItem, LM_RequirementItemCollection>
	{
		public static LM_RequirementItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_RequirementItemCollection result = new LM_RequirementItemCollection();
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
			foreach (LM_RequirementItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_RequirementItems table.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementItem : ActiveRecord<LM_RequirementItem>, INotifyPropertyChanged
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

		public LM_RequirementItem()
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
				TableSchema.Table schema = new TableSchema.Table("LM_RequirementItems", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequirementItemID = new TableSchema.TableColumn(schema);
				colvarRequirementItemID.ColumnName = "RequirementItemID";
				colvarRequirementItemID.DataType = DbType.Int32;
				colvarRequirementItemID.MaxLength = 0;
				colvarRequirementItemID.AutoIncrement = true;
				colvarRequirementItemID.IsNullable = false;
				colvarRequirementItemID.IsPrimaryKey = true;
				colvarRequirementItemID.IsForeignKey = false;
				colvarRequirementItemID.IsReadOnly = false;
				colvarRequirementItemID.DefaultSetting = @"";
				colvarRequirementItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementItemID);

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = false;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = false;
				colvarRequirementID.IsForeignKey = true;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "LM_Requirements";
				schema.Columns.Add(colvarRequirementID);

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

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = -1;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

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

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_RequirementItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_RequirementItem LoadFrom(LM_RequirementItem item)
		{
			LM_RequirementItem result = new LM_RequirementItem();
			if (item.RequirementItemID != default(int)) {
				result.LoadByKey(item.RequirementItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int RequirementItemID { 
			get { return GetColumnValue<int>(Columns.RequirementItemID); }
			set {
				SetColumnValue(Columns.RequirementItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementItemID));
			}
		}
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set {
				SetColumnValue(Columns.RequirementID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementID));
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

		#endregion //Properties

		#region ForeignKey Properties

		private LM_Requirement _Requirement;
		//Relationship: FK_LM_Requirements_LM_Licenses
		public LM_Requirement Requirement
		{
			get
			{
				if(_Requirement == null) {
					_Requirement = LM_Requirement.FetchByID(this.RequirementID);
				}
				return _Requirement;
			}
			set
			{
				SetColumnValue("RequirementID", value.RequirementID);
				_Requirement = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RequirementItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequirementItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequirementItemID = @"RequirementItemID";
			public static readonly string RequirementID = @"RequirementID";
			public static readonly string Name = @"Name";
			public static readonly string Description = @"Description";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequirementItemID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LM_Requirement class.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementCollection : ActiveList<LM_Requirement, LM_RequirementCollection>
	{
		public static LM_RequirementCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_RequirementCollection result = new LM_RequirementCollection();
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
			foreach (LM_Requirement item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_Requirements table.
	/// </summary>
	[DataContract]
	public partial class LM_Requirement : ActiveRecord<LM_Requirement>, INotifyPropertyChanged
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

		public LM_Requirement()
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
				TableSchema.Table schema = new TableSchema.Table("LM_Requirements", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = true;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = true;
				colvarRequirementID.IsForeignKey = false;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementID);

				TableSchema.TableColumn colvarLocationID = new TableSchema.TableColumn(schema);
				colvarLocationID.ColumnName = "LocationID";
				colvarLocationID.DataType = DbType.Int32;
				colvarLocationID.MaxLength = 0;
				colvarLocationID.AutoIncrement = false;
				colvarLocationID.IsNullable = false;
				colvarLocationID.IsPrimaryKey = false;
				colvarLocationID.IsForeignKey = true;
				colvarLocationID.IsReadOnly = false;
				colvarLocationID.DefaultSetting = @"";
				colvarLocationID.ForeignKeyTableName = "LM_Locations";
				schema.Columns.Add(colvarLocationID);

				TableSchema.TableColumn colvarRequirementTypeID = new TableSchema.TableColumn(schema);
				colvarRequirementTypeID.ColumnName = "RequirementTypeID";
				colvarRequirementTypeID.DataType = DbType.Int32;
				colvarRequirementTypeID.MaxLength = 0;
				colvarRequirementTypeID.AutoIncrement = false;
				colvarRequirementTypeID.IsNullable = false;
				colvarRequirementTypeID.IsPrimaryKey = false;
				colvarRequirementTypeID.IsForeignKey = true;
				colvarRequirementTypeID.IsReadOnly = false;
				colvarRequirementTypeID.DefaultSetting = @"";
				colvarRequirementTypeID.ForeignKeyTableName = "LM_RequirementTypes";
				schema.Columns.Add(colvarRequirementTypeID);

				TableSchema.TableColumn colvarAgencyID = new TableSchema.TableColumn(schema);
				colvarAgencyID.ColumnName = "AgencyID";
				colvarAgencyID.DataType = DbType.Int32;
				colvarAgencyID.MaxLength = 0;
				colvarAgencyID.AutoIncrement = false;
				colvarAgencyID.IsNullable = true;
				colvarAgencyID.IsPrimaryKey = false;
				colvarAgencyID.IsForeignKey = true;
				colvarAgencyID.IsReadOnly = false;
				colvarAgencyID.DefaultSetting = @"";
				colvarAgencyID.ForeignKeyTableName = "LM_Agencies";
				schema.Columns.Add(colvarAgencyID);

				TableSchema.TableColumn colvarLockID = new TableSchema.TableColumn(schema);
				colvarLockID.ColumnName = "LockID";
				colvarLockID.DataType = DbType.Int32;
				colvarLockID.MaxLength = 0;
				colvarLockID.AutoIncrement = false;
				colvarLockID.IsNullable = false;
				colvarLockID.IsPrimaryKey = false;
				colvarLockID.IsForeignKey = true;
				colvarLockID.IsReadOnly = false;
				colvarLockID.DefaultSetting = @"";
				colvarLockID.ForeignKeyTableName = "LM_Locks";
				schema.Columns.Add(colvarLockID);

				TableSchema.TableColumn colvarTemplateID = new TableSchema.TableColumn(schema);
				colvarTemplateID.ColumnName = "TemplateID";
				colvarTemplateID.DataType = DbType.Int32;
				colvarTemplateID.MaxLength = 0;
				colvarTemplateID.AutoIncrement = false;
				colvarTemplateID.IsNullable = true;
				colvarTemplateID.IsPrimaryKey = false;
				colvarTemplateID.IsForeignKey = false;
				colvarTemplateID.IsReadOnly = false;
				colvarTemplateID.DefaultSetting = @"";
				colvarTemplateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTemplateID);

				TableSchema.TableColumn colvarRequirementName = new TableSchema.TableColumn(schema);
				colvarRequirementName.ColumnName = "RequirementName";
				colvarRequirementName.DataType = DbType.String;
				colvarRequirementName.MaxLength = 100;
				colvarRequirementName.AutoIncrement = false;
				colvarRequirementName.IsNullable = false;
				colvarRequirementName.IsPrimaryKey = false;
				colvarRequirementName.IsForeignKey = false;
				colvarRequirementName.IsReadOnly = false;
				colvarRequirementName.DefaultSetting = @"";
				colvarRequirementName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementName);

				TableSchema.TableColumn colvarApplicationDescription = new TableSchema.TableColumn(schema);
				colvarApplicationDescription.ColumnName = "ApplicationDescription";
				colvarApplicationDescription.DataType = DbType.String;
				colvarApplicationDescription.MaxLength = -1;
				colvarApplicationDescription.AutoIncrement = false;
				colvarApplicationDescription.IsNullable = true;
				colvarApplicationDescription.IsPrimaryKey = false;
				colvarApplicationDescription.IsForeignKey = false;
				colvarApplicationDescription.IsReadOnly = false;
				colvarApplicationDescription.DefaultSetting = @"";
				colvarApplicationDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarApplicationDescription);

				TableSchema.TableColumn colvarCallCenterMessage = new TableSchema.TableColumn(schema);
				colvarCallCenterMessage.ColumnName = "CallCenterMessage";
				colvarCallCenterMessage.DataType = DbType.String;
				colvarCallCenterMessage.MaxLength = -1;
				colvarCallCenterMessage.AutoIncrement = false;
				colvarCallCenterMessage.IsNullable = true;
				colvarCallCenterMessage.IsPrimaryKey = false;
				colvarCallCenterMessage.IsForeignKey = false;
				colvarCallCenterMessage.IsReadOnly = false;
				colvarCallCenterMessage.DefaultSetting = @"";
				colvarCallCenterMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCallCenterMessage);

				TableSchema.TableColumn colvarRequiredForFunding = new TableSchema.TableColumn(schema);
				colvarRequiredForFunding.ColumnName = "RequiredForFunding";
				colvarRequiredForFunding.DataType = DbType.Boolean;
				colvarRequiredForFunding.MaxLength = 0;
				colvarRequiredForFunding.AutoIncrement = false;
				colvarRequiredForFunding.IsNullable = true;
				colvarRequiredForFunding.IsPrimaryKey = false;
				colvarRequiredForFunding.IsForeignKey = false;
				colvarRequiredForFunding.IsReadOnly = false;
				colvarRequiredForFunding.DefaultSetting = @"";
				colvarRequiredForFunding.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequiredForFunding);

				TableSchema.TableColumn colvarFee = new TableSchema.TableColumn(schema);
				colvarFee.ColumnName = "Fee";
				colvarFee.DataType = DbType.Currency;
				colvarFee.MaxLength = 0;
				colvarFee.AutoIncrement = false;
				colvarFee.IsNullable = true;
				colvarFee.IsPrimaryKey = false;
				colvarFee.IsForeignKey = false;
				colvarFee.IsReadOnly = false;
				colvarFee.DefaultSetting = @"";
				colvarFee.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFee);

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
				colvarCreatedByDate.DefaultSetting = @"(getdate())";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

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

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_Requirements",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_Requirement LoadFrom(LM_Requirement item)
		{
			LM_Requirement result = new LM_Requirement();
			if (item.RequirementID != default(int)) {
				result.LoadByKey(item.RequirementID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set {
				SetColumnValue(Columns.RequirementID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementID));
			}
		}
		[DataMember]
		public int LocationID { 
			get { return GetColumnValue<int>(Columns.LocationID); }
			set {
				SetColumnValue(Columns.LocationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocationID));
			}
		}
		[DataMember]
		public int RequirementTypeID { 
			get { return GetColumnValue<int>(Columns.RequirementTypeID); }
			set {
				SetColumnValue(Columns.RequirementTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementTypeID));
			}
		}
		[DataMember]
		public int? AgencyID { 
			get { return GetColumnValue<int?>(Columns.AgencyID); }
			set {
				SetColumnValue(Columns.AgencyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AgencyID));
			}
		}
		[DataMember]
		public int LockID { 
			get { return GetColumnValue<int>(Columns.LockID); }
			set {
				SetColumnValue(Columns.LockID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LockID));
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
		public string RequirementName { 
			get { return GetColumnValue<string>(Columns.RequirementName); }
			set {
				SetColumnValue(Columns.RequirementName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementName));
			}
		}
		[DataMember]
		public string ApplicationDescription { 
			get { return GetColumnValue<string>(Columns.ApplicationDescription); }
			set {
				SetColumnValue(Columns.ApplicationDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ApplicationDescription));
			}
		}
		[DataMember]
		public string CallCenterMessage { 
			get { return GetColumnValue<string>(Columns.CallCenterMessage); }
			set {
				SetColumnValue(Columns.CallCenterMessage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CallCenterMessage));
			}
		}
		[DataMember]
		public bool? RequiredForFunding { 
			get { return GetColumnValue<bool?>(Columns.RequiredForFunding); }
			set {
				SetColumnValue(Columns.RequiredForFunding, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequiredForFunding));
			}
		}
		[DataMember]
		public decimal? Fee { 
			get { return GetColumnValue<decimal?>(Columns.Fee); }
			set {
				SetColumnValue(Columns.Fee, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Fee));
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

		#endregion //Properties

		#region ForeignKey Properties

		private LM_Agency _Agency;
		//Relationship: FK_LM_Licenses_LM_Agencies
		public LM_Agency Agency
		{
			get
			{
				if(_Agency == null) {
					_Agency = LM_Agency.FetchByID(this.AgencyID);
				}
				return _Agency;
			}
			set
			{
				SetColumnValue("AgencyID", value.AgencyID);
				_Agency = value;
			}
		}

		private LM_RequirementType _RequirementType;
		//Relationship: FK_LM_Requirements_LM_RequirementTypes
		public LM_RequirementType RequirementType
		{
			get
			{
				if(_RequirementType == null) {
					_RequirementType = LM_RequirementType.FetchByID(this.RequirementTypeID);
				}
				return _RequirementType;
			}
			set
			{
				SetColumnValue("RequirementTypeID", value.RequirementTypeID);
				_RequirementType = value;
			}
		}

		private LM_Location _Location;
		//Relationship: FK_LM_Licenses_LM_Locations
		public LM_Location Location
		{
			get
			{
				if(_Location == null) {
					_Location = LM_Location.FetchByID(this.LocationID);
				}
				return _Location;
			}
			set
			{
				SetColumnValue("LocationID", value.LocationID);
				_Location = value;
			}
		}

		private LM_Lock _Lock;
		//Relationship: FK_LM_Requirements_LM_Locks
		public LM_Lock Lock
		{
			get
			{
				if(_Lock == null) {
					_Lock = LM_Lock.FetchByID(this.LockID);
				}
				return _Lock;
			}
			set
			{
				SetColumnValue("LockID", value.LockID);
				_Lock = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RequirementID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RequirementTypeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AgencyIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LockIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RequirementNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ApplicationDescriptionColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CallCenterMessageColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn RequiredForFundingColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn FeeColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[16]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequirementID = @"RequirementID";
			public static readonly string LocationID = @"LocationID";
			public static readonly string RequirementTypeID = @"RequirementTypeID";
			public static readonly string AgencyID = @"AgencyID";
			public static readonly string LockID = @"LockID";
			public static readonly string TemplateID = @"TemplateID";
			public static readonly string RequirementName = @"RequirementName";
			public static readonly string ApplicationDescription = @"ApplicationDescription";
			public static readonly string CallCenterMessage = @"CallCenterMessage";
			public static readonly string RequiredForFunding = @"RequiredForFunding";
			public static readonly string Fee = @"Fee";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedByID = @"CreatedByID";
			public static readonly string CreatedByDate = @"CreatedByDate";
			public static readonly string ModifiedByID = @"ModifiedByID";
			public static readonly string ModifiedByDate = @"ModifiedByDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequirementID; }
		}
		*/

		#region Foreign Collections

		private LM_LicenseCollection _LM_LicensesCol;
		//Relationship: FK_LM_Permits_LM_Licenses
		public LM_LicenseCollection LM_LicensesCol
		{
			get
			{
				if(_LM_LicensesCol == null) {
					_LM_LicensesCol = new LM_LicenseCollection();
					_LM_LicensesCol.LoadAndCloseReader(LM_License.Query()
						.WHERE(LM_License.Columns.RequirementID, RequirementID).ExecuteReader());
				}
				return _LM_LicensesCol;
			}
		}

		private LM_RequirementItemCollection _LM_RequirementItemsCol;
		//Relationship: FK_LM_Requirements_LM_Licenses
		public LM_RequirementItemCollection LM_RequirementItemsCol
		{
			get
			{
				if(_LM_RequirementItemsCol == null) {
					_LM_RequirementItemsCol = new LM_RequirementItemCollection();
					_LM_RequirementItemsCol.LoadAndCloseReader(LM_RequirementItem.Query()
						.WHERE(LM_RequirementItem.Columns.RequirementID, RequirementID).ExecuteReader());
				}
				return _LM_RequirementItemsCol;
			}
		}

		private SAE_LicenseIndexCollection _SAE_LicenseIndicesCol;
		//Relationship: FK_SAE_LicenseIndex_SAE_LicenseIndex
		public SAE_LicenseIndexCollection SAE_LicenseIndicesCol
		{
			get
			{
				if(_SAE_LicenseIndicesCol == null) {
					_SAE_LicenseIndicesCol = new SAE_LicenseIndexCollection();
					_SAE_LicenseIndicesCol.LoadAndCloseReader(SAE_LicenseIndex.Query()
						.WHERE(SAE_LicenseIndex.Columns.RequirementID, RequirementID).ExecuteReader());
				}
				return _SAE_LicenseIndicesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LM_RequirementType class.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementTypeCollection : ActiveList<LM_RequirementType, LM_RequirementTypeCollection>
	{
		public static LM_RequirementTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_RequirementTypeCollection result = new LM_RequirementTypeCollection();
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
			foreach (LM_RequirementType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the LM_RequirementTypes table.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementType : ActiveRecord<LM_RequirementType>, INotifyPropertyChanged
	{
		#region Enum
		
		[DataContract]
		public enum RequirementTypeEnum : int
		{
			[EnumMember()] Company = 1,
			[EnumMember()] Sales_Rep = 2,
			[EnumMember()] Tech = 3,
			[EnumMember()] Customer_Permit = 4,
		}
		
		//[DataMember]
		//public RequirementTypeEnum RequirementTypeCode
		//{
		//	get { return (RequirementTypeEnum)RequirementTypeID; }
		//	set { RequirementTypeID = (int)value; }
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

		public LM_RequirementType()
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
				TableSchema.Table schema = new TableSchema.Table("LM_RequirementTypes", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequirementTypeID = new TableSchema.TableColumn(schema);
				colvarRequirementTypeID.ColumnName = "RequirementTypeID";
				colvarRequirementTypeID.DataType = DbType.Int32;
				colvarRequirementTypeID.MaxLength = 0;
				colvarRequirementTypeID.AutoIncrement = true;
				colvarRequirementTypeID.IsNullable = false;
				colvarRequirementTypeID.IsPrimaryKey = true;
				colvarRequirementTypeID.IsForeignKey = false;
				colvarRequirementTypeID.IsReadOnly = false;
				colvarRequirementTypeID.DefaultSetting = @"";
				colvarRequirementTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementTypeID);

				TableSchema.TableColumn colvarRequirementTypeName = new TableSchema.TableColumn(schema);
				colvarRequirementTypeName.ColumnName = "RequirementTypeName";
				colvarRequirementTypeName.DataType = DbType.String;
				colvarRequirementTypeName.MaxLength = 50;
				colvarRequirementTypeName.AutoIncrement = false;
				colvarRequirementTypeName.IsNullable = false;
				colvarRequirementTypeName.IsPrimaryKey = false;
				colvarRequirementTypeName.IsForeignKey = false;
				colvarRequirementTypeName.IsReadOnly = false;
				colvarRequirementTypeName.DefaultSetting = @"";
				colvarRequirementTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementTypeName);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("LM_RequirementTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LM_RequirementType LoadFrom(LM_RequirementType item)
		{
			LM_RequirementType result = new LM_RequirementType();
			if (item.RequirementTypeID != default(int)) {
				result.LoadByKey(item.RequirementTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int RequirementTypeID { 
			get { return GetColumnValue<int>(Columns.RequirementTypeID); }
			set {
				SetColumnValue(Columns.RequirementTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementTypeID));
			}
		}
		[DataMember]
		public string RequirementTypeName { 
			get { return GetColumnValue<string>(Columns.RequirementTypeName); }
			set {
				SetColumnValue(Columns.RequirementTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementTypeName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return RequirementTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequirementTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequirementTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequirementTypeID = @"RequirementTypeID";
			public static readonly string RequirementTypeName = @"RequirementTypeName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequirementTypeID; }
		}
		*/

		#region Foreign Collections

		private LM_RequirementCollection _LM_RequirementsCol;
		//Relationship: FK_LM_Requirements_LM_RequirementTypes
		public LM_RequirementCollection LM_RequirementsCol
		{
			get
			{
				if(_LM_RequirementsCol == null) {
					_LM_RequirementsCol = new LM_RequirementCollection();
					_LM_RequirementsCol.LoadAndCloseReader(LM_Requirement.Query()
						.WHERE(LM_Requirement.Columns.RequirementTypeID, RequirementTypeID).ExecuteReader());
				}
				return _LM_RequirementsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SAE_LicenseIndex class.
	/// </summary>
	[DataContract]
	public partial class SAE_LicenseIndexCollection : ActiveList<SAE_LicenseIndex, SAE_LicenseIndexCollection>
	{
		public static SAE_LicenseIndexCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SAE_LicenseIndexCollection result = new SAE_LicenseIndexCollection();
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
			foreach (SAE_LicenseIndex item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the SAE_LicenseIndex table.
	/// </summary>
	[DataContract]
	public partial class SAE_LicenseIndex : ActiveRecord<SAE_LicenseIndex>, INotifyPropertyChanged
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

		public SAE_LicenseIndex()
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
				TableSchema.Table schema = new TableSchema.Table("SAE_LicenseIndex", TableType.Table, DataService.GetInstance("NxsLicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarID = new TableSchema.TableColumn(schema);
				colvarID.ColumnName = "ID";
				colvarID.DataType = DbType.Int64;
				colvarID.MaxLength = 0;
				colvarID.AutoIncrement = false;
				colvarID.IsNullable = false;
				colvarID.IsPrimaryKey = true;
				colvarID.IsForeignKey = false;
				colvarID.IsReadOnly = false;
				colvarID.DefaultSetting = @"";
				colvarID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarID);

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = false;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = false;
				colvarRequirementID.IsForeignKey = true;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "LM_Requirements";
				schema.Columns.Add(colvarRequirementID);

				TableSchema.TableColumn colvarCountry = new TableSchema.TableColumn(schema);
				colvarCountry.ColumnName = "Country";
				colvarCountry.DataType = DbType.String;
				colvarCountry.MaxLength = 50;
				colvarCountry.AutoIncrement = false;
				colvarCountry.IsNullable = false;
				colvarCountry.IsPrimaryKey = false;
				colvarCountry.IsForeignKey = false;
				colvarCountry.IsReadOnly = false;
				colvarCountry.DefaultSetting = @"";
				colvarCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountry);

				TableSchema.TableColumn colvarStateAB = new TableSchema.TableColumn(schema);
				colvarStateAB.ColumnName = "StateAB";
				colvarStateAB.DataType = DbType.AnsiStringFixedLength;
				colvarStateAB.MaxLength = 2;
				colvarStateAB.AutoIncrement = false;
				colvarStateAB.IsNullable = true;
				colvarStateAB.IsPrimaryKey = false;
				colvarStateAB.IsForeignKey = false;
				colvarStateAB.IsReadOnly = false;
				colvarStateAB.DefaultSetting = @"";
				colvarStateAB.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateAB);

				TableSchema.TableColumn colvarCounty = new TableSchema.TableColumn(schema);
				colvarCounty.ColumnName = "County";
				colvarCounty.DataType = DbType.String;
				colvarCounty.MaxLength = 50;
				colvarCounty.AutoIncrement = false;
				colvarCounty.IsNullable = true;
				colvarCounty.IsPrimaryKey = false;
				colvarCounty.IsForeignKey = false;
				colvarCounty.IsReadOnly = false;
				colvarCounty.DefaultSetting = @"";
				colvarCounty.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCounty);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarTownship = new TableSchema.TableColumn(schema);
				colvarTownship.ColumnName = "Township";
				colvarTownship.DataType = DbType.String;
				colvarTownship.MaxLength = 50;
				colvarTownship.AutoIncrement = false;
				colvarTownship.IsNullable = true;
				colvarTownship.IsPrimaryKey = false;
				colvarTownship.IsForeignKey = false;
				colvarTownship.IsReadOnly = false;
				colvarTownship.DefaultSetting = @"";
				colvarTownship.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTownship);

				BaseSchema = schema;
				DataService.Providers["NxsLicensingProvider"].AddSchema("SAE_LicenseIndex",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SAE_LicenseIndex LoadFrom(SAE_LicenseIndex item)
		{
			SAE_LicenseIndex result = new SAE_LicenseIndex();
			if (item.ID != default(long)) {
				result.LoadByKey(item.ID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ID { 
			get { return GetColumnValue<long>(Columns.ID); }
			set {
				SetColumnValue(Columns.ID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ID));
			}
		}
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set {
				SetColumnValue(Columns.RequirementID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequirementID));
			}
		}
		[DataMember]
		public string Country { 
			get { return GetColumnValue<string>(Columns.Country); }
			set {
				SetColumnValue(Columns.Country, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Country));
			}
		}
		[DataMember]
		public string StateAB { 
			get { return GetColumnValue<string>(Columns.StateAB); }
			set {
				SetColumnValue(Columns.StateAB, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StateAB));
			}
		}
		[DataMember]
		public string County { 
			get { return GetColumnValue<string>(Columns.County); }
			set {
				SetColumnValue(Columns.County, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.County));
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
		public string Township { 
			get { return GetColumnValue<string>(Columns.Township); }
			set {
				SetColumnValue(Columns.Township, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Township));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LM_Requirement _Requirement;
		//Relationship: FK_SAE_LicenseIndex_SAE_LicenseIndex
		public LM_Requirement Requirement
		{
			get
			{
				if(_Requirement == null) {
					_Requirement = LM_Requirement.FetchByID(this.RequirementID);
				}
				return _Requirement;
			}
			set
			{
				SetColumnValue("RequirementID", value.RequirementID);
				_Requirement = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn IDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CountryColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn StateABColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CountyColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TownshipColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ID = @"ID";
			public static readonly string RequirementID = @"RequirementID";
			public static readonly string Country = @"Country";
			public static readonly string StateAB = @"StateAB";
			public static readonly string County = @"County";
			public static readonly string City = @"City";
			public static readonly string Township = @"Township";
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
