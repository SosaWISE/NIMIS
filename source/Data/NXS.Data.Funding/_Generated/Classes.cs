


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

namespace NXS.Data.Funding
{
	/// <summary>
	/// Strongly-typed collection for the FE_AccountFundingStatus class.
	/// </summary>
	[DataContract]
	public partial class FE_AccountFundingStatusCollection : ActiveList<FE_AccountFundingStatus, FE_AccountFundingStatusCollection>
	{
		public static FE_AccountFundingStatusCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_AccountFundingStatusCollection result = new FE_AccountFundingStatusCollection();
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
			foreach (FE_AccountFundingStatus item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_AccountFundingStatus table.
	/// </summary>
	[DataContract]
	public partial class FE_AccountFundingStatus : ActiveRecord<FE_AccountFundingStatus>, INotifyPropertyChanged
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

		public FE_AccountFundingStatus()
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
				TableSchema.Table schema = new TableSchema.Table("FE_AccountFundingStatus", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAccountFundingStatusID = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusID.ColumnName = "AccountFundingStatusID";
				colvarAccountFundingStatusID.DataType = DbType.Int64;
				colvarAccountFundingStatusID.MaxLength = 0;
				colvarAccountFundingStatusID.AutoIncrement = true;
				colvarAccountFundingStatusID.IsNullable = false;
				colvarAccountFundingStatusID.IsPrimaryKey = true;
				colvarAccountFundingStatusID.IsForeignKey = false;
				colvarAccountFundingStatusID.IsReadOnly = false;
				colvarAccountFundingStatusID.DefaultSetting = @"";
				colvarAccountFundingStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingStatusID);

				TableSchema.TableColumn colvarAccountFundingStatusTypeId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusTypeId.ColumnName = "AccountFundingStatusTypeId";
				colvarAccountFundingStatusTypeId.DataType = DbType.Int32;
				colvarAccountFundingStatusTypeId.MaxLength = 0;
				colvarAccountFundingStatusTypeId.AutoIncrement = false;
				colvarAccountFundingStatusTypeId.IsNullable = false;
				colvarAccountFundingStatusTypeId.IsPrimaryKey = false;
				colvarAccountFundingStatusTypeId.IsForeignKey = true;
				colvarAccountFundingStatusTypeId.IsReadOnly = false;
				colvarAccountFundingStatusTypeId.DefaultSetting = @"";
				colvarAccountFundingStatusTypeId.ForeignKeyTableName = "FE_AccountFundingStatusTypes";
				schema.Columns.Add(colvarAccountFundingStatusTypeId);

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

				TableSchema.TableColumn colvarAccountStatusEventId = new TableSchema.TableColumn(schema);
				colvarAccountStatusEventId.ColumnName = "AccountStatusEventId";
				colvarAccountStatusEventId.DataType = DbType.Int64;
				colvarAccountStatusEventId.MaxLength = 0;
				colvarAccountStatusEventId.AutoIncrement = false;
				colvarAccountStatusEventId.IsNullable = true;
				colvarAccountStatusEventId.IsPrimaryKey = false;
				colvarAccountStatusEventId.IsForeignKey = false;
				colvarAccountStatusEventId.IsReadOnly = false;
				colvarAccountStatusEventId.DefaultSetting = @"";
				colvarAccountStatusEventId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountStatusEventId);

				TableSchema.TableColumn colvarPacketItemId = new TableSchema.TableColumn(schema);
				colvarPacketItemId.ColumnName = "PacketItemId";
				colvarPacketItemId.DataType = DbType.Int32;
				colvarPacketItemId.MaxLength = 0;
				colvarPacketItemId.AutoIncrement = false;
				colvarPacketItemId.IsNullable = true;
				colvarPacketItemId.IsPrimaryKey = false;
				colvarPacketItemId.IsForeignKey = false;
				colvarPacketItemId.IsReadOnly = false;
				colvarPacketItemId.DefaultSetting = @"";
				colvarPacketItemId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketItemId);

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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_AccountFundingStatus",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_AccountFundingStatus LoadFrom(FE_AccountFundingStatus item)
		{
			FE_AccountFundingStatus result = new FE_AccountFundingStatus();
			if (item.AccountFundingStatusID != default(long)) {
				result.LoadByKey(item.AccountFundingStatusID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AccountFundingStatusID {
			get { return GetColumnValue<long>(Columns.AccountFundingStatusID); }
			set {
				SetColumnValue(Columns.AccountFundingStatusID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusID));
			}
		}
		[DataMember]
		public int AccountFundingStatusTypeId {
			get { return GetColumnValue<int>(Columns.AccountFundingStatusTypeId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusTypeId));
			}
		}
		[DataMember]
		public int AccountId {
			get { return GetColumnValue<int>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public long? AccountStatusEventId {
			get { return GetColumnValue<long?>(Columns.AccountStatusEventId); }
			set {
				SetColumnValue(Columns.AccountStatusEventId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountStatusEventId));
			}
		}
		[DataMember]
		public int? PacketItemId {
			get { return GetColumnValue<int?>(Columns.PacketItemId); }
			set {
				SetColumnValue(Columns.PacketItemId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketItemId));
			}
		}
		[DataMember]
		public string AccountStatusNote {
			get { return GetColumnValue<string>(Columns.AccountStatusNote); }
			set {
				SetColumnValue(Columns.AccountStatusNote, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountStatusNote));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatusType _AccountFundingStatusType;
		//Relationship: FK_FE_AccountFundingStatus_FE_AccountFundingStatusTypes
		public FE_AccountFundingStatusType AccountFundingStatusType
		{
			get
			{
				if(_AccountFundingStatusType == null) {
					_AccountFundingStatusType = FE_AccountFundingStatusType.FetchByID(this.AccountFundingStatusTypeId);
				}
				return _AccountFundingStatusType;
			}
			set
			{
				SetColumnValue("AccountFundingStatusTypeId", value.AccountFundingStatusTypeID);
				_AccountFundingStatusType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AccountFundingStatusID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AccountFundingStatusIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountStatusEventIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PacketItemIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AccountStatusNoteColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AccountFundingStatusID = @"AccountFundingStatusID";
			public static readonly string AccountFundingStatusTypeId = @"AccountFundingStatusTypeId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string AccountStatusEventId = @"AccountStatusEventId";
			public static readonly string PacketItemId = @"PacketItemId";
			public static readonly string AccountStatusNote = @"AccountStatusNote";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AccountFundingStatusID; }
		}
		*/

		#region Foreign Collections

		private FE_FundingHistoryCollection _FE_FundingHistoriesCol;
		//Relationship: FK_FE_FundingHistory_FE_AccountFundingStatus
		public FE_FundingHistoryCollection FE_FundingHistoriesCol
		{
			get
			{
				if(_FE_FundingHistoriesCol == null) {
					_FE_FundingHistoriesCol = new FE_FundingHistoryCollection();
					_FE_FundingHistoriesCol.LoadAndCloseReader(FE_FundingHistory.Query()
						.WHERE(FE_FundingHistory.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_FundingHistoriesCol;
			}
		}

		private FE_PacketItemCollection _FE_PacketItemsCol;
		//Relationship: FK_FE_PacketItems_FE_AccountFundingStatus
		public FE_PacketItemCollection FE_PacketItemsCol
		{
			get
			{
				if(_FE_PacketItemsCol == null) {
					_FE_PacketItemsCol = new FE_PacketItemCollection();
					_FE_PacketItemsCol.LoadAndCloseReader(FE_PacketItem.Query()
						.WHERE(FE_PacketItem.Columns.ReturnAccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_PacketItemsCol;
			}
		}

		private FE_PurchasedAccountCollection _FE_PurchasedAccountsCol;
		//Relationship: FK_FE_PurchasedAccount_FE_AccountFundingStatus
		public FE_PurchasedAccountCollection FE_PurchasedAccountsCol
		{
			get
			{
				if(_FE_PurchasedAccountsCol == null) {
					_FE_PurchasedAccountsCol = new FE_PurchasedAccountCollection();
					_FE_PurchasedAccountsCol.LoadAndCloseReader(FE_PurchasedAccount.Query()
						.WHERE(FE_PurchasedAccount.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_PurchasedAccountsCol;
			}
		}

		private FE_RejectedAccountCollection _FE_RejectedAccountsCol;
		//Relationship: FK_FE_RejectedAccount_FE_AccountFundingStatus
		public FE_RejectedAccountCollection FE_RejectedAccountsCol
		{
			get
			{
				if(_FE_RejectedAccountsCol == null) {
					_FE_RejectedAccountsCol = new FE_RejectedAccountCollection();
					_FE_RejectedAccountsCol.LoadAndCloseReader(FE_RejectedAccount.Query()
						.WHERE(FE_RejectedAccount.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_RejectedAccountsCol;
			}
		}

		private FE_RejectionCollection _FE_RejectionsCol;
		//Relationship: FK_FE_Rejection_FE_AccountFundingStatus
		public FE_RejectionCollection FE_RejectionsCol
		{
			get
			{
				if(_FE_RejectionsCol == null) {
					_FE_RejectionsCol = new FE_RejectionCollection();
					_FE_RejectionsCol.LoadAndCloseReader(FE_Rejection.Query()
						.WHERE(FE_Rejection.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_RejectionsCol;
			}
		}

		private FE_ReplacedAccountCollection _FE_ReplacedAccountsCol;
		//Relationship: FK_FE_ReplacedAccount_FE_AccountFundingStatus
		public FE_ReplacedAccountCollection FE_ReplacedAccountsCol
		{
			get
			{
				if(_FE_ReplacedAccountsCol == null) {
					_FE_ReplacedAccountsCol = new FE_ReplacedAccountCollection();
					_FE_ReplacedAccountsCol.LoadAndCloseReader(FE_ReplacedAccount.Query()
						.WHERE(FE_ReplacedAccount.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_ReplacedAccountsCol;
			}
		}

		private FE_SubmittedToPurchaserAccountCollection _FE_SubmittedToPurchaserAccountsCol;
		//Relationship: FK_FE_SubmittedToPurchaserAccount_FE_AccountFundingStatus
		public FE_SubmittedToPurchaserAccountCollection FE_SubmittedToPurchaserAccountsCol
		{
			get
			{
				if(_FE_SubmittedToPurchaserAccountsCol == null) {
					_FE_SubmittedToPurchaserAccountsCol = new FE_SubmittedToPurchaserAccountCollection();
					_FE_SubmittedToPurchaserAccountsCol.LoadAndCloseReader(FE_SubmittedToPurchaserAccount.Query()
						.WHERE(FE_SubmittedToPurchaserAccount.Columns.AccountFundingStatusId, AccountFundingStatusID).ExecuteReader());
				}
				return _FE_SubmittedToPurchaserAccountsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_AccountFundingStatusType class.
	/// </summary>
	[DataContract]
	public partial class FE_AccountFundingStatusTypeCollection : ActiveList<FE_AccountFundingStatusType, FE_AccountFundingStatusTypeCollection>
	{
		public static FE_AccountFundingStatusTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_AccountFundingStatusTypeCollection result = new FE_AccountFundingStatusTypeCollection();
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
			foreach (FE_AccountFundingStatusType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_AccountFundingStatusTypes table.
	/// </summary>
	[DataContract]
	public partial class FE_AccountFundingStatusType : ActiveRecord<FE_AccountFundingStatusType>, INotifyPropertyChanged
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

		public FE_AccountFundingStatusType()
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
				TableSchema.Table schema = new TableSchema.Table("FE_AccountFundingStatusTypes", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAccountFundingStatusTypeID = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusTypeID.ColumnName = "AccountFundingStatusTypeID";
				colvarAccountFundingStatusTypeID.DataType = DbType.Int32;
				colvarAccountFundingStatusTypeID.MaxLength = 0;
				colvarAccountFundingStatusTypeID.AutoIncrement = true;
				colvarAccountFundingStatusTypeID.IsNullable = false;
				colvarAccountFundingStatusTypeID.IsPrimaryKey = true;
				colvarAccountFundingStatusTypeID.IsForeignKey = false;
				colvarAccountFundingStatusTypeID.IsReadOnly = false;
				colvarAccountFundingStatusTypeID.DefaultSetting = @"";
				colvarAccountFundingStatusTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingStatusTypeID);

				TableSchema.TableColumn colvarAccountFundingStatusName = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusName.ColumnName = "AccountFundingStatusName";
				colvarAccountFundingStatusName.DataType = DbType.String;
				colvarAccountFundingStatusName.MaxLength = 100;
				colvarAccountFundingStatusName.AutoIncrement = false;
				colvarAccountFundingStatusName.IsNullable = false;
				colvarAccountFundingStatusName.IsPrimaryKey = false;
				colvarAccountFundingStatusName.IsForeignKey = false;
				colvarAccountFundingStatusName.IsReadOnly = false;
				colvarAccountFundingStatusName.DefaultSetting = @"";
				colvarAccountFundingStatusName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingStatusName);

				TableSchema.TableColumn colvarIsAvailable = new TableSchema.TableColumn(schema);
				colvarIsAvailable.ColumnName = "IsAvailable";
				colvarIsAvailable.DataType = DbType.Boolean;
				colvarIsAvailable.MaxLength = 0;
				colvarIsAvailable.AutoIncrement = false;
				colvarIsAvailable.IsNullable = true;
				colvarIsAvailable.IsPrimaryKey = false;
				colvarIsAvailable.IsForeignKey = false;
				colvarIsAvailable.IsReadOnly = false;
				colvarIsAvailable.DefaultSetting = @"";
				colvarIsAvailable.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsAvailable);

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

				TableSchema.TableColumn colvarAccountFundingStatusDescription = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusDescription.ColumnName = "AccountFundingStatusDescription";
				colvarAccountFundingStatusDescription.DataType = DbType.String;
				colvarAccountFundingStatusDescription.MaxLength = 1073741823;
				colvarAccountFundingStatusDescription.AutoIncrement = false;
				colvarAccountFundingStatusDescription.IsNullable = true;
				colvarAccountFundingStatusDescription.IsPrimaryKey = false;
				colvarAccountFundingStatusDescription.IsForeignKey = false;
				colvarAccountFundingStatusDescription.IsReadOnly = false;
				colvarAccountFundingStatusDescription.DefaultSetting = @"";
				colvarAccountFundingStatusDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountFundingStatusDescription);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_AccountFundingStatusTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_AccountFundingStatusType LoadFrom(FE_AccountFundingStatusType item)
		{
			FE_AccountFundingStatusType result = new FE_AccountFundingStatusType();
			if (item.AccountFundingStatusTypeID != default(int)) {
				result.LoadByKey(item.AccountFundingStatusTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int AccountFundingStatusTypeID {
			get { return GetColumnValue<int>(Columns.AccountFundingStatusTypeID); }
			set {
				SetColumnValue(Columns.AccountFundingStatusTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusTypeID));
			}
		}
		[DataMember]
		public string AccountFundingStatusName {
			get { return GetColumnValue<string>(Columns.AccountFundingStatusName); }
			set {
				SetColumnValue(Columns.AccountFundingStatusName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusName));
			}
		}
		[DataMember]
		public bool? IsAvailable {
			get { return GetColumnValue<bool?>(Columns.IsAvailable); }
			set {
				SetColumnValue(Columns.IsAvailable, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsAvailable));
			}
		}
		[DataMember]
		public string AccountFundingShortDesc {
			get { return GetColumnValue<string>(Columns.AccountFundingShortDesc); }
			set {
				SetColumnValue(Columns.AccountFundingShortDesc, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingShortDesc));
			}
		}
		[DataMember]
		public string AccountFundingStatusDescription {
			get { return GetColumnValue<string>(Columns.AccountFundingStatusDescription); }
			set {
				SetColumnValue(Columns.AccountFundingStatusDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return AccountFundingStatusName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn AccountFundingStatusTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsAvailableColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountFundingShortDescColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusDescriptionColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AccountFundingStatusTypeID = @"AccountFundingStatusTypeID";
			public static readonly string AccountFundingStatusName = @"AccountFundingStatusName";
			public static readonly string IsAvailable = @"IsAvailable";
			public static readonly string AccountFundingShortDesc = @"AccountFundingShortDesc";
			public static readonly string AccountFundingStatusDescription = @"AccountFundingStatusDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AccountFundingStatusTypeID; }
		}
		*/

		#region Foreign Collections

		private FE_AccountFundingStatusCollection _FE_AccountFundingStatusesCol;
		//Relationship: FK_FE_AccountFundingStatus_FE_AccountFundingStatusTypes
		public FE_AccountFundingStatusCollection FE_AccountFundingStatusesCol
		{
			get
			{
				if(_FE_AccountFundingStatusesCol == null) {
					_FE_AccountFundingStatusesCol = new FE_AccountFundingStatusCollection();
					_FE_AccountFundingStatusesCol.LoadAndCloseReader(FE_AccountFundingStatus.Query()
						.WHERE(FE_AccountFundingStatus.Columns.AccountFundingStatusTypeId, AccountFundingStatusTypeID).ExecuteReader());
				}
				return _FE_AccountFundingStatusesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_BundleItem class.
	/// </summary>
	[DataContract]
	public partial class FE_BundleItemCollection : ActiveList<FE_BundleItem, FE_BundleItemCollection>
	{
		public static FE_BundleItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_BundleItemCollection result = new FE_BundleItemCollection();
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
			foreach (FE_BundleItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_BundleItems table.
	/// </summary>
	[DataContract]
	public partial class FE_BundleItem : ActiveRecord<FE_BundleItem>, INotifyPropertyChanged
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

		public FE_BundleItem()
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
				TableSchema.Table schema = new TableSchema.Table("FE_BundleItems", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarBundleItemID = new TableSchema.TableColumn(schema);
				colvarBundleItemID.ColumnName = "BundleItemID";
				colvarBundleItemID.DataType = DbType.Int32;
				colvarBundleItemID.MaxLength = 0;
				colvarBundleItemID.AutoIncrement = true;
				colvarBundleItemID.IsNullable = false;
				colvarBundleItemID.IsPrimaryKey = true;
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
				colvarBundleId.IsForeignKey = true;
				colvarBundleId.IsReadOnly = false;
				colvarBundleId.DefaultSetting = @"";
				colvarBundleId.ForeignKeyTableName = "FE_Bundles";
				schema.Columns.Add(colvarBundleId);

				TableSchema.TableColumn colvarPacketId = new TableSchema.TableColumn(schema);
				colvarPacketId.ColumnName = "PacketId";
				colvarPacketId.DataType = DbType.Int32;
				colvarPacketId.MaxLength = 0;
				colvarPacketId.AutoIncrement = false;
				colvarPacketId.IsNullable = false;
				colvarPacketId.IsPrimaryKey = false;
				colvarPacketId.IsForeignKey = true;
				colvarPacketId.IsReadOnly = false;
				colvarPacketId.DefaultSetting = @"";
				colvarPacketId.ForeignKeyTableName = "FE_Packets";
				schema.Columns.Add(colvarPacketId);

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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_BundleItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_BundleItem LoadFrom(FE_BundleItem item)
		{
			FE_BundleItem result = new FE_BundleItem();
			if (item.BundleItemID != default(int)) {
				result.LoadByKey(item.BundleItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int BundleItemID {
			get { return GetColumnValue<int>(Columns.BundleItemID); }
			set {
				SetColumnValue(Columns.BundleItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BundleItemID));
			}
		}
		[DataMember]
		public int BundleId {
			get { return GetColumnValue<int>(Columns.BundleId); }
			set {
				SetColumnValue(Columns.BundleId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BundleId));
			}
		}
		[DataMember]
		public int PacketId {
			get { return GetColumnValue<int>(Columns.PacketId); }
			set {
				SetColumnValue(Columns.PacketId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Bundle _Bundle;
		//Relationship: FK_FE_BundleItems_FE_Bundles
		public FE_Bundle Bundle
		{
			get
			{
				if(_Bundle == null) {
					_Bundle = FE_Bundle.FetchByID(this.BundleId);
				}
				return _Bundle;
			}
			set
			{
				SetColumnValue("BundleId", value.BundleID);
				_Bundle = value;
			}
		}

		private FE_Packet _Packet;
		//Relationship: FK_FE_BundleItems_FE_Packets
		public FE_Packet Packet
		{
			get
			{
				if(_Packet == null) {
					_Packet = FE_Packet.FetchByID(this.PacketId);
				}
				return _Packet;
			}
			set
			{
				SetColumnValue("PacketId", value.PacketID);
				_Packet = value;
			}
		}

		#endregion //ForeignKey Properties

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
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string BundleItemID = @"BundleItemID";
			public static readonly string BundleId = @"BundleId";
			public static readonly string PacketId = @"PacketId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return BundleItemID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_Bundle class.
	/// </summary>
	[DataContract]
	public partial class FE_BundleCollection : ActiveList<FE_Bundle, FE_BundleCollection>
	{
		public static FE_BundleCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_BundleCollection result = new FE_BundleCollection();
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
			foreach (FE_Bundle item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_Bundles table.
	/// </summary>
	[DataContract]
	public partial class FE_Bundle : ActiveRecord<FE_Bundle>, INotifyPropertyChanged
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

		public FE_Bundle()
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
				TableSchema.Table schema = new TableSchema.Table("FE_Bundles", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarBundleID = new TableSchema.TableColumn(schema);
				colvarBundleID.ColumnName = "BundleID";
				colvarBundleID.DataType = DbType.Int32;
				colvarBundleID.MaxLength = 0;
				colvarBundleID.AutoIncrement = true;
				colvarBundleID.IsNullable = false;
				colvarBundleID.IsPrimaryKey = true;
				colvarBundleID.IsForeignKey = false;
				colvarBundleID.IsReadOnly = false;
				colvarBundleID.DefaultSetting = @"";
				colvarBundleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBundleID);

				TableSchema.TableColumn colvarPurchaserId = new TableSchema.TableColumn(schema);
				colvarPurchaserId.ColumnName = "PurchaserId";
				colvarPurchaserId.DataType = DbType.AnsiString;
				colvarPurchaserId.MaxLength = 10;
				colvarPurchaserId.AutoIncrement = false;
				colvarPurchaserId.IsNullable = true;
				colvarPurchaserId.IsPrimaryKey = false;
				colvarPurchaserId.IsForeignKey = true;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "FE_Purchasers";
				schema.Columns.Add(colvarPurchaserId);

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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_Bundles",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_Bundle LoadFrom(FE_Bundle item)
		{
			FE_Bundle result = new FE_Bundle();
			if (item.BundleID != default(int)) {
				result.LoadByKey(item.BundleID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int BundleID {
			get { return GetColumnValue<int>(Columns.BundleID); }
			set {
				SetColumnValue(Columns.BundleID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BundleID));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public DateTime? SubmittedOn {
			get { return GetColumnValue<DateTime?>(Columns.SubmittedOn); }
			set {
				SetColumnValue(Columns.SubmittedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmittedOn));
			}
		}
		[DataMember]
		public string SubmittedBy {
			get { return GetColumnValue<string>(Columns.SubmittedBy); }
			set {
				SetColumnValue(Columns.SubmittedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmittedBy));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Purchaser _Purchaser;
		//Relationship: FK_FE_Bundles_FE_Purchasers
		public FE_Purchaser Purchaser
		{
			get
			{
				if(_Purchaser == null) {
					_Purchaser = FE_Purchaser.FetchByID(this.PurchaserId);
				}
				return _Purchaser;
			}
			set
			{
				SetColumnValue("PurchaserId", value.PurchaserID);
				_Purchaser = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaserId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn BundleIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SubmittedOnColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SubmittedByColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string BundleID = @"BundleID";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string SubmittedOn = @"SubmittedOn";
			public static readonly string SubmittedBy = @"SubmittedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return BundleID; }
		}
		*/

		#region Foreign Collections

		private FE_BundleItemCollection _FE_BundleItemsCol;
		//Relationship: FK_FE_BundleItems_FE_Bundles
		public FE_BundleItemCollection FE_BundleItemsCol
		{
			get
			{
				if(_FE_BundleItemsCol == null) {
					_FE_BundleItemsCol = new FE_BundleItemCollection();
					_FE_BundleItemsCol.LoadAndCloseReader(FE_BundleItem.Query()
						.WHERE(FE_BundleItem.Columns.BundleId, BundleID).ExecuteReader());
				}
				return _FE_BundleItemsCol;
			}
		}

		private FE_TrackingNumberCollection _FE_TrackingNumbersCol;
		//Relationship: FK_FE_TrackingNumber_FE_Bundles
		public FE_TrackingNumberCollection FE_TrackingNumbersCol
		{
			get
			{
				if(_FE_TrackingNumbersCol == null) {
					_FE_TrackingNumbersCol = new FE_TrackingNumberCollection();
					_FE_TrackingNumbersCol.LoadAndCloseReader(FE_TrackingNumber.Query()
						.WHERE(FE_TrackingNumber.Columns.BundleId, BundleID).ExecuteReader());
				}
				return _FE_TrackingNumbersCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_CriteriaItem class.
	/// </summary>
	[DataContract]
	public partial class FE_CriteriaItemCollection : ActiveList<FE_CriteriaItem, FE_CriteriaItemCollection>
	{
		public static FE_CriteriaItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_CriteriaItemCollection result = new FE_CriteriaItemCollection();
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
			foreach (FE_CriteriaItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_CriteriaItems table.
	/// </summary>
	[DataContract]
	public partial class FE_CriteriaItem : ActiveRecord<FE_CriteriaItem>, INotifyPropertyChanged
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

		public FE_CriteriaItem()
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
				TableSchema.Table schema = new TableSchema.Table("FE_CriteriaItems", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCriteraItemID = new TableSchema.TableColumn(schema);
				colvarCriteraItemID.ColumnName = "CriteraItemID";
				colvarCriteraItemID.DataType = DbType.Int32;
				colvarCriteraItemID.MaxLength = 0;
				colvarCriteraItemID.AutoIncrement = true;
				colvarCriteraItemID.IsNullable = false;
				colvarCriteraItemID.IsPrimaryKey = true;
				colvarCriteraItemID.IsForeignKey = false;
				colvarCriteraItemID.IsReadOnly = false;
				colvarCriteraItemID.DefaultSetting = @"";
				colvarCriteraItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCriteraItemID);

				TableSchema.TableColumn colvarCriteriaId = new TableSchema.TableColumn(schema);
				colvarCriteriaId.ColumnName = "CriteriaId";
				colvarCriteriaId.DataType = DbType.Int32;
				colvarCriteriaId.MaxLength = 0;
				colvarCriteriaId.AutoIncrement = false;
				colvarCriteriaId.IsNullable = false;
				colvarCriteriaId.IsPrimaryKey = false;
				colvarCriteriaId.IsForeignKey = true;
				colvarCriteriaId.IsReadOnly = false;
				colvarCriteriaId.DefaultSetting = @"";
				colvarCriteriaId.ForeignKeyTableName = "FE_Criterias";
				schema.Columns.Add(colvarCriteriaId);

				TableSchema.TableColumn colvarColumnName = new TableSchema.TableColumn(schema);
				colvarColumnName.ColumnName = "ColumnName";
				colvarColumnName.DataType = DbType.String;
				colvarColumnName.MaxLength = 50;
				colvarColumnName.AutoIncrement = false;
				colvarColumnName.IsNullable = false;
				colvarColumnName.IsPrimaryKey = false;
				colvarColumnName.IsForeignKey = false;
				colvarColumnName.IsReadOnly = false;
				colvarColumnName.DefaultSetting = @"";
				colvarColumnName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarColumnName);

				TableSchema.TableColumn colvarValueX = new TableSchema.TableColumn(schema);
				colvarValueX.ColumnName = "Value";
				colvarValueX.DataType = DbType.String;
				colvarValueX.MaxLength = 50;
				colvarValueX.AutoIncrement = false;
				colvarValueX.IsNullable = false;
				colvarValueX.IsPrimaryKey = false;
				colvarValueX.IsForeignKey = false;
				colvarValueX.IsReadOnly = false;
				colvarValueX.DefaultSetting = @"";
				colvarValueX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValueX);

				TableSchema.TableColumn colvarOperatorX = new TableSchema.TableColumn(schema);
				colvarOperatorX.ColumnName = "Operator";
				colvarOperatorX.DataType = DbType.String;
				colvarOperatorX.MaxLength = 50;
				colvarOperatorX.AutoIncrement = false;
				colvarOperatorX.IsNullable = false;
				colvarOperatorX.IsPrimaryKey = false;
				colvarOperatorX.IsForeignKey = false;
				colvarOperatorX.IsReadOnly = false;
				colvarOperatorX.DefaultSetting = @"";
				colvarOperatorX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOperatorX);

				TableSchema.TableColumn colvarNotMetMessage = new TableSchema.TableColumn(schema);
				colvarNotMetMessage.ColumnName = "NotMetMessage";
				colvarNotMetMessage.DataType = DbType.String;
				colvarNotMetMessage.MaxLength = 50;
				colvarNotMetMessage.AutoIncrement = false;
				colvarNotMetMessage.IsNullable = true;
				colvarNotMetMessage.IsPrimaryKey = false;
				colvarNotMetMessage.IsForeignKey = false;
				colvarNotMetMessage.IsReadOnly = false;
				colvarNotMetMessage.DefaultSetting = @"";
				colvarNotMetMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNotMetMessage);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_CriteriaItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_CriteriaItem LoadFrom(FE_CriteriaItem item)
		{
			FE_CriteriaItem result = new FE_CriteriaItem();
			if (item.CriteraItemID != default(int)) {
				result.LoadByKey(item.CriteraItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int CriteraItemID {
			get { return GetColumnValue<int>(Columns.CriteraItemID); }
			set {
				SetColumnValue(Columns.CriteraItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CriteraItemID));
			}
		}
		[DataMember]
		public int CriteriaId {
			get { return GetColumnValue<int>(Columns.CriteriaId); }
			set {
				SetColumnValue(Columns.CriteriaId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CriteriaId));
			}
		}
		[DataMember]
		public string ColumnName {
			get { return GetColumnValue<string>(Columns.ColumnName); }
			set {
				SetColumnValue(Columns.ColumnName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ColumnName));
			}
		}
		[DataMember]
		public string ValueX {
			get { return GetColumnValue<string>(Columns.ValueX); }
			set {
				SetColumnValue(Columns.ValueX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ValueX));
			}
		}
		[DataMember]
		public string OperatorX {
			get { return GetColumnValue<string>(Columns.OperatorX); }
			set {
				SetColumnValue(Columns.OperatorX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OperatorX));
			}
		}
		[DataMember]
		public string NotMetMessage {
			get { return GetColumnValue<string>(Columns.NotMetMessage); }
			set {
				SetColumnValue(Columns.NotMetMessage, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NotMetMessage));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Criteria _Criteria;
		//Relationship: FK_FE_CriteriaItems_FE_Criteria
		public FE_Criteria Criteria
		{
			get
			{
				if(_Criteria == null) {
					_Criteria = FE_Criteria.FetchByID(this.CriteriaId);
				}
				return _Criteria;
			}
			set
			{
				SetColumnValue("CriteriaId", value.CriteriaID);
				_Criteria = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return CriteraItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CriteraItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CriteriaIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ColumnNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ValueXColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn OperatorXColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn NotMetMessageColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string CriteraItemID = @"CriteraItemID";
			public static readonly string CriteriaId = @"CriteriaId";
			public static readonly string ColumnName = @"ColumnName";
			public static readonly string ValueX = @"Value";
			public static readonly string OperatorX = @"Operator";
			public static readonly string NotMetMessage = @"NotMetMessage";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CriteraItemID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_Criteria class.
	/// </summary>
	[DataContract]
	public partial class FE_CriteriaCollection : ActiveList<FE_Criteria, FE_CriteriaCollection>
	{
		public static FE_CriteriaCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_CriteriaCollection result = new FE_CriteriaCollection();
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
			foreach (FE_Criteria item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_Criterias table.
	/// </summary>
	[DataContract]
	public partial class FE_Criteria : ActiveRecord<FE_Criteria>, INotifyPropertyChanged
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

		public FE_Criteria()
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
				TableSchema.Table schema = new TableSchema.Table("FE_Criterias", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCriteriaID = new TableSchema.TableColumn(schema);
				colvarCriteriaID.ColumnName = "CriteriaID";
				colvarCriteriaID.DataType = DbType.Int32;
				colvarCriteriaID.MaxLength = 0;
				colvarCriteriaID.AutoIncrement = true;
				colvarCriteriaID.IsNullable = false;
				colvarCriteriaID.IsPrimaryKey = true;
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
				colvarPurchaserId.IsForeignKey = true;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "FE_Purchasers";
				schema.Columns.Add(colvarPurchaserId);

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

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 20;
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_Criterias",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_Criteria LoadFrom(FE_Criteria item)
		{
			FE_Criteria result = new FE_Criteria();
			if (item.CriteriaID != default(int)) {
				result.LoadByKey(item.CriteriaID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int CriteriaID {
			get { return GetColumnValue<int>(Columns.CriteriaID); }
			set {
				SetColumnValue(Columns.CriteriaID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CriteriaID));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public string CriteriaName {
			get { return GetColumnValue<string>(Columns.CriteriaName); }
			set {
				SetColumnValue(Columns.CriteriaName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CriteriaName));
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
		public string FilterString {
			get { return GetColumnValue<string>(Columns.FilterString); }
			set {
				SetColumnValue(Columns.FilterString, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FilterString));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Purchaser _Purchaser;
		//Relationship: FK_FE_Criteria_FE_Purchasers
		public FE_Purchaser Purchaser
		{
			get
			{
				if(_Purchaser == null) {
					_Purchaser = FE_Purchaser.FetchByID(this.PurchaserId);
				}
				return _Purchaser;
			}
			set
			{
				SetColumnValue("PurchaserId", value.PurchaserID);
				_Purchaser = value;
			}
		}

		#endregion //ForeignKey Properties

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
		public static TableSchema.TableColumn CriteriaNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn FilterStringColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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
			public static readonly string CriteriaID = @"CriteriaID";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string CriteriaName = @"CriteriaName";
			public static readonly string Description = @"Description";
			public static readonly string FilterString = @"FilterString";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return CriteriaID; }
		}
		*/

		#region Foreign Collections

		private FE_CriteriaItemCollection _FE_CriteriaItemsCol;
		//Relationship: FK_FE_CriteriaItems_FE_Criteria
		public FE_CriteriaItemCollection FE_CriteriaItemsCol
		{
			get
			{
				if(_FE_CriteriaItemsCol == null) {
					_FE_CriteriaItemsCol = new FE_CriteriaItemCollection();
					_FE_CriteriaItemsCol.LoadAndCloseReader(FE_CriteriaItem.Query()
						.WHERE(FE_CriteriaItem.Columns.CriteriaId, CriteriaID).ExecuteReader());
				}
				return _FE_CriteriaItemsCol;
			}
		}

		private FE_PacketCollection _FE_PacketsCol;
		//Relationship: FK_FE_Packets_FE_Criteria
		public FE_PacketCollection FE_PacketsCol
		{
			get
			{
				if(_FE_PacketsCol == null) {
					_FE_PacketsCol = new FE_PacketCollection();
					_FE_PacketsCol.LoadAndCloseReader(FE_Packet.Query()
						.WHERE(FE_Packet.Columns.CriteriaId, CriteriaID).ExecuteReader());
				}
				return _FE_PacketsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_FundingHistory class.
	/// </summary>
	[DataContract]
	public partial class FE_FundingHistoryCollection : ActiveList<FE_FundingHistory, FE_FundingHistoryCollection>
	{
		public static FE_FundingHistoryCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_FundingHistoryCollection result = new FE_FundingHistoryCollection();
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
			foreach (FE_FundingHistory item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_FundingHistory table.
	/// </summary>
	[DataContract]
	public partial class FE_FundingHistory : ActiveRecord<FE_FundingHistory>, INotifyPropertyChanged
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

		public FE_FundingHistory()
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
				TableSchema.Table schema = new TableSchema.Table("FE_FundingHistory", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeclinedAccountID = new TableSchema.TableColumn(schema);
				colvarDeclinedAccountID.ColumnName = "DeclinedAccountID";
				colvarDeclinedAccountID.DataType = DbType.Int32;
				colvarDeclinedAccountID.MaxLength = 0;
				colvarDeclinedAccountID.AutoIncrement = true;
				colvarDeclinedAccountID.IsNullable = false;
				colvarDeclinedAccountID.IsPrimaryKey = true;
				colvarDeclinedAccountID.IsForeignKey = false;
				colvarDeclinedAccountID.IsReadOnly = false;
				colvarDeclinedAccountID.DefaultSetting = @"";
				colvarDeclinedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeclinedAccountID);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = true;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
				schema.Columns.Add(colvarAccountFundingStatusId);

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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_FundingHistory",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_FundingHistory LoadFrom(FE_FundingHistory item)
		{
			FE_FundingHistory result = new FE_FundingHistory();
			if (item.DeclinedAccountID != default(int)) {
				result.LoadByKey(item.DeclinedAccountID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int DeclinedAccountID {
			get { return GetColumnValue<int>(Columns.DeclinedAccountID); }
			set {
				SetColumnValue(Columns.DeclinedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeclinedAccountID));
			}
		}
		[DataMember]
		public long? AccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
			}
		}
		[DataMember]
		public int AccountId {
			get { return GetColumnValue<int>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_FundingHistory_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return DeclinedAccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeclinedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeclinedAccountID = @"DeclinedAccountID";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return DeclinedAccountID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_PacketItem class.
	/// </summary>
	[DataContract]
	public partial class FE_PacketItemCollection : ActiveList<FE_PacketItem, FE_PacketItemCollection>
	{
		public static FE_PacketItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PacketItemCollection result = new FE_PacketItemCollection();
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
			foreach (FE_PacketItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_PacketItems table.
	/// </summary>
	[DataContract]
	public partial class FE_PacketItem : ActiveRecord<FE_PacketItem>, INotifyPropertyChanged
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

		public FE_PacketItem()
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
				TableSchema.Table schema = new TableSchema.Table("FE_PacketItems", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPacketItemID = new TableSchema.TableColumn(schema);
				colvarPacketItemID.ColumnName = "PacketItemID";
				colvarPacketItemID.DataType = DbType.Int64;
				colvarPacketItemID.MaxLength = 0;
				colvarPacketItemID.AutoIncrement = true;
				colvarPacketItemID.IsNullable = false;
				colvarPacketItemID.IsPrimaryKey = true;
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
				colvarPacketId.IsForeignKey = true;
				colvarPacketId.IsReadOnly = false;
				colvarPacketId.DefaultSetting = @"";
				colvarPacketId.ForeignKeyTableName = "FE_Packets";
				schema.Columns.Add(colvarPacketId);

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

				TableSchema.TableColumn colvarReturnAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarReturnAccountFundingStatusId.ColumnName = "ReturnAccountFundingStatusId";
				colvarReturnAccountFundingStatusId.DataType = DbType.Int64;
				colvarReturnAccountFundingStatusId.MaxLength = 0;
				colvarReturnAccountFundingStatusId.AutoIncrement = false;
				colvarReturnAccountFundingStatusId.IsNullable = true;
				colvarReturnAccountFundingStatusId.IsPrimaryKey = false;
				colvarReturnAccountFundingStatusId.IsForeignKey = true;
				colvarReturnAccountFundingStatusId.IsReadOnly = false;
				colvarReturnAccountFundingStatusId.DefaultSetting = @"";
				colvarReturnAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
				schema.Columns.Add(colvarReturnAccountFundingStatusId);

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
				colvarModifiedOn.DefaultSetting = @"(getutcdate())";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_PacketItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_PacketItem LoadFrom(FE_PacketItem item)
		{
			FE_PacketItem result = new FE_PacketItem();
			if (item.PacketItemID != default(long)) {
				result.LoadByKey(item.PacketItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long PacketItemID {
			get { return GetColumnValue<long>(Columns.PacketItemID); }
			set {
				SetColumnValue(Columns.PacketItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketItemID));
			}
		}
		[DataMember]
		public int PacketId {
			get { return GetColumnValue<int>(Columns.PacketId); }
			set {
				SetColumnValue(Columns.PacketId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketId));
			}
		}
		[DataMember]
		public int AccountId {
			get { return GetColumnValue<int>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public long? ReturnAccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.ReturnAccountFundingStatusId); }
			set {
				SetColumnValue(Columns.ReturnAccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnAccountFundingStatusId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _ReturnAccountFundingStatus;
		//Relationship: FK_FE_PacketItems_FE_AccountFundingStatus
		public FE_AccountFundingStatus ReturnAccountFundingStatus
		{
			get
			{
				if(_ReturnAccountFundingStatus == null) {
					_ReturnAccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.ReturnAccountFundingStatusId);
				}
				return _ReturnAccountFundingStatus;
			}
			set
			{
				SetColumnValue("ReturnAccountFundingStatusId", value.AccountFundingStatusID);
				_ReturnAccountFundingStatus = value;
			}
		}

		private FE_Packet _Packet;
		//Relationship: FK_FE_PacketItems_FE_Packets
		public FE_Packet Packet
		{
			get
			{
				if(_Packet == null) {
					_Packet = FE_Packet.FetchByID(this.PacketId);
				}
				return _Packet;
			}
			set
			{
				SetColumnValue("PacketId", value.PacketID);
				_Packet = value;
			}
		}

		#endregion //ForeignKey Properties

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
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ReturnAccountFundingStatusIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
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
			public static readonly string PacketItemID = @"PacketItemID";
			public static readonly string PacketId = @"PacketId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string ReturnAccountFundingStatusId = @"ReturnAccountFundingStatusId";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PacketItemID; }
		}
		*/

		#region Foreign Collections

		private FE_RejectedAccountCollection _FE_RejectedAccountsCol;
		//Relationship: FK_FE_RejectedAccount_FE_PacketItems
		public FE_RejectedAccountCollection FE_RejectedAccountsCol
		{
			get
			{
				if(_FE_RejectedAccountsCol == null) {
					_FE_RejectedAccountsCol = new FE_RejectedAccountCollection();
					_FE_RejectedAccountsCol.LoadAndCloseReader(FE_RejectedAccount.Query()
						.WHERE(FE_RejectedAccount.Columns.PacketItemId, PacketItemID).ExecuteReader());
				}
				return _FE_RejectedAccountsCol;
			}
		}

		private FE_RejectionCollection _FE_RejectionsCol;
		//Relationship: FK_FE_Rejection_FE_PacketItems
		public FE_RejectionCollection FE_RejectionsCol
		{
			get
			{
				if(_FE_RejectionsCol == null) {
					_FE_RejectionsCol = new FE_RejectionCollection();
					_FE_RejectionsCol.LoadAndCloseReader(FE_Rejection.Query()
						.WHERE(FE_Rejection.Columns.PacketItemId, PacketItemID).ExecuteReader());
				}
				return _FE_RejectionsCol;
			}
		}

		private FE_SubmittedToPurchaserAccountCollection _FE_SubmittedToPurchaserAccountsCol;
		//Relationship: FK_FE_SubmittedToPurchaserAccount_FE_PacketItems
		public FE_SubmittedToPurchaserAccountCollection FE_SubmittedToPurchaserAccountsCol
		{
			get
			{
				if(_FE_SubmittedToPurchaserAccountsCol == null) {
					_FE_SubmittedToPurchaserAccountsCol = new FE_SubmittedToPurchaserAccountCollection();
					_FE_SubmittedToPurchaserAccountsCol.LoadAndCloseReader(FE_SubmittedToPurchaserAccount.Query()
						.WHERE(FE_SubmittedToPurchaserAccount.Columns.PacketItemId, PacketItemID).ExecuteReader());
				}
				return _FE_SubmittedToPurchaserAccountsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_Packet class.
	/// </summary>
	[DataContract]
	public partial class FE_PacketCollection : ActiveList<FE_Packet, FE_PacketCollection>
	{
		public static FE_PacketCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PacketCollection result = new FE_PacketCollection();
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
			foreach (FE_Packet item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_Packets table.
	/// </summary>
	[DataContract]
	public partial class FE_Packet : ActiveRecord<FE_Packet>, INotifyPropertyChanged
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

		public FE_Packet()
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
				TableSchema.Table schema = new TableSchema.Table("FE_Packets", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPacketID = new TableSchema.TableColumn(schema);
				colvarPacketID.ColumnName = "PacketID";
				colvarPacketID.DataType = DbType.Int32;
				colvarPacketID.MaxLength = 0;
				colvarPacketID.AutoIncrement = true;
				colvarPacketID.IsNullable = false;
				colvarPacketID.IsPrimaryKey = true;
				colvarPacketID.IsForeignKey = false;
				colvarPacketID.IsReadOnly = false;
				colvarPacketID.DefaultSetting = @"";
				colvarPacketID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPacketID);

				TableSchema.TableColumn colvarCriteriaId = new TableSchema.TableColumn(schema);
				colvarCriteriaId.ColumnName = "CriteriaId";
				colvarCriteriaId.DataType = DbType.Int32;
				colvarCriteriaId.MaxLength = 0;
				colvarCriteriaId.AutoIncrement = false;
				colvarCriteriaId.IsNullable = true;
				colvarCriteriaId.IsPrimaryKey = false;
				colvarCriteriaId.IsForeignKey = true;
				colvarCriteriaId.IsReadOnly = false;
				colvarCriteriaId.DefaultSetting = @"";
				colvarCriteriaId.ForeignKeyTableName = "FE_Criterias";
				schema.Columns.Add(colvarCriteriaId);

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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_Packets",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_Packet LoadFrom(FE_Packet item)
		{
			FE_Packet result = new FE_Packet();
			if (item.PacketID != default(int)) {
				result.LoadByKey(item.PacketID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PacketID {
			get { return GetColumnValue<int>(Columns.PacketID); }
			set {
				SetColumnValue(Columns.PacketID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketID));
			}
		}
		[DataMember]
		public int? CriteriaId {
			get { return GetColumnValue<int?>(Columns.CriteriaId); }
			set {
				SetColumnValue(Columns.CriteriaId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CriteriaId));
			}
		}
		[DataMember]
		public DateTime? SubmittedOn {
			get { return GetColumnValue<DateTime?>(Columns.SubmittedOn); }
			set {
				SetColumnValue(Columns.SubmittedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmittedOn));
			}
		}
		[DataMember]
		public string SubmittedBy {
			get { return GetColumnValue<string>(Columns.SubmittedBy); }
			set {
				SetColumnValue(Columns.SubmittedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmittedBy));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Criteria _Criteria;
		//Relationship: FK_FE_Packets_FE_Criteria
		public FE_Criteria Criteria
		{
			get
			{
				if(_Criteria == null) {
					_Criteria = FE_Criteria.FetchByID(this.CriteriaId);
				}
				return _Criteria;
			}
			set
			{
				SetColumnValue("CriteriaId", value.CriteriaID);
				_Criteria = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PacketID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PacketIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CriteriaIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SubmittedOnColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SubmittedByColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PacketID = @"PacketID";
			public static readonly string CriteriaId = @"CriteriaId";
			public static readonly string SubmittedOn = @"SubmittedOn";
			public static readonly string SubmittedBy = @"SubmittedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PacketID; }
		}
		*/

		#region Foreign Collections

		private FE_BundleItemCollection _FE_BundleItemsCol;
		//Relationship: FK_FE_BundleItems_FE_Packets
		public FE_BundleItemCollection FE_BundleItemsCol
		{
			get
			{
				if(_FE_BundleItemsCol == null) {
					_FE_BundleItemsCol = new FE_BundleItemCollection();
					_FE_BundleItemsCol.LoadAndCloseReader(FE_BundleItem.Query()
						.WHERE(FE_BundleItem.Columns.PacketId, PacketID).ExecuteReader());
				}
				return _FE_BundleItemsCol;
			}
		}

		private FE_PacketItemCollection _FE_PacketItemsCol;
		//Relationship: FK_FE_PacketItems_FE_Packets
		public FE_PacketItemCollection FE_PacketItemsCol
		{
			get
			{
				if(_FE_PacketItemsCol == null) {
					_FE_PacketItemsCol = new FE_PacketItemCollection();
					_FE_PacketItemsCol.LoadAndCloseReader(FE_PacketItem.Query()
						.WHERE(FE_PacketItem.Columns.PacketId, PacketID).ExecuteReader());
				}
				return _FE_PacketItemsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_PurchaseContractMonitronic class.
	/// </summary>
	[DataContract]
	public partial class FE_PurchaseContractMonitronicCollection : ActiveList<FE_PurchaseContractMonitronic, FE_PurchaseContractMonitronicCollection>
	{
		public static FE_PurchaseContractMonitronicCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PurchaseContractMonitronicCollection result = new FE_PurchaseContractMonitronicCollection();
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
			foreach (FE_PurchaseContractMonitronic item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_PurchaseContractMonitronics table.
	/// </summary>
	[DataContract]
	public partial class FE_PurchaseContractMonitronic : ActiveRecord<FE_PurchaseContractMonitronic>, INotifyPropertyChanged
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

		public FE_PurchaseContractMonitronic()
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
				TableSchema.Table schema = new TableSchema.Table("FE_PurchaseContractMonitronics", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchaseContractMonitronicsID = new TableSchema.TableColumn(schema);
				colvarPurchaseContractMonitronicsID.ColumnName = "PurchaseContractMonitronicsID";
				colvarPurchaseContractMonitronicsID.DataType = DbType.Int32;
				colvarPurchaseContractMonitronicsID.MaxLength = 0;
				colvarPurchaseContractMonitronicsID.AutoIncrement = true;
				colvarPurchaseContractMonitronicsID.IsNullable = false;
				colvarPurchaseContractMonitronicsID.IsPrimaryKey = true;
				colvarPurchaseContractMonitronicsID.IsForeignKey = false;
				colvarPurchaseContractMonitronicsID.IsReadOnly = false;
				colvarPurchaseContractMonitronicsID.DefaultSetting = @"";
				colvarPurchaseContractMonitronicsID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseContractMonitronicsID);

				TableSchema.TableColumn colvarPurchaseContractId = new TableSchema.TableColumn(schema);
				colvarPurchaseContractId.ColumnName = "PurchaseContractId";
				colvarPurchaseContractId.DataType = DbType.Int32;
				colvarPurchaseContractId.MaxLength = 0;
				colvarPurchaseContractId.AutoIncrement = false;
				colvarPurchaseContractId.IsNullable = false;
				colvarPurchaseContractId.IsPrimaryKey = false;
				colvarPurchaseContractId.IsForeignKey = true;
				colvarPurchaseContractId.IsReadOnly = false;
				colvarPurchaseContractId.DefaultSetting = @"";
				colvarPurchaseContractId.ForeignKeyTableName = "FE_PurchaseContracts";
				schema.Columns.Add(colvarPurchaseContractId);

				TableSchema.TableColumn colvarPPMonitronicsId = new TableSchema.TableColumn(schema);
				colvarPPMonitronicsId.ColumnName = "PPMonitronicsId";
				colvarPPMonitronicsId.DataType = DbType.Int32;
				colvarPPMonitronicsId.MaxLength = 0;
				colvarPPMonitronicsId.AutoIncrement = false;
				colvarPPMonitronicsId.IsNullable = true;
				colvarPPMonitronicsId.IsPrimaryKey = false;
				colvarPPMonitronicsId.IsForeignKey = false;
				colvarPPMonitronicsId.IsReadOnly = false;
				colvarPPMonitronicsId.DefaultSetting = @"";
				colvarPPMonitronicsId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPPMonitronicsId);

				TableSchema.TableColumn colvarPPContractContact = new TableSchema.TableColumn(schema);
				colvarPPContractContact.ColumnName = "PPContractContact";
				colvarPPContractContact.DataType = DbType.String;
				colvarPPContractContact.MaxLength = 50;
				colvarPPContractContact.AutoIncrement = false;
				colvarPPContractContact.IsNullable = true;
				colvarPPContractContact.IsPrimaryKey = false;
				colvarPPContractContact.IsForeignKey = false;
				colvarPPContractContact.IsReadOnly = false;
				colvarPPContractContact.DefaultSetting = @"";
				colvarPPContractContact.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPPContractContact);

				TableSchema.TableColumn colvarPPContractEntity = new TableSchema.TableColumn(schema);
				colvarPPContractEntity.ColumnName = "PPContractEntity";
				colvarPPContractEntity.DataType = DbType.String;
				colvarPPContractEntity.MaxLength = 50;
				colvarPPContractEntity.AutoIncrement = false;
				colvarPPContractEntity.IsNullable = true;
				colvarPPContractEntity.IsPrimaryKey = false;
				colvarPPContractEntity.IsForeignKey = false;
				colvarPPContractEntity.IsReadOnly = false;
				colvarPPContractEntity.DefaultSetting = @"";
				colvarPPContractEntity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPPContractEntity);

				TableSchema.TableColumn colvarPPContractPhone = new TableSchema.TableColumn(schema);
				colvarPPContractPhone.ColumnName = "PPContractPhone";
				colvarPPContractPhone.DataType = DbType.String;
				colvarPPContractPhone.MaxLength = 15;
				colvarPPContractPhone.AutoIncrement = false;
				colvarPPContractPhone.IsNullable = true;
				colvarPPContractPhone.IsPrimaryKey = false;
				colvarPPContractPhone.IsForeignKey = false;
				colvarPPContractPhone.IsReadOnly = false;
				colvarPPContractPhone.DefaultSetting = @"";
				colvarPPContractPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPPContractPhone);

				TableSchema.TableColumn colvarNumberOfPurchasedAccounts = new TableSchema.TableColumn(schema);
				colvarNumberOfPurchasedAccounts.ColumnName = "NumberOfPurchasedAccounts";
				colvarNumberOfPurchasedAccounts.DataType = DbType.Int32;
				colvarNumberOfPurchasedAccounts.MaxLength = 0;
				colvarNumberOfPurchasedAccounts.AutoIncrement = false;
				colvarNumberOfPurchasedAccounts.IsNullable = true;
				colvarNumberOfPurchasedAccounts.IsPrimaryKey = false;
				colvarNumberOfPurchasedAccounts.IsForeignKey = false;
				colvarNumberOfPurchasedAccounts.IsReadOnly = false;
				colvarNumberOfPurchasedAccounts.DefaultSetting = @"";
				colvarNumberOfPurchasedAccounts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberOfPurchasedAccounts);

				TableSchema.TableColumn colvarScheduleA = new TableSchema.TableColumn(schema);
				colvarScheduleA.ColumnName = "ScheduleA";
				colvarScheduleA.DataType = DbType.String;
				colvarScheduleA.MaxLength = 50;
				colvarScheduleA.AutoIncrement = false;
				colvarScheduleA.IsNullable = true;
				colvarScheduleA.IsPrimaryKey = false;
				colvarScheduleA.IsForeignKey = false;
				colvarScheduleA.IsReadOnly = false;
				colvarScheduleA.DefaultSetting = @"";
				colvarScheduleA.ForeignKeyTableName = "";
				schema.Columns.Add(colvarScheduleA);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_PurchaseContractMonitronics",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_PurchaseContractMonitronic LoadFrom(FE_PurchaseContractMonitronic item)
		{
			FE_PurchaseContractMonitronic result = new FE_PurchaseContractMonitronic();
			if (item.PurchaseContractMonitronicsID != default(int)) {
				result.LoadByKey(item.PurchaseContractMonitronicsID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PurchaseContractMonitronicsID {
			get { return GetColumnValue<int>(Columns.PurchaseContractMonitronicsID); }
			set {
				SetColumnValue(Columns.PurchaseContractMonitronicsID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseContractMonitronicsID));
			}
		}
		[DataMember]
		public int PurchaseContractId {
			get { return GetColumnValue<int>(Columns.PurchaseContractId); }
			set {
				SetColumnValue(Columns.PurchaseContractId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseContractId));
			}
		}
		[DataMember]
		public int? PPMonitronicsId {
			get { return GetColumnValue<int?>(Columns.PPMonitronicsId); }
			set {
				SetColumnValue(Columns.PPMonitronicsId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PPMonitronicsId));
			}
		}
		[DataMember]
		public string PPContractContact {
			get { return GetColumnValue<string>(Columns.PPContractContact); }
			set {
				SetColumnValue(Columns.PPContractContact, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PPContractContact));
			}
		}
		[DataMember]
		public string PPContractEntity {
			get { return GetColumnValue<string>(Columns.PPContractEntity); }
			set {
				SetColumnValue(Columns.PPContractEntity, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PPContractEntity));
			}
		}
		[DataMember]
		public string PPContractPhone {
			get { return GetColumnValue<string>(Columns.PPContractPhone); }
			set {
				SetColumnValue(Columns.PPContractPhone, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PPContractPhone));
			}
		}
		[DataMember]
		public int? NumberOfPurchasedAccounts {
			get { return GetColumnValue<int?>(Columns.NumberOfPurchasedAccounts); }
			set {
				SetColumnValue(Columns.NumberOfPurchasedAccounts, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NumberOfPurchasedAccounts));
			}
		}
		[DataMember]
		public string ScheduleA {
			get { return GetColumnValue<string>(Columns.ScheduleA); }
			set {
				SetColumnValue(Columns.ScheduleA, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ScheduleA));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_PurchaseContract _PurchaseContract;
		//Relationship: FK_FE_PurchaseContractMonitronics_FE_PurchaseContracts
		public FE_PurchaseContract PurchaseContract
		{
			get
			{
				if(_PurchaseContract == null) {
					_PurchaseContract = FE_PurchaseContract.FetchByID(this.PurchaseContractId);
				}
				return _PurchaseContract;
			}
			set
			{
				SetColumnValue("PurchaseContractId", value.PurchaseContractID);
				_PurchaseContract = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaseContractMonitronicsID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchaseContractMonitronicsIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaseContractIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PPMonitronicsIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PPContractContactColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PPContractEntityColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PPContractPhoneColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn NumberOfPurchasedAccountsColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ScheduleAColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchaseContractMonitronicsID = @"PurchaseContractMonitronicsID";
			public static readonly string PurchaseContractId = @"PurchaseContractId";
			public static readonly string PPMonitronicsId = @"PPMonitronicsId";
			public static readonly string PPContractContact = @"PPContractContact";
			public static readonly string PPContractEntity = @"PPContractEntity";
			public static readonly string PPContractPhone = @"PPContractPhone";
			public static readonly string NumberOfPurchasedAccounts = @"NumberOfPurchasedAccounts";
			public static readonly string ScheduleA = @"ScheduleA";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PurchaseContractMonitronicsID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_PurchaseContract class.
	/// </summary>
	[DataContract]
	public partial class FE_PurchaseContractCollection : ActiveList<FE_PurchaseContract, FE_PurchaseContractCollection>
	{
		public static FE_PurchaseContractCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PurchaseContractCollection result = new FE_PurchaseContractCollection();
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
			foreach (FE_PurchaseContract item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_PurchaseContracts table.
	/// </summary>
	[DataContract]
	public partial class FE_PurchaseContract : ActiveRecord<FE_PurchaseContract>, INotifyPropertyChanged
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

		public FE_PurchaseContract()
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
				TableSchema.Table schema = new TableSchema.Table("FE_PurchaseContracts", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchaseContractID = new TableSchema.TableColumn(schema);
				colvarPurchaseContractID.ColumnName = "PurchaseContractID";
				colvarPurchaseContractID.DataType = DbType.Int32;
				colvarPurchaseContractID.MaxLength = 0;
				colvarPurchaseContractID.AutoIncrement = true;
				colvarPurchaseContractID.IsNullable = false;
				colvarPurchaseContractID.IsPrimaryKey = true;
				colvarPurchaseContractID.IsForeignKey = false;
				colvarPurchaseContractID.IsReadOnly = false;
				colvarPurchaseContractID.DefaultSetting = @"";
				colvarPurchaseContractID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseContractID);

				TableSchema.TableColumn colvarPurchaserId = new TableSchema.TableColumn(schema);
				colvarPurchaserId.ColumnName = "PurchaserId";
				colvarPurchaserId.DataType = DbType.AnsiString;
				colvarPurchaserId.MaxLength = 10;
				colvarPurchaserId.AutoIncrement = false;
				colvarPurchaserId.IsNullable = false;
				colvarPurchaserId.IsPrimaryKey = false;
				colvarPurchaserId.IsForeignKey = true;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "FE_Purchasers";
				schema.Columns.Add(colvarPurchaserId);

				TableSchema.TableColumn colvarIsColateralized = new TableSchema.TableColumn(schema);
				colvarIsColateralized.ColumnName = "IsColateralized";
				colvarIsColateralized.DataType = DbType.Boolean;
				colvarIsColateralized.MaxLength = 0;
				colvarIsColateralized.AutoIncrement = false;
				colvarIsColateralized.IsNullable = false;
				colvarIsColateralized.IsPrimaryKey = false;
				colvarIsColateralized.IsForeignKey = false;
				colvarIsColateralized.IsReadOnly = false;
				colvarIsColateralized.DefaultSetting = @"";
				colvarIsColateralized.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsColateralized);

				TableSchema.TableColumn colvarPurchaseDate = new TableSchema.TableColumn(schema);
				colvarPurchaseDate.ColumnName = "PurchaseDate";
				colvarPurchaseDate.DataType = DbType.DateTime;
				colvarPurchaseDate.MaxLength = 0;
				colvarPurchaseDate.AutoIncrement = false;
				colvarPurchaseDate.IsNullable = false;
				colvarPurchaseDate.IsPrimaryKey = false;
				colvarPurchaseDate.IsForeignKey = false;
				colvarPurchaseDate.IsReadOnly = false;
				colvarPurchaseDate.DefaultSetting = @"";
				colvarPurchaseDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseDate);

				TableSchema.TableColumn colvarContractNumber = new TableSchema.TableColumn(schema);
				colvarContractNumber.ColumnName = "ContractNumber";
				colvarContractNumber.DataType = DbType.String;
				colvarContractNumber.MaxLength = 50;
				colvarContractNumber.AutoIncrement = false;
				colvarContractNumber.IsNullable = false;
				colvarContractNumber.IsPrimaryKey = false;
				colvarContractNumber.IsForeignKey = false;
				colvarContractNumber.IsReadOnly = false;
				colvarContractNumber.DefaultSetting = @"";
				colvarContractNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNumber);

				TableSchema.TableColumn colvarContractNotes = new TableSchema.TableColumn(schema);
				colvarContractNotes.ColumnName = "ContractNotes";
				colvarContractNotes.DataType = DbType.String;
				colvarContractNotes.MaxLength = -1;
				colvarContractNotes.AutoIncrement = false;
				colvarContractNotes.IsNullable = true;
				colvarContractNotes.IsPrimaryKey = false;
				colvarContractNotes.IsForeignKey = false;
				colvarContractNotes.IsReadOnly = false;
				colvarContractNotes.DefaultSetting = @"";
				colvarContractNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNotes);

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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_PurchaseContracts",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_PurchaseContract LoadFrom(FE_PurchaseContract item)
		{
			FE_PurchaseContract result = new FE_PurchaseContract();
			if (item.PurchaseContractID != default(int)) {
				result.LoadByKey(item.PurchaseContractID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PurchaseContractID {
			get { return GetColumnValue<int>(Columns.PurchaseContractID); }
			set {
				SetColumnValue(Columns.PurchaseContractID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseContractID));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public bool IsColateralized {
			get { return GetColumnValue<bool>(Columns.IsColateralized); }
			set {
				SetColumnValue(Columns.IsColateralized, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsColateralized));
			}
		}
		[DataMember]
		public DateTime PurchaseDate {
			get { return GetColumnValue<DateTime>(Columns.PurchaseDate); }
			set {
				SetColumnValue(Columns.PurchaseDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseDate));
			}
		}
		[DataMember]
		public string ContractNumber {
			get { return GetColumnValue<string>(Columns.ContractNumber); }
			set {
				SetColumnValue(Columns.ContractNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContractNumber));
			}
		}
		[DataMember]
		public string ContractNotes {
			get { return GetColumnValue<string>(Columns.ContractNotes); }
			set {
				SetColumnValue(Columns.ContractNotes, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContractNotes));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Purchaser _Purchaser;
		//Relationship: FK_FE_PurchaseContracts_FE_Purchasers
		public FE_Purchaser Purchaser
		{
			get
			{
				if(_Purchaser == null) {
					_Purchaser = FE_Purchaser.FetchByID(this.PurchaserId);
				}
				return _Purchaser;
			}
			set
			{
				SetColumnValue("PurchaserId", value.PurchaserID);
				_Purchaser = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaserId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchaseContractIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsColateralizedColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PurchaseDateColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ContractNumberColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ContractNotesColumn
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
			public static readonly string PurchaseContractID = @"PurchaseContractID";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string IsColateralized = @"IsColateralized";
			public static readonly string PurchaseDate = @"PurchaseDate";
			public static readonly string ContractNumber = @"ContractNumber";
			public static readonly string ContractNotes = @"ContractNotes";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PurchaseContractID; }
		}
		*/

		#region Foreign Collections

		private FE_PurchaseContractMonitronicCollection _FE_PurchaseContractMonitronicsCol;
		//Relationship: FK_FE_PurchaseContractMonitronics_FE_PurchaseContracts
		public FE_PurchaseContractMonitronicCollection FE_PurchaseContractMonitronicsCol
		{
			get
			{
				if(_FE_PurchaseContractMonitronicsCol == null) {
					_FE_PurchaseContractMonitronicsCol = new FE_PurchaseContractMonitronicCollection();
					_FE_PurchaseContractMonitronicsCol.LoadAndCloseReader(FE_PurchaseContractMonitronic.Query()
						.WHERE(FE_PurchaseContractMonitronic.Columns.PurchaseContractId, PurchaseContractID).ExecuteReader());
				}
				return _FE_PurchaseContractMonitronicsCol;
			}
		}

		private FE_PurchasedAccountCollection _FE_PurchasedAccountsCol;
		//Relationship: FK_FE_PurchasedAccount_FE_PurchaseContracts
		public FE_PurchasedAccountCollection FE_PurchasedAccountsCol
		{
			get
			{
				if(_FE_PurchasedAccountsCol == null) {
					_FE_PurchasedAccountsCol = new FE_PurchasedAccountCollection();
					_FE_PurchasedAccountsCol.LoadAndCloseReader(FE_PurchasedAccount.Query()
						.WHERE(FE_PurchasedAccount.Columns.PurchaseContractId, PurchaseContractID).ExecuteReader());
				}
				return _FE_PurchasedAccountsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_PurchasedAccount class.
	/// </summary>
	[DataContract]
	public partial class FE_PurchasedAccountCollection : ActiveList<FE_PurchasedAccount, FE_PurchasedAccountCollection>
	{
		public static FE_PurchasedAccountCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PurchasedAccountCollection result = new FE_PurchasedAccountCollection();
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
			foreach (FE_PurchasedAccount item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_PurchasedAccount table.
	/// </summary>
	[DataContract]
	public partial class FE_PurchasedAccount : ActiveRecord<FE_PurchasedAccount>, INotifyPropertyChanged
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

		public FE_PurchasedAccount()
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
				TableSchema.Table schema = new TableSchema.Table("FE_PurchasedAccount", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchasedAccountID = new TableSchema.TableColumn(schema);
				colvarPurchasedAccountID.ColumnName = "PurchasedAccountID";
				colvarPurchasedAccountID.DataType = DbType.Int64;
				colvarPurchasedAccountID.MaxLength = 0;
				colvarPurchasedAccountID.AutoIncrement = true;
				colvarPurchasedAccountID.IsNullable = false;
				colvarPurchasedAccountID.IsPrimaryKey = true;
				colvarPurchasedAccountID.IsForeignKey = false;
				colvarPurchasedAccountID.IsReadOnly = false;
				colvarPurchasedAccountID.DefaultSetting = @"";
				colvarPurchasedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchasedAccountID);

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

				TableSchema.TableColumn colvarPurchaseContractId = new TableSchema.TableColumn(schema);
				colvarPurchaseContractId.ColumnName = "PurchaseContractId";
				colvarPurchaseContractId.DataType = DbType.Int32;
				colvarPurchaseContractId.MaxLength = 0;
				colvarPurchaseContractId.AutoIncrement = false;
				colvarPurchaseContractId.IsNullable = false;
				colvarPurchaseContractId.IsPrimaryKey = false;
				colvarPurchaseContractId.IsForeignKey = true;
				colvarPurchaseContractId.IsReadOnly = false;
				colvarPurchaseContractId.DefaultSetting = @"";
				colvarPurchaseContractId.ForeignKeyTableName = "FE_PurchaseContracts";
				schema.Columns.Add(colvarPurchaseContractId);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = false;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
				schema.Columns.Add(colvarAccountFundingStatusId);

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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_PurchasedAccount",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_PurchasedAccount LoadFrom(FE_PurchasedAccount item)
		{
			FE_PurchasedAccount result = new FE_PurchasedAccount();
			if (item.PurchasedAccountID != default(long)) {
				result.LoadByKey(item.PurchasedAccountID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long PurchasedAccountID {
			get { return GetColumnValue<long>(Columns.PurchasedAccountID); }
			set {
				SetColumnValue(Columns.PurchasedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchasedAccountID));
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
		public int PurchaseContractId {
			get { return GetColumnValue<int>(Columns.PurchaseContractId); }
			set {
				SetColumnValue(Columns.PurchaseContractId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseContractId));
			}
		}
		[DataMember]
		public long AccountFundingStatusId {
			get { return GetColumnValue<long>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_PurchasedAccount_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		private FE_PurchaseContract _PurchaseContract;
		//Relationship: FK_FE_PurchasedAccount_FE_PurchaseContracts
		public FE_PurchaseContract PurchaseContract
		{
			get
			{
				if(_PurchaseContract == null) {
					_PurchaseContract = FE_PurchaseContract.FetchByID(this.PurchaseContractId);
				}
				return _PurchaseContract;
			}
			set
			{
				SetColumnValue("PurchaseContractId", value.PurchaseContractID);
				_PurchaseContract = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchasedAccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchasedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PurchaseContractIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchasedAccountID = @"PurchasedAccountID";
			public static readonly string AccountId = @"AccountId";
			public static readonly string PurchaseContractId = @"PurchaseContractId";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PurchasedAccountID; }
		}
		*/

		#region Foreign Collections

		private FE_PurchasedAccountMonitronicCollection _FE_PurchasedAccountMonitronicsCol;
		//Relationship: FK_FE_PurchasedAccountMonitronics_FE_PurchasedAccount
		public FE_PurchasedAccountMonitronicCollection FE_PurchasedAccountMonitronicsCol
		{
			get
			{
				if(_FE_PurchasedAccountMonitronicsCol == null) {
					_FE_PurchasedAccountMonitronicsCol = new FE_PurchasedAccountMonitronicCollection();
					_FE_PurchasedAccountMonitronicsCol.LoadAndCloseReader(FE_PurchasedAccountMonitronic.Query()
						.WHERE(FE_PurchasedAccountMonitronic.Columns.PurchasedAccountID, PurchasedAccountID).ExecuteReader());
				}
				return _FE_PurchasedAccountMonitronicsCol;
			}
		}

		private FE_ReplacedAccountCollection _FE_ReplacedAccountsCol;
		//Relationship: FK_FE_ReplacedAccount_FE_PurchasedAccount
		public FE_ReplacedAccountCollection FE_ReplacedAccountsCol
		{
			get
			{
				if(_FE_ReplacedAccountsCol == null) {
					_FE_ReplacedAccountsCol = new FE_ReplacedAccountCollection();
					_FE_ReplacedAccountsCol.LoadAndCloseReader(FE_ReplacedAccount.Query()
						.WHERE(FE_ReplacedAccount.Columns.PurchasedAccountId, PurchasedAccountID).ExecuteReader());
				}
				return _FE_ReplacedAccountsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_PurchasedAccountMonitronic class.
	/// </summary>
	[DataContract]
	public partial class FE_PurchasedAccountMonitronicCollection : ActiveList<FE_PurchasedAccountMonitronic, FE_PurchasedAccountMonitronicCollection>
	{
		public static FE_PurchasedAccountMonitronicCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PurchasedAccountMonitronicCollection result = new FE_PurchasedAccountMonitronicCollection();
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
			foreach (FE_PurchasedAccountMonitronic item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_PurchasedAccountMonitronics table.
	/// </summary>
	[DataContract]
	public partial class FE_PurchasedAccountMonitronic : ActiveRecord<FE_PurchasedAccountMonitronic>, INotifyPropertyChanged
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

		public FE_PurchasedAccountMonitronic()
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
				TableSchema.Table schema = new TableSchema.Table("FE_PurchasedAccountMonitronics", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchasedAccountID = new TableSchema.TableColumn(schema);
				colvarPurchasedAccountID.ColumnName = "PurchasedAccountID";
				colvarPurchasedAccountID.DataType = DbType.Int64;
				colvarPurchasedAccountID.MaxLength = 0;
				colvarPurchasedAccountID.AutoIncrement = false;
				colvarPurchasedAccountID.IsNullable = false;
				colvarPurchasedAccountID.IsPrimaryKey = true;
				colvarPurchasedAccountID.IsForeignKey = false;
				colvarPurchasedAccountID.IsReadOnly = false;
				colvarPurchasedAccountID.DefaultSetting = @"";
				colvarPurchasedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchasedAccountID);

				TableSchema.TableColumn colvarTypeTxt = new TableSchema.TableColumn(schema);
				colvarTypeTxt.ColumnName = "TypeTxt";
				colvarTypeTxt.DataType = DbType.String;
				colvarTypeTxt.MaxLength = 50;
				colvarTypeTxt.AutoIncrement = false;
				colvarTypeTxt.IsNullable = true;
				colvarTypeTxt.IsPrimaryKey = false;
				colvarTypeTxt.IsForeignKey = false;
				colvarTypeTxt.IsReadOnly = false;
				colvarTypeTxt.DefaultSetting = @"";
				colvarTypeTxt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTypeTxt);

				TableSchema.TableColumn colvarSubscriberNumber = new TableSchema.TableColumn(schema);
				colvarSubscriberNumber.ColumnName = "SubscriberNumber";
				colvarSubscriberNumber.DataType = DbType.String;
				colvarSubscriberNumber.MaxLength = 50;
				colvarSubscriberNumber.AutoIncrement = false;
				colvarSubscriberNumber.IsNullable = true;
				colvarSubscriberNumber.IsPrimaryKey = false;
				colvarSubscriberNumber.IsForeignKey = false;
				colvarSubscriberNumber.IsReadOnly = false;
				colvarSubscriberNumber.DefaultSetting = @"";
				colvarSubscriberNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubscriberNumber);

				TableSchema.TableColumn colvarMonitoring = new TableSchema.TableColumn(schema);
				colvarMonitoring.ColumnName = "Monitoring";
				colvarMonitoring.DataType = DbType.Currency;
				colvarMonitoring.MaxLength = 0;
				colvarMonitoring.AutoIncrement = false;
				colvarMonitoring.IsNullable = true;
				colvarMonitoring.IsPrimaryKey = false;
				colvarMonitoring.IsForeignKey = false;
				colvarMonitoring.IsReadOnly = false;
				colvarMonitoring.DefaultSetting = @"";
				colvarMonitoring.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonitoring);

				TableSchema.TableColumn colvarPurchaseMultiple = new TableSchema.TableColumn(schema);
				colvarPurchaseMultiple.ColumnName = "PurchaseMultiple";
				colvarPurchaseMultiple.DataType = DbType.Decimal;
				colvarPurchaseMultiple.MaxLength = 0;
				colvarPurchaseMultiple.AutoIncrement = false;
				colvarPurchaseMultiple.IsNullable = true;
				colvarPurchaseMultiple.IsPrimaryKey = false;
				colvarPurchaseMultiple.IsForeignKey = false;
				colvarPurchaseMultiple.IsReadOnly = false;
				colvarPurchaseMultiple.DefaultSetting = @"";
				colvarPurchaseMultiple.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseMultiple);

				TableSchema.TableColumn colvarPurchasePrice = new TableSchema.TableColumn(schema);
				colvarPurchasePrice.ColumnName = "PurchasePrice";
				colvarPurchasePrice.DataType = DbType.Currency;
				colvarPurchasePrice.MaxLength = 0;
				colvarPurchasePrice.AutoIncrement = false;
				colvarPurchasePrice.IsNullable = true;
				colvarPurchasePrice.IsPrimaryKey = false;
				colvarPurchasePrice.IsForeignKey = false;
				colvarPurchasePrice.IsReadOnly = false;
				colvarPurchasePrice.DefaultSetting = @"";
				colvarPurchasePrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchasePrice);

				TableSchema.TableColumn colvarTerm = new TableSchema.TableColumn(schema);
				colvarTerm.ColumnName = "Term";
				colvarTerm.DataType = DbType.Int32;
				colvarTerm.MaxLength = 0;
				colvarTerm.AutoIncrement = false;
				colvarTerm.IsNullable = true;
				colvarTerm.IsPrimaryKey = false;
				colvarTerm.IsForeignKey = false;
				colvarTerm.IsReadOnly = false;
				colvarTerm.DefaultSetting = @"";
				colvarTerm.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerm);

				TableSchema.TableColumn colvarDiscount = new TableSchema.TableColumn(schema);
				colvarDiscount.ColumnName = "Discount";
				colvarDiscount.DataType = DbType.Currency;
				colvarDiscount.MaxLength = 0;
				colvarDiscount.AutoIncrement = false;
				colvarDiscount.IsNullable = true;
				colvarDiscount.IsPrimaryKey = false;
				colvarDiscount.IsForeignKey = false;
				colvarDiscount.IsReadOnly = false;
				colvarDiscount.DefaultSetting = @"";
				colvarDiscount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDiscount);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_PurchasedAccountMonitronics",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_PurchasedAccountMonitronic LoadFrom(FE_PurchasedAccountMonitronic item)
		{
			FE_PurchasedAccountMonitronic result = new FE_PurchasedAccountMonitronic();
			if (item.PurchasedAccountID != default(long)) {
				result.LoadByKey(item.PurchasedAccountID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long PurchasedAccountID {
			get { return GetColumnValue<long>(Columns.PurchasedAccountID); }
			set {
				SetColumnValue(Columns.PurchasedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchasedAccountID));
			}
		}
		[DataMember]
		public string TypeTxt {
			get { return GetColumnValue<string>(Columns.TypeTxt); }
			set {
				SetColumnValue(Columns.TypeTxt, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TypeTxt));
			}
		}
		[DataMember]
		public string SubscriberNumber {
			get { return GetColumnValue<string>(Columns.SubscriberNumber); }
			set {
				SetColumnValue(Columns.SubscriberNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubscriberNumber));
			}
		}
		[DataMember]
		public decimal? Monitoring {
			get { return GetColumnValue<decimal?>(Columns.Monitoring); }
			set {
				SetColumnValue(Columns.Monitoring, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Monitoring));
			}
		}
		[DataMember]
		public decimal? PurchaseMultiple {
			get { return GetColumnValue<decimal?>(Columns.PurchaseMultiple); }
			set {
				SetColumnValue(Columns.PurchaseMultiple, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseMultiple));
			}
		}
		[DataMember]
		public decimal? PurchasePrice {
			get { return GetColumnValue<decimal?>(Columns.PurchasePrice); }
			set {
				SetColumnValue(Columns.PurchasePrice, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchasePrice));
			}
		}
		[DataMember]
		public int? Term {
			get { return GetColumnValue<int?>(Columns.Term); }
			set {
				SetColumnValue(Columns.Term, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Term));
			}
		}
		[DataMember]
		public decimal? Discount {
			get { return GetColumnValue<decimal?>(Columns.Discount); }
			set {
				SetColumnValue(Columns.Discount, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Discount));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_PurchasedAccount _PurchasedAccount;
		//Relationship: FK_FE_PurchasedAccountMonitronics_FE_PurchasedAccount
		public FE_PurchasedAccount PurchasedAccount
		{
			get
			{
				if(_PurchasedAccount == null) {
					_PurchasedAccount = FE_PurchasedAccount.FetchByID(this.PurchasedAccountID);
				}
				return _PurchasedAccount;
			}
			set
			{
				SetColumnValue("PurchasedAccountID", value.PurchasedAccountID);
				_PurchasedAccount = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return TypeTxt;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchasedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TypeTxtColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SubscriberNumberColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn MonitoringColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PurchaseMultipleColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PurchasePriceColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TermColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DiscountColumn
		{
			get { return Schema.Columns[7]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchasedAccountID = @"PurchasedAccountID";
			public static readonly string TypeTxt = @"TypeTxt";
			public static readonly string SubscriberNumber = @"SubscriberNumber";
			public static readonly string Monitoring = @"Monitoring";
			public static readonly string PurchaseMultiple = @"PurchaseMultiple";
			public static readonly string PurchasePrice = @"PurchasePrice";
			public static readonly string Term = @"Term";
			public static readonly string Discount = @"Discount";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PurchasedAccountID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_Purchaser class.
	/// </summary>
	[DataContract]
	public partial class FE_PurchaserCollection : ActiveList<FE_Purchaser, FE_PurchaserCollection>
	{
		public static FE_PurchaserCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_PurchaserCollection result = new FE_PurchaserCollection();
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
			foreach (FE_Purchaser item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_Purchasers table.
	/// </summary>
	[DataContract]
	public partial class FE_Purchaser : ActiveRecord<FE_Purchaser>, INotifyPropertyChanged
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

		public FE_Purchaser()
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
				TableSchema.Table schema = new TableSchema.Table("FE_Purchasers", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchaserID = new TableSchema.TableColumn(schema);
				colvarPurchaserID.ColumnName = "PurchaserID";
				colvarPurchaserID.DataType = DbType.AnsiString;
				colvarPurchaserID.MaxLength = 10;
				colvarPurchaserID.AutoIncrement = false;
				colvarPurchaserID.IsNullable = false;
				colvarPurchaserID.IsPrimaryKey = true;
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_Purchasers",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_Purchaser LoadFrom(FE_Purchaser item)
		{
			FE_Purchaser result = new FE_Purchaser();
			if (item.PurchaserID != default(string)) {
				result.LoadByKey(item.PurchaserID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string PurchaserID {
			get { return GetColumnValue<string>(Columns.PurchaserID); }
			set {
				SetColumnValue(Columns.PurchaserID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserID));
			}
		}
		[DataMember]
		public string PurchaserName {
			get { return GetColumnValue<string>(Columns.PurchaserName); }
			set {
				SetColumnValue(Columns.PurchaserName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return PurchaserName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchaserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchaserID = @"PurchaserID";
			public static readonly string PurchaserName = @"PurchaserName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PurchaserID; }
		}
		*/

		#region Foreign Collections

		private FE_BundleCollection _FE_BundlesCol;
		//Relationship: FK_FE_Bundles_FE_Purchasers
		public FE_BundleCollection FE_BundlesCol
		{
			get
			{
				if(_FE_BundlesCol == null) {
					_FE_BundlesCol = new FE_BundleCollection();
					_FE_BundlesCol.LoadAndCloseReader(FE_Bundle.Query()
						.WHERE(FE_Bundle.Columns.PurchaserId, PurchaserID).ExecuteReader());
				}
				return _FE_BundlesCol;
			}
		}

		private FE_CriteriaCollection _FE_CriteriasCol;
		//Relationship: FK_FE_Criteria_FE_Purchasers
		public FE_CriteriaCollection FE_CriteriasCol
		{
			get
			{
				if(_FE_CriteriasCol == null) {
					_FE_CriteriasCol = new FE_CriteriaCollection();
					_FE_CriteriasCol.LoadAndCloseReader(FE_Criteria.Query()
						.WHERE(FE_Criteria.Columns.PurchaserId, PurchaserID).ExecuteReader());
				}
				return _FE_CriteriasCol;
			}
		}

		private FE_PurchaseContractCollection _FE_PurchaseContractsCol;
		//Relationship: FK_FE_PurchaseContracts_FE_Purchasers
		public FE_PurchaseContractCollection FE_PurchaseContractsCol
		{
			get
			{
				if(_FE_PurchaseContractsCol == null) {
					_FE_PurchaseContractsCol = new FE_PurchaseContractCollection();
					_FE_PurchaseContractsCol.LoadAndCloseReader(FE_PurchaseContract.Query()
						.WHERE(FE_PurchaseContract.Columns.PurchaserId, PurchaserID).ExecuteReader());
				}
				return _FE_PurchaseContractsCol;
			}
		}

		private FE_ReturnActionCollection _FE_ReturnActionsCol;
		//Relationship: FK_FE_ReturnActions_FE_Purchasers
		public FE_ReturnActionCollection FE_ReturnActionsCol
		{
			get
			{
				if(_FE_ReturnActionsCol == null) {
					_FE_ReturnActionsCol = new FE_ReturnActionCollection();
					_FE_ReturnActionsCol.LoadAndCloseReader(FE_ReturnAction.Query()
						.WHERE(FE_ReturnAction.Columns.PurchaserId, PurchaserID).ExecuteReader());
				}
				return _FE_ReturnActionsCol;
			}
		}

		private FE_ReturnManifestCollection _FE_ReturnManifestsCol;
		//Relationship: FK_FE_ReturnManifests_FE_Purchasers
		public FE_ReturnManifestCollection FE_ReturnManifestsCol
		{
			get
			{
				if(_FE_ReturnManifestsCol == null) {
					_FE_ReturnManifestsCol = new FE_ReturnManifestCollection();
					_FE_ReturnManifestsCol.LoadAndCloseReader(FE_ReturnManifest.Query()
						.WHERE(FE_ReturnManifest.Columns.PurchaserId, PurchaserID).ExecuteReader());
				}
				return _FE_ReturnManifestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_RejectedAccount class.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccountCollection : ActiveList<FE_RejectedAccount, FE_RejectedAccountCollection>
	{
		public static FE_RejectedAccountCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_RejectedAccountCollection result = new FE_RejectedAccountCollection();
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
			foreach (FE_RejectedAccount item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_RejectedAccount table.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccount : ActiveRecord<FE_RejectedAccount>, INotifyPropertyChanged
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

		public FE_RejectedAccount()
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
				TableSchema.Table schema = new TableSchema.Table("FE_RejectedAccount", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRejectedAccountID = new TableSchema.TableColumn(schema);
				colvarRejectedAccountID.ColumnName = "RejectedAccountID";
				colvarRejectedAccountID.DataType = DbType.Int64;
				colvarRejectedAccountID.MaxLength = 0;
				colvarRejectedAccountID.AutoIncrement = true;
				colvarRejectedAccountID.IsNullable = false;
				colvarRejectedAccountID.IsPrimaryKey = true;
				colvarRejectedAccountID.IsForeignKey = false;
				colvarRejectedAccountID.IsReadOnly = false;
				colvarRejectedAccountID.DefaultSetting = @"";
				colvarRejectedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectedAccountID);

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
				colvarPacketItemId.IsForeignKey = true;
				colvarPacketItemId.IsReadOnly = false;
				colvarPacketItemId.DefaultSetting = @"";
				colvarPacketItemId.ForeignKeyTableName = "FE_PacketItems";
				schema.Columns.Add(colvarPacketItemId);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = true;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_RejectedAccount",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_RejectedAccount LoadFrom(FE_RejectedAccount item)
		{
			FE_RejectedAccount result = new FE_RejectedAccount();
			if (item.RejectedAccountID != default(long)) {
				result.LoadByKey(item.RejectedAccountID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long RejectedAccountID {
			get { return GetColumnValue<long>(Columns.RejectedAccountID); }
			set {
				SetColumnValue(Columns.RejectedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectedAccountID));
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
		public long? PacketItemId {
			get { return GetColumnValue<long?>(Columns.PacketItemId); }
			set {
				SetColumnValue(Columns.PacketItemId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketItemId));
			}
		}
		[DataMember]
		public long? AccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_RejectedAccount_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		private FE_PacketItem _PacketItem;
		//Relationship: FK_FE_RejectedAccount_FE_PacketItems
		public FE_PacketItem PacketItem
		{
			get
			{
				if(_PacketItem == null) {
					_PacketItem = FE_PacketItem.FetchByID(this.PacketItemId);
				}
				return _PacketItem;
			}
			set
			{
				SetColumnValue("PacketItemId", value.PacketItemID);
				_PacketItem = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RejectedAccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RejectedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PacketItemIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RejectedAccountID = @"RejectedAccountID";
			public static readonly string AccountId = @"AccountId";
			public static readonly string PacketItemId = @"PacketItemId";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RejectedAccountID; }
		}
		*/

		#region Foreign Collections

		private FE_RejectedAccountMonitronicCollection _FE_RejectedAccountMonitronicsCol;
		//Relationship: FK_FE_RejectedAccountMonitronics_FE_RejectedAccount
		public FE_RejectedAccountMonitronicCollection FE_RejectedAccountMonitronicsCol
		{
			get
			{
				if(_FE_RejectedAccountMonitronicsCol == null) {
					_FE_RejectedAccountMonitronicsCol = new FE_RejectedAccountMonitronicCollection();
					_FE_RejectedAccountMonitronicsCol.LoadAndCloseReader(FE_RejectedAccountMonitronic.Query()
						.WHERE(FE_RejectedAccountMonitronic.Columns.RejectedAccountID, RejectedAccountID).ExecuteReader());
				}
				return _FE_RejectedAccountMonitronicsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_RejectedAccountMonitronic class.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccountMonitronicCollection : ActiveList<FE_RejectedAccountMonitronic, FE_RejectedAccountMonitronicCollection>
	{
		public static FE_RejectedAccountMonitronicCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_RejectedAccountMonitronicCollection result = new FE_RejectedAccountMonitronicCollection();
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
			foreach (FE_RejectedAccountMonitronic item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_RejectedAccountMonitronics table.
	/// </summary>
	[DataContract]
	public partial class FE_RejectedAccountMonitronic : ActiveRecord<FE_RejectedAccountMonitronic>, INotifyPropertyChanged
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

		public FE_RejectedAccountMonitronic()
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
				TableSchema.Table schema = new TableSchema.Table("FE_RejectedAccountMonitronics", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRejectedAccountID = new TableSchema.TableColumn(schema);
				colvarRejectedAccountID.ColumnName = "RejectedAccountID";
				colvarRejectedAccountID.DataType = DbType.Int64;
				colvarRejectedAccountID.MaxLength = 0;
				colvarRejectedAccountID.AutoIncrement = false;
				colvarRejectedAccountID.IsNullable = false;
				colvarRejectedAccountID.IsPrimaryKey = false;
				colvarRejectedAccountID.IsForeignKey = true;
				colvarRejectedAccountID.IsReadOnly = false;
				colvarRejectedAccountID.DefaultSetting = @"";
				colvarRejectedAccountID.ForeignKeyTableName = "FE_RejectedAccount";
				schema.Columns.Add(colvarRejectedAccountID);

				TableSchema.TableColumn colvarComments = new TableSchema.TableColumn(schema);
				colvarComments.ColumnName = "Comments";
				colvarComments.DataType = DbType.String;
				colvarComments.MaxLength = 1073741823;
				colvarComments.AutoIncrement = false;
				colvarComments.IsNullable = true;
				colvarComments.IsPrimaryKey = false;
				colvarComments.IsForeignKey = false;
				colvarComments.IsReadOnly = false;
				colvarComments.DefaultSetting = @"";
				colvarComments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComments);

				TableSchema.TableColumn colvarSurveyDecline = new TableSchema.TableColumn(schema);
				colvarSurveyDecline.ColumnName = "SurveyDecline";
				colvarSurveyDecline.DataType = DbType.String;
				colvarSurveyDecline.MaxLength = 50;
				colvarSurveyDecline.AutoIncrement = false;
				colvarSurveyDecline.IsNullable = true;
				colvarSurveyDecline.IsPrimaryKey = false;
				colvarSurveyDecline.IsForeignKey = false;
				colvarSurveyDecline.IsReadOnly = false;
				colvarSurveyDecline.DefaultSetting = @"";
				colvarSurveyDecline.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyDecline);

				TableSchema.TableColumn colvarSurveyDealerComments = new TableSchema.TableColumn(schema);
				colvarSurveyDealerComments.ColumnName = "SurveyDealerComments";
				colvarSurveyDealerComments.DataType = DbType.String;
				colvarSurveyDealerComments.MaxLength = 1073741823;
				colvarSurveyDealerComments.AutoIncrement = false;
				colvarSurveyDealerComments.IsNullable = true;
				colvarSurveyDealerComments.IsPrimaryKey = false;
				colvarSurveyDealerComments.IsForeignKey = false;
				colvarSurveyDealerComments.IsReadOnly = false;
				colvarSurveyDealerComments.DefaultSetting = @"";
				colvarSurveyDealerComments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyDealerComments);

				TableSchema.TableColumn colvarOnlineDate = new TableSchema.TableColumn(schema);
				colvarOnlineDate.ColumnName = "OnlineDate";
				colvarOnlineDate.DataType = DbType.DateTime;
				colvarOnlineDate.MaxLength = 0;
				colvarOnlineDate.AutoIncrement = false;
				colvarOnlineDate.IsNullable = true;
				colvarOnlineDate.IsPrimaryKey = false;
				colvarOnlineDate.IsForeignKey = false;
				colvarOnlineDate.IsReadOnly = false;
				colvarOnlineDate.DefaultSetting = @"";
				colvarOnlineDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOnlineDate);

				TableSchema.TableColumn colvarLastSignalDate = new TableSchema.TableColumn(schema);
				colvarLastSignalDate.ColumnName = "LastSignalDate";
				colvarLastSignalDate.DataType = DbType.DateTime;
				colvarLastSignalDate.MaxLength = 0;
				colvarLastSignalDate.AutoIncrement = false;
				colvarLastSignalDate.IsNullable = true;
				colvarLastSignalDate.IsPrimaryKey = false;
				colvarLastSignalDate.IsForeignKey = false;
				colvarLastSignalDate.IsReadOnly = false;
				colvarLastSignalDate.DefaultSetting = @"";
				colvarLastSignalDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastSignalDate);

				TableSchema.TableColumn colvarContractNumber = new TableSchema.TableColumn(schema);
				colvarContractNumber.ColumnName = "ContractNumber";
				colvarContractNumber.DataType = DbType.String;
				colvarContractNumber.MaxLength = 50;
				colvarContractNumber.AutoIncrement = false;
				colvarContractNumber.IsNullable = true;
				colvarContractNumber.IsPrimaryKey = false;
				colvarContractNumber.IsForeignKey = false;
				colvarContractNumber.IsReadOnly = false;
				colvarContractNumber.DefaultSetting = @"";
				colvarContractNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNumber);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_RejectedAccountMonitronics",schema);
			}
		}
		#endregion // Schema and Query Accessor


		#region Properties
		[DataMember]
		public long RejectedAccountID {
			get { return GetColumnValue<long>(Columns.RejectedAccountID); }
			set {
				SetColumnValue(Columns.RejectedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectedAccountID));
			}
		}
		[DataMember]
		public string Comments {
			get { return GetColumnValue<string>(Columns.Comments); }
			set {
				SetColumnValue(Columns.Comments, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Comments));
			}
		}
		[DataMember]
		public string SurveyDecline {
			get { return GetColumnValue<string>(Columns.SurveyDecline); }
			set {
				SetColumnValue(Columns.SurveyDecline, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyDecline));
			}
		}
		[DataMember]
		public string SurveyDealerComments {
			get { return GetColumnValue<string>(Columns.SurveyDealerComments); }
			set {
				SetColumnValue(Columns.SurveyDealerComments, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyDealerComments));
			}
		}
		[DataMember]
		public DateTime? OnlineDate {
			get { return GetColumnValue<DateTime?>(Columns.OnlineDate); }
			set {
				SetColumnValue(Columns.OnlineDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OnlineDate));
			}
		}
		[DataMember]
		public DateTime? LastSignalDate {
			get { return GetColumnValue<DateTime?>(Columns.LastSignalDate); }
			set {
				SetColumnValue(Columns.LastSignalDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastSignalDate));
			}
		}
		[DataMember]
		public string ContractNumber {
			get { return GetColumnValue<string>(Columns.ContractNumber); }
			set {
				SetColumnValue(Columns.ContractNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContractNumber));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_RejectedAccount _RejectedAccount;
		//Relationship: FK_FE_RejectedAccountMonitronics_FE_RejectedAccount
		public FE_RejectedAccount RejectedAccount
		{
			get
			{
				if(_RejectedAccount == null) {
					_RejectedAccount = FE_RejectedAccount.FetchByID(this.RejectedAccountID);
				}
				return _RejectedAccount;
			}
			set
			{
				SetColumnValue("RejectedAccountID", value.RejectedAccountID);
				_RejectedAccount = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return Comments;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RejectedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CommentsColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SurveyDeclineColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SurveyDealerCommentsColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn OnlineDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LastSignalDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ContractNumberColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RejectedAccountID = @"RejectedAccountID";
			public static readonly string Comments = @"Comments";
			public static readonly string SurveyDecline = @"SurveyDecline";
			public static readonly string SurveyDealerComments = @"SurveyDealerComments";
			public static readonly string OnlineDate = @"OnlineDate";
			public static readonly string LastSignalDate = @"LastSignalDate";
			public static readonly string ContractNumber = @"ContractNumber";
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
	/// Strongly-typed collection for the FE_Rejection class.
	/// </summary>
	[DataContract]
	public partial class FE_RejectionCollection : ActiveList<FE_Rejection, FE_RejectionCollection>
	{
		public static FE_RejectionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_RejectionCollection result = new FE_RejectionCollection();
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
			foreach (FE_Rejection item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_Rejection table.
	/// </summary>
	[DataContract]
	public partial class FE_Rejection : ActiveRecord<FE_Rejection>, INotifyPropertyChanged
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

		public FE_Rejection()
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
				TableSchema.Table schema = new TableSchema.Table("FE_Rejection", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRejectionID = new TableSchema.TableColumn(schema);
				colvarRejectionID.ColumnName = "RejectionID";
				colvarRejectionID.DataType = DbType.Int32;
				colvarRejectionID.MaxLength = 0;
				colvarRejectionID.AutoIncrement = true;
				colvarRejectionID.IsNullable = false;
				colvarRejectionID.IsPrimaryKey = true;
				colvarRejectionID.IsForeignKey = false;
				colvarRejectionID.IsReadOnly = false;
				colvarRejectionID.DefaultSetting = @"";
				colvarRejectionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionID);

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

				TableSchema.TableColumn colvarRejectionTypeId = new TableSchema.TableColumn(schema);
				colvarRejectionTypeId.ColumnName = "RejectionTypeId";
				colvarRejectionTypeId.DataType = DbType.Int32;
				colvarRejectionTypeId.MaxLength = 0;
				colvarRejectionTypeId.AutoIncrement = false;
				colvarRejectionTypeId.IsNullable = false;
				colvarRejectionTypeId.IsPrimaryKey = false;
				colvarRejectionTypeId.IsForeignKey = true;
				colvarRejectionTypeId.IsReadOnly = false;
				colvarRejectionTypeId.DefaultSetting = @"";
				colvarRejectionTypeId.ForeignKeyTableName = "FE_RejectionTypes";
				schema.Columns.Add(colvarRejectionTypeId);

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

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = true;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
				schema.Columns.Add(colvarAccountFundingStatusId);

				TableSchema.TableColumn colvarPacketItemId = new TableSchema.TableColumn(schema);
				colvarPacketItemId.ColumnName = "PacketItemId";
				colvarPacketItemId.DataType = DbType.Int64;
				colvarPacketItemId.MaxLength = 0;
				colvarPacketItemId.AutoIncrement = false;
				colvarPacketItemId.IsNullable = true;
				colvarPacketItemId.IsPrimaryKey = false;
				colvarPacketItemId.IsForeignKey = true;
				colvarPacketItemId.IsReadOnly = false;
				colvarPacketItemId.DefaultSetting = @"";
				colvarPacketItemId.ForeignKeyTableName = "FE_PacketItems";
				schema.Columns.Add(colvarPacketItemId);

				TableSchema.TableColumn colvarRejectionDescription = new TableSchema.TableColumn(schema);
				colvarRejectionDescription.ColumnName = "RejectionDescription";
				colvarRejectionDescription.DataType = DbType.String;
				colvarRejectionDescription.MaxLength = -1;
				colvarRejectionDescription.AutoIncrement = false;
				colvarRejectionDescription.IsNullable = true;
				colvarRejectionDescription.IsPrimaryKey = false;
				colvarRejectionDescription.IsForeignKey = false;
				colvarRejectionDescription.IsReadOnly = false;
				colvarRejectionDescription.DefaultSetting = @"";
				colvarRejectionDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionDescription);

				TableSchema.TableColumn colvarResolvedBy = new TableSchema.TableColumn(schema);
				colvarResolvedBy.ColumnName = "ResolvedBy";
				colvarResolvedBy.DataType = DbType.String;
				colvarResolvedBy.MaxLength = 50;
				colvarResolvedBy.AutoIncrement = false;
				colvarResolvedBy.IsNullable = true;
				colvarResolvedBy.IsPrimaryKey = false;
				colvarResolvedBy.IsForeignKey = false;
				colvarResolvedBy.IsReadOnly = false;
				colvarResolvedBy.DefaultSetting = @"";
				colvarResolvedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResolvedBy);

				TableSchema.TableColumn colvarResolvedOn = new TableSchema.TableColumn(schema);
				colvarResolvedOn.ColumnName = "ResolvedOn";
				colvarResolvedOn.DataType = DbType.DateTime;
				colvarResolvedOn.MaxLength = 0;
				colvarResolvedOn.AutoIncrement = false;
				colvarResolvedOn.IsNullable = true;
				colvarResolvedOn.IsPrimaryKey = false;
				colvarResolvedOn.IsForeignKey = false;
				colvarResolvedOn.IsReadOnly = false;
				colvarResolvedOn.DefaultSetting = @"";
				colvarResolvedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResolvedOn);

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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_Rejection",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_Rejection LoadFrom(FE_Rejection item)
		{
			FE_Rejection result = new FE_Rejection();
			if (item.RejectionID != default(int)) {
				result.LoadByKey(item.RejectionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int RejectionID {
			get { return GetColumnValue<int>(Columns.RejectionID); }
			set {
				SetColumnValue(Columns.RejectionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionID));
			}
		}
		[DataMember]
		public int AccountId {
			get { return GetColumnValue<int>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public int RejectionTypeId {
			get { return GetColumnValue<int>(Columns.RejectionTypeId); }
			set {
				SetColumnValue(Columns.RejectionTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionTypeId));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public long? AccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
			}
		}
		[DataMember]
		public long? PacketItemId {
			get { return GetColumnValue<long?>(Columns.PacketItemId); }
			set {
				SetColumnValue(Columns.PacketItemId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketItemId));
			}
		}
		[DataMember]
		public string RejectionDescription {
			get { return GetColumnValue<string>(Columns.RejectionDescription); }
			set {
				SetColumnValue(Columns.RejectionDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionDescription));
			}
		}
		[DataMember]
		public string ResolvedBy {
			get { return GetColumnValue<string>(Columns.ResolvedBy); }
			set {
				SetColumnValue(Columns.ResolvedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResolvedBy));
			}
		}
		[DataMember]
		public DateTime? ResolvedOn {
			get { return GetColumnValue<DateTime?>(Columns.ResolvedOn); }
			set {
				SetColumnValue(Columns.ResolvedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResolvedOn));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_Rejection_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		private FE_PacketItem _PacketItem;
		//Relationship: FK_FE_Rejection_FE_PacketItems
		public FE_PacketItem PacketItem
		{
			get
			{
				if(_PacketItem == null) {
					_PacketItem = FE_PacketItem.FetchByID(this.PacketItemId);
				}
				return _PacketItem;
			}
			set
			{
				SetColumnValue("PacketItemId", value.PacketItemID);
				_PacketItem = value;
			}
		}

		private FE_RejectionType _RejectionType;
		//Relationship: FK_FE_Rejection_FE_RejectionTypes
		public FE_RejectionType RejectionType
		{
			get
			{
				if(_RejectionType == null) {
					_RejectionType = FE_RejectionType.FetchByID(this.RejectionTypeId);
				}
				return _RejectionType;
			}
			set
			{
				SetColumnValue("RejectionTypeId", value.RejectionTypeID);
				_RejectionType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return RejectionID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RejectionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RejectionTypeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PacketItemIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RejectionDescriptionColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ResolvedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ResolvedOnColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RejectionID = @"RejectionID";
			public static readonly string AccountId = @"AccountId";
			public static readonly string RejectionTypeId = @"RejectionTypeId";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string PacketItemId = @"PacketItemId";
			public static readonly string RejectionDescription = @"RejectionDescription";
			public static readonly string ResolvedBy = @"ResolvedBy";
			public static readonly string ResolvedOn = @"ResolvedOn";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RejectionID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_RejectionType class.
	/// </summary>
	[DataContract]
	public partial class FE_RejectionTypeCollection : ActiveList<FE_RejectionType, FE_RejectionTypeCollection>
	{
		public static FE_RejectionTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_RejectionTypeCollection result = new FE_RejectionTypeCollection();
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
			foreach (FE_RejectionType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_RejectionTypes table.
	/// </summary>
	[DataContract]
	public partial class FE_RejectionType : ActiveRecord<FE_RejectionType>, INotifyPropertyChanged
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

		public FE_RejectionType()
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
				TableSchema.Table schema = new TableSchema.Table("FE_RejectionTypes", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRejectionTypeID = new TableSchema.TableColumn(schema);
				colvarRejectionTypeID.ColumnName = "RejectionTypeID";
				colvarRejectionTypeID.DataType = DbType.Int32;
				colvarRejectionTypeID.MaxLength = 0;
				colvarRejectionTypeID.AutoIncrement = true;
				colvarRejectionTypeID.IsNullable = false;
				colvarRejectionTypeID.IsPrimaryKey = true;
				colvarRejectionTypeID.IsForeignKey = false;
				colvarRejectionTypeID.IsReadOnly = false;
				colvarRejectionTypeID.DefaultSetting = @"";
				colvarRejectionTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionTypeID);

				TableSchema.TableColumn colvarRejectionType = new TableSchema.TableColumn(schema);
				colvarRejectionType.ColumnName = "RejectionType";
				colvarRejectionType.DataType = DbType.String;
				colvarRejectionType.MaxLength = 50;
				colvarRejectionType.AutoIncrement = false;
				colvarRejectionType.IsNullable = false;
				colvarRejectionType.IsPrimaryKey = false;
				colvarRejectionType.IsForeignKey = false;
				colvarRejectionType.IsReadOnly = false;
				colvarRejectionType.DefaultSetting = @"";
				colvarRejectionType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionType);

				TableSchema.TableColumn colvarIsPacketSellable = new TableSchema.TableColumn(schema);
				colvarIsPacketSellable.ColumnName = "IsPacketSellable";
				colvarIsPacketSellable.DataType = DbType.Boolean;
				colvarIsPacketSellable.MaxLength = 0;
				colvarIsPacketSellable.AutoIncrement = false;
				colvarIsPacketSellable.IsNullable = false;
				colvarIsPacketSellable.IsPrimaryKey = false;
				colvarIsPacketSellable.IsForeignKey = false;
				colvarIsPacketSellable.IsReadOnly = false;
				colvarIsPacketSellable.DefaultSetting = @"";
				colvarIsPacketSellable.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsPacketSellable);

				TableSchema.TableColumn colvarIsBundleSellable = new TableSchema.TableColumn(schema);
				colvarIsBundleSellable.ColumnName = "IsBundleSellable";
				colvarIsBundleSellable.DataType = DbType.Boolean;
				colvarIsBundleSellable.MaxLength = 0;
				colvarIsBundleSellable.AutoIncrement = false;
				colvarIsBundleSellable.IsNullable = false;
				colvarIsBundleSellable.IsPrimaryKey = false;
				colvarIsBundleSellable.IsForeignKey = false;
				colvarIsBundleSellable.IsReadOnly = false;
				colvarIsBundleSellable.DefaultSetting = @"";
				colvarIsBundleSellable.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsBundleSellable);

				TableSchema.TableColumn colvarRejectionTypeDescription = new TableSchema.TableColumn(schema);
				colvarRejectionTypeDescription.ColumnName = "RejectionTypeDescription";
				colvarRejectionTypeDescription.DataType = DbType.String;
				colvarRejectionTypeDescription.MaxLength = 1073741823;
				colvarRejectionTypeDescription.AutoIncrement = false;
				colvarRejectionTypeDescription.IsNullable = true;
				colvarRejectionTypeDescription.IsPrimaryKey = false;
				colvarRejectionTypeDescription.IsForeignKey = false;
				colvarRejectionTypeDescription.IsReadOnly = false;
				colvarRejectionTypeDescription.DefaultSetting = @"";
				colvarRejectionTypeDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRejectionTypeDescription);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_RejectionTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_RejectionType LoadFrom(FE_RejectionType item)
		{
			FE_RejectionType result = new FE_RejectionType();
			if (item.RejectionTypeID != default(int)) {
				result.LoadByKey(item.RejectionTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int RejectionTypeID {
			get { return GetColumnValue<int>(Columns.RejectionTypeID); }
			set {
				SetColumnValue(Columns.RejectionTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionTypeID));
			}
		}
		[DataMember]
		public string RejectionType {
			get { return GetColumnValue<string>(Columns.RejectionType); }
			set {
				SetColumnValue(Columns.RejectionType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionType));
			}
		}
		[DataMember]
		public bool IsPacketSellable {
			get { return GetColumnValue<bool>(Columns.IsPacketSellable); }
			set {
				SetColumnValue(Columns.IsPacketSellable, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsPacketSellable));
			}
		}
		[DataMember]
		public bool IsBundleSellable {
			get { return GetColumnValue<bool>(Columns.IsBundleSellable); }
			set {
				SetColumnValue(Columns.IsBundleSellable, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsBundleSellable));
			}
		}
		[DataMember]
		public string RejectionTypeDescription {
			get { return GetColumnValue<string>(Columns.RejectionTypeDescription); }
			set {
				SetColumnValue(Columns.RejectionTypeDescription, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RejectionTypeDescription));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return RejectionType;
		}

		#region Typed Columns

		public static TableSchema.TableColumn RejectionTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RejectionTypeColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsPacketSellableColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsBundleSellableColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RejectionTypeDescriptionColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string RejectionTypeID = @"RejectionTypeID";
			public static readonly string RejectionType = @"RejectionType";
			public static readonly string IsPacketSellable = @"IsPacketSellable";
			public static readonly string IsBundleSellable = @"IsBundleSellable";
			public static readonly string RejectionTypeDescription = @"RejectionTypeDescription";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return RejectionTypeID; }
		}
		*/

		#region Foreign Collections

		private FE_RejectionCollection _FE_RejectionsCol;
		//Relationship: FK_FE_Rejection_FE_RejectionTypes
		public FE_RejectionCollection FE_RejectionsCol
		{
			get
			{
				if(_FE_RejectionsCol == null) {
					_FE_RejectionsCol = new FE_RejectionCollection();
					_FE_RejectionsCol.LoadAndCloseReader(FE_Rejection.Query()
						.WHERE(FE_Rejection.Columns.RejectionTypeId, RejectionTypeID).ExecuteReader());
				}
				return _FE_RejectionsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_ReplacedAccount class.
	/// </summary>
	[DataContract]
	public partial class FE_ReplacedAccountCollection : ActiveList<FE_ReplacedAccount, FE_ReplacedAccountCollection>
	{
		public static FE_ReplacedAccountCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_ReplacedAccountCollection result = new FE_ReplacedAccountCollection();
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
			foreach (FE_ReplacedAccount item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_ReplacedAccount table.
	/// </summary>
	[DataContract]
	public partial class FE_ReplacedAccount : ActiveRecord<FE_ReplacedAccount>, INotifyPropertyChanged
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

		public FE_ReplacedAccount()
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
				TableSchema.Table schema = new TableSchema.Table("FE_ReplacedAccount", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarReplacedAccountID = new TableSchema.TableColumn(schema);
				colvarReplacedAccountID.ColumnName = "ReplacedAccountID";
				colvarReplacedAccountID.DataType = DbType.Int64;
				colvarReplacedAccountID.MaxLength = 0;
				colvarReplacedAccountID.AutoIncrement = true;
				colvarReplacedAccountID.IsNullable = false;
				colvarReplacedAccountID.IsPrimaryKey = true;
				colvarReplacedAccountID.IsForeignKey = false;
				colvarReplacedAccountID.IsReadOnly = false;
				colvarReplacedAccountID.DefaultSetting = @"";
				colvarReplacedAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReplacedAccountID);

				TableSchema.TableColumn colvarPurchasedAccountId = new TableSchema.TableColumn(schema);
				colvarPurchasedAccountId.ColumnName = "PurchasedAccountId";
				colvarPurchasedAccountId.DataType = DbType.Int64;
				colvarPurchasedAccountId.MaxLength = 0;
				colvarPurchasedAccountId.AutoIncrement = false;
				colvarPurchasedAccountId.IsNullable = false;
				colvarPurchasedAccountId.IsPrimaryKey = false;
				colvarPurchasedAccountId.IsForeignKey = true;
				colvarPurchasedAccountId.IsReadOnly = false;
				colvarPurchasedAccountId.DefaultSetting = @"";
				colvarPurchasedAccountId.ForeignKeyTableName = "FE_PurchasedAccount";
				schema.Columns.Add(colvarPurchasedAccountId);

				TableSchema.TableColumn colvarReplacementAccountId = new TableSchema.TableColumn(schema);
				colvarReplacementAccountId.ColumnName = "ReplacementAccountId";
				colvarReplacementAccountId.DataType = DbType.Int64;
				colvarReplacementAccountId.MaxLength = 0;
				colvarReplacementAccountId.AutoIncrement = false;
				colvarReplacementAccountId.IsNullable = false;
				colvarReplacementAccountId.IsPrimaryKey = false;
				colvarReplacementAccountId.IsForeignKey = false;
				colvarReplacementAccountId.IsReadOnly = false;
				colvarReplacementAccountId.DefaultSetting = @"";
				colvarReplacementAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReplacementAccountId);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = false;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_ReplacedAccount",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_ReplacedAccount LoadFrom(FE_ReplacedAccount item)
		{
			FE_ReplacedAccount result = new FE_ReplacedAccount();
			if (item.ReplacedAccountID != default(long)) {
				result.LoadByKey(item.ReplacedAccountID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ReplacedAccountID {
			get { return GetColumnValue<long>(Columns.ReplacedAccountID); }
			set {
				SetColumnValue(Columns.ReplacedAccountID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReplacedAccountID));
			}
		}
		[DataMember]
		public long PurchasedAccountId {
			get { return GetColumnValue<long>(Columns.PurchasedAccountId); }
			set {
				SetColumnValue(Columns.PurchasedAccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchasedAccountId));
			}
		}
		[DataMember]
		public long ReplacementAccountId {
			get { return GetColumnValue<long>(Columns.ReplacementAccountId); }
			set {
				SetColumnValue(Columns.ReplacementAccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReplacementAccountId));
			}
		}
		[DataMember]
		public long AccountFundingStatusId {
			get { return GetColumnValue<long>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_ReplacedAccount_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		private FE_PurchasedAccount _PurchasedAccount;
		//Relationship: FK_FE_ReplacedAccount_FE_PurchasedAccount
		public FE_PurchasedAccount PurchasedAccount
		{
			get
			{
				if(_PurchasedAccount == null) {
					_PurchasedAccount = FE_PurchasedAccount.FetchByID(this.PurchasedAccountId);
				}
				return _PurchasedAccount;
			}
			set
			{
				SetColumnValue("PurchasedAccountId", value.PurchasedAccountID);
				_PurchasedAccount = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ReplacedAccountID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ReplacedAccountIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchasedAccountIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReplacementAccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ReplacedAccountID = @"ReplacedAccountID";
			public static readonly string PurchasedAccountId = @"PurchasedAccountId";
			public static readonly string ReplacementAccountId = @"ReplacementAccountId";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ReplacedAccountID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_ReturnAction class.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnActionCollection : ActiveList<FE_ReturnAction, FE_ReturnActionCollection>
	{
		public static FE_ReturnActionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_ReturnActionCollection result = new FE_ReturnActionCollection();
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
			foreach (FE_ReturnAction item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_ReturnActions table.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnAction : ActiveRecord<FE_ReturnAction>, INotifyPropertyChanged
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

		public FE_ReturnAction()
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
				TableSchema.Table schema = new TableSchema.Table("FE_ReturnActions", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarReturnActionID = new TableSchema.TableColumn(schema);
				colvarReturnActionID.ColumnName = "ReturnActionID";
				colvarReturnActionID.DataType = DbType.Int64;
				colvarReturnActionID.MaxLength = 0;
				colvarReturnActionID.AutoIncrement = true;
				colvarReturnActionID.IsNullable = false;
				colvarReturnActionID.IsPrimaryKey = true;
				colvarReturnActionID.IsForeignKey = false;
				colvarReturnActionID.IsReadOnly = false;
				colvarReturnActionID.DefaultSetting = @"";
				colvarReturnActionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReturnActionID);

				TableSchema.TableColumn colvarPurchaserId = new TableSchema.TableColumn(schema);
				colvarPurchaserId.ColumnName = "PurchaserId";
				colvarPurchaserId.DataType = DbType.AnsiString;
				colvarPurchaserId.MaxLength = 10;
				colvarPurchaserId.AutoIncrement = false;
				colvarPurchaserId.IsNullable = false;
				colvarPurchaserId.IsPrimaryKey = false;
				colvarPurchaserId.IsForeignKey = true;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "FE_Purchasers";
				schema.Columns.Add(colvarPurchaserId);

				TableSchema.TableColumn colvarReturnAction = new TableSchema.TableColumn(schema);
				colvarReturnAction.ColumnName = "ReturnAction";
				colvarReturnAction.DataType = DbType.String;
				colvarReturnAction.MaxLength = 50;
				colvarReturnAction.AutoIncrement = false;
				colvarReturnAction.IsNullable = false;
				colvarReturnAction.IsPrimaryKey = false;
				colvarReturnAction.IsForeignKey = false;
				colvarReturnAction.IsReadOnly = false;
				colvarReturnAction.DefaultSetting = @"";
				colvarReturnAction.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReturnAction);

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
				colvarModifiedOn.DefaultSetting = @"(getutcdate())";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_ReturnActions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_ReturnAction LoadFrom(FE_ReturnAction item)
		{
			FE_ReturnAction result = new FE_ReturnAction();
			if (item.ReturnActionID != default(long)) {
				result.LoadByKey(item.ReturnActionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ReturnActionID {
			get { return GetColumnValue<long>(Columns.ReturnActionID); }
			set {
				SetColumnValue(Columns.ReturnActionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnActionID));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public string ReturnAction {
			get { return GetColumnValue<string>(Columns.ReturnAction); }
			set {
				SetColumnValue(Columns.ReturnAction, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnAction));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Purchaser _Purchaser;
		//Relationship: FK_FE_ReturnActions_FE_Purchasers
		public FE_Purchaser Purchaser
		{
			get
			{
				if(_Purchaser == null) {
					_Purchaser = FE_Purchaser.FetchByID(this.PurchaserId);
				}
				return _Purchaser;
			}
			set
			{
				SetColumnValue("PurchaserId", value.PurchaserID);
				_Purchaser = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaserId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ReturnActionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReturnActionColumn
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
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
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
			public static readonly string ReturnActionID = @"ReturnActionID";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string ReturnAction = @"ReturnAction";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ReturnActionID; }
		}
		*/

		#region Foreign Collections

		private FE_ReturnManifestCollection _FE_ReturnManifestsCol;
		//Relationship: FK_FE_ReturnManifests_FE_ReturnActions
		public FE_ReturnManifestCollection FE_ReturnManifestsCol
		{
			get
			{
				if(_FE_ReturnManifestsCol == null) {
					_FE_ReturnManifestsCol = new FE_ReturnManifestCollection();
					_FE_ReturnManifestsCol.LoadAndCloseReader(FE_ReturnManifest.Query()
						.WHERE(FE_ReturnManifest.Columns.ReturnActionId, ReturnActionID).ExecuteReader());
				}
				return _FE_ReturnManifestsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_ReturnManifestMonitronicsDetail class.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnManifestMonitronicsDetailCollection : ActiveList<FE_ReturnManifestMonitronicsDetail, FE_ReturnManifestMonitronicsDetailCollection>
	{
		public static FE_ReturnManifestMonitronicsDetailCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_ReturnManifestMonitronicsDetailCollection result = new FE_ReturnManifestMonitronicsDetailCollection();
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
			foreach (FE_ReturnManifestMonitronicsDetail item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_ReturnManifestMonitronicsDetails table.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnManifestMonitronicsDetail : ActiveRecord<FE_ReturnManifestMonitronicsDetail>, INotifyPropertyChanged
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

		public FE_ReturnManifestMonitronicsDetail()
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
				TableSchema.Table schema = new TableSchema.Table("FE_ReturnManifestMonitronicsDetails", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarReturnManifestDetailsID = new TableSchema.TableColumn(schema);
				colvarReturnManifestDetailsID.ColumnName = "ReturnManifestDetailsID";
				colvarReturnManifestDetailsID.DataType = DbType.Int64;
				colvarReturnManifestDetailsID.MaxLength = 0;
				colvarReturnManifestDetailsID.AutoIncrement = true;
				colvarReturnManifestDetailsID.IsNullable = false;
				colvarReturnManifestDetailsID.IsPrimaryKey = true;
				colvarReturnManifestDetailsID.IsForeignKey = false;
				colvarReturnManifestDetailsID.IsReadOnly = false;
				colvarReturnManifestDetailsID.DefaultSetting = @"";
				colvarReturnManifestDetailsID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReturnManifestDetailsID);

				TableSchema.TableColumn colvarReturnManifestId = new TableSchema.TableColumn(schema);
				colvarReturnManifestId.ColumnName = "ReturnManifestId";
				colvarReturnManifestId.DataType = DbType.Int64;
				colvarReturnManifestId.MaxLength = 0;
				colvarReturnManifestId.AutoIncrement = false;
				colvarReturnManifestId.IsNullable = false;
				colvarReturnManifestId.IsPrimaryKey = false;
				colvarReturnManifestId.IsForeignKey = true;
				colvarReturnManifestId.IsReadOnly = false;
				colvarReturnManifestId.DefaultSetting = @"";
				colvarReturnManifestId.ForeignKeyTableName = "FE_ReturnManifests";
				schema.Columns.Add(colvarReturnManifestId);

				TableSchema.TableColumn colvarAccountStatusEventId = new TableSchema.TableColumn(schema);
				colvarAccountStatusEventId.ColumnName = "AccountStatusEventId";
				colvarAccountStatusEventId.DataType = DbType.Int64;
				colvarAccountStatusEventId.MaxLength = 0;
				colvarAccountStatusEventId.AutoIncrement = false;
				colvarAccountStatusEventId.IsNullable = true;
				colvarAccountStatusEventId.IsPrimaryKey = false;
				colvarAccountStatusEventId.IsForeignKey = false;
				colvarAccountStatusEventId.IsReadOnly = false;
				colvarAccountStatusEventId.DefaultSetting = @"";
				colvarAccountStatusEventId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountStatusEventId);

				TableSchema.TableColumn colvarDLRNumber = new TableSchema.TableColumn(schema);
				colvarDLRNumber.ColumnName = "DLRNumber";
				colvarDLRNumber.DataType = DbType.AnsiString;
				colvarDLRNumber.MaxLength = 20;
				colvarDLRNumber.AutoIncrement = false;
				colvarDLRNumber.IsNullable = true;
				colvarDLRNumber.IsPrimaryKey = false;
				colvarDLRNumber.IsForeignKey = false;
				colvarDLRNumber.IsReadOnly = false;
				colvarDLRNumber.DefaultSetting = @"";
				colvarDLRNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLRNumber);

				TableSchema.TableColumn colvarAccountId = new TableSchema.TableColumn(schema);
				colvarAccountId.ColumnName = "AccountId";
				colvarAccountId.DataType = DbType.Int64;
				colvarAccountId.MaxLength = 0;
				colvarAccountId.AutoIncrement = false;
				colvarAccountId.IsNullable = true;
				colvarAccountId.IsPrimaryKey = false;
				colvarAccountId.IsForeignKey = false;
				colvarAccountId.IsReadOnly = false;
				colvarAccountId.DefaultSetting = @"";
				colvarAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountId);

				TableSchema.TableColumn colvarCSID = new TableSchema.TableColumn(schema);
				colvarCSID.ColumnName = "CSID";
				colvarCSID.DataType = DbType.AnsiString;
				colvarCSID.MaxLength = 15;
				colvarCSID.AutoIncrement = false;
				colvarCSID.IsNullable = false;
				colvarCSID.IsPrimaryKey = false;
				colvarCSID.IsForeignKey = false;
				colvarCSID.IsReadOnly = false;
				colvarCSID.DefaultSetting = @"";
				colvarCSID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCSID);

				TableSchema.TableColumn colvarSubscriberNumber = new TableSchema.TableColumn(schema);
				colvarSubscriberNumber.ColumnName = "SubscriberNumber";
				colvarSubscriberNumber.DataType = DbType.AnsiString;
				colvarSubscriberNumber.MaxLength = 15;
				colvarSubscriberNumber.AutoIncrement = false;
				colvarSubscriberNumber.IsNullable = true;
				colvarSubscriberNumber.IsPrimaryKey = false;
				colvarSubscriberNumber.IsForeignKey = false;
				colvarSubscriberNumber.IsReadOnly = false;
				colvarSubscriberNumber.DefaultSetting = @"";
				colvarSubscriberNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubscriberNumber);

				TableSchema.TableColumn colvarContractNumber = new TableSchema.TableColumn(schema);
				colvarContractNumber.ColumnName = "ContractNumber";
				colvarContractNumber.DataType = DbType.String;
				colvarContractNumber.MaxLength = 15;
				colvarContractNumber.AutoIncrement = false;
				colvarContractNumber.IsNullable = true;
				colvarContractNumber.IsPrimaryKey = false;
				colvarContractNumber.IsForeignKey = false;
				colvarContractNumber.IsReadOnly = false;
				colvarContractNumber.DefaultSetting = @"";
				colvarContractNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNumber);

				TableSchema.TableColumn colvarFullname = new TableSchema.TableColumn(schema);
				colvarFullname.ColumnName = "Fullname";
				colvarFullname.DataType = DbType.String;
				colvarFullname.MaxLength = 100;
				colvarFullname.AutoIncrement = false;
				colvarFullname.IsNullable = true;
				colvarFullname.IsPrimaryKey = false;
				colvarFullname.IsForeignKey = false;
				colvarFullname.IsReadOnly = false;
				colvarFullname.DefaultSetting = @"";
				colvarFullname.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullname);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_ReturnManifestMonitronicsDetails",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_ReturnManifestMonitronicsDetail LoadFrom(FE_ReturnManifestMonitronicsDetail item)
		{
			FE_ReturnManifestMonitronicsDetail result = new FE_ReturnManifestMonitronicsDetail();
			if (item.ReturnManifestDetailsID != default(long)) {
				result.LoadByKey(item.ReturnManifestDetailsID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ReturnManifestDetailsID {
			get { return GetColumnValue<long>(Columns.ReturnManifestDetailsID); }
			set {
				SetColumnValue(Columns.ReturnManifestDetailsID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnManifestDetailsID));
			}
		}
		[DataMember]
		public long ReturnManifestId {
			get { return GetColumnValue<long>(Columns.ReturnManifestId); }
			set {
				SetColumnValue(Columns.ReturnManifestId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnManifestId));
			}
		}
		[DataMember]
		public long? AccountStatusEventId {
			get { return GetColumnValue<long?>(Columns.AccountStatusEventId); }
			set {
				SetColumnValue(Columns.AccountStatusEventId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountStatusEventId));
			}
		}
		[DataMember]
		public string DLRNumber {
			get { return GetColumnValue<string>(Columns.DLRNumber); }
			set {
				SetColumnValue(Columns.DLRNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DLRNumber));
			}
		}
		[DataMember]
		public long? AccountId {
			get { return GetColumnValue<long?>(Columns.AccountId); }
			set {
				SetColumnValue(Columns.AccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountId));
			}
		}
		[DataMember]
		public string CSID {
			get { return GetColumnValue<string>(Columns.CSID); }
			set {
				SetColumnValue(Columns.CSID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CSID));
			}
		}
		[DataMember]
		public string SubscriberNumber {
			get { return GetColumnValue<string>(Columns.SubscriberNumber); }
			set {
				SetColumnValue(Columns.SubscriberNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubscriberNumber));
			}
		}
		[DataMember]
		public string ContractNumber {
			get { return GetColumnValue<string>(Columns.ContractNumber); }
			set {
				SetColumnValue(Columns.ContractNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContractNumber));
			}
		}
		[DataMember]
		public string Fullname {
			get { return GetColumnValue<string>(Columns.Fullname); }
			set {
				SetColumnValue(Columns.Fullname, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Fullname));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_ReturnManifest _ReturnManifest;
		//Relationship: FK_FE_ReturnManifestMonitronicsDetails_FE_ReturnManifests
		public FE_ReturnManifest ReturnManifest
		{
			get
			{
				if(_ReturnManifest == null) {
					_ReturnManifest = FE_ReturnManifest.FetchByID(this.ReturnManifestId);
				}
				return _ReturnManifest;
			}
			set
			{
				SetColumnValue("ReturnManifestId", value.ReturnManifestID);
				_ReturnManifest = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ReturnManifestDetailsID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ReturnManifestDetailsIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ReturnManifestIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountStatusEventIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DLRNumberColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CSIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SubscriberNumberColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ContractNumberColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FullnameColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ReturnManifestDetailsID = @"ReturnManifestDetailsID";
			public static readonly string ReturnManifestId = @"ReturnManifestId";
			public static readonly string AccountStatusEventId = @"AccountStatusEventId";
			public static readonly string DLRNumber = @"DLRNumber";
			public static readonly string AccountId = @"AccountId";
			public static readonly string CSID = @"CSID";
			public static readonly string SubscriberNumber = @"SubscriberNumber";
			public static readonly string ContractNumber = @"ContractNumber";
			public static readonly string Fullname = @"Fullname";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ReturnManifestDetailsID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_ReturnManifest class.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnManifestCollection : ActiveList<FE_ReturnManifest, FE_ReturnManifestCollection>
	{
		public static FE_ReturnManifestCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_ReturnManifestCollection result = new FE_ReturnManifestCollection();
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
			foreach (FE_ReturnManifest item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_ReturnManifests table.
	/// </summary>
	[DataContract]
	public partial class FE_ReturnManifest : ActiveRecord<FE_ReturnManifest>, INotifyPropertyChanged
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

		public FE_ReturnManifest()
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
				TableSchema.Table schema = new TableSchema.Table("FE_ReturnManifests", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarReturnManifestID = new TableSchema.TableColumn(schema);
				colvarReturnManifestID.ColumnName = "ReturnManifestID";
				colvarReturnManifestID.DataType = DbType.Int64;
				colvarReturnManifestID.MaxLength = 0;
				colvarReturnManifestID.AutoIncrement = true;
				colvarReturnManifestID.IsNullable = false;
				colvarReturnManifestID.IsPrimaryKey = true;
				colvarReturnManifestID.IsForeignKey = false;
				colvarReturnManifestID.IsReadOnly = false;
				colvarReturnManifestID.DefaultSetting = @"";
				colvarReturnManifestID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReturnManifestID);

				TableSchema.TableColumn colvarPurchaserId = new TableSchema.TableColumn(schema);
				colvarPurchaserId.ColumnName = "PurchaserId";
				colvarPurchaserId.DataType = DbType.AnsiString;
				colvarPurchaserId.MaxLength = 10;
				colvarPurchaserId.AutoIncrement = false;
				colvarPurchaserId.IsNullable = false;
				colvarPurchaserId.IsPrimaryKey = false;
				colvarPurchaserId.IsForeignKey = true;
				colvarPurchaserId.IsReadOnly = false;
				colvarPurchaserId.DefaultSetting = @"";
				colvarPurchaserId.ForeignKeyTableName = "FE_Purchasers";
				schema.Columns.Add(colvarPurchaserId);

				TableSchema.TableColumn colvarReturnActionId = new TableSchema.TableColumn(schema);
				colvarReturnActionId.ColumnName = "ReturnActionId";
				colvarReturnActionId.DataType = DbType.Int64;
				colvarReturnActionId.MaxLength = 0;
				colvarReturnActionId.AutoIncrement = false;
				colvarReturnActionId.IsNullable = false;
				colvarReturnActionId.IsPrimaryKey = false;
				colvarReturnActionId.IsForeignKey = true;
				colvarReturnActionId.IsReadOnly = false;
				colvarReturnActionId.DefaultSetting = @"";
				colvarReturnActionId.ForeignKeyTableName = "FE_ReturnActions";
				schema.Columns.Add(colvarReturnActionId);

				TableSchema.TableColumn colvarTrackingNumber = new TableSchema.TableColumn(schema);
				colvarTrackingNumber.ColumnName = "TrackingNumber";
				colvarTrackingNumber.DataType = DbType.AnsiString;
				colvarTrackingNumber.MaxLength = 30;
				colvarTrackingNumber.AutoIncrement = false;
				colvarTrackingNumber.IsNullable = true;
				colvarTrackingNumber.IsPrimaryKey = false;
				colvarTrackingNumber.IsForeignKey = false;
				colvarTrackingNumber.IsReadOnly = false;
				colvarTrackingNumber.DefaultSetting = @"";
				colvarTrackingNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrackingNumber);

				TableSchema.TableColumn colvarReceivedBy = new TableSchema.TableColumn(schema);
				colvarReceivedBy.ColumnName = "ReceivedBy";
				colvarReceivedBy.DataType = DbType.String;
				colvarReceivedBy.MaxLength = 50;
				colvarReceivedBy.AutoIncrement = false;
				colvarReceivedBy.IsNullable = true;
				colvarReceivedBy.IsPrimaryKey = false;
				colvarReceivedBy.IsForeignKey = false;
				colvarReceivedBy.IsReadOnly = false;
				colvarReceivedBy.DefaultSetting = @"";
				colvarReceivedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReceivedBy);

				TableSchema.TableColumn colvarReceivedDate = new TableSchema.TableColumn(schema);
				colvarReceivedDate.ColumnName = "ReceivedDate";
				colvarReceivedDate.DataType = DbType.DateTime;
				colvarReceivedDate.MaxLength = 0;
				colvarReceivedDate.AutoIncrement = false;
				colvarReceivedDate.IsNullable = true;
				colvarReceivedDate.IsPrimaryKey = false;
				colvarReceivedDate.IsForeignKey = false;
				colvarReceivedDate.IsReadOnly = false;
				colvarReceivedDate.DefaultSetting = @"";
				colvarReceivedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReceivedDate);

				TableSchema.TableColumn colvarManifestCreatedBy = new TableSchema.TableColumn(schema);
				colvarManifestCreatedBy.ColumnName = "ManifestCreatedBy";
				colvarManifestCreatedBy.DataType = DbType.String;
				colvarManifestCreatedBy.MaxLength = 50;
				colvarManifestCreatedBy.AutoIncrement = false;
				colvarManifestCreatedBy.IsNullable = true;
				colvarManifestCreatedBy.IsPrimaryKey = false;
				colvarManifestCreatedBy.IsForeignKey = false;
				colvarManifestCreatedBy.IsReadOnly = false;
				colvarManifestCreatedBy.DefaultSetting = @"";
				colvarManifestCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManifestCreatedBy);

				TableSchema.TableColumn colvarManifestCreatedDate = new TableSchema.TableColumn(schema);
				colvarManifestCreatedDate.ColumnName = "ManifestCreatedDate";
				colvarManifestCreatedDate.DataType = DbType.DateTime;
				colvarManifestCreatedDate.MaxLength = 0;
				colvarManifestCreatedDate.AutoIncrement = false;
				colvarManifestCreatedDate.IsNullable = true;
				colvarManifestCreatedDate.IsPrimaryKey = false;
				colvarManifestCreatedDate.IsForeignKey = false;
				colvarManifestCreatedDate.IsReadOnly = false;
				colvarManifestCreatedDate.DefaultSetting = @"";
				colvarManifestCreatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManifestCreatedDate);

				TableSchema.TableColumn colvarNumberOfContracts = new TableSchema.TableColumn(schema);
				colvarNumberOfContracts.ColumnName = "NumberOfContracts";
				colvarNumberOfContracts.DataType = DbType.Int32;
				colvarNumberOfContracts.MaxLength = 0;
				colvarNumberOfContracts.AutoIncrement = false;
				colvarNumberOfContracts.IsNullable = true;
				colvarNumberOfContracts.IsPrimaryKey = false;
				colvarNumberOfContracts.IsForeignKey = false;
				colvarNumberOfContracts.IsReadOnly = false;
				colvarNumberOfContracts.DefaultSetting = @"";
				colvarNumberOfContracts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberOfContracts);

				TableSchema.TableColumn colvarCompletedDate = new TableSchema.TableColumn(schema);
				colvarCompletedDate.ColumnName = "CompletedDate";
				colvarCompletedDate.DataType = DbType.DateTime;
				colvarCompletedDate.MaxLength = 0;
				colvarCompletedDate.AutoIncrement = false;
				colvarCompletedDate.IsNullable = true;
				colvarCompletedDate.IsPrimaryKey = false;
				colvarCompletedDate.IsForeignKey = false;
				colvarCompletedDate.IsReadOnly = false;
				colvarCompletedDate.DefaultSetting = @"";
				colvarCompletedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompletedDate);

				TableSchema.TableColumn colvarManifest = new TableSchema.TableColumn(schema);
				colvarManifest.ColumnName = "Manifest";
				colvarManifest.DataType = DbType.Binary;
				colvarManifest.MaxLength = 2147483647;
				colvarManifest.AutoIncrement = false;
				colvarManifest.IsNullable = true;
				colvarManifest.IsPrimaryKey = false;
				colvarManifest.IsForeignKey = false;
				colvarManifest.IsReadOnly = false;
				colvarManifest.DefaultSetting = @"";
				colvarManifest.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManifest);

				TableSchema.TableColumn colvarManifestFileName = new TableSchema.TableColumn(schema);
				colvarManifestFileName.ColumnName = "ManifestFileName";
				colvarManifestFileName.DataType = DbType.String;
				colvarManifestFileName.MaxLength = 256;
				colvarManifestFileName.AutoIncrement = false;
				colvarManifestFileName.IsNullable = true;
				colvarManifestFileName.IsPrimaryKey = false;
				colvarManifestFileName.IsForeignKey = false;
				colvarManifestFileName.IsReadOnly = false;
				colvarManifestFileName.DefaultSetting = @"";
				colvarManifestFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManifestFileName);

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
				colvarModifiedOn.DefaultSetting = @"(getutcdate())";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_ReturnManifests",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_ReturnManifest LoadFrom(FE_ReturnManifest item)
		{
			FE_ReturnManifest result = new FE_ReturnManifest();
			if (item.ReturnManifestID != default(long)) {
				result.LoadByKey(item.ReturnManifestID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ReturnManifestID {
			get { return GetColumnValue<long>(Columns.ReturnManifestID); }
			set {
				SetColumnValue(Columns.ReturnManifestID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnManifestID));
			}
		}
		[DataMember]
		public string PurchaserId {
			get { return GetColumnValue<string>(Columns.PurchaserId); }
			set {
				SetColumnValue(Columns.PurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaserId));
			}
		}
		[DataMember]
		public long ReturnActionId {
			get { return GetColumnValue<long>(Columns.ReturnActionId); }
			set {
				SetColumnValue(Columns.ReturnActionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReturnActionId));
			}
		}
		[DataMember]
		public string TrackingNumber {
			get { return GetColumnValue<string>(Columns.TrackingNumber); }
			set {
				SetColumnValue(Columns.TrackingNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TrackingNumber));
			}
		}
		[DataMember]
		public string ReceivedBy {
			get { return GetColumnValue<string>(Columns.ReceivedBy); }
			set {
				SetColumnValue(Columns.ReceivedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReceivedBy));
			}
		}
		[DataMember]
		public DateTime? ReceivedDate {
			get { return GetColumnValue<DateTime?>(Columns.ReceivedDate); }
			set {
				SetColumnValue(Columns.ReceivedDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ReceivedDate));
			}
		}
		[DataMember]
		public string ManifestCreatedBy {
			get { return GetColumnValue<string>(Columns.ManifestCreatedBy); }
			set {
				SetColumnValue(Columns.ManifestCreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ManifestCreatedBy));
			}
		}
		[DataMember]
		public DateTime? ManifestCreatedDate {
			get { return GetColumnValue<DateTime?>(Columns.ManifestCreatedDate); }
			set {
				SetColumnValue(Columns.ManifestCreatedDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ManifestCreatedDate));
			}
		}
		[DataMember]
		public int? NumberOfContracts {
			get { return GetColumnValue<int?>(Columns.NumberOfContracts); }
			set {
				SetColumnValue(Columns.NumberOfContracts, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.NumberOfContracts));
			}
		}
		[DataMember]
		public DateTime? CompletedDate {
			get { return GetColumnValue<DateTime?>(Columns.CompletedDate); }
			set {
				SetColumnValue(Columns.CompletedDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CompletedDate));
			}
		}
		[DataMember]
		public byte[] Manifest {
			get { return GetColumnValue<byte[]>(Columns.Manifest); }
			set {
				SetColumnValue(Columns.Manifest, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Manifest));
			}
		}
		[DataMember]
		public string ManifestFileName {
			get { return GetColumnValue<string>(Columns.ManifestFileName); }
			set {
				SetColumnValue(Columns.ManifestFileName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ManifestFileName));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Purchaser _Purchaser;
		//Relationship: FK_FE_ReturnManifests_FE_Purchasers
		public FE_Purchaser Purchaser
		{
			get
			{
				if(_Purchaser == null) {
					_Purchaser = FE_Purchaser.FetchByID(this.PurchaserId);
				}
				return _Purchaser;
			}
			set
			{
				SetColumnValue("PurchaserId", value.PurchaserID);
				_Purchaser = value;
			}
		}

		private FE_ReturnAction _ReturnAction;
		//Relationship: FK_FE_ReturnManifests_FE_ReturnActions
		public FE_ReturnAction ReturnAction
		{
			get
			{
				if(_ReturnAction == null) {
					_ReturnAction = FE_ReturnAction.FetchByID(this.ReturnActionId);
				}
				return _ReturnAction;
			}
			set
			{
				SetColumnValue("ReturnActionId", value.ReturnActionID);
				_ReturnAction = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaserId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ReturnManifestIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaserIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReturnActionIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TrackingNumberColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ReceivedByColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ReceivedDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ManifestCreatedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ManifestCreatedDateColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn NumberOfContractsColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CompletedDateColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn ManifestColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ManifestFileNameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[15]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ReturnManifestID = @"ReturnManifestID";
			public static readonly string PurchaserId = @"PurchaserId";
			public static readonly string ReturnActionId = @"ReturnActionId";
			public static readonly string TrackingNumber = @"TrackingNumber";
			public static readonly string ReceivedBy = @"ReceivedBy";
			public static readonly string ReceivedDate = @"ReceivedDate";
			public static readonly string ManifestCreatedBy = @"ManifestCreatedBy";
			public static readonly string ManifestCreatedDate = @"ManifestCreatedDate";
			public static readonly string NumberOfContracts = @"NumberOfContracts";
			public static readonly string CompletedDate = @"CompletedDate";
			public static readonly string Manifest = @"Manifest";
			public static readonly string ManifestFileName = @"ManifestFileName";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ReturnManifestID; }
		}
		*/

		#region Foreign Collections

		private FE_ReturnManifestMonitronicsDetailCollection _FE_ReturnManifestMonitronicsDetailsCol;
		//Relationship: FK_FE_ReturnManifestMonitronicsDetails_FE_ReturnManifests
		public FE_ReturnManifestMonitronicsDetailCollection FE_ReturnManifestMonitronicsDetailsCol
		{
			get
			{
				if(_FE_ReturnManifestMonitronicsDetailsCol == null) {
					_FE_ReturnManifestMonitronicsDetailsCol = new FE_ReturnManifestMonitronicsDetailCollection();
					_FE_ReturnManifestMonitronicsDetailsCol.LoadAndCloseReader(FE_ReturnManifestMonitronicsDetail.Query()
						.WHERE(FE_ReturnManifestMonitronicsDetail.Columns.ReturnManifestId, ReturnManifestID).ExecuteReader());
				}
				return _FE_ReturnManifestMonitronicsDetailsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the FE_SubmittedToPurchaserAccount class.
	/// </summary>
	[DataContract]
	public partial class FE_SubmittedToPurchaserAccountCollection : ActiveList<FE_SubmittedToPurchaserAccount, FE_SubmittedToPurchaserAccountCollection>
	{
		public static FE_SubmittedToPurchaserAccountCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_SubmittedToPurchaserAccountCollection result = new FE_SubmittedToPurchaserAccountCollection();
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
			foreach (FE_SubmittedToPurchaserAccount item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_SubmittedToPurchaserAccount table.
	/// </summary>
	[DataContract]
	public partial class FE_SubmittedToPurchaserAccount : ActiveRecord<FE_SubmittedToPurchaserAccount>, INotifyPropertyChanged
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

		public FE_SubmittedToPurchaserAccount()
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
				TableSchema.Table schema = new TableSchema.Table("FE_SubmittedToPurchaserAccount", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSubmittedToPurchaserId = new TableSchema.TableColumn(schema);
				colvarSubmittedToPurchaserId.ColumnName = "SubmittedToPurchaserId";
				colvarSubmittedToPurchaserId.DataType = DbType.Int64;
				colvarSubmittedToPurchaserId.MaxLength = 0;
				colvarSubmittedToPurchaserId.AutoIncrement = true;
				colvarSubmittedToPurchaserId.IsNullable = false;
				colvarSubmittedToPurchaserId.IsPrimaryKey = true;
				colvarSubmittedToPurchaserId.IsForeignKey = false;
				colvarSubmittedToPurchaserId.IsReadOnly = false;
				colvarSubmittedToPurchaserId.DefaultSetting = @"";
				colvarSubmittedToPurchaserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubmittedToPurchaserId);

				TableSchema.TableColumn colvarPacketItemId = new TableSchema.TableColumn(schema);
				colvarPacketItemId.ColumnName = "PacketItemId";
				colvarPacketItemId.DataType = DbType.Int64;
				colvarPacketItemId.MaxLength = 0;
				colvarPacketItemId.AutoIncrement = false;
				colvarPacketItemId.IsNullable = false;
				colvarPacketItemId.IsPrimaryKey = false;
				colvarPacketItemId.IsForeignKey = true;
				colvarPacketItemId.IsReadOnly = false;
				colvarPacketItemId.DefaultSetting = @"";
				colvarPacketItemId.ForeignKeyTableName = "FE_PacketItems";
				schema.Columns.Add(colvarPacketItemId);

				TableSchema.TableColumn colvarAccountFundingStatusId = new TableSchema.TableColumn(schema);
				colvarAccountFundingStatusId.ColumnName = "AccountFundingStatusId";
				colvarAccountFundingStatusId.DataType = DbType.Int64;
				colvarAccountFundingStatusId.MaxLength = 0;
				colvarAccountFundingStatusId.AutoIncrement = false;
				colvarAccountFundingStatusId.IsNullable = true;
				colvarAccountFundingStatusId.IsPrimaryKey = false;
				colvarAccountFundingStatusId.IsForeignKey = true;
				colvarAccountFundingStatusId.IsReadOnly = false;
				colvarAccountFundingStatusId.DefaultSetting = @"";
				colvarAccountFundingStatusId.ForeignKeyTableName = "FE_AccountFundingStatus";
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
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_SubmittedToPurchaserAccount",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_SubmittedToPurchaserAccount LoadFrom(FE_SubmittedToPurchaserAccount item)
		{
			FE_SubmittedToPurchaserAccount result = new FE_SubmittedToPurchaserAccount();
			if (item.SubmittedToPurchaserId != default(long)) {
				result.LoadByKey(item.SubmittedToPurchaserId);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long SubmittedToPurchaserId {
			get { return GetColumnValue<long>(Columns.SubmittedToPurchaserId); }
			set {
				SetColumnValue(Columns.SubmittedToPurchaserId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SubmittedToPurchaserId));
			}
		}
		[DataMember]
		public long PacketItemId {
			get { return GetColumnValue<long>(Columns.PacketItemId); }
			set {
				SetColumnValue(Columns.PacketItemId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PacketItemId));
			}
		}
		[DataMember]
		public long? AccountFundingStatusId {
			get { return GetColumnValue<long?>(Columns.AccountFundingStatusId); }
			set {
				SetColumnValue(Columns.AccountFundingStatusId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AccountFundingStatusId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private FE_AccountFundingStatus _AccountFundingStatus;
		//Relationship: FK_FE_SubmittedToPurchaserAccount_FE_AccountFundingStatus
		public FE_AccountFundingStatus AccountFundingStatus
		{
			get
			{
				if(_AccountFundingStatus == null) {
					_AccountFundingStatus = FE_AccountFundingStatus.FetchByID(this.AccountFundingStatusId);
				}
				return _AccountFundingStatus;
			}
			set
			{
				SetColumnValue("AccountFundingStatusId", value.AccountFundingStatusID);
				_AccountFundingStatus = value;
			}
		}

		private FE_PacketItem _PacketItem;
		//Relationship: FK_FE_SubmittedToPurchaserAccount_FE_PacketItems
		public FE_PacketItem PacketItem
		{
			get
			{
				if(_PacketItem == null) {
					_PacketItem = FE_PacketItem.FetchByID(this.PacketItemId);
				}
				return _PacketItem;
			}
			set
			{
				SetColumnValue("PacketItemId", value.PacketItemID);
				_PacketItem = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return SubmittedToPurchaserId.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn SubmittedToPurchaserIdColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PacketItemIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountFundingStatusIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SubmittedToPurchaserId = @"SubmittedToPurchaserId";
			public static readonly string PacketItemId = @"PacketItemId";
			public static readonly string AccountFundingStatusId = @"AccountFundingStatusId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return SubmittedToPurchaserId; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the FE_TrackingNumber class.
	/// </summary>
	[DataContract]
	public partial class FE_TrackingNumberCollection : ActiveList<FE_TrackingNumber, FE_TrackingNumberCollection>
	{
		public static FE_TrackingNumberCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			FE_TrackingNumberCollection result = new FE_TrackingNumberCollection();
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
			foreach (FE_TrackingNumber item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the FE_TrackingNumber table.
	/// </summary>
	[DataContract]
	public partial class FE_TrackingNumber : ActiveRecord<FE_TrackingNumber>, INotifyPropertyChanged
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

		public FE_TrackingNumber()
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
				TableSchema.Table schema = new TableSchema.Table("FE_TrackingNumber", TableType.Table, DataService.GetInstance("NxseFundingProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTrackingNumberID = new TableSchema.TableColumn(schema);
				colvarTrackingNumberID.ColumnName = "TrackingNumberID";
				colvarTrackingNumberID.DataType = DbType.Int64;
				colvarTrackingNumberID.MaxLength = 0;
				colvarTrackingNumberID.AutoIncrement = true;
				colvarTrackingNumberID.IsNullable = false;
				colvarTrackingNumberID.IsPrimaryKey = true;
				colvarTrackingNumberID.IsForeignKey = false;
				colvarTrackingNumberID.IsReadOnly = false;
				colvarTrackingNumberID.DefaultSetting = @"";
				colvarTrackingNumberID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrackingNumberID);

				TableSchema.TableColumn colvarBundleId = new TableSchema.TableColumn(schema);
				colvarBundleId.ColumnName = "BundleId";
				colvarBundleId.DataType = DbType.Int32;
				colvarBundleId.MaxLength = 0;
				colvarBundleId.AutoIncrement = false;
				colvarBundleId.IsNullable = false;
				colvarBundleId.IsPrimaryKey = false;
				colvarBundleId.IsForeignKey = true;
				colvarBundleId.IsReadOnly = false;
				colvarBundleId.DefaultSetting = @"";
				colvarBundleId.ForeignKeyTableName = "FE_Bundles";
				schema.Columns.Add(colvarBundleId);

				TableSchema.TableColumn colvarTrackingNumber = new TableSchema.TableColumn(schema);
				colvarTrackingNumber.ColumnName = "TrackingNumber";
				colvarTrackingNumber.DataType = DbType.String;
				colvarTrackingNumber.MaxLength = 50;
				colvarTrackingNumber.AutoIncrement = false;
				colvarTrackingNumber.IsNullable = false;
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

				BaseSchema = schema;
				DataService.Providers["NxseFundingProvider"].AddSchema("FE_TrackingNumber",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static FE_TrackingNumber LoadFrom(FE_TrackingNumber item)
		{
			FE_TrackingNumber result = new FE_TrackingNumber();
			if (item.TrackingNumberID != default(long)) {
				result.LoadByKey(item.TrackingNumberID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long TrackingNumberID {
			get { return GetColumnValue<long>(Columns.TrackingNumberID); }
			set {
				SetColumnValue(Columns.TrackingNumberID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TrackingNumberID));
			}
		}
		[DataMember]
		public int BundleId {
			get { return GetColumnValue<int>(Columns.BundleId); }
			set {
				SetColumnValue(Columns.BundleId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.BundleId));
			}
		}
		[DataMember]
		public string TrackingNumber {
			get { return GetColumnValue<string>(Columns.TrackingNumber); }
			set {
				SetColumnValue(Columns.TrackingNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TrackingNumber));
			}
		}
		[DataMember]
		public DateTime? DeliveryDate {
			get { return GetColumnValue<DateTime?>(Columns.DeliveryDate); }
			set {
				SetColumnValue(Columns.DeliveryDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeliveryDate));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private FE_Bundle _Bundle;
		//Relationship: FK_FE_TrackingNumber_FE_Bundles
		public FE_Bundle Bundle
		{
			get
			{
				if(_Bundle == null) {
					_Bundle = FE_Bundle.FetchByID(this.BundleId);
				}
				return _Bundle;
			}
			set
			{
				SetColumnValue("BundleId", value.BundleID);
				_Bundle = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return TrackingNumberID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TrackingNumberIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn BundleIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TrackingNumberColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DeliveryDateColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TrackingNumberID = @"TrackingNumberID";
			public static readonly string BundleId = @"BundleId";
			public static readonly string TrackingNumber = @"TrackingNumber";
			public static readonly string DeliveryDate = @"DeliveryDate";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return TrackingNumberID; }
		}
		*/


	}
}
