


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

namespace NXS.Data.Inventory
{
	/// <summary>
	/// Strongly-typed collection for the EP_Device class.
	/// </summary>
	[DataContract]
	public partial class EP_DeviceCollection : ActiveList<EP_Device, EP_DeviceCollection>
	{
		public static EP_DeviceCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			EP_DeviceCollection result = new EP_DeviceCollection();
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
			foreach (EP_Device item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the EP_Devices table.
	/// </summary>
	[DataContract]
	public partial class EP_Device : ActiveRecord<EP_Device>, INotifyPropertyChanged
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

		public EP_Device()
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
				TableSchema.Table schema = new TableSchema.Table("EP_Devices", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarDeviceID = new TableSchema.TableColumn(schema);
				colvarDeviceID.ColumnName = "DeviceID";
				colvarDeviceID.DataType = DbType.Int32;
				colvarDeviceID.MaxLength = 0;
				colvarDeviceID.AutoIncrement = true;
				colvarDeviceID.IsNullable = false;
				colvarDeviceID.IsPrimaryKey = true;
				colvarDeviceID.IsForeignKey = false;
				colvarDeviceID.IsReadOnly = false;
				colvarDeviceID.DefaultSetting = @"";
				colvarDeviceID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeviceID);

				TableSchema.TableColumn colvarEquipmentManufacturerId = new TableSchema.TableColumn(schema);
				colvarEquipmentManufacturerId.ColumnName = "EquipmentManufacturerId";
				colvarEquipmentManufacturerId.DataType = DbType.AnsiString;
				colvarEquipmentManufacturerId.MaxLength = 20;
				colvarEquipmentManufacturerId.AutoIncrement = false;
				colvarEquipmentManufacturerId.IsNullable = false;
				colvarEquipmentManufacturerId.IsPrimaryKey = false;
				colvarEquipmentManufacturerId.IsForeignKey = true;
				colvarEquipmentManufacturerId.IsReadOnly = false;
				colvarEquipmentManufacturerId.DefaultSetting = @"";
				colvarEquipmentManufacturerId.ForeignKeyTableName = "EP_EquipmentManufacturers";
				schema.Columns.Add(colvarEquipmentManufacturerId);

				TableSchema.TableColumn colvarEquipmentTypeId = new TableSchema.TableColumn(schema);
				colvarEquipmentTypeId.ColumnName = "EquipmentTypeId";
				colvarEquipmentTypeId.DataType = DbType.AnsiString;
				colvarEquipmentTypeId.MaxLength = 20;
				colvarEquipmentTypeId.AutoIncrement = false;
				colvarEquipmentTypeId.IsNullable = false;
				colvarEquipmentTypeId.IsPrimaryKey = false;
				colvarEquipmentTypeId.IsForeignKey = true;
				colvarEquipmentTypeId.IsReadOnly = false;
				colvarEquipmentTypeId.DefaultSetting = @"";
				colvarEquipmentTypeId.ForeignKeyTableName = "EP_EquipmentTypes";
				schema.Columns.Add(colvarEquipmentTypeId);

				TableSchema.TableColumn colvarEquipmentItemTypeId = new TableSchema.TableColumn(schema);
				colvarEquipmentItemTypeId.ColumnName = "EquipmentItemTypeId";
				colvarEquipmentItemTypeId.DataType = DbType.AnsiString;
				colvarEquipmentItemTypeId.MaxLength = 20;
				colvarEquipmentItemTypeId.AutoIncrement = false;
				colvarEquipmentItemTypeId.IsNullable = false;
				colvarEquipmentItemTypeId.IsPrimaryKey = false;
				colvarEquipmentItemTypeId.IsForeignKey = true;
				colvarEquipmentItemTypeId.IsReadOnly = false;
				colvarEquipmentItemTypeId.DefaultSetting = @"";
				colvarEquipmentItemTypeId.ForeignKeyTableName = "EP_EquipmentItemTypes";
				schema.Columns.Add(colvarEquipmentItemTypeId);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 128;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarModelNumber = new TableSchema.TableColumn(schema);
				colvarModelNumber.ColumnName = "ModelNumber";
				colvarModelNumber.DataType = DbType.AnsiString;
				colvarModelNumber.MaxLength = 50;
				colvarModelNumber.AutoIncrement = false;
				colvarModelNumber.IsNullable = true;
				colvarModelNumber.IsPrimaryKey = false;
				colvarModelNumber.IsForeignKey = false;
				colvarModelNumber.IsReadOnly = false;
				colvarModelNumber.DefaultSetting = @"";
				colvarModelNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModelNumber);

				TableSchema.TableColumn colvarPoints = new TableSchema.TableColumn(schema);
				colvarPoints.ColumnName = "Points";
				colvarPoints.DataType = DbType.Decimal;
				colvarPoints.MaxLength = 0;
				colvarPoints.AutoIncrement = false;
				colvarPoints.IsNullable = false;
				colvarPoints.IsPrimaryKey = false;
				colvarPoints.IsForeignKey = false;
				colvarPoints.IsReadOnly = false;
				colvarPoints.DefaultSetting = @"((0))";
				colvarPoints.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPoints);

				TableSchema.TableColumn colvarPointsA = new TableSchema.TableColumn(schema);
				colvarPointsA.ColumnName = "PointsA";
				colvarPointsA.DataType = DbType.Decimal;
				colvarPointsA.MaxLength = 0;
				colvarPointsA.AutoIncrement = false;
				colvarPointsA.IsNullable = false;
				colvarPointsA.IsPrimaryKey = false;
				colvarPointsA.IsForeignKey = false;
				colvarPointsA.IsReadOnly = false;
				colvarPointsA.DefaultSetting = @"((0))";
				colvarPointsA.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPointsA);

				TableSchema.TableColumn colvarCost = new TableSchema.TableColumn(schema);
				colvarCost.ColumnName = "Cost";
				colvarCost.DataType = DbType.Currency;
				colvarCost.MaxLength = 0;
				colvarCost.AutoIncrement = false;
				colvarCost.IsNullable = false;
				colvarCost.IsPrimaryKey = false;
				colvarCost.IsForeignKey = false;
				colvarCost.IsReadOnly = false;
				colvarCost.DefaultSetting = @"((0))";
				colvarCost.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCost);

				TableSchema.TableColumn colvarRetailPrice = new TableSchema.TableColumn(schema);
				colvarRetailPrice.ColumnName = "RetailPrice";
				colvarRetailPrice.DataType = DbType.Currency;
				colvarRetailPrice.MaxLength = 0;
				colvarRetailPrice.AutoIncrement = false;
				colvarRetailPrice.IsNullable = false;
				colvarRetailPrice.IsPrimaryKey = false;
				colvarRetailPrice.IsForeignKey = false;
				colvarRetailPrice.IsReadOnly = false;
				colvarRetailPrice.DefaultSetting = @"((0))";
				colvarRetailPrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRetailPrice);

				TableSchema.TableColumn colvarWholeSalePrice = new TableSchema.TableColumn(schema);
				colvarWholeSalePrice.ColumnName = "WholeSalePrice";
				colvarWholeSalePrice.DataType = DbType.Currency;
				colvarWholeSalePrice.MaxLength = 0;
				colvarWholeSalePrice.AutoIncrement = false;
				colvarWholeSalePrice.IsNullable = true;
				colvarWholeSalePrice.IsPrimaryKey = false;
				colvarWholeSalePrice.IsForeignKey = false;
				colvarWholeSalePrice.IsReadOnly = false;
				colvarWholeSalePrice.DefaultSetting = @"";
				colvarWholeSalePrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWholeSalePrice);

				TableSchema.TableColumn colvarBonus = new TableSchema.TableColumn(schema);
				colvarBonus.ColumnName = "Bonus";
				colvarBonus.DataType = DbType.Currency;
				colvarBonus.MaxLength = 0;
				colvarBonus.AutoIncrement = false;
				colvarBonus.IsNullable = true;
				colvarBonus.IsPrimaryKey = false;
				colvarBonus.IsForeignKey = false;
				colvarBonus.IsReadOnly = false;
				colvarBonus.DefaultSetting = @"";
				colvarBonus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBonus);

				TableSchema.TableColumn colvarMonitronicsCode = new TableSchema.TableColumn(schema);
				colvarMonitronicsCode.ColumnName = "MonitronicsCode";
				colvarMonitronicsCode.DataType = DbType.AnsiString;
				colvarMonitronicsCode.MaxLength = 50;
				colvarMonitronicsCode.AutoIncrement = false;
				colvarMonitronicsCode.IsNullable = true;
				colvarMonitronicsCode.IsPrimaryKey = false;
				colvarMonitronicsCode.IsForeignKey = false;
				colvarMonitronicsCode.IsReadOnly = false;
				colvarMonitronicsCode.DefaultSetting = @"";
				colvarMonitronicsCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonitronicsCode);

				TableSchema.TableColumn colvarIsZone = new TableSchema.TableColumn(schema);
				colvarIsZone.ColumnName = "IsZone";
				colvarIsZone.DataType = DbType.Byte;
				colvarIsZone.MaxLength = 0;
				colvarIsZone.AutoIncrement = false;
				colvarIsZone.IsNullable = true;
				colvarIsZone.IsPrimaryKey = false;
				colvarIsZone.IsForeignKey = false;
				colvarIsZone.IsReadOnly = false;
				colvarIsZone.DefaultSetting = @"";
				colvarIsZone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsZone);

				TableSchema.TableColumn colvarIsSerialized = new TableSchema.TableColumn(schema);
				colvarIsSerialized.ColumnName = "IsSerialized";
				colvarIsSerialized.DataType = DbType.Byte;
				colvarIsSerialized.MaxLength = 0;
				colvarIsSerialized.AutoIncrement = false;
				colvarIsSerialized.IsNullable = true;
				colvarIsSerialized.IsPrimaryKey = false;
				colvarIsSerialized.IsForeignKey = false;
				colvarIsSerialized.IsReadOnly = false;
				colvarIsSerialized.DefaultSetting = @"";
				colvarIsSerialized.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsSerialized);

				TableSchema.TableColumn colvarSortOrder = new TableSchema.TableColumn(schema);
				colvarSortOrder.ColumnName = "SortOrder";
				colvarSortOrder.DataType = DbType.Int32;
				colvarSortOrder.MaxLength = 0;
				colvarSortOrder.AutoIncrement = false;
				colvarSortOrder.IsNullable = true;
				colvarSortOrder.IsPrimaryKey = false;
				colvarSortOrder.IsForeignKey = false;
				colvarSortOrder.IsReadOnly = false;
				colvarSortOrder.DefaultSetting = @"";
				colvarSortOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSortOrder);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				DataService.Providers["InventoryProvider"].AddSchema("EP_Devices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static EP_Device LoadFrom(EP_Device item)
		{
			EP_Device result = new EP_Device();
			if (item.DeviceID != default(int)) {
				result.LoadByKey(item.DeviceID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int DeviceID { 
			get { return GetColumnValue<int>(Columns.DeviceID); }
			set {
				SetColumnValue(Columns.DeviceID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceID));
			}
		}
		[DataMember]
		public string EquipmentManufacturerId { 
			get { return GetColumnValue<string>(Columns.EquipmentManufacturerId); }
			set {
				SetColumnValue(Columns.EquipmentManufacturerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentManufacturerId));
			}
		}
		[DataMember]
		public string EquipmentTypeId { 
			get { return GetColumnValue<string>(Columns.EquipmentTypeId); }
			set {
				SetColumnValue(Columns.EquipmentTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentTypeId));
			}
		}
		[DataMember]
		public string EquipmentItemTypeId { 
			get { return GetColumnValue<string>(Columns.EquipmentItemTypeId); }
			set {
				SetColumnValue(Columns.EquipmentItemTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentItemTypeId));
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
		public string ModelNumber { 
			get { return GetColumnValue<string>(Columns.ModelNumber); }
			set {
				SetColumnValue(Columns.ModelNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ModelNumber));
			}
		}
		[DataMember]
		public decimal Points { 
			get { return GetColumnValue<decimal>(Columns.Points); }
			set {
				SetColumnValue(Columns.Points, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Points));
			}
		}
		[DataMember]
		public decimal PointsA { 
			get { return GetColumnValue<decimal>(Columns.PointsA); }
			set {
				SetColumnValue(Columns.PointsA, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PointsA));
			}
		}
		[DataMember]
		public decimal Cost { 
			get { return GetColumnValue<decimal>(Columns.Cost); }
			set {
				SetColumnValue(Columns.Cost, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Cost));
			}
		}
		[DataMember]
		public decimal RetailPrice { 
			get { return GetColumnValue<decimal>(Columns.RetailPrice); }
			set {
				SetColumnValue(Columns.RetailPrice, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RetailPrice));
			}
		}
		[DataMember]
		public decimal? WholeSalePrice { 
			get { return GetColumnValue<decimal?>(Columns.WholeSalePrice); }
			set {
				SetColumnValue(Columns.WholeSalePrice, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.WholeSalePrice));
			}
		}
		[DataMember]
		public decimal? Bonus { 
			get { return GetColumnValue<decimal?>(Columns.Bonus); }
			set {
				SetColumnValue(Columns.Bonus, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Bonus));
			}
		}
		[DataMember]
		public string MonitronicsCode { 
			get { return GetColumnValue<string>(Columns.MonitronicsCode); }
			set {
				SetColumnValue(Columns.MonitronicsCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MonitronicsCode));
			}
		}
		[DataMember]
		public byte? IsZone { 
			get { return GetColumnValue<byte?>(Columns.IsZone); }
			set {
				SetColumnValue(Columns.IsZone, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsZone));
			}
		}
		[DataMember]
		public byte? IsSerialized { 
			get { return GetColumnValue<byte?>(Columns.IsSerialized); }
			set {
				SetColumnValue(Columns.IsSerialized, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsSerialized));
			}
		}
		[DataMember]
		public int? SortOrder { 
			get { return GetColumnValue<int?>(Columns.SortOrder); }
			set {
				SetColumnValue(Columns.SortOrder, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SortOrder));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
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

		private EP_EquipmentItemType _EquipmentItemType;
		//Relationship: FK_Devices_drp_EquipmentItemTypes
		public EP_EquipmentItemType EquipmentItemType
		{
			get
			{
				if(_EquipmentItemType == null) {
					_EquipmentItemType = EP_EquipmentItemType.FetchByID(this.EquipmentItemTypeId);
				}
				return _EquipmentItemType;
			}
			set
			{
				SetColumnValue("EquipmentItemTypeId", value.EquipmentItemTypeID);
				_EquipmentItemType = value;
			}
		}

		private EP_EquipmentManufacturer _EquipmentManufacturer;
		//Relationship: FK_Devices_drp_EquipmentManufacturers
		public EP_EquipmentManufacturer EquipmentManufacturer
		{
			get
			{
				if(_EquipmentManufacturer == null) {
					_EquipmentManufacturer = EP_EquipmentManufacturer.FetchByID(this.EquipmentManufacturerId);
				}
				return _EquipmentManufacturer;
			}
			set
			{
				SetColumnValue("EquipmentManufacturerId", value.EquipmentManufacturerID);
				_EquipmentManufacturer = value;
			}
		}

		private EP_EquipmentType _EquipmentType;
		//Relationship: FK_Devices_drp_EquipmentTypes
		public EP_EquipmentType EquipmentType
		{
			get
			{
				if(_EquipmentType == null) {
					_EquipmentType = EP_EquipmentType.FetchByID(this.EquipmentTypeId);
				}
				return _EquipmentType;
			}
			set
			{
				SetColumnValue("EquipmentTypeId", value.EquipmentTypeID);
				_EquipmentType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return EquipmentManufacturerId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn DeviceIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EquipmentManufacturerIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentTypeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn EquipmentItemTypeIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModelNumberColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PointsColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PointsAColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CostColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn RetailPriceColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn WholeSalePriceColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn BonusColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn MonitronicsCodeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn IsZoneColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsSerializedColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn SortOrderColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[22]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string DeviceID = @"DeviceID";
			public static readonly string EquipmentManufacturerId = @"EquipmentManufacturerId";
			public static readonly string EquipmentTypeId = @"EquipmentTypeId";
			public static readonly string EquipmentItemTypeId = @"EquipmentItemTypeId";
			public static readonly string Name = @"Name";
			public static readonly string ModelNumber = @"ModelNumber";
			public static readonly string Points = @"Points";
			public static readonly string PointsA = @"PointsA";
			public static readonly string Cost = @"Cost";
			public static readonly string RetailPrice = @"RetailPrice";
			public static readonly string WholeSalePrice = @"WholeSalePrice";
			public static readonly string Bonus = @"Bonus";
			public static readonly string MonitronicsCode = @"MonitronicsCode";
			public static readonly string IsZone = @"IsZone";
			public static readonly string IsSerialized = @"IsSerialized";
			public static readonly string SortOrder = @"SortOrder";
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
			get { return DeviceID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the EP_EquipmentItemType class.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentItemTypeCollection : ActiveList<EP_EquipmentItemType, EP_EquipmentItemTypeCollection>
	{
		public static EP_EquipmentItemTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			EP_EquipmentItemTypeCollection result = new EP_EquipmentItemTypeCollection();
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
			foreach (EP_EquipmentItemType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the EP_EquipmentItemTypes table.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentItemType : ActiveRecord<EP_EquipmentItemType>, INotifyPropertyChanged
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

		public EP_EquipmentItemType()
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
				TableSchema.Table schema = new TableSchema.Table("EP_EquipmentItemTypes", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEquipmentItemTypeID = new TableSchema.TableColumn(schema);
				colvarEquipmentItemTypeID.ColumnName = "EquipmentItemTypeID";
				colvarEquipmentItemTypeID.DataType = DbType.AnsiString;
				colvarEquipmentItemTypeID.MaxLength = 20;
				colvarEquipmentItemTypeID.AutoIncrement = false;
				colvarEquipmentItemTypeID.IsNullable = false;
				colvarEquipmentItemTypeID.IsPrimaryKey = true;
				colvarEquipmentItemTypeID.IsForeignKey = false;
				colvarEquipmentItemTypeID.IsReadOnly = false;
				colvarEquipmentItemTypeID.DefaultSetting = @"";
				colvarEquipmentItemTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEquipmentItemTypeID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 128;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = true;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("EP_EquipmentItemTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static EP_EquipmentItemType LoadFrom(EP_EquipmentItemType item)
		{
			EP_EquipmentItemType result = new EP_EquipmentItemType();
			if (item.EquipmentItemTypeID != default(string)) {
				result.LoadByKey(item.EquipmentItemTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string EquipmentItemTypeID { 
			get { return GetColumnValue<string>(Columns.EquipmentItemTypeID); }
			set {
				SetColumnValue(Columns.EquipmentItemTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentItemTypeID));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
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
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EquipmentItemTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
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
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string EquipmentItemTypeID = @"EquipmentItemTypeID";
			public static readonly string Name = @"Name";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return EquipmentItemTypeID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the EP_EquipmentManufacturer class.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentManufacturerCollection : ActiveList<EP_EquipmentManufacturer, EP_EquipmentManufacturerCollection>
	{
		public static EP_EquipmentManufacturerCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			EP_EquipmentManufacturerCollection result = new EP_EquipmentManufacturerCollection();
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
			foreach (EP_EquipmentManufacturer item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the EP_EquipmentManufacturers table.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentManufacturer : ActiveRecord<EP_EquipmentManufacturer>, INotifyPropertyChanged
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

		public EP_EquipmentManufacturer()
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
				TableSchema.Table schema = new TableSchema.Table("EP_EquipmentManufacturers", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEquipmentManufacturerID = new TableSchema.TableColumn(schema);
				colvarEquipmentManufacturerID.ColumnName = "EquipmentManufacturerID";
				colvarEquipmentManufacturerID.DataType = DbType.AnsiString;
				colvarEquipmentManufacturerID.MaxLength = 20;
				colvarEquipmentManufacturerID.AutoIncrement = false;
				colvarEquipmentManufacturerID.IsNullable = false;
				colvarEquipmentManufacturerID.IsPrimaryKey = true;
				colvarEquipmentManufacturerID.IsForeignKey = false;
				colvarEquipmentManufacturerID.IsReadOnly = false;
				colvarEquipmentManufacturerID.DefaultSetting = @"";
				colvarEquipmentManufacturerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEquipmentManufacturerID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 128;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarisDefault = new TableSchema.TableColumn(schema);
				colvarisDefault.ColumnName = "isDefault";
				colvarisDefault.DataType = DbType.Boolean;
				colvarisDefault.MaxLength = 0;
				colvarisDefault.AutoIncrement = false;
				colvarisDefault.IsNullable = true;
				colvarisDefault.IsPrimaryKey = false;
				colvarisDefault.IsForeignKey = false;
				colvarisDefault.IsReadOnly = false;
				colvarisDefault.DefaultSetting = @"";
				colvarisDefault.ForeignKeyTableName = "";
				schema.Columns.Add(colvarisDefault);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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

				TableSchema.TableColumn colvarDEX_ROW_ID = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_ID.ColumnName = "DEX_ROW_ID";
				colvarDEX_ROW_ID.DataType = DbType.Int32;
				colvarDEX_ROW_ID.MaxLength = 0;
				colvarDEX_ROW_ID.AutoIncrement = true;
				colvarDEX_ROW_ID.IsNullable = false;
				colvarDEX_ROW_ID.IsPrimaryKey = false;
				colvarDEX_ROW_ID.IsForeignKey = false;
				colvarDEX_ROW_ID.IsReadOnly = false;
				colvarDEX_ROW_ID.DefaultSetting = @"";
				colvarDEX_ROW_ID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_ID);

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("EP_EquipmentManufacturers",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static EP_EquipmentManufacturer LoadFrom(EP_EquipmentManufacturer item)
		{
			EP_EquipmentManufacturer result = new EP_EquipmentManufacturer();
			if (item.EquipmentManufacturerID != default(string)) {
				result.LoadByKey(item.EquipmentManufacturerID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string EquipmentManufacturerID { 
			get { return GetColumnValue<string>(Columns.EquipmentManufacturerID); }
			set {
				SetColumnValue(Columns.EquipmentManufacturerID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentManufacturerID));
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
		public bool? isDefault { 
			get { return GetColumnValue<bool?>(Columns.isDefault); }
			set {
				SetColumnValue(Columns.isDefault, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.isDefault));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
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
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EquipmentManufacturerIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn isDefaultColumn
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
		public static TableSchema.TableColumn DEX_ROW_IDColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string EquipmentManufacturerID = @"EquipmentManufacturerID";
			public static readonly string Name = @"Name";
			public static readonly string isDefault = @"isDefault";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
			public static readonly string DEX_ROW_ID = @"DEX_ROW_ID";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return EquipmentManufacturerID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the EP_Equipment class.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentCollection : ActiveList<EP_Equipment, EP_EquipmentCollection>
	{
		public static EP_EquipmentCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			EP_EquipmentCollection result = new EP_EquipmentCollection();
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
			foreach (EP_Equipment item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the EP_Equipments table.
	/// </summary>
	[DataContract]
	public partial class EP_Equipment : ActiveRecord<EP_Equipment>, INotifyPropertyChanged
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

		public EP_Equipment()
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
				TableSchema.Table schema = new TableSchema.Table("EP_Equipments", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEquipmentID = new TableSchema.TableColumn(schema);
				colvarEquipmentID.ColumnName = "EquipmentID";
				colvarEquipmentID.DataType = DbType.Int32;
				colvarEquipmentID.MaxLength = 0;
				colvarEquipmentID.AutoIncrement = true;
				colvarEquipmentID.IsNullable = false;
				colvarEquipmentID.IsPrimaryKey = true;
				colvarEquipmentID.IsForeignKey = false;
				colvarEquipmentID.IsReadOnly = false;
				colvarEquipmentID.DefaultSetting = @"";
				colvarEquipmentID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEquipmentID);

				TableSchema.TableColumn colvarDeviceId = new TableSchema.TableColumn(schema);
				colvarDeviceId.ColumnName = "DeviceId";
				colvarDeviceId.DataType = DbType.Int32;
				colvarDeviceId.MaxLength = 0;
				colvarDeviceId.AutoIncrement = false;
				colvarDeviceId.IsNullable = false;
				colvarDeviceId.IsPrimaryKey = false;
				colvarDeviceId.IsForeignKey = true;
				colvarDeviceId.IsReadOnly = false;
				colvarDeviceId.DefaultSetting = @"";
				colvarDeviceId.ForeignKeyTableName = "EP_Devices";
				schema.Columns.Add(colvarDeviceId);

				TableSchema.TableColumn colvarSupplierId = new TableSchema.TableColumn(schema);
				colvarSupplierId.ColumnName = "SupplierId";
				colvarSupplierId.DataType = DbType.AnsiString;
				colvarSupplierId.MaxLength = 20;
				colvarSupplierId.AutoIncrement = false;
				colvarSupplierId.IsNullable = false;
				colvarSupplierId.IsPrimaryKey = false;
				colvarSupplierId.IsForeignKey = true;
				colvarSupplierId.IsReadOnly = false;
				colvarSupplierId.DefaultSetting = @"";
				colvarSupplierId.ForeignKeyTableName = "IV_Supplier";
				schema.Columns.Add(colvarSupplierId);

				TableSchema.TableColumn colvarPartNumber = new TableSchema.TableColumn(schema);
				colvarPartNumber.ColumnName = "PartNumber";
				colvarPartNumber.DataType = DbType.AnsiString;
				colvarPartNumber.MaxLength = 50;
				colvarPartNumber.AutoIncrement = false;
				colvarPartNumber.IsNullable = false;
				colvarPartNumber.IsPrimaryKey = false;
				colvarPartNumber.IsForeignKey = false;
				colvarPartNumber.IsReadOnly = false;
				colvarPartNumber.DefaultSetting = @"";
				colvarPartNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPartNumber);

				TableSchema.TableColumn colvarNotes = new TableSchema.TableColumn(schema);
				colvarNotes.ColumnName = "Notes";
				colvarNotes.DataType = DbType.String;
				colvarNotes.MaxLength = -1;
				colvarNotes.AutoIncrement = false;
				colvarNotes.IsNullable = true;
				colvarNotes.IsPrimaryKey = false;
				colvarNotes.IsForeignKey = false;
				colvarNotes.IsReadOnly = false;
				colvarNotes.DefaultSetting = @"";
				colvarNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNotes);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				DataService.Providers["InventoryProvider"].AddSchema("EP_Equipments",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static EP_Equipment LoadFrom(EP_Equipment item)
		{
			EP_Equipment result = new EP_Equipment();
			if (item.EquipmentID != default(int)) {
				result.LoadByKey(item.EquipmentID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int EquipmentID { 
			get { return GetColumnValue<int>(Columns.EquipmentID); }
			set {
				SetColumnValue(Columns.EquipmentID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentID));
			}
		}
		[DataMember]
		public int DeviceId { 
			get { return GetColumnValue<int>(Columns.DeviceId); }
			set {
				SetColumnValue(Columns.DeviceId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeviceId));
			}
		}
		[DataMember]
		public string SupplierId { 
			get { return GetColumnValue<string>(Columns.SupplierId); }
			set {
				SetColumnValue(Columns.SupplierId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SupplierId));
			}
		}
		[DataMember]
		public string PartNumber { 
			get { return GetColumnValue<string>(Columns.PartNumber); }
			set {
				SetColumnValue(Columns.PartNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PartNumber));
			}
		}
		[DataMember]
		public string Notes { 
			get { return GetColumnValue<string>(Columns.Notes); }
			set {
				SetColumnValue(Columns.Notes, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Notes));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
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

		private EP_Device _Device;
		//Relationship: FK_drp_Equipments_Devices
		public EP_Device Device
		{
			get
			{
				if(_Device == null) {
					_Device = EP_Device.FetchByID(this.DeviceId);
				}
				return _Device;
			}
			set
			{
				SetColumnValue("DeviceId", value.DeviceID);
				_Device = value;
			}
		}

		private IV_Supplier _Supplier;
		//Relationship: FK_drp_Equipments_IV_Supplier
		public IV_Supplier Supplier
		{
			get
			{
				if(_Supplier == null) {
					_Supplier = IV_Supplier.FetchByID(this.SupplierId);
				}
				return _Supplier;
			}
			set
			{
				SetColumnValue("SupplierId", value.SupplierID);
				_Supplier = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return EquipmentID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn EquipmentIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DeviceIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SupplierIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PartNumberColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn NotesColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string EquipmentID = @"EquipmentID";
			public static readonly string DeviceId = @"DeviceId";
			public static readonly string SupplierId = @"SupplierId";
			public static readonly string PartNumber = @"PartNumber";
			public static readonly string Notes = @"Notes";
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
			get { return EquipmentID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the EP_EquipmentType class.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentTypeCollection : ActiveList<EP_EquipmentType, EP_EquipmentTypeCollection>
	{
		public static EP_EquipmentTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			EP_EquipmentTypeCollection result = new EP_EquipmentTypeCollection();
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
			foreach (EP_EquipmentType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the EP_EquipmentTypes table.
	/// </summary>
	[DataContract]
	public partial class EP_EquipmentType : ActiveRecord<EP_EquipmentType>, INotifyPropertyChanged
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

		public EP_EquipmentType()
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
				TableSchema.Table schema = new TableSchema.Table("EP_EquipmentTypes", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarEquipmentTypeID = new TableSchema.TableColumn(schema);
				colvarEquipmentTypeID.ColumnName = "EquipmentTypeID";
				colvarEquipmentTypeID.DataType = DbType.AnsiString;
				colvarEquipmentTypeID.MaxLength = 20;
				colvarEquipmentTypeID.AutoIncrement = false;
				colvarEquipmentTypeID.IsNullable = false;
				colvarEquipmentTypeID.IsPrimaryKey = true;
				colvarEquipmentTypeID.IsForeignKey = false;
				colvarEquipmentTypeID.IsReadOnly = false;
				colvarEquipmentTypeID.DefaultSetting = @"";
				colvarEquipmentTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEquipmentTypeID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 128;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				TableSchema.TableColumn colvarisDefault = new TableSchema.TableColumn(schema);
				colvarisDefault.ColumnName = "isDefault";
				colvarisDefault.DataType = DbType.Boolean;
				colvarisDefault.MaxLength = 0;
				colvarisDefault.AutoIncrement = false;
				colvarisDefault.IsNullable = true;
				colvarisDefault.IsPrimaryKey = false;
				colvarisDefault.IsForeignKey = false;
				colvarisDefault.IsReadOnly = false;
				colvarisDefault.DefaultSetting = @"";
				colvarisDefault.ForeignKeyTableName = "";
				schema.Columns.Add(colvarisDefault);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				DataService.Providers["InventoryProvider"].AddSchema("EP_EquipmentTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static EP_EquipmentType LoadFrom(EP_EquipmentType item)
		{
			EP_EquipmentType result = new EP_EquipmentType();
			if (item.EquipmentTypeID != default(string)) {
				result.LoadByKey(item.EquipmentTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string EquipmentTypeID { 
			get { return GetColumnValue<string>(Columns.EquipmentTypeID); }
			set {
				SetColumnValue(Columns.EquipmentTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentTypeID));
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
		public bool? isDefault { 
			get { return GetColumnValue<bool?>(Columns.isDefault); }
			set {
				SetColumnValue(Columns.isDefault, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.isDefault));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
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
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn EquipmentTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn isDefaultColumn
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
			public static readonly string EquipmentTypeID = @"EquipmentTypeID";
			public static readonly string Name = @"Name";
			public static readonly string isDefault = @"isDefault";
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
			get { return EquipmentTypeID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_Asset class.
	/// </summary>
	[DataContract]
	public partial class IV_AssetCollection : ActiveList<IV_Asset, IV_AssetCollection>
	{
		public static IV_AssetCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_AssetCollection result = new IV_AssetCollection();
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
			foreach (IV_Asset item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_Assets table.
	/// </summary>
	[DataContract]
	public partial class IV_Asset : ActiveRecord<IV_Asset>, INotifyPropertyChanged
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

		public IV_Asset()
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
				TableSchema.Table schema = new TableSchema.Table("IV_Assets", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAssetID = new TableSchema.TableColumn(schema);
				colvarAssetID.ColumnName = "AssetID";
				colvarAssetID.DataType = DbType.Int64;
				colvarAssetID.MaxLength = 0;
				colvarAssetID.AutoIncrement = true;
				colvarAssetID.IsNullable = false;
				colvarAssetID.IsPrimaryKey = true;
				colvarAssetID.IsForeignKey = false;
				colvarAssetID.IsReadOnly = false;
				colvarAssetID.DefaultSetting = @"";
				colvarAssetID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetID);

				TableSchema.TableColumn colvarPurchaseOrderItemId = new TableSchema.TableColumn(schema);
				colvarPurchaseOrderItemId.ColumnName = "PurchaseOrderItemId";
				colvarPurchaseOrderItemId.DataType = DbType.Int64;
				colvarPurchaseOrderItemId.MaxLength = 0;
				colvarPurchaseOrderItemId.AutoIncrement = false;
				colvarPurchaseOrderItemId.IsNullable = false;
				colvarPurchaseOrderItemId.IsPrimaryKey = false;
				colvarPurchaseOrderItemId.IsForeignKey = true;
				colvarPurchaseOrderItemId.IsReadOnly = false;
				colvarPurchaseOrderItemId.DefaultSetting = @"";
				colvarPurchaseOrderItemId.ForeignKeyTableName = "IV_PurchaseOrderItems";
				schema.Columns.Add(colvarPurchaseOrderItemId);

				TableSchema.TableColumn colvarEquipmentId = new TableSchema.TableColumn(schema);
				colvarEquipmentId.ColumnName = "EquipmentId";
				colvarEquipmentId.DataType = DbType.Int32;
				colvarEquipmentId.MaxLength = 0;
				colvarEquipmentId.AutoIncrement = false;
				colvarEquipmentId.IsNullable = false;
				colvarEquipmentId.IsPrimaryKey = false;
				colvarEquipmentId.IsForeignKey = true;
				colvarEquipmentId.IsReadOnly = false;
				colvarEquipmentId.DefaultSetting = @"";
				colvarEquipmentId.ForeignKeyTableName = "EP_Equipments";
				schema.Columns.Add(colvarEquipmentId);

				TableSchema.TableColumn colvarAssetTrackingId = new TableSchema.TableColumn(schema);
				colvarAssetTrackingId.ColumnName = "AssetTrackingId";
				colvarAssetTrackingId.DataType = DbType.Int64;
				colvarAssetTrackingId.MaxLength = 0;
				colvarAssetTrackingId.AutoIncrement = false;
				colvarAssetTrackingId.IsNullable = true;
				colvarAssetTrackingId.IsPrimaryKey = false;
				colvarAssetTrackingId.IsForeignKey = false;
				colvarAssetTrackingId.IsReadOnly = false;
				colvarAssetTrackingId.DefaultSetting = @"";
				colvarAssetTrackingId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetTrackingId);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_Assets",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_Asset LoadFrom(IV_Asset item)
		{
			IV_Asset result = new IV_Asset();
			if (item.AssetID != default(long)) {
				result.LoadByKey(item.AssetID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AssetID { 
			get { return GetColumnValue<long>(Columns.AssetID); }
			set {
				SetColumnValue(Columns.AssetID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetID));
			}
		}
		[DataMember]
		public long PurchaseOrderItemId { 
			get { return GetColumnValue<long>(Columns.PurchaseOrderItemId); }
			set {
				SetColumnValue(Columns.PurchaseOrderItemId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseOrderItemId));
			}
		}
		[DataMember]
		public int EquipmentId { 
			get { return GetColumnValue<int>(Columns.EquipmentId); }
			set {
				SetColumnValue(Columns.EquipmentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentId));
			}
		}
		[DataMember]
		public long? AssetTrackingId { 
			get { return GetColumnValue<long?>(Columns.AssetTrackingId); }
			set {
				SetColumnValue(Columns.AssetTrackingId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTrackingId));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private EP_Equipment _Equipment;
		//Relationship: FK_IV_Assets_EP_Equipments
		public EP_Equipment Equipment
		{
			get
			{
				if(_Equipment == null) {
					_Equipment = EP_Equipment.FetchByID(this.EquipmentId);
				}
				return _Equipment;
			}
			set
			{
				SetColumnValue("EquipmentId", value.EquipmentID);
				_Equipment = value;
			}
		}

		private IV_PurchaseOrderItem _PurchaseOrderItem;
		//Relationship: FK_IV_Parts_IV_PurchaseOrderItems
		public IV_PurchaseOrderItem PurchaseOrderItem
		{
			get
			{
				if(_PurchaseOrderItem == null) {
					_PurchaseOrderItem = IV_PurchaseOrderItem.FetchByID(this.PurchaseOrderItemId);
				}
				return _PurchaseOrderItem;
			}
			set
			{
				SetColumnValue("PurchaseOrderItemId", value.PurchaseOrderItemID);
				_PurchaseOrderItem = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AssetID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AssetIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaseOrderItemIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AssetTrackingIdColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AssetID = @"AssetID";
			public static readonly string PurchaseOrderItemId = @"PurchaseOrderItemId";
			public static readonly string EquipmentId = @"EquipmentId";
			public static readonly string AssetTrackingId = @"AssetTrackingId";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return AssetID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_AssetTag class.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTagCollection : ActiveList<IV_AssetTag, IV_AssetTagCollection>
	{
		public static IV_AssetTagCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_AssetTagCollection result = new IV_AssetTagCollection();
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
			foreach (IV_AssetTag item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_AssetTags table.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTag : ActiveRecord<IV_AssetTag>, INotifyPropertyChanged
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

		public IV_AssetTag()
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
				TableSchema.Table schema = new TableSchema.Table("IV_AssetTags", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAssetTagID = new TableSchema.TableColumn(schema);
				colvarAssetTagID.ColumnName = "AssetTagID";
				colvarAssetTagID.DataType = DbType.String;
				colvarAssetTagID.MaxLength = 50;
				colvarAssetTagID.AutoIncrement = false;
				colvarAssetTagID.IsNullable = false;
				colvarAssetTagID.IsPrimaryKey = true;
				colvarAssetTagID.IsForeignKey = false;
				colvarAssetTagID.IsReadOnly = false;
				colvarAssetTagID.DefaultSetting = @"";
				colvarAssetTagID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetTagID);

				TableSchema.TableColumn colvarAssetId = new TableSchema.TableColumn(schema);
				colvarAssetId.ColumnName = "AssetId";
				colvarAssetId.DataType = DbType.Int64;
				colvarAssetId.MaxLength = 0;
				colvarAssetId.AutoIncrement = false;
				colvarAssetId.IsNullable = true;
				colvarAssetId.IsPrimaryKey = false;
				colvarAssetId.IsForeignKey = true;
				colvarAssetId.IsReadOnly = false;
				colvarAssetId.DefaultSetting = @"";
				colvarAssetId.ForeignKeyTableName = "IV_Assets";
				schema.Columns.Add(colvarAssetId);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_AssetTags",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_AssetTag LoadFrom(IV_AssetTag item)
		{
			IV_AssetTag result = new IV_AssetTag();
			if (item.AssetTagID != default(string)) {
				result.LoadByKey(item.AssetTagID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string AssetTagID { 
			get { return GetColumnValue<string>(Columns.AssetTagID); }
			set {
				SetColumnValue(Columns.AssetTagID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTagID));
			}
		}
		[DataMember]
		public long? AssetId { 
			get { return GetColumnValue<long?>(Columns.AssetId); }
			set {
				SetColumnValue(Columns.AssetId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetId));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private IV_Asset _Asset;
		//Relationship: FK_IV_AssetTags_IV_Assets
		public IV_Asset Asset
		{
			get
			{
				if(_Asset == null) {
					_Asset = IV_Asset.FetchByID(this.AssetId);
				}
				return _Asset;
			}
			set
			{
				SetColumnValue("AssetId", value.AssetID);
				_Asset = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AssetTagID;
		}

		#region Typed Columns

		public static TableSchema.TableColumn AssetTagIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AssetIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
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
			public static readonly string AssetTagID = @"AssetTagID";
			public static readonly string AssetId = @"AssetId";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return AssetTagID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_AssetTracking class.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTrackingCollection : ActiveList<IV_AssetTracking, IV_AssetTrackingCollection>
	{
		public static IV_AssetTrackingCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_AssetTrackingCollection result = new IV_AssetTrackingCollection();
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
			foreach (IV_AssetTracking item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_AssetTracking table.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTracking : ActiveRecord<IV_AssetTracking>, INotifyPropertyChanged
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

		public IV_AssetTracking()
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
				TableSchema.Table schema = new TableSchema.Table("IV_AssetTracking", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAssetTrackingID = new TableSchema.TableColumn(schema);
				colvarAssetTrackingID.ColumnName = "AssetTrackingID";
				colvarAssetTrackingID.DataType = DbType.Int64;
				colvarAssetTrackingID.MaxLength = 0;
				colvarAssetTrackingID.AutoIncrement = true;
				colvarAssetTrackingID.IsNullable = false;
				colvarAssetTrackingID.IsPrimaryKey = true;
				colvarAssetTrackingID.IsForeignKey = false;
				colvarAssetTrackingID.IsReadOnly = false;
				colvarAssetTrackingID.DefaultSetting = @"";
				colvarAssetTrackingID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetTrackingID);

				TableSchema.TableColumn colvarAssetId = new TableSchema.TableColumn(schema);
				colvarAssetId.ColumnName = "AssetId";
				colvarAssetId.DataType = DbType.Int64;
				colvarAssetId.MaxLength = 0;
				colvarAssetId.AutoIncrement = false;
				colvarAssetId.IsNullable = false;
				colvarAssetId.IsPrimaryKey = false;
				colvarAssetId.IsForeignKey = true;
				colvarAssetId.IsReadOnly = false;
				colvarAssetId.DefaultSetting = @"";
				colvarAssetId.ForeignKeyTableName = "IV_Assets";
				schema.Columns.Add(colvarAssetId);

				TableSchema.TableColumn colvarAssetTrackingTypeId = new TableSchema.TableColumn(schema);
				colvarAssetTrackingTypeId.ColumnName = "AssetTrackingTypeId";
				colvarAssetTrackingTypeId.DataType = DbType.AnsiString;
				colvarAssetTrackingTypeId.MaxLength = 20;
				colvarAssetTrackingTypeId.AutoIncrement = false;
				colvarAssetTrackingTypeId.IsNullable = false;
				colvarAssetTrackingTypeId.IsPrimaryKey = false;
				colvarAssetTrackingTypeId.IsForeignKey = true;
				colvarAssetTrackingTypeId.IsReadOnly = false;
				colvarAssetTrackingTypeId.DefaultSetting = @"";
				colvarAssetTrackingTypeId.ForeignKeyTableName = "IV_AssetTrackingTypes";
				schema.Columns.Add(colvarAssetTrackingTypeId);

				TableSchema.TableColumn colvarPartId = new TableSchema.TableColumn(schema);
				colvarPartId.ColumnName = "PartId";
				colvarPartId.DataType = DbType.Int64;
				colvarPartId.MaxLength = 0;
				colvarPartId.AutoIncrement = false;
				colvarPartId.IsNullable = false;
				colvarPartId.IsPrimaryKey = false;
				colvarPartId.IsForeignKey = false;
				colvarPartId.IsReadOnly = false;
				colvarPartId.DefaultSetting = @"";
				colvarPartId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPartId);

				TableSchema.TableColumn colvarOfficeId = new TableSchema.TableColumn(schema);
				colvarOfficeId.ColumnName = "OfficeId";
				colvarOfficeId.DataType = DbType.Int32;
				colvarOfficeId.MaxLength = 0;
				colvarOfficeId.AutoIncrement = false;
				colvarOfficeId.IsNullable = false;
				colvarOfficeId.IsPrimaryKey = false;
				colvarOfficeId.IsForeignKey = true;
				colvarOfficeId.IsReadOnly = false;
				colvarOfficeId.DefaultSetting = @"";
				colvarOfficeId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeId);

				TableSchema.TableColumn colvarTechId = new TableSchema.TableColumn(schema);
				colvarTechId.ColumnName = "TechId";
				colvarTechId.DataType = DbType.Guid;
				colvarTechId.MaxLength = 0;
				colvarTechId.AutoIncrement = false;
				colvarTechId.IsNullable = true;
				colvarTechId.IsPrimaryKey = false;
				colvarTechId.IsForeignKey = false;
				colvarTechId.IsReadOnly = false;
				colvarTechId.DefaultSetting = @"";
				colvarTechId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechId);

				TableSchema.TableColumn colvarOfficeAuditId = new TableSchema.TableColumn(schema);
				colvarOfficeAuditId.ColumnName = "OfficeAuditId";
				colvarOfficeAuditId.DataType = DbType.Int32;
				colvarOfficeAuditId.MaxLength = 0;
				colvarOfficeAuditId.AutoIncrement = false;
				colvarOfficeAuditId.IsNullable = true;
				colvarOfficeAuditId.IsPrimaryKey = false;
				colvarOfficeAuditId.IsForeignKey = true;
				colvarOfficeAuditId.IsReadOnly = false;
				colvarOfficeAuditId.DefaultSetting = @"";
				colvarOfficeAuditId.ForeignKeyTableName = "IV_OfficeAudits";
				schema.Columns.Add(colvarOfficeAuditId);

				TableSchema.TableColumn colvarTechAuditId = new TableSchema.TableColumn(schema);
				colvarTechAuditId.ColumnName = "TechAuditId";
				colvarTechAuditId.DataType = DbType.Int32;
				colvarTechAuditId.MaxLength = 0;
				colvarTechAuditId.AutoIncrement = false;
				colvarTechAuditId.IsNullable = true;
				colvarTechAuditId.IsPrimaryKey = false;
				colvarTechAuditId.IsForeignKey = true;
				colvarTechAuditId.IsReadOnly = false;
				colvarTechAuditId.DefaultSetting = @"";
				colvarTechAuditId.ForeignKeyTableName = "IV_TechAudits";
				schema.Columns.Add(colvarTechAuditId);

				TableSchema.TableColumn colvarCustomerAccountId = new TableSchema.TableColumn(schema);
				colvarCustomerAccountId.ColumnName = "CustomerAccountId";
				colvarCustomerAccountId.DataType = DbType.Int32;
				colvarCustomerAccountId.MaxLength = 0;
				colvarCustomerAccountId.AutoIncrement = false;
				colvarCustomerAccountId.IsNullable = true;
				colvarCustomerAccountId.IsPrimaryKey = false;
				colvarCustomerAccountId.IsForeignKey = false;
				colvarCustomerAccountId.IsReadOnly = false;
				colvarCustomerAccountId.DefaultSetting = @"";
				colvarCustomerAccountId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerAccountId);

				TableSchema.TableColumn colvarOfficeTransferId = new TableSchema.TableColumn(schema);
				colvarOfficeTransferId.ColumnName = "OfficeTransferId";
				colvarOfficeTransferId.DataType = DbType.Int32;
				colvarOfficeTransferId.MaxLength = 0;
				colvarOfficeTransferId.AutoIncrement = false;
				colvarOfficeTransferId.IsNullable = true;
				colvarOfficeTransferId.IsPrimaryKey = false;
				colvarOfficeTransferId.IsForeignKey = true;
				colvarOfficeTransferId.IsReadOnly = false;
				colvarOfficeTransferId.DefaultSetting = @"";
				colvarOfficeTransferId.ForeignKeyTableName = "IV_OfficeTransfers";
				schema.Columns.Add(colvarOfficeTransferId);

				TableSchema.TableColumn colvarRMANumber = new TableSchema.TableColumn(schema);
				colvarRMANumber.ColumnName = "RMANumber";
				colvarRMANumber.DataType = DbType.String;
				colvarRMANumber.MaxLength = 50;
				colvarRMANumber.AutoIncrement = false;
				colvarRMANumber.IsNullable = true;
				colvarRMANumber.IsPrimaryKey = false;
				colvarRMANumber.IsForeignKey = false;
				colvarRMANumber.IsReadOnly = false;
				colvarRMANumber.DefaultSetting = @"";
				colvarRMANumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRMANumber);

				TableSchema.TableColumn colvarComment = new TableSchema.TableColumn(schema);
				colvarComment.ColumnName = "Comment";
				colvarComment.DataType = DbType.String;
				colvarComment.MaxLength = 1024;
				colvarComment.AutoIncrement = false;
				colvarComment.IsNullable = true;
				colvarComment.IsPrimaryKey = false;
				colvarComment.IsForeignKey = false;
				colvarComment.IsReadOnly = false;
				colvarComment.DefaultSetting = @"";
				colvarComment.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComment);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_AssetTracking",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_AssetTracking LoadFrom(IV_AssetTracking item)
		{
			IV_AssetTracking result = new IV_AssetTracking();
			if (item.AssetTrackingID != default(long)) {
				result.LoadByKey(item.AssetTrackingID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AssetTrackingID { 
			get { return GetColumnValue<long>(Columns.AssetTrackingID); }
			set {
				SetColumnValue(Columns.AssetTrackingID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTrackingID));
			}
		}
		[DataMember]
		public long AssetId { 
			get { return GetColumnValue<long>(Columns.AssetId); }
			set {
				SetColumnValue(Columns.AssetId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetId));
			}
		}
		[DataMember]
		public string AssetTrackingTypeId { 
			get { return GetColumnValue<string>(Columns.AssetTrackingTypeId); }
			set {
				SetColumnValue(Columns.AssetTrackingTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTrackingTypeId));
			}
		}
		[DataMember]
		public long PartId { 
			get { return GetColumnValue<long>(Columns.PartId); }
			set {
				SetColumnValue(Columns.PartId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PartId));
			}
		}
		[DataMember]
		public int OfficeId { 
			get { return GetColumnValue<int>(Columns.OfficeId); }
			set {
				SetColumnValue(Columns.OfficeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeId));
			}
		}
		[DataMember]
		public Guid? TechId { 
			get { return GetColumnValue<Guid?>(Columns.TechId); }
			set {
				SetColumnValue(Columns.TechId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechId));
			}
		}
		[DataMember]
		public int? OfficeAuditId { 
			get { return GetColumnValue<int?>(Columns.OfficeAuditId); }
			set {
				SetColumnValue(Columns.OfficeAuditId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeAuditId));
			}
		}
		[DataMember]
		public int? TechAuditId { 
			get { return GetColumnValue<int?>(Columns.TechAuditId); }
			set {
				SetColumnValue(Columns.TechAuditId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechAuditId));
			}
		}
		[DataMember]
		public int? CustomerAccountId { 
			get { return GetColumnValue<int?>(Columns.CustomerAccountId); }
			set {
				SetColumnValue(Columns.CustomerAccountId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CustomerAccountId));
			}
		}
		[DataMember]
		public int? OfficeTransferId { 
			get { return GetColumnValue<int?>(Columns.OfficeTransferId); }
			set {
				SetColumnValue(Columns.OfficeTransferId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeTransferId));
			}
		}
		[DataMember]
		public string RMANumber { 
			get { return GetColumnValue<string>(Columns.RMANumber); }
			set {
				SetColumnValue(Columns.RMANumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.RMANumber));
			}
		}
		[DataMember]
		public string Comment { 
			get { return GetColumnValue<string>(Columns.Comment); }
			set {
				SetColumnValue(Columns.Comment, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Comment));
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

		private IV_OfficeAudit _OfficeAudit;
		//Relationship: FK_IV_PartBarcodeTracking_IV_OfficeAudits
		public IV_OfficeAudit OfficeAudit
		{
			get
			{
				if(_OfficeAudit == null) {
					_OfficeAudit = IV_OfficeAudit.FetchByID(this.OfficeAuditId);
				}
				return _OfficeAudit;
			}
			set
			{
				SetColumnValue("OfficeAuditId", value.OfficeAuditID);
				_OfficeAudit = value;
			}
		}

		private IV_OfficeTransfer _OfficeTransfer;
		//Relationship: FK_IV_PartBarcodeTracking_IV_OfficeTransfers
		public IV_OfficeTransfer OfficeTransfer
		{
			get
			{
				if(_OfficeTransfer == null) {
					_OfficeTransfer = IV_OfficeTransfer.FetchByID(this.OfficeTransferId);
				}
				return _OfficeTransfer;
			}
			set
			{
				SetColumnValue("OfficeTransferId", value.OfficeTransferID);
				_OfficeTransfer = value;
			}
		}

		private IV_AssetTrackingType _AssetTrackingType;
		//Relationship: FK_IV_AssetTracking_IV_AssetTrackingTypes
		public IV_AssetTrackingType AssetTrackingType
		{
			get
			{
				if(_AssetTrackingType == null) {
					_AssetTrackingType = IV_AssetTrackingType.FetchByID(this.AssetTrackingTypeId);
				}
				return _AssetTrackingType;
			}
			set
			{
				SetColumnValue("AssetTrackingTypeId", value.AssetTrackingTypeID);
				_AssetTrackingType = value;
			}
		}

		private IV_Asset _Asset;
		//Relationship: FK_IV_AssetTracking_IV_Assets
		public IV_Asset Asset
		{
			get
			{
				if(_Asset == null) {
					_Asset = IV_Asset.FetchByID(this.AssetId);
				}
				return _Asset;
			}
			set
			{
				SetColumnValue("AssetId", value.AssetID);
				_Asset = value;
			}
		}

		private IV_TechAudit _TechAudit;
		//Relationship: FK_IV_PartBarcodeTracking_IV_TechAudits
		public IV_TechAudit TechAudit
		{
			get
			{
				if(_TechAudit == null) {
					_TechAudit = IV_TechAudit.FetchByID(this.TechAuditId);
				}
				return _TechAudit;
			}
			set
			{
				SetColumnValue("TechAuditId", value.TechAuditID);
				_TechAudit = value;
			}
		}

		private IV_Office _Office;
		//Relationship: FK_IV_PartBarcodeTracking_Offices
		public IV_Office Office
		{
			get
			{
				if(_Office == null) {
					_Office = IV_Office.FetchByID(this.OfficeId);
				}
				return _Office;
			}
			set
			{
				SetColumnValue("OfficeId", value.OfficeID);
				_Office = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AssetTrackingID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AssetTrackingIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AssetIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AssetTrackingTypeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PartIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn OfficeIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TechIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn OfficeAuditIdColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TechAuditIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CustomerAccountIdColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn OfficeTransferIdColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn RMANumberColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CommentColumn
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
			public static readonly string AssetTrackingID = @"AssetTrackingID";
			public static readonly string AssetId = @"AssetId";
			public static readonly string AssetTrackingTypeId = @"AssetTrackingTypeId";
			public static readonly string PartId = @"PartId";
			public static readonly string OfficeId = @"OfficeId";
			public static readonly string TechId = @"TechId";
			public static readonly string OfficeAuditId = @"OfficeAuditId";
			public static readonly string TechAuditId = @"TechAuditId";
			public static readonly string CustomerAccountId = @"CustomerAccountId";
			public static readonly string OfficeTransferId = @"OfficeTransferId";
			public static readonly string RMANumber = @"RMANumber";
			public static readonly string Comment = @"Comment";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return AssetTrackingID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_AssetTrackingType class.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTrackingTypeCollection : ActiveList<IV_AssetTrackingType, IV_AssetTrackingTypeCollection>
	{
		public static IV_AssetTrackingTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_AssetTrackingTypeCollection result = new IV_AssetTrackingTypeCollection();
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
			foreach (IV_AssetTrackingType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_AssetTrackingTypes table.
	/// </summary>
	[DataContract]
	public partial class IV_AssetTrackingType : ActiveRecord<IV_AssetTrackingType>, INotifyPropertyChanged
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

		public IV_AssetTrackingType()
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
				TableSchema.Table schema = new TableSchema.Table("IV_AssetTrackingTypes", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAssetTrackingTypeID = new TableSchema.TableColumn(schema);
				colvarAssetTrackingTypeID.ColumnName = "AssetTrackingTypeID";
				colvarAssetTrackingTypeID.DataType = DbType.AnsiString;
				colvarAssetTrackingTypeID.MaxLength = 20;
				colvarAssetTrackingTypeID.AutoIncrement = false;
				colvarAssetTrackingTypeID.IsNullable = false;
				colvarAssetTrackingTypeID.IsPrimaryKey = true;
				colvarAssetTrackingTypeID.IsForeignKey = false;
				colvarAssetTrackingTypeID.IsReadOnly = false;
				colvarAssetTrackingTypeID.DefaultSetting = @"";
				colvarAssetTrackingTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetTrackingTypeID);

				TableSchema.TableColumn colvarAssetTrackingTypeName = new TableSchema.TableColumn(schema);
				colvarAssetTrackingTypeName.ColumnName = "AssetTrackingTypeName";
				colvarAssetTrackingTypeName.DataType = DbType.AnsiString;
				colvarAssetTrackingTypeName.MaxLength = 100;
				colvarAssetTrackingTypeName.AutoIncrement = false;
				colvarAssetTrackingTypeName.IsNullable = false;
				colvarAssetTrackingTypeName.IsPrimaryKey = false;
				colvarAssetTrackingTypeName.IsForeignKey = false;
				colvarAssetTrackingTypeName.IsReadOnly = false;
				colvarAssetTrackingTypeName.DefaultSetting = @"";
				colvarAssetTrackingTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssetTrackingTypeName);

				TableSchema.TableColumn colvarIsTracked = new TableSchema.TableColumn(schema);
				colvarIsTracked.ColumnName = "IsTracked";
				colvarIsTracked.DataType = DbType.Boolean;
				colvarIsTracked.MaxLength = 0;
				colvarIsTracked.AutoIncrement = false;
				colvarIsTracked.IsNullable = false;
				colvarIsTracked.IsPrimaryKey = false;
				colvarIsTracked.IsForeignKey = false;
				colvarIsTracked.IsReadOnly = false;
				colvarIsTracked.DefaultSetting = @"";
				colvarIsTracked.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsTracked);

				TableSchema.TableColumn colvarAssignsToOffice = new TableSchema.TableColumn(schema);
				colvarAssignsToOffice.ColumnName = "AssignsToOffice";
				colvarAssignsToOffice.DataType = DbType.Boolean;
				colvarAssignsToOffice.MaxLength = 0;
				colvarAssignsToOffice.AutoIncrement = false;
				colvarAssignsToOffice.IsNullable = true;
				colvarAssignsToOffice.IsPrimaryKey = false;
				colvarAssignsToOffice.IsForeignKey = false;
				colvarAssignsToOffice.IsReadOnly = false;
				colvarAssignsToOffice.DefaultSetting = @"";
				colvarAssignsToOffice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssignsToOffice);

				TableSchema.TableColumn colvarAssignsToTech = new TableSchema.TableColumn(schema);
				colvarAssignsToTech.ColumnName = "AssignsToTech";
				colvarAssignsToTech.DataType = DbType.Boolean;
				colvarAssignsToTech.MaxLength = 0;
				colvarAssignsToTech.AutoIncrement = false;
				colvarAssignsToTech.IsNullable = true;
				colvarAssignsToTech.IsPrimaryKey = false;
				colvarAssignsToTech.IsForeignKey = false;
				colvarAssignsToTech.IsReadOnly = false;
				colvarAssignsToTech.DefaultSetting = @"";
				colvarAssignsToTech.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssignsToTech);

				TableSchema.TableColumn colvarAssignsToCustomer = new TableSchema.TableColumn(schema);
				colvarAssignsToCustomer.ColumnName = "AssignsToCustomer";
				colvarAssignsToCustomer.DataType = DbType.Boolean;
				colvarAssignsToCustomer.MaxLength = 0;
				colvarAssignsToCustomer.AutoIncrement = false;
				colvarAssignsToCustomer.IsNullable = true;
				colvarAssignsToCustomer.IsPrimaryKey = false;
				colvarAssignsToCustomer.IsForeignKey = false;
				colvarAssignsToCustomer.IsReadOnly = false;
				colvarAssignsToCustomer.DefaultSetting = @"";
				colvarAssignsToCustomer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAssignsToCustomer);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_AssetTrackingTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_AssetTrackingType LoadFrom(IV_AssetTrackingType item)
		{
			IV_AssetTrackingType result = new IV_AssetTrackingType();
			if (item.AssetTrackingTypeID != default(string)) {
				result.LoadByKey(item.AssetTrackingTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string AssetTrackingTypeID { 
			get { return GetColumnValue<string>(Columns.AssetTrackingTypeID); }
			set {
				SetColumnValue(Columns.AssetTrackingTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTrackingTypeID));
			}
		}
		[DataMember]
		public string AssetTrackingTypeName { 
			get { return GetColumnValue<string>(Columns.AssetTrackingTypeName); }
			set {
				SetColumnValue(Columns.AssetTrackingTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssetTrackingTypeName));
			}
		}
		[DataMember]
		public bool IsTracked { 
			get { return GetColumnValue<bool>(Columns.IsTracked); }
			set {
				SetColumnValue(Columns.IsTracked, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsTracked));
			}
		}
		[DataMember]
		public bool? AssignsToOffice { 
			get { return GetColumnValue<bool?>(Columns.AssignsToOffice); }
			set {
				SetColumnValue(Columns.AssignsToOffice, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssignsToOffice));
			}
		}
		[DataMember]
		public bool? AssignsToTech { 
			get { return GetColumnValue<bool?>(Columns.AssignsToTech); }
			set {
				SetColumnValue(Columns.AssignsToTech, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssignsToTech));
			}
		}
		[DataMember]
		public bool? AssignsToCustomer { 
			get { return GetColumnValue<bool?>(Columns.AssignsToCustomer); }
			set {
				SetColumnValue(Columns.AssignsToCustomer, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AssignsToCustomer));
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


		public override string ToString()
		{
			return AssetTrackingTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn AssetTrackingTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AssetTrackingTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn IsTrackedColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AssignsToOfficeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn AssignsToTechColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AssignsToCustomerColumn
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
			public static readonly string AssetTrackingTypeID = @"AssetTrackingTypeID";
			public static readonly string AssetTrackingTypeName = @"AssetTrackingTypeName";
			public static readonly string IsTracked = @"IsTracked";
			public static readonly string AssignsToOffice = @"AssignsToOffice";
			public static readonly string AssignsToTech = @"AssignsToTech";
			public static readonly string AssignsToCustomer = @"AssignsToCustomer";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return AssetTrackingTypeID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_OfficeAudit class.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeAuditCollection : ActiveList<IV_OfficeAudit, IV_OfficeAuditCollection>
	{
		public static IV_OfficeAuditCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_OfficeAuditCollection result = new IV_OfficeAuditCollection();
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
			foreach (IV_OfficeAudit item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_OfficeAudits table.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeAudit : ActiveRecord<IV_OfficeAudit>, INotifyPropertyChanged
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

		public IV_OfficeAudit()
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
				TableSchema.Table schema = new TableSchema.Table("IV_OfficeAudits", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarOfficeAuditID = new TableSchema.TableColumn(schema);
				colvarOfficeAuditID.ColumnName = "OfficeAuditID";
				colvarOfficeAuditID.DataType = DbType.Int32;
				colvarOfficeAuditID.MaxLength = 0;
				colvarOfficeAuditID.AutoIncrement = true;
				colvarOfficeAuditID.IsNullable = false;
				colvarOfficeAuditID.IsPrimaryKey = true;
				colvarOfficeAuditID.IsForeignKey = false;
				colvarOfficeAuditID.IsReadOnly = false;
				colvarOfficeAuditID.DefaultSetting = @"";
				colvarOfficeAuditID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeAuditID);

				TableSchema.TableColumn colvarOfficeId = new TableSchema.TableColumn(schema);
				colvarOfficeId.ColumnName = "OfficeId";
				colvarOfficeId.DataType = DbType.Int32;
				colvarOfficeId.MaxLength = 0;
				colvarOfficeId.AutoIncrement = false;
				colvarOfficeId.IsNullable = false;
				colvarOfficeId.IsPrimaryKey = false;
				colvarOfficeId.IsForeignKey = true;
				colvarOfficeId.IsReadOnly = false;
				colvarOfficeId.DefaultSetting = @"";
				colvarOfficeId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeId);

				TableSchema.TableColumn colvarEquipmentId = new TableSchema.TableColumn(schema);
				colvarEquipmentId.ColumnName = "EquipmentId";
				colvarEquipmentId.DataType = DbType.Int32;
				colvarEquipmentId.MaxLength = 0;
				colvarEquipmentId.AutoIncrement = false;
				colvarEquipmentId.IsNullable = false;
				colvarEquipmentId.IsPrimaryKey = false;
				colvarEquipmentId.IsForeignKey = true;
				colvarEquipmentId.IsReadOnly = false;
				colvarEquipmentId.DefaultSetting = @"";
				colvarEquipmentId.ForeignKeyTableName = "EP_Equipments";
				schema.Columns.Add(colvarEquipmentId);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_OfficeAudits",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_OfficeAudit LoadFrom(IV_OfficeAudit item)
		{
			IV_OfficeAudit result = new IV_OfficeAudit();
			if (item.OfficeAuditID != default(int)) {
				result.LoadByKey(item.OfficeAuditID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int OfficeAuditID { 
			get { return GetColumnValue<int>(Columns.OfficeAuditID); }
			set {
				SetColumnValue(Columns.OfficeAuditID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeAuditID));
			}
		}
		[DataMember]
		public int OfficeId { 
			get { return GetColumnValue<int>(Columns.OfficeId); }
			set {
				SetColumnValue(Columns.OfficeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeId));
			}
		}
		[DataMember]
		public int EquipmentId { 
			get { return GetColumnValue<int>(Columns.EquipmentId); }
			set {
				SetColumnValue(Columns.EquipmentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentId));
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

		private EP_Equipment _Equipment;
		//Relationship: FK_IV_OfficeAudits_drp_Equipments
		public EP_Equipment Equipment
		{
			get
			{
				if(_Equipment == null) {
					_Equipment = EP_Equipment.FetchByID(this.EquipmentId);
				}
				return _Equipment;
			}
			set
			{
				SetColumnValue("EquipmentId", value.EquipmentID);
				_Equipment = value;
			}
		}

		private IV_Office _Office;
		//Relationship: FK_IV_OfficeAudits_Offices
		public IV_Office Office
		{
			get
			{
				if(_Office == null) {
					_Office = IV_Office.FetchByID(this.OfficeId);
				}
				return _Office;
			}
			set
			{
				SetColumnValue("OfficeId", value.OfficeID);
				_Office = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return OfficeAuditID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn OfficeAuditIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn OfficeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentIdColumn
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
			public static readonly string OfficeAuditID = @"OfficeAuditID";
			public static readonly string OfficeId = @"OfficeId";
			public static readonly string EquipmentId = @"EquipmentId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return OfficeAuditID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_Office class.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeCollection : ActiveList<IV_Office, IV_OfficeCollection>
	{
		public static IV_OfficeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_OfficeCollection result = new IV_OfficeCollection();
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
			foreach (IV_Office item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_Offices table.
	/// </summary>
	[DataContract]
	public partial class IV_Office : ActiveRecord<IV_Office>, INotifyPropertyChanged
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

		public IV_Office()
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
				TableSchema.Table schema = new TableSchema.Table("IV_Offices", TableType.Table, DataService.GetInstance("InventoryProvider"));
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
				colvarTimeZoneId.DefaultSetting = @"((0))";
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
				DataService.Providers["InventoryProvider"].AddSchema("IV_Offices",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_Office LoadFrom(IV_Office item)
		{
			IV_Office result = new IV_Office();
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
	/// Strongly-typed collection for the IV_OfficeSafetyStock class.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeSafetyStockCollection : ActiveList<IV_OfficeSafetyStock, IV_OfficeSafetyStockCollection>
	{
		public static IV_OfficeSafetyStockCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_OfficeSafetyStockCollection result = new IV_OfficeSafetyStockCollection();
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
			foreach (IV_OfficeSafetyStock item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_OfficeSafetyStock table.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeSafetyStock : ActiveRecord<IV_OfficeSafetyStock>, INotifyPropertyChanged
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

		public IV_OfficeSafetyStock()
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
				TableSchema.Table schema = new TableSchema.Table("IV_OfficeSafetyStock", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarOfficeSafetyStockID = new TableSchema.TableColumn(schema);
				colvarOfficeSafetyStockID.ColumnName = "OfficeSafetyStockID";
				colvarOfficeSafetyStockID.DataType = DbType.Int32;
				colvarOfficeSafetyStockID.MaxLength = 0;
				colvarOfficeSafetyStockID.AutoIncrement = true;
				colvarOfficeSafetyStockID.IsNullable = false;
				colvarOfficeSafetyStockID.IsPrimaryKey = true;
				colvarOfficeSafetyStockID.IsForeignKey = false;
				colvarOfficeSafetyStockID.IsReadOnly = false;
				colvarOfficeSafetyStockID.DefaultSetting = @"";
				colvarOfficeSafetyStockID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeSafetyStockID);

				TableSchema.TableColumn colvarOfficeId = new TableSchema.TableColumn(schema);
				colvarOfficeId.ColumnName = "OfficeId";
				colvarOfficeId.DataType = DbType.Int32;
				colvarOfficeId.MaxLength = 0;
				colvarOfficeId.AutoIncrement = false;
				colvarOfficeId.IsNullable = false;
				colvarOfficeId.IsPrimaryKey = false;
				colvarOfficeId.IsForeignKey = true;
				colvarOfficeId.IsReadOnly = false;
				colvarOfficeId.DefaultSetting = @"";
				colvarOfficeId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeId);

				TableSchema.TableColumn colvarEquipmentId = new TableSchema.TableColumn(schema);
				colvarEquipmentId.ColumnName = "EquipmentId";
				colvarEquipmentId.DataType = DbType.Int32;
				colvarEquipmentId.MaxLength = 0;
				colvarEquipmentId.AutoIncrement = false;
				colvarEquipmentId.IsNullable = false;
				colvarEquipmentId.IsPrimaryKey = false;
				colvarEquipmentId.IsForeignKey = true;
				colvarEquipmentId.IsReadOnly = false;
				colvarEquipmentId.DefaultSetting = @"";
				colvarEquipmentId.ForeignKeyTableName = "EP_Equipments";
				schema.Columns.Add(colvarEquipmentId);

				TableSchema.TableColumn colvarSafetyStock = new TableSchema.TableColumn(schema);
				colvarSafetyStock.ColumnName = "SafetyStock";
				colvarSafetyStock.DataType = DbType.Int32;
				colvarSafetyStock.MaxLength = 0;
				colvarSafetyStock.AutoIncrement = false;
				colvarSafetyStock.IsNullable = false;
				colvarSafetyStock.IsPrimaryKey = false;
				colvarSafetyStock.IsForeignKey = false;
				colvarSafetyStock.IsReadOnly = false;
				colvarSafetyStock.DefaultSetting = @"";
				colvarSafetyStock.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSafetyStock);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_OfficeSafetyStock",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_OfficeSafetyStock LoadFrom(IV_OfficeSafetyStock item)
		{
			IV_OfficeSafetyStock result = new IV_OfficeSafetyStock();
			if (item.OfficeSafetyStockID != default(int)) {
				result.LoadByKey(item.OfficeSafetyStockID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int OfficeSafetyStockID { 
			get { return GetColumnValue<int>(Columns.OfficeSafetyStockID); }
			set {
				SetColumnValue(Columns.OfficeSafetyStockID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeSafetyStockID));
			}
		}
		[DataMember]
		public int OfficeId { 
			get { return GetColumnValue<int>(Columns.OfficeId); }
			set {
				SetColumnValue(Columns.OfficeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeId));
			}
		}
		[DataMember]
		public int EquipmentId { 
			get { return GetColumnValue<int>(Columns.EquipmentId); }
			set {
				SetColumnValue(Columns.EquipmentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentId));
			}
		}
		[DataMember]
		public int SafetyStock { 
			get { return GetColumnValue<int>(Columns.SafetyStock); }
			set {
				SetColumnValue(Columns.SafetyStock, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SafetyStock));
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

		#endregion //Properties

		#region ForeignKey Properties

		private EP_Equipment _Equipment;
		//Relationship: FK_IV_OfficeSafetyStock_drp_Equipments
		public EP_Equipment Equipment
		{
			get
			{
				if(_Equipment == null) {
					_Equipment = EP_Equipment.FetchByID(this.EquipmentId);
				}
				return _Equipment;
			}
			set
			{
				SetColumnValue("EquipmentId", value.EquipmentID);
				_Equipment = value;
			}
		}

		private IV_Office _Office;
		//Relationship: FK_IV_OfficeSafetyStock_Offices
		public IV_Office Office
		{
			get
			{
				if(_Office == null) {
					_Office = IV_Office.FetchByID(this.OfficeId);
				}
				return _Office;
			}
			set
			{
				SetColumnValue("OfficeId", value.OfficeID);
				_Office = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return OfficeSafetyStockID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn OfficeSafetyStockIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn OfficeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SafetyStockColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
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
			public static readonly string OfficeSafetyStockID = @"OfficeSafetyStockID";
			public static readonly string OfficeId = @"OfficeId";
			public static readonly string EquipmentId = @"EquipmentId";
			public static readonly string SafetyStock = @"SafetyStock";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return OfficeSafetyStockID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_OfficeTransfer class.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeTransferCollection : ActiveList<IV_OfficeTransfer, IV_OfficeTransferCollection>
	{
		public static IV_OfficeTransferCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_OfficeTransferCollection result = new IV_OfficeTransferCollection();
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
			foreach (IV_OfficeTransfer item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_OfficeTransfers table.
	/// </summary>
	[DataContract]
	public partial class IV_OfficeTransfer : ActiveRecord<IV_OfficeTransfer>, INotifyPropertyChanged
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

		public IV_OfficeTransfer()
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
				TableSchema.Table schema = new TableSchema.Table("IV_OfficeTransfers", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarOfficeTransferID = new TableSchema.TableColumn(schema);
				colvarOfficeTransferID.ColumnName = "OfficeTransferID";
				colvarOfficeTransferID.DataType = DbType.Int32;
				colvarOfficeTransferID.MaxLength = 0;
				colvarOfficeTransferID.AutoIncrement = true;
				colvarOfficeTransferID.IsNullable = false;
				colvarOfficeTransferID.IsPrimaryKey = true;
				colvarOfficeTransferID.IsForeignKey = false;
				colvarOfficeTransferID.IsReadOnly = false;
				colvarOfficeTransferID.DefaultSetting = @"";
				colvarOfficeTransferID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeTransferID);

				TableSchema.TableColumn colvarOfficeFromId = new TableSchema.TableColumn(schema);
				colvarOfficeFromId.ColumnName = "OfficeFromId";
				colvarOfficeFromId.DataType = DbType.Int32;
				colvarOfficeFromId.MaxLength = 0;
				colvarOfficeFromId.AutoIncrement = false;
				colvarOfficeFromId.IsNullable = false;
				colvarOfficeFromId.IsPrimaryKey = false;
				colvarOfficeFromId.IsForeignKey = true;
				colvarOfficeFromId.IsReadOnly = false;
				colvarOfficeFromId.DefaultSetting = @"";
				colvarOfficeFromId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeFromId);

				TableSchema.TableColumn colvarOfficeToId = new TableSchema.TableColumn(schema);
				colvarOfficeToId.ColumnName = "OfficeToId";
				colvarOfficeToId.DataType = DbType.Int32;
				colvarOfficeToId.MaxLength = 0;
				colvarOfficeToId.AutoIncrement = false;
				colvarOfficeToId.IsNullable = false;
				colvarOfficeToId.IsPrimaryKey = false;
				colvarOfficeToId.IsForeignKey = true;
				colvarOfficeToId.IsReadOnly = false;
				colvarOfficeToId.DefaultSetting = @"";
				colvarOfficeToId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeToId);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_OfficeTransfers",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_OfficeTransfer LoadFrom(IV_OfficeTransfer item)
		{
			IV_OfficeTransfer result = new IV_OfficeTransfer();
			if (item.OfficeTransferID != default(int)) {
				result.LoadByKey(item.OfficeTransferID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int OfficeTransferID { 
			get { return GetColumnValue<int>(Columns.OfficeTransferID); }
			set {
				SetColumnValue(Columns.OfficeTransferID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeTransferID));
			}
		}
		[DataMember]
		public int OfficeFromId { 
			get { return GetColumnValue<int>(Columns.OfficeFromId); }
			set {
				SetColumnValue(Columns.OfficeFromId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeFromId));
			}
		}
		[DataMember]
		public int OfficeToId { 
			get { return GetColumnValue<int>(Columns.OfficeToId); }
			set {
				SetColumnValue(Columns.OfficeToId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeToId));
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

		private IV_Office _OfficeFrom;
		//Relationship: FK_IV_OfficeTransfers_Offices
		public IV_Office OfficeFrom
		{
			get
			{
				if(_OfficeFrom == null) {
					_OfficeFrom = IV_Office.FetchByID(this.OfficeFromId);
				}
				return _OfficeFrom;
			}
			set
			{
				SetColumnValue("OfficeFromId", value.OfficeID);
				_OfficeFrom = value;
			}
		}

		private IV_Office _OfficeTo;
		//Relationship: FK_IV_OfficeTransfers_Offices1
		public IV_Office OfficeTo
		{
			get
			{
				if(_OfficeTo == null) {
					_OfficeTo = IV_Office.FetchByID(this.OfficeToId);
				}
				return _OfficeTo;
			}
			set
			{
				SetColumnValue("OfficeToId", value.OfficeID);
				_OfficeTo = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return OfficeTransferID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn OfficeTransferIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn OfficeFromIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn OfficeToIdColumn
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
			public static readonly string OfficeTransferID = @"OfficeTransferID";
			public static readonly string OfficeFromId = @"OfficeFromId";
			public static readonly string OfficeToId = @"OfficeToId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return OfficeTransferID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_PurchaseOrderItem class.
	/// </summary>
	[DataContract]
	public partial class IV_PurchaseOrderItemCollection : ActiveList<IV_PurchaseOrderItem, IV_PurchaseOrderItemCollection>
	{
		public static IV_PurchaseOrderItemCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_PurchaseOrderItemCollection result = new IV_PurchaseOrderItemCollection();
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
			foreach (IV_PurchaseOrderItem item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_PurchaseOrderItems table.
	/// </summary>
	[DataContract]
	public partial class IV_PurchaseOrderItem : ActiveRecord<IV_PurchaseOrderItem>, INotifyPropertyChanged
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

		public IV_PurchaseOrderItem()
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
				TableSchema.Table schema = new TableSchema.Table("IV_PurchaseOrderItems", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchaseOrderItemID = new TableSchema.TableColumn(schema);
				colvarPurchaseOrderItemID.ColumnName = "PurchaseOrderItemID";
				colvarPurchaseOrderItemID.DataType = DbType.Int64;
				colvarPurchaseOrderItemID.MaxLength = 0;
				colvarPurchaseOrderItemID.AutoIncrement = true;
				colvarPurchaseOrderItemID.IsNullable = false;
				colvarPurchaseOrderItemID.IsPrimaryKey = true;
				colvarPurchaseOrderItemID.IsForeignKey = false;
				colvarPurchaseOrderItemID.IsReadOnly = false;
				colvarPurchaseOrderItemID.DefaultSetting = @"";
				colvarPurchaseOrderItemID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseOrderItemID);

				TableSchema.TableColumn colvarPurchaseOrderId = new TableSchema.TableColumn(schema);
				colvarPurchaseOrderId.ColumnName = "PurchaseOrderId";
				colvarPurchaseOrderId.DataType = DbType.Int32;
				colvarPurchaseOrderId.MaxLength = 0;
				colvarPurchaseOrderId.AutoIncrement = false;
				colvarPurchaseOrderId.IsNullable = false;
				colvarPurchaseOrderId.IsPrimaryKey = false;
				colvarPurchaseOrderId.IsForeignKey = true;
				colvarPurchaseOrderId.IsReadOnly = false;
				colvarPurchaseOrderId.DefaultSetting = @"";
				colvarPurchaseOrderId.ForeignKeyTableName = "IV_PurchaseOrders";
				schema.Columns.Add(colvarPurchaseOrderId);

				TableSchema.TableColumn colvarEquipmentId = new TableSchema.TableColumn(schema);
				colvarEquipmentId.ColumnName = "EquipmentId";
				colvarEquipmentId.DataType = DbType.Int32;
				colvarEquipmentId.MaxLength = 0;
				colvarEquipmentId.AutoIncrement = false;
				colvarEquipmentId.IsNullable = false;
				colvarEquipmentId.IsPrimaryKey = false;
				colvarEquipmentId.IsForeignKey = true;
				colvarEquipmentId.IsReadOnly = false;
				colvarEquipmentId.DefaultSetting = @"";
				colvarEquipmentId.ForeignKeyTableName = "EP_Equipments";
				schema.Columns.Add(colvarEquipmentId);

				TableSchema.TableColumn colvarQty = new TableSchema.TableColumn(schema);
				colvarQty.ColumnName = "Qty";
				colvarQty.DataType = DbType.Int32;
				colvarQty.MaxLength = 0;
				colvarQty.AutoIncrement = false;
				colvarQty.IsNullable = false;
				colvarQty.IsPrimaryKey = false;
				colvarQty.IsForeignKey = false;
				colvarQty.IsReadOnly = false;
				colvarQty.DefaultSetting = @"((1))";
				colvarQty.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQty);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_PurchaseOrderItems",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_PurchaseOrderItem LoadFrom(IV_PurchaseOrderItem item)
		{
			IV_PurchaseOrderItem result = new IV_PurchaseOrderItem();
			if (item.PurchaseOrderItemID != default(long)) {
				result.LoadByKey(item.PurchaseOrderItemID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long PurchaseOrderItemID { 
			get { return GetColumnValue<long>(Columns.PurchaseOrderItemID); }
			set {
				SetColumnValue(Columns.PurchaseOrderItemID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseOrderItemID));
			}
		}
		[DataMember]
		public int PurchaseOrderId { 
			get { return GetColumnValue<int>(Columns.PurchaseOrderId); }
			set {
				SetColumnValue(Columns.PurchaseOrderId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseOrderId));
			}
		}
		[DataMember]
		public int EquipmentId { 
			get { return GetColumnValue<int>(Columns.EquipmentId); }
			set {
				SetColumnValue(Columns.EquipmentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentId));
			}
		}
		[DataMember]
		public int Qty { 
			get { return GetColumnValue<int>(Columns.Qty); }
			set {
				SetColumnValue(Columns.Qty, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Qty));
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

		#endregion //Properties

		#region ForeignKey Properties

		private EP_Equipment _Equipment;
		//Relationship: FK_IV_PurchaseOrderItems_drp_Equipments
		public EP_Equipment Equipment
		{
			get
			{
				if(_Equipment == null) {
					_Equipment = EP_Equipment.FetchByID(this.EquipmentId);
				}
				return _Equipment;
			}
			set
			{
				SetColumnValue("EquipmentId", value.EquipmentID);
				_Equipment = value;
			}
		}

		private IV_PurchaseOrder _PurchaseOrder;
		//Relationship: FK_IV_PurchaseOrderItems_IV_PurchaseOrders
		public IV_PurchaseOrder PurchaseOrder
		{
			get
			{
				if(_PurchaseOrder == null) {
					_PurchaseOrder = IV_PurchaseOrder.FetchByID(this.PurchaseOrderId);
				}
				return _PurchaseOrder;
			}
			set
			{
				SetColumnValue("PurchaseOrderId", value.PurchaseOrderID);
				_PurchaseOrder = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PurchaseOrderItemID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchaseOrderItemIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PurchaseOrderIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn QtyColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchaseOrderItemID = @"PurchaseOrderItemID";
			public static readonly string PurchaseOrderId = @"PurchaseOrderId";
			public static readonly string EquipmentId = @"EquipmentId";
			public static readonly string Qty = @"Qty";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return PurchaseOrderItemID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_PurchaseOrder class.
	/// </summary>
	[DataContract]
	public partial class IV_PurchaseOrderCollection : ActiveList<IV_PurchaseOrder, IV_PurchaseOrderCollection>
	{
		public static IV_PurchaseOrderCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_PurchaseOrderCollection result = new IV_PurchaseOrderCollection();
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
			foreach (IV_PurchaseOrder item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_PurchaseOrders table.
	/// </summary>
	[DataContract]
	public partial class IV_PurchaseOrder : ActiveRecord<IV_PurchaseOrder>, INotifyPropertyChanged
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

		public IV_PurchaseOrder()
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
				TableSchema.Table schema = new TableSchema.Table("IV_PurchaseOrders", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPurchaseOrderID = new TableSchema.TableColumn(schema);
				colvarPurchaseOrderID.ColumnName = "PurchaseOrderID";
				colvarPurchaseOrderID.DataType = DbType.Int32;
				colvarPurchaseOrderID.MaxLength = 0;
				colvarPurchaseOrderID.AutoIncrement = true;
				colvarPurchaseOrderID.IsNullable = false;
				colvarPurchaseOrderID.IsPrimaryKey = true;
				colvarPurchaseOrderID.IsForeignKey = false;
				colvarPurchaseOrderID.IsReadOnly = false;
				colvarPurchaseOrderID.DefaultSetting = @"";
				colvarPurchaseOrderID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseOrderID);

				TableSchema.TableColumn colvarSupplierId = new TableSchema.TableColumn(schema);
				colvarSupplierId.ColumnName = "SupplierId";
				colvarSupplierId.DataType = DbType.AnsiString;
				colvarSupplierId.MaxLength = 20;
				colvarSupplierId.AutoIncrement = false;
				colvarSupplierId.IsNullable = false;
				colvarSupplierId.IsPrimaryKey = false;
				colvarSupplierId.IsForeignKey = true;
				colvarSupplierId.IsReadOnly = false;
				colvarSupplierId.DefaultSetting = @"";
				colvarSupplierId.ForeignKeyTableName = "IV_Supplier";
				schema.Columns.Add(colvarSupplierId);

				TableSchema.TableColumn colvarOfficeId = new TableSchema.TableColumn(schema);
				colvarOfficeId.ColumnName = "OfficeId";
				colvarOfficeId.DataType = DbType.Int32;
				colvarOfficeId.MaxLength = 0;
				colvarOfficeId.AutoIncrement = false;
				colvarOfficeId.IsNullable = false;
				colvarOfficeId.IsPrimaryKey = false;
				colvarOfficeId.IsForeignKey = true;
				colvarOfficeId.IsReadOnly = false;
				colvarOfficeId.DefaultSetting = @"";
				colvarOfficeId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeId);

				TableSchema.TableColumn colvarArOrderNumber = new TableSchema.TableColumn(schema);
				colvarArOrderNumber.ColumnName = "ArOrderNumber";
				colvarArOrderNumber.DataType = DbType.AnsiString;
				colvarArOrderNumber.MaxLength = 50;
				colvarArOrderNumber.AutoIncrement = false;
				colvarArOrderNumber.IsNullable = true;
				colvarArOrderNumber.IsPrimaryKey = false;
				colvarArOrderNumber.IsForeignKey = false;
				colvarArOrderNumber.IsReadOnly = false;
				colvarArOrderNumber.DefaultSetting = @"";
				colvarArOrderNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarArOrderNumber);

				TableSchema.TableColumn colvarPoClosedDate = new TableSchema.TableColumn(schema);
				colvarPoClosedDate.ColumnName = "PoClosedDate";
				colvarPoClosedDate.DataType = DbType.DateTime;
				colvarPoClosedDate.MaxLength = 0;
				colvarPoClosedDate.AutoIncrement = false;
				colvarPoClosedDate.IsNullable = true;
				colvarPoClosedDate.IsPrimaryKey = false;
				colvarPoClosedDate.IsForeignKey = false;
				colvarPoClosedDate.IsReadOnly = false;
				colvarPoClosedDate.DefaultSetting = @"";
				colvarPoClosedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPoClosedDate);

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

				TableSchema.TableColumn colvarOrderedBy = new TableSchema.TableColumn(schema);
				colvarOrderedBy.ColumnName = "OrderedBy";
				colvarOrderedBy.DataType = DbType.AnsiString;
				colvarOrderedBy.MaxLength = 50;
				colvarOrderedBy.AutoIncrement = false;
				colvarOrderedBy.IsNullable = false;
				colvarOrderedBy.IsPrimaryKey = false;
				colvarOrderedBy.IsForeignKey = false;
				colvarOrderedBy.IsReadOnly = false;
				colvarOrderedBy.DefaultSetting = @"";
				colvarOrderedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOrderedBy);

				TableSchema.TableColumn colvarOrderedOn = new TableSchema.TableColumn(schema);
				colvarOrderedOn.ColumnName = "OrderedOn";
				colvarOrderedOn.DataType = DbType.DateTime;
				colvarOrderedOn.MaxLength = 0;
				colvarOrderedOn.AutoIncrement = false;
				colvarOrderedOn.IsNullable = false;
				colvarOrderedOn.IsPrimaryKey = false;
				colvarOrderedOn.IsForeignKey = false;
				colvarOrderedOn.IsReadOnly = false;
				colvarOrderedOn.DefaultSetting = @"";
				colvarOrderedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOrderedOn);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_PurchaseOrders",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_PurchaseOrder LoadFrom(IV_PurchaseOrder item)
		{
			IV_PurchaseOrder result = new IV_PurchaseOrder();
			if (item.PurchaseOrderID != default(int)) {
				result.LoadByKey(item.PurchaseOrderID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PurchaseOrderID { 
			get { return GetColumnValue<int>(Columns.PurchaseOrderID); }
			set {
				SetColumnValue(Columns.PurchaseOrderID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PurchaseOrderID));
			}
		}
		[DataMember]
		public string SupplierId { 
			get { return GetColumnValue<string>(Columns.SupplierId); }
			set {
				SetColumnValue(Columns.SupplierId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SupplierId));
			}
		}
		[DataMember]
		public int OfficeId { 
			get { return GetColumnValue<int>(Columns.OfficeId); }
			set {
				SetColumnValue(Columns.OfficeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeId));
			}
		}
		[DataMember]
		public string ArOrderNumber { 
			get { return GetColumnValue<string>(Columns.ArOrderNumber); }
			set {
				SetColumnValue(Columns.ArOrderNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ArOrderNumber));
			}
		}
		[DataMember]
		public DateTime? PoClosedDate { 
			get { return GetColumnValue<DateTime?>(Columns.PoClosedDate); }
			set {
				SetColumnValue(Columns.PoClosedDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PoClosedDate));
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
		public string OrderedBy { 
			get { return GetColumnValue<string>(Columns.OrderedBy); }
			set {
				SetColumnValue(Columns.OrderedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OrderedBy));
			}
		}
		[DataMember]
		public DateTime OrderedOn { 
			get { return GetColumnValue<DateTime>(Columns.OrderedOn); }
			set {
				SetColumnValue(Columns.OrderedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OrderedOn));
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

		#endregion //Properties

		#region ForeignKey Properties

		private IV_Supplier _Supplier;
		//Relationship: FK_IV_PurchaseOrders_IV_Supplier
		public IV_Supplier Supplier
		{
			get
			{
				if(_Supplier == null) {
					_Supplier = IV_Supplier.FetchByID(this.SupplierId);
				}
				return _Supplier;
			}
			set
			{
				SetColumnValue("SupplierId", value.SupplierID);
				_Supplier = value;
			}
		}

		private IV_Office _Office;
		//Relationship: FK_IV_PurchaseOrders_Offices
		public IV_Office Office
		{
			get
			{
				if(_Office == null) {
					_Office = IV_Office.FetchByID(this.OfficeId);
				}
				return _Office;
			}
			set
			{
				SetColumnValue("OfficeId", value.OfficeID);
				_Office = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return SupplierId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PurchaseOrderIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SupplierIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn OfficeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ArOrderNumberColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PoClosedDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn OrderedByColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn OrderedOnColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PurchaseOrderID = @"PurchaseOrderID";
			public static readonly string SupplierId = @"SupplierId";
			public static readonly string OfficeId = @"OfficeId";
			public static readonly string ArOrderNumber = @"ArOrderNumber";
			public static readonly string PoClosedDate = @"PoClosedDate";
			public static readonly string IsActive = @"IsActive";
			public static readonly string OrderedBy = @"OrderedBy";
			public static readonly string OrderedOn = @"OrderedOn";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return PurchaseOrderID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_Supplier class.
	/// </summary>
	[DataContract]
	public partial class IV_SupplierCollection : ActiveList<IV_Supplier, IV_SupplierCollection>
	{
		public static IV_SupplierCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_SupplierCollection result = new IV_SupplierCollection();
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
			foreach (IV_Supplier item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_Supplier table.
	/// </summary>
	[DataContract]
	public partial class IV_Supplier : ActiveRecord<IV_Supplier>, INotifyPropertyChanged
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

		public IV_Supplier()
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
				TableSchema.Table schema = new TableSchema.Table("IV_Supplier", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSupplierID = new TableSchema.TableColumn(schema);
				colvarSupplierID.ColumnName = "SupplierID";
				colvarSupplierID.DataType = DbType.AnsiString;
				colvarSupplierID.MaxLength = 20;
				colvarSupplierID.AutoIncrement = false;
				colvarSupplierID.IsNullable = false;
				colvarSupplierID.IsPrimaryKey = true;
				colvarSupplierID.IsForeignKey = false;
				colvarSupplierID.IsReadOnly = false;
				colvarSupplierID.DefaultSetting = @"";
				colvarSupplierID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSupplierID);

				TableSchema.TableColumn colvarSupplierName = new TableSchema.TableColumn(schema);
				colvarSupplierName.ColumnName = "SupplierName";
				colvarSupplierName.DataType = DbType.AnsiString;
				colvarSupplierName.MaxLength = 100;
				colvarSupplierName.AutoIncrement = false;
				colvarSupplierName.IsNullable = false;
				colvarSupplierName.IsPrimaryKey = false;
				colvarSupplierName.IsForeignKey = false;
				colvarSupplierName.IsReadOnly = false;
				colvarSupplierName.DefaultSetting = @"";
				colvarSupplierName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSupplierName);

				TableSchema.TableColumn colvarAbbreviation = new TableSchema.TableColumn(schema);
				colvarAbbreviation.ColumnName = "Abbreviation";
				colvarAbbreviation.DataType = DbType.AnsiString;
				colvarAbbreviation.MaxLength = 50;
				colvarAbbreviation.AutoIncrement = false;
				colvarAbbreviation.IsNullable = false;
				colvarAbbreviation.IsPrimaryKey = false;
				colvarAbbreviation.IsForeignKey = false;
				colvarAbbreviation.IsReadOnly = false;
				colvarAbbreviation.DefaultSetting = @"";
				colvarAbbreviation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAbbreviation);

				TableSchema.TableColumn colvarContactName = new TableSchema.TableColumn(schema);
				colvarContactName.ColumnName = "ContactName";
				colvarContactName.DataType = DbType.AnsiString;
				colvarContactName.MaxLength = 100;
				colvarContactName.AutoIncrement = false;
				colvarContactName.IsNullable = false;
				colvarContactName.IsPrimaryKey = false;
				colvarContactName.IsForeignKey = false;
				colvarContactName.IsReadOnly = false;
				colvarContactName.DefaultSetting = @"";
				colvarContactName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactName);

				TableSchema.TableColumn colvarContactEmail = new TableSchema.TableColumn(schema);
				colvarContactEmail.ColumnName = "ContactEmail";
				colvarContactEmail.DataType = DbType.AnsiString;
				colvarContactEmail.MaxLength = 255;
				colvarContactEmail.AutoIncrement = false;
				colvarContactEmail.IsNullable = false;
				colvarContactEmail.IsPrimaryKey = false;
				colvarContactEmail.IsForeignKey = false;
				colvarContactEmail.IsReadOnly = false;
				colvarContactEmail.DefaultSetting = @"";
				colvarContactEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactEmail);

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
				colvarModifiedBy.DataType = DbType.Guid;
				colvarModifiedBy.MaxLength = 0;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = false;
				colvarModifiedBy.IsPrimaryKey = false;
				colvarModifiedBy.IsForeignKey = false;
				colvarModifiedBy.IsReadOnly = false;
				colvarModifiedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
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
				colvarCreatedBy.DataType = DbType.Guid;
				colvarCreatedBy.MaxLength = 0;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('10000000-1000-1000-1000-100000000001')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_Supplier",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_Supplier LoadFrom(IV_Supplier item)
		{
			IV_Supplier result = new IV_Supplier();
			if (item.SupplierID != default(string)) {
				result.LoadByKey(item.SupplierID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string SupplierID { 
			get { return GetColumnValue<string>(Columns.SupplierID); }
			set {
				SetColumnValue(Columns.SupplierID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SupplierID));
			}
		}
		[DataMember]
		public string SupplierName { 
			get { return GetColumnValue<string>(Columns.SupplierName); }
			set {
				SetColumnValue(Columns.SupplierName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SupplierName));
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
		public string ContactName { 
			get { return GetColumnValue<string>(Columns.ContactName); }
			set {
				SetColumnValue(Columns.ContactName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactName));
			}
		}
		[DataMember]
		public string ContactEmail { 
			get { return GetColumnValue<string>(Columns.ContactEmail); }
			set {
				SetColumnValue(Columns.ContactEmail, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactEmail));
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
		public Guid ModifiedBy { 
			get { return GetColumnValue<Guid>(Columns.ModifiedBy); }
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
		public Guid CreatedBy { 
			get { return GetColumnValue<Guid>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return SupplierName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn SupplierIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SupplierNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AbbreviationColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ContactNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ContactEmailColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SupplierID = @"SupplierID";
			public static readonly string SupplierName = @"SupplierName";
			public static readonly string Abbreviation = @"Abbreviation";
			public static readonly string ContactName = @"ContactName";
			public static readonly string ContactEmail = @"ContactEmail";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return SupplierID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_TechAudit class.
	/// </summary>
	[DataContract]
	public partial class IV_TechAuditCollection : ActiveList<IV_TechAudit, IV_TechAuditCollection>
	{
		public static IV_TechAuditCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_TechAuditCollection result = new IV_TechAuditCollection();
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
			foreach (IV_TechAudit item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_TechAudits table.
	/// </summary>
	[DataContract]
	public partial class IV_TechAudit : ActiveRecord<IV_TechAudit>, INotifyPropertyChanged
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

		public IV_TechAudit()
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
				TableSchema.Table schema = new TableSchema.Table("IV_TechAudits", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTechAuditID = new TableSchema.TableColumn(schema);
				colvarTechAuditID.ColumnName = "TechAuditID";
				colvarTechAuditID.DataType = DbType.Int32;
				colvarTechAuditID.MaxLength = 0;
				colvarTechAuditID.AutoIncrement = true;
				colvarTechAuditID.IsNullable = false;
				colvarTechAuditID.IsPrimaryKey = true;
				colvarTechAuditID.IsForeignKey = false;
				colvarTechAuditID.IsReadOnly = false;
				colvarTechAuditID.DefaultSetting = @"";
				colvarTechAuditID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechAuditID);

				TableSchema.TableColumn colvarOfficeId = new TableSchema.TableColumn(schema);
				colvarOfficeId.ColumnName = "OfficeId";
				colvarOfficeId.DataType = DbType.Int32;
				colvarOfficeId.MaxLength = 0;
				colvarOfficeId.AutoIncrement = false;
				colvarOfficeId.IsNullable = false;
				colvarOfficeId.IsPrimaryKey = false;
				colvarOfficeId.IsForeignKey = true;
				colvarOfficeId.IsReadOnly = false;
				colvarOfficeId.DefaultSetting = @"";
				colvarOfficeId.ForeignKeyTableName = "IV_Offices";
				schema.Columns.Add(colvarOfficeId);

				TableSchema.TableColumn colvarTechId = new TableSchema.TableColumn(schema);
				colvarTechId.ColumnName = "TechId";
				colvarTechId.DataType = DbType.Guid;
				colvarTechId.MaxLength = 0;
				colvarTechId.AutoIncrement = false;
				colvarTechId.IsNullable = false;
				colvarTechId.IsPrimaryKey = false;
				colvarTechId.IsForeignKey = false;
				colvarTechId.IsReadOnly = false;
				colvarTechId.DefaultSetting = @"";
				colvarTechId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechId);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_TechAudits",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_TechAudit LoadFrom(IV_TechAudit item)
		{
			IV_TechAudit result = new IV_TechAudit();
			if (item.TechAuditID != default(int)) {
				result.LoadByKey(item.TechAuditID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int TechAuditID { 
			get { return GetColumnValue<int>(Columns.TechAuditID); }
			set {
				SetColumnValue(Columns.TechAuditID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechAuditID));
			}
		}
		[DataMember]
		public int OfficeId { 
			get { return GetColumnValue<int>(Columns.OfficeId); }
			set {
				SetColumnValue(Columns.OfficeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.OfficeId));
			}
		}
		[DataMember]
		public Guid TechId { 
			get { return GetColumnValue<Guid>(Columns.TechId); }
			set {
				SetColumnValue(Columns.TechId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechId));
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

		private IV_Office _Office;
		//Relationship: FK_IV_TechAudits_Offices
		public IV_Office Office
		{
			get
			{
				if(_Office == null) {
					_Office = IV_Office.FetchByID(this.OfficeId);
				}
				return _Office;
			}
			set
			{
				SetColumnValue("OfficeId", value.OfficeID);
				_Office = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return TechAuditID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TechAuditIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn OfficeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TechIdColumn
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
			public static readonly string TechAuditID = @"TechAuditID";
			public static readonly string OfficeId = @"OfficeId";
			public static readonly string TechId = @"TechId";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return TechAuditID; }
		}
		*/
	}
	/// <summary>
	/// Strongly-typed collection for the IV_TechSafetyStock class.
	/// </summary>
	[DataContract]
	public partial class IV_TechSafetyStockCollection : ActiveList<IV_TechSafetyStock, IV_TechSafetyStockCollection>
	{
		public static IV_TechSafetyStockCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			IV_TechSafetyStockCollection result = new IV_TechSafetyStockCollection();
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
			foreach (IV_TechSafetyStock item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}
	
	/// <summary>
	/// This is an ActiveRecord class which wraps the IV_TechSafetyStock table.
	/// </summary>
	[DataContract]
	public partial class IV_TechSafetyStock : ActiveRecord<IV_TechSafetyStock>, INotifyPropertyChanged
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

		public IV_TechSafetyStock()
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
				TableSchema.Table schema = new TableSchema.Table("IV_TechSafetyStock", TableType.Table, DataService.GetInstance("InventoryProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTechSafetyStockID = new TableSchema.TableColumn(schema);
				colvarTechSafetyStockID.ColumnName = "TechSafetyStockID";
				colvarTechSafetyStockID.DataType = DbType.Int32;
				colvarTechSafetyStockID.MaxLength = 0;
				colvarTechSafetyStockID.AutoIncrement = true;
				colvarTechSafetyStockID.IsNullable = false;
				colvarTechSafetyStockID.IsPrimaryKey = true;
				colvarTechSafetyStockID.IsForeignKey = false;
				colvarTechSafetyStockID.IsReadOnly = false;
				colvarTechSafetyStockID.DefaultSetting = @"";
				colvarTechSafetyStockID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechSafetyStockID);

				TableSchema.TableColumn colvarTechId = new TableSchema.TableColumn(schema);
				colvarTechId.ColumnName = "TechId";
				colvarTechId.DataType = DbType.Guid;
				colvarTechId.MaxLength = 0;
				colvarTechId.AutoIncrement = false;
				colvarTechId.IsNullable = false;
				colvarTechId.IsPrimaryKey = false;
				colvarTechId.IsForeignKey = false;
				colvarTechId.IsReadOnly = false;
				colvarTechId.DefaultSetting = @"";
				colvarTechId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechId);

				TableSchema.TableColumn colvarEquipmentId = new TableSchema.TableColumn(schema);
				colvarEquipmentId.ColumnName = "EquipmentId";
				colvarEquipmentId.DataType = DbType.Int32;
				colvarEquipmentId.MaxLength = 0;
				colvarEquipmentId.AutoIncrement = false;
				colvarEquipmentId.IsNullable = false;
				colvarEquipmentId.IsPrimaryKey = false;
				colvarEquipmentId.IsForeignKey = true;
				colvarEquipmentId.IsReadOnly = false;
				colvarEquipmentId.DefaultSetting = @"";
				colvarEquipmentId.ForeignKeyTableName = "EP_Equipments";
				schema.Columns.Add(colvarEquipmentId);

				TableSchema.TableColumn colvarSafetyStock = new TableSchema.TableColumn(schema);
				colvarSafetyStock.ColumnName = "SafetyStock";
				colvarSafetyStock.DataType = DbType.Int32;
				colvarSafetyStock.MaxLength = 0;
				colvarSafetyStock.AutoIncrement = false;
				colvarSafetyStock.IsNullable = false;
				colvarSafetyStock.IsPrimaryKey = false;
				colvarSafetyStock.IsForeignKey = false;
				colvarSafetyStock.IsReadOnly = false;
				colvarSafetyStock.DefaultSetting = @"";
				colvarSafetyStock.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSafetyStock);

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

				BaseSchema = schema;
				DataService.Providers["InventoryProvider"].AddSchema("IV_TechSafetyStock",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static IV_TechSafetyStock LoadFrom(IV_TechSafetyStock item)
		{
			IV_TechSafetyStock result = new IV_TechSafetyStock();
			if (item.TechSafetyStockID != default(int)) {
				result.LoadByKey(item.TechSafetyStockID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int TechSafetyStockID { 
			get { return GetColumnValue<int>(Columns.TechSafetyStockID); }
			set {
				SetColumnValue(Columns.TechSafetyStockID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechSafetyStockID));
			}
		}
		[DataMember]
		public Guid TechId { 
			get { return GetColumnValue<Guid>(Columns.TechId); }
			set {
				SetColumnValue(Columns.TechId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TechId));
			}
		}
		[DataMember]
		public int EquipmentId { 
			get { return GetColumnValue<int>(Columns.EquipmentId); }
			set {
				SetColumnValue(Columns.EquipmentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.EquipmentId));
			}
		}
		[DataMember]
		public int SafetyStock { 
			get { return GetColumnValue<int>(Columns.SafetyStock); }
			set {
				SetColumnValue(Columns.SafetyStock, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SafetyStock));
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

		#endregion //Properties

		#region ForeignKey Properties

		private EP_Equipment _Equipment;
		//Relationship: FK_IV_TechSafetyStock_drp_Equipments
		public EP_Equipment Equipment
		{
			get
			{
				if(_Equipment == null) {
					_Equipment = EP_Equipment.FetchByID(this.EquipmentId);
				}
				return _Equipment;
			}
			set
			{
				SetColumnValue("EquipmentId", value.EquipmentID);
				_Equipment = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return TechSafetyStockID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TechSafetyStockIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TechIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn EquipmentIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SafetyStockColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
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
			public static readonly string TechSafetyStockID = @"TechSafetyStockID";
			public static readonly string TechId = @"TechId";
			public static readonly string EquipmentId = @"EquipmentId";
			public static readonly string SafetyStock = @"SafetyStock";
			public static readonly string ModifiedOn = @"ModifiedOn";
			public static readonly string ModifiedBy = @"ModifiedBy";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct
		/*
		public override object PrimaryKeyValue
		{
			get { return TechSafetyStockID; }
		}
		*/
	}
}

