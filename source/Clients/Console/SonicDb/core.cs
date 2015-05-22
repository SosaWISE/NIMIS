using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDb
{
	public static class core
	{
		public static Loggit Logger = new Loggit();
	}
	public enum LogLevel
	{
		INFO,
		DBUG,
		WARN,
		EROR,
		CRIT,
	}
	public class Loggit
	{
		LogLevel _logLevel = LogLevel.INFO;
		internal void SetLogLvl(string loglvl)
		{
			var logLevel = _logLevel;
			switch (loglvl.ToUpper())
			{
				case "INFO":
					logLevel = LogLevel.INFO;
					break;
				case "DEBUG":
				case "DBUG":
					logLevel = LogLevel.DBUG;
					break;
				case "WARN":
					logLevel = LogLevel.WARN;
					break;
				case "ERROR":
				case "EROR":
					logLevel = LogLevel.EROR;
					break;
				case "CRIT":
					logLevel = LogLevel.CRIT;
					break;
			}
			_logLevel = logLevel;
		}

		internal void Info(string p, params object[] args)
		{
			Write(LogLevel.INFO, ConsoleColor.Green, p, args);
		}
		internal void Debug(string p, params object[] args)
		{
			Write(LogLevel.DBUG, ConsoleColor.Cyan, p, args);
		}
		internal void Warn(string p, params object[] args)
		{
			Write(LogLevel.WARN, ConsoleColor.Yellow, p, args);
		}
		internal void Error(string p, params object[] args)
		{
			Write(LogLevel.EROR, ConsoleColor.Red, p, args);
		}
		internal void Crit(string p, params object[] args)
		{
			Write(LogLevel.CRIT, ConsoleColor.Magenta, p, args);
		}

		private void Write(LogLevel level, ConsoleColor color, string p, params object[] args)
		{
			if (level < _logLevel)
				return;

			var prevColor = Console.ForegroundColor;

			Console.ForegroundColor = color;
			Console.Write(level);
			Console.ForegroundColor = prevColor;

			//[05-05|12:50:11]
			var now = DateTime.Now;
			Console.Write("[");
			Console.Write(now.ToString("MM-dd"));
			Console.Write("|");
			Console.Write(now.ToString("hh:mm:ss"));
			Console.Write("]");

			Console.Write(" ");
			Console.Write(p);
			var remaining = 40 - p.Length;
			if (args.Length > 0)
				while (remaining-- > 0) Console.Write(" ");

			var num = args.Length;
			for (var i = 0; i < num; i += 2)
			{
				Console.Write(" ");
				Console.ForegroundColor = color;
				Console.Write(args[i]);
				Console.ForegroundColor = prevColor;
				Console.Write("=");
				Console.Write(args[i + 1]);
			}

			Console.WriteLine();
		}
	}

	public static class fmt
	{
		public static void Printf(string p, params object[] args)
		{
			Console.Write(p, args);
		}
		public static string Sprintf(string p, params object[] args)
		{
			return string.Format(p, args);
		}
	}
	public static class strings
	{
		public static string[] Split(string str, string seperator)
		{
			return str.Split(new string[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
