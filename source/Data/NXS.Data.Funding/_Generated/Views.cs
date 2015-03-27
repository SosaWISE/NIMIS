﻿


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
				colvarAccountStatusNote.MaxLength = 1073741823;
				colvarAccountStatusNote.AutoIncrement = false;
				colvarAccountStatusNote.IsNullable = true;
				colvarAccountStatusNote.IsPrimaryKey = false;
				colvarAccountStatusNote.IsForeignKey = false;
				colvarAccountStatusNote.IsReadOnly = false;
				colvarAccountStatusNote.DefaultSetting = @"";
				colvarAccountStatusNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountStatusNote);

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
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ReturnAccountFundingStatusIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn AccountFundingShortDescColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn AccountStatusNoteColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[13]; }
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
			public const string FirstName = @"FirstName";
			public const string LastName = @"LastName";
			public const string ReturnAccountFundingStatusId = @"ReturnAccountFundingStatusId";
			public const string AccountFundingShortDesc = @"AccountFundingShortDesc";
			public const string AccountStatusNote = @"AccountStatusNote";
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
}