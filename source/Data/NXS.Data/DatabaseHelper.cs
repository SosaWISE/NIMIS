using System;

namespace NXS.Data
{
	public static class DatabaseHelper
	{
		public static string FormatConnectionString(string host, string database, string username, string password, string appName)
		{
			return string.Format("Initial Catalog={0};Data Source={1};User ID={2};Password={3};Application Name={4};Persist Security Info=True",
				host, database, username, password, appName);
		}
	}
}
