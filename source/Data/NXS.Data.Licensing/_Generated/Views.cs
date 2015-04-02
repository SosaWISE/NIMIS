


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
	/// Strongly-typed collection for the LM_RequirementsView class.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementsViewCollection : ReadOnlyList<LM_RequirementsView, LM_RequirementsViewCollection>
	{
		public static LM_RequirementsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LM_RequirementsViewCollection result = new LM_RequirementsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwLM_Requirements view.
	/// </summary>
	[DataContract]
	public partial class LM_RequirementsView : ReadOnlyRecord<LM_RequirementsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwLM_Requirements", TableType.Table, DataService.GetInstance("LicensingProvider"));
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

				TableSchema.TableColumn colvarLocationID = new TableSchema.TableColumn(schema);
				colvarLocationID.ColumnName = "LocationID";
				colvarLocationID.DataType = DbType.Int32;
				colvarLocationID.MaxLength = 0;
				colvarLocationID.AutoIncrement = false;
				colvarLocationID.IsNullable = false;
				colvarLocationID.IsPrimaryKey = false;
				colvarLocationID.IsForeignKey = false;
				colvarLocationID.IsReadOnly = false;
				colvarLocationID.DefaultSetting = @"";
				colvarLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocationID);

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

				TableSchema.TableColumn colvarAgencyID = new TableSchema.TableColumn(schema);
				colvarAgencyID.ColumnName = "AgencyID";
				colvarAgencyID.DataType = DbType.Int32;
				colvarAgencyID.MaxLength = 0;
				colvarAgencyID.AutoIncrement = false;
				colvarAgencyID.IsNullable = true;
				colvarAgencyID.IsPrimaryKey = false;
				colvarAgencyID.IsForeignKey = false;
				colvarAgencyID.IsReadOnly = false;
				colvarAgencyID.DefaultSetting = @"";
				colvarAgencyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAgencyID);

				TableSchema.TableColumn colvarLockID = new TableSchema.TableColumn(schema);
				colvarLockID.ColumnName = "LockID";
				colvarLockID.DataType = DbType.Int32;
				colvarLockID.MaxLength = 0;
				colvarLockID.AutoIncrement = false;
				colvarLockID.IsNullable = false;
				colvarLockID.IsPrimaryKey = false;
				colvarLockID.IsForeignKey = false;
				colvarLockID.IsReadOnly = false;
				colvarLockID.DefaultSetting = @"";
				colvarLockID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLockID);

				TableSchema.TableColumn colvarLockTypeName = new TableSchema.TableColumn(schema);
				colvarLockTypeName.ColumnName = "LockTypeName";
				colvarLockTypeName.DataType = DbType.String;
				colvarLockTypeName.MaxLength = 50;
				colvarLockTypeName.AutoIncrement = false;
				colvarLockTypeName.IsNullable = false;
				colvarLockTypeName.IsPrimaryKey = false;
				colvarLockTypeName.IsForeignKey = false;
				colvarLockTypeName.IsReadOnly = false;
				colvarLockTypeName.DefaultSetting = @"";
				colvarLockTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLockTypeName);

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
				DataService.Providers["LicensingProvider"].AddSchema("vwLM_Requirements",schema);
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
		public LM_RequirementsView()
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
		public int LocationID { 
			get { return GetColumnValue<int>(Columns.LocationID); }
			set { SetColumnValue(Columns.LocationID, value); }
		}
		[DataMember]
		public string LocationName { 
			get { return GetColumnValue<string>(Columns.LocationName); }
			set { SetColumnValue(Columns.LocationName, value); }
		}
		[DataMember]
		public string LocationTypeName { 
			get { return GetColumnValue<string>(Columns.LocationTypeName); }
			set { SetColumnValue(Columns.LocationTypeName, value); }
		}
		[DataMember]
		public int RequirementTypeID { 
			get { return GetColumnValue<int>(Columns.RequirementTypeID); }
			set { SetColumnValue(Columns.RequirementTypeID, value); }
		}
		[DataMember]
		public string RequirementTypeName { 
			get { return GetColumnValue<string>(Columns.RequirementTypeName); }
			set { SetColumnValue(Columns.RequirementTypeName, value); }
		}
		[DataMember]
		public int? AgencyID { 
			get { return GetColumnValue<int?>(Columns.AgencyID); }
			set { SetColumnValue(Columns.AgencyID, value); }
		}
		[DataMember]
		public int LockID { 
			get { return GetColumnValue<int>(Columns.LockID); }
			set { SetColumnValue(Columns.LockID, value); }
		}
		[DataMember]
		public string LockTypeName { 
			get { return GetColumnValue<string>(Columns.LockTypeName); }
			set { SetColumnValue(Columns.LockTypeName, value); }
		}
		[DataMember]
		public int? TemplateID { 
			get { return GetColumnValue<int?>(Columns.TemplateID); }
			set { SetColumnValue(Columns.TemplateID, value); }
		}
		[DataMember]
		public string RequirementName { 
			get { return GetColumnValue<string>(Columns.RequirementName); }
			set { SetColumnValue(Columns.RequirementName, value); }
		}
		[DataMember]
		public string ApplicationDescription { 
			get { return GetColumnValue<string>(Columns.ApplicationDescription); }
			set { SetColumnValue(Columns.ApplicationDescription, value); }
		}
		[DataMember]
		public string CallCenterMessage { 
			get { return GetColumnValue<string>(Columns.CallCenterMessage); }
			set { SetColumnValue(Columns.CallCenterMessage, value); }
		}
		[DataMember]
		public bool? RequiredForFunding { 
			get { return GetColumnValue<bool?>(Columns.RequiredForFunding); }
			set { SetColumnValue(Columns.RequiredForFunding, value); }
		}
		[DataMember]
		public decimal? Fee { 
			get { return GetColumnValue<decimal?>(Columns.Fee); }
			set { SetColumnValue(Columns.Fee, value); }
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
		[DataMember]
		public string CreatedByID { 
			get { return GetColumnValue<string>(Columns.CreatedByID); }
			set { SetColumnValue(Columns.CreatedByID, value); }
		}
		[DataMember]
		public DateTime CreatedByDate { 
			get { return GetColumnValue<DateTime>(Columns.CreatedByDate); }
			set { SetColumnValue(Columns.CreatedByDate, value); }
		}
		[DataMember]
		public string ModifiedByID { 
			get { return GetColumnValue<string>(Columns.ModifiedByID); }
			set { SetColumnValue(Columns.ModifiedByID, value); }
		}
		[DataMember]
		public DateTime ModifiedByDate { 
			get { return GetColumnValue<DateTime>(Columns.ModifiedByDate); }
			set { SetColumnValue(Columns.ModifiedByDate, value); }
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
		public static TableSchema.TableColumn LocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LocationNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LocationTypeNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RequirementTypeIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn RequirementTypeNameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn AgencyIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LockIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LockTypeNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TemplateIDColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn RequirementNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ApplicationDescriptionColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CallCenterMessageColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn RequiredForFundingColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn FeeColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn ModifiedByIDColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn ModifiedByDateColumn
		{
			get { return Schema.Columns[20]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RequirementID = @"RequirementID";
			public const string LocationID = @"LocationID";
			public const string LocationName = @"LocationName";
			public const string LocationTypeName = @"LocationTypeName";
			public const string RequirementTypeID = @"RequirementTypeID";
			public const string RequirementTypeName = @"RequirementTypeName";
			public const string AgencyID = @"AgencyID";
			public const string LockID = @"LockID";
			public const string LockTypeName = @"LockTypeName";
			public const string TemplateID = @"TemplateID";
			public const string RequirementName = @"RequirementName";
			public const string ApplicationDescription = @"ApplicationDescription";
			public const string CallCenterMessage = @"CallCenterMessage";
			public const string RequiredForFunding = @"RequiredForFunding";
			public const string Fee = @"Fee";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
			public const string CreatedByID = @"CreatedByID";
			public const string CreatedByDate = @"CreatedByDate";
			public const string ModifiedByID = @"ModifiedByID";
			public const string ModifiedByDate = @"ModifiedByDate";
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
