


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

namespace SSE.Data.SurveyEngine
{
	/// <summary>
	/// Strongly-typed collection for the SV_ResultsView class.
	/// </summary>
	[DataContract]
	public partial class SV_ResultsViewCollection : ReadOnlyList<SV_ResultsView, SV_ResultsViewCollection>
	{
		public static SV_ResultsViewCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_ResultsViewCollection result = new SV_ResultsViewCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
	}

	/// <summary>
	/// This is a Read-only wrapper class for the vwSV_Results view.
	/// </summary>
	[DataContract]
	public partial class SV_ResultsView : ReadOnlyRecord<SV_ResultsView>
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
				TableSchema.Table schema = new TableSchema.Table("vwSV_Results", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarResultID = new TableSchema.TableColumn(schema);
				colvarResultID.ColumnName = "ResultID";
				colvarResultID.DataType = DbType.Int64;
				colvarResultID.MaxLength = 0;
				colvarResultID.AutoIncrement = false;
				colvarResultID.IsNullable = false;
				colvarResultID.IsPrimaryKey = false;
				colvarResultID.IsForeignKey = false;
				colvarResultID.IsReadOnly = false;
				colvarResultID.DefaultSetting = @"";
				colvarResultID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarResultID);

				TableSchema.TableColumn colvarSurveyTranslationId = new TableSchema.TableColumn(schema);
				colvarSurveyTranslationId.ColumnName = "SurveyTranslationId";
				colvarSurveyTranslationId.DataType = DbType.Int32;
				colvarSurveyTranslationId.MaxLength = 0;
				colvarSurveyTranslationId.AutoIncrement = false;
				colvarSurveyTranslationId.IsNullable = false;
				colvarSurveyTranslationId.IsPrimaryKey = false;
				colvarSurveyTranslationId.IsForeignKey = false;
				colvarSurveyTranslationId.IsReadOnly = false;
				colvarSurveyTranslationId.DefaultSetting = @"";
				colvarSurveyTranslationId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyTranslationId);

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

				TableSchema.TableColumn colvarPassed = new TableSchema.TableColumn(schema);
				colvarPassed.ColumnName = "Passed";
				colvarPassed.DataType = DbType.Boolean;
				colvarPassed.MaxLength = 0;
				colvarPassed.AutoIncrement = false;
				colvarPassed.IsNullable = false;
				colvarPassed.IsPrimaryKey = false;
				colvarPassed.IsForeignKey = false;
				colvarPassed.IsReadOnly = false;
				colvarPassed.DefaultSetting = @"";
				colvarPassed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassed);

				TableSchema.TableColumn colvarIsComplete = new TableSchema.TableColumn(schema);
				colvarIsComplete.ColumnName = "IsComplete";
				colvarIsComplete.DataType = DbType.Boolean;
				colvarIsComplete.MaxLength = 0;
				colvarIsComplete.AutoIncrement = false;
				colvarIsComplete.IsNullable = false;
				colvarIsComplete.IsPrimaryKey = false;
				colvarIsComplete.IsForeignKey = false;
				colvarIsComplete.IsReadOnly = false;
				colvarIsComplete.DefaultSetting = @"";
				colvarIsComplete.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsComplete);

				TableSchema.TableColumn colvarContext = new TableSchema.TableColumn(schema);
				colvarContext.ColumnName = "Context";
				colvarContext.DataType = DbType.String;
				colvarContext.MaxLength = -1;
				colvarContext.AutoIncrement = false;
				colvarContext.IsNullable = false;
				colvarContext.IsPrimaryKey = false;
				colvarContext.IsForeignKey = false;
				colvarContext.IsReadOnly = false;
				colvarContext.DefaultSetting = @"";
				colvarContext.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContext);

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

				TableSchema.TableColumn colvarSurveyId = new TableSchema.TableColumn(schema);
				colvarSurveyId.ColumnName = "SurveyId";
				colvarSurveyId.DataType = DbType.Int32;
				colvarSurveyId.MaxLength = 0;
				colvarSurveyId.AutoIncrement = false;
				colvarSurveyId.IsNullable = false;
				colvarSurveyId.IsPrimaryKey = false;
				colvarSurveyId.IsForeignKey = false;
				colvarSurveyId.IsReadOnly = false;
				colvarSurveyId.DefaultSetting = @"";
				colvarSurveyId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyId);

				TableSchema.TableColumn colvarVersion = new TableSchema.TableColumn(schema);
				colvarVersion.ColumnName = "Version";
				colvarVersion.DataType = DbType.AnsiString;
				colvarVersion.MaxLength = 10;
				colvarVersion.AutoIncrement = false;
				colvarVersion.IsNullable = false;
				colvarVersion.IsPrimaryKey = false;
				colvarVersion.IsForeignKey = false;
				colvarVersion.IsReadOnly = false;
				colvarVersion.DefaultSetting = @"";
				colvarVersion.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVersion);

				TableSchema.TableColumn colvarSurveyTypeId = new TableSchema.TableColumn(schema);
				colvarSurveyTypeId.ColumnName = "SurveyTypeId";
				colvarSurveyTypeId.DataType = DbType.Int32;
				colvarSurveyTypeId.MaxLength = 0;
				colvarSurveyTypeId.AutoIncrement = false;
				colvarSurveyTypeId.IsNullable = false;
				colvarSurveyTypeId.IsPrimaryKey = false;
				colvarSurveyTypeId.IsForeignKey = false;
				colvarSurveyTypeId.IsReadOnly = false;
				colvarSurveyTypeId.DefaultSetting = @"";
				colvarSurveyTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyTypeId);

				TableSchema.TableColumn colvarSurveyType = new TableSchema.TableColumn(schema);
				colvarSurveyType.ColumnName = "SurveyType";
				colvarSurveyType.DataType = DbType.AnsiString;
				colvarSurveyType.MaxLength = 50;
				colvarSurveyType.AutoIncrement = false;
				colvarSurveyType.IsNullable = false;
				colvarSurveyType.IsPrimaryKey = false;
				colvarSurveyType.IsForeignKey = false;
				colvarSurveyType.IsReadOnly = false;
				colvarSurveyType.DefaultSetting = @"";
				colvarSurveyType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyType);

				TableSchema.TableColumn colvarLocalizationCode = new TableSchema.TableColumn(schema);
				colvarLocalizationCode.ColumnName = "LocalizationCode";
				colvarLocalizationCode.DataType = DbType.String;
				colvarLocalizationCode.MaxLength = 10;
				colvarLocalizationCode.AutoIncrement = false;
				colvarLocalizationCode.IsNullable = false;
				colvarLocalizationCode.IsPrimaryKey = false;
				colvarLocalizationCode.IsForeignKey = false;
				colvarLocalizationCode.IsReadOnly = false;
				colvarLocalizationCode.DefaultSetting = @"";
				colvarLocalizationCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocalizationCode);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("vwSV_Results",schema);
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
		public SV_ResultsView()
		{
			SetSQLProps();SetDefaults();MarkNew();
		}
		#endregion

		#region Properties
		[DataMember]
		public long ResultID {
			get { return GetColumnValue<long>(Columns.ResultID); }
			set { SetColumnValue(Columns.ResultID, value); }
		}
		[DataMember]
		public int SurveyTranslationId {
			get { return GetColumnValue<int>(Columns.SurveyTranslationId); }
			set { SetColumnValue(Columns.SurveyTranslationId, value); }
		}
		[DataMember]
		public long AccountId {
			get { return GetColumnValue<long>(Columns.AccountId); }
			set { SetColumnValue(Columns.AccountId, value); }
		}
		[DataMember]
		public bool Passed {
			get { return GetColumnValue<bool>(Columns.Passed); }
			set { SetColumnValue(Columns.Passed, value); }
		}
		[DataMember]
		public bool IsComplete {
			get { return GetColumnValue<bool>(Columns.IsComplete); }
			set { SetColumnValue(Columns.IsComplete, value); }
		}
		[DataMember]
		public string Context {
			get { return GetColumnValue<string>(Columns.Context); }
			set { SetColumnValue(Columns.Context, value); }
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
		public int SurveyId {
			get { return GetColumnValue<int>(Columns.SurveyId); }
			set { SetColumnValue(Columns.SurveyId, value); }
		}
		[DataMember]
		public string Version {
			get { return GetColumnValue<string>(Columns.Version); }
			set { SetColumnValue(Columns.Version, value); }
		}
		[DataMember]
		public int SurveyTypeId {
			get { return GetColumnValue<int>(Columns.SurveyTypeId); }
			set { SetColumnValue(Columns.SurveyTypeId, value); }
		}
		[DataMember]
		public string SurveyType {
			get { return GetColumnValue<string>(Columns.SurveyType); }
			set { SetColumnValue(Columns.SurveyType, value); }
		}
		[DataMember]
		public string LocalizationCode {
			get { return GetColumnValue<string>(Columns.LocalizationCode); }
			set { SetColumnValue(Columns.LocalizationCode, value); }
		}

		#endregion //Properties

		public override string ToString()
		{
			return ResultID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ResultIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyTranslationIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AccountIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn PassedColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsCompleteColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn ContextColumn
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
		public static TableSchema.TableColumn SurveyIdColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn VersionColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SurveyTypeIdColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn SurveyTypeColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn LocalizationCodeColumn
		{
			get { return Schema.Columns[12]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public const string ResultID = @"ResultID";
			public const string SurveyTranslationId = @"SurveyTranslationId";
			public const string AccountId = @"AccountId";
			public const string Passed = @"Passed";
			public const string IsComplete = @"IsComplete";
			public const string Context = @"Context";
			public const string CreatedBy = @"CreatedBy";
			public const string CreatedOn = @"CreatedOn";
			public const string SurveyId = @"SurveyId";
			public const string Version = @"Version";
			public const string SurveyTypeId = @"SurveyTypeId";
			public const string SurveyType = @"SurveyType";
			public const string LocalizationCode = @"LocalizationCode";
		}
		#endregion Columns Struct
	}
}
