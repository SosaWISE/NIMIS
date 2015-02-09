


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

namespace SOS.Data.GpsTracking
{
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFenceCircle class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceCircleCollection : ActiveList<GS_AccountGeoFenceCircle, GS_AccountGeoFenceCircleCollection>
	{
		public static GS_AccountGeoFenceCircleCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFenceCircleCollection result = new GS_AccountGeoFenceCircleCollection();
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
			foreach (GS_AccountGeoFenceCircle item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFenceCircles table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceCircle : ActiveRecord<GS_AccountGeoFenceCircle>, INotifyPropertyChanged
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

		public GS_AccountGeoFenceCircle()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFenceCircles", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = false;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = true;
				colvarGeoFenceID.IsForeignKey = false;
				colvarGeoFenceID.IsReadOnly = false;
				colvarGeoFenceID.DefaultSetting = @"";
				colvarGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceID);

				TableSchema.TableColumn colvarCenterLattitude = new TableSchema.TableColumn(schema);
				colvarCenterLattitude.ColumnName = "CenterLattitude";
				colvarCenterLattitude.DataType = DbType.Double;
				colvarCenterLattitude.MaxLength = 0;
				colvarCenterLattitude.AutoIncrement = false;
				colvarCenterLattitude.IsNullable = false;
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
				colvarCenterLongitude.IsNullable = false;
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
				colvarRadius.IsNullable = false;
				colvarRadius.IsPrimaryKey = false;
				colvarRadius.IsForeignKey = false;
				colvarRadius.IsReadOnly = false;
				colvarRadius.DefaultSetting = @"";
				colvarRadius.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRadius);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFenceCircles",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFenceCircle LoadFrom(GS_AccountGeoFenceCircle item)
		{
			GS_AccountGeoFenceCircle result = new GS_AccountGeoFenceCircle();
			if (item.GeoFenceID != default(long)) {
				result.LoadByKey(item.GeoFenceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set {
				SetColumnValue(Columns.GeoFenceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceID));
			}
		}
		[DataMember]
		public double CenterLattitude {
			get { return GetColumnValue<double>(Columns.CenterLattitude); }
			set {
				SetColumnValue(Columns.CenterLattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CenterLattitude));
			}
		}
		[DataMember]
		public double CenterLongitude {
			get { return GetColumnValue<double>(Columns.CenterLongitude); }
			set {
				SetColumnValue(Columns.CenterLongitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CenterLongitude));
			}
		}
		[DataMember]
		public double Radius {
			get { return GetColumnValue<double>(Columns.Radius); }
			set {
				SetColumnValue(Columns.Radius, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Radius));
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
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFence _GeoFence;
		//Relationship: FK_GS_AccountGeoFenceCircles_GS_AccountGeoFences
		public GS_AccountGeoFence GeoFence
		{
			get
			{
				if(_GeoFence == null) {
					_GeoFence = GS_AccountGeoFence.FetchByID(this.GeoFenceID);
				}
				return _GeoFence;
			}
			set
			{
				SetColumnValue("GeoFenceID", value.GeoFenceID);
				_GeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return GeoFenceID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CenterLattitudeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CenterLongitudeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RadiusColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFenceID = @"GeoFenceID";
			public static readonly string CenterLattitude = @"CenterLattitude";
			public static readonly string CenterLongitude = @"CenterLongitude";
			public static readonly string Radius = @"Radius";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFenceID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFencePoint class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePointCollection : ActiveList<GS_AccountGeoFencePoint, GS_AccountGeoFencePointCollection>
	{
		public static GS_AccountGeoFencePointCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFencePointCollection result = new GS_AccountGeoFencePointCollection();
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
			foreach (GS_AccountGeoFencePoint item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFencePoints table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePoint : ActiveRecord<GS_AccountGeoFencePoint>, INotifyPropertyChanged
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

		public GS_AccountGeoFencePoint()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFencePoints", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = false;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = true;
				colvarGeoFenceID.IsForeignKey = false;
				colvarGeoFenceID.IsReadOnly = false;
				colvarGeoFenceID.DefaultSetting = @"";
				colvarGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceID);

				TableSchema.TableColumn colvarPlaceName = new TableSchema.TableColumn(schema);
				colvarPlaceName.ColumnName = "PlaceName";
				colvarPlaceName.DataType = DbType.String;
				colvarPlaceName.MaxLength = 500;
				colvarPlaceName.AutoIncrement = false;
				colvarPlaceName.IsNullable = false;
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

				TableSchema.TableColumn colvarLattitude = new TableSchema.TableColumn(schema);
				colvarLattitude.ColumnName = "Lattitude";
				colvarLattitude.DataType = DbType.Double;
				colvarLattitude.MaxLength = 0;
				colvarLattitude.AutoIncrement = false;
				colvarLattitude.IsNullable = false;
				colvarLattitude.IsPrimaryKey = false;
				colvarLattitude.IsForeignKey = false;
				colvarLattitude.IsReadOnly = false;
				colvarLattitude.DefaultSetting = @"";
				colvarLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitude);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.Double;
				colvarLongitude.MaxLength = 0;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = false;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFencePoints",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFencePoint LoadFrom(GS_AccountGeoFencePoint item)
		{
			GS_AccountGeoFencePoint result = new GS_AccountGeoFencePoint();
			if (item.GeoFenceID != default(long)) {
				result.LoadByKey(item.GeoFenceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set {
				SetColumnValue(Columns.GeoFenceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceID));
			}
		}
		[DataMember]
		public string PlaceName {
			get { return GetColumnValue<string>(Columns.PlaceName); }
			set {
				SetColumnValue(Columns.PlaceName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PlaceName));
			}
		}
		[DataMember]
		public string PlaceDescription {
			get { return GetColumnValue<string>(Columns.PlaceDescription); }
			set {
				SetColumnValue(Columns.PlaceDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PlaceDescription));
			}
		}
		[DataMember]
		public double Lattitude {
			get { return GetColumnValue<double>(Columns.Lattitude); }
			set {
				SetColumnValue(Columns.Lattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Lattitude));
			}
		}
		[DataMember]
		public double Longitude {
			get { return GetColumnValue<double>(Columns.Longitude); }
			set {
				SetColumnValue(Columns.Longitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Longitude));
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
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFence _GeoFence;
		//Relationship: FK_GS_AccountGeoFencePoints_GS_AccountGeoFences
		public GS_AccountGeoFence GeoFence
		{
			get
			{
				if(_GeoFence == null) {
					_GeoFence = GS_AccountGeoFence.FetchByID(this.GeoFenceID);
				}
				return _GeoFence;
			}
			set
			{
				SetColumnValue("GeoFenceID", value.GeoFenceID);
				_GeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PlaceName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PlaceNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PlaceDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LattitudeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFenceID = @"GeoFenceID";
			public static readonly string PlaceName = @"PlaceName";
			public static readonly string PlaceDescription = @"PlaceDescription";
			public static readonly string Lattitude = @"Lattitude";
			public static readonly string Longitude = @"Longitude";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFenceID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFencePolygon class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePolygonCollection : ActiveList<GS_AccountGeoFencePolygon, GS_AccountGeoFencePolygonCollection>
	{
		public static GS_AccountGeoFencePolygonCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFencePolygonCollection result = new GS_AccountGeoFencePolygonCollection();
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
			foreach (GS_AccountGeoFencePolygon item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFencePolygons table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFencePolygon : ActiveRecord<GS_AccountGeoFencePolygon>, INotifyPropertyChanged
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

		public GS_AccountGeoFencePolygon()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFencePolygons", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFencePolygonID = new TableSchema.TableColumn(schema);
				colvarGeoFencePolygonID.ColumnName = "GeoFencePolygonID";
				colvarGeoFencePolygonID.DataType = DbType.Int64;
				colvarGeoFencePolygonID.MaxLength = 0;
				colvarGeoFencePolygonID.AutoIncrement = true;
				colvarGeoFencePolygonID.IsNullable = false;
				colvarGeoFencePolygonID.IsPrimaryKey = true;
				colvarGeoFencePolygonID.IsForeignKey = false;
				colvarGeoFencePolygonID.IsReadOnly = false;
				colvarGeoFencePolygonID.DefaultSetting = @"";
				colvarGeoFencePolygonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFencePolygonID);

				TableSchema.TableColumn colvarGeoFenceId = new TableSchema.TableColumn(schema);
				colvarGeoFenceId.ColumnName = "GeoFenceId";
				colvarGeoFenceId.DataType = DbType.Int64;
				colvarGeoFenceId.MaxLength = 0;
				colvarGeoFenceId.AutoIncrement = false;
				colvarGeoFenceId.IsNullable = false;
				colvarGeoFenceId.IsPrimaryKey = false;
				colvarGeoFenceId.IsForeignKey = true;
				colvarGeoFenceId.IsReadOnly = false;
				colvarGeoFenceId.DefaultSetting = @"";
				colvarGeoFenceId.ForeignKeyTableName = "GS_AccountGeoFences";
				schema.Columns.Add(colvarGeoFenceId);

				TableSchema.TableColumn colvarSequence = new TableSchema.TableColumn(schema);
				colvarSequence.ColumnName = "Sequence";
				colvarSequence.DataType = DbType.Int32;
				colvarSequence.MaxLength = 0;
				colvarSequence.AutoIncrement = false;
				colvarSequence.IsNullable = false;
				colvarSequence.IsPrimaryKey = false;
				colvarSequence.IsForeignKey = false;
				colvarSequence.IsReadOnly = false;
				colvarSequence.DefaultSetting = @"((1))";
				colvarSequence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSequence);

				TableSchema.TableColumn colvarLattitude = new TableSchema.TableColumn(schema);
				colvarLattitude.ColumnName = "Lattitude";
				colvarLattitude.DataType = DbType.Double;
				colvarLattitude.MaxLength = 0;
				colvarLattitude.AutoIncrement = false;
				colvarLattitude.IsNullable = false;
				colvarLattitude.IsPrimaryKey = false;
				colvarLattitude.IsForeignKey = false;
				colvarLattitude.IsReadOnly = false;
				colvarLattitude.DefaultSetting = @"";
				colvarLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitude);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.Double;
				colvarLongitude.MaxLength = 0;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = false;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFencePolygons",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFencePolygon LoadFrom(GS_AccountGeoFencePolygon item)
		{
			GS_AccountGeoFencePolygon result = new GS_AccountGeoFencePolygon();
			if (item.GeoFencePolygonID != default(long)) {
				result.LoadByKey(item.GeoFencePolygonID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long GeoFencePolygonID {
			get { return GetColumnValue<long>(Columns.GeoFencePolygonID); }
			set {
				SetColumnValue(Columns.GeoFencePolygonID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFencePolygonID));
			}
		}
		[DataMember]
		public long GeoFenceId {
			get { return GetColumnValue<long>(Columns.GeoFenceId); }
			set {
				SetColumnValue(Columns.GeoFenceId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceId));
			}
		}
		[DataMember]
		public int Sequence {
			get { return GetColumnValue<int>(Columns.Sequence); }
			set {
				SetColumnValue(Columns.Sequence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sequence));
			}
		}
		[DataMember]
		public double Lattitude {
			get { return GetColumnValue<double>(Columns.Lattitude); }
			set {
				SetColumnValue(Columns.Lattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Lattitude));
			}
		}
		[DataMember]
		public double Longitude {
			get { return GetColumnValue<double>(Columns.Longitude); }
			set {
				SetColumnValue(Columns.Longitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Longitude));
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
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFence _GeoFence;
		//Relationship: FK_GS_AccountGeoFencePolygons_GS_AccountGeoFences
		public GS_AccountGeoFence GeoFence
		{
			get
			{
				if(_GeoFence == null) {
					_GeoFence = GS_AccountGeoFence.FetchByID(this.GeoFenceId);
				}
				return _GeoFence;
			}
			set
			{
				SetColumnValue("GeoFenceId", value.GeoFenceID);
				_GeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return GeoFencePolygonID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFencePolygonIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GeoFenceIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SequenceColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LattitudeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFencePolygonID = @"GeoFencePolygonID";
			public static readonly string GeoFenceId = @"GeoFenceId";
			public static readonly string Sequence = @"Sequence";
			public static readonly string Lattitude = @"Lattitude";
			public static readonly string Longitude = @"Longitude";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFencePolygonID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFenceRectangle class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceRectangleCollection : ActiveList<GS_AccountGeoFenceRectangle, GS_AccountGeoFenceRectangleCollection>
	{
		public static GS_AccountGeoFenceRectangleCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFenceRectangleCollection result = new GS_AccountGeoFenceRectangleCollection();
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
			foreach (GS_AccountGeoFenceRectangle item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFenceRectangles table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceRectangle : ActiveRecord<GS_AccountGeoFenceRectangle>, INotifyPropertyChanged
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

		public GS_AccountGeoFenceRectangle()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFenceRectangles", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = false;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = true;
				colvarGeoFenceID.IsForeignKey = false;
				colvarGeoFenceID.IsReadOnly = false;
				colvarGeoFenceID.DefaultSetting = @"";
				colvarGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceID);

				TableSchema.TableColumn colvarMaxLattitude = new TableSchema.TableColumn(schema);
				colvarMaxLattitude.ColumnName = "MaxLattitude";
				colvarMaxLattitude.DataType = DbType.Double;
				colvarMaxLattitude.MaxLength = 0;
				colvarMaxLattitude.AutoIncrement = false;
				colvarMaxLattitude.IsNullable = false;
				colvarMaxLattitude.IsPrimaryKey = false;
				colvarMaxLattitude.IsForeignKey = false;
				colvarMaxLattitude.IsReadOnly = false;
				colvarMaxLattitude.DefaultSetting = @"";
				colvarMaxLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaxLattitude);

				TableSchema.TableColumn colvarMinLongitude = new TableSchema.TableColumn(schema);
				colvarMinLongitude.ColumnName = "MinLongitude";
				colvarMinLongitude.DataType = DbType.Double;
				colvarMinLongitude.MaxLength = 0;
				colvarMinLongitude.AutoIncrement = false;
				colvarMinLongitude.IsNullable = false;
				colvarMinLongitude.IsPrimaryKey = false;
				colvarMinLongitude.IsForeignKey = false;
				colvarMinLongitude.IsReadOnly = false;
				colvarMinLongitude.DefaultSetting = @"";
				colvarMinLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMinLongitude);

				TableSchema.TableColumn colvarMinLattitude = new TableSchema.TableColumn(schema);
				colvarMinLattitude.ColumnName = "MinLattitude";
				colvarMinLattitude.DataType = DbType.Double;
				colvarMinLattitude.MaxLength = 0;
				colvarMinLattitude.AutoIncrement = false;
				colvarMinLattitude.IsNullable = false;
				colvarMinLattitude.IsPrimaryKey = false;
				colvarMinLattitude.IsForeignKey = false;
				colvarMinLattitude.IsReadOnly = false;
				colvarMinLattitude.DefaultSetting = @"";
				colvarMinLattitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMinLattitude);

				TableSchema.TableColumn colvarMaxLongitude = new TableSchema.TableColumn(schema);
				colvarMaxLongitude.ColumnName = "MaxLongitude";
				colvarMaxLongitude.DataType = DbType.Double;
				colvarMaxLongitude.MaxLength = 0;
				colvarMaxLongitude.AutoIncrement = false;
				colvarMaxLongitude.IsNullable = false;
				colvarMaxLongitude.IsPrimaryKey = false;
				colvarMaxLongitude.IsForeignKey = false;
				colvarMaxLongitude.IsReadOnly = false;
				colvarMaxLongitude.DefaultSetting = @"";
				colvarMaxLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaxLongitude);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFenceRectangles",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFenceRectangle LoadFrom(GS_AccountGeoFenceRectangle item)
		{
			GS_AccountGeoFenceRectangle result = new GS_AccountGeoFenceRectangle();
			if (item.GeoFenceID != default(long)) {
				result.LoadByKey(item.GeoFenceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set {
				SetColumnValue(Columns.GeoFenceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceID));
			}
		}
		[DataMember]
		public double MaxLattitude {
			get { return GetColumnValue<double>(Columns.MaxLattitude); }
			set {
				SetColumnValue(Columns.MaxLattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MaxLattitude));
			}
		}
		[DataMember]
		public double MinLongitude {
			get { return GetColumnValue<double>(Columns.MinLongitude); }
			set {
				SetColumnValue(Columns.MinLongitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MinLongitude));
			}
		}
		[DataMember]
		public double MinLattitude {
			get { return GetColumnValue<double>(Columns.MinLattitude); }
			set {
				SetColumnValue(Columns.MinLattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MinLattitude));
			}
		}
		[DataMember]
		public double MaxLongitude {
			get { return GetColumnValue<double>(Columns.MaxLongitude); }
			set {
				SetColumnValue(Columns.MaxLongitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MaxLongitude));
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
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFence _GeoFence;
		//Relationship: FK_GS_AccountGeoFenceRectangles_GS_AccountGeoFences
		public GS_AccountGeoFence GeoFence
		{
			get
			{
				if(_GeoFence == null) {
					_GeoFence = GS_AccountGeoFence.FetchByID(this.GeoFenceID);
				}
				return _GeoFence;
			}
			set
			{
				SetColumnValue("GeoFenceID", value.GeoFenceID);
				_GeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return GeoFenceID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn MaxLattitudeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn MinLongitudeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn MinLattitudeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MaxLongitudeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFenceID = @"GeoFenceID";
			public static readonly string MaxLattitude = @"MaxLattitude";
			public static readonly string MinLongitude = @"MinLongitude";
			public static readonly string MinLattitude = @"MinLattitude";
			public static readonly string MaxLongitude = @"MaxLongitude";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFenceID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFenceReportMode class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceReportModeCollection : ActiveList<GS_AccountGeoFenceReportMode, GS_AccountGeoFenceReportModeCollection>
	{
		public static GS_AccountGeoFenceReportModeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFenceReportModeCollection result = new GS_AccountGeoFenceReportModeCollection();
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
			foreach (GS_AccountGeoFenceReportMode item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFenceReportModes table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceReportMode : ActiveRecord<GS_AccountGeoFenceReportMode>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string UncertainID = "0";
			[EnumMember()] public const string ExitAlertID = "1";
			[EnumMember()] public const string EnterAlertID = "2";
			[EnumMember()] public const string ExitEnterAlertID = "3";
			[EnumMember()] public const string DeleteID = "4";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public GS_AccountGeoFenceReportMode()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFenceReportModes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarReportModeID = new TableSchema.TableColumn(schema);
				colvarReportModeID.ColumnName = "ReportModeID";
				colvarReportModeID.DataType = DbType.AnsiString;
				colvarReportModeID.MaxLength = 3;
				colvarReportModeID.AutoIncrement = false;
				colvarReportModeID.IsNullable = false;
				colvarReportModeID.IsPrimaryKey = true;
				colvarReportModeID.IsForeignKey = false;
				colvarReportModeID.IsReadOnly = false;
				colvarReportModeID.DefaultSetting = @"";
				colvarReportModeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeID);

				TableSchema.TableColumn colvarReportModeName = new TableSchema.TableColumn(schema);
				colvarReportModeName.ColumnName = "ReportModeName";
				colvarReportModeName.DataType = DbType.AnsiString;
				colvarReportModeName.MaxLength = 50;
				colvarReportModeName.AutoIncrement = false;
				colvarReportModeName.IsNullable = false;
				colvarReportModeName.IsPrimaryKey = false;
				colvarReportModeName.IsForeignKey = false;
				colvarReportModeName.IsReadOnly = false;
				colvarReportModeName.DefaultSetting = @"";
				colvarReportModeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeName);

				TableSchema.TableColumn colvarReportModeDesc = new TableSchema.TableColumn(schema);
				colvarReportModeDesc.ColumnName = "ReportModeDesc";
				colvarReportModeDesc.DataType = DbType.AnsiString;
				colvarReportModeDesc.MaxLength = 500;
				colvarReportModeDesc.AutoIncrement = false;
				colvarReportModeDesc.IsNullable = true;
				colvarReportModeDesc.IsPrimaryKey = false;
				colvarReportModeDesc.IsForeignKey = false;
				colvarReportModeDesc.IsReadOnly = false;
				colvarReportModeDesc.DefaultSetting = @"";
				colvarReportModeDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeDesc);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFenceReportModes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFenceReportMode LoadFrom(GS_AccountGeoFenceReportMode item)
		{
			GS_AccountGeoFenceReportMode result = new GS_AccountGeoFenceReportMode();
			if (item.ReportModeID != default(string)) {
				result.LoadByKey(item.ReportModeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string ReportModeID {
			get { return GetColumnValue<string>(Columns.ReportModeID); }
			set {
				SetColumnValue(Columns.ReportModeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeID));
			}
		}
		[DataMember]
		public string ReportModeName {
			get { return GetColumnValue<string>(Columns.ReportModeName); }
			set {
				SetColumnValue(Columns.ReportModeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeName));
			}
		}
		[DataMember]
		public string ReportModeDesc {
			get { return GetColumnValue<string>(Columns.ReportModeDesc); }
			set {
				SetColumnValue(Columns.ReportModeDesc, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeDesc));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return ReportModeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ReportModeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ReportModeNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReportModeDescColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ReportModeID = @"ReportModeID";
			public static readonly string ReportModeName = @"ReportModeName";
			public static readonly string ReportModeDesc = @"ReportModeDesc";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ReportModeID; }
		}
		*/

		#region Foreign Collections

		private GS_AccountGeoFenceCollection _GS_AccountGeoFencesCol;
		//Relationship: FK_GS_AccountGeoFences_GS_AccountGeoFenceReportModes
		public GS_AccountGeoFenceCollection GS_AccountGeoFencesCol
		{
			get
			{
				if(_GS_AccountGeoFencesCol == null) {
					_GS_AccountGeoFencesCol = new GS_AccountGeoFenceCollection();
					_GS_AccountGeoFencesCol.LoadAndCloseReader(GS_AccountGeoFence.Query()
						.WHERE(GS_AccountGeoFence.Columns.ReportModeId, ReportModeID).ExecuteReader());
				}
				return _GS_AccountGeoFencesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFence class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceCollection : ActiveList<GS_AccountGeoFence, GS_AccountGeoFenceCollection>
	{
		public static GS_AccountGeoFenceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFenceCollection result = new GS_AccountGeoFenceCollection();
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
			foreach (GS_AccountGeoFence item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFences table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFence : ActiveRecord<GS_AccountGeoFence>, INotifyPropertyChanged
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

		public GS_AccountGeoFence()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFences", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceID = new TableSchema.TableColumn(schema);
				colvarGeoFenceID.ColumnName = "GeoFenceID";
				colvarGeoFenceID.DataType = DbType.Int64;
				colvarGeoFenceID.MaxLength = 0;
				colvarGeoFenceID.AutoIncrement = true;
				colvarGeoFenceID.IsNullable = false;
				colvarGeoFenceID.IsPrimaryKey = true;
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
				colvarGeoFenceTypeId.IsForeignKey = true;
				colvarGeoFenceTypeId.IsReadOnly = false;
				colvarGeoFenceTypeId.DefaultSetting = @"";
				colvarGeoFenceTypeId.ForeignKeyTableName = "GS_AccountGeoFenceTypes";
				schema.Columns.Add(colvarGeoFenceTypeId);

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

				TableSchema.TableColumn colvarReportModeId = new TableSchema.TableColumn(schema);
				colvarReportModeId.ColumnName = "ReportModeId";
				colvarReportModeId.DataType = DbType.AnsiString;
				colvarReportModeId.MaxLength = 3;
				colvarReportModeId.AutoIncrement = false;
				colvarReportModeId.IsNullable = false;
				colvarReportModeId.IsPrimaryKey = false;
				colvarReportModeId.IsForeignKey = true;
				colvarReportModeId.IsReadOnly = false;
				colvarReportModeId.DefaultSetting = @"((3))";
				colvarReportModeId.ForeignKeyTableName = "GS_AccountGeoFenceReportModes";
				schema.Columns.Add(colvarReportModeId);

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

				TableSchema.TableColumn colvarGeogCol1 = new TableSchema.TableColumn(schema);
				colvarGeogCol1.ColumnName = "GeogCol1";
				colvarGeogCol1.DataType = DbType.AnsiString;
				colvarGeogCol1.MaxLength = -1;
				colvarGeogCol1.AutoIncrement = false;
				colvarGeogCol1.IsNullable = false;
				colvarGeogCol1.IsPrimaryKey = false;
				colvarGeogCol1.IsForeignKey = false;
				colvarGeogCol1.IsReadOnly = false;
				colvarGeogCol1.DefaultSetting = @"";
				colvarGeogCol1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeogCol1);

				TableSchema.TableColumn colvarGeogCol2 = new TableSchema.TableColumn(schema);
				colvarGeogCol2.ColumnName = "GeogCol2";
				colvarGeogCol2.DataType = DbType.String;
				colvarGeogCol2.MaxLength = -1;
				colvarGeogCol2.AutoIncrement = false;
				colvarGeogCol2.IsNullable = true;
				colvarGeogCol2.IsPrimaryKey = false;
				colvarGeogCol2.IsForeignKey = false;
				colvarGeogCol2.IsReadOnly = true;
				colvarGeogCol2.DefaultSetting = @"";
				colvarGeogCol2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeogCol2);

				TableSchema.TableColumn colvarArea = new TableSchema.TableColumn(schema);
				colvarArea.ColumnName = "Area";
				colvarArea.DataType = DbType.Double;
				colvarArea.MaxLength = 0;
				colvarArea.AutoIncrement = false;
				colvarArea.IsNullable = true;
				colvarArea.IsPrimaryKey = false;
				colvarArea.IsForeignKey = false;
				colvarArea.IsReadOnly = true;
				colvarArea.DefaultSetting = @"";
				colvarArea.ForeignKeyTableName = "";
				schema.Columns.Add(colvarArea);

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

				TableSchema.TableColumn colvarGoogleMapZoomLevel = new TableSchema.TableColumn(schema);
				colvarGoogleMapZoomLevel.ColumnName = "GoogleMapZoomLevel";
				colvarGoogleMapZoomLevel.DataType = DbType.Int16;
				colvarGoogleMapZoomLevel.MaxLength = 0;
				colvarGoogleMapZoomLevel.AutoIncrement = false;
				colvarGoogleMapZoomLevel.IsNullable = true;
				colvarGoogleMapZoomLevel.IsPrimaryKey = false;
				colvarGoogleMapZoomLevel.IsForeignKey = false;
				colvarGoogleMapZoomLevel.IsReadOnly = false;
				colvarGoogleMapZoomLevel.DefaultSetting = @"";
				colvarGoogleMapZoomLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGoogleMapZoomLevel);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFences",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFence LoadFrom(GS_AccountGeoFence item)
		{
			GS_AccountGeoFence result = new GS_AccountGeoFence();
			if (item.GeoFenceID != default(long)) {
				result.LoadByKey(item.GeoFenceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long GeoFenceID {
			get { return GetColumnValue<long>(Columns.GeoFenceID); }
			set {
				SetColumnValue(Columns.GeoFenceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceID));
			}
		}
		[DataMember]
		public string GeoFenceTypeId {
			get { return GetColumnValue<string>(Columns.GeoFenceTypeId); }
			set {
				SetColumnValue(Columns.GeoFenceTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceTypeId));
			}
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public string ReportModeId {
			get { return GetColumnValue<string>(Columns.ReportModeId); }
			set {
				SetColumnValue(Columns.ReportModeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeId));
			}
		}
		[DataMember]
		public string GeoFenceName {
			get { return GetColumnValue<string>(Columns.GeoFenceName); }
			set {
				SetColumnValue(Columns.GeoFenceName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceName));
			}
		}
		[DataMember]
		public string GeoFenceDescription {
			get { return GetColumnValue<string>(Columns.GeoFenceDescription); }
			set {
				SetColumnValue(Columns.GeoFenceDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceDescription));
			}
		}
		[DataMember]
		public string GeogCol1 {
			get { return GetColumnValue<string>(Columns.GeogCol1); }
			set {
				SetColumnValue(Columns.GeogCol1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeogCol1));
			}
		}
		[DataMember]
		public string GeogCol2 {
			get { return GetColumnValue<string>(Columns.GeogCol2); }
			set {
				SetColumnValue(Columns.GeogCol2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeogCol2));
			}
		}
		[DataMember]
		public double? Area {
			get { return GetColumnValue<double?>(Columns.Area); }
			set {
				SetColumnValue(Columns.Area, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Area));
			}
		}
		[DataMember]
		public double? MeanLattitude {
			get { return GetColumnValue<double?>(Columns.MeanLattitude); }
			set {
				SetColumnValue(Columns.MeanLattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MeanLattitude));
			}
		}
		[DataMember]
		public double? MeanLongitude {
			get { return GetColumnValue<double?>(Columns.MeanLongitude); }
			set {
				SetColumnValue(Columns.MeanLongitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MeanLongitude));
			}
		}
		[DataMember]
		public short? GoogleMapZoomLevel {
			get { return GetColumnValue<short?>(Columns.GoogleMapZoomLevel); }
			set {
				SetColumnValue(Columns.GoogleMapZoomLevel, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GoogleMapZoomLevel));
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
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
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
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFenceReportMode _ReportMode;
		//Relationship: FK_GS_AccountGeoFences_GS_AccountGeoFenceReportModes
		public GS_AccountGeoFenceReportMode ReportMode
		{
			get
			{
				if(_ReportMode == null) {
					_ReportMode = GS_AccountGeoFenceReportMode.FetchByID(this.ReportModeId);
				}
				return _ReportMode;
			}
			set
			{
				SetColumnValue("ReportModeId", value.ReportModeID);
				_ReportMode = value;
			}
		}

		private GS_AccountGeoFenceType _GeoFenceType;
		//Relationship: FK_GS_AccountGeoFences_GS_AccountGeoFenceTypes
		public GS_AccountGeoFenceType GeoFenceType
		{
			get
			{
				if(_GeoFenceType == null) {
					_GeoFenceType = GS_AccountGeoFenceType.FetchByID(this.GeoFenceTypeId);
				}
				return _GeoFenceType;
			}
			set
			{
				SetColumnValue("GeoFenceTypeId", value.GeoFenceTypeID);
				_GeoFenceType = value;
			}
		}

		#endregion //ForeignKey Properties

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
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ReportModeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn GeoFenceNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn GeoFenceDescriptionColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn GeogCol1Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn GeogCol2Column
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn AreaColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn MeanLattitudeColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn MeanLongitudeColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn GoogleMapZoomLevelColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[18]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFenceID = @"GeoFenceID";
			public static readonly string GeoFenceTypeId = @"GeoFenceTypeId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string ReportModeId = @"ReportModeId";
			public static readonly string GeoFenceName = @"GeoFenceName";
			public static readonly string GeoFenceDescription = @"GeoFenceDescription";
			public static readonly string GeogCol1 = @"GeogCol1";
			public static readonly string GeogCol2 = @"GeogCol2";
			public static readonly string Area = @"Area";
			public static readonly string MeanLattitude = @"MeanLattitude";
			public static readonly string MeanLongitude = @"MeanLongitude";
			public static readonly string GoogleMapZoomLevel = @"GoogleMapZoomLevel";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFenceID; }
		}
		*/

		#region Foreign Collections

		private GS_AccountGeoFenceCircleCollection _GS_AccountGeoFenceCirclesCol;
		//Relationship: FK_GS_AccountGeoFenceCircles_GS_AccountGeoFences
		public GS_AccountGeoFenceCircleCollection GS_AccountGeoFenceCirclesCol
		{
			get
			{
				if(_GS_AccountGeoFenceCirclesCol == null) {
					_GS_AccountGeoFenceCirclesCol = new GS_AccountGeoFenceCircleCollection();
					_GS_AccountGeoFenceCirclesCol.LoadAndCloseReader(GS_AccountGeoFenceCircle.Query()
						.WHERE(GS_AccountGeoFenceCircle.Columns.GeoFenceID, GeoFenceID).ExecuteReader());
				}
				return _GS_AccountGeoFenceCirclesCol;
			}
		}

		private GS_AccountGeoFencePointCollection _GS_AccountGeoFencePointsCol;
		//Relationship: FK_GS_AccountGeoFencePoints_GS_AccountGeoFences
		public GS_AccountGeoFencePointCollection GS_AccountGeoFencePointsCol
		{
			get
			{
				if(_GS_AccountGeoFencePointsCol == null) {
					_GS_AccountGeoFencePointsCol = new GS_AccountGeoFencePointCollection();
					_GS_AccountGeoFencePointsCol.LoadAndCloseReader(GS_AccountGeoFencePoint.Query()
						.WHERE(GS_AccountGeoFencePoint.Columns.GeoFenceID, GeoFenceID).ExecuteReader());
				}
				return _GS_AccountGeoFencePointsCol;
			}
		}

		private GS_AccountGeoFencePolygonCollection _GS_AccountGeoFencePolygonsCol;
		//Relationship: FK_GS_AccountGeoFencePolygons_GS_AccountGeoFences
		public GS_AccountGeoFencePolygonCollection GS_AccountGeoFencePolygonsCol
		{
			get
			{
				if(_GS_AccountGeoFencePolygonsCol == null) {
					_GS_AccountGeoFencePolygonsCol = new GS_AccountGeoFencePolygonCollection();
					_GS_AccountGeoFencePolygonsCol.LoadAndCloseReader(GS_AccountGeoFencePolygon.Query()
						.WHERE(GS_AccountGeoFencePolygon.Columns.GeoFenceId, GeoFenceID).ExecuteReader());
				}
				return _GS_AccountGeoFencePolygonsCol;
			}
		}

		private GS_AccountGeoFenceRectangleCollection _GS_AccountGeoFenceRectanglesCol;
		//Relationship: FK_GS_AccountGeoFenceRectangles_GS_AccountGeoFences
		public GS_AccountGeoFenceRectangleCollection GS_AccountGeoFenceRectanglesCol
		{
			get
			{
				if(_GS_AccountGeoFenceRectanglesCol == null) {
					_GS_AccountGeoFenceRectanglesCol = new GS_AccountGeoFenceRectangleCollection();
					_GS_AccountGeoFenceRectanglesCol.LoadAndCloseReader(GS_AccountGeoFenceRectangle.Query()
						.WHERE(GS_AccountGeoFenceRectangle.Columns.GeoFenceID, GeoFenceID).ExecuteReader());
				}
				return _GS_AccountGeoFenceRectanglesCol;
			}
		}

		private LP_GsGeoFenceCollection _LP_GsGeoFencesCol;
		//Relationship: FK_LP_GsGeoFence_GS_AccountGeoFences
		public LP_GsGeoFenceCollection LP_GsGeoFencesCol
		{
			get
			{
				if(_LP_GsGeoFencesCol == null) {
					_LP_GsGeoFencesCol = new LP_GsGeoFenceCollection();
					_LP_GsGeoFencesCol.LoadAndCloseReader(LP_GsGeoFence.Query()
						.WHERE(LP_GsGeoFence.Columns.GsGeoFenceId, GeoFenceID).ExecuteReader());
				}
				return _LP_GsGeoFencesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the GS_AccountGeoFenceType class.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceTypeCollection : ActiveList<GS_AccountGeoFenceType, GS_AccountGeoFenceTypeCollection>
	{
		public static GS_AccountGeoFenceTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_AccountGeoFenceTypeCollection result = new GS_AccountGeoFenceTypeCollection();
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
			foreach (GS_AccountGeoFenceType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_AccountGeoFenceTypes table.
	/// </summary>
	[DataContract]
	public partial class GS_AccountGeoFenceType : ActiveRecord<GS_AccountGeoFenceType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string CircleID = "CIR";
			[EnumMember()] public const string PointID = "PNT";
			[EnumMember()] public const string PolygonID = "POLY";
			[EnumMember()] public const string RectangleID = "RECT";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public GS_AccountGeoFenceType()
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
				TableSchema.Table schema = new TableSchema.Table("GS_AccountGeoFenceTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarGeoFenceTypeID = new TableSchema.TableColumn(schema);
				colvarGeoFenceTypeID.ColumnName = "GeoFenceTypeID";
				colvarGeoFenceTypeID.DataType = DbType.AnsiString;
				colvarGeoFenceTypeID.MaxLength = 50;
				colvarGeoFenceTypeID.AutoIncrement = false;
				colvarGeoFenceTypeID.IsNullable = false;
				colvarGeoFenceTypeID.IsPrimaryKey = true;
				colvarGeoFenceTypeID.IsForeignKey = false;
				colvarGeoFenceTypeID.IsReadOnly = false;
				colvarGeoFenceTypeID.DefaultSetting = @"";
				colvarGeoFenceTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceTypeID);

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

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_AccountGeoFenceTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_AccountGeoFenceType LoadFrom(GS_AccountGeoFenceType item)
		{
			GS_AccountGeoFenceType result = new GS_AccountGeoFenceType();
			if (item.GeoFenceTypeID != default(string)) {
				result.LoadByKey(item.GeoFenceTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string GeoFenceTypeID {
			get { return GetColumnValue<string>(Columns.GeoFenceTypeID); }
			set {
				SetColumnValue(Columns.GeoFenceTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceTypeID));
			}
		}
		[DataMember]
		public string GeoFenceType {
			get { return GetColumnValue<string>(Columns.GeoFenceType); }
			set {
				SetColumnValue(Columns.GeoFenceType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceType));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return GeoFenceType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn GeoFenceTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GeoFenceTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string GeoFenceTypeID = @"GeoFenceTypeID";
			public static readonly string GeoFenceType = @"GeoFenceType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return GeoFenceTypeID; }
		}
		*/

		#region Foreign Collections

		private GS_AccountGeoFenceCollection _GS_AccountGeoFencesCol;
		//Relationship: FK_GS_AccountGeoFences_GS_AccountGeoFenceTypes
		public GS_AccountGeoFenceCollection GS_AccountGeoFencesCol
		{
			get
			{
				if(_GS_AccountGeoFencesCol == null) {
					_GS_AccountGeoFencesCol = new GS_AccountGeoFenceCollection();
					_GS_AccountGeoFencesCol.LoadAndCloseReader(GS_AccountGeoFence.Query()
						.WHERE(GS_AccountGeoFence.Columns.GeoFenceTypeId, GeoFenceTypeID).ExecuteReader());
				}
				return _GS_AccountGeoFencesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the GS_DeviceType class.
	/// </summary>
	[DataContract]
	public partial class GS_DeviceTypeCollection : ActiveList<GS_DeviceType, GS_DeviceTypeCollection>
	{
		public static GS_DeviceTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_DeviceTypeCollection result = new GS_DeviceTypeCollection();
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
			foreach (GS_DeviceType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_DeviceTypes table.
	/// </summary>
	[DataContract]
	public partial class GS_DeviceType : ActiveRecord<GS_DeviceType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public GS_DeviceType()
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
				TableSchema.Table schema = new TableSchema.Table("GS_DeviceTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeviceTypeID = new TableSchema.TableColumn(schema);
				colvarDeviceTypeID.ColumnName = "DeviceTypeID";
				colvarDeviceTypeID.DataType = DbType.AnsiString;
				colvarDeviceTypeID.MaxLength = 10;
				colvarDeviceTypeID.AutoIncrement = false;
				colvarDeviceTypeID.IsNullable = false;
				colvarDeviceTypeID.IsPrimaryKey = true;
				colvarDeviceTypeID.IsForeignKey = false;
				colvarDeviceTypeID.IsReadOnly = false;
				colvarDeviceTypeID.DefaultSetting = @"";
				colvarDeviceTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceTypeID);

				TableSchema.TableColumn colvarDeviceName = new TableSchema.TableColumn(schema);
				colvarDeviceName.ColumnName = "DeviceName";
				colvarDeviceName.DataType = DbType.AnsiString;
				colvarDeviceName.MaxLength = 50;
				colvarDeviceName.AutoIncrement = false;
				colvarDeviceName.IsNullable = false;
				colvarDeviceName.IsPrimaryKey = false;
				colvarDeviceName.IsForeignKey = false;
				colvarDeviceName.IsReadOnly = false;
				colvarDeviceName.DefaultSetting = @"";
				colvarDeviceName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceName);

				TableSchema.TableColumn colvarAbbreviation = new TableSchema.TableColumn(schema);
				colvarAbbreviation.ColumnName = "Abbreviation";
				colvarAbbreviation.DataType = DbType.AnsiString;
				colvarAbbreviation.MaxLength = 2;
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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_DeviceTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_DeviceType LoadFrom(GS_DeviceType item)
		{
			GS_DeviceType result = new GS_DeviceType();
			if (item.DeviceTypeID != default(string)) {
				result.LoadByKey(item.DeviceTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string DeviceTypeID {
			get { return GetColumnValue<string>(Columns.DeviceTypeID); }
			set {
				SetColumnValue(Columns.DeviceTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceTypeID));
			}
		}
		[DataMember]
		public string DeviceName {
			get { return GetColumnValue<string>(Columns.DeviceName); }
			set {
				SetColumnValue(Columns.DeviceName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceName));
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
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set {
				SetColumnValue(Columns.ModifiedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModifiedOn));
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
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return DeviceName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeviceTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DeviceNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AbbreviationColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeviceTypeID = @"DeviceTypeID";
			public static readonly string DeviceName = @"DeviceName";
			public static readonly string Abbreviation = @"Abbreviation";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DeviceTypeID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_Event class.
	/// </summary>
	[DataContract]
	public partial class GS_EventCollection : ActiveList<GS_Event, GS_EventCollection>
	{
		public static GS_EventCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_EventCollection result = new GS_EventCollection();
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
			foreach (GS_Event item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_Events table.
	/// </summary>
	[DataContract]
	public partial class GS_Event : ActiveRecord<GS_Event>, INotifyPropertyChanged
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

		public GS_Event()
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
				TableSchema.Table schema = new TableSchema.Table("GS_Events", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventID = new TableSchema.TableColumn(schema);
				colvarEventID.ColumnName = "EventID";
				colvarEventID.DataType = DbType.Int64;
				colvarEventID.MaxLength = 0;
				colvarEventID.AutoIncrement = true;
				colvarEventID.IsNullable = false;
				colvarEventID.IsPrimaryKey = true;
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
				colvarEventTypeId.IsForeignKey = true;
				colvarEventTypeId.IsReadOnly = false;
				colvarEventTypeId.DefaultSetting = @"";
				colvarEventTypeId.ForeignKeyTableName = "GS_EventTypes";
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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_Events",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_Event LoadFrom(GS_Event item)
		{
			GS_Event result = new GS_Event();
			if (item.EventID != default(long)) {
				result.LoadByKey(item.EventID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long EventID {
			get { return GetColumnValue<long>(Columns.EventID); }
			set {
				SetColumnValue(Columns.EventID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventID));
			}
		}
		[DataMember]
		public string EventTypeId {
			get { return GetColumnValue<string>(Columns.EventTypeId); }
			set {
				SetColumnValue(Columns.EventTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventTypeId));
			}
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public string EventName {
			get { return GetColumnValue<string>(Columns.EventName); }
			set {
				SetColumnValue(Columns.EventName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventName));
			}
		}
		[DataMember]
		public DateTime EventDate {
			get { return GetColumnValue<DateTime>(Columns.EventDate); }
			set {
				SetColumnValue(Columns.EventDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventDate));
			}
		}
		[DataMember]
		public string Lattitude {
			get { return GetColumnValue<string>(Columns.Lattitude); }
			set {
				SetColumnValue(Columns.Lattitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Lattitude));
			}
		}
		[DataMember]
		public string Longitude {
			get { return GetColumnValue<string>(Columns.Longitude); }
			set {
				SetColumnValue(Columns.Longitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Longitude));
			}
		}
		[DataMember]
		public decimal? Speed {
			get { return GetColumnValue<decimal?>(Columns.Speed); }
			set {
				SetColumnValue(Columns.Speed, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Speed));
			}
		}
		[DataMember]
		public decimal? Course {
			get { return GetColumnValue<decimal?>(Columns.Course); }
			set {
				SetColumnValue(Columns.Course, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Course));
			}
		}
		[DataMember]
		public int? CurrentMilage {
			get { return GetColumnValue<int?>(Columns.CurrentMilage); }
			set {
				SetColumnValue(Columns.CurrentMilage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CurrentMilage));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_EventType _EventType;
		//Relationship: FK_GS_Events_GS_EventTypes
		public GS_EventType EventType
		{
			get
			{
				if(_EventType == null) {
					_EventType = GS_EventType.FetchByID(this.EventTypeId);
				}
				return _EventType;
			}
			set
			{
				SetColumnValue("EventTypeId", value.EventTypeID);
				_EventType = value;
			}
		}

		#endregion //ForeignKey Properties

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
			public static readonly string EventID = @"EventID";
			public static readonly string EventTypeId = @"EventTypeId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string EventName = @"EventName";
			public static readonly string EventDate = @"EventDate";
			public static readonly string Lattitude = @"Lattitude";
			public static readonly string Longitude = @"Longitude";
			public static readonly string Speed = @"Speed";
			public static readonly string Course = @"Course";
			public static readonly string CurrentMilage = @"CurrentMilage";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return EventID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the GS_EventType class.
	/// </summary>
	[DataContract]
	public partial class GS_EventTypeCollection : ActiveList<GS_EventType, GS_EventTypeCollection>
	{
		public static GS_EventTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			GS_EventTypeCollection result = new GS_EventTypeCollection();
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
			foreach (GS_EventType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the GS_EventTypes table.
	/// </summary>
	[DataContract]
	public partial class GS_EventType : ActiveRecord<GS_EventType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string EmergencyID = "EMERG";
			[EnumMember()] public const string FallAlertID = "FALL";
			[EnumMember()] public const string FenceBreachID = "FENCE";
			[EnumMember()] public const string FenceRestoreID = "FENCE_RT";
			[EnumMember()] public const string FireID = "FIRE";
			[EnumMember()] public const string LowBatteryAlertID = "LOWBAT";
			[EnumMember()] public const string MedicalID = "MEDICAL";
			[EnumMember()] public const string SpeedAlertID = "SPEED";
			[EnumMember()] public const string TamperAlertID = "TAMPER";
			[EnumMember()] public const string TamperID = "TAMPER_RT";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public GS_EventType()
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
				TableSchema.Table schema = new TableSchema.Table("GS_EventTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventTypeID = new TableSchema.TableColumn(schema);
				colvarEventTypeID.ColumnName = "EventTypeID";
				colvarEventTypeID.DataType = DbType.AnsiString;
				colvarEventTypeID.MaxLength = 20;
				colvarEventTypeID.AutoIncrement = false;
				colvarEventTypeID.IsNullable = false;
				colvarEventTypeID.IsPrimaryKey = true;
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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("GS_EventTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static GS_EventType LoadFrom(GS_EventType item)
		{
			GS_EventType result = new GS_EventType();
			if (item.EventTypeID != default(string)) {
				result.LoadByKey(item.EventTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string EventTypeID {
			get { return GetColumnValue<string>(Columns.EventTypeID); }
			set {
				SetColumnValue(Columns.EventTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventTypeID));
			}
		}
		[DataMember]
		public string EventType {
			get { return GetColumnValue<string>(Columns.EventType); }
			set {
				SetColumnValue(Columns.EventType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventType));
			}
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
			public static readonly string EventTypeID = @"EventTypeID";
			public static readonly string EventType = @"EventType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return EventTypeID; }
		}
		*/

		#region Foreign Collections

		private GS_EventCollection _GS_EventsCol;
		//Relationship: FK_GS_Events_GS_EventTypes
		public GS_EventCollection GS_EventsCol
		{
			get
			{
				if(_GS_EventsCol == null) {
					_GS_EventsCol = new GS_EventCollection();
					_GS_EventsCol.LoadAndCloseReader(GS_Event.Query()
						.WHERE(GS_Event.Columns.EventTypeId, EventTypeID).ExecuteReader());
				}
				return _GS_EventsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the KW_CommandMessage class.
	/// </summary>
	[DataContract]
	public partial class KW_CommandMessageCollection : ActiveList<KW_CommandMessage, KW_CommandMessageCollection>
	{
		public static KW_CommandMessageCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			KW_CommandMessageCollection result = new KW_CommandMessageCollection();
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
			foreach (KW_CommandMessage item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the KW_CommandMessages table.
	/// </summary>
	[DataContract]
	public partial class KW_CommandMessage : ActiveRecord<KW_CommandMessage>, INotifyPropertyChanged
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

		public KW_CommandMessage()
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
				TableSchema.Table schema = new TableSchema.Table("KW_CommandMessages", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = true;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarRequestId = new TableSchema.TableColumn(schema);
				colvarRequestId.ColumnName = "RequestId";
				colvarRequestId.DataType = DbType.Int64;
				colvarRequestId.MaxLength = 0;
				colvarRequestId.AutoIncrement = false;
				colvarRequestId.IsNullable = true;
				colvarRequestId.IsPrimaryKey = false;
				colvarRequestId.IsForeignKey = true;
				colvarRequestId.IsReadOnly = false;
				colvarRequestId.DefaultSetting = @"";
				colvarRequestId.ForeignKeyTableName = "KW_Requests";
				schema.Columns.Add(colvarRequestId);

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

				TableSchema.TableColumn colvarMessageDate = new TableSchema.TableColumn(schema);
				colvarMessageDate.ColumnName = "MessageDate";
				colvarMessageDate.DataType = DbType.DateTime;
				colvarMessageDate.MaxLength = 0;
				colvarMessageDate.AutoIncrement = false;
				colvarMessageDate.IsNullable = true;
				colvarMessageDate.IsPrimaryKey = false;
				colvarMessageDate.IsForeignKey = false;
				colvarMessageDate.IsReadOnly = false;
				colvarMessageDate.DefaultSetting = @"";
				colvarMessageDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageDate);

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("KW_CommandMessages",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static KW_CommandMessage LoadFrom(KW_CommandMessage item)
		{
			KW_CommandMessage result = new KW_CommandMessage();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long? RequestId {
			get { return GetColumnValue<long?>(Columns.RequestId); }
			set {
				SetColumnValue(Columns.RequestId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestId));
			}
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public long? UnitID {
			get { return GetColumnValue<long?>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public DateTime? MessageDate {
			get { return GetColumnValue<DateTime?>(Columns.MessageDate); }
			set {
				SetColumnValue(Columns.MessageDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageDate));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private KW_Request _Request;
		//Relationship: FK_KW_CommandMessages_KW_Requests
		public KW_Request Request
		{
			get
			{
				if(_Request == null) {
					_Request = KW_Request.FetchByID(this.RequestId);
				}
				return _Request;
			}
			set
			{
				SetColumnValue("RequestId", value.RequestID);
				_Request = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequestIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn MessageDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string RequestId = @"RequestId";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string UnitID = @"UnitID";
			public static readonly string MessageDate = @"MessageDate";
			public static readonly string Sentence = @"Sentence";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/

		#region Foreign Collections

		private KW_RequestCollection _KW_RequestsCol;
		//Relationship: FK_KW_Requests_KW_CommandMessages
		public KW_RequestCollection KW_RequestsCol
		{
			get
			{
				if(_KW_RequestsCol == null) {
					_KW_RequestsCol = new KW_RequestCollection();
					_KW_RequestsCol.LoadAndCloseReader(KW_Request.Query()
						.WHERE(KW_Request.Columns.CommandMessageId, CommandMessageID).ExecuteReader());
				}
				return _KW_RequestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the KW_Device class.
	/// </summary>
	[DataContract]
	public partial class KW_DeviceCollection : ActiveList<KW_Device, KW_DeviceCollection>
	{
		public static KW_DeviceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			KW_DeviceCollection result = new KW_DeviceCollection();
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
			foreach (KW_Device item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the KW_Devices table.
	/// </summary>
	[DataContract]
	public partial class KW_Device : ActiveRecord<KW_Device>, INotifyPropertyChanged
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

		public KW_Device()
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
				TableSchema.Table schema = new TableSchema.Table("KW_Devices", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUnitID = new TableSchema.TableColumn(schema);
				colvarUnitID.ColumnName = "UnitID";
				colvarUnitID.DataType = DbType.Int64;
				colvarUnitID.MaxLength = 0;
				colvarUnitID.AutoIncrement = false;
				colvarUnitID.IsNullable = false;
				colvarUnitID.IsPrimaryKey = true;
				colvarUnitID.IsForeignKey = false;
				colvarUnitID.IsReadOnly = false;
				colvarUnitID.DefaultSetting = @"";
				colvarUnitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitID);

				TableSchema.TableColumn colvarAccountID = new TableSchema.TableColumn(schema);
				colvarAccountID.ColumnName = "AccountID";
				colvarAccountID.DataType = DbType.Int64;
				colvarAccountID.MaxLength = 0;
				colvarAccountID.AutoIncrement = false;
				colvarAccountID.IsNullable = true;
				colvarAccountID.IsPrimaryKey = false;
				colvarAccountID.IsForeignKey = false;
				colvarAccountID.IsReadOnly = false;
				colvarAccountID.DefaultSetting = @"";
				colvarAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountID);

				TableSchema.TableColumn colvarSimProductBarcodeId = new TableSchema.TableColumn(schema);
				colvarSimProductBarcodeId.ColumnName = "SimProductBarcodeId";
				colvarSimProductBarcodeId.DataType = DbType.String;
				colvarSimProductBarcodeId.MaxLength = 50;
				colvarSimProductBarcodeId.AutoIncrement = false;
				colvarSimProductBarcodeId.IsNullable = false;
				colvarSimProductBarcodeId.IsPrimaryKey = false;
				colvarSimProductBarcodeId.IsForeignKey = false;
				colvarSimProductBarcodeId.IsReadOnly = false;
				colvarSimProductBarcodeId.DefaultSetting = @"";
				colvarSimProductBarcodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSimProductBarcodeId);

				TableSchema.TableColumn colvarProductBarcodeId = new TableSchema.TableColumn(schema);
				colvarProductBarcodeId.ColumnName = "ProductBarcodeId";
				colvarProductBarcodeId.DataType = DbType.AnsiString;
				colvarProductBarcodeId.MaxLength = 50;
				colvarProductBarcodeId.AutoIncrement = false;
				colvarProductBarcodeId.IsNullable = true;
				colvarProductBarcodeId.IsPrimaryKey = false;
				colvarProductBarcodeId.IsForeignKey = false;
				colvarProductBarcodeId.IsReadOnly = false;
				colvarProductBarcodeId.DefaultSetting = @"";
				colvarProductBarcodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProductBarcodeId);

				TableSchema.TableColumn colvarPhoneNumber = new TableSchema.TableColumn(schema);
				colvarPhoneNumber.ColumnName = "PhoneNumber";
				colvarPhoneNumber.DataType = DbType.AnsiString;
				colvarPhoneNumber.MaxLength = 20;
				colvarPhoneNumber.AutoIncrement = false;
				colvarPhoneNumber.IsNullable = false;
				colvarPhoneNumber.IsPrimaryKey = false;
				colvarPhoneNumber.IsForeignKey = false;
				colvarPhoneNumber.IsReadOnly = false;
				colvarPhoneNumber.DefaultSetting = @"";
				colvarPhoneNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneNumber);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 8;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

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

				TableSchema.TableColumn colvarLastAccessDate = new TableSchema.TableColumn(schema);
				colvarLastAccessDate.ColumnName = "LastAccessDate";
				colvarLastAccessDate.DataType = DbType.DateTime;
				colvarLastAccessDate.MaxLength = 0;
				colvarLastAccessDate.AutoIncrement = false;
				colvarLastAccessDate.IsNullable = true;
				colvarLastAccessDate.IsPrimaryKey = false;
				colvarLastAccessDate.IsForeignKey = false;
				colvarLastAccessDate.IsReadOnly = false;
				colvarLastAccessDate.DefaultSetting = @"";
				colvarLastAccessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAccessDate);

				TableSchema.TableColumn colvarFirmwareVersion = new TableSchema.TableColumn(schema);
				colvarFirmwareVersion.ColumnName = "FirmwareVersion";
				colvarFirmwareVersion.DataType = DbType.AnsiString;
				colvarFirmwareVersion.MaxLength = 50;
				colvarFirmwareVersion.AutoIncrement = false;
				colvarFirmwareVersion.IsNullable = true;
				colvarFirmwareVersion.IsPrimaryKey = false;
				colvarFirmwareVersion.IsForeignKey = false;
				colvarFirmwareVersion.IsReadOnly = false;
				colvarFirmwareVersion.DefaultSetting = @"";
				colvarFirmwareVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirmwareVersion);

				TableSchema.TableColumn colvarSerialNumber = new TableSchema.TableColumn(schema);
				colvarSerialNumber.ColumnName = "SerialNumber";
				colvarSerialNumber.DataType = DbType.AnsiString;
				colvarSerialNumber.MaxLength = 50;
				colvarSerialNumber.AutoIncrement = false;
				colvarSerialNumber.IsNullable = true;
				colvarSerialNumber.IsPrimaryKey = false;
				colvarSerialNumber.IsForeignKey = false;
				colvarSerialNumber.IsReadOnly = false;
				colvarSerialNumber.DefaultSetting = @"";
				colvarSerialNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSerialNumber);

				TableSchema.TableColumn colvarMemorySize = new TableSchema.TableColumn(schema);
				colvarMemorySize.ColumnName = "MemorySize";
				colvarMemorySize.DataType = DbType.AnsiString;
				colvarMemorySize.MaxLength = 50;
				colvarMemorySize.AutoIncrement = false;
				colvarMemorySize.IsNullable = true;
				colvarMemorySize.IsPrimaryKey = false;
				colvarMemorySize.IsForeignKey = false;
				colvarMemorySize.IsReadOnly = false;
				colvarMemorySize.DefaultSetting = @"";
				colvarMemorySize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMemorySize);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("KW_Devices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static KW_Device LoadFrom(KW_Device item)
		{
			KW_Device result = new KW_Device();
			if (item.UnitID != default(long)) {
				result.LoadByKey(item.UnitID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public long? AccountID {
			get { return GetColumnValue<long?>(Columns.AccountID); }
			set {
				SetColumnValue(Columns.AccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountID));
			}
		}
		[DataMember]
		public string SimProductBarcodeId {
			get { return GetColumnValue<string>(Columns.SimProductBarcodeId); }
			set {
				SetColumnValue(Columns.SimProductBarcodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SimProductBarcodeId));
			}
		}
		[DataMember]
		public string ProductBarcodeId {
			get { return GetColumnValue<string>(Columns.ProductBarcodeId); }
			set {
				SetColumnValue(Columns.ProductBarcodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProductBarcodeId));
			}
		}
		[DataMember]
		public string PhoneNumber {
			get { return GetColumnValue<string>(Columns.PhoneNumber); }
			set {
				SetColumnValue(Columns.PhoneNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneNumber));
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
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public DateTime? LastAccessDate {
			get { return GetColumnValue<DateTime?>(Columns.LastAccessDate); }
			set {
				SetColumnValue(Columns.LastAccessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAccessDate));
			}
		}
		[DataMember]
		public string FirmwareVersion {
			get { return GetColumnValue<string>(Columns.FirmwareVersion); }
			set {
				SetColumnValue(Columns.FirmwareVersion, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FirmwareVersion));
			}
		}
		[DataMember]
		public string SerialNumber {
			get { return GetColumnValue<string>(Columns.SerialNumber); }
			set {
				SetColumnValue(Columns.SerialNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SerialNumber));
			}
		}
		[DataMember]
		public string MemorySize {
			get { return GetColumnValue<string>(Columns.MemorySize); }
			set {
				SetColumnValue(Columns.MemorySize, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MemorySize));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return UnitID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SimProductBarcodeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ProductBarcodeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhoneNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LastAccessDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn FirmwareVersionColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SerialNumberColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn MemorySizeColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UnitID = @"UnitID";
			public static readonly string AccountID = @"AccountID";
			public static readonly string SimProductBarcodeId = @"SimProductBarcodeId";
			public static readonly string ProductBarcodeId = @"ProductBarcodeId";
			public static readonly string PhoneNumber = @"PhoneNumber";
			public static readonly string Password = @"Password";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string LastAccessDate = @"LastAccessDate";
			public static readonly string FirmwareVersion = @"FirmwareVersion";
			public static readonly string SerialNumber = @"SerialNumber";
			public static readonly string MemorySize = @"MemorySize";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UnitID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the KW_Request class.
	/// </summary>
	[DataContract]
	public partial class KW_RequestCollection : ActiveList<KW_Request, KW_RequestCollection>
	{
		public static KW_RequestCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			KW_RequestCollection result = new KW_RequestCollection();
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
			foreach (KW_Request item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the KW_Requests table.
	/// </summary>
	[DataContract]
	public partial class KW_Request : ActiveRecord<KW_Request>, INotifyPropertyChanged
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

		public KW_Request()
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
				TableSchema.Table schema = new TableSchema.Table("KW_Requests", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
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

				TableSchema.TableColumn colvarCommandMessageId = new TableSchema.TableColumn(schema);
				colvarCommandMessageId.ColumnName = "CommandMessageId";
				colvarCommandMessageId.DataType = DbType.Int64;
				colvarCommandMessageId.MaxLength = 0;
				colvarCommandMessageId.AutoIncrement = false;
				colvarCommandMessageId.IsNullable = true;
				colvarCommandMessageId.IsPrimaryKey = false;
				colvarCommandMessageId.IsForeignKey = true;
				colvarCommandMessageId.IsReadOnly = false;
				colvarCommandMessageId.DefaultSetting = @"";
				colvarCommandMessageId.ForeignKeyTableName = "KW_CommandMessages";
				schema.Columns.Add(colvarCommandMessageId);

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

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

				TableSchema.TableColumn colvarAttempts = new TableSchema.TableColumn(schema);
				colvarAttempts.ColumnName = "Attempts";
				colvarAttempts.DataType = DbType.Int32;
				colvarAttempts.MaxLength = 0;
				colvarAttempts.AutoIncrement = false;
				colvarAttempts.IsNullable = true;
				colvarAttempts.IsPrimaryKey = false;
				colvarAttempts.IsForeignKey = false;
				colvarAttempts.IsReadOnly = false;
				colvarAttempts.DefaultSetting = @"";
				colvarAttempts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttempts);

				TableSchema.TableColumn colvarLastAttempDate = new TableSchema.TableColumn(schema);
				colvarLastAttempDate.ColumnName = "LastAttempDate";
				colvarLastAttempDate.DataType = DbType.DateTime;
				colvarLastAttempDate.MaxLength = 0;
				colvarLastAttempDate.AutoIncrement = false;
				colvarLastAttempDate.IsNullable = true;
				colvarLastAttempDate.IsPrimaryKey = false;
				colvarLastAttempDate.IsForeignKey = false;
				colvarLastAttempDate.IsReadOnly = false;
				colvarLastAttempDate.DefaultSetting = @"";
				colvarLastAttempDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAttempDate);

				TableSchema.TableColumn colvarProcessDate = new TableSchema.TableColumn(schema);
				colvarProcessDate.ColumnName = "ProcessDate";
				colvarProcessDate.DataType = DbType.DateTime;
				colvarProcessDate.MaxLength = 0;
				colvarProcessDate.AutoIncrement = false;
				colvarProcessDate.IsNullable = true;
				colvarProcessDate.IsPrimaryKey = false;
				colvarProcessDate.IsForeignKey = false;
				colvarProcessDate.IsReadOnly = false;
				colvarProcessDate.DefaultSetting = @"";
				colvarProcessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessDate);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("KW_Requests",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static KW_Request LoadFrom(KW_Request item)
		{
			KW_Request result = new KW_Request();
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
		public long? CommandMessageId {
			get { return GetColumnValue<long?>(Columns.CommandMessageId); }
			set {
				SetColumnValue(Columns.CommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageId));
			}
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
			}
		}
		[DataMember]
		public int? Attempts {
			get { return GetColumnValue<int?>(Columns.Attempts); }
			set {
				SetColumnValue(Columns.Attempts, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Attempts));
			}
		}
		[DataMember]
		public DateTime? LastAttempDate {
			get { return GetColumnValue<DateTime?>(Columns.LastAttempDate); }
			set {
				SetColumnValue(Columns.LastAttempDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAttempDate));
			}
		}
		[DataMember]
		public DateTime? ProcessDate {
			get { return GetColumnValue<DateTime?>(Columns.ProcessDate); }
			set {
				SetColumnValue(Columns.ProcessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProcessDate));
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

		private KW_CommandMessage _CommandMessage;
		//Relationship: FK_KW_Requests_KW_CommandMessages
		public KW_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = KW_CommandMessage.FetchByID(this.CommandMessageId);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageId", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RequestID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandMessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AttemptsColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LastAttempDateColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ProcessDateColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequestID = @"RequestID";
			public static readonly string CommandMessageId = @"CommandMessageId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string UnitID = @"UnitID";
			public static readonly string Sentence = @"Sentence";
			public static readonly string Attempts = @"Attempts";
			public static readonly string LastAttempDate = @"LastAttempDate";
			public static readonly string ProcessDate = @"ProcessDate";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequestID; }
		}
		*/

		#region Foreign Collections

		private KW_CommandMessageCollection _KW_CommandMessagesCol;
		//Relationship: FK_KW_CommandMessages_KW_Requests
		public KW_CommandMessageCollection KW_CommandMessagesCol
		{
			get
			{
				if(_KW_CommandMessagesCol == null) {
					_KW_CommandMessagesCol = new KW_CommandMessageCollection();
					_KW_CommandMessagesCol.LoadAndCloseReader(KW_CommandMessage.Query()
						.WHERE(KW_CommandMessage.Columns.RequestId, RequestID).ExecuteReader());
				}
				return _KW_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_AVCFGCode class.
	/// </summary>
	[DataContract]
	public partial class LP_AVCFGCodeCollection : ActiveList<LP_AVCFGCode, LP_AVCFGCodeCollection>
	{
		public static LP_AVCFGCodeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_AVCFGCodeCollection result = new LP_AVCFGCodeCollection();
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
			foreach (LP_AVCFGCode item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_AVCFGCodes table.
	/// </summary>
	[DataContract]
	public partial class LP_AVCFGCode : ActiveRecord<LP_AVCFGCode>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string DisableGSensorID = "6";
			[EnumMember()] public const string EnableGSensorID = "7";
			[EnumMember()] public const string DisableGPSReceiverID = "8";
			[EnumMember()] public const string EnableGPSReceiverID = "9";
			[EnumMember()] public const string EnableAutoAnswerModeID = "a";
			[EnumMember()] public const string DisableAutoAnswerModeID = "b";
			[EnumMember()] public const string AcknowledgeSOSPanicAlertMessageID = "d";
			[EnumMember()] public const string EnableMonitorModeID = "e";
			[EnumMember()] public const string DisableMonitorModeID = "f";
			[EnumMember()] public const string EnableTamperDetectionID = "g";
			[EnumMember()] public const string DisableTamperDetectionID = "h";
			[EnumMember()] public const string QueryCurrentFeatureFlagStatusID = "QUE";
			[EnumMember()] public const string AcknowledgeTamperDetectionAlertMessageID = "t";
			[EnumMember()] public const string AcknowledgeGeoFenceAlertMessageID = "x";
			[EnumMember()] public const string AcknowledgeLowBatteryAlertMessageID = "z";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LP_AVCFGCode()
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
				TableSchema.Table schema = new TableSchema.Table("LP_AVCFGCodes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAVCFGCodeID = new TableSchema.TableColumn(schema);
				colvarAVCFGCodeID.ColumnName = "AVCFGCodeID";
				colvarAVCFGCodeID.DataType = DbType.AnsiString;
				colvarAVCFGCodeID.MaxLength = 3;
				colvarAVCFGCodeID.AutoIncrement = false;
				colvarAVCFGCodeID.IsNullable = false;
				colvarAVCFGCodeID.IsPrimaryKey = true;
				colvarAVCFGCodeID.IsForeignKey = false;
				colvarAVCFGCodeID.IsReadOnly = false;
				colvarAVCFGCodeID.DefaultSetting = @"";
				colvarAVCFGCodeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAVCFGCodeID);

				TableSchema.TableColumn colvarAVCFGCode = new TableSchema.TableColumn(schema);
				colvarAVCFGCode.ColumnName = "AVCFGCode";
				colvarAVCFGCode.DataType = DbType.AnsiString;
				colvarAVCFGCode.MaxLength = 50;
				colvarAVCFGCode.AutoIncrement = false;
				colvarAVCFGCode.IsNullable = false;
				colvarAVCFGCode.IsPrimaryKey = false;
				colvarAVCFGCode.IsForeignKey = false;
				colvarAVCFGCode.IsReadOnly = false;
				colvarAVCFGCode.DefaultSetting = @"";
				colvarAVCFGCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAVCFGCode);

				TableSchema.TableColumn colvarCodeDescription = new TableSchema.TableColumn(schema);
				colvarCodeDescription.ColumnName = "CodeDescription";
				colvarCodeDescription.DataType = DbType.AnsiString;
				colvarCodeDescription.MaxLength = -1;
				colvarCodeDescription.AutoIncrement = false;
				colvarCodeDescription.IsNullable = false;
				colvarCodeDescription.IsPrimaryKey = false;
				colvarCodeDescription.IsForeignKey = false;
				colvarCodeDescription.IsReadOnly = false;
				colvarCodeDescription.DefaultSetting = @"";
				colvarCodeDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCodeDescription);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_AVCFGCodes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_AVCFGCode LoadFrom(LP_AVCFGCode item)
		{
			LP_AVCFGCode result = new LP_AVCFGCode();
			if (item.AVCFGCodeID != default(string)) {
				result.LoadByKey(item.AVCFGCodeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string AVCFGCodeID {
			get { return GetColumnValue<string>(Columns.AVCFGCodeID); }
			set {
				SetColumnValue(Columns.AVCFGCodeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AVCFGCodeID));
			}
		}
		[DataMember]
		public string AVCFGCode {
			get { return GetColumnValue<string>(Columns.AVCFGCode); }
			set {
				SetColumnValue(Columns.AVCFGCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AVCFGCode));
			}
		}
		[DataMember]
		public string CodeDescription {
			get { return GetColumnValue<string>(Columns.CodeDescription); }
			set {
				SetColumnValue(Columns.CodeDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CodeDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return AVCFGCode;
		}

		#region Typed Columns

		public static TableSchema.TableColumn AVCFGCodeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AVCFGCodeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CodeDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AVCFGCodeID = @"AVCFGCodeID";
			public static readonly string AVCFGCode = @"AVCFGCode";
			public static readonly string CodeDescription = @"CodeDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AVCFGCodeID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageAVCFGFF class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVCFGFFCollection : ActiveList<LP_CommandMessageAVCFGFF, LP_CommandMessageAVCFGFFCollection>
	{
		public static LP_CommandMessageAVCFGFFCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageAVCFGFFCollection result = new LP_CommandMessageAVCFGFFCollection();
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
			foreach (LP_CommandMessageAVCFGFF item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageAVCFGFFs table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVCFGFF : ActiveRecord<LP_CommandMessageAVCFGFF>, INotifyPropertyChanged
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

		public LP_CommandMessageAVCFGFF()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageAVCFGFFs", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarResponseToCommandMessageId = new TableSchema.TableColumn(schema);
				colvarResponseToCommandMessageId.ColumnName = "ResponseToCommandMessageId";
				colvarResponseToCommandMessageId.DataType = DbType.Int64;
				colvarResponseToCommandMessageId.MaxLength = 0;
				colvarResponseToCommandMessageId.AutoIncrement = false;
				colvarResponseToCommandMessageId.IsNullable = true;
				colvarResponseToCommandMessageId.IsPrimaryKey = false;
				colvarResponseToCommandMessageId.IsForeignKey = false;
				colvarResponseToCommandMessageId.IsReadOnly = false;
				colvarResponseToCommandMessageId.DefaultSetting = @"";
				colvarResponseToCommandMessageId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResponseToCommandMessageId);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 8;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarCode = new TableSchema.TableColumn(schema);
				colvarCode.ColumnName = "Code";
				colvarCode.DataType = DbType.AnsiString;
				colvarCode.MaxLength = 3;
				colvarCode.AutoIncrement = false;
				colvarCode.IsNullable = false;
				colvarCode.IsPrimaryKey = false;
				colvarCode.IsForeignKey = false;
				colvarCode.IsReadOnly = false;
				colvarCode.DefaultSetting = @"";
				colvarCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCode);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageAVCFGFFs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageAVCFGFF LoadFrom(LP_CommandMessageAVCFGFF item)
		{
			LP_CommandMessageAVCFGFF result = new LP_CommandMessageAVCFGFF();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long? ResponseToCommandMessageId {
			get { return GetColumnValue<long?>(Columns.ResponseToCommandMessageId); }
			set {
				SetColumnValue(Columns.ResponseToCommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResponseToCommandMessageId));
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
		public string Code {
			get { return GetColumnValue<string>(Columns.Code); }
			set {
				SetColumnValue(Columns.Code, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Code));
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

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageAVCFGFF_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ResponseToCommandMessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CodeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string ResponseToCommandMessageId = @"ResponseToCommandMessageId";
			public static readonly string Password = @"Password";
			public static readonly string Code = @"Code";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageAVRMC class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVRMCCollection : ActiveList<LP_CommandMessageAVRMC, LP_CommandMessageAVRMCCollection>
	{
		public static LP_CommandMessageAVRMCCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageAVRMCCollection result = new LP_CommandMessageAVRMCCollection();
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
			foreach (LP_CommandMessageAVRMC item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageAVRMCs table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageAVRMC : ActiveRecord<LP_CommandMessageAVRMC>, INotifyPropertyChanged
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

		public LP_CommandMessageAVRMC()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageAVRMCs", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
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
				colvarReqCommandMessageId.IsForeignKey = true;
				colvarReqCommandMessageId.IsReadOnly = false;
				colvarReqCommandMessageId.DefaultSetting = @"";
				colvarReqCommandMessageId.ForeignKeyTableName = "LP_CommandMessages";
				schema.Columns.Add(colvarReqCommandMessageId);

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
				colvarCreatedOn.DefaultSetting = @"(getdate())";
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
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageAVRMCs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageAVRMC LoadFrom(LP_CommandMessageAVRMC item)
		{
			LP_CommandMessageAVRMC result = new LP_CommandMessageAVRMC();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long? ReqCommandMessageId {
			get { return GetColumnValue<long?>(Columns.ReqCommandMessageId); }
			set {
				SetColumnValue(Columns.ReqCommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReqCommandMessageId));
			}
		}
		[DataMember]
		public DateTime? UTCDateTime {
			get { return GetColumnValue<DateTime?>(Columns.UTCDateTime); }
			set {
				SetColumnValue(Columns.UTCDateTime, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UTCDateTime));
			}
		}
		[DataMember]
		public string DeviceStatusId {
			get { return GetColumnValue<string>(Columns.DeviceStatusId); }
			set {
				SetColumnValue(Columns.DeviceStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceStatusId));
			}
		}
		[DataMember]
		public double? Latitude {
			get { return GetColumnValue<double?>(Columns.Latitude); }
			set {
				SetColumnValue(Columns.Latitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Latitude));
			}
		}
		[DataMember]
		public string NSIndicator {
			get { return GetColumnValue<string>(Columns.NSIndicator); }
			set {
				SetColumnValue(Columns.NSIndicator, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NSIndicator));
			}
		}
		[DataMember]
		public double? Longitude {
			get { return GetColumnValue<double?>(Columns.Longitude); }
			set {
				SetColumnValue(Columns.Longitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Longitude));
			}
		}
		[DataMember]
		public string EWIndicator {
			get { return GetColumnValue<string>(Columns.EWIndicator); }
			set {
				SetColumnValue(Columns.EWIndicator, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EWIndicator));
			}
		}
		[DataMember]
		public decimal? Speed {
			get { return GetColumnValue<decimal?>(Columns.Speed); }
			set {
				SetColumnValue(Columns.Speed, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Speed));
			}
		}
		[DataMember]
		public decimal? Course {
			get { return GetColumnValue<decimal?>(Columns.Course); }
			set {
				SetColumnValue(Columns.Course, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Course));
			}
		}
		[DataMember]
		public string EventCodeId {
			get { return GetColumnValue<string>(Columns.EventCodeId); }
			set {
				SetColumnValue(Columns.EventCodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventCodeId));
			}
		}
		[DataMember]
		public int? BatteryVoltage {
			get { return GetColumnValue<int?>(Columns.BatteryVoltage); }
			set {
				SetColumnValue(Columns.BatteryVoltage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BatteryVoltage));
			}
		}
		[DataMember]
		public int? CurrentMilage {
			get { return GetColumnValue<int?>(Columns.CurrentMilage); }
			set {
				SetColumnValue(Columns.CurrentMilage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CurrentMilage));
			}
		}
		[DataMember]
		public bool? GPSStatus {
			get { return GetColumnValue<bool?>(Columns.GPSStatus); }
			set {
				SetColumnValue(Columns.GPSStatus, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GPSStatus));
			}
		}
		[DataMember]
		public bool? AnalogPort1 {
			get { return GetColumnValue<bool?>(Columns.AnalogPort1); }
			set {
				SetColumnValue(Columns.AnalogPort1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AnalogPort1));
			}
		}
		[DataMember]
		public bool? AnalogPort2 {
			get { return GetColumnValue<bool?>(Columns.AnalogPort2); }
			set {
				SetColumnValue(Columns.AnalogPort2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AnalogPort2));
			}
		}
		[DataMember]
		public DateTime? ProcessedDate {
			get { return GetColumnValue<DateTime?>(Columns.ProcessedDate); }
			set {
				SetColumnValue(Columns.ProcessedDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProcessedDate));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageAVRMC_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		private LP_CommandMessage _ReqCommandMessage;
		//Relationship: FK_LP_CommandMessageAVRMCs_LP_CommandMessages
		public LP_CommandMessage ReqCommandMessage
		{
			get
			{
				if(_ReqCommandMessage == null) {
					_ReqCommandMessage = LP_CommandMessage.FetchByID(this.ReqCommandMessageId);
				}
				return _ReqCommandMessage;
			}
			set
			{
				SetColumnValue("ReqCommandMessageId", value.CommandMessageID);
				_ReqCommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

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
		public static TableSchema.TableColumn UTCDateTimeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DeviceStatusIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LatitudeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn NSIndicatorColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn EWIndicatorColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn SpeedColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CourseColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn EventCodeIdColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn BatteryVoltageColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CurrentMilageColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn GPSStatusColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn AnalogPort1Column
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn AnalogPort2Column
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn ProcessedDateColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[18]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string ReqCommandMessageId = @"ReqCommandMessageId";
			public static readonly string UTCDateTime = @"UTCDateTime";
			public static readonly string DeviceStatusId = @"DeviceStatusId";
			public static readonly string Latitude = @"Latitude";
			public static readonly string NSIndicator = @"NSIndicator";
			public static readonly string Longitude = @"Longitude";
			public static readonly string EWIndicator = @"EWIndicator";
			public static readonly string Speed = @"Speed";
			public static readonly string Course = @"Course";
			public static readonly string EventCodeId = @"EventCodeId";
			public static readonly string BatteryVoltage = @"BatteryVoltage";
			public static readonly string CurrentMilage = @"CurrentMilage";
			public static readonly string GPSStatus = @"GPSStatus";
			public static readonly string AnalogPort1 = @"AnalogPort1";
			public static readonly string AnalogPort2 = @"AnalogPort2";
			public static readonly string ProcessedDate = @"ProcessedDate";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVACK class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVACKCollection : ActiveList<LP_CommandMessageEAVACK, LP_CommandMessageEAVACKCollection>
	{
		public static LP_CommandMessageEAVACKCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVACKCollection result = new LP_CommandMessageEAVACKCollection();
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
			foreach (LP_CommandMessageEAVACK item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageEAVACKs table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVACK : ActiveRecord<LP_CommandMessageEAVACK>, INotifyPropertyChanged
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

		public LP_CommandMessageEAVACK()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageEAVACKs", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarResponseToCommandMessageId = new TableSchema.TableColumn(schema);
				colvarResponseToCommandMessageId.ColumnName = "ResponseToCommandMessageId";
				colvarResponseToCommandMessageId.DataType = DbType.Int64;
				colvarResponseToCommandMessageId.MaxLength = 0;
				colvarResponseToCommandMessageId.AutoIncrement = false;
				colvarResponseToCommandMessageId.IsNullable = true;
				colvarResponseToCommandMessageId.IsPrimaryKey = false;
				colvarResponseToCommandMessageId.IsForeignKey = false;
				colvarResponseToCommandMessageId.IsReadOnly = false;
				colvarResponseToCommandMessageId.DefaultSetting = @"";
				colvarResponseToCommandMessageId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResponseToCommandMessageId);

				TableSchema.TableColumn colvarAckCode = new TableSchema.TableColumn(schema);
				colvarAckCode.ColumnName = "AckCode";
				colvarAckCode.DataType = DbType.AnsiString;
				colvarAckCode.MaxLength = 2;
				colvarAckCode.AutoIncrement = false;
				colvarAckCode.IsNullable = false;
				colvarAckCode.IsPrimaryKey = false;
				colvarAckCode.IsForeignKey = false;
				colvarAckCode.IsReadOnly = false;
				colvarAckCode.DefaultSetting = @"";
				colvarAckCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAckCode);

				TableSchema.TableColumn colvarAckSum = new TableSchema.TableColumn(schema);
				colvarAckSum.ColumnName = "AckSum";
				colvarAckSum.DataType = DbType.AnsiString;
				colvarAckSum.MaxLength = 5;
				colvarAckSum.AutoIncrement = false;
				colvarAckSum.IsNullable = false;
				colvarAckSum.IsPrimaryKey = false;
				colvarAckSum.IsForeignKey = false;
				colvarAckSum.IsReadOnly = false;
				colvarAckSum.DefaultSetting = @"";
				colvarAckSum.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAckSum);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageEAVACKs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageEAVACK LoadFrom(LP_CommandMessageEAVACK item)
		{
			LP_CommandMessageEAVACK result = new LP_CommandMessageEAVACK();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long? ResponseToCommandMessageId {
			get { return GetColumnValue<long?>(Columns.ResponseToCommandMessageId); }
			set {
				SetColumnValue(Columns.ResponseToCommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResponseToCommandMessageId));
			}
		}
		[DataMember]
		public string AckCode {
			get { return GetColumnValue<string>(Columns.AckCode); }
			set {
				SetColumnValue(Columns.AckCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AckCode));
			}
		}
		[DataMember]
		public string AckSum {
			get { return GetColumnValue<string>(Columns.AckSum); }
			set {
				SetColumnValue(Columns.AckSum, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AckSum));
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

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageEAVACKs_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ResponseToCommandMessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AckCodeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AckSumColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string ResponseToCommandMessageId = @"ResponseToCommandMessageId";
			public static readonly string AckCode = @"AckCode";
			public static readonly string AckSum = @"AckSum";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVGOF3 class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVGOF3Collection : ActiveList<LP_CommandMessageEAVGOF3, LP_CommandMessageEAVGOF3Collection>
	{
		public static LP_CommandMessageEAVGOF3Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVGOF3Collection result = new LP_CommandMessageEAVGOF3Collection();
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
			foreach (LP_CommandMessageEAVGOF3 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageEAVGOF3s table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVGOF3 : ActiveRecord<LP_CommandMessageEAVGOF3>, INotifyPropertyChanged
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

		public LP_CommandMessageEAVGOF3()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageEAVGOF3s", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarTotal = new TableSchema.TableColumn(schema);
				colvarTotal.ColumnName = "Total";
				colvarTotal.DataType = DbType.Byte;
				colvarTotal.MaxLength = 0;
				colvarTotal.AutoIncrement = false;
				colvarTotal.IsNullable = false;
				colvarTotal.IsPrimaryKey = false;
				colvarTotal.IsForeignKey = false;
				colvarTotal.IsReadOnly = false;
				colvarTotal.DefaultSetting = @"((1))";
				colvarTotal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotal);

				TableSchema.TableColumn colvarGeoFenceI = new TableSchema.TableColumn(schema);
				colvarGeoFenceI.ColumnName = "GeoFenceI";
				colvarGeoFenceI.DataType = DbType.Byte;
				colvarGeoFenceI.MaxLength = 0;
				colvarGeoFenceI.AutoIncrement = false;
				colvarGeoFenceI.IsNullable = false;
				colvarGeoFenceI.IsPrimaryKey = false;
				colvarGeoFenceI.IsForeignKey = false;
				colvarGeoFenceI.IsReadOnly = false;
				colvarGeoFenceI.DefaultSetting = @"((0))";
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
				colvarReportModeI.DefaultSetting = @"((3))";
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
				colvarLattitudeI1.DefaultSetting = @"((0))";
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
				colvarLongitudeI1.DefaultSetting = @"((0))";
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
				colvarLattitudeI2.DefaultSetting = @"((0))";
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
				colvarLongitudeI2.DefaultSetting = @"((0))";
				colvarLongitudeI2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeI2);

				TableSchema.TableColumn colvarGeoFenceJ = new TableSchema.TableColumn(schema);
				colvarGeoFenceJ.ColumnName = "GeoFenceJ";
				colvarGeoFenceJ.DataType = DbType.Byte;
				colvarGeoFenceJ.MaxLength = 0;
				colvarGeoFenceJ.AutoIncrement = false;
				colvarGeoFenceJ.IsNullable = true;
				colvarGeoFenceJ.IsPrimaryKey = false;
				colvarGeoFenceJ.IsForeignKey = false;
				colvarGeoFenceJ.IsReadOnly = false;
				colvarGeoFenceJ.DefaultSetting = @"";
				colvarGeoFenceJ.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceJ);

				TableSchema.TableColumn colvarReportModeJ = new TableSchema.TableColumn(schema);
				colvarReportModeJ.ColumnName = "ReportModeJ";
				colvarReportModeJ.DataType = DbType.AnsiStringFixedLength;
				colvarReportModeJ.MaxLength = 10;
				colvarReportModeJ.AutoIncrement = false;
				colvarReportModeJ.IsNullable = true;
				colvarReportModeJ.IsPrimaryKey = false;
				colvarReportModeJ.IsForeignKey = false;
				colvarReportModeJ.IsReadOnly = false;
				colvarReportModeJ.DefaultSetting = @"";
				colvarReportModeJ.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeJ);

				TableSchema.TableColumn colvarLattitudeJ1 = new TableSchema.TableColumn(schema);
				colvarLattitudeJ1.ColumnName = "LattitudeJ1";
				colvarLattitudeJ1.DataType = DbType.Double;
				colvarLattitudeJ1.MaxLength = 0;
				colvarLattitudeJ1.AutoIncrement = false;
				colvarLattitudeJ1.IsNullable = true;
				colvarLattitudeJ1.IsPrimaryKey = false;
				colvarLattitudeJ1.IsForeignKey = false;
				colvarLattitudeJ1.IsReadOnly = false;
				colvarLattitudeJ1.DefaultSetting = @"";
				colvarLattitudeJ1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeJ1);

				TableSchema.TableColumn colvarLongitudeJ1 = new TableSchema.TableColumn(schema);
				colvarLongitudeJ1.ColumnName = "LongitudeJ1";
				colvarLongitudeJ1.DataType = DbType.Double;
				colvarLongitudeJ1.MaxLength = 0;
				colvarLongitudeJ1.AutoIncrement = false;
				colvarLongitudeJ1.IsNullable = true;
				colvarLongitudeJ1.IsPrimaryKey = false;
				colvarLongitudeJ1.IsForeignKey = false;
				colvarLongitudeJ1.IsReadOnly = false;
				colvarLongitudeJ1.DefaultSetting = @"";
				colvarLongitudeJ1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeJ1);

				TableSchema.TableColumn colvarLattitudeJ2 = new TableSchema.TableColumn(schema);
				colvarLattitudeJ2.ColumnName = "LattitudeJ2";
				colvarLattitudeJ2.DataType = DbType.Double;
				colvarLattitudeJ2.MaxLength = 0;
				colvarLattitudeJ2.AutoIncrement = false;
				colvarLattitudeJ2.IsNullable = true;
				colvarLattitudeJ2.IsPrimaryKey = false;
				colvarLattitudeJ2.IsForeignKey = false;
				colvarLattitudeJ2.IsReadOnly = false;
				colvarLattitudeJ2.DefaultSetting = @"";
				colvarLattitudeJ2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeJ2);

				TableSchema.TableColumn colvarLongitudeJ2 = new TableSchema.TableColumn(schema);
				colvarLongitudeJ2.ColumnName = "LongitudeJ2";
				colvarLongitudeJ2.DataType = DbType.Double;
				colvarLongitudeJ2.MaxLength = 0;
				colvarLongitudeJ2.AutoIncrement = false;
				colvarLongitudeJ2.IsNullable = true;
				colvarLongitudeJ2.IsPrimaryKey = false;
				colvarLongitudeJ2.IsForeignKey = false;
				colvarLongitudeJ2.IsReadOnly = false;
				colvarLongitudeJ2.DefaultSetting = @"";
				colvarLongitudeJ2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeJ2);

				TableSchema.TableColumn colvarGeoFenceK = new TableSchema.TableColumn(schema);
				colvarGeoFenceK.ColumnName = "GeoFenceK";
				colvarGeoFenceK.DataType = DbType.Byte;
				colvarGeoFenceK.MaxLength = 0;
				colvarGeoFenceK.AutoIncrement = false;
				colvarGeoFenceK.IsNullable = true;
				colvarGeoFenceK.IsPrimaryKey = false;
				colvarGeoFenceK.IsForeignKey = false;
				colvarGeoFenceK.IsReadOnly = false;
				colvarGeoFenceK.DefaultSetting = @"";
				colvarGeoFenceK.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceK);

				TableSchema.TableColumn colvarReportModeK = new TableSchema.TableColumn(schema);
				colvarReportModeK.ColumnName = "ReportModeK";
				colvarReportModeK.DataType = DbType.AnsiStringFixedLength;
				colvarReportModeK.MaxLength = 10;
				colvarReportModeK.AutoIncrement = false;
				colvarReportModeK.IsNullable = true;
				colvarReportModeK.IsPrimaryKey = false;
				colvarReportModeK.IsForeignKey = false;
				colvarReportModeK.IsReadOnly = false;
				colvarReportModeK.DefaultSetting = @"";
				colvarReportModeK.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeK);

				TableSchema.TableColumn colvarLattitudeK1 = new TableSchema.TableColumn(schema);
				colvarLattitudeK1.ColumnName = "LattitudeK1";
				colvarLattitudeK1.DataType = DbType.Double;
				colvarLattitudeK1.MaxLength = 0;
				colvarLattitudeK1.AutoIncrement = false;
				colvarLattitudeK1.IsNullable = true;
				colvarLattitudeK1.IsPrimaryKey = false;
				colvarLattitudeK1.IsForeignKey = false;
				colvarLattitudeK1.IsReadOnly = false;
				colvarLattitudeK1.DefaultSetting = @"";
				colvarLattitudeK1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeK1);

				TableSchema.TableColumn colvarLongitudeK1 = new TableSchema.TableColumn(schema);
				colvarLongitudeK1.ColumnName = "LongitudeK1";
				colvarLongitudeK1.DataType = DbType.Double;
				colvarLongitudeK1.MaxLength = 0;
				colvarLongitudeK1.AutoIncrement = false;
				colvarLongitudeK1.IsNullable = true;
				colvarLongitudeK1.IsPrimaryKey = false;
				colvarLongitudeK1.IsForeignKey = false;
				colvarLongitudeK1.IsReadOnly = false;
				colvarLongitudeK1.DefaultSetting = @"";
				colvarLongitudeK1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeK1);

				TableSchema.TableColumn colvarLattitudeK2 = new TableSchema.TableColumn(schema);
				colvarLattitudeK2.ColumnName = "LattitudeK2";
				colvarLattitudeK2.DataType = DbType.Double;
				colvarLattitudeK2.MaxLength = 0;
				colvarLattitudeK2.AutoIncrement = false;
				colvarLattitudeK2.IsNullable = true;
				colvarLattitudeK2.IsPrimaryKey = false;
				colvarLattitudeK2.IsForeignKey = false;
				colvarLattitudeK2.IsReadOnly = false;
				colvarLattitudeK2.DefaultSetting = @"";
				colvarLattitudeK2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeK2);

				TableSchema.TableColumn colvarLongitudeK2 = new TableSchema.TableColumn(schema);
				colvarLongitudeK2.ColumnName = "LongitudeK2";
				colvarLongitudeK2.DataType = DbType.Double;
				colvarLongitudeK2.MaxLength = 0;
				colvarLongitudeK2.AutoIncrement = false;
				colvarLongitudeK2.IsNullable = true;
				colvarLongitudeK2.IsPrimaryKey = false;
				colvarLongitudeK2.IsForeignKey = false;
				colvarLongitudeK2.IsReadOnly = false;
				colvarLongitudeK2.DefaultSetting = @"";
				colvarLongitudeK2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeK2);

				TableSchema.TableColumn colvarGeoFenceL = new TableSchema.TableColumn(schema);
				colvarGeoFenceL.ColumnName = "GeoFenceL";
				colvarGeoFenceL.DataType = DbType.Byte;
				colvarGeoFenceL.MaxLength = 0;
				colvarGeoFenceL.AutoIncrement = false;
				colvarGeoFenceL.IsNullable = true;
				colvarGeoFenceL.IsPrimaryKey = false;
				colvarGeoFenceL.IsForeignKey = false;
				colvarGeoFenceL.IsReadOnly = false;
				colvarGeoFenceL.DefaultSetting = @"";
				colvarGeoFenceL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceL);

				TableSchema.TableColumn colvarReportModeL = new TableSchema.TableColumn(schema);
				colvarReportModeL.ColumnName = "ReportModeL";
				colvarReportModeL.DataType = DbType.AnsiStringFixedLength;
				colvarReportModeL.MaxLength = 10;
				colvarReportModeL.AutoIncrement = false;
				colvarReportModeL.IsNullable = true;
				colvarReportModeL.IsPrimaryKey = false;
				colvarReportModeL.IsForeignKey = false;
				colvarReportModeL.IsReadOnly = false;
				colvarReportModeL.DefaultSetting = @"";
				colvarReportModeL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeL);

				TableSchema.TableColumn colvarLattitudeL1 = new TableSchema.TableColumn(schema);
				colvarLattitudeL1.ColumnName = "LattitudeL1";
				colvarLattitudeL1.DataType = DbType.Double;
				colvarLattitudeL1.MaxLength = 0;
				colvarLattitudeL1.AutoIncrement = false;
				colvarLattitudeL1.IsNullable = true;
				colvarLattitudeL1.IsPrimaryKey = false;
				colvarLattitudeL1.IsForeignKey = false;
				colvarLattitudeL1.IsReadOnly = false;
				colvarLattitudeL1.DefaultSetting = @"";
				colvarLattitudeL1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeL1);

				TableSchema.TableColumn colvarLongitudeL1 = new TableSchema.TableColumn(schema);
				colvarLongitudeL1.ColumnName = "LongitudeL1";
				colvarLongitudeL1.DataType = DbType.Double;
				colvarLongitudeL1.MaxLength = 0;
				colvarLongitudeL1.AutoIncrement = false;
				colvarLongitudeL1.IsNullable = true;
				colvarLongitudeL1.IsPrimaryKey = false;
				colvarLongitudeL1.IsForeignKey = false;
				colvarLongitudeL1.IsReadOnly = false;
				colvarLongitudeL1.DefaultSetting = @"";
				colvarLongitudeL1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeL1);

				TableSchema.TableColumn colvarLattitudeL2 = new TableSchema.TableColumn(schema);
				colvarLattitudeL2.ColumnName = "LattitudeL2";
				colvarLattitudeL2.DataType = DbType.Double;
				colvarLattitudeL2.MaxLength = 0;
				colvarLattitudeL2.AutoIncrement = false;
				colvarLattitudeL2.IsNullable = true;
				colvarLattitudeL2.IsPrimaryKey = false;
				colvarLattitudeL2.IsForeignKey = false;
				colvarLattitudeL2.IsReadOnly = false;
				colvarLattitudeL2.DefaultSetting = @"";
				colvarLattitudeL2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeL2);

				TableSchema.TableColumn colvarLongitudeL2 = new TableSchema.TableColumn(schema);
				colvarLongitudeL2.ColumnName = "LongitudeL2";
				colvarLongitudeL2.DataType = DbType.Double;
				colvarLongitudeL2.MaxLength = 0;
				colvarLongitudeL2.AutoIncrement = false;
				colvarLongitudeL2.IsNullable = true;
				colvarLongitudeL2.IsPrimaryKey = false;
				colvarLongitudeL2.IsForeignKey = false;
				colvarLongitudeL2.IsReadOnly = false;
				colvarLongitudeL2.DefaultSetting = @"";
				colvarLongitudeL2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeL2);

				TableSchema.TableColumn colvarGeoFenceM = new TableSchema.TableColumn(schema);
				colvarGeoFenceM.ColumnName = "GeoFenceM";
				colvarGeoFenceM.DataType = DbType.Byte;
				colvarGeoFenceM.MaxLength = 0;
				colvarGeoFenceM.AutoIncrement = false;
				colvarGeoFenceM.IsNullable = true;
				colvarGeoFenceM.IsPrimaryKey = false;
				colvarGeoFenceM.IsForeignKey = false;
				colvarGeoFenceM.IsReadOnly = false;
				colvarGeoFenceM.DefaultSetting = @"";
				colvarGeoFenceM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceM);

				TableSchema.TableColumn colvarReportModeM = new TableSchema.TableColumn(schema);
				colvarReportModeM.ColumnName = "ReportModeM";
				colvarReportModeM.DataType = DbType.AnsiStringFixedLength;
				colvarReportModeM.MaxLength = 10;
				colvarReportModeM.AutoIncrement = false;
				colvarReportModeM.IsNullable = true;
				colvarReportModeM.IsPrimaryKey = false;
				colvarReportModeM.IsForeignKey = false;
				colvarReportModeM.IsReadOnly = false;
				colvarReportModeM.DefaultSetting = @"";
				colvarReportModeM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportModeM);

				TableSchema.TableColumn colvarLattitudeM1 = new TableSchema.TableColumn(schema);
				colvarLattitudeM1.ColumnName = "LattitudeM1";
				colvarLattitudeM1.DataType = DbType.Double;
				colvarLattitudeM1.MaxLength = 0;
				colvarLattitudeM1.AutoIncrement = false;
				colvarLattitudeM1.IsNullable = true;
				colvarLattitudeM1.IsPrimaryKey = false;
				colvarLattitudeM1.IsForeignKey = false;
				colvarLattitudeM1.IsReadOnly = false;
				colvarLattitudeM1.DefaultSetting = @"";
				colvarLattitudeM1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeM1);

				TableSchema.TableColumn colvarLongitudeM1 = new TableSchema.TableColumn(schema);
				colvarLongitudeM1.ColumnName = "LongitudeM1";
				colvarLongitudeM1.DataType = DbType.Double;
				colvarLongitudeM1.MaxLength = 0;
				colvarLongitudeM1.AutoIncrement = false;
				colvarLongitudeM1.IsNullable = true;
				colvarLongitudeM1.IsPrimaryKey = false;
				colvarLongitudeM1.IsForeignKey = false;
				colvarLongitudeM1.IsReadOnly = false;
				colvarLongitudeM1.DefaultSetting = @"";
				colvarLongitudeM1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeM1);

				TableSchema.TableColumn colvarLattitudeM2 = new TableSchema.TableColumn(schema);
				colvarLattitudeM2.ColumnName = "LattitudeM2";
				colvarLattitudeM2.DataType = DbType.Double;
				colvarLattitudeM2.MaxLength = 0;
				colvarLattitudeM2.AutoIncrement = false;
				colvarLattitudeM2.IsNullable = true;
				colvarLattitudeM2.IsPrimaryKey = false;
				colvarLattitudeM2.IsForeignKey = false;
				colvarLattitudeM2.IsReadOnly = false;
				colvarLattitudeM2.DefaultSetting = @"";
				colvarLattitudeM2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLattitudeM2);

				TableSchema.TableColumn colvarLongitudeM2 = new TableSchema.TableColumn(schema);
				colvarLongitudeM2.ColumnName = "LongitudeM2";
				colvarLongitudeM2.DataType = DbType.Double;
				colvarLongitudeM2.MaxLength = 0;
				colvarLongitudeM2.AutoIncrement = false;
				colvarLongitudeM2.IsNullable = true;
				colvarLongitudeM2.IsPrimaryKey = false;
				colvarLongitudeM2.IsForeignKey = false;
				colvarLongitudeM2.IsReadOnly = false;
				colvarLongitudeM2.DefaultSetting = @"";
				colvarLongitudeM2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeM2);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageEAVGOF3s",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageEAVGOF3 LoadFrom(LP_CommandMessageEAVGOF3 item)
		{
			LP_CommandMessageEAVGOF3 result = new LP_CommandMessageEAVGOF3();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public byte Total {
			get { return GetColumnValue<byte>(Columns.Total); }
			set {
				SetColumnValue(Columns.Total, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Total));
			}
		}
		[DataMember]
		public byte GeoFenceI {
			get { return GetColumnValue<byte>(Columns.GeoFenceI); }
			set {
				SetColumnValue(Columns.GeoFenceI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceI));
			}
		}
		[DataMember]
		public string ReportModeI {
			get { return GetColumnValue<string>(Columns.ReportModeI); }
			set {
				SetColumnValue(Columns.ReportModeI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeI));
			}
		}
		[DataMember]
		public double LattitudeI1 {
			get { return GetColumnValue<double>(Columns.LattitudeI1); }
			set {
				SetColumnValue(Columns.LattitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI1));
			}
		}
		[DataMember]
		public double LongitudeI1 {
			get { return GetColumnValue<double>(Columns.LongitudeI1); }
			set {
				SetColumnValue(Columns.LongitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI1));
			}
		}
		[DataMember]
		public double LattitudeI2 {
			get { return GetColumnValue<double>(Columns.LattitudeI2); }
			set {
				SetColumnValue(Columns.LattitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI2));
			}
		}
		[DataMember]
		public double LongitudeI2 {
			get { return GetColumnValue<double>(Columns.LongitudeI2); }
			set {
				SetColumnValue(Columns.LongitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI2));
			}
		}
		[DataMember]
		public byte? GeoFenceJ {
			get { return GetColumnValue<byte?>(Columns.GeoFenceJ); }
			set {
				SetColumnValue(Columns.GeoFenceJ, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceJ));
			}
		}
		[DataMember]
		public string ReportModeJ {
			get { return GetColumnValue<string>(Columns.ReportModeJ); }
			set {
				SetColumnValue(Columns.ReportModeJ, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeJ));
			}
		}
		[DataMember]
		public double? LattitudeJ1 {
			get { return GetColumnValue<double?>(Columns.LattitudeJ1); }
			set {
				SetColumnValue(Columns.LattitudeJ1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeJ1));
			}
		}
		[DataMember]
		public double? LongitudeJ1 {
			get { return GetColumnValue<double?>(Columns.LongitudeJ1); }
			set {
				SetColumnValue(Columns.LongitudeJ1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeJ1));
			}
		}
		[DataMember]
		public double? LattitudeJ2 {
			get { return GetColumnValue<double?>(Columns.LattitudeJ2); }
			set {
				SetColumnValue(Columns.LattitudeJ2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeJ2));
			}
		}
		[DataMember]
		public double? LongitudeJ2 {
			get { return GetColumnValue<double?>(Columns.LongitudeJ2); }
			set {
				SetColumnValue(Columns.LongitudeJ2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeJ2));
			}
		}
		[DataMember]
		public byte? GeoFenceK {
			get { return GetColumnValue<byte?>(Columns.GeoFenceK); }
			set {
				SetColumnValue(Columns.GeoFenceK, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceK));
			}
		}
		[DataMember]
		public string ReportModeK {
			get { return GetColumnValue<string>(Columns.ReportModeK); }
			set {
				SetColumnValue(Columns.ReportModeK, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeK));
			}
		}
		[DataMember]
		public double? LattitudeK1 {
			get { return GetColumnValue<double?>(Columns.LattitudeK1); }
			set {
				SetColumnValue(Columns.LattitudeK1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeK1));
			}
		}
		[DataMember]
		public double? LongitudeK1 {
			get { return GetColumnValue<double?>(Columns.LongitudeK1); }
			set {
				SetColumnValue(Columns.LongitudeK1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeK1));
			}
		}
		[DataMember]
		public double? LattitudeK2 {
			get { return GetColumnValue<double?>(Columns.LattitudeK2); }
			set {
				SetColumnValue(Columns.LattitudeK2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeK2));
			}
		}
		[DataMember]
		public double? LongitudeK2 {
			get { return GetColumnValue<double?>(Columns.LongitudeK2); }
			set {
				SetColumnValue(Columns.LongitudeK2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeK2));
			}
		}
		[DataMember]
		public byte? GeoFenceL {
			get { return GetColumnValue<byte?>(Columns.GeoFenceL); }
			set {
				SetColumnValue(Columns.GeoFenceL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceL));
			}
		}
		[DataMember]
		public string ReportModeL {
			get { return GetColumnValue<string>(Columns.ReportModeL); }
			set {
				SetColumnValue(Columns.ReportModeL, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeL));
			}
		}
		[DataMember]
		public double? LattitudeL1 {
			get { return GetColumnValue<double?>(Columns.LattitudeL1); }
			set {
				SetColumnValue(Columns.LattitudeL1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeL1));
			}
		}
		[DataMember]
		public double? LongitudeL1 {
			get { return GetColumnValue<double?>(Columns.LongitudeL1); }
			set {
				SetColumnValue(Columns.LongitudeL1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeL1));
			}
		}
		[DataMember]
		public double? LattitudeL2 {
			get { return GetColumnValue<double?>(Columns.LattitudeL2); }
			set {
				SetColumnValue(Columns.LattitudeL2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeL2));
			}
		}
		[DataMember]
		public double? LongitudeL2 {
			get { return GetColumnValue<double?>(Columns.LongitudeL2); }
			set {
				SetColumnValue(Columns.LongitudeL2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeL2));
			}
		}
		[DataMember]
		public byte? GeoFenceM {
			get { return GetColumnValue<byte?>(Columns.GeoFenceM); }
			set {
				SetColumnValue(Columns.GeoFenceM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceM));
			}
		}
		[DataMember]
		public string ReportModeM {
			get { return GetColumnValue<string>(Columns.ReportModeM); }
			set {
				SetColumnValue(Columns.ReportModeM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeM));
			}
		}
		[DataMember]
		public double? LattitudeM1 {
			get { return GetColumnValue<double?>(Columns.LattitudeM1); }
			set {
				SetColumnValue(Columns.LattitudeM1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeM1));
			}
		}
		[DataMember]
		public double? LongitudeM1 {
			get { return GetColumnValue<double?>(Columns.LongitudeM1); }
			set {
				SetColumnValue(Columns.LongitudeM1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeM1));
			}
		}
		[DataMember]
		public double? LattitudeM2 {
			get { return GetColumnValue<double?>(Columns.LattitudeM2); }
			set {
				SetColumnValue(Columns.LattitudeM2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeM2));
			}
		}
		[DataMember]
		public double? LongitudeM2 {
			get { return GetColumnValue<double?>(Columns.LongitudeM2); }
			set {
				SetColumnValue(Columns.LongitudeM2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeM2));
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

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageEAVGOF3s_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TotalColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GeoFenceIColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ReportModeIColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LattitudeI1Column
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LongitudeI1Column
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LattitudeI2Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LongitudeI2Column
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn GeoFenceJColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ReportModeJColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn LattitudeJ1Column
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn LongitudeJ1Column
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn LattitudeJ2Column
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn LongitudeJ2Column
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn GeoFenceKColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ReportModeKColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn LattitudeK1Column
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn LongitudeK1Column
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn LattitudeK2Column
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn LongitudeK2Column
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn GeoFenceLColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn ReportModeLColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn LattitudeL1Column
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn LongitudeL1Column
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn LattitudeL2Column
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn LongitudeL2Column
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn GeoFenceMColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn ReportModeMColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn LattitudeM1Column
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn LongitudeM1Column
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn LattitudeM2Column
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn LongitudeM2Column
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[32]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string Total = @"Total";
			public static readonly string GeoFenceI = @"GeoFenceI";
			public static readonly string ReportModeI = @"ReportModeI";
			public static readonly string LattitudeI1 = @"LattitudeI1";
			public static readonly string LongitudeI1 = @"LongitudeI1";
			public static readonly string LattitudeI2 = @"LattitudeI2";
			public static readonly string LongitudeI2 = @"LongitudeI2";
			public static readonly string GeoFenceJ = @"GeoFenceJ";
			public static readonly string ReportModeJ = @"ReportModeJ";
			public static readonly string LattitudeJ1 = @"LattitudeJ1";
			public static readonly string LongitudeJ1 = @"LongitudeJ1";
			public static readonly string LattitudeJ2 = @"LattitudeJ2";
			public static readonly string LongitudeJ2 = @"LongitudeJ2";
			public static readonly string GeoFenceK = @"GeoFenceK";
			public static readonly string ReportModeK = @"ReportModeK";
			public static readonly string LattitudeK1 = @"LattitudeK1";
			public static readonly string LongitudeK1 = @"LongitudeK1";
			public static readonly string LattitudeK2 = @"LattitudeK2";
			public static readonly string LongitudeK2 = @"LongitudeK2";
			public static readonly string GeoFenceL = @"GeoFenceL";
			public static readonly string ReportModeL = @"ReportModeL";
			public static readonly string LattitudeL1 = @"LattitudeL1";
			public static readonly string LongitudeL1 = @"LongitudeL1";
			public static readonly string LattitudeL2 = @"LattitudeL2";
			public static readonly string LongitudeL2 = @"LongitudeL2";
			public static readonly string GeoFenceM = @"GeoFenceM";
			public static readonly string ReportModeM = @"ReportModeM";
			public static readonly string LattitudeM1 = @"LattitudeM1";
			public static readonly string LongitudeM1 = @"LongitudeM1";
			public static readonly string LattitudeM2 = @"LattitudeM2";
			public static readonly string LongitudeM2 = @"LongitudeM2";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVGOF6 class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVGOF6Collection : ActiveList<LP_CommandMessageEAVGOF6, LP_CommandMessageEAVGOF6Collection>
	{
		public static LP_CommandMessageEAVGOF6Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVGOF6Collection result = new LP_CommandMessageEAVGOF6Collection();
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
			foreach (LP_CommandMessageEAVGOF6 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageEAVGOF6s table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVGOF6 : ActiveRecord<LP_CommandMessageEAVGOF6>, INotifyPropertyChanged
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

		public LP_CommandMessageEAVGOF6()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageEAVGOF6s", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarGeoFenceI = new TableSchema.TableColumn(schema);
				colvarGeoFenceI.ColumnName = "GeoFenceI";
				colvarGeoFenceI.DataType = DbType.Byte;
				colvarGeoFenceI.MaxLength = 0;
				colvarGeoFenceI.AutoIncrement = false;
				colvarGeoFenceI.IsNullable = false;
				colvarGeoFenceI.IsPrimaryKey = false;
				colvarGeoFenceI.IsForeignKey = false;
				colvarGeoFenceI.IsReadOnly = false;
				colvarGeoFenceI.DefaultSetting = @"((0))";
				colvarGeoFenceI.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceI);

				TableSchema.TableColumn colvarReportModeI = new TableSchema.TableColumn(schema);
				colvarReportModeI.ColumnName = "ReportModeI";
				colvarReportModeI.DataType = DbType.Byte;
				colvarReportModeI.MaxLength = 0;
				colvarReportModeI.AutoIncrement = false;
				colvarReportModeI.IsNullable = false;
				colvarReportModeI.IsPrimaryKey = false;
				colvarReportModeI.IsForeignKey = false;
				colvarReportModeI.IsReadOnly = false;
				colvarReportModeI.DefaultSetting = @"((3))";
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
				colvarLattitudeI1.DefaultSetting = @"((0))";
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
				colvarLongitudeI1.DefaultSetting = @"((0))";
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
				colvarLattitudeI2.DefaultSetting = @"((0))";
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
				colvarLongitudeI2.DefaultSetting = @"((0))";
				colvarLongitudeI2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeI2);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageEAVGOF6s",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageEAVGOF6 LoadFrom(LP_CommandMessageEAVGOF6 item)
		{
			LP_CommandMessageEAVGOF6 result = new LP_CommandMessageEAVGOF6();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public byte GeoFenceI {
			get { return GetColumnValue<byte>(Columns.GeoFenceI); }
			set {
				SetColumnValue(Columns.GeoFenceI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceI));
			}
		}
		[DataMember]
		public byte ReportModeI {
			get { return GetColumnValue<byte>(Columns.ReportModeI); }
			set {
				SetColumnValue(Columns.ReportModeI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeI));
			}
		}
		[DataMember]
		public double LattitudeI1 {
			get { return GetColumnValue<double>(Columns.LattitudeI1); }
			set {
				SetColumnValue(Columns.LattitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI1));
			}
		}
		[DataMember]
		public double LongitudeI1 {
			get { return GetColumnValue<double>(Columns.LongitudeI1); }
			set {
				SetColumnValue(Columns.LongitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI1));
			}
		}
		[DataMember]
		public double LattitudeI2 {
			get { return GetColumnValue<double>(Columns.LattitudeI2); }
			set {
				SetColumnValue(Columns.LattitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI2));
			}
		}
		[DataMember]
		public double LongitudeI2 {
			get { return GetColumnValue<double>(Columns.LongitudeI2); }
			set {
				SetColumnValue(Columns.LongitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI2));
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

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageEAVGOF6s_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn GeoFenceIColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReportModeIColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LattitudeI1Column
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LongitudeI1Column
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LattitudeI2Column
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LongitudeI2Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string GeoFenceI = @"GeoFenceI";
			public static readonly string ReportModeI = @"ReportModeI";
			public static readonly string LattitudeI1 = @"LattitudeI1";
			public static readonly string LongitudeI1 = @"LongitudeI1";
			public static readonly string LattitudeI2 = @"LattitudeI2";
			public static readonly string LongitudeI2 = @"LongitudeI2";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVRSP3 class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP3Collection : ActiveList<LP_CommandMessageEAVRSP3, LP_CommandMessageEAVRSP3Collection>
	{
		public static LP_CommandMessageEAVRSP3Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVRSP3Collection result = new LP_CommandMessageEAVRSP3Collection();
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
			foreach (LP_CommandMessageEAVRSP3 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageEAVRSP3s table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP3 : ActiveRecord<LP_CommandMessageEAVRSP3>, INotifyPropertyChanged
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

		public LP_CommandMessageEAVRSP3()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageEAVRSP3s", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

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

				TableSchema.TableColumn colvarRESPCode = new TableSchema.TableColumn(schema);
				colvarRESPCode.ColumnName = "RESPCode";
				colvarRESPCode.DataType = DbType.Int32;
				colvarRESPCode.MaxLength = 0;
				colvarRESPCode.AutoIncrement = false;
				colvarRESPCode.IsNullable = false;
				colvarRESPCode.IsPrimaryKey = false;
				colvarRESPCode.IsForeignKey = false;
				colvarRESPCode.IsReadOnly = false;
				colvarRESPCode.DefaultSetting = @"";
				colvarRESPCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRESPCode);

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
				colvarCreatedOn.DefaultSetting = @"(getdate())";
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
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageEAVRSP3s",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageEAVRSP3 LoadFrom(LP_CommandMessageEAVRSP3 item)
		{
			LP_CommandMessageEAVRSP3 result = new LP_CommandMessageEAVRSP3();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public int RESPCode {
			get { return GetColumnValue<int>(Columns.RESPCode); }
			set {
				SetColumnValue(Columns.RESPCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RESPCode));
			}
		}
		[DataMember]
		public DateTime? Processed {
			get { return GetColumnValue<DateTime?>(Columns.Processed); }
			set {
				SetColumnValue(Columns.Processed, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Processed));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageEAVRSP3s_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RESPCodeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ProcessedColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string UnitID = @"UnitID";
			public static readonly string RESPCode = @"RESPCode";
			public static readonly string Processed = @"Processed";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageEAVRSP4 class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP4Collection : ActiveList<LP_CommandMessageEAVRSP4, LP_CommandMessageEAVRSP4Collection>
	{
		public static LP_CommandMessageEAVRSP4Collection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageEAVRSP4Collection result = new LP_CommandMessageEAVRSP4Collection();
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
			foreach (LP_CommandMessageEAVRSP4 item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageEAVRSP4s table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageEAVRSP4 : ActiveRecord<LP_CommandMessageEAVRSP4>, INotifyPropertyChanged
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

		public LP_CommandMessageEAVRSP4()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageEAVRSP4s", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = false;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
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
				colvarLPGeoFenceId.IsForeignKey = true;
				colvarLPGeoFenceId.IsReadOnly = false;
				colvarLPGeoFenceId.DefaultSetting = @"";
				colvarLPGeoFenceId.ForeignKeyTableName = "LP_GsGeoFences";
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
				colvarLattitudeI1.DefaultSetting = @"((0))";
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
				colvarLongitudeI1.DefaultSetting = @"((0))";
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
				colvarLattitudeI2.DefaultSetting = @"((0))";
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
				colvarLongitudeI2.DefaultSetting = @"((0))";
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
				colvarCreatedOn.DefaultSetting = @"(getdate())";
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
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageEAVRSP4s",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageEAVRSP4 LoadFrom(LP_CommandMessageEAVRSP4 item)
		{
			LP_CommandMessageEAVRSP4 result = new LP_CommandMessageEAVRSP4();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public long? LPGeoFenceId {
			get { return GetColumnValue<long?>(Columns.LPGeoFenceId); }
			set {
				SetColumnValue(Columns.LPGeoFenceId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LPGeoFenceId));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public byte GeoFenceI {
			get { return GetColumnValue<byte>(Columns.GeoFenceI); }
			set {
				SetColumnValue(Columns.GeoFenceI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceI));
			}
		}
		[DataMember]
		public string ReportModeI {
			get { return GetColumnValue<string>(Columns.ReportModeI); }
			set {
				SetColumnValue(Columns.ReportModeI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeI));
			}
		}
		[DataMember]
		public double LattitudeI1 {
			get { return GetColumnValue<double>(Columns.LattitudeI1); }
			set {
				SetColumnValue(Columns.LattitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI1));
			}
		}
		[DataMember]
		public double LongitudeI1 {
			get { return GetColumnValue<double>(Columns.LongitudeI1); }
			set {
				SetColumnValue(Columns.LongitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI1));
			}
		}
		[DataMember]
		public double LattitudeI2 {
			get { return GetColumnValue<double>(Columns.LattitudeI2); }
			set {
				SetColumnValue(Columns.LattitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI2));
			}
		}
		[DataMember]
		public double LongitudeI2 {
			get { return GetColumnValue<double>(Columns.LongitudeI2); }
			set {
				SetColumnValue(Columns.LongitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI2));
			}
		}
		[DataMember]
		public DateTime? Processed {
			get { return GetColumnValue<DateTime?>(Columns.Processed); }
			set {
				SetColumnValue(Columns.Processed, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Processed));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageEAVRSP4_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageID);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageID", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		private LP_GsGeoFence _LPGeoFence;
		//Relationship: FK_LP_CommandMessageEAVRSP4s_LP_GsGeoFences
		public LP_GsGeoFence LPGeoFence
		{
			get
			{
				if(_LPGeoFence == null) {
					_LPGeoFence = LP_GsGeoFence.FetchByID(this.LPGeoFenceId);
				}
				return _LPGeoFence;
			}
			set
			{
				SetColumnValue("LPGeoFenceId", value.LPGeoFenceID);
				_LPGeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

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
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string LPGeoFenceId = @"LPGeoFenceId";
			public static readonly string UnitID = @"UnitID";
			public static readonly string GeoFenceI = @"GeoFenceI";
			public static readonly string ReportModeI = @"ReportModeI";
			public static readonly string LattitudeI1 = @"LattitudeI1";
			public static readonly string LongitudeI1 = @"LongitudeI1";
			public static readonly string LattitudeI2 = @"LattitudeI2";
			public static readonly string LongitudeI2 = @"LongitudeI2";
			public static readonly string Processed = @"Processed";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessageECHK class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageECHKCollection : ActiveList<LP_CommandMessageECHK, LP_CommandMessageECHKCollection>
	{
		public static LP_CommandMessageECHKCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageECHKCollection result = new LP_CommandMessageECHKCollection();
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
			foreach (LP_CommandMessageECHK item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessageECHKs table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageECHK : ActiveRecord<LP_CommandMessageECHK>, INotifyPropertyChanged
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

		public LP_CommandMessageECHK()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessageECHKs", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageECHKID = new TableSchema.TableColumn(schema);
				colvarCommandMessageECHKID.ColumnName = "CommandMessageECHKID";
				colvarCommandMessageECHKID.DataType = DbType.Int64;
				colvarCommandMessageECHKID.MaxLength = 0;
				colvarCommandMessageECHKID.AutoIncrement = true;
				colvarCommandMessageECHKID.IsNullable = false;
				colvarCommandMessageECHKID.IsPrimaryKey = true;
				colvarCommandMessageECHKID.IsForeignKey = false;
				colvarCommandMessageECHKID.IsReadOnly = false;
				colvarCommandMessageECHKID.DefaultSetting = @"";
				colvarCommandMessageECHKID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageECHKID);

				TableSchema.TableColumn colvarResponseToCommandMessageECHKId = new TableSchema.TableColumn(schema);
				colvarResponseToCommandMessageECHKId.ColumnName = "ResponseToCommandMessageECHKId";
				colvarResponseToCommandMessageECHKId.DataType = DbType.Int64;
				colvarResponseToCommandMessageECHKId.MaxLength = 0;
				colvarResponseToCommandMessageECHKId.AutoIncrement = false;
				colvarResponseToCommandMessageECHKId.IsNullable = true;
				colvarResponseToCommandMessageECHKId.IsPrimaryKey = false;
				colvarResponseToCommandMessageECHKId.IsForeignKey = true;
				colvarResponseToCommandMessageECHKId.IsReadOnly = false;
				colvarResponseToCommandMessageECHKId.DefaultSetting = @"";
				colvarResponseToCommandMessageECHKId.ForeignKeyTableName = "LP_CommandMessageECHKs";
				schema.Columns.Add(colvarResponseToCommandMessageECHKId);

				TableSchema.TableColumn colvarCommandMessageId = new TableSchema.TableColumn(schema);
				colvarCommandMessageId.ColumnName = "CommandMessageId";
				colvarCommandMessageId.DataType = DbType.Int64;
				colvarCommandMessageId.MaxLength = 0;
				colvarCommandMessageId.AutoIncrement = false;
				colvarCommandMessageId.IsNullable = false;
				colvarCommandMessageId.IsPrimaryKey = false;
				colvarCommandMessageId.IsForeignKey = true;
				colvarCommandMessageId.IsReadOnly = false;
				colvarCommandMessageId.DefaultSetting = @"";
				colvarCommandMessageId.ForeignKeyTableName = "LP_CommandMessages";
				schema.Columns.Add(colvarCommandMessageId);

				TableSchema.TableColumn colvarCommandTypeId = new TableSchema.TableColumn(schema);
				colvarCommandTypeId.ColumnName = "CommandTypeId";
				colvarCommandTypeId.DataType = DbType.AnsiString;
				colvarCommandTypeId.MaxLength = 20;
				colvarCommandTypeId.AutoIncrement = false;
				colvarCommandTypeId.IsNullable = false;
				colvarCommandTypeId.IsPrimaryKey = false;
				colvarCommandTypeId.IsForeignKey = true;
				colvarCommandTypeId.IsReadOnly = false;
				colvarCommandTypeId.DefaultSetting = @"";
				colvarCommandTypeId.ForeignKeyTableName = "LP_CommandTypes";
				schema.Columns.Add(colvarCommandTypeId);

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

				TableSchema.TableColumn colvarSeqNo = new TableSchema.TableColumn(schema);
				colvarSeqNo.ColumnName = "SeqNo";
				colvarSeqNo.DataType = DbType.AnsiString;
				colvarSeqNo.MaxLength = 2;
				colvarSeqNo.AutoIncrement = false;
				colvarSeqNo.IsNullable = false;
				colvarSeqNo.IsPrimaryKey = false;
				colvarSeqNo.IsForeignKey = false;
				colvarSeqNo.IsReadOnly = false;
				colvarSeqNo.DefaultSetting = @"";
				colvarSeqNo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeqNo);

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
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessageECHKs",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessageECHK LoadFrom(LP_CommandMessageECHK item)
		{
			LP_CommandMessageECHK result = new LP_CommandMessageECHK();
			if (item.CommandMessageECHKID != default(long)) {
				result.LoadByKey(item.CommandMessageECHKID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageECHKID {
			get { return GetColumnValue<long>(Columns.CommandMessageECHKID); }
			set {
				SetColumnValue(Columns.CommandMessageECHKID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageECHKID));
			}
		}
		[DataMember]
		public long? ResponseToCommandMessageECHKId {
			get { return GetColumnValue<long?>(Columns.ResponseToCommandMessageECHKId); }
			set {
				SetColumnValue(Columns.ResponseToCommandMessageECHKId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResponseToCommandMessageECHKId));
			}
		}
		[DataMember]
		public long CommandMessageId {
			get { return GetColumnValue<long>(Columns.CommandMessageId); }
			set {
				SetColumnValue(Columns.CommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageId));
			}
		}
		[DataMember]
		public string CommandTypeId {
			get { return GetColumnValue<string>(Columns.CommandTypeId); }
			set {
				SetColumnValue(Columns.CommandTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandTypeId));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public string SeqNo {
			get { return GetColumnValue<string>(Columns.SeqNo); }
			set {
				SetColumnValue(Columns.SeqNo, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SeqNo));
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

		private LP_CommandMessageECHK _ResponseToCommandMessageECHK;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandMessageECHKs
		public LP_CommandMessageECHK ResponseToCommandMessageECHK
		{
			get
			{
				if(_ResponseToCommandMessageECHK == null) {
					_ResponseToCommandMessageECHK = LP_CommandMessageECHK.FetchByID(this.ResponseToCommandMessageECHKId);
				}
				return _ResponseToCommandMessageECHK;
			}
			set
			{
				SetColumnValue("ResponseToCommandMessageECHKId", value.CommandMessageECHKID);
				_ResponseToCommandMessageECHK = value;
			}
		}

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageId);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageId", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		private LP_CommandType _CommandType;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandTypes
		public LP_CommandType CommandType
		{
			get
			{
				if(_CommandType == null) {
					_CommandType = LP_CommandType.FetchByID(this.CommandTypeId);
				}
				return _CommandType;
			}
			set
			{
				SetColumnValue("CommandTypeId", value.CommandTypeID);
				_CommandType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageECHKID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageECHKIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ResponseToCommandMessageECHKIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandMessageIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CommandTypeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn SeqNoColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageECHKID = @"CommandMessageECHKID";
			public static readonly string ResponseToCommandMessageECHKId = @"ResponseToCommandMessageECHKId";
			public static readonly string CommandMessageId = @"CommandMessageId";
			public static readonly string CommandTypeId = @"CommandTypeId";
			public static readonly string UnitID = @"UnitID";
			public static readonly string SeqNo = @"SeqNo";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageECHKID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageECHKCollection _ChildLP_CommandMessageECHKsCol;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandMessageECHKs
		public LP_CommandMessageECHKCollection ChildLP_CommandMessageECHKsCol
		{
			get
			{
				if(_ChildLP_CommandMessageECHKsCol == null) {
					_ChildLP_CommandMessageECHKsCol = new LP_CommandMessageECHKCollection();
					_ChildLP_CommandMessageECHKsCol.LoadAndCloseReader(LP_CommandMessageECHK.Query()
						.WHERE(LP_CommandMessageECHK.Columns.ResponseToCommandMessageECHKId, CommandMessageECHKID).ExecuteReader());
				}
				return _ChildLP_CommandMessageECHKsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandMessage class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessageCollection : ActiveList<LP_CommandMessage, LP_CommandMessageCollection>
	{
		public static LP_CommandMessageCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandMessageCollection result = new LP_CommandMessageCollection();
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
			foreach (LP_CommandMessage item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandMessages table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandMessage : ActiveRecord<LP_CommandMessage>, INotifyPropertyChanged
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

		public LP_CommandMessage()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandMessages", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = true;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarCommandTypeId = new TableSchema.TableColumn(schema);
				colvarCommandTypeId.ColumnName = "CommandTypeId";
				colvarCommandTypeId.DataType = DbType.AnsiString;
				colvarCommandTypeId.MaxLength = 20;
				colvarCommandTypeId.AutoIncrement = false;
				colvarCommandTypeId.IsNullable = false;
				colvarCommandTypeId.IsPrimaryKey = false;
				colvarCommandTypeId.IsForeignKey = true;
				colvarCommandTypeId.IsReadOnly = false;
				colvarCommandTypeId.DefaultSetting = @"('UND')";
				colvarCommandTypeId.ForeignKeyTableName = "LP_CommandTypes";
				schema.Columns.Add(colvarCommandTypeId);

				TableSchema.TableColumn colvarCommandNameId = new TableSchema.TableColumn(schema);
				colvarCommandNameId.ColumnName = "CommandNameId";
				colvarCommandNameId.DataType = DbType.AnsiString;
				colvarCommandNameId.MaxLength = 20;
				colvarCommandNameId.AutoIncrement = false;
				colvarCommandNameId.IsNullable = true;
				colvarCommandNameId.IsPrimaryKey = false;
				colvarCommandNameId.IsForeignKey = true;
				colvarCommandNameId.IsReadOnly = false;
				colvarCommandNameId.DefaultSetting = @"";
				colvarCommandNameId.ForeignKeyTableName = "LP_CommandNames";
				schema.Columns.Add(colvarCommandNameId);

				TableSchema.TableColumn colvarRequestId = new TableSchema.TableColumn(schema);
				colvarRequestId.ColumnName = "RequestId";
				colvarRequestId.DataType = DbType.Int64;
				colvarRequestId.MaxLength = 0;
				colvarRequestId.AutoIncrement = false;
				colvarRequestId.IsNullable = true;
				colvarRequestId.IsPrimaryKey = false;
				colvarRequestId.IsForeignKey = true;
				colvarRequestId.IsReadOnly = false;
				colvarRequestId.DefaultSetting = @"";
				colvarRequestId.ForeignKeyTableName = "LP_Requests";
				schema.Columns.Add(colvarRequestId);

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

				TableSchema.TableColumn colvarMessageDate = new TableSchema.TableColumn(schema);
				colvarMessageDate.ColumnName = "MessageDate";
				colvarMessageDate.DataType = DbType.DateTime;
				colvarMessageDate.MaxLength = 0;
				colvarMessageDate.AutoIncrement = false;
				colvarMessageDate.IsNullable = true;
				colvarMessageDate.IsPrimaryKey = false;
				colvarMessageDate.IsForeignKey = false;
				colvarMessageDate.IsReadOnly = false;
				colvarMessageDate.DefaultSetting = @"";
				colvarMessageDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageDate);

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandMessages",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandMessage LoadFrom(LP_CommandMessage item)
		{
			LP_CommandMessage result = new LP_CommandMessage();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public string CommandTypeId {
			get { return GetColumnValue<string>(Columns.CommandTypeId); }
			set {
				SetColumnValue(Columns.CommandTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandTypeId));
			}
		}
		[DataMember]
		public string CommandNameId {
			get { return GetColumnValue<string>(Columns.CommandNameId); }
			set {
				SetColumnValue(Columns.CommandNameId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandNameId));
			}
		}
		[DataMember]
		public long? RequestId {
			get { return GetColumnValue<long?>(Columns.RequestId); }
			set {
				SetColumnValue(Columns.RequestId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestId));
			}
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public long? UnitID {
			get { return GetColumnValue<long?>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public DateTime? MessageDate {
			get { return GetColumnValue<DateTime?>(Columns.MessageDate); }
			set {
				SetColumnValue(Columns.MessageDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageDate));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private LP_CommandName _CommandName;
		//Relationship: FK_LP_CommandMessages_LP_CommandNames
		public LP_CommandName CommandName
		{
			get
			{
				if(_CommandName == null) {
					_CommandName = LP_CommandName.FetchByID(this.CommandNameId);
				}
				return _CommandName;
			}
			set
			{
				SetColumnValue("CommandNameId", value.CommandNameID);
				_CommandName = value;
			}
		}

		private LP_CommandType _CommandType;
		//Relationship: FK_LP_CommandMessages_LP_CommandTypes
		public LP_CommandType CommandType
		{
			get
			{
				if(_CommandType == null) {
					_CommandType = LP_CommandType.FetchByID(this.CommandTypeId);
				}
				return _CommandType;
			}
			set
			{
				SetColumnValue("CommandTypeId", value.CommandTypeID);
				_CommandType = value;
			}
		}

		private LP_Request _Request;
		//Relationship: FK_LP_CommandMessages_LP_Requests
		public LP_Request Request
		{
			get
			{
				if(_Request == null) {
					_Request = LP_Request.FetchByID(this.RequestId);
				}
				return _Request;
			}
			set
			{
				SetColumnValue("RequestId", value.RequestID);
				_Request = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandNameIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RequestIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn MessageDateColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string CommandTypeId = @"CommandTypeId";
			public static readonly string CommandNameId = @"CommandNameId";
			public static readonly string RequestId = @"RequestId";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string UnitID = @"UnitID";
			public static readonly string MessageDate = @"MessageDate";
			public static readonly string Sentence = @"Sentence";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageAVCFGFFCollection _LP_CommandMessageAVCFGFFsCol;
		//Relationship: FK_LP_CommandMessageAVCFGFF_LP_CommandMessages
		public LP_CommandMessageAVCFGFFCollection LP_CommandMessageAVCFGFFsCol
		{
			get
			{
				if(_LP_CommandMessageAVCFGFFsCol == null) {
					_LP_CommandMessageAVCFGFFsCol = new LP_CommandMessageAVCFGFFCollection();
					_LP_CommandMessageAVCFGFFsCol.LoadAndCloseReader(LP_CommandMessageAVCFGFF.Query()
						.WHERE(LP_CommandMessageAVCFGFF.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageAVCFGFFsCol;
			}
		}

		private LP_CommandMessageAVRMCCollection _LP_CommandMessageAVRMCsCol;
		//Relationship: FK_LP_CommandMessageAVRMC_LP_CommandMessages
		public LP_CommandMessageAVRMCCollection LP_CommandMessageAVRMCsCol
		{
			get
			{
				if(_LP_CommandMessageAVRMCsCol == null) {
					_LP_CommandMessageAVRMCsCol = new LP_CommandMessageAVRMCCollection();
					_LP_CommandMessageAVRMCsCol.LoadAndCloseReader(LP_CommandMessageAVRMC.Query()
						.WHERE(LP_CommandMessageAVRMC.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageAVRMCsCol;
			}
		}

		private LP_CommandMessageAVRMCCollection _LP_CommandMessageAVRMCs02Col;
		//Relationship: FK_LP_CommandMessageAVRMCs_LP_CommandMessages
		public LP_CommandMessageAVRMCCollection LP_CommandMessageAVRMCs02Col
		{
			get
			{
				if(_LP_CommandMessageAVRMCs02Col == null) {
					_LP_CommandMessageAVRMCs02Col = new LP_CommandMessageAVRMCCollection();
					_LP_CommandMessageAVRMCs02Col.LoadAndCloseReader(LP_CommandMessageAVRMC.Query()
						.WHERE(LP_CommandMessageAVRMC.Columns.ReqCommandMessageId, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageAVRMCs02Col;
			}
		}

		private LP_CommandMessageEAVACKCollection _LP_CommandMessageEAVACKsCol;
		//Relationship: FK_LP_CommandMessageEAVACKs_LP_CommandMessages
		public LP_CommandMessageEAVACKCollection LP_CommandMessageEAVACKsCol
		{
			get
			{
				if(_LP_CommandMessageEAVACKsCol == null) {
					_LP_CommandMessageEAVACKsCol = new LP_CommandMessageEAVACKCollection();
					_LP_CommandMessageEAVACKsCol.LoadAndCloseReader(LP_CommandMessageEAVACK.Query()
						.WHERE(LP_CommandMessageEAVACK.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageEAVACKsCol;
			}
		}

		private LP_CommandMessageEAVGOF3Collection _LP_CommandMessageEAVGOF3sCol;
		//Relationship: FK_LP_CommandMessageEAVGOF3s_LP_CommandMessages
		public LP_CommandMessageEAVGOF3Collection LP_CommandMessageEAVGOF3sCol
		{
			get
			{
				if(_LP_CommandMessageEAVGOF3sCol == null) {
					_LP_CommandMessageEAVGOF3sCol = new LP_CommandMessageEAVGOF3Collection();
					_LP_CommandMessageEAVGOF3sCol.LoadAndCloseReader(LP_CommandMessageEAVGOF3.Query()
						.WHERE(LP_CommandMessageEAVGOF3.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageEAVGOF3sCol;
			}
		}

		private LP_CommandMessageEAVGOF6Collection _LP_CommandMessageEAVGOF6sCol;
		//Relationship: FK_LP_CommandMessageEAVGOF6s_LP_CommandMessages
		public LP_CommandMessageEAVGOF6Collection LP_CommandMessageEAVGOF6sCol
		{
			get
			{
				if(_LP_CommandMessageEAVGOF6sCol == null) {
					_LP_CommandMessageEAVGOF6sCol = new LP_CommandMessageEAVGOF6Collection();
					_LP_CommandMessageEAVGOF6sCol.LoadAndCloseReader(LP_CommandMessageEAVGOF6.Query()
						.WHERE(LP_CommandMessageEAVGOF6.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageEAVGOF6sCol;
			}
		}

		private LP_CommandMessageEAVRSP3Collection _LP_CommandMessageEAVRSP3sCol;
		//Relationship: FK_LP_CommandMessageEAVRSP3s_LP_CommandMessages
		public LP_CommandMessageEAVRSP3Collection LP_CommandMessageEAVRSP3sCol
		{
			get
			{
				if(_LP_CommandMessageEAVRSP3sCol == null) {
					_LP_CommandMessageEAVRSP3sCol = new LP_CommandMessageEAVRSP3Collection();
					_LP_CommandMessageEAVRSP3sCol.LoadAndCloseReader(LP_CommandMessageEAVRSP3.Query()
						.WHERE(LP_CommandMessageEAVRSP3.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageEAVRSP3sCol;
			}
		}

		private LP_CommandMessageEAVRSP4Collection _LP_CommandMessageEAVRSP4sCol;
		//Relationship: FK_LP_CommandMessageEAVRSP4_LP_CommandMessages
		public LP_CommandMessageEAVRSP4Collection LP_CommandMessageEAVRSP4sCol
		{
			get
			{
				if(_LP_CommandMessageEAVRSP4sCol == null) {
					_LP_CommandMessageEAVRSP4sCol = new LP_CommandMessageEAVRSP4Collection();
					_LP_CommandMessageEAVRSP4sCol.LoadAndCloseReader(LP_CommandMessageEAVRSP4.Query()
						.WHERE(LP_CommandMessageEAVRSP4.Columns.CommandMessageID, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageEAVRSP4sCol;
			}
		}

		private LP_CommandMessageECHKCollection _LP_CommandMessageECHKsCol;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandMessages
		public LP_CommandMessageECHKCollection LP_CommandMessageECHKsCol
		{
			get
			{
				if(_LP_CommandMessageECHKsCol == null) {
					_LP_CommandMessageECHKsCol = new LP_CommandMessageECHKCollection();
					_LP_CommandMessageECHKsCol.LoadAndCloseReader(LP_CommandMessageECHK.Query()
						.WHERE(LP_CommandMessageECHK.Columns.CommandMessageId, CommandMessageID).ExecuteReader());
				}
				return _LP_CommandMessageECHKsCol;
			}
		}

		private LP_RequestCollection _LP_RequestsCol;
		//Relationship: FK_LP_Requests_LP_CommandMessages
		public LP_RequestCollection LP_RequestsCol
		{
			get
			{
				if(_LP_RequestsCol == null) {
					_LP_RequestsCol = new LP_RequestCollection();
					_LP_RequestsCol.LoadAndCloseReader(LP_Request.Query()
						.WHERE(LP_Request.Columns.CommandMessageId, CommandMessageID).ExecuteReader());
				}
				return _LP_RequestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandName class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandNameCollection : ActiveList<LP_CommandName, LP_CommandNameCollection>
	{
		public static LP_CommandNameCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandNameCollection result = new LP_CommandNameCollection();
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
			foreach (LP_CommandName item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandNames table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandName : ActiveRecord<LP_CommandName>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string AVALLID = "AVALL";
			[EnumMember()] public const string AVCFGID = "AVCFG";
			[EnumMember()] public const string AVCFGFeatureFlagID = "AVCFG_FF";
			[EnumMember()] public const string AVCOMID = "AVCOM";
			[EnumMember()] public const string AVDELID = "AVDEL";
			[EnumMember()] public const string AVLOGID = "AVLOG";
			[EnumMember()] public const string AVMLGID = "AVMLG";
			[EnumMember()] public const string AVPARID = "AVPAR";
			[EnumMember()] public const string AVPHNID = "AVPHN";
			[EnumMember()] public const string AVREGID = "AVREG";
			[EnumMember()] public const string AVREQID = "AVREQ";
			[EnumMember()] public const string AVRESETID = "AVRESET";
			[EnumMember()] public const string AVRMCID = "AVRMC";
			[EnumMember()] public const string AVSETID = "AVSET";
			[EnumMember()] public const string AVSTSID = "AVSTS";
			[EnumMember()] public const string AVSYSID = "AVSYS";
			[EnumMember()] public const string EAVACKID = "EAVACK";
			[EnumMember()] public const string EAVGOF2ID = "EAVGOF2";
			[EnumMember()] public const string EAVGOF3ID = "EAVGOF3";
			[EnumMember()] public const string EAVGOF4ID = "EAVGOF4";
			[EnumMember()] public const string EAVGOF5ID = "EAVGOF5";
			[EnumMember()] public const string EAVGOF6ID = "EAVGOF6";
			[EnumMember()] public const string EAVRSPID = "EAVRSP";
			[EnumMember()] public const string EAVRSP235ID = "EAVRSP235";
			[EnumMember()] public const string EAVRSP4ID = "EAVRSP4";
			[EnumMember()] public const string EAVRSP6ID = "EAVRSP6";
			[EnumMember()] public const string ECHKID = "ECHK";
			[EnumMember()] public const string GPRMCID = "GPRMC";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LP_CommandName()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandNames", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandNameID = new TableSchema.TableColumn(schema);
				colvarCommandNameID.ColumnName = "CommandNameID";
				colvarCommandNameID.DataType = DbType.AnsiString;
				colvarCommandNameID.MaxLength = 20;
				colvarCommandNameID.AutoIncrement = false;
				colvarCommandNameID.IsNullable = false;
				colvarCommandNameID.IsPrimaryKey = true;
				colvarCommandNameID.IsForeignKey = false;
				colvarCommandNameID.IsReadOnly = false;
				colvarCommandNameID.DefaultSetting = @"";
				colvarCommandNameID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandNameID);

				TableSchema.TableColumn colvarCommandName = new TableSchema.TableColumn(schema);
				colvarCommandName.ColumnName = "CommandName";
				colvarCommandName.DataType = DbType.AnsiString;
				colvarCommandName.MaxLength = 50;
				colvarCommandName.AutoIncrement = false;
				colvarCommandName.IsNullable = false;
				colvarCommandName.IsPrimaryKey = false;
				colvarCommandName.IsForeignKey = false;
				colvarCommandName.IsReadOnly = false;
				colvarCommandName.DefaultSetting = @"";
				colvarCommandName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandName);

				TableSchema.TableColumn colvarCommandDescription = new TableSchema.TableColumn(schema);
				colvarCommandDescription.ColumnName = "CommandDescription";
				colvarCommandDescription.DataType = DbType.AnsiString;
				colvarCommandDescription.MaxLength = -1;
				colvarCommandDescription.AutoIncrement = false;
				colvarCommandDescription.IsNullable = true;
				colvarCommandDescription.IsPrimaryKey = false;
				colvarCommandDescription.IsForeignKey = false;
				colvarCommandDescription.IsReadOnly = false;
				colvarCommandDescription.DefaultSetting = @"";
				colvarCommandDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandDescription);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandNames",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandName LoadFrom(LP_CommandName item)
		{
			LP_CommandName result = new LP_CommandName();
			if (item.CommandNameID != default(string)) {
				result.LoadByKey(item.CommandNameID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string CommandNameID {
			get { return GetColumnValue<string>(Columns.CommandNameID); }
			set {
				SetColumnValue(Columns.CommandNameID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandNameID));
			}
		}
		[DataMember]
		public string CommandName {
			get { return GetColumnValue<string>(Columns.CommandName); }
			set {
				SetColumnValue(Columns.CommandName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandName));
			}
		}
		[DataMember]
		public string CommandDescription {
			get { return GetColumnValue<string>(Columns.CommandDescription); }
			set {
				SetColumnValue(Columns.CommandDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return CommandName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandNameIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandNameID = @"CommandNameID";
			public static readonly string CommandName = @"CommandName";
			public static readonly string CommandDescription = @"CommandDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandNameID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageCollection _LP_CommandMessagesCol;
		//Relationship: FK_LP_CommandMessages_LP_CommandNames
		public LP_CommandMessageCollection LP_CommandMessagesCol
		{
			get
			{
				if(_LP_CommandMessagesCol == null) {
					_LP_CommandMessagesCol = new LP_CommandMessageCollection();
					_LP_CommandMessagesCol.LoadAndCloseReader(LP_CommandMessage.Query()
						.WHERE(LP_CommandMessage.Columns.CommandNameId, CommandNameID).ExecuteReader());
				}
				return _LP_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_CommandType class.
	/// </summary>
	[DataContract]
	public partial class LP_CommandTypeCollection : ActiveList<LP_CommandType, LP_CommandTypeCollection>
	{
		public static LP_CommandTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_CommandTypeCollection result = new LP_CommandTypeCollection();
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
			foreach (LP_CommandType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_CommandTypes table.
	/// </summary>
	[DataContract]
	public partial class LP_CommandType : ActiveRecord<LP_CommandType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string ConfigurationCommandsID = "CFG";
			[EnumMember()] public const string ControlCommandsID = "CNTRL";
			[EnumMember()] public const string ExtendedCommandsID = "EXT";
			[EnumMember()] public const string OthersID = "OTHERS";
			[EnumMember()] public const string RequestCommandsID = "REQ";
			[EnumMember()] public const string UndefinedID = "UND";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LP_CommandType()
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
				TableSchema.Table schema = new TableSchema.Table("LP_CommandTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandTypeID = new TableSchema.TableColumn(schema);
				colvarCommandTypeID.ColumnName = "CommandTypeID";
				colvarCommandTypeID.DataType = DbType.AnsiString;
				colvarCommandTypeID.MaxLength = 20;
				colvarCommandTypeID.AutoIncrement = false;
				colvarCommandTypeID.IsNullable = false;
				colvarCommandTypeID.IsPrimaryKey = true;
				colvarCommandTypeID.IsForeignKey = false;
				colvarCommandTypeID.IsReadOnly = false;
				colvarCommandTypeID.DefaultSetting = @"";
				colvarCommandTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandTypeID);

				TableSchema.TableColumn colvarCommandType = new TableSchema.TableColumn(schema);
				colvarCommandType.ColumnName = "CommandType";
				colvarCommandType.DataType = DbType.AnsiString;
				colvarCommandType.MaxLength = 50;
				colvarCommandType.AutoIncrement = false;
				colvarCommandType.IsNullable = false;
				colvarCommandType.IsPrimaryKey = false;
				colvarCommandType.IsForeignKey = false;
				colvarCommandType.IsReadOnly = false;
				colvarCommandType.DefaultSetting = @"";
				colvarCommandType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandType);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_CommandTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_CommandType LoadFrom(LP_CommandType item)
		{
			LP_CommandType result = new LP_CommandType();
			if (item.CommandTypeID != default(string)) {
				result.LoadByKey(item.CommandTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string CommandTypeID {
			get { return GetColumnValue<string>(Columns.CommandTypeID); }
			set {
				SetColumnValue(Columns.CommandTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandTypeID));
			}
		}
		[DataMember]
		public string CommandType {
			get { return GetColumnValue<string>(Columns.CommandType); }
			set {
				SetColumnValue(Columns.CommandType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandType));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return CommandType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandTypeID = @"CommandTypeID";
			public static readonly string CommandType = @"CommandType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandTypeID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageECHKCollection _LP_CommandMessageECHKsCol;
		//Relationship: FK_LP_CommandMessageECHKs_LP_CommandTypes
		public LP_CommandMessageECHKCollection LP_CommandMessageECHKsCol
		{
			get
			{
				if(_LP_CommandMessageECHKsCol == null) {
					_LP_CommandMessageECHKsCol = new LP_CommandMessageECHKCollection();
					_LP_CommandMessageECHKsCol.LoadAndCloseReader(LP_CommandMessageECHK.Query()
						.WHERE(LP_CommandMessageECHK.Columns.CommandTypeId, CommandTypeID).ExecuteReader());
				}
				return _LP_CommandMessageECHKsCol;
			}
		}

		private LP_CommandMessageCollection _LP_CommandMessagesCol;
		//Relationship: FK_LP_CommandMessages_LP_CommandTypes
		public LP_CommandMessageCollection LP_CommandMessagesCol
		{
			get
			{
				if(_LP_CommandMessagesCol == null) {
					_LP_CommandMessagesCol = new LP_CommandMessageCollection();
					_LP_CommandMessagesCol.LoadAndCloseReader(LP_CommandMessage.Query()
						.WHERE(LP_CommandMessage.Columns.CommandTypeId, CommandTypeID).ExecuteReader());
				}
				return _LP_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_Device class.
	/// </summary>
	[DataContract]
	public partial class LP_DeviceCollection : ActiveList<LP_Device, LP_DeviceCollection>
	{
		public static LP_DeviceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_DeviceCollection result = new LP_DeviceCollection();
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
			foreach (LP_Device item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_Devices table.
	/// </summary>
	[DataContract]
	public partial class LP_Device : ActiveRecord<LP_Device>, INotifyPropertyChanged
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

		public LP_Device()
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
				TableSchema.Table schema = new TableSchema.Table("LP_Devices", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUnitID = new TableSchema.TableColumn(schema);
				colvarUnitID.ColumnName = "UnitID";
				colvarUnitID.DataType = DbType.Int64;
				colvarUnitID.MaxLength = 0;
				colvarUnitID.AutoIncrement = false;
				colvarUnitID.IsNullable = false;
				colvarUnitID.IsPrimaryKey = true;
				colvarUnitID.IsForeignKey = false;
				colvarUnitID.IsReadOnly = false;
				colvarUnitID.DefaultSetting = @"";
				colvarUnitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitID);

				TableSchema.TableColumn colvarAccountID = new TableSchema.TableColumn(schema);
				colvarAccountID.ColumnName = "AccountID";
				colvarAccountID.DataType = DbType.Int64;
				colvarAccountID.MaxLength = 0;
				colvarAccountID.AutoIncrement = false;
				colvarAccountID.IsNullable = false;
				colvarAccountID.IsPrimaryKey = false;
				colvarAccountID.IsForeignKey = false;
				colvarAccountID.IsReadOnly = false;
				colvarAccountID.DefaultSetting = @"";
				colvarAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountID);

				TableSchema.TableColumn colvarSimProductBarcodeId = new TableSchema.TableColumn(schema);
				colvarSimProductBarcodeId.ColumnName = "SimProductBarcodeId";
				colvarSimProductBarcodeId.DataType = DbType.String;
				colvarSimProductBarcodeId.MaxLength = 50;
				colvarSimProductBarcodeId.AutoIncrement = false;
				colvarSimProductBarcodeId.IsNullable = false;
				colvarSimProductBarcodeId.IsPrimaryKey = false;
				colvarSimProductBarcodeId.IsForeignKey = false;
				colvarSimProductBarcodeId.IsReadOnly = false;
				colvarSimProductBarcodeId.DefaultSetting = @"";
				colvarSimProductBarcodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSimProductBarcodeId);

				TableSchema.TableColumn colvarProductBarcodeId = new TableSchema.TableColumn(schema);
				colvarProductBarcodeId.ColumnName = "ProductBarcodeId";
				colvarProductBarcodeId.DataType = DbType.AnsiString;
				colvarProductBarcodeId.MaxLength = 50;
				colvarProductBarcodeId.AutoIncrement = false;
				colvarProductBarcodeId.IsNullable = true;
				colvarProductBarcodeId.IsPrimaryKey = false;
				colvarProductBarcodeId.IsForeignKey = false;
				colvarProductBarcodeId.IsReadOnly = false;
				colvarProductBarcodeId.DefaultSetting = @"";
				colvarProductBarcodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProductBarcodeId);

				TableSchema.TableColumn colvarPhoneNumber = new TableSchema.TableColumn(schema);
				colvarPhoneNumber.ColumnName = "PhoneNumber";
				colvarPhoneNumber.DataType = DbType.AnsiString;
				colvarPhoneNumber.MaxLength = 20;
				colvarPhoneNumber.AutoIncrement = false;
				colvarPhoneNumber.IsNullable = false;
				colvarPhoneNumber.IsPrimaryKey = false;
				colvarPhoneNumber.IsForeignKey = false;
				colvarPhoneNumber.IsReadOnly = false;
				colvarPhoneNumber.DefaultSetting = @"";
				colvarPhoneNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneNumber);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 8;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

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

				TableSchema.TableColumn colvarLastAccessDate = new TableSchema.TableColumn(schema);
				colvarLastAccessDate.ColumnName = "LastAccessDate";
				colvarLastAccessDate.DataType = DbType.DateTime;
				colvarLastAccessDate.MaxLength = 0;
				colvarLastAccessDate.AutoIncrement = false;
				colvarLastAccessDate.IsNullable = true;
				colvarLastAccessDate.IsPrimaryKey = false;
				colvarLastAccessDate.IsForeignKey = false;
				colvarLastAccessDate.IsReadOnly = false;
				colvarLastAccessDate.DefaultSetting = @"";
				colvarLastAccessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAccessDate);

				TableSchema.TableColumn colvarFirmwareVersion = new TableSchema.TableColumn(schema);
				colvarFirmwareVersion.ColumnName = "FirmwareVersion";
				colvarFirmwareVersion.DataType = DbType.AnsiString;
				colvarFirmwareVersion.MaxLength = 50;
				colvarFirmwareVersion.AutoIncrement = false;
				colvarFirmwareVersion.IsNullable = true;
				colvarFirmwareVersion.IsPrimaryKey = false;
				colvarFirmwareVersion.IsForeignKey = false;
				colvarFirmwareVersion.IsReadOnly = false;
				colvarFirmwareVersion.DefaultSetting = @"";
				colvarFirmwareVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirmwareVersion);

				TableSchema.TableColumn colvarSerialNumber = new TableSchema.TableColumn(schema);
				colvarSerialNumber.ColumnName = "SerialNumber";
				colvarSerialNumber.DataType = DbType.AnsiString;
				colvarSerialNumber.MaxLength = 50;
				colvarSerialNumber.AutoIncrement = false;
				colvarSerialNumber.IsNullable = true;
				colvarSerialNumber.IsPrimaryKey = false;
				colvarSerialNumber.IsForeignKey = false;
				colvarSerialNumber.IsReadOnly = false;
				colvarSerialNumber.DefaultSetting = @"";
				colvarSerialNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSerialNumber);

				TableSchema.TableColumn colvarMemorySize = new TableSchema.TableColumn(schema);
				colvarMemorySize.ColumnName = "MemorySize";
				colvarMemorySize.DataType = DbType.AnsiString;
				colvarMemorySize.MaxLength = 50;
				colvarMemorySize.AutoIncrement = false;
				colvarMemorySize.IsNullable = true;
				colvarMemorySize.IsPrimaryKey = false;
				colvarMemorySize.IsForeignKey = false;
				colvarMemorySize.IsReadOnly = false;
				colvarMemorySize.DefaultSetting = @"";
				colvarMemorySize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMemorySize);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_Devices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_Device LoadFrom(LP_Device item)
		{
			LP_Device result = new LP_Device();
			if (item.UnitID != default(long)) {
				result.LoadByKey(item.UnitID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public long AccountID {
			get { return GetColumnValue<long>(Columns.AccountID); }
			set {
				SetColumnValue(Columns.AccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountID));
			}
		}
		[DataMember]
		public string SimProductBarcodeId {
			get { return GetColumnValue<string>(Columns.SimProductBarcodeId); }
			set {
				SetColumnValue(Columns.SimProductBarcodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SimProductBarcodeId));
			}
		}
		[DataMember]
		public string ProductBarcodeId {
			get { return GetColumnValue<string>(Columns.ProductBarcodeId); }
			set {
				SetColumnValue(Columns.ProductBarcodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProductBarcodeId));
			}
		}
		[DataMember]
		public string PhoneNumber {
			get { return GetColumnValue<string>(Columns.PhoneNumber); }
			set {
				SetColumnValue(Columns.PhoneNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneNumber));
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
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public DateTime? LastAccessDate {
			get { return GetColumnValue<DateTime?>(Columns.LastAccessDate); }
			set {
				SetColumnValue(Columns.LastAccessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAccessDate));
			}
		}
		[DataMember]
		public string FirmwareVersion {
			get { return GetColumnValue<string>(Columns.FirmwareVersion); }
			set {
				SetColumnValue(Columns.FirmwareVersion, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FirmwareVersion));
			}
		}
		[DataMember]
		public string SerialNumber {
			get { return GetColumnValue<string>(Columns.SerialNumber); }
			set {
				SetColumnValue(Columns.SerialNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SerialNumber));
			}
		}
		[DataMember]
		public string MemorySize {
			get { return GetColumnValue<string>(Columns.MemorySize); }
			set {
				SetColumnValue(Columns.MemorySize, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MemorySize));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return UnitID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SimProductBarcodeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ProductBarcodeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhoneNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LastAccessDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn FirmwareVersionColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SerialNumberColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn MemorySizeColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string UnitID = @"UnitID";
			public static readonly string AccountID = @"AccountID";
			public static readonly string SimProductBarcodeId = @"SimProductBarcodeId";
			public static readonly string ProductBarcodeId = @"ProductBarcodeId";
			public static readonly string PhoneNumber = @"PhoneNumber";
			public static readonly string Password = @"Password";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string LastAccessDate = @"LastAccessDate";
			public static readonly string FirmwareVersion = @"FirmwareVersion";
			public static readonly string SerialNumber = @"SerialNumber";
			public static readonly string MemorySize = @"MemorySize";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return UnitID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_DeviceStatus class.
	/// </summary>
	[DataContract]
	public partial class LP_DeviceStatusCollection : ActiveList<LP_DeviceStatus, LP_DeviceStatusCollection>
	{
		public static LP_DeviceStatusCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_DeviceStatusCollection result = new LP_DeviceStatusCollection();
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
			foreach (LP_DeviceStatus item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_DeviceStatus table.
	/// </summary>
	[DataContract]
	public partial class LP_DeviceStatus : ActiveRecord<LP_DeviceStatus>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string UndefinedID = "_UD";
			[EnumMember()] public const string ActiveID = "A";
			[EnumMember()] public const string NotRealTimeDataID = "R";
			[EnumMember()] public const string VeryBadSignalInformationID = "V";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LP_DeviceStatus()
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
				TableSchema.Table schema = new TableSchema.Table("LP_DeviceStatus", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeviceStatusID = new TableSchema.TableColumn(schema);
				colvarDeviceStatusID.ColumnName = "DeviceStatusID";
				colvarDeviceStatusID.DataType = DbType.AnsiString;
				colvarDeviceStatusID.MaxLength = 3;
				colvarDeviceStatusID.AutoIncrement = false;
				colvarDeviceStatusID.IsNullable = false;
				colvarDeviceStatusID.IsPrimaryKey = true;
				colvarDeviceStatusID.IsForeignKey = false;
				colvarDeviceStatusID.IsReadOnly = false;
				colvarDeviceStatusID.DefaultSetting = @"";
				colvarDeviceStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceStatusID);

				TableSchema.TableColumn colvarDeviceStatus = new TableSchema.TableColumn(schema);
				colvarDeviceStatus.ColumnName = "DeviceStatus";
				colvarDeviceStatus.DataType = DbType.AnsiString;
				colvarDeviceStatus.MaxLength = 50;
				colvarDeviceStatus.AutoIncrement = false;
				colvarDeviceStatus.IsNullable = false;
				colvarDeviceStatus.IsPrimaryKey = false;
				colvarDeviceStatus.IsForeignKey = false;
				colvarDeviceStatus.IsReadOnly = false;
				colvarDeviceStatus.DefaultSetting = @"";
				colvarDeviceStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceStatus);

				TableSchema.TableColumn colvarStatusDescription = new TableSchema.TableColumn(schema);
				colvarStatusDescription.ColumnName = "StatusDescription";
				colvarStatusDescription.DataType = DbType.AnsiString;
				colvarStatusDescription.MaxLength = 500;
				colvarStatusDescription.AutoIncrement = false;
				colvarStatusDescription.IsNullable = false;
				colvarStatusDescription.IsPrimaryKey = false;
				colvarStatusDescription.IsForeignKey = false;
				colvarStatusDescription.IsReadOnly = false;
				colvarStatusDescription.DefaultSetting = @"";
				colvarStatusDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStatusDescription);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_DeviceStatus",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_DeviceStatus LoadFrom(LP_DeviceStatus item)
		{
			LP_DeviceStatus result = new LP_DeviceStatus();
			if (item.DeviceStatusID != default(string)) {
				result.LoadByKey(item.DeviceStatusID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string DeviceStatusID {
			get { return GetColumnValue<string>(Columns.DeviceStatusID); }
			set {
				SetColumnValue(Columns.DeviceStatusID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceStatusID));
			}
		}
		[DataMember]
		public string DeviceStatus {
			get { return GetColumnValue<string>(Columns.DeviceStatus); }
			set {
				SetColumnValue(Columns.DeviceStatus, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceStatus));
			}
		}
		[DataMember]
		public string StatusDescription {
			get { return GetColumnValue<string>(Columns.StatusDescription); }
			set {
				SetColumnValue(Columns.StatusDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StatusDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return DeviceStatus;
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeviceStatusIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DeviceStatusColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn StatusDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeviceStatusID = @"DeviceStatusID";
			public static readonly string DeviceStatus = @"DeviceStatus";
			public static readonly string StatusDescription = @"StatusDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DeviceStatusID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_EventCode class.
	/// </summary>
	[DataContract]
	public partial class LP_EventCodeCollection : ActiveList<LP_EventCode, LP_EventCodeCollection>
	{
		public static LP_EventCodeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_EventCodeCollection result = new LP_EventCodeCollection();
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
			foreach (LP_EventCode item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_EventCodes table.
	/// </summary>
	[DataContract]
	public partial class LP_EventCode : ActiveRecord<LP_EventCode>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string UndefinedID = "_UD";
			[EnumMember()] public const string RegularreportID = "0";
			[EnumMember()] public const string SOSbuttonpressedalertID = "1";
			[EnumMember()] public const string PanicSOSbuttonpressedalertID = "3";
			[EnumMember()] public const string Geo_fenceexitsalertID = "4";
			[EnumMember()] public const string OverspeedalertID = "6";
			[EnumMember()] public const string InstanceGeo_fenceexitalertID = "7";
			[EnumMember()] public const string G_Sensoralert1ID = "8";
			[EnumMember()] public const string ResponseToRequestCurrentPositionID = "A";
			[EnumMember()] public const string GSMconnectionchangedtoroamingID = "F";
			[EnumMember()] public const string GSMconnectionbacktohomenetworkID = "G";
			[EnumMember()] public const string UnitispoweredofforchargerispluggedinID = "H";
			[EnumMember()] public const string MileagealertID = "M";
			[EnumMember()] public const string TamperdetectionswitchisclosealertID = "S";
			[EnumMember()] public const string TamperdetectionswitchisopenalertID = "T";
			[EnumMember()] public const string Geo_fenceenteralertID = "X";
			[EnumMember()] public const string LowbatteryalertID = "Z";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public LP_EventCode()
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
				TableSchema.Table schema = new TableSchema.Table("LP_EventCodes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEventCodeID = new TableSchema.TableColumn(schema);
				colvarEventCodeID.ColumnName = "EventCodeID";
				colvarEventCodeID.DataType = DbType.AnsiString;
				colvarEventCodeID.MaxLength = 3;
				colvarEventCodeID.AutoIncrement = false;
				colvarEventCodeID.IsNullable = false;
				colvarEventCodeID.IsPrimaryKey = true;
				colvarEventCodeID.IsForeignKey = false;
				colvarEventCodeID.IsReadOnly = false;
				colvarEventCodeID.DefaultSetting = @"";
				colvarEventCodeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventCodeID);

				TableSchema.TableColumn colvarEventCode = new TableSchema.TableColumn(schema);
				colvarEventCode.ColumnName = "EventCode";
				colvarEventCode.DataType = DbType.AnsiString;
				colvarEventCode.MaxLength = 50;
				colvarEventCode.AutoIncrement = false;
				colvarEventCode.IsNullable = false;
				colvarEventCode.IsPrimaryKey = false;
				colvarEventCode.IsForeignKey = false;
				colvarEventCode.IsReadOnly = false;
				colvarEventCode.DefaultSetting = @"";
				colvarEventCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventCode);

				TableSchema.TableColumn colvarCodeDescription = new TableSchema.TableColumn(schema);
				colvarCodeDescription.ColumnName = "CodeDescription";
				colvarCodeDescription.DataType = DbType.AnsiString;
				colvarCodeDescription.MaxLength = 500;
				colvarCodeDescription.AutoIncrement = false;
				colvarCodeDescription.IsNullable = false;
				colvarCodeDescription.IsPrimaryKey = false;
				colvarCodeDescription.IsForeignKey = false;
				colvarCodeDescription.IsReadOnly = false;
				colvarCodeDescription.DefaultSetting = @"";
				colvarCodeDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCodeDescription);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_EventCodes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_EventCode LoadFrom(LP_EventCode item)
		{
			LP_EventCode result = new LP_EventCode();
			if (item.EventCodeID != default(string)) {
				result.LoadByKey(item.EventCodeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string EventCodeID {
			get { return GetColumnValue<string>(Columns.EventCodeID); }
			set {
				SetColumnValue(Columns.EventCodeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventCodeID));
			}
		}
		[DataMember]
		public string EventCode {
			get { return GetColumnValue<string>(Columns.EventCode); }
			set {
				SetColumnValue(Columns.EventCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EventCode));
			}
		}
		[DataMember]
		public string CodeDescription {
			get { return GetColumnValue<string>(Columns.CodeDescription); }
			set {
				SetColumnValue(Columns.CodeDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CodeDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return EventCode;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EventCodeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EventCodeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CodeDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string EventCodeID = @"EventCodeID";
			public static readonly string EventCode = @"EventCode";
			public static readonly string CodeDescription = @"CodeDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return EventCodeID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the LP_GsGeoFence class.
	/// </summary>
	[DataContract]
	public partial class LP_GsGeoFenceCollection : ActiveList<LP_GsGeoFence, LP_GsGeoFenceCollection>
	{
		public static LP_GsGeoFenceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_GsGeoFenceCollection result = new LP_GsGeoFenceCollection();
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
			foreach (LP_GsGeoFence item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_GsGeoFences table.
	/// </summary>
	[DataContract]
	public partial class LP_GsGeoFence : ActiveRecord<LP_GsGeoFence>, INotifyPropertyChanged
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

		public LP_GsGeoFence()
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
				TableSchema.Table schema = new TableSchema.Table("LP_GsGeoFences", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarLPGeoFenceID = new TableSchema.TableColumn(schema);
				colvarLPGeoFenceID.ColumnName = "LPGeoFenceID";
				colvarLPGeoFenceID.DataType = DbType.Int64;
				colvarLPGeoFenceID.MaxLength = 0;
				colvarLPGeoFenceID.AutoIncrement = true;
				colvarLPGeoFenceID.IsNullable = false;
				colvarLPGeoFenceID.IsPrimaryKey = true;
				colvarLPGeoFenceID.IsForeignKey = false;
				colvarLPGeoFenceID.IsReadOnly = false;
				colvarLPGeoFenceID.DefaultSetting = @"";
				colvarLPGeoFenceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLPGeoFenceID);

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

				TableSchema.TableColumn colvarGsGeoFenceId = new TableSchema.TableColumn(schema);
				colvarGsGeoFenceId.ColumnName = "GsGeoFenceId";
				colvarGsGeoFenceId.DataType = DbType.Int64;
				colvarGsGeoFenceId.MaxLength = 0;
				colvarGsGeoFenceId.AutoIncrement = false;
				colvarGsGeoFenceId.IsNullable = false;
				colvarGsGeoFenceId.IsPrimaryKey = false;
				colvarGsGeoFenceId.IsForeignKey = true;
				colvarGsGeoFenceId.IsReadOnly = false;
				colvarGsGeoFenceId.DefaultSetting = @"";
				colvarGsGeoFenceId.ForeignKeyTableName = "GS_AccountGeoFences";
				schema.Columns.Add(colvarGsGeoFenceId);

				TableSchema.TableColumn colvarGeoFenceI = new TableSchema.TableColumn(schema);
				colvarGeoFenceI.ColumnName = "GeoFenceI";
				colvarGeoFenceI.DataType = DbType.Byte;
				colvarGeoFenceI.MaxLength = 0;
				colvarGeoFenceI.AutoIncrement = false;
				colvarGeoFenceI.IsNullable = false;
				colvarGeoFenceI.IsPrimaryKey = false;
				colvarGeoFenceI.IsForeignKey = false;
				colvarGeoFenceI.IsReadOnly = false;
				colvarGeoFenceI.DefaultSetting = @"((0))";
				colvarGeoFenceI.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGeoFenceI);

				TableSchema.TableColumn colvarReportModeI = new TableSchema.TableColumn(schema);
				colvarReportModeI.ColumnName = "ReportModeI";
				colvarReportModeI.DataType = DbType.Byte;
				colvarReportModeI.MaxLength = 0;
				colvarReportModeI.AutoIncrement = false;
				colvarReportModeI.IsNullable = false;
				colvarReportModeI.IsPrimaryKey = false;
				colvarReportModeI.IsForeignKey = false;
				colvarReportModeI.IsReadOnly = false;
				colvarReportModeI.DefaultSetting = @"((3))";
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
				colvarLattitudeI1.DefaultSetting = @"((0))";
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
				colvarLongitudeI1.DefaultSetting = @"((0))";
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
				colvarLattitudeI2.DefaultSetting = @"((0))";
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
				colvarLongitudeI2.DefaultSetting = @"((0))";
				colvarLongitudeI2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitudeI2);

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

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.AnsiString;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('SYSTEM')";
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

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.AnsiString;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('SYSTEM')";
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

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_GsGeoFences",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_GsGeoFence LoadFrom(LP_GsGeoFence item)
		{
			LP_GsGeoFence result = new LP_GsGeoFence();
			if (item.LPGeoFenceID != default(long)) {
				result.LoadByKey(item.LPGeoFenceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long LPGeoFenceID {
			get { return GetColumnValue<long>(Columns.LPGeoFenceID); }
			set {
				SetColumnValue(Columns.LPGeoFenceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LPGeoFenceID));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public long GsGeoFenceId {
			get { return GetColumnValue<long>(Columns.GsGeoFenceId); }
			set {
				SetColumnValue(Columns.GsGeoFenceId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GsGeoFenceId));
			}
		}
		[DataMember]
		public byte GeoFenceI {
			get { return GetColumnValue<byte>(Columns.GeoFenceI); }
			set {
				SetColumnValue(Columns.GeoFenceI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GeoFenceI));
			}
		}
		[DataMember]
		public byte ReportModeI {
			get { return GetColumnValue<byte>(Columns.ReportModeI); }
			set {
				SetColumnValue(Columns.ReportModeI, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReportModeI));
			}
		}
		[DataMember]
		public double LattitudeI1 {
			get { return GetColumnValue<double>(Columns.LattitudeI1); }
			set {
				SetColumnValue(Columns.LattitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI1));
			}
		}
		[DataMember]
		public double LongitudeI1 {
			get { return GetColumnValue<double>(Columns.LongitudeI1); }
			set {
				SetColumnValue(Columns.LongitudeI1, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI1));
			}
		}
		[DataMember]
		public double LattitudeI2 {
			get { return GetColumnValue<double>(Columns.LattitudeI2); }
			set {
				SetColumnValue(Columns.LattitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LattitudeI2));
			}
		}
		[DataMember]
		public double LongitudeI2 {
			get { return GetColumnValue<double>(Columns.LongitudeI2); }
			set {
				SetColumnValue(Columns.LongitudeI2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LongitudeI2));
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
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private GS_AccountGeoFence _GsGeoFence;
		//Relationship: FK_LP_GsGeoFence_GS_AccountGeoFences
		public GS_AccountGeoFence GsGeoFence
		{
			get
			{
				if(_GsGeoFence == null) {
					_GsGeoFence = GS_AccountGeoFence.FetchByID(this.GsGeoFenceId);
				}
				return _GsGeoFence;
			}
			set
			{
				SetColumnValue("GsGeoFenceId", value.GeoFenceID);
				_GsGeoFence = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return LPGeoFenceID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn LPGeoFenceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GsGeoFenceIdColumn
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
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[15]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string LPGeoFenceID = @"LPGeoFenceID";
			public static readonly string UnitID = @"UnitID";
			public static readonly string GsGeoFenceId = @"GsGeoFenceId";
			public static readonly string GeoFenceI = @"GeoFenceI";
			public static readonly string ReportModeI = @"ReportModeI";
			public static readonly string LattitudeI1 = @"LattitudeI1";
			public static readonly string LongitudeI1 = @"LongitudeI1";
			public static readonly string LattitudeI2 = @"LattitudeI2";
			public static readonly string LongitudeI2 = @"LongitudeI2";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return LPGeoFenceID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageEAVRSP4Collection _LP_CommandMessageEAVRSP4sCol;
		//Relationship: FK_LP_CommandMessageEAVRSP4s_LP_GsGeoFences
		public LP_CommandMessageEAVRSP4Collection LP_CommandMessageEAVRSP4sCol
		{
			get
			{
				if(_LP_CommandMessageEAVRSP4sCol == null) {
					_LP_CommandMessageEAVRSP4sCol = new LP_CommandMessageEAVRSP4Collection();
					_LP_CommandMessageEAVRSP4sCol.LoadAndCloseReader(LP_CommandMessageEAVRSP4.Query()
						.WHERE(LP_CommandMessageEAVRSP4.Columns.LPGeoFenceId, LPGeoFenceID).ExecuteReader());
				}
				return _LP_CommandMessageEAVRSP4sCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_RequestName class.
	/// </summary>
	[DataContract]
	public partial class LP_RequestNameCollection : ActiveList<LP_RequestName, LP_RequestNameCollection>
	{
		public static LP_RequestNameCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_RequestNameCollection result = new LP_RequestNameCollection();
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
			foreach (LP_RequestName item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_RequestNames table.
	/// </summary>
	[DataContract]
	public partial class LP_RequestName : ActiveRecord<LP_RequestName>, INotifyPropertyChanged
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

		public LP_RequestName()
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
				TableSchema.Table schema = new TableSchema.Table("LP_RequestNames", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRequestNameID = new TableSchema.TableColumn(schema);
				colvarRequestNameID.ColumnName = "RequestNameID";
				colvarRequestNameID.DataType = DbType.AnsiString;
				colvarRequestNameID.MaxLength = 20;
				colvarRequestNameID.AutoIncrement = false;
				colvarRequestNameID.IsNullable = false;
				colvarRequestNameID.IsPrimaryKey = true;
				colvarRequestNameID.IsForeignKey = false;
				colvarRequestNameID.IsReadOnly = false;
				colvarRequestNameID.DefaultSetting = @"";
				colvarRequestNameID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestNameID);

				TableSchema.TableColumn colvarRequestName = new TableSchema.TableColumn(schema);
				colvarRequestName.ColumnName = "RequestName";
				colvarRequestName.DataType = DbType.AnsiString;
				colvarRequestName.MaxLength = 50;
				colvarRequestName.AutoIncrement = false;
				colvarRequestName.IsNullable = false;
				colvarRequestName.IsPrimaryKey = false;
				colvarRequestName.IsForeignKey = false;
				colvarRequestName.IsReadOnly = false;
				colvarRequestName.DefaultSetting = @"";
				colvarRequestName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestName);

				TableSchema.TableColumn colvarRequestNameDescription = new TableSchema.TableColumn(schema);
				colvarRequestNameDescription.ColumnName = "RequestNameDescription";
				colvarRequestNameDescription.DataType = DbType.AnsiString;
				colvarRequestNameDescription.MaxLength = -1;
				colvarRequestNameDescription.AutoIncrement = false;
				colvarRequestNameDescription.IsNullable = false;
				colvarRequestNameDescription.IsPrimaryKey = false;
				colvarRequestNameDescription.IsForeignKey = false;
				colvarRequestNameDescription.IsReadOnly = false;
				colvarRequestNameDescription.DefaultSetting = @"";
				colvarRequestNameDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestNameDescription);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_RequestNames",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_RequestName LoadFrom(LP_RequestName item)
		{
			LP_RequestName result = new LP_RequestName();
			if (item.RequestNameID != default(string)) {
				result.LoadByKey(item.RequestNameID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string RequestNameID {
			get { return GetColumnValue<string>(Columns.RequestNameID); }
			set {
				SetColumnValue(Columns.RequestNameID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestNameID));
			}
		}
		[DataMember]
		public string RequestName {
			get { return GetColumnValue<string>(Columns.RequestName); }
			set {
				SetColumnValue(Columns.RequestName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestName));
			}
		}
		[DataMember]
		public string RequestNameDescription {
			get { return GetColumnValue<string>(Columns.RequestNameDescription); }
			set {
				SetColumnValue(Columns.RequestNameDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestNameDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return RequestName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequestNameIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequestNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RequestNameDescriptionColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequestNameID = @"RequestNameID";
			public static readonly string RequestName = @"RequestName";
			public static readonly string RequestNameDescription = @"RequestNameDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequestNameID; }
		}
		*/

		#region Foreign Collections

		private LP_RequestCollection _LP_RequestsCol;
		//Relationship: FK_LP_Requests_LP_RequestNames
		public LP_RequestCollection LP_RequestsCol
		{
			get
			{
				if(_LP_RequestsCol == null) {
					_LP_RequestsCol = new LP_RequestCollection();
					_LP_RequestsCol.LoadAndCloseReader(LP_Request.Query()
						.WHERE(LP_Request.Columns.RequestNameId, RequestNameID).ExecuteReader());
				}
				return _LP_RequestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the LP_Request class.
	/// </summary>
	[DataContract]
	public partial class LP_RequestCollection : ActiveList<LP_Request, LP_RequestCollection>
	{
		public static LP_RequestCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			LP_RequestCollection result = new LP_RequestCollection();
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
			foreach (LP_Request item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the LP_Requests table.
	/// </summary>
	[DataContract]
	public partial class LP_Request : ActiveRecord<LP_Request>, INotifyPropertyChanged
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

		public LP_Request()
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
				TableSchema.Table schema = new TableSchema.Table("LP_Requests", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
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

				TableSchema.TableColumn colvarRequestNameId = new TableSchema.TableColumn(schema);
				colvarRequestNameId.ColumnName = "RequestNameId";
				colvarRequestNameId.DataType = DbType.AnsiString;
				colvarRequestNameId.MaxLength = 20;
				colvarRequestNameId.AutoIncrement = false;
				colvarRequestNameId.IsNullable = false;
				colvarRequestNameId.IsPrimaryKey = false;
				colvarRequestNameId.IsForeignKey = true;
				colvarRequestNameId.IsReadOnly = false;
				colvarRequestNameId.DefaultSetting = @"";
				colvarRequestNameId.ForeignKeyTableName = "LP_RequestNames";
				schema.Columns.Add(colvarRequestNameId);

				TableSchema.TableColumn colvarCommandMessageId = new TableSchema.TableColumn(schema);
				colvarCommandMessageId.ColumnName = "CommandMessageId";
				colvarCommandMessageId.DataType = DbType.Int64;
				colvarCommandMessageId.MaxLength = 0;
				colvarCommandMessageId.AutoIncrement = false;
				colvarCommandMessageId.IsNullable = true;
				colvarCommandMessageId.IsPrimaryKey = false;
				colvarCommandMessageId.IsForeignKey = true;
				colvarCommandMessageId.IsReadOnly = false;
				colvarCommandMessageId.DefaultSetting = @"";
				colvarCommandMessageId.ForeignKeyTableName = "LP_CommandMessages";
				schema.Columns.Add(colvarCommandMessageId);

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

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

				TableSchema.TableColumn colvarAttempts = new TableSchema.TableColumn(schema);
				colvarAttempts.ColumnName = "Attempts";
				colvarAttempts.DataType = DbType.Int32;
				colvarAttempts.MaxLength = 0;
				colvarAttempts.AutoIncrement = false;
				colvarAttempts.IsNullable = true;
				colvarAttempts.IsPrimaryKey = false;
				colvarAttempts.IsForeignKey = false;
				colvarAttempts.IsReadOnly = false;
				colvarAttempts.DefaultSetting = @"";
				colvarAttempts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttempts);

				TableSchema.TableColumn colvarLastAttempDate = new TableSchema.TableColumn(schema);
				colvarLastAttempDate.ColumnName = "LastAttempDate";
				colvarLastAttempDate.DataType = DbType.DateTime;
				colvarLastAttempDate.MaxLength = 0;
				colvarLastAttempDate.AutoIncrement = false;
				colvarLastAttempDate.IsNullable = true;
				colvarLastAttempDate.IsPrimaryKey = false;
				colvarLastAttempDate.IsForeignKey = false;
				colvarLastAttempDate.IsReadOnly = false;
				colvarLastAttempDate.DefaultSetting = @"";
				colvarLastAttempDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAttempDate);

				TableSchema.TableColumn colvarProcessDate = new TableSchema.TableColumn(schema);
				colvarProcessDate.ColumnName = "ProcessDate";
				colvarProcessDate.DataType = DbType.DateTime;
				colvarProcessDate.MaxLength = 0;
				colvarProcessDate.AutoIncrement = false;
				colvarProcessDate.IsNullable = true;
				colvarProcessDate.IsPrimaryKey = false;
				colvarProcessDate.IsForeignKey = false;
				colvarProcessDate.IsReadOnly = false;
				colvarProcessDate.DefaultSetting = @"";
				colvarProcessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessDate);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("LP_Requests",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static LP_Request LoadFrom(LP_Request item)
		{
			LP_Request result = new LP_Request();
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
		public string RequestNameId {
			get { return GetColumnValue<string>(Columns.RequestNameId); }
			set {
				SetColumnValue(Columns.RequestNameId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RequestNameId));
			}
		}
		[DataMember]
		public long? CommandMessageId {
			get { return GetColumnValue<long?>(Columns.CommandMessageId); }
			set {
				SetColumnValue(Columns.CommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageId));
			}
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public long UnitID {
			get { return GetColumnValue<long>(Columns.UnitID); }
			set {
				SetColumnValue(Columns.UnitID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UnitID));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
			}
		}
		[DataMember]
		public int? Attempts {
			get { return GetColumnValue<int?>(Columns.Attempts); }
			set {
				SetColumnValue(Columns.Attempts, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Attempts));
			}
		}
		[DataMember]
		public DateTime? LastAttempDate {
			get { return GetColumnValue<DateTime?>(Columns.LastAttempDate); }
			set {
				SetColumnValue(Columns.LastAttempDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAttempDate));
			}
		}
		[DataMember]
		public DateTime? ProcessDate {
			get { return GetColumnValue<DateTime?>(Columns.ProcessDate); }
			set {
				SetColumnValue(Columns.ProcessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProcessDate));
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

		private LP_CommandMessage _CommandMessage;
		//Relationship: FK_LP_Requests_LP_CommandMessages
		public LP_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = LP_CommandMessage.FetchByID(this.CommandMessageId);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageId", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		private LP_RequestName _RequestName;
		//Relationship: FK_LP_Requests_LP_RequestNames
		public LP_RequestName RequestName
		{
			get
			{
				if(_RequestName == null) {
					_RequestName = LP_RequestName.FetchByID(this.RequestNameId);
				}
				return _RequestName;
			}
			set
			{
				SetColumnValue("RequestNameId", value.RequestNameID);
				_RequestName = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RequestNameId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RequestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RequestNameIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandMessageIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UnitIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn AttemptsColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LastAttempDateColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ProcessDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RequestID = @"RequestID";
			public static readonly string RequestNameId = @"RequestNameId";
			public static readonly string CommandMessageId = @"CommandMessageId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string UnitID = @"UnitID";
			public static readonly string Sentence = @"Sentence";
			public static readonly string Attempts = @"Attempts";
			public static readonly string LastAttempDate = @"LastAttempDate";
			public static readonly string ProcessDate = @"ProcessDate";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RequestID; }
		}
		*/

		#region Foreign Collections

		private LP_CommandMessageCollection _LP_CommandMessagesCol;
		//Relationship: FK_LP_CommandMessages_LP_Requests
		public LP_CommandMessageCollection LP_CommandMessagesCol
		{
			get
			{
				if(_LP_CommandMessagesCol == null) {
					_LP_CommandMessagesCol = new LP_CommandMessageCollection();
					_LP_CommandMessagesCol.LoadAndCloseReader(LP_CommandMessage.Query()
						.WHERE(LP_CommandMessage.Columns.RequestId, RequestID).ExecuteReader());
				}
				return _LP_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SS_CommandMessageName class.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessageNameCollection : ActiveList<SS_CommandMessageName, SS_CommandMessageNameCollection>
	{
		public static SS_CommandMessageNameCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SS_CommandMessageNameCollection result = new SS_CommandMessageNameCollection();
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
			foreach (SS_CommandMessageName item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SS_CommandMessageNames table.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessageName : ActiveRecord<SS_CommandMessageName>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string BDAID = "BDA";
			[EnumMember()] public const string BSAID = "BSA";
			[EnumMember()] public const string BTAID = "BTA";
			[EnumMember()] public const string CGCID = "CGC";
			[EnumMember()] public const string CLRID = "CLR";
			[EnumMember()] public const string ERRID = "ERR";
			[EnumMember()] public const string EZBID = "EZB";
			[EnumMember()] public const string FDAID = "FDA";
			[EnumMember()] public const string IZBID = "IZB";
			[EnumMember()] public const string LBAID = "LBA";
			[EnumMember()] public const string PDEID = "PDE";
			[EnumMember()] public const string PWOID = "PWO";
			[EnumMember()] public const string PWRID = "PWR";
			[EnumMember()] public const string RSCID = "RSC";
			[EnumMember()] public const string SHDID = "SHD";
			[EnumMember()] public const string SIRID = "SIR";
			[EnumMember()] public const string SOSID = "SOS";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public SS_CommandMessageName()
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
				TableSchema.Table schema = new TableSchema.Table("SS_CommandMessageNames", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageNameID = new TableSchema.TableColumn(schema);
				colvarCommandMessageNameID.ColumnName = "CommandMessageNameID";
				colvarCommandMessageNameID.DataType = DbType.AnsiStringFixedLength;
				colvarCommandMessageNameID.MaxLength = 3;
				colvarCommandMessageNameID.AutoIncrement = false;
				colvarCommandMessageNameID.IsNullable = false;
				colvarCommandMessageNameID.IsPrimaryKey = true;
				colvarCommandMessageNameID.IsForeignKey = false;
				colvarCommandMessageNameID.IsReadOnly = false;
				colvarCommandMessageNameID.DefaultSetting = @"";
				colvarCommandMessageNameID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageNameID);

				TableSchema.TableColumn colvarCommandMessageName = new TableSchema.TableColumn(schema);
				colvarCommandMessageName.ColumnName = "CommandMessageName";
				colvarCommandMessageName.DataType = DbType.AnsiString;
				colvarCommandMessageName.MaxLength = 5;
				colvarCommandMessageName.AutoIncrement = false;
				colvarCommandMessageName.IsNullable = false;
				colvarCommandMessageName.IsPrimaryKey = false;
				colvarCommandMessageName.IsForeignKey = false;
				colvarCommandMessageName.IsReadOnly = false;
				colvarCommandMessageName.DefaultSetting = @"";
				colvarCommandMessageName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageName);

				TableSchema.TableColumn colvarCommandMessageNameDesc = new TableSchema.TableColumn(schema);
				colvarCommandMessageNameDesc.ColumnName = "CommandMessageNameDesc";
				colvarCommandMessageNameDesc.DataType = DbType.String;
				colvarCommandMessageNameDesc.MaxLength = 250;
				colvarCommandMessageNameDesc.AutoIncrement = false;
				colvarCommandMessageNameDesc.IsNullable = false;
				colvarCommandMessageNameDesc.IsPrimaryKey = false;
				colvarCommandMessageNameDesc.IsForeignKey = false;
				colvarCommandMessageNameDesc.IsReadOnly = false;
				colvarCommandMessageNameDesc.DefaultSetting = @"";
				colvarCommandMessageNameDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageNameDesc);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("SS_CommandMessageNames",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SS_CommandMessageName LoadFrom(SS_CommandMessageName item)
		{
			SS_CommandMessageName result = new SS_CommandMessageName();
			if (item.CommandMessageNameID != default(string)) {
				result.LoadByKey(item.CommandMessageNameID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string CommandMessageNameID {
			get { return GetColumnValue<string>(Columns.CommandMessageNameID); }
			set {
				SetColumnValue(Columns.CommandMessageNameID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageNameID));
			}
		}
		[DataMember]
		public string CommandMessageName {
			get { return GetColumnValue<string>(Columns.CommandMessageName); }
			set {
				SetColumnValue(Columns.CommandMessageName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageName));
			}
		}
		[DataMember]
		public string CommandMessageNameDesc {
			get { return GetColumnValue<string>(Columns.CommandMessageNameDesc); }
			set {
				SetColumnValue(Columns.CommandMessageNameDesc, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageNameDesc));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return CommandMessageName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageNameIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandMessageNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandMessageNameDescColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageNameID = @"CommandMessageNameID";
			public static readonly string CommandMessageName = @"CommandMessageName";
			public static readonly string CommandMessageNameDesc = @"CommandMessageNameDesc";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageNameID; }
		}
		*/

		#region Foreign Collections

		private SS_CommandMessageCollection _SS_CommandMessagesCol;
		//Relationship: FK_SS_CommandMessages_SS_CommandMessageNames
		public SS_CommandMessageCollection SS_CommandMessagesCol
		{
			get
			{
				if(_SS_CommandMessagesCol == null) {
					_SS_CommandMessagesCol = new SS_CommandMessageCollection();
					_SS_CommandMessagesCol.LoadAndCloseReader(SS_CommandMessage.Query()
						.WHERE(SS_CommandMessage.Columns.CommandMessageNameId, CommandMessageNameID).ExecuteReader());
				}
				return _SS_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SS_CommandMessage class.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessageCollection : ActiveList<SS_CommandMessage, SS_CommandMessageCollection>
	{
		public static SS_CommandMessageCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SS_CommandMessageCollection result = new SS_CommandMessageCollection();
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
			foreach (SS_CommandMessage item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SS_CommandMessages table.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessage : ActiveRecord<SS_CommandMessage>, INotifyPropertyChanged
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

		public SS_CommandMessage()
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
				TableSchema.Table schema = new TableSchema.Table("SS_CommandMessages", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageID = new TableSchema.TableColumn(schema);
				colvarCommandMessageID.ColumnName = "CommandMessageID";
				colvarCommandMessageID.DataType = DbType.Int64;
				colvarCommandMessageID.MaxLength = 0;
				colvarCommandMessageID.AutoIncrement = true;
				colvarCommandMessageID.IsNullable = false;
				colvarCommandMessageID.IsPrimaryKey = true;
				colvarCommandMessageID.IsForeignKey = false;
				colvarCommandMessageID.IsReadOnly = false;
				colvarCommandMessageID.DefaultSetting = @"";
				colvarCommandMessageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageID);

				TableSchema.TableColumn colvarCommandMessageTypeId = new TableSchema.TableColumn(schema);
				colvarCommandMessageTypeId.ColumnName = "CommandMessageTypeId";
				colvarCommandMessageTypeId.DataType = DbType.AnsiString;
				colvarCommandMessageTypeId.MaxLength = 20;
				colvarCommandMessageTypeId.AutoIncrement = false;
				colvarCommandMessageTypeId.IsNullable = false;
				colvarCommandMessageTypeId.IsPrimaryKey = false;
				colvarCommandMessageTypeId.IsForeignKey = true;
				colvarCommandMessageTypeId.IsReadOnly = false;
				colvarCommandMessageTypeId.DefaultSetting = @"";
				colvarCommandMessageTypeId.ForeignKeyTableName = "SS_CommandMessageTypes";
				schema.Columns.Add(colvarCommandMessageTypeId);

				TableSchema.TableColumn colvarCommandMessageNameId = new TableSchema.TableColumn(schema);
				colvarCommandMessageNameId.ColumnName = "CommandMessageNameId";
				colvarCommandMessageNameId.DataType = DbType.AnsiStringFixedLength;
				colvarCommandMessageNameId.MaxLength = 3;
				colvarCommandMessageNameId.AutoIncrement = false;
				colvarCommandMessageNameId.IsNullable = false;
				colvarCommandMessageNameId.IsPrimaryKey = false;
				colvarCommandMessageNameId.IsForeignKey = true;
				colvarCommandMessageNameId.IsReadOnly = false;
				colvarCommandMessageNameId.DefaultSetting = @"";
				colvarCommandMessageNameId.ForeignKeyTableName = "SS_CommandMessageNames";
				schema.Columns.Add(colvarCommandMessageNameId);

				TableSchema.TableColumn colvarDeviceRequestId = new TableSchema.TableColumn(schema);
				colvarDeviceRequestId.ColumnName = "DeviceRequestId";
				colvarDeviceRequestId.DataType = DbType.Int64;
				colvarDeviceRequestId.MaxLength = 0;
				colvarDeviceRequestId.AutoIncrement = false;
				colvarDeviceRequestId.IsNullable = true;
				colvarDeviceRequestId.IsPrimaryKey = false;
				colvarDeviceRequestId.IsForeignKey = true;
				colvarDeviceRequestId.IsReadOnly = false;
				colvarDeviceRequestId.DefaultSetting = @"";
				colvarDeviceRequestId.ForeignKeyTableName = "SS_DeviceRequests";
				schema.Columns.Add(colvarDeviceRequestId);

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

				TableSchema.TableColumn colvarMessageDate = new TableSchema.TableColumn(schema);
				colvarMessageDate.ColumnName = "MessageDate";
				colvarMessageDate.DataType = DbType.DateTime;
				colvarMessageDate.MaxLength = 0;
				colvarMessageDate.AutoIncrement = false;
				colvarMessageDate.IsNullable = true;
				colvarMessageDate.IsPrimaryKey = false;
				colvarMessageDate.IsForeignKey = false;
				colvarMessageDate.IsReadOnly = false;
				colvarMessageDate.DefaultSetting = @"";
				colvarMessageDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMessageDate);

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("SS_CommandMessages",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SS_CommandMessage LoadFrom(SS_CommandMessage item)
		{
			SS_CommandMessage result = new SS_CommandMessage();
			if (item.CommandMessageID != default(long)) {
				result.LoadByKey(item.CommandMessageID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long CommandMessageID {
			get { return GetColumnValue<long>(Columns.CommandMessageID); }
			set {
				SetColumnValue(Columns.CommandMessageID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageID));
			}
		}
		[DataMember]
		public string CommandMessageTypeId {
			get { return GetColumnValue<string>(Columns.CommandMessageTypeId); }
			set {
				SetColumnValue(Columns.CommandMessageTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageTypeId));
			}
		}
		[DataMember]
		public string CommandMessageNameId {
			get { return GetColumnValue<string>(Columns.CommandMessageNameId); }
			set {
				SetColumnValue(Columns.CommandMessageNameId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageNameId));
			}
		}
		[DataMember]
		public long? DeviceRequestId {
			get { return GetColumnValue<long?>(Columns.DeviceRequestId); }
			set {
				SetColumnValue(Columns.DeviceRequestId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceRequestId));
			}
		}
		[DataMember]
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int? Port {
			get { return GetColumnValue<int?>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public DateTime? MessageDate {
			get { return GetColumnValue<DateTime?>(Columns.MessageDate); }
			set {
				SetColumnValue(Columns.MessageDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MessageDate));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
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

		private SS_CommandMessageName _CommandMessageName;
		//Relationship: FK_SS_CommandMessages_SS_CommandMessageNames
		public SS_CommandMessageName CommandMessageName
		{
			get
			{
				if(_CommandMessageName == null) {
					_CommandMessageName = SS_CommandMessageName.FetchByID(this.CommandMessageNameId);
				}
				return _CommandMessageName;
			}
			set
			{
				SetColumnValue("CommandMessageNameId", value.CommandMessageNameID);
				_CommandMessageName = value;
			}
		}

		private SS_CommandMessageType _CommandMessageType;
		//Relationship: FK_SS_CommandMessages_SS_CommandMessageTypes
		public SS_CommandMessageType CommandMessageType
		{
			get
			{
				if(_CommandMessageType == null) {
					_CommandMessageType = SS_CommandMessageType.FetchByID(this.CommandMessageTypeId);
				}
				return _CommandMessageType;
			}
			set
			{
				SetColumnValue("CommandMessageTypeId", value.CommandMessageTypeID);
				_CommandMessageType = value;
			}
		}

		private SS_DeviceRequest _DeviceRequest;
		//Relationship: FK_SS_CommandMessages_SS_DeviceRequests
		public SS_DeviceRequest DeviceRequest
		{
			get
			{
				if(_DeviceRequest == null) {
					_DeviceRequest = SS_DeviceRequest.FetchByID(this.DeviceRequestId);
				}
				return _DeviceRequest;
			}
			set
			{
				SetColumnValue("DeviceRequestId", value.DeviceRequestID);
				_DeviceRequest = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CommandMessageTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandMessageTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CommandMessageNameIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DeviceRequestIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn MessageDateColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageID = @"CommandMessageID";
			public static readonly string CommandMessageTypeId = @"CommandMessageTypeId";
			public static readonly string CommandMessageNameId = @"CommandMessageNameId";
			public static readonly string DeviceRequestId = @"DeviceRequestId";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string MessageDate = @"MessageDate";
			public static readonly string Sentence = @"Sentence";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageID; }
		}
		*/

		#region Foreign Collections

		private SS_DeviceRequestCollection _SS_DeviceRequestsCol;
		//Relationship: FK_SS_DeviceRequests_SS_CommandMessages
		public SS_DeviceRequestCollection SS_DeviceRequestsCol
		{
			get
			{
				if(_SS_DeviceRequestsCol == null) {
					_SS_DeviceRequestsCol = new SS_DeviceRequestCollection();
					_SS_DeviceRequestsCol.LoadAndCloseReader(SS_DeviceRequest.Query()
						.WHERE(SS_DeviceRequest.Columns.CommandMessageId, CommandMessageID).ExecuteReader());
				}
				return _SS_DeviceRequestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SS_CommandMessageType class.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessageTypeCollection : ActiveList<SS_CommandMessageType, SS_CommandMessageTypeCollection>
	{
		public static SS_CommandMessageTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SS_CommandMessageTypeCollection result = new SS_CommandMessageTypeCollection();
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
			foreach (SS_CommandMessageType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SS_CommandMessageTypes table.
	/// </summary>
	[DataContract]
	public partial class SS_CommandMessageType : ActiveRecord<SS_CommandMessageType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
			[EnumMember()] public const string ClientRequestID = "CREQ";
			[EnumMember()] public const string ClientResponseID = "CRES";
			[EnumMember()] public const string ServerRequestID = "SREQ";
			[EnumMember()] public const string ServerResponseID = "SRES";
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public SS_CommandMessageType()
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
				TableSchema.Table schema = new TableSchema.Table("SS_CommandMessageTypes", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCommandMessageTypeID = new TableSchema.TableColumn(schema);
				colvarCommandMessageTypeID.ColumnName = "CommandMessageTypeID";
				colvarCommandMessageTypeID.DataType = DbType.AnsiString;
				colvarCommandMessageTypeID.MaxLength = 20;
				colvarCommandMessageTypeID.AutoIncrement = false;
				colvarCommandMessageTypeID.IsNullable = false;
				colvarCommandMessageTypeID.IsPrimaryKey = true;
				colvarCommandMessageTypeID.IsForeignKey = false;
				colvarCommandMessageTypeID.IsReadOnly = false;
				colvarCommandMessageTypeID.DefaultSetting = @"";
				colvarCommandMessageTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageTypeID);

				TableSchema.TableColumn colvarCommandMessageType = new TableSchema.TableColumn(schema);
				colvarCommandMessageType.ColumnName = "CommandMessageType";
				colvarCommandMessageType.DataType = DbType.AnsiString;
				colvarCommandMessageType.MaxLength = 50;
				colvarCommandMessageType.AutoIncrement = false;
				colvarCommandMessageType.IsNullable = false;
				colvarCommandMessageType.IsPrimaryKey = false;
				colvarCommandMessageType.IsForeignKey = false;
				colvarCommandMessageType.IsReadOnly = false;
				colvarCommandMessageType.DefaultSetting = @"";
				colvarCommandMessageType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCommandMessageType);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("SS_CommandMessageTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SS_CommandMessageType LoadFrom(SS_CommandMessageType item)
		{
			SS_CommandMessageType result = new SS_CommandMessageType();
			if (item.CommandMessageTypeID != default(string)) {
				result.LoadByKey(item.CommandMessageTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string CommandMessageTypeID {
			get { return GetColumnValue<string>(Columns.CommandMessageTypeID); }
			set {
				SetColumnValue(Columns.CommandMessageTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageTypeID));
			}
		}
		[DataMember]
		public string CommandMessageType {
			get { return GetColumnValue<string>(Columns.CommandMessageType); }
			set {
				SetColumnValue(Columns.CommandMessageType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageType));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return CommandMessageType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CommandMessageTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandMessageTypeColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CommandMessageTypeID = @"CommandMessageTypeID";
			public static readonly string CommandMessageType = @"CommandMessageType";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CommandMessageTypeID; }
		}
		*/

		#region Foreign Collections

		private SS_CommandMessageCollection _SS_CommandMessagesCol;
		//Relationship: FK_SS_CommandMessages_SS_CommandMessageTypes
		public SS_CommandMessageCollection SS_CommandMessagesCol
		{
			get
			{
				if(_SS_CommandMessagesCol == null) {
					_SS_CommandMessagesCol = new SS_CommandMessageCollection();
					_SS_CommandMessagesCol.LoadAndCloseReader(SS_CommandMessage.Query()
						.WHERE(SS_CommandMessage.Columns.CommandMessageTypeId, CommandMessageTypeID).ExecuteReader());
				}
				return _SS_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SS_DeviceRequest class.
	/// </summary>
	[DataContract]
	public partial class SS_DeviceRequestCollection : ActiveList<SS_DeviceRequest, SS_DeviceRequestCollection>
	{
		public static SS_DeviceRequestCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SS_DeviceRequestCollection result = new SS_DeviceRequestCollection();
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
			foreach (SS_DeviceRequest item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SS_DeviceRequests table.
	/// </summary>
	[DataContract]
	public partial class SS_DeviceRequest : ActiveRecord<SS_DeviceRequest>, INotifyPropertyChanged
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

		public SS_DeviceRequest()
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
				TableSchema.Table schema = new TableSchema.Table("SS_DeviceRequests", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeviceRequestID = new TableSchema.TableColumn(schema);
				colvarDeviceRequestID.ColumnName = "DeviceRequestID";
				colvarDeviceRequestID.DataType = DbType.Int64;
				colvarDeviceRequestID.MaxLength = 0;
				colvarDeviceRequestID.AutoIncrement = true;
				colvarDeviceRequestID.IsNullable = false;
				colvarDeviceRequestID.IsPrimaryKey = true;
				colvarDeviceRequestID.IsForeignKey = false;
				colvarDeviceRequestID.IsReadOnly = false;
				colvarDeviceRequestID.DefaultSetting = @"";
				colvarDeviceRequestID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceRequestID);

				TableSchema.TableColumn colvarCommandMessageId = new TableSchema.TableColumn(schema);
				colvarCommandMessageId.ColumnName = "CommandMessageId";
				colvarCommandMessageId.DataType = DbType.Int64;
				colvarCommandMessageId.MaxLength = 0;
				colvarCommandMessageId.AutoIncrement = false;
				colvarCommandMessageId.IsNullable = true;
				colvarCommandMessageId.IsPrimaryKey = false;
				colvarCommandMessageId.IsForeignKey = true;
				colvarCommandMessageId.IsReadOnly = false;
				colvarCommandMessageId.DefaultSetting = @"";
				colvarCommandMessageId.ForeignKeyTableName = "SS_CommandMessages";
				schema.Columns.Add(colvarCommandMessageId);

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

				TableSchema.TableColumn colvarSentence = new TableSchema.TableColumn(schema);
				colvarSentence.ColumnName = "Sentence";
				colvarSentence.DataType = DbType.AnsiString;
				colvarSentence.MaxLength = 250;
				colvarSentence.AutoIncrement = false;
				colvarSentence.IsNullable = false;
				colvarSentence.IsPrimaryKey = false;
				colvarSentence.IsForeignKey = false;
				colvarSentence.IsReadOnly = false;
				colvarSentence.DefaultSetting = @"";
				colvarSentence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSentence);

				TableSchema.TableColumn colvarAttempts = new TableSchema.TableColumn(schema);
				colvarAttempts.ColumnName = "Attempts";
				colvarAttempts.DataType = DbType.Int32;
				colvarAttempts.MaxLength = 0;
				colvarAttempts.AutoIncrement = false;
				colvarAttempts.IsNullable = true;
				colvarAttempts.IsPrimaryKey = false;
				colvarAttempts.IsForeignKey = false;
				colvarAttempts.IsReadOnly = false;
				colvarAttempts.DefaultSetting = @"";
				colvarAttempts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAttempts);

				TableSchema.TableColumn colvarLastAttemptDate = new TableSchema.TableColumn(schema);
				colvarLastAttemptDate.ColumnName = "LastAttemptDate";
				colvarLastAttemptDate.DataType = DbType.DateTime;
				colvarLastAttemptDate.MaxLength = 0;
				colvarLastAttemptDate.AutoIncrement = false;
				colvarLastAttemptDate.IsNullable = true;
				colvarLastAttemptDate.IsPrimaryKey = false;
				colvarLastAttemptDate.IsForeignKey = false;
				colvarLastAttemptDate.IsReadOnly = false;
				colvarLastAttemptDate.DefaultSetting = @"";
				colvarLastAttemptDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAttemptDate);

				TableSchema.TableColumn colvarProcessDate = new TableSchema.TableColumn(schema);
				colvarProcessDate.ColumnName = "ProcessDate";
				colvarProcessDate.DataType = DbType.DateTime;
				colvarProcessDate.MaxLength = 0;
				colvarProcessDate.AutoIncrement = false;
				colvarProcessDate.IsNullable = true;
				colvarProcessDate.IsPrimaryKey = false;
				colvarProcessDate.IsForeignKey = false;
				colvarProcessDate.IsReadOnly = false;
				colvarProcessDate.DefaultSetting = @"";
				colvarProcessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProcessDate);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("SS_DeviceRequests",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SS_DeviceRequest LoadFrom(SS_DeviceRequest item)
		{
			SS_DeviceRequest result = new SS_DeviceRequest();
			if (item.DeviceRequestID != default(long)) {
				result.LoadByKey(item.DeviceRequestID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long DeviceRequestID {
			get { return GetColumnValue<long>(Columns.DeviceRequestID); }
			set {
				SetColumnValue(Columns.DeviceRequestID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceRequestID));
			}
		}
		[DataMember]
		public long? CommandMessageId {
			get { return GetColumnValue<long?>(Columns.CommandMessageId); }
			set {
				SetColumnValue(Columns.CommandMessageId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CommandMessageId));
			}
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public string Sentence {
			get { return GetColumnValue<string>(Columns.Sentence); }
			set {
				SetColumnValue(Columns.Sentence, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Sentence));
			}
		}
		[DataMember]
		public int? Attempts {
			get { return GetColumnValue<int?>(Columns.Attempts); }
			set {
				SetColumnValue(Columns.Attempts, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Attempts));
			}
		}
		[DataMember]
		public DateTime? LastAttemptDate {
			get { return GetColumnValue<DateTime?>(Columns.LastAttemptDate); }
			set {
				SetColumnValue(Columns.LastAttemptDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAttemptDate));
			}
		}
		[DataMember]
		public DateTime? ProcessDate {
			get { return GetColumnValue<DateTime?>(Columns.ProcessDate); }
			set {
				SetColumnValue(Columns.ProcessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProcessDate));
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

		private SS_CommandMessage _CommandMessage;
		//Relationship: FK_SS_DeviceRequests_SS_CommandMessages
		public SS_CommandMessage CommandMessage
		{
			get
			{
				if(_CommandMessage == null) {
					_CommandMessage = SS_CommandMessage.FetchByID(this.CommandMessageId);
				}
				return _CommandMessage;
			}
			set
			{
				SetColumnValue("CommandMessageId", value.CommandMessageID);
				_CommandMessage = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return DeviceRequestID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeviceRequestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommandMessageIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SentenceColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AttemptsColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LastAttemptDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ProcessDateColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeviceRequestID = @"DeviceRequestID";
			public static readonly string CommandMessageId = @"CommandMessageId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string Sentence = @"Sentence";
			public static readonly string Attempts = @"Attempts";
			public static readonly string LastAttemptDate = @"LastAttemptDate";
			public static readonly string ProcessDate = @"ProcessDate";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DeviceRequestID; }
		}
		*/

		#region Foreign Collections

		private SS_CommandMessageCollection _SS_CommandMessagesCol;
		//Relationship: FK_SS_CommandMessages_SS_DeviceRequests
		public SS_CommandMessageCollection SS_CommandMessagesCol
		{
			get
			{
				if(_SS_CommandMessagesCol == null) {
					_SS_CommandMessagesCol = new SS_CommandMessageCollection();
					_SS_CommandMessagesCol.LoadAndCloseReader(SS_CommandMessage.Query()
						.WHERE(SS_CommandMessage.Columns.DeviceRequestId, DeviceRequestID).ExecuteReader());
				}
				return _SS_CommandMessagesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SS_Device class.
	/// </summary>
	[DataContract]
	public partial class SS_DeviceCollection : ActiveList<SS_Device, SS_DeviceCollection>
	{
		public static SS_DeviceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SS_DeviceCollection result = new SS_DeviceCollection();
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
			foreach (SS_Device item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SS_Devices table.
	/// </summary>
	[DataContract]
	public partial class SS_Device : ActiveRecord<SS_Device>, INotifyPropertyChanged
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

		public SS_Device()
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
				TableSchema.Table schema = new TableSchema.Table("SS_Devices", TableType.Table, DataService.GetInstance("SosGpsTrackingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeviceID = new TableSchema.TableColumn(schema);
				colvarDeviceID.ColumnName = "DeviceID";
				colvarDeviceID.DataType = DbType.Int64;
				colvarDeviceID.MaxLength = 0;
				colvarDeviceID.AutoIncrement = false;
				colvarDeviceID.IsNullable = false;
				colvarDeviceID.IsPrimaryKey = true;
				colvarDeviceID.IsForeignKey = false;
				colvarDeviceID.IsReadOnly = false;
				colvarDeviceID.DefaultSetting = @"";
				colvarDeviceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceID);

				TableSchema.TableColumn colvarAccountID = new TableSchema.TableColumn(schema);
				colvarAccountID.ColumnName = "AccountID";
				colvarAccountID.DataType = DbType.Int64;
				colvarAccountID.MaxLength = 0;
				colvarAccountID.AutoIncrement = false;
				colvarAccountID.IsNullable = false;
				colvarAccountID.IsPrimaryKey = false;
				colvarAccountID.IsForeignKey = false;
				colvarAccountID.IsReadOnly = false;
				colvarAccountID.DefaultSetting = @"";
				colvarAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountID);

				TableSchema.TableColumn colvarSIM = new TableSchema.TableColumn(schema);
				colvarSIM.ColumnName = "SIM";
				colvarSIM.DataType = DbType.AnsiString;
				colvarSIM.MaxLength = 50;
				colvarSIM.AutoIncrement = false;
				colvarSIM.IsNullable = true;
				colvarSIM.IsPrimaryKey = false;
				colvarSIM.IsForeignKey = false;
				colvarSIM.IsReadOnly = false;
				colvarSIM.DefaultSetting = @"";
				colvarSIM.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSIM);

				TableSchema.TableColumn colvarProductBarcodeId = new TableSchema.TableColumn(schema);
				colvarProductBarcodeId.ColumnName = "ProductBarcodeId";
				colvarProductBarcodeId.DataType = DbType.AnsiString;
				colvarProductBarcodeId.MaxLength = 50;
				colvarProductBarcodeId.AutoIncrement = false;
				colvarProductBarcodeId.IsNullable = true;
				colvarProductBarcodeId.IsPrimaryKey = false;
				colvarProductBarcodeId.IsForeignKey = false;
				colvarProductBarcodeId.IsReadOnly = false;
				colvarProductBarcodeId.DefaultSetting = @"";
				colvarProductBarcodeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarProductBarcodeId);

				TableSchema.TableColumn colvarPhoneNumber = new TableSchema.TableColumn(schema);
				colvarPhoneNumber.ColumnName = "PhoneNumber";
				colvarPhoneNumber.DataType = DbType.AnsiString;
				colvarPhoneNumber.MaxLength = 20;
				colvarPhoneNumber.AutoIncrement = false;
				colvarPhoneNumber.IsNullable = true;
				colvarPhoneNumber.IsPrimaryKey = false;
				colvarPhoneNumber.IsForeignKey = false;
				colvarPhoneNumber.IsReadOnly = false;
				colvarPhoneNumber.DefaultSetting = @"";
				colvarPhoneNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneNumber);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 8;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = true;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarIPAddress = new TableSchema.TableColumn(schema);
				colvarIPAddress.ColumnName = "IPAddress";
				colvarIPAddress.DataType = DbType.AnsiString;
				colvarIPAddress.MaxLength = 15;
				colvarIPAddress.AutoIncrement = false;
				colvarIPAddress.IsNullable = false;
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
				colvarPort.IsNullable = false;
				colvarPort.IsPrimaryKey = false;
				colvarPort.IsForeignKey = false;
				colvarPort.IsReadOnly = false;
				colvarPort.DefaultSetting = @"";
				colvarPort.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPort);

				TableSchema.TableColumn colvarLastAccessDate = new TableSchema.TableColumn(schema);
				colvarLastAccessDate.ColumnName = "LastAccessDate";
				colvarLastAccessDate.DataType = DbType.DateTime;
				colvarLastAccessDate.MaxLength = 0;
				colvarLastAccessDate.AutoIncrement = false;
				colvarLastAccessDate.IsNullable = false;
				colvarLastAccessDate.IsPrimaryKey = false;
				colvarLastAccessDate.IsForeignKey = false;
				colvarLastAccessDate.IsReadOnly = false;
				colvarLastAccessDate.DefaultSetting = @"(getdate())";
				colvarLastAccessDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastAccessDate);

				TableSchema.TableColumn colvarFirmwareVersion = new TableSchema.TableColumn(schema);
				colvarFirmwareVersion.ColumnName = "FirmwareVersion";
				colvarFirmwareVersion.DataType = DbType.AnsiString;
				colvarFirmwareVersion.MaxLength = 20;
				colvarFirmwareVersion.AutoIncrement = false;
				colvarFirmwareVersion.IsNullable = true;
				colvarFirmwareVersion.IsPrimaryKey = false;
				colvarFirmwareVersion.IsForeignKey = false;
				colvarFirmwareVersion.IsReadOnly = false;
				colvarFirmwareVersion.DefaultSetting = @"";
				colvarFirmwareVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirmwareVersion);

				TableSchema.TableColumn colvarSerialNumber = new TableSchema.TableColumn(schema);
				colvarSerialNumber.ColumnName = "SerialNumber";
				colvarSerialNumber.DataType = DbType.AnsiString;
				colvarSerialNumber.MaxLength = 50;
				colvarSerialNumber.AutoIncrement = false;
				colvarSerialNumber.IsNullable = true;
				colvarSerialNumber.IsPrimaryKey = false;
				colvarSerialNumber.IsForeignKey = false;
				colvarSerialNumber.IsReadOnly = false;
				colvarSerialNumber.DefaultSetting = @"";
				colvarSerialNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSerialNumber);

				BaseSchema = schema;
				DataService.Providers["SosGpsTrackingProvider"].AddSchema("SS_Devices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SS_Device LoadFrom(SS_Device item)
		{
			SS_Device result = new SS_Device();
			if (item.DeviceID != default(long)) {
				result.LoadByKey(item.DeviceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long DeviceID {
			get { return GetColumnValue<long>(Columns.DeviceID); }
			set {
				SetColumnValue(Columns.DeviceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceID));
			}
		}
		[DataMember]
		public long AccountID {
			get { return GetColumnValue<long>(Columns.AccountID); }
			set {
				SetColumnValue(Columns.AccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountID));
			}
		}
		[DataMember]
		public string SIM {
			get { return GetColumnValue<string>(Columns.SIM); }
			set {
				SetColumnValue(Columns.SIM, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SIM));
			}
		}
		[DataMember]
		public string ProductBarcodeId {
			get { return GetColumnValue<string>(Columns.ProductBarcodeId); }
			set {
				SetColumnValue(Columns.ProductBarcodeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ProductBarcodeId));
			}
		}
		[DataMember]
		public string PhoneNumber {
			get { return GetColumnValue<string>(Columns.PhoneNumber); }
			set {
				SetColumnValue(Columns.PhoneNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneNumber));
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
		public string IPAddress {
			get { return GetColumnValue<string>(Columns.IPAddress); }
			set {
				SetColumnValue(Columns.IPAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IPAddress));
			}
		}
		[DataMember]
		public int Port {
			get { return GetColumnValue<int>(Columns.Port); }
			set {
				SetColumnValue(Columns.Port, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Port));
			}
		}
		[DataMember]
		public DateTime LastAccessDate {
			get { return GetColumnValue<DateTime>(Columns.LastAccessDate); }
			set {
				SetColumnValue(Columns.LastAccessDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastAccessDate));
			}
		}
		[DataMember]
		public string FirmwareVersion {
			get { return GetColumnValue<string>(Columns.FirmwareVersion); }
			set {
				SetColumnValue(Columns.FirmwareVersion, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FirmwareVersion));
			}
		}
		[DataMember]
		public string SerialNumber {
			get { return GetColumnValue<string>(Columns.SerialNumber); }
			set {
				SetColumnValue(Columns.SerialNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SerialNumber));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return DeviceID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeviceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SIMColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ProductBarcodeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhoneNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IPAddressColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PortColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn LastAccessDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn FirmwareVersionColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SerialNumberColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeviceID = @"DeviceID";
			public static readonly string AccountID = @"AccountID";
			public static readonly string SIM = @"SIM";
			public static readonly string ProductBarcodeId = @"ProductBarcodeId";
			public static readonly string PhoneNumber = @"PhoneNumber";
			public static readonly string Password = @"Password";
			public static readonly string IPAddress = @"IPAddress";
			public static readonly string Port = @"Port";
			public static readonly string LastAccessDate = @"LastAccessDate";
			public static readonly string FirmwareVersion = @"FirmwareVersion";
			public static readonly string SerialNumber = @"SerialNumber";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DeviceID; }
		}
		*/


	}
}
