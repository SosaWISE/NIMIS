namespace SOS.Lib.Util
{
	public static class UnitTestingHelper
	{
		public static class CmsCORS
		{
			#region .ctor

			static CmsCORS()
			{
			}

			#endregion .ctor

			#region Properties
			public static bool IsActive { get; set; }
			public static object SseCmsUser { get; set; }
			public static string AppToken { get; set; }
			public static string IPAddress { get; set; }
			public static string Username { get; set; }
			public static string Password { get; set; }
			#endregion Properties
		}
	}
}
