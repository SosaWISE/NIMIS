using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SubsonicReverse
{
	class Program
	{
		static void Main(string[] args)
		{
			var t = typeof(SubsonicReverseMe.AssemblyHandle);
			FromSubsonicClasses(Assembly.GetAssembly(t));
		}
		static void FromSubsonicClasses(Assembly assembly)
		{
			SubSonic.DataService.LoadProviders();

			StringBuilder sob = new StringBuilder();
			StringBuilder dropSob = new StringBuilder();

			var arTypeList = new List<Type>();
			var arType = typeof(SubSonic.ActiveRecord<>);
			foreach (var t in assembly.ExportedTypes)
			{
				var bType = t.BaseType;
				if (bType != null && bType.IsGenericType && bType.GetGenericTypeDefinition() == arType)
					arTypeList.Add(t);
			}

			var fks = new List<ForeignKeyRow>();
			var tablePkNameDict = new Dictionary<string, string>();
			foreach (var t in arTypeList)
			{
				//SubSonic.TableSchema.Table tschema = Bcr.Data.Main.AD_AdvertiserEvent.Schema;
				var tschemaProp = t.GetProperty("Schema", BindingFlags.Public | BindingFlags.Static);
				if (tschemaProp == null)
					continue;
				var tschema = (SubSonic.TableSchema.Table)tschemaProp.GetValue(null, null);
				SubSonic.TableSchema.TableColumn pkColumn;
				WriteTableScript(sob, tschema, fks, out pkColumn);
				tablePkNameDict.Add(tschema.TableName, (pkColumn != null) ? pkColumn.ColumnName : null);
			}

			sob.Append("\n\n\n\n");

			//ALTER TABLE [dbo].[MC_Accounts] ADD CONSTRAINT [FK_MC_Accounts_AE_Customers]
			//	FOREIGN KEY([ShipContactId]) REFERENCES [dbo].[AE_Customers] ([CustomerID])
			//	ON UPDATE NO ACTION ON DELETE NO ACTION
			foreach (var fk in fks)
			{
				var pkName = tablePkNameDict[fk.OtherTable];
				var constraintName = string.Format("FK_{0}_{1}_{2}_{3}", fk.OtherTable, pkName, fk.ThisTable, fk.ThisColumn);
				sob.AppendFormat("ALTER TABLE [dbo].[{2}] ADD CONSTRAINT [{4}] FOREIGN KEY([{3}]) REFERENCES [dbo].[{0}] ([{1}])\n",
					fk.OtherTable, pkName, fk.ThisTable, fk.ThisColumn, constraintName);

				dropSob.AppendFormat("ALTER TABLE [dbo].[{0}] DROP [{1}]\n", fk.ThisTable, constraintName);
			}

			sob.Append("\n\n\n\n");
			dropSob.Append("\n\n\n\n");

			foreach (var tableName in tablePkNameDict.Keys)
			{
				dropSob.AppendFormat("DROP TABLE [dbo].[{0}]\n", tableName);
			}

			dropSob.Append("\n\n\n\n");


			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../schema.up.sql");
			File.WriteAllText(path, sob.ToString());

			path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../schema.down.sql");
			File.WriteAllText(path, dropSob.ToString());
		}
		static void WriteTableScript(StringBuilder sob, SubSonic.TableSchema.Table tschema, List<ForeignKeyRow> fks, out SubSonic.TableSchema.TableColumn pkColumn)
		{
			pkColumn = null;
			//CREATE TABLE [dbo].[AE_BankAccountTypes] (
			sob.AppendFormat("CREATE TABLE [{0}].[{1}] (\n", tschema.SchemaName, tschema.TableName);
			bool isFirst = true;
			foreach (var c in tschema.Columns)
			{
				//
				if (c.IsForeignKey)
				{
					fks.Add(new ForeignKeyRow()
					{
						ThisTable = tschema.TableName,
						ThisColumn = c.ColumnName,
						OtherTable = c.ForeignKeyTableName,
						//OtherColumn = null,
					});
				}


				//
				if (c.IsPrimaryKey)
				{
					if (pkColumn != null)
					{
						int ttt = 0;
					}
					pkColumn = c;
				}

				sob.Append("  "); // indent
				if (isFirst)
					isFirst = false;
				else
					sob.Append(", ");
				//	[BankAccountTypeID] [INT] IDENTITY(1,1) NOT NULL
				sob.Append("[");
				sob.Append(c.ColumnName);
				sob.Append("] ");
				sob.Append(GetSqlType(c.DataType, c.MaxLength));
				if (c.AutoIncrement)
					sob.Append(" IDENTITY(1,1)");
				if (!c.IsNullable)
					sob.Append(" NOT");
				sob.Append(" NULL");
				//if (!string.IsNullOrEmpty(c.DefaultSetting))
				//	sob.AppendFormat(" CONSTRAINT DEFAULT({0})", c.DefaultSetting);
				sob.Append("\n");
			}
			if (pkColumn != null)
			{
				//  , CONSTRAINT [PK_AE_BankAccountTypes] PRIMARY KEY CLUSTERED([BankAccountTypeID])
				sob.AppendFormat("  , CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED([{1}])\n", tschema.TableName, pkColumn.ColumnName);
			}
			sob.Append(");\n\n");

			//	[BankAccountTypeID] [INT] IDENTITY(1,1) NOT NULL
			//  , [BankAccountType] [NVARCHAR](50) NOT NULL
			//  , CONSTRAINT [PK_AE_BankAccountTypes] PRIMARY KEY CLUSTERED([BankAccountTypeID])
			//);
		}
		public static string GetSqlType(DbType dbType, int maxLength)
		{
			string max;
			switch (dbType)
			{
				default:
					max = (maxLength < 0 || maxLength > 8000) ? "max" : maxLength.ToString();
					return string.Format("varchar({0})", max);
				case DbType.AnsiString:
					max = (maxLength < 0 || maxLength > 8000) ? "max" : maxLength.ToString();
					return string.Format("varchar({0})", max);
				case DbType.String:
					max = (maxLength < 0 || maxLength > 4000) ? "max" : maxLength.ToString();
					return string.Format("nvarchar({0})", max);
				case DbType.Int32:
					return "int";
				case DbType.Guid:
					return "uniqueidentifier";
				case DbType.Date:
					return "date";
				case DbType.DateTime:
					return "datetime";
				case DbType.Time:
					return "time(7)";
				case DbType.Int64:
					return "bigint";
				case DbType.Binary:
					max = (maxLength < 0 || maxLength > 8000) ? "max" : maxLength.ToString();
					return string.Format("nvarchar({0})", max);
				case DbType.Boolean:
					return "bit";
				case DbType.AnsiStringFixedLength:
					return string.Format("char({0})", maxLength);
				case DbType.Decimal:
					return "decimal";
				case DbType.Double:
					return "float";
				case DbType.Currency:
					return "money";
				case DbType.Single:
					return "real";
				case DbType.Int16:
					return "smallint";
				case DbType.Byte:
					return "tinyint";
			}
		}
	}
}
