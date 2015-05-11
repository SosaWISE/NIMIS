using Antlr4.StringTemplate;
using Newtonsoft.Json;
using SonicDb.Langs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb
{
	public abstract class Generator
	{
		protected string ConnectionString;

		System.Reflection.Assembly _assembly;
		TopDbSettings _topSettings;
		private GeneratorContext Context { get; set; }

		public Generator(TopDbSettings topSettings)
		{
			_assembly = System.Reflection.Assembly.GetExecutingAssembly();
			_topSettings = topSettings;
		}

		public void GenerateFiles(string outputPath, string[] outputLangs)
		{
			var startTime = DateTime.Now;
			core.Logger.Info("start - " + _topSettings.Database);

			//var deps = DbModuleDependencys();

			// load from db
			//var allSpParams []dbSPParam
			//data.Sprocs, allSpParams = GetSPs();
			List<DbSPParam> allSpParams = GetAllSPParams();
			var allSprocs = GetSPs();
			var tableColumnMap = TableColumnMap();
			//var tableIndexes = TableIndexes();
			//checkConstraints = TableCheckConstraints();
			var fkRows = ForeignKeyRows();
			var allTables = LoadTables(_topSettings.Database, tableColumnMap, fkRows);//, tableIndexes, checkConstraints);
			var allViews = LoadViews(_topSettings.Database, tableColumnMap, fkRows);
			//data.ModuleLevels = DbModuleLevels(deps);

			core.Logger.Info("Sql Timer - " + _topSettings.Database, "duration", DateTime.Now.Subtract(startTime));

			var rNames = _assembly.GetManifestResourceNames();

			foreach (var langName in outputLangs)
			{
				var genStart = DateTime.Now;

				var lang = GetLanguage(langName);
				// ensure output directories exists
				var pathTmpl = new Template(outputPath, '<', '>');
				pathTmpl.Add("lang", langName);
				pathTmpl.Add("database", _topSettings.Database);
				var outdir = Path.GetFullPath(pathTmpl.Render());
				Directory.CreateDirectory(outdir);

				// attempt to load dbsettings
				DbSettings settings = null;
				var settingsPath = Path.Combine(outdir, "./dbsettings.json");
				if (File.Exists(settingsPath))
				{
					core.Logger.Debug(_topSettings.Database, "action", "load directory db settings", "path", settingsPath);
					settings = JsonConvert.DeserializeObject<DbSettings>(File.ReadAllText(settingsPath));
				}
				else
					core.Logger.Debug(_topSettings.Database, "action", "using top db settings");
				this.Context = new GeneratorContext(_topSettings, settings);

				this.Context.SprocsImportTime = false;
				this.Context.Sprocs = new List<SP>();
				foreach (var sp in allSprocs)
				{
					if (ShouldGenerateSproc(sp.Name))
					{
						if (sp.LangChanged(lang, allSpParams, this))
							this.Context.SprocsImportTime = true;
						this.Context.Sprocs.Add(sp);
					}
				}

				var aliasNameCount = new Dictionary<string, int>();
				this.Context.Tables = new List<Table>();
				foreach (var tbl in allTables)
				{
					if (ShouldGenerateTable(tbl.Name))
					{
						tbl.LangChanged(lang, TableAlias(tbl.Name, aliasNameCount), this);
						this.Context.Tables.Add(tbl);

						if (this.Context.EnumTables.Contains(tbl.Name))
							tbl.Enum = LoadEnumTable(tbl.Name);
						else
							tbl.Enum = null;
						if (this.Context.MetaDataTables.Contains(tbl.Name))
							tbl.MetaData = LoadMetaDataTable(tbl.Name);
						else
							tbl.MetaData = null;
					}
					//else core.Logger.Info("Do not generate", "table", tbl.Name);
				}
				this.Context.Views = new List<Table>();
				foreach (var tbl in allViews)
				{
					if (ShouldGenerateTable(tbl.Name))
					{
						tbl.LangChanged(lang, TableAlias(tbl.Name, aliasNameCount), this);
						this.Context.Views.Add(tbl);
					}
					//else core.Logger.Info("Do not generate", "view", tbl.Name);
				}

				var prefix = string.Format("SonicDb.Templates.{0}.", langName);
				foreach (var resourceName in rNames)
				{
					if (!resourceName.StartsWith(prefix))
						continue;
					var t = new Template(ReadResourceFile(resourceName), '<', '>');
					t.Add("Model", this.Context);
					var txt = t.Render();
					File.WriteAllText(Path.Combine(outdir, Path.ChangeExtension(resourceName.Substring(prefix.Length), lang.FileExt())), txt);
				}

				core.Logger.Info("Gen Timer - " + _topSettings.Database, "outdir", outdir, "lang", langName, "duration", DateTime.Now.Subtract(genStart));
			}
			core.Logger.Info("Total Timer - " + _topSettings.Database, "duration", DateTime.Now.Subtract(startTime));
		}

		string ReadResourceFile(string resourceName)
		{
			using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}

		public static DbConfig LoadDbConfig()
		{
			var path = Path.GetFullPath("./dbconfig.json");
			var credsPath = Path.GetFullPath("./dbconfig-creds.json");
			core.Logger.Debug("load config", "path", path, "creds", credsPath);
			var config = JsonConvert.DeserializeObject<DbConfig>(File.ReadAllText(path));
			GenDbSettingsCreds creds = null;
			if (File.Exists(credsPath))
			{
				creds = JsonConvert.DeserializeObject<GenDbSettingsCreds>(File.ReadAllText(credsPath));
			}
			foreach (var c in config.Connections)
			{
				var setting = c.Value;
				//
				ServerConnectionCred cred;
				if (creds != null && creds.TryGetValue(c.Key, out cred))
				{
					setting.Username = cred.Username;
					setting.Password = cred.Password;
				}
				//
				if (string.IsNullOrEmpty(setting.DbType))
					setting.DbType = "mssql";
			}
			foreach (var settings in config.Databases)
			{
				if (string.IsNullOrEmpty(settings.OutputDir))
					settings.OutputDir = "/<lang>/<database>";
			}
			return config;
		}
		public static DbSettings LoadDbSettings(string path)
		{
			path = Path.GetFullPath(path);
			if (File.Exists(path))
			{
				core.Logger.Debug("load settings", "path", path);
				return JsonConvert.DeserializeObject<DbSettings>(File.ReadAllText(path));
			}
			return null;
		}


		Dictionary<string, EnumTable> _enumTableDict = new Dictionary<string, EnumTable>();
		public EnumTable GetEnumTable(string tableName)
		{
			EnumTable result;
			if (!_enumTableDict.TryGetValue(tableName, out result))
			{
				result = LoadEnumTable(tableName);
				_enumTableDict.Add(tableName, result);
			}
			return result;
		}
		Dictionary<string, MetaDataTable> _metaDataTableDict = new Dictionary<string, MetaDataTable>();
		public MetaDataTable GetMetaDataTable(string tableName)
		{
			MetaDataTable result;
			if (!_metaDataTableDict.TryGetValue(tableName, out result))
			{
				result = LoadMetaDataTable(tableName);
				_metaDataTableDict.Add(tableName, result);
			}
			return result;
		}

		public abstract List<DbSPParam> GetAllSPParams();
		public abstract List<SP> GetSPs();
		public abstract Dictionary<string, List<Column>> TableColumnMap();
		public abstract ForeignKeyRowCollection ForeignKeyRows();
		public abstract List<Table> LoadTables(string database, bool isTables, Dictionary<string, List<Column>> tableColumnMap, ForeignKeyRowCollection fkRows);
		public abstract EnumTable LoadEnumTable(string tableName);
		public abstract MetaDataTable LoadMetaDataTable(string tableName);

		public List<Table> LoadTables(string database, Dictionary<string, List<Column>> tableColumnMap, ForeignKeyRowCollection fkRows)// ,TableIndexMap tableIndexMap , map[string][]CheckConstraint checkConstraints )  {
		{
			return LoadTables(database, true, tableColumnMap, fkRows);//, tableIndexMap, checkConstraints)
		}
		public List<Table> LoadViews(string database, Dictionary<string, List<Column>> tableColumnMap, ForeignKeyRowCollection fkRows)
		{
			return LoadTables(database, false, tableColumnMap, fkRows);//, null, null);
		}

		private static ILang GetLanguage(string lang)
		{
			switch (lang)
			{
				default:
					return new CSharp();
				//	return SqlLanguage{}
				//case "golang":
				//	return GoLanguage{}
				//case "csharp":
				//	return CsharpLanguage{}
				//case "dapper":
				//	return CsharpDapperLanguage{}
			}
		}

		public bool ShouldGenerateTable(string tableName)
		{
			return ShouldGenerate(tableName, this.Context.IncludeTables, this.Context.ExcludeTables);
		}
		public bool ShouldGenerateSproc(string spName)
		{
			return ShouldGenerate(spName, this.Context.IncludeProcedures, this.Context.ExcludeProcedures);
		}
		public static bool ShouldGenerate(string objectName, string[] includeList, string[] excludeList)
		{
			bool result = true;
			bool generateAll = false;

			// first, check to see if the includeList says to include all tables
			// this is the default
			if (includeList.Length == 1)
			{
				if (includeList[0] == "*")
					generateAll = true;
			}

			// if we need to generate all tables, then we need to check the excludeList
			if (generateAll)
			{
				foreach (string s in excludeList)
				{
					if (Utility.IsRegexMatch(objectName, s.Trim()))
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				// IncludeList TRUMPs excludeList
				//	in case of confusion what this means is that if there is an includeList,
				//	be definition there's an excludeList of all tables not included
				// yep, confusing.

				// this means that tables were specifically requested in the includeList
				// need to make them prove themselves
				result = false;

				foreach (string s in includeList)
				{
					if (Utility.IsRegexMatch(objectName, s.Trim()))
					{
						result = true;
						break;
					}
				}
			}

			return result;
		}

		public string TransformName(ILang lang, string name)
		{
			if (String.IsNullOrEmpty(name))
				return String.Empty;

			string newName = name;
			newName = Utility.RegexTransform(newName, this.Context.RegexIgnoreCase, this.Context.RegexMatchExpression, this.Context.RegexDictionaryReplace, this.Context.RegexReplaceExpression);
			newName = Utility.StripText(newName, this.Context.StripText);
			//newName = Inflector.ToPascalCase(newName, false);
			newName = Utility.IsStringNumeric(newName) ? "_" + newName : newName;
			newName = Utility.StripNonAlphaNumeric(newName);
			newName = newName.Trim();
			newName = Utility.KeyWordCheck(lang.Name(), newName, string.Empty);
			return newName;
		}


		private static string TableAlias(string name, Dictionary<string, int> aliasNameCount)
		{
			var prefix = "";
			var s = name;

			// remove `vw`
			if (s.StartsWith("vw"))
			{
				s = s.Substring(2);
			}

			// remove AB_ prefix
			var i = s.IndexOf("_");
			if (i >= 0)
			{
				// use prefix - first letter(s) upper, last letter lower
				prefix = s.Substring(0, i - 1).ToUpper() + s.Substring(i - 1, 1).ToLower();
				// remove prefix
				s = s.Substring(i + 1);
			}
			// ///////////
			// prefix = ""
			// ///////////

			var words = ToWords(s);

			for (var t = 1; t <= 3; t++)
			{
				var alias = prefix + MakeAcronymn(words, t);
				core.Logger.Info("tableAlias", "name", name, "alias", alias, "try", t);
				//don't allow duplicate aliases
				var count = GetAliasCount(aliasNameCount, alias) + 1;
				if (count == 1 && alias.Length > 1)
				{
					SetAliasCount(aliasNameCount, alias, count);
					return alias;
				}
			}

			{
				var shortAlias = prefix + MakeAcronymn(words, 1);
				for (var t = 1; t <= 10; t++)
				{
					//don't allow duplicate aliases
					var alias = shortAlias + fmt.Sprintf("{0:D2}", t);
					var count = GetAliasCount(aliasNameCount, alias) + 1;
					core.Logger.Info("tableAlias", "name", name, "alias", alias, "try", t, "mode", "fallback");
					if (count == 1 && alias.Length > 3)
					{
						SetAliasCount(aliasNameCount, alias, count);
						return alias;
					}
				}
			}

			return name;
		}
		private static int GetAliasCount(Dictionary<string, int> aliasNameCount, string alias)
		{
			int count;
			aliasNameCount.TryGetValue(alias, out count);
			return count;
		}
		private static void SetAliasCount(Dictionary<string, int> aliasNameCount, string alias, int count)
		{
			if (!aliasNameCount.ContainsKey(alias))
				aliasNameCount.Add(alias, count);
			else
				aliasNameCount[alias] = count;
		}
		private static List<string> ToWords(string s)
		{
			var words = new List<string>();
			for (var i = 0; s.Length > 0; s = s.Substring(i))
			{
				i = IndexOfUpper(s.Substring(1)) + 1;
				if (i <= 0)
				{
					i = s.Length;
				}
				if (i == 1)
				{
					// group uppercase letters not separated by lowercase letters
					i = IndexOfLower(s.Substring(1));
					if (i < 0)
					{
						i = s.Length;
					}
					words.Add(s.Substring(0, 1) + s.Substring(1, i).ToLower());
					continue;
				}
				else
				{
					words.Add(s.Substring(0, i));
				}
			}
			return words;
		}
		private static string MakeAcronymn(List<string> words, int length)
		{
			if (length < 1)
			{
				length = 1;
			}
			var abbreviations = new List<string>();
			foreach (var w in words)
			{
				var l = length;
				if (l > w.Length)
					l = w.Length;
				abbreviations.Add(w.Substring(0, l));
			}
			return string.Join("", abbreviations);
		}

		private static int IndexOfUpper(string str)
		{
			int index = 0;
			foreach (Char c in str)
			{
				if (Char.IsUpper(c)) return index;
				index++;
			}
			return -1;
		}
		private static int IndexOfLower(string str)
		{
			int index = 0;
			foreach (Char c in str)
			{
				if (Char.IsLower(c)) return index;
				index++;
			}
			return -1;
		}
	}
}
