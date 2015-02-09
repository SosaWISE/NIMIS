


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
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 50;
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
				colvarTeamType.IsNullable = true;
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
	/// Strongly-typed collection for the AE_CustomersGetCustomerInfoConnextView class.
	/// </summary>
	[DataContract]
	public partial class AE_CustomersGetCustomerInfoConnextViewCollection : ReadOnlyList<AE_CustomersGetCustomerInfoConnextView, AE_CustomersGetCustomerInfoConnextViewCollection>
	{
		public static AE_CustomersGetCustomerInfoConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			AE_CustomersGetCustomerInfoConnextViewCollection result = new AE_CustomersGetCustomerInfoConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwAE_CustomersGetCustomerInfoConnext view.
	/// </summary>
	[DataContract]
	public partial class AE_CustomersGetCustomerInfoConnextView : ReadOnlyRecord<AE_CustomersGetCustomerInfoConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwAE_CustomersGetCustomerInfoConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarCustomerMasterFileID = new TableSchema.TableColumn(schema);
				colvarCustomerMasterFileID.ColumnName = "CustomerMasterFileID";
				colvarCustomerMasterFileID.DataType = DbType.Int64;
				colvarCustomerMasterFileID.MaxLength = 0;
				colvarCustomerMasterFileID.AutoIncrement = false;
				colvarCustomerMasterFileID.IsNullable = false;
				colvarCustomerMasterFileID.IsPrimaryKey = false;
				colvarCustomerMasterFileID.IsForeignKey = false;
				colvarCustomerMasterFileID.IsReadOnly = false;
				colvarCustomerMasterFileID.DefaultSetting = @"";
				colvarCustomerMasterFileID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMasterFileID);

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
				colvarLastName.IsNullable = false;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarStreetAddress = new TableSchema.TableColumn(schema);
				colvarStreetAddress.ColumnName = "StreetAddress";
				colvarStreetAddress.DataType = DbType.String;
				colvarStreetAddress.MaxLength = 50;
				colvarStreetAddress.AutoIncrement = false;
				colvarStreetAddress.IsNullable = false;
				colvarStreetAddress.IsPrimaryKey = false;
				colvarStreetAddress.IsForeignKey = false;
				colvarStreetAddress.IsReadOnly = false;
				colvarStreetAddress.DefaultSetting = @"";
				colvarStreetAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress);

				TableSchema.TableColumn colvarStreetAddress2 = new TableSchema.TableColumn(schema);
				colvarStreetAddress2.ColumnName = "StreetAddress2";
				colvarStreetAddress2.DataType = DbType.String;
				colvarStreetAddress2.MaxLength = 50;
				colvarStreetAddress2.AutoIncrement = false;
				colvarStreetAddress2.IsNullable = true;
				colvarStreetAddress2.IsPrimaryKey = false;
				colvarStreetAddress2.IsForeignKey = false;
				colvarStreetAddress2.IsReadOnly = false;
				colvarStreetAddress2.DefaultSetting = @"";
				colvarStreetAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress2);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = false;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarState = new TableSchema.TableColumn(schema);
				colvarState.ColumnName = "State";
				colvarState.DataType = DbType.AnsiStringFixedLength;
				colvarState.MaxLength = 2;
				colvarState.AutoIncrement = false;
				colvarState.IsNullable = false;
				colvarState.IsPrimaryKey = false;
				colvarState.IsForeignKey = false;
				colvarState.IsReadOnly = false;
				colvarState.DefaultSetting = @"";
				colvarState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarState);

				TableSchema.TableColumn colvarPostalCode = new TableSchema.TableColumn(schema);
				colvarPostalCode.ColumnName = "PostalCode";
				colvarPostalCode.DataType = DbType.AnsiString;
				colvarPostalCode.MaxLength = 5;
				colvarPostalCode.AutoIncrement = false;
				colvarPostalCode.IsNullable = false;
				colvarPostalCode.IsPrimaryKey = false;
				colvarPostalCode.IsForeignKey = false;
				colvarPostalCode.IsReadOnly = false;
				colvarPostalCode.DefaultSetting = @"";
				colvarPostalCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPostalCode);

				TableSchema.TableColumn colvarPhoneHome = new TableSchema.TableColumn(schema);
				colvarPhoneHome.ColumnName = "PhoneHome";
				colvarPhoneHome.DataType = DbType.AnsiString;
				colvarPhoneHome.MaxLength = 20;
				colvarPhoneHome.AutoIncrement = false;
				colvarPhoneHome.IsNullable = false;
				colvarPhoneHome.IsPrimaryKey = false;
				colvarPhoneHome.IsForeignKey = false;
				colvarPhoneHome.IsReadOnly = false;
				colvarPhoneHome.DefaultSetting = @"";
				colvarPhoneHome.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneHome);

				TableSchema.TableColumn colvarPhoneWork = new TableSchema.TableColumn(schema);
				colvarPhoneWork.ColumnName = "PhoneWork";
				colvarPhoneWork.DataType = DbType.AnsiString;
				colvarPhoneWork.MaxLength = 30;
				colvarPhoneWork.AutoIncrement = false;
				colvarPhoneWork.IsNullable = false;
				colvarPhoneWork.IsPrimaryKey = false;
				colvarPhoneWork.IsForeignKey = false;
				colvarPhoneWork.IsReadOnly = false;
				colvarPhoneWork.DefaultSetting = @"";
				colvarPhoneWork.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneWork);

				TableSchema.TableColumn colvarPhoneMobile = new TableSchema.TableColumn(schema);
				colvarPhoneMobile.ColumnName = "PhoneMobile";
				colvarPhoneMobile.DataType = DbType.AnsiString;
				colvarPhoneMobile.MaxLength = 20;
				colvarPhoneMobile.AutoIncrement = false;
				colvarPhoneMobile.IsNullable = false;
				colvarPhoneMobile.IsPrimaryKey = false;
				colvarPhoneMobile.IsForeignKey = false;
				colvarPhoneMobile.IsReadOnly = false;
				colvarPhoneMobile.DefaultSetting = @"";
				colvarPhoneMobile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneMobile);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.AnsiString;
				colvarEmail.MaxLength = 256;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarContractDate = new TableSchema.TableColumn(schema);
				colvarContractDate.ColumnName = "ContractDate";
				colvarContractDate.DataType = DbType.DateTime;
				colvarContractDate.MaxLength = 0;
				colvarContractDate.AutoIncrement = false;
				colvarContractDate.IsNullable = true;
				colvarContractDate.IsPrimaryKey = false;
				colvarContractDate.IsForeignKey = false;
				colvarContractDate.IsReadOnly = false;
				colvarContractDate.DefaultSetting = @"";
				colvarContractDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractDate);

				TableSchema.TableColumn colvarAccountStatus = new TableSchema.TableColumn(schema);
				colvarAccountStatus.ColumnName = "AccountStatus";
				colvarAccountStatus.DataType = DbType.AnsiString;
				colvarAccountStatus.MaxLength = 20;
				colvarAccountStatus.AutoIncrement = false;
				colvarAccountStatus.IsNullable = true;
				colvarAccountStatus.IsPrimaryKey = false;
				colvarAccountStatus.IsForeignKey = false;
				colvarAccountStatus.IsReadOnly = false;
				colvarAccountStatus.DefaultSetting = @"";
				colvarAccountStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAccountStatus);

				TableSchema.TableColumn colvarTotalCommission = new TableSchema.TableColumn(schema);
				colvarTotalCommission.ColumnName = "TotalCommission";
				colvarTotalCommission.DataType = DbType.Currency;
				colvarTotalCommission.MaxLength = 0;
				colvarTotalCommission.AutoIncrement = false;
				colvarTotalCommission.IsNullable = true;
				colvarTotalCommission.IsPrimaryKey = false;
				colvarTotalCommission.IsForeignKey = false;
				colvarTotalCommission.IsReadOnly = false;
				colvarTotalCommission.DefaultSetting = @"";
				colvarTotalCommission.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalCommission);

				TableSchema.TableColumn colvarNumberReferralsMade = new TableSchema.TableColumn(schema);
				colvarNumberReferralsMade.ColumnName = "NumberReferralsMade";
				colvarNumberReferralsMade.DataType = DbType.Int32;
				colvarNumberReferralsMade.MaxLength = 0;
				colvarNumberReferralsMade.AutoIncrement = false;
				colvarNumberReferralsMade.IsNullable = true;
				colvarNumberReferralsMade.IsPrimaryKey = false;
				colvarNumberReferralsMade.IsForeignKey = false;
				colvarNumberReferralsMade.IsReadOnly = false;
				colvarNumberReferralsMade.DefaultSetting = @"";
				colvarNumberReferralsMade.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberReferralsMade);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwAE_CustomersGetCustomerInfoConnext",schema);
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
		public AE_CustomersGetCustomerInfoConnextView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long CustomerMasterFileID {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileID); }
			set { SetColumnValue(Columns.CustomerMasterFileID, value); }
		}
		[DataMember]
		public long CustomerID {
			get { return GetColumnValue<long>(Columns.CustomerID); }
			set { SetColumnValue(Columns.CustomerID, value); }
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
		public string StreetAddress {
			get { return GetColumnValue<string>(Columns.StreetAddress); }
			set { SetColumnValue(Columns.StreetAddress, value); }
		}
		[DataMember]
		public string StreetAddress2 {
			get { return GetColumnValue<string>(Columns.StreetAddress2); }
			set { SetColumnValue(Columns.StreetAddress2, value); }
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
		public string PostalCode {
			get { return GetColumnValue<string>(Columns.PostalCode); }
			set { SetColumnValue(Columns.PostalCode, value); }
		}
		[DataMember]
		public string PhoneHome {
			get { return GetColumnValue<string>(Columns.PhoneHome); }
			set { SetColumnValue(Columns.PhoneHome, value); }
		}
		[DataMember]
		public string PhoneWork {
			get { return GetColumnValue<string>(Columns.PhoneWork); }
			set { SetColumnValue(Columns.PhoneWork, value); }
		}
		[DataMember]
		public string PhoneMobile {
			get { return GetColumnValue<string>(Columns.PhoneMobile); }
			set { SetColumnValue(Columns.PhoneMobile, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public DateTime ContractDate {
			get { return GetColumnValue<DateTime>(Columns.ContractDate); }
			set { SetColumnValue(Columns.ContractDate, value); }
		}
		[DataMember]
		public string AccountStatus {
			get { return GetColumnValue<string>(Columns.AccountStatus); }
			set { SetColumnValue(Columns.AccountStatus, value); }
		}
		[DataMember]
		public decimal TotalCommission {
			get { return GetColumnValue<decimal>(Columns.TotalCommission); }
			set { SetColumnValue(Columns.TotalCommission, value); }
		}
		[DataMember]
		public int NumberReferralsMade {
			get { return GetColumnValue<int>(Columns.NumberReferralsMade); }
			set { SetColumnValue(Columns.NumberReferralsMade, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return CustomerMasterFileID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn CustomerMasterFileIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn CustomerIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn StreetAddressColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn StreetAddress2Column
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn StateColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn PostalCodeColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn PhoneHomeColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PhoneWorkColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PhoneMobileColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn ContractDateColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn AccountStatusColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn TotalCommissionColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn NumberReferralsMadeColumn
		{
			get { return Schema.Columns[17]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string CustomerMasterFileID = @"CustomerMasterFileID";
			public const string CustomerID = @"CustomerID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string StreetAddress = @"StreetAddress";
			public const string StreetAddress2 = @"StreetAddress2";
			public const string City = @"City";
			public const string State = @"State";
			public const string PostalCode = @"PostalCode";
			public const string PhoneHome = @"PhoneHome";
			public const string PhoneWork = @"PhoneWork";
			public const string PhoneMobile = @"PhoneMobile";
			public const string Email = @"Email";
			public const string ContractDate = @"ContractDate";
			public const string AccountStatus = @"AccountStatus";
			public const string TotalCommission = @"TotalCommission";
			public const string NumberReferralsMade = @"NumberReferralsMade";
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
	/// Strongly-typed collection for the RU_UsersAccountListConnextView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersAccountListConnextViewCollection : ReadOnlyList<RU_UsersAccountListConnextView, RU_UsersAccountListConnextViewCollection>
	{
		public static RU_UsersAccountListConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersAccountListConnextViewCollection result = new RU_UsersAccountListConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersAccountListConnext view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersAccountListConnextView : ReadOnlyRecord<RU_UsersAccountListConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersAccountListConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarCustomerFirstName = new TableSchema.TableColumn(schema);
				colvarCustomerFirstName.ColumnName = "CustomerFirstName";
				colvarCustomerFirstName.DataType = DbType.String;
				colvarCustomerFirstName.MaxLength = 50;
				colvarCustomerFirstName.AutoIncrement = false;
				colvarCustomerFirstName.IsNullable = false;
				colvarCustomerFirstName.IsPrimaryKey = false;
				colvarCustomerFirstName.IsForeignKey = false;
				colvarCustomerFirstName.IsReadOnly = false;
				colvarCustomerFirstName.DefaultSetting = @"";
				colvarCustomerFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerFirstName);

				TableSchema.TableColumn colvarCustomerMiddleName = new TableSchema.TableColumn(schema);
				colvarCustomerMiddleName.ColumnName = "CustomerMiddleName";
				colvarCustomerMiddleName.DataType = DbType.String;
				colvarCustomerMiddleName.MaxLength = 50;
				colvarCustomerMiddleName.AutoIncrement = false;
				colvarCustomerMiddleName.IsNullable = false;
				colvarCustomerMiddleName.IsPrimaryKey = false;
				colvarCustomerMiddleName.IsForeignKey = false;
				colvarCustomerMiddleName.IsReadOnly = false;
				colvarCustomerMiddleName.DefaultSetting = @"";
				colvarCustomerMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMiddleName);

				TableSchema.TableColumn colvarCustomerLastName = new TableSchema.TableColumn(schema);
				colvarCustomerLastName.ColumnName = "CustomerLastName";
				colvarCustomerLastName.DataType = DbType.String;
				colvarCustomerLastName.MaxLength = 50;
				colvarCustomerLastName.AutoIncrement = false;
				colvarCustomerLastName.IsNullable = false;
				colvarCustomerLastName.IsPrimaryKey = false;
				colvarCustomerLastName.IsForeignKey = false;
				colvarCustomerLastName.IsReadOnly = false;
				colvarCustomerLastName.DefaultSetting = @"";
				colvarCustomerLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerLastName);

				TableSchema.TableColumn colvarContractDate = new TableSchema.TableColumn(schema);
				colvarContractDate.ColumnName = "ContractDate";
				colvarContractDate.DataType = DbType.DateTime;
				colvarContractDate.MaxLength = 0;
				colvarContractDate.AutoIncrement = false;
				colvarContractDate.IsNullable = true;
				colvarContractDate.IsPrimaryKey = false;
				colvarContractDate.IsForeignKey = false;
				colvarContractDate.IsReadOnly = false;
				colvarContractDate.DefaultSetting = @"";
				colvarContractDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractDate);

				TableSchema.TableColumn colvarCreditQuality = new TableSchema.TableColumn(schema);
				colvarCreditQuality.ColumnName = "CreditQuality";
				colvarCreditQuality.DataType = DbType.AnsiString;
				colvarCreditQuality.MaxLength = 20;
				colvarCreditQuality.AutoIncrement = false;
				colvarCreditQuality.IsNullable = true;
				colvarCreditQuality.IsPrimaryKey = false;
				colvarCreditQuality.IsForeignKey = false;
				colvarCreditQuality.IsReadOnly = false;
				colvarCreditQuality.DefaultSetting = @"";
				colvarCreditQuality.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreditQuality);

				TableSchema.TableColumn colvarActivationFee = new TableSchema.TableColumn(schema);
				colvarActivationFee.ColumnName = "ActivationFee";
				colvarActivationFee.DataType = DbType.Currency;
				colvarActivationFee.MaxLength = 0;
				colvarActivationFee.AutoIncrement = false;
				colvarActivationFee.IsNullable = true;
				colvarActivationFee.IsPrimaryKey = false;
				colvarActivationFee.IsForeignKey = false;
				colvarActivationFee.IsReadOnly = false;
				colvarActivationFee.DefaultSetting = @"";
				colvarActivationFee.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActivationFee);

				TableSchema.TableColumn colvarContractLength = new TableSchema.TableColumn(schema);
				colvarContractLength.ColumnName = "ContractLength";
				colvarContractLength.DataType = DbType.Int32;
				colvarContractLength.MaxLength = 0;
				colvarContractLength.AutoIncrement = false;
				colvarContractLength.IsNullable = true;
				colvarContractLength.IsPrimaryKey = false;
				colvarContractLength.IsForeignKey = false;
				colvarContractLength.IsReadOnly = false;
				colvarContractLength.DefaultSetting = @"";
				colvarContractLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractLength);

				TableSchema.TableColumn colvarServiceType = new TableSchema.TableColumn(schema);
				colvarServiceType.ColumnName = "ServiceType";
				colvarServiceType.DataType = DbType.AnsiString;
				colvarServiceType.MaxLength = 50;
				colvarServiceType.AutoIncrement = false;
				colvarServiceType.IsNullable = true;
				colvarServiceType.IsPrimaryKey = false;
				colvarServiceType.IsForeignKey = false;
				colvarServiceType.IsReadOnly = false;
				colvarServiceType.DefaultSetting = @"";
				colvarServiceType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceType);

				TableSchema.TableColumn colvarMonthlyPayment = new TableSchema.TableColumn(schema);
				colvarMonthlyPayment.ColumnName = "MonthlyPayment";
				colvarMonthlyPayment.DataType = DbType.Currency;
				colvarMonthlyPayment.MaxLength = 0;
				colvarMonthlyPayment.AutoIncrement = false;
				colvarMonthlyPayment.IsNullable = true;
				colvarMonthlyPayment.IsPrimaryKey = false;
				colvarMonthlyPayment.IsForeignKey = false;
				colvarMonthlyPayment.IsReadOnly = false;
				colvarMonthlyPayment.DefaultSetting = @"";
				colvarMonthlyPayment.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonthlyPayment);

				TableSchema.TableColumn colvarPaymentMethod = new TableSchema.TableColumn(schema);
				colvarPaymentMethod.ColumnName = "PaymentMethod";
				colvarPaymentMethod.DataType = DbType.AnsiString;
				colvarPaymentMethod.MaxLength = 50;
				colvarPaymentMethod.AutoIncrement = false;
				colvarPaymentMethod.IsNullable = true;
				colvarPaymentMethod.IsPrimaryKey = false;
				colvarPaymentMethod.IsForeignKey = false;
				colvarPaymentMethod.IsReadOnly = false;
				colvarPaymentMethod.DefaultSetting = @"";
				colvarPaymentMethod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPaymentMethod);

				TableSchema.TableColumn colvarTotalCommission = new TableSchema.TableColumn(schema);
				colvarTotalCommission.ColumnName = "TotalCommission";
				colvarTotalCommission.DataType = DbType.Currency;
				colvarTotalCommission.MaxLength = 0;
				colvarTotalCommission.AutoIncrement = false;
				colvarTotalCommission.IsNullable = true;
				colvarTotalCommission.IsPrimaryKey = false;
				colvarTotalCommission.IsForeignKey = false;
				colvarTotalCommission.IsReadOnly = false;
				colvarTotalCommission.DefaultSetting = @"";
				colvarTotalCommission.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalCommission);

				TableSchema.TableColumn colvarisActive = new TableSchema.TableColumn(schema);
				colvarisActive.ColumnName = "isActive";
				colvarisActive.DataType = DbType.Boolean;
				colvarisActive.MaxLength = 0;
				colvarisActive.AutoIncrement = false;
				colvarisActive.IsNullable = false;
				colvarisActive.IsPrimaryKey = false;
				colvarisActive.IsForeignKey = false;
				colvarisActive.IsReadOnly = false;
				colvarisActive.DefaultSetting = @"";
				colvarisActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarisActive);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersAccountListConnext",schema);
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
		public RU_UsersAccountListConnextView()
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
		public long CustomerID {
			get { return GetColumnValue<long>(Columns.CustomerID); }
			set { SetColumnValue(Columns.CustomerID, value); }
		}
		[DataMember]
		public string CustomerFirstName {
			get { return GetColumnValue<string>(Columns.CustomerFirstName); }
			set { SetColumnValue(Columns.CustomerFirstName, value); }
		}
		[DataMember]
		public string CustomerMiddleName {
			get { return GetColumnValue<string>(Columns.CustomerMiddleName); }
			set { SetColumnValue(Columns.CustomerMiddleName, value); }
		}
		[DataMember]
		public string CustomerLastName {
			get { return GetColumnValue<string>(Columns.CustomerLastName); }
			set { SetColumnValue(Columns.CustomerLastName, value); }
		}
		[DataMember]
		public DateTime ContractDate {
			get { return GetColumnValue<DateTime>(Columns.ContractDate); }
			set { SetColumnValue(Columns.ContractDate, value); }
		}
		[DataMember]
		public string CreditQuality {
			get { return GetColumnValue<string>(Columns.CreditQuality); }
			set { SetColumnValue(Columns.CreditQuality, value); }
		}
		[DataMember]
		public decimal ActivationFee {
			get { return GetColumnValue<decimal>(Columns.ActivationFee); }
			set { SetColumnValue(Columns.ActivationFee, value); }
		}
		[DataMember]
		public int ContractLength {
			get { return GetColumnValue<int>(Columns.ContractLength); }
			set { SetColumnValue(Columns.ContractLength, value); }
		}
		[DataMember]
		public string ServiceType {
			get { return GetColumnValue<string>(Columns.ServiceType); }
			set { SetColumnValue(Columns.ServiceType, value); }
		}
		[DataMember]
		public decimal MonthlyPayment {
			get { return GetColumnValue<decimal>(Columns.MonthlyPayment); }
			set { SetColumnValue(Columns.MonthlyPayment, value); }
		}
		[DataMember]
		public string PaymentMethod {
			get { return GetColumnValue<string>(Columns.PaymentMethod); }
			set { SetColumnValue(Columns.PaymentMethod, value); }
		}
		[DataMember]
		public decimal TotalCommission {
			get { return GetColumnValue<decimal>(Columns.TotalCommission); }
			set { SetColumnValue(Columns.TotalCommission, value); }
		}
		[DataMember]
		public bool isActive {
			get { return GetColumnValue<bool>(Columns.isActive); }
			set { SetColumnValue(Columns.isActive, value); }
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
		public static TableSchema.TableColumn CustomerIDColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CustomerFirstNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CustomerMiddleNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CustomerLastNameColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ContractDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CreditQualityColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn ActivationFeeColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn ContractLengthColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn ServiceTypeColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn MonthlyPaymentColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PaymentMethodColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn TotalCommissionColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn isActiveColumn
		{
			get { return Schema.Columns[13]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string CustomerID = @"CustomerID";
			public const string CustomerFirstName = @"CustomerFirstName";
			public const string CustomerMiddleName = @"CustomerMiddleName";
			public const string CustomerLastName = @"CustomerLastName";
			public const string ContractDate = @"ContractDate";
			public const string CreditQuality = @"CreditQuality";
			public const string ActivationFee = @"ActivationFee";
			public const string ContractLength = @"ContractLength";
			public const string ServiceType = @"ServiceType";
			public const string MonthlyPayment = @"MonthlyPayment";
			public const string PaymentMethod = @"PaymentMethod";
			public const string TotalCommission = @"TotalCommission";
			public const string isActive = @"isActive";
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
	/// Strongly-typed collection for the RU_UsersGetDetailedStatisticsConnextView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersGetDetailedStatisticsConnextViewCollection : ReadOnlyList<RU_UsersGetDetailedStatisticsConnextView, RU_UsersGetDetailedStatisticsConnextViewCollection>
	{
		public static RU_UsersGetDetailedStatisticsConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersGetDetailedStatisticsConnextViewCollection result = new RU_UsersGetDetailedStatisticsConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersGetDetailedStatisticsConnext view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersGetDetailedStatisticsConnextView : ReadOnlyRecord<RU_UsersGetDetailedStatisticsConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersGetDetailedStatisticsConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarSalesYear = new TableSchema.TableColumn(schema);
				colvarSalesYear.ColumnName = "SalesYear";
				colvarSalesYear.DataType = DbType.Int32;
				colvarSalesYear.MaxLength = 0;
				colvarSalesYear.AutoIncrement = false;
				colvarSalesYear.IsNullable = true;
				colvarSalesYear.IsPrimaryKey = false;
				colvarSalesYear.IsForeignKey = false;
				colvarSalesYear.IsReadOnly = false;
				colvarSalesYear.DefaultSetting = @"";
				colvarSalesYear.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesYear);

				TableSchema.TableColumn colvarSalesMonth = new TableSchema.TableColumn(schema);
				colvarSalesMonth.ColumnName = "SalesMonth";
				colvarSalesMonth.DataType = DbType.Int32;
				colvarSalesMonth.MaxLength = 0;
				colvarSalesMonth.AutoIncrement = false;
				colvarSalesMonth.IsNullable = true;
				colvarSalesMonth.IsPrimaryKey = false;
				colvarSalesMonth.IsForeignKey = false;
				colvarSalesMonth.IsReadOnly = false;
				colvarSalesMonth.DefaultSetting = @"";
				colvarSalesMonth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesMonth);

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

				TableSchema.TableColumn colvarRegionName = new TableSchema.TableColumn(schema);
				colvarRegionName.ColumnName = "RegionName";
				colvarRegionName.DataType = DbType.AnsiString;
				colvarRegionName.MaxLength = 50;
				colvarRegionName.AutoIncrement = false;
				colvarRegionName.IsNullable = false;
				colvarRegionName.IsPrimaryKey = false;
				colvarRegionName.IsForeignKey = false;
				colvarRegionName.IsReadOnly = false;
				colvarRegionName.DefaultSetting = @"";
				colvarRegionName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionName);

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

				TableSchema.TableColumn colvarOfficeID = new TableSchema.TableColumn(schema);
				colvarOfficeID.ColumnName = "OfficeID";
				colvarOfficeID.DataType = DbType.Int32;
				colvarOfficeID.MaxLength = 0;
				colvarOfficeID.AutoIncrement = false;
				colvarOfficeID.IsNullable = true;
				colvarOfficeID.IsPrimaryKey = false;
				colvarOfficeID.IsForeignKey = false;
				colvarOfficeID.IsReadOnly = false;
				colvarOfficeID.DefaultSetting = @"";
				colvarOfficeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeID);

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

				TableSchema.TableColumn colvarHasRecruits = new TableSchema.TableColumn(schema);
				colvarHasRecruits.ColumnName = "HasRecruits";
				colvarHasRecruits.DataType = DbType.Boolean;
				colvarHasRecruits.MaxLength = 0;
				colvarHasRecruits.AutoIncrement = false;
				colvarHasRecruits.IsNullable = true;
				colvarHasRecruits.IsPrimaryKey = false;
				colvarHasRecruits.IsForeignKey = false;
				colvarHasRecruits.IsReadOnly = false;
				colvarHasRecruits.DefaultSetting = @"";
				colvarHasRecruits.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHasRecruits);

				TableSchema.TableColumn colvarNumberCreditReportsPulled = new TableSchema.TableColumn(schema);
				colvarNumberCreditReportsPulled.ColumnName = "NumberCreditReportsPulled";
				colvarNumberCreditReportsPulled.DataType = DbType.Int32;
				colvarNumberCreditReportsPulled.MaxLength = 0;
				colvarNumberCreditReportsPulled.AutoIncrement = false;
				colvarNumberCreditReportsPulled.IsNullable = true;
				colvarNumberCreditReportsPulled.IsPrimaryKey = false;
				colvarNumberCreditReportsPulled.IsForeignKey = false;
				colvarNumberCreditReportsPulled.IsReadOnly = false;
				colvarNumberCreditReportsPulled.DefaultSetting = @"";
				colvarNumberCreditReportsPulled.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberCreditReportsPulled);

				TableSchema.TableColumn colvarNumberCreditsPassed = new TableSchema.TableColumn(schema);
				colvarNumberCreditsPassed.ColumnName = "NumberCreditsPassed";
				colvarNumberCreditsPassed.DataType = DbType.Int32;
				colvarNumberCreditsPassed.MaxLength = 0;
				colvarNumberCreditsPassed.AutoIncrement = false;
				colvarNumberCreditsPassed.IsNullable = true;
				colvarNumberCreditsPassed.IsPrimaryKey = false;
				colvarNumberCreditsPassed.IsForeignKey = false;
				colvarNumberCreditsPassed.IsReadOnly = false;
				colvarNumberCreditsPassed.DefaultSetting = @"";
				colvarNumberCreditsPassed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberCreditsPassed);

				TableSchema.TableColumn colvarNumberExcellentCreditScores = new TableSchema.TableColumn(schema);
				colvarNumberExcellentCreditScores.ColumnName = "NumberExcellentCreditScores";
				colvarNumberExcellentCreditScores.DataType = DbType.Int32;
				colvarNumberExcellentCreditScores.MaxLength = 0;
				colvarNumberExcellentCreditScores.AutoIncrement = false;
				colvarNumberExcellentCreditScores.IsNullable = true;
				colvarNumberExcellentCreditScores.IsPrimaryKey = false;
				colvarNumberExcellentCreditScores.IsForeignKey = false;
				colvarNumberExcellentCreditScores.IsReadOnly = false;
				colvarNumberExcellentCreditScores.DefaultSetting = @"";
				colvarNumberExcellentCreditScores.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberExcellentCreditScores);

				TableSchema.TableColumn colvarNumberGoodCreditScores = new TableSchema.TableColumn(schema);
				colvarNumberGoodCreditScores.ColumnName = "NumberGoodCreditScores";
				colvarNumberGoodCreditScores.DataType = DbType.Int32;
				colvarNumberGoodCreditScores.MaxLength = 0;
				colvarNumberGoodCreditScores.AutoIncrement = false;
				colvarNumberGoodCreditScores.IsNullable = true;
				colvarNumberGoodCreditScores.IsPrimaryKey = false;
				colvarNumberGoodCreditScores.IsForeignKey = false;
				colvarNumberGoodCreditScores.IsReadOnly = false;
				colvarNumberGoodCreditScores.DefaultSetting = @"";
				colvarNumberGoodCreditScores.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberGoodCreditScores);

				TableSchema.TableColumn colvarNumberBadCreditScores = new TableSchema.TableColumn(schema);
				colvarNumberBadCreditScores.ColumnName = "NumberBadCreditScores";
				colvarNumberBadCreditScores.DataType = DbType.Int32;
				colvarNumberBadCreditScores.MaxLength = 0;
				colvarNumberBadCreditScores.AutoIncrement = false;
				colvarNumberBadCreditScores.IsNullable = true;
				colvarNumberBadCreditScores.IsPrimaryKey = false;
				colvarNumberBadCreditScores.IsForeignKey = false;
				colvarNumberBadCreditScores.IsReadOnly = false;
				colvarNumberBadCreditScores.DefaultSetting = @"";
				colvarNumberBadCreditScores.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberBadCreditScores);

				TableSchema.TableColumn colvarAverageCreditScore = new TableSchema.TableColumn(schema);
				colvarAverageCreditScore.ColumnName = "AverageCreditScore";
				colvarAverageCreditScore.DataType = DbType.Int32;
				colvarAverageCreditScore.MaxLength = 0;
				colvarAverageCreditScore.AutoIncrement = false;
				colvarAverageCreditScore.IsNullable = true;
				colvarAverageCreditScore.IsPrimaryKey = false;
				colvarAverageCreditScore.IsForeignKey = false;
				colvarAverageCreditScore.IsReadOnly = false;
				colvarAverageCreditScore.DefaultSetting = @"";
				colvarAverageCreditScore.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAverageCreditScore);

				TableSchema.TableColumn colvarCreditPassPercentage = new TableSchema.TableColumn(schema);
				colvarCreditPassPercentage.ColumnName = "CreditPassPercentage";
				colvarCreditPassPercentage.DataType = DbType.Decimal;
				colvarCreditPassPercentage.MaxLength = 0;
				colvarCreditPassPercentage.AutoIncrement = false;
				colvarCreditPassPercentage.IsNullable = true;
				colvarCreditPassPercentage.IsPrimaryKey = false;
				colvarCreditPassPercentage.IsForeignKey = false;
				colvarCreditPassPercentage.IsReadOnly = false;
				colvarCreditPassPercentage.DefaultSetting = @"";
				colvarCreditPassPercentage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreditPassPercentage);

				TableSchema.TableColumn colvarPassAndInstallPercentage = new TableSchema.TableColumn(schema);
				colvarPassAndInstallPercentage.ColumnName = "PassAndInstallPercentage";
				colvarPassAndInstallPercentage.DataType = DbType.Decimal;
				colvarPassAndInstallPercentage.MaxLength = 0;
				colvarPassAndInstallPercentage.AutoIncrement = false;
				colvarPassAndInstallPercentage.IsNullable = true;
				colvarPassAndInstallPercentage.IsPrimaryKey = false;
				colvarPassAndInstallPercentage.IsForeignKey = false;
				colvarPassAndInstallPercentage.IsReadOnly = false;
				colvarPassAndInstallPercentage.DefaultSetting = @"";
				colvarPassAndInstallPercentage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassAndInstallPercentage);

				TableSchema.TableColumn colvarNumberCancels = new TableSchema.TableColumn(schema);
				colvarNumberCancels.ColumnName = "NumberCancels";
				colvarNumberCancels.DataType = DbType.Int32;
				colvarNumberCancels.MaxLength = 0;
				colvarNumberCancels.AutoIncrement = false;
				colvarNumberCancels.IsNullable = true;
				colvarNumberCancels.IsPrimaryKey = false;
				colvarNumberCancels.IsForeignKey = false;
				colvarNumberCancels.IsReadOnly = false;
				colvarNumberCancels.DefaultSetting = @"";
				colvarNumberCancels.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberCancels);

				TableSchema.TableColumn colvarNumberNetSales = new TableSchema.TableColumn(schema);
				colvarNumberNetSales.ColumnName = "NumberNetSales";
				colvarNumberNetSales.DataType = DbType.Int32;
				colvarNumberNetSales.MaxLength = 0;
				colvarNumberNetSales.AutoIncrement = false;
				colvarNumberNetSales.IsNullable = true;
				colvarNumberNetSales.IsPrimaryKey = false;
				colvarNumberNetSales.IsForeignKey = false;
				colvarNumberNetSales.IsReadOnly = false;
				colvarNumberNetSales.DefaultSetting = @"";
				colvarNumberNetSales.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberNetSales);

				TableSchema.TableColumn colvarNumberPresurveys = new TableSchema.TableColumn(schema);
				colvarNumberPresurveys.ColumnName = "NumberPresurveys";
				colvarNumberPresurveys.DataType = DbType.Int32;
				colvarNumberPresurveys.MaxLength = 0;
				colvarNumberPresurveys.AutoIncrement = false;
				colvarNumberPresurveys.IsNullable = true;
				colvarNumberPresurveys.IsPrimaryKey = false;
				colvarNumberPresurveys.IsForeignKey = false;
				colvarNumberPresurveys.IsReadOnly = false;
				colvarNumberPresurveys.DefaultSetting = @"";
				colvarNumberPresurveys.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberPresurveys);

				TableSchema.TableColumn colvarNumberPostsurveys = new TableSchema.TableColumn(schema);
				colvarNumberPostsurveys.ColumnName = "NumberPostsurveys";
				colvarNumberPostsurveys.DataType = DbType.Int32;
				colvarNumberPostsurveys.MaxLength = 0;
				colvarNumberPostsurveys.AutoIncrement = false;
				colvarNumberPostsurveys.IsNullable = true;
				colvarNumberPostsurveys.IsPrimaryKey = false;
				colvarNumberPostsurveys.IsForeignKey = false;
				colvarNumberPostsurveys.IsReadOnly = false;
				colvarNumberPostsurveys.DefaultSetting = @"";
				colvarNumberPostsurveys.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberPostsurveys);

				TableSchema.TableColumn colvarNumberInstallations = new TableSchema.TableColumn(schema);
				colvarNumberInstallations.ColumnName = "NumberInstallations";
				colvarNumberInstallations.DataType = DbType.Int32;
				colvarNumberInstallations.MaxLength = 0;
				colvarNumberInstallations.AutoIncrement = false;
				colvarNumberInstallations.IsNullable = true;
				colvarNumberInstallations.IsPrimaryKey = false;
				colvarNumberInstallations.IsForeignKey = false;
				colvarNumberInstallations.IsReadOnly = false;
				colvarNumberInstallations.DefaultSetting = @"";
				colvarNumberInstallations.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberInstallations);

				TableSchema.TableColumn colvarNumberSameDayInstallations = new TableSchema.TableColumn(schema);
				colvarNumberSameDayInstallations.ColumnName = "NumberSameDayInstallations";
				colvarNumberSameDayInstallations.DataType = DbType.Int32;
				colvarNumberSameDayInstallations.MaxLength = 0;
				colvarNumberSameDayInstallations.AutoIncrement = false;
				colvarNumberSameDayInstallations.IsNullable = true;
				colvarNumberSameDayInstallations.IsPrimaryKey = false;
				colvarNumberSameDayInstallations.IsForeignKey = false;
				colvarNumberSameDayInstallations.IsReadOnly = false;
				colvarNumberSameDayInstallations.DefaultSetting = @"";
				colvarNumberSameDayInstallations.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberSameDayInstallations);

				TableSchema.TableColumn colvarSameDayInstallationPercentage = new TableSchema.TableColumn(schema);
				colvarSameDayInstallationPercentage.ColumnName = "SameDayInstallationPercentage";
				colvarSameDayInstallationPercentage.DataType = DbType.Decimal;
				colvarSameDayInstallationPercentage.MaxLength = 0;
				colvarSameDayInstallationPercentage.AutoIncrement = false;
				colvarSameDayInstallationPercentage.IsNullable = true;
				colvarSameDayInstallationPercentage.IsPrimaryKey = false;
				colvarSameDayInstallationPercentage.IsForeignKey = false;
				colvarSameDayInstallationPercentage.IsReadOnly = false;
				colvarSameDayInstallationPercentage.DefaultSetting = @"";
				colvarSameDayInstallationPercentage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSameDayInstallationPercentage);

				TableSchema.TableColumn colvarNumberActivationsWaived = new TableSchema.TableColumn(schema);
				colvarNumberActivationsWaived.ColumnName = "NumberActivationsWaived";
				colvarNumberActivationsWaived.DataType = DbType.Int32;
				colvarNumberActivationsWaived.MaxLength = 0;
				colvarNumberActivationsWaived.AutoIncrement = false;
				colvarNumberActivationsWaived.IsNullable = true;
				colvarNumberActivationsWaived.IsPrimaryKey = false;
				colvarNumberActivationsWaived.IsForeignKey = false;
				colvarNumberActivationsWaived.IsReadOnly = false;
				colvarNumberActivationsWaived.DefaultSetting = @"";
				colvarNumberActivationsWaived.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberActivationsWaived);

				TableSchema.TableColumn colvarActivationsWaivedPercentage = new TableSchema.TableColumn(schema);
				colvarActivationsWaivedPercentage.ColumnName = "ActivationsWaivedPercentage";
				colvarActivationsWaivedPercentage.DataType = DbType.Decimal;
				colvarActivationsWaivedPercentage.MaxLength = 0;
				colvarActivationsWaivedPercentage.AutoIncrement = false;
				colvarActivationsWaivedPercentage.IsNullable = true;
				colvarActivationsWaivedPercentage.IsPrimaryKey = false;
				colvarActivationsWaivedPercentage.IsForeignKey = false;
				colvarActivationsWaivedPercentage.IsReadOnly = false;
				colvarActivationsWaivedPercentage.DefaultSetting = @"";
				colvarActivationsWaivedPercentage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActivationsWaivedPercentage);

				TableSchema.TableColumn colvarNumberCCPayments = new TableSchema.TableColumn(schema);
				colvarNumberCCPayments.ColumnName = "NumberCCPayments";
				colvarNumberCCPayments.DataType = DbType.Int32;
				colvarNumberCCPayments.MaxLength = 0;
				colvarNumberCCPayments.AutoIncrement = false;
				colvarNumberCCPayments.IsNullable = true;
				colvarNumberCCPayments.IsPrimaryKey = false;
				colvarNumberCCPayments.IsForeignKey = false;
				colvarNumberCCPayments.IsReadOnly = false;
				colvarNumberCCPayments.DefaultSetting = @"";
				colvarNumberCCPayments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberCCPayments);

				TableSchema.TableColumn colvarNumberACHPayments = new TableSchema.TableColumn(schema);
				colvarNumberACHPayments.ColumnName = "NumberACHPayments";
				colvarNumberACHPayments.DataType = DbType.Int32;
				colvarNumberACHPayments.MaxLength = 0;
				colvarNumberACHPayments.AutoIncrement = false;
				colvarNumberACHPayments.IsNullable = true;
				colvarNumberACHPayments.IsPrimaryKey = false;
				colvarNumberACHPayments.IsForeignKey = false;
				colvarNumberACHPayments.IsReadOnly = false;
				colvarNumberACHPayments.DefaultSetting = @"";
				colvarNumberACHPayments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberACHPayments);

				TableSchema.TableColumn colvarNumberInvoicePayments = new TableSchema.TableColumn(schema);
				colvarNumberInvoicePayments.ColumnName = "NumberInvoicePayments";
				colvarNumberInvoicePayments.DataType = DbType.Int32;
				colvarNumberInvoicePayments.MaxLength = 0;
				colvarNumberInvoicePayments.AutoIncrement = false;
				colvarNumberInvoicePayments.IsNullable = true;
				colvarNumberInvoicePayments.IsPrimaryKey = false;
				colvarNumberInvoicePayments.IsForeignKey = false;
				colvarNumberInvoicePayments.IsReadOnly = false;
				colvarNumberInvoicePayments.DefaultSetting = @"";
				colvarNumberInvoicePayments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberInvoicePayments);

				TableSchema.TableColumn colvarNumberSystemsOver8Points = new TableSchema.TableColumn(schema);
				colvarNumberSystemsOver8Points.ColumnName = "NumberSystemsOver8Points";
				colvarNumberSystemsOver8Points.DataType = DbType.Int32;
				colvarNumberSystemsOver8Points.MaxLength = 0;
				colvarNumberSystemsOver8Points.AutoIncrement = false;
				colvarNumberSystemsOver8Points.IsNullable = true;
				colvarNumberSystemsOver8Points.IsPrimaryKey = false;
				colvarNumberSystemsOver8Points.IsForeignKey = false;
				colvarNumberSystemsOver8Points.IsReadOnly = false;
				colvarNumberSystemsOver8Points.DefaultSetting = @"";
				colvarNumberSystemsOver8Points.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberSystemsOver8Points);

				TableSchema.TableColumn colvarNumberFreePointsGivenBySalesRep = new TableSchema.TableColumn(schema);
				colvarNumberFreePointsGivenBySalesRep.ColumnName = "NumberFreePointsGivenBySalesRep";
				colvarNumberFreePointsGivenBySalesRep.DataType = DbType.Int32;
				colvarNumberFreePointsGivenBySalesRep.MaxLength = 0;
				colvarNumberFreePointsGivenBySalesRep.AutoIncrement = false;
				colvarNumberFreePointsGivenBySalesRep.IsNullable = true;
				colvarNumberFreePointsGivenBySalesRep.IsPrimaryKey = false;
				colvarNumberFreePointsGivenBySalesRep.IsForeignKey = false;
				colvarNumberFreePointsGivenBySalesRep.IsReadOnly = false;
				colvarNumberFreePointsGivenBySalesRep.DefaultSetting = @"";
				colvarNumberFreePointsGivenBySalesRep.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberFreePointsGivenBySalesRep);

				TableSchema.TableColumn colvarNumberFreePointsGivenByTech = new TableSchema.TableColumn(schema);
				colvarNumberFreePointsGivenByTech.ColumnName = "NumberFreePointsGivenByTech";
				colvarNumberFreePointsGivenByTech.DataType = DbType.Int32;
				colvarNumberFreePointsGivenByTech.MaxLength = 0;
				colvarNumberFreePointsGivenByTech.AutoIncrement = false;
				colvarNumberFreePointsGivenByTech.IsNullable = true;
				colvarNumberFreePointsGivenByTech.IsPrimaryKey = false;
				colvarNumberFreePointsGivenByTech.IsForeignKey = false;
				colvarNumberFreePointsGivenByTech.IsReadOnly = false;
				colvarNumberFreePointsGivenByTech.DefaultSetting = @"";
				colvarNumberFreePointsGivenByTech.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNumberFreePointsGivenByTech);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersGetDetailedStatisticsConnext",schema);
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
		public RU_UsersGetDetailedStatisticsConnextView()
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
		public int? SalesYear {
			get { return GetColumnValue<int>(Columns.SalesYear); }
			set { SetColumnValue(Columns.SalesYear, value); }
		}
		[DataMember]
		public int? SalesMonth {
			get { return GetColumnValue<int>(Columns.SalesMonth); }
			set { SetColumnValue(Columns.SalesMonth, value); }
		}
		[DataMember]
		public int? RegionID {
			get { return GetColumnValue<int>(Columns.RegionID); }
			set { SetColumnValue(Columns.RegionID, value); }
		}
		[DataMember]
		public string RegionName {
			get { return GetColumnValue<string>(Columns.RegionName); }
			set { SetColumnValue(Columns.RegionName, value); }
		}
		[DataMember]
		public int? TeamID {
			get { return GetColumnValue<int>(Columns.TeamID); }
			set { SetColumnValue(Columns.TeamID, value); }
		}
		[DataMember]
		public string TeamName {
			get { return GetColumnValue<string>(Columns.TeamName); }
			set { SetColumnValue(Columns.TeamName, value); }
		}
		[DataMember]
		public int? OfficeID {
			get { return GetColumnValue<int>(Columns.OfficeID); }
			set { SetColumnValue(Columns.OfficeID, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}
		[DataMember]
		public bool HasRecruits {
			get { return GetColumnValue<bool>(Columns.HasRecruits); }
			set { SetColumnValue(Columns.HasRecruits, value); }
		}
		[DataMember]
		public int? NumberCreditReportsPulled {
			get { return GetColumnValue<int>(Columns.NumberCreditReportsPulled); }
			set { SetColumnValue(Columns.NumberCreditReportsPulled, value); }
		}
		[DataMember]
		public int? NumberCreditsPassed {
			get { return GetColumnValue<int>(Columns.NumberCreditsPassed); }
			set { SetColumnValue(Columns.NumberCreditsPassed, value); }
		}
		[DataMember]
		public int? NumberExcellentCreditScores {
			get { return GetColumnValue<int>(Columns.NumberExcellentCreditScores); }
			set { SetColumnValue(Columns.NumberExcellentCreditScores, value); }
		}
		[DataMember]
		public int? NumberGoodCreditScores {
			get { return GetColumnValue<int>(Columns.NumberGoodCreditScores); }
			set { SetColumnValue(Columns.NumberGoodCreditScores, value); }
		}
		[DataMember]
		public int? NumberBadCreditScores {
			get { return GetColumnValue<int>(Columns.NumberBadCreditScores); }
			set { SetColumnValue(Columns.NumberBadCreditScores, value); }
		}
		[DataMember]
		public int? AverageCreditScore {
			get { return GetColumnValue<int>(Columns.AverageCreditScore); }
			set { SetColumnValue(Columns.AverageCreditScore, value); }
		}
		[DataMember]
		public decimal? CreditPassPercentage {
			get { return GetColumnValue<decimal>(Columns.CreditPassPercentage); }
			set { SetColumnValue(Columns.CreditPassPercentage, value); }
		}
		[DataMember]
		public decimal? PassAndInstallPercentage {
			get { return GetColumnValue<decimal>(Columns.PassAndInstallPercentage); }
			set { SetColumnValue(Columns.PassAndInstallPercentage, value); }
		}
		[DataMember]
		public int? NumberCancels {
			get { return GetColumnValue<int>(Columns.NumberCancels); }
			set { SetColumnValue(Columns.NumberCancels, value); }
		}
		[DataMember]
		public int? NumberNetSales {
			get { return GetColumnValue<int>(Columns.NumberNetSales); }
			set { SetColumnValue(Columns.NumberNetSales, value); }
		}
		[DataMember]
		public int? NumberPresurveys {
			get { return GetColumnValue<int>(Columns.NumberPresurveys); }
			set { SetColumnValue(Columns.NumberPresurveys, value); }
		}
		[DataMember]
		public int? NumberPostsurveys {
			get { return GetColumnValue<int>(Columns.NumberPostsurveys); }
			set { SetColumnValue(Columns.NumberPostsurveys, value); }
		}
		[DataMember]
		public int? NumberInstallations {
			get { return GetColumnValue<int>(Columns.NumberInstallations); }
			set { SetColumnValue(Columns.NumberInstallations, value); }
		}
		[DataMember]
		public int? NumberSameDayInstallations {
			get { return GetColumnValue<int>(Columns.NumberSameDayInstallations); }
			set { SetColumnValue(Columns.NumberSameDayInstallations, value); }
		}
		[DataMember]
		public decimal? SameDayInstallationPercentage {
			get { return GetColumnValue<decimal>(Columns.SameDayInstallationPercentage); }
			set { SetColumnValue(Columns.SameDayInstallationPercentage, value); }
		}
		[DataMember]
		public int? NumberActivationsWaived {
			get { return GetColumnValue<int>(Columns.NumberActivationsWaived); }
			set { SetColumnValue(Columns.NumberActivationsWaived, value); }
		}
		[DataMember]
		public decimal? ActivationsWaivedPercentage {
			get { return GetColumnValue<decimal>(Columns.ActivationsWaivedPercentage); }
			set { SetColumnValue(Columns.ActivationsWaivedPercentage, value); }
		}
		[DataMember]
		public int? NumberCCPayments {
			get { return GetColumnValue<int>(Columns.NumberCCPayments); }
			set { SetColumnValue(Columns.NumberCCPayments, value); }
		}
		[DataMember]
		public int? NumberACHPayments {
			get { return GetColumnValue<int>(Columns.NumberACHPayments); }
			set { SetColumnValue(Columns.NumberACHPayments, value); }
		}
		[DataMember]
		public int? NumberInvoicePayments {
			get { return GetColumnValue<int>(Columns.NumberInvoicePayments); }
			set { SetColumnValue(Columns.NumberInvoicePayments, value); }
		}
		[DataMember]
		public int? NumberSystemsOver8Points {
			get { return GetColumnValue<int>(Columns.NumberSystemsOver8Points); }
			set { SetColumnValue(Columns.NumberSystemsOver8Points, value); }
		}
		[DataMember]
		public int? NumberFreePointsGivenBySalesRep {
			get { return GetColumnValue<int>(Columns.NumberFreePointsGivenBySalesRep); }
			set { SetColumnValue(Columns.NumberFreePointsGivenBySalesRep, value); }
		}
		[DataMember]
		public int? NumberFreePointsGivenByTech {
			get { return GetColumnValue<int>(Columns.NumberFreePointsGivenByTech); }
			set { SetColumnValue(Columns.NumberFreePointsGivenByTech, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FirstName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SalesYearColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn SalesMonthColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn RegionIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn RegionNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn TeamIDColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn TeamNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn OfficeIDColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn HasRecruitsColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn NumberCreditReportsPulledColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn NumberCreditsPassedColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn NumberExcellentCreditScoresColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn NumberGoodCreditScoresColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn NumberBadCreditScoresColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn AverageCreditScoreColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn CreditPassPercentageColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn PassAndInstallPercentageColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn NumberCancelsColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn NumberNetSalesColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn NumberPresurveysColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn NumberPostsurveysColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn NumberInstallationsColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn NumberSameDayInstallationsColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn SameDayInstallationPercentageColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn NumberActivationsWaivedColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn ActivationsWaivedPercentageColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn NumberCCPaymentsColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn NumberACHPaymentsColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn NumberInvoicePaymentsColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn NumberSystemsOver8PointsColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn NumberFreePointsGivenBySalesRepColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn NumberFreePointsGivenByTechColumn
		{
			get { return Schema.Columns[34]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FirstName = @"FirstName";
			public const string LastName = @"LastName";
			public const string SalesYear = @"SalesYear";
			public const string SalesMonth = @"SalesMonth";
			public const string RegionID = @"RegionID";
			public const string RegionName = @"RegionName";
			public const string TeamID = @"TeamID";
			public const string TeamName = @"TeamName";
			public const string OfficeID = @"OfficeID";
			public const string OfficeName = @"OfficeName";
			public const string HasRecruits = @"HasRecruits";
			public const string NumberCreditReportsPulled = @"NumberCreditReportsPulled";
			public const string NumberCreditsPassed = @"NumberCreditsPassed";
			public const string NumberExcellentCreditScores = @"NumberExcellentCreditScores";
			public const string NumberGoodCreditScores = @"NumberGoodCreditScores";
			public const string NumberBadCreditScores = @"NumberBadCreditScores";
			public const string AverageCreditScore = @"AverageCreditScore";
			public const string CreditPassPercentage = @"CreditPassPercentage";
			public const string PassAndInstallPercentage = @"PassAndInstallPercentage";
			public const string NumberCancels = @"NumberCancels";
			public const string NumberNetSales = @"NumberNetSales";
			public const string NumberPresurveys = @"NumberPresurveys";
			public const string NumberPostsurveys = @"NumberPostsurveys";
			public const string NumberInstallations = @"NumberInstallations";
			public const string NumberSameDayInstallations = @"NumberSameDayInstallations";
			public const string SameDayInstallationPercentage = @"SameDayInstallationPercentage";
			public const string NumberActivationsWaived = @"NumberActivationsWaived";
			public const string ActivationsWaivedPercentage = @"ActivationsWaivedPercentage";
			public const string NumberCCPayments = @"NumberCCPayments";
			public const string NumberACHPayments = @"NumberACHPayments";
			public const string NumberInvoicePayments = @"NumberInvoicePayments";
			public const string NumberSystemsOver8Points = @"NumberSystemsOver8Points";
			public const string NumberFreePointsGivenBySalesRep = @"NumberFreePointsGivenBySalesRep";
			public const string NumberFreePointsGivenByTech = @"NumberFreePointsGivenByTech";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_UsersSalesInfoConnextView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesInfoConnextViewCollection : ReadOnlyList<RU_UsersSalesInfoConnextView, RU_UsersSalesInfoConnextViewCollection>
	{
		public static RU_UsersSalesInfoConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersSalesInfoConnextViewCollection result = new RU_UsersSalesInfoConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersSalesInfoConnext view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesInfoConnextView : ReadOnlyRecord<RU_UsersSalesInfoConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersSalesInfoConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarPhotoURL = new TableSchema.TableColumn(schema);
				colvarPhotoURL.ColumnName = "PhotoURL";
				colvarPhotoURL.DataType = DbType.AnsiString;
				colvarPhotoURL.MaxLength = 65;
				colvarPhotoURL.AutoIncrement = false;
				colvarPhotoURL.IsNullable = false;
				colvarPhotoURL.IsPrimaryKey = false;
				colvarPhotoURL.IsForeignKey = false;
				colvarPhotoURL.IsReadOnly = false;
				colvarPhotoURL.DefaultSetting = @"";
				colvarPhotoURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhotoURL);

				TableSchema.TableColumn colvarMLMDepth = new TableSchema.TableColumn(schema);
				colvarMLMDepth.ColumnName = "MLMDepth";
				colvarMLMDepth.DataType = DbType.Int64;
				colvarMLMDepth.MaxLength = 0;
				colvarMLMDepth.AutoIncrement = false;
				colvarMLMDepth.IsNullable = true;
				colvarMLMDepth.IsPrimaryKey = false;
				colvarMLMDepth.IsForeignKey = false;
				colvarMLMDepth.IsReadOnly = false;
				colvarMLMDepth.DefaultSetting = @"";
				colvarMLMDepth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMLMDepth);

				TableSchema.TableColumn colvarManagerHasOwnTeam = new TableSchema.TableColumn(schema);
				colvarManagerHasOwnTeam.ColumnName = "ManagerHasOwnTeam";
				colvarManagerHasOwnTeam.DataType = DbType.Boolean;
				colvarManagerHasOwnTeam.MaxLength = 0;
				colvarManagerHasOwnTeam.AutoIncrement = false;
				colvarManagerHasOwnTeam.IsNullable = true;
				colvarManagerHasOwnTeam.IsPrimaryKey = false;
				colvarManagerHasOwnTeam.IsForeignKey = false;
				colvarManagerHasOwnTeam.IsReadOnly = false;
				colvarManagerHasOwnTeam.DefaultSetting = @"";
				colvarManagerHasOwnTeam.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerHasOwnTeam);

				TableSchema.TableColumn colvarRegionName = new TableSchema.TableColumn(schema);
				colvarRegionName.ColumnName = "RegionName";
				colvarRegionName.DataType = DbType.AnsiString;
				colvarRegionName.MaxLength = 50;
				colvarRegionName.AutoIncrement = false;
				colvarRegionName.IsNullable = true;
				colvarRegionName.IsPrimaryKey = false;
				colvarRegionName.IsForeignKey = false;
				colvarRegionName.IsReadOnly = false;
				colvarRegionName.DefaultSetting = @"";
				colvarRegionName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionName);

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

				TableSchema.TableColumn colvarTeamName = new TableSchema.TableColumn(schema);
				colvarTeamName.ColumnName = "TeamName";
				colvarTeamName.DataType = DbType.AnsiString;
				colvarTeamName.MaxLength = 50;
				colvarTeamName.AutoIncrement = false;
				colvarTeamName.IsNullable = true;
				colvarTeamName.IsPrimaryKey = false;
				colvarTeamName.IsForeignKey = false;
				colvarTeamName.IsReadOnly = false;
				colvarTeamName.DefaultSetting = @"";
				colvarTeamName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamName);

				TableSchema.TableColumn colvarCurrentNationalRank = new TableSchema.TableColumn(schema);
				colvarCurrentNationalRank.ColumnName = "CurrentNationalRank";
				colvarCurrentNationalRank.DataType = DbType.Int64;
				colvarCurrentNationalRank.MaxLength = 0;
				colvarCurrentNationalRank.AutoIncrement = false;
				colvarCurrentNationalRank.IsNullable = true;
				colvarCurrentNationalRank.IsPrimaryKey = false;
				colvarCurrentNationalRank.IsForeignKey = false;
				colvarCurrentNationalRank.IsReadOnly = false;
				colvarCurrentNationalRank.DefaultSetting = @"";
				colvarCurrentNationalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentNationalRank);

				TableSchema.TableColumn colvarPreviousNationalRank = new TableSchema.TableColumn(schema);
				colvarPreviousNationalRank.ColumnName = "PreviousNationalRank";
				colvarPreviousNationalRank.DataType = DbType.Int64;
				colvarPreviousNationalRank.MaxLength = 0;
				colvarPreviousNationalRank.AutoIncrement = false;
				colvarPreviousNationalRank.IsNullable = true;
				colvarPreviousNationalRank.IsPrimaryKey = false;
				colvarPreviousNationalRank.IsForeignKey = false;
				colvarPreviousNationalRank.IsReadOnly = false;
				colvarPreviousNationalRank.DefaultSetting = @"";
				colvarPreviousNationalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousNationalRank);

				TableSchema.TableColumn colvarCurrentRegionalRank = new TableSchema.TableColumn(schema);
				colvarCurrentRegionalRank.ColumnName = "CurrentRegionalRank";
				colvarCurrentRegionalRank.DataType = DbType.Int64;
				colvarCurrentRegionalRank.MaxLength = 0;
				colvarCurrentRegionalRank.AutoIncrement = false;
				colvarCurrentRegionalRank.IsNullable = true;
				colvarCurrentRegionalRank.IsPrimaryKey = false;
				colvarCurrentRegionalRank.IsForeignKey = false;
				colvarCurrentRegionalRank.IsReadOnly = false;
				colvarCurrentRegionalRank.DefaultSetting = @"";
				colvarCurrentRegionalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentRegionalRank);

				TableSchema.TableColumn colvarPreviousRegionalRank = new TableSchema.TableColumn(schema);
				colvarPreviousRegionalRank.ColumnName = "PreviousRegionalRank";
				colvarPreviousRegionalRank.DataType = DbType.Int64;
				colvarPreviousRegionalRank.MaxLength = 0;
				colvarPreviousRegionalRank.AutoIncrement = false;
				colvarPreviousRegionalRank.IsNullable = true;
				colvarPreviousRegionalRank.IsPrimaryKey = false;
				colvarPreviousRegionalRank.IsForeignKey = false;
				colvarPreviousRegionalRank.IsReadOnly = false;
				colvarPreviousRegionalRank.DefaultSetting = @"";
				colvarPreviousRegionalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousRegionalRank);

				TableSchema.TableColumn colvarCurrentOfficeRank = new TableSchema.TableColumn(schema);
				colvarCurrentOfficeRank.ColumnName = "CurrentOfficeRank";
				colvarCurrentOfficeRank.DataType = DbType.Int64;
				colvarCurrentOfficeRank.MaxLength = 0;
				colvarCurrentOfficeRank.AutoIncrement = false;
				colvarCurrentOfficeRank.IsNullable = true;
				colvarCurrentOfficeRank.IsPrimaryKey = false;
				colvarCurrentOfficeRank.IsForeignKey = false;
				colvarCurrentOfficeRank.IsReadOnly = false;
				colvarCurrentOfficeRank.DefaultSetting = @"";
				colvarCurrentOfficeRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentOfficeRank);

				TableSchema.TableColumn colvarPreviousOfficeRank = new TableSchema.TableColumn(schema);
				colvarPreviousOfficeRank.ColumnName = "PreviousOfficeRank";
				colvarPreviousOfficeRank.DataType = DbType.Int64;
				colvarPreviousOfficeRank.MaxLength = 0;
				colvarPreviousOfficeRank.AutoIncrement = false;
				colvarPreviousOfficeRank.IsNullable = true;
				colvarPreviousOfficeRank.IsPrimaryKey = false;
				colvarPreviousOfficeRank.IsForeignKey = false;
				colvarPreviousOfficeRank.IsReadOnly = false;
				colvarPreviousOfficeRank.DefaultSetting = @"";
				colvarPreviousOfficeRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousOfficeRank);

				TableSchema.TableColumn colvarCurrentTeamRank = new TableSchema.TableColumn(schema);
				colvarCurrentTeamRank.ColumnName = "CurrentTeamRank";
				colvarCurrentTeamRank.DataType = DbType.Int64;
				colvarCurrentTeamRank.MaxLength = 0;
				colvarCurrentTeamRank.AutoIncrement = false;
				colvarCurrentTeamRank.IsNullable = true;
				colvarCurrentTeamRank.IsPrimaryKey = false;
				colvarCurrentTeamRank.IsForeignKey = false;
				colvarCurrentTeamRank.IsReadOnly = false;
				colvarCurrentTeamRank.DefaultSetting = @"";
				colvarCurrentTeamRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentTeamRank);

				TableSchema.TableColumn colvarPreviousTeamRank = new TableSchema.TableColumn(schema);
				colvarPreviousTeamRank.ColumnName = "PreviousTeamRank";
				colvarPreviousTeamRank.DataType = DbType.Int64;
				colvarPreviousTeamRank.MaxLength = 0;
				colvarPreviousTeamRank.AutoIncrement = false;
				colvarPreviousTeamRank.IsNullable = true;
				colvarPreviousTeamRank.IsPrimaryKey = false;
				colvarPreviousTeamRank.IsForeignKey = false;
				colvarPreviousTeamRank.IsReadOnly = false;
				colvarPreviousTeamRank.DefaultSetting = @"";
				colvarPreviousTeamRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousTeamRank);

				TableSchema.TableColumn colvarStartDate = new TableSchema.TableColumn(schema);
				colvarStartDate.ColumnName = "StartDate";
				colvarStartDate.DataType = DbType.DateTime;
				colvarStartDate.MaxLength = 0;
				colvarStartDate.AutoIncrement = false;
				colvarStartDate.IsNullable = true;
				colvarStartDate.IsPrimaryKey = false;
				colvarStartDate.IsForeignKey = false;
				colvarStartDate.IsReadOnly = false;
				colvarStartDate.DefaultSetting = @"";
				colvarStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartDate);

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

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersSalesInfoConnext",schema);
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
		public RU_UsersSalesInfoConnextView()
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
		public string PhotoURL {
			get { return GetColumnValue<string>(Columns.PhotoURL); }
			set { SetColumnValue(Columns.PhotoURL, value); }
		}
		[DataMember]
		public long? MLMDepth {
			get { return GetColumnValue<long?>(Columns.MLMDepth); }
			set { SetColumnValue(Columns.MLMDepth, value); }
		}
		[DataMember]
		public bool? ManagerHasOwnTeam {
			get { return GetColumnValue<bool?>(Columns.ManagerHasOwnTeam); }
			set { SetColumnValue(Columns.ManagerHasOwnTeam, value); }
		}
		[DataMember]
		public string RegionName {
			get { return GetColumnValue<string>(Columns.RegionName); }
			set { SetColumnValue(Columns.RegionName, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}
		[DataMember]
		public string TeamName {
			get { return GetColumnValue<string>(Columns.TeamName); }
			set { SetColumnValue(Columns.TeamName, value); }
		}
		[DataMember]
		public long? CurrentNationalRank {
			get { return GetColumnValue<long?>(Columns.CurrentNationalRank); }
			set { SetColumnValue(Columns.CurrentNationalRank, value); }
		}
		[DataMember]
		public long? PreviousNationalRank {
			get { return GetColumnValue<long?>(Columns.PreviousNationalRank); }
			set { SetColumnValue(Columns.PreviousNationalRank, value); }
		}
		[DataMember]
		public long? CurrentRegionalRank {
			get { return GetColumnValue<long?>(Columns.CurrentRegionalRank); }
			set { SetColumnValue(Columns.CurrentRegionalRank, value); }
		}
		[DataMember]
		public long? PreviousRegionalRank {
			get { return GetColumnValue<long?>(Columns.PreviousRegionalRank); }
			set { SetColumnValue(Columns.PreviousRegionalRank, value); }
		}
		[DataMember]
		public long? CurrentOfficeRank {
			get { return GetColumnValue<long?>(Columns.CurrentOfficeRank); }
			set { SetColumnValue(Columns.CurrentOfficeRank, value); }
		}
		[DataMember]
		public long? PreviousOfficeRank {
			get { return GetColumnValue<long?>(Columns.PreviousOfficeRank); }
			set { SetColumnValue(Columns.PreviousOfficeRank, value); }
		}
		[DataMember]
		public long? CurrentTeamRank {
			get { return GetColumnValue<long?>(Columns.CurrentTeamRank); }
			set { SetColumnValue(Columns.CurrentTeamRank, value); }
		}
		[DataMember]
		public long? PreviousTeamRank {
			get { return GetColumnValue<long?>(Columns.PreviousTeamRank); }
			set { SetColumnValue(Columns.PreviousTeamRank, value); }
		}
		[DataMember]
		public DateTime? StartDate {
			get { return GetColumnValue<DateTime?>(Columns.StartDate); }
			set { SetColumnValue(Columns.StartDate, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FirstName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhotoURLColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn MLMDepthColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ManagerHasOwnTeamColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn RegionNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TeamNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CurrentNationalRankColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PreviousNationalRankColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CurrentRegionalRankColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn PreviousRegionalRankColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CurrentOfficeRankColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn PreviousOfficeRankColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CurrentTeamRankColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn PreviousTeamRankColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn StartDateColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[19]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PhotoURL = @"PhotoURL";
			public const string MLMDepth = @"MLMDepth";
			public const string ManagerHasOwnTeam = @"ManagerHasOwnTeam";
			public const string RegionName = @"RegionName";
			public const string OfficeName = @"OfficeName";
			public const string TeamName = @"TeamName";
			public const string CurrentNationalRank = @"CurrentNationalRank";
			public const string PreviousNationalRank = @"PreviousNationalRank";
			public const string CurrentRegionalRank = @"CurrentRegionalRank";
			public const string PreviousRegionalRank = @"PreviousRegionalRank";
			public const string CurrentOfficeRank = @"CurrentOfficeRank";
			public const string PreviousOfficeRank = @"PreviousOfficeRank";
			public const string CurrentTeamRank = @"CurrentTeamRank";
			public const string PreviousTeamRank = @"PreviousTeamRank";
			public const string StartDate = @"StartDate";
			public const string Email = @"Email";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_UsersSalesInfoExtendedConnextView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesInfoExtendedConnextViewCollection : ReadOnlyList<RU_UsersSalesInfoExtendedConnextView, RU_UsersSalesInfoExtendedConnextViewCollection>
	{
		public static RU_UsersSalesInfoExtendedConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersSalesInfoExtendedConnextViewCollection result = new RU_UsersSalesInfoExtendedConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersSalesInfoExtendedConnext view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesInfoExtendedConnextView : ReadOnlyRecord<RU_UsersSalesInfoExtendedConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersSalesInfoExtendedConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarPhotoURL = new TableSchema.TableColumn(schema);
				colvarPhotoURL.ColumnName = "PhotoURL";
				colvarPhotoURL.DataType = DbType.AnsiString;
				colvarPhotoURL.MaxLength = 16;
				colvarPhotoURL.AutoIncrement = false;
				colvarPhotoURL.IsNullable = false;
				colvarPhotoURL.IsPrimaryKey = false;
				colvarPhotoURL.IsForeignKey = false;
				colvarPhotoURL.IsReadOnly = false;
				colvarPhotoURL.DefaultSetting = @"";
				colvarPhotoURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhotoURL);

				TableSchema.TableColumn colvarMLMDepth = new TableSchema.TableColumn(schema);
				colvarMLMDepth.ColumnName = "MLMDepth";
				colvarMLMDepth.DataType = DbType.Int64;
				colvarMLMDepth.MaxLength = 0;
				colvarMLMDepth.AutoIncrement = false;
				colvarMLMDepth.IsNullable = true;
				colvarMLMDepth.IsPrimaryKey = false;
				colvarMLMDepth.IsForeignKey = false;
				colvarMLMDepth.IsReadOnly = false;
				colvarMLMDepth.DefaultSetting = @"";
				colvarMLMDepth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMLMDepth);

				TableSchema.TableColumn colvarManagerHasOwnTeam = new TableSchema.TableColumn(schema);
				colvarManagerHasOwnTeam.ColumnName = "ManagerHasOwnTeam";
				colvarManagerHasOwnTeam.DataType = DbType.Boolean;
				colvarManagerHasOwnTeam.MaxLength = 0;
				colvarManagerHasOwnTeam.AutoIncrement = false;
				colvarManagerHasOwnTeam.IsNullable = true;
				colvarManagerHasOwnTeam.IsPrimaryKey = false;
				colvarManagerHasOwnTeam.IsForeignKey = false;
				colvarManagerHasOwnTeam.IsReadOnly = false;
				colvarManagerHasOwnTeam.DefaultSetting = @"";
				colvarManagerHasOwnTeam.ForeignKeyTableName = "";
				schema.Columns.Add(colvarManagerHasOwnTeam);

				TableSchema.TableColumn colvarRegionName = new TableSchema.TableColumn(schema);
				colvarRegionName.ColumnName = "RegionName";
				colvarRegionName.DataType = DbType.AnsiString;
				colvarRegionName.MaxLength = 9;
				colvarRegionName.AutoIncrement = false;
				colvarRegionName.IsNullable = false;
				colvarRegionName.IsPrimaryKey = false;
				colvarRegionName.IsForeignKey = false;
				colvarRegionName.IsReadOnly = false;
				colvarRegionName.DefaultSetting = @"";
				colvarRegionName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRegionName);

				TableSchema.TableColumn colvarOfficeName = new TableSchema.TableColumn(schema);
				colvarOfficeName.ColumnName = "OfficeName";
				colvarOfficeName.DataType = DbType.AnsiString;
				colvarOfficeName.MaxLength = 10;
				colvarOfficeName.AutoIncrement = false;
				colvarOfficeName.IsNullable = false;
				colvarOfficeName.IsPrimaryKey = false;
				colvarOfficeName.IsForeignKey = false;
				colvarOfficeName.IsReadOnly = false;
				colvarOfficeName.DefaultSetting = @"";
				colvarOfficeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeName);

				TableSchema.TableColumn colvarTeamName = new TableSchema.TableColumn(schema);
				colvarTeamName.ColumnName = "TeamName";
				colvarTeamName.DataType = DbType.AnsiString;
				colvarTeamName.MaxLength = 10;
				colvarTeamName.AutoIncrement = false;
				colvarTeamName.IsNullable = false;
				colvarTeamName.IsPrimaryKey = false;
				colvarTeamName.IsForeignKey = false;
				colvarTeamName.IsReadOnly = false;
				colvarTeamName.DefaultSetting = @"";
				colvarTeamName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamName);

				TableSchema.TableColumn colvarCurrentNationalRank = new TableSchema.TableColumn(schema);
				colvarCurrentNationalRank.ColumnName = "CurrentNationalRank";
				colvarCurrentNationalRank.DataType = DbType.Int64;
				colvarCurrentNationalRank.MaxLength = 0;
				colvarCurrentNationalRank.AutoIncrement = false;
				colvarCurrentNationalRank.IsNullable = true;
				colvarCurrentNationalRank.IsPrimaryKey = false;
				colvarCurrentNationalRank.IsForeignKey = false;
				colvarCurrentNationalRank.IsReadOnly = false;
				colvarCurrentNationalRank.DefaultSetting = @"";
				colvarCurrentNationalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentNationalRank);

				TableSchema.TableColumn colvarPreviousNationalRank = new TableSchema.TableColumn(schema);
				colvarPreviousNationalRank.ColumnName = "PreviousNationalRank";
				colvarPreviousNationalRank.DataType = DbType.Int64;
				colvarPreviousNationalRank.MaxLength = 0;
				colvarPreviousNationalRank.AutoIncrement = false;
				colvarPreviousNationalRank.IsNullable = true;
				colvarPreviousNationalRank.IsPrimaryKey = false;
				colvarPreviousNationalRank.IsForeignKey = false;
				colvarPreviousNationalRank.IsReadOnly = false;
				colvarPreviousNationalRank.DefaultSetting = @"";
				colvarPreviousNationalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousNationalRank);

				TableSchema.TableColumn colvarCurrentRegionalRank = new TableSchema.TableColumn(schema);
				colvarCurrentRegionalRank.ColumnName = "CurrentRegionalRank";
				colvarCurrentRegionalRank.DataType = DbType.Int64;
				colvarCurrentRegionalRank.MaxLength = 0;
				colvarCurrentRegionalRank.AutoIncrement = false;
				colvarCurrentRegionalRank.IsNullable = true;
				colvarCurrentRegionalRank.IsPrimaryKey = false;
				colvarCurrentRegionalRank.IsForeignKey = false;
				colvarCurrentRegionalRank.IsReadOnly = false;
				colvarCurrentRegionalRank.DefaultSetting = @"";
				colvarCurrentRegionalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentRegionalRank);

				TableSchema.TableColumn colvarPreviousRegionalRank = new TableSchema.TableColumn(schema);
				colvarPreviousRegionalRank.ColumnName = "PreviousRegionalRank";
				colvarPreviousRegionalRank.DataType = DbType.Int64;
				colvarPreviousRegionalRank.MaxLength = 0;
				colvarPreviousRegionalRank.AutoIncrement = false;
				colvarPreviousRegionalRank.IsNullable = true;
				colvarPreviousRegionalRank.IsPrimaryKey = false;
				colvarPreviousRegionalRank.IsForeignKey = false;
				colvarPreviousRegionalRank.IsReadOnly = false;
				colvarPreviousRegionalRank.DefaultSetting = @"";
				colvarPreviousRegionalRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousRegionalRank);

				TableSchema.TableColumn colvarCurrentOfficeRank = new TableSchema.TableColumn(schema);
				colvarCurrentOfficeRank.ColumnName = "CurrentOfficeRank";
				colvarCurrentOfficeRank.DataType = DbType.Int64;
				colvarCurrentOfficeRank.MaxLength = 0;
				colvarCurrentOfficeRank.AutoIncrement = false;
				colvarCurrentOfficeRank.IsNullable = true;
				colvarCurrentOfficeRank.IsPrimaryKey = false;
				colvarCurrentOfficeRank.IsForeignKey = false;
				colvarCurrentOfficeRank.IsReadOnly = false;
				colvarCurrentOfficeRank.DefaultSetting = @"";
				colvarCurrentOfficeRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentOfficeRank);

				TableSchema.TableColumn colvarPreviousOfficeRank = new TableSchema.TableColumn(schema);
				colvarPreviousOfficeRank.ColumnName = "PreviousOfficeRank";
				colvarPreviousOfficeRank.DataType = DbType.Int64;
				colvarPreviousOfficeRank.MaxLength = 0;
				colvarPreviousOfficeRank.AutoIncrement = false;
				colvarPreviousOfficeRank.IsNullable = true;
				colvarPreviousOfficeRank.IsPrimaryKey = false;
				colvarPreviousOfficeRank.IsForeignKey = false;
				colvarPreviousOfficeRank.IsReadOnly = false;
				colvarPreviousOfficeRank.DefaultSetting = @"";
				colvarPreviousOfficeRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousOfficeRank);

				TableSchema.TableColumn colvarCurrentTeamRank = new TableSchema.TableColumn(schema);
				colvarCurrentTeamRank.ColumnName = "CurrentTeamRank";
				colvarCurrentTeamRank.DataType = DbType.Int64;
				colvarCurrentTeamRank.MaxLength = 0;
				colvarCurrentTeamRank.AutoIncrement = false;
				colvarCurrentTeamRank.IsNullable = true;
				colvarCurrentTeamRank.IsPrimaryKey = false;
				colvarCurrentTeamRank.IsForeignKey = false;
				colvarCurrentTeamRank.IsReadOnly = false;
				colvarCurrentTeamRank.DefaultSetting = @"";
				colvarCurrentTeamRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentTeamRank);

				TableSchema.TableColumn colvarPreviousTeamRank = new TableSchema.TableColumn(schema);
				colvarPreviousTeamRank.ColumnName = "PreviousTeamRank";
				colvarPreviousTeamRank.DataType = DbType.Int64;
				colvarPreviousTeamRank.MaxLength = 0;
				colvarPreviousTeamRank.AutoIncrement = false;
				colvarPreviousTeamRank.IsNullable = true;
				colvarPreviousTeamRank.IsPrimaryKey = false;
				colvarPreviousTeamRank.IsForeignKey = false;
				colvarPreviousTeamRank.IsReadOnly = false;
				colvarPreviousTeamRank.DefaultSetting = @"";
				colvarPreviousTeamRank.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousTeamRank);

				TableSchema.TableColumn colvarStartDate = new TableSchema.TableColumn(schema);
				colvarStartDate.ColumnName = "StartDate";
				colvarStartDate.DataType = DbType.DateTime;
				colvarStartDate.MaxLength = 0;
				colvarStartDate.AutoIncrement = false;
				colvarStartDate.IsNullable = true;
				colvarStartDate.IsPrimaryKey = false;
				colvarStartDate.IsForeignKey = false;
				colvarStartDate.IsReadOnly = false;
				colvarStartDate.DefaultSetting = @"";
				colvarStartDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStartDate);

				TableSchema.TableColumn colvarPhone = new TableSchema.TableColumn(schema);
				colvarPhone.ColumnName = "Phone";
				colvarPhone.DataType = DbType.String;
				colvarPhone.MaxLength = 50;
				colvarPhone.AutoIncrement = false;
				colvarPhone.IsNullable = true;
				colvarPhone.IsPrimaryKey = false;
				colvarPhone.IsForeignKey = false;
				colvarPhone.IsReadOnly = false;
				colvarPhone.DefaultSetting = @"";
				colvarPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone);

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

				TableSchema.TableColumn colvarStreetAddress = new TableSchema.TableColumn(schema);
				colvarStreetAddress.ColumnName = "StreetAddress";
				colvarStreetAddress.DataType = DbType.String;
				colvarStreetAddress.MaxLength = 50;
				colvarStreetAddress.AutoIncrement = false;
				colvarStreetAddress.IsNullable = true;
				colvarStreetAddress.IsPrimaryKey = false;
				colvarStreetAddress.IsForeignKey = false;
				colvarStreetAddress.IsReadOnly = false;
				colvarStreetAddress.DefaultSetting = @"";
				colvarStreetAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress);

				TableSchema.TableColumn colvarStreetAddress2 = new TableSchema.TableColumn(schema);
				colvarStreetAddress2.ColumnName = "StreetAddress2";
				colvarStreetAddress2.DataType = DbType.String;
				colvarStreetAddress2.MaxLength = 50;
				colvarStreetAddress2.AutoIncrement = false;
				colvarStreetAddress2.IsNullable = true;
				colvarStreetAddress2.IsPrimaryKey = false;
				colvarStreetAddress2.IsForeignKey = false;
				colvarStreetAddress2.IsReadOnly = false;
				colvarStreetAddress2.DefaultSetting = @"";
				colvarStreetAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress2);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
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
				colvarState.DataType = DbType.AnsiStringFixedLength;
				colvarState.MaxLength = 2;
				colvarState.AutoIncrement = false;
				colvarState.IsNullable = true;
				colvarState.IsPrimaryKey = false;
				colvarState.IsForeignKey = false;
				colvarState.IsReadOnly = false;
				colvarState.DefaultSetting = @"";
				colvarState.ForeignKeyTableName = "";
				schema.Columns.Add(colvarState);

				TableSchema.TableColumn colvarZip = new TableSchema.TableColumn(schema);
				colvarZip.ColumnName = "Zip";
				colvarZip.DataType = DbType.String;
				colvarZip.MaxLength = 10;
				colvarZip.AutoIncrement = false;
				colvarZip.IsNullable = true;
				colvarZip.IsPrimaryKey = false;
				colvarZip.IsForeignKey = false;
				colvarZip.IsReadOnly = false;
				colvarZip.DefaultSetting = @"";
				colvarZip.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZip);

				TableSchema.TableColumn colvarWeeklySalesGoal = new TableSchema.TableColumn(schema);
				colvarWeeklySalesGoal.ColumnName = "WeeklySalesGoal";
				colvarWeeklySalesGoal.DataType = DbType.Int32;
				colvarWeeklySalesGoal.MaxLength = 0;
				colvarWeeklySalesGoal.AutoIncrement = false;
				colvarWeeklySalesGoal.IsNullable = true;
				colvarWeeklySalesGoal.IsPrimaryKey = false;
				colvarWeeklySalesGoal.IsForeignKey = false;
				colvarWeeklySalesGoal.IsReadOnly = false;
				colvarWeeklySalesGoal.DefaultSetting = @"";
				colvarWeeklySalesGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWeeklySalesGoal);

				TableSchema.TableColumn colvarMonthlySalesGoal = new TableSchema.TableColumn(schema);
				colvarMonthlySalesGoal.ColumnName = "MonthlySalesGoal";
				colvarMonthlySalesGoal.DataType = DbType.Int32;
				colvarMonthlySalesGoal.MaxLength = 0;
				colvarMonthlySalesGoal.AutoIncrement = false;
				colvarMonthlySalesGoal.IsNullable = true;
				colvarMonthlySalesGoal.IsPrimaryKey = false;
				colvarMonthlySalesGoal.IsForeignKey = false;
				colvarMonthlySalesGoal.IsReadOnly = false;
				colvarMonthlySalesGoal.DefaultSetting = @"";
				colvarMonthlySalesGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonthlySalesGoal);

				TableSchema.TableColumn colvarYearlySalesGoal = new TableSchema.TableColumn(schema);
				colvarYearlySalesGoal.ColumnName = "YearlySalesGoal";
				colvarYearlySalesGoal.DataType = DbType.Int32;
				colvarYearlySalesGoal.MaxLength = 0;
				colvarYearlySalesGoal.AutoIncrement = false;
				colvarYearlySalesGoal.IsNullable = true;
				colvarYearlySalesGoal.IsPrimaryKey = false;
				colvarYearlySalesGoal.IsForeignKey = false;
				colvarYearlySalesGoal.IsReadOnly = false;
				colvarYearlySalesGoal.DefaultSetting = @"";
				colvarYearlySalesGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarYearlySalesGoal);

				TableSchema.TableColumn colvarWeeklyQualityGoal = new TableSchema.TableColumn(schema);
				colvarWeeklyQualityGoal.ColumnName = "WeeklyQualityGoal";
				colvarWeeklyQualityGoal.DataType = DbType.Double;
				colvarWeeklyQualityGoal.MaxLength = 0;
				colvarWeeklyQualityGoal.AutoIncrement = false;
				colvarWeeklyQualityGoal.IsNullable = true;
				colvarWeeklyQualityGoal.IsPrimaryKey = false;
				colvarWeeklyQualityGoal.IsForeignKey = false;
				colvarWeeklyQualityGoal.IsReadOnly = false;
				colvarWeeklyQualityGoal.DefaultSetting = @"";
				colvarWeeklyQualityGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWeeklyQualityGoal);

				TableSchema.TableColumn colvarMonthlyQualityGoal = new TableSchema.TableColumn(schema);
				colvarMonthlyQualityGoal.ColumnName = "MonthlyQualityGoal";
				colvarMonthlyQualityGoal.DataType = DbType.Double;
				colvarMonthlyQualityGoal.MaxLength = 0;
				colvarMonthlyQualityGoal.AutoIncrement = false;
				colvarMonthlyQualityGoal.IsNullable = true;
				colvarMonthlyQualityGoal.IsPrimaryKey = false;
				colvarMonthlyQualityGoal.IsForeignKey = false;
				colvarMonthlyQualityGoal.IsReadOnly = false;
				colvarMonthlyQualityGoal.DefaultSetting = @"";
				colvarMonthlyQualityGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonthlyQualityGoal);

				TableSchema.TableColumn colvarYearlyQualityGoal = new TableSchema.TableColumn(schema);
				colvarYearlyQualityGoal.ColumnName = "YearlyQualityGoal";
				colvarYearlyQualityGoal.DataType = DbType.Double;
				colvarYearlyQualityGoal.MaxLength = 0;
				colvarYearlyQualityGoal.AutoIncrement = false;
				colvarYearlyQualityGoal.IsNullable = true;
				colvarYearlyQualityGoal.IsPrimaryKey = false;
				colvarYearlyQualityGoal.IsForeignKey = false;
				colvarYearlyQualityGoal.IsReadOnly = false;
				colvarYearlyQualityGoal.DefaultSetting = @"";
				colvarYearlyQualityGoal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarYearlyQualityGoal);

				TableSchema.TableColumn colvarLicense1 = new TableSchema.TableColumn(schema);
				colvarLicense1.ColumnName = "License1";
				colvarLicense1.DataType = DbType.AnsiString;
				colvarLicense1.MaxLength = 18;
				colvarLicense1.AutoIncrement = false;
				colvarLicense1.IsNullable = false;
				colvarLicense1.IsPrimaryKey = false;
				colvarLicense1.IsForeignKey = false;
				colvarLicense1.IsReadOnly = false;
				colvarLicense1.DefaultSetting = @"";
				colvarLicense1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense1);

				TableSchema.TableColumn colvarLicense1URL = new TableSchema.TableColumn(schema);
				colvarLicense1URL.ColumnName = "License1URL";
				colvarLicense1URL.DataType = DbType.AnsiString;
				colvarLicense1URL.MaxLength = 16;
				colvarLicense1URL.AutoIncrement = false;
				colvarLicense1URL.IsNullable = false;
				colvarLicense1URL.IsPrimaryKey = false;
				colvarLicense1URL.IsForeignKey = false;
				colvarLicense1URL.IsReadOnly = false;
				colvarLicense1URL.DefaultSetting = @"";
				colvarLicense1URL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense1URL);

				TableSchema.TableColumn colvarLicense2 = new TableSchema.TableColumn(schema);
				colvarLicense2.ColumnName = "License2";
				colvarLicense2.DataType = DbType.AnsiString;
				colvarLicense2.MaxLength = 29;
				colvarLicense2.AutoIncrement = false;
				colvarLicense2.IsNullable = false;
				colvarLicense2.IsPrimaryKey = false;
				colvarLicense2.IsForeignKey = false;
				colvarLicense2.IsReadOnly = false;
				colvarLicense2.DefaultSetting = @"";
				colvarLicense2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense2);

				TableSchema.TableColumn colvarLicense2URL = new TableSchema.TableColumn(schema);
				colvarLicense2URL.ColumnName = "License2URL";
				colvarLicense2URL.DataType = DbType.AnsiString;
				colvarLicense2URL.MaxLength = 14;
				colvarLicense2URL.AutoIncrement = false;
				colvarLicense2URL.IsNullable = false;
				colvarLicense2URL.IsPrimaryKey = false;
				colvarLicense2URL.IsForeignKey = false;
				colvarLicense2URL.IsReadOnly = false;
				colvarLicense2URL.DefaultSetting = @"";
				colvarLicense2URL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense2URL);

				TableSchema.TableColumn colvarLicense3 = new TableSchema.TableColumn(schema);
				colvarLicense3.ColumnName = "License3";
				colvarLicense3.DataType = DbType.AnsiString;
				colvarLicense3.MaxLength = 29;
				colvarLicense3.AutoIncrement = false;
				colvarLicense3.IsNullable = false;
				colvarLicense3.IsPrimaryKey = false;
				colvarLicense3.IsForeignKey = false;
				colvarLicense3.IsReadOnly = false;
				colvarLicense3.DefaultSetting = @"";
				colvarLicense3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense3);

				TableSchema.TableColumn colvarLicense3URL = new TableSchema.TableColumn(schema);
				colvarLicense3URL.ColumnName = "License3URL";
				colvarLicense3URL.DataType = DbType.AnsiString;
				colvarLicense3URL.MaxLength = 16;
				colvarLicense3URL.AutoIncrement = false;
				colvarLicense3URL.IsNullable = false;
				colvarLicense3URL.IsPrimaryKey = false;
				colvarLicense3URL.IsForeignKey = false;
				colvarLicense3URL.IsReadOnly = false;
				colvarLicense3URL.DefaultSetting = @"";
				colvarLicense3URL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLicense3URL);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersSalesInfoExtendedConnext",schema);
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
		public RU_UsersSalesInfoExtendedConnextView()
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
		public string PhotoURL {
			get { return GetColumnValue<string>(Columns.PhotoURL); }
			set { SetColumnValue(Columns.PhotoURL, value); }
		}
		[DataMember]
		public long? MLMDepth {
			get { return GetColumnValue<long?>(Columns.MLMDepth); }
			set { SetColumnValue(Columns.MLMDepth, value); }
		}
		[DataMember]
		public bool? ManagerHasOwnTeam {
			get { return GetColumnValue<bool?>(Columns.ManagerHasOwnTeam); }
			set { SetColumnValue(Columns.ManagerHasOwnTeam, value); }
		}
		[DataMember]
		public string RegionName {
			get { return GetColumnValue<string>(Columns.RegionName); }
			set { SetColumnValue(Columns.RegionName, value); }
		}
		[DataMember]
		public string OfficeName {
			get { return GetColumnValue<string>(Columns.OfficeName); }
			set { SetColumnValue(Columns.OfficeName, value); }
		}
		[DataMember]
		public string TeamName {
			get { return GetColumnValue<string>(Columns.TeamName); }
			set { SetColumnValue(Columns.TeamName, value); }
		}
		[DataMember]
		public long? CurrentNationalRank {
			get { return GetColumnValue<long?>(Columns.CurrentNationalRank); }
			set { SetColumnValue(Columns.CurrentNationalRank, value); }
		}
		[DataMember]
		public long? PreviousNationalRank {
			get { return GetColumnValue<long?>(Columns.PreviousNationalRank); }
			set { SetColumnValue(Columns.PreviousNationalRank, value); }
		}
		[DataMember]
		public long? CurrentRegionalRank {
			get { return GetColumnValue<long?>(Columns.CurrentRegionalRank); }
			set { SetColumnValue(Columns.CurrentRegionalRank, value); }
		}
		[DataMember]
		public long? PreviousRegionalRank {
			get { return GetColumnValue<long?>(Columns.PreviousRegionalRank); }
			set { SetColumnValue(Columns.PreviousRegionalRank, value); }
		}
		[DataMember]
		public long? CurrentOfficeRank {
			get { return GetColumnValue<long?>(Columns.CurrentOfficeRank); }
			set { SetColumnValue(Columns.CurrentOfficeRank, value); }
		}
		[DataMember]
		public long? PreviousOfficeRank {
			get { return GetColumnValue<long?>(Columns.PreviousOfficeRank); }
			set { SetColumnValue(Columns.PreviousOfficeRank, value); }
		}
		[DataMember]
		public long? CurrentTeamRank {
			get { return GetColumnValue<long?>(Columns.CurrentTeamRank); }
			set { SetColumnValue(Columns.CurrentTeamRank, value); }
		}
		[DataMember]
		public long? PreviousTeamRank {
			get { return GetColumnValue<long?>(Columns.PreviousTeamRank); }
			set { SetColumnValue(Columns.PreviousTeamRank, value); }
		}
		[DataMember]
		public DateTime? StartDate {
			get { return GetColumnValue<DateTime?>(Columns.StartDate); }
			set { SetColumnValue(Columns.StartDate, value); }
		}
		[DataMember]
		public string Phone {
			get { return GetColumnValue<string>(Columns.Phone); }
			set { SetColumnValue(Columns.Phone, value); }
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set { SetColumnValue(Columns.Email, value); }
		}
		[DataMember]
		public string StreetAddress {
			get { return GetColumnValue<string>(Columns.StreetAddress); }
			set { SetColumnValue(Columns.StreetAddress, value); }
		}
		[DataMember]
		public string StreetAddress2 {
			get { return GetColumnValue<string>(Columns.StreetAddress2); }
			set { SetColumnValue(Columns.StreetAddress2, value); }
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
		public string Zip {
			get { return GetColumnValue<string>(Columns.Zip); }
			set { SetColumnValue(Columns.Zip, value); }
		}
		[DataMember]
		public int? WeeklySalesGoal {
			get { return GetColumnValue<int?>(Columns.WeeklySalesGoal); }
			set { SetColumnValue(Columns.WeeklySalesGoal, value); }
		}
		[DataMember]
		public int? MonthlySalesGoal {
			get { return GetColumnValue<int?>(Columns.MonthlySalesGoal); }
			set { SetColumnValue(Columns.MonthlySalesGoal, value); }
		}
		[DataMember]
		public int? YearlySalesGoal {
			get { return GetColumnValue<int?>(Columns.YearlySalesGoal); }
			set { SetColumnValue(Columns.YearlySalesGoal, value); }
		}
		[DataMember]
		public double? WeeklyQualityGoal {
			get { return GetColumnValue<double?>(Columns.WeeklyQualityGoal); }
			set { SetColumnValue(Columns.WeeklyQualityGoal, value); }
		}
		[DataMember]
		public double? MonthlyQualityGoal {
			get { return GetColumnValue<double?>(Columns.MonthlyQualityGoal); }
			set { SetColumnValue(Columns.MonthlyQualityGoal, value); }
		}
		[DataMember]
		public double? YearlyQualityGoal {
			get { return GetColumnValue<double?>(Columns.YearlyQualityGoal); }
			set { SetColumnValue(Columns.YearlyQualityGoal, value); }
		}
		[DataMember]
		public string License1 {
			get { return GetColumnValue<string>(Columns.License1); }
			set { SetColumnValue(Columns.License1, value); }
		}
		[DataMember]
		public string License1URL {
			get { return GetColumnValue<string>(Columns.License1URL); }
			set { SetColumnValue(Columns.License1URL, value); }
		}
		[DataMember]
		public string License2 {
			get { return GetColumnValue<string>(Columns.License2); }
			set { SetColumnValue(Columns.License2, value); }
		}
		[DataMember]
		public string License2URL {
			get { return GetColumnValue<string>(Columns.License2URL); }
			set { SetColumnValue(Columns.License2URL, value); }
		}
		[DataMember]
		public string License3 {
			get { return GetColumnValue<string>(Columns.License3); }
			set { SetColumnValue(Columns.License3, value); }
		}
		[DataMember]
		public string License3URL {
			get { return GetColumnValue<string>(Columns.License3URL); }
			set { SetColumnValue(Columns.License3URL, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FirstName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhotoURLColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn MLMDepthColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ManagerHasOwnTeamColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn RegionNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn OfficeNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TeamNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CurrentNationalRankColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn PreviousNationalRankColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn CurrentRegionalRankColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn PreviousRegionalRankColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn CurrentOfficeRankColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn PreviousOfficeRankColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn CurrentTeamRankColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn PreviousTeamRankColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn StartDateColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn PhoneColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn StreetAddressColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn StreetAddress2Column
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn StateColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn ZipColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn WeeklySalesGoalColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn MonthlySalesGoalColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn YearlySalesGoalColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn WeeklyQualityGoalColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn MonthlyQualityGoalColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn YearlyQualityGoalColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn License1Column
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn License1URLColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn License2Column
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn License2URLColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn License3Column
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn License3URLColumn
		{
			get { return Schema.Columns[37]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PhotoURL = @"PhotoURL";
			public const string MLMDepth = @"MLMDepth";
			public const string ManagerHasOwnTeam = @"ManagerHasOwnTeam";
			public const string RegionName = @"RegionName";
			public const string OfficeName = @"OfficeName";
			public const string TeamName = @"TeamName";
			public const string CurrentNationalRank = @"CurrentNationalRank";
			public const string PreviousNationalRank = @"PreviousNationalRank";
			public const string CurrentRegionalRank = @"CurrentRegionalRank";
			public const string PreviousRegionalRank = @"PreviousRegionalRank";
			public const string CurrentOfficeRank = @"CurrentOfficeRank";
			public const string PreviousOfficeRank = @"PreviousOfficeRank";
			public const string CurrentTeamRank = @"CurrentTeamRank";
			public const string PreviousTeamRank = @"PreviousTeamRank";
			public const string StartDate = @"StartDate";
			public const string Phone = @"Phone";
			public const string Email = @"Email";
			public const string StreetAddress = @"StreetAddress";
			public const string StreetAddress2 = @"StreetAddress2";
			public const string City = @"City";
			public const string State = @"State";
			public const string Zip = @"Zip";
			public const string WeeklySalesGoal = @"WeeklySalesGoal";
			public const string MonthlySalesGoal = @"MonthlySalesGoal";
			public const string YearlySalesGoal = @"YearlySalesGoal";
			public const string WeeklyQualityGoal = @"WeeklyQualityGoal";
			public const string MonthlyQualityGoal = @"MonthlyQualityGoal";
			public const string YearlyQualityGoal = @"YearlyQualityGoal";
			public const string License1 = @"License1";
			public const string License1URL = @"License1URL";
			public const string License2 = @"License2";
			public const string License2URL = @"License2URL";
			public const string License3 = @"License3";
			public const string License3URL = @"License3URL";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the RU_UsersSalesRankingConnextView class.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesRankingConnextViewCollection : ReadOnlyList<RU_UsersSalesRankingConnextView, RU_UsersSalesRankingConnextViewCollection>
	{
		public static RU_UsersSalesRankingConnextViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			RU_UsersSalesRankingConnextViewCollection result = new RU_UsersSalesRankingConnextViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwRU_UsersSalesRankingConnext view.
	/// </summary>
	[DataContract]
	public partial class RU_UsersSalesRankingConnextView : ReadOnlyRecord<RU_UsersSalesRankingConnextView>
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
				TableSchema.Table schema = new TableSchema.Table("vwRU_UsersSalesRankingConnext", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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
				colvarMiddleName.IsNullable = false;
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

				TableSchema.TableColumn colvarPhotoURL = new TableSchema.TableColumn(schema);
				colvarPhotoURL.ColumnName = "PhotoURL";
				colvarPhotoURL.DataType = DbType.AnsiString;
				colvarPhotoURL.MaxLength = 65;
				colvarPhotoURL.AutoIncrement = false;
				colvarPhotoURL.IsNullable = false;
				colvarPhotoURL.IsPrimaryKey = false;
				colvarPhotoURL.IsForeignKey = false;
				colvarPhotoURL.IsReadOnly = false;
				colvarPhotoURL.DefaultSetting = @"";
				colvarPhotoURL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhotoURL);

				TableSchema.TableColumn colvarPeriodEndingDate = new TableSchema.TableColumn(schema);
				colvarPeriodEndingDate.ColumnName = "PeriodEndingDate";
				colvarPeriodEndingDate.DataType = DbType.DateTime;
				colvarPeriodEndingDate.MaxLength = 0;
				colvarPeriodEndingDate.AutoIncrement = false;
				colvarPeriodEndingDate.IsNullable = false;
				colvarPeriodEndingDate.IsPrimaryKey = false;
				colvarPeriodEndingDate.IsForeignKey = false;
				colvarPeriodEndingDate.IsReadOnly = false;
				colvarPeriodEndingDate.DefaultSetting = @"";
				colvarPeriodEndingDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPeriodEndingDate);

				TableSchema.TableColumn colvarResultsType = new TableSchema.TableColumn(schema);
				colvarResultsType.ColumnName = "ResultsType";
				colvarResultsType.DataType = DbType.AnsiString;
				colvarResultsType.MaxLength = 20;
				colvarResultsType.AutoIncrement = false;
				colvarResultsType.IsNullable = false;
				colvarResultsType.IsPrimaryKey = false;
				colvarResultsType.IsForeignKey = false;
				colvarResultsType.IsReadOnly = false;
				colvarResultsType.DefaultSetting = @"";
				colvarResultsType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResultsType);

				TableSchema.TableColumn colvarRankingGroup = new TableSchema.TableColumn(schema);
				colvarRankingGroup.ColumnName = "RankingGroup";
				colvarRankingGroup.DataType = DbType.AnsiString;
				colvarRankingGroup.MaxLength = 20;
				colvarRankingGroup.AutoIncrement = false;
				colvarRankingGroup.IsNullable = false;
				colvarRankingGroup.IsPrimaryKey = false;
				colvarRankingGroup.IsForeignKey = false;
				colvarRankingGroup.IsReadOnly = false;
				colvarRankingGroup.DefaultSetting = @"";
				colvarRankingGroup.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRankingGroup);

				TableSchema.TableColumn colvarRankingPeriod = new TableSchema.TableColumn(schema);
				colvarRankingPeriod.ColumnName = "RankingPeriod";
				colvarRankingPeriod.DataType = DbType.AnsiString;
				colvarRankingPeriod.MaxLength = 20;
				colvarRankingPeriod.AutoIncrement = false;
				colvarRankingPeriod.IsNullable = false;
				colvarRankingPeriod.IsPrimaryKey = false;
				colvarRankingPeriod.IsForeignKey = false;
				colvarRankingPeriod.IsReadOnly = false;
				colvarRankingPeriod.DefaultSetting = @"";
				colvarRankingPeriod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRankingPeriod);

				TableSchema.TableColumn colvarCurrentResults = new TableSchema.TableColumn(schema);
				colvarCurrentResults.ColumnName = "CurrentResults";
				colvarCurrentResults.DataType = DbType.Int32;
				colvarCurrentResults.MaxLength = 0;
				colvarCurrentResults.AutoIncrement = false;
				colvarCurrentResults.IsNullable = false;
				colvarCurrentResults.IsPrimaryKey = false;
				colvarCurrentResults.IsForeignKey = false;
				colvarCurrentResults.IsReadOnly = false;
				colvarCurrentResults.DefaultSetting = @"";
				colvarCurrentResults.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentResults);

				TableSchema.TableColumn colvarCurrentSequence = new TableSchema.TableColumn(schema);
				colvarCurrentSequence.ColumnName = "CurrentSequence";
				colvarCurrentSequence.DataType = DbType.Int32;
				colvarCurrentSequence.MaxLength = 0;
				colvarCurrentSequence.AutoIncrement = false;
				colvarCurrentSequence.IsNullable = false;
				colvarCurrentSequence.IsPrimaryKey = false;
				colvarCurrentSequence.IsForeignKey = false;
				colvarCurrentSequence.IsReadOnly = false;
				colvarCurrentSequence.DefaultSetting = @"";
				colvarCurrentSequence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentSequence);

				TableSchema.TableColumn colvarCurrentRanking = new TableSchema.TableColumn(schema);
				colvarCurrentRanking.ColumnName = "CurrentRanking";
				colvarCurrentRanking.DataType = DbType.Int32;
				colvarCurrentRanking.MaxLength = 0;
				colvarCurrentRanking.AutoIncrement = false;
				colvarCurrentRanking.IsNullable = false;
				colvarCurrentRanking.IsPrimaryKey = false;
				colvarCurrentRanking.IsForeignKey = false;
				colvarCurrentRanking.IsReadOnly = false;
				colvarCurrentRanking.DefaultSetting = @"";
				colvarCurrentRanking.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentRanking);

				TableSchema.TableColumn colvarPreviousResults = new TableSchema.TableColumn(schema);
				colvarPreviousResults.ColumnName = "PreviousResults";
				colvarPreviousResults.DataType = DbType.Int32;
				colvarPreviousResults.MaxLength = 0;
				colvarPreviousResults.AutoIncrement = false;
				colvarPreviousResults.IsNullable = false;
				colvarPreviousResults.IsPrimaryKey = false;
				colvarPreviousResults.IsForeignKey = false;
				colvarPreviousResults.IsReadOnly = false;
				colvarPreviousResults.DefaultSetting = @"";
				colvarPreviousResults.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousResults);

				TableSchema.TableColumn colvarPreviousSequence = new TableSchema.TableColumn(schema);
				colvarPreviousSequence.ColumnName = "PreviousSequence";
				colvarPreviousSequence.DataType = DbType.Int32;
				colvarPreviousSequence.MaxLength = 0;
				colvarPreviousSequence.AutoIncrement = false;
				colvarPreviousSequence.IsNullable = false;
				colvarPreviousSequence.IsPrimaryKey = false;
				colvarPreviousSequence.IsForeignKey = false;
				colvarPreviousSequence.IsReadOnly = false;
				colvarPreviousSequence.DefaultSetting = @"";
				colvarPreviousSequence.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousSequence);

				TableSchema.TableColumn colvarPreviousRanking = new TableSchema.TableColumn(schema);
				colvarPreviousRanking.ColumnName = "PreviousRanking";
				colvarPreviousRanking.DataType = DbType.Int32;
				colvarPreviousRanking.MaxLength = 0;
				colvarPreviousRanking.AutoIncrement = false;
				colvarPreviousRanking.IsNullable = false;
				colvarPreviousRanking.IsPrimaryKey = false;
				colvarPreviousRanking.IsForeignKey = false;
				colvarPreviousRanking.IsReadOnly = false;
				colvarPreviousRanking.DefaultSetting = @"";
				colvarPreviousRanking.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreviousRanking);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwRU_UsersSalesRankingConnext",schema);
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
		public RU_UsersSalesRankingConnextView()
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
		public string PhotoURL {
			get { return GetColumnValue<string>(Columns.PhotoURL); }
			set { SetColumnValue(Columns.PhotoURL, value); }
		}
		[DataMember]
		public DateTime PeriodEndingDate {
			get { return GetColumnValue<DateTime>(Columns.PeriodEndingDate); }
			set { SetColumnValue(Columns.PeriodEndingDate, value); }
		}
		[DataMember]
		public string ResultsType {
			get { return GetColumnValue<string>(Columns.ResultsType); }
			set { SetColumnValue(Columns.ResultsType, value); }
		}
		[DataMember]
		public string RankingGroup {
			get { return GetColumnValue<string>(Columns.RankingGroup); }
			set { SetColumnValue(Columns.RankingGroup, value); }
		}
		[DataMember]
		public string RankingPeriod {
			get { return GetColumnValue<string>(Columns.RankingPeriod); }
			set { SetColumnValue(Columns.RankingPeriod, value); }
		}
		[DataMember]
		public int CurrentResults {
			get { return GetColumnValue<int>(Columns.CurrentResults); }
			set { SetColumnValue(Columns.CurrentResults, value); }
		}
		[DataMember]
		public int CurrentSequence {
			get { return GetColumnValue<int>(Columns.CurrentSequence); }
			set { SetColumnValue(Columns.CurrentSequence, value); }
		}
		[DataMember]
		public int CurrentRanking {
			get { return GetColumnValue<int>(Columns.CurrentRanking); }
			set { SetColumnValue(Columns.CurrentRanking, value); }
		}
		[DataMember]
		public int PreviousResults {
			get { return GetColumnValue<int>(Columns.PreviousResults); }
			set { SetColumnValue(Columns.PreviousResults, value); }
		}
		[DataMember]
		public int PreviousSequence {
			get { return GetColumnValue<int>(Columns.PreviousSequence); }
			set { SetColumnValue(Columns.PreviousSequence, value); }
		}
		[DataMember]
		public int PreviousRanking {
			get { return GetColumnValue<int>(Columns.PreviousRanking); }
			set { SetColumnValue(Columns.PreviousRanking, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return FirstName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn UserIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn PhotoURLColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn PeriodEndingDateColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ResultsTypeColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn RankingGroupColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn RankingPeriodColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CurrentResultsColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn CurrentSequenceColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn CurrentRankingColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn PreviousResultsColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn PreviousSequenceColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn PreviousRankingColumn
		{
			get { return Schema.Columns[14]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FirstName = @"FirstName";
			public const string MiddleName = @"MiddleName";
			public const string LastName = @"LastName";
			public const string PhotoURL = @"PhotoURL";
			public const string PeriodEndingDate = @"PeriodEndingDate";
			public const string ResultsType = @"ResultsType";
			public const string RankingGroup = @"RankingGroup";
			public const string RankingPeriod = @"RankingPeriod";
			public const string CurrentResults = @"CurrentResults";
			public const string CurrentSequence = @"CurrentSequence";
			public const string CurrentRanking = @"CurrentRanking";
			public const string PreviousResults = @"PreviousResults";
			public const string PreviousSequence = @"PreviousSequence";
			public const string PreviousRanking = @"PreviousRanking";
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

				TableSchema.TableColumn colvarPermanentAddressID = new TableSchema.TableColumn(schema);
				colvarPermanentAddressID.ColumnName = "PermanentAddressID";
				colvarPermanentAddressID.DataType = DbType.Int32;
				colvarPermanentAddressID.MaxLength = 0;
				colvarPermanentAddressID.AutoIncrement = false;
				colvarPermanentAddressID.IsNullable = true;
				colvarPermanentAddressID.IsPrimaryKey = false;
				colvarPermanentAddressID.IsForeignKey = false;
				colvarPermanentAddressID.IsReadOnly = false;
				colvarPermanentAddressID.DefaultSetting = @"";
				colvarPermanentAddressID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPermanentAddressID);

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
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 50;
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
		public string UserEmployeeTypeId {
			get { return GetColumnValue<string>(Columns.UserEmployeeTypeId); }
			set { SetColumnValue(Columns.UserEmployeeTypeId, value); }
		}
		[DataMember]
		public int? PermanentAddressID {
			get { return GetColumnValue<int?>(Columns.PermanentAddressID); }
			set { SetColumnValue(Columns.PermanentAddressID, value); }
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
		public static TableSchema.TableColumn RecruitedByIDColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn GPEmployeeIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn UserEmployeeTypeIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn PermanentAddressIDColumn
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
		public static TableSchema.TableColumn DLExpirationColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn HeightColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn WeightColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn EyeColorColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn HairColorColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn PhoneHomeColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn PhoneCellColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn PhoneCellCarrierIDColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn PhoneFaxColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn CorporateEmailColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn TreeLevelColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn HasVerifiedAddressColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn RightToWorkExpirationDateColumn
		{
			get { return Schema.Columns[41]; }
		}
		public static TableSchema.TableColumn RightToWorkNotesColumn
		{
			get { return Schema.Columns[42]; }
		}
		public static TableSchema.TableColumn RightToWorkStatusIDColumn
		{
			get { return Schema.Columns[43]; }
		}
		public static TableSchema.TableColumn IsLockedColumn
		{
			get { return Schema.Columns[44]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[45]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[46]; }
		}
		public static TableSchema.TableColumn RecruitedDateColumn
		{
			get { return Schema.Columns[47]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[48]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[49]; }
		}
		public static TableSchema.TableColumn ModifiedByColumn
		{
			get { return Schema.Columns[50]; }
		}
		public static TableSchema.TableColumn ModifiedOnColumn
		{
			get { return Schema.Columns[51]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string FullName = @"FullName";
			public const string PublicFullName = @"PublicFullName";
			public const string RecruitedByID = @"RecruitedByID";
			public const string GPEmployeeID = @"GPEmployeeID";
			public const string UserEmployeeTypeId = @"UserEmployeeTypeId";
			public const string PermanentAddressID = @"PermanentAddressID";
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
	/// Strongly-typed collection for the SAE_SalesSalespersonMonthlyCommissionsView class.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyCommissionsViewCollection : ReadOnlyList<SAE_SalesSalespersonMonthlyCommissionsView, SAE_SalesSalespersonMonthlyCommissionsViewCollection>
	{
		public static SAE_SalesSalespersonMonthlyCommissionsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SAE_SalesSalespersonMonthlyCommissionsViewCollection result = new SAE_SalesSalespersonMonthlyCommissionsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwSAE_SalesSalespersonMonthlyCommissions view.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyCommissionsView : ReadOnlyRecord<SAE_SalesSalespersonMonthlyCommissionsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwSAE_SalesSalespersonMonthlyCommissions", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarContractDate = new TableSchema.TableColumn(schema);
				colvarContractDate.ColumnName = "ContractDate";
				colvarContractDate.DataType = DbType.DateTime;
				colvarContractDate.MaxLength = 0;
				colvarContractDate.AutoIncrement = false;
				colvarContractDate.IsNullable = true;
				colvarContractDate.IsPrimaryKey = false;
				colvarContractDate.IsForeignKey = false;
				colvarContractDate.IsReadOnly = false;
				colvarContractDate.DefaultSetting = @"";
				colvarContractDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractDate);

				TableSchema.TableColumn colvarSalesMonth = new TableSchema.TableColumn(schema);
				colvarSalesMonth.ColumnName = "SalesMonth";
				colvarSalesMonth.DataType = DbType.Int32;
				colvarSalesMonth.MaxLength = 0;
				colvarSalesMonth.AutoIncrement = false;
				colvarSalesMonth.IsNullable = true;
				colvarSalesMonth.IsPrimaryKey = false;
				colvarSalesMonth.IsForeignKey = false;
				colvarSalesMonth.IsReadOnly = false;
				colvarSalesMonth.DefaultSetting = @"";
				colvarSalesMonth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesMonth);

				TableSchema.TableColumn colvarSalesYear = new TableSchema.TableColumn(schema);
				colvarSalesYear.ColumnName = "SalesYear";
				colvarSalesYear.DataType = DbType.Int32;
				colvarSalesYear.MaxLength = 0;
				colvarSalesYear.AutoIncrement = false;
				colvarSalesYear.IsNullable = true;
				colvarSalesYear.IsPrimaryKey = false;
				colvarSalesYear.IsForeignKey = false;
				colvarSalesYear.IsReadOnly = false;
				colvarSalesYear.DefaultSetting = @"";
				colvarSalesYear.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesYear);

				TableSchema.TableColumn colvarCustomerMasterFileID = new TableSchema.TableColumn(schema);
				colvarCustomerMasterFileID.ColumnName = "CustomerMasterFileID";
				colvarCustomerMasterFileID.DataType = DbType.Int64;
				colvarCustomerMasterFileID.MaxLength = 0;
				colvarCustomerMasterFileID.AutoIncrement = false;
				colvarCustomerMasterFileID.IsNullable = false;
				colvarCustomerMasterFileID.IsPrimaryKey = false;
				colvarCustomerMasterFileID.IsForeignKey = false;
				colvarCustomerMasterFileID.IsReadOnly = false;
				colvarCustomerMasterFileID.DefaultSetting = @"";
				colvarCustomerMasterFileID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMasterFileID);

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

				TableSchema.TableColumn colvarCustomerFirstName = new TableSchema.TableColumn(schema);
				colvarCustomerFirstName.ColumnName = "CustomerFirstName";
				colvarCustomerFirstName.DataType = DbType.String;
				colvarCustomerFirstName.MaxLength = 50;
				colvarCustomerFirstName.AutoIncrement = false;
				colvarCustomerFirstName.IsNullable = false;
				colvarCustomerFirstName.IsPrimaryKey = false;
				colvarCustomerFirstName.IsForeignKey = false;
				colvarCustomerFirstName.IsReadOnly = false;
				colvarCustomerFirstName.DefaultSetting = @"";
				colvarCustomerFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerFirstName);

				TableSchema.TableColumn colvarCustomerMiddleName = new TableSchema.TableColumn(schema);
				colvarCustomerMiddleName.ColumnName = "CustomerMiddleName";
				colvarCustomerMiddleName.DataType = DbType.String;
				colvarCustomerMiddleName.MaxLength = 50;
				colvarCustomerMiddleName.AutoIncrement = false;
				colvarCustomerMiddleName.IsNullable = false;
				colvarCustomerMiddleName.IsPrimaryKey = false;
				colvarCustomerMiddleName.IsForeignKey = false;
				colvarCustomerMiddleName.IsReadOnly = false;
				colvarCustomerMiddleName.DefaultSetting = @"";
				colvarCustomerMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMiddleName);

				TableSchema.TableColumn colvarCustomerLastName = new TableSchema.TableColumn(schema);
				colvarCustomerLastName.ColumnName = "CustomerLastName";
				colvarCustomerLastName.DataType = DbType.String;
				colvarCustomerLastName.MaxLength = 50;
				colvarCustomerLastName.AutoIncrement = false;
				colvarCustomerLastName.IsNullable = false;
				colvarCustomerLastName.IsPrimaryKey = false;
				colvarCustomerLastName.IsForeignKey = false;
				colvarCustomerLastName.IsReadOnly = false;
				colvarCustomerLastName.DefaultSetting = @"";
				colvarCustomerLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerLastName);

				TableSchema.TableColumn colvarCreditRating = new TableSchema.TableColumn(schema);
				colvarCreditRating.ColumnName = "CreditRating";
				colvarCreditRating.DataType = DbType.AnsiString;
				colvarCreditRating.MaxLength = 20;
				colvarCreditRating.AutoIncrement = false;
				colvarCreditRating.IsNullable = true;
				colvarCreditRating.IsPrimaryKey = false;
				colvarCreditRating.IsForeignKey = false;
				colvarCreditRating.IsReadOnly = false;
				colvarCreditRating.DefaultSetting = @"";
				colvarCreditRating.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreditRating);

				TableSchema.TableColumn colvarActivationFeeAmt = new TableSchema.TableColumn(schema);
				colvarActivationFeeAmt.ColumnName = "ActivationFeeAmt";
				colvarActivationFeeAmt.DataType = DbType.Currency;
				colvarActivationFeeAmt.MaxLength = 0;
				colvarActivationFeeAmt.AutoIncrement = false;
				colvarActivationFeeAmt.IsNullable = true;
				colvarActivationFeeAmt.IsPrimaryKey = false;
				colvarActivationFeeAmt.IsForeignKey = false;
				colvarActivationFeeAmt.IsReadOnly = false;
				colvarActivationFeeAmt.DefaultSetting = @"";
				colvarActivationFeeAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActivationFeeAmt);

				TableSchema.TableColumn colvarContractLength = new TableSchema.TableColumn(schema);
				colvarContractLength.ColumnName = "ContractLength";
				colvarContractLength.DataType = DbType.Int32;
				colvarContractLength.MaxLength = 0;
				colvarContractLength.AutoIncrement = false;
				colvarContractLength.IsNullable = true;
				colvarContractLength.IsPrimaryKey = false;
				colvarContractLength.IsForeignKey = false;
				colvarContractLength.IsReadOnly = false;
				colvarContractLength.DefaultSetting = @"";
				colvarContractLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractLength);

				TableSchema.TableColumn colvarServiceType = new TableSchema.TableColumn(schema);
				colvarServiceType.ColumnName = "ServiceType";
				colvarServiceType.DataType = DbType.AnsiString;
				colvarServiceType.MaxLength = 50;
				colvarServiceType.AutoIncrement = false;
				colvarServiceType.IsNullable = true;
				colvarServiceType.IsPrimaryKey = false;
				colvarServiceType.IsForeignKey = false;
				colvarServiceType.IsReadOnly = false;
				colvarServiceType.DefaultSetting = @"";
				colvarServiceType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarServiceType);

				TableSchema.TableColumn colvarMonthlyPaymentAmt = new TableSchema.TableColumn(schema);
				colvarMonthlyPaymentAmt.ColumnName = "MonthlyPaymentAmt";
				colvarMonthlyPaymentAmt.DataType = DbType.Currency;
				colvarMonthlyPaymentAmt.MaxLength = 0;
				colvarMonthlyPaymentAmt.AutoIncrement = false;
				colvarMonthlyPaymentAmt.IsNullable = true;
				colvarMonthlyPaymentAmt.IsPrimaryKey = false;
				colvarMonthlyPaymentAmt.IsForeignKey = false;
				colvarMonthlyPaymentAmt.IsReadOnly = false;
				colvarMonthlyPaymentAmt.DefaultSetting = @"";
				colvarMonthlyPaymentAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMonthlyPaymentAmt);

				TableSchema.TableColumn colvarPaymentMethod = new TableSchema.TableColumn(schema);
				colvarPaymentMethod.ColumnName = "PaymentMethod";
				colvarPaymentMethod.DataType = DbType.AnsiString;
				colvarPaymentMethod.MaxLength = 50;
				colvarPaymentMethod.AutoIncrement = false;
				colvarPaymentMethod.IsNullable = true;
				colvarPaymentMethod.IsPrimaryKey = false;
				colvarPaymentMethod.IsForeignKey = false;
				colvarPaymentMethod.IsReadOnly = false;
				colvarPaymentMethod.DefaultSetting = @"";
				colvarPaymentMethod.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPaymentMethod);

				TableSchema.TableColumn colvarSalesCommissionAmt = new TableSchema.TableColumn(schema);
				colvarSalesCommissionAmt.ColumnName = "SalesCommissionAmt";
				colvarSalesCommissionAmt.DataType = DbType.Currency;
				colvarSalesCommissionAmt.MaxLength = 0;
				colvarSalesCommissionAmt.AutoIncrement = false;
				colvarSalesCommissionAmt.IsNullable = true;
				colvarSalesCommissionAmt.IsPrimaryKey = false;
				colvarSalesCommissionAmt.IsForeignKey = false;
				colvarSalesCommissionAmt.IsReadOnly = false;
				colvarSalesCommissionAmt.DefaultSetting = @"";
				colvarSalesCommissionAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesCommissionAmt);

				TableSchema.TableColumn colvarRecurringCommissionAmt = new TableSchema.TableColumn(schema);
				colvarRecurringCommissionAmt.ColumnName = "RecurringCommissionAmt";
				colvarRecurringCommissionAmt.DataType = DbType.Currency;
				colvarRecurringCommissionAmt.MaxLength = 0;
				colvarRecurringCommissionAmt.AutoIncrement = false;
				colvarRecurringCommissionAmt.IsNullable = true;
				colvarRecurringCommissionAmt.IsPrimaryKey = false;
				colvarRecurringCommissionAmt.IsForeignKey = false;
				colvarRecurringCommissionAmt.IsReadOnly = false;
				colvarRecurringCommissionAmt.DefaultSetting = @"";
				colvarRecurringCommissionAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecurringCommissionAmt);

				TableSchema.TableColumn colvarisActive = new TableSchema.TableColumn(schema);
				colvarisActive.ColumnName = "isActive";
				colvarisActive.DataType = DbType.Boolean;
				colvarisActive.MaxLength = 0;
				colvarisActive.AutoIncrement = false;
				colvarisActive.IsNullable = true;
				colvarisActive.IsPrimaryKey = false;
				colvarisActive.IsForeignKey = false;
				colvarisActive.IsReadOnly = false;
				colvarisActive.DefaultSetting = @"";
				colvarisActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarisActive);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwSAE_SalesSalespersonMonthlyCommissions",schema);
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
		public SAE_SalesSalespersonMonthlyCommissionsView()
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
		public DateTime ContractDate {
			get { return GetColumnValue<DateTime>(Columns.ContractDate); }
			set { SetColumnValue(Columns.ContractDate, value); }
		}
		[DataMember]
		public int SalesMonth {
			get { return GetColumnValue<int>(Columns.SalesMonth); }
			set { SetColumnValue(Columns.SalesMonth, value); }
		}
		[DataMember]
		public int SalesYear {
			get { return GetColumnValue<int>(Columns.SalesYear); }
			set { SetColumnValue(Columns.SalesYear, value); }
		}
		[DataMember]
		public long CustomerMasterFileID {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileID); }
			set { SetColumnValue(Columns.CustomerMasterFileID, value); }
		}
		[DataMember]
		public long AccountID {
			get { return GetColumnValue<long>(Columns.AccountID); }
			set { SetColumnValue(Columns.AccountID, value); }
		}
		[DataMember]
		public string CustomerFirstName {
			get { return GetColumnValue<string>(Columns.CustomerFirstName); }
			set { SetColumnValue(Columns.CustomerFirstName, value); }
		}
		[DataMember]
		public string CustomerMiddleName {
			get { return GetColumnValue<string>(Columns.CustomerMiddleName); }
			set { SetColumnValue(Columns.CustomerMiddleName, value); }
		}
		[DataMember]
		public string CustomerLastName {
			get { return GetColumnValue<string>(Columns.CustomerLastName); }
			set { SetColumnValue(Columns.CustomerLastName, value); }
		}
		[DataMember]
		public string CreditRating {
			get { return GetColumnValue<string>(Columns.CreditRating); }
			set { SetColumnValue(Columns.CreditRating, value); }
		}
		[DataMember]
		public decimal ActivationFeeAmt {
			get { return GetColumnValue<decimal>(Columns.ActivationFeeAmt); }
			set { SetColumnValue(Columns.ActivationFeeAmt, value); }
		}
		[DataMember]
		public int ContractLength {
			get { return GetColumnValue<int>(Columns.ContractLength); }
			set { SetColumnValue(Columns.ContractLength, value); }
		}
		[DataMember]
		public string ServiceType {
			get { return GetColumnValue<string>(Columns.ServiceType); }
			set { SetColumnValue(Columns.ServiceType, value); }
		}
		[DataMember]
		public decimal MonthlyPaymentAmt {
			get { return GetColumnValue<decimal>(Columns.MonthlyPaymentAmt); }
			set { SetColumnValue(Columns.MonthlyPaymentAmt, value); }
		}
		[DataMember]
		public string PaymentMethod {
			get { return GetColumnValue<string>(Columns.PaymentMethod); }
			set { SetColumnValue(Columns.PaymentMethod, value); }
		}
		[DataMember]
		public decimal SalesCommissionAmt {
			get { return GetColumnValue<decimal>(Columns.SalesCommissionAmt); }
			set { SetColumnValue(Columns.SalesCommissionAmt, value); }
		}
		[DataMember]
		public decimal RecurringCommissionAmt {
			get { return GetColumnValue<decimal>(Columns.RecurringCommissionAmt); }
			set { SetColumnValue(Columns.RecurringCommissionAmt, value); }
		}
		[DataMember]
		public bool isActive {
			get { return GetColumnValue<bool>(Columns.isActive); }
			set { SetColumnValue(Columns.isActive, value); }
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
		public static TableSchema.TableColumn ContractDateColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SalesMonthColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SalesYearColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CustomerMasterFileIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CustomerFirstNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CustomerMiddleNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CustomerLastNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn CreditRatingColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn ActivationFeeAmtColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn ContractLengthColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn ServiceTypeColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn MonthlyPaymentAmtColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn PaymentMethodColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn SalesCommissionAmtColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn RecurringCommissionAmtColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn isActiveColumn
		{
			get { return Schema.Columns[17]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string ContractDate = @"ContractDate";
			public const string SalesMonth = @"SalesMonth";
			public const string SalesYear = @"SalesYear";
			public const string CustomerMasterFileID = @"CustomerMasterFileID";
			public const string AccountID = @"AccountID";
			public const string CustomerFirstName = @"CustomerFirstName";
			public const string CustomerMiddleName = @"CustomerMiddleName";
			public const string CustomerLastName = @"CustomerLastName";
			public const string CreditRating = @"CreditRating";
			public const string ActivationFeeAmt = @"ActivationFeeAmt";
			public const string ContractLength = @"ContractLength";
			public const string ServiceType = @"ServiceType";
			public const string MonthlyPaymentAmt = @"MonthlyPaymentAmt";
			public const string PaymentMethod = @"PaymentMethod";
			public const string SalesCommissionAmt = @"SalesCommissionAmt";
			public const string RecurringCommissionAmt = @"RecurringCommissionAmt";
			public const string isActive = @"isActive";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the SAE_SalesSalespersonMonthlyEarningsView class.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyEarningsViewCollection : ReadOnlyList<SAE_SalesSalespersonMonthlyEarningsView, SAE_SalesSalespersonMonthlyEarningsViewCollection>
	{
		public static SAE_SalesSalespersonMonthlyEarningsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SAE_SalesSalespersonMonthlyEarningsViewCollection result = new SAE_SalesSalespersonMonthlyEarningsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwSAE_SalesSalespersonMonthlyEarnings view.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyEarningsView : ReadOnlyRecord<SAE_SalesSalespersonMonthlyEarningsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwSAE_SalesSalespersonMonthlyEarnings", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarSalesMonth = new TableSchema.TableColumn(schema);
				colvarSalesMonth.ColumnName = "SalesMonth";
				colvarSalesMonth.DataType = DbType.Int32;
				colvarSalesMonth.MaxLength = 0;
				colvarSalesMonth.AutoIncrement = false;
				colvarSalesMonth.IsNullable = false;
				colvarSalesMonth.IsPrimaryKey = false;
				colvarSalesMonth.IsForeignKey = false;
				colvarSalesMonth.IsReadOnly = false;
				colvarSalesMonth.DefaultSetting = @"";
				colvarSalesMonth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesMonth);

				TableSchema.TableColumn colvarSalesYear = new TableSchema.TableColumn(schema);
				colvarSalesYear.ColumnName = "SalesYear";
				colvarSalesYear.DataType = DbType.Int32;
				colvarSalesYear.MaxLength = 0;
				colvarSalesYear.AutoIncrement = false;
				colvarSalesYear.IsNullable = false;
				colvarSalesYear.IsPrimaryKey = false;
				colvarSalesYear.IsForeignKey = false;
				colvarSalesYear.IsReadOnly = false;
				colvarSalesYear.DefaultSetting = @"";
				colvarSalesYear.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesYear);

				TableSchema.TableColumn colvarSalesAmt = new TableSchema.TableColumn(schema);
				colvarSalesAmt.ColumnName = "SalesAmt";
				colvarSalesAmt.DataType = DbType.Currency;
				colvarSalesAmt.MaxLength = 0;
				colvarSalesAmt.AutoIncrement = false;
				colvarSalesAmt.IsNullable = true;
				colvarSalesAmt.IsPrimaryKey = false;
				colvarSalesAmt.IsForeignKey = false;
				colvarSalesAmt.IsReadOnly = false;
				colvarSalesAmt.DefaultSetting = @"";
				colvarSalesAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesAmt);

				TableSchema.TableColumn colvarRecurringAmt = new TableSchema.TableColumn(schema);
				colvarRecurringAmt.ColumnName = "RecurringAmt";
				colvarRecurringAmt.DataType = DbType.Currency;
				colvarRecurringAmt.MaxLength = 0;
				colvarRecurringAmt.AutoIncrement = false;
				colvarRecurringAmt.IsNullable = true;
				colvarRecurringAmt.IsPrimaryKey = false;
				colvarRecurringAmt.IsForeignKey = false;
				colvarRecurringAmt.IsReadOnly = false;
				colvarRecurringAmt.DefaultSetting = @"";
				colvarRecurringAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecurringAmt);

				TableSchema.TableColumn colvarRecruitingAmt = new TableSchema.TableColumn(schema);
				colvarRecruitingAmt.ColumnName = "RecruitingAmt";
				colvarRecruitingAmt.DataType = DbType.Currency;
				colvarRecruitingAmt.MaxLength = 0;
				colvarRecruitingAmt.AutoIncrement = false;
				colvarRecruitingAmt.IsNullable = true;
				colvarRecruitingAmt.IsPrimaryKey = false;
				colvarRecruitingAmt.IsForeignKey = false;
				colvarRecruitingAmt.IsReadOnly = false;
				colvarRecruitingAmt.DefaultSetting = @"";
				colvarRecruitingAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRecruitingAmt);

				TableSchema.TableColumn colvarBonusAmt = new TableSchema.TableColumn(schema);
				colvarBonusAmt.ColumnName = "BonusAmt";
				colvarBonusAmt.DataType = DbType.Currency;
				colvarBonusAmt.MaxLength = 0;
				colvarBonusAmt.AutoIncrement = false;
				colvarBonusAmt.IsNullable = true;
				colvarBonusAmt.IsPrimaryKey = false;
				colvarBonusAmt.IsForeignKey = false;
				colvarBonusAmt.IsReadOnly = false;
				colvarBonusAmt.DefaultSetting = @"";
				colvarBonusAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBonusAmt);

				TableSchema.TableColumn colvarDeductionAmt = new TableSchema.TableColumn(schema);
				colvarDeductionAmt.ColumnName = "DeductionAmt";
				colvarDeductionAmt.DataType = DbType.Currency;
				colvarDeductionAmt.MaxLength = 0;
				colvarDeductionAmt.AutoIncrement = false;
				colvarDeductionAmt.IsNullable = true;
				colvarDeductionAmt.IsPrimaryKey = false;
				colvarDeductionAmt.IsForeignKey = false;
				colvarDeductionAmt.IsReadOnly = false;
				colvarDeductionAmt.DefaultSetting = @"";
				colvarDeductionAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeductionAmt);

				TableSchema.TableColumn colvarHoldAmt = new TableSchema.TableColumn(schema);
				colvarHoldAmt.ColumnName = "HoldAmt";
				colvarHoldAmt.DataType = DbType.Currency;
				colvarHoldAmt.MaxLength = 0;
				colvarHoldAmt.AutoIncrement = false;
				colvarHoldAmt.IsNullable = true;
				colvarHoldAmt.IsPrimaryKey = false;
				colvarHoldAmt.IsForeignKey = false;
				colvarHoldAmt.IsReadOnly = false;
				colvarHoldAmt.DefaultSetting = @"";
				colvarHoldAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHoldAmt);

				TableSchema.TableColumn colvarTotalCommissionAmt = new TableSchema.TableColumn(schema);
				colvarTotalCommissionAmt.ColumnName = "TotalCommissionAmt";
				colvarTotalCommissionAmt.DataType = DbType.Currency;
				colvarTotalCommissionAmt.MaxLength = 0;
				colvarTotalCommissionAmt.AutoIncrement = false;
				colvarTotalCommissionAmt.IsNullable = true;
				colvarTotalCommissionAmt.IsPrimaryKey = false;
				colvarTotalCommissionAmt.IsForeignKey = false;
				colvarTotalCommissionAmt.IsReadOnly = false;
				colvarTotalCommissionAmt.DefaultSetting = @"";
				colvarTotalCommissionAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalCommissionAmt);

				TableSchema.TableColumn colvarYTDIncentiveBonusAmt = new TableSchema.TableColumn(schema);
				colvarYTDIncentiveBonusAmt.ColumnName = "YTDIncentiveBonusAmt";
				colvarYTDIncentiveBonusAmt.DataType = DbType.Currency;
				colvarYTDIncentiveBonusAmt.MaxLength = 0;
				colvarYTDIncentiveBonusAmt.AutoIncrement = false;
				colvarYTDIncentiveBonusAmt.IsNullable = true;
				colvarYTDIncentiveBonusAmt.IsPrimaryKey = false;
				colvarYTDIncentiveBonusAmt.IsForeignKey = false;
				colvarYTDIncentiveBonusAmt.IsReadOnly = false;
				colvarYTDIncentiveBonusAmt.DefaultSetting = @"";
				colvarYTDIncentiveBonusAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarYTDIncentiveBonusAmt);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwSAE_SalesSalespersonMonthlyEarnings",schema);
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
		public SAE_SalesSalespersonMonthlyEarningsView()
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
		public int SalesMonth {
			get { return GetColumnValue<int>(Columns.SalesMonth); }
			set { SetColumnValue(Columns.SalesMonth, value); }
		}
		[DataMember]
		public int SalesYear {
			get { return GetColumnValue<int>(Columns.SalesYear); }
			set { SetColumnValue(Columns.SalesYear, value); }
		}
		[DataMember]
		public decimal SalesAmt {
			get { return GetColumnValue<decimal>(Columns.SalesAmt); }
			set { SetColumnValue(Columns.SalesAmt, value); }
		}
		[DataMember]
		public decimal RecurringAmt {
			get { return GetColumnValue<decimal>(Columns.RecurringAmt); }
			set { SetColumnValue(Columns.RecurringAmt, value); }
		}
		[DataMember]
		public decimal RecruitingAmt {
			get { return GetColumnValue<decimal>(Columns.RecruitingAmt); }
			set { SetColumnValue(Columns.RecruitingAmt, value); }
		}
		[DataMember]
		public decimal BonusAmt {
			get { return GetColumnValue<decimal>(Columns.BonusAmt); }
			set { SetColumnValue(Columns.BonusAmt, value); }
		}
		[DataMember]
		public decimal DeductionAmt {
			get { return GetColumnValue<decimal>(Columns.DeductionAmt); }
			set { SetColumnValue(Columns.DeductionAmt, value); }
		}
		[DataMember]
		public decimal HoldAmt {
			get { return GetColumnValue<decimal>(Columns.HoldAmt); }
			set { SetColumnValue(Columns.HoldAmt, value); }
		}
		[DataMember]
		public decimal TotalCommissionAmt {
			get { return GetColumnValue<decimal>(Columns.TotalCommissionAmt); }
			set { SetColumnValue(Columns.TotalCommissionAmt, value); }
		}
		[DataMember]
		public decimal YTDIncentiveBonusAmt {
			get { return GetColumnValue<decimal>(Columns.YTDIncentiveBonusAmt); }
			set { SetColumnValue(Columns.YTDIncentiveBonusAmt, value); }
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
		public static TableSchema.TableColumn SalesMonthColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SalesYearColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SalesAmtColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn RecurringAmtColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn RecruitingAmtColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn BonusAmtColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn DeductionAmtColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn HoldAmtColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TotalCommissionAmtColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn YTDIncentiveBonusAmtColumn
		{
			get { return Schema.Columns[10]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string SalesMonth = @"SalesMonth";
			public const string SalesYear = @"SalesYear";
			public const string SalesAmt = @"SalesAmt";
			public const string RecurringAmt = @"RecurringAmt";
			public const string RecruitingAmt = @"RecruitingAmt";
			public const string BonusAmt = @"BonusAmt";
			public const string DeductionAmt = @"DeductionAmt";
			public const string HoldAmt = @"HoldAmt";
			public const string TotalCommissionAmt = @"TotalCommissionAmt";
			public const string YTDIncentiveBonusAmt = @"YTDIncentiveBonusAmt";
		}
		#endregion Columns Struct
	}
	/// <summary>
	/// Strongly-typed collection for the SAE_SalesSalespersonMonthlyHoldsView class.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyHoldsViewCollection : ReadOnlyList<SAE_SalesSalespersonMonthlyHoldsView, SAE_SalesSalespersonMonthlyHoldsViewCollection>
	{
		public static SAE_SalesSalespersonMonthlyHoldsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SAE_SalesSalespersonMonthlyHoldsViewCollection result = new SAE_SalesSalespersonMonthlyHoldsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwSAE_SalesSalespersonMonthlyHolds view.
	/// </summary>
	[DataContract]
	public partial class SAE_SalesSalespersonMonthlyHoldsView : ReadOnlyRecord<SAE_SalesSalespersonMonthlyHoldsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwSAE_SalesSalespersonMonthlyHolds", TableType.Table, DataService.GetInstance("SosHumanResourceProvider"));
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

				TableSchema.TableColumn colvarContractDate = new TableSchema.TableColumn(schema);
				colvarContractDate.ColumnName = "ContractDate";
				colvarContractDate.DataType = DbType.DateTime;
				colvarContractDate.MaxLength = 0;
				colvarContractDate.AutoIncrement = false;
				colvarContractDate.IsNullable = true;
				colvarContractDate.IsPrimaryKey = false;
				colvarContractDate.IsForeignKey = false;
				colvarContractDate.IsReadOnly = false;
				colvarContractDate.DefaultSetting = @"";
				colvarContractDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContractDate);

				TableSchema.TableColumn colvarSalesMonth = new TableSchema.TableColumn(schema);
				colvarSalesMonth.ColumnName = "SalesMonth";
				colvarSalesMonth.DataType = DbType.Int32;
				colvarSalesMonth.MaxLength = 0;
				colvarSalesMonth.AutoIncrement = false;
				colvarSalesMonth.IsNullable = true;
				colvarSalesMonth.IsPrimaryKey = false;
				colvarSalesMonth.IsForeignKey = false;
				colvarSalesMonth.IsReadOnly = false;
				colvarSalesMonth.DefaultSetting = @"";
				colvarSalesMonth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesMonth);

				TableSchema.TableColumn colvarSalesYear = new TableSchema.TableColumn(schema);
				colvarSalesYear.ColumnName = "SalesYear";
				colvarSalesYear.DataType = DbType.Int32;
				colvarSalesYear.MaxLength = 0;
				colvarSalesYear.AutoIncrement = false;
				colvarSalesYear.IsNullable = true;
				colvarSalesYear.IsPrimaryKey = false;
				colvarSalesYear.IsForeignKey = false;
				colvarSalesYear.IsReadOnly = false;
				colvarSalesYear.DefaultSetting = @"";
				colvarSalesYear.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesYear);

				TableSchema.TableColumn colvarCustomerMasterFileID = new TableSchema.TableColumn(schema);
				colvarCustomerMasterFileID.ColumnName = "CustomerMasterFileID";
				colvarCustomerMasterFileID.DataType = DbType.Int64;
				colvarCustomerMasterFileID.MaxLength = 0;
				colvarCustomerMasterFileID.AutoIncrement = false;
				colvarCustomerMasterFileID.IsNullable = false;
				colvarCustomerMasterFileID.IsPrimaryKey = false;
				colvarCustomerMasterFileID.IsForeignKey = false;
				colvarCustomerMasterFileID.IsReadOnly = false;
				colvarCustomerMasterFileID.DefaultSetting = @"";
				colvarCustomerMasterFileID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMasterFileID);

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

				TableSchema.TableColumn colvarCustomerFirstName = new TableSchema.TableColumn(schema);
				colvarCustomerFirstName.ColumnName = "CustomerFirstName";
				colvarCustomerFirstName.DataType = DbType.String;
				colvarCustomerFirstName.MaxLength = 50;
				colvarCustomerFirstName.AutoIncrement = false;
				colvarCustomerFirstName.IsNullable = false;
				colvarCustomerFirstName.IsPrimaryKey = false;
				colvarCustomerFirstName.IsForeignKey = false;
				colvarCustomerFirstName.IsReadOnly = false;
				colvarCustomerFirstName.DefaultSetting = @"";
				colvarCustomerFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerFirstName);

				TableSchema.TableColumn colvarCustomerMiddleName = new TableSchema.TableColumn(schema);
				colvarCustomerMiddleName.ColumnName = "CustomerMiddleName";
				colvarCustomerMiddleName.DataType = DbType.String;
				colvarCustomerMiddleName.MaxLength = 50;
				colvarCustomerMiddleName.AutoIncrement = false;
				colvarCustomerMiddleName.IsNullable = false;
				colvarCustomerMiddleName.IsPrimaryKey = false;
				colvarCustomerMiddleName.IsForeignKey = false;
				colvarCustomerMiddleName.IsReadOnly = false;
				colvarCustomerMiddleName.DefaultSetting = @"";
				colvarCustomerMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerMiddleName);

				TableSchema.TableColumn colvarCustomerLastName = new TableSchema.TableColumn(schema);
				colvarCustomerLastName.ColumnName = "CustomerLastName";
				colvarCustomerLastName.DataType = DbType.String;
				colvarCustomerLastName.MaxLength = 50;
				colvarCustomerLastName.AutoIncrement = false;
				colvarCustomerLastName.IsNullable = false;
				colvarCustomerLastName.IsPrimaryKey = false;
				colvarCustomerLastName.IsForeignKey = false;
				colvarCustomerLastName.IsReadOnly = false;
				colvarCustomerLastName.DefaultSetting = @"";
				colvarCustomerLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomerLastName);

				TableSchema.TableColumn colvarHoldName = new TableSchema.TableColumn(schema);
				colvarHoldName.ColumnName = "HoldName";
				colvarHoldName.DataType = DbType.String;
				colvarHoldName.MaxLength = 50;
				colvarHoldName.AutoIncrement = false;
				colvarHoldName.IsNullable = false;
				colvarHoldName.IsPrimaryKey = false;
				colvarHoldName.IsForeignKey = false;
				colvarHoldName.IsReadOnly = false;
				colvarHoldName.DefaultSetting = @"";
				colvarHoldName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHoldName);

				TableSchema.TableColumn colvarHoldDescription = new TableSchema.TableColumn(schema);
				colvarHoldDescription.ColumnName = "HoldDescription";
				colvarHoldDescription.DataType = DbType.String;
				colvarHoldDescription.MaxLength = -1;
				colvarHoldDescription.AutoIncrement = false;
				colvarHoldDescription.IsNullable = true;
				colvarHoldDescription.IsPrimaryKey = false;
				colvarHoldDescription.IsForeignKey = false;
				colvarHoldDescription.IsReadOnly = false;
				colvarHoldDescription.DefaultSetting = @"";
				colvarHoldDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHoldDescription);

				TableSchema.TableColumn colvarHoldAmt = new TableSchema.TableColumn(schema);
				colvarHoldAmt.ColumnName = "HoldAmt";
				colvarHoldAmt.DataType = DbType.Decimal;
				colvarHoldAmt.MaxLength = 0;
				colvarHoldAmt.AutoIncrement = false;
				colvarHoldAmt.IsNullable = true;
				colvarHoldAmt.IsPrimaryKey = false;
				colvarHoldAmt.IsForeignKey = false;
				colvarHoldAmt.IsReadOnly = false;
				colvarHoldAmt.DefaultSetting = @"";
				colvarHoldAmt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHoldAmt);

				BaseSchema = schema;
				DataService.Providers["SosHumanResourceProvider"].AddSchema("vwSAE_SalesSalespersonMonthlyHolds",schema);
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
		public SAE_SalesSalespersonMonthlyHoldsView()
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
		public DateTime ContractDate {
			get { return GetColumnValue<DateTime>(Columns.ContractDate); }
			set { SetColumnValue(Columns.ContractDate, value); }
		}
		[DataMember]
		public int SalesMonth {
			get { return GetColumnValue<int>(Columns.SalesMonth); }
			set { SetColumnValue(Columns.SalesMonth, value); }
		}
		[DataMember]
		public int SalesYear {
			get { return GetColumnValue<int>(Columns.SalesYear); }
			set { SetColumnValue(Columns.SalesYear, value); }
		}
		[DataMember]
		public long CustomerMasterFileID {
			get { return GetColumnValue<long>(Columns.CustomerMasterFileID); }
			set { SetColumnValue(Columns.CustomerMasterFileID, value); }
		}
		[DataMember]
		public long AccountID {
			get { return GetColumnValue<long>(Columns.AccountID); }
			set { SetColumnValue(Columns.AccountID, value); }
		}
		[DataMember]
		public string CustomerFirstName {
			get { return GetColumnValue<string>(Columns.CustomerFirstName); }
			set { SetColumnValue(Columns.CustomerFirstName, value); }
		}
		[DataMember]
		public string CustomerMiddleName {
			get { return GetColumnValue<string>(Columns.CustomerMiddleName); }
			set { SetColumnValue(Columns.CustomerMiddleName, value); }
		}
		[DataMember]
		public string CustomerLastName {
			get { return GetColumnValue<string>(Columns.CustomerLastName); }
			set { SetColumnValue(Columns.CustomerLastName, value); }
		}
		[DataMember]
		public string HoldName {
			get { return GetColumnValue<string>(Columns.HoldName); }
			set { SetColumnValue(Columns.HoldName, value); }
		}
		[DataMember]
		public string HoldDescription {
			get { return GetColumnValue<string>(Columns.HoldDescription); }
			set { SetColumnValue(Columns.HoldDescription, value); }
		}
		[DataMember]
		public decimal HoldAmt {
			get { return GetColumnValue<decimal>(Columns.HoldAmt); }
			set { SetColumnValue(Columns.HoldAmt, value); }
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
		public static TableSchema.TableColumn ContractDateColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn SalesMonthColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn SalesYearColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CustomerMasterFileIDColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn AccountIDColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn CustomerFirstNameColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn CustomerMiddleNameColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn CustomerLastNameColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn HoldNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn HoldDescriptionColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn HoldAmtColumn
		{
			get { return Schema.Columns[11]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string UserID = @"UserID";
			public const string ContractDate = @"ContractDate";
			public const string SalesMonth = @"SalesMonth";
			public const string SalesYear = @"SalesYear";
			public const string CustomerMasterFileID = @"CustomerMasterFileID";
			public const string AccountID = @"AccountID";
			public const string CustomerFirstName = @"CustomerFirstName";
			public const string CustomerMiddleName = @"CustomerMiddleName";
			public const string CustomerLastName = @"CustomerLastName";
			public const string HoldName = @"HoldName";
			public const string HoldDescription = @"HoldDescription";
			public const string HoldAmt = @"HoldAmt";
		}
		#endregion Columns Struct
	}
}
