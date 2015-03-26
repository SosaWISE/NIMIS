


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
}
