using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb
{
	public class MsSqlGenerator : Generator
	{
		IDataReader GetReader(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand(sql, conn);
			conn.Open();
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}
		SqlCommand GetCommand(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand(sql, conn);
			conn.Open();
			return cmd;
		}

		public MsSqlGenerator(ServerConnection connection, TopDbSettings topSettings)
			: base(topSettings)
		{
			const string format = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
			ConnectionString = string.Format(format, connection.Host, topSettings.Database, connection.Username, connection.Password);
		}

		public override List<DbSPParam> GetAllSPParams()
		{
			string spParamSqlAll =
@"SELECT
    PARAMETER_NAME AS Name
    , REPLACE(PARAMETER_NAME, '@', '') AS CleanName
    , DATA_TYPE AS DataType
    , SPECIFIC_SCHEMA AS SPSchema
    , SPECIFIC_NAME AS SPName
    , ORDINAL_POSITION AS OrdinalPosition
    , PARAMETER_MODE AS ParamType
    , IS_RESULT AS IsResult
    , CAST(CHARACTER_MAXIMUM_LENGTH AS BIGINT) AS DataLength
    , PARAMETER_MODE as [Mode]
    , CAST(NUMERIC_PRECISION AS BIGINT) as NumericPrecision
    , CAST(NUMERIC_SCALE AS BIGINT) as NumericScale
FROM INFORMATION_SCHEMA.PARAMETERS
ORDER BY ORDINAL_POSITION";

			var result = new List<DbSPParam>();
			using (IDataReader rdr = GetReader(spParamSqlAll))
			{
				while (rdr.Read())
				{
					var par = new DbSPParam();
					//SetParameter(rdr, par);
					par.Name = rdr.GetString(rdr.GetOrdinal("Name"));
					par.CleanName = rdr.GetString(rdr.GetOrdinal("CleanName"));
					par.DataType = rdr.GetString(rdr.GetOrdinal("DataType"));
					par.SPSchema = rdr.GetString(rdr.GetOrdinal("SPSchema"));
					par.SPName = rdr.GetString(rdr.GetOrdinal("SPName"));
					par.OrdinalPosition = rdr.GetInt32(rdr.GetOrdinal("OrdinalPosition"));
					par.ParamType = rdr.GetString(rdr.GetOrdinal("ParamType"));
					par.IsResult = rdr.GetString(rdr.GetOrdinal("IsResult"));
					par.DataLength = GetNullableInt64(rdr, rdr.GetOrdinal("DataLength"));
					par.Mode = (rdr.GetString(rdr.GetOrdinal("Mode")) == "INOUT") ? ParameterDirection.InputOutput : ParameterDirection.Input;
					par.NumericPrecision = GetNullableInt64(rdr, rdr.GetOrdinal("NumericPrecision"));
					par.NumericScale = GetNullableInt64(rdr, rdr.GetOrdinal("NumericScale"));
					result.Add(par);
				}
			}
			return result;
		}
		//bool IsNullableDbType(DbType dbType)
		//{
		//	switch (dbType)
		//	{
		//		case DbType.AnsiString:
		//		case DbType.AnsiStringFixedLength:
		//		case DbType.Binary:
		//		case DbType.Object:
		//		case DbType.String:
		//		case DbType.StringFixedLength:
		//			return false;
		//		default:
		//			return true;
		//	}
		//}
		public override List<SP> GetSPs()
		{
			DataTable sprocs;
			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{
				conn.Open();
				sprocs = conn.GetSchema("Procedures");
				conn.Close();
			}

			var result = new List<SP>();
			foreach (DataRow row in sprocs.Rows)
			{
				string spType = row["ROUTINE_TYPE"].ToString();
				var sp = new SP();
				sp.Name = row["ROUTINE_NAME"].ToString();

				//sp.CleanName = get.TransformName(lang, sp.Name);
				//sp.Parameters = GetSPParams(sp.Name);
				result.Add(sp);
			}
			return result;
		}

		public override Dictionary<string, List<Column>> TableColumnMap()
		{
			var _tableColumnDict = new Dictionary<string, List<Column>>();

			//load
			var result = new List<Column>();

			#region Sql
			string sql =
@"SELECT
	C.TABLE_SCHEMA AS [Schema],
	C.TABLE_NAME AS TableName,
	C.ORDINAL_POSITION AS OrdinalPosition,
	C.COLUMN_NAME AS Name,
	C.COLLATION_NAME AS [CollationName],
	CAST(COALESCE(Computed.IsComputed, 0) AS BIT) AS IsComputed,
	Computed.ComputedDefinition,
	C.DATA_TYPE AS DataType,
	CAST(COALESCE(Ident.IsIdentity, 0) AS BIT) AS IsIdentity,
	CAST(Ident.SeedValue AS BIGINT) AS SeedValue,
	CAST(Ident.IncrementValue AS BIGINT) AS IncrementValue,
	CAST(C.CHARACTER_MAXIMUM_LENGTH AS BIGINT) AS MaxLength,
	CAST(C.DATETIME_PRECISION AS BIGINT) AS DatePrecision,
	CAST(C.NUMERIC_PRECISION AS BIGINT) AS NumericPrecision,
	CAST(C.NUMERIC_SCALE AS BIGINT) AS NumericScale,
	--C.NUMERIC_PRECISION_RADIX AS NumericPrecisionRadix,
	CAST((CASE WHEN C.IS_NULLABLE = 'YES' THEN 1 ELSE 0 END) AS BIT) AS IsNullable,
	DFConstraint.ConstraintName AS DefaultConstraintName,
	C.COLUMN_DEFAULT AS DefaultValue,
	CAST(COALESCE(PKColumn.IsPK, 0) AS BIT) AS IsPK
FROM INFORMATION_SCHEMA.COLUMNS AS C
LEFT OUTER JOIN
(
	SELECT
		KCU.COLUMN_NAME AS ColumnName
		, TC.TABLE_NAME AS TableName
		, CAST(1 AS BIT) AS IsPK
	FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
	JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
	ON
		KCU.CONSTRAINT_NAME=TC.CONSTRAINT_NAME
	WHERE
		TC.CONSTRAINT_TYPE='PRIMARY KEY'
) AS PKColumn
ON
	(C.COLUMN_NAME = PKColumn.ColumnName)
	AND (C.TABLE_NAME = PKColumn.TableName)
LEFT OUTER JOIN
(
	SELECT
		tables.name AS TableName,
		identity_columns.name AS ColumnName,
		identity_columns.is_identity AS IsIdentity,
	CAST(identity_columns.seed_value AS DECIMAL(18,0)) AS SeedValue,
	CAST(identity_columns.increment_value AS DECIMAL(18,0)) AS IncrementValue
	FROM sys.identity_columns
	INNER JOIN sys.tables
	ON
		identity_columns.object_id = tables.object_id
) AS Ident
ON
	(C.TABLE_NAME = Ident.TableName)
	AND (C.COLUMN_NAME = Ident.ColumnName)
LEFT OUTER JOIN
(
	SELECT
		default_constraints.name AS ConstraintName,
		tables.name AS TableName,
		all_columns.name AS ColumnName
	FROM sys.all_columns
	INNER JOIN sys.tables
	ON
		all_columns.object_id = tables.object_id
	INNER JOIN sys.schemas
	ON
		tables.schema_id = schemas.schema_id
	INNER JOIN sys.default_constraints
	ON
		all_columns.default_object_id = default_constraints.object_id
	WHERE
		schemas.name = 'dbo'
) AS DFConstraint
ON
	(C.TABLE_NAME = DFConstraint.TableName)
	AND (C.COLUMN_NAME = DFConstraint.ColumnName)
LEFT OUTER JOIN
(
	SELECT
		tables.name AS TableName,
		computed_columns.name AS ColumnName,
		computed_columns.is_computed AS IsComputed,
		CONCAT(computed_columns.definition, (CASE WHEN computed_columns.is_persisted = 1 THEN ' PERSISTED' ELSE '' END)) AS ComputedDefinition
	FROM sys.computed_columns
	INNER JOIN sys.tables
	ON
		computed_columns.object_id = tables.object_id
) AS Computed
ON
	(C.TABLE_NAME = Computed.TableName)
	AND (C.COLUMN_NAME = Computed.ColumnName)
ORDER BY
	[Schema],
	TableName ASC,
	OrdinalPosition ASC";
			#endregion //Sql

			string currentTableName = null;
			List<Column> list = null;

			bool canSetPk = true;//only allow one PK per table
			using (IDataReader rdr = GetReader(sql))
			{
				while (rdr.Read())
				{
					string tableName = (string)rdr["TableName"];
					if (currentTableName != tableName)
					{
						currentTableName = tableName;
						list = new List<Column>();
						_tableColumnDict.Add(currentTableName, list);
						canSetPk = true;
					}

					Column col = new Column();
					col.Schema = rdr.GetString(rdr.GetOrdinal("Schema"));
					col.TableName = rdr.GetString(rdr.GetOrdinal("TableName"));
					col.OrdinalPosition = rdr.GetInt32(rdr.GetOrdinal("OrdinalPosition"));
					col.Name = rdr.GetString(rdr.GetOrdinal("Name"));
					col.CollationName = GetNullableString(rdr, rdr.GetOrdinal("CollationName"));
					col.IsComputed = rdr.GetBoolean(rdr.GetOrdinal("IsComputed"));
					col.ComputedDefinition = GetNullableString(rdr, rdr.GetOrdinal("ComputedDefinition"));
					col.DataType = rdr.GetString(rdr.GetOrdinal("DataType"));
					col.IsIdentity = rdr.GetBoolean(rdr.GetOrdinal("IsIdentity"));
					col.SeedValue = GetNullableInt64(rdr, rdr.GetOrdinal("SeedValue"));
					col.IncrementValue = GetNullableInt64(rdr, rdr.GetOrdinal("IncrementValue"));
					col.MaxLength = GetNullableInt64(rdr, rdr.GetOrdinal("MaxLength"));
					col.DatePrecision = GetNullableInt64(rdr, rdr.GetOrdinal("DatePrecision"));
					col.NumericPrecision = GetNullableInt64(rdr, rdr.GetOrdinal("NumericPrecision"));
					col.NumericScale = GetNullableInt64(rdr, rdr.GetOrdinal("NumericScale"));
					col.IsNullable = rdr.GetBoolean(rdr.GetOrdinal("IsNullable"));
					col.DefaultConstraintName = GetNullableString(rdr, rdr.GetOrdinal("DefaultConstraintName"));
					col.DefaultValue = GetNullableString(rdr, rdr.GetOrdinal("DefaultValue"));
					col.IsPK = rdr.GetBoolean(rdr.GetOrdinal("IsPK"));
					if (canSetPk)
					{
						if (col.IsPK)
						{
							// core.Logger.Debug("TableColumnMap", "col", col)
							canSetPk = false;
						}
					}
					else if (col.IsPK)
					{
						//core.Logger.Debug("TableColumnMap - excessive primary keys", "tbl", currentTableName, "col", col.Name);
						col.IsPK = false; // set to false to match current generated code
					}
					//
					list.Add(col);
				}
			}
			return _tableColumnDict;
		}

		private static long? GetNullableInt64(IDataReader rdr, int index)
		{
			var obj = rdr[index];
			return (obj == DBNull.Value) ? (long?)null : (long)obj;
		}
		private static string GetNullableString(IDataReader rdr, int index)
		{
			var obj = rdr[index];
			return (obj == DBNull.Value) ? null : (string)obj;
		}

		//public IEnumerable<Table> GetAllTables()
		//{
		//	foreach (Table tbl in LoadTables())
		//	{
		//		if (tbl.PrimaryKey != null)
		//		{
		//			yield return tbl;
		//		}
		//	}
		//	foreach (Table tbl in LoadViews())
		//	{
		//		yield return tbl;
		//	}
		//}

		public override List<Table> LoadTables(string database, bool isTables, Dictionary<string, List<Column>> tableColumnMap, ForeignKeyRowCollection fkRows)//, tableIndexMap TableIndexMap, checkConstraints map[string][]CheckConstraint) []Table {
		{
			string sql;
			if (isTables)
			{
				sql = @"SELECT
	T.TABLE_SCHEMA
	, T.TABLE_NAME
	-- , T.TABLE_CATALOG
	-- , T.TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES AS T
WHERE
	T.TABLE_TYPE='BASE TABLE'
ORDER BY
	T.TABLE_SCHEMA,
	T.TABLE_NAME";
			}
			else
			{
				sql = @"SELECT
	T.TABLE_SCHEMA
	, T.TABLE_NAME
    -- , T.TABLE_CATALOG
	-- , T.TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES AS T
WHERE
	T.TABLE_TYPE='VIEW'
ORDER BY
	T.TABLE_SCHEMA,
	T.TABLE_NAME";
			}

			var allTables = new List<Table>();
			using (IDataReader rdr = GetReader(sql))
			{
				while (rdr.Read())
				{
					var tbl = new Table();
					tbl.Name = (string)rdr["TABLE_NAME"];
					if (!tableColumnMap.ContainsKey(tbl.Name))
					{
						core.Logger.Info("No columns for Table: " + tbl.Name);
						continue;
					}
					tbl.Schema = (string)rdr["TABLE_SCHEMA"];
					allTables.Add(tbl);
				}
			}
			core.Logger.Debug(database, "isTables", isTables, "count", allTables.Count);

			var tables = new List<Table>();
			foreach (var tbl in allTables)
			{
				var columns = tableColumnMap[tbl.Name];

				core.Logger.Info("Generate", "table", tbl.Name);
				tbl.Columns = columns;

				tbl.SingleFKTables = LoadSingleFKTables(tbl.Name, fkRows);
				tbl.QueryableFKTables = LoadManyFKTables(tbl.Name, fkRows);

				tbl.FKTables = LoadFKTables(tbl.Name, fkRows);

				//loop the FK tables and see if there's a match for our FK columns
				foreach (var col in columns)
				{
					// core.Logger.Debug("------------------col data", "col", col)
					FKTable fkt = null;
					foreach (var t in tbl.FKTables)
					{
						if (string.Equals(t.ThisColumn, col.Name, StringComparison.InvariantCultureIgnoreCase))
						{
							fkt = t;
							break;
						}
					}
					col.IsForeignKey = !col.IsPK && fkt != null;
					col.ForeignKeyTableName = "";
					if (!col.IsPK && fkt != null)
					{
						col.ForeignKeyTableName = fkt.OtherTable;
					}
				}

				//if (this.Context.EnumTables.Contains(tbl.Name))
				//	tbl.Enum = GetEnumTable(tbl.Name);
				//if (this.Context.MetaDataTables.Contains(tbl.Name))
				//	tbl.MetaData = GetMetaDataTable(tbl.Name);

				//// core.Logger.Debug("loadTables", "tableIndexMap", tableIndexMap)
				//if tableIndexMap != null {
				//	tbl.AllIndexes = tableIndexMap[tbl.Name];
				//}
				//if checkConstraints != null {
				//	tbl.CheckConstraints = checkConstraints[tbl.Name];
				//}

				tbl.PK = tbl.Columns.SingleOrDefault(x => x.IsPK) ?? tbl.Columns[0];
				if (isTables && tbl.PK == null)
					core.Logger.Warn("Table missing primary key", "table", tbl.Name);
				tables.Add(tbl);
			}
			return tables;
		}
		List<FKTable> LoadSingleFKTables(string tableName, ForeignKeyRowCollection fkRows)
		{
			var results = new List<FKTable>();
			var thisColumnUsed = new Dictionary<string, bool>();
			foreach (var row in fkRows)
			{
				// test for correct table
				if (!string.Equals(row.ThisTable, tableName, StringComparison.InvariantCultureIgnoreCase))
					continue;
				// test for column used
				if (thisColumnUsed.ContainsKey(row.ThisColumn))
					continue;
				thisColumnUsed.Add(row.ThisColumn, true);

				var fk = new FKTable();
				fk.ThisTable = row.ThisTable;
				fk.ThisColumn = row.ThisColumn;

				fk.OtherTable = row.OtherTable;
				fk.OtherColumn = row.OtherColumn;

				fk.ConstraintName = row.ConstraintName;

				//fk.UpdateRule = row.UpdateRule;
				//fk.DeleteRule = row.DeleteRule;

				//remove ID or add Value
				if (fk.ThisColumn.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase))
					fk.PropName = fk.ThisColumn.Substring(0, fk.ThisColumn.Length - 2);
				else
					fk.PropName = fk.ThisColumn + "Value";

				results.Add(fk);
			}

			return results;
		}
		List<FKTable> LoadManyFKTables(string tableName, ForeignKeyRowCollection fkRows)
		{
			var results = new List<FKTable>();
			foreach (var row in fkRows)
			{
				// test for correct table
				if (!string.Equals(row.OtherTable, tableName, StringComparison.InvariantCultureIgnoreCase))
					continue;

				var fk = new FKTable();
				fk.ThisTable = row.OtherTable;
				fk.ThisColumn = row.OtherColumn;

				fk.OtherTable = row.ThisTable;
				fk.OtherColumn = row.ThisColumn;

				fk.ConstraintName = row.ConstraintName;

				results.Add(fk);
			}

			return results;
		}
		List<FKTable> LoadFKTables(string tableName, ForeignKeyRowCollection fkRows)
		{
			var results = new List<FKTable>();
			foreach (var row in fkRows)
			{
				// test for correct table
				if (!string.Equals(row.ThisTable, tableName, StringComparison.InvariantCultureIgnoreCase) &&
					!string.Equals(row.OtherTable, tableName, StringComparison.InvariantCultureIgnoreCase))
					continue;

				var fk = new FKTable();
				if (string.Equals(row.ThisTable, tableName, StringComparison.InvariantCultureIgnoreCase))
				{
					fk.ThisTable = row.ThisTable;
					fk.ThisColumn = row.ThisColumn;

					fk.OtherTable = row.OtherTable;
					fk.OtherColumn = row.OtherColumn;
				}
				else
				{
					fk.ThisTable = row.OtherTable;
					fk.ThisColumn = row.OtherColumn;

					fk.OtherTable = row.ThisTable;
					fk.OtherColumn = row.ThisColumn;
				}

				fk.ConstraintName = row.ConstraintName;

				results.Add(fk);
			}

			return results;
		}

		//func getEnumTable(tableName string) *EnumTable {
		//	//////////////////////TESTING//////////////////////////
		//	var idDbType DbType
		//	idDbType = Int32
		//	//////////////////////TESTING//////////////////////////

		//	result := new(EnumTable)

		//	rows, err := dbmap.Db.Query("SELECT * FROM " + tableName)
		//	if err != null {
		//		panic(err)
		//	}
		//	defer rows.Close()

		//	columns, err := rows.Columns()
		//	if err != null {
		//		panic(err)
		//	}

		//	result.Columns = columns
		//	result.IDName = columns[0]
		//	result.ValueName = columns[1]
		//	result.EnumName = result.IDName
		//	if strings.HasSuffix(strings.ToLower(result.EnumName), "id") {
		//		result.EnumName = result.EnumName[:len(result.EnumName)-2]
		//	}

		//	baseClassName := ""
		//	switch idDbType {
		//	case Int16:
		//		baseClassName = "short"
		//	case Int32:
		//		baseClassName = "int"
		//	case Int64:
		//		baseClassName = "long"
		//	default:
		//		panic("Enum type must be int16, int32 or int64")
		//	}
		//	result.BaseClassName = baseClassName

		//	tmp := make([]interface{}, len(columns))
		//	for i := range columns {
		//		tmp[i] = &GenericValue{}
		//	}
		//	for rows.Next() {
		//		if err := rows.Scan(tmp...); err != null {
		//			panic(err)
		//		}
		//		id := tmp[0].(*GenericValue).Value
		//		// name := transformEnumName(tmp[1].(*GenericValue).Value.(string))
		//		realName := tmp[1].(*GenericValue).Value.(string)

		//		insertValues := make([]string, len(columns))
		//		for i, gv := range tmp {
		//			switch t := gv.(*GenericValue).Value.(type) {
		//			case null:
		//				insertValues[i] = "NULL"
		//			case string:
		//				insertValues[i] = "'" + t + "'"
		//			case time.Time:
		//				// format all as DATETIME. DATETIME2 not supported
		//				insertValues[i] = "'" + t.UTC().Format("2006-01-02 15:04:05.999") + "'" // fmt.Sprintf("'%v'", t)
		//			case bool:
		//				if t {
		//					insertValues[i] = "1"
		//				} else {
		//					insertValues[i] = "0"
		//				}
		//			case int8, int16, int32, int64:
		//				insertValues[i] = fmt.Sprintf("%d", t)
		//			case float32, float64:
		//				insertValues[i] = fmt.Sprintf("%f", t)
		//			case []byte:
		//				insertValues[i] = string(t)
		//			default:
		//				core.Logger.Debug("getEnumTable", "case", "default", "t", t)
		//				insertValues[i] = fmt.Sprintf("%v", t)
		//			}
		//		}

		//		result.Values = append(result.Values, enumRow{id, realName, insertValues, realName})
		//	}

		//	return result
		//}

		public override EnumTable LoadEnumTable(string tableName)
		{
			EnumTable result = new EnumTable();
			result.Values = new List<EnumRow>();

			using (IDataReader rdr = GetReader("SELECT * FROM " + tableName))
			{
				result.Columns = new List<string>();
				for (var i = 0; i < rdr.FieldCount; i++)
					result.Columns.Add(rdr.GetName(i));

				result.IDName = result.Columns[0];
				result.ValueName = result.Columns[1];
				result.EnumName = result.IDName;
				if (result.EnumName.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase))
					result.EnumName = result.EnumName.Substring(0, result.EnumName.Length - 2);

				string baseClassName;
				Type fType = rdr.GetFieldType(0);
				if (fType == typeof(int))
					baseClassName = "int";
				else if (fType == typeof(short))
					baseClassName = "short";
				else if (fType == typeof(long))
					baseClassName = "long";
				else
					throw new NotSupportedException("Enum type must be int, short or long");
				result.BaseClassName = baseClassName;

				while (rdr.Read())
				{
					var item = new EnumRow();
					item.ID = rdr.GetValue(0);
					item.RealName = rdr.GetString(1);
					item.Name = item.RealName;

					//@TODO: get insertValues
					var insertValues = new List<string>();
					item.InsertValues = insertValues;

					result.Values.Add(item);
				}
			}

			return result;
		}

		public override MetaDataTable LoadMetaDataTable(string tableName)
		{
			var result = new MetaDataTable();
			result.Values = new List<MetaDataRow>();

			using (IDataReader rdr = GetReader("SELECT * FROM " + tableName))
			{
				result.Columns = new List<string>();
				for (var i = 0; i < rdr.FieldCount; i++)
				{
					var c = rdr.GetName(i);
					if (c == "DEX_ROW_TS" || c == "DEX_ROW_ID")
						continue;
					result.Columns.Add(c);
				}

				result.IDName = result.Columns[0];
				result.ValueName = result.Columns[1];
				result.MetaDataName = result.IDName;
				if (result.MetaDataName.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase))
					result.MetaDataName = result.MetaDataName.Substring(0, result.MetaDataName.Length - 2);

				while (rdr.Read())
				{
					var item = new MetaDataRow();
					item.ID = rdr.GetValue(0);
					item.Name = TransformMetaDataName(rdr.GetString(1));

					//@TODO: get insertValues
					var insertValues = new List<string>();
					item.InsertValues = insertValues;

					result.Values.Add(item);
				}
			}
			return result;
		}
		public string TransformMetaDataName(string name)
		{
			if (string.IsNullOrEmpty(name))
				return string.Empty;
			name = Utility.StripChars(name, "',-", "_");
			name = Utility.StripChars(name, " .()", "");
			name += "ID";
			return name;
		}

		private string GetDataTypeText(string dataType, int maxLength, bool isNullable)
		{
			string notNullableText = isNullable ? "" : " NOT NULL";
			string maxLengthText = (maxLength == 0) ? "" : "(" + maxLength + ")";

			return string.Format("{0}{1}{2}", dataType, maxLengthText, notNullableText);
		}

		public override ForeignKeyRowCollection ForeignKeyRows()
		{
			ForeignKeyRowCollection _foreignKeyRows = new ForeignKeyRowCollection();

			#region Sql
			string sql =
@"SELECT
	FK.TABLE_NAME AS ThisTable
	, KCU.COLUMN_NAME AS ThisColumn
	
	, PK.TABLE_NAME AS OtherTable
	, PT.COLUMN_NAME AS OtherColumn
	
	, TC.CONSTRAINT_NAME AS ConstraintName
	, FK.TABLE_SCHEMA AS [Owner]
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS TC
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS FK
ON
	TC.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS PK
ON
	TC.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
ON
	TC.CONSTRAINT_NAME = KCU.CONSTRAINT_NAME
INNER JOIN
(	
    SELECT
		TC.TABLE_NAME
		, KCU.COLUMN_NAME
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
	ON
		TC.CONSTRAINT_NAME = KCU.CONSTRAINT_NAME
    WHERE
		TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
) AS PT
ON
	PK.TABLE_NAME = PT.TABLE_NAME
ORDER BY
	ConstraintName";
			#endregion //Sql

			using (IDataReader rdr = GetReader(sql))
			{
				while (rdr.Read())
				{
					var row = new ForeignKeyRow();

					row.ThisTable = (string)rdr["ThisTable"];
					row.ThisColumn = (string)rdr["ThisColumn"];

					row.OtherTable = (string)rdr["OtherTable"];
					row.OtherColumn = (string)rdr["OtherColumn"];

					row.ConstraintName = (string)rdr["ConstraintName"];
					row.Owner = (string)rdr["Owner"];

					_foreignKeyRows.Add(row);
				}
			}
			return _foreignKeyRows;
		}
	}
}
