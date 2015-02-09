


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

namespace NXS.Data.Licensing
{
	/// <summary>
	/// Strongly-typed collection for the AllRequirementsPerLocationView class.
	/// </summary>
	[DataContract]
	public partial class AllRequirementsPerLocationViewCollection : ReadOnlyList<AllRequirementsPerLocationView, AllRequirementsPerLocationViewCollection>
	{
		public static AllRequirementsPerLocationViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AllRequirementsPerLocationViewCollection result = new AllRequirementsPerLocationViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAllRequirementsPerLocation view.
	/// </summary>
	[DataContract]
	public partial class AllRequirementsPerLocationView : ReadOnlyRecord<AllRequirementsPerLocationView>
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
				TableSchema.Table schema = new TableSchema.Table("vwAllRequirementsPerLocation", TableType.Table, DataService.GetInstance("LicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

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

				TableSchema.TableColumn colvarLocationTypeID = new TableSchema.TableColumn(schema);
				colvarLocationTypeID.ColumnName = "LocationTypeID";
				colvarLocationTypeID.DataType = DbType.Int32;
				colvarLocationTypeID.MaxLength = 0;
				colvarLocationTypeID.AutoIncrement = false;
				colvarLocationTypeID.IsNullable = false;
				colvarLocationTypeID.IsPrimaryKey = false;
				colvarLocationTypeID.IsForeignKey = false;
				colvarLocationTypeID.IsReadOnly = false;
				colvarLocationTypeID.DefaultSetting = @"";
				colvarLocationTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationTypeID);

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = false;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = false;
				colvarRequirementID.IsForeignKey = false;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementID);

				TableSchema.TableColumn colvarRequirementTypeID = new TableSchema.TableColumn(schema);
				colvarRequirementTypeID.ColumnName = "RequirementTypeID";
				colvarRequirementTypeID.DataType = DbType.Int32;
				colvarRequirementTypeID.MaxLength = 0;
				colvarRequirementTypeID.AutoIncrement = false;
				colvarRequirementTypeID.IsNullable = false;
				colvarRequirementTypeID.IsPrimaryKey = false;
				colvarRequirementTypeID.IsForeignKey = false;
				colvarRequirementTypeID.IsReadOnly = false;
				colvarRequirementTypeID.DefaultSetting = @"";
				colvarRequirementTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementTypeID);

				BaseSchema = schema;
				DataService.Providers["LicensingProvider"].AddSchema("vwAllRequirementsPerLocation",schema);
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
		public AllRequirementsPerLocationView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string LocationName { 
			get { return GetColumnValue<string>(Columns.LocationName); }
			set { SetColumnValue(Columns.LocationName, value); }
		}
		[DataMember]
		public string Abbreviation { 
			get { return GetColumnValue<string>(Columns.Abbreviation); }
			set { SetColumnValue(Columns.Abbreviation, value); }
		}
		[DataMember]
		public int LocationTypeID { 
			get { return GetColumnValue<int>(Columns.LocationTypeID); }
			set { SetColumnValue(Columns.LocationTypeID, value); }
		}
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set { SetColumnValue(Columns.RequirementID, value); }
		}
		[DataMember]
		public int RequirementTypeID { 
			get { return GetColumnValue<int>(Columns.RequirementTypeID); }
			set { SetColumnValue(Columns.RequirementTypeID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return Abbreviation;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LocationNameColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AbbreviationColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LocationTypeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RequirementTypeIDColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string LocationName = @"LocationName";
			public const string Abbreviation = @"Abbreviation";
			public const string LocationTypeID = @"LocationTypeID";
			public const string RequirementID = @"RequirementID";
			public const string RequirementTypeID = @"RequirementTypeID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the LicenseLocationsView class.
	/// </summary>
	[DataContract]
	public partial class LicenseLocationsViewCollection : ReadOnlyList<LicenseLocationsView, LicenseLocationsViewCollection>
	{
		public static LicenseLocationsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LicenseLocationsViewCollection result = new LicenseLocationsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwLicenseLocations view.
	/// </summary>
	[DataContract]
	public partial class LicenseLocationsView : ReadOnlyRecord<LicenseLocationsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwLicenseLocations", TableType.Table, DataService.GetInstance("LicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLicenseID = new TableSchema.TableColumn(schema);
				colvarLicenseID.ColumnName = "LicenseID";
				colvarLicenseID.DataType = DbType.Int32;
				colvarLicenseID.MaxLength = 0;
				colvarLicenseID.AutoIncrement = false;
				colvarLicenseID.IsNullable = false;
				colvarLicenseID.IsPrimaryKey = false;
				colvarLicenseID.IsForeignKey = false;
				colvarLicenseID.IsReadOnly = false;
				colvarLicenseID.DefaultSetting = @"";
				colvarLicenseID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicenseID);

				TableSchema.TableColumn colvarCountry = new TableSchema.TableColumn(schema);
				colvarCountry.ColumnName = "Country";
				colvarCountry.DataType = DbType.String;
				colvarCountry.MaxLength = 100;
				colvarCountry.AutoIncrement = false;
				colvarCountry.IsNullable = true;
				colvarCountry.IsPrimaryKey = false;
				colvarCountry.IsForeignKey = false;
				colvarCountry.IsReadOnly = false;
				colvarCountry.DefaultSetting = @"";
				colvarCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountry);

				TableSchema.TableColumn colvarState = new TableSchema.TableColumn(schema);
				colvarState.ColumnName = "State";
				colvarState.DataType = DbType.String;
				colvarState.MaxLength = 100;
				colvarState.AutoIncrement = false;
				colvarState.IsNullable = true;
				colvarState.IsPrimaryKey = false;
				colvarState.IsForeignKey = false;
				colvarState.IsReadOnly = false;
				colvarState.DefaultSetting = @"";
				colvarState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarState);

				TableSchema.TableColumn colvarStateAB = new TableSchema.TableColumn(schema);
				colvarStateAB.ColumnName = "StateAB";
				colvarStateAB.DataType = DbType.String;
				colvarStateAB.MaxLength = 10;
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
				colvarCounty.MaxLength = 100;
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
				colvarCity.MaxLength = 100;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				BaseSchema = schema;
				DataService.Providers["LicensingProvider"].AddSchema("vwLicenseLocations",schema);
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
		public LicenseLocationsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int LicenseID { 
			get { return GetColumnValue<int>(Columns.LicenseID); }
			set { SetColumnValue(Columns.LicenseID, value); }
		}
		[DataMember]
		public string Country { 
			get { return GetColumnValue<string>(Columns.Country); }
			set { SetColumnValue(Columns.Country, value); }
		}
		[DataMember]
		public string State { 
			get { return GetColumnValue<string>(Columns.State); }
			set { SetColumnValue(Columns.State, value); }
		}
		[DataMember]
		public string StateAB { 
			get { return GetColumnValue<string>(Columns.StateAB); }
			set { SetColumnValue(Columns.StateAB, value); }
		}
		[DataMember]
		public string County { 
			get { return GetColumnValue<string>(Columns.County); }
			set { SetColumnValue(Columns.County, value); }
		}
		[DataMember]
		public string City { 
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return Country;
		}

		#region Typed Columns

		public static TableSchema.TableColumn LicenseIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CountryColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn StateColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string LicenseID = @"LicenseID";
			public const string Country = @"Country";
			public const string State = @"State";
			public const string StateAB = @"StateAB";
			public const string County = @"County";
			public const string City = @"City";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RequirementLocationsView class.
	/// </summary>
	[DataContract]
	public partial class RequirementLocationsViewCollection : ReadOnlyList<RequirementLocationsView, RequirementLocationsViewCollection>
	{
		public static RequirementLocationsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RequirementLocationsViewCollection result = new RequirementLocationsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRequirementLocations view.
	/// </summary>
	[DataContract]
	public partial class RequirementLocationsView : ReadOnlyRecord<RequirementLocationsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRequirementLocations", TableType.Table, DataService.GetInstance("LicensingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequirementID = new TableSchema.TableColumn(schema);
				colvarRequirementID.ColumnName = "RequirementID";
				colvarRequirementID.DataType = DbType.Int32;
				colvarRequirementID.MaxLength = 0;
				colvarRequirementID.AutoIncrement = false;
				colvarRequirementID.IsNullable = false;
				colvarRequirementID.IsPrimaryKey = false;
				colvarRequirementID.IsForeignKey = false;
				colvarRequirementID.IsReadOnly = false;
				colvarRequirementID.DefaultSetting = @"";
				colvarRequirementID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementID);

				TableSchema.TableColumn colvarRequirementTypeID = new TableSchema.TableColumn(schema);
				colvarRequirementTypeID.ColumnName = "RequirementTypeID";
				colvarRequirementTypeID.DataType = DbType.Int32;
				colvarRequirementTypeID.MaxLength = 0;
				colvarRequirementTypeID.AutoIncrement = false;
				colvarRequirementTypeID.IsNullable = false;
				colvarRequirementTypeID.IsPrimaryKey = false;
				colvarRequirementTypeID.IsForeignKey = false;
				colvarRequirementTypeID.IsReadOnly = false;
				colvarRequirementTypeID.DefaultSetting = @"";
				colvarRequirementTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequirementTypeID);

				TableSchema.TableColumn colvarCountry = new TableSchema.TableColumn(schema);
				colvarCountry.ColumnName = "Country";
				colvarCountry.DataType = DbType.String;
				colvarCountry.MaxLength = 100;
				colvarCountry.AutoIncrement = false;
				colvarCountry.IsNullable = true;
				colvarCountry.IsPrimaryKey = false;
				colvarCountry.IsForeignKey = false;
				colvarCountry.IsReadOnly = false;
				colvarCountry.DefaultSetting = @"";
				colvarCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountry);

				TableSchema.TableColumn colvarState = new TableSchema.TableColumn(schema);
				colvarState.ColumnName = "State";
				colvarState.DataType = DbType.String;
				colvarState.MaxLength = 100;
				colvarState.AutoIncrement = false;
				colvarState.IsNullable = true;
				colvarState.IsPrimaryKey = false;
				colvarState.IsForeignKey = false;
				colvarState.IsReadOnly = false;
				colvarState.DefaultSetting = @"";
				colvarState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarState);

				TableSchema.TableColumn colvarStateAB = new TableSchema.TableColumn(schema);
				colvarStateAB.ColumnName = "StateAB";
				colvarStateAB.DataType = DbType.String;
				colvarStateAB.MaxLength = 10;
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
				colvarCounty.MaxLength = 100;
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
				colvarCity.MaxLength = 100;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				BaseSchema = schema;
				DataService.Providers["LicensingProvider"].AddSchema("vwRequirementLocations",schema);
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
		public RequirementLocationsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RequirementID { 
			get { return GetColumnValue<int>(Columns.RequirementID); }
			set { SetColumnValue(Columns.RequirementID, value); }
		}
		[DataMember]
		public int RequirementTypeID { 
			get { return GetColumnValue<int>(Columns.RequirementTypeID); }
			set { SetColumnValue(Columns.RequirementTypeID, value); }
		}
		[DataMember]
		public string Country { 
			get { return GetColumnValue<string>(Columns.Country); }
			set { SetColumnValue(Columns.Country, value); }
		}
		[DataMember]
		public string State { 
			get { return GetColumnValue<string>(Columns.State); }
			set { SetColumnValue(Columns.State, value); }
		}
		[DataMember]
		public string StateAB { 
			get { return GetColumnValue<string>(Columns.StateAB); }
			set { SetColumnValue(Columns.StateAB, value); }
		}
		[DataMember]
		public string County { 
			get { return GetColumnValue<string>(Columns.County); }
			set { SetColumnValue(Columns.County, value); }
		}
		[DataMember]
		public string City { 
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RequirementID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequirementIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequirementTypeIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CountryColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn StateColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn StateABColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CountyColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RequirementID = @"RequirementID";
			public const string RequirementTypeID = @"RequirementTypeID";
			public const string Country = @"Country";
			public const string State = @"State";
			public const string StateAB = @"StateAB";
			public const string County = @"County";
			public const string City = @"City";
		}
		#endregion Columns Struct
	}
}
