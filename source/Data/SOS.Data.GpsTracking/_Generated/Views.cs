


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

namespace SOS.Data.GpsTracking
{
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFencePointsView class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePointsViewCollection : ReadOnlyList<GS_AccountGeoFencePointsView, GS_AccountGeoFencePointsViewCollection>
	{
		public static GS_AccountGeoFencePointsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFencePointsViewCollection result = new GS_AccountGeoFencePointsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwGS_AccountGeoFencePoints view.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePointsView : ReadOnlyRecord<GS_AccountGeoFencePointsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwGS_AccountGeoFencePoints", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = false;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = false;
				colvarGeoFenceID.IsForeignKey = false;
				colvarGeoFenceID.IsReadOnly = false;
				colvarGeoFenceID.DefaultSetting = @"";
				colvarGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceID);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int64;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = false;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarGeogCol2 = new TableSchema.TableColumn(schema);
				colvarGeogCol2.ColumnName = "GeogCol2";
				colvarGeogCol2.DataType = DbType.String;
				colvarGeogCol2.MaxLength = -1;
				colvarGeogCol2.AutoIncrement = false;
				colvarGeogCol2.IsNullable = true;
				colvarGeogCol2.IsPrimaryKey = false;
				colvarGeogCol2.IsForeignKey = false;
				colvarGeogCol2.IsReadOnly = false;
				colvarGeogCol2.DefaultSetting = @"";
				colvarGeogCol2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeogCol2);

				TableSchema.TableColumn colvarPlaceName = new TableSchema.TableColumn(schema);
				colvarPlaceName.ColumnName = "PlaceName";
				colvarPlaceName.DataType = DbType.String;
				colvarPlaceName.MaxLength = 500;
				colvarPlaceName.AutoIncrement = false;
				colvarPlaceName.IsNullable = true;
				colvarPlaceName.IsPrimaryKey = false;
				colvarPlaceName.IsForeignKey = false;
				colvarPlaceName.IsReadOnly = false;
				colvarPlaceName.DefaultSetting = @"";
				colvarPlaceName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlaceName);

				TableSchema.TableColumn colvarPlaceDescription = new TableSchema.TableColumn(schema);
				colvarPlaceDescription.ColumnName = "PlaceDescription";
				colvarPlaceDescription.DataType = DbType.String;
				colvarPlaceDescription.MaxLength = -1;
				colvarPlaceDescription.AutoIncrement = false;
				colvarPlaceDescription.IsNullable = true;
				colvarPlaceDescription.IsPrimaryKey = false;
				colvarPlaceDescription.IsForeignKey = false;
				colvarPlaceDescription.IsReadOnly = false;
				colvarPlaceDescription.DefaultSetting = @"";
				colvarPlaceDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlaceDescription);

				TableSchema.TableColumn colvarPointLatitude = new TableSchema.TableColumn(schema);
				colvarPointLatitude.ColumnName = "PointLatitude";
				colvarPointLatitude.DataType = DbType.Double;
				colvarPointLatitude.MaxLength = 0;
				colvarPointLatitude.AutoIncrement = false;
				colvarPointLatitude.IsNullable = true;
				colvarPointLatitude.IsPrimaryKey = false;
				colvarPointLatitude.IsForeignKey = false;
				colvarPointLatitude.IsReadOnly = false;
				colvarPointLatitude.DefaultSetting = @"";
				colvarPointLatitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPointLatitude);

				TableSchema.TableColumn colvarPointLongitude = new TableSchema.TableColumn(schema);
				colvarPointLongitude.ColumnName = "PointLongitude";
				colvarPointLongitude.DataType = DbType.Double;
				colvarPointLongitude.MaxLength = 0;
				colvarPointLongitude.AutoIncrement = false;
				colvarPointLongitude.IsNullable = true;
				colvarPointLongitude.IsPrimaryKey = false;
				colvarPointLongitude.IsForeignKey = false;
				colvarPointLongitude.IsReadOnly = false;
				colvarPointLongitude.DefaultSetting = @"";
				colvarPointLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPointLongitude);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.String;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedBy);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwGS_AccountGeoFencePoints",schema);
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
		public GS_AccountGeoFencePointsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set { SetColumnValue(Columns.GeoFenceID, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public string GeogCol2 {
			get { return GetColumnValue<string>(Columns.GeogCol2); }
			set { SetColumnValue(Columns.GeogCol2, value); }
		}
		[DataMember]
		public string PlaceName {
			get { return GetColumnValue<string>(Columns.PlaceName); }
			set { SetColumnValue(Columns.PlaceName, value); }
		}
		[DataMember]
		public string PlaceDescription {
			get { return GetColumnValue<string>(Columns.PlaceDescription); }
			set { SetColumnValue(Columns.PlaceDescription, value); }
		}
		[DataMember]
		public double? PointLatitude {
			get { return GetColumnValue<double?>(Columns.PointLatitude); }
			set { SetColumnValue(Columns.PointLatitude, value); }
		}
		[DataMember]
		public double? PointLongitude {
			get { return GetColumnValue<double?>(Columns.PointLongitude); }
			set { SetColumnValue(Columns.PointLongitude, value); }
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set { SetColumnValue(Columns.ModifiedOn, value); }
		}
		[DataMember]
		public string ModifiedBy {
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
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
			return GeoFenceID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GeogCol2Column
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PlaceNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PlaceDescriptionColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PointLatitudeColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PointLongitudeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string GeoFenceID = @"GeoFenceID";
			public const string AccountId = @"AccountId";
			public const string GeogCol2 = @"GeogCol2";
			public const string PlaceName = @"PlaceName";
			public const string PlaceDescription = @"PlaceDescription";
			public const string PointLatitude = @"PointLatitude";
			public const string PointLongitude = @"PointLongitude";
			public const string ModifiedOn = @"ModifiedOn";
			public const string ModifiedBy = @"ModifiedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFencesView class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencesViewCollection : ReadOnlyList<GS_AccountGeoFencesView, GS_AccountGeoFencesViewCollection>
	{
		public static GS_AccountGeoFencesViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFencesViewCollection result = new GS_AccountGeoFencesViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwGS_AccountGeoFences view.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencesView : ReadOnlyRecord<GS_AccountGeoFencesView>
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
				TableSchema.Table schema = new TableSchema.Table("vwGS_AccountGeoFences", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = false;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = false;
				colvarGeoFenceID.IsForeignKey = false;
				colvarGeoFenceID.IsReadOnly = false;
				colvarGeoFenceID.DefaultSetting = @"";
				colvarGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceID);

				TableSchema.TableColumn colvarGeoFenceTypeId = new TableSchema.TableColumn(schema);
				colvarGeoFenceTypeId.ColumnName = "GeoFenceTypeId";
				colvarGeoFenceTypeId.DataType = DbType.AnsiString;
				colvarGeoFenceTypeId.MaxLength = 50;
				colvarGeoFenceTypeId.AutoIncrement = false;
				colvarGeoFenceTypeId.IsNullable = false;
				colvarGeoFenceTypeId.IsPrimaryKey = false;
				colvarGeoFenceTypeId.IsForeignKey = false;
				colvarGeoFenceTypeId.IsReadOnly = false;
				colvarGeoFenceTypeId.DefaultSetting = @"";
				colvarGeoFenceTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceTypeId);

				TableSchema.TableColumn colvarGeoFenceTypeUi = new TableSchema.TableColumn(schema);
				colvarGeoFenceTypeUi.ColumnName = "GeoFenceTypeUi";
				colvarGeoFenceTypeUi.DataType = DbType.String;
				colvarGeoFenceTypeUi.MaxLength = 100;
				colvarGeoFenceTypeUi.AutoIncrement = false;
				colvarGeoFenceTypeUi.IsNullable = true;
				colvarGeoFenceTypeUi.IsPrimaryKey = false;
				colvarGeoFenceTypeUi.IsForeignKey = false;
				colvarGeoFenceTypeUi.IsReadOnly = false;
				colvarGeoFenceTypeUi.DefaultSetting = @"";
				colvarGeoFenceTypeUi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceTypeUi);

				TableSchema.TableColumn colvarReportModeId = new TableSchema.TableColumn(schema);
				colvarReportModeId.ColumnName = "ReportModeId";
				colvarReportModeId.DataType = DbType.AnsiString;
				colvarReportModeId.MaxLength = 3;
				colvarReportModeId.AutoIncrement = false;
				colvarReportModeId.IsNullable = false;
				colvarReportModeId.IsPrimaryKey = false;
				colvarReportModeId.IsForeignKey = false;
				colvarReportModeId.IsReadOnly = false;
				colvarReportModeId.DefaultSetting = @"";
				colvarReportModeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeId);

				TableSchema.TableColumn colvarReportModeUi = new TableSchema.TableColumn(schema);
				colvarReportModeUi.ColumnName = "ReportModeUi";
				colvarReportModeUi.DataType = DbType.String;
				colvarReportModeUi.MaxLength = 6;
				colvarReportModeUi.AutoIncrement = false;
				colvarReportModeUi.IsNullable = true;
				colvarReportModeUi.IsPrimaryKey = false;
				colvarReportModeUi.IsForeignKey = false;
				colvarReportModeUi.IsReadOnly = false;
				colvarReportModeUi.DefaultSetting = @"";
				colvarReportModeUi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeUi);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int64;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = false;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarCustomerID = new TableSchema.TableColumn(schema);
				colvarCustomerID.ColumnName = "CustomerID";
				colvarCustomerID.DataType = DbType.Int64;
				colvarCustomerID.MaxLength = 0;
				colvarCustomerID.AutoIncrement = false;
				colvarCustomerID.IsNullable = false;
				colvarCustomerID.IsPrimaryKey = false;
				colvarCustomerID.IsForeignKey = false;
				colvarCustomerID.IsReadOnly = false;
				colvarCustomerID.DefaultSetting = @"";
				colvarCustomerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerID);

				TableSchema.TableColumn colvarCustomerMasterFileId = new TableSchema.TableColumn(schema);
				colvarCustomerMasterFileId.ColumnName = "CustomerMasterFileId";
				colvarCustomerMasterFileId.DataType = DbType.Int64;
				colvarCustomerMasterFileId.MaxLength = 0;
				colvarCustomerMasterFileId.AutoIncrement = false;
				colvarCustomerMasterFileId.IsNullable = false;
				colvarCustomerMasterFileId.IsPrimaryKey = false;
				colvarCustomerMasterFileId.IsForeignKey = false;
				colvarCustomerMasterFileId.IsReadOnly = false;
				colvarCustomerMasterFileId.DefaultSetting = @"";
				colvarCustomerMasterFileId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMasterFileId);

				TableSchema.TableColumn colvarGeoFenceName = new TableSchema.TableColumn(schema);
				colvarGeoFenceName.ColumnName = "GeoFenceName";
				colvarGeoFenceName.DataType = DbType.String;
				colvarGeoFenceName.MaxLength = 50;
				colvarGeoFenceName.AutoIncrement = false;
				colvarGeoFenceName.IsNullable = true;
				colvarGeoFenceName.IsPrimaryKey = false;
				colvarGeoFenceName.IsForeignKey = false;
				colvarGeoFenceName.IsReadOnly = false;
				colvarGeoFenceName.DefaultSetting = @"";
				colvarGeoFenceName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceName);

				TableSchema.TableColumn colvarGeoFenceNameUi = new TableSchema.TableColumn(schema);
				colvarGeoFenceNameUi.ColumnName = "GeoFenceNameUi";
				colvarGeoFenceNameUi.DataType = DbType.String;
				colvarGeoFenceNameUi.MaxLength = 100;
				colvarGeoFenceNameUi.AutoIncrement = false;
				colvarGeoFenceNameUi.IsNullable = true;
				colvarGeoFenceNameUi.IsPrimaryKey = false;
				colvarGeoFenceNameUi.IsForeignKey = false;
				colvarGeoFenceNameUi.IsReadOnly = false;
				colvarGeoFenceNameUi.DefaultSetting = @"";
				colvarGeoFenceNameUi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceNameUi);

				TableSchema.TableColumn colvarGeoFenceDescription = new TableSchema.TableColumn(schema);
				colvarGeoFenceDescription.ColumnName = "GeoFenceDescription";
				colvarGeoFenceDescription.DataType = DbType.String;
				colvarGeoFenceDescription.MaxLength = -1;
				colvarGeoFenceDescription.AutoIncrement = false;
				colvarGeoFenceDescription.IsNullable = true;
				colvarGeoFenceDescription.IsPrimaryKey = false;
				colvarGeoFenceDescription.IsForeignKey = false;
				colvarGeoFenceDescription.IsReadOnly = false;
				colvarGeoFenceDescription.DefaultSetting = @"";
				colvarGeoFenceDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceDescription);

				TableSchema.TableColumn colvarGeogCol2 = new TableSchema.TableColumn(schema);
				colvarGeogCol2.ColumnName = "GeogCol2";
				colvarGeogCol2.DataType = DbType.String;
				colvarGeogCol2.MaxLength = -1;
				colvarGeogCol2.AutoIncrement = false;
				colvarGeogCol2.IsNullable = true;
				colvarGeogCol2.IsPrimaryKey = false;
				colvarGeogCol2.IsForeignKey = false;
				colvarGeogCol2.IsReadOnly = false;
				colvarGeogCol2.DefaultSetting = @"";
				colvarGeogCol2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeogCol2);

				TableSchema.TableColumn colvarMeanLattitude = new TableSchema.TableColumn(schema);
				colvarMeanLattitude.ColumnName = "MeanLattitude";
				colvarMeanLattitude.DataType = DbType.Double;
				colvarMeanLattitude.MaxLength = 0;
				colvarMeanLattitude.AutoIncrement = false;
				colvarMeanLattitude.IsNullable = true;
				colvarMeanLattitude.IsPrimaryKey = false;
				colvarMeanLattitude.IsForeignKey = false;
				colvarMeanLattitude.IsReadOnly = false;
				colvarMeanLattitude.DefaultSetting = @"";
				colvarMeanLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMeanLattitude);

				TableSchema.TableColumn colvarMeanLongitude = new TableSchema.TableColumn(schema);
				colvarMeanLongitude.ColumnName = "MeanLongitude";
				colvarMeanLongitude.DataType = DbType.Double;
				colvarMeanLongitude.MaxLength = 0;
				colvarMeanLongitude.AutoIncrement = false;
				colvarMeanLongitude.IsNullable = true;
				colvarMeanLongitude.IsPrimaryKey = false;
				colvarMeanLongitude.IsForeignKey = false;
				colvarMeanLongitude.IsReadOnly = false;
				colvarMeanLongitude.DefaultSetting = @"";
				colvarMeanLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMeanLongitude);

				TableSchema.TableColumn colvarArea = new TableSchema.TableColumn(schema);
				colvarArea.ColumnName = "Area";
				colvarArea.DataType = DbType.Double;
				colvarArea.MaxLength = 0;
				colvarArea.AutoIncrement = false;
				colvarArea.IsNullable = true;
				colvarArea.IsPrimaryKey = false;
				colvarArea.IsForeignKey = false;
				colvarArea.IsReadOnly = false;
				colvarArea.DefaultSetting = @"";
				colvarArea.ForeignKeyTableName = "";
				schema.Columns.Add(colvarArea);

				TableSchema.TableColumn colvarGeoFenceType = new TableSchema.TableColumn(schema);
				colvarGeoFenceType.ColumnName = "GeoFenceType";
				colvarGeoFenceType.DataType = DbType.String;
				colvarGeoFenceType.MaxLength = 50;
				colvarGeoFenceType.AutoIncrement = false;
				colvarGeoFenceType.IsNullable = false;
				colvarGeoFenceType.IsPrimaryKey = false;
				colvarGeoFenceType.IsForeignKey = false;
				colvarGeoFenceType.IsReadOnly = false;
				colvarGeoFenceType.DefaultSetting = @"";
				colvarGeoFenceType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceType);

				TableSchema.TableColumn colvarPlaceName = new TableSchema.TableColumn(schema);
				colvarPlaceName.ColumnName = "PlaceName";
				colvarPlaceName.DataType = DbType.String;
				colvarPlaceName.MaxLength = 500;
				colvarPlaceName.AutoIncrement = false;
				colvarPlaceName.IsNullable = true;
				colvarPlaceName.IsPrimaryKey = false;
				colvarPlaceName.IsForeignKey = false;
				colvarPlaceName.IsReadOnly = false;
				colvarPlaceName.DefaultSetting = @"";
				colvarPlaceName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlaceName);

				TableSchema.TableColumn colvarPlaceDescription = new TableSchema.TableColumn(schema);
				colvarPlaceDescription.ColumnName = "PlaceDescription";
				colvarPlaceDescription.DataType = DbType.String;
				colvarPlaceDescription.MaxLength = -1;
				colvarPlaceDescription.AutoIncrement = false;
				colvarPlaceDescription.IsNullable = true;
				colvarPlaceDescription.IsPrimaryKey = false;
				colvarPlaceDescription.IsForeignKey = false;
				colvarPlaceDescription.IsReadOnly = false;
				colvarPlaceDescription.DefaultSetting = @"";
				colvarPlaceDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlaceDescription);

				TableSchema.TableColumn colvarPointLatitude = new TableSchema.TableColumn(schema);
				colvarPointLatitude.ColumnName = "PointLatitude";
				colvarPointLatitude.DataType = DbType.Double;
				colvarPointLatitude.MaxLength = 0;
				colvarPointLatitude.AutoIncrement = false;
				colvarPointLatitude.IsNullable = true;
				colvarPointLatitude.IsPrimaryKey = false;
				colvarPointLatitude.IsForeignKey = false;
				colvarPointLatitude.IsReadOnly = false;
				colvarPointLatitude.DefaultSetting = @"";
				colvarPointLatitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPointLatitude);

				TableSchema.TableColumn colvarPointLongitude = new TableSchema.TableColumn(schema);
				colvarPointLongitude.ColumnName = "PointLongitude";
				colvarPointLongitude.DataType = DbType.Double;
				colvarPointLongitude.MaxLength = 0;
				colvarPointLongitude.AutoIncrement = false;
				colvarPointLongitude.IsNullable = true;
				colvarPointLongitude.IsPrimaryKey = false;
				colvarPointLongitude.IsForeignKey = false;
				colvarPointLongitude.IsReadOnly = false;
				colvarPointLongitude.DefaultSetting = @"";
				colvarPointLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPointLongitude);

				TableSchema.TableColumn colvarCenterLattitude = new TableSchema.TableColumn(schema);
				colvarCenterLattitude.ColumnName = "CenterLattitude";
				colvarCenterLattitude.DataType = DbType.Double;
				colvarCenterLattitude.MaxLength = 0;
				colvarCenterLattitude.AutoIncrement = false;
				colvarCenterLattitude.IsNullable = true;
				colvarCenterLattitude.IsPrimaryKey = false;
				colvarCenterLattitude.IsForeignKey = false;
				colvarCenterLattitude.IsReadOnly = false;
				colvarCenterLattitude.DefaultSetting = @"";
				colvarCenterLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCenterLattitude);

				TableSchema.TableColumn colvarCenterLongitude = new TableSchema.TableColumn(schema);
				colvarCenterLongitude.ColumnName = "CenterLongitude";
				colvarCenterLongitude.DataType = DbType.Double;
				colvarCenterLongitude.MaxLength = 0;
				colvarCenterLongitude.AutoIncrement = false;
				colvarCenterLongitude.IsNullable = true;
				colvarCenterLongitude.IsPrimaryKey = false;
				colvarCenterLongitude.IsForeignKey = false;
				colvarCenterLongitude.IsReadOnly = false;
				colvarCenterLongitude.DefaultSetting = @"";
				colvarCenterLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCenterLongitude);

				TableSchema.TableColumn colvarRadius = new TableSchema.TableColumn(schema);
				colvarRadius.ColumnName = "Radius";
				colvarRadius.DataType = DbType.Double;
				colvarRadius.MaxLength = 0;
				colvarRadius.AutoIncrement = false;
				colvarRadius.IsNullable = true;
				colvarRadius.IsPrimaryKey = false;
				colvarRadius.IsForeignKey = false;
				colvarRadius.IsReadOnly = false;
				colvarRadius.DefaultSetting = @"";
				colvarRadius.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRadius);

				TableSchema.TableColumn colvarMinLattitude = new TableSchema.TableColumn(schema);
				colvarMinLattitude.ColumnName = "MinLattitude";
				colvarMinLattitude.DataType = DbType.Double;
				colvarMinLattitude.MaxLength = 0;
				colvarMinLattitude.AutoIncrement = false;
				colvarMinLattitude.IsNullable = true;
				colvarMinLattitude.IsPrimaryKey = false;
				colvarMinLattitude.IsForeignKey = false;
				colvarMinLattitude.IsReadOnly = false;
				colvarMinLattitude.DefaultSetting = @"";
				colvarMinLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMinLattitude);

				TableSchema.TableColumn colvarMinLongitude = new TableSchema.TableColumn(schema);
				colvarMinLongitude.ColumnName = "MinLongitude";
				colvarMinLongitude.DataType = DbType.Double;
				colvarMinLongitude.MaxLength = 0;
				colvarMinLongitude.AutoIncrement = false;
				colvarMinLongitude.IsNullable = true;
				colvarMinLongitude.IsPrimaryKey = false;
				colvarMinLongitude.IsForeignKey = false;
				colvarMinLongitude.IsReadOnly = false;
				colvarMinLongitude.DefaultSetting = @"";
				colvarMinLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMinLongitude);

				TableSchema.TableColumn colvarMaxLattitude = new TableSchema.TableColumn(schema);
				colvarMaxLattitude.ColumnName = "MaxLattitude";
				colvarMaxLattitude.DataType = DbType.Double;
				colvarMaxLattitude.MaxLength = 0;
				colvarMaxLattitude.AutoIncrement = false;
				colvarMaxLattitude.IsNullable = true;
				colvarMaxLattitude.IsPrimaryKey = false;
				colvarMaxLattitude.IsForeignKey = false;
				colvarMaxLattitude.IsReadOnly = false;
				colvarMaxLattitude.DefaultSetting = @"";
				colvarMaxLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaxLattitude);

				TableSchema.TableColumn colvarMaxLongitude = new TableSchema.TableColumn(schema);
				colvarMaxLongitude.ColumnName = "MaxLongitude";
				colvarMaxLongitude.DataType = DbType.Double;
				colvarMaxLongitude.MaxLength = 0;
				colvarMaxLongitude.AutoIncrement = false;
				colvarMaxLongitude.IsNullable = true;
				colvarMaxLongitude.IsPrimaryKey = false;
				colvarMaxLongitude.IsForeignKey = false;
				colvarMaxLongitude.IsReadOnly = false;
				colvarMaxLongitude.DefaultSetting = @"";
				colvarMaxLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaxLongitude);

				TableSchema.TableColumn colvarZoomLevel = new TableSchema.TableColumn(schema);
				colvarZoomLevel.ColumnName = "ZoomLevel";
				colvarZoomLevel.DataType = DbType.Int16;
				colvarZoomLevel.MaxLength = 0;
				colvarZoomLevel.AutoIncrement = false;
				colvarZoomLevel.IsNullable = true;
				colvarZoomLevel.IsPrimaryKey = false;
				colvarZoomLevel.IsForeignKey = false;
				colvarZoomLevel.IsReadOnly = false;
				colvarZoomLevel.DefaultSetting = @"";
				colvarZoomLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZoomLevel);

				TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(schema);
				colvarModifiedOn.ColumnName = "ModifiedOn";
				colvarModifiedOn.DataType = DbType.DateTime;
				colvarModifiedOn.MaxLength = 0;
				colvarModifiedOn.AutoIncrement = false;
				colvarModifiedOn.IsNullable = false;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.String;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"";
				colvarModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedBy);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwGS_AccountGeoFences",schema);
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
		public GS_AccountGeoFencesView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set { SetColumnValue(Columns.GeoFenceID, value); }
		}
		[DataMember]
		public string GeoFenceTypeId {
			get { return GetColumnValue<string>(Columns.GeoFenceTypeId); }
			set { SetColumnValue(Columns.GeoFenceTypeId, value); }
		}
		[DataMember]
		public string GeoFenceTypeUi {
			get { return GetColumnValue<string>(Columns.GeoFenceTypeUi); }
			set { SetColumnValue(Columns.GeoFenceTypeUi, value); }
		}
		[DataMember]
		public string ReportModeId {
			get { return GetColumnValue<string>(Columns.ReportModeId); }
			set { SetColumnValue(Columns.ReportModeId, value); }
		}
		[DataMember]
		public string ReportModeUi {
			get { return GetColumnValue<string>(Columns.ReportModeUi); }
			set { SetColumnValue(Columns.ReportModeUi, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public long CustomerID {
			get { return GetColumnValue<long>(Columns.CustomerID); }
			set { SetColumnValue(Columns.CustomerID, value); }
		}
		[DataMember]
		public long CustomerMasterFileId {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileId); }
			set { SetColumnValue(Columns.CustomerMasterFileId, value); }
		}
		[DataMember]
		public string GeoFenceName {
			get { return GetColumnValue<string>(Columns.GeoFenceName); }
			set { SetColumnValue(Columns.GeoFenceName, value); }
		}
		[DataMember]
		public string GeoFenceNameUi {
			get { return GetColumnValue<string>(Columns.GeoFenceNameUi); }
			set { SetColumnValue(Columns.GeoFenceNameUi, value); }
		}
		[DataMember]
		public string GeoFenceDescription {
			get { return GetColumnValue<string>(Columns.GeoFenceDescription); }
			set { SetColumnValue(Columns.GeoFenceDescription, value); }
		}
		[DataMember]
		public string GeogCol2 {
			get { return GetColumnValue<string>(Columns.GeogCol2); }
			set { SetColumnValue(Columns.GeogCol2, value); }
		}
		[DataMember]
		public double? MeanLattitude {
			get { return GetColumnValue<double?>(Columns.MeanLattitude); }
			set { SetColumnValue(Columns.MeanLattitude, value); }
		}
		[DataMember]
		public double? MeanLongitude {
			get { return GetColumnValue<double?>(Columns.MeanLongitude); }
			set { SetColumnValue(Columns.MeanLongitude, value); }
		}
		[DataMember]
		public double? Area {
			get { return GetColumnValue<double?>(Columns.Area); }
			set { SetColumnValue(Columns.Area, value); }
		}
		[DataMember]
		public string GeoFenceType {
			get { return GetColumnValue<string>(Columns.GeoFenceType); }
			set { SetColumnValue(Columns.GeoFenceType, value); }
		}
		[DataMember]
		public string PlaceName {
			get { return GetColumnValue<string>(Columns.PlaceName); }
			set { SetColumnValue(Columns.PlaceName, value); }
		}
		[DataMember]
		public string PlaceDescription {
			get { return GetColumnValue<string>(Columns.PlaceDescription); }
			set { SetColumnValue(Columns.PlaceDescription, value); }
		}
		[DataMember]
		public double? PointLatitude {
			get { return GetColumnValue<double?>(Columns.PointLatitude); }
			set { SetColumnValue(Columns.PointLatitude, value); }
		}
		[DataMember]
		public double? PointLongitude {
			get { return GetColumnValue<double?>(Columns.PointLongitude); }
			set { SetColumnValue(Columns.PointLongitude, value); }
		}
		[DataMember]
		public double? CenterLattitude {
			get { return GetColumnValue<double?>(Columns.CenterLattitude); }
			set { SetColumnValue(Columns.CenterLattitude, value); }
		}
		[DataMember]
		public double? CenterLongitude {
			get { return GetColumnValue<double?>(Columns.CenterLongitude); }
			set { SetColumnValue(Columns.CenterLongitude, value); }
		}
		[DataMember]
		public double? Radius {
			get { return GetColumnValue<double?>(Columns.Radius); }
			set { SetColumnValue(Columns.Radius, value); }
		}
		[DataMember]
		public double? MinLattitude {
			get { return GetColumnValue<double?>(Columns.MinLattitude); }
			set { SetColumnValue(Columns.MinLattitude, value); }
		}
		[DataMember]
		public double? MinLongitude {
			get { return GetColumnValue<double?>(Columns.MinLongitude); }
			set { SetColumnValue(Columns.MinLongitude, value); }
		}
		[DataMember]
		public double? MaxLattitude {
			get { return GetColumnValue<double?>(Columns.MaxLattitude); }
			set { SetColumnValue(Columns.MaxLattitude, value); }
		}
		[DataMember]
		public double? MaxLongitude {
			get { return GetColumnValue<double?>(Columns.MaxLongitude); }
			set { SetColumnValue(Columns.MaxLongitude, value); }
		}
		[DataMember]
		public short? ZoomLevel {
			get { return GetColumnValue<short?>(Columns.ZoomLevel); }
			set { SetColumnValue(Columns.ZoomLevel, value); }
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set { SetColumnValue(Columns.ModifiedOn, value); }
		}
		[DataMember]
		public string ModifiedBy {
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
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
			return GeoFenceTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GeoFenceTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GeoFenceTypeUiColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ReportModeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ReportModeUiColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CustomerIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CustomerMasterFileIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn GeoFenceNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn GeoFenceNameUiColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn GeoFenceDescriptionColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn GeogCol2Column
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn MeanLattitudeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn MeanLongitudeColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn AreaColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn GeoFenceTypeColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn PlaceNameColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn PlaceDescriptionColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn PointLatitudeColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn PointLongitudeColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CenterLattitudeColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn CenterLongitudeColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn RadiusColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn MinLattitudeColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn MinLongitudeColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn MaxLattitudeColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn MaxLongitudeColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn ZoomLevelColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[33]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string GeoFenceID = @"GeoFenceID";
			public const string GeoFenceTypeId = @"GeoFenceTypeId";
			public const string GeoFenceTypeUi = @"GeoFenceTypeUi";
			public const string ReportModeId = @"ReportModeId";
			public const string ReportModeUi = @"ReportModeUi";
			public const string AccountId = @"AccountId";
			public const string CustomerID = @"CustomerID";
			public const string CustomerMasterFileId = @"CustomerMasterFileId";
			public const string GeoFenceName = @"GeoFenceName";
			public const string GeoFenceNameUi = @"GeoFenceNameUi";
			public const string GeoFenceDescription = @"GeoFenceDescription";
			public const string GeogCol2 = @"GeogCol2";
			public const string MeanLattitude = @"MeanLattitude";
			public const string MeanLongitude = @"MeanLongitude";
			public const string Area = @"Area";
			public const string GeoFenceType = @"GeoFenceType";
			public const string PlaceName = @"PlaceName";
			public const string PlaceDescription = @"PlaceDescription";
			public const string PointLatitude = @"PointLatitude";
			public const string PointLongitude = @"PointLongitude";
			public const string CenterLattitude = @"CenterLattitude";
			public const string CenterLongitude = @"CenterLongitude";
			public const string Radius = @"Radius";
			public const string MinLattitude = @"MinLattitude";
			public const string MinLongitude = @"MinLongitude";
			public const string MaxLattitude = @"MaxLattitude";
			public const string MaxLongitude = @"MaxLongitude";
			public const string ZoomLevel = @"ZoomLevel";
			public const string ModifiedOn = @"ModifiedOn";
			public const string ModifiedBy = @"ModifiedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the GS_EventsView class.
	/// </summary>
	[DataContract]
	public partial class GS_EventsViewCollection : ReadOnlyList<GS_EventsView, GS_EventsViewCollection>
	{
		public static GS_EventsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_EventsViewCollection result = new GS_EventsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwGS_Events view.
	/// </summary>
	[DataContract]
	public partial class GS_EventsView : ReadOnlyRecord<GS_EventsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwGS_Events", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventID = new TableSchema.TableColumn(schema);
				colvarEventID.ColumnName = "EventID";
				colvarEventID.DataType = DbType.Int64;
				colvarEventID.MaxLength = 0;
				colvarEventID.AutoIncrement = false;
				colvarEventID.IsNullable = false;
				colvarEventID.IsPrimaryKey = false;
				colvarEventID.IsForeignKey = false;
				colvarEventID.IsReadOnly = false;
				colvarEventID.DefaultSetting = @"";
				colvarEventID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventID);

				TableSchema.TableColumn colvarEventTypeId = new TableSchema.TableColumn(schema);
				colvarEventTypeId.ColumnName = "EventTypeId";
				colvarEventTypeId.DataType = DbType.AnsiString;
				colvarEventTypeId.MaxLength = 20;
				colvarEventTypeId.AutoIncrement = false;
				colvarEventTypeId.IsNullable = false;
				colvarEventTypeId.IsPrimaryKey = false;
				colvarEventTypeId.IsForeignKey = false;
				colvarEventTypeId.IsReadOnly = false;
				colvarEventTypeId.DefaultSetting = @"";
				colvarEventTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventTypeId);

				TableSchema.TableColumn colvarEventType = new TableSchema.TableColumn(schema);
				colvarEventType.ColumnName = "EventType";
				colvarEventType.DataType = DbType.String;
				colvarEventType.MaxLength = 50;
				colvarEventType.AutoIncrement = false;
				colvarEventType.IsNullable = false;
				colvarEventType.IsPrimaryKey = false;
				colvarEventType.IsForeignKey = false;
				colvarEventType.IsReadOnly = false;
				colvarEventType.DefaultSetting = @"";
				colvarEventType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventType);

				TableSchema.TableColumn colvarEventTypeUi = new TableSchema.TableColumn(schema);
				colvarEventTypeUi.ColumnName = "EventTypeUi";
				colvarEventTypeUi.DataType = DbType.String;
				colvarEventTypeUi.MaxLength = 50;
				colvarEventTypeUi.AutoIncrement = false;
				colvarEventTypeUi.IsNullable = true;
				colvarEventTypeUi.IsPrimaryKey = false;
				colvarEventTypeUi.IsForeignKey = false;
				colvarEventTypeUi.IsReadOnly = false;
				colvarEventTypeUi.DefaultSetting = @"";
				colvarEventTypeUi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventTypeUi);

				TableSchema.TableColumn colvarEventShortDesc = new TableSchema.TableColumn(schema);
				colvarEventShortDesc.ColumnName = "EventShortDesc";
				colvarEventShortDesc.DataType = DbType.String;
				colvarEventShortDesc.MaxLength = 150;
				colvarEventShortDesc.AutoIncrement = false;
				colvarEventShortDesc.IsNullable = true;
				colvarEventShortDesc.IsPrimaryKey = false;
				colvarEventShortDesc.IsForeignKey = false;
				colvarEventShortDesc.IsReadOnly = false;
				colvarEventShortDesc.DefaultSetting = @"";
				colvarEventShortDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventShortDesc);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int64;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = false;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarCustomerID = new TableSchema.TableColumn(schema);
				colvarCustomerID.ColumnName = "CustomerID";
				colvarCustomerID.DataType = DbType.Int64;
				colvarCustomerID.MaxLength = 0;
				colvarCustomerID.AutoIncrement = false;
				colvarCustomerID.IsNullable = false;
				colvarCustomerID.IsPrimaryKey = false;
				colvarCustomerID.IsForeignKey = false;
				colvarCustomerID.IsReadOnly = false;
				colvarCustomerID.DefaultSetting = @"";
				colvarCustomerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerID);

				TableSchema.TableColumn colvarCustomerMasterFileId = new TableSchema.TableColumn(schema);
				colvarCustomerMasterFileId.ColumnName = "CustomerMasterFileId";
				colvarCustomerMasterFileId.DataType = DbType.Int64;
				colvarCustomerMasterFileId.MaxLength = 0;
				colvarCustomerMasterFileId.AutoIncrement = false;
				colvarCustomerMasterFileId.IsNullable = false;
				colvarCustomerMasterFileId.IsPrimaryKey = false;
				colvarCustomerMasterFileId.IsForeignKey = false;
				colvarCustomerMasterFileId.IsReadOnly = false;
				colvarCustomerMasterFileId.DefaultSetting = @"";
				colvarCustomerMasterFileId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMasterFileId);

				TableSchema.TableColumn colvarGeoFenceId = new TableSchema.TableColumn(schema);
				colvarGeoFenceId.ColumnName = "GeoFenceId";
				colvarGeoFenceId.DataType = DbType.Int64;
				colvarGeoFenceId.MaxLength = 0;
				colvarGeoFenceId.AutoIncrement = false;
				colvarGeoFenceId.IsNullable = true;
				colvarGeoFenceId.IsPrimaryKey = false;
				colvarGeoFenceId.IsForeignKey = false;
				colvarGeoFenceId.IsReadOnly = false;
				colvarGeoFenceId.DefaultSetting = @"";
				colvarGeoFenceId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceId);

				TableSchema.TableColumn colvarAccountName = new TableSchema.TableColumn(schema);
				colvarAccountName.ColumnName = "AccountName";
				colvarAccountName.DataType = DbType.String;
				colvarAccountName.MaxLength = 50;
				colvarAccountName.AutoIncrement = false;
				colvarAccountName.IsNullable = true;
				colvarAccountName.IsPrimaryKey = false;
				colvarAccountName.IsForeignKey = false;
				colvarAccountName.IsReadOnly = false;
				colvarAccountName.DefaultSetting = @"";
				colvarAccountName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountName);

				TableSchema.TableColumn colvarEventName = new TableSchema.TableColumn(schema);
				colvarEventName.ColumnName = "EventName";
				colvarEventName.DataType = DbType.String;
				colvarEventName.MaxLength = 50;
				colvarEventName.AutoIncrement = false;
				colvarEventName.IsNullable = false;
				colvarEventName.IsPrimaryKey = false;
				colvarEventName.IsForeignKey = false;
				colvarEventName.IsReadOnly = false;
				colvarEventName.DefaultSetting = @"";
				colvarEventName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventName);

				TableSchema.TableColumn colvarEventDate = new TableSchema.TableColumn(schema);
				colvarEventDate.ColumnName = "EventDate";
				colvarEventDate.DataType = DbType.DateTime;
				colvarEventDate.MaxLength = 0;
				colvarEventDate.AutoIncrement = false;
				colvarEventDate.IsNullable = false;
				colvarEventDate.IsPrimaryKey = false;
				colvarEventDate.IsForeignKey = false;
				colvarEventDate.IsReadOnly = false;
				colvarEventDate.DefaultSetting = @"";
				colvarEventDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventDate);

				TableSchema.TableColumn colvarLattitude = new TableSchema.TableColumn(schema);
				colvarLattitude.ColumnName = "Lattitude";
				colvarLattitude.DataType = DbType.AnsiString;
				colvarLattitude.MaxLength = 50;
				colvarLattitude.AutoIncrement = false;
				colvarLattitude.IsNullable = true;
				colvarLattitude.IsPrimaryKey = false;
				colvarLattitude.IsForeignKey = false;
				colvarLattitude.IsReadOnly = false;
				colvarLattitude.DefaultSetting = @"";
				colvarLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitude);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.AnsiString;
				colvarLongitude.MaxLength = 50;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = true;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwGS_Events",schema);
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
		public GS_EventsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long EventID {
			get { return GetColumnValue<long>(Columns.EventID); }
			set { SetColumnValue(Columns.EventID, value); }
		}
		[DataMember]
		public string EventTypeId {
			get { return GetColumnValue<string>(Columns.EventTypeId); }
			set { SetColumnValue(Columns.EventTypeId, value); }
		}
		[DataMember]
		public string EventType {
			get { return GetColumnValue<string>(Columns.EventType); }
			set { SetColumnValue(Columns.EventType, value); }
		}
		[DataMember]
		public string EventTypeUi {
			get { return GetColumnValue<string>(Columns.EventTypeUi); }
			set { SetColumnValue(Columns.EventTypeUi, value); }
		}
		[DataMember]
		public string EventShortDesc {
			get { return GetColumnValue<string>(Columns.EventShortDesc); }
			set { SetColumnValue(Columns.EventShortDesc, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public long CustomerID {
			get { return GetColumnValue<long>(Columns.CustomerID); }
			set { SetColumnValue(Columns.CustomerID, value); }
		}
		[DataMember]
		public long CustomerMasterFileId {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileId); }
			set { SetColumnValue(Columns.CustomerMasterFileId, value); }
		}
		[DataMember]
		public long? GeoFenceId {
			get { return GetColumnValue<long?>(Columns.GeoFenceId); }
			set { SetColumnValue(Columns.GeoFenceId, value); }
		}
		[DataMember]
		public string AccountName {
			get { return GetColumnValue<string>(Columns.AccountName); }
			set { SetColumnValue(Columns.AccountName, value); }
		}
		[DataMember]
		public string EventName {
			get { return GetColumnValue<string>(Columns.EventName); }
			set { SetColumnValue(Columns.EventName, value); }
		}
		[DataMember]
		public DateTime EventDate {
			get { return GetColumnValue<DateTime>(Columns.EventDate); }
			set { SetColumnValue(Columns.EventDate, value); }
		}
		[DataMember]
		public string Lattitude {
			get { return GetColumnValue<string>(Columns.Lattitude); }
			set { SetColumnValue(Columns.Lattitude, value); }
		}
		[DataMember]
		public string Longitude {
			get { return GetColumnValue<string>(Columns.Longitude); }
			set { SetColumnValue(Columns.Longitude, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return EventTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EventIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EventTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EventTypeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn EventTypeUiColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn EventShortDescColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CustomerIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CustomerMasterFileIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn GeoFenceIdColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn AccountNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn EventNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn EventDateColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn LattitudeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[13]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string EventID = @"EventID";
			public const string EventTypeId = @"EventTypeId";
			public const string EventType = @"EventType";
			public const string EventTypeUi = @"EventTypeUi";
			public const string EventShortDesc = @"EventShortDesc";
			public const string AccountId = @"AccountId";
			public const string CustomerID = @"CustomerID";
			public const string CustomerMasterFileId = @"CustomerMasterFileId";
			public const string GeoFenceId = @"GeoFenceId";
			public const string AccountName = @"AccountName";
			public const string EventName = @"EventName";
			public const string EventDate = @"EventDate";
			public const string Lattitude = @"Lattitude";
			public const string Longitude = @"Longitude";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the GS_EventsLastView class.
	/// </summary>
	[DataContract]
	public partial class GS_EventsLastViewCollection : ReadOnlyList<GS_EventsLastView, GS_EventsLastViewCollection>
	{
		public static GS_EventsLastViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_EventsLastViewCollection result = new GS_EventsLastViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwGS_EventsLast view.
	/// </summary>
	[DataContract]
	public partial class GS_EventsLastView : ReadOnlyRecord<GS_EventsLastView>
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
				TableSchema.Table schema = new TableSchema.Table("vwGS_EventsLast", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventID = new TableSchema.TableColumn(schema);
				colvarEventID.ColumnName = "EventID";
				colvarEventID.DataType = DbType.Int64;
				colvarEventID.MaxLength = 0;
				colvarEventID.AutoIncrement = true;
				colvarEventID.IsNullable = false;
				colvarEventID.IsPrimaryKey = false;
				colvarEventID.IsForeignKey = false;
				colvarEventID.IsReadOnly = false;
				colvarEventID.DefaultSetting = @"";
				colvarEventID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventID);

				TableSchema.TableColumn colvarEventTypeId = new TableSchema.TableColumn(schema);
				colvarEventTypeId.ColumnName = "EventTypeId";
				colvarEventTypeId.DataType = DbType.AnsiString;
				colvarEventTypeId.MaxLength = 20;
				colvarEventTypeId.AutoIncrement = false;
				colvarEventTypeId.IsNullable = false;
				colvarEventTypeId.IsPrimaryKey = false;
				colvarEventTypeId.IsForeignKey = false;
				colvarEventTypeId.IsReadOnly = false;
				colvarEventTypeId.DefaultSetting = @"";
				colvarEventTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventTypeId);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int64;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = false;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarEventName = new TableSchema.TableColumn(schema);
				colvarEventName.ColumnName = "EventName";
				colvarEventName.DataType = DbType.String;
				colvarEventName.MaxLength = 50;
				colvarEventName.AutoIncrement = false;
				colvarEventName.IsNullable = false;
				colvarEventName.IsPrimaryKey = false;
				colvarEventName.IsForeignKey = false;
				colvarEventName.IsReadOnly = false;
				colvarEventName.DefaultSetting = @"";
				colvarEventName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventName);

				TableSchema.TableColumn colvarEventDate = new TableSchema.TableColumn(schema);
				colvarEventDate.ColumnName = "EventDate";
				colvarEventDate.DataType = DbType.DateTime;
				colvarEventDate.MaxLength = 0;
				colvarEventDate.AutoIncrement = false;
				colvarEventDate.IsNullable = false;
				colvarEventDate.IsPrimaryKey = false;
				colvarEventDate.IsForeignKey = false;
				colvarEventDate.IsReadOnly = false;
				colvarEventDate.DefaultSetting = @"";
				colvarEventDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventDate);

				TableSchema.TableColumn colvarLattitude = new TableSchema.TableColumn(schema);
				colvarLattitude.ColumnName = "Lattitude";
				colvarLattitude.DataType = DbType.AnsiString;
				colvarLattitude.MaxLength = 50;
				colvarLattitude.AutoIncrement = false;
				colvarLattitude.IsNullable = true;
				colvarLattitude.IsPrimaryKey = false;
				colvarLattitude.IsForeignKey = false;
				colvarLattitude.IsReadOnly = false;
				colvarLattitude.DefaultSetting = @"";
				colvarLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitude);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.AnsiString;
				colvarLongitude.MaxLength = 50;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = true;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

				TableSchema.TableColumn colvarSpeed = new TableSchema.TableColumn(schema);
				colvarSpeed.ColumnName = "Speed";
				colvarSpeed.DataType = DbType.Decimal;
				colvarSpeed.MaxLength = 0;
				colvarSpeed.AutoIncrement = false;
				colvarSpeed.IsNullable = true;
				colvarSpeed.IsPrimaryKey = false;
				colvarSpeed.IsForeignKey = false;
				colvarSpeed.IsReadOnly = false;
				colvarSpeed.DefaultSetting = @"";
				colvarSpeed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSpeed);

				TableSchema.TableColumn colvarCourse = new TableSchema.TableColumn(schema);
				colvarCourse.ColumnName = "Course";
				colvarCourse.DataType = DbType.Decimal;
				colvarCourse.MaxLength = 0;
				colvarCourse.AutoIncrement = false;
				colvarCourse.IsNullable = true;
				colvarCourse.IsPrimaryKey = false;
				colvarCourse.IsForeignKey = false;
				colvarCourse.IsReadOnly = false;
				colvarCourse.DefaultSetting = @"";
				colvarCourse.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCourse);

				TableSchema.TableColumn colvarCurrentMilage = new TableSchema.TableColumn(schema);
				colvarCurrentMilage.ColumnName = "CurrentMilage";
				colvarCurrentMilage.DataType = DbType.Int32;
				colvarCurrentMilage.MaxLength = 0;
				colvarCurrentMilage.AutoIncrement = false;
				colvarCurrentMilage.IsNullable = true;
				colvarCurrentMilage.IsPrimaryKey = false;
				colvarCurrentMilage.IsForeignKey = false;
				colvarCurrentMilage.IsReadOnly = false;
				colvarCurrentMilage.DefaultSetting = @"";
				colvarCurrentMilage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentMilage);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwGS_EventsLast",schema);
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
		public GS_EventsLastView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long EventID {
			get { return GetColumnValue<long>(Columns.EventID); }
			set { SetColumnValue(Columns.EventID, value); }
		}
		[DataMember]
		public string EventTypeId {
			get { return GetColumnValue<string>(Columns.EventTypeId); }
			set { SetColumnValue(Columns.EventTypeId, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public string EventName {
			get { return GetColumnValue<string>(Columns.EventName); }
			set { SetColumnValue(Columns.EventName, value); }
		}
		[DataMember]
		public DateTime EventDate {
			get { return GetColumnValue<DateTime>(Columns.EventDate); }
			set { SetColumnValue(Columns.EventDate, value); }
		}
		[DataMember]
		public string Lattitude {
			get { return GetColumnValue<string>(Columns.Lattitude); }
			set { SetColumnValue(Columns.Lattitude, value); }
		}
		[DataMember]
		public string Longitude {
			get { return GetColumnValue<string>(Columns.Longitude); }
			set { SetColumnValue(Columns.Longitude, value); }
		}
		[DataMember]
		public decimal? Speed {
			get { return GetColumnValue<decimal?>(Columns.Speed); }
			set { SetColumnValue(Columns.Speed, value); }
		}
		[DataMember]
		public decimal? Course {
			get { return GetColumnValue<decimal?>(Columns.Course); }
			set { SetColumnValue(Columns.Course, value); }
		}
		[DataMember]
		public int? CurrentMilage {
			get { return GetColumnValue<int?>(Columns.CurrentMilage); }
			set { SetColumnValue(Columns.CurrentMilage, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return EventTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EventIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EventTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn EventNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn EventDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LattitudeColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SpeedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CourseColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CurrentMilageColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string EventID = @"EventID";
			public const string EventTypeId = @"EventTypeId";
			public const string AccountId = @"AccountId";
			public const string EventName = @"EventName";
			public const string EventDate = @"EventDate";
			public const string Lattitude = @"Lattitude";
			public const string Longitude = @"Longitude";
			public const string Speed = @"Speed";
			public const string Course = @"Course";
			public const string CurrentMilage = @"CurrentMilage";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the GS_EventTypesView class.
	/// </summary>
	[DataContract]
	public partial class GS_EventTypesViewCollection : ReadOnlyList<GS_EventTypesView, GS_EventTypesViewCollection>
	{
		public static GS_EventTypesViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_EventTypesViewCollection result = new GS_EventTypesViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwGS_EventTypes view.
	/// </summary>
	[DataContract]
	public partial class GS_EventTypesView : ReadOnlyRecord<GS_EventTypesView>
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
				TableSchema.Table schema = new TableSchema.Table("vwGS_EventTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventTypeID = new TableSchema.TableColumn(schema);
				colvarEventTypeID.ColumnName = "EventTypeID";
				colvarEventTypeID.DataType = DbType.AnsiString;
				colvarEventTypeID.MaxLength = 20;
				colvarEventTypeID.AutoIncrement = false;
				colvarEventTypeID.IsNullable = false;
				colvarEventTypeID.IsPrimaryKey = false;
				colvarEventTypeID.IsForeignKey = false;
				colvarEventTypeID.IsReadOnly = false;
				colvarEventTypeID.DefaultSetting = @"";
				colvarEventTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventTypeID);

				TableSchema.TableColumn colvarEventType = new TableSchema.TableColumn(schema);
				colvarEventType.ColumnName = "EventType";
				colvarEventType.DataType = DbType.String;
				colvarEventType.MaxLength = 50;
				colvarEventType.AutoIncrement = false;
				colvarEventType.IsNullable = false;
				colvarEventType.IsPrimaryKey = false;
				colvarEventType.IsForeignKey = false;
				colvarEventType.IsReadOnly = false;
				colvarEventType.DefaultSetting = @"";
				colvarEventType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventType);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwGS_EventTypes",schema);
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
		public GS_EventTypesView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string EventTypeID {
			get { return GetColumnValue<string>(Columns.EventTypeID); }
			set { SetColumnValue(Columns.EventTypeID, value); }
		}
		[DataMember]
		public string EventType {
			get { return GetColumnValue<string>(Columns.EventType); }
			set { SetColumnValue(Columns.EventType, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return EventType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EventTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EventTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string EventTypeID = @"EventTypeID";
			public const string EventType = @"EventType";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageAVRMCsView class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVRMCsViewCollection : ReadOnlyList<LP_CommandMessageAVRMCsView, LP_CommandMessageAVRMCsViewCollection>
	{
		public static LP_CommandMessageAVRMCsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageAVRMCsViewCollection result = new LP_CommandMessageAVRMCsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwLP_CommandMessageAVRMCs view.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVRMCsView : ReadOnlyRecord<LP_CommandMessageAVRMCsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwLP_CommandMessageAVRMCs", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = false;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarReqCommandMessageId = new TableSchema.TableColumn(schema);
				colvarReqCommandMessageId.ColumnName = "ReqCommandMessageId";
				colvarReqCommandMessageId.DataType = DbType.Int64;
				colvarReqCommandMessageId.MaxLength = 0;
				colvarReqCommandMessageId.AutoIncrement = false;
				colvarReqCommandMessageId.IsNullable = true;
				colvarReqCommandMessageId.IsPrimaryKey = false;
				colvarReqCommandMessageId.IsForeignKey = false;
				colvarReqCommandMessageId.IsReadOnly = false;
				colvarReqCommandMessageId.DefaultSetting = @"";
				colvarReqCommandMessageId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReqCommandMessageId);

				TableSchema.TableColumn colvarUnitID = new TableSchema.TableColumn(schema);
				colvarUnitID.ColumnName = "UnitID";
				colvarUnitID.DataType = DbType.Int64;
				colvarUnitID.MaxLength = 0;
				colvarUnitID.AutoIncrement = false;
				colvarUnitID.IsNullable = true;
				colvarUnitID.IsPrimaryKey = false;
				colvarUnitID.IsForeignKey = false;
				colvarUnitID.IsReadOnly = false;
				colvarUnitID.DefaultSetting = @"";
				colvarUnitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitID);

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

				TableSchema.TableColumn colvarPort = new TableSchema.TableColumn(schema);
				colvarPort.ColumnName = "Port";
				colvarPort.DataType = DbType.Int32;
				colvarPort.MaxLength = 0;
				colvarPort.AutoIncrement = false;
				colvarPort.IsNullable = true;
				colvarPort.IsPrimaryKey = false;
				colvarPort.IsForeignKey = false;
				colvarPort.IsReadOnly = false;
				colvarPort.DefaultSetting = @"";
				colvarPort.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPort);

				TableSchema.TableColumn colvarUTCDateTime = new TableSchema.TableColumn(schema);
				colvarUTCDateTime.ColumnName = "UTCDateTime";
				colvarUTCDateTime.DataType = DbType.DateTime;
				colvarUTCDateTime.MaxLength = 0;
				colvarUTCDateTime.AutoIncrement = false;
				colvarUTCDateTime.IsNullable = true;
				colvarUTCDateTime.IsPrimaryKey = false;
				colvarUTCDateTime.IsForeignKey = false;
				colvarUTCDateTime.IsReadOnly = false;
				colvarUTCDateTime.DefaultSetting = @"";
				colvarUTCDateTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUTCDateTime);

				TableSchema.TableColumn colvarDeviceStatusId = new TableSchema.TableColumn(schema);
				colvarDeviceStatusId.ColumnName = "DeviceStatusId";
				colvarDeviceStatusId.DataType = DbType.AnsiString;
				colvarDeviceStatusId.MaxLength = 3;
				colvarDeviceStatusId.AutoIncrement = false;
				colvarDeviceStatusId.IsNullable = true;
				colvarDeviceStatusId.IsPrimaryKey = false;
				colvarDeviceStatusId.IsForeignKey = false;
				colvarDeviceStatusId.IsReadOnly = false;
				colvarDeviceStatusId.DefaultSetting = @"";
				colvarDeviceStatusId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceStatusId);

				TableSchema.TableColumn colvarDeviceStatus = new TableSchema.TableColumn(schema);
				colvarDeviceStatus.ColumnName = "DeviceStatus";
				colvarDeviceStatus.DataType = DbType.AnsiString;
				colvarDeviceStatus.MaxLength = 50;
				colvarDeviceStatus.AutoIncrement = false;
				colvarDeviceStatus.IsNullable = true;
				colvarDeviceStatus.IsPrimaryKey = false;
				colvarDeviceStatus.IsForeignKey = false;
				colvarDeviceStatus.IsReadOnly = false;
				colvarDeviceStatus.DefaultSetting = @"";
				colvarDeviceStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceStatus);

				TableSchema.TableColumn colvarLatitude = new TableSchema.TableColumn(schema);
				colvarLatitude.ColumnName = "Latitude";
				colvarLatitude.DataType = DbType.Double;
				colvarLatitude.MaxLength = 0;
				colvarLatitude.AutoIncrement = false;
				colvarLatitude.IsNullable = true;
				colvarLatitude.IsPrimaryKey = false;
				colvarLatitude.IsForeignKey = false;
				colvarLatitude.IsReadOnly = false;
				colvarLatitude.DefaultSetting = @"";
				colvarLatitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLatitude);

				TableSchema.TableColumn colvarNSIndicator = new TableSchema.TableColumn(schema);
				colvarNSIndicator.ColumnName = "NSIndicator";
				colvarNSIndicator.DataType = DbType.AnsiStringFixedLength;
				colvarNSIndicator.MaxLength = 1;
				colvarNSIndicator.AutoIncrement = false;
				colvarNSIndicator.IsNullable = true;
				colvarNSIndicator.IsPrimaryKey = false;
				colvarNSIndicator.IsForeignKey = false;
				colvarNSIndicator.IsReadOnly = false;
				colvarNSIndicator.DefaultSetting = @"";
				colvarNSIndicator.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNSIndicator);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.Double;
				colvarLongitude.MaxLength = 0;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = true;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

				TableSchema.TableColumn colvarEWIndicator = new TableSchema.TableColumn(schema);
				colvarEWIndicator.ColumnName = "EWIndicator";
				colvarEWIndicator.DataType = DbType.AnsiStringFixedLength;
				colvarEWIndicator.MaxLength = 1;
				colvarEWIndicator.AutoIncrement = false;
				colvarEWIndicator.IsNullable = true;
				colvarEWIndicator.IsPrimaryKey = false;
				colvarEWIndicator.IsForeignKey = false;
				colvarEWIndicator.IsReadOnly = false;
				colvarEWIndicator.DefaultSetting = @"";
				colvarEWIndicator.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEWIndicator);

				TableSchema.TableColumn colvarSpeed = new TableSchema.TableColumn(schema);
				colvarSpeed.ColumnName = "Speed";
				colvarSpeed.DataType = DbType.Decimal;
				colvarSpeed.MaxLength = 0;
				colvarSpeed.AutoIncrement = false;
				colvarSpeed.IsNullable = true;
				colvarSpeed.IsPrimaryKey = false;
				colvarSpeed.IsForeignKey = false;
				colvarSpeed.IsReadOnly = false;
				colvarSpeed.DefaultSetting = @"";
				colvarSpeed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSpeed);

				TableSchema.TableColumn colvarCourse = new TableSchema.TableColumn(schema);
				colvarCourse.ColumnName = "Course";
				colvarCourse.DataType = DbType.Decimal;
				colvarCourse.MaxLength = 0;
				colvarCourse.AutoIncrement = false;
				colvarCourse.IsNullable = true;
				colvarCourse.IsPrimaryKey = false;
				colvarCourse.IsForeignKey = false;
				colvarCourse.IsReadOnly = false;
				colvarCourse.DefaultSetting = @"";
				colvarCourse.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCourse);

				TableSchema.TableColumn colvarEventCodeId = new TableSchema.TableColumn(schema);
				colvarEventCodeId.ColumnName = "EventCodeId";
				colvarEventCodeId.DataType = DbType.AnsiString;
				colvarEventCodeId.MaxLength = 3;
				colvarEventCodeId.AutoIncrement = false;
				colvarEventCodeId.IsNullable = true;
				colvarEventCodeId.IsPrimaryKey = false;
				colvarEventCodeId.IsForeignKey = false;
				colvarEventCodeId.IsReadOnly = false;
				colvarEventCodeId.DefaultSetting = @"";
				colvarEventCodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventCodeId);

				TableSchema.TableColumn colvarEventCode = new TableSchema.TableColumn(schema);
				colvarEventCode.ColumnName = "EventCode";
				colvarEventCode.DataType = DbType.AnsiString;
				colvarEventCode.MaxLength = 50;
				colvarEventCode.AutoIncrement = false;
				colvarEventCode.IsNullable = true;
				colvarEventCode.IsPrimaryKey = false;
				colvarEventCode.IsForeignKey = false;
				colvarEventCode.IsReadOnly = false;
				colvarEventCode.DefaultSetting = @"";
				colvarEventCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventCode);

				TableSchema.TableColumn colvarBatteryVoltage = new TableSchema.TableColumn(schema);
				colvarBatteryVoltage.ColumnName = "BatteryVoltage";
				colvarBatteryVoltage.DataType = DbType.Int32;
				colvarBatteryVoltage.MaxLength = 0;
				colvarBatteryVoltage.AutoIncrement = false;
				colvarBatteryVoltage.IsNullable = true;
				colvarBatteryVoltage.IsPrimaryKey = false;
				colvarBatteryVoltage.IsForeignKey = false;
				colvarBatteryVoltage.IsReadOnly = false;
				colvarBatteryVoltage.DefaultSetting = @"";
				colvarBatteryVoltage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBatteryVoltage);

				TableSchema.TableColumn colvarCurrentMilage = new TableSchema.TableColumn(schema);
				colvarCurrentMilage.ColumnName = "CurrentMilage";
				colvarCurrentMilage.DataType = DbType.Int32;
				colvarCurrentMilage.MaxLength = 0;
				colvarCurrentMilage.AutoIncrement = false;
				colvarCurrentMilage.IsNullable = true;
				colvarCurrentMilage.IsPrimaryKey = false;
				colvarCurrentMilage.IsForeignKey = false;
				colvarCurrentMilage.IsReadOnly = false;
				colvarCurrentMilage.DefaultSetting = @"";
				colvarCurrentMilage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentMilage);

				TableSchema.TableColumn colvarGPSStatus = new TableSchema.TableColumn(schema);
				colvarGPSStatus.ColumnName = "GPSStatus";
				colvarGPSStatus.DataType = DbType.Boolean;
				colvarGPSStatus.MaxLength = 0;
				colvarGPSStatus.AutoIncrement = false;
				colvarGPSStatus.IsNullable = true;
				colvarGPSStatus.IsPrimaryKey = false;
				colvarGPSStatus.IsForeignKey = false;
				colvarGPSStatus.IsReadOnly = false;
				colvarGPSStatus.DefaultSetting = @"";
				colvarGPSStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPSStatus);

				TableSchema.TableColumn colvarAnalogPort1 = new TableSchema.TableColumn(schema);
				colvarAnalogPort1.ColumnName = "AnalogPort1";
				colvarAnalogPort1.DataType = DbType.Boolean;
				colvarAnalogPort1.MaxLength = 0;
				colvarAnalogPort1.AutoIncrement = false;
				colvarAnalogPort1.IsNullable = true;
				colvarAnalogPort1.IsPrimaryKey = false;
				colvarAnalogPort1.IsForeignKey = false;
				colvarAnalogPort1.IsReadOnly = false;
				colvarAnalogPort1.DefaultSetting = @"";
				colvarAnalogPort1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnalogPort1);

				TableSchema.TableColumn colvarAnalogPort2 = new TableSchema.TableColumn(schema);
				colvarAnalogPort2.ColumnName = "AnalogPort2";
				colvarAnalogPort2.DataType = DbType.Boolean;
				colvarAnalogPort2.MaxLength = 0;
				colvarAnalogPort2.AutoIncrement = false;
				colvarAnalogPort2.IsNullable = true;
				colvarAnalogPort2.IsPrimaryKey = false;
				colvarAnalogPort2.IsForeignKey = false;
				colvarAnalogPort2.IsReadOnly = false;
				colvarAnalogPort2.DefaultSetting = @"";
				colvarAnalogPort2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnalogPort2);

				TableSchema.TableColumn colvarProcessedDate = new TableSchema.TableColumn(schema);
				colvarProcessedDate.ColumnName = "ProcessedDate";
				colvarProcessedDate.DataType = DbType.DateTime;
				colvarProcessedDate.MaxLength = 0;
				colvarProcessedDate.AutoIncrement = false;
				colvarProcessedDate.IsNullable = true;
				colvarProcessedDate.IsPrimaryKey = false;
				colvarProcessedDate.IsForeignKey = false;
				colvarProcessedDate.IsReadOnly = false;
				colvarProcessedDate.DefaultSetting = @"";
				colvarProcessedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessedDate);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwLP_CommandMessageAVRMCs",schema);
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
		public LP_CommandMessageAVRMCsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set { SetColumnValue(Columns.CommandMessageID, value); }
		}
		[DataMember]
		public long? ReqCommandMessageId {
			get { return GetColumnValue<long?>(Columns.ReqCommandMessageId); }
			set { SetColumnValue(Columns.ReqCommandMessageId, value); }
		}
		[DataMember]
		public long? UnitID {
			get { return GetColumnValue<long?>(Columns.UnitID); }
			set { SetColumnValue(Columns.UnitID, value); }
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set { SetColumnValue(Columns.IPAddress, value); }
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set { SetColumnValue(Columns.Port, value); }
		}
		[DataMember]
		public DateTime? UTCDateTime {
			get { return GetColumnValue<DateTime?>(Columns.UTCDateTime); }
			set { SetColumnValue(Columns.UTCDateTime, value); }
		}
		[DataMember]
		public string DeviceStatusId {
			get { return GetColumnValue<string>(Columns.DeviceStatusId); }
			set { SetColumnValue(Columns.DeviceStatusId, value); }
		}
		[DataMember]
		public string DeviceStatus {
			get { return GetColumnValue<string>(Columns.DeviceStatus); }
			set { SetColumnValue(Columns.DeviceStatus, value); }
		}
		[DataMember]
		public double? Latitude {
			get { return GetColumnValue<double?>(Columns.Latitude); }
			set { SetColumnValue(Columns.Latitude, value); }
		}
		[DataMember]
		public string NSIndicator {
			get { return GetColumnValue<string>(Columns.NSIndicator); }
			set { SetColumnValue(Columns.NSIndicator, value); }
		}
		[DataMember]
		public double? Longitude {
			get { return GetColumnValue<double?>(Columns.Longitude); }
			set { SetColumnValue(Columns.Longitude, value); }
		}
		[DataMember]
		public string EWIndicator {
			get { return GetColumnValue<string>(Columns.EWIndicator); }
			set { SetColumnValue(Columns.EWIndicator, value); }
		}
		[DataMember]
		public decimal? Speed {
			get { return GetColumnValue<decimal?>(Columns.Speed); }
			set { SetColumnValue(Columns.Speed, value); }
		}
		[DataMember]
		public decimal? Course {
			get { return GetColumnValue<decimal?>(Columns.Course); }
			set { SetColumnValue(Columns.Course, value); }
		}
		[DataMember]
		public string EventCodeId {
			get { return GetColumnValue<string>(Columns.EventCodeId); }
			set { SetColumnValue(Columns.EventCodeId, value); }
		}
		[DataMember]
		public string EventCode {
			get { return GetColumnValue<string>(Columns.EventCode); }
			set { SetColumnValue(Columns.EventCode, value); }
		}
		[DataMember]
		public int? BatteryVoltage {
			get { return GetColumnValue<int?>(Columns.BatteryVoltage); }
			set { SetColumnValue(Columns.BatteryVoltage, value); }
		}
		[DataMember]
		public int? CurrentMilage {
			get { return GetColumnValue<int?>(Columns.CurrentMilage); }
			set { SetColumnValue(Columns.CurrentMilage, value); }
		}
		[DataMember]
		public bool? GPSStatus {
			get { return GetColumnValue<bool?>(Columns.GPSStatus); }
			set { SetColumnValue(Columns.GPSStatus, value); }
		}
		[DataMember]
		public bool? AnalogPort1 {
			get { return GetColumnValue<bool?>(Columns.AnalogPort1); }
			set { SetColumnValue(Columns.AnalogPort1, value); }
		}
		[DataMember]
		public bool? AnalogPort2 {
			get { return GetColumnValue<bool?>(Columns.AnalogPort2); }
			set { SetColumnValue(Columns.AnalogPort2, value); }
		}
		[DataMember]
		public DateTime? ProcessedDate {
			get { return GetColumnValue<DateTime?>(Columns.ProcessedDate); }
			set { SetColumnValue(Columns.ProcessedDate, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set { SetColumnValue(Columns.DEX_ROW_TS, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ReqCommandMessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn UTCDateTimeColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn DeviceStatusIdColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DeviceStatusColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LatitudeColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn NSIndicatorColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn EWIndicatorColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn SpeedColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CourseColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn EventCodeIdColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn EventCodeColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn BatteryVoltageColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CurrentMilageColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn GPSStatusColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn AnalogPort1Column
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn AnalogPort2Column
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn ProcessedDateColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[23]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CommandMessageID = @"CommandMessageID";
			public const string ReqCommandMessageId = @"ReqCommandMessageId";
			public const string UnitID = @"UnitID";
			public const string IPAddress = @"IPAddress";
			public const string Port = @"Port";
			public const string UTCDateTime = @"UTCDateTime";
			public const string DeviceStatusId = @"DeviceStatusId";
			public const string DeviceStatus = @"DeviceStatus";
			public const string Latitude = @"Latitude";
			public const string NSIndicator = @"NSIndicator";
			public const string Longitude = @"Longitude";
			public const string EWIndicator = @"EWIndicator";
			public const string Speed = @"Speed";
			public const string Course = @"Course";
			public const string EventCodeId = @"EventCodeId";
			public const string EventCode = @"EventCode";
			public const string BatteryVoltage = @"BatteryVoltage";
			public const string CurrentMilage = @"CurrentMilage";
			public const string GPSStatus = @"GPSStatus";
			public const string AnalogPort1 = @"AnalogPort1";
			public const string AnalogPort2 = @"AnalogPort2";
			public const string ProcessedDate = @"ProcessedDate";
			public const string CreatedOn = @"CreatedOn";
			public const string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVRSP4sView class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP4sViewCollection : ReadOnlyList<LP_CommandMessageEAVRSP4sView, LP_CommandMessageEAVRSP4sViewCollection>
	{
		public static LP_CommandMessageEAVRSP4sViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVRSP4sViewCollection result = new LP_CommandMessageEAVRSP4sViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwLP_CommandMessageEAVRSP4s view.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP4sView : ReadOnlyRecord<LP_CommandMessageEAVRSP4sView>
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
				TableSchema.Table schema = new TableSchema.Table("vwLP_CommandMessageEAVRSP4s", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = false;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarLPGeoFenceId = new TableSchema.TableColumn(schema);
				colvarLPGeoFenceId.ColumnName = "LPGeoFenceId";
				colvarLPGeoFenceId.DataType = DbType.Int64;
				colvarLPGeoFenceId.MaxLength = 0;
				colvarLPGeoFenceId.AutoIncrement = false;
				colvarLPGeoFenceId.IsNullable = true;
				colvarLPGeoFenceId.IsPrimaryKey = false;
				colvarLPGeoFenceId.IsForeignKey = false;
				colvarLPGeoFenceId.IsReadOnly = false;
				colvarLPGeoFenceId.DefaultSetting = @"";
				colvarLPGeoFenceId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLPGeoFenceId);

				TableSchema.TableColumn colvarUnitID = new TableSchema.TableColumn(schema);
				colvarUnitID.ColumnName = "UnitID";
				colvarUnitID.DataType = DbType.Int64;
				colvarUnitID.MaxLength = 0;
				colvarUnitID.AutoIncrement = false;
				colvarUnitID.IsNullable = false;
				colvarUnitID.IsPrimaryKey = false;
				colvarUnitID.IsForeignKey = false;
				colvarUnitID.IsReadOnly = false;
				colvarUnitID.DefaultSetting = @"";
				colvarUnitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitID);

				TableSchema.TableColumn colvarGeoFenceI = new TableSchema.TableColumn(schema);
				colvarGeoFenceI.ColumnName = "GeoFenceI";
				colvarGeoFenceI.DataType = DbType.Byte;
				colvarGeoFenceI.MaxLength = 0;
				colvarGeoFenceI.AutoIncrement = false;
				colvarGeoFenceI.IsNullable = false;
				colvarGeoFenceI.IsPrimaryKey = false;
				colvarGeoFenceI.IsForeignKey = false;
				colvarGeoFenceI.IsReadOnly = false;
				colvarGeoFenceI.DefaultSetting = @"";
				colvarGeoFenceI.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceI);

				TableSchema.TableColumn colvarReportModeI = new TableSchema.TableColumn(schema);
				colvarReportModeI.ColumnName = "ReportModeI";
				colvarReportModeI.DataType = DbType.AnsiStringFixedLength;
				colvarReportModeI.MaxLength = 1;
				colvarReportModeI.AutoIncrement = false;
				colvarReportModeI.IsNullable = false;
				colvarReportModeI.IsPrimaryKey = false;
				colvarReportModeI.IsForeignKey = false;
				colvarReportModeI.IsReadOnly = false;
				colvarReportModeI.DefaultSetting = @"";
				colvarReportModeI.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeI);

				TableSchema.TableColumn colvarLattitudeI1 = new TableSchema.TableColumn(schema);
				colvarLattitudeI1.ColumnName = "LattitudeI1";
				colvarLattitudeI1.DataType = DbType.Double;
				colvarLattitudeI1.MaxLength = 0;
				colvarLattitudeI1.AutoIncrement = false;
				colvarLattitudeI1.IsNullable = false;
				colvarLattitudeI1.IsPrimaryKey = false;
				colvarLattitudeI1.IsForeignKey = false;
				colvarLattitudeI1.IsReadOnly = false;
				colvarLattitudeI1.DefaultSetting = @"";
				colvarLattitudeI1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeI1);

				TableSchema.TableColumn colvarLongitudeI1 = new TableSchema.TableColumn(schema);
				colvarLongitudeI1.ColumnName = "LongitudeI1";
				colvarLongitudeI1.DataType = DbType.Double;
				colvarLongitudeI1.MaxLength = 0;
				colvarLongitudeI1.AutoIncrement = false;
				colvarLongitudeI1.IsNullable = false;
				colvarLongitudeI1.IsPrimaryKey = false;
				colvarLongitudeI1.IsForeignKey = false;
				colvarLongitudeI1.IsReadOnly = false;
				colvarLongitudeI1.DefaultSetting = @"";
				colvarLongitudeI1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeI1);

				TableSchema.TableColumn colvarLattitudeI2 = new TableSchema.TableColumn(schema);
				colvarLattitudeI2.ColumnName = "LattitudeI2";
				colvarLattitudeI2.DataType = DbType.Double;
				colvarLattitudeI2.MaxLength = 0;
				colvarLattitudeI2.AutoIncrement = false;
				colvarLattitudeI2.IsNullable = false;
				colvarLattitudeI2.IsPrimaryKey = false;
				colvarLattitudeI2.IsForeignKey = false;
				colvarLattitudeI2.IsReadOnly = false;
				colvarLattitudeI2.DefaultSetting = @"";
				colvarLattitudeI2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeI2);

				TableSchema.TableColumn colvarLongitudeI2 = new TableSchema.TableColumn(schema);
				colvarLongitudeI2.ColumnName = "LongitudeI2";
				colvarLongitudeI2.DataType = DbType.Double;
				colvarLongitudeI2.MaxLength = 0;
				colvarLongitudeI2.AutoIncrement = false;
				colvarLongitudeI2.IsNullable = false;
				colvarLongitudeI2.IsPrimaryKey = false;
				colvarLongitudeI2.IsForeignKey = false;
				colvarLongitudeI2.IsReadOnly = false;
				colvarLongitudeI2.DefaultSetting = @"";
				colvarLongitudeI2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeI2);

				TableSchema.TableColumn colvarProcessed = new TableSchema.TableColumn(schema);
				colvarProcessed.ColumnName = "Processed";
				colvarProcessed.DataType = DbType.DateTime;
				colvarProcessed.MaxLength = 0;
				colvarProcessed.AutoIncrement = false;
				colvarProcessed.IsNullable = true;
				colvarProcessed.IsPrimaryKey = false;
				colvarProcessed.IsForeignKey = false;
				colvarProcessed.IsReadOnly = false;
				colvarProcessed.DefaultSetting = @"";
				colvarProcessed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessed);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("vwLP_CommandMessageEAVRSP4s",schema);
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
		public LP_CommandMessageEAVRSP4sView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set { SetColumnValue(Columns.CommandMessageID, value); }
		}
		[DataMember]
		public long? LPGeoFenceId {
			get { return GetColumnValue<long?>(Columns.LPGeoFenceId); }
			set { SetColumnValue(Columns.LPGeoFenceId, value); }
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set { SetColumnValue(Columns.UnitID, value); }
		}
		[DataMember]
		public byte GeoFenceI {
			get { return GetColumnValue<byte>(Columns.GeoFenceI); }
			set { SetColumnValue(Columns.GeoFenceI, value); }
		}
		[DataMember]
		public string ReportModeI {
			get { return GetColumnValue<string>(Columns.ReportModeI); }
			set { SetColumnValue(Columns.ReportModeI, value); }
		}
		[DataMember]
		public double LattitudeI1 {
			get { return GetColumnValue<double>(Columns.LattitudeI1); }
			set { SetColumnValue(Columns.LattitudeI1, value); }
		}
		[DataMember]
		public double LongitudeI1 {
			get { return GetColumnValue<double>(Columns.LongitudeI1); }
			set { SetColumnValue(Columns.LongitudeI1, value); }
		}
		[DataMember]
		public double LattitudeI2 {
			get { return GetColumnValue<double>(Columns.LattitudeI2); }
			set { SetColumnValue(Columns.LattitudeI2, value); }
		}
		[DataMember]
		public double LongitudeI2 {
			get { return GetColumnValue<double>(Columns.LongitudeI2); }
			set { SetColumnValue(Columns.LongitudeI2, value); }
		}
		[DataMember]
		public DateTime? Processed {
			get { return GetColumnValue<DateTime?>(Columns.Processed); }
			set { SetColumnValue(Columns.Processed, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set { SetColumnValue(Columns.DEX_ROW_TS, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn LPGeoFenceIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn GeoFenceIColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ReportModeIColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LattitudeI1Column
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LongitudeI1Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LattitudeI2Column
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LongitudeI2Column
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ProcessedColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CommandMessageID = @"CommandMessageID";
			public const string LPGeoFenceId = @"LPGeoFenceId";
			public const string UnitID = @"UnitID";
			public const string GeoFenceI = @"GeoFenceI";
			public const string ReportModeI = @"ReportModeI";
			public const string LattitudeI1 = @"LattitudeI1";
			public const string LongitudeI1 = @"LongitudeI1";
			public const string LattitudeI2 = @"LattitudeI2";
			public const string LongitudeI2 = @"LongitudeI2";
			public const string Processed = @"Processed";
			public const string CreatedOn = @"CreatedOn";
			public const string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct
	}
}
