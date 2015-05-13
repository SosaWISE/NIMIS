


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

namespace SOS.Data.HumanResource
{
	/// <summary>
	/// Strongly-typed collection for the AllRegionalsView class.
	/// </summary>
	[DataContract]
	public partial class AllRegionalsViewCollection : ReadOnlyList<AllRegionalsView, AllRegionalsViewCollection>
	{
		public static AllRegionalsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AllRegionalsViewCollection result = new AllRegionalsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_AllRegionals view.
	/// </summary>
	[DataContract]
	public partial class AllRegionalsView : ReadOnlyRecord<AllRegionalsView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_AllRegionals", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = true;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarUserTypeId = new TableSchema.TableColumn(schema);
				colvarUserTypeId.ColumnName = "UserTypeId";
				colvarUserTypeId.DataType = DbType.Int16;
				colvarUserTypeId.MaxLength = 0;
				colvarUserTypeId.AutoIncrement = false;
				colvarUserTypeId.IsNullable = false;
				colvarUserTypeId.IsPrimaryKey = false;
				colvarUserTypeId.IsForeignKey = false;
				colvarUserTypeId.IsReadOnly = false;
				colvarUserTypeId.DefaultSetting = @"";
				colvarUserTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeId);

				TableSchema.TableColumn colvarReportsToID = new TableSchema.TableColumn(schema);
				colvarReportsToID.ColumnName = "ReportsToID";
				colvarReportsToID.DataType = DbType.Int32;
				colvarReportsToID.MaxLength = 0;
				colvarReportsToID.AutoIncrement = false;
				colvarReportsToID.IsNullable = true;
				colvarReportsToID.IsPrimaryKey = false;
				colvarReportsToID.IsForeignKey = false;
				colvarReportsToID.IsReadOnly = false;
				colvarReportsToID.DefaultSetting = @"";
				colvarReportsToID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportsToID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarPayScaleID = new TableSchema.TableColumn(schema);
				colvarPayScaleID.ColumnName = "PayScaleID";
				colvarPayScaleID.DataType = DbType.Int32;
				colvarPayScaleID.MaxLength = 0;
				colvarPayScaleID.AutoIncrement = false;
				colvarPayScaleID.IsNullable = true;
				colvarPayScaleID.IsPrimaryKey = false;
				colvarPayScaleID.IsForeignKey = false;
				colvarPayScaleID.IsReadOnly = false;
				colvarPayScaleID.DefaultSetting = @"";
				colvarPayScaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScaleID);

				TableSchema.TableColumn colvarIsRecruiter = new TableSchema.TableColumn(schema);
				colvarIsRecruiter.ColumnName = "IsRecruiter";
				colvarIsRecruiter.DataType = DbType.Boolean;
				colvarIsRecruiter.MaxLength = 0;
				colvarIsRecruiter.AutoIncrement = false;
				colvarIsRecruiter.IsNullable = false;
				colvarIsRecruiter.IsPrimaryKey = false;
				colvarIsRecruiter.IsForeignKey = false;
				colvarIsRecruiter.IsReadOnly = false;
				colvarIsRecruiter.DefaultSetting = @"";
				colvarIsRecruiter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsRecruiter);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_AllRegionals",schema);
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
		public AllRegionalsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public short UserTypeId {
			get { return GetColumnValue<short>(Columns.UserTypeId); }
			set { SetColumnValue(Columns.UserTypeId, value); }
		}
		[DataMember]
		public int? ReportsToID {
			get { return GetColumnValue<int?>(Columns.ReportsToID); }
			set { SetColumnValue(Columns.ReportsToID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? PayScaleID {
			get { return GetColumnValue<int?>(Columns.PayScaleID); }
			set { SetColumnValue(Columns.PayScaleID, value); }
		}
		[DataMember]
		public bool IsRecruiter {
			get { return GetColumnValue<bool>(Columns.IsRecruiter); }
			set { SetColumnValue(Columns.IsRecruiter, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TeamID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UserTypeIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ReportsToIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PayScaleIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsRecruiterColumn
		{
			get { return Schema.Columns[8]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TeamID = @"TeamID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string RecruitID = @"RecruitID";
			public const string UserID = @"UserID";
			public const string UserTypeId = @"UserTypeId";
			public const string ReportsToID = @"ReportsToID";
			public const string SeasonID = @"SeasonID";
			public const string PayScaleID = @"PayScaleID";
			public const string IsRecruiter = @"IsRecruiter";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the AllSalesManagersView class.
	/// </summary>
	[DataContract]
	public partial class AllSalesManagersViewCollection : ReadOnlyList<AllSalesManagersView, AllSalesManagersViewCollection>
	{
		public static AllSalesManagersViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AllSalesManagersViewCollection result = new AllSalesManagersViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_AllSalesManagers view.
	/// </summary>
	[DataContract]
	public partial class AllSalesManagersView : ReadOnlyRecord<AllSalesManagersView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_AllSalesManagers", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = false;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarUserTypeId = new TableSchema.TableColumn(schema);
				colvarUserTypeId.ColumnName = "UserTypeId";
				colvarUserTypeId.DataType = DbType.Int16;
				colvarUserTypeId.MaxLength = 0;
				colvarUserTypeId.AutoIncrement = false;
				colvarUserTypeId.IsNullable = false;
				colvarUserTypeId.IsPrimaryKey = false;
				colvarUserTypeId.IsForeignKey = false;
				colvarUserTypeId.IsReadOnly = false;
				colvarUserTypeId.DefaultSetting = @"";
				colvarUserTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeId);

				TableSchema.TableColumn colvarReportsToID = new TableSchema.TableColumn(schema);
				colvarReportsToID.ColumnName = "ReportsToID";
				colvarReportsToID.DataType = DbType.Int32;
				colvarReportsToID.MaxLength = 0;
				colvarReportsToID.AutoIncrement = false;
				colvarReportsToID.IsNullable = true;
				colvarReportsToID.IsPrimaryKey = false;
				colvarReportsToID.IsForeignKey = false;
				colvarReportsToID.IsReadOnly = false;
				colvarReportsToID.DefaultSetting = @"";
				colvarReportsToID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportsToID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarPayScaleID = new TableSchema.TableColumn(schema);
				colvarPayScaleID.ColumnName = "PayScaleID";
				colvarPayScaleID.DataType = DbType.Int32;
				colvarPayScaleID.MaxLength = 0;
				colvarPayScaleID.AutoIncrement = false;
				colvarPayScaleID.IsNullable = true;
				colvarPayScaleID.IsPrimaryKey = false;
				colvarPayScaleID.IsForeignKey = false;
				colvarPayScaleID.IsReadOnly = false;
				colvarPayScaleID.DefaultSetting = @"";
				colvarPayScaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScaleID);

				TableSchema.TableColumn colvarIsRecruiter = new TableSchema.TableColumn(schema);
				colvarIsRecruiter.ColumnName = "IsRecruiter";
				colvarIsRecruiter.DataType = DbType.Boolean;
				colvarIsRecruiter.MaxLength = 0;
				colvarIsRecruiter.AutoIncrement = false;
				colvarIsRecruiter.IsNullable = false;
				colvarIsRecruiter.IsPrimaryKey = false;
				colvarIsRecruiter.IsForeignKey = false;
				colvarIsRecruiter.IsReadOnly = false;
				colvarIsRecruiter.DefaultSetting = @"";
				colvarIsRecruiter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsRecruiter);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_AllSalesManagers",schema);
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
		public AllSalesManagersView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int TeamLocationID {
			get { return GetColumnValue<int>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public short UserTypeId {
			get { return GetColumnValue<short>(Columns.UserTypeId); }
			set { SetColumnValue(Columns.UserTypeId, value); }
		}
		[DataMember]
		public int? ReportsToID {
			get { return GetColumnValue<int?>(Columns.ReportsToID); }
			set { SetColumnValue(Columns.ReportsToID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? PayScaleID {
			get { return GetColumnValue<int?>(Columns.PayScaleID); }
			set { SetColumnValue(Columns.PayScaleID, value); }
		}
		[DataMember]
		public bool IsRecruiter {
			get { return GetColumnValue<bool>(Columns.IsRecruiter); }
			set { SetColumnValue(Columns.IsRecruiter, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TeamID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UserTypeIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ReportsToIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PayScaleIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsRecruiterColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TeamID = @"TeamID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string RecruitID = @"RecruitID";
			public const string UserID = @"UserID";
			public const string UserTypeId = @"UserTypeId";
			public const string ReportsToID = @"ReportsToID";
			public const string SeasonID = @"SeasonID";
			public const string PayScaleID = @"PayScaleID";
			public const string IsRecruiter = @"IsRecruiter";
			public const string GPEmployeeID = @"GPEmployeeID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RecruitingLineView class.
	/// </summary>
	[DataContract]
	public partial class RecruitingLineViewCollection : ReadOnlyList<RecruitingLineView, RecruitingLineViewCollection>
	{
		public static RecruitingLineViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RecruitingLineViewCollection result = new RecruitingLineViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_RecruitingLine view.
	/// </summary>
	[DataContract]
	public partial class RecruitingLineView : ReadOnlyRecord<RecruitingLineView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_RecruitingLine", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarReportingLevel = new TableSchema.TableColumn(schema);
				colvarReportingLevel.ColumnName = "ReportingLevel";
				colvarReportingLevel.DataType = DbType.Int32;
				colvarReportingLevel.MaxLength = 0;
				colvarReportingLevel.AutoIncrement = false;
				colvarReportingLevel.IsNullable = false;
				colvarReportingLevel.IsPrimaryKey = false;
				colvarReportingLevel.IsForeignKey = false;
				colvarReportingLevel.IsReadOnly = false;
				colvarReportingLevel.DefaultSetting = @"";
				colvarReportingLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportingLevel);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = false;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarRepID = new TableSchema.TableColumn(schema);
				colvarRepID.ColumnName = "RepID";
				colvarRepID.DataType = DbType.Int32;
				colvarRepID.MaxLength = 0;
				colvarRepID.AutoIncrement = false;
				colvarRepID.IsNullable = true;
				colvarRepID.IsPrimaryKey = false;
				colvarRepID.IsForeignKey = false;
				colvarRepID.IsReadOnly = false;
				colvarRepID.DefaultSetting = @"";
				colvarRepID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRepID);

				TableSchema.TableColumn colvarManagerID = new TableSchema.TableColumn(schema);
				colvarManagerID.ColumnName = "ManagerID";
				colvarManagerID.DataType = DbType.Int32;
				colvarManagerID.MaxLength = 0;
				colvarManagerID.AutoIncrement = false;
				colvarManagerID.IsNullable = true;
				colvarManagerID.IsPrimaryKey = false;
				colvarManagerID.IsForeignKey = false;
				colvarManagerID.IsReadOnly = false;
				colvarManagerID.DefaultSetting = @"";
				colvarManagerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerID);

				TableSchema.TableColumn colvarRegionID = new TableSchema.TableColumn(schema);
				colvarRegionID.ColumnName = "RegionID";
				colvarRegionID.DataType = DbType.Int32;
				colvarRegionID.MaxLength = 0;
				colvarRegionID.AutoIncrement = false;
				colvarRegionID.IsNullable = true;
				colvarRegionID.IsPrimaryKey = false;
				colvarRegionID.IsForeignKey = false;
				colvarRegionID.IsReadOnly = false;
				colvarRegionID.DefaultSetting = @"";
				colvarRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionID);

				TableSchema.TableColumn colvarNationalRegionID = new TableSchema.TableColumn(schema);
				colvarNationalRegionID.ColumnName = "NationalRegionID";
				colvarNationalRegionID.DataType = DbType.Int32;
				colvarNationalRegionID.MaxLength = 0;
				colvarNationalRegionID.AutoIncrement = false;
				colvarNationalRegionID.IsNullable = true;
				colvarNationalRegionID.IsPrimaryKey = false;
				colvarNationalRegionID.IsForeignKey = false;
				colvarNationalRegionID.IsReadOnly = false;
				colvarNationalRegionID.DefaultSetting = @"";
				colvarNationalRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNationalRegionID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_RecruitingLine",schema);
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
		public RecruitingLineView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int ReportingLevel {
			get { return GetColumnValue<int>(Columns.ReportingLevel); }
			set { SetColumnValue(Columns.ReportingLevel, value); }
		}
		[DataMember]
		public int RoleLocationID {
			get { return GetColumnValue<int>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int? RepID {
			get { return GetColumnValue<int?>(Columns.RepID); }
			set { SetColumnValue(Columns.RepID, value); }
		}
		[DataMember]
		public int? ManagerID {
			get { return GetColumnValue<int?>(Columns.ManagerID); }
			set { SetColumnValue(Columns.ManagerID, value); }
		}
		[DataMember]
		public int? RegionID {
			get { return GetColumnValue<int?>(Columns.RegionID); }
			set { SetColumnValue(Columns.RegionID, value); }
		}
		[DataMember]
		public int? NationalRegionID {
			get { return GetColumnValue<int?>(Columns.NationalRegionID); }
			set { SetColumnValue(Columns.NationalRegionID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return SeasonID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReportingLevelColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RepIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ManagerIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn RegionIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn NationalRegionIDColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string SeasonID = @"SeasonID";
			public const string RecruitID = @"RecruitID";
			public const string ReportingLevel = @"ReportingLevel";
			public const string RoleLocationID = @"RoleLocationID";
			public const string TeamID = @"TeamID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string RepID = @"RepID";
			public const string ManagerID = @"ManagerID";
			public const string RegionID = @"RegionID";
			public const string NationalRegionID = @"NationalRegionID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RecruitingLineMobileView class.
	/// </summary>
	[DataContract]
	public partial class RecruitingLineMobileViewCollection : ReadOnlyList<RecruitingLineMobileView, RecruitingLineMobileViewCollection>
	{
		public static RecruitingLineMobileViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RecruitingLineMobileViewCollection result = new RecruitingLineMobileViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_RecruitingLineMobile view.
	/// </summary>
	[DataContract]
	public partial class RecruitingLineMobileView : ReadOnlyRecord<RecruitingLineMobileView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_RecruitingLineMobile", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarReportingLevel = new TableSchema.TableColumn(schema);
				colvarReportingLevel.ColumnName = "ReportingLevel";
				colvarReportingLevel.DataType = DbType.Int32;
				colvarReportingLevel.MaxLength = 0;
				colvarReportingLevel.AutoIncrement = false;
				colvarReportingLevel.IsNullable = false;
				colvarReportingLevel.IsPrimaryKey = false;
				colvarReportingLevel.IsForeignKey = false;
				colvarReportingLevel.IsReadOnly = false;
				colvarReportingLevel.DefaultSetting = @"";
				colvarReportingLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportingLevel);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = false;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarRepID = new TableSchema.TableColumn(schema);
				colvarRepID.ColumnName = "RepID";
				colvarRepID.DataType = DbType.Int32;
				colvarRepID.MaxLength = 0;
				colvarRepID.AutoIncrement = false;
				colvarRepID.IsNullable = true;
				colvarRepID.IsPrimaryKey = false;
				colvarRepID.IsForeignKey = false;
				colvarRepID.IsReadOnly = false;
				colvarRepID.DefaultSetting = @"";
				colvarRepID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRepID);

				TableSchema.TableColumn colvarManagerID = new TableSchema.TableColumn(schema);
				colvarManagerID.ColumnName = "ManagerID";
				colvarManagerID.DataType = DbType.Int32;
				colvarManagerID.MaxLength = 0;
				colvarManagerID.AutoIncrement = false;
				colvarManagerID.IsNullable = true;
				colvarManagerID.IsPrimaryKey = false;
				colvarManagerID.IsForeignKey = false;
				colvarManagerID.IsReadOnly = false;
				colvarManagerID.DefaultSetting = @"";
				colvarManagerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerID);

				TableSchema.TableColumn colvarRegionID = new TableSchema.TableColumn(schema);
				colvarRegionID.ColumnName = "RegionID";
				colvarRegionID.DataType = DbType.Int32;
				colvarRegionID.MaxLength = 0;
				colvarRegionID.AutoIncrement = false;
				colvarRegionID.IsNullable = true;
				colvarRegionID.IsPrimaryKey = false;
				colvarRegionID.IsForeignKey = false;
				colvarRegionID.IsReadOnly = false;
				colvarRegionID.DefaultSetting = @"";
				colvarRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionID);

				TableSchema.TableColumn colvarNationalRegionID = new TableSchema.TableColumn(schema);
				colvarNationalRegionID.ColumnName = "NationalRegionID";
				colvarNationalRegionID.DataType = DbType.Int32;
				colvarNationalRegionID.MaxLength = 0;
				colvarNationalRegionID.AutoIncrement = false;
				colvarNationalRegionID.IsNullable = true;
				colvarNationalRegionID.IsPrimaryKey = false;
				colvarNationalRegionID.IsForeignKey = false;
				colvarNationalRegionID.IsReadOnly = false;
				colvarNationalRegionID.DefaultSetting = @"";
				colvarNationalRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNationalRegionID);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarUserName = new TableSchema.TableColumn(schema);
				colvarUserName.ColumnName = "UserName";
				colvarUserName.DataType = DbType.String;
				colvarUserName.MaxLength = 50;
				colvarUserName.AutoIncrement = false;
				colvarUserName.IsNullable = false;
				colvarUserName.IsPrimaryKey = false;
				colvarUserName.IsForeignKey = false;
				colvarUserName.IsReadOnly = false;
				colvarUserName.DefaultSetting = @"";
				colvarUserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserName);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 60;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarOfficeName = new TableSchema.TableColumn(schema);
				colvarOfficeName.ColumnName = "Office Name";
				colvarOfficeName.DataType = DbType.AnsiString;
				colvarOfficeName.MaxLength = 50;
				colvarOfficeName.AutoIncrement = false;
				colvarOfficeName.IsNullable = true;
				colvarOfficeName.IsPrimaryKey = false;
				colvarOfficeName.IsForeignKey = false;
				colvarOfficeName.IsReadOnly = false;
				colvarOfficeName.DefaultSetting = @"";
				colvarOfficeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeName);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_RecruitingLineMobile",schema);
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
		public RecruitingLineMobileView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int ReportingLevel {
			get { return GetColumnValue<int>(Columns.ReportingLevel); }
			set { SetColumnValue(Columns.ReportingLevel, value); }
		}
		[DataMember]
		public int RoleLocationID {
			get { return GetColumnValue<int>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int? RepID {
			get { return GetColumnValue<int?>(Columns.RepID); }
			set { SetColumnValue(Columns.RepID, value); }
		}
		[DataMember]
		public int? ManagerID {
			get { return GetColumnValue<int?>(Columns.ManagerID); }
			set { SetColumnValue(Columns.ManagerID, value); }
		}
		[DataMember]
		public int? RegionID {
			get { return GetColumnValue<int?>(Columns.RegionID); }
			set { SetColumnValue(Columns.RegionID, value); }
		}
		[DataMember]
		public int? NationalRegionID {
			get { return GetColumnValue<int?>(Columns.NationalRegionID); }
			set { SetColumnValue(Columns.NationalRegionID, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string UserName {
			get { return GetColumnValue<string>(Columns.UserName); }
			set { SetColumnValue(Columns.UserName, value); }
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return SeasonID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ReportingLevelColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RepIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ManagerIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn RegionIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn NationalRegionIDColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn UserNameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string SeasonID = @"SeasonID";
			public const string RecruitID = @"RecruitID";
			public const string ReportingLevel = @"ReportingLevel";
			public const string RoleLocationID = @"RoleLocationID";
			public const string TeamID = @"TeamID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string RepID = @"RepID";
			public const string ManagerID = @"ManagerID";
			public const string RegionID = @"RegionID";
			public const string NationalRegionID = @"NationalRegionID";
			public const string FullName = @"FullName";
			public const string UserName = @"UserName";
			public const string Password = @"Password";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string OfficeName = @"OfficeName";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RecruitingStructureView class.
	/// </summary>
	[DataContract]
	public partial class RecruitingStructureViewCollection : ReadOnlyList<RecruitingStructureView, RecruitingStructureViewCollection>
	{
		public static RecruitingStructureViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RecruitingStructureViewCollection result = new RecruitingStructureViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_RecruitingStructure view.
	/// </summary>
	[DataContract]
	public partial class RecruitingStructureView : ReadOnlyRecord<RecruitingStructureView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_RecruitingStructure", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarManagerID = new TableSchema.TableColumn(schema);
				colvarManagerID.ColumnName = "ManagerID";
				colvarManagerID.DataType = DbType.Int32;
				colvarManagerID.MaxLength = 0;
				colvarManagerID.AutoIncrement = false;
				colvarManagerID.IsNullable = true;
				colvarManagerID.IsPrimaryKey = false;
				colvarManagerID.IsForeignKey = false;
				colvarManagerID.IsReadOnly = false;
				colvarManagerID.DefaultSetting = @"";
				colvarManagerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerID);

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarRegionID = new TableSchema.TableColumn(schema);
				colvarRegionID.ColumnName = "RegionID";
				colvarRegionID.DataType = DbType.Int32;
				colvarRegionID.MaxLength = 0;
				colvarRegionID.AutoIncrement = false;
				colvarRegionID.IsNullable = true;
				colvarRegionID.IsPrimaryKey = false;
				colvarRegionID.IsForeignKey = false;
				colvarRegionID.IsReadOnly = false;
				colvarRegionID.DefaultSetting = @"";
				colvarRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionID);

				TableSchema.TableColumn colvarNationalRegionID = new TableSchema.TableColumn(schema);
				colvarNationalRegionID.ColumnName = "NationalRegionID";
				colvarNationalRegionID.DataType = DbType.Int32;
				colvarNationalRegionID.MaxLength = 0;
				colvarNationalRegionID.AutoIncrement = false;
				colvarNationalRegionID.IsNullable = true;
				colvarNationalRegionID.IsPrimaryKey = false;
				colvarNationalRegionID.IsForeignKey = false;
				colvarNationalRegionID.IsReadOnly = false;
				colvarNationalRegionID.DefaultSetting = @"";
				colvarNationalRegionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNationalRegionID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_RecruitingStructure",schema);
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
		public RecruitingStructureView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? ManagerID {
			get { return GetColumnValue<int?>(Columns.ManagerID); }
			set { SetColumnValue(Columns.ManagerID, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int? RegionID {
			get { return GetColumnValue<int?>(Columns.RegionID); }
			set { SetColumnValue(Columns.RegionID, value); }
		}
		[DataMember]
		public int? NationalRegionID {
			get { return GetColumnValue<int?>(Columns.NationalRegionID); }
			set { SetColumnValue(Columns.NationalRegionID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RecruitID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ManagerIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RegionIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn NationalRegionIDColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RecruitID = @"RecruitID";
			public const string SeasonID = @"SeasonID";
			public const string ManagerID = @"ManagerID";
			public const string TeamID = @"TeamID";
			public const string RegionID = @"RegionID";
			public const string NationalRegionID = @"NationalRegionID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RecruitPayrollStatusView class.
	/// </summary>
	[DataContract]
	public partial class RecruitPayrollStatusViewCollection : ReadOnlyList<RecruitPayrollStatusView, RecruitPayrollStatusViewCollection>
	{
		public static RecruitPayrollStatusViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RecruitPayrollStatusViewCollection result = new RecruitPayrollStatusViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_RecruitPayrollStatus view.
	/// </summary>
	[DataContract]
	public partial class RecruitPayrollStatusView : ReadOnlyRecord<RecruitPayrollStatusView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_RecruitPayrollStatus", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarUserTypeID = new TableSchema.TableColumn(schema);
				colvarUserTypeID.ColumnName = "UserTypeID";
				colvarUserTypeID.DataType = DbType.Int16;
				colvarUserTypeID.MaxLength = 0;
				colvarUserTypeID.AutoIncrement = false;
				colvarUserTypeID.IsNullable = false;
				colvarUserTypeID.IsPrimaryKey = false;
				colvarUserTypeID.IsForeignKey = false;
				colvarUserTypeID.IsReadOnly = false;
				colvarUserTypeID.DefaultSetting = @"";
				colvarUserTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeID);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = false;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
				colvarTitle.DataType = DbType.AnsiString;
				colvarTitle.MaxLength = 30;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = false;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				colvarTitle.DefaultSetting = @"";
				colvarTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitle);

				TableSchema.TableColumn colvarPayscaleID = new TableSchema.TableColumn(schema);
				colvarPayscaleID.ColumnName = "PayscaleID";
				colvarPayscaleID.DataType = DbType.Int32;
				colvarPayscaleID.MaxLength = 0;
				colvarPayscaleID.AutoIncrement = false;
				colvarPayscaleID.IsNullable = false;
				colvarPayscaleID.IsPrimaryKey = false;
				colvarPayscaleID.IsForeignKey = false;
				colvarPayscaleID.IsReadOnly = false;
				colvarPayscaleID.DefaultSetting = @"";
				colvarPayscaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayscaleID);

				TableSchema.TableColumn colvarIsActiveRecruit = new TableSchema.TableColumn(schema);
				colvarIsActiveRecruit.ColumnName = "IsActiveRecruit";
				colvarIsActiveRecruit.DataType = DbType.Boolean;
				colvarIsActiveRecruit.MaxLength = 0;
				colvarIsActiveRecruit.AutoIncrement = false;
				colvarIsActiveRecruit.IsNullable = false;
				colvarIsActiveRecruit.IsPrimaryKey = false;
				colvarIsActiveRecruit.IsForeignKey = false;
				colvarIsActiveRecruit.IsReadOnly = false;
				colvarIsActiveRecruit.DefaultSetting = @"";
				colvarIsActiveRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveRecruit);

				TableSchema.TableColumn colvarIsActiveUser = new TableSchema.TableColumn(schema);
				colvarIsActiveUser.ColumnName = "IsActiveUser";
				colvarIsActiveUser.DataType = DbType.Boolean;
				colvarIsActiveUser.MaxLength = 0;
				colvarIsActiveUser.AutoIncrement = false;
				colvarIsActiveUser.IsNullable = false;
				colvarIsActiveUser.IsPrimaryKey = false;
				colvarIsActiveUser.IsForeignKey = false;
				colvarIsActiveUser.IsReadOnly = false;
				colvarIsActiveUser.DefaultSetting = @"";
				colvarIsActiveUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveUser);

				TableSchema.TableColumn colvarIsDeletedRecruit = new TableSchema.TableColumn(schema);
				colvarIsDeletedRecruit.ColumnName = "IsDeletedRecruit";
				colvarIsDeletedRecruit.DataType = DbType.Boolean;
				colvarIsDeletedRecruit.MaxLength = 0;
				colvarIsDeletedRecruit.AutoIncrement = false;
				colvarIsDeletedRecruit.IsNullable = false;
				colvarIsDeletedRecruit.IsPrimaryKey = false;
				colvarIsDeletedRecruit.IsForeignKey = false;
				colvarIsDeletedRecruit.IsReadOnly = false;
				colvarIsDeletedRecruit.DefaultSetting = @"";
				colvarIsDeletedRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedRecruit);

				TableSchema.TableColumn colvarIsDeletedUser = new TableSchema.TableColumn(schema);
				colvarIsDeletedUser.ColumnName = "IsDeletedUser";
				colvarIsDeletedUser.DataType = DbType.Boolean;
				colvarIsDeletedUser.MaxLength = 0;
				colvarIsDeletedUser.AutoIncrement = false;
				colvarIsDeletedUser.IsNullable = false;
				colvarIsDeletedUser.IsPrimaryKey = false;
				colvarIsDeletedUser.IsForeignKey = false;
				colvarIsDeletedUser.IsReadOnly = false;
				colvarIsDeletedUser.DefaultSetting = @"";
				colvarIsDeletedUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedUser);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarOfficeStateId = new TableSchema.TableColumn(schema);
				colvarOfficeStateId.ColumnName = "OfficeStateId";
				colvarOfficeStateId.DataType = DbType.AnsiString;
				colvarOfficeStateId.MaxLength = 4;
				colvarOfficeStateId.AutoIncrement = false;
				colvarOfficeStateId.IsNullable = true;
				colvarOfficeStateId.IsPrimaryKey = false;
				colvarOfficeStateId.IsForeignKey = false;
				colvarOfficeStateId.IsReadOnly = false;
				colvarOfficeStateId.DefaultSetting = @"";
				colvarOfficeStateId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeStateId);

				TableSchema.TableColumn colvarArrivalDate = new TableSchema.TableColumn(schema);
				colvarArrivalDate.ColumnName = "ArrivalDate";
				colvarArrivalDate.DataType = DbType.DateTime;
				colvarArrivalDate.MaxLength = 0;
				colvarArrivalDate.AutoIncrement = false;
				colvarArrivalDate.IsNullable = true;
				colvarArrivalDate.IsPrimaryKey = false;
				colvarArrivalDate.IsForeignKey = false;
				colvarArrivalDate.IsReadOnly = false;
				colvarArrivalDate.DefaultSetting = @"";
				colvarArrivalDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarArrivalDate);

				TableSchema.TableColumn colvarQuitDate = new TableSchema.TableColumn(schema);
				colvarQuitDate.ColumnName = "QuitDate";
				colvarQuitDate.DataType = DbType.DateTime;
				colvarQuitDate.MaxLength = 0;
				colvarQuitDate.AutoIncrement = false;
				colvarQuitDate.IsNullable = true;
				colvarQuitDate.IsPrimaryKey = false;
				colvarQuitDate.IsForeignKey = false;
				colvarQuitDate.IsReadOnly = false;
				colvarQuitDate.DefaultSetting = @"";
				colvarQuitDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuitDate);

				TableSchema.TableColumn colvarTerminationCategoryID = new TableSchema.TableColumn(schema);
				colvarTerminationCategoryID.ColumnName = "TerminationCategoryID";
				colvarTerminationCategoryID.DataType = DbType.Int32;
				colvarTerminationCategoryID.MaxLength = 0;
				colvarTerminationCategoryID.AutoIncrement = false;
				colvarTerminationCategoryID.IsNullable = true;
				colvarTerminationCategoryID.IsPrimaryKey = false;
				colvarTerminationCategoryID.IsForeignKey = false;
				colvarTerminationCategoryID.IsReadOnly = false;
				colvarTerminationCategoryID.DefaultSetting = @"";
				colvarTerminationCategoryID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationCategoryID);

				TableSchema.TableColumn colvarLastRollCallDate = new TableSchema.TableColumn(schema);
				colvarLastRollCallDate.ColumnName = "LastRollCallDate";
				colvarLastRollCallDate.DataType = DbType.DateTime;
				colvarLastRollCallDate.MaxLength = 0;
				colvarLastRollCallDate.AutoIncrement = false;
				colvarLastRollCallDate.IsNullable = true;
				colvarLastRollCallDate.IsPrimaryKey = false;
				colvarLastRollCallDate.IsForeignKey = false;
				colvarLastRollCallDate.IsReadOnly = false;
				colvarLastRollCallDate.DefaultSetting = @"";
				colvarLastRollCallDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastRollCallDate);

				TableSchema.TableColumn colvarIsServiceTech = new TableSchema.TableColumn(schema);
				colvarIsServiceTech.ColumnName = "IsServiceTech";
				colvarIsServiceTech.DataType = DbType.Boolean;
				colvarIsServiceTech.MaxLength = 0;
				colvarIsServiceTech.AutoIncrement = false;
				colvarIsServiceTech.IsNullable = true;
				colvarIsServiceTech.IsPrimaryKey = false;
				colvarIsServiceTech.IsForeignKey = false;
				colvarIsServiceTech.IsReadOnly = false;
				colvarIsServiceTech.DefaultSetting = @"";
				colvarIsServiceTech.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsServiceTech);

				TableSchema.TableColumn colvarOfficeName = new TableSchema.TableColumn(schema);
				colvarOfficeName.ColumnName = "OfficeName";
				colvarOfficeName.DataType = DbType.AnsiString;
				colvarOfficeName.MaxLength = 50;
				colvarOfficeName.AutoIncrement = false;
				colvarOfficeName.IsNullable = true;
				colvarOfficeName.IsPrimaryKey = false;
				colvarOfficeName.IsForeignKey = false;
				colvarOfficeName.IsReadOnly = false;
				colvarOfficeName.DefaultSetting = @"";
				colvarOfficeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeName);

				TableSchema.TableColumn colvarStartingPointBank = new TableSchema.TableColumn(schema);
				colvarStartingPointBank.ColumnName = "StartingPointBank";
				colvarStartingPointBank.DataType = DbType.Double;
				colvarStartingPointBank.MaxLength = 0;
				colvarStartingPointBank.AutoIncrement = false;
				colvarStartingPointBank.IsNullable = true;
				colvarStartingPointBank.IsPrimaryKey = false;
				colvarStartingPointBank.IsForeignKey = false;
				colvarStartingPointBank.IsReadOnly = false;
				colvarStartingPointBank.DefaultSetting = @"";
				colvarStartingPointBank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartingPointBank);

				TableSchema.TableColumn colvarPayScheduleID = new TableSchema.TableColumn(schema);
				colvarPayScheduleID.ColumnName = "PayScheduleID";
				colvarPayScheduleID.DataType = DbType.Int32;
				colvarPayScheduleID.MaxLength = 0;
				colvarPayScheduleID.AutoIncrement = false;
				colvarPayScheduleID.IsNullable = true;
				colvarPayScheduleID.IsPrimaryKey = false;
				colvarPayScheduleID.IsForeignKey = false;
				colvarPayScheduleID.IsReadOnly = false;
				colvarPayScheduleID.DefaultSetting = @"";
				colvarPayScheduleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScheduleID);

				TableSchema.TableColumn colvarPayAsContractor = new TableSchema.TableColumn(schema);
				colvarPayAsContractor.ColumnName = "PayAsContractor";
				colvarPayAsContractor.DataType = DbType.Boolean;
				colvarPayAsContractor.MaxLength = 0;
				colvarPayAsContractor.AutoIncrement = false;
				colvarPayAsContractor.IsNullable = true;
				colvarPayAsContractor.IsPrimaryKey = false;
				colvarPayAsContractor.IsForeignKey = false;
				colvarPayAsContractor.IsReadOnly = false;
				colvarPayAsContractor.DefaultSetting = @"";
				colvarPayAsContractor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayAsContractor);

				TableSchema.TableColumn colvarManagerRecruitId = new TableSchema.TableColumn(schema);
				colvarManagerRecruitId.ColumnName = "ManagerRecruitId";
				colvarManagerRecruitId.DataType = DbType.Int32;
				colvarManagerRecruitId.MaxLength = 0;
				colvarManagerRecruitId.AutoIncrement = false;
				colvarManagerRecruitId.IsNullable = true;
				colvarManagerRecruitId.IsPrimaryKey = false;
				colvarManagerRecruitId.IsForeignKey = false;
				colvarManagerRecruitId.IsReadOnly = false;
				colvarManagerRecruitId.DefaultSetting = @"";
				colvarManagerRecruitId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerRecruitId);

				TableSchema.TableColumn colvarManagerName = new TableSchema.TableColumn(schema);
				colvarManagerName.ColumnName = "ManagerName";
				colvarManagerName.DataType = DbType.String;
				colvarManagerName.MaxLength = 101;
				colvarManagerName.AutoIncrement = false;
				colvarManagerName.IsNullable = true;
				colvarManagerName.IsPrimaryKey = false;
				colvarManagerName.IsForeignKey = false;
				colvarManagerName.IsReadOnly = false;
				colvarManagerName.DefaultSetting = @"";
				colvarManagerName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerName);

				TableSchema.TableColumn colvarFirstArrivalDate = new TableSchema.TableColumn(schema);
				colvarFirstArrivalDate.ColumnName = "FirstArrivalDate";
				colvarFirstArrivalDate.DataType = DbType.DateTime;
				colvarFirstArrivalDate.MaxLength = 0;
				colvarFirstArrivalDate.AutoIncrement = false;
				colvarFirstArrivalDate.IsNullable = true;
				colvarFirstArrivalDate.IsPrimaryKey = false;
				colvarFirstArrivalDate.IsForeignKey = false;
				colvarFirstArrivalDate.IsReadOnly = false;
				colvarFirstArrivalDate.DefaultSetting = @"";
				colvarFirstArrivalDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstArrivalDate);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_RecruitPayrollStatus",schema);
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
		public RecruitPayrollStatusView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public short UserTypeID {
			get { return GetColumnValue<short>(Columns.UserTypeID); }
			set { SetColumnValue(Columns.UserTypeID, value); }
		}
		[DataMember]
		public int RoleLocationID {
			get { return GetColumnValue<int>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public string Title {
			get { return GetColumnValue<string>(Columns.Title); }
			set { SetColumnValue(Columns.Title, value); }
		}
		[DataMember]
		public int PayscaleID {
			get { return GetColumnValue<int>(Columns.PayscaleID); }
			set { SetColumnValue(Columns.PayscaleID, value); }
		}
		[DataMember]
		public bool IsActiveRecruit {
			get { return GetColumnValue<bool>(Columns.IsActiveRecruit); }
			set { SetColumnValue(Columns.IsActiveRecruit, value); }
		}
		[DataMember]
		public bool IsActiveUser {
			get { return GetColumnValue<bool>(Columns.IsActiveUser); }
			set { SetColumnValue(Columns.IsActiveUser, value); }
		}
		[DataMember]
		public bool IsDeletedRecruit {
			get { return GetColumnValue<bool>(Columns.IsDeletedRecruit); }
			set { SetColumnValue(Columns.IsDeletedRecruit, value); }
		}
		[DataMember]
		public bool IsDeletedUser {
			get { return GetColumnValue<bool>(Columns.IsDeletedUser); }
			set { SetColumnValue(Columns.IsDeletedUser, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public string OfficeStateId {
			get { return GetColumnValue<string>(Columns.OfficeStateId); }
			set { SetColumnValue(Columns.OfficeStateId, value); }
		}
		[DataMember]
		public DateTime? ArrivalDate {
			get { return GetColumnValue<DateTime?>(Columns.ArrivalDate); }
			set { SetColumnValue(Columns.ArrivalDate, value); }
		}
		[DataMember]
		public DateTime? QuitDate {
			get { return GetColumnValue<DateTime?>(Columns.QuitDate); }
			set { SetColumnValue(Columns.QuitDate, value); }
		}
		[DataMember]
		public int? TerminationCategoryID {
			get { return GetColumnValue<int?>(Columns.TerminationCategoryID); }
			set { SetColumnValue(Columns.TerminationCategoryID, value); }
		}
		[DataMember]
		public DateTime? LastRollCallDate {
			get { return GetColumnValue<DateTime?>(Columns.LastRollCallDate); }
			set { SetColumnValue(Columns.LastRollCallDate, value); }
		}
		[DataMember]
		public bool? IsServiceTech {
			get { return GetColumnValue<bool?>(Columns.IsServiceTech); }
			set { SetColumnValue(Columns.IsServiceTech, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}
		[DataMember]
		public double? StartingPointBank {
			get { return GetColumnValue<double?>(Columns.StartingPointBank); }
			set { SetColumnValue(Columns.StartingPointBank, value); }
		}
		[DataMember]
		public int? PayScheduleID {
			get { return GetColumnValue<int?>(Columns.PayScheduleID); }
			set { SetColumnValue(Columns.PayScheduleID, value); }
		}
		[DataMember]
		public bool? PayAsContractor {
			get { return GetColumnValue<bool?>(Columns.PayAsContractor); }
			set { SetColumnValue(Columns.PayAsContractor, value); }
		}
		[DataMember]
		public int? ManagerRecruitId {
			get { return GetColumnValue<int?>(Columns.ManagerRecruitId); }
			set { SetColumnValue(Columns.ManagerRecruitId, value); }
		}
		[DataMember]
		public string ManagerName {
			get { return GetColumnValue<string>(Columns.ManagerName); }
			set { SetColumnValue(Columns.ManagerName, value); }
		}
		[DataMember]
		public DateTime? FirstArrivalDate {
			get { return GetColumnValue<DateTime?>(Columns.FirstArrivalDate); }
			set { SetColumnValue(Columns.FirstArrivalDate, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return UserID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn UserTypeIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TitleColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn PayscaleIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn IsActiveRecruitColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn IsActiveUserColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn IsDeletedRecruitColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn IsDeletedUserColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn OfficeStateIdColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn ArrivalDateColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn QuitDateColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn TerminationCategoryIDColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn LastRollCallDateColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn IsServiceTechColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn StartingPointBankColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn PayScheduleIDColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn PayAsContractorColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn ManagerRecruitIdColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn ManagerNameColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn FirstArrivalDateColumn
		{
			get { return Schema.Columns[26]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string RecruitID = @"RecruitID";
			public const string SeasonID = @"SeasonID";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string FullName = @"FullName";
			public const string UserTypeID = @"UserTypeID";
			public const string RoleLocationID = @"RoleLocationID";
			public const string Title = @"Title";
			public const string PayscaleID = @"PayscaleID";
			public const string IsActiveRecruit = @"IsActiveRecruit";
			public const string IsActiveUser = @"IsActiveUser";
			public const string IsDeletedRecruit = @"IsDeletedRecruit";
			public const string IsDeletedUser = @"IsDeletedUser";
			public const string TeamLocationID = @"TeamLocationID";
			public const string OfficeStateId = @"OfficeStateId";
			public const string ArrivalDate = @"ArrivalDate";
			public const string QuitDate = @"QuitDate";
			public const string TerminationCategoryID = @"TerminationCategoryID";
			public const string LastRollCallDate = @"LastRollCallDate";
			public const string IsServiceTech = @"IsServiceTech";
			public const string OfficeName = @"OfficeName";
			public const string StartingPointBank = @"StartingPointBank";
			public const string PayScheduleID = @"PayScheduleID";
			public const string PayAsContractor = @"PayAsContractor";
			public const string ManagerRecruitId = @"ManagerRecruitId";
			public const string ManagerName = @"ManagerName";
			public const string FirstArrivalDate = @"FirstArrivalDate";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RecruitUserView class.
	/// </summary>
	[DataContract]
	public partial class RecruitUserViewCollection : ReadOnlyList<RecruitUserView, RecruitUserViewCollection>
	{
		public static RecruitUserViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RecruitUserViewCollection result = new RecruitUserViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_RecruitUser view.
	/// </summary>
	[DataContract]
	public partial class RecruitUserView : ReadOnlyRecord<RecruitUserView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_RecruitUser", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarRecruitedByID = new TableSchema.TableColumn(schema);
				colvarRecruitedByID.ColumnName = "RecruitedByID";
				colvarRecruitedByID.DataType = DbType.Int32;
				colvarRecruitedByID.MaxLength = 0;
				colvarRecruitedByID.AutoIncrement = false;
				colvarRecruitedByID.IsNullable = true;
				colvarRecruitedByID.IsPrimaryKey = false;
				colvarRecruitedByID.IsForeignKey = false;
				colvarRecruitedByID.IsReadOnly = false;
				colvarRecruitedByID.DefaultSetting = @"";
				colvarRecruitedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitedByID);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = true;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarMiddleName = new TableSchema.TableColumn(schema);
				colvarMiddleName.ColumnName = "MiddleName";
				colvarMiddleName.DataType = DbType.String;
				colvarMiddleName.MaxLength = 50;
				colvarMiddleName.AutoIncrement = false;
				colvarMiddleName.IsNullable = true;
				colvarMiddleName.IsPrimaryKey = false;
				colvarMiddleName.IsForeignKey = false;
				colvarMiddleName.IsReadOnly = false;
				colvarMiddleName.DefaultSetting = @"";
				colvarMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddleName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = true;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarPreferredName = new TableSchema.TableColumn(schema);
				colvarPreferredName.ColumnName = "PreferredName";
				colvarPreferredName.DataType = DbType.String;
				colvarPreferredName.MaxLength = 50;
				colvarPreferredName.AutoIncrement = false;
				colvarPreferredName.IsNullable = true;
				colvarPreferredName.IsPrimaryKey = false;
				colvarPreferredName.IsForeignKey = false;
				colvarPreferredName.IsReadOnly = false;
				colvarPreferredName.DefaultSetting = @"";
				colvarPreferredName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreferredName);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarPublicFullName = new TableSchema.TableColumn(schema);
				colvarPublicFullName.ColumnName = "PublicFullName";
				colvarPublicFullName.DataType = DbType.String;
				colvarPublicFullName.MaxLength = 53;
				colvarPublicFullName.AutoIncrement = false;
				colvarPublicFullName.IsNullable = true;
				colvarPublicFullName.IsPrimaryKey = false;
				colvarPublicFullName.IsForeignKey = false;
				colvarPublicFullName.IsReadOnly = false;
				colvarPublicFullName.DefaultSetting = @"";
				colvarPublicFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPublicFullName);

				TableSchema.TableColumn colvarUserName = new TableSchema.TableColumn(schema);
				colvarUserName.ColumnName = "UserName";
				colvarUserName.DataType = DbType.String;
				colvarUserName.MaxLength = 50;
				colvarUserName.AutoIncrement = false;
				colvarUserName.IsNullable = false;
				colvarUserName.IsPrimaryKey = false;
				colvarUserName.IsForeignKey = false;
				colvarUserName.IsReadOnly = false;
				colvarUserName.DefaultSetting = @"";
				colvarUserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserName);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 100;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarPhoneCell = new TableSchema.TableColumn(schema);
				colvarPhoneCell.ColumnName = "PhoneCell";
				colvarPhoneCell.DataType = DbType.String;
				colvarPhoneCell.MaxLength = 50;
				colvarPhoneCell.AutoIncrement = false;
				colvarPhoneCell.IsNullable = true;
				colvarPhoneCell.IsPrimaryKey = false;
				colvarPhoneCell.IsForeignKey = false;
				colvarPhoneCell.IsReadOnly = false;
				colvarPhoneCell.DefaultSetting = @"";
				colvarPhoneCell.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCell);

				TableSchema.TableColumn colvarPhoneCellCarrierID = new TableSchema.TableColumn(schema);
				colvarPhoneCellCarrierID.ColumnName = "PhoneCellCarrierID";
				colvarPhoneCellCarrierID.DataType = DbType.Int16;
				colvarPhoneCellCarrierID.MaxLength = 0;
				colvarPhoneCellCarrierID.AutoIncrement = false;
				colvarPhoneCellCarrierID.IsNullable = true;
				colvarPhoneCellCarrierID.IsPrimaryKey = false;
				colvarPhoneCellCarrierID.IsForeignKey = false;
				colvarPhoneCellCarrierID.IsReadOnly = false;
				colvarPhoneCellCarrierID.DefaultSetting = @"";
				colvarPhoneCellCarrierID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCellCarrierID);

				TableSchema.TableColumn colvarIsActiveUser = new TableSchema.TableColumn(schema);
				colvarIsActiveUser.ColumnName = "IsActiveUser";
				colvarIsActiveUser.DataType = DbType.Boolean;
				colvarIsActiveUser.MaxLength = 0;
				colvarIsActiveUser.AutoIncrement = false;
				colvarIsActiveUser.IsNullable = false;
				colvarIsActiveUser.IsPrimaryKey = false;
				colvarIsActiveUser.IsForeignKey = false;
				colvarIsActiveUser.IsReadOnly = false;
				colvarIsActiveUser.DefaultSetting = @"";
				colvarIsActiveUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveUser);

				TableSchema.TableColumn colvarIsDeletedUser = new TableSchema.TableColumn(schema);
				colvarIsDeletedUser.ColumnName = "IsDeletedUser";
				colvarIsDeletedUser.DataType = DbType.Boolean;
				colvarIsDeletedUser.MaxLength = 0;
				colvarIsDeletedUser.AutoIncrement = false;
				colvarIsDeletedUser.IsNullable = false;
				colvarIsDeletedUser.IsPrimaryKey = false;
				colvarIsDeletedUser.IsForeignKey = false;
				colvarIsDeletedUser.IsReadOnly = false;
				colvarIsDeletedUser.DefaultSetting = @"";
				colvarIsDeletedUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedUser);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarReportsToID = new TableSchema.TableColumn(schema);
				colvarReportsToID.ColumnName = "ReportsToID";
				colvarReportsToID.DataType = DbType.Int32;
				colvarReportsToID.MaxLength = 0;
				colvarReportsToID.AutoIncrement = false;
				colvarReportsToID.IsNullable = true;
				colvarReportsToID.IsPrimaryKey = false;
				colvarReportsToID.IsForeignKey = false;
				colvarReportsToID.IsReadOnly = false;
				colvarReportsToID.DefaultSetting = @"";
				colvarReportsToID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportsToID);

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarSocialSecCardStatusID = new TableSchema.TableColumn(schema);
				colvarSocialSecCardStatusID.ColumnName = "SocialSecCardStatusID";
				colvarSocialSecCardStatusID.DataType = DbType.Int32;
				colvarSocialSecCardStatusID.MaxLength = 0;
				colvarSocialSecCardStatusID.AutoIncrement = false;
				colvarSocialSecCardStatusID.IsNullable = false;
				colvarSocialSecCardStatusID.IsPrimaryKey = false;
				colvarSocialSecCardStatusID.IsForeignKey = false;
				colvarSocialSecCardStatusID.IsReadOnly = false;
				colvarSocialSecCardStatusID.DefaultSetting = @"";
				colvarSocialSecCardStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSocialSecCardStatusID);

				TableSchema.TableColumn colvarDriversLicenseStatusID = new TableSchema.TableColumn(schema);
				colvarDriversLicenseStatusID.ColumnName = "DriversLicenseStatusID";
				colvarDriversLicenseStatusID.DataType = DbType.Int32;
				colvarDriversLicenseStatusID.MaxLength = 0;
				colvarDriversLicenseStatusID.AutoIncrement = false;
				colvarDriversLicenseStatusID.IsNullable = false;
				colvarDriversLicenseStatusID.IsPrimaryKey = false;
				colvarDriversLicenseStatusID.IsForeignKey = false;
				colvarDriversLicenseStatusID.IsReadOnly = false;
				colvarDriversLicenseStatusID.DefaultSetting = @"";
				colvarDriversLicenseStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDriversLicenseStatusID);

				TableSchema.TableColumn colvarW9StatusID = new TableSchema.TableColumn(schema);
				colvarW9StatusID.ColumnName = "W9StatusID";
				colvarW9StatusID.DataType = DbType.Int32;
				colvarW9StatusID.MaxLength = 0;
				colvarW9StatusID.AutoIncrement = false;
				colvarW9StatusID.IsNullable = false;
				colvarW9StatusID.IsPrimaryKey = false;
				colvarW9StatusID.IsForeignKey = false;
				colvarW9StatusID.IsReadOnly = false;
				colvarW9StatusID.DefaultSetting = @"";
				colvarW9StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW9StatusID);

				TableSchema.TableColumn colvarI9StatusID = new TableSchema.TableColumn(schema);
				colvarI9StatusID.ColumnName = "I9StatusID";
				colvarI9StatusID.DataType = DbType.Int32;
				colvarI9StatusID.MaxLength = 0;
				colvarI9StatusID.AutoIncrement = false;
				colvarI9StatusID.IsNullable = false;
				colvarI9StatusID.IsPrimaryKey = false;
				colvarI9StatusID.IsForeignKey = false;
				colvarI9StatusID.IsReadOnly = false;
				colvarI9StatusID.DefaultSetting = @"";
				colvarI9StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarI9StatusID);

				TableSchema.TableColumn colvarW4StatusID = new TableSchema.TableColumn(schema);
				colvarW4StatusID.ColumnName = "W4StatusID";
				colvarW4StatusID.DataType = DbType.Int32;
				colvarW4StatusID.MaxLength = 0;
				colvarW4StatusID.AutoIncrement = false;
				colvarW4StatusID.IsNullable = false;
				colvarW4StatusID.IsPrimaryKey = false;
				colvarW4StatusID.IsForeignKey = false;
				colvarW4StatusID.IsReadOnly = false;
				colvarW4StatusID.DefaultSetting = @"";
				colvarW4StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW4StatusID);

				TableSchema.TableColumn colvarSocialSecCardNotes = new TableSchema.TableColumn(schema);
				colvarSocialSecCardNotes.ColumnName = "SocialSecCardNotes";
				colvarSocialSecCardNotes.DataType = DbType.String;
				colvarSocialSecCardNotes.MaxLength = 250;
				colvarSocialSecCardNotes.AutoIncrement = false;
				colvarSocialSecCardNotes.IsNullable = true;
				colvarSocialSecCardNotes.IsPrimaryKey = false;
				colvarSocialSecCardNotes.IsForeignKey = false;
				colvarSocialSecCardNotes.IsReadOnly = false;
				colvarSocialSecCardNotes.DefaultSetting = @"";
				colvarSocialSecCardNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSocialSecCardNotes);

				TableSchema.TableColumn colvarDriversLicenseNotes = new TableSchema.TableColumn(schema);
				colvarDriversLicenseNotes.ColumnName = "DriversLicenseNotes";
				colvarDriversLicenseNotes.DataType = DbType.String;
				colvarDriversLicenseNotes.MaxLength = 250;
				colvarDriversLicenseNotes.AutoIncrement = false;
				colvarDriversLicenseNotes.IsNullable = true;
				colvarDriversLicenseNotes.IsPrimaryKey = false;
				colvarDriversLicenseNotes.IsForeignKey = false;
				colvarDriversLicenseNotes.IsReadOnly = false;
				colvarDriversLicenseNotes.DefaultSetting = @"";
				colvarDriversLicenseNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDriversLicenseNotes);

				TableSchema.TableColumn colvarW9Notes = new TableSchema.TableColumn(schema);
				colvarW9Notes.ColumnName = "W9Notes";
				colvarW9Notes.DataType = DbType.String;
				colvarW9Notes.MaxLength = 250;
				colvarW9Notes.AutoIncrement = false;
				colvarW9Notes.IsNullable = true;
				colvarW9Notes.IsPrimaryKey = false;
				colvarW9Notes.IsForeignKey = false;
				colvarW9Notes.IsReadOnly = false;
				colvarW9Notes.DefaultSetting = @"";
				colvarW9Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW9Notes);

				TableSchema.TableColumn colvarI9Notes = new TableSchema.TableColumn(schema);
				colvarI9Notes.ColumnName = "I9Notes";
				colvarI9Notes.DataType = DbType.String;
				colvarI9Notes.MaxLength = 250;
				colvarI9Notes.AutoIncrement = false;
				colvarI9Notes.IsNullable = true;
				colvarI9Notes.IsPrimaryKey = false;
				colvarI9Notes.IsForeignKey = false;
				colvarI9Notes.IsReadOnly = false;
				colvarI9Notes.DefaultSetting = @"";
				colvarI9Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarI9Notes);

				TableSchema.TableColumn colvarW4Notes = new TableSchema.TableColumn(schema);
				colvarW4Notes.ColumnName = "W4Notes";
				colvarW4Notes.DataType = DbType.String;
				colvarW4Notes.MaxLength = 250;
				colvarW4Notes.AutoIncrement = false;
				colvarW4Notes.IsNullable = true;
				colvarW4Notes.IsPrimaryKey = false;
				colvarW4Notes.IsForeignKey = false;
				colvarW4Notes.IsReadOnly = false;
				colvarW4Notes.DefaultSetting = @"";
				colvarW4Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW4Notes);

				TableSchema.TableColumn colvarIsActiveRecruit = new TableSchema.TableColumn(schema);
				colvarIsActiveRecruit.ColumnName = "IsActiveRecruit";
				colvarIsActiveRecruit.DataType = DbType.Boolean;
				colvarIsActiveRecruit.MaxLength = 0;
				colvarIsActiveRecruit.AutoIncrement = false;
				colvarIsActiveRecruit.IsNullable = false;
				colvarIsActiveRecruit.IsPrimaryKey = false;
				colvarIsActiveRecruit.IsForeignKey = false;
				colvarIsActiveRecruit.IsReadOnly = false;
				colvarIsActiveRecruit.DefaultSetting = @"";
				colvarIsActiveRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveRecruit);

				TableSchema.TableColumn colvarIsDeletedRecruit = new TableSchema.TableColumn(schema);
				colvarIsDeletedRecruit.ColumnName = "IsDeletedRecruit";
				colvarIsDeletedRecruit.DataType = DbType.Boolean;
				colvarIsDeletedRecruit.MaxLength = 0;
				colvarIsDeletedRecruit.AutoIncrement = false;
				colvarIsDeletedRecruit.IsNullable = false;
				colvarIsDeletedRecruit.IsPrimaryKey = false;
				colvarIsDeletedRecruit.IsForeignKey = false;
				colvarIsDeletedRecruit.IsReadOnly = false;
				colvarIsDeletedRecruit.DefaultSetting = @"";
				colvarIsDeletedRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedRecruit);

				TableSchema.TableColumn colvarUserTypeID = new TableSchema.TableColumn(schema);
				colvarUserTypeID.ColumnName = "UserTypeID";
				colvarUserTypeID.DataType = DbType.Int16;
				colvarUserTypeID.MaxLength = 0;
				colvarUserTypeID.AutoIncrement = false;
				colvarUserTypeID.IsNullable = false;
				colvarUserTypeID.IsPrimaryKey = false;
				colvarUserTypeID.IsForeignKey = false;
				colvarUserTypeID.IsReadOnly = false;
				colvarUserTypeID.DefaultSetting = @"";
				colvarUserTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeID);

				TableSchema.TableColumn colvarUserType = new TableSchema.TableColumn(schema);
				colvarUserType.ColumnName = "UserType";
				colvarUserType.DataType = DbType.AnsiString;
				colvarUserType.MaxLength = 30;
				colvarUserType.AutoIncrement = false;
				colvarUserType.IsNullable = false;
				colvarUserType.IsPrimaryKey = false;
				colvarUserType.IsForeignKey = false;
				colvarUserType.IsReadOnly = false;
				colvarUserType.DefaultSetting = @"";
				colvarUserType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserType);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = false;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarSecurityLevel = new TableSchema.TableColumn(schema);
				colvarSecurityLevel.ColumnName = "SecurityLevel";
				colvarSecurityLevel.DataType = DbType.Byte;
				colvarSecurityLevel.MaxLength = 0;
				colvarSecurityLevel.AutoIncrement = false;
				colvarSecurityLevel.IsNullable = false;
				colvarSecurityLevel.IsPrimaryKey = false;
				colvarSecurityLevel.IsForeignKey = false;
				colvarSecurityLevel.IsReadOnly = false;
				colvarSecurityLevel.DefaultSetting = @"";
				colvarSecurityLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSecurityLevel);

				TableSchema.TableColumn colvarReportingLevel = new TableSchema.TableColumn(schema);
				colvarReportingLevel.ColumnName = "ReportingLevel";
				colvarReportingLevel.DataType = DbType.Int32;
				colvarReportingLevel.MaxLength = 0;
				colvarReportingLevel.AutoIncrement = false;
				colvarReportingLevel.IsNullable = false;
				colvarReportingLevel.IsPrimaryKey = false;
				colvarReportingLevel.IsForeignKey = false;
				colvarReportingLevel.IsReadOnly = false;
				colvarReportingLevel.DefaultSetting = @"";
				colvarReportingLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportingLevel);

				TableSchema.TableColumn colvarUserTypeTeamTypeID = new TableSchema.TableColumn(schema);
				colvarUserTypeTeamTypeID.ColumnName = "UserTypeTeamTypeID";
				colvarUserTypeTeamTypeID.DataType = DbType.Int32;
				colvarUserTypeTeamTypeID.MaxLength = 0;
				colvarUserTypeTeamTypeID.AutoIncrement = false;
				colvarUserTypeTeamTypeID.IsNullable = false;
				colvarUserTypeTeamTypeID.IsPrimaryKey = false;
				colvarUserTypeTeamTypeID.IsForeignKey = false;
				colvarUserTypeTeamTypeID.IsReadOnly = false;
				colvarUserTypeTeamTypeID.DefaultSetting = @"";
				colvarUserTypeTeamTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeTeamTypeID);

				TableSchema.TableColumn colvarUserTypeTeamType = new TableSchema.TableColumn(schema);
				colvarUserTypeTeamType.ColumnName = "UserTypeTeamType";
				colvarUserTypeTeamType.DataType = DbType.AnsiString;
				colvarUserTypeTeamType.MaxLength = 30;
				colvarUserTypeTeamType.AutoIncrement = false;
				colvarUserTypeTeamType.IsNullable = false;
				colvarUserTypeTeamType.IsPrimaryKey = false;
				colvarUserTypeTeamType.IsForeignKey = false;
				colvarUserTypeTeamType.IsReadOnly = false;
				colvarUserTypeTeamType.DefaultSetting = @"";
				colvarUserTypeTeamType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeTeamType);

				TableSchema.TableColumn colvarPayscaleID = new TableSchema.TableColumn(schema);
				colvarPayscaleID.ColumnName = "PayscaleID";
				colvarPayscaleID.DataType = DbType.Int32;
				colvarPayscaleID.MaxLength = 0;
				colvarPayscaleID.AutoIncrement = false;
				colvarPayscaleID.IsNullable = false;
				colvarPayscaleID.IsPrimaryKey = false;
				colvarPayscaleID.IsForeignKey = false;
				colvarPayscaleID.IsReadOnly = false;
				colvarPayscaleID.DefaultSetting = @"";
				colvarPayscaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayscaleID);

				TableSchema.TableColumn colvarPayScaleName = new TableSchema.TableColumn(schema);
				colvarPayScaleName.ColumnName = "PayScaleName";
				colvarPayScaleName.DataType = DbType.String;
				colvarPayScaleName.MaxLength = 20;
				colvarPayScaleName.AutoIncrement = false;
				colvarPayScaleName.IsNullable = true;
				colvarPayScaleName.IsPrimaryKey = false;
				colvarPayScaleName.IsForeignKey = false;
				colvarPayScaleName.IsReadOnly = false;
				colvarPayScaleName.DefaultSetting = @"";
				colvarPayScaleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScaleName);

				TableSchema.TableColumn colvarAlternatePayScheduleID = new TableSchema.TableColumn(schema);
				colvarAlternatePayScheduleID.ColumnName = "AlternatePayScheduleID";
				colvarAlternatePayScheduleID.DataType = DbType.Int32;
				colvarAlternatePayScheduleID.MaxLength = 0;
				colvarAlternatePayScheduleID.AutoIncrement = false;
				colvarAlternatePayScheduleID.IsNullable = true;
				colvarAlternatePayScheduleID.IsPrimaryKey = false;
				colvarAlternatePayScheduleID.IsForeignKey = false;
				colvarAlternatePayScheduleID.IsReadOnly = false;
				colvarAlternatePayScheduleID.DefaultSetting = @"";
				colvarAlternatePayScheduleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAlternatePayScheduleID);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = true;
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
				colvarIsDeleted.IsNullable = true;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarIsServiceTech = new TableSchema.TableColumn(schema);
				colvarIsServiceTech.ColumnName = "IsServiceTech";
				colvarIsServiceTech.DataType = DbType.Boolean;
				colvarIsServiceTech.MaxLength = 0;
				colvarIsServiceTech.AutoIncrement = false;
				colvarIsServiceTech.IsNullable = true;
				colvarIsServiceTech.IsPrimaryKey = false;
				colvarIsServiceTech.IsForeignKey = false;
				colvarIsServiceTech.IsReadOnly = false;
				colvarIsServiceTech.DefaultSetting = @"";
				colvarIsServiceTech.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsServiceTech);

				TableSchema.TableColumn colvarSeasonIsVisibleToRecruits = new TableSchema.TableColumn(schema);
				colvarSeasonIsVisibleToRecruits.ColumnName = "SeasonIsVisibleToRecruits";
				colvarSeasonIsVisibleToRecruits.DataType = DbType.Boolean;
				colvarSeasonIsVisibleToRecruits.MaxLength = 0;
				colvarSeasonIsVisibleToRecruits.AutoIncrement = false;
				colvarSeasonIsVisibleToRecruits.IsNullable = false;
				colvarSeasonIsVisibleToRecruits.IsPrimaryKey = false;
				colvarSeasonIsVisibleToRecruits.IsForeignKey = false;
				colvarSeasonIsVisibleToRecruits.IsReadOnly = false;
				colvarSeasonIsVisibleToRecruits.DefaultSetting = @"";
				colvarSeasonIsVisibleToRecruits.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsVisibleToRecruits);

				TableSchema.TableColumn colvarSeasonStartDate = new TableSchema.TableColumn(schema);
				colvarSeasonStartDate.ColumnName = "SeasonStartDate";
				colvarSeasonStartDate.DataType = DbType.DateTime;
				colvarSeasonStartDate.MaxLength = 0;
				colvarSeasonStartDate.AutoIncrement = false;
				colvarSeasonStartDate.IsNullable = true;
				colvarSeasonStartDate.IsPrimaryKey = false;
				colvarSeasonStartDate.IsForeignKey = false;
				colvarSeasonStartDate.IsReadOnly = false;
				colvarSeasonStartDate.DefaultSetting = @"";
				colvarSeasonStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonStartDate);

				TableSchema.TableColumn colvarSeasonEndDate = new TableSchema.TableColumn(schema);
				colvarSeasonEndDate.ColumnName = "SeasonEndDate";
				colvarSeasonEndDate.DataType = DbType.DateTime;
				colvarSeasonEndDate.MaxLength = 0;
				colvarSeasonEndDate.AutoIncrement = false;
				colvarSeasonEndDate.IsNullable = true;
				colvarSeasonEndDate.IsPrimaryKey = false;
				colvarSeasonEndDate.IsForeignKey = false;
				colvarSeasonEndDate.IsReadOnly = false;
				colvarSeasonEndDate.DefaultSetting = @"";
				colvarSeasonEndDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonEndDate);

				TableSchema.TableColumn colvarSeasonIsCurrent = new TableSchema.TableColumn(schema);
				colvarSeasonIsCurrent.ColumnName = "SeasonIsCurrent";
				colvarSeasonIsCurrent.DataType = DbType.Boolean;
				colvarSeasonIsCurrent.MaxLength = 0;
				colvarSeasonIsCurrent.AutoIncrement = false;
				colvarSeasonIsCurrent.IsNullable = false;
				colvarSeasonIsCurrent.IsPrimaryKey = false;
				colvarSeasonIsCurrent.IsForeignKey = false;
				colvarSeasonIsCurrent.IsReadOnly = false;
				colvarSeasonIsCurrent.DefaultSetting = @"";
				colvarSeasonIsCurrent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsCurrent);

				TableSchema.TableColumn colvarSeasonIsActive = new TableSchema.TableColumn(schema);
				colvarSeasonIsActive.ColumnName = "SeasonIsActive";
				colvarSeasonIsActive.DataType = DbType.Boolean;
				colvarSeasonIsActive.MaxLength = 0;
				colvarSeasonIsActive.AutoIncrement = false;
				colvarSeasonIsActive.IsNullable = false;
				colvarSeasonIsActive.IsPrimaryKey = false;
				colvarSeasonIsActive.IsForeignKey = false;
				colvarSeasonIsActive.IsReadOnly = false;
				colvarSeasonIsActive.DefaultSetting = @"";
				colvarSeasonIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsActive);

				TableSchema.TableColumn colvarSeasonIsDeleted = new TableSchema.TableColumn(schema);
				colvarSeasonIsDeleted.ColumnName = "SeasonIsDeleted";
				colvarSeasonIsDeleted.DataType = DbType.Boolean;
				colvarSeasonIsDeleted.MaxLength = 0;
				colvarSeasonIsDeleted.AutoIncrement = false;
				colvarSeasonIsDeleted.IsNullable = false;
				colvarSeasonIsDeleted.IsPrimaryKey = false;
				colvarSeasonIsDeleted.IsForeignKey = false;
				colvarSeasonIsDeleted.IsReadOnly = false;
				colvarSeasonIsDeleted.DefaultSetting = @"";
				colvarSeasonIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsDeleted);

				TableSchema.TableColumn colvarSeasonName = new TableSchema.TableColumn(schema);
				colvarSeasonName.ColumnName = "SeasonName";
				colvarSeasonName.DataType = DbType.String;
				colvarSeasonName.MaxLength = 50;
				colvarSeasonName.AutoIncrement = false;
				colvarSeasonName.IsNullable = false;
				colvarSeasonName.IsPrimaryKey = false;
				colvarSeasonName.IsForeignKey = false;
				colvarSeasonName.IsReadOnly = false;
				colvarSeasonName.DefaultSetting = @"";
				colvarSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonName);

				TableSchema.TableColumn colvarActualTeamID = new TableSchema.TableColumn(schema);
				colvarActualTeamID.ColumnName = "ActualTeamID";
				colvarActualTeamID.DataType = DbType.Int32;
				colvarActualTeamID.MaxLength = 0;
				colvarActualTeamID.AutoIncrement = false;
				colvarActualTeamID.IsNullable = true;
				colvarActualTeamID.IsPrimaryKey = false;
				colvarActualTeamID.IsForeignKey = false;
				colvarActualTeamID.IsReadOnly = false;
				colvarActualTeamID.DefaultSetting = @"";
				colvarActualTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActualTeamID);

				TableSchema.TableColumn colvarActualTeamLocationID = new TableSchema.TableColumn(schema);
				colvarActualTeamLocationID.ColumnName = "ActualTeamLocationID";
				colvarActualTeamLocationID.DataType = DbType.Int32;
				colvarActualTeamLocationID.MaxLength = 0;
				colvarActualTeamLocationID.AutoIncrement = false;
				colvarActualTeamLocationID.IsNullable = true;
				colvarActualTeamLocationID.IsPrimaryKey = false;
				colvarActualTeamLocationID.IsForeignKey = false;
				colvarActualTeamLocationID.IsReadOnly = false;
				colvarActualTeamLocationID.DefaultSetting = @"";
				colvarActualTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActualTeamLocationID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_RecruitUser",schema);
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
		public RecruitUserView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public int? RecruitedByID {
			get { return GetColumnValue<int?>(Columns.RecruitedByID); }
			set { SetColumnValue(Columns.RecruitedByID, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		[DataMember]
		public string MiddleName {
			get { return GetColumnValue<string>(Columns.MiddleName); }
			set { SetColumnValue(Columns.MiddleName, value); }
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		[DataMember]
		public string PreferredName {
			get { return GetColumnValue<string>(Columns.PreferredName); }
			set { SetColumnValue(Columns.PreferredName, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string PublicFullName {
			get { return GetColumnValue<string>(Columns.PublicFullName); }
			set { SetColumnValue(Columns.PublicFullName, value); }
		}
		[DataMember]
		public string UserName {
			get { return GetColumnValue<string>(Columns.UserName); }
			set { SetColumnValue(Columns.UserName, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public string PhoneCell {
			get { return GetColumnValue<string>(Columns.PhoneCell); }
			set { SetColumnValue(Columns.PhoneCell, value); }
		}
		[DataMember]
		public short? PhoneCellCarrierID {
			get { return GetColumnValue<short?>(Columns.PhoneCellCarrierID); }
			set { SetColumnValue(Columns.PhoneCellCarrierID, value); }
		}
		[DataMember]
		public bool IsActiveUser {
			get { return GetColumnValue<bool>(Columns.IsActiveUser); }
			set { SetColumnValue(Columns.IsActiveUser, value); }
		}
		[DataMember]
		public bool IsDeletedUser {
			get { return GetColumnValue<bool>(Columns.IsDeletedUser); }
			set { SetColumnValue(Columns.IsDeletedUser, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? ReportsToID {
			get { return GetColumnValue<int?>(Columns.ReportsToID); }
			set { SetColumnValue(Columns.ReportsToID, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int SocialSecCardStatusID {
			get { return GetColumnValue<int>(Columns.SocialSecCardStatusID); }
			set { SetColumnValue(Columns.SocialSecCardStatusID, value); }
		}
		[DataMember]
		public int DriversLicenseStatusID {
			get { return GetColumnValue<int>(Columns.DriversLicenseStatusID); }
			set { SetColumnValue(Columns.DriversLicenseStatusID, value); }
		}
		[DataMember]
		public int W9StatusID {
			get { return GetColumnValue<int>(Columns.W9StatusID); }
			set { SetColumnValue(Columns.W9StatusID, value); }
		}
		[DataMember]
		public int I9StatusID {
			get { return GetColumnValue<int>(Columns.I9StatusID); }
			set { SetColumnValue(Columns.I9StatusID, value); }
		}
		[DataMember]
		public int W4StatusID {
			get { return GetColumnValue<int>(Columns.W4StatusID); }
			set { SetColumnValue(Columns.W4StatusID, value); }
		}
		[DataMember]
		public string SocialSecCardNotes {
			get { return GetColumnValue<string>(Columns.SocialSecCardNotes); }
			set { SetColumnValue(Columns.SocialSecCardNotes, value); }
		}
		[DataMember]
		public string DriversLicenseNotes {
			get { return GetColumnValue<string>(Columns.DriversLicenseNotes); }
			set { SetColumnValue(Columns.DriversLicenseNotes, value); }
		}
		[DataMember]
		public string W9Notes {
			get { return GetColumnValue<string>(Columns.W9Notes); }
			set { SetColumnValue(Columns.W9Notes, value); }
		}
		[DataMember]
		public string I9Notes {
			get { return GetColumnValue<string>(Columns.I9Notes); }
			set { SetColumnValue(Columns.I9Notes, value); }
		}
		[DataMember]
		public string W4Notes {
			get { return GetColumnValue<string>(Columns.W4Notes); }
			set { SetColumnValue(Columns.W4Notes, value); }
		}
		[DataMember]
		public bool IsActiveRecruit {
			get { return GetColumnValue<bool>(Columns.IsActiveRecruit); }
			set { SetColumnValue(Columns.IsActiveRecruit, value); }
		}
		[DataMember]
		public bool IsDeletedRecruit {
			get { return GetColumnValue<bool>(Columns.IsDeletedRecruit); }
			set { SetColumnValue(Columns.IsDeletedRecruit, value); }
		}
		[DataMember]
		public short UserTypeID {
			get { return GetColumnValue<short>(Columns.UserTypeID); }
			set { SetColumnValue(Columns.UserTypeID, value); }
		}
		[DataMember]
		public string UserType {
			get { return GetColumnValue<string>(Columns.UserType); }
			set { SetColumnValue(Columns.UserType, value); }
		}
		[DataMember]
		public int RoleLocationID {
			get { return GetColumnValue<int>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public byte SecurityLevel {
			get { return GetColumnValue<byte>(Columns.SecurityLevel); }
			set { SetColumnValue(Columns.SecurityLevel, value); }
		}
		[DataMember]
		public int ReportingLevel {
			get { return GetColumnValue<int>(Columns.ReportingLevel); }
			set { SetColumnValue(Columns.ReportingLevel, value); }
		}
		[DataMember]
		public int UserTypeTeamTypeID {
			get { return GetColumnValue<int>(Columns.UserTypeTeamTypeID); }
			set { SetColumnValue(Columns.UserTypeTeamTypeID, value); }
		}
		[DataMember]
		public string UserTypeTeamType {
			get { return GetColumnValue<string>(Columns.UserTypeTeamType); }
			set { SetColumnValue(Columns.UserTypeTeamType, value); }
		}
		[DataMember]
		public int PayscaleID {
			get { return GetColumnValue<int>(Columns.PayscaleID); }
			set { SetColumnValue(Columns.PayscaleID, value); }
		}
		[DataMember]
		public string PayScaleName {
			get { return GetColumnValue<string>(Columns.PayScaleName); }
			set { SetColumnValue(Columns.PayScaleName, value); }
		}
		[DataMember]
		public int? AlternatePayScheduleID {
			get { return GetColumnValue<int?>(Columns.AlternatePayScheduleID); }
			set { SetColumnValue(Columns.AlternatePayScheduleID, value); }
		}
		[DataMember]
		public bool? IsActive {
			get { return GetColumnValue<bool?>(Columns.IsActive); }
			set { SetColumnValue(Columns.IsActive, value); }
		}
		[DataMember]
		public bool? IsDeleted {
			get { return GetColumnValue<bool?>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
		}
		[DataMember]
		public bool? IsServiceTech {
			get { return GetColumnValue<bool?>(Columns.IsServiceTech); }
			set { SetColumnValue(Columns.IsServiceTech, value); }
		}
		[DataMember]
		public bool SeasonIsVisibleToRecruits {
			get { return GetColumnValue<bool>(Columns.SeasonIsVisibleToRecruits); }
			set { SetColumnValue(Columns.SeasonIsVisibleToRecruits, value); }
		}
		[DataMember]
		public DateTime? SeasonStartDate {
			get { return GetColumnValue<DateTime?>(Columns.SeasonStartDate); }
			set { SetColumnValue(Columns.SeasonStartDate, value); }
		}
		[DataMember]
		public DateTime? SeasonEndDate {
			get { return GetColumnValue<DateTime?>(Columns.SeasonEndDate); }
			set { SetColumnValue(Columns.SeasonEndDate, value); }
		}
		[DataMember]
		public bool SeasonIsCurrent {
			get { return GetColumnValue<bool>(Columns.SeasonIsCurrent); }
			set { SetColumnValue(Columns.SeasonIsCurrent, value); }
		}
		[DataMember]
		public bool SeasonIsActive {
			get { return GetColumnValue<bool>(Columns.SeasonIsActive); }
			set { SetColumnValue(Columns.SeasonIsActive, value); }
		}
		[DataMember]
		public bool SeasonIsDeleted {
			get { return GetColumnValue<bool>(Columns.SeasonIsDeleted); }
			set { SetColumnValue(Columns.SeasonIsDeleted, value); }
		}
		[DataMember]
		public string SeasonName {
			get { return GetColumnValue<string>(Columns.SeasonName); }
			set { SetColumnValue(Columns.SeasonName, value); }
		}
		[DataMember]
		public int? ActualTeamID {
			get { return GetColumnValue<int?>(Columns.ActualTeamID); }
			set { SetColumnValue(Columns.ActualTeamID, value); }
		}
		[DataMember]
		public int? ActualTeamLocationID {
			get { return GetColumnValue<int?>(Columns.ActualTeamLocationID); }
			set { SetColumnValue(Columns.ActualTeamLocationID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return UserID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitedByIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PreferredNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn PublicFullNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn UserNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PhoneCellColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PhoneCellCarrierIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn IsActiveUserColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsDeletedUserColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn ReportsToIDColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn SocialSecCardStatusIDColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn DriversLicenseStatusIDColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn W9StatusIDColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn I9StatusIDColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn W4StatusIDColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn SocialSecCardNotesColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn DriversLicenseNotesColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn W9NotesColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn I9NotesColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn W4NotesColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn IsActiveRecruitColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn IsDeletedRecruitColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn UserTypeIDColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn UserTypeColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn SecurityLevelColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn ReportingLevelColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn UserTypeTeamTypeIDColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn UserTypeTeamTypeColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn PayscaleIDColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn PayScaleNameColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn AlternatePayScheduleIDColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn IsServiceTechColumn
		{
			get { return Schema.Columns[43]; }
		}
		public static TableSchema.TableColumn SeasonIsVisibleToRecruitsColumn
		{
			get { return Schema.Columns[44]; }
		}
		public static TableSchema.TableColumn SeasonStartDateColumn
		{
			get { return Schema.Columns[45]; }
		}
		public static TableSchema.TableColumn SeasonEndDateColumn
		{
			get { return Schema.Columns[46]; }
		}
		public static TableSchema.TableColumn SeasonIsCurrentColumn
		{
			get { return Schema.Columns[47]; }
		}
		public static TableSchema.TableColumn SeasonIsActiveColumn
		{
			get { return Schema.Columns[48]; }
		}
		public static TableSchema.TableColumn SeasonIsDeletedColumn
		{
			get { return Schema.Columns[49]; }
		}
		public static TableSchema.TableColumn SeasonNameColumn
		{
			get { return Schema.Columns[50]; }
		}
		public static TableSchema.TableColumn ActualTeamIDColumn
		{
			get { return Schema.Columns[51]; }
		}
		public static TableSchema.TableColumn ActualTeamLocationIDColumn
		{
			get { return Schema.Columns[52]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string RecruitedByID = @"RecruitedByID";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PreferredName = @"PreferredName";
			public const string FullName = @"FullName";
			public const string PublicFullName = @"PublicFullName";
			public const string UserName = @"UserName";
			public const string Email = @"Email";
			public const string PhoneCell = @"PhoneCell";
			public const string PhoneCellCarrierID = @"PhoneCellCarrierID";
			public const string IsActiveUser = @"IsActiveUser";
			public const string IsDeletedUser = @"IsDeletedUser";
			public const string RecruitID = @"RecruitID";
			public const string SeasonID = @"SeasonID";
			public const string ReportsToID = @"ReportsToID";
			public const string TeamID = @"TeamID";
			public const string SocialSecCardStatusID = @"SocialSecCardStatusID";
			public const string DriversLicenseStatusID = @"DriversLicenseStatusID";
			public const string W9StatusID = @"W9StatusID";
			public const string I9StatusID = @"I9StatusID";
			public const string W4StatusID = @"W4StatusID";
			public const string SocialSecCardNotes = @"SocialSecCardNotes";
			public const string DriversLicenseNotes = @"DriversLicenseNotes";
			public const string W9Notes = @"W9Notes";
			public const string I9Notes = @"I9Notes";
			public const string W4Notes = @"W4Notes";
			public const string IsActiveRecruit = @"IsActiveRecruit";
			public const string IsDeletedRecruit = @"IsDeletedRecruit";
			public const string UserTypeID = @"UserTypeID";
			public const string UserType = @"UserType";
			public const string RoleLocationID = @"RoleLocationID";
			public const string SecurityLevel = @"SecurityLevel";
			public const string ReportingLevel = @"ReportingLevel";
			public const string UserTypeTeamTypeID = @"UserTypeTeamTypeID";
			public const string UserTypeTeamType = @"UserTypeTeamType";
			public const string PayscaleID = @"PayscaleID";
			public const string PayScaleName = @"PayScaleName";
			public const string AlternatePayScheduleID = @"AlternatePayScheduleID";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
			public const string IsServiceTech = @"IsServiceTech";
			public const string SeasonIsVisibleToRecruits = @"SeasonIsVisibleToRecruits";
			public const string SeasonStartDate = @"SeasonStartDate";
			public const string SeasonEndDate = @"SeasonEndDate";
			public const string SeasonIsCurrent = @"SeasonIsCurrent";
			public const string SeasonIsActive = @"SeasonIsActive";
			public const string SeasonIsDeleted = @"SeasonIsDeleted";
			public const string SeasonName = @"SeasonName";
			public const string ActualTeamID = @"ActualTeamID";
			public const string ActualTeamLocationID = @"ActualTeamLocationID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the TeamsView class.
	/// </summary>
	[DataContract]
	public partial class TeamsViewCollection : ReadOnlyList<TeamsView, TeamsViewCollection>
	{
		public static TeamsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			TeamsViewCollection result = new TeamsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_Teams view.
	/// </summary>
	[DataContract]
	public partial class TeamsView : ReadOnlyRecord<TeamsView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_Teams", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = false;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamName = new TableSchema.TableColumn(schema);
				colvarTeamName.ColumnName = "TeamName";
				colvarTeamName.DataType = DbType.AnsiString;
				colvarTeamName.MaxLength = 50;
				colvarTeamName.AutoIncrement = false;
				colvarTeamName.IsNullable = false;
				colvarTeamName.IsPrimaryKey = false;
				colvarTeamName.IsForeignKey = false;
				colvarTeamName.IsReadOnly = false;
				colvarTeamName.DefaultSetting = @"";
				colvarTeamName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamName);

				TableSchema.TableColumn colvarCreatedFromTeamID = new TableSchema.TableColumn(schema);
				colvarCreatedFromTeamID.ColumnName = "CreatedFromTeamID";
				colvarCreatedFromTeamID.DataType = DbType.Int32;
				colvarCreatedFromTeamID.MaxLength = 0;
				colvarCreatedFromTeamID.AutoIncrement = false;
				colvarCreatedFromTeamID.IsNullable = true;
				colvarCreatedFromTeamID.IsPrimaryKey = false;
				colvarCreatedFromTeamID.IsForeignKey = false;
				colvarCreatedFromTeamID.IsReadOnly = false;
				colvarCreatedFromTeamID.DefaultSetting = @"";
				colvarCreatedFromTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedFromTeamID);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = true;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarRegionalManagerRecruitID = new TableSchema.TableColumn(schema);
				colvarRegionalManagerRecruitID.ColumnName = "RegionalManagerRecruitID";
				colvarRegionalManagerRecruitID.DataType = DbType.Int32;
				colvarRegionalManagerRecruitID.MaxLength = 0;
				colvarRegionalManagerRecruitID.AutoIncrement = false;
				colvarRegionalManagerRecruitID.IsNullable = true;
				colvarRegionalManagerRecruitID.IsPrimaryKey = false;
				colvarRegionalManagerRecruitID.IsForeignKey = false;
				colvarRegionalManagerRecruitID.IsReadOnly = false;
				colvarRegionalManagerRecruitID.DefaultSetting = @"";
				colvarRegionalManagerRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionalManagerRecruitID);

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

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = false;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarCreatedFromTeamLocationID = new TableSchema.TableColumn(schema);
				colvarCreatedFromTeamLocationID.ColumnName = "CreatedFromTeamLocationID";
				colvarCreatedFromTeamLocationID.DataType = DbType.Int32;
				colvarCreatedFromTeamLocationID.MaxLength = 0;
				colvarCreatedFromTeamLocationID.AutoIncrement = false;
				colvarCreatedFromTeamLocationID.IsNullable = true;
				colvarCreatedFromTeamLocationID.IsPrimaryKey = false;
				colvarCreatedFromTeamLocationID.IsForeignKey = false;
				colvarCreatedFromTeamLocationID.IsReadOnly = false;
				colvarCreatedFromTeamLocationID.DefaultSetting = @"";
				colvarCreatedFromTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedFromTeamLocationID);

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
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = false;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarSeasonName = new TableSchema.TableColumn(schema);
				colvarSeasonName.ColumnName = "SeasonName";
				colvarSeasonName.DataType = DbType.String;
				colvarSeasonName.MaxLength = 50;
				colvarSeasonName.AutoIncrement = false;
				colvarSeasonName.IsNullable = false;
				colvarSeasonName.IsPrimaryKey = false;
				colvarSeasonName.IsForeignKey = false;
				colvarSeasonName.IsReadOnly = false;
				colvarSeasonName.DefaultSetting = @"";
				colvarSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonName);

				TableSchema.TableColumn colvarStateID = new TableSchema.TableColumn(schema);
				colvarStateID.ColumnName = "StateID";
				colvarStateID.DataType = DbType.AnsiString;
				colvarStateID.MaxLength = 4;
				colvarStateID.AutoIncrement = false;
				colvarStateID.IsNullable = false;
				colvarStateID.IsPrimaryKey = false;
				colvarStateID.IsForeignKey = false;
				colvarStateID.IsReadOnly = false;
				colvarStateID.DefaultSetting = @"";
				colvarStateID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateID);

				TableSchema.TableColumn colvarStateName = new TableSchema.TableColumn(schema);
				colvarStateName.ColumnName = "StateName";
				colvarStateName.DataType = DbType.String;
				colvarStateName.MaxLength = 100;
				colvarStateName.AutoIncrement = false;
				colvarStateName.IsNullable = false;
				colvarStateName.IsPrimaryKey = false;
				colvarStateName.IsForeignKey = false;
				colvarStateName.IsReadOnly = false;
				colvarStateName.DefaultSetting = @"";
				colvarStateName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateName);

				TableSchema.TableColumn colvarStateAbbreviation = new TableSchema.TableColumn(schema);
				colvarStateAbbreviation.ColumnName = "StateAbbreviation";
				colvarStateAbbreviation.DataType = DbType.AnsiStringFixedLength;
				colvarStateAbbreviation.MaxLength = 2;
				colvarStateAbbreviation.AutoIncrement = false;
				colvarStateAbbreviation.IsNullable = false;
				colvarStateAbbreviation.IsPrimaryKey = false;
				colvarStateAbbreviation.IsForeignKey = false;
				colvarStateAbbreviation.IsReadOnly = false;
				colvarStateAbbreviation.DefaultSetting = @"";
				colvarStateAbbreviation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateAbbreviation);

				TableSchema.TableColumn colvarOfficeCreatedBy = new TableSchema.TableColumn(schema);
				colvarOfficeCreatedBy.ColumnName = "OfficeCreatedBy";
				colvarOfficeCreatedBy.DataType = DbType.String;
				colvarOfficeCreatedBy.MaxLength = 50;
				colvarOfficeCreatedBy.AutoIncrement = false;
				colvarOfficeCreatedBy.IsNullable = false;
				colvarOfficeCreatedBy.IsPrimaryKey = false;
				colvarOfficeCreatedBy.IsForeignKey = false;
				colvarOfficeCreatedBy.IsReadOnly = false;
				colvarOfficeCreatedBy.DefaultSetting = @"";
				colvarOfficeCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeCreatedBy);

				TableSchema.TableColumn colvarOfficeCreatedOn = new TableSchema.TableColumn(schema);
				colvarOfficeCreatedOn.ColumnName = "OfficeCreatedOn";
				colvarOfficeCreatedOn.DataType = DbType.DateTime;
				colvarOfficeCreatedOn.MaxLength = 0;
				colvarOfficeCreatedOn.AutoIncrement = false;
				colvarOfficeCreatedOn.IsNullable = false;
				colvarOfficeCreatedOn.IsPrimaryKey = false;
				colvarOfficeCreatedOn.IsForeignKey = false;
				colvarOfficeCreatedOn.IsReadOnly = false;
				colvarOfficeCreatedOn.DefaultSetting = @"";
				colvarOfficeCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeCreatedOn);

				TableSchema.TableColumn colvarOfficeModifiedBy = new TableSchema.TableColumn(schema);
				colvarOfficeModifiedBy.ColumnName = "OfficeModifiedBy";
				colvarOfficeModifiedBy.DataType = DbType.String;
				colvarOfficeModifiedBy.MaxLength = 50;
				colvarOfficeModifiedBy.AutoIncrement = false;
				colvarOfficeModifiedBy.IsNullable = false;
				colvarOfficeModifiedBy.IsPrimaryKey = false;
				colvarOfficeModifiedBy.IsForeignKey = false;
				colvarOfficeModifiedBy.IsReadOnly = false;
				colvarOfficeModifiedBy.DefaultSetting = @"";
				colvarOfficeModifiedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeModifiedBy);

				TableSchema.TableColumn colvarOfficeModifiedOn = new TableSchema.TableColumn(schema);
				colvarOfficeModifiedOn.ColumnName = "OfficeModifiedOn";
				colvarOfficeModifiedOn.DataType = DbType.DateTime;
				colvarOfficeModifiedOn.MaxLength = 0;
				colvarOfficeModifiedOn.AutoIncrement = false;
				colvarOfficeModifiedOn.IsNullable = false;
				colvarOfficeModifiedOn.IsPrimaryKey = false;
				colvarOfficeModifiedOn.IsForeignKey = false;
				colvarOfficeModifiedOn.IsReadOnly = false;
				colvarOfficeModifiedOn.DefaultSetting = @"";
				colvarOfficeModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeModifiedOn);

				TableSchema.TableColumn colvarTeamType = new TableSchema.TableColumn(schema);
				colvarTeamType.ColumnName = "TeamType";
				colvarTeamType.DataType = DbType.String;
				colvarTeamType.MaxLength = 50;
				colvarTeamType.AutoIncrement = false;
				colvarTeamType.IsNullable = false;
				colvarTeamType.IsPrimaryKey = false;
				colvarTeamType.IsForeignKey = false;
				colvarTeamType.IsReadOnly = false;
				colvarTeamType.DefaultSetting = @"";
				colvarTeamType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamType);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_Teams",schema);
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
		public TeamsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int TeamID {
			get { return GetColumnValue<int>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public string TeamName {
			get { return GetColumnValue<string>(Columns.TeamName); }
			set { SetColumnValue(Columns.TeamName, value); }
		}
		[DataMember]
		public int? CreatedFromTeamID {
			get { return GetColumnValue<int?>(Columns.CreatedFromTeamID); }
			set { SetColumnValue(Columns.CreatedFromTeamID, value); }
		}
		[DataMember]
		public int? RoleLocationID {
			get { return GetColumnValue<int?>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public int? RegionalManagerRecruitID {
			get { return GetColumnValue<int?>(Columns.RegionalManagerRecruitID); }
			set { SetColumnValue(Columns.RegionalManagerRecruitID, value); }
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
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
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
		public int TeamLocationID {
			get { return GetColumnValue<int>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int? CreatedFromTeamLocationID {
			get { return GetColumnValue<int?>(Columns.CreatedFromTeamLocationID); }
			set { SetColumnValue(Columns.CreatedFromTeamLocationID, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}
		[DataMember]
		public string City {
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public string SeasonName {
			get { return GetColumnValue<string>(Columns.SeasonName); }
			set { SetColumnValue(Columns.SeasonName, value); }
		}
		[DataMember]
		public string StateID {
			get { return GetColumnValue<string>(Columns.StateID); }
			set { SetColumnValue(Columns.StateID, value); }
		}
		[DataMember]
		public string StateName {
			get { return GetColumnValue<string>(Columns.StateName); }
			set { SetColumnValue(Columns.StateName, value); }
		}
		[DataMember]
		public string StateAbbreviation {
			get { return GetColumnValue<string>(Columns.StateAbbreviation); }
			set { SetColumnValue(Columns.StateAbbreviation, value); }
		}
		[DataMember]
		public string OfficeCreatedBy {
			get { return GetColumnValue<string>(Columns.OfficeCreatedBy); }
			set { SetColumnValue(Columns.OfficeCreatedBy, value); }
		}
		[DataMember]
		public DateTime OfficeCreatedOn {
			get { return GetColumnValue<DateTime>(Columns.OfficeCreatedOn); }
			set { SetColumnValue(Columns.OfficeCreatedOn, value); }
		}
		[DataMember]
		public string OfficeModifiedBy {
			get { return GetColumnValue<string>(Columns.OfficeModifiedBy); }
			set { SetColumnValue(Columns.OfficeModifiedBy, value); }
		}
		[DataMember]
		public DateTime OfficeModifiedOn {
			get { return GetColumnValue<DateTime>(Columns.OfficeModifiedOn); }
			set { SetColumnValue(Columns.OfficeModifiedOn, value); }
		}
		[DataMember]
		public string TeamType {
			get { return GetColumnValue<string>(Columns.TeamType); }
			set { SetColumnValue(Columns.TeamType, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TeamName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CreatedFromTeamIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RegionalManagerRecruitIDColumn
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
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CreatedFromTeamLocationIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn SeasonNameColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn StateIDColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn StateNameColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn StateAbbreviationColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn OfficeCreatedByColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn OfficeCreatedOnColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn OfficeModifiedByColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn OfficeModifiedOnColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn TeamTypeColumn
		{
			get { return Schema.Columns[24]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TeamID = @"TeamID";
			public const string TeamName = @"TeamName";
			public const string CreatedFromTeamID = @"CreatedFromTeamID";
			public const string RoleLocationID = @"RoleLocationID";
			public const string RegionalManagerRecruitID = @"RegionalManagerRecruitID";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
			public const string CreatedBy = @"CreatedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string ModifiedBy = @"ModifiedBy";
			public const string ModifiedOn = @"ModifiedOn";
			public const string TeamLocationID = @"TeamLocationID";
			public const string CreatedFromTeamLocationID = @"CreatedFromTeamLocationID";
			public const string OfficeName = @"OfficeName";
			public const string City = @"City";
			public const string SeasonID = @"SeasonID";
			public const string SeasonName = @"SeasonName";
			public const string StateID = @"StateID";
			public const string StateName = @"StateName";
			public const string StateAbbreviation = @"StateAbbreviation";
			public const string OfficeCreatedBy = @"OfficeCreatedBy";
			public const string OfficeCreatedOn = @"OfficeCreatedOn";
			public const string OfficeModifiedBy = @"OfficeModifiedBy";
			public const string OfficeModifiedOn = @"OfficeModifiedOn";
			public const string TeamType = @"TeamType";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the TeamSalesManagersView class.
	/// </summary>
	[DataContract]
	public partial class TeamSalesManagersViewCollection : ReadOnlyList<TeamSalesManagersView, TeamSalesManagersViewCollection>
	{
		public static TeamSalesManagersViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			TeamSalesManagersViewCollection result = new TeamSalesManagersViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the VW_TeamSalesManagers view.
	/// </summary>
	[DataContract]
	public partial class TeamSalesManagersView : ReadOnlyRecord<TeamSalesManagersView>
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
				TableSchema.Table schema = new TableSchema.Table("VW_TeamSalesManagers", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = false;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarUserTypeId = new TableSchema.TableColumn(schema);
				colvarUserTypeId.ColumnName = "UserTypeId";
				colvarUserTypeId.DataType = DbType.Int16;
				colvarUserTypeId.MaxLength = 0;
				colvarUserTypeId.AutoIncrement = false;
				colvarUserTypeId.IsNullable = false;
				colvarUserTypeId.IsPrimaryKey = false;
				colvarUserTypeId.IsForeignKey = false;
				colvarUserTypeId.IsReadOnly = false;
				colvarUserTypeId.DefaultSetting = @"";
				colvarUserTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeId);

				TableSchema.TableColumn colvarReportsToID = new TableSchema.TableColumn(schema);
				colvarReportsToID.ColumnName = "ReportsToID";
				colvarReportsToID.DataType = DbType.Int32;
				colvarReportsToID.MaxLength = 0;
				colvarReportsToID.AutoIncrement = false;
				colvarReportsToID.IsNullable = true;
				colvarReportsToID.IsPrimaryKey = false;
				colvarReportsToID.IsForeignKey = false;
				colvarReportsToID.IsReadOnly = false;
				colvarReportsToID.DefaultSetting = @"";
				colvarReportsToID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportsToID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarPayScaleID = new TableSchema.TableColumn(schema);
				colvarPayScaleID.ColumnName = "PayScaleID";
				colvarPayScaleID.DataType = DbType.Int32;
				colvarPayScaleID.MaxLength = 0;
				colvarPayScaleID.AutoIncrement = false;
				colvarPayScaleID.IsNullable = true;
				colvarPayScaleID.IsPrimaryKey = false;
				colvarPayScaleID.IsForeignKey = false;
				colvarPayScaleID.IsReadOnly = false;
				colvarPayScaleID.DefaultSetting = @"";
				colvarPayScaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScaleID);

				TableSchema.TableColumn colvarIsRecruiter = new TableSchema.TableColumn(schema);
				colvarIsRecruiter.ColumnName = "IsRecruiter";
				colvarIsRecruiter.DataType = DbType.Boolean;
				colvarIsRecruiter.MaxLength = 0;
				colvarIsRecruiter.AutoIncrement = false;
				colvarIsRecruiter.IsNullable = false;
				colvarIsRecruiter.IsPrimaryKey = false;
				colvarIsRecruiter.IsForeignKey = false;
				colvarIsRecruiter.IsReadOnly = false;
				colvarIsRecruiter.DefaultSetting = @"";
				colvarIsRecruiter.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsRecruiter);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("VW_TeamSalesManagers",schema);
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
		public TeamSalesManagersView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int TeamLocationID {
			get { return GetColumnValue<int>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public short UserTypeId {
			get { return GetColumnValue<short>(Columns.UserTypeId); }
			set { SetColumnValue(Columns.UserTypeId, value); }
		}
		[DataMember]
		public int? ReportsToID {
			get { return GetColumnValue<int?>(Columns.ReportsToID); }
			set { SetColumnValue(Columns.ReportsToID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? PayScaleID {
			get { return GetColumnValue<int?>(Columns.PayScaleID); }
			set { SetColumnValue(Columns.PayScaleID, value); }
		}
		[DataMember]
		public bool IsRecruiter {
			get { return GetColumnValue<bool>(Columns.IsRecruiter); }
			set { SetColumnValue(Columns.IsRecruiter, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TeamID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn UserTypeIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ReportsToIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn PayScaleIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn IsRecruiterColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[9]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TeamID = @"TeamID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string RecruitID = @"RecruitID";
			public const string UserID = @"UserID";
			public const string UserTypeId = @"UserTypeId";
			public const string ReportsToID = @"ReportsToID";
			public const string SeasonID = @"SeasonID";
			public const string PayScaleID = @"PayScaleID";
			public const string IsRecruiter = @"IsRecruiter";
			public const string GPEmployeeID = @"GPEmployeeID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_RecruitUserView class.
	/// </summary>
	[DataContract]
	public partial class RU_RecruitUserViewCollection : ReadOnlyList<RU_RecruitUserView, RU_RecruitUserViewCollection>
	{
		public static RU_RecruitUserViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_RecruitUserViewCollection result = new RU_RecruitUserViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_RecruitUser view.
	/// </summary>
	[DataContract]
	public partial class RU_RecruitUserView : ReadOnlyRecord<RU_RecruitUserView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_RecruitUser", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarRecruitedByID = new TableSchema.TableColumn(schema);
				colvarRecruitedByID.ColumnName = "RecruitedByID";
				colvarRecruitedByID.DataType = DbType.Int32;
				colvarRecruitedByID.MaxLength = 0;
				colvarRecruitedByID.AutoIncrement = false;
				colvarRecruitedByID.IsNullable = true;
				colvarRecruitedByID.IsPrimaryKey = false;
				colvarRecruitedByID.IsForeignKey = false;
				colvarRecruitedByID.IsReadOnly = false;
				colvarRecruitedByID.DefaultSetting = @"";
				colvarRecruitedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitedByID);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = true;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarMiddleName = new TableSchema.TableColumn(schema);
				colvarMiddleName.ColumnName = "MiddleName";
				colvarMiddleName.DataType = DbType.String;
				colvarMiddleName.MaxLength = 50;
				colvarMiddleName.AutoIncrement = false;
				colvarMiddleName.IsNullable = true;
				colvarMiddleName.IsPrimaryKey = false;
				colvarMiddleName.IsForeignKey = false;
				colvarMiddleName.IsReadOnly = false;
				colvarMiddleName.DefaultSetting = @"";
				colvarMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddleName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = true;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarPreferredName = new TableSchema.TableColumn(schema);
				colvarPreferredName.ColumnName = "PreferredName";
				colvarPreferredName.DataType = DbType.String;
				colvarPreferredName.MaxLength = 50;
				colvarPreferredName.AutoIncrement = false;
				colvarPreferredName.IsNullable = true;
				colvarPreferredName.IsPrimaryKey = false;
				colvarPreferredName.IsForeignKey = false;
				colvarPreferredName.IsReadOnly = false;
				colvarPreferredName.DefaultSetting = @"";
				colvarPreferredName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreferredName);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarPublicFullName = new TableSchema.TableColumn(schema);
				colvarPublicFullName.ColumnName = "PublicFullName";
				colvarPublicFullName.DataType = DbType.String;
				colvarPublicFullName.MaxLength = 53;
				colvarPublicFullName.AutoIncrement = false;
				colvarPublicFullName.IsNullable = true;
				colvarPublicFullName.IsPrimaryKey = false;
				colvarPublicFullName.IsForeignKey = false;
				colvarPublicFullName.IsReadOnly = false;
				colvarPublicFullName.DefaultSetting = @"";
				colvarPublicFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPublicFullName);

				TableSchema.TableColumn colvarUserName = new TableSchema.TableColumn(schema);
				colvarUserName.ColumnName = "UserName";
				colvarUserName.DataType = DbType.String;
				colvarUserName.MaxLength = 50;
				colvarUserName.AutoIncrement = false;
				colvarUserName.IsNullable = false;
				colvarUserName.IsPrimaryKey = false;
				colvarUserName.IsForeignKey = false;
				colvarUserName.IsReadOnly = false;
				colvarUserName.DefaultSetting = @"";
				colvarUserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserName);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 100;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarPhoneCell = new TableSchema.TableColumn(schema);
				colvarPhoneCell.ColumnName = "PhoneCell";
				colvarPhoneCell.DataType = DbType.String;
				colvarPhoneCell.MaxLength = 50;
				colvarPhoneCell.AutoIncrement = false;
				colvarPhoneCell.IsNullable = true;
				colvarPhoneCell.IsPrimaryKey = false;
				colvarPhoneCell.IsForeignKey = false;
				colvarPhoneCell.IsReadOnly = false;
				colvarPhoneCell.DefaultSetting = @"";
				colvarPhoneCell.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCell);

				TableSchema.TableColumn colvarPhoneCellCarrierID = new TableSchema.TableColumn(schema);
				colvarPhoneCellCarrierID.ColumnName = "PhoneCellCarrierID";
				colvarPhoneCellCarrierID.DataType = DbType.Int16;
				colvarPhoneCellCarrierID.MaxLength = 0;
				colvarPhoneCellCarrierID.AutoIncrement = false;
				colvarPhoneCellCarrierID.IsNullable = true;
				colvarPhoneCellCarrierID.IsPrimaryKey = false;
				colvarPhoneCellCarrierID.IsForeignKey = false;
				colvarPhoneCellCarrierID.IsReadOnly = false;
				colvarPhoneCellCarrierID.DefaultSetting = @"";
				colvarPhoneCellCarrierID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCellCarrierID);

				TableSchema.TableColumn colvarIsActiveUser = new TableSchema.TableColumn(schema);
				colvarIsActiveUser.ColumnName = "IsActiveUser";
				colvarIsActiveUser.DataType = DbType.Boolean;
				colvarIsActiveUser.MaxLength = 0;
				colvarIsActiveUser.AutoIncrement = false;
				colvarIsActiveUser.IsNullable = false;
				colvarIsActiveUser.IsPrimaryKey = false;
				colvarIsActiveUser.IsForeignKey = false;
				colvarIsActiveUser.IsReadOnly = false;
				colvarIsActiveUser.DefaultSetting = @"";
				colvarIsActiveUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveUser);

				TableSchema.TableColumn colvarIsDeletedUser = new TableSchema.TableColumn(schema);
				colvarIsDeletedUser.ColumnName = "IsDeletedUser";
				colvarIsDeletedUser.DataType = DbType.Boolean;
				colvarIsDeletedUser.MaxLength = 0;
				colvarIsDeletedUser.AutoIncrement = false;
				colvarIsDeletedUser.IsNullable = false;
				colvarIsDeletedUser.IsPrimaryKey = false;
				colvarIsDeletedUser.IsForeignKey = false;
				colvarIsDeletedUser.IsReadOnly = false;
				colvarIsDeletedUser.DefaultSetting = @"";
				colvarIsDeletedUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedUser);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarReportsToID = new TableSchema.TableColumn(schema);
				colvarReportsToID.ColumnName = "ReportsToID";
				colvarReportsToID.DataType = DbType.Int32;
				colvarReportsToID.MaxLength = 0;
				colvarReportsToID.AutoIncrement = false;
				colvarReportsToID.IsNullable = true;
				colvarReportsToID.IsPrimaryKey = false;
				colvarReportsToID.IsForeignKey = false;
				colvarReportsToID.IsReadOnly = false;
				colvarReportsToID.DefaultSetting = @"";
				colvarReportsToID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportsToID);

				TableSchema.TableColumn colvarTeamID = new TableSchema.TableColumn(schema);
				colvarTeamID.ColumnName = "TeamID";
				colvarTeamID.DataType = DbType.Int32;
				colvarTeamID.MaxLength = 0;
				colvarTeamID.AutoIncrement = false;
				colvarTeamID.IsNullable = true;
				colvarTeamID.IsPrimaryKey = false;
				colvarTeamID.IsForeignKey = false;
				colvarTeamID.IsReadOnly = false;
				colvarTeamID.DefaultSetting = @"";
				colvarTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamID);

				TableSchema.TableColumn colvarSocialSecCardStatusID = new TableSchema.TableColumn(schema);
				colvarSocialSecCardStatusID.ColumnName = "SocialSecCardStatusID";
				colvarSocialSecCardStatusID.DataType = DbType.Int32;
				colvarSocialSecCardStatusID.MaxLength = 0;
				colvarSocialSecCardStatusID.AutoIncrement = false;
				colvarSocialSecCardStatusID.IsNullable = false;
				colvarSocialSecCardStatusID.IsPrimaryKey = false;
				colvarSocialSecCardStatusID.IsForeignKey = false;
				colvarSocialSecCardStatusID.IsReadOnly = false;
				colvarSocialSecCardStatusID.DefaultSetting = @"";
				colvarSocialSecCardStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSocialSecCardStatusID);

				TableSchema.TableColumn colvarDriversLicenseStatusID = new TableSchema.TableColumn(schema);
				colvarDriversLicenseStatusID.ColumnName = "DriversLicenseStatusID";
				colvarDriversLicenseStatusID.DataType = DbType.Int32;
				colvarDriversLicenseStatusID.MaxLength = 0;
				colvarDriversLicenseStatusID.AutoIncrement = false;
				colvarDriversLicenseStatusID.IsNullable = false;
				colvarDriversLicenseStatusID.IsPrimaryKey = false;
				colvarDriversLicenseStatusID.IsForeignKey = false;
				colvarDriversLicenseStatusID.IsReadOnly = false;
				colvarDriversLicenseStatusID.DefaultSetting = @"";
				colvarDriversLicenseStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDriversLicenseStatusID);

				TableSchema.TableColumn colvarW9StatusID = new TableSchema.TableColumn(schema);
				colvarW9StatusID.ColumnName = "W9StatusID";
				colvarW9StatusID.DataType = DbType.Int32;
				colvarW9StatusID.MaxLength = 0;
				colvarW9StatusID.AutoIncrement = false;
				colvarW9StatusID.IsNullable = false;
				colvarW9StatusID.IsPrimaryKey = false;
				colvarW9StatusID.IsForeignKey = false;
				colvarW9StatusID.IsReadOnly = false;
				colvarW9StatusID.DefaultSetting = @"";
				colvarW9StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW9StatusID);

				TableSchema.TableColumn colvarI9StatusID = new TableSchema.TableColumn(schema);
				colvarI9StatusID.ColumnName = "I9StatusID";
				colvarI9StatusID.DataType = DbType.Int32;
				colvarI9StatusID.MaxLength = 0;
				colvarI9StatusID.AutoIncrement = false;
				colvarI9StatusID.IsNullable = false;
				colvarI9StatusID.IsPrimaryKey = false;
				colvarI9StatusID.IsForeignKey = false;
				colvarI9StatusID.IsReadOnly = false;
				colvarI9StatusID.DefaultSetting = @"";
				colvarI9StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarI9StatusID);

				TableSchema.TableColumn colvarW4StatusID = new TableSchema.TableColumn(schema);
				colvarW4StatusID.ColumnName = "W4StatusID";
				colvarW4StatusID.DataType = DbType.Int32;
				colvarW4StatusID.MaxLength = 0;
				colvarW4StatusID.AutoIncrement = false;
				colvarW4StatusID.IsNullable = false;
				colvarW4StatusID.IsPrimaryKey = false;
				colvarW4StatusID.IsForeignKey = false;
				colvarW4StatusID.IsReadOnly = false;
				colvarW4StatusID.DefaultSetting = @"";
				colvarW4StatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW4StatusID);

				TableSchema.TableColumn colvarSocialSecCardNotes = new TableSchema.TableColumn(schema);
				colvarSocialSecCardNotes.ColumnName = "SocialSecCardNotes";
				colvarSocialSecCardNotes.DataType = DbType.String;
				colvarSocialSecCardNotes.MaxLength = 250;
				colvarSocialSecCardNotes.AutoIncrement = false;
				colvarSocialSecCardNotes.IsNullable = true;
				colvarSocialSecCardNotes.IsPrimaryKey = false;
				colvarSocialSecCardNotes.IsForeignKey = false;
				colvarSocialSecCardNotes.IsReadOnly = false;
				colvarSocialSecCardNotes.DefaultSetting = @"";
				colvarSocialSecCardNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSocialSecCardNotes);

				TableSchema.TableColumn colvarDriversLicenseNotes = new TableSchema.TableColumn(schema);
				colvarDriversLicenseNotes.ColumnName = "DriversLicenseNotes";
				colvarDriversLicenseNotes.DataType = DbType.String;
				colvarDriversLicenseNotes.MaxLength = 250;
				colvarDriversLicenseNotes.AutoIncrement = false;
				colvarDriversLicenseNotes.IsNullable = true;
				colvarDriversLicenseNotes.IsPrimaryKey = false;
				colvarDriversLicenseNotes.IsForeignKey = false;
				colvarDriversLicenseNotes.IsReadOnly = false;
				colvarDriversLicenseNotes.DefaultSetting = @"";
				colvarDriversLicenseNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDriversLicenseNotes);

				TableSchema.TableColumn colvarW9Notes = new TableSchema.TableColumn(schema);
				colvarW9Notes.ColumnName = "W9Notes";
				colvarW9Notes.DataType = DbType.String;
				colvarW9Notes.MaxLength = 250;
				colvarW9Notes.AutoIncrement = false;
				colvarW9Notes.IsNullable = true;
				colvarW9Notes.IsPrimaryKey = false;
				colvarW9Notes.IsForeignKey = false;
				colvarW9Notes.IsReadOnly = false;
				colvarW9Notes.DefaultSetting = @"";
				colvarW9Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW9Notes);

				TableSchema.TableColumn colvarI9Notes = new TableSchema.TableColumn(schema);
				colvarI9Notes.ColumnName = "I9Notes";
				colvarI9Notes.DataType = DbType.String;
				colvarI9Notes.MaxLength = 250;
				colvarI9Notes.AutoIncrement = false;
				colvarI9Notes.IsNullable = true;
				colvarI9Notes.IsPrimaryKey = false;
				colvarI9Notes.IsForeignKey = false;
				colvarI9Notes.IsReadOnly = false;
				colvarI9Notes.DefaultSetting = @"";
				colvarI9Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarI9Notes);

				TableSchema.TableColumn colvarW4Notes = new TableSchema.TableColumn(schema);
				colvarW4Notes.ColumnName = "W4Notes";
				colvarW4Notes.DataType = DbType.String;
				colvarW4Notes.MaxLength = 250;
				colvarW4Notes.AutoIncrement = false;
				colvarW4Notes.IsNullable = true;
				colvarW4Notes.IsPrimaryKey = false;
				colvarW4Notes.IsForeignKey = false;
				colvarW4Notes.IsReadOnly = false;
				colvarW4Notes.DefaultSetting = @"";
				colvarW4Notes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarW4Notes);

				TableSchema.TableColumn colvarIsActiveRecruit = new TableSchema.TableColumn(schema);
				colvarIsActiveRecruit.ColumnName = "IsActiveRecruit";
				colvarIsActiveRecruit.DataType = DbType.Boolean;
				colvarIsActiveRecruit.MaxLength = 0;
				colvarIsActiveRecruit.AutoIncrement = false;
				colvarIsActiveRecruit.IsNullable = false;
				colvarIsActiveRecruit.IsPrimaryKey = false;
				colvarIsActiveRecruit.IsForeignKey = false;
				colvarIsActiveRecruit.IsReadOnly = false;
				colvarIsActiveRecruit.DefaultSetting = @"";
				colvarIsActiveRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActiveRecruit);

				TableSchema.TableColumn colvarIsDeletedRecruit = new TableSchema.TableColumn(schema);
				colvarIsDeletedRecruit.ColumnName = "IsDeletedRecruit";
				colvarIsDeletedRecruit.DataType = DbType.Boolean;
				colvarIsDeletedRecruit.MaxLength = 0;
				colvarIsDeletedRecruit.AutoIncrement = false;
				colvarIsDeletedRecruit.IsNullable = false;
				colvarIsDeletedRecruit.IsPrimaryKey = false;
				colvarIsDeletedRecruit.IsForeignKey = false;
				colvarIsDeletedRecruit.IsReadOnly = false;
				colvarIsDeletedRecruit.DefaultSetting = @"";
				colvarIsDeletedRecruit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeletedRecruit);

				TableSchema.TableColumn colvarUserTypeID = new TableSchema.TableColumn(schema);
				colvarUserTypeID.ColumnName = "UserTypeID";
				colvarUserTypeID.DataType = DbType.Int16;
				colvarUserTypeID.MaxLength = 0;
				colvarUserTypeID.AutoIncrement = false;
				colvarUserTypeID.IsNullable = false;
				colvarUserTypeID.IsPrimaryKey = false;
				colvarUserTypeID.IsForeignKey = false;
				colvarUserTypeID.IsReadOnly = false;
				colvarUserTypeID.DefaultSetting = @"";
				colvarUserTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeID);

				TableSchema.TableColumn colvarUserType = new TableSchema.TableColumn(schema);
				colvarUserType.ColumnName = "UserType";
				colvarUserType.DataType = DbType.AnsiString;
				colvarUserType.MaxLength = 30;
				colvarUserType.AutoIncrement = false;
				colvarUserType.IsNullable = false;
				colvarUserType.IsPrimaryKey = false;
				colvarUserType.IsForeignKey = false;
				colvarUserType.IsReadOnly = false;
				colvarUserType.DefaultSetting = @"";
				colvarUserType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserType);

				TableSchema.TableColumn colvarRoleLocationID = new TableSchema.TableColumn(schema);
				colvarRoleLocationID.ColumnName = "RoleLocationID";
				colvarRoleLocationID.DataType = DbType.Int32;
				colvarRoleLocationID.MaxLength = 0;
				colvarRoleLocationID.AutoIncrement = false;
				colvarRoleLocationID.IsNullable = false;
				colvarRoleLocationID.IsPrimaryKey = false;
				colvarRoleLocationID.IsForeignKey = false;
				colvarRoleLocationID.IsReadOnly = false;
				colvarRoleLocationID.DefaultSetting = @"";
				colvarRoleLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRoleLocationID);

				TableSchema.TableColumn colvarSecurityLevel = new TableSchema.TableColumn(schema);
				colvarSecurityLevel.ColumnName = "SecurityLevel";
				colvarSecurityLevel.DataType = DbType.Byte;
				colvarSecurityLevel.MaxLength = 0;
				colvarSecurityLevel.AutoIncrement = false;
				colvarSecurityLevel.IsNullable = false;
				colvarSecurityLevel.IsPrimaryKey = false;
				colvarSecurityLevel.IsForeignKey = false;
				colvarSecurityLevel.IsReadOnly = false;
				colvarSecurityLevel.DefaultSetting = @"";
				colvarSecurityLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSecurityLevel);

				TableSchema.TableColumn colvarReportingLevel = new TableSchema.TableColumn(schema);
				colvarReportingLevel.ColumnName = "ReportingLevel";
				colvarReportingLevel.DataType = DbType.Int32;
				colvarReportingLevel.MaxLength = 0;
				colvarReportingLevel.AutoIncrement = false;
				colvarReportingLevel.IsNullable = false;
				colvarReportingLevel.IsPrimaryKey = false;
				colvarReportingLevel.IsForeignKey = false;
				colvarReportingLevel.IsReadOnly = false;
				colvarReportingLevel.DefaultSetting = @"";
				colvarReportingLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReportingLevel);

				TableSchema.TableColumn colvarUserTypeTeamTypeID = new TableSchema.TableColumn(schema);
				colvarUserTypeTeamTypeID.ColumnName = "UserTypeTeamTypeID";
				colvarUserTypeTeamTypeID.DataType = DbType.Int32;
				colvarUserTypeTeamTypeID.MaxLength = 0;
				colvarUserTypeTeamTypeID.AutoIncrement = false;
				colvarUserTypeTeamTypeID.IsNullable = false;
				colvarUserTypeTeamTypeID.IsPrimaryKey = false;
				colvarUserTypeTeamTypeID.IsForeignKey = false;
				colvarUserTypeTeamTypeID.IsReadOnly = false;
				colvarUserTypeTeamTypeID.DefaultSetting = @"";
				colvarUserTypeTeamTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeTeamTypeID);

				TableSchema.TableColumn colvarUserTypeTeamType = new TableSchema.TableColumn(schema);
				colvarUserTypeTeamType.ColumnName = "UserTypeTeamType";
				colvarUserTypeTeamType.DataType = DbType.AnsiString;
				colvarUserTypeTeamType.MaxLength = 30;
				colvarUserTypeTeamType.AutoIncrement = false;
				colvarUserTypeTeamType.IsNullable = false;
				colvarUserTypeTeamType.IsPrimaryKey = false;
				colvarUserTypeTeamType.IsForeignKey = false;
				colvarUserTypeTeamType.IsReadOnly = false;
				colvarUserTypeTeamType.DefaultSetting = @"";
				colvarUserTypeTeamType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserTypeTeamType);

				TableSchema.TableColumn colvarPayscaleID = new TableSchema.TableColumn(schema);
				colvarPayscaleID.ColumnName = "PayscaleID";
				colvarPayscaleID.DataType = DbType.Int32;
				colvarPayscaleID.MaxLength = 0;
				colvarPayscaleID.AutoIncrement = false;
				colvarPayscaleID.IsNullable = false;
				colvarPayscaleID.IsPrimaryKey = false;
				colvarPayscaleID.IsForeignKey = false;
				colvarPayscaleID.IsReadOnly = false;
				colvarPayscaleID.DefaultSetting = @"";
				colvarPayscaleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayscaleID);

				TableSchema.TableColumn colvarPayScaleName = new TableSchema.TableColumn(schema);
				colvarPayScaleName.ColumnName = "PayScaleName";
				colvarPayScaleName.DataType = DbType.String;
				colvarPayScaleName.MaxLength = 20;
				colvarPayScaleName.AutoIncrement = false;
				colvarPayScaleName.IsNullable = true;
				colvarPayScaleName.IsPrimaryKey = false;
				colvarPayScaleName.IsForeignKey = false;
				colvarPayScaleName.IsReadOnly = false;
				colvarPayScaleName.DefaultSetting = @"";
				colvarPayScaleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPayScaleName);

				TableSchema.TableColumn colvarAlternatePayScheduleID = new TableSchema.TableColumn(schema);
				colvarAlternatePayScheduleID.ColumnName = "AlternatePayScheduleID";
				colvarAlternatePayScheduleID.DataType = DbType.Int32;
				colvarAlternatePayScheduleID.MaxLength = 0;
				colvarAlternatePayScheduleID.AutoIncrement = false;
				colvarAlternatePayScheduleID.IsNullable = true;
				colvarAlternatePayScheduleID.IsPrimaryKey = false;
				colvarAlternatePayScheduleID.IsForeignKey = false;
				colvarAlternatePayScheduleID.IsReadOnly = false;
				colvarAlternatePayScheduleID.DefaultSetting = @"";
				colvarAlternatePayScheduleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAlternatePayScheduleID);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = true;
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
				colvarIsDeleted.IsNullable = true;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarIsServiceTech = new TableSchema.TableColumn(schema);
				colvarIsServiceTech.ColumnName = "IsServiceTech";
				colvarIsServiceTech.DataType = DbType.Boolean;
				colvarIsServiceTech.MaxLength = 0;
				colvarIsServiceTech.AutoIncrement = false;
				colvarIsServiceTech.IsNullable = true;
				colvarIsServiceTech.IsPrimaryKey = false;
				colvarIsServiceTech.IsForeignKey = false;
				colvarIsServiceTech.IsReadOnly = false;
				colvarIsServiceTech.DefaultSetting = @"";
				colvarIsServiceTech.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsServiceTech);

				TableSchema.TableColumn colvarSeasonIsVisibleToRecruits = new TableSchema.TableColumn(schema);
				colvarSeasonIsVisibleToRecruits.ColumnName = "SeasonIsVisibleToRecruits";
				colvarSeasonIsVisibleToRecruits.DataType = DbType.Boolean;
				colvarSeasonIsVisibleToRecruits.MaxLength = 0;
				colvarSeasonIsVisibleToRecruits.AutoIncrement = false;
				colvarSeasonIsVisibleToRecruits.IsNullable = false;
				colvarSeasonIsVisibleToRecruits.IsPrimaryKey = false;
				colvarSeasonIsVisibleToRecruits.IsForeignKey = false;
				colvarSeasonIsVisibleToRecruits.IsReadOnly = false;
				colvarSeasonIsVisibleToRecruits.DefaultSetting = @"";
				colvarSeasonIsVisibleToRecruits.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsVisibleToRecruits);

				TableSchema.TableColumn colvarSeasonStartDate = new TableSchema.TableColumn(schema);
				colvarSeasonStartDate.ColumnName = "SeasonStartDate";
				colvarSeasonStartDate.DataType = DbType.DateTime;
				colvarSeasonStartDate.MaxLength = 0;
				colvarSeasonStartDate.AutoIncrement = false;
				colvarSeasonStartDate.IsNullable = true;
				colvarSeasonStartDate.IsPrimaryKey = false;
				colvarSeasonStartDate.IsForeignKey = false;
				colvarSeasonStartDate.IsReadOnly = false;
				colvarSeasonStartDate.DefaultSetting = @"";
				colvarSeasonStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonStartDate);

				TableSchema.TableColumn colvarSeasonEndDate = new TableSchema.TableColumn(schema);
				colvarSeasonEndDate.ColumnName = "SeasonEndDate";
				colvarSeasonEndDate.DataType = DbType.DateTime;
				colvarSeasonEndDate.MaxLength = 0;
				colvarSeasonEndDate.AutoIncrement = false;
				colvarSeasonEndDate.IsNullable = true;
				colvarSeasonEndDate.IsPrimaryKey = false;
				colvarSeasonEndDate.IsForeignKey = false;
				colvarSeasonEndDate.IsReadOnly = false;
				colvarSeasonEndDate.DefaultSetting = @"";
				colvarSeasonEndDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonEndDate);

				TableSchema.TableColumn colvarSeasonIsCurrent = new TableSchema.TableColumn(schema);
				colvarSeasonIsCurrent.ColumnName = "SeasonIsCurrent";
				colvarSeasonIsCurrent.DataType = DbType.Boolean;
				colvarSeasonIsCurrent.MaxLength = 0;
				colvarSeasonIsCurrent.AutoIncrement = false;
				colvarSeasonIsCurrent.IsNullable = false;
				colvarSeasonIsCurrent.IsPrimaryKey = false;
				colvarSeasonIsCurrent.IsForeignKey = false;
				colvarSeasonIsCurrent.IsReadOnly = false;
				colvarSeasonIsCurrent.DefaultSetting = @"";
				colvarSeasonIsCurrent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsCurrent);

				TableSchema.TableColumn colvarSeasonIsActive = new TableSchema.TableColumn(schema);
				colvarSeasonIsActive.ColumnName = "SeasonIsActive";
				colvarSeasonIsActive.DataType = DbType.Boolean;
				colvarSeasonIsActive.MaxLength = 0;
				colvarSeasonIsActive.AutoIncrement = false;
				colvarSeasonIsActive.IsNullable = false;
				colvarSeasonIsActive.IsPrimaryKey = false;
				colvarSeasonIsActive.IsForeignKey = false;
				colvarSeasonIsActive.IsReadOnly = false;
				colvarSeasonIsActive.DefaultSetting = @"";
				colvarSeasonIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsActive);

				TableSchema.TableColumn colvarSeasonIsDeleted = new TableSchema.TableColumn(schema);
				colvarSeasonIsDeleted.ColumnName = "SeasonIsDeleted";
				colvarSeasonIsDeleted.DataType = DbType.Boolean;
				colvarSeasonIsDeleted.MaxLength = 0;
				colvarSeasonIsDeleted.AutoIncrement = false;
				colvarSeasonIsDeleted.IsNullable = false;
				colvarSeasonIsDeleted.IsPrimaryKey = false;
				colvarSeasonIsDeleted.IsForeignKey = false;
				colvarSeasonIsDeleted.IsReadOnly = false;
				colvarSeasonIsDeleted.DefaultSetting = @"";
				colvarSeasonIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonIsDeleted);

				TableSchema.TableColumn colvarSeasonName = new TableSchema.TableColumn(schema);
				colvarSeasonName.ColumnName = "SeasonName";
				colvarSeasonName.DataType = DbType.String;
				colvarSeasonName.MaxLength = 50;
				colvarSeasonName.AutoIncrement = false;
				colvarSeasonName.IsNullable = false;
				colvarSeasonName.IsPrimaryKey = false;
				colvarSeasonName.IsForeignKey = false;
				colvarSeasonName.IsReadOnly = false;
				colvarSeasonName.DefaultSetting = @"";
				colvarSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonName);

				TableSchema.TableColumn colvarActualTeamID = new TableSchema.TableColumn(schema);
				colvarActualTeamID.ColumnName = "ActualTeamID";
				colvarActualTeamID.DataType = DbType.Int32;
				colvarActualTeamID.MaxLength = 0;
				colvarActualTeamID.AutoIncrement = false;
				colvarActualTeamID.IsNullable = true;
				colvarActualTeamID.IsPrimaryKey = false;
				colvarActualTeamID.IsForeignKey = false;
				colvarActualTeamID.IsReadOnly = false;
				colvarActualTeamID.DefaultSetting = @"";
				colvarActualTeamID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActualTeamID);

				TableSchema.TableColumn colvarActualTeamLocationID = new TableSchema.TableColumn(schema);
				colvarActualTeamLocationID.ColumnName = "ActualTeamLocationID";
				colvarActualTeamLocationID.DataType = DbType.Int32;
				colvarActualTeamLocationID.MaxLength = 0;
				colvarActualTeamLocationID.AutoIncrement = false;
				colvarActualTeamLocationID.IsNullable = true;
				colvarActualTeamLocationID.IsPrimaryKey = false;
				colvarActualTeamLocationID.IsForeignKey = false;
				colvarActualTeamLocationID.IsReadOnly = false;
				colvarActualTeamLocationID.DefaultSetting = @"";
				colvarActualTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActualTeamLocationID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_RecruitUser",schema);
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
		public RU_RecruitUserView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public int? RecruitedByID {
			get { return GetColumnValue<int?>(Columns.RecruitedByID); }
			set { SetColumnValue(Columns.RecruitedByID, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		[DataMember]
		public string MiddleName {
			get { return GetColumnValue<string>(Columns.MiddleName); }
			set { SetColumnValue(Columns.MiddleName, value); }
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		[DataMember]
		public string PreferredName {
			get { return GetColumnValue<string>(Columns.PreferredName); }
			set { SetColumnValue(Columns.PreferredName, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string PublicFullName {
			get { return GetColumnValue<string>(Columns.PublicFullName); }
			set { SetColumnValue(Columns.PublicFullName, value); }
		}
		[DataMember]
		public string UserName {
			get { return GetColumnValue<string>(Columns.UserName); }
			set { SetColumnValue(Columns.UserName, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public string PhoneCell {
			get { return GetColumnValue<string>(Columns.PhoneCell); }
			set { SetColumnValue(Columns.PhoneCell, value); }
		}
		[DataMember]
		public short? PhoneCellCarrierID {
			get { return GetColumnValue<short?>(Columns.PhoneCellCarrierID); }
			set { SetColumnValue(Columns.PhoneCellCarrierID, value); }
		}
		[DataMember]
		public bool IsActiveUser {
			get { return GetColumnValue<bool>(Columns.IsActiveUser); }
			set { SetColumnValue(Columns.IsActiveUser, value); }
		}
		[DataMember]
		public bool IsDeletedUser {
			get { return GetColumnValue<bool>(Columns.IsDeletedUser); }
			set { SetColumnValue(Columns.IsDeletedUser, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public int? ReportsToID {
			get { return GetColumnValue<int?>(Columns.ReportsToID); }
			set { SetColumnValue(Columns.ReportsToID, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int?>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public int SocialSecCardStatusID {
			get { return GetColumnValue<int>(Columns.SocialSecCardStatusID); }
			set { SetColumnValue(Columns.SocialSecCardStatusID, value); }
		}
		[DataMember]
		public int DriversLicenseStatusID {
			get { return GetColumnValue<int>(Columns.DriversLicenseStatusID); }
			set { SetColumnValue(Columns.DriversLicenseStatusID, value); }
		}
		[DataMember]
		public int W9StatusID {
			get { return GetColumnValue<int>(Columns.W9StatusID); }
			set { SetColumnValue(Columns.W9StatusID, value); }
		}
		[DataMember]
		public int I9StatusID {
			get { return GetColumnValue<int>(Columns.I9StatusID); }
			set { SetColumnValue(Columns.I9StatusID, value); }
		}
		[DataMember]
		public int W4StatusID {
			get { return GetColumnValue<int>(Columns.W4StatusID); }
			set { SetColumnValue(Columns.W4StatusID, value); }
		}
		[DataMember]
		public string SocialSecCardNotes {
			get { return GetColumnValue<string>(Columns.SocialSecCardNotes); }
			set { SetColumnValue(Columns.SocialSecCardNotes, value); }
		}
		[DataMember]
		public string DriversLicenseNotes {
			get { return GetColumnValue<string>(Columns.DriversLicenseNotes); }
			set { SetColumnValue(Columns.DriversLicenseNotes, value); }
		}
		[DataMember]
		public string W9Notes {
			get { return GetColumnValue<string>(Columns.W9Notes); }
			set { SetColumnValue(Columns.W9Notes, value); }
		}
		[DataMember]
		public string I9Notes {
			get { return GetColumnValue<string>(Columns.I9Notes); }
			set { SetColumnValue(Columns.I9Notes, value); }
		}
		[DataMember]
		public string W4Notes {
			get { return GetColumnValue<string>(Columns.W4Notes); }
			set { SetColumnValue(Columns.W4Notes, value); }
		}
		[DataMember]
		public bool IsActiveRecruit {
			get { return GetColumnValue<bool>(Columns.IsActiveRecruit); }
			set { SetColumnValue(Columns.IsActiveRecruit, value); }
		}
		[DataMember]
		public bool IsDeletedRecruit {
			get { return GetColumnValue<bool>(Columns.IsDeletedRecruit); }
			set { SetColumnValue(Columns.IsDeletedRecruit, value); }
		}
		[DataMember]
		public short UserTypeID {
			get { return GetColumnValue<short>(Columns.UserTypeID); }
			set { SetColumnValue(Columns.UserTypeID, value); }
		}
		[DataMember]
		public string UserType {
			get { return GetColumnValue<string>(Columns.UserType); }
			set { SetColumnValue(Columns.UserType, value); }
		}
		[DataMember]
		public int RoleLocationID {
			get { return GetColumnValue<int>(Columns.RoleLocationID); }
			set { SetColumnValue(Columns.RoleLocationID, value); }
		}
		[DataMember]
		public byte SecurityLevel {
			get { return GetColumnValue<byte>(Columns.SecurityLevel); }
			set { SetColumnValue(Columns.SecurityLevel, value); }
		}
		[DataMember]
		public int ReportingLevel {
			get { return GetColumnValue<int>(Columns.ReportingLevel); }
			set { SetColumnValue(Columns.ReportingLevel, value); }
		}
		[DataMember]
		public int UserTypeTeamTypeID {
			get { return GetColumnValue<int>(Columns.UserTypeTeamTypeID); }
			set { SetColumnValue(Columns.UserTypeTeamTypeID, value); }
		}
		[DataMember]
		public string UserTypeTeamType {
			get { return GetColumnValue<string>(Columns.UserTypeTeamType); }
			set { SetColumnValue(Columns.UserTypeTeamType, value); }
		}
		[DataMember]
		public int PayscaleID {
			get { return GetColumnValue<int>(Columns.PayscaleID); }
			set { SetColumnValue(Columns.PayscaleID, value); }
		}
		[DataMember]
		public string PayScaleName {
			get { return GetColumnValue<string>(Columns.PayScaleName); }
			set { SetColumnValue(Columns.PayScaleName, value); }
		}
		[DataMember]
		public int? AlternatePayScheduleID {
			get { return GetColumnValue<int?>(Columns.AlternatePayScheduleID); }
			set { SetColumnValue(Columns.AlternatePayScheduleID, value); }
		}
		[DataMember]
		public bool? IsActive {
			get { return GetColumnValue<bool?>(Columns.IsActive); }
			set { SetColumnValue(Columns.IsActive, value); }
		}
		[DataMember]
		public bool? IsDeleted {
			get { return GetColumnValue<bool?>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
		}
		[DataMember]
		public bool? IsServiceTech {
			get { return GetColumnValue<bool?>(Columns.IsServiceTech); }
			set { SetColumnValue(Columns.IsServiceTech, value); }
		}
		[DataMember]
		public bool SeasonIsVisibleToRecruits {
			get { return GetColumnValue<bool>(Columns.SeasonIsVisibleToRecruits); }
			set { SetColumnValue(Columns.SeasonIsVisibleToRecruits, value); }
		}
		[DataMember]
		public DateTime? SeasonStartDate {
			get { return GetColumnValue<DateTime?>(Columns.SeasonStartDate); }
			set { SetColumnValue(Columns.SeasonStartDate, value); }
		}
		[DataMember]
		public DateTime? SeasonEndDate {
			get { return GetColumnValue<DateTime?>(Columns.SeasonEndDate); }
			set { SetColumnValue(Columns.SeasonEndDate, value); }
		}
		[DataMember]
		public bool SeasonIsCurrent {
			get { return GetColumnValue<bool>(Columns.SeasonIsCurrent); }
			set { SetColumnValue(Columns.SeasonIsCurrent, value); }
		}
		[DataMember]
		public bool SeasonIsActive {
			get { return GetColumnValue<bool>(Columns.SeasonIsActive); }
			set { SetColumnValue(Columns.SeasonIsActive, value); }
		}
		[DataMember]
		public bool SeasonIsDeleted {
			get { return GetColumnValue<bool>(Columns.SeasonIsDeleted); }
			set { SetColumnValue(Columns.SeasonIsDeleted, value); }
		}
		[DataMember]
		public string SeasonName {
			get { return GetColumnValue<string>(Columns.SeasonName); }
			set { SetColumnValue(Columns.SeasonName, value); }
		}
		[DataMember]
		public int? ActualTeamID {
			get { return GetColumnValue<int?>(Columns.ActualTeamID); }
			set { SetColumnValue(Columns.ActualTeamID, value); }
		}
		[DataMember]
		public int? ActualTeamLocationID {
			get { return GetColumnValue<int?>(Columns.ActualTeamLocationID); }
			set { SetColumnValue(Columns.ActualTeamLocationID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return UserID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitedByIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PreferredNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn PublicFullNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn UserNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PhoneCellColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PhoneCellCarrierIDColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn IsActiveUserColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn IsDeletedUserColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn ReportsToIDColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn SocialSecCardStatusIDColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn DriversLicenseStatusIDColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn W9StatusIDColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn I9StatusIDColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn W4StatusIDColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn SocialSecCardNotesColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn DriversLicenseNotesColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn W9NotesColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn I9NotesColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn W4NotesColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn IsActiveRecruitColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn IsDeletedRecruitColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn UserTypeIDColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn UserTypeColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn RoleLocationIDColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn SecurityLevelColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn ReportingLevelColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn UserTypeTeamTypeIDColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn UserTypeTeamTypeColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn PayscaleIDColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn PayScaleNameColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn AlternatePayScheduleIDColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn IsServiceTechColumn
		{
			get { return Schema.Columns[43]; }
		}
		public static TableSchema.TableColumn SeasonIsVisibleToRecruitsColumn
		{
			get { return Schema.Columns[44]; }
		}
		public static TableSchema.TableColumn SeasonStartDateColumn
		{
			get { return Schema.Columns[45]; }
		}
		public static TableSchema.TableColumn SeasonEndDateColumn
		{
			get { return Schema.Columns[46]; }
		}
		public static TableSchema.TableColumn SeasonIsCurrentColumn
		{
			get { return Schema.Columns[47]; }
		}
		public static TableSchema.TableColumn SeasonIsActiveColumn
		{
			get { return Schema.Columns[48]; }
		}
		public static TableSchema.TableColumn SeasonIsDeletedColumn
		{
			get { return Schema.Columns[49]; }
		}
		public static TableSchema.TableColumn SeasonNameColumn
		{
			get { return Schema.Columns[50]; }
		}
		public static TableSchema.TableColumn ActualTeamIDColumn
		{
			get { return Schema.Columns[51]; }
		}
		public static TableSchema.TableColumn ActualTeamLocationIDColumn
		{
			get { return Schema.Columns[52]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string RecruitedByID = @"RecruitedByID";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PreferredName = @"PreferredName";
			public const string FullName = @"FullName";
			public const string PublicFullName = @"PublicFullName";
			public const string UserName = @"UserName";
			public const string Email = @"Email";
			public const string PhoneCell = @"PhoneCell";
			public const string PhoneCellCarrierID = @"PhoneCellCarrierID";
			public const string IsActiveUser = @"IsActiveUser";
			public const string IsDeletedUser = @"IsDeletedUser";
			public const string RecruitID = @"RecruitID";
			public const string SeasonID = @"SeasonID";
			public const string ReportsToID = @"ReportsToID";
			public const string TeamID = @"TeamID";
			public const string SocialSecCardStatusID = @"SocialSecCardStatusID";
			public const string DriversLicenseStatusID = @"DriversLicenseStatusID";
			public const string W9StatusID = @"W9StatusID";
			public const string I9StatusID = @"I9StatusID";
			public const string W4StatusID = @"W4StatusID";
			public const string SocialSecCardNotes = @"SocialSecCardNotes";
			public const string DriversLicenseNotes = @"DriversLicenseNotes";
			public const string W9Notes = @"W9Notes";
			public const string I9Notes = @"I9Notes";
			public const string W4Notes = @"W4Notes";
			public const string IsActiveRecruit = @"IsActiveRecruit";
			public const string IsDeletedRecruit = @"IsDeletedRecruit";
			public const string UserTypeID = @"UserTypeID";
			public const string UserType = @"UserType";
			public const string RoleLocationID = @"RoleLocationID";
			public const string SecurityLevel = @"SecurityLevel";
			public const string ReportingLevel = @"ReportingLevel";
			public const string UserTypeTeamTypeID = @"UserTypeTeamTypeID";
			public const string UserTypeTeamType = @"UserTypeTeamType";
			public const string PayscaleID = @"PayscaleID";
			public const string PayScaleName = @"PayScaleName";
			public const string AlternatePayScheduleID = @"AlternatePayScheduleID";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
			public const string IsServiceTech = @"IsServiceTech";
			public const string SeasonIsVisibleToRecruits = @"SeasonIsVisibleToRecruits";
			public const string SeasonStartDate = @"SeasonStartDate";
			public const string SeasonEndDate = @"SeasonEndDate";
			public const string SeasonIsCurrent = @"SeasonIsCurrent";
			public const string SeasonIsActive = @"SeasonIsActive";
			public const string SeasonIsDeleted = @"SeasonIsDeleted";
			public const string SeasonName = @"SeasonName";
			public const string ActualTeamID = @"ActualTeamID";
			public const string ActualTeamLocationID = @"ActualTeamLocationID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_RollCallRecordsLatestByRecruitView class.
	/// </summary>
	[DataContract]
	public partial class RU_RollCallRecordsLatestByRecruitViewCollection : ReadOnlyList<RU_RollCallRecordsLatestByRecruitView, RU_RollCallRecordsLatestByRecruitViewCollection>
	{
		public static RU_RollCallRecordsLatestByRecruitViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_RollCallRecordsLatestByRecruitViewCollection result = new RU_RollCallRecordsLatestByRecruitViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_RollCallRecordsLatestByRecruit view.
	/// </summary>
	[DataContract]
	public partial class RU_RollCallRecordsLatestByRecruitView : ReadOnlyRecord<RU_RollCallRecordsLatestByRecruitView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_RollCallRecordsLatestByRecruit", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRollCallRecordID = new TableSchema.TableColumn(schema);
				colvarRollCallRecordID.ColumnName = "RollCallRecordID";
				colvarRollCallRecordID.DataType = DbType.Int32;
				colvarRollCallRecordID.MaxLength = 0;
				colvarRollCallRecordID.AutoIncrement = true;
				colvarRollCallRecordID.IsNullable = false;
				colvarRollCallRecordID.IsPrimaryKey = false;
				colvarRollCallRecordID.IsForeignKey = false;
				colvarRollCallRecordID.IsReadOnly = false;
				colvarRollCallRecordID.DefaultSetting = @"";
				colvarRollCallRecordID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRollCallRecordID);

				TableSchema.TableColumn colvarRollCallID = new TableSchema.TableColumn(schema);
				colvarRollCallID.ColumnName = "RollCallID";
				colvarRollCallID.DataType = DbType.Int32;
				colvarRollCallID.MaxLength = 0;
				colvarRollCallID.AutoIncrement = false;
				colvarRollCallID.IsNullable = false;
				colvarRollCallID.IsPrimaryKey = false;
				colvarRollCallID.IsForeignKey = false;
				colvarRollCallID.IsReadOnly = false;
				colvarRollCallID.DefaultSetting = @"";
				colvarRollCallID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRollCallID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarPresent = new TableSchema.TableColumn(schema);
				colvarPresent.ColumnName = "Present";
				colvarPresent.DataType = DbType.Boolean;
				colvarPresent.MaxLength = 0;
				colvarPresent.AutoIncrement = false;
				colvarPresent.IsNullable = false;
				colvarPresent.IsPrimaryKey = false;
				colvarPresent.IsForeignKey = false;
				colvarPresent.IsReadOnly = false;
				colvarPresent.DefaultSetting = @"";
				colvarPresent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPresent);

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

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = false;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"";
				colvarCreatedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByID);

				TableSchema.TableColumn colvarRN = new TableSchema.TableColumn(schema);
				colvarRN.ColumnName = "RN";
				colvarRN.DataType = DbType.Int64;
				colvarRN.MaxLength = 0;
				colvarRN.AutoIncrement = false;
				colvarRN.IsNullable = true;
				colvarRN.IsPrimaryKey = false;
				colvarRN.IsForeignKey = false;
				colvarRN.IsReadOnly = false;
				colvarRN.DefaultSetting = @"";
				colvarRN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRN);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_RollCallRecordsLatestByRecruit",schema);
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
		public RU_RollCallRecordsLatestByRecruitView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RollCallRecordID {
			get { return GetColumnValue<int>(Columns.RollCallRecordID); }
			set { SetColumnValue(Columns.RollCallRecordID, value); }
		}
		[DataMember]
		public int RollCallID {
			get { return GetColumnValue<int>(Columns.RollCallID); }
			set { SetColumnValue(Columns.RollCallID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public bool Present {
			get { return GetColumnValue<bool>(Columns.Present); }
			set { SetColumnValue(Columns.Present, value); }
		}
		[DataMember]
		public DateTime CreatedByDate {
			get { return GetColumnValue<DateTime>(Columns.CreatedByDate); }
			set { SetColumnValue(Columns.CreatedByDate, value); }
		}
		[DataMember]
		public string CreatedByID {
			get { return GetColumnValue<string>(Columns.CreatedByID); }
			set { SetColumnValue(Columns.CreatedByID, value); }
		}
		[DataMember]
		public long? RN {
			get { return GetColumnValue<long?>(Columns.RN); }
			set { SetColumnValue(Columns.RN, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RollCallRecordID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RollCallRecordIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RollCallIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PresentColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RNColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RollCallRecordID = @"RollCallRecordID";
			public const string RollCallID = @"RollCallID";
			public const string RecruitID = @"RecruitID";
			public const string Present = @"Present";
			public const string CreatedByDate = @"CreatedByDate";
			public const string CreatedByID = @"CreatedByID";
			public const string RN = @"RN";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TeamLocationView class.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationViewCollection : ReadOnlyList<RU_TeamLocationView, RU_TeamLocationViewCollection>
	{
		public static RU_TeamLocationViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TeamLocationViewCollection result = new RU_TeamLocationViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TeamLocation view.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationView : ReadOnlyRecord<RU_TeamLocationView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TeamLocation", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = false;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.AnsiString;
				colvarDescription.MaxLength = 50;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);

				TableSchema.TableColumn colvarSeasonID = new TableSchema.TableColumn(schema);
				colvarSeasonID.ColumnName = "SeasonID";
				colvarSeasonID.DataType = DbType.Int32;
				colvarSeasonID.MaxLength = 0;
				colvarSeasonID.AutoIncrement = false;
				colvarSeasonID.IsNullable = false;
				colvarSeasonID.IsPrimaryKey = false;
				colvarSeasonID.IsForeignKey = false;
				colvarSeasonID.IsReadOnly = false;
				colvarSeasonID.DefaultSetting = @"";
				colvarSeasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonID);

				TableSchema.TableColumn colvarSeasonName = new TableSchema.TableColumn(schema);
				colvarSeasonName.ColumnName = "SeasonName";
				colvarSeasonName.DataType = DbType.String;
				colvarSeasonName.MaxLength = 50;
				colvarSeasonName.AutoIncrement = false;
				colvarSeasonName.IsNullable = false;
				colvarSeasonName.IsPrimaryKey = false;
				colvarSeasonName.IsForeignKey = false;
				colvarSeasonName.IsReadOnly = false;
				colvarSeasonName.DefaultSetting = @"";
				colvarSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonName);

				TableSchema.TableColumn colvarMarketID = new TableSchema.TableColumn(schema);
				colvarMarketID.ColumnName = "MarketID";
				colvarMarketID.DataType = DbType.Int32;
				colvarMarketID.MaxLength = 0;
				colvarMarketID.AutoIncrement = false;
				colvarMarketID.IsNullable = false;
				colvarMarketID.IsPrimaryKey = false;
				colvarMarketID.IsForeignKey = false;
				colvarMarketID.IsReadOnly = false;
				colvarMarketID.DefaultSetting = @"";
				colvarMarketID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMarketID);

				TableSchema.TableColumn colvarMarketName = new TableSchema.TableColumn(schema);
				colvarMarketName.ColumnName = "MarketName";
				colvarMarketName.DataType = DbType.String;
				colvarMarketName.MaxLength = 50;
				colvarMarketName.AutoIncrement = false;
				colvarMarketName.IsNullable = false;
				colvarMarketName.IsPrimaryKey = false;
				colvarMarketName.IsForeignKey = false;
				colvarMarketName.IsReadOnly = false;
				colvarMarketName.DefaultSetting = @"";
				colvarMarketName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMarketName);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.AnsiString;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = false;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarStateAB = new TableSchema.TableColumn(schema);
				colvarStateAB.ColumnName = "StateAB";
				colvarStateAB.DataType = DbType.AnsiStringFixedLength;
				colvarStateAB.MaxLength = 2;
				colvarStateAB.AutoIncrement = false;
				colvarStateAB.IsNullable = false;
				colvarStateAB.IsPrimaryKey = false;
				colvarStateAB.IsForeignKey = false;
				colvarStateAB.IsReadOnly = false;
				colvarStateAB.DefaultSetting = @"";
				colvarStateAB.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateAB);

				TableSchema.TableColumn colvarTimeZoneID = new TableSchema.TableColumn(schema);
				colvarTimeZoneID.ColumnName = "TimeZoneID";
				colvarTimeZoneID.DataType = DbType.Int32;
				colvarTimeZoneID.MaxLength = 0;
				colvarTimeZoneID.AutoIncrement = false;
				colvarTimeZoneID.IsNullable = true;
				colvarTimeZoneID.IsPrimaryKey = false;
				colvarTimeZoneID.IsForeignKey = false;
				colvarTimeZoneID.IsReadOnly = false;
				colvarTimeZoneID.DefaultSetting = @"";
				colvarTimeZoneID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTimeZoneID);

				TableSchema.TableColumn colvarTimeZoneName = new TableSchema.TableColumn(schema);
				colvarTimeZoneName.ColumnName = "TimeZoneName";
				colvarTimeZoneName.DataType = DbType.AnsiString;
				colvarTimeZoneName.MaxLength = 50;
				colvarTimeZoneName.AutoIncrement = false;
				colvarTimeZoneName.IsNullable = true;
				colvarTimeZoneName.IsPrimaryKey = false;
				colvarTimeZoneName.IsForeignKey = false;
				colvarTimeZoneName.IsReadOnly = false;
				colvarTimeZoneName.DefaultSetting = @"";
				colvarTimeZoneName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTimeZoneName);

				TableSchema.TableColumn colvarHourDifference = new TableSchema.TableColumn(schema);
				colvarHourDifference.ColumnName = "HourDifference";
				colvarHourDifference.DataType = DbType.Int32;
				colvarHourDifference.MaxLength = 0;
				colvarHourDifference.AutoIncrement = false;
				colvarHourDifference.IsNullable = true;
				colvarHourDifference.IsPrimaryKey = false;
				colvarHourDifference.IsForeignKey = false;
				colvarHourDifference.IsReadOnly = false;
				colvarHourDifference.DefaultSetting = @"";
				colvarHourDifference.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHourDifference);

				TableSchema.TableColumn colvarCurrTime = new TableSchema.TableColumn(schema);
				colvarCurrTime.ColumnName = "CurrTime";
				colvarCurrTime.DataType = DbType.DateTime;
				colvarCurrTime.MaxLength = 0;
				colvarCurrTime.AutoIncrement = false;
				colvarCurrTime.IsNullable = true;
				colvarCurrTime.IsPrimaryKey = false;
				colvarCurrTime.IsForeignKey = false;
				colvarCurrTime.IsReadOnly = false;
				colvarCurrTime.DefaultSetting = @"";
				colvarCurrTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrTime);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TeamLocation",schema);
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
		public RU_TeamLocationView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int TeamLocationID {
			get { return GetColumnValue<int>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public string Description {
			get { return GetColumnValue<string>(Columns.Description); }
			set { SetColumnValue(Columns.Description, value); }
		}
		[DataMember]
		public int SeasonID {
			get { return GetColumnValue<int>(Columns.SeasonID); }
			set { SetColumnValue(Columns.SeasonID, value); }
		}
		[DataMember]
		public string SeasonName {
			get { return GetColumnValue<string>(Columns.SeasonName); }
			set { SetColumnValue(Columns.SeasonName, value); }
		}
		[DataMember]
		public int MarketID {
			get { return GetColumnValue<int>(Columns.MarketID); }
			set { SetColumnValue(Columns.MarketID, value); }
		}
		[DataMember]
		public string MarketName {
			get { return GetColumnValue<string>(Columns.MarketName); }
			set { SetColumnValue(Columns.MarketName, value); }
		}
		[DataMember]
		public string City {
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}
		[DataMember]
		public string StateAB {
			get { return GetColumnValue<string>(Columns.StateAB); }
			set { SetColumnValue(Columns.StateAB, value); }
		}
		[DataMember]
		public int? TimeZoneID {
			get { return GetColumnValue<int?>(Columns.TimeZoneID); }
			set { SetColumnValue(Columns.TimeZoneID, value); }
		}
		[DataMember]
		public string TimeZoneName {
			get { return GetColumnValue<string>(Columns.TimeZoneName); }
			set { SetColumnValue(Columns.TimeZoneName, value); }
		}
		[DataMember]
		public int? HourDifference {
			get { return GetColumnValue<int?>(Columns.HourDifference); }
			set { SetColumnValue(Columns.HourDifference, value); }
		}
		[DataMember]
		public DateTime? CurrTime {
			get { return GetColumnValue<DateTime?>(Columns.CurrTime); }
			set { SetColumnValue(Columns.CurrTime, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return Description;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DescriptionColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SeasonIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SeasonNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn MarketIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn MarketNameColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn StateABColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn TimeZoneIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TimeZoneNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn HourDifferenceColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CurrTimeColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TeamLocationID = @"TeamLocationID";
			public const string Description = @"Description";
			public const string SeasonID = @"SeasonID";
			public const string SeasonName = @"SeasonName";
			public const string MarketID = @"MarketID";
			public const string MarketName = @"MarketName";
			public const string City = @"City";
			public const string StateAB = @"StateAB";
			public const string TimeZoneID = @"TimeZoneID";
			public const string TimeZoneName = @"TimeZoneName";
			public const string HourDifference = @"HourDifference";
			public const string CurrTime = @"CurrTime";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TeamLocationRosterByWeekView class.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationRosterByWeekViewCollection : ReadOnlyList<RU_TeamLocationRosterByWeekView, RU_TeamLocationRosterByWeekViewCollection>
	{
		public static RU_TeamLocationRosterByWeekViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TeamLocationRosterByWeekViewCollection result = new RU_TeamLocationRosterByWeekViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TeamLocationRosterByWeek view.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationRosterByWeekView : ReadOnlyRecord<RU_TeamLocationRosterByWeekView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TeamLocationRosterByWeek", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarStartOfWeek = new TableSchema.TableColumn(schema);
				colvarStartOfWeek.ColumnName = "StartOfWeek";
				colvarStartOfWeek.DataType = DbType.DateTime;
				colvarStartOfWeek.MaxLength = 0;
				colvarStartOfWeek.AutoIncrement = false;
				colvarStartOfWeek.IsNullable = false;
				colvarStartOfWeek.IsPrimaryKey = false;
				colvarStartOfWeek.IsForeignKey = false;
				colvarStartOfWeek.IsReadOnly = false;
				colvarStartOfWeek.DefaultSetting = @"";
				colvarStartOfWeek.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartOfWeek);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TeamLocationRosterByWeek",schema);
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
		public RU_TeamLocationRosterByWeekView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public DateTime StartOfWeek {
			get { return GetColumnValue<DateTime>(Columns.StartOfWeek); }
			set { SetColumnValue(Columns.StartOfWeek, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RecruitID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn StartOfWeekColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RecruitID = @"RecruitID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string StartOfWeek = @"StartOfWeek";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TeamLocationRosterTransfersView class.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationRosterTransfersViewCollection : ReadOnlyList<RU_TeamLocationRosterTransfersView, RU_TeamLocationRosterTransfersViewCollection>
	{
		public static RU_TeamLocationRosterTransfersViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TeamLocationRosterTransfersViewCollection result = new RU_TeamLocationRosterTransfersViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TeamLocationRosterTransfers view.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocationRosterTransfersView : ReadOnlyRecord<RU_TeamLocationRosterTransfersView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TeamLocationRosterTransfers", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRosterID = new TableSchema.TableColumn(schema);
				colvarRosterID.ColumnName = "RosterID";
				colvarRosterID.DataType = DbType.Int32;
				colvarRosterID.MaxLength = 0;
				colvarRosterID.AutoIncrement = true;
				colvarRosterID.IsNullable = false;
				colvarRosterID.IsPrimaryKey = false;
				colvarRosterID.IsForeignKey = false;
				colvarRosterID.IsReadOnly = false;
				colvarRosterID.DefaultSetting = @"";
				colvarRosterID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRosterID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarTransferDate = new TableSchema.TableColumn(schema);
				colvarTransferDate.ColumnName = "TransferDate";
				colvarTransferDate.DataType = DbType.DateTime;
				colvarTransferDate.MaxLength = 0;
				colvarTransferDate.AutoIncrement = false;
				colvarTransferDate.IsNullable = true;
				colvarTransferDate.IsPrimaryKey = false;
				colvarTransferDate.IsForeignKey = false;
				colvarTransferDate.IsReadOnly = false;
				colvarTransferDate.DefaultSetting = @"";
				colvarTransferDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTransferDate);

				TableSchema.TableColumn colvarHasQuit = new TableSchema.TableColumn(schema);
				colvarHasQuit.ColumnName = "HasQuit";
				colvarHasQuit.DataType = DbType.Boolean;
				colvarHasQuit.MaxLength = 0;
				colvarHasQuit.AutoIncrement = false;
				colvarHasQuit.IsNullable = true;
				colvarHasQuit.IsPrimaryKey = false;
				colvarHasQuit.IsForeignKey = false;
				colvarHasQuit.IsReadOnly = false;
				colvarHasQuit.DefaultSetting = @"";
				colvarHasQuit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHasQuit);

				TableSchema.TableColumn colvarRN = new TableSchema.TableColumn(schema);
				colvarRN.ColumnName = "RN";
				colvarRN.DataType = DbType.Int64;
				colvarRN.MaxLength = 0;
				colvarRN.AutoIncrement = false;
				colvarRN.IsNullable = true;
				colvarRN.IsPrimaryKey = false;
				colvarRN.IsForeignKey = false;
				colvarRN.IsReadOnly = false;
				colvarRN.DefaultSetting = @"";
				colvarRN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRN);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TeamLocationRosterTransfers",schema);
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
		public RU_TeamLocationRosterTransfersView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RosterID {
			get { return GetColumnValue<int>(Columns.RosterID); }
			set { SetColumnValue(Columns.RosterID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public DateTime? TransferDate {
			get { return GetColumnValue<DateTime?>(Columns.TransferDate); }
			set { SetColumnValue(Columns.TransferDate, value); }
		}
		[DataMember]
		public bool? HasQuit {
			get { return GetColumnValue<bool?>(Columns.HasQuit); }
			set { SetColumnValue(Columns.HasQuit, value); }
		}
		[DataMember]
		public long? RN {
			get { return GetColumnValue<long?>(Columns.RN); }
			set { SetColumnValue(Columns.RN, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RosterID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RosterIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TransferDateColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn HasQuitColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn RNColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RosterID = @"RosterID";
			public const string RecruitID = @"RecruitID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string TransferDate = @"TransferDate";
			public const string HasQuit = @"HasQuit";
			public const string RN = @"RN";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TeamLocatonRosterCurrentByRecruitView class.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocatonRosterCurrentByRecruitViewCollection : ReadOnlyList<RU_TeamLocatonRosterCurrentByRecruitView, RU_TeamLocatonRosterCurrentByRecruitViewCollection>
	{
		public static RU_TeamLocatonRosterCurrentByRecruitViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TeamLocatonRosterCurrentByRecruitViewCollection result = new RU_TeamLocatonRosterCurrentByRecruitViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TeamLocatonRosterCurrentByRecruit view.
	/// </summary>
	[DataContract]
	public partial class RU_TeamLocatonRosterCurrentByRecruitView : ReadOnlyRecord<RU_TeamLocatonRosterCurrentByRecruitView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TeamLocatonRosterCurrentByRecruit", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarRosterID = new TableSchema.TableColumn(schema);
				colvarRosterID.ColumnName = "RosterID";
				colvarRosterID.DataType = DbType.Int32;
				colvarRosterID.MaxLength = 0;
				colvarRosterID.AutoIncrement = false;
				colvarRosterID.IsNullable = false;
				colvarRosterID.IsPrimaryKey = false;
				colvarRosterID.IsForeignKey = false;
				colvarRosterID.IsReadOnly = false;
				colvarRosterID.DefaultSetting = @"";
				colvarRosterID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRosterID);

				TableSchema.TableColumn colvarTeamLocationID = new TableSchema.TableColumn(schema);
				colvarTeamLocationID.ColumnName = "TeamLocationID";
				colvarTeamLocationID.DataType = DbType.Int32;
				colvarTeamLocationID.MaxLength = 0;
				colvarTeamLocationID.AutoIncrement = false;
				colvarTeamLocationID.IsNullable = true;
				colvarTeamLocationID.IsPrimaryKey = false;
				colvarTeamLocationID.IsForeignKey = false;
				colvarTeamLocationID.IsReadOnly = false;
				colvarTeamLocationID.DefaultSetting = @"";
				colvarTeamLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationID);

				TableSchema.TableColumn colvarTerminationReasonID = new TableSchema.TableColumn(schema);
				colvarTerminationReasonID.ColumnName = "TerminationReasonID";
				colvarTerminationReasonID.DataType = DbType.Int32;
				colvarTerminationReasonID.MaxLength = 0;
				colvarTerminationReasonID.AutoIncrement = false;
				colvarTerminationReasonID.IsNullable = true;
				colvarTerminationReasonID.IsPrimaryKey = false;
				colvarTerminationReasonID.IsForeignKey = false;
				colvarTerminationReasonID.IsReadOnly = false;
				colvarTerminationReasonID.DefaultSetting = @"";
				colvarTerminationReasonID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationReasonID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarArrivalDate = new TableSchema.TableColumn(schema);
				colvarArrivalDate.ColumnName = "ArrivalDate";
				colvarArrivalDate.DataType = DbType.DateTime;
				colvarArrivalDate.MaxLength = 0;
				colvarArrivalDate.AutoIncrement = false;
				colvarArrivalDate.IsNullable = true;
				colvarArrivalDate.IsPrimaryKey = false;
				colvarArrivalDate.IsForeignKey = false;
				colvarArrivalDate.IsReadOnly = false;
				colvarArrivalDate.DefaultSetting = @"";
				colvarArrivalDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarArrivalDate);

				TableSchema.TableColumn colvarQuitDate = new TableSchema.TableColumn(schema);
				colvarQuitDate.ColumnName = "QuitDate";
				colvarQuitDate.DataType = DbType.DateTime;
				colvarQuitDate.MaxLength = 0;
				colvarQuitDate.AutoIncrement = false;
				colvarQuitDate.IsNullable = true;
				colvarQuitDate.IsPrimaryKey = false;
				colvarQuitDate.IsForeignKey = false;
				colvarQuitDate.IsReadOnly = false;
				colvarQuitDate.DefaultSetting = @"";
				colvarQuitDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuitDate);

				TableSchema.TableColumn colvarReason = new TableSchema.TableColumn(schema);
				colvarReason.ColumnName = "Reason";
				colvarReason.DataType = DbType.String;
				colvarReason.MaxLength = -1;
				colvarReason.AutoIncrement = false;
				colvarReason.IsNullable = true;
				colvarReason.IsPrimaryKey = false;
				colvarReason.IsForeignKey = false;
				colvarReason.IsReadOnly = false;
				colvarReason.DefaultSetting = @"";
				colvarReason.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReason);

				TableSchema.TableColumn colvarTerminationType = new TableSchema.TableColumn(schema);
				colvarTerminationType.ColumnName = "TerminationType";
				colvarTerminationType.DataType = DbType.String;
				colvarTerminationType.MaxLength = 50;
				colvarTerminationType.AutoIncrement = false;
				colvarTerminationType.IsNullable = true;
				colvarTerminationType.IsPrimaryKey = false;
				colvarTerminationType.IsForeignKey = false;
				colvarTerminationType.IsReadOnly = false;
				colvarTerminationType.DefaultSetting = @"";
				colvarTerminationType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationType);

				TableSchema.TableColumn colvarNote = new TableSchema.TableColumn(schema);
				colvarNote.ColumnName = "Note";
				colvarNote.DataType = DbType.String;
				colvarNote.MaxLength = -1;
				colvarNote.AutoIncrement = false;
				colvarNote.IsNullable = true;
				colvarNote.IsPrimaryKey = false;
				colvarNote.IsForeignKey = false;
				colvarNote.IsReadOnly = false;
				colvarNote.DefaultSetting = @"";
				colvarNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNote);

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

				TableSchema.TableColumn colvarCreatedByDate = new TableSchema.TableColumn(schema);
				colvarCreatedByDate.ColumnName = "CreatedByDate";
				colvarCreatedByDate.DataType = DbType.DateTime;
				colvarCreatedByDate.MaxLength = 0;
				colvarCreatedByDate.AutoIncrement = false;
				colvarCreatedByDate.IsNullable = true;
				colvarCreatedByDate.IsPrimaryKey = false;
				colvarCreatedByDate.IsForeignKey = false;
				colvarCreatedByDate.IsReadOnly = false;
				colvarCreatedByDate.DefaultSetting = @"";
				colvarCreatedByDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByDate);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = true;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"";
				colvarCreatedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TeamLocatonRosterCurrentByRecruit",schema);
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
		public RU_TeamLocatonRosterCurrentByRecruitView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int RosterID {
			get { return GetColumnValue<int>(Columns.RosterID); }
			set { SetColumnValue(Columns.RosterID, value); }
		}
		[DataMember]
		public int? TeamLocationID {
			get { return GetColumnValue<int?>(Columns.TeamLocationID); }
			set { SetColumnValue(Columns.TeamLocationID, value); }
		}
		[DataMember]
		public int? TerminationReasonID {
			get { return GetColumnValue<int?>(Columns.TerminationReasonID); }
			set { SetColumnValue(Columns.TerminationReasonID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public DateTime? ArrivalDate {
			get { return GetColumnValue<DateTime?>(Columns.ArrivalDate); }
			set { SetColumnValue(Columns.ArrivalDate, value); }
		}
		[DataMember]
		public DateTime? QuitDate {
			get { return GetColumnValue<DateTime?>(Columns.QuitDate); }
			set { SetColumnValue(Columns.QuitDate, value); }
		}
		[DataMember]
		public string Reason {
			get { return GetColumnValue<string>(Columns.Reason); }
			set { SetColumnValue(Columns.Reason, value); }
		}
		[DataMember]
		public string TerminationType {
			get { return GetColumnValue<string>(Columns.TerminationType); }
			set { SetColumnValue(Columns.TerminationType, value); }
		}
		[DataMember]
		public string Note {
			get { return GetColumnValue<string>(Columns.Note); }
			set { SetColumnValue(Columns.Note, value); }
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
		}
		[DataMember]
		public DateTime? CreatedByDate {
			get { return GetColumnValue<DateTime?>(Columns.CreatedByDate); }
			set { SetColumnValue(Columns.CreatedByDate, value); }
		}
		[DataMember]
		public string CreatedByID {
			get { return GetColumnValue<string>(Columns.CreatedByID); }
			set { SetColumnValue(Columns.CreatedByID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return RosterID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn RosterIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TeamLocationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TerminationReasonIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn ArrivalDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn QuitDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ReasonColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TerminationTypeColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn NoteColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string RosterID = @"RosterID";
			public const string TeamLocationID = @"TeamLocationID";
			public const string TerminationReasonID = @"TerminationReasonID";
			public const string RecruitID = @"RecruitID";
			public const string ArrivalDate = @"ArrivalDate";
			public const string QuitDate = @"QuitDate";
			public const string Reason = @"Reason";
			public const string TerminationType = @"TerminationType";
			public const string Note = @"Note";
			public const string IsDeleted = @"IsDeleted";
			public const string CreatedByDate = @"CreatedByDate";
			public const string CreatedByID = @"CreatedByID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TechniciansView class.
	/// </summary>
	[DataContract]
	public partial class RU_TechniciansViewCollection : ReadOnlyList<RU_TechniciansView, RU_TechniciansViewCollection>
	{
		public static RU_TechniciansViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TechniciansViewCollection result = new RU_TechniciansViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_Technicians view.
	/// </summary>
	[DataContract]
	public partial class RU_TechniciansView : ReadOnlyRecord<RU_TechniciansView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_Technicians", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTechnicianId = new TableSchema.TableColumn(schema);
				colvarTechnicianId.ColumnName = "TechnicianId";
				colvarTechnicianId.DataType = DbType.String;
				colvarTechnicianId.MaxLength = 25;
				colvarTechnicianId.AutoIncrement = false;
				colvarTechnicianId.IsNullable = true;
				colvarTechnicianId.IsPrimaryKey = false;
				colvarTechnicianId.IsForeignKey = false;
				colvarTechnicianId.IsReadOnly = false;
				colvarTechnicianId.DefaultSetting = @"";
				colvarTechnicianId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechnicianId);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarTechFirstName = new TableSchema.TableColumn(schema);
				colvarTechFirstName.ColumnName = "TechFirstName";
				colvarTechFirstName.DataType = DbType.String;
				colvarTechFirstName.MaxLength = 50;
				colvarTechFirstName.AutoIncrement = false;
				colvarTechFirstName.IsNullable = true;
				colvarTechFirstName.IsPrimaryKey = false;
				colvarTechFirstName.IsForeignKey = false;
				colvarTechFirstName.IsReadOnly = false;
				colvarTechFirstName.DefaultSetting = @"";
				colvarTechFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechFirstName);

				TableSchema.TableColumn colvarTechLastName = new TableSchema.TableColumn(schema);
				colvarTechLastName.ColumnName = "TechLastName";
				colvarTechLastName.DataType = DbType.String;
				colvarTechLastName.MaxLength = 50;
				colvarTechLastName.AutoIncrement = false;
				colvarTechLastName.IsNullable = true;
				colvarTechLastName.IsPrimaryKey = false;
				colvarTechLastName.IsForeignKey = false;
				colvarTechLastName.IsReadOnly = false;
				colvarTechLastName.DefaultSetting = @"";
				colvarTechLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechLastName);

				TableSchema.TableColumn colvarTechBirthDate = new TableSchema.TableColumn(schema);
				colvarTechBirthDate.ColumnName = "TechBirthDate";
				colvarTechBirthDate.DataType = DbType.DateTime;
				colvarTechBirthDate.MaxLength = 0;
				colvarTechBirthDate.AutoIncrement = false;
				colvarTechBirthDate.IsNullable = true;
				colvarTechBirthDate.IsPrimaryKey = false;
				colvarTechBirthDate.IsForeignKey = false;
				colvarTechBirthDate.IsReadOnly = false;
				colvarTechBirthDate.DefaultSetting = @"";
				colvarTechBirthDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechBirthDate);

				TableSchema.TableColumn colvarTechSeasonId = new TableSchema.TableColumn(schema);
				colvarTechSeasonId.ColumnName = "TechSeasonId";
				colvarTechSeasonId.DataType = DbType.Int32;
				colvarTechSeasonId.MaxLength = 0;
				colvarTechSeasonId.AutoIncrement = false;
				colvarTechSeasonId.IsNullable = false;
				colvarTechSeasonId.IsPrimaryKey = false;
				colvarTechSeasonId.IsForeignKey = false;
				colvarTechSeasonId.IsReadOnly = false;
				colvarTechSeasonId.DefaultSetting = @"";
				colvarTechSeasonId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechSeasonId);

				TableSchema.TableColumn colvarTechSeasonName = new TableSchema.TableColumn(schema);
				colvarTechSeasonName.ColumnName = "TechSeasonName";
				colvarTechSeasonName.DataType = DbType.String;
				colvarTechSeasonName.MaxLength = 50;
				colvarTechSeasonName.AutoIncrement = false;
				colvarTechSeasonName.IsNullable = false;
				colvarTechSeasonName.IsPrimaryKey = false;
				colvarTechSeasonName.IsForeignKey = false;
				colvarTechSeasonName.IsReadOnly = false;
				colvarTechSeasonName.DefaultSetting = @"";
				colvarTechSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechSeasonName);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_Technicians",schema);
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
		public RU_TechniciansView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string TechnicianId {
			get { return GetColumnValue<string>(Columns.TechnicianId); }
			set { SetColumnValue(Columns.TechnicianId, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string TechFirstName {
			get { return GetColumnValue<string>(Columns.TechFirstName); }
			set { SetColumnValue(Columns.TechFirstName, value); }
		}
		[DataMember]
		public string TechLastName {
			get { return GetColumnValue<string>(Columns.TechLastName); }
			set { SetColumnValue(Columns.TechLastName, value); }
		}
		[DataMember]
		public DateTime? TechBirthDate {
			get { return GetColumnValue<DateTime?>(Columns.TechBirthDate); }
			set { SetColumnValue(Columns.TechBirthDate, value); }
		}
		[DataMember]
		public int TechSeasonId {
			get { return GetColumnValue<int>(Columns.TechSeasonId); }
			set { SetColumnValue(Columns.TechSeasonId, value); }
		}
		[DataMember]
		public string TechSeasonName {
			get { return GetColumnValue<string>(Columns.TechSeasonName); }
			set { SetColumnValue(Columns.TechSeasonName, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FullName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TechnicianIdColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TechFirstNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TechLastNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TechBirthDateColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TechSeasonIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TechSeasonNameColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TechnicianId = @"TechnicianId";
			public const string FullName = @"FullName";
			public const string TechFirstName = @"TechFirstName";
			public const string TechLastName = @"TechLastName";
			public const string TechBirthDate = @"TechBirthDate";
			public const string TechSeasonId = @"TechSeasonId";
			public const string TechSeasonName = @"TechSeasonName";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TerminationStatusCurrentStatusView class.
	/// </summary>
	[DataContract]
	public partial class RU_TerminationStatusCurrentStatusViewCollection : ReadOnlyList<RU_TerminationStatusCurrentStatusView, RU_TerminationStatusCurrentStatusViewCollection>
	{
		public static RU_TerminationStatusCurrentStatusViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TerminationStatusCurrentStatusViewCollection result = new RU_TerminationStatusCurrentStatusViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TerminationStatusCurrentStatus view.
	/// </summary>
	[DataContract]
	public partial class RU_TerminationStatusCurrentStatusView : ReadOnlyRecord<RU_TerminationStatusCurrentStatusView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TerminationStatusCurrentStatus", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTerminationStatusID = new TableSchema.TableColumn(schema);
				colvarTerminationStatusID.ColumnName = "TerminationStatusID";
				colvarTerminationStatusID.DataType = DbType.Int32;
				colvarTerminationStatusID.MaxLength = 0;
				colvarTerminationStatusID.AutoIncrement = true;
				colvarTerminationStatusID.IsNullable = false;
				colvarTerminationStatusID.IsPrimaryKey = false;
				colvarTerminationStatusID.IsForeignKey = false;
				colvarTerminationStatusID.IsReadOnly = false;
				colvarTerminationStatusID.DefaultSetting = @"";
				colvarTerminationStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationStatusID);

				TableSchema.TableColumn colvarTerminationID = new TableSchema.TableColumn(schema);
				colvarTerminationID.ColumnName = "TerminationID";
				colvarTerminationID.DataType = DbType.Int32;
				colvarTerminationID.MaxLength = 0;
				colvarTerminationID.AutoIncrement = false;
				colvarTerminationID.IsNullable = false;
				colvarTerminationID.IsPrimaryKey = false;
				colvarTerminationID.IsForeignKey = false;
				colvarTerminationID.IsReadOnly = false;
				colvarTerminationID.DefaultSetting = @"";
				colvarTerminationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationID);

				TableSchema.TableColumn colvarTerminationStatusCodeID = new TableSchema.TableColumn(schema);
				colvarTerminationStatusCodeID.ColumnName = "TerminationStatusCodeID";
				colvarTerminationStatusCodeID.DataType = DbType.Int32;
				colvarTerminationStatusCodeID.MaxLength = 0;
				colvarTerminationStatusCodeID.AutoIncrement = false;
				colvarTerminationStatusCodeID.IsNullable = false;
				colvarTerminationStatusCodeID.IsPrimaryKey = false;
				colvarTerminationStatusCodeID.IsForeignKey = false;
				colvarTerminationStatusCodeID.IsReadOnly = false;
				colvarTerminationStatusCodeID.DefaultSetting = @"";
				colvarTerminationStatusCodeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationStatusCodeID);

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

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
				colvarCreatedByID.AutoIncrement = false;
				colvarCreatedByID.IsNullable = false;
				colvarCreatedByID.IsPrimaryKey = false;
				colvarCreatedByID.IsForeignKey = false;
				colvarCreatedByID.IsReadOnly = false;
				colvarCreatedByID.DefaultSetting = @"";
				colvarCreatedByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedByID);

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

				TableSchema.TableColumn colvarRN = new TableSchema.TableColumn(schema);
				colvarRN.ColumnName = "RN";
				colvarRN.DataType = DbType.Int64;
				colvarRN.MaxLength = 0;
				colvarRN.AutoIncrement = false;
				colvarRN.IsNullable = true;
				colvarRN.IsPrimaryKey = false;
				colvarRN.IsForeignKey = false;
				colvarRN.IsReadOnly = false;
				colvarRN.DefaultSetting = @"";
				colvarRN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRN);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TerminationStatusCurrentStatus",schema);
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
		public RU_TerminationStatusCurrentStatusView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int TerminationStatusID {
			get { return GetColumnValue<int>(Columns.TerminationStatusID); }
			set { SetColumnValue(Columns.TerminationStatusID, value); }
		}
		[DataMember]
		public int TerminationID {
			get { return GetColumnValue<int>(Columns.TerminationID); }
			set { SetColumnValue(Columns.TerminationID, value); }
		}
		[DataMember]
		public int TerminationStatusCodeID {
			get { return GetColumnValue<int>(Columns.TerminationStatusCodeID); }
			set { SetColumnValue(Columns.TerminationStatusCodeID, value); }
		}
		[DataMember]
		public DateTime CreatedByDate {
			get { return GetColumnValue<DateTime>(Columns.CreatedByDate); }
			set { SetColumnValue(Columns.CreatedByDate, value); }
		}
		[DataMember]
		public string CreatedByID {
			get { return GetColumnValue<string>(Columns.CreatedByID); }
			set { SetColumnValue(Columns.CreatedByID, value); }
		}
		[DataMember]
		public string Comments {
			get { return GetColumnValue<string>(Columns.Comments); }
			set { SetColumnValue(Columns.Comments, value); }
		}
		[DataMember]
		public long? RN {
			get { return GetColumnValue<long?>(Columns.RN); }
			set { SetColumnValue(Columns.RN, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TerminationStatusID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TerminationStatusIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TerminationIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TerminationStatusCodeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CommentsColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RNColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TerminationStatusID = @"TerminationStatusID";
			public const string TerminationID = @"TerminationID";
			public const string TerminationStatusCodeID = @"TerminationStatusCodeID";
			public const string CreatedByDate = @"CreatedByDate";
			public const string CreatedByID = @"CreatedByID";
			public const string Comments = @"Comments";
			public const string RN = @"RN";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_TerminationsWithStatusView class.
	/// </summary>
	[DataContract]
	public partial class RU_TerminationsWithStatusViewCollection : ReadOnlyList<RU_TerminationsWithStatusView, RU_TerminationsWithStatusViewCollection>
	{
		public static RU_TerminationsWithStatusViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_TerminationsWithStatusViewCollection result = new RU_TerminationsWithStatusViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_TerminationsWithStatus view.
	/// </summary>
	[DataContract]
	public partial class RU_TerminationsWithStatusView : ReadOnlyRecord<RU_TerminationsWithStatusView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_TerminationsWithStatus", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTerminationID = new TableSchema.TableColumn(schema);
				colvarTerminationID.ColumnName = "TerminationID";
				colvarTerminationID.DataType = DbType.Int32;
				colvarTerminationID.MaxLength = 0;
				colvarTerminationID.AutoIncrement = false;
				colvarTerminationID.IsNullable = false;
				colvarTerminationID.IsPrimaryKey = false;
				colvarTerminationID.IsForeignKey = false;
				colvarTerminationID.IsReadOnly = false;
				colvarTerminationID.DefaultSetting = @"";
				colvarTerminationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationID);

				TableSchema.TableColumn colvarRecruitID = new TableSchema.TableColumn(schema);
				colvarRecruitID.ColumnName = "RecruitID";
				colvarRecruitID.DataType = DbType.Int32;
				colvarRecruitID.MaxLength = 0;
				colvarRecruitID.AutoIncrement = false;
				colvarRecruitID.IsNullable = false;
				colvarRecruitID.IsPrimaryKey = false;
				colvarRecruitID.IsForeignKey = false;
				colvarRecruitID.IsReadOnly = false;
				colvarRecruitID.DefaultSetting = @"";
				colvarRecruitID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitID);

				TableSchema.TableColumn colvarGPEmployeeID = new TableSchema.TableColumn(schema);
				colvarGPEmployeeID.ColumnName = "GPEmployeeID";
				colvarGPEmployeeID.DataType = DbType.String;
				colvarGPEmployeeID.MaxLength = 25;
				colvarGPEmployeeID.AutoIncrement = false;
				colvarGPEmployeeID.IsNullable = true;
				colvarGPEmployeeID.IsPrimaryKey = false;
				colvarGPEmployeeID.IsForeignKey = false;
				colvarGPEmployeeID.IsReadOnly = false;
				colvarGPEmployeeID.DefaultSetting = @"";
				colvarGPEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeID);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarTerminationCategory = new TableSchema.TableColumn(schema);
				colvarTerminationCategory.ColumnName = "TerminationCategory";
				colvarTerminationCategory.DataType = DbType.String;
				colvarTerminationCategory.MaxLength = 50;
				colvarTerminationCategory.AutoIncrement = false;
				colvarTerminationCategory.IsNullable = false;
				colvarTerminationCategory.IsPrimaryKey = false;
				colvarTerminationCategory.IsForeignKey = false;
				colvarTerminationCategory.IsReadOnly = false;
				colvarTerminationCategory.DefaultSetting = @"";
				colvarTerminationCategory.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationCategory);

				TableSchema.TableColumn colvarTerminationStatusCodeID = new TableSchema.TableColumn(schema);
				colvarTerminationStatusCodeID.ColumnName = "TerminationStatusCodeID";
				colvarTerminationStatusCodeID.DataType = DbType.Int32;
				colvarTerminationStatusCodeID.MaxLength = 0;
				colvarTerminationStatusCodeID.AutoIncrement = false;
				colvarTerminationStatusCodeID.IsNullable = false;
				colvarTerminationStatusCodeID.IsPrimaryKey = false;
				colvarTerminationStatusCodeID.IsForeignKey = false;
				colvarTerminationStatusCodeID.IsReadOnly = false;
				colvarTerminationStatusCodeID.DefaultSetting = @"";
				colvarTerminationStatusCodeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationStatusCodeID);

				TableSchema.TableColumn colvarTerminationStatusCode = new TableSchema.TableColumn(schema);
				colvarTerminationStatusCode.ColumnName = "TerminationStatusCode";
				colvarTerminationStatusCode.DataType = DbType.String;
				colvarTerminationStatusCode.MaxLength = 50;
				colvarTerminationStatusCode.AutoIncrement = false;
				colvarTerminationStatusCode.IsNullable = false;
				colvarTerminationStatusCode.IsPrimaryKey = false;
				colvarTerminationStatusCode.IsForeignKey = false;
				colvarTerminationStatusCode.IsReadOnly = false;
				colvarTerminationStatusCode.DefaultSetting = @"";
				colvarTerminationStatusCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTerminationStatusCode);

				TableSchema.TableColumn colvarLastDateWorked = new TableSchema.TableColumn(schema);
				colvarLastDateWorked.ColumnName = "LastDateWorked";
				colvarLastDateWorked.DataType = DbType.DateTime;
				colvarLastDateWorked.MaxLength = 0;
				colvarLastDateWorked.AutoIncrement = false;
				colvarLastDateWorked.IsNullable = true;
				colvarLastDateWorked.IsPrimaryKey = false;
				colvarLastDateWorked.IsForeignKey = false;
				colvarLastDateWorked.IsReadOnly = false;
				colvarLastDateWorked.DefaultSetting = @"";
				colvarLastDateWorked.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastDateWorked);

				TableSchema.TableColumn colvarCreatedByID = new TableSchema.TableColumn(schema);
				colvarCreatedByID.ColumnName = "CreatedByID";
				colvarCreatedByID.DataType = DbType.String;
				colvarCreatedByID.MaxLength = 50;
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

				TableSchema.TableColumn colvarOfficeName = new TableSchema.TableColumn(schema);
				colvarOfficeName.ColumnName = "OfficeName";
				colvarOfficeName.DataType = DbType.AnsiString;
				colvarOfficeName.MaxLength = 50;
				colvarOfficeName.AutoIncrement = false;
				colvarOfficeName.IsNullable = true;
				colvarOfficeName.IsPrimaryKey = false;
				colvarOfficeName.IsForeignKey = false;
				colvarOfficeName.IsReadOnly = false;
				colvarOfficeName.DefaultSetting = @"";
				colvarOfficeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeName);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_TerminationsWithStatus",schema);
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
		public RU_TerminationsWithStatusView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int TerminationID {
			get { return GetColumnValue<int>(Columns.TerminationID); }
			set { SetColumnValue(Columns.TerminationID, value); }
		}
		[DataMember]
		public int RecruitID {
			get { return GetColumnValue<int>(Columns.RecruitID); }
			set { SetColumnValue(Columns.RecruitID, value); }
		}
		[DataMember]
		public string GPEmployeeID {
			get { return GetColumnValue<string>(Columns.GPEmployeeID); }
			set { SetColumnValue(Columns.GPEmployeeID, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string TerminationCategory {
			get { return GetColumnValue<string>(Columns.TerminationCategory); }
			set { SetColumnValue(Columns.TerminationCategory, value); }
		}
		[DataMember]
		public int TerminationStatusCodeID {
			get { return GetColumnValue<int>(Columns.TerminationStatusCodeID); }
			set { SetColumnValue(Columns.TerminationStatusCodeID, value); }
		}
		[DataMember]
		public string TerminationStatusCode {
			get { return GetColumnValue<string>(Columns.TerminationStatusCode); }
			set { SetColumnValue(Columns.TerminationStatusCode, value); }
		}
		[DataMember]
		public DateTime? LastDateWorked {
			get { return GetColumnValue<DateTime?>(Columns.LastDateWorked); }
			set { SetColumnValue(Columns.LastDateWorked, value); }
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
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TerminationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn TerminationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn RecruitIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TerminationCategoryColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TerminationStatusCodeIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TerminationStatusCodeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn LastDateWorkedColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CreatedByIDColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreatedByDateColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TerminationID = @"TerminationID";
			public const string RecruitID = @"RecruitID";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string FullName = @"FullName";
			public const string TerminationCategory = @"TerminationCategory";
			public const string TerminationStatusCodeID = @"TerminationStatusCodeID";
			public const string TerminationStatusCode = @"TerminationStatusCode";
			public const string LastDateWorked = @"LastDateWorked";
			public const string CreatedByID = @"CreatedByID";
			public const string CreatedByDate = @"CreatedByDate";
			public const string OfficeName = @"OfficeName";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_UsersCallerIDView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersCallerIDViewCollection : ReadOnlyList<RU_UsersCallerIDView, RU_UsersCallerIDViewCollection>
	{
		public static RU_UsersCallerIDViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersCallerIDViewCollection result = new RU_UsersCallerIDViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersCallerID view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersCallerIDView : ReadOnlyRecord<RU_UsersCallerIDView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersCallerID", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarEmployeeID = new TableSchema.TableColumn(schema);
				colvarEmployeeID.ColumnName = "EmployeeID";
				colvarEmployeeID.DataType = DbType.String;
				colvarEmployeeID.MaxLength = 25;
				colvarEmployeeID.AutoIncrement = false;
				colvarEmployeeID.IsNullable = true;
				colvarEmployeeID.IsPrimaryKey = false;
				colvarEmployeeID.IsForeignKey = false;
				colvarEmployeeID.IsReadOnly = false;
				colvarEmployeeID.DefaultSetting = @"";
				colvarEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmployeeID);

				TableSchema.TableColumn colvarUserEmployeeTypeId = new TableSchema.TableColumn(schema);
				colvarUserEmployeeTypeId.ColumnName = "UserEmployeeTypeId";
				colvarUserEmployeeTypeId.DataType = DbType.AnsiString;
				colvarUserEmployeeTypeId.MaxLength = 20;
				colvarUserEmployeeTypeId.AutoIncrement = false;
				colvarUserEmployeeTypeId.IsNullable = false;
				colvarUserEmployeeTypeId.IsPrimaryKey = false;
				colvarUserEmployeeTypeId.IsForeignKey = false;
				colvarUserEmployeeTypeId.IsReadOnly = false;
				colvarUserEmployeeTypeId.DefaultSetting = @"";
				colvarUserEmployeeTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserEmployeeTypeId);

				TableSchema.TableColumn colvarCallerID = new TableSchema.TableColumn(schema);
				colvarCallerID.ColumnName = "CallerID";
				colvarCallerID.DataType = DbType.AnsiString;
				colvarCallerID.MaxLength = 20;
				colvarCallerID.AutoIncrement = false;
				colvarCallerID.IsNullable = true;
				colvarCallerID.IsPrimaryKey = false;
				colvarCallerID.IsForeignKey = false;
				colvarCallerID.IsReadOnly = false;
				colvarCallerID.DefaultSetting = @"";
				colvarCallerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCallerID);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersCallerID",schema);
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
		public RU_UsersCallerIDView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public string EmployeeID {
			get { return GetColumnValue<string>(Columns.EmployeeID); }
			set { SetColumnValue(Columns.EmployeeID, value); }
		}
		[DataMember]
		public string UserEmployeeTypeId {
			get { return GetColumnValue<string>(Columns.UserEmployeeTypeId); }
			set { SetColumnValue(Columns.UserEmployeeTypeId, value); }
		}
		[DataMember]
		public string CallerID {
			get { return GetColumnValue<string>(Columns.CallerID); }
			set { SetColumnValue(Columns.CallerID, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return EmployeeID;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn EmployeeIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn UserEmployeeTypeIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CallerIDColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string EmployeeID = @"EmployeeID";
			public const string UserEmployeeTypeId = @"UserEmployeeTypeId";
			public const string CallerID = @"CallerID";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_UsersTechView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersTechViewCollection : ReadOnlyList<RU_UsersTechView, RU_UsersTechViewCollection>
	{
		public static RU_UsersTechViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersTechViewCollection result = new RU_UsersTechViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersTech view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersTechView : ReadOnlyRecord<RU_UsersTechView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersTech", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarUserID = new TableSchema.TableColumn(schema);
				colvarUserID.ColumnName = "UserID";
				colvarUserID.DataType = DbType.Int32;
				colvarUserID.MaxLength = 0;
				colvarUserID.AutoIncrement = false;
				colvarUserID.IsNullable = false;
				colvarUserID.IsPrimaryKey = false;
				colvarUserID.IsForeignKey = false;
				colvarUserID.IsReadOnly = false;
				colvarUserID.DefaultSetting = @"";
				colvarUserID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserID);

				TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
				colvarFullName.ColumnName = "FullName";
				colvarFullName.DataType = DbType.String;
				colvarFullName.MaxLength = 101;
				colvarFullName.AutoIncrement = false;
				colvarFullName.IsNullable = true;
				colvarFullName.IsPrimaryKey = false;
				colvarFullName.IsForeignKey = false;
				colvarFullName.IsReadOnly = false;
				colvarFullName.DefaultSetting = @"";
				colvarFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullName);

				TableSchema.TableColumn colvarPublicFullName = new TableSchema.TableColumn(schema);
				colvarPublicFullName.ColumnName = "PublicFullName";
				colvarPublicFullName.DataType = DbType.String;
				colvarPublicFullName.MaxLength = 53;
				colvarPublicFullName.AutoIncrement = false;
				colvarPublicFullName.IsNullable = true;
				colvarPublicFullName.IsPrimaryKey = false;
				colvarPublicFullName.IsForeignKey = false;
				colvarPublicFullName.IsReadOnly = false;
				colvarPublicFullName.DefaultSetting = @"";
				colvarPublicFullName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPublicFullName);

				TableSchema.TableColumn colvarRecruitedById = new TableSchema.TableColumn(schema);
				colvarRecruitedById.ColumnName = "RecruitedById";
				colvarRecruitedById.DataType = DbType.Int32;
				colvarRecruitedById.MaxLength = 0;
				colvarRecruitedById.AutoIncrement = false;
				colvarRecruitedById.IsNullable = true;
				colvarRecruitedById.IsPrimaryKey = false;
				colvarRecruitedById.IsForeignKey = false;
				colvarRecruitedById.IsReadOnly = false;
				colvarRecruitedById.DefaultSetting = @"";
				colvarRecruitedById.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitedById);

				TableSchema.TableColumn colvarGPEmployeeId = new TableSchema.TableColumn(schema);
				colvarGPEmployeeId.ColumnName = "GPEmployeeId";
				colvarGPEmployeeId.DataType = DbType.String;
				colvarGPEmployeeId.MaxLength = 25;
				colvarGPEmployeeId.AutoIncrement = false;
				colvarGPEmployeeId.IsNullable = true;
				colvarGPEmployeeId.IsPrimaryKey = false;
				colvarGPEmployeeId.IsForeignKey = false;
				colvarGPEmployeeId.IsReadOnly = false;
				colvarGPEmployeeId.DefaultSetting = @"";
				colvarGPEmployeeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGPEmployeeId);

				TableSchema.TableColumn colvarUserEmployeeTypeId = new TableSchema.TableColumn(schema);
				colvarUserEmployeeTypeId.ColumnName = "UserEmployeeTypeId";
				colvarUserEmployeeTypeId.DataType = DbType.AnsiString;
				colvarUserEmployeeTypeId.MaxLength = 20;
				colvarUserEmployeeTypeId.AutoIncrement = false;
				colvarUserEmployeeTypeId.IsNullable = false;
				colvarUserEmployeeTypeId.IsPrimaryKey = false;
				colvarUserEmployeeTypeId.IsForeignKey = false;
				colvarUserEmployeeTypeId.IsReadOnly = false;
				colvarUserEmployeeTypeId.DefaultSetting = @"";
				colvarUserEmployeeTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserEmployeeTypeId);

				TableSchema.TableColumn colvarPermanentAddressId = new TableSchema.TableColumn(schema);
				colvarPermanentAddressId.ColumnName = "PermanentAddressId";
				colvarPermanentAddressId.DataType = DbType.Int32;
				colvarPermanentAddressId.MaxLength = 0;
				colvarPermanentAddressId.AutoIncrement = false;
				colvarPermanentAddressId.IsNullable = true;
				colvarPermanentAddressId.IsPrimaryKey = false;
				colvarPermanentAddressId.IsForeignKey = false;
				colvarPermanentAddressId.IsReadOnly = false;
				colvarPermanentAddressId.DefaultSetting = @"";
				colvarPermanentAddressId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermanentAddressId);

				TableSchema.TableColumn colvarSSN = new TableSchema.TableColumn(schema);
				colvarSSN.ColumnName = "SSN";
				colvarSSN.DataType = DbType.String;
				colvarSSN.MaxLength = 50;
				colvarSSN.AutoIncrement = false;
				colvarSSN.IsNullable = true;
				colvarSSN.IsPrimaryKey = false;
				colvarSSN.IsForeignKey = false;
				colvarSSN.IsReadOnly = false;
				colvarSSN.DefaultSetting = @"";
				colvarSSN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSSN);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = true;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarMiddleName = new TableSchema.TableColumn(schema);
				colvarMiddleName.ColumnName = "MiddleName";
				colvarMiddleName.DataType = DbType.String;
				colvarMiddleName.MaxLength = 50;
				colvarMiddleName.AutoIncrement = false;
				colvarMiddleName.IsNullable = true;
				colvarMiddleName.IsPrimaryKey = false;
				colvarMiddleName.IsForeignKey = false;
				colvarMiddleName.IsReadOnly = false;
				colvarMiddleName.DefaultSetting = @"";
				colvarMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddleName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = true;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarPreferredName = new TableSchema.TableColumn(schema);
				colvarPreferredName.ColumnName = "PreferredName";
				colvarPreferredName.DataType = DbType.String;
				colvarPreferredName.MaxLength = 50;
				colvarPreferredName.AutoIncrement = false;
				colvarPreferredName.IsNullable = true;
				colvarPreferredName.IsPrimaryKey = false;
				colvarPreferredName.IsForeignKey = false;
				colvarPreferredName.IsReadOnly = false;
				colvarPreferredName.DefaultSetting = @"";
				colvarPreferredName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreferredName);

				TableSchema.TableColumn colvarCompanyName = new TableSchema.TableColumn(schema);
				colvarCompanyName.ColumnName = "CompanyName";
				colvarCompanyName.DataType = DbType.String;
				colvarCompanyName.MaxLength = 50;
				colvarCompanyName.AutoIncrement = false;
				colvarCompanyName.IsNullable = true;
				colvarCompanyName.IsPrimaryKey = false;
				colvarCompanyName.IsForeignKey = false;
				colvarCompanyName.IsReadOnly = false;
				colvarCompanyName.DefaultSetting = @"";
				colvarCompanyName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompanyName);

				TableSchema.TableColumn colvarMaritalStatus = new TableSchema.TableColumn(schema);
				colvarMaritalStatus.ColumnName = "MaritalStatus";
				colvarMaritalStatus.DataType = DbType.Boolean;
				colvarMaritalStatus.MaxLength = 0;
				colvarMaritalStatus.AutoIncrement = false;
				colvarMaritalStatus.IsNullable = true;
				colvarMaritalStatus.IsPrimaryKey = false;
				colvarMaritalStatus.IsForeignKey = false;
				colvarMaritalStatus.IsReadOnly = false;
				colvarMaritalStatus.DefaultSetting = @"";
				colvarMaritalStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaritalStatus);

				TableSchema.TableColumn colvarSpouseName = new TableSchema.TableColumn(schema);
				colvarSpouseName.ColumnName = "SpouseName";
				colvarSpouseName.DataType = DbType.String;
				colvarSpouseName.MaxLength = 50;
				colvarSpouseName.AutoIncrement = false;
				colvarSpouseName.IsNullable = true;
				colvarSpouseName.IsPrimaryKey = false;
				colvarSpouseName.IsForeignKey = false;
				colvarSpouseName.IsReadOnly = false;
				colvarSpouseName.DefaultSetting = @"";
				colvarSpouseName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSpouseName);

				TableSchema.TableColumn colvarUserName = new TableSchema.TableColumn(schema);
				colvarUserName.ColumnName = "UserName";
				colvarUserName.DataType = DbType.String;
				colvarUserName.MaxLength = 50;
				colvarUserName.AutoIncrement = false;
				colvarUserName.IsNullable = false;
				colvarUserName.IsPrimaryKey = false;
				colvarUserName.IsForeignKey = false;
				colvarUserName.IsReadOnly = false;
				colvarUserName.DefaultSetting = @"";
				colvarUserName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserName);

				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.AnsiString;
				colvarPassword.MaxLength = 60;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);

				TableSchema.TableColumn colvarBirthDate = new TableSchema.TableColumn(schema);
				colvarBirthDate.ColumnName = "BirthDate";
				colvarBirthDate.DataType = DbType.DateTime;
				colvarBirthDate.MaxLength = 0;
				colvarBirthDate.AutoIncrement = false;
				colvarBirthDate.IsNullable = true;
				colvarBirthDate.IsPrimaryKey = false;
				colvarBirthDate.IsForeignKey = false;
				colvarBirthDate.IsReadOnly = false;
				colvarBirthDate.DefaultSetting = @"";
				colvarBirthDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBirthDate);

				TableSchema.TableColumn colvarHomeTown = new TableSchema.TableColumn(schema);
				colvarHomeTown.ColumnName = "HomeTown";
				colvarHomeTown.DataType = DbType.String;
				colvarHomeTown.MaxLength = 50;
				colvarHomeTown.AutoIncrement = false;
				colvarHomeTown.IsNullable = true;
				colvarHomeTown.IsPrimaryKey = false;
				colvarHomeTown.IsForeignKey = false;
				colvarHomeTown.IsReadOnly = false;
				colvarHomeTown.DefaultSetting = @"";
				colvarHomeTown.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHomeTown);

				TableSchema.TableColumn colvarBirthCity = new TableSchema.TableColumn(schema);
				colvarBirthCity.ColumnName = "BirthCity";
				colvarBirthCity.DataType = DbType.String;
				colvarBirthCity.MaxLength = 50;
				colvarBirthCity.AutoIncrement = false;
				colvarBirthCity.IsNullable = true;
				colvarBirthCity.IsPrimaryKey = false;
				colvarBirthCity.IsForeignKey = false;
				colvarBirthCity.IsReadOnly = false;
				colvarBirthCity.DefaultSetting = @"";
				colvarBirthCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBirthCity);

				TableSchema.TableColumn colvarBirthState = new TableSchema.TableColumn(schema);
				colvarBirthState.ColumnName = "BirthState";
				colvarBirthState.DataType = DbType.String;
				colvarBirthState.MaxLength = 50;
				colvarBirthState.AutoIncrement = false;
				colvarBirthState.IsNullable = true;
				colvarBirthState.IsPrimaryKey = false;
				colvarBirthState.IsForeignKey = false;
				colvarBirthState.IsReadOnly = false;
				colvarBirthState.DefaultSetting = @"";
				colvarBirthState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBirthState);

				TableSchema.TableColumn colvarBirthCountry = new TableSchema.TableColumn(schema);
				colvarBirthCountry.ColumnName = "BirthCountry";
				colvarBirthCountry.DataType = DbType.String;
				colvarBirthCountry.MaxLength = 50;
				colvarBirthCountry.AutoIncrement = false;
				colvarBirthCountry.IsNullable = true;
				colvarBirthCountry.IsPrimaryKey = false;
				colvarBirthCountry.IsForeignKey = false;
				colvarBirthCountry.IsReadOnly = false;
				colvarBirthCountry.DefaultSetting = @"";
				colvarBirthCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBirthCountry);

				TableSchema.TableColumn colvarSex = new TableSchema.TableColumn(schema);
				colvarSex.ColumnName = "Sex";
				colvarSex.DataType = DbType.Byte;
				colvarSex.MaxLength = 0;
				colvarSex.AutoIncrement = false;
				colvarSex.IsNullable = false;
				colvarSex.IsPrimaryKey = false;
				colvarSex.IsForeignKey = false;
				colvarSex.IsReadOnly = false;
				colvarSex.DefaultSetting = @"";
				colvarSex.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSex);

				TableSchema.TableColumn colvarShirtSize = new TableSchema.TableColumn(schema);
				colvarShirtSize.ColumnName = "ShirtSize";
				colvarShirtSize.DataType = DbType.Byte;
				colvarShirtSize.MaxLength = 0;
				colvarShirtSize.AutoIncrement = false;
				colvarShirtSize.IsNullable = true;
				colvarShirtSize.IsPrimaryKey = false;
				colvarShirtSize.IsForeignKey = false;
				colvarShirtSize.IsReadOnly = false;
				colvarShirtSize.DefaultSetting = @"";
				colvarShirtSize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShirtSize);

				TableSchema.TableColumn colvarHatSize = new TableSchema.TableColumn(schema);
				colvarHatSize.ColumnName = "HatSize";
				colvarHatSize.DataType = DbType.Byte;
				colvarHatSize.MaxLength = 0;
				colvarHatSize.AutoIncrement = false;
				colvarHatSize.IsNullable = true;
				colvarHatSize.IsPrimaryKey = false;
				colvarHatSize.IsForeignKey = false;
				colvarHatSize.IsReadOnly = false;
				colvarHatSize.DefaultSetting = @"";
				colvarHatSize.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHatSize);

				TableSchema.TableColumn colvarDLNumber = new TableSchema.TableColumn(schema);
				colvarDLNumber.ColumnName = "DLNumber";
				colvarDLNumber.DataType = DbType.String;
				colvarDLNumber.MaxLength = 50;
				colvarDLNumber.AutoIncrement = false;
				colvarDLNumber.IsNullable = true;
				colvarDLNumber.IsPrimaryKey = false;
				colvarDLNumber.IsForeignKey = false;
				colvarDLNumber.IsReadOnly = false;
				colvarDLNumber.DefaultSetting = @"";
				colvarDLNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLNumber);

				TableSchema.TableColumn colvarDLState = new TableSchema.TableColumn(schema);
				colvarDLState.ColumnName = "DLState";
				colvarDLState.DataType = DbType.String;
				colvarDLState.MaxLength = 50;
				colvarDLState.AutoIncrement = false;
				colvarDLState.IsNullable = true;
				colvarDLState.IsPrimaryKey = false;
				colvarDLState.IsForeignKey = false;
				colvarDLState.IsReadOnly = false;
				colvarDLState.DefaultSetting = @"";
				colvarDLState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLState);

				TableSchema.TableColumn colvarDLCountry = new TableSchema.TableColumn(schema);
				colvarDLCountry.ColumnName = "DLCountry";
				colvarDLCountry.DataType = DbType.String;
				colvarDLCountry.MaxLength = 50;
				colvarDLCountry.AutoIncrement = false;
				colvarDLCountry.IsNullable = true;
				colvarDLCountry.IsPrimaryKey = false;
				colvarDLCountry.IsForeignKey = false;
				colvarDLCountry.IsReadOnly = false;
				colvarDLCountry.DefaultSetting = @"";
				colvarDLCountry.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLCountry);

				TableSchema.TableColumn colvarDLExpiresOn = new TableSchema.TableColumn(schema);
				colvarDLExpiresOn.ColumnName = "DLExpiresOn";
				colvarDLExpiresOn.DataType = DbType.DateTime;
				colvarDLExpiresOn.MaxLength = 0;
				colvarDLExpiresOn.AutoIncrement = false;
				colvarDLExpiresOn.IsNullable = true;
				colvarDLExpiresOn.IsPrimaryKey = false;
				colvarDLExpiresOn.IsForeignKey = false;
				colvarDLExpiresOn.IsReadOnly = false;
				colvarDLExpiresOn.DefaultSetting = @"";
				colvarDLExpiresOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLExpiresOn);

				TableSchema.TableColumn colvarDLExpiration = new TableSchema.TableColumn(schema);
				colvarDLExpiration.ColumnName = "DLExpiration";
				colvarDLExpiration.DataType = DbType.String;
				colvarDLExpiration.MaxLength = 50;
				colvarDLExpiration.AutoIncrement = false;
				colvarDLExpiration.IsNullable = true;
				colvarDLExpiration.IsPrimaryKey = false;
				colvarDLExpiration.IsForeignKey = false;
				colvarDLExpiration.IsReadOnly = false;
				colvarDLExpiration.DefaultSetting = @"";
				colvarDLExpiration.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDLExpiration);

				TableSchema.TableColumn colvarHeight = new TableSchema.TableColumn(schema);
				colvarHeight.ColumnName = "Height";
				colvarHeight.DataType = DbType.String;
				colvarHeight.MaxLength = 10;
				colvarHeight.AutoIncrement = false;
				colvarHeight.IsNullable = true;
				colvarHeight.IsPrimaryKey = false;
				colvarHeight.IsForeignKey = false;
				colvarHeight.IsReadOnly = false;
				colvarHeight.DefaultSetting = @"";
				colvarHeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHeight);

				TableSchema.TableColumn colvarWeight = new TableSchema.TableColumn(schema);
				colvarWeight.ColumnName = "Weight";
				colvarWeight.DataType = DbType.String;
				colvarWeight.MaxLength = 10;
				colvarWeight.AutoIncrement = false;
				colvarWeight.IsNullable = true;
				colvarWeight.IsPrimaryKey = false;
				colvarWeight.IsForeignKey = false;
				colvarWeight.IsReadOnly = false;
				colvarWeight.DefaultSetting = @"";
				colvarWeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWeight);

				TableSchema.TableColumn colvarEyeColor = new TableSchema.TableColumn(schema);
				colvarEyeColor.ColumnName = "EyeColor";
				colvarEyeColor.DataType = DbType.String;
				colvarEyeColor.MaxLength = 20;
				colvarEyeColor.AutoIncrement = false;
				colvarEyeColor.IsNullable = true;
				colvarEyeColor.IsPrimaryKey = false;
				colvarEyeColor.IsForeignKey = false;
				colvarEyeColor.IsReadOnly = false;
				colvarEyeColor.DefaultSetting = @"";
				colvarEyeColor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEyeColor);

				TableSchema.TableColumn colvarHairColor = new TableSchema.TableColumn(schema);
				colvarHairColor.ColumnName = "HairColor";
				colvarHairColor.DataType = DbType.String;
				colvarHairColor.MaxLength = 20;
				colvarHairColor.AutoIncrement = false;
				colvarHairColor.IsNullable = true;
				colvarHairColor.IsPrimaryKey = false;
				colvarHairColor.IsForeignKey = false;
				colvarHairColor.IsReadOnly = false;
				colvarHairColor.DefaultSetting = @"";
				colvarHairColor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHairColor);

				TableSchema.TableColumn colvarPhoneHome = new TableSchema.TableColumn(schema);
				colvarPhoneHome.ColumnName = "PhoneHome";
				colvarPhoneHome.DataType = DbType.String;
				colvarPhoneHome.MaxLength = 25;
				colvarPhoneHome.AutoIncrement = false;
				colvarPhoneHome.IsNullable = true;
				colvarPhoneHome.IsPrimaryKey = false;
				colvarPhoneHome.IsForeignKey = false;
				colvarPhoneHome.IsReadOnly = false;
				colvarPhoneHome.DefaultSetting = @"";
				colvarPhoneHome.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneHome);

				TableSchema.TableColumn colvarPhoneCell = new TableSchema.TableColumn(schema);
				colvarPhoneCell.ColumnName = "PhoneCell";
				colvarPhoneCell.DataType = DbType.String;
				colvarPhoneCell.MaxLength = 50;
				colvarPhoneCell.AutoIncrement = false;
				colvarPhoneCell.IsNullable = true;
				colvarPhoneCell.IsPrimaryKey = false;
				colvarPhoneCell.IsForeignKey = false;
				colvarPhoneCell.IsReadOnly = false;
				colvarPhoneCell.DefaultSetting = @"";
				colvarPhoneCell.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCell);

				TableSchema.TableColumn colvarPhoneCellCarrierID = new TableSchema.TableColumn(schema);
				colvarPhoneCellCarrierID.ColumnName = "PhoneCellCarrierID";
				colvarPhoneCellCarrierID.DataType = DbType.Int16;
				colvarPhoneCellCarrierID.MaxLength = 0;
				colvarPhoneCellCarrierID.AutoIncrement = false;
				colvarPhoneCellCarrierID.IsNullable = true;
				colvarPhoneCellCarrierID.IsPrimaryKey = false;
				colvarPhoneCellCarrierID.IsForeignKey = false;
				colvarPhoneCellCarrierID.IsReadOnly = false;
				colvarPhoneCellCarrierID.DefaultSetting = @"";
				colvarPhoneCellCarrierID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneCellCarrierID);

				TableSchema.TableColumn colvarPhoneFax = new TableSchema.TableColumn(schema);
				colvarPhoneFax.ColumnName = "PhoneFax";
				colvarPhoneFax.DataType = DbType.String;
				colvarPhoneFax.MaxLength = 25;
				colvarPhoneFax.AutoIncrement = false;
				colvarPhoneFax.IsNullable = true;
				colvarPhoneFax.IsPrimaryKey = false;
				colvarPhoneFax.IsForeignKey = false;
				colvarPhoneFax.IsReadOnly = false;
				colvarPhoneFax.DefaultSetting = @"";
				colvarPhoneFax.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneFax);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 100;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarCorporateEmail = new TableSchema.TableColumn(schema);
				colvarCorporateEmail.ColumnName = "CorporateEmail";
				colvarCorporateEmail.DataType = DbType.String;
				colvarCorporateEmail.MaxLength = 100;
				colvarCorporateEmail.AutoIncrement = false;
				colvarCorporateEmail.IsNullable = true;
				colvarCorporateEmail.IsPrimaryKey = false;
				colvarCorporateEmail.IsForeignKey = false;
				colvarCorporateEmail.IsReadOnly = false;
				colvarCorporateEmail.DefaultSetting = @"";
				colvarCorporateEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCorporateEmail);

				TableSchema.TableColumn colvarTreeLevel = new TableSchema.TableColumn(schema);
				colvarTreeLevel.ColumnName = "TreeLevel";
				colvarTreeLevel.DataType = DbType.Int32;
				colvarTreeLevel.MaxLength = 0;
				colvarTreeLevel.AutoIncrement = false;
				colvarTreeLevel.IsNullable = true;
				colvarTreeLevel.IsPrimaryKey = false;
				colvarTreeLevel.IsForeignKey = false;
				colvarTreeLevel.IsReadOnly = false;
				colvarTreeLevel.DefaultSetting = @"";
				colvarTreeLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTreeLevel);

				TableSchema.TableColumn colvarHasVerifiedAddress = new TableSchema.TableColumn(schema);
				colvarHasVerifiedAddress.ColumnName = "HasVerifiedAddress";
				colvarHasVerifiedAddress.DataType = DbType.Boolean;
				colvarHasVerifiedAddress.MaxLength = 0;
				colvarHasVerifiedAddress.AutoIncrement = false;
				colvarHasVerifiedAddress.IsNullable = false;
				colvarHasVerifiedAddress.IsPrimaryKey = false;
				colvarHasVerifiedAddress.IsForeignKey = false;
				colvarHasVerifiedAddress.IsReadOnly = false;
				colvarHasVerifiedAddress.DefaultSetting = @"";
				colvarHasVerifiedAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHasVerifiedAddress);

				TableSchema.TableColumn colvarRightToWorkExpirationDate = new TableSchema.TableColumn(schema);
				colvarRightToWorkExpirationDate.ColumnName = "RightToWorkExpirationDate";
				colvarRightToWorkExpirationDate.DataType = DbType.DateTime;
				colvarRightToWorkExpirationDate.MaxLength = 0;
				colvarRightToWorkExpirationDate.AutoIncrement = false;
				colvarRightToWorkExpirationDate.IsNullable = true;
				colvarRightToWorkExpirationDate.IsPrimaryKey = false;
				colvarRightToWorkExpirationDate.IsForeignKey = false;
				colvarRightToWorkExpirationDate.IsReadOnly = false;
				colvarRightToWorkExpirationDate.DefaultSetting = @"";
				colvarRightToWorkExpirationDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRightToWorkExpirationDate);

				TableSchema.TableColumn colvarRightToWorkNotes = new TableSchema.TableColumn(schema);
				colvarRightToWorkNotes.ColumnName = "RightToWorkNotes";
				colvarRightToWorkNotes.DataType = DbType.String;
				colvarRightToWorkNotes.MaxLength = 250;
				colvarRightToWorkNotes.AutoIncrement = false;
				colvarRightToWorkNotes.IsNullable = true;
				colvarRightToWorkNotes.IsPrimaryKey = false;
				colvarRightToWorkNotes.IsForeignKey = false;
				colvarRightToWorkNotes.IsReadOnly = false;
				colvarRightToWorkNotes.DefaultSetting = @"";
				colvarRightToWorkNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRightToWorkNotes);

				TableSchema.TableColumn colvarRightToWorkStatusID = new TableSchema.TableColumn(schema);
				colvarRightToWorkStatusID.ColumnName = "RightToWorkStatusID";
				colvarRightToWorkStatusID.DataType = DbType.Int32;
				colvarRightToWorkStatusID.MaxLength = 0;
				colvarRightToWorkStatusID.AutoIncrement = false;
				colvarRightToWorkStatusID.IsNullable = true;
				colvarRightToWorkStatusID.IsPrimaryKey = false;
				colvarRightToWorkStatusID.IsForeignKey = false;
				colvarRightToWorkStatusID.IsReadOnly = false;
				colvarRightToWorkStatusID.DefaultSetting = @"";
				colvarRightToWorkStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRightToWorkStatusID);

				TableSchema.TableColumn colvarIsLocked = new TableSchema.TableColumn(schema);
				colvarIsLocked.ColumnName = "IsLocked";
				colvarIsLocked.DataType = DbType.Boolean;
				colvarIsLocked.MaxLength = 0;
				colvarIsLocked.AutoIncrement = false;
				colvarIsLocked.IsNullable = false;
				colvarIsLocked.IsPrimaryKey = false;
				colvarIsLocked.IsForeignKey = false;
				colvarIsLocked.IsReadOnly = false;
				colvarIsLocked.DefaultSetting = @"";
				colvarIsLocked.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsLocked);

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

				TableSchema.TableColumn colvarRecruitedDate = new TableSchema.TableColumn(schema);
				colvarRecruitedDate.ColumnName = "RecruitedDate";
				colvarRecruitedDate.DataType = DbType.DateTime;
				colvarRecruitedDate.MaxLength = 0;
				colvarRecruitedDate.AutoIncrement = false;
				colvarRecruitedDate.IsNullable = false;
				colvarRecruitedDate.IsPrimaryKey = false;
				colvarRecruitedDate.IsForeignKey = false;
				colvarRecruitedDate.IsReadOnly = false;
				colvarRecruitedDate.DefaultSetting = @"";
				colvarRecruitedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitedDate);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = true;
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
				colvarCreatedOn.IsNullable = true;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(schema);
				colvarModifiedBy.ColumnName = "ModifiedBy";
				colvarModifiedBy.DataType = DbType.String;
				colvarModifiedBy.MaxLength = 50;
				colvarModifiedBy.AutoIncrement = false;
				colvarModifiedBy.IsNullable = true;
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
				colvarModifiedOn.IsNullable = true;
				colvarModifiedOn.IsPrimaryKey = false;
				colvarModifiedOn.IsForeignKey = false;
				colvarModifiedOn.IsReadOnly = false;
				colvarModifiedOn.DefaultSetting = @"";
				colvarModifiedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarModifiedOn);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersTech",schema);
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
		public RU_UsersTechView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public int UserID {
			get { return GetColumnValue<int>(Columns.UserID); }
			set { SetColumnValue(Columns.UserID, value); }
		}
		[DataMember]
		public string FullName {
			get { return GetColumnValue<string>(Columns.FullName); }
			set { SetColumnValue(Columns.FullName, value); }
		}
		[DataMember]
		public string PublicFullName {
			get { return GetColumnValue<string>(Columns.PublicFullName); }
			set { SetColumnValue(Columns.PublicFullName, value); }
		}
		[DataMember]
		public int? RecruitedById {
			get { return GetColumnValue<int?>(Columns.RecruitedById); }
			set { SetColumnValue(Columns.RecruitedById, value); }
		}
		[DataMember]
		public string GPEmployeeId {
			get { return GetColumnValue<string>(Columns.GPEmployeeId); }
			set { SetColumnValue(Columns.GPEmployeeId, value); }
		}
		[DataMember]
		public string UserEmployeeTypeId {
			get { return GetColumnValue<string>(Columns.UserEmployeeTypeId); }
			set { SetColumnValue(Columns.UserEmployeeTypeId, value); }
		}
		[DataMember]
		public int? PermanentAddressId {
			get { return GetColumnValue<int?>(Columns.PermanentAddressId); }
			set { SetColumnValue(Columns.PermanentAddressId, value); }
		}
		[DataMember]
		public string SSN {
			get { return GetColumnValue<string>(Columns.SSN); }
			set { SetColumnValue(Columns.SSN, value); }
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		[DataMember]
		public string MiddleName {
			get { return GetColumnValue<string>(Columns.MiddleName); }
			set { SetColumnValue(Columns.MiddleName, value); }
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		[DataMember]
		public string PreferredName {
			get { return GetColumnValue<string>(Columns.PreferredName); }
			set { SetColumnValue(Columns.PreferredName, value); }
		}
		[DataMember]
		public string CompanyName {
			get { return GetColumnValue<string>(Columns.CompanyName); }
			set { SetColumnValue(Columns.CompanyName, value); }
		}
		[DataMember]
		public bool? MaritalStatus {
			get { return GetColumnValue<bool?>(Columns.MaritalStatus); }
			set { SetColumnValue(Columns.MaritalStatus, value); }
		}
		[DataMember]
		public string SpouseName {
			get { return GetColumnValue<string>(Columns.SpouseName); }
			set { SetColumnValue(Columns.SpouseName, value); }
		}
		[DataMember]
		public string UserName {
			get { return GetColumnValue<string>(Columns.UserName); }
			set { SetColumnValue(Columns.UserName, value); }
		}
		[DataMember]
		public string Password {
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		[DataMember]
		public DateTime? BirthDate {
			get { return GetColumnValue<DateTime?>(Columns.BirthDate); }
			set { SetColumnValue(Columns.BirthDate, value); }
		}
		[DataMember]
		public string HomeTown {
			get { return GetColumnValue<string>(Columns.HomeTown); }
			set { SetColumnValue(Columns.HomeTown, value); }
		}
		[DataMember]
		public string BirthCity {
			get { return GetColumnValue<string>(Columns.BirthCity); }
			set { SetColumnValue(Columns.BirthCity, value); }
		}
		[DataMember]
		public string BirthState {
			get { return GetColumnValue<string>(Columns.BirthState); }
			set { SetColumnValue(Columns.BirthState, value); }
		}
		[DataMember]
		public string BirthCountry {
			get { return GetColumnValue<string>(Columns.BirthCountry); }
			set { SetColumnValue(Columns.BirthCountry, value); }
		}
		[DataMember]
		public byte Sex {
			get { return GetColumnValue<byte>(Columns.Sex); }
			set { SetColumnValue(Columns.Sex, value); }
		}
		[DataMember]
		public byte? ShirtSize {
			get { return GetColumnValue<byte?>(Columns.ShirtSize); }
			set { SetColumnValue(Columns.ShirtSize, value); }
		}
		[DataMember]
		public byte? HatSize {
			get { return GetColumnValue<byte?>(Columns.HatSize); }
			set { SetColumnValue(Columns.HatSize, value); }
		}
		[DataMember]
		public string DLNumber {
			get { return GetColumnValue<string>(Columns.DLNumber); }
			set { SetColumnValue(Columns.DLNumber, value); }
		}
		[DataMember]
		public string DLState {
			get { return GetColumnValue<string>(Columns.DLState); }
			set { SetColumnValue(Columns.DLState, value); }
		}
		[DataMember]
		public string DLCountry {
			get { return GetColumnValue<string>(Columns.DLCountry); }
			set { SetColumnValue(Columns.DLCountry, value); }
		}
		[DataMember]
		public DateTime? DLExpiresOn {
			get { return GetColumnValue<DateTime?>(Columns.DLExpiresOn); }
			set { SetColumnValue(Columns.DLExpiresOn, value); }
		}
		[DataMember]
		public string DLExpiration {
			get { return GetColumnValue<string>(Columns.DLExpiration); }
			set { SetColumnValue(Columns.DLExpiration, value); }
		}
		[DataMember]
		public string Height {
			get { return GetColumnValue<string>(Columns.Height); }
			set { SetColumnValue(Columns.Height, value); }
		}
		[DataMember]
		public string Weight {
			get { return GetColumnValue<string>(Columns.Weight); }
			set { SetColumnValue(Columns.Weight, value); }
		}
		[DataMember]
		public string EyeColor {
			get { return GetColumnValue<string>(Columns.EyeColor); }
			set { SetColumnValue(Columns.EyeColor, value); }
		}
		[DataMember]
		public string HairColor {
			get { return GetColumnValue<string>(Columns.HairColor); }
			set { SetColumnValue(Columns.HairColor, value); }
		}
		[DataMember]
		public string PhoneHome {
			get { return GetColumnValue<string>(Columns.PhoneHome); }
			set { SetColumnValue(Columns.PhoneHome, value); }
		}
		[DataMember]
		public string PhoneCell {
			get { return GetColumnValue<string>(Columns.PhoneCell); }
			set { SetColumnValue(Columns.PhoneCell, value); }
		}
		[DataMember]
		public short? PhoneCellCarrierID {
			get { return GetColumnValue<short?>(Columns.PhoneCellCarrierID); }
			set { SetColumnValue(Columns.PhoneCellCarrierID, value); }
		}
		[DataMember]
		public string PhoneFax {
			get { return GetColumnValue<string>(Columns.PhoneFax); }
			set { SetColumnValue(Columns.PhoneFax, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public string CorporateEmail {
			get { return GetColumnValue<string>(Columns.CorporateEmail); }
			set { SetColumnValue(Columns.CorporateEmail, value); }
		}
		[DataMember]
		public int? TreeLevel {
			get { return GetColumnValue<int?>(Columns.TreeLevel); }
			set { SetColumnValue(Columns.TreeLevel, value); }
		}
		[DataMember]
		public bool HasVerifiedAddress {
			get { return GetColumnValue<bool>(Columns.HasVerifiedAddress); }
			set { SetColumnValue(Columns.HasVerifiedAddress, value); }
		}
		[DataMember]
		public DateTime? RightToWorkExpirationDate {
			get { return GetColumnValue<DateTime?>(Columns.RightToWorkExpirationDate); }
			set { SetColumnValue(Columns.RightToWorkExpirationDate, value); }
		}
		[DataMember]
		public string RightToWorkNotes {
			get { return GetColumnValue<string>(Columns.RightToWorkNotes); }
			set { SetColumnValue(Columns.RightToWorkNotes, value); }
		}
		[DataMember]
		public int? RightToWorkStatusID {
			get { return GetColumnValue<int?>(Columns.RightToWorkStatusID); }
			set { SetColumnValue(Columns.RightToWorkStatusID, value); }
		}
		[DataMember]
		public bool IsLocked {
			get { return GetColumnValue<bool>(Columns.IsLocked); }
			set { SetColumnValue(Columns.IsLocked, value); }
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
		public DateTime RecruitedDate {
			get { return GetColumnValue<DateTime>(Columns.RecruitedDate); }
			set { SetColumnValue(Columns.RecruitedDate, value); }
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		[DataMember]
		public DateTime? CreatedOn {
			get { return GetColumnValue<DateTime?>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		[DataMember]
		public string ModifiedBy {
			get { return GetColumnValue<string>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		[DataMember]
		public DateTime? ModifiedOn {
			get { return GetColumnValue<DateTime?>(Columns.ModifiedOn); }
			set { SetColumnValue(Columns.ModifiedOn, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FullName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FullNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn PublicFullNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn RecruitedByIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn GPEmployeeIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn UserEmployeeTypeIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PermanentAddressIdColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SSNColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PreferredNameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CompanyNameColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn MaritalStatusColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn SpouseNameColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn UserNameColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn PasswordColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn BirthDateColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn HomeTownColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn BirthCityColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn BirthStateColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn BirthCountryColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn SexColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn ShirtSizeColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn HatSizeColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn DLNumberColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn DLStateColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn DLCountryColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn DLExpiresOnColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn DLExpirationColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn HeightColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn WeightColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn EyeColorColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn HairColorColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn PhoneHomeColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn PhoneCellColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn PhoneCellCarrierIDColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn PhoneFaxColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn CorporateEmailColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn TreeLevelColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn HasVerifiedAddressColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn RightToWorkExpirationDateColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn RightToWorkNotesColumn
		{
			get { return Schema.Columns[43]; }
		}
		public static TableSchema.TableColumn RightToWorkStatusIDColumn
		{
			get { return Schema.Columns[44]; }
		}
		public static TableSchema.TableColumn IsLockedColumn
		{
			get { return Schema.Columns[45]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[46]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[47]; }
		}
		public static TableSchema.TableColumn RecruitedDateColumn
		{
			get { return Schema.Columns[48]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[49]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[50]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[51]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[52]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FullName = @"FullName";
			public const string PublicFullName = @"PublicFullName";
			public const string RecruitedById = @"RecruitedById";
			public const string GPEmployeeId = @"GPEmployeeId";
			public const string UserEmployeeTypeId = @"UserEmployeeTypeId";
			public const string PermanentAddressId = @"PermanentAddressId";
			public const string SSN = @"SSN";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PreferredName = @"PreferredName";
			public const string CompanyName = @"CompanyName";
			public const string MaritalStatus = @"MaritalStatus";
			public const string SpouseName = @"SpouseName";
			public const string UserName = @"UserName";
			public const string Password = @"Password";
			public const string BirthDate = @"BirthDate";
			public const string HomeTown = @"HomeTown";
			public const string BirthCity = @"BirthCity";
			public const string BirthState = @"BirthState";
			public const string BirthCountry = @"BirthCountry";
			public const string Sex = @"Sex";
			public const string ShirtSize = @"ShirtSize";
			public const string HatSize = @"HatSize";
			public const string DLNumber = @"DLNumber";
			public const string DLState = @"DLState";
			public const string DLCountry = @"DLCountry";
			public const string DLExpiresOn = @"DLExpiresOn";
			public const string DLExpiration = @"DLExpiration";
			public const string Height = @"Height";
			public const string Weight = @"Weight";
			public const string EyeColor = @"EyeColor";
			public const string HairColor = @"HairColor";
			public const string PhoneHome = @"PhoneHome";
			public const string PhoneCell = @"PhoneCell";
			public const string PhoneCellCarrierID = @"PhoneCellCarrierID";
			public const string PhoneFax = @"PhoneFax";
			public const string Email = @"Email";
			public const string CorporateEmail = @"CorporateEmail";
			public const string TreeLevel = @"TreeLevel";
			public const string HasVerifiedAddress = @"HasVerifiedAddress";
			public const string RightToWorkExpirationDate = @"RightToWorkExpirationDate";
			public const string RightToWorkNotes = @"RightToWorkNotes";
			public const string RightToWorkStatusID = @"RightToWorkStatusID";
			public const string IsLocked = @"IsLocked";
			public const string IsActive = @"IsActive";
			public const string IsDeleted = @"IsDeleted";
			public const string RecruitedDate = @"RecruitedDate";
			public const string CreatedBy = @"CreatedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string ModifiedBy = @"ModifiedBy";
			public const string ModifiedOn = @"ModifiedOn";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the TechniciansView class.
	/// </summary>
	[DataContract]
	public partial class TechniciansViewCollection : ReadOnlyList<TechniciansView, TechniciansViewCollection>
	{
		public static TechniciansViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			TechniciansViewCollection result = new TechniciansViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwTechnicians view.
	/// </summary>
	[DataContract]
	public partial class TechniciansView : ReadOnlyRecord<TechniciansView>
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
				TableSchema.Table schema = new TableSchema.Table("vwTechnicians", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTechId = new TableSchema.TableColumn(schema);
				colvarTechId.ColumnName = "TechId";
				colvarTechId.DataType = DbType.String;
				colvarTechId.MaxLength = 25;
				colvarTechId.AutoIncrement = false;
				colvarTechId.IsNullable = true;
				colvarTechId.IsPrimaryKey = false;
				colvarTechId.IsForeignKey = false;
				colvarTechId.IsReadOnly = false;
				colvarTechId.DefaultSetting = @"";
				colvarTechId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechId);

				TableSchema.TableColumn colvarTechFirstName = new TableSchema.TableColumn(schema);
				colvarTechFirstName.ColumnName = "TechFirstName";
				colvarTechFirstName.DataType = DbType.String;
				colvarTechFirstName.MaxLength = 50;
				colvarTechFirstName.AutoIncrement = false;
				colvarTechFirstName.IsNullable = true;
				colvarTechFirstName.IsPrimaryKey = false;
				colvarTechFirstName.IsForeignKey = false;
				colvarTechFirstName.IsReadOnly = false;
				colvarTechFirstName.DefaultSetting = @"";
				colvarTechFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechFirstName);

				TableSchema.TableColumn colvarTechLastName = new TableSchema.TableColumn(schema);
				colvarTechLastName.ColumnName = "TechLastName";
				colvarTechLastName.DataType = DbType.String;
				colvarTechLastName.MaxLength = 50;
				colvarTechLastName.AutoIncrement = false;
				colvarTechLastName.IsNullable = true;
				colvarTechLastName.IsPrimaryKey = false;
				colvarTechLastName.IsForeignKey = false;
				colvarTechLastName.IsReadOnly = false;
				colvarTechLastName.DefaultSetting = @"";
				colvarTechLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechLastName);

				TableSchema.TableColumn colvarTechBDay = new TableSchema.TableColumn(schema);
				colvarTechBDay.ColumnName = "TechBDay";
				colvarTechBDay.DataType = DbType.DateTime;
				colvarTechBDay.MaxLength = 0;
				colvarTechBDay.AutoIncrement = false;
				colvarTechBDay.IsNullable = true;
				colvarTechBDay.IsPrimaryKey = false;
				colvarTechBDay.IsForeignKey = false;
				colvarTechBDay.IsReadOnly = false;
				colvarTechBDay.DefaultSetting = @"";
				colvarTechBDay.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechBDay);

				TableSchema.TableColumn colvarTechSeasonId = new TableSchema.TableColumn(schema);
				colvarTechSeasonId.ColumnName = "TechSeasonId";
				colvarTechSeasonId.DataType = DbType.Int32;
				colvarTechSeasonId.MaxLength = 0;
				colvarTechSeasonId.AutoIncrement = false;
				colvarTechSeasonId.IsNullable = false;
				colvarTechSeasonId.IsPrimaryKey = false;
				colvarTechSeasonId.IsForeignKey = false;
				colvarTechSeasonId.IsReadOnly = false;
				colvarTechSeasonId.DefaultSetting = @"";
				colvarTechSeasonId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechSeasonId);

				TableSchema.TableColumn colvarTechSeasonName = new TableSchema.TableColumn(schema);
				colvarTechSeasonName.ColumnName = "TechSeasonName";
				colvarTechSeasonName.DataType = DbType.String;
				colvarTechSeasonName.MaxLength = 50;
				colvarTechSeasonName.AutoIncrement = false;
				colvarTechSeasonName.IsNullable = false;
				colvarTechSeasonName.IsPrimaryKey = false;
				colvarTechSeasonName.IsForeignKey = false;
				colvarTechSeasonName.IsReadOnly = false;
				colvarTechSeasonName.DefaultSetting = @"";
				colvarTechSeasonName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTechSeasonName);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwTechnicians",schema);
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
		public TechniciansView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public string TechId {
			get { return GetColumnValue<string>(Columns.TechId); }
			set { SetColumnValue(Columns.TechId, value); }
		}
		[DataMember]
		public string TechFirstName {
			get { return GetColumnValue<string>(Columns.TechFirstName); }
			set { SetColumnValue(Columns.TechFirstName, value); }
		}
		[DataMember]
		public string TechLastName {
			get { return GetColumnValue<string>(Columns.TechLastName); }
			set { SetColumnValue(Columns.TechLastName, value); }
		}
		[DataMember]
		public DateTime? TechBDay {
			get { return GetColumnValue<DateTime?>(Columns.TechBDay); }
			set { SetColumnValue(Columns.TechBDay, value); }
		}
		[DataMember]
		public int TechSeasonId {
			get { return GetColumnValue<int>(Columns.TechSeasonId); }
			set { SetColumnValue(Columns.TechSeasonId, value); }
		}
		[DataMember]
		public string TechSeasonName {
			get { return GetColumnValue<string>(Columns.TechSeasonName); }
			set { SetColumnValue(Columns.TechSeasonName, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return TechFirstName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TechIdColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TechFirstNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn TechLastNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TechBDayColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn TechSeasonIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TechSeasonNameColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string TechId = @"TechId";
			public const string TechFirstName = @"TechFirstName";
			public const string TechLastName = @"TechLastName";
			public const string TechBDay = @"TechBDay";
			public const string TechSeasonId = @"TechSeasonId";
			public const string TechSeasonName = @"TechSeasonName";
		}
		#endregion Columns Struct
	}
}
