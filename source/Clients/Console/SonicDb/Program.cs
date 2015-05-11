using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb
{
	class Program
	{
		static void Main(string[] args)
		{
			var startTime = DateTime.Now;

			core.Logger.Info("info", "p", "v");
			core.Logger.Debug("debug", "p", "v");
			core.Logger.Warn("warn", "p", "v");
			core.Logger.Error("error", "p", "v");
			core.Logger.Crit("crit", "p", "v");

			var argMan = new ArgMan(args);
			var connsPtr = argMan["-conns"] ?? "dev,";
			var langsPtr = argMan["-langs"] ?? "dapper,";
			var databasesPtr = argMan["-databases"] ?? "";
			var loglvlPtr = argMan["-loglvl"] ?? "dbug";
			core.Logger.SetLogLvl(loglvlPtr);
			core.Logger.Info("LogLevel", "loglvl", loglvlPtr);

			var dir = argMan["-dir"];
			if (dir != null)
			{
				core.Logger.Debug("Setting Current Directory", "new", dir, "old", Directory.GetCurrentDirectory());
				Directory.SetCurrentDirectory(dir);
			}

			var connsMap = new HashSet<string>(connsPtr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

			var outputLangs = langsPtr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (outputLangs.Length == 0)
			{
				core.Logger.Crit("No output langs");
				return;
			}

			var config = Generator.LoadDbConfig();

			var databasesMap = new HashSet<string>(databasesPtr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

			//
			// generate
			//
			foreach (var kvp in config.Connections)
			{
				var name = kvp.Key;
				var connection = kvp.Value;
				if (connsMap.Count > 0 && !connsMap.Contains(name))
				{
					core.Logger.Info("skipped connection: " + name);
					continue;
				}
				core.Logger.Info("Connection " + name, "DbType", connection.DbType, "Host", connection.Host, "Port", connection.Port, "Username", connection.Username);

				switch (connection.DbType)
				{
					//case "mysql":
					case "mssql":
						break;
					default:
						core.Logger.Info(connection.DbType + " DbType is not supported");
						continue;
				}

				//if (connection.Username != "" && connection.Password == "") {
				//	fmt.Printf("Password: ");
				//	connection.Password = string(gopass.GetPasswd()); // Silent, for *'s use gopass.GetPasswdMasked()
				//}

				foreach (var settings in config.Databases)
				{
					if (databasesMap.Count > 0 && !databasesMap.Contains(settings.Database))
					{
						core.Logger.Info("skip database", "name", settings.Database);
						continue;
					}

					Generator gen;
					switch (connection.DbType)
					{
						case "mssql":
							gen = new MsSqlGenerator(connection, settings);
							break;
						//case "mysql":
						//	dsn = Generate.MySqlDsn(connection, database.Database);
						default:
							return;
					}
					var outputPath = connection.OutputPath + settings.OutputDir;
					gen.GenerateFiles(outputPath, outputLangs);
				}

				// clear password after use
				connection.Password = "";
			}
			core.Logger.Debug("Success", "duration", DateTime.Now.Subtract(startTime));
		}
	}
}

