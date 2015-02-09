


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

namespace NXS.Data.GreatPlains
{
	/// <summary>
	/// Strongly-typed collection for the BillingContractsView class.
	/// </summary>
	[DataContract]
	public partial class BillingContractsViewCollection : ReadOnlyList<BillingContractsView, BillingContractsViewCollection>
	{
		public static BillingContractsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			BillingContractsViewCollection result = new BillingContractsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwBillingContracts view.
	/// </summary>
	[DataContract]
	public partial class BillingContractsView : ReadOnlyRecord<BillingContractsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwBillingContracts", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiString;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = true;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarContractNumber = new TableSchema.TableColumn(schema);
				colvarContractNumber.ColumnName = "ContractNumber";
				colvarContractNumber.DataType = DbType.AnsiString;
				colvarContractNumber.MaxLength = 11;
				colvarContractNumber.AutoIncrement = false;
				colvarContractNumber.IsNullable = true;
				colvarContractNumber.IsPrimaryKey = false;
				colvarContractNumber.IsForeignKey = false;
				colvarContractNumber.IsReadOnly = false;
				colvarContractNumber.DefaultSetting = @"";
				colvarContractNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNumber);

				TableSchema.TableColumn colvarContractType = new TableSchema.TableColumn(schema);
				colvarContractType.ColumnName = "ContractType";
				colvarContractType.DataType = DbType.AnsiString;
				colvarContractType.MaxLength = 11;
				colvarContractType.AutoIncrement = false;
				colvarContractType.IsNullable = true;
				colvarContractType.IsPrimaryKey = false;
				colvarContractType.IsForeignKey = false;
				colvarContractType.IsReadOnly = false;
				colvarContractType.DefaultSetting = @"";
				colvarContractType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractType);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.AnsiString;
				colvarDescription.MaxLength = 31;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarBillingPeriod = new TableSchema.TableColumn(schema);
				colvarBillingPeriod.ColumnName = "BillingPeriod";
				colvarBillingPeriod.DataType = DbType.Int16;
				colvarBillingPeriod.MaxLength = 0;
				colvarBillingPeriod.AutoIncrement = false;
				colvarBillingPeriod.IsNullable = false;
				colvarBillingPeriod.IsPrimaryKey = false;
				colvarBillingPeriod.IsForeignKey = false;
				colvarBillingPeriod.IsReadOnly = false;
				colvarBillingPeriod.DefaultSetting = @"";
				colvarBillingPeriod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingPeriod);

				TableSchema.TableColumn colvarPriceSchedule = new TableSchema.TableColumn(schema);
				colvarPriceSchedule.ColumnName = "PriceSchedule";
				colvarPriceSchedule.DataType = DbType.Currency;
				colvarPriceSchedule.MaxLength = 0;
				colvarPriceSchedule.AutoIncrement = false;
				colvarPriceSchedule.IsNullable = true;
				colvarPriceSchedule.IsPrimaryKey = false;
				colvarPriceSchedule.IsForeignKey = false;
				colvarPriceSchedule.IsReadOnly = false;
				colvarPriceSchedule.DefaultSetting = @"";
				colvarPriceSchedule.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPriceSchedule);

				TableSchema.TableColumn colvarContractLength = new TableSchema.TableColumn(schema);
				colvarContractLength.ColumnName = "ContractLength";
				colvarContractLength.DataType = DbType.Int16;
				colvarContractLength.MaxLength = 0;
				colvarContractLength.AutoIncrement = false;
				colvarContractLength.IsNullable = false;
				colvarContractLength.IsPrimaryKey = false;
				colvarContractLength.IsForeignKey = false;
				colvarContractLength.IsReadOnly = false;
				colvarContractLength.DefaultSetting = @"";
				colvarContractLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractLength);

				TableSchema.TableColumn colvarServiceType = new TableSchema.TableColumn(schema);
				colvarServiceType.ColumnName = "ServiceType";
				colvarServiceType.DataType = DbType.AnsiString;
				colvarServiceType.MaxLength = 11;
				colvarServiceType.AutoIncrement = false;
				colvarServiceType.IsNullable = true;
				colvarServiceType.IsPrimaryKey = false;
				colvarServiceType.IsForeignKey = false;
				colvarServiceType.IsReadOnly = false;
				colvarServiceType.DefaultSetting = @"";
				colvarServiceType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceType);

				TableSchema.TableColumn colvarStartDate = new TableSchema.TableColumn(schema);
				colvarStartDate.ColumnName = "StartDate";
				colvarStartDate.DataType = DbType.DateTime;
				colvarStartDate.MaxLength = 0;
				colvarStartDate.AutoIncrement = false;
				colvarStartDate.IsNullable = false;
				colvarStartDate.IsPrimaryKey = false;
				colvarStartDate.IsForeignKey = false;
				colvarStartDate.IsReadOnly = false;
				colvarStartDate.DefaultSetting = @"";
				colvarStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartDate);

				TableSchema.TableColumn colvarEndDate = new TableSchema.TableColumn(schema);
				colvarEndDate.ColumnName = "EndDate";
				colvarEndDate.DataType = DbType.DateTime;
				colvarEndDate.MaxLength = 0;
				colvarEndDate.AutoIncrement = false;
				colvarEndDate.IsNullable = false;
				colvarEndDate.IsPrimaryKey = false;
				colvarEndDate.IsForeignKey = false;
				colvarEndDate.IsReadOnly = false;
				colvarEndDate.DefaultSetting = @"";
				colvarEndDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEndDate);

				TableSchema.TableColumn colvarOriginalTotal = new TableSchema.TableColumn(schema);
				colvarOriginalTotal.ColumnName = "OriginalTotal";
				colvarOriginalTotal.DataType = DbType.Decimal;
				colvarOriginalTotal.MaxLength = 0;
				colvarOriginalTotal.AutoIncrement = false;
				colvarOriginalTotal.IsNullable = false;
				colvarOriginalTotal.IsPrimaryKey = false;
				colvarOriginalTotal.IsForeignKey = false;
				colvarOriginalTotal.IsReadOnly = false;
				colvarOriginalTotal.DefaultSetting = @"";
				colvarOriginalTotal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginalTotal);

				TableSchema.TableColumn colvarTaxScheduleID = new TableSchema.TableColumn(schema);
				colvarTaxScheduleID.ColumnName = "TaxScheduleID";
				colvarTaxScheduleID.DataType = DbType.AnsiString;
				colvarTaxScheduleID.MaxLength = 15;
				colvarTaxScheduleID.AutoIncrement = false;
				colvarTaxScheduleID.IsNullable = true;
				colvarTaxScheduleID.IsPrimaryKey = false;
				colvarTaxScheduleID.IsForeignKey = false;
				colvarTaxScheduleID.IsReadOnly = false;
				colvarTaxScheduleID.DefaultSetting = @"";
				colvarTaxScheduleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxScheduleID);

				TableSchema.TableColumn colvarBillingCycle = new TableSchema.TableColumn(schema);
				colvarBillingCycle.ColumnName = "BillingCycle";
				colvarBillingCycle.DataType = DbType.Int16;
				colvarBillingCycle.MaxLength = 0;
				colvarBillingCycle.AutoIncrement = false;
				colvarBillingCycle.IsNullable = false;
				colvarBillingCycle.IsPrimaryKey = false;
				colvarBillingCycle.IsForeignKey = false;
				colvarBillingCycle.IsReadOnly = false;
				colvarBillingCycle.DefaultSetting = @"";
				colvarBillingCycle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingCycle);

				TableSchema.TableColumn colvarBillingDay = new TableSchema.TableColumn(schema);
				colvarBillingDay.ColumnName = "BillingDay";
				colvarBillingDay.DataType = DbType.Int16;
				colvarBillingDay.MaxLength = 0;
				colvarBillingDay.AutoIncrement = false;
				colvarBillingDay.IsNullable = false;
				colvarBillingDay.IsPrimaryKey = false;
				colvarBillingDay.IsForeignKey = false;
				colvarBillingDay.IsReadOnly = false;
				colvarBillingDay.DefaultSetting = @"";
				colvarBillingDay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingDay);

				TableSchema.TableColumn colvarIsOnHold = new TableSchema.TableColumn(schema);
				colvarIsOnHold.ColumnName = "IsOnHold";
				colvarIsOnHold.DataType = DbType.Boolean;
				colvarIsOnHold.MaxLength = 0;
				colvarIsOnHold.AutoIncrement = false;
				colvarIsOnHold.IsNullable = true;
				colvarIsOnHold.IsPrimaryKey = false;
				colvarIsOnHold.IsForeignKey = false;
				colvarIsOnHold.IsReadOnly = false;
				colvarIsOnHold.DefaultSetting = @"";
				colvarIsOnHold.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsOnHold);

				TableSchema.TableColumn colvarIsCanceled = new TableSchema.TableColumn(schema);
				colvarIsCanceled.ColumnName = "IsCanceled";
				colvarIsCanceled.DataType = DbType.Boolean;
				colvarIsCanceled.MaxLength = 0;
				colvarIsCanceled.AutoIncrement = false;
				colvarIsCanceled.IsNullable = true;
				colvarIsCanceled.IsPrimaryKey = false;
				colvarIsCanceled.IsForeignKey = false;
				colvarIsCanceled.IsReadOnly = false;
				colvarIsCanceled.DefaultSetting = @"";
				colvarIsCanceled.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsCanceled);

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
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwBillingContracts",schema);
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
		public BillingContractsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string ContractNumber { 
			get { return GetColumnValue<string>(Columns.ContractNumber); }
			set { SetColumnValue(Columns.ContractNumber, value); }
		}
		[DataMember]
		public string ContractType { 
			get { return GetColumnValue<string>(Columns.ContractType); }
			set { SetColumnValue(Columns.ContractType, value); }
		}
		[DataMember]
		public string Description { 
			get { return GetColumnValue<string>(Columns.Description); }
			set { SetColumnValue(Columns.Description, value); }
		}
		[DataMember]
		public short BillingPeriod { 
			get { return GetColumnValue<short>(Columns.BillingPeriod); }
			set { SetColumnValue(Columns.BillingPeriod, value); }
		}
		[DataMember]
		public decimal? PriceSchedule { 
			get { return GetColumnValue<decimal?>(Columns.PriceSchedule); }
			set { SetColumnValue(Columns.PriceSchedule, value); }
		}
		[DataMember]
		public short ContractLength { 
			get { return GetColumnValue<short>(Columns.ContractLength); }
			set { SetColumnValue(Columns.ContractLength, value); }
		}
		[DataMember]
		public string ServiceType { 
			get { return GetColumnValue<string>(Columns.ServiceType); }
			set { SetColumnValue(Columns.ServiceType, value); }
		}
		[DataMember]
		public DateTime StartDate { 
			get { return GetColumnValue<DateTime>(Columns.StartDate); }
			set { SetColumnValue(Columns.StartDate, value); }
		}
		[DataMember]
		public DateTime EndDate { 
			get { return GetColumnValue<DateTime>(Columns.EndDate); }
			set { SetColumnValue(Columns.EndDate, value); }
		}
		[DataMember]
		public decimal OriginalTotal { 
			get { return GetColumnValue<decimal>(Columns.OriginalTotal); }
			set { SetColumnValue(Columns.OriginalTotal, value); }
		}
		[DataMember]
		public string TaxScheduleID { 
			get { return GetColumnValue<string>(Columns.TaxScheduleID); }
			set { SetColumnValue(Columns.TaxScheduleID, value); }
		}
		[DataMember]
		public short BillingCycle { 
			get { return GetColumnValue<short>(Columns.BillingCycle); }
			set { SetColumnValue(Columns.BillingCycle, value); }
		}
		[DataMember]
		public short BillingDay { 
			get { return GetColumnValue<short>(Columns.BillingDay); }
			set { SetColumnValue(Columns.BillingDay, value); }
		}
		[DataMember]
		public bool? IsOnHold { 
			get { return GetColumnValue<bool?>(Columns.IsOnHold); }
			set { SetColumnValue(Columns.IsOnHold, value); }
		}
		[DataMember]
		public bool? IsCanceled { 
			get { return GetColumnValue<bool?>(Columns.IsCanceled); }
			set { SetColumnValue(Columns.IsCanceled, value); }
		}
		[DataMember]
		public DateTime CreatedOn { 
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return ContractNumber;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContractNumberColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ContractTypeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn BillingPeriodColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PriceScheduleColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ContractLengthColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ServiceTypeColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn StartDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn EndDateColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn OriginalTotalColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TaxScheduleIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn BillingCycleColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn BillingDayColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsOnHoldColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn IsCanceledColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[16]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string ContractNumber = @"ContractNumber";
			public const string ContractType = @"ContractType";
			public const string Description = @"Description";
			public const string BillingPeriod = @"BillingPeriod";
			public const string PriceSchedule = @"PriceSchedule";
			public const string ContractLength = @"ContractLength";
			public const string ServiceType = @"ServiceType";
			public const string StartDate = @"StartDate";
			public const string EndDate = @"EndDate";
			public const string OriginalTotal = @"OriginalTotal";
			public const string TaxScheduleID = @"TaxScheduleID";
			public const string BillingCycle = @"BillingCycle";
			public const string BillingDay = @"BillingDay";
			public const string IsOnHold = @"IsOnHold";
			public const string IsCanceled = @"IsCanceled";
			public const string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the CustomerInformationView class.
	/// </summary>
	[DataContract]
	public partial class CustomerInformationViewCollection : ReadOnlyList<CustomerInformationView, CustomerInformationViewCollection>
	{
		public static CustomerInformationViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CustomerInformationViewCollection result = new CustomerInformationViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwCustomerInformation view.
	/// </summary>
	[DataContract]
	public partial class CustomerInformationView : ReadOnlyRecord<CustomerInformationView>
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
				TableSchema.Table schema = new TableSchema.Table("vwCustomerInformation", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiString;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = true;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarCustomerName = new TableSchema.TableColumn(schema);
				colvarCustomerName.ColumnName = "CustomerName";
				colvarCustomerName.DataType = DbType.AnsiString;
				colvarCustomerName.MaxLength = 65;
				colvarCustomerName.AutoIncrement = false;
				colvarCustomerName.IsNullable = true;
				colvarCustomerName.IsPrimaryKey = false;
				colvarCustomerName.IsForeignKey = false;
				colvarCustomerName.IsReadOnly = false;
				colvarCustomerName.DefaultSetting = @"";
				colvarCustomerName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerName);

				TableSchema.TableColumn colvarCustomerClass = new TableSchema.TableColumn(schema);
				colvarCustomerClass.ColumnName = "CustomerClass";
				colvarCustomerClass.DataType = DbType.AnsiString;
				colvarCustomerClass.MaxLength = 15;
				colvarCustomerClass.AutoIncrement = false;
				colvarCustomerClass.IsNullable = true;
				colvarCustomerClass.IsPrimaryKey = false;
				colvarCustomerClass.IsForeignKey = false;
				colvarCustomerClass.IsReadOnly = false;
				colvarCustomerClass.DefaultSetting = @"";
				colvarCustomerClass.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerClass);

				TableSchema.TableColumn colvarAddressCode = new TableSchema.TableColumn(schema);
				colvarAddressCode.ColumnName = "AddressCode";
				colvarAddressCode.DataType = DbType.AnsiString;
				colvarAddressCode.MaxLength = 15;
				colvarAddressCode.AutoIncrement = false;
				colvarAddressCode.IsNullable = true;
				colvarAddressCode.IsPrimaryKey = false;
				colvarAddressCode.IsForeignKey = false;
				colvarAddressCode.IsReadOnly = false;
				colvarAddressCode.DefaultSetting = @"";
				colvarAddressCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddressCode);

				TableSchema.TableColumn colvarBillingAddressCode = new TableSchema.TableColumn(schema);
				colvarBillingAddressCode.ColumnName = "BillingAddressCode";
				colvarBillingAddressCode.DataType = DbType.AnsiString;
				colvarBillingAddressCode.MaxLength = 15;
				colvarBillingAddressCode.AutoIncrement = false;
				colvarBillingAddressCode.IsNullable = true;
				colvarBillingAddressCode.IsPrimaryKey = false;
				colvarBillingAddressCode.IsForeignKey = false;
				colvarBillingAddressCode.IsReadOnly = false;
				colvarBillingAddressCode.DefaultSetting = @"";
				colvarBillingAddressCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingAddressCode);

				TableSchema.TableColumn colvarShippingAddressCode = new TableSchema.TableColumn(schema);
				colvarShippingAddressCode.ColumnName = "ShippingAddressCode";
				colvarShippingAddressCode.DataType = DbType.AnsiString;
				colvarShippingAddressCode.MaxLength = 15;
				colvarShippingAddressCode.AutoIncrement = false;
				colvarShippingAddressCode.IsNullable = true;
				colvarShippingAddressCode.IsPrimaryKey = false;
				colvarShippingAddressCode.IsForeignKey = false;
				colvarShippingAddressCode.IsReadOnly = false;
				colvarShippingAddressCode.DefaultSetting = @"";
				colvarShippingAddressCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShippingAddressCode);

				TableSchema.TableColumn colvarTaxCodeID = new TableSchema.TableColumn(schema);
				colvarTaxCodeID.ColumnName = "TaxCodeID";
				colvarTaxCodeID.DataType = DbType.AnsiString;
				colvarTaxCodeID.MaxLength = 15;
				colvarTaxCodeID.AutoIncrement = false;
				colvarTaxCodeID.IsNullable = true;
				colvarTaxCodeID.IsPrimaryKey = false;
				colvarTaxCodeID.IsForeignKey = false;
				colvarTaxCodeID.IsReadOnly = false;
				colvarTaxCodeID.DefaultSetting = @"";
				colvarTaxCodeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxCodeID);

				TableSchema.TableColumn colvarAddress1 = new TableSchema.TableColumn(schema);
				colvarAddress1.ColumnName = "Address1";
				colvarAddress1.DataType = DbType.AnsiString;
				colvarAddress1.MaxLength = 61;
				colvarAddress1.AutoIncrement = false;
				colvarAddress1.IsNullable = true;
				colvarAddress1.IsPrimaryKey = false;
				colvarAddress1.IsForeignKey = false;
				colvarAddress1.IsReadOnly = false;
				colvarAddress1.DefaultSetting = @"";
				colvarAddress1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress1);

				TableSchema.TableColumn colvarAddress2 = new TableSchema.TableColumn(schema);
				colvarAddress2.ColumnName = "Address2";
				colvarAddress2.DataType = DbType.AnsiString;
				colvarAddress2.MaxLength = 61;
				colvarAddress2.AutoIncrement = false;
				colvarAddress2.IsNullable = true;
				colvarAddress2.IsPrimaryKey = false;
				colvarAddress2.IsForeignKey = false;
				colvarAddress2.IsReadOnly = false;
				colvarAddress2.DefaultSetting = @"";
				colvarAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress2);

				TableSchema.TableColumn colvarAddress3 = new TableSchema.TableColumn(schema);
				colvarAddress3.ColumnName = "Address3";
				colvarAddress3.DataType = DbType.AnsiString;
				colvarAddress3.MaxLength = 61;
				colvarAddress3.AutoIncrement = false;
				colvarAddress3.IsNullable = true;
				colvarAddress3.IsPrimaryKey = false;
				colvarAddress3.IsForeignKey = false;
				colvarAddress3.IsReadOnly = false;
				colvarAddress3.DefaultSetting = @"";
				colvarAddress3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress3);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.AnsiString;
				colvarCity.MaxLength = 35;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarState = new TableSchema.TableColumn(schema);
				colvarState.ColumnName = "State";
				colvarState.DataType = DbType.AnsiString;
				colvarState.MaxLength = 29;
				colvarState.AutoIncrement = false;
				colvarState.IsNullable = true;
				colvarState.IsPrimaryKey = false;
				colvarState.IsForeignKey = false;
				colvarState.IsReadOnly = false;
				colvarState.DefaultSetting = @"";
				colvarState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarState);

				TableSchema.TableColumn colvarZipCode = new TableSchema.TableColumn(schema);
				colvarZipCode.ColumnName = "ZipCode";
				colvarZipCode.DataType = DbType.AnsiString;
				colvarZipCode.MaxLength = 11;
				colvarZipCode.AutoIncrement = false;
				colvarZipCode.IsNullable = true;
				colvarZipCode.IsPrimaryKey = false;
				colvarZipCode.IsForeignKey = false;
				colvarZipCode.IsReadOnly = false;
				colvarZipCode.DefaultSetting = @"";
				colvarZipCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZipCode);

				TableSchema.TableColumn colvarCountry = new TableSchema.TableColumn(schema);
				colvarCountry.ColumnName = "Country";
				colvarCountry.DataType = DbType.AnsiString;
				colvarCountry.MaxLength = 61;
				colvarCountry.AutoIncrement = false;
				colvarCountry.IsNullable = true;
				colvarCountry.IsPrimaryKey = false;
				colvarCountry.IsForeignKey = false;
				colvarCountry.IsReadOnly = false;
				colvarCountry.DefaultSetting = @"";
				colvarCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountry);

				TableSchema.TableColumn colvarPhone1 = new TableSchema.TableColumn(schema);
				colvarPhone1.ColumnName = "Phone1";
				colvarPhone1.DataType = DbType.AnsiString;
				colvarPhone1.MaxLength = 21;
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
				colvarPhone2.DataType = DbType.AnsiString;
				colvarPhone2.MaxLength = 21;
				colvarPhone2.AutoIncrement = false;
				colvarPhone2.IsNullable = true;
				colvarPhone2.IsPrimaryKey = false;
				colvarPhone2.IsForeignKey = false;
				colvarPhone2.IsReadOnly = false;
				colvarPhone2.DefaultSetting = @"";
				colvarPhone2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone2);

				TableSchema.TableColumn colvarPhone3 = new TableSchema.TableColumn(schema);
				colvarPhone3.ColumnName = "Phone3";
				colvarPhone3.DataType = DbType.AnsiString;
				colvarPhone3.MaxLength = 21;
				colvarPhone3.AutoIncrement = false;
				colvarPhone3.IsNullable = true;
				colvarPhone3.IsPrimaryKey = false;
				colvarPhone3.IsForeignKey = false;
				colvarPhone3.IsReadOnly = false;
				colvarPhone3.DefaultSetting = @"";
				colvarPhone3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone3);

				TableSchema.TableColumn colvarFax = new TableSchema.TableColumn(schema);
				colvarFax.ColumnName = "Fax";
				colvarFax.DataType = DbType.AnsiString;
				colvarFax.MaxLength = 21;
				colvarFax.AutoIncrement = false;
				colvarFax.IsNullable = true;
				colvarFax.IsPrimaryKey = false;
				colvarFax.IsForeignKey = false;
				colvarFax.IsReadOnly = false;
				colvarFax.DefaultSetting = @"";
				colvarFax.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFax);

				BaseSchema = schema;
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwCustomerInformation",schema);
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
		public CustomerInformationView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string CustomerName { 
			get { return GetColumnValue<string>(Columns.CustomerName); }
			set { SetColumnValue(Columns.CustomerName, value); }
		}
		[DataMember]
		public string CustomerClass { 
			get { return GetColumnValue<string>(Columns.CustomerClass); }
			set { SetColumnValue(Columns.CustomerClass, value); }
		}
		[DataMember]
		public string AddressCode { 
			get { return GetColumnValue<string>(Columns.AddressCode); }
			set { SetColumnValue(Columns.AddressCode, value); }
		}
		[DataMember]
		public string BillingAddressCode { 
			get { return GetColumnValue<string>(Columns.BillingAddressCode); }
			set { SetColumnValue(Columns.BillingAddressCode, value); }
		}
		[DataMember]
		public string ShippingAddressCode { 
			get { return GetColumnValue<string>(Columns.ShippingAddressCode); }
			set { SetColumnValue(Columns.ShippingAddressCode, value); }
		}
		[DataMember]
		public string TaxCodeID { 
			get { return GetColumnValue<string>(Columns.TaxCodeID); }
			set { SetColumnValue(Columns.TaxCodeID, value); }
		}
		[DataMember]
		public string Address1 { 
			get { return GetColumnValue<string>(Columns.Address1); }
			set { SetColumnValue(Columns.Address1, value); }
		}
		[DataMember]
		public string Address2 { 
			get { return GetColumnValue<string>(Columns.Address2); }
			set { SetColumnValue(Columns.Address2, value); }
		}
		[DataMember]
		public string Address3 { 
			get { return GetColumnValue<string>(Columns.Address3); }
			set { SetColumnValue(Columns.Address3, value); }
		}
		[DataMember]
		public string City { 
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}
		[DataMember]
		public string State { 
			get { return GetColumnValue<string>(Columns.State); }
			set { SetColumnValue(Columns.State, value); }
		}
		[DataMember]
		public string ZipCode { 
			get { return GetColumnValue<string>(Columns.ZipCode); }
			set { SetColumnValue(Columns.ZipCode, value); }
		}
		[DataMember]
		public string Country { 
			get { return GetColumnValue<string>(Columns.Country); }
			set { SetColumnValue(Columns.Country, value); }
		}
		[DataMember]
		public string Phone1 { 
			get { return GetColumnValue<string>(Columns.Phone1); }
			set { SetColumnValue(Columns.Phone1, value); }
		}
		[DataMember]
		public string Phone2 { 
			get { return GetColumnValue<string>(Columns.Phone2); }
			set { SetColumnValue(Columns.Phone2, value); }
		}
		[DataMember]
		public string Phone3 { 
			get { return GetColumnValue<string>(Columns.Phone3); }
			set { SetColumnValue(Columns.Phone3, value); }
		}
		[DataMember]
		public string Fax { 
			get { return GetColumnValue<string>(Columns.Fax); }
			set { SetColumnValue(Columns.Fax, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return CustomerName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CustomerNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CustomerClassColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AddressCodeColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn BillingAddressCodeColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ShippingAddressCodeColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TaxCodeIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn Address1Column
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn Address2Column
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn Address3Column
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn StateColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ZipCodeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn CountryColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn Phone1Column
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn Phone2Column
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn Phone3Column
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn FaxColumn
		{
			get { return Schema.Columns[17]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string CustomerName = @"CustomerName";
			public const string CustomerClass = @"CustomerClass";
			public const string AddressCode = @"AddressCode";
			public const string BillingAddressCode = @"BillingAddressCode";
			public const string ShippingAddressCode = @"ShippingAddressCode";
			public const string TaxCodeID = @"TaxCodeID";
			public const string Address1 = @"Address1";
			public const string Address2 = @"Address2";
			public const string Address3 = @"Address3";
			public const string City = @"City";
			public const string State = @"State";
			public const string ZipCode = @"ZipCode";
			public const string Country = @"Country";
			public const string Phone1 = @"Phone1";
			public const string Phone2 = @"Phone2";
			public const string Phone3 = @"Phone3";
			public const string Fax = @"Fax";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the CustomerInvoicesView class.
	/// </summary>
	[DataContract]
	public partial class CustomerInvoicesViewCollection : ReadOnlyList<CustomerInvoicesView, CustomerInvoicesViewCollection>
	{
		public static CustomerInvoicesViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CustomerInvoicesViewCollection result = new CustomerInvoicesViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwCustomerInvoices view.
	/// </summary>
	[DataContract]
	public partial class CustomerInvoicesView : ReadOnlyRecord<CustomerInvoicesView>
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
				TableSchema.Table schema = new TableSchema.Table("vwCustomerInvoices", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiString;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = true;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarInvoiceNumber = new TableSchema.TableColumn(schema);
				colvarInvoiceNumber.ColumnName = "InvoiceNumber";
				colvarInvoiceNumber.DataType = DbType.AnsiString;
				colvarInvoiceNumber.MaxLength = 21;
				colvarInvoiceNumber.AutoIncrement = false;
				colvarInvoiceNumber.IsNullable = true;
				colvarInvoiceNumber.IsPrimaryKey = false;
				colvarInvoiceNumber.IsForeignKey = false;
				colvarInvoiceNumber.IsReadOnly = false;
				colvarInvoiceNumber.DefaultSetting = @"";
				colvarInvoiceNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceNumber);

				TableSchema.TableColumn colvarInvoiceDate = new TableSchema.TableColumn(schema);
				colvarInvoiceDate.ColumnName = "InvoiceDate";
				colvarInvoiceDate.DataType = DbType.DateTime;
				colvarInvoiceDate.MaxLength = 0;
				colvarInvoiceDate.AutoIncrement = false;
				colvarInvoiceDate.IsNullable = false;
				colvarInvoiceDate.IsPrimaryKey = false;
				colvarInvoiceDate.IsForeignKey = false;
				colvarInvoiceDate.IsReadOnly = false;
				colvarInvoiceDate.DefaultSetting = @"";
				colvarInvoiceDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceDate);

				TableSchema.TableColumn colvarInvoiceAmount = new TableSchema.TableColumn(schema);
				colvarInvoiceAmount.ColumnName = "InvoiceAmount";
				colvarInvoiceAmount.DataType = DbType.Decimal;
				colvarInvoiceAmount.MaxLength = 0;
				colvarInvoiceAmount.AutoIncrement = false;
				colvarInvoiceAmount.IsNullable = false;
				colvarInvoiceAmount.IsPrimaryKey = false;
				colvarInvoiceAmount.IsForeignKey = false;
				colvarInvoiceAmount.IsReadOnly = false;
				colvarInvoiceAmount.DefaultSetting = @"";
				colvarInvoiceAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceAmount);

				TableSchema.TableColumn colvarIsVoided = new TableSchema.TableColumn(schema);
				colvarIsVoided.ColumnName = "IsVoided";
				colvarIsVoided.DataType = DbType.Boolean;
				colvarIsVoided.MaxLength = 0;
				colvarIsVoided.AutoIncrement = false;
				colvarIsVoided.IsNullable = true;
				colvarIsVoided.IsPrimaryKey = false;
				colvarIsVoided.IsForeignKey = false;
				colvarIsVoided.IsReadOnly = false;
				colvarIsVoided.DefaultSetting = @"";
				colvarIsVoided.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsVoided);

				TableSchema.TableColumn colvarIsNSF = new TableSchema.TableColumn(schema);
				colvarIsNSF.ColumnName = "IsNSF";
				colvarIsNSF.DataType = DbType.Boolean;
				colvarIsNSF.MaxLength = 0;
				colvarIsNSF.AutoIncrement = false;
				colvarIsNSF.IsNullable = true;
				colvarIsNSF.IsPrimaryKey = false;
				colvarIsNSF.IsForeignKey = false;
				colvarIsNSF.IsReadOnly = false;
				colvarIsNSF.DefaultSetting = @"";
				colvarIsNSF.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsNSF);

				TableSchema.TableColumn colvarCheckNumber = new TableSchema.TableColumn(schema);
				colvarCheckNumber.ColumnName = "CheckNumber";
				colvarCheckNumber.DataType = DbType.AnsiString;
				colvarCheckNumber.MaxLength = 21;
				colvarCheckNumber.AutoIncrement = false;
				colvarCheckNumber.IsNullable = true;
				colvarCheckNumber.IsPrimaryKey = false;
				colvarCheckNumber.IsForeignKey = false;
				colvarCheckNumber.IsReadOnly = false;
				colvarCheckNumber.DefaultSetting = @"";
				colvarCheckNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCheckNumber);

				TableSchema.TableColumn colvarCardType = new TableSchema.TableColumn(schema);
				colvarCardType.ColumnName = "CardType";
				colvarCardType.DataType = DbType.AnsiString;
				colvarCardType.MaxLength = 31;
				colvarCardType.AutoIncrement = false;
				colvarCardType.IsNullable = true;
				colvarCardType.IsPrimaryKey = false;
				colvarCardType.IsForeignKey = false;
				colvarCardType.IsReadOnly = false;
				colvarCardType.DefaultSetting = @"";
				colvarCardType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCardType);

				TableSchema.TableColumn colvarCardTrxType = new TableSchema.TableColumn(schema);
				colvarCardTrxType.ColumnName = "CardTrxType";
				colvarCardTrxType.DataType = DbType.AnsiString;
				colvarCardTrxType.MaxLength = 4;
				colvarCardTrxType.AutoIncrement = false;
				colvarCardTrxType.IsNullable = true;
				colvarCardTrxType.IsPrimaryKey = false;
				colvarCardTrxType.IsForeignKey = false;
				colvarCardTrxType.IsReadOnly = false;
				colvarCardTrxType.DefaultSetting = @"";
				colvarCardTrxType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCardTrxType);

				TableSchema.TableColumn colvarLastCardStatus = new TableSchema.TableColumn(schema);
				colvarLastCardStatus.ColumnName = "LastCardStatus";
				colvarLastCardStatus.DataType = DbType.AnsiString;
				colvarLastCardStatus.MaxLength = 8;
				colvarLastCardStatus.AutoIncrement = false;
				colvarLastCardStatus.IsNullable = true;
				colvarLastCardStatus.IsPrimaryKey = false;
				colvarLastCardStatus.IsForeignKey = false;
				colvarLastCardStatus.IsReadOnly = false;
				colvarLastCardStatus.DefaultSetting = @"";
				colvarLastCardStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastCardStatus);

				TableSchema.TableColumn colvarCardResponseMessage = new TableSchema.TableColumn(schema);
				colvarCardResponseMessage.ColumnName = "CardResponseMessage";
				colvarCardResponseMessage.DataType = DbType.AnsiString;
				colvarCardResponseMessage.MaxLength = 101;
				colvarCardResponseMessage.AutoIncrement = false;
				colvarCardResponseMessage.IsNullable = true;
				colvarCardResponseMessage.IsPrimaryKey = false;
				colvarCardResponseMessage.IsForeignKey = false;
				colvarCardResponseMessage.IsReadOnly = false;
				colvarCardResponseMessage.DefaultSetting = @"";
				colvarCardResponseMessage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCardResponseMessage);

				TableSchema.TableColumn colvarTaxAmount = new TableSchema.TableColumn(schema);
				colvarTaxAmount.ColumnName = "TaxAmount";
				colvarTaxAmount.DataType = DbType.Decimal;
				colvarTaxAmount.MaxLength = 0;
				colvarTaxAmount.AutoIncrement = false;
				colvarTaxAmount.IsNullable = false;
				colvarTaxAmount.IsPrimaryKey = false;
				colvarTaxAmount.IsForeignKey = false;
				colvarTaxAmount.IsReadOnly = false;
				colvarTaxAmount.DefaultSetting = @"";
				colvarTaxAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxAmount);

				BaseSchema = schema;
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwCustomerInvoices",schema);
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
		public CustomerInvoicesView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string InvoiceNumber { 
			get { return GetColumnValue<string>(Columns.InvoiceNumber); }
			set { SetColumnValue(Columns.InvoiceNumber, value); }
		}
		[DataMember]
		public DateTime InvoiceDate { 
			get { return GetColumnValue<DateTime>(Columns.InvoiceDate); }
			set { SetColumnValue(Columns.InvoiceDate, value); }
		}
		[DataMember]
		public decimal InvoiceAmount { 
			get { return GetColumnValue<decimal>(Columns.InvoiceAmount); }
			set { SetColumnValue(Columns.InvoiceAmount, value); }
		}
		[DataMember]
		public bool? IsVoided { 
			get { return GetColumnValue<bool?>(Columns.IsVoided); }
			set { SetColumnValue(Columns.IsVoided, value); }
		}
		[DataMember]
		public bool? IsNSF { 
			get { return GetColumnValue<bool?>(Columns.IsNSF); }
			set { SetColumnValue(Columns.IsNSF, value); }
		}
		[DataMember]
		public string CheckNumber { 
			get { return GetColumnValue<string>(Columns.CheckNumber); }
			set { SetColumnValue(Columns.CheckNumber, value); }
		}
		[DataMember]
		public string CardType { 
			get { return GetColumnValue<string>(Columns.CardType); }
			set { SetColumnValue(Columns.CardType, value); }
		}
		[DataMember]
		public string CardTrxType { 
			get { return GetColumnValue<string>(Columns.CardTrxType); }
			set { SetColumnValue(Columns.CardTrxType, value); }
		}
		[DataMember]
		public string LastCardStatus { 
			get { return GetColumnValue<string>(Columns.LastCardStatus); }
			set { SetColumnValue(Columns.LastCardStatus, value); }
		}
		[DataMember]
		public string CardResponseMessage { 
			get { return GetColumnValue<string>(Columns.CardResponseMessage); }
			set { SetColumnValue(Columns.CardResponseMessage, value); }
		}
		[DataMember]
		public decimal TaxAmount { 
			get { return GetColumnValue<decimal>(Columns.TaxAmount); }
			set { SetColumnValue(Columns.TaxAmount, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return InvoiceNumber;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn InvoiceNumberColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn InvoiceDateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn InvoiceAmountColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsVoidedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn IsNSFColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CheckNumberColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CardTypeColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CardTrxTypeColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn LastCardStatusColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CardResponseMessageColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TaxAmountColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string InvoiceNumber = @"InvoiceNumber";
			public const string InvoiceDate = @"InvoiceDate";
			public const string InvoiceAmount = @"InvoiceAmount";
			public const string IsVoided = @"IsVoided";
			public const string IsNSF = @"IsNSF";
			public const string CheckNumber = @"CheckNumber";
			public const string CardType = @"CardType";
			public const string CardTrxType = @"CardTrxType";
			public const string LastCardStatus = @"LastCardStatus";
			public const string CardResponseMessage = @"CardResponseMessage";
			public const string TaxAmount = @"TaxAmount";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the CustomerOutstandingInvoicesView class.
	/// </summary>
	[DataContract]
	public partial class CustomerOutstandingInvoicesViewCollection : ReadOnlyList<CustomerOutstandingInvoicesView, CustomerOutstandingInvoicesViewCollection>
	{
		public static CustomerOutstandingInvoicesViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CustomerOutstandingInvoicesViewCollection result = new CustomerOutstandingInvoicesViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwCustomerOutstandingInvoices view.
	/// </summary>
	[DataContract]
	public partial class CustomerOutstandingInvoicesView : ReadOnlyRecord<CustomerOutstandingInvoicesView>
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
				TableSchema.Table schema = new TableSchema.Table("vwCustomerOutstandingInvoices", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiStringFixedLength;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = false;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarDocumentNumber = new TableSchema.TableColumn(schema);
				colvarDocumentNumber.ColumnName = "DocumentNumber";
				colvarDocumentNumber.DataType = DbType.AnsiStringFixedLength;
				colvarDocumentNumber.MaxLength = 21;
				colvarDocumentNumber.AutoIncrement = false;
				colvarDocumentNumber.IsNullable = false;
				colvarDocumentNumber.IsPrimaryKey = false;
				colvarDocumentNumber.IsForeignKey = false;
				colvarDocumentNumber.IsReadOnly = false;
				colvarDocumentNumber.DefaultSetting = @"";
				colvarDocumentNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocumentNumber);

				TableSchema.TableColumn colvarDocumentDate = new TableSchema.TableColumn(schema);
				colvarDocumentDate.ColumnName = "DocumentDate";
				colvarDocumentDate.DataType = DbType.DateTime;
				colvarDocumentDate.MaxLength = 0;
				colvarDocumentDate.AutoIncrement = false;
				colvarDocumentDate.IsNullable = false;
				colvarDocumentDate.IsPrimaryKey = false;
				colvarDocumentDate.IsForeignKey = false;
				colvarDocumentDate.IsReadOnly = false;
				colvarDocumentDate.DefaultSetting = @"";
				colvarDocumentDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocumentDate);

				TableSchema.TableColumn colvarInvoiceAmount = new TableSchema.TableColumn(schema);
				colvarInvoiceAmount.ColumnName = "InvoiceAmount";
				colvarInvoiceAmount.DataType = DbType.Decimal;
				colvarInvoiceAmount.MaxLength = 0;
				colvarInvoiceAmount.AutoIncrement = false;
				colvarInvoiceAmount.IsNullable = false;
				colvarInvoiceAmount.IsPrimaryKey = false;
				colvarInvoiceAmount.IsForeignKey = false;
				colvarInvoiceAmount.IsReadOnly = false;
				colvarInvoiceAmount.DefaultSetting = @"";
				colvarInvoiceAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceAmount);

				TableSchema.TableColumn colvarInvoiceBalance = new TableSchema.TableColumn(schema);
				colvarInvoiceBalance.ColumnName = "InvoiceBalance";
				colvarInvoiceBalance.DataType = DbType.Decimal;
				colvarInvoiceBalance.MaxLength = 0;
				colvarInvoiceBalance.AutoIncrement = false;
				colvarInvoiceBalance.IsNullable = true;
				colvarInvoiceBalance.IsPrimaryKey = false;
				colvarInvoiceBalance.IsForeignKey = false;
				colvarInvoiceBalance.IsReadOnly = false;
				colvarInvoiceBalance.DefaultSetting = @"";
				colvarInvoiceBalance.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceBalance);

				BaseSchema = schema;
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwCustomerOutstandingInvoices",schema);
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
		public CustomerOutstandingInvoicesView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string DocumentNumber { 
			get { return GetColumnValue<string>(Columns.DocumentNumber); }
			set { SetColumnValue(Columns.DocumentNumber, value); }
		}
		[DataMember]
		public DateTime DocumentDate { 
			get { return GetColumnValue<DateTime>(Columns.DocumentDate); }
			set { SetColumnValue(Columns.DocumentDate, value); }
		}
		[DataMember]
		public decimal InvoiceAmount { 
			get { return GetColumnValue<decimal>(Columns.InvoiceAmount); }
			set { SetColumnValue(Columns.InvoiceAmount, value); }
		}
		[DataMember]
		public decimal? InvoiceBalance { 
			get { return GetColumnValue<decimal?>(Columns.InvoiceBalance); }
			set { SetColumnValue(Columns.InvoiceBalance, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return DocumentNumber;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DocumentNumberColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DocumentDateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn InvoiceAmountColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn InvoiceBalanceColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string DocumentNumber = @"DocumentNumber";
			public const string DocumentDate = @"DocumentDate";
			public const string InvoiceAmount = @"InvoiceAmount";
			public const string InvoiceBalance = @"InvoiceBalance";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the MonitoringContractsView class.
	/// </summary>
	[DataContract]
	public partial class MonitoringContractsViewCollection : ReadOnlyList<MonitoringContractsView, MonitoringContractsViewCollection>
	{
		public static MonitoringContractsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			MonitoringContractsViewCollection result = new MonitoringContractsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwMonitoringContracts view.
	/// </summary>
	[DataContract]
	public partial class MonitoringContractsView : ReadOnlyRecord<MonitoringContractsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwMonitoringContracts", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiString;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = true;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarContractNumber = new TableSchema.TableColumn(schema);
				colvarContractNumber.ColumnName = "ContractNumber";
				colvarContractNumber.DataType = DbType.AnsiString;
				colvarContractNumber.MaxLength = 11;
				colvarContractNumber.AutoIncrement = false;
				colvarContractNumber.IsNullable = true;
				colvarContractNumber.IsPrimaryKey = false;
				colvarContractNumber.IsForeignKey = false;
				colvarContractNumber.IsReadOnly = false;
				colvarContractNumber.DefaultSetting = @"";
				colvarContractNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractNumber);

				TableSchema.TableColumn colvarContractType = new TableSchema.TableColumn(schema);
				colvarContractType.ColumnName = "ContractType";
				colvarContractType.DataType = DbType.AnsiString;
				colvarContractType.MaxLength = 11;
				colvarContractType.AutoIncrement = false;
				colvarContractType.IsNullable = true;
				colvarContractType.IsPrimaryKey = false;
				colvarContractType.IsForeignKey = false;
				colvarContractType.IsReadOnly = false;
				colvarContractType.DefaultSetting = @"";
				colvarContractType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractType);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.AnsiString;
				colvarDescription.MaxLength = 31;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = true;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarBillingPeriod = new TableSchema.TableColumn(schema);
				colvarBillingPeriod.ColumnName = "BillingPeriod";
				colvarBillingPeriod.DataType = DbType.Int16;
				colvarBillingPeriod.MaxLength = 0;
				colvarBillingPeriod.AutoIncrement = false;
				colvarBillingPeriod.IsNullable = false;
				colvarBillingPeriod.IsPrimaryKey = false;
				colvarBillingPeriod.IsForeignKey = false;
				colvarBillingPeriod.IsReadOnly = false;
				colvarBillingPeriod.DefaultSetting = @"";
				colvarBillingPeriod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingPeriod);

				TableSchema.TableColumn colvarPriceSchedule = new TableSchema.TableColumn(schema);
				colvarPriceSchedule.ColumnName = "PriceSchedule";
				colvarPriceSchedule.DataType = DbType.Currency;
				colvarPriceSchedule.MaxLength = 0;
				colvarPriceSchedule.AutoIncrement = false;
				colvarPriceSchedule.IsNullable = true;
				colvarPriceSchedule.IsPrimaryKey = false;
				colvarPriceSchedule.IsForeignKey = false;
				colvarPriceSchedule.IsReadOnly = false;
				colvarPriceSchedule.DefaultSetting = @"";
				colvarPriceSchedule.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPriceSchedule);

				TableSchema.TableColumn colvarContractLength = new TableSchema.TableColumn(schema);
				colvarContractLength.ColumnName = "ContractLength";
				colvarContractLength.DataType = DbType.Int16;
				colvarContractLength.MaxLength = 0;
				colvarContractLength.AutoIncrement = false;
				colvarContractLength.IsNullable = false;
				colvarContractLength.IsPrimaryKey = false;
				colvarContractLength.IsForeignKey = false;
				colvarContractLength.IsReadOnly = false;
				colvarContractLength.DefaultSetting = @"";
				colvarContractLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractLength);

				TableSchema.TableColumn colvarServiceType = new TableSchema.TableColumn(schema);
				colvarServiceType.ColumnName = "ServiceType";
				colvarServiceType.DataType = DbType.AnsiString;
				colvarServiceType.MaxLength = 11;
				colvarServiceType.AutoIncrement = false;
				colvarServiceType.IsNullable = true;
				colvarServiceType.IsPrimaryKey = false;
				colvarServiceType.IsForeignKey = false;
				colvarServiceType.IsReadOnly = false;
				colvarServiceType.DefaultSetting = @"";
				colvarServiceType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceType);

				TableSchema.TableColumn colvarStartDate = new TableSchema.TableColumn(schema);
				colvarStartDate.ColumnName = "StartDate";
				colvarStartDate.DataType = DbType.DateTime;
				colvarStartDate.MaxLength = 0;
				colvarStartDate.AutoIncrement = false;
				colvarStartDate.IsNullable = false;
				colvarStartDate.IsPrimaryKey = false;
				colvarStartDate.IsForeignKey = false;
				colvarStartDate.IsReadOnly = false;
				colvarStartDate.DefaultSetting = @"";
				colvarStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartDate);

				TableSchema.TableColumn colvarEndDate = new TableSchema.TableColumn(schema);
				colvarEndDate.ColumnName = "EndDate";
				colvarEndDate.DataType = DbType.DateTime;
				colvarEndDate.MaxLength = 0;
				colvarEndDate.AutoIncrement = false;
				colvarEndDate.IsNullable = false;
				colvarEndDate.IsPrimaryKey = false;
				colvarEndDate.IsForeignKey = false;
				colvarEndDate.IsReadOnly = false;
				colvarEndDate.DefaultSetting = @"";
				colvarEndDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEndDate);

				TableSchema.TableColumn colvarOriginalTotal = new TableSchema.TableColumn(schema);
				colvarOriginalTotal.ColumnName = "OriginalTotal";
				colvarOriginalTotal.DataType = DbType.Decimal;
				colvarOriginalTotal.MaxLength = 0;
				colvarOriginalTotal.AutoIncrement = false;
				colvarOriginalTotal.IsNullable = false;
				colvarOriginalTotal.IsPrimaryKey = false;
				colvarOriginalTotal.IsForeignKey = false;
				colvarOriginalTotal.IsReadOnly = false;
				colvarOriginalTotal.DefaultSetting = @"";
				colvarOriginalTotal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginalTotal);

				TableSchema.TableColumn colvarTaxScheduleID = new TableSchema.TableColumn(schema);
				colvarTaxScheduleID.ColumnName = "TaxScheduleID";
				colvarTaxScheduleID.DataType = DbType.AnsiString;
				colvarTaxScheduleID.MaxLength = 15;
				colvarTaxScheduleID.AutoIncrement = false;
				colvarTaxScheduleID.IsNullable = true;
				colvarTaxScheduleID.IsPrimaryKey = false;
				colvarTaxScheduleID.IsForeignKey = false;
				colvarTaxScheduleID.IsReadOnly = false;
				colvarTaxScheduleID.DefaultSetting = @"";
				colvarTaxScheduleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTaxScheduleID);

				TableSchema.TableColumn colvarBillingCycle = new TableSchema.TableColumn(schema);
				colvarBillingCycle.ColumnName = "BillingCycle";
				colvarBillingCycle.DataType = DbType.Int16;
				colvarBillingCycle.MaxLength = 0;
				colvarBillingCycle.AutoIncrement = false;
				colvarBillingCycle.IsNullable = false;
				colvarBillingCycle.IsPrimaryKey = false;
				colvarBillingCycle.IsForeignKey = false;
				colvarBillingCycle.IsReadOnly = false;
				colvarBillingCycle.DefaultSetting = @"";
				colvarBillingCycle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingCycle);

				TableSchema.TableColumn colvarBillingDay = new TableSchema.TableColumn(schema);
				colvarBillingDay.ColumnName = "BillingDay";
				colvarBillingDay.DataType = DbType.Int16;
				colvarBillingDay.MaxLength = 0;
				colvarBillingDay.AutoIncrement = false;
				colvarBillingDay.IsNullable = false;
				colvarBillingDay.IsPrimaryKey = false;
				colvarBillingDay.IsForeignKey = false;
				colvarBillingDay.IsReadOnly = false;
				colvarBillingDay.DefaultSetting = @"";
				colvarBillingDay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBillingDay);

				TableSchema.TableColumn colvarIsOnHold = new TableSchema.TableColumn(schema);
				colvarIsOnHold.ColumnName = "IsOnHold";
				colvarIsOnHold.DataType = DbType.Boolean;
				colvarIsOnHold.MaxLength = 0;
				colvarIsOnHold.AutoIncrement = false;
				colvarIsOnHold.IsNullable = true;
				colvarIsOnHold.IsPrimaryKey = false;
				colvarIsOnHold.IsForeignKey = false;
				colvarIsOnHold.IsReadOnly = false;
				colvarIsOnHold.DefaultSetting = @"";
				colvarIsOnHold.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsOnHold);

				TableSchema.TableColumn colvarIsCanceled = new TableSchema.TableColumn(schema);
				colvarIsCanceled.ColumnName = "IsCanceled";
				colvarIsCanceled.DataType = DbType.Boolean;
				colvarIsCanceled.MaxLength = 0;
				colvarIsCanceled.AutoIncrement = false;
				colvarIsCanceled.IsNullable = true;
				colvarIsCanceled.IsPrimaryKey = false;
				colvarIsCanceled.IsForeignKey = false;
				colvarIsCanceled.IsReadOnly = false;
				colvarIsCanceled.DefaultSetting = @"";
				colvarIsCanceled.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsCanceled);

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
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwMonitoringContracts",schema);
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
		public MonitoringContractsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string ContractNumber { 
			get { return GetColumnValue<string>(Columns.ContractNumber); }
			set { SetColumnValue(Columns.ContractNumber, value); }
		}
		[DataMember]
		public string ContractType { 
			get { return GetColumnValue<string>(Columns.ContractType); }
			set { SetColumnValue(Columns.ContractType, value); }
		}
		[DataMember]
		public string Description { 
			get { return GetColumnValue<string>(Columns.Description); }
			set { SetColumnValue(Columns.Description, value); }
		}
		[DataMember]
		public short BillingPeriod { 
			get { return GetColumnValue<short>(Columns.BillingPeriod); }
			set { SetColumnValue(Columns.BillingPeriod, value); }
		}
		[DataMember]
		public decimal? PriceSchedule { 
			get { return GetColumnValue<decimal?>(Columns.PriceSchedule); }
			set { SetColumnValue(Columns.PriceSchedule, value); }
		}
		[DataMember]
		public short ContractLength { 
			get { return GetColumnValue<short>(Columns.ContractLength); }
			set { SetColumnValue(Columns.ContractLength, value); }
		}
		[DataMember]
		public string ServiceType { 
			get { return GetColumnValue<string>(Columns.ServiceType); }
			set { SetColumnValue(Columns.ServiceType, value); }
		}
		[DataMember]
		public DateTime StartDate { 
			get { return GetColumnValue<DateTime>(Columns.StartDate); }
			set { SetColumnValue(Columns.StartDate, value); }
		}
		[DataMember]
		public DateTime EndDate { 
			get { return GetColumnValue<DateTime>(Columns.EndDate); }
			set { SetColumnValue(Columns.EndDate, value); }
		}
		[DataMember]
		public decimal OriginalTotal { 
			get { return GetColumnValue<decimal>(Columns.OriginalTotal); }
			set { SetColumnValue(Columns.OriginalTotal, value); }
		}
		[DataMember]
		public string TaxScheduleID { 
			get { return GetColumnValue<string>(Columns.TaxScheduleID); }
			set { SetColumnValue(Columns.TaxScheduleID, value); }
		}
		[DataMember]
		public short BillingCycle { 
			get { return GetColumnValue<short>(Columns.BillingCycle); }
			set { SetColumnValue(Columns.BillingCycle, value); }
		}
		[DataMember]
		public short BillingDay { 
			get { return GetColumnValue<short>(Columns.BillingDay); }
			set { SetColumnValue(Columns.BillingDay, value); }
		}
		[DataMember]
		public bool? IsOnHold { 
			get { return GetColumnValue<bool?>(Columns.IsOnHold); }
			set { SetColumnValue(Columns.IsOnHold, value); }
		}
		[DataMember]
		public bool? IsCanceled { 
			get { return GetColumnValue<bool?>(Columns.IsCanceled); }
			set { SetColumnValue(Columns.IsCanceled, value); }
		}
		[DataMember]
		public DateTime CreatedOn { 
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return ContractNumber;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContractNumberColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ContractTypeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn BillingPeriodColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PriceScheduleColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ContractLengthColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ServiceTypeColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn StartDateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn EndDateColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn OriginalTotalColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TaxScheduleIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn BillingCycleColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn BillingDayColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsOnHoldColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn IsCanceledColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[16]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string ContractNumber = @"ContractNumber";
			public const string ContractType = @"ContractType";
			public const string Description = @"Description";
			public const string BillingPeriod = @"BillingPeriod";
			public const string PriceSchedule = @"PriceSchedule";
			public const string ContractLength = @"ContractLength";
			public const string ServiceType = @"ServiceType";
			public const string StartDate = @"StartDate";
			public const string EndDate = @"EndDate";
			public const string OriginalTotal = @"OriginalTotal";
			public const string TaxScheduleID = @"TaxScheduleID";
			public const string BillingCycle = @"BillingCycle";
			public const string BillingDay = @"BillingDay";
			public const string IsOnHold = @"IsOnHold";
			public const string IsCanceled = @"IsCanceled";
			public const string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the OpenPaymentsAndCreditsView class.
	/// </summary>
	[DataContract]
	public partial class OpenPaymentsAndCreditsViewCollection : ReadOnlyList<OpenPaymentsAndCreditsView, OpenPaymentsAndCreditsViewCollection>
	{
		public static OpenPaymentsAndCreditsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			OpenPaymentsAndCreditsViewCollection result = new OpenPaymentsAndCreditsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwOpenPaymentsAndCredits view.
	/// </summary>
	[DataContract]
	public partial class OpenPaymentsAndCreditsView : ReadOnlyRecord<OpenPaymentsAndCreditsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwOpenPaymentsAndCredits", TableType.Table, DataService.GetInstance("GreatPlainsProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerNumber = new TableSchema.TableColumn(schema);
				colvarCustomerNumber.ColumnName = "CustomerNumber";
				colvarCustomerNumber.DataType = DbType.AnsiString;
				colvarCustomerNumber.MaxLength = 15;
				colvarCustomerNumber.AutoIncrement = false;
				colvarCustomerNumber.IsNullable = true;
				colvarCustomerNumber.IsPrimaryKey = false;
				colvarCustomerNumber.IsForeignKey = false;
				colvarCustomerNumber.IsReadOnly = false;
				colvarCustomerNumber.DefaultSetting = @"";
				colvarCustomerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerNumber);

				TableSchema.TableColumn colvarDocumentNumber = new TableSchema.TableColumn(schema);
				colvarDocumentNumber.ColumnName = "DocumentNumber";
				colvarDocumentNumber.DataType = DbType.AnsiString;
				colvarDocumentNumber.MaxLength = 21;
				colvarDocumentNumber.AutoIncrement = false;
				colvarDocumentNumber.IsNullable = true;
				colvarDocumentNumber.IsPrimaryKey = false;
				colvarDocumentNumber.IsForeignKey = false;
				colvarDocumentNumber.IsReadOnly = false;
				colvarDocumentNumber.DefaultSetting = @"";
				colvarDocumentNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocumentNumber);

				TableSchema.TableColumn colvarDocumentDate = new TableSchema.TableColumn(schema);
				colvarDocumentDate.ColumnName = "DocumentDate";
				colvarDocumentDate.DataType = DbType.DateTime;
				colvarDocumentDate.MaxLength = 0;
				colvarDocumentDate.AutoIncrement = false;
				colvarDocumentDate.IsNullable = false;
				colvarDocumentDate.IsPrimaryKey = false;
				colvarDocumentDate.IsForeignKey = false;
				colvarDocumentDate.IsReadOnly = false;
				colvarDocumentDate.DefaultSetting = @"";
				colvarDocumentDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocumentDate);

				TableSchema.TableColumn colvarPaymentAmount = new TableSchema.TableColumn(schema);
				colvarPaymentAmount.ColumnName = "PaymentAmount";
				colvarPaymentAmount.DataType = DbType.Decimal;
				colvarPaymentAmount.MaxLength = 0;
				colvarPaymentAmount.AutoIncrement = false;
				colvarPaymentAmount.IsNullable = false;
				colvarPaymentAmount.IsPrimaryKey = false;
				colvarPaymentAmount.IsForeignKey = false;
				colvarPaymentAmount.IsReadOnly = false;
				colvarPaymentAmount.DefaultSetting = @"";
				colvarPaymentAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPaymentAmount);

				TableSchema.TableColumn colvarTotalApplied = new TableSchema.TableColumn(schema);
				colvarTotalApplied.ColumnName = "TotalApplied";
				colvarTotalApplied.DataType = DbType.Decimal;
				colvarTotalApplied.MaxLength = 0;
				colvarTotalApplied.AutoIncrement = false;
				colvarTotalApplied.IsNullable = true;
				colvarTotalApplied.IsPrimaryKey = false;
				colvarTotalApplied.IsForeignKey = false;
				colvarTotalApplied.IsReadOnly = false;
				colvarTotalApplied.DefaultSetting = @"";
				colvarTotalApplied.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalApplied);

				TableSchema.TableColumn colvarRemainingBalance = new TableSchema.TableColumn(schema);
				colvarRemainingBalance.ColumnName = "RemainingBalance";
				colvarRemainingBalance.DataType = DbType.Decimal;
				colvarRemainingBalance.MaxLength = 0;
				colvarRemainingBalance.AutoIncrement = false;
				colvarRemainingBalance.IsNullable = false;
				colvarRemainingBalance.IsPrimaryKey = false;
				colvarRemainingBalance.IsForeignKey = false;
				colvarRemainingBalance.IsReadOnly = false;
				colvarRemainingBalance.DefaultSetting = @"";
				colvarRemainingBalance.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRemainingBalance);

				BaseSchema = schema;
				DataService.Providers["GreatPlainsProvider"].AddSchema("vwOpenPaymentsAndCredits",schema);
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
		public OpenPaymentsAndCreditsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string CustomerNumber { 
			get { return GetColumnValue<string>(Columns.CustomerNumber); }
			set { SetColumnValue(Columns.CustomerNumber, value); }
		}
		[DataMember]
		public string DocumentNumber { 
			get { return GetColumnValue<string>(Columns.DocumentNumber); }
			set { SetColumnValue(Columns.DocumentNumber, value); }
		}
		[DataMember]
		public DateTime DocumentDate { 
			get { return GetColumnValue<DateTime>(Columns.DocumentDate); }
			set { SetColumnValue(Columns.DocumentDate, value); }
		}
		[DataMember]
		public decimal PaymentAmount { 
			get { return GetColumnValue<decimal>(Columns.PaymentAmount); }
			set { SetColumnValue(Columns.PaymentAmount, value); }
		}
		[DataMember]
		public decimal? TotalApplied { 
			get { return GetColumnValue<decimal?>(Columns.TotalApplied); }
			set { SetColumnValue(Columns.TotalApplied, value); }
		}
		[DataMember]
		public decimal RemainingBalance { 
			get { return GetColumnValue<decimal>(Columns.RemainingBalance); }
			set { SetColumnValue(Columns.RemainingBalance, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return DocumentNumber;
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerNumberColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DocumentNumberColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn DocumentDateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PaymentAmountColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TotalAppliedColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn RemainingBalanceColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerNumber = @"CustomerNumber";
			public const string DocumentNumber = @"DocumentNumber";
			public const string DocumentDate = @"DocumentDate";
			public const string PaymentAmount = @"PaymentAmount";
			public const string TotalApplied = @"TotalApplied";
			public const string RemainingBalance = @"RemainingBalance";
		}
		#endregion Columns Struct
	}
}
