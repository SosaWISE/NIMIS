using System;

namespace NXS.Data
{
	public static class DatabaseHelper
	{
		public static string FormatConnectionString(string database, string host, string username, string password, string appName)
		{
			return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};Application Name={4};Persist Security Info=True",
				host, database, username, password, appName);
		}

		public static string FormatMySqlConnectionString(string database, string host, string username, string password, string appName)
		{
			//return string.Format("server={0};database={1};userid={2};password={3}",
			return string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
				host, database, username, password, appName);
		}
	}
}
