


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

namespace NXS.Data.Funding
{
	/// <summary>
	/// Strongly-typed collection for the FE_BundleItemsView class.
	/// </summary>
	[DataContract]
	public partial class FE_BundleItemsViewCollection : ReadOnlyList<FE_BundleItemsView, FE_BundleItemsViewCollection>
	{
		public static FE_BundleItemsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_BundleItemsViewCollection result = new FE_BundleItemsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_BundleItems view.
	/// </summary>
	[DataContract]
	public partial class FE_BundleItemsView : ReadOnlyRecord<FE_BundleItemsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_BundleItems", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarBundleItemID = new TableSchema.TableColumn(schema);
				colvarBundleItemID.ColumnName = "BundleItemID";
				colvarBundleItemID.DataType = DbType.Int32;
				colvarBundleItemID.MaxLength = 0;
				colvarBundleItemID.AutoIncrement = false;
				colvarBundleItemID.IsNullable = false;
				colvarBundleItemID.IsPrimaryKey = false;
				colvarBundleItemID.IsForeignKey = false;
				colvarBundleItemID.IsReadOnly = false;
				colvarBundleItemID.DefaultSetting = @"";
				colvarBundleItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBundleItemID);

				TableSchema.TableColumn colvarBundleId = new TableSchema.TableColumn(schema);
				colvarBundleId.ColumnName = "BundleId";
				colvarBundleId.DataType = DbType.Int32;
				colvarBundleId.MaxLength = 0;
				colvarBundleId.AutoIncrement = false;
				colvarBundleId.IsNullable = false;
				colvarBundleId.IsPrimaryKey = false;
				colvarBundleId.IsForeignKey = false;
				colvarBundleId.IsReadOnly = false;
				colvarBundleId.DefaultSetting = @"";
				colvarBundleId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBundleId);

				TableSchema.TableColumn colvarPacketId = new TableSchema.TableColumn(schema);
				colvarPacketId.ColumnName = "PacketId";
				colvarPacketId.DataType = DbType.Int32;
				colvarPacketId.MaxLength = 0;
				colvarPacketId.AutoIncrement = false;
				colvarPacketId.IsNullable = false;
				colvarPacketId.IsPrimaryKey = false;
				colvarPacketId.IsForeignKey = false;
				colvarPacketId.IsReadOnly = false;
				colvarPacketId.DefaultSetting = @"";
				colvarPacketId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketId);

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

				TableSchema.TableColumn colvarPSubmittedOn = new TableSchema.TableColumn(schema);
				colvarPSubmittedOn.ColumnName = "PSubmittedOn";
				colvarPSubmittedOn.DataType = DbType.DateTime;
				colvarPSubmittedOn.MaxLength = 0;
				colvarPSubmittedOn.AutoIncrement = false;
				colvarPSubmittedOn.IsNullable = true;
				colvarPSubmittedOn.IsPrimaryKey = false;
				colvarPSubmittedOn.IsForeignKey = false;
				colvarPSubmittedOn.IsReadOnly = false;
				colvarPSubmittedOn.DefaultSetting = @"";
				colvarPSubmittedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPSubmittedOn);

				TableSchema.TableColumn colvarPSubmittedBy = new TableSchema.TableColumn(schema);
				colvarPSubmittedBy.ColumnName = "PSubmittedBy";
				colvarPSubmittedBy.DataType = DbType.String;
				colvarPSubmittedBy.MaxLength = 50;
				colvarPSubmittedBy.AutoIncrement = false;
				colvarPSubmittedBy.IsNullable = true;
				colvarPSubmittedBy.IsPrimaryKey = false;
				colvarPSubmittedBy.IsForeignKey = false;
				colvarPSubmittedBy.IsReadOnly = false;
				colvarPSubmittedBy.DefaultSetting = @"";
				colvarPSubmittedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPSubmittedBy);

				TableSchema.TableColumn colvarPCreatedOn = new TableSchema.TableColumn(schema);
				colvarPCreatedOn.ColumnName = "PCreatedOn";
				colvarPCreatedOn.DataType = DbType.DateTime;
				colvarPCreatedOn.MaxLength = 0;
				colvarPCreatedOn.AutoIncrement = false;
				colvarPCreatedOn.IsNullable = false;
				colvarPCreatedOn.IsPrimaryKey = false;
				colvarPCreatedOn.IsForeignKey = false;
				colvarPCreatedOn.IsReadOnly = false;
				colvarPCreatedOn.DefaultSetting = @"";
				colvarPCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPCreatedOn);

				TableSchema.TableColumn colvarPCreatedBy = new TableSchema.TableColumn(schema);
				colvarPCreatedBy.ColumnName = "PCreatedBy";
				colvarPCreatedBy.DataType = DbType.String;
				colvarPCreatedBy.MaxLength = 50;
				colvarPCreatedBy.AutoIncrement = false;
				colvarPCreatedBy.IsNullable = false;
				colvarPCreatedBy.IsPrimaryKey = false;
				colvarPCreatedBy.IsForeignKey = false;
				colvarPCreatedBy.IsReadOnly = false;
				colvarPCreatedBy.DefaultSetting = @"";
				colvarPCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPCreatedBy);

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_BundleItems",schema);
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
		public FE_BundleItemsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int BundleItemID {
			get { return GetColumnValue<int>(Columns.BundleItemID); }
			set { SetColumnValue(Columns.BundleItemID, value); }
		}
		[DataMember]
		public int BundleId {
			get { return GetColumnValue<int>(Columns.BundleId); }
			set { SetColumnValue(Columns.BundleId, value); }
		}
		[DataMember]
		public int PacketId {
			get { return GetColumnValue<int>(Columns.PacketId); }
			set { SetColumnValue(Columns.PacketId, value); }
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
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
		public DateTime? PSubmittedOn {
			get { return GetColumnValue<DateTime?>(Columns.PSubmittedOn); }
			set { SetColumnValue(Columns.PSubmittedOn, value); }
		}
		[DataMember]
		public string PSubmittedBy {
			get { return GetColumnValue<string>(Columns.PSubmittedBy); }
			set { SetColumnValue(Columns.PSubmittedBy, value); }
		}
		[DataMember]
		public DateTime PCreatedOn {
			get { return GetColumnValue<DateTime>(Columns.PCreatedOn); }
			set { SetColumnValue(Columns.PCreatedOn, value); }
		}
		[DataMember]
		public string PCreatedBy {
			get { return GetColumnValue<string>(Columns.PCreatedBy); }
			set { SetColumnValue(Columns.PCreatedBy, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return BundleItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn BundleItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn BundleIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PacketIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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
		public static TableSchema.TableColumn PSubmittedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PSubmittedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn PCreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn PCreatedByColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string BundleItemID = @"BundleItemID";
			public const string BundleId = @"BundleId";
			public const string PacketId = @"PacketId";
			public const string IsDeleted = @"IsDeleted";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
			public const string PSubmittedOn = @"PSubmittedOn";
			public const string PSubmittedBy = @"PSubmittedBy";
			public const string PCreatedOn = @"PCreatedOn";
			public const string PCreatedBy = @"PCreatedBy";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the FE_BundlesView class.
	/// </summary>
	[DataContract]
	public partial class FE_BundlesViewCollection : ReadOnlyList<FE_BundlesView, FE_BundlesViewCollection>
	{
		public static FE_BundlesViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_BundlesViewCollection result = new FE_BundlesViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_Bundles view.
	/// </summary>
	[DataContract]
	public partial class FE_BundlesView : ReadOnlyRecord<FE_BundlesView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_Bundles", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarBundleID = new TableSchema.TableColumn(schema);
				colvarBundleID.ColumnName = "BundleID";
				colvarBundleID.DataType = DbType.Int32;
				colvarBundleID.MaxLength = 0;
				colvarBundleID.AutoIncrement = false;
				colvarBundleID.IsNullable = false;
				colvarBundleID.IsPrimaryKey = false;
				colvarBundleID.IsForeignKey = false;
				colvarBundleID.IsReadOnly = false;
				colvarBundleID.DefaultSetting = @"";
				colvarBundleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBundleID);

				TableSchema.TableColumn colvarPurchaserID = new TableSchema.TableColumn(schema);
				colvarPurchaserID.ColumnName = "PurchaserID";
				colvarPurchaserID.DataType = DbType.AnsiString;
				colvarPurchaserID.MaxLength = 10;
				colvarPurchaserID.AutoIncrement = false;
				colvarPurchaserID.IsNullable = false;
				colvarPurchaserID.IsPrimaryKey = false;
				colvarPurchaserID.IsForeignKey = false;
				colvarPurchaserID.IsReadOnly = false;
				colvarPurchaserID.DefaultSetting = @"";
				colvarPurchaserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserID);

				TableSchema.TableColumn colvarPurchaserName = new TableSchema.TableColumn(schema);
				colvarPurchaserName.ColumnName = "PurchaserName";
				colvarPurchaserName.DataType = DbType.AnsiString;
				colvarPurchaserName.MaxLength = 50;
				colvarPurchaserName.AutoIncrement = false;
				colvarPurchaserName.IsNullable = false;
				colvarPurchaserName.IsPrimaryKey = false;
				colvarPurchaserName.IsForeignKey = false;
				colvarPurchaserName.IsReadOnly = false;
				colvarPurchaserName.DefaultSetting = @"";
				colvarPurchaserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserName);

				TableSchema.TableColumn colvarTrackingNumberID = new TableSchema.TableColumn(schema);
				colvarTrackingNumberID.ColumnName = "TrackingNumberID";
				colvarTrackingNumberID.DataType = DbType.Int64;
				colvarTrackingNumberID.MaxLength = 0;
				colvarTrackingNumberID.AutoIncrement = false;
				colvarTrackingNumberID.IsNullable = true;
				colvarTrackingNumberID.IsPrimaryKey = false;
				colvarTrackingNumberID.IsForeignKey = false;
				colvarTrackingNumberID.IsReadOnly = false;
				colvarTrackingNumberID.DefaultSetting = @"";
				colvarTrackingNumberID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrackingNumberID);

				TableSchema.TableColumn colvarTrackingNumber = new TableSchema.TableColumn(schema);
				colvarTrackingNumber.ColumnName = "TrackingNumber";
				colvarTrackingNumber.DataType = DbType.String;
				colvarTrackingNumber.MaxLength = 50;
				colvarTrackingNumber.AutoIncrement = false;
				colvarTrackingNumber.IsNullable = true;
				colvarTrackingNumber.IsPrimaryKey = false;
				colvarTrackingNumber.IsForeignKey = false;
				colvarTrackingNumber.IsReadOnly = false;
				colvarTrackingNumber.DefaultSetting = @"";
				colvarTrackingNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrackingNumber);

				TableSchema.TableColumn colvarDeliveryDate = new TableSchema.TableColumn(schema);
				colvarDeliveryDate.ColumnName = "DeliveryDate";
				colvarDeliveryDate.DataType = DbType.DateTime;
				colvarDeliveryDate.MaxLength = 0;
				colvarDeliveryDate.AutoIncrement = false;
				colvarDeliveryDate.IsNullable = true;
				colvarDeliveryDate.IsPrimaryKey = false;
				colvarDeliveryDate.IsForeignKey = false;
				colvarDeliveryDate.IsReadOnly = false;
				colvarDeliveryDate.DefaultSetting = @"";
				colvarDeliveryDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeliveryDate);

				TableSchema.TableColumn colvarSubmittedOn = new TableSchema.TableColumn(schema);
				colvarSubmittedOn.ColumnName = "SubmittedOn";
				colvarSubmittedOn.DataType = DbType.DateTime;
				colvarSubmittedOn.MaxLength = 0;
				colvarSubmittedOn.AutoIncrement = false;
				colvarSubmittedOn.IsNullable = true;
				colvarSubmittedOn.IsPrimaryKey = false;
				colvarSubmittedOn.IsForeignKey = false;
				colvarSubmittedOn.IsReadOnly = false;
				colvarSubmittedOn.DefaultSetting = @"";
				colvarSubmittedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmittedOn);

				TableSchema.TableColumn colvarSubmittedBy = new TableSchema.TableColumn(schema);
				colvarSubmittedBy.ColumnName = "SubmittedBy";
				colvarSubmittedBy.DataType = DbType.String;
				colvarSubmittedBy.MaxLength = 50;
				colvarSubmittedBy.AutoIncrement = false;
				colvarSubmittedBy.IsNullable = true;
				colvarSubmittedBy.IsPrimaryKey = false;
				colvarSubmittedBy.IsForeignKey = false;
				colvarSubmittedBy.IsReadOnly = false;
				colvarSubmittedBy.DefaultSetting = @"";
				colvarSubmittedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmittedBy);

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

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_Bundles",schema);
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
		public FE_BundlesView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int BundleID {
			get { return GetColumnValue<int>(Columns.BundleID); }
			set { SetColumnValue(Columns.BundleID, value); }
		}
		[DataMember]
		public string PurchaserID {
			get { return GetColumnValue<string>(Columns.PurchaserID); }
			set { SetColumnValue(Columns.PurchaserID, value); }
		}
		[DataMember]
		public string PurchaserName {
			get { return GetColumnValue<string>(Columns.PurchaserName); }
			set { SetColumnValue(Columns.PurchaserName, value); }
		}
		[DataMember]
		public long? TrackingNumberID {
			get { return GetColumnValue<long?>(Columns.TrackingNumberID); }
			set { SetColumnValue(Columns.TrackingNumberID, value); }
		}
		[DataMember]
		public string TrackingNumber {
			get { return GetColumnValue<string>(Columns.TrackingNumber); }
			set { SetColumnValue(Columns.TrackingNumber, value); }
		}
		[DataMember]
		public DateTime? DeliveryDate {
			get { return GetColumnValue<DateTime?>(Columns.DeliveryDate); }
			set { SetColumnValue(Columns.DeliveryDate, value); }
		}
		[DataMember]
		public DateTime? SubmittedOn {
			get { return GetColumnValue<DateTime?>(Columns.SubmittedOn); }
			set { SetColumnValue(Columns.SubmittedOn, value); }
		}
		[DataMember]
		public string SubmittedBy {
			get { return GetColumnValue<string>(Columns.SubmittedBy); }
			set { SetColumnValue(Columns.SubmittedBy, value); }
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

		#endregion //Properties

		public override string ToString()
		{
			return PurchaserID;
		}

		#region Typed Columns

		public static TableSchema.TableColumn BundleIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PurchaserNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TrackingNumberIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TrackingNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn DeliveryDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SubmittedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SubmittedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string BundleID = @"BundleID";
			public const string PurchaserID = @"PurchaserID";
			public const string PurchaserName = @"PurchaserName";
			public const string TrackingNumberID = @"TrackingNumberID";
			public const string TrackingNumber = @"TrackingNumber";
			public const string DeliveryDate = @"DeliveryDate";
			public const string SubmittedOn = @"SubmittedOn";
			public const string SubmittedBy = @"SubmittedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the FE_CriteriasView class.
	/// </summary>
	[DataContract]
	public partial class FE_CriteriasViewCollection : ReadOnlyList<FE_CriteriasView, FE_CriteriasViewCollection>
	{
		public static FE_CriteriasViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_CriteriasViewCollection result = new FE_CriteriasViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_Criterias view.
	/// </summary>
	[DataContract]
	public partial class FE_CriteriasView : ReadOnlyRecord<FE_CriteriasView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_Criterias", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCriteriaID = new TableSchema.TableColumn(schema);
				colvarCriteriaID.ColumnName = "CriteriaID";
				colvarCriteriaID.DataType = DbType.Int32;
				colvarCriteriaID.MaxLength = 0;
				colvarCriteriaID.AutoIncrement = false;
				colvarCriteriaID.IsNullable = false;
				colvarCriteriaID.IsPrimaryKey = false;
				colvarCriteriaID.IsForeignKey = false;
				colvarCriteriaID.IsReadOnly = false;
				colvarCriteriaID.DefaultSetting = @"";
				colvarCriteriaID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCriteriaID);

				TableSchema.TableColumn colvarPurchaserId = new TableSchema.TableColumn(schema);
				colvarPurchaserId.ColumnName = "PurchaserId";
				colvarPurchaserId.DataType = DbType.AnsiString;
				colvarPurchaserId.MaxLength = 10;
				colvarPurchaserId.AutoIncrement = false;
				colvarPurchaserId.IsNullable = false;
				colvarPurchaserId.IsPrimaryKey = false;
				colvarPurchaserId.IsForeignKey = false;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserId);

				TableSchema.TableColumn colvarPurchaserName = new TableSchema.TableColumn(schema);
				colvarPurchaserName.ColumnName = "PurchaserName";
				colvarPurchaserName.DataType = DbType.AnsiString;
				colvarPurchaserName.MaxLength = 50;
				colvarPurchaserName.AutoIncrement = false;
				colvarPurchaserName.IsNullable = false;
				colvarPurchaserName.IsPrimaryKey = false;
				colvarPurchaserName.IsForeignKey = false;
				colvarPurchaserName.IsReadOnly = false;
				colvarPurchaserName.DefaultSetting = @"";
				colvarPurchaserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserName);

				TableSchema.TableColumn colvarCriteriaName = new TableSchema.TableColumn(schema);
				colvarCriteriaName.ColumnName = "CriteriaName";
				colvarCriteriaName.DataType = DbType.String;
				colvarCriteriaName.MaxLength = 50;
				colvarCriteriaName.AutoIncrement = false;
				colvarCriteriaName.IsNullable = false;
				colvarCriteriaName.IsPrimaryKey = false;
				colvarCriteriaName.IsForeignKey = false;
				colvarCriteriaName.IsReadOnly = false;
				colvarCriteriaName.DefaultSetting = @"";
				colvarCriteriaName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCriteriaName);

				TableSchema.TableColumn colvarDESCRIPTION = new TableSchema.TableColumn(schema);
				colvarDESCRIPTION.ColumnName = "DESCRIPTION";
				colvarDESCRIPTION.DataType = DbType.String;
				colvarDESCRIPTION.MaxLength = -1;
				colvarDESCRIPTION.AutoIncrement = false;
				colvarDESCRIPTION.IsNullable = false;
				colvarDESCRIPTION.IsPrimaryKey = false;
				colvarDESCRIPTION.IsForeignKey = false;
				colvarDESCRIPTION.IsReadOnly = false;
				colvarDESCRIPTION.DefaultSetting = @"";
				colvarDESCRIPTION.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDESCRIPTION);

				TableSchema.TableColumn colvarFilterString = new TableSchema.TableColumn(schema);
				colvarFilterString.ColumnName = "FilterString";
				colvarFilterString.DataType = DbType.String;
				colvarFilterString.MaxLength = -1;
				colvarFilterString.AutoIncrement = false;
				colvarFilterString.IsNullable = false;
				colvarFilterString.IsPrimaryKey = false;
				colvarFilterString.IsForeignKey = false;
				colvarFilterString.IsReadOnly = false;
				colvarFilterString.DefaultSetting = @"";
				colvarFilterString.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFilterString);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 20;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"";
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
				colvarCreatedOn.DefaultSetting = @"";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_Criterias",schema);
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
		public FE_CriteriasView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int CriteriaID {
			get { return GetColumnValue<int>(Columns.CriteriaID); }
			set { SetColumnValue(Columns.CriteriaID, value); }
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set { SetColumnValue(Columns.PurchaserId, value); }
		}
		[DataMember]
		public string PurchaserName {
			get { return GetColumnValue<string>(Columns.PurchaserName); }
			set { SetColumnValue(Columns.PurchaserName, value); }
		}
		[DataMember]
		public string CriteriaName {
			get { return GetColumnValue<string>(Columns.CriteriaName); }
			set { SetColumnValue(Columns.CriteriaName, value); }
		}
		[DataMember]
		public string DESCRIPTION {
			get { return GetColumnValue<string>(Columns.DESCRIPTION); }
			set { SetColumnValue(Columns.DESCRIPTION, value); }
		}
		[DataMember]
		public string FilterString {
			get { return GetColumnValue<string>(Columns.FilterString); }
			set { SetColumnValue(Columns.FilterString, value); }
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return PurchaserId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CriteriaIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PurchaserNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CriteriaNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn DESCRIPTIONColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn FilterStringColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
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
			public const string CriteriaID = @"CriteriaID";
			public const string PurchaserId = @"PurchaserId";
			public const string PurchaserName = @"PurchaserName";
			public const string CriteriaName = @"CriteriaName";
			public const string DESCRIPTION = @"DESCRIPTION";
			public const string FilterString = @"FilterString";
			public const string CreatedBy = @"CreatedBy";
			public const string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the FE_PacketItemsView class.
	/// </summary>
	[DataContract]
	public partial class FE_PacketItemsViewCollection : ReadOnlyList<FE_PacketItemsView, FE_PacketItemsViewCollection>
	{
		public static FE_PacketItemsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PacketItemsViewCollection result = new FE_PacketItemsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_PacketItems view.
	/// </summary>
	[DataContract]
	public partial class FE_PacketItemsView : ReadOnlyRecord<FE_PacketItemsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_PacketItems", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPacketItemID = new TableSchema.TableColumn(schema);
				colvarPacketItemID.ColumnName = "PacketItemID";
				colvarPacketItemID.DataType = DbType.Int64;
				colvarPacketItemID.MaxLength = 0;
				colvarPacketItemID.AutoIncrement = false;
				colvarPacketItemID.IsNullable = false;
				colvarPacketItemID.IsPrimaryKey = false;
				colvarPacketItemID.IsForeignKey = false;
				colvarPacketItemID.IsReadOnly = false;
				colvarPacketItemID.DefaultSetting = @"";
				colvarPacketItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketItemID);

				TableSchema.TableColumn colvarPacketId = new TableSchema.TableColumn(schema);
				colvarPacketId.ColumnName = "PacketId";
				colvarPacketId.DataType = DbType.Int32;
				colvarPacketId.MaxLength = 0;
				colvarPacketId.AutoIncrement = false;
				colvarPacketId.IsNullable = false;
				colvarPacketId.IsPrimaryKey = false;
				colvarPacketId.IsForeignKey = false;
				colvarPacketId.IsReadOnly = false;
				colvarPacketId.DefaultSetting = @"";
				colvarPacketId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketId);

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.Int64;
				colvarCustomerNumber.MaxLength = 0;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = false;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarCustomerId = new TableSchema.TableColumn(schema);
				colvarCustomerId.ColumnName = "CustomerId";
				colvarCustomerId.DataType = DbType.Int64;
				colvarCustomerId.MaxLength = 0;
				colvarCustomerId.AutoIncrement = false;
				colvarCustomerId.IsNullable = false;
				colvarCustomerId.IsPrimaryKey = false;
				colvarCustomerId.IsForeignKey = false;
				colvarCustomerId.IsReadOnly = false;
				colvarCustomerId.DefaultSetting = @"";
				colvarCustomerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerId);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int32;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = false;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarCsid = new TableSchema.TableColumn(schema);
				colvarCsid.ColumnName = "Csid";
				colvarCsid.DataType = DbType.AnsiString;
				colvarCsid.MaxLength = 15;
				colvarCsid.AutoIncrement = false;
				colvarCsid.IsNullable = true;
				colvarCsid.IsPrimaryKey = false;
				colvarCsid.IsForeignKey = false;
				colvarCsid.IsReadOnly = false;
				colvarCsid.DefaultSetting = @"";
				colvarCsid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCsid);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = false;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = false;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarReturnAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarReturnAccountFundingStatusId.ColumnName = "ReturnAccountFundingStatusId";
				colvarReturnAccountFundingStatusId.DataType = DbType.Int64;
				colvarReturnAccountFundingStatusId.MaxLength = 0;
				colvarReturnAccountFundingStatusId.AutoIncrement = false;
				colvarReturnAccountFundingStatusId.IsNullable = true;
				colvarReturnAccountFundingStatusId.IsPrimaryKey = false;
				colvarReturnAccountFundingStatusId.IsForeignKey = false;
				colvarReturnAccountFundingStatusId.IsReadOnly = false;
				colvarReturnAccountFundingStatusId.DefaultSetting = @"";
				colvarReturnAccountFundingStatusId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReturnAccountFundingStatusId);

				TableSchema.TableColumn colvarAccountFundingShortDesc = new TableSchema.TableColumn(schema);
				colvarAccountFundingShortDesc.ColumnName = "AccountFundingShortDesc";
				colvarAccountFundingShortDesc.DataType = DbType.String;
				colvarAccountFundingShortDesc.MaxLength = 250;
				colvarAccountFundingShortDesc.AutoIncrement = false;
				colvarAccountFundingShortDesc.IsNullable = true;
				colvarAccountFundingShortDesc.IsPrimaryKey = false;
				colvarAccountFundingShortDesc.IsForeignKey = false;
				colvarAccountFundingShortDesc.IsReadOnly = false;
				colvarAccountFundingShortDesc.DefaultSetting = @"";
				colvarAccountFundingShortDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingShortDesc);

				TableSchema.TableColumn colvarAccountStatusNote = new TableSchema.TableColumn(schema);
				colvarAccountStatusNote.ColumnName = "AccountStatusNote";
				colvarAccountStatusNote.DataType = DbType.String;
				colvarAccountStatusNote.MaxLength = -1;
				colvarAccountStatusNote.AutoIncrement = false;
				colvarAccountStatusNote.IsNullable = true;
				colvarAccountStatusNote.IsPrimaryKey = false;
				colvarAccountStatusNote.IsForeignKey = false;
				colvarAccountStatusNote.IsReadOnly = false;
				colvarAccountStatusNote.DefaultSetting = @"";
				colvarAccountStatusNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountStatusNote);

				TableSchema.TableColumn colvarTransactionID = new TableSchema.TableColumn(schema);
				colvarTransactionID.ColumnName = "TransactionID";
				colvarTransactionID.DataType = DbType.AnsiString;
				colvarTransactionID.MaxLength = 100;
				colvarTransactionID.AutoIncrement = false;
				colvarTransactionID.IsNullable = true;
				colvarTransactionID.IsPrimaryKey = false;
				colvarTransactionID.IsForeignKey = false;
				colvarTransactionID.IsReadOnly = false;
				colvarTransactionID.DefaultSetting = @"";
				colvarTransactionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTransactionID);

				TableSchema.TableColumn colvarReportGuid = new TableSchema.TableColumn(schema);
				colvarReportGuid.ColumnName = "ReportGuid";
				colvarReportGuid.DataType = DbType.AnsiString;
				colvarReportGuid.MaxLength = 500;
				colvarReportGuid.AutoIncrement = false;
				colvarReportGuid.IsNullable = true;
				colvarReportGuid.IsPrimaryKey = false;
				colvarReportGuid.IsForeignKey = false;
				colvarReportGuid.IsReadOnly = false;
				colvarReportGuid.DefaultSetting = @"";
				colvarReportGuid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportGuid);

				TableSchema.TableColumn colvarBureau = new TableSchema.TableColumn(schema);
				colvarBureau.ColumnName = "Bureau";
				colvarBureau.DataType = DbType.AnsiString;
				colvarBureau.MaxLength = 50;
				colvarBureau.AutoIncrement = false;
				colvarBureau.IsNullable = true;
				colvarBureau.IsPrimaryKey = false;
				colvarBureau.IsForeignKey = false;
				colvarBureau.IsReadOnly = false;
				colvarBureau.DefaultSetting = @"";
				colvarBureau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBureau);

				TableSchema.TableColumn colvarGateway = new TableSchema.TableColumn(schema);
				colvarGateway.ColumnName = "Gateway";
				colvarGateway.DataType = DbType.String;
				colvarGateway.MaxLength = 50;
				colvarGateway.AutoIncrement = false;
				colvarGateway.IsNullable = true;
				colvarGateway.IsPrimaryKey = false;
				colvarGateway.IsForeignKey = false;
				colvarGateway.IsReadOnly = false;
				colvarGateway.DefaultSetting = @"";
				colvarGateway.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGateway);

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

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_PacketItems",schema);
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
		public FE_PacketItemsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long PacketItemID {
			get { return GetColumnValue<long>(Columns.PacketItemID); }
			set { SetColumnValue(Columns.PacketItemID, value); }
		}
		[DataMember]
		public int PacketId {
			get { return GetColumnValue<int>(Columns.PacketId); }
			set { SetColumnValue(Columns.PacketId, value); }
		}
		[DataMember]
		public long CustomerNumber {
			get { return GetColumnValue<long>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public long CustomerId {
			get { return GetColumnValue<long>(Columns.CustomerId); }
			set { SetColumnValue(Columns.CustomerId, value); }
		}
		[DataMember]
		public int AccountId {
			get { return GetColumnValue<int>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public string Csid {
			get { return GetColumnValue<string>(Columns.Csid); }
			set { SetColumnValue(Columns.Csid, value); }
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		[DataMember]
		public long? ReturnAccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.ReturnAccountFundingStatusId); }
			set { SetColumnValue(Columns.ReturnAccountFundingStatusId, value); }
		}
		[DataMember]
		public string AccountFundingShortDesc {
			get { return GetColumnValue<string>(Columns.AccountFundingShortDesc); }
			set { SetColumnValue(Columns.AccountFundingShortDesc, value); }
		}
		[DataMember]
		public string AccountStatusNote {
			get { return GetColumnValue<string>(Columns.AccountStatusNote); }
			set { SetColumnValue(Columns.AccountStatusNote, value); }
		}
		[DataMember]
		public string TransactionID {
			get { return GetColumnValue<string>(Columns.TransactionID); }
			set { SetColumnValue(Columns.TransactionID, value); }
		}
		[DataMember]
		public string ReportGuid {
			get { return GetColumnValue<string>(Columns.ReportGuid); }
			set { SetColumnValue(Columns.ReportGuid, value); }
		}
		[DataMember]
		public string Bureau {
			get { return GetColumnValue<string>(Columns.Bureau); }
			set { SetColumnValue(Columns.Bureau, value); }
		}
		[DataMember]
		public string Gateway {
			get { return GetColumnValue<string>(Columns.Gateway); }
			set { SetColumnValue(Columns.Gateway, value); }
		}
		[DataMember]
		public string ModifiedBy {
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		[DataMember]
		public DateTime ModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.ModifiedOn); }
			set { SetColumnValue(Columns.ModifiedOn, value); }
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return PacketItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PacketItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PacketIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CustomerIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CsidColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ReturnAccountFundingStatusIdColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn AccountFundingShortDescColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn AccountStatusNoteColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TransactionIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ReportGuidColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn BureauColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn GatewayColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[18]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string PacketItemID = @"PacketItemID";
			public const string PacketId = @"PacketId";
			public const string CustomerNumber = @"CustomerNumber";
			public const string CustomerId = @"CustomerId";
			public const string AccountId = @"AccountId";
			public const string Csid = @"Csid";
			public const string FirstName = @"FirstName";
			public const string LastName = @"LastName";
			public const string ReturnAccountFundingStatusId = @"ReturnAccountFundingStatusId";
			public const string AccountFundingShortDesc = @"AccountFundingShortDesc";
			public const string AccountStatusNote = @"AccountStatusNote";
			public const string TransactionID = @"TransactionID";
			public const string ReportGuid = @"ReportGuid";
			public const string Bureau = @"Bureau";
			public const string Gateway = @"Gateway";
			public const string ModifiedBy = @"ModifiedBy";
			public const string ModifiedOn = @"ModifiedOn";
			public const string CreatedBy = @"CreatedBy";
			public const string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the FE_PacketsView class.
	/// </summary>
	[DataContract]
	public partial class FE_PacketsViewCollection : ReadOnlyList<FE_PacketsView, FE_PacketsViewCollection>
	{
		public static FE_PacketsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PacketsViewCollection result = new FE_PacketsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_Packets view.
	/// </summary>
	[DataContract]
	public partial class FE_PacketsView : ReadOnlyRecord<FE_PacketsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_Packets", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPacketID = new TableSchema.TableColumn(schema);
				colvarPacketID.ColumnName = "PacketID";
				colvarPacketID.DataType = DbType.Int32;
				colvarPacketID.MaxLength = 0;
				colvarPacketID.AutoIncrement = false;
				colvarPacketID.IsNullable = false;
				colvarPacketID.IsPrimaryKey = false;
				colvarPacketID.IsForeignKey = false;
				colvarPacketID.IsReadOnly = false;
				colvarPacketID.DefaultSetting = @"";
				colvarPacketID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketID);

				TableSchema.TableColumn colvarCriteriaName = new TableSchema.TableColumn(schema);
				colvarCriteriaName.ColumnName = "CriteriaName";
				colvarCriteriaName.DataType = DbType.String;
				colvarCriteriaName.MaxLength = 50;
				colvarCriteriaName.AutoIncrement = false;
				colvarCriteriaName.IsNullable = false;
				colvarCriteriaName.IsPrimaryKey = false;
				colvarCriteriaName.IsForeignKey = false;
				colvarCriteriaName.IsReadOnly = false;
				colvarCriteriaName.DefaultSetting = @"";
				colvarCriteriaName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCriteriaName);

				TableSchema.TableColumn colvarCriteriaId = new TableSchema.TableColumn(schema);
				colvarCriteriaId.ColumnName = "CriteriaId";
				colvarCriteriaId.DataType = DbType.Int32;
				colvarCriteriaId.MaxLength = 0;
				colvarCriteriaId.AutoIncrement = false;
				colvarCriteriaId.IsNullable = true;
				colvarCriteriaId.IsPrimaryKey = false;
				colvarCriteriaId.IsForeignKey = false;
				colvarCriteriaId.IsReadOnly = false;
				colvarCriteriaId.DefaultSetting = @"";
				colvarCriteriaId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCriteriaId);

				TableSchema.TableColumn colvarPurchaserID = new TableSchema.TableColumn(schema);
				colvarPurchaserID.ColumnName = "PurchaserID";
				colvarPurchaserID.DataType = DbType.AnsiString;
				colvarPurchaserID.MaxLength = 10;
				colvarPurchaserID.AutoIncrement = false;
				colvarPurchaserID.IsNullable = false;
				colvarPurchaserID.IsPrimaryKey = false;
				colvarPurchaserID.IsForeignKey = false;
				colvarPurchaserID.IsReadOnly = false;
				colvarPurchaserID.DefaultSetting = @"";
				colvarPurchaserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserID);

				TableSchema.TableColumn colvarPurchaserName = new TableSchema.TableColumn(schema);
				colvarPurchaserName.ColumnName = "PurchaserName";
				colvarPurchaserName.DataType = DbType.AnsiString;
				colvarPurchaserName.MaxLength = 50;
				colvarPurchaserName.AutoIncrement = false;
				colvarPurchaserName.IsNullable = false;
				colvarPurchaserName.IsPrimaryKey = false;
				colvarPurchaserName.IsForeignKey = false;
				colvarPurchaserName.IsReadOnly = false;
				colvarPurchaserName.DefaultSetting = @"";
				colvarPurchaserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaserName);

				TableSchema.TableColumn colvarSubmittedOn = new TableSchema.TableColumn(schema);
				colvarSubmittedOn.ColumnName = "SubmittedOn";
				colvarSubmittedOn.DataType = DbType.DateTime;
				colvarSubmittedOn.MaxLength = 0;
				colvarSubmittedOn.AutoIncrement = false;
				colvarSubmittedOn.IsNullable = true;
				colvarSubmittedOn.IsPrimaryKey = false;
				colvarSubmittedOn.IsForeignKey = false;
				colvarSubmittedOn.IsReadOnly = false;
				colvarSubmittedOn.DefaultSetting = @"";
				colvarSubmittedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmittedOn);

				TableSchema.TableColumn colvarSubmittedBy = new TableSchema.TableColumn(schema);
				colvarSubmittedBy.ColumnName = "SubmittedBy";
				colvarSubmittedBy.DataType = DbType.String;
				colvarSubmittedBy.MaxLength = 50;
				colvarSubmittedBy.AutoIncrement = false;
				colvarSubmittedBy.IsNullable = true;
				colvarSubmittedBy.IsPrimaryKey = false;
				colvarSubmittedBy.IsForeignKey = false;
				colvarSubmittedBy.IsReadOnly = false;
				colvarSubmittedBy.DefaultSetting = @"";
				colvarSubmittedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmittedBy);

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

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_Packets",schema);
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
		public FE_PacketsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int PacketID {
			get { return GetColumnValue<int>(Columns.PacketID); }
			set { SetColumnValue(Columns.PacketID, value); }
		}
		[DataMember]
		public string CriteriaName {
			get { return GetColumnValue<string>(Columns.CriteriaName); }
			set { SetColumnValue(Columns.CriteriaName, value); }
		}
		[DataMember]
		public int? CriteriaId {
			get { return GetColumnValue<int?>(Columns.CriteriaId); }
			set { SetColumnValue(Columns.CriteriaId, value); }
		}
		[DataMember]
		public string PurchaserID {
			get { return GetColumnValue<string>(Columns.PurchaserID); }
			set { SetColumnValue(Columns.PurchaserID, value); }
		}
		[DataMember]
		public string PurchaserName {
			get { return GetColumnValue<string>(Columns.PurchaserName); }
			set { SetColumnValue(Columns.PurchaserName, value); }
		}
		[DataMember]
		public DateTime? SubmittedOn {
			get { return GetColumnValue<DateTime?>(Columns.SubmittedOn); }
			set { SetColumnValue(Columns.SubmittedOn, value); }
		}
		[DataMember]
		public string SubmittedBy {
			get { return GetColumnValue<string>(Columns.SubmittedBy); }
			set { SetColumnValue(Columns.SubmittedBy, value); }
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
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

		#endregion //Properties

		public override string ToString()
		{
			return CriteriaName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PacketIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CriteriaNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CriteriaIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PurchaserIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PurchaserNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn SubmittedOnColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SubmittedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string PacketID = @"PacketID";
			public const string CriteriaName = @"CriteriaName";
			public const string CriteriaId = @"CriteriaId";
			public const string PurchaserID = @"PurchaserID";
			public const string PurchaserName = @"PurchaserName";
			public const string SubmittedOn = @"SubmittedOn";
			public const string SubmittedBy = @"SubmittedBy";
			public const string IsDeleted = @"IsDeleted";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the FE_RejectedAccountsView class.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccountsViewCollection : ReadOnlyList<FE_RejectedAccountsView, FE_RejectedAccountsViewCollection>
	{
		public static FE_RejectedAccountsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_RejectedAccountsViewCollection result = new FE_RejectedAccountsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwFE_RejectedAccounts view.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccountsView : ReadOnlyRecord<FE_RejectedAccountsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwFE_RejectedAccounts", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRejectedAccountID = new TableSchema.TableColumn(schema);
				colvarRejectedAccountID.ColumnName = "RejectedAccountID";
				colvarRejectedAccountID.DataType = DbType.Int64;
				colvarRejectedAccountID.MaxLength = 0;
				colvarRejectedAccountID.AutoIncrement = true;
				colvarRejectedAccountID.IsNullable = false;
				colvarRejectedAccountID.IsPrimaryKey = false;
				colvarRejectedAccountID.IsForeignKey = false;
				colvarRejectedAccountID.IsReadOnly = false;
				colvarRejectedAccountID.DefaultSetting = @"";
				colvarRejectedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectedAccountID);

				TableSchema.TableColumn colvarRejectionId = new TableSchema.TableColumn(schema);
				colvarRejectionId.ColumnName = "RejectionId";
				colvarRejectionId.DataType = DbType.Int64;
				colvarRejectionId.MaxLength = 0;
				colvarRejectionId.AutoIncrement = false;
				colvarRejectionId.IsNullable = false;
				colvarRejectionId.IsPrimaryKey = false;
				colvarRejectionId.IsForeignKey = false;
				colvarRejectionId.IsReadOnly = false;
				colvarRejectionId.DefaultSetting = @"";
				colvarRejectionId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionId);

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

				TableSchema.TableColumn colvarPacketItemId = new TableSchema.TableColumn(schema);
				colvarPacketItemId.ColumnName = "PacketItemId";
				colvarPacketItemId.DataType = DbType.Int64;
				colvarPacketItemId.MaxLength = 0;
				colvarPacketItemId.AutoIncrement = false;
				colvarPacketItemId.IsNullable = true;
				colvarPacketItemId.IsPrimaryKey = false;
				colvarPacketItemId.IsForeignKey = false;
				colvarPacketItemId.IsReadOnly = false;
				colvarPacketItemId.DefaultSetting = @"";
				colvarPacketItemId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketItemId);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = true;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = false;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingStatusId);

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

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwFE_RejectedAccounts",schema);
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
		public FE_RejectedAccountsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long RejectedAccountID {
			get { return GetColumnValue<long>(Columns.RejectedAccountID); }
			set { SetColumnValue(Columns.RejectedAccountID, value); }
		}
		[DataMember]
		public long RejectionId {
			get { return GetColumnValue<long>(Columns.RejectionId); }
			set { SetColumnValue(Columns.RejectionId, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public long? PacketItemId {
			get { return GetColumnValue<long?>(Columns.PacketItemId); }
			set { SetColumnValue(Columns.PacketItemId, value); }
		}
		[DataMember]
		public long? AccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.AccountFundingStatusId); }
			set { SetColumnValue(Columns.AccountFundingStatusId, value); }
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

		#endregion //Properties

		public override string ToString()
		{
			return RejectedAccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RejectedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RejectionIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PacketItemIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RejectedAccountID = @"RejectedAccountID";
			public const string RejectionId = @"RejectionId";
			public const string AccountId = @"AccountId";
			public const string PacketItemId = @"PacketItemId";
			public const string AccountFundingStatusId = @"AccountFundingStatusId";
			public const string CreatedOn = @"CreatedOn";
			public const string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the MS_AccountsView class.
	/// </summary>
	[DataContract]
	public partial class MS_AccountsViewCollection : ReadOnlyList<MS_AccountsView, MS_AccountsViewCollection>
	{
		public static MS_AccountsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			MS_AccountsViewCollection result = new MS_AccountsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwMS_Accounts view.
	/// </summary>
	[DataContract]
	public partial class MS_AccountsView : ReadOnlyRecord<MS_AccountsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwMS_Accounts", TableType.Table, DataService.GetInstance("NxsFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

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

				TableSchema.TableColumn colvarCustomerId = new TableSchema.TableColumn(schema);
				colvarCustomerId.ColumnName = "CustomerId";
				colvarCustomerId.DataType = DbType.Int64;
				colvarCustomerId.MaxLength = 0;
				colvarCustomerId.AutoIncrement = false;
				colvarCustomerId.IsNullable = false;
				colvarCustomerId.IsPrimaryKey = false;
				colvarCustomerId.IsForeignKey = false;
				colvarCustomerId.IsReadOnly = false;
				colvarCustomerId.DefaultSetting = @"";
				colvarCustomerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerId);

				TableSchema.TableColumn colvarCsid = new TableSchema.TableColumn(schema);
				colvarCsid.ColumnName = "Csid";
				colvarCsid.DataType = DbType.AnsiString;
				colvarCsid.MaxLength = 15;
				colvarCsid.AutoIncrement = false;
				colvarCsid.IsNullable = true;
				colvarCsid.IsPrimaryKey = false;
				colvarCsid.IsForeignKey = false;
				colvarCsid.IsReadOnly = false;
				colvarCsid.DefaultSetting = @"";
				colvarCsid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCsid);

				BaseSchema = schema;
				DataService.Providers["NxsFundingProvider"].AddSchema("vwMS_Accounts",schema);
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
		public MS_AccountsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long AccountID {
			get { return GetColumnValue<long>(Columns.AccountID); }
			set { SetColumnValue(Columns.AccountID, value); }
		}
		[DataMember]
		public long CustomerMasterFileId {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileId); }
			set { SetColumnValue(Columns.CustomerMasterFileId, value); }
		}
		[DataMember]
		public long CustomerId {
			get { return GetColumnValue<long>(Columns.CustomerId); }
			set { SetColumnValue(Columns.CustomerId, value); }
		}
		[DataMember]
		public string Csid {
			get { return GetColumnValue<string>(Columns.Csid); }
			set { SetColumnValue(Columns.Csid, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return AccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CustomerMasterFileIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CustomerIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CsidColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string AccountID = @"AccountID";
			public const string CustomerMasterFileId = @"CustomerMasterFileId";
			public const string CustomerId = @"CustomerId";
			public const string Csid = @"Csid";
		}
		#endregion Columns Struct
	}
}
