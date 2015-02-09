


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

namespace SSE.Data.SurveyEngine
{
	/// <summary>
	/// Strongly-typed collection for the SV_Answer class.
	/// </summary>
	[DataContract]
	public partial class SV_AnswerCollection : ActiveList<SV_Answer, SV_AnswerCollection>
	{
		public static SV_AnswerCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_AnswerCollection result = new SV_AnswerCollection();
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
			foreach (SV_Answer item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Answers table.
	/// </summary>
	[DataContract]
	public partial class SV_Answer : ActiveRecord<SV_Answer>, INotifyPropertyChanged
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

		public SV_Answer()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Answers", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAnswerID = new TableSchema.TableColumn(schema);
				colvarAnswerID.ColumnName = "AnswerID";
				colvarAnswerID.DataType = DbType.Int64;
				colvarAnswerID.MaxLength = 0;
				colvarAnswerID.AutoIncrement = true;
				colvarAnswerID.IsNullable = false;
				colvarAnswerID.IsPrimaryKey = true;
				colvarAnswerID.IsForeignKey = false;
				colvarAnswerID.IsReadOnly = false;
				colvarAnswerID.DefaultSetting = @"";
				colvarAnswerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnswerID);

				TableSchema.TableColumn colvarResultId = new TableSchema.TableColumn(schema);
				colvarResultId.ColumnName = "ResultId";
				colvarResultId.DataType = DbType.Int64;
				colvarResultId.MaxLength = 0;
				colvarResultId.AutoIncrement = false;
				colvarResultId.IsNullable = false;
				colvarResultId.IsPrimaryKey = false;
				colvarResultId.IsForeignKey = true;
				colvarResultId.IsReadOnly = false;
				colvarResultId.DefaultSetting = @"";
				colvarResultId.ForeignKeyTableName = "SV_Results";
				schema.Columns.Add(colvarResultId);

				TableSchema.TableColumn colvarQuestionId = new TableSchema.TableColumn(schema);
				colvarQuestionId.ColumnName = "QuestionId";
				colvarQuestionId.DataType = DbType.Int32;
				colvarQuestionId.MaxLength = 0;
				colvarQuestionId.AutoIncrement = false;
				colvarQuestionId.IsNullable = false;
				colvarQuestionId.IsPrimaryKey = false;
				colvarQuestionId.IsForeignKey = true;
				colvarQuestionId.IsReadOnly = false;
				colvarQuestionId.DefaultSetting = @"";
				colvarQuestionId.ForeignKeyTableName = "SV_Questions";
				schema.Columns.Add(colvarQuestionId);

				TableSchema.TableColumn colvarAnswerText = new TableSchema.TableColumn(schema);
				colvarAnswerText.ColumnName = "AnswerText";
				colvarAnswerText.DataType = DbType.String;
				colvarAnswerText.MaxLength = -1;
				colvarAnswerText.AutoIncrement = false;
				colvarAnswerText.IsNullable = false;
				colvarAnswerText.IsPrimaryKey = false;
				colvarAnswerText.IsForeignKey = false;
				colvarAnswerText.IsReadOnly = false;
				colvarAnswerText.DefaultSetting = @"";
				colvarAnswerText.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnswerText);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Answers",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Answer LoadFrom(SV_Answer item)
		{
			SV_Answer result = new SV_Answer();
			if (item.AnswerID != default(long)) {
				result.LoadByKey(item.AnswerID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AnswerID {
			get { return GetColumnValue<long>(Columns.AnswerID); }
			set {
				SetColumnValue(Columns.AnswerID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AnswerID));
			}
		}
		[DataMember]
		public long ResultId {
			get { return GetColumnValue<long>(Columns.ResultId); }
			set {
				SetColumnValue(Columns.ResultId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResultId));
			}
		}
		[DataMember]
		public int QuestionId {
			get { return GetColumnValue<int>(Columns.QuestionId); }
			set {
				SetColumnValue(Columns.QuestionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionId));
			}
		}
		[DataMember]
		public string AnswerText {
			get { return GetColumnValue<string>(Columns.AnswerText); }
			set {
				SetColumnValue(Columns.AnswerText, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AnswerText));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_Question _Question;
		//Relationship: FK_SV_Answers_SV_Questions
		public SV_Question Question
		{
			get
			{
				if(_Question == null) {
					_Question = SV_Question.FetchByID(this.QuestionId);
				}
				return _Question;
			}
			set
			{
				SetColumnValue("QuestionId", value.QuestionID);
				_Question = value;
			}
		}

		private SV_Result _Result;
		//Relationship: FK_SV_Answers_SV_Results
		public SV_Result Result
		{
			get
			{
				if(_Result == null) {
					_Result = SV_Result.FetchByID(this.ResultId);
				}
				return _Result;
			}
			set
			{
				SetColumnValue("ResultId", value.ResultID);
				_Result = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AnswerID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AnswerIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ResultIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn QuestionIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AnswerTextColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AnswerID = @"AnswerID";
			public static readonly string ResultId = @"ResultId";
			public static readonly string QuestionId = @"QuestionId";
			public static readonly string AnswerText = @"AnswerText";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AnswerID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the SV_PossibleAnswer class.
	/// </summary>
	[DataContract]
	public partial class SV_PossibleAnswerCollection : ActiveList<SV_PossibleAnswer, SV_PossibleAnswerCollection>
	{
		public static SV_PossibleAnswerCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_PossibleAnswerCollection result = new SV_PossibleAnswerCollection();
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
			foreach (SV_PossibleAnswer item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_PossibleAnswers table.
	/// </summary>
	[DataContract]
	public partial class SV_PossibleAnswer : ActiveRecord<SV_PossibleAnswer>, INotifyPropertyChanged
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

		public SV_PossibleAnswer()
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
				TableSchema.Table schema = new TableSchema.Table("SV_PossibleAnswers", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPossibleAnswerID = new TableSchema.TableColumn(schema);
				colvarPossibleAnswerID.ColumnName = "PossibleAnswerID";
				colvarPossibleAnswerID.DataType = DbType.Int32;
				colvarPossibleAnswerID.MaxLength = 0;
				colvarPossibleAnswerID.AutoIncrement = true;
				colvarPossibleAnswerID.IsNullable = false;
				colvarPossibleAnswerID.IsPrimaryKey = true;
				colvarPossibleAnswerID.IsForeignKey = false;
				colvarPossibleAnswerID.IsReadOnly = false;
				colvarPossibleAnswerID.DefaultSetting = @"";
				colvarPossibleAnswerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPossibleAnswerID);

				TableSchema.TableColumn colvarAnswerText = new TableSchema.TableColumn(schema);
				colvarAnswerText.ColumnName = "AnswerText";
				colvarAnswerText.DataType = DbType.String;
				colvarAnswerText.MaxLength = 50;
				colvarAnswerText.AutoIncrement = false;
				colvarAnswerText.IsNullable = false;
				colvarAnswerText.IsPrimaryKey = false;
				colvarAnswerText.IsForeignKey = false;
				colvarAnswerText.IsReadOnly = false;
				colvarAnswerText.DefaultSetting = @"";
				colvarAnswerText.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnswerText);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_PossibleAnswers",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_PossibleAnswer LoadFrom(SV_PossibleAnswer item)
		{
			SV_PossibleAnswer result = new SV_PossibleAnswer();
			if (item.PossibleAnswerID != default(int)) {
				result.LoadByKey(item.PossibleAnswerID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PossibleAnswerID {
			get { return GetColumnValue<int>(Columns.PossibleAnswerID); }
			set {
				SetColumnValue(Columns.PossibleAnswerID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PossibleAnswerID));
			}
		}
		[DataMember]
		public string AnswerText {
			get { return GetColumnValue<string>(Columns.AnswerText); }
			set {
				SetColumnValue(Columns.AnswerText, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AnswerText));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return AnswerText;
		}

		#region Typed Columns

		public static TableSchema.TableColumn PossibleAnswerIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn AnswerTextColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PossibleAnswerID = @"PossibleAnswerID";
			public static readonly string AnswerText = @"AnswerText";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PossibleAnswerID; }
		}
		*/

		#region Foreign Collections

		private SV_PossibleAnswerTranslationCollection _SV_PossibleAnswerTranslationsCol;
		//Relationship: FK_SV_PossibleAnswerTranslations_SV_PossibleAnswers
		public SV_PossibleAnswerTranslationCollection SV_PossibleAnswerTranslationsCol
		{
			get
			{
				if(_SV_PossibleAnswerTranslationsCol == null) {
					_SV_PossibleAnswerTranslationsCol = new SV_PossibleAnswerTranslationCollection();
					_SV_PossibleAnswerTranslationsCol.LoadAndCloseReader(SV_PossibleAnswerTranslation.Query()
						.WHERE(SV_PossibleAnswerTranslation.Columns.PossibleAnswerId, PossibleAnswerID).ExecuteReader());
				}
				return _SV_PossibleAnswerTranslationsCol;
			}
		}

		private SV_Questions_PossibleAnswers_MapCollection _SV_Questions_PossibleAnswers_MapsCol;
		//Relationship: FK_SV_Questions_PossibleAnswers_Map_SV_PossibleAnswers
		public SV_Questions_PossibleAnswers_MapCollection SV_Questions_PossibleAnswers_MapsCol
		{
			get
			{
				if(_SV_Questions_PossibleAnswers_MapsCol == null) {
					_SV_Questions_PossibleAnswers_MapsCol = new SV_Questions_PossibleAnswers_MapCollection();
					_SV_Questions_PossibleAnswers_MapsCol.LoadAndCloseReader(SV_Questions_PossibleAnswers_Map.Query()
						.WHERE(SV_Questions_PossibleAnswers_Map.Columns.PossibleAnswerId, PossibleAnswerID).ExecuteReader());
				}
				return _SV_Questions_PossibleAnswers_MapsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_PossibleAnswerTranslation class.
	/// </summary>
	[DataContract]
	public partial class SV_PossibleAnswerTranslationCollection : ActiveList<SV_PossibleAnswerTranslation, SV_PossibleAnswerTranslationCollection>
	{
		public static SV_PossibleAnswerTranslationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_PossibleAnswerTranslationCollection result = new SV_PossibleAnswerTranslationCollection();
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
			foreach (SV_PossibleAnswerTranslation item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_PossibleAnswerTranslations table.
	/// </summary>
	[DataContract]
	public partial class SV_PossibleAnswerTranslation : ActiveRecord<SV_PossibleAnswerTranslation>, INotifyPropertyChanged
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

		public SV_PossibleAnswerTranslation()
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
				TableSchema.Table schema = new TableSchema.Table("SV_PossibleAnswerTranslations", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarPossibleAnswerTranslationID = new TableSchema.TableColumn(schema);
				colvarPossibleAnswerTranslationID.ColumnName = "PossibleAnswerTranslationID";
				colvarPossibleAnswerTranslationID.DataType = DbType.Int32;
				colvarPossibleAnswerTranslationID.MaxLength = 0;
				colvarPossibleAnswerTranslationID.AutoIncrement = true;
				colvarPossibleAnswerTranslationID.IsNullable = false;
				colvarPossibleAnswerTranslationID.IsPrimaryKey = true;
				colvarPossibleAnswerTranslationID.IsForeignKey = false;
				colvarPossibleAnswerTranslationID.IsReadOnly = false;
				colvarPossibleAnswerTranslationID.DefaultSetting = @"";
				colvarPossibleAnswerTranslationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPossibleAnswerTranslationID);

				TableSchema.TableColumn colvarPossibleAnswerId = new TableSchema.TableColumn(schema);
				colvarPossibleAnswerId.ColumnName = "PossibleAnswerId";
				colvarPossibleAnswerId.DataType = DbType.Int32;
				colvarPossibleAnswerId.MaxLength = 0;
				colvarPossibleAnswerId.AutoIncrement = false;
				colvarPossibleAnswerId.IsNullable = false;
				colvarPossibleAnswerId.IsPrimaryKey = false;
				colvarPossibleAnswerId.IsForeignKey = true;
				colvarPossibleAnswerId.IsReadOnly = false;
				colvarPossibleAnswerId.DefaultSetting = @"";
				colvarPossibleAnswerId.ForeignKeyTableName = "SV_PossibleAnswers";
				schema.Columns.Add(colvarPossibleAnswerId);

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

				TableSchema.TableColumn colvarTextX = new TableSchema.TableColumn(schema);
				colvarTextX.ColumnName = "Text";
				colvarTextX.DataType = DbType.String;
				colvarTextX.MaxLength = -1;
				colvarTextX.AutoIncrement = false;
				colvarTextX.IsNullable = false;
				colvarTextX.IsPrimaryKey = false;
				colvarTextX.IsForeignKey = false;
				colvarTextX.IsReadOnly = false;
				colvarTextX.DefaultSetting = @"";
				colvarTextX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTextX);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_PossibleAnswerTranslations",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_PossibleAnswerTranslation LoadFrom(SV_PossibleAnswerTranslation item)
		{
			SV_PossibleAnswerTranslation result = new SV_PossibleAnswerTranslation();
			if (item.PossibleAnswerTranslationID != default(int)) {
				result.LoadByKey(item.PossibleAnswerTranslationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int PossibleAnswerTranslationID {
			get { return GetColumnValue<int>(Columns.PossibleAnswerTranslationID); }
			set {
				SetColumnValue(Columns.PossibleAnswerTranslationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PossibleAnswerTranslationID));
			}
		}
		[DataMember]
		public int PossibleAnswerId {
			get { return GetColumnValue<int>(Columns.PossibleAnswerId); }
			set {
				SetColumnValue(Columns.PossibleAnswerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PossibleAnswerId));
			}
		}
		[DataMember]
		public string LocalizationCode {
			get { return GetColumnValue<string>(Columns.LocalizationCode); }
			set {
				SetColumnValue(Columns.LocalizationCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocalizationCode));
			}
		}
		[DataMember]
		public string TextX {
			get { return GetColumnValue<string>(Columns.TextX); }
			set {
				SetColumnValue(Columns.TextX, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TextX));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_PossibleAnswer _PossibleAnswer;
		//Relationship: FK_SV_PossibleAnswerTranslations_SV_PossibleAnswers
		public SV_PossibleAnswer PossibleAnswer
		{
			get
			{
				if(_PossibleAnswer == null) {
					_PossibleAnswer = SV_PossibleAnswer.FetchByID(this.PossibleAnswerId);
				}
				return _PossibleAnswer;
			}
			set
			{
				SetColumnValue("PossibleAnswerId", value.PossibleAnswerID);
				_PossibleAnswer = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return PossibleAnswerTranslationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn PossibleAnswerTranslationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PossibleAnswerIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LocalizationCodeColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TextXColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string PossibleAnswerTranslationID = @"PossibleAnswerTranslationID";
			public static readonly string PossibleAnswerId = @"PossibleAnswerId";
			public static readonly string LocalizationCode = @"LocalizationCode";
			public static readonly string TextX = @"Text";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return PossibleAnswerTranslationID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the SV_QuestionMeaning class.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionMeaningCollection : ActiveList<SV_QuestionMeaning, SV_QuestionMeaningCollection>
	{
		public static SV_QuestionMeaningCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_QuestionMeaningCollection result = new SV_QuestionMeaningCollection();
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
			foreach (SV_QuestionMeaning item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_QuestionMeanings table.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionMeaning : ActiveRecord<SV_QuestionMeaning>, INotifyPropertyChanged
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

		public SV_QuestionMeaning()
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
				TableSchema.Table schema = new TableSchema.Table("SV_QuestionMeanings", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarQuestionMeaningID = new TableSchema.TableColumn(schema);
				colvarQuestionMeaningID.ColumnName = "QuestionMeaningID";
				colvarQuestionMeaningID.DataType = DbType.Int32;
				colvarQuestionMeaningID.MaxLength = 0;
				colvarQuestionMeaningID.AutoIncrement = true;
				colvarQuestionMeaningID.IsNullable = false;
				colvarQuestionMeaningID.IsPrimaryKey = true;
				colvarQuestionMeaningID.IsForeignKey = false;
				colvarQuestionMeaningID.IsReadOnly = false;
				colvarQuestionMeaningID.DefaultSetting = @"";
				colvarQuestionMeaningID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuestionMeaningID);

				TableSchema.TableColumn colvarSurveyTypeId = new TableSchema.TableColumn(schema);
				colvarSurveyTypeId.ColumnName = "SurveyTypeId";
				colvarSurveyTypeId.DataType = DbType.Int32;
				colvarSurveyTypeId.MaxLength = 0;
				colvarSurveyTypeId.AutoIncrement = false;
				colvarSurveyTypeId.IsNullable = false;
				colvarSurveyTypeId.IsPrimaryKey = false;
				colvarSurveyTypeId.IsForeignKey = true;
				colvarSurveyTypeId.IsReadOnly = false;
				colvarSurveyTypeId.DefaultSetting = @"";
				colvarSurveyTypeId.ForeignKeyTableName = "SV_SurveyTypes";
				schema.Columns.Add(colvarSurveyTypeId);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 1024;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_QuestionMeanings",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_QuestionMeaning LoadFrom(SV_QuestionMeaning item)
		{
			SV_QuestionMeaning result = new SV_QuestionMeaning();
			if (item.QuestionMeaningID != default(int)) {
				result.LoadByKey(item.QuestionMeaningID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int QuestionMeaningID {
			get { return GetColumnValue<int>(Columns.QuestionMeaningID); }
			set {
				SetColumnValue(Columns.QuestionMeaningID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionMeaningID));
			}
		}
		[DataMember]
		public int SurveyTypeId {
			get { return GetColumnValue<int>(Columns.SurveyTypeId); }
			set {
				SetColumnValue(Columns.SurveyTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTypeId));
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

		#endregion //Properties

		#region ForeignKey Properties

		private SV_SurveyType _SurveyType;
		//Relationship: FK_SV_QuestionMeanings_SV_SurveyTypes
		public SV_SurveyType SurveyType
		{
			get
			{
				if(_SurveyType == null) {
					_SurveyType = SV_SurveyType.FetchByID(this.SurveyTypeId);
				}
				return _SurveyType;
			}
			set
			{
				SetColumnValue("SurveyTypeId", value.SurveyTypeID);
				_SurveyType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return QuestionMeaningID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn QuestionMeaningIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string QuestionMeaningID = @"QuestionMeaningID";
			public static readonly string SurveyTypeId = @"SurveyTypeId";
			public static readonly string Name = @"Name";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return QuestionMeaningID; }
		}
		*/

		#region Foreign Collections

		private SV_QuestionMeanings_Tokens_MapCollection _SV_QuestionMeanings_Tokens_MapsCol;
		//Relationship: FK_SV_QuestionMeanings_Tokens_Map_SV_QuestionMeanings
		public SV_QuestionMeanings_Tokens_MapCollection SV_QuestionMeanings_Tokens_MapsCol
		{
			get
			{
				if(_SV_QuestionMeanings_Tokens_MapsCol == null) {
					_SV_QuestionMeanings_Tokens_MapsCol = new SV_QuestionMeanings_Tokens_MapCollection();
					_SV_QuestionMeanings_Tokens_MapsCol.LoadAndCloseReader(SV_QuestionMeanings_Tokens_Map.Query()
						.WHERE(SV_QuestionMeanings_Tokens_Map.Columns.QuestionMeaningId, QuestionMeaningID).ExecuteReader());
				}
				return _SV_QuestionMeanings_Tokens_MapsCol;
			}
		}

		private SV_QuestionCollection _SV_QuestionsCol;
		//Relationship: FK_SV_Questions_SV_QuestionMeanings
		public SV_QuestionCollection SV_QuestionsCol
		{
			get
			{
				if(_SV_QuestionsCol == null) {
					_SV_QuestionsCol = new SV_QuestionCollection();
					_SV_QuestionsCol.LoadAndCloseReader(SV_Question.Query()
						.WHERE(SV_Question.Columns.QuestionMeaningId, QuestionMeaningID).ExecuteReader());
				}
				return _SV_QuestionsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_QuestionMeanings_Tokens_Map class.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionMeanings_Tokens_MapCollection : ActiveList<SV_QuestionMeanings_Tokens_Map, SV_QuestionMeanings_Tokens_MapCollection>
	{
		public static SV_QuestionMeanings_Tokens_MapCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_QuestionMeanings_Tokens_MapCollection result = new SV_QuestionMeanings_Tokens_MapCollection();
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
			foreach (SV_QuestionMeanings_Tokens_Map item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_QuestionMeanings_Tokens_Map table.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionMeanings_Tokens_Map : ActiveRecord<SV_QuestionMeanings_Tokens_Map>, INotifyPropertyChanged
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

		public SV_QuestionMeanings_Tokens_Map()
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
				TableSchema.Table schema = new TableSchema.Table("SV_QuestionMeanings_Tokens_Map", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarQuestionMeaningId = new TableSchema.TableColumn(schema);
				colvarQuestionMeaningId.ColumnName = "QuestionMeaningId";
				colvarQuestionMeaningId.DataType = DbType.Int32;
				colvarQuestionMeaningId.MaxLength = 0;
				colvarQuestionMeaningId.AutoIncrement = false;
				colvarQuestionMeaningId.IsNullable = false;
				colvarQuestionMeaningId.IsPrimaryKey = true;
				colvarQuestionMeaningId.IsForeignKey = false;
				colvarQuestionMeaningId.IsReadOnly = false;
				colvarQuestionMeaningId.DefaultSetting = @"";
				colvarQuestionMeaningId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuestionMeaningId);

				TableSchema.TableColumn colvarTokenId = new TableSchema.TableColumn(schema);
				colvarTokenId.ColumnName = "TokenId";
				colvarTokenId.DataType = DbType.Int32;
				colvarTokenId.MaxLength = 0;
				colvarTokenId.AutoIncrement = false;
				colvarTokenId.IsNullable = false;
				colvarTokenId.IsPrimaryKey = false;
				colvarTokenId.IsForeignKey = true;
				colvarTokenId.IsReadOnly = false;
				colvarTokenId.DefaultSetting = @"";
				colvarTokenId.ForeignKeyTableName = "SV_Tokens";
				schema.Columns.Add(colvarTokenId);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = true;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_QuestionMeanings_Tokens_Map",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_QuestionMeanings_Tokens_Map LoadFrom(SV_QuestionMeanings_Tokens_Map item)
		{
			SV_QuestionMeanings_Tokens_Map result = new SV_QuestionMeanings_Tokens_Map();
			if (item.QuestionMeaningId != default(int)) {
				result.LoadByKey(item.QuestionMeaningId);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int QuestionMeaningId {
			get { return GetColumnValue<int>(Columns.QuestionMeaningId); }
			set {
				SetColumnValue(Columns.QuestionMeaningId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionMeaningId));
			}
		}
		[DataMember]
		public int TokenId {
			get { return GetColumnValue<int>(Columns.TokenId); }
			set {
				SetColumnValue(Columns.TokenId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TokenId));
			}
		}
		[DataMember]
		public DateTime? CreatedOn {
			get { return GetColumnValue<DateTime?>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_QuestionMeaning _QuestionMeaning;
		//Relationship: FK_SV_QuestionMeanings_Tokens_Map_SV_QuestionMeanings
		public SV_QuestionMeaning QuestionMeaning
		{
			get
			{
				if(_QuestionMeaning == null) {
					_QuestionMeaning = SV_QuestionMeaning.FetchByID(this.QuestionMeaningId);
				}
				return _QuestionMeaning;
			}
			set
			{
				SetColumnValue("QuestionMeaningId", value.QuestionMeaningID);
				_QuestionMeaning = value;
			}
		}

		private SV_Token _Token;
		//Relationship: FK_SV_QuestionMeanings_Tokens_Map_SV_Tokens
		public SV_Token Token
		{
			get
			{
				if(_Token == null) {
					_Token = SV_Token.FetchByID(this.TokenId);
				}
				return _Token;
			}
			set
			{
				SetColumnValue("TokenId", value.TokenID);
				_Token = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return QuestionMeaningId.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn QuestionMeaningIdColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TokenIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string QuestionMeaningId = @"QuestionMeaningId";
			public static readonly string TokenId = @"TokenId";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return QuestionMeaningId; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the SV_Question class.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionCollection : ActiveList<SV_Question, SV_QuestionCollection>
	{
		public static SV_QuestionCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_QuestionCollection result = new SV_QuestionCollection();
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
			foreach (SV_Question item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Questions table.
	/// </summary>
	[DataContract]
	public partial class SV_Question : ActiveRecord<SV_Question>, INotifyPropertyChanged
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

		public SV_Question()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Questions", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarQuestionID = new TableSchema.TableColumn(schema);
				colvarQuestionID.ColumnName = "QuestionID";
				colvarQuestionID.DataType = DbType.Int32;
				colvarQuestionID.MaxLength = 0;
				colvarQuestionID.AutoIncrement = true;
				colvarQuestionID.IsNullable = false;
				colvarQuestionID.IsPrimaryKey = true;
				colvarQuestionID.IsForeignKey = false;
				colvarQuestionID.IsReadOnly = false;
				colvarQuestionID.DefaultSetting = @"";
				colvarQuestionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuestionID);

				TableSchema.TableColumn colvarSurveyId = new TableSchema.TableColumn(schema);
				colvarSurveyId.ColumnName = "SurveyId";
				colvarSurveyId.DataType = DbType.Int32;
				colvarSurveyId.MaxLength = 0;
				colvarSurveyId.AutoIncrement = false;
				colvarSurveyId.IsNullable = false;
				colvarSurveyId.IsPrimaryKey = false;
				colvarSurveyId.IsForeignKey = true;
				colvarSurveyId.IsReadOnly = false;
				colvarSurveyId.DefaultSetting = @"";
				colvarSurveyId.ForeignKeyTableName = "SV_Surveys";
				schema.Columns.Add(colvarSurveyId);

				TableSchema.TableColumn colvarQuestionMeaningId = new TableSchema.TableColumn(schema);
				colvarQuestionMeaningId.ColumnName = "QuestionMeaningId";
				colvarQuestionMeaningId.DataType = DbType.Int32;
				colvarQuestionMeaningId.MaxLength = 0;
				colvarQuestionMeaningId.AutoIncrement = false;
				colvarQuestionMeaningId.IsNullable = false;
				colvarQuestionMeaningId.IsPrimaryKey = false;
				colvarQuestionMeaningId.IsForeignKey = true;
				colvarQuestionMeaningId.IsReadOnly = false;
				colvarQuestionMeaningId.DefaultSetting = @"";
				colvarQuestionMeaningId.ForeignKeyTableName = "SV_QuestionMeanings";
				schema.Columns.Add(colvarQuestionMeaningId);

				TableSchema.TableColumn colvarParentId = new TableSchema.TableColumn(schema);
				colvarParentId.ColumnName = "ParentId";
				colvarParentId.DataType = DbType.Int32;
				colvarParentId.MaxLength = 0;
				colvarParentId.AutoIncrement = false;
				colvarParentId.IsNullable = true;
				colvarParentId.IsPrimaryKey = false;
				colvarParentId.IsForeignKey = true;
				colvarParentId.IsReadOnly = false;
				colvarParentId.DefaultSetting = @"";
				colvarParentId.ForeignKeyTableName = "SV_Questions";
				schema.Columns.Add(colvarParentId);

				TableSchema.TableColumn colvarGroupOrder = new TableSchema.TableColumn(schema);
				colvarGroupOrder.ColumnName = "GroupOrder";
				colvarGroupOrder.DataType = DbType.Int32;
				colvarGroupOrder.MaxLength = 0;
				colvarGroupOrder.AutoIncrement = false;
				colvarGroupOrder.IsNullable = false;
				colvarGroupOrder.IsPrimaryKey = false;
				colvarGroupOrder.IsForeignKey = false;
				colvarGroupOrder.IsReadOnly = false;
				colvarGroupOrder.DefaultSetting = @"";
				colvarGroupOrder.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupOrder);

				TableSchema.TableColumn colvarMapToTokenId = new TableSchema.TableColumn(schema);
				colvarMapToTokenId.ColumnName = "MapToTokenId";
				colvarMapToTokenId.DataType = DbType.Int32;
				colvarMapToTokenId.MaxLength = 0;
				colvarMapToTokenId.AutoIncrement = false;
				colvarMapToTokenId.IsNullable = true;
				colvarMapToTokenId.IsPrimaryKey = false;
				colvarMapToTokenId.IsForeignKey = false;
				colvarMapToTokenId.IsReadOnly = false;
				colvarMapToTokenId.DefaultSetting = @"";
				colvarMapToTokenId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMapToTokenId);

				TableSchema.TableColumn colvarConditionJson = new TableSchema.TableColumn(schema);
				colvarConditionJson.ColumnName = "ConditionJson";
				colvarConditionJson.DataType = DbType.String;
				colvarConditionJson.MaxLength = -1;
				colvarConditionJson.AutoIncrement = false;
				colvarConditionJson.IsNullable = true;
				colvarConditionJson.IsPrimaryKey = false;
				colvarConditionJson.IsForeignKey = false;
				colvarConditionJson.IsReadOnly = false;
				colvarConditionJson.DefaultSetting = @"";
				colvarConditionJson.ForeignKeyTableName = "";
				schema.Columns.Add(colvarConditionJson);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Questions",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Question LoadFrom(SV_Question item)
		{
			SV_Question result = new SV_Question();
			if (item.QuestionID != default(int)) {
				result.LoadByKey(item.QuestionID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int QuestionID {
			get { return GetColumnValue<int>(Columns.QuestionID); }
			set {
				SetColumnValue(Columns.QuestionID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionID));
			}
		}
		[DataMember]
		public int SurveyId {
			get { return GetColumnValue<int>(Columns.SurveyId); }
			set {
				SetColumnValue(Columns.SurveyId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyId));
			}
		}
		[DataMember]
		public int QuestionMeaningId {
			get { return GetColumnValue<int>(Columns.QuestionMeaningId); }
			set {
				SetColumnValue(Columns.QuestionMeaningId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionMeaningId));
			}
		}
		[DataMember]
		public int? ParentId {
			get { return GetColumnValue<int?>(Columns.ParentId); }
			set {
				SetColumnValue(Columns.ParentId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ParentId));
			}
		}
		[DataMember]
		public int GroupOrder {
			get { return GetColumnValue<int>(Columns.GroupOrder); }
			set {
				SetColumnValue(Columns.GroupOrder, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.GroupOrder));
			}
		}
		[DataMember]
		public int? MapToTokenId {
			get { return GetColumnValue<int?>(Columns.MapToTokenId); }
			set {
				SetColumnValue(Columns.MapToTokenId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MapToTokenId));
			}
		}
		[DataMember]
		public string ConditionJson {
			get { return GetColumnValue<string>(Columns.ConditionJson); }
			set {
				SetColumnValue(Columns.ConditionJson, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ConditionJson));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_QuestionMeaning _QuestionMeaning;
		//Relationship: FK_SV_Questions_SV_QuestionMeanings
		public SV_QuestionMeaning QuestionMeaning
		{
			get
			{
				if(_QuestionMeaning == null) {
					_QuestionMeaning = SV_QuestionMeaning.FetchByID(this.QuestionMeaningId);
				}
				return _QuestionMeaning;
			}
			set
			{
				SetColumnValue("QuestionMeaningId", value.QuestionMeaningID);
				_QuestionMeaning = value;
			}
		}

		private SV_Question _Parent;
		//Relationship: FK_SV_Questions_SV_Questions
		public SV_Question Parent
		{
			get
			{
				if(_Parent == null) {
					_Parent = SV_Question.FetchByID(this.ParentId);
				}
				return _Parent;
			}
			set
			{
				SetColumnValue("ParentId", value.QuestionID);
				_Parent = value;
			}
		}

		private SV_Survey _Survey;
		//Relationship: FK_SV_Questions_SV_Surveys
		public SV_Survey Survey
		{
			get
			{
				if(_Survey == null) {
					_Survey = SV_Survey.FetchByID(this.SurveyId);
				}
				return _Survey;
			}
			set
			{
				SetColumnValue("SurveyId", value.SurveyID);
				_Survey = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return QuestionID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn QuestionIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn QuestionMeaningIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn ParentIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn GroupOrderColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn MapToTokenIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn ConditionJsonColumn
		{
			get { return Schema.Columns[6]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string QuestionID = @"QuestionID";
			public static readonly string SurveyId = @"SurveyId";
			public static readonly string QuestionMeaningId = @"QuestionMeaningId";
			public static readonly string ParentId = @"ParentId";
			public static readonly string GroupOrder = @"GroupOrder";
			public static readonly string MapToTokenId = @"MapToTokenId";
			public static readonly string ConditionJson = @"ConditionJson";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return QuestionID; }
		}
		*/

		#region Foreign Collections

		private SV_AnswerCollection _SV_AnswersCol;
		//Relationship: FK_SV_Answers_SV_Questions
		public SV_AnswerCollection SV_AnswersCol
		{
			get
			{
				if(_SV_AnswersCol == null) {
					_SV_AnswersCol = new SV_AnswerCollection();
					_SV_AnswersCol.LoadAndCloseReader(SV_Answer.Query()
						.WHERE(SV_Answer.Columns.QuestionId, QuestionID).ExecuteReader());
				}
				return _SV_AnswersCol;
			}
		}

		private SV_Questions_PossibleAnswers_MapCollection _SV_Questions_PossibleAnswers_MapsCol;
		//Relationship: FK_SV_Questions_PossibleAnswers_Map_SV_Questions
		public SV_Questions_PossibleAnswers_MapCollection SV_Questions_PossibleAnswers_MapsCol
		{
			get
			{
				if(_SV_Questions_PossibleAnswers_MapsCol == null) {
					_SV_Questions_PossibleAnswers_MapsCol = new SV_Questions_PossibleAnswers_MapCollection();
					_SV_Questions_PossibleAnswers_MapsCol.LoadAndCloseReader(SV_Questions_PossibleAnswers_Map.Query()
						.WHERE(SV_Questions_PossibleAnswers_Map.Columns.QuestionId, QuestionID).ExecuteReader());
				}
				return _SV_Questions_PossibleAnswers_MapsCol;
			}
		}

		private SV_QuestionCollection _ChildSV_QuestionsCol;
		//Relationship: FK_SV_Questions_SV_Questions
		public SV_QuestionCollection ChildSV_QuestionsCol
		{
			get
			{
				if(_ChildSV_QuestionsCol == null) {
					_ChildSV_QuestionsCol = new SV_QuestionCollection();
					_ChildSV_QuestionsCol.LoadAndCloseReader(SV_Question.Query()
						.WHERE(SV_Question.Columns.ParentId, QuestionID).ExecuteReader());
				}
				return _ChildSV_QuestionsCol;
			}
		}

		private SV_QuestionTranslationCollection _SV_QuestionTranslationsCol;
		//Relationship: FK_SV_QuestionTranslations_SV_Questions
		public SV_QuestionTranslationCollection SV_QuestionTranslationsCol
		{
			get
			{
				if(_SV_QuestionTranslationsCol == null) {
					_SV_QuestionTranslationsCol = new SV_QuestionTranslationCollection();
					_SV_QuestionTranslationsCol.LoadAndCloseReader(SV_QuestionTranslation.Query()
						.WHERE(SV_QuestionTranslation.Columns.QuestionId, QuestionID).ExecuteReader());
				}
				return _SV_QuestionTranslationsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_Questions_PossibleAnswers_Map class.
	/// </summary>
	[DataContract]
	public partial class SV_Questions_PossibleAnswers_MapCollection : ActiveList<SV_Questions_PossibleAnswers_Map, SV_Questions_PossibleAnswers_MapCollection>
	{
		public static SV_Questions_PossibleAnswers_MapCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_Questions_PossibleAnswers_MapCollection result = new SV_Questions_PossibleAnswers_MapCollection();
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
			foreach (SV_Questions_PossibleAnswers_Map item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Questions_PossibleAnswers_Map table.
	/// </summary>
	[DataContract]
	public partial class SV_Questions_PossibleAnswers_Map : ActiveRecord<SV_Questions_PossibleAnswers_Map>, INotifyPropertyChanged
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

		public SV_Questions_PossibleAnswers_Map()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Questions_PossibleAnswers_Map", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarQuestionId = new TableSchema.TableColumn(schema);
				colvarQuestionId.ColumnName = "QuestionId";
				colvarQuestionId.DataType = DbType.Int32;
				colvarQuestionId.MaxLength = 0;
				colvarQuestionId.AutoIncrement = false;
				colvarQuestionId.IsNullable = false;
				colvarQuestionId.IsPrimaryKey = true;
				colvarQuestionId.IsForeignKey = false;
				colvarQuestionId.IsReadOnly = false;
				colvarQuestionId.DefaultSetting = @"";
				colvarQuestionId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuestionId);

				TableSchema.TableColumn colvarPossibleAnswerId = new TableSchema.TableColumn(schema);
				colvarPossibleAnswerId.ColumnName = "PossibleAnswerId";
				colvarPossibleAnswerId.DataType = DbType.Int32;
				colvarPossibleAnswerId.MaxLength = 0;
				colvarPossibleAnswerId.AutoIncrement = false;
				colvarPossibleAnswerId.IsNullable = false;
				colvarPossibleAnswerId.IsPrimaryKey = false;
				colvarPossibleAnswerId.IsForeignKey = true;
				colvarPossibleAnswerId.IsReadOnly = false;
				colvarPossibleAnswerId.DefaultSetting = @"";
				colvarPossibleAnswerId.ForeignKeyTableName = "SV_PossibleAnswers";
				schema.Columns.Add(colvarPossibleAnswerId);

				TableSchema.TableColumn colvarExpands = new TableSchema.TableColumn(schema);
				colvarExpands.ColumnName = "Expands";
				colvarExpands.DataType = DbType.Boolean;
				colvarExpands.MaxLength = 0;
				colvarExpands.AutoIncrement = false;
				colvarExpands.IsNullable = false;
				colvarExpands.IsPrimaryKey = false;
				colvarExpands.IsForeignKey = false;
				colvarExpands.IsReadOnly = false;
				colvarExpands.DefaultSetting = @"";
				colvarExpands.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExpands);

				TableSchema.TableColumn colvarFails = new TableSchema.TableColumn(schema);
				colvarFails.ColumnName = "Fails";
				colvarFails.DataType = DbType.Boolean;
				colvarFails.MaxLength = 0;
				colvarFails.AutoIncrement = false;
				colvarFails.IsNullable = false;
				colvarFails.IsPrimaryKey = false;
				colvarFails.IsForeignKey = false;
				colvarFails.IsReadOnly = false;
				colvarFails.DefaultSetting = @"";
				colvarFails.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFails);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = true;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Questions_PossibleAnswers_Map",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Questions_PossibleAnswers_Map LoadFrom(SV_Questions_PossibleAnswers_Map item)
		{
			SV_Questions_PossibleAnswers_Map result = new SV_Questions_PossibleAnswers_Map();
			if (item.QuestionId != default(int)) {
				result.LoadByKey(item.QuestionId);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int QuestionId {
			get { return GetColumnValue<int>(Columns.QuestionId); }
			set {
				SetColumnValue(Columns.QuestionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionId));
			}
		}
		[DataMember]
		public int PossibleAnswerId {
			get { return GetColumnValue<int>(Columns.PossibleAnswerId); }
			set {
				SetColumnValue(Columns.PossibleAnswerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PossibleAnswerId));
			}
		}
		[DataMember]
		public bool Expands {
			get { return GetColumnValue<bool>(Columns.Expands); }
			set {
				SetColumnValue(Columns.Expands, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Expands));
			}
		}
		[DataMember]
		public bool Fails {
			get { return GetColumnValue<bool>(Columns.Fails); }
			set {
				SetColumnValue(Columns.Fails, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Fails));
			}
		}
		[DataMember]
		public DateTime? CreatedOn {
			get { return GetColumnValue<DateTime?>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_PossibleAnswer _PossibleAnswer;
		//Relationship: FK_SV_Questions_PossibleAnswers_Map_SV_PossibleAnswers
		public SV_PossibleAnswer PossibleAnswer
		{
			get
			{
				if(_PossibleAnswer == null) {
					_PossibleAnswer = SV_PossibleAnswer.FetchByID(this.PossibleAnswerId);
				}
				return _PossibleAnswer;
			}
			set
			{
				SetColumnValue("PossibleAnswerId", value.PossibleAnswerID);
				_PossibleAnswer = value;
			}
		}

		private SV_Question _Question;
		//Relationship: FK_SV_Questions_PossibleAnswers_Map_SV_Questions
		public SV_Question Question
		{
			get
			{
				if(_Question == null) {
					_Question = SV_Question.FetchByID(this.QuestionId);
				}
				return _Question;
			}
			set
			{
				SetColumnValue("QuestionId", value.QuestionID);
				_Question = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return QuestionId.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn QuestionIdColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn PossibleAnswerIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ExpandsColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn FailsColumn
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
			public static readonly string QuestionId = @"QuestionId";
			public static readonly string PossibleAnswerId = @"PossibleAnswerId";
			public static readonly string Expands = @"Expands";
			public static readonly string Fails = @"Fails";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return QuestionId; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the SV_QuestionTranslation class.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionTranslationCollection : ActiveList<SV_QuestionTranslation, SV_QuestionTranslationCollection>
	{
		public static SV_QuestionTranslationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_QuestionTranslationCollection result = new SV_QuestionTranslationCollection();
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
			foreach (SV_QuestionTranslation item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_QuestionTranslations table.
	/// </summary>
	[DataContract]
	public partial class SV_QuestionTranslation : ActiveRecord<SV_QuestionTranslation>, INotifyPropertyChanged
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

		public SV_QuestionTranslation()
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
				TableSchema.Table schema = new TableSchema.Table("SV_QuestionTranslations", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarQuestionTranslationID = new TableSchema.TableColumn(schema);
				colvarQuestionTranslationID.ColumnName = "QuestionTranslationID";
				colvarQuestionTranslationID.DataType = DbType.Int32;
				colvarQuestionTranslationID.MaxLength = 0;
				colvarQuestionTranslationID.AutoIncrement = true;
				colvarQuestionTranslationID.IsNullable = false;
				colvarQuestionTranslationID.IsPrimaryKey = true;
				colvarQuestionTranslationID.IsForeignKey = false;
				colvarQuestionTranslationID.IsReadOnly = false;
				colvarQuestionTranslationID.DefaultSetting = @"";
				colvarQuestionTranslationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuestionTranslationID);

				TableSchema.TableColumn colvarSurveyTranslationId = new TableSchema.TableColumn(schema);
				colvarSurveyTranslationId.ColumnName = "SurveyTranslationId";
				colvarSurveyTranslationId.DataType = DbType.Int32;
				colvarSurveyTranslationId.MaxLength = 0;
				colvarSurveyTranslationId.AutoIncrement = false;
				colvarSurveyTranslationId.IsNullable = false;
				colvarSurveyTranslationId.IsPrimaryKey = false;
				colvarSurveyTranslationId.IsForeignKey = true;
				colvarSurveyTranslationId.IsReadOnly = false;
				colvarSurveyTranslationId.DefaultSetting = @"";
				colvarSurveyTranslationId.ForeignKeyTableName = "SV_SurveyTranslations";
				schema.Columns.Add(colvarSurveyTranslationId);

				TableSchema.TableColumn colvarQuestionId = new TableSchema.TableColumn(schema);
				colvarQuestionId.ColumnName = "QuestionId";
				colvarQuestionId.DataType = DbType.Int32;
				colvarQuestionId.MaxLength = 0;
				colvarQuestionId.AutoIncrement = false;
				colvarQuestionId.IsNullable = false;
				colvarQuestionId.IsPrimaryKey = false;
				colvarQuestionId.IsForeignKey = true;
				colvarQuestionId.IsReadOnly = false;
				colvarQuestionId.DefaultSetting = @"";
				colvarQuestionId.ForeignKeyTableName = "SV_Questions";
				schema.Columns.Add(colvarQuestionId);

				TableSchema.TableColumn colvarTextFormat = new TableSchema.TableColumn(schema);
				colvarTextFormat.ColumnName = "TextFormat";
				colvarTextFormat.DataType = DbType.String;
				colvarTextFormat.MaxLength = -1;
				colvarTextFormat.AutoIncrement = false;
				colvarTextFormat.IsNullable = false;
				colvarTextFormat.IsPrimaryKey = false;
				colvarTextFormat.IsForeignKey = false;
				colvarTextFormat.IsReadOnly = false;
				colvarTextFormat.DefaultSetting = @"";
				colvarTextFormat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTextFormat);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_QuestionTranslations",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_QuestionTranslation LoadFrom(SV_QuestionTranslation item)
		{
			SV_QuestionTranslation result = new SV_QuestionTranslation();
			if (item.QuestionTranslationID != default(int)) {
				result.LoadByKey(item.QuestionTranslationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int QuestionTranslationID {
			get { return GetColumnValue<int>(Columns.QuestionTranslationID); }
			set {
				SetColumnValue(Columns.QuestionTranslationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionTranslationID));
			}
		}
		[DataMember]
		public int SurveyTranslationId {
			get { return GetColumnValue<int>(Columns.SurveyTranslationId); }
			set {
				SetColumnValue(Columns.SurveyTranslationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTranslationId));
			}
		}
		[DataMember]
		public int QuestionId {
			get { return GetColumnValue<int>(Columns.QuestionId); }
			set {
				SetColumnValue(Columns.QuestionId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.QuestionId));
			}
		}
		[DataMember]
		public string TextFormat {
			get { return GetColumnValue<string>(Columns.TextFormat); }
			set {
				SetColumnValue(Columns.TextFormat, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TextFormat));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_Question _Question;
		//Relationship: FK_SV_QuestionTranslations_SV_Questions
		public SV_Question Question
		{
			get
			{
				if(_Question == null) {
					_Question = SV_Question.FetchByID(this.QuestionId);
				}
				return _Question;
			}
			set
			{
				SetColumnValue("QuestionId", value.QuestionID);
				_Question = value;
			}
		}

		private SV_SurveyTranslation _SurveyTranslation;
		//Relationship: FK_SV_QuestionTranslations_SV_SurveyTranslations
		public SV_SurveyTranslation SurveyTranslation
		{
			get
			{
				if(_SurveyTranslation == null) {
					_SurveyTranslation = SV_SurveyTranslation.FetchByID(this.SurveyTranslationId);
				}
				return _SurveyTranslation;
			}
			set
			{
				SetColumnValue("SurveyTranslationId", value.SurveyTranslationID);
				_SurveyTranslation = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return QuestionTranslationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn QuestionTranslationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyTranslationIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn QuestionIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn TextFormatColumn
		{
			get { return Schema.Columns[3]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string QuestionTranslationID = @"QuestionTranslationID";
			public static readonly string SurveyTranslationId = @"SurveyTranslationId";
			public static readonly string QuestionId = @"QuestionId";
			public static readonly string TextFormat = @"TextFormat";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return QuestionTranslationID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the SV_Result class.
	/// </summary>
	[DataContract]
	public partial class SV_ResultCollection : ActiveList<SV_Result, SV_ResultCollection>
	{
		public static SV_ResultCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_ResultCollection result = new SV_ResultCollection();
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
			foreach (SV_Result item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Results table.
	/// </summary>
	[DataContract]
	public partial class SV_Result : ActiveRecord<SV_Result>, INotifyPropertyChanged
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

		public SV_Result()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Results", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarResultID = new TableSchema.TableColumn(schema);
				colvarResultID.ColumnName = "ResultID";
				colvarResultID.DataType = DbType.Int64;
				colvarResultID.MaxLength = 0;
				colvarResultID.AutoIncrement = true;
				colvarResultID.IsNullable = false;
				colvarResultID.IsPrimaryKey = true;
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
				colvarSurveyTranslationId.IsForeignKey = true;
				colvarSurveyTranslationId.IsReadOnly = false;
				colvarSurveyTranslationId.DefaultSetting = @"";
				colvarSurveyTranslationId.ForeignKeyTableName = "SV_SurveyTranslations";
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

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Results",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Result LoadFrom(SV_Result item)
		{
			SV_Result result = new SV_Result();
			if (item.ResultID != default(long)) {
				result.LoadByKey(item.ResultID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ResultID {
			get { return GetColumnValue<long>(Columns.ResultID); }
			set {
				SetColumnValue(Columns.ResultID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ResultID));
			}
		}
		[DataMember]
		public int SurveyTranslationId {
			get { return GetColumnValue<int>(Columns.SurveyTranslationId); }
			set {
				SetColumnValue(Columns.SurveyTranslationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTranslationId));
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
		public bool Passed {
			get { return GetColumnValue<bool>(Columns.Passed); }
			set {
				SetColumnValue(Columns.Passed, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Passed));
			}
		}
		[DataMember]
		public bool IsComplete {
			get { return GetColumnValue<bool>(Columns.IsComplete); }
			set {
				SetColumnValue(Columns.IsComplete, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsComplete));
			}
		}
		[DataMember]
		public string Context {
			get { return GetColumnValue<string>(Columns.Context); }
			set {
				SetColumnValue(Columns.Context, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Context));
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

		private SV_SurveyTranslation _SurveyTranslation;
		//Relationship: FK_SV_Results_SV_SurveyTranslations
		public SV_SurveyTranslation SurveyTranslation
		{
			get
			{
				if(_SurveyTranslation == null) {
					_SurveyTranslation = SV_SurveyTranslation.FetchByID(this.SurveyTranslationId);
				}
				return _SurveyTranslation;
			}
			set
			{
				SetColumnValue("SurveyTranslationId", value.SurveyTranslationID);
				_SurveyTranslation = value;
			}
		}

		#endregion //ForeignKey Properties

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

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ResultID = @"ResultID";
			public static readonly string SurveyTranslationId = @"SurveyTranslationId";
			public static readonly string AccountId = @"AccountId";
			public static readonly string Passed = @"Passed";
			public static readonly string IsComplete = @"IsComplete";
			public static readonly string Context = @"Context";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ResultID; }
		}
		*/

		#region Foreign Collections

		private SV_AnswerCollection _SV_AnswersCol;
		//Relationship: FK_SV_Answers_SV_Results
		public SV_AnswerCollection SV_AnswersCol
		{
			get
			{
				if(_SV_AnswersCol == null) {
					_SV_AnswersCol = new SV_AnswerCollection();
					_SV_AnswersCol.LoadAndCloseReader(SV_Answer.Query()
						.WHERE(SV_Answer.Columns.ResultId, ResultID).ExecuteReader());
				}
				return _SV_AnswersCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_Survey class.
	/// </summary>
	[DataContract]
	public partial class SV_SurveyCollection : ActiveList<SV_Survey, SV_SurveyCollection>
	{
		public static SV_SurveyCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_SurveyCollection result = new SV_SurveyCollection();
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
			foreach (SV_Survey item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Surveys table.
	/// </summary>
	[DataContract]
	public partial class SV_Survey : ActiveRecord<SV_Survey>, INotifyPropertyChanged
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

		public SV_Survey()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Surveys", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSurveyID = new TableSchema.TableColumn(schema);
				colvarSurveyID.ColumnName = "SurveyID";
				colvarSurveyID.DataType = DbType.Int32;
				colvarSurveyID.MaxLength = 0;
				colvarSurveyID.AutoIncrement = true;
				colvarSurveyID.IsNullable = false;
				colvarSurveyID.IsPrimaryKey = true;
				colvarSurveyID.IsForeignKey = false;
				colvarSurveyID.IsReadOnly = false;
				colvarSurveyID.DefaultSetting = @"";
				colvarSurveyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyID);

				TableSchema.TableColumn colvarSurveyTypeId = new TableSchema.TableColumn(schema);
				colvarSurveyTypeId.ColumnName = "SurveyTypeId";
				colvarSurveyTypeId.DataType = DbType.Int32;
				colvarSurveyTypeId.MaxLength = 0;
				colvarSurveyTypeId.AutoIncrement = false;
				colvarSurveyTypeId.IsNullable = false;
				colvarSurveyTypeId.IsPrimaryKey = false;
				colvarSurveyTypeId.IsForeignKey = true;
				colvarSurveyTypeId.IsReadOnly = false;
				colvarSurveyTypeId.DefaultSetting = @"";
				colvarSurveyTypeId.ForeignKeyTableName = "SV_SurveyTypes";
				schema.Columns.Add(colvarSurveyTypeId);

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

				TableSchema.TableColumn colvarIsCurrent = new TableSchema.TableColumn(schema);
				colvarIsCurrent.ColumnName = "IsCurrent";
				colvarIsCurrent.DataType = DbType.Boolean;
				colvarIsCurrent.MaxLength = 0;
				colvarIsCurrent.AutoIncrement = false;
				colvarIsCurrent.IsNullable = false;
				colvarIsCurrent.IsPrimaryKey = false;
				colvarIsCurrent.IsForeignKey = false;
				colvarIsCurrent.IsReadOnly = false;
				colvarIsCurrent.DefaultSetting = @"";
				colvarIsCurrent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsCurrent);

				TableSchema.TableColumn colvarIsReadonly = new TableSchema.TableColumn(schema);
				colvarIsReadonly.ColumnName = "IsReadonly";
				colvarIsReadonly.DataType = DbType.Boolean;
				colvarIsReadonly.MaxLength = 0;
				colvarIsReadonly.AutoIncrement = false;
				colvarIsReadonly.IsNullable = false;
				colvarIsReadonly.IsPrimaryKey = false;
				colvarIsReadonly.IsForeignKey = false;
				colvarIsReadonly.IsReadOnly = false;
				colvarIsReadonly.DefaultSetting = @"";
				colvarIsReadonly.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsReadonly);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Surveys",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Survey LoadFrom(SV_Survey item)
		{
			SV_Survey result = new SV_Survey();
			if (item.SurveyID != default(int)) {
				result.LoadByKey(item.SurveyID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int SurveyID {
			get { return GetColumnValue<int>(Columns.SurveyID); }
			set {
				SetColumnValue(Columns.SurveyID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyID));
			}
		}
		[DataMember]
		public int SurveyTypeId {
			get { return GetColumnValue<int>(Columns.SurveyTypeId); }
			set {
				SetColumnValue(Columns.SurveyTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTypeId));
			}
		}
		[DataMember]
		public string Version {
			get { return GetColumnValue<string>(Columns.Version); }
			set {
				SetColumnValue(Columns.Version, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Version));
			}
		}
		[DataMember]
		public bool IsCurrent {
			get { return GetColumnValue<bool>(Columns.IsCurrent); }
			set {
				SetColumnValue(Columns.IsCurrent, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsCurrent));
			}
		}
		[DataMember]
		public bool IsReadonly {
			get { return GetColumnValue<bool>(Columns.IsReadonly); }
			set {
				SetColumnValue(Columns.IsReadonly, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsReadonly));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_SurveyType _SurveyType;
		//Relationship: FK_SV_Surveys_SV_SurveyTypes
		public SV_SurveyType SurveyType
		{
			get
			{
				if(_SurveyType == null) {
					_SurveyType = SV_SurveyType.FetchByID(this.SurveyTypeId);
				}
				return _SurveyType;
			}
			set
			{
				SetColumnValue("SurveyTypeId", value.SurveyTypeID);
				_SurveyType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return SurveyID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn SurveyIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn VersionColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn IsCurrentColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn IsReadonlyColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SurveyID = @"SurveyID";
			public static readonly string SurveyTypeId = @"SurveyTypeId";
			public static readonly string Version = @"Version";
			public static readonly string IsCurrent = @"IsCurrent";
			public static readonly string IsReadonly = @"IsReadonly";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return SurveyID; }
		}
		*/

		#region Foreign Collections

		private SV_QuestionCollection _SV_QuestionsCol;
		//Relationship: FK_SV_Questions_SV_Surveys
		public SV_QuestionCollection SV_QuestionsCol
		{
			get
			{
				if(_SV_QuestionsCol == null) {
					_SV_QuestionsCol = new SV_QuestionCollection();
					_SV_QuestionsCol.LoadAndCloseReader(SV_Question.Query()
						.WHERE(SV_Question.Columns.SurveyId, SurveyID).ExecuteReader());
				}
				return _SV_QuestionsCol;
			}
		}

		private SV_SurveyTranslationCollection _SV_SurveyTranslationsCol;
		//Relationship: FK_SV_SurveyTranslations_SV_Surveys
		public SV_SurveyTranslationCollection SV_SurveyTranslationsCol
		{
			get
			{
				if(_SV_SurveyTranslationsCol == null) {
					_SV_SurveyTranslationsCol = new SV_SurveyTranslationCollection();
					_SV_SurveyTranslationsCol.LoadAndCloseReader(SV_SurveyTranslation.Query()
						.WHERE(SV_SurveyTranslation.Columns.SurveyId, SurveyID).ExecuteReader());
				}
				return _SV_SurveyTranslationsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_SurveyTranslation class.
	/// </summary>
	[DataContract]
	public partial class SV_SurveyTranslationCollection : ActiveList<SV_SurveyTranslation, SV_SurveyTranslationCollection>
	{
		public static SV_SurveyTranslationCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_SurveyTranslationCollection result = new SV_SurveyTranslationCollection();
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
			foreach (SV_SurveyTranslation item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_SurveyTranslations table.
	/// </summary>
	[DataContract]
	public partial class SV_SurveyTranslation : ActiveRecord<SV_SurveyTranslation>, INotifyPropertyChanged
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

		public SV_SurveyTranslation()
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
				TableSchema.Table schema = new TableSchema.Table("SV_SurveyTranslations", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSurveyTranslationID = new TableSchema.TableColumn(schema);
				colvarSurveyTranslationID.ColumnName = "SurveyTranslationID";
				colvarSurveyTranslationID.DataType = DbType.Int32;
				colvarSurveyTranslationID.MaxLength = 0;
				colvarSurveyTranslationID.AutoIncrement = true;
				colvarSurveyTranslationID.IsNullable = false;
				colvarSurveyTranslationID.IsPrimaryKey = true;
				colvarSurveyTranslationID.IsForeignKey = false;
				colvarSurveyTranslationID.IsReadOnly = false;
				colvarSurveyTranslationID.DefaultSetting = @"";
				colvarSurveyTranslationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyTranslationID);

				TableSchema.TableColumn colvarSurveyId = new TableSchema.TableColumn(schema);
				colvarSurveyId.ColumnName = "SurveyId";
				colvarSurveyId.DataType = DbType.Int32;
				colvarSurveyId.MaxLength = 0;
				colvarSurveyId.AutoIncrement = false;
				colvarSurveyId.IsNullable = false;
				colvarSurveyId.IsPrimaryKey = false;
				colvarSurveyId.IsForeignKey = true;
				colvarSurveyId.IsReadOnly = false;
				colvarSurveyId.DefaultSetting = @"";
				colvarSurveyId.ForeignKeyTableName = "SV_Surveys";
				schema.Columns.Add(colvarSurveyId);

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
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_SurveyTranslations",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_SurveyTranslation LoadFrom(SV_SurveyTranslation item)
		{
			SV_SurveyTranslation result = new SV_SurveyTranslation();
			if (item.SurveyTranslationID != default(int)) {
				result.LoadByKey(item.SurveyTranslationID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int SurveyTranslationID {
			get { return GetColumnValue<int>(Columns.SurveyTranslationID); }
			set {
				SetColumnValue(Columns.SurveyTranslationID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTranslationID));
			}
		}
		[DataMember]
		public int SurveyId {
			get { return GetColumnValue<int>(Columns.SurveyId); }
			set {
				SetColumnValue(Columns.SurveyId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyId));
			}
		}
		[DataMember]
		public string LocalizationCode {
			get { return GetColumnValue<string>(Columns.LocalizationCode); }
			set {
				SetColumnValue(Columns.LocalizationCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocalizationCode));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private SV_Survey _Survey;
		//Relationship: FK_SV_SurveyTranslations_SV_Surveys
		public SV_Survey Survey
		{
			get
			{
				if(_Survey == null) {
					_Survey = SV_Survey.FetchByID(this.SurveyId);
				}
				return _Survey;
			}
			set
			{
				SetColumnValue("SurveyId", value.SurveyID);
				_Survey = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return SurveyTranslationID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn SurveyTranslationIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn SurveyIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn LocalizationCodeColumn
		{
			get { return Schema.Columns[2]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SurveyTranslationID = @"SurveyTranslationID";
			public static readonly string SurveyId = @"SurveyId";
			public static readonly string LocalizationCode = @"LocalizationCode";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return SurveyTranslationID; }
		}
		*/

		#region Foreign Collections

		private SV_QuestionTranslationCollection _SV_QuestionTranslationsCol;
		//Relationship: FK_SV_QuestionTranslations_SV_SurveyTranslations
		public SV_QuestionTranslationCollection SV_QuestionTranslationsCol
		{
			get
			{
				if(_SV_QuestionTranslationsCol == null) {
					_SV_QuestionTranslationsCol = new SV_QuestionTranslationCollection();
					_SV_QuestionTranslationsCol.LoadAndCloseReader(SV_QuestionTranslation.Query()
						.WHERE(SV_QuestionTranslation.Columns.SurveyTranslationId, SurveyTranslationID).ExecuteReader());
				}
				return _SV_QuestionTranslationsCol;
			}
		}

		private SV_ResultCollection _SV_ResultsCol;
		//Relationship: FK_SV_Results_SV_SurveyTranslations
		public SV_ResultCollection SV_ResultsCol
		{
			get
			{
				if(_SV_ResultsCol == null) {
					_SV_ResultsCol = new SV_ResultCollection();
					_SV_ResultsCol.LoadAndCloseReader(SV_Result.Query()
						.WHERE(SV_Result.Columns.SurveyTranslationId, SurveyTranslationID).ExecuteReader());
				}
				return _SV_ResultsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_SurveyType class.
	/// </summary>
	[DataContract]
	public partial class SV_SurveyTypeCollection : ActiveList<SV_SurveyType, SV_SurveyTypeCollection>
	{
		public static SV_SurveyTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_SurveyTypeCollection result = new SV_SurveyTypeCollection();
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
			foreach (SV_SurveyType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_SurveyTypes table.
	/// </summary>
	[DataContract]
	public partial class SV_SurveyType : ActiveRecord<SV_SurveyType>, INotifyPropertyChanged
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

		public SV_SurveyType()
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
				TableSchema.Table schema = new TableSchema.Table("SV_SurveyTypes", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarSurveyTypeID = new TableSchema.TableColumn(schema);
				colvarSurveyTypeID.ColumnName = "SurveyTypeID";
				colvarSurveyTypeID.DataType = DbType.Int32;
				colvarSurveyTypeID.MaxLength = 0;
				colvarSurveyTypeID.AutoIncrement = false;
				colvarSurveyTypeID.IsNullable = false;
				colvarSurveyTypeID.IsPrimaryKey = true;
				colvarSurveyTypeID.IsForeignKey = false;
				colvarSurveyTypeID.IsReadOnly = false;
				colvarSurveyTypeID.DefaultSetting = @"";
				colvarSurveyTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSurveyTypeID);

				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.AnsiString;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_SurveyTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_SurveyType LoadFrom(SV_SurveyType item)
		{
			SV_SurveyType result = new SV_SurveyType();
			if (item.SurveyTypeID != default(int)) {
				result.LoadByKey(item.SurveyTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int SurveyTypeID {
			get { return GetColumnValue<int>(Columns.SurveyTypeID); }
			set {
				SetColumnValue(Columns.SurveyTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SurveyTypeID));
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

		#endregion //Properties


		public override string ToString()
		{
			return Name;
		}

		#region Typed Columns

		public static TableSchema.TableColumn SurveyTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn NameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string SurveyTypeID = @"SurveyTypeID";
			public static readonly string Name = @"Name";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return SurveyTypeID; }
		}
		*/

		#region Foreign Collections

		private SV_QuestionMeaningCollection _SV_QuestionMeaningsCol;
		//Relationship: FK_SV_QuestionMeanings_SV_SurveyTypes
		public SV_QuestionMeaningCollection SV_QuestionMeaningsCol
		{
			get
			{
				if(_SV_QuestionMeaningsCol == null) {
					_SV_QuestionMeaningsCol = new SV_QuestionMeaningCollection();
					_SV_QuestionMeaningsCol.LoadAndCloseReader(SV_QuestionMeaning.Query()
						.WHERE(SV_QuestionMeaning.Columns.SurveyTypeId, SurveyTypeID).ExecuteReader());
				}
				return _SV_QuestionMeaningsCol;
			}
		}

		private SV_SurveyCollection _SV_SurveysCol;
		//Relationship: FK_SV_Surveys_SV_SurveyTypes
		public SV_SurveyCollection SV_SurveysCol
		{
			get
			{
				if(_SV_SurveysCol == null) {
					_SV_SurveysCol = new SV_SurveyCollection();
					_SV_SurveysCol.LoadAndCloseReader(SV_Survey.Query()
						.WHERE(SV_Survey.Columns.SurveyTypeId, SurveyTypeID).ExecuteReader());
				}
				return _SV_SurveysCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the SV_Token class.
	/// </summary>
	[DataContract]
	public partial class SV_TokenCollection : ActiveList<SV_Token, SV_TokenCollection>
	{
		public static SV_TokenCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			SV_TokenCollection result = new SV_TokenCollection();
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
			foreach (SV_Token item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the SV_Tokens table.
	/// </summary>
	[DataContract]
	public partial class SV_Token : ActiveRecord<SV_Token>, INotifyPropertyChanged
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

		public SV_Token()
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
				TableSchema.Table schema = new TableSchema.Table("SV_Tokens", TableType.Table, DataService.GetInstance("SseSurveyEngineProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarTokenID = new TableSchema.TableColumn(schema);
				colvarTokenID.ColumnName = "TokenID";
				colvarTokenID.DataType = DbType.Int32;
				colvarTokenID.MaxLength = 0;
				colvarTokenID.AutoIncrement = false;
				colvarTokenID.IsNullable = false;
				colvarTokenID.IsPrimaryKey = true;
				colvarTokenID.IsForeignKey = false;
				colvarTokenID.IsReadOnly = false;
				colvarTokenID.DefaultSetting = @"";
				colvarTokenID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTokenID);

				TableSchema.TableColumn colvarToken = new TableSchema.TableColumn(schema);
				colvarToken.ColumnName = "Token";
				colvarToken.DataType = DbType.AnsiString;
				colvarToken.MaxLength = 512;
				colvarToken.AutoIncrement = false;
				colvarToken.IsNullable = true;
				colvarToken.IsPrimaryKey = false;
				colvarToken.IsForeignKey = false;
				colvarToken.IsReadOnly = false;
				colvarToken.DefaultSetting = @"";
				colvarToken.ForeignKeyTableName = "";
				schema.Columns.Add(colvarToken);

				BaseSchema = schema;
				DataService.Providers["SseSurveyEngineProvider"].AddSchema("SV_Tokens",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static SV_Token LoadFrom(SV_Token item)
		{
			SV_Token result = new SV_Token();
			if (item.TokenID != default(int)) {
				result.LoadByKey(item.TokenID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public int TokenID {
			get { return GetColumnValue<int>(Columns.TokenID); }
			set {
				SetColumnValue(Columns.TokenID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TokenID));
			}
		}
		[DataMember]
		public string Token {
			get { return GetColumnValue<string>(Columns.Token); }
			set {
				SetColumnValue(Columns.Token, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Token));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return Token;
		}

		#region Typed Columns

		public static TableSchema.TableColumn TokenIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn TokenColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string TokenID = @"TokenID";
			public static readonly string Token = @"Token";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return TokenID; }
		}
		*/

		#region Foreign Collections

		private SV_QuestionMeanings_Tokens_MapCollection _SV_QuestionMeanings_Tokens_MapsCol;
		//Relationship: FK_SV_QuestionMeanings_Tokens_Map_SV_Tokens
		public SV_QuestionMeanings_Tokens_MapCollection SV_QuestionMeanings_Tokens_MapsCol
		{
			get
			{
				if(_SV_QuestionMeanings_Tokens_MapsCol == null) {
					_SV_QuestionMeanings_Tokens_MapsCol = new SV_QuestionMeanings_Tokens_MapCollection();
					_SV_QuestionMeanings_Tokens_MapsCol.LoadAndCloseReader(SV_QuestionMeanings_Tokens_Map.Query()
						.WHERE(SV_QuestionMeanings_Tokens_Map.Columns.TokenId, TokenID).ExecuteReader());
				}
				return _SV_QuestionMeanings_Tokens_MapsCol;
			}
		}

		#endregion Foreign Collections

	}
}
