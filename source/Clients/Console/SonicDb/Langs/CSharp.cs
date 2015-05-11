using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb.Langs
{
	public class CSharp : ILang
	{
		public string Name()
		{
			return "csharp";
		}
		public string FileExt()
		{
			return ".cs";
		}
		public string GetSysType(string sqlType)
		{
			var sysType = "string";
			switch (sqlType)
			{
				case "bigint":
					sysType = "long";
					break;
				case "smallint":
					sysType = "short";
					break;
				case "int":
					sysType = "int";
					break;
				case "uniqueidentifier":
					sysType = "Guid";
					break;
				case "smalldatetime":
				case "datetime":
				case "date":
					sysType = "DateTime";
					break;
				case "float":
					sysType = "double";
					break;
				case "real":
				case "numeric":
				case "smallmoney":
				case "decimal":
				case "money":
					sysType = "decimal";
					break;
				case "tinyint":
					sysType = "byte";
					break;
				case "bit":
					sysType = "bool";
					break;
				case "image":
				case "binary":
				case "varbinary":
					sysType = "byte[]";
					break;
			}
			return sysType;
		}
		public string GetSysTypeFull(string sqlType, bool isNullable)
		{
			var sysType = this.GetSysType(sqlType);
			if (isNullable && sysType != "byte[]" && sysType != "string")
			{
				sysType += "?";
			}
			return sysType;
		}
		public string FormatArgs(SP sp)
		{
			var list = new List<string>(sp.Parameters.Count);
			foreach (var par in sp.Parameters)
			{
				var sysTypeFull = par.SysType;
				if (Utility.IsNullableDbType(par.DbType))
				{
					sysTypeFull += "?";
				}
				list.Add(sysTypeFull + " " + par.CleanName);
			}
			return string.Join(",", list);
		}
	}
}
