using SonicDb.Langs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb
{
	public class EnumTable
	{
		public string IDName;
		public string ValueName;
		public string EnumName;
		public string BaseClassName;

		public List<string> Columns;
		public List<EnumRow> Values;
	}
	public class EnumRow
	{
		public object ID;
		public string RealName;

		public List<string> InsertValues;

		public string Name;
	}

	public class MetaDataTable
	{
		public string IDName;
		public string ValueName;
		public string MetaDataName;
		public string BaseClassName;

		public List<string> Columns;
		public List<MetaDataRow> Values;
	}
	public class MetaDataRow
	{
		public object ID;
		public string Name;

		public List<string> InsertValues;
	}

	public class Table
	{
		public List<Column> Columns;
		public List<FKTable> FKTables;
		public List<FKTable> SingleFKTables;
		public List<FKTable> QueryableFKTables;
		public string Name;
		public string CleanName;
		public string ClassName;
		public string PrimaryKey;
		public string Schema;
		public string QueryableName;
		public string Alias;
		public EnumTable Enum;
		public MetaDataTable MetaData;

		public bool HasLogicalDelete()
		{
			return this.Columns.Any(x => x.Name.ToLower() == "deleted" || x.Name.ToLower() == "isdeleted");
		}
		public Column DeleteColumn
		{
			get
			{
				Column result = null;
				if (this.Columns.Any(x => x.Name.ToLower() == "deleted"))
					result = this.Columns.Single(x => x.Name.ToLower() == "deleted");
				if (this.Columns.Any(x => x.Name.ToLower() == "isdeleted"))
					result = this.Columns.Single(x => x.Name.ToLower() == "isdeleted");
				return result;
			}
		}
		public Column PK { get; set; }
		//{
		//	get
		//	{
		//		return this.Columns.SingleOrDefault(x => x.IsPK) ?? this.Columns[0];
		//	}
		//}
		public Column Descriptor
		{
			get
			{
				Column result = null;
				if (this.Columns.Count > 0)
				{

					if ((this.Columns.Count > 1) && (this.Columns[1].SysType.ToLower().Trim() == "string"))
					{
						//use second column if it's text
						result = this.Columns[1];
					}
					else
					{
						//else use ID, which should be the first column
						result = this.Columns[0];
					}
				}
				return result;
			}
		}

		string transformColumnName(ILang lang, string name, Generator gen)
		{
			name = gen.TransformName(lang, name);
			if (lang.Name() == "golang")
			{
				var ch = name.Substring(0, 1);
				if (ch.ToLower() == ch)
				{
					name = ch.ToUpper() + name.Substring(1);
				}
			}
			return name;
		}
		public void LangChanged(ILang lang, string alias, Generator gen)
		{
			this.CleanName = gen.TransformName(lang, this.Name);
			this.ClassName = Inflector.MakeSingular(this.CleanName);
			this.QueryableName = Inflector.MakePlural(this.ClassName);
			this.Alias = alias;

			foreach (var col in this.Columns)
			{
				col.CleanName = transformColumnName(lang, col.Name, gen);
				col.SysType = lang.GetSysType(col.DataType);
				col.SysTypeFull = lang.GetSysTypeFull(col.DataType, col.IsNullable);
			}

			foreach (var fk in this.SingleFKTables)
			{
				fk.OtherClass = Inflector.MakeSingular(gen.TransformName(lang, fk.OtherTable));
				fk.OtherQueryable = Inflector.MakePlural(Inflector.MakeSingular(gen.TransformName(lang, fk.ThisTable)));
				if (string.Equals(fk.ThisTable, fk.OtherTable, StringComparison.InvariantCultureIgnoreCase))
				{
					fk.OtherQueryable = "Child" + fk.OtherQueryable;
				}
			}

			var propNameCount = new Dictionary<string, int>();
			foreach (var fk in this.QueryableFKTables)
			{
				fk.OtherClass = Inflector.MakeSingular(gen.TransformName(lang, fk.OtherTable));
				fk.OtherQueryable = Inflector.MakePlural(fk.OtherClass);

				fk.PropName = fk.OtherQueryable;
				////remove ID or add Values
				//if (fk.ThisColumn.EndsWith("ID", StringComparison.InvariantCultureIgnoreCase)) {
				// fk.PropName = inflector.MakePlural(fk.ThisColumn.Substring(0, fk.ThisColumn.Length - 2));
				//}
				//else {
				// fk.PropName = fk.ThisColumn + "Values";
				//}

				if (string.Equals(fk.ThisTable, fk.OtherTable, StringComparison.InvariantCultureIgnoreCase))
					fk.PropName = "Child" + fk.PropName;

				//don't allow duplicate prop names
				if (!propNameCount.ContainsKey(fk.PropName))
					propNameCount.Add(fk.PropName, 0);
				var count = propNameCount[fk.PropName] + 1;
				propNameCount[fk.PropName] = count;
				if (count > 1)
					fk.PropName += fmt.Sprintf("{0:D2}", count);
			}

			foreach (var fk in this.FKTables)
			{
				fk.OtherClass = Inflector.MakeSingular(gen.TransformName(lang, fk.OtherTable));
				fk.OtherQueryable = Inflector.MakePlural(fk.OtherClass);

				fk.PropName = fk.OtherQueryable;
			}

			if (this.Enum != null)
			{
				foreach (var e in this.Enum.Values)
					e.Name = TransformEnumName(lang, e.RealName);
			}
		}

		private string TransformEnumName(ILang lang, string name)
		{
			if (name != "")
			{
				name = Inflector.ToPascalCase(name, false);
				if (Utility.IsStringNumeric(name))
					name = "_" + name;
				name = Utility.StripNonAlphaNumeric(name);
				name = name.Trim();
				name = Utility.KeyWordCheck(lang.Name(), name, "");
			}
			return name;
		}
	}

	public class Column
	{
		//public string Name;
		//public string CleanName;
		//public string SysType;
		//public string DataType;
		//public DbType DbType;
		//public bool AutoIncrement;
		//public bool IsPK;
		//public int MaxLength;
		//public bool IsNullable;
		//public bool IsForeignKey;
		//public bool IsComputed;
		//public string ColumnAttributeText;
		//
		//public bool IsReadOnly;
		//public string DefaultSetting;
		//public string ForeignKeyTableName;

		public string Schema;
		public string TableName;
		public int OrdinalPosition;
		public string Name;
		public string CollationName;
		public bool IsComputed;
		public string ComputedDefinition;
		public string DataType;
		public bool IsIdentity;
		public long? SeedValue;
		public long? IncrementValue;
		public long? MaxLength;
		public long? DatePrecision;
		public long? NumericPrecision;
		public long? NumericScale;
		public bool IsNullable;
		public string DefaultConstraintName;
		public string DefaultValue;

		public bool IsPK;

		public DbType DbType;
		public bool IsForeignKey;
		public string ForeignKeyTableName;

		public string CleanName;
		public string SysType;
		public string SysTypeFull;

		public bool IsID
		{
			get { return CleanName == "ID"; }
		}
	}
	public class FKTable
	{
		public string ThisTable;
		public string ThisColumn;
		public string OtherTable;
		public string OtherColumn;
		public string OtherClass;
		public string OtherQueryable;
		public string PropName;
		public string ConstraintName;
	}

	public class ForeignKeyRowCollection : List<ForeignKeyRow>
	{
	}
	public class ForeignKeyRow
	{
		public string ThisTable { get; set; }
		public string ThisColumn { get; set; }

		public string OtherTable { get; set; }
		public string OtherColumn { get; set; }

		public string ConstraintName { get; set; }
		public string Owner { get; set; }
	}

	public class SP
	{
		public string Name;
		public string CleanName;
		public string ClassName;
		//
		public List<SPParam> Parameters;
		public string Args;

		//public SP()
		//{
		//	Parameters = new List<SPParam>();
		//}
		//public string ArgList
		//{
		//	get
		//	{
		//		StringBuilder sb = new StringBuilder();
		//		int loopCount = 1;
		//		foreach (var par in Parameters)
		//		{
		//			sb.AppendFormat("{0} {1}", par.SysType, par.CleanName);
		//			if (loopCount < Parameters.Count)
		//				sb.Append(",");
		//			loopCount++;
		//		}
		//		return sb.ToString();
		//	}
		//}
		public bool LangChanged(ILang lang, List<DbSPParam> allSpParams, Generator gen)
		{
			bool importsTime;

			this.CleanName = gen.TransformName(lang, this.Name);
			this.Parameters = GetSPParams(lang, allSpParams, this.Name, out importsTime);
			this.Args = lang.FormatArgs(this);

			return importsTime;
		}
		private List<SPParam> GetSPParams(ILang lang, List<DbSPParam> allSpParams, string spName, out bool importsTime)
		{
			var results = new List<SPParam>();
			importsTime = false;
			// filter
			foreach (var p in allSpParams)
			{
				if (p.SPName == spName)
				{
					var param = createSPParam(lang, p);
					if (param.SysType.StartsWith("time."))
					{
						importsTime = true;
					}
					results.Add(param);
				}
			}
			return results;
		}

		private SPParam createSPParam(ILang lang, DbSPParam p)
		{
			var sqlType = p.DataType;
			var parameter = new SPParam();
			parameter.SysType = lang.GetSysType(sqlType);
			parameter.DbType = Utility.GetDbType(sqlType);
			parameter.Name = p.Name;
			parameter.CleanName = p.CleanName;
			parameter.InputOutput = p.Mode == ParameterDirection.InputOutput;

			// precision := p.NumericPrecision;
			// if (precision != null && precision != DBNull.Value) {
			//     parameter.Precision = Convert.ToInt32(precision);
			// }
			// object objScale = p.NumericScale;
			// if (objScale != null && objScale != DBNull.Value) {
			//     parameter.Scale = Convert.ToInt32(objScale);
			// }

			return parameter;
		}
	}
	public class DbSPParam
	{
		public string Name { get; set; }
		public string CleanName { get; set; }
		public string DataType { get; set; }
		public string SPSchema { get; set; }
		public string SPName { get; set; }
		//
		public int OrdinalPosition { get; set; }
		public string ParamType { get; set; }
		public string IsResult { get; set; }
		public long? DataLength { get; set; }
		public ParameterDirection Mode { get; set; }
		public long? NumericPrecision { get; set; }
		public long? NumericScale { get; set; }
	}
	public class SPParam
	{
		public string Name;
		public string CleanName;
		public string SysType;
		public DbType DbType;
		public ParameterDirection Mode;
		public int Precision;
		public int Scale;
		public bool InputOutput;


		//public Column GetMatchingColumn(Table tbl, params string[] columns)
		//{
		//	Column result = null;
		//	foreach (string columnName in columns) {
		//		result = tbl.Columns.SingleOrDefault(x => String.Equals(x.Name, columnName, StringComparison.InvariantCultureIgnoreCase));
		//		if (result != null) {
		//			break;
		//		}
		//	}
		//	return result;
		//}
	}
}
