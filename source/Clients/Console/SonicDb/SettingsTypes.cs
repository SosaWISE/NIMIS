using System.Collections.Generic;

namespace SonicDb
{
	public class ServerConnection
	{
		public string DbType { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string OutputPath { get; set; }
	}

	public class DbConfig
	{
		// Connection ServerConnection
		public Dictionary<string, ServerConnection> Connections { get; set; }

		public List<TopDbSettings> Databases { get; set; }

		public bool LogSql { get; set; }
	}
	public class GenDbSettingsCreds : Dictionary<string, ServerConnectionCred> { }
	public class ServerConnectionCred
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}

	public class TopDbSettings : DbSettings
	{
		public string Database { get; set; }
		public string OutputDir { get; set; }
	}
	public class DbSettings
	{
		public string DatabaseName { get; set; }
		public string Namespace { get; set; }
		public string ProviderName { get; set; }

		public string[] IncludeTableList { get; set; }
		public string[] EnumTableList { get; set; }
		public string[] MetaDataTableList { get; set; }
	}
	public class GenControllerContext
	{
		public string DatabaseName { get; set; }
		public Table Table { get; set; }
	}

	public class DbModule
	{
		public int ObjectID { get; set; }
		public string SchemaName { get; set; }
		public string ModuleName { get; set; }
		public string TypeDesc { get; set; }
		public string Definition { get; set; }
		public bool UsesAnsiNulls { get; set; }
		public bool UsesQuotedIdentifier { get; set; }
		public string TableName { get; set; } // used only for triggers
		public string ModuleType { get; set; }

	}

	public class GeneratorContext
	{
		public HashSet<string> EnumTables { get; private set; }
		public HashSet<string> MetaDataTables { get; private set; }

		public string RegexReplaceExpression = "";
		public string RegexMatchExpression = "";
		public string RegexDictionaryReplace = @"^vw_(.*),$1View;^vw([A-Z]{2})_(.*),$1\_$2View;vw(.*),$1View;^VW_(.*),$1View;^cust([A-Z][A-Z][A-Z]?)_,$1\_;^proc_([A-Z][A-Z][A-Z]?)_,$1\_;^([A-Z][A-Z][A-Z]?)_,$1\_";
		public bool RegexIgnoreCase = false;

		public string StripText = "^cust_|^cust";

		public string[] IncludeProcedures = strings.Split("^cust*,^wiseSP_*", ",");

		public string[] ExcludeProcedures = strings.Split("^custFx*", ",");

		public string[] IncludeTables { get; private set; }

		public string[] ExcludeTables = strings.Split("sysdiagrams,dtproperty,MSreplication_objects,MSreplication_subscriptions" +
				",MSsavedforeignkeycolumns,MSsavedforeignkeyextendedproperties,MSsavedforeignkeys" +
				",MSsnapshotdeliveryprogress,MSsubscription_agents,MSsubscription_properties,^temp$", ",");

		public string[] IncludeFunctions = strings.Split("*", ","); //"^fx[A-Z]*,^fn_[A-Z]*"

		public string[] ExcludeFunctions = strings.Split("fn_diagramobjects", ",");

		public GeneratorContext(TopDbSettings topDbSettings, DbSettings settings = null)
		{
			Database = topDbSettings.Database;
			settings = settings ?? topDbSettings;
			DatabaseName = settings.DatabaseName ?? settings.DatabaseName ?? "DBase";
			Namespace = settings.Namespace ?? settings.Namespace;
			ProviderName = settings.ProviderName ?? settings.ProviderName;

			var includeTableList = settings.IncludeTableList ?? topDbSettings.IncludeTableList;
			IncludeTables = (includeTableList == null || includeTableList.Length == 0) ? new string[] { "*" } : includeTableList;

			EnumTables = new HashSet<string>(settings.EnumTableList ?? topDbSettings.EnumTableList);
			MetaDataTables = new HashSet<string>(settings.MetaDataTableList ?? topDbSettings.MetaDataTableList);
		}

		public string Database { get; set; }
		public string DatabaseName { get; set; }
		public string Namespace { get; set; }
		public string ProviderName { get; set; }

		DbModule[][] ModuleLevels { get; set; }

		public bool SprocsImportTime { get; set; }
		public List<SP> Sprocs { get; set; }
		public List<Table> Tables { get; set; }
		public List<Table> Views { get; set; }
	}

}